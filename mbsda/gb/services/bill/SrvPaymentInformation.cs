using System;
using System.Data;
using System.Data.SqlClient;

namespace gb.mbs.da.services.bill
{
    public class SrvPaymentInformation
    {
        private string sSQLCon = string.Empty;
        public SrvPaymentInformation()
        {
            this.sSQLCon = dataaccess.ConnectionManager.GetConnectionString(null);
        }

        public model.bill.Bill GetPaymentInfo(string BillNo,string CompanyID)
        {
            model.bill.Bill p_oBill = new model.bill.Bill();
            DataSet ds;
            SqlCommand sqlCmd;
            SqlDataAdapter sqlDa;            
            SqlConnection sqlCon = new SqlConnection(sSQLCon);
           
            try
            {
                 sqlCon.Open();
                 sqlCmd = new SqlCommand("sp_mbs_get_payment_information", sqlCon);
                 sqlCmd.CommandType = CommandType.StoredProcedure;                
                 sqlCmd.CommandTimeout = 0;
                 sqlCmd.Parameters.AddWithValue("@sz_bill_number", BillNo);
                 sqlCmd.Parameters.AddWithValue("@sz_company_id", CompanyID);                
                 sqlDa = new SqlDataAdapter(sqlCmd);
                 ds = new DataSet();
                 sqlDa.Fill(ds);
                 if (ds.Tables.Count > 0)
                 {
                     if (ds.Tables[0].Rows.Count > 0)
                     {
                         p_oBill.Number = ds.Tables[0].Rows[0]["SZ_BILL_NUMBER"].ToString();
                         p_oBill.Patient = new model.patient.Patient();
                         p_oBill.Patient.CaseID = Convert.ToInt32(ds.Tables[0].Rows[0]["SZ_CASE_ID"].ToString());
                         p_oBill.Patient.CaseNo = Convert.ToInt32(ds.Tables[0].Rows[0]["SZ_CASE_NO"].ToString());
                         p_oBill.Specialty = new model.specialty.Specialty();
                         p_oBill.Specialty.ID = ds.Tables[0].Rows[0]["SZ_SPECIALITY_ID"].ToString();
                         p_oBill.OutstandingAmount = Convert.ToDouble(ds.Tables[0].Rows[0]["FLT_BALANCE"].ToString());
                         p_oBill.FirstVisitDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_FIRST_VISIT_DATE"].ToString());
                         p_oBill.LastVisitDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_LAST_VISIT_DATE"].ToString());
                     }
                 }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if(sqlCon.State==ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return p_oBill;
        }
    }
}
