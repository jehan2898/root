using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using gbmodel = gb.mbs.da.model;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.service.util;
using chmodel = gb.mbs.da.model.appointment.note.ch;

namespace gb.mbs.da.services.appointment.note.ch
{
    /**
        This class must be used to fetch or update CH note data required for an appointment
        Do not use this class to print a CH note PDF
    **/
    public class SrvCHNote
    {
        private string sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);
        /**
            Required input model fields: 
                gbmodel.appointment.Appointment.ID
                gbmodel.appointment.Appointment.Patient.Account.ID

            Returns the datatable with the 
        **/
        public DataTable SelectObjectiveFinding(gbmodel.appointment.Appointment p_oAppointment)
        {
            DataSet ds = null;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            DataTable tblMatrix = new DataTable();

            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("sp_select_event_objective_findings", connection);
                selectCommand.Parameters.AddWithValue("@i_event_id", p_oAppointment.ID);
                selectCommand.Parameters.AddWithValue("@sz_company_id", p_oAppointment.Patient.Account.ID);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandTimeout = 0;
                ds = new DataSet();
                new SqlDataAdapter(selectCommand).Fill(ds);

                //get the masters
                ArrayList alMasters = SelectFindingNameAndLocation(p_oAppointment.Patient.Account);
                if (alMasters != null)
                {
                    if (alMasters.Count != 3)
                    {
                        throw new Exception("Masters for objective findings are not set OR there was a technical problem fetching them");
                    }
                    else
                    {
                        List<chmodel.ObjectiveFinding> lstName = (List<chmodel.ObjectiveFinding>)alMasters[0];
                        List<chmodel.ObjectiveFinding> lstLocation = (List<chmodel.ObjectiveFinding>)alMasters[1];

                        tblMatrix.Columns.Add("Name");

                        foreach (chmodel.ObjectiveFinding of in lstLocation)
                        {
                            tblMatrix.Columns.Add(of.Location);
                        }

                        foreach (chmodel.ObjectiveFinding of in lstName)
                        {
                            DataRow row = tblMatrix.NewRow();
                            row["Name"] = of.Name;
                            tblMatrix.Rows.Add(row);
                            tblMatrix.AcceptChanges();
                        }
                    }
                }

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string szFindings = ds.Tables[0].Rows[0]["sz_objective_findings"].ToString();
                    string[] arrFinding = szFindings.Split(',');

                    DataTable tblIntersect = (DataTable) alMasters[2];
                    for (int i=0; i < arrFinding.Length; i++)
                    {
                        if (arrFinding[i] != "")
                        {
                            DataRow[] drLookup = tblIntersect.Select("ID = " + arrFinding[i]);
                            if (drLookup != null && drLookup.Length > 0)
                            {
                                string szName = drLookup[0]["sz_objective_findings_name"].ToString();
                                string szLocation = drLookup[0]["sz_objective_findings_location_name"].ToString();

                                DataRow[] drFound = tblMatrix.Select("Name = '" + szName + "'");
                                int iNameIndex = tblMatrix.Rows.IndexOf(drFound[0]);
                                int iLocationIndex = tblMatrix.Columns[szLocation].Ordinal;

                                tblMatrix.Rows[iNameIndex][iLocationIndex] = "1";
                            }
                        }
                    }
                }
            }
            finally
            {
                if(connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return tblMatrix;
        }

        /**
            Required input model fields:
            gbmobel.account.Account.ID

            The method returns an arraylist where index 0 is the master for obj finding name and index 1 is the master for
            obj finding location
        **/
        public ArrayList SelectFindingNameAndLocation(model.account.Account p_oAccount)
        {
            DataSet ds = null;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            ArrayList list = new ArrayList();

            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand();
                ds = new DataSet();
                SqlDataAdapter adapter_one = new SqlDataAdapter("exec sp_select_ch_objective_findings_name @sz_company_id='"+p_oAccount.ID+"' EXEC sp_select_ch_objective_findings_location @sz_company_id='"+p_oAccount.ID+ "' EXEC sp_select_objective_findings @sz_company_id='" + p_oAccount.ID + "'", connection);
                adapter_one.SelectCommand.CommandTimeout = 0;
                adapter_one.Fill(ds);

                gbmodel.appointment.Appointment oAppointment = new gbmodel.appointment.Appointment();
                oAppointment.Note = new gbmodel.appointment.note.Note();

                List <gbmodel.appointment.note.ch.ObjectiveFinding> oObjectiveFindingName = new List<gbmodel.appointment.note.ch.ObjectiveFinding>();
                List <gbmodel.appointment.note.ch.ObjectiveFinding> oObjectiveFindingLocation = new List<gbmodel.appointment.note.ch.ObjectiveFinding>();

                gbmodel.appointment.Appointment p_oAppointment = new gbmodel.appointment.Appointment();
                p_oAppointment.Note = new gbmodel.appointment.note.Note();
                p_oAppointment.Note.ObjectiveFinding = new List<gbmodel.appointment.note.ch.ObjectiveFinding>();

                gbmodel.appointment.note.ch.ObjectiveFinding ch_obj_name = new gbmodel.appointment.note.ch.ObjectiveFinding();
                gbmodel.appointment.note.ch.ObjectiveFinding ch_obj_location = new gbmodel.appointment.note.ch.ObjectiveFinding();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ch_obj_name = new gbmodel.appointment.note.ch.ObjectiveFinding();
                    ch_obj_name.ID = Convert.ToInt32(dr["i_id"]);
                    ch_obj_name.Name = dr["sz_objective_findings_name"].ToString();
                    oObjectiveFindingName.Add(ch_obj_name);
                }
                list.Add(oObjectiveFindingName);
                
                foreach (DataRow dr_location in ds.Tables[1].Rows)
                {
                    ch_obj_location = new gbmodel.appointment.note.ch.ObjectiveFinding();
                    ch_obj_location.ID = Convert.ToInt32(dr_location["i_id"]);
                    ch_obj_location.Location = dr_location["sz_objective_findings_location_name"].ToString();
                    oObjectiveFindingLocation.Add(ch_obj_location);
                }
                list.Add(oObjectiveFindingLocation);

                list.Add(ds.Tables[2]);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
            }
            return list;
        }

        /**
            Required input model fields: 
                gbmodel.appointment.Appointment.ID
                gbmodel.appointment.Appointment.Patient.Account.ID
                gbmodel.appointment.Appointment.User.ID
                gbmodel.appointment.Appointment.Note.ObjectiveFinding <List>

            Returns the datatable with the 
        **/
        public int Create(gbmodel.appointment.Appointment p_oAppointment)
        {
            SqlConnection sqlCon = new SqlConnection(DBUtil.ConnectionString);
            sqlCon.Open();
            int iRowsAffected = 0;

            try
            {
                string sSelectedFindings = "";

                foreach (gbmodel.appointment.note.ch.ObjectiveFinding of in p_oAppointment.Note.ObjectiveFinding)
                {
                    sSelectedFindings += of.ID + ",";
                }

                if(sSelectedFindings != null && sSelectedFindings.EndsWith(","))
                {
                    sSelectedFindings = sSelectedFindings.TrimEnd(',');
                }

                SqlCommand command = new SqlCommand("sp_create_event_objective_findings", sqlCon);
                command.Parameters.AddWithValue("@i_event_id", p_oAppointment.ID);
                command.Parameters.AddWithValue("@sz_objective_findings", sSelectedFindings);
                command.Parameters.AddWithValue("@sz_company_id", p_oAppointment.Patient.Account.ID);
                command.Parameters.AddWithValue("@sz_user_id", p_oAppointment.User.ID);
                command.CommandType = CommandType.StoredProcedure;
                iRowsAffected = command.ExecuteNonQuery();
            }
            finally
            {
                if(sqlCon != null)
                {
                    if (sqlCon.State == ConnectionState.Open)
                    {
                        sqlCon.Close();
                    }
                }
                sqlCon = null;
            }

            return iRowsAffected;
        }
        #region treatment plan
        public DataTable SelectTreatmentPlan(gbmodel.appointment.Appointment p_oAppointment)
        {
            DataSet ds = null;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            DataTable tblMatrix = new DataTable();

            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("sp_select_event_treatment_plan", connection);
                selectCommand.Parameters.AddWithValue("@i_event_id", p_oAppointment.ID);
                selectCommand.Parameters.AddWithValue("@sz_company_id", p_oAppointment.Patient.Account.ID);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.CommandTimeout = 0;
                ds = new DataSet();
                new SqlDataAdapter(selectCommand).Fill(ds);

                //get the masters
                ArrayList alMasters = SelectTreatmentPlanNameAndLocation(p_oAppointment.Patient.Account);
                if (alMasters != null)
                {
                    if (alMasters.Count != 3)
                    {
                        throw new Exception("Masters for treatment plan are not set OR there was a technical problem fetching them");
                    }
                    else
                    {
                        List<chmodel.TreatmentPlan> lstName = (List<chmodel.TreatmentPlan>)alMasters[0];
                        List<chmodel.TreatmentPlan> lstLocation = (List<chmodel.TreatmentPlan>)alMasters[1];

                        tblMatrix.Columns.Add("Name");

                        foreach (chmodel.TreatmentPlan of in lstLocation)
                        {
                            tblMatrix.Columns.Add(of.Location);
                        }

                        foreach (chmodel.TreatmentPlan of in lstName)
                        {
                            DataRow row = tblMatrix.NewRow();
                            row["Name"] = of.Name;
                            tblMatrix.Rows.Add(row);
                            tblMatrix.AcceptChanges();
                        }
                    }
                }

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string szTreatments = ds.Tables[0].Rows[0]["sz_treatment_plan"].ToString();
                    string[] arrTreatments = szTreatments.Split(',');

                    DataTable tblIntersect = (DataTable)alMasters[2];
                    for (int i = 0; i < arrTreatments.Length; i++)
                    {
                        if (arrTreatments[i] != "")
                        {
                            DataRow[] drLookup = tblIntersect.Select("ID = " + arrTreatments[i]);
                            if (drLookup != null && drLookup.Length > 0)
                            {
                                string szName = drLookup[0]["sz_treatment_plan_name"].ToString();
                                string szLocation = drLookup[0]["sz_treatment_plan_location_name"].ToString();

                                DataRow[] drFound = tblMatrix.Select("Name = '" + szName + "'");
                                int iNameIndex = tblMatrix.Rows.IndexOf(drFound[0]);
                                int iLocationIndex = tblMatrix.Columns[szLocation].Ordinal;

                                tblMatrix.Rows[iNameIndex][iLocationIndex] = "1";
                            }
                        }
                    }
                }
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return tblMatrix;
        }
        public ArrayList SelectTreatmentPlanNameAndLocation(model.account.Account p_oAccount)
        {
            DataSet ds = null;
            SqlConnection connection = new SqlConnection(DBUtil.ConnectionString);
            ArrayList list = new ArrayList();

            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand();
                ds = new DataSet();
                SqlDataAdapter adapter_one = new SqlDataAdapter("exec sp_select_ch_treatment_plan_name @sz_company_id='" + p_oAccount.ID + "' EXEC sp_select_ch_treatment_plan_location @sz_company_id='" + p_oAccount.ID + "' EXEC sp_select_treatment_plan @sz_company_id='" + p_oAccount.ID + "'", connection);
                adapter_one.SelectCommand.CommandTimeout = 0;
                adapter_one.Fill(ds);

                gbmodel.appointment.Appointment oAppointment = new gbmodel.appointment.Appointment();
                oAppointment.Note = new gbmodel.appointment.note.Note();

                List<gbmodel.appointment.note.ch.TreatmentPlan> oTreatementPlanName = new List<gbmodel.appointment.note.ch.TreatmentPlan>();
                List<gbmodel.appointment.note.ch.TreatmentPlan> oTreatmentPlanLocation = new List<gbmodel.appointment.note.ch.TreatmentPlan>();

                gbmodel.appointment.Appointment p_oAppointment = new gbmodel.appointment.Appointment();
                p_oAppointment.Note = new gbmodel.appointment.note.Note();
                p_oAppointment.Note.TreatmentPlan = new List<gbmodel.appointment.note.ch.TreatmentPlan>();

                gbmodel.appointment.note.ch.TreatmentPlan ch_plan_name = new gbmodel.appointment.note.ch.TreatmentPlan();
                gbmodel.appointment.note.ch.TreatmentPlan ch_plan_location = new gbmodel.appointment.note.ch.TreatmentPlan();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ch_plan_name = new gbmodel.appointment.note.ch.TreatmentPlan();
                    ch_plan_name.ID = Convert.ToInt32(dr["i_id"]);
                    ch_plan_name.Name = dr["sz_treatment_plan_name"].ToString();
                    oTreatementPlanName.Add(ch_plan_name);
                }
                list.Add(oTreatementPlanName);

                foreach (DataRow dr_location in ds.Tables[1].Rows)
                {
                    ch_plan_location = new gbmodel.appointment.note.ch.TreatmentPlan();
                    ch_plan_location.ID = Convert.ToInt32(dr_location["i_id"]);
                    ch_plan_location.Location = dr_location["sz_treatment_plan_location_name"].ToString();
                    oTreatmentPlanLocation.Add(ch_plan_location);
                }
                list.Add(oTreatmentPlanLocation);

                list.Add(ds.Tables[2]);
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection = null;
                }
            }
            return list;
        }

        public int CreateTreatment(gbmodel.appointment.Appointment p_oAppointment)
        {
            SqlConnection sqlCon = new SqlConnection(DBUtil.ConnectionString);
            sqlCon.Open();
            int iRowsAffected = 0;

            try
            {
                string sSelectedFindings = "";

                foreach (gbmodel.appointment.note.ch.TreatmentPlan of in p_oAppointment.Note.TreatmentPlan)
                {
                    sSelectedFindings += of.ID + ",";
                }

                if (sSelectedFindings != null && sSelectedFindings.EndsWith(","))
                {
                    sSelectedFindings = sSelectedFindings.TrimEnd(',');
                }

                SqlCommand command = new SqlCommand("sp_create_event_treatment_plan", sqlCon);
                command.Parameters.AddWithValue("@i_event_id", p_oAppointment.ID);
                command.Parameters.AddWithValue("@sz_treatment_plan", sSelectedFindings);
                command.Parameters.AddWithValue("@sz_company_id", p_oAppointment.Patient.Account.ID);
                command.Parameters.AddWithValue("@sz_user_id", p_oAppointment.User.ID);
                command.CommandType = CommandType.StoredProcedure;
                iRowsAffected = command.ExecuteNonQuery();
            }
            finally
            {
                if (sqlCon != null)
                {
                    if (sqlCon.State == ConnectionState.Open)
                    {
                        sqlCon.Close();
                    }
                }
                sqlCon = null;
            }

            return iRowsAffected;
        }
        #endregion
    }
}