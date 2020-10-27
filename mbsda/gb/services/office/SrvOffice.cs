using System;
using System.Collections.Generic;
using System.Text;
using gbmodel = gb.mbs.da.model;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.service.util;

namespace gb.mbs.da.services.office
{
    public class SrvOffice
    {
        public List<gbmodel.physician.Physician> SelectReferringDoctor(gbmodel.account.Account p_oAccount, List<gbmodel.office.Office> p_lstOffice)
        {
            SqlConnection sqlConnection = null;

            DataTable oDTOffice = null;
            if (p_lstOffice != null && p_lstOffice.Count > 0)
            {
                oDTOffice = gbmodel.office.type.TypeOffice.FillDBType(p_lstOffice);
            }
            List<gbmodel.physician.Physician> oList = new List<gbmodel.physician.Physician>();

            try
            {

                sqlConnection = new SqlConnection(DBUtil.ConnectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = null;
                sqlCommand = new SqlCommand("sp_select_referring_doctor_for_office", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@sz_company_id", p_oAccount.ID));

                if (oDTOffice != null && oDTOffice.Rows.Count > 0)
                {
                    SqlParameter tvpParamOffice = sqlCommand.Parameters.AddWithValue(
                        "@tvp_office", oDTOffice);
                    tvpParamOffice.SqlDbType = SqlDbType.Structured;
                    tvpParamOffice.TypeName = gbmodel.office.type.TypeOffice.GetTypeName();
                }
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];
                        gbmodel.physician.Physician oElement = new gbmodel.physician.Physician();
                       
                        oElement.Name = dr["Description"].ToString();
                        oElement.ID = dr["CODE"].ToString();
                      
                        oList.Add(oElement);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return oList;
        }
    }
}
