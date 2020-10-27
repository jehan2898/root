using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using gb.mbs.da.service.util;
using gbmodel = gb.mbs.da.model;
using System.Configuration;

namespace gb.mbs.da.services.bill
{
    public class SrvBill
    {
        public DataSet GetBillDenialDetails(DataTable dataTable, List<gbmodel.bill.Bill> p_oBill, model.account.Account p_oAccount)
        {
            DataSet dataSet = null;
            SqlConnection oConnection = new SqlConnection(DBUtil.ConnectionString);
            oConnection.Open();

            DataTable oDTBill = null;
            if (p_oBill != null && p_oBill.Count > 0)
            {
                oDTBill = gbmodel.bill.type.TypeBill.FillDBType(p_oBill);
            }
            try
            {
                SqlCommand sqlCmd = new SqlCommand("sp_get_bill_denial_description", oConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tvpParamPhysician = sqlCmd.Parameters.AddWithValue(
                            "@typ_bill", oDTBill);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                sqlCmd.ExecuteNonQuery();
                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                dataSet = new DataSet();
                sqlda.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                }
            }
        }

        public DataSet GetBilVerificationlDetails(DataTable dataTable, List<gbmodel.bill.Bill> p_oBill, model.account.Account p_oAccount)
        {
            DataSet dataSet = null;
            SqlConnection oConnection = new SqlConnection(DBUtil.ConnectionString);
            oConnection.Open();

            DataTable oDTBill = null;
            if (p_oBill != null && p_oBill.Count > 0)
            {
                oDTBill = gbmodel.bill.type.TypeBill.FillDBType(p_oBill);
            }
            try
            {
                SqlCommand sqlCmd = new SqlCommand("sp_get_verification_received_description", oConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tvpParamPhysician = sqlCmd.Parameters.AddWithValue(
                            "@typ_bill", oDTBill);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                sqlCmd.ExecuteNonQuery();
                SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
                dataSet = new DataSet();
                sqlda.Fill(dataSet);
                return dataSet;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (oConnection.State == ConnectionState.Open)
                {
                    oConnection.Close();
                }
            }
        }

        public int DeleteVerificationDescription(gbmodel.bill.Bill p_oBill, gbmodel.bill.verification.Verification p_oVerification, model.account.Account p_oAccount)
        {
            string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            SqlConnection sqlCon = new SqlConnection(strConn);
            int i = 0;
            try
            {
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("sp_delete_verification_received_details", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@sz_bill_number", p_oBill.Number);
                sqlCmd.Parameters.AddWithValue("@i_verification_id", p_oVerification.Id);
                sqlCmd.Parameters.AddWithValue("@sz_company_id", p_oAccount.ID);
                i = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
            return i;
        }
    }
}
