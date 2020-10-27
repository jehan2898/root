using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for SearchReportBO
/// </summary>
public class SearchReportBO
{
    public SearchReportBO()
    {

    }
    public DataSet  searchbills(ReportDAO DAO)
    {
        DataSet ds1 = new DataSet();
        DataSet ds = new DataSet();
        SqlConnection conn1 = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());

        try
        {
            conn1.Open();
            SqlCommand cmd1 = new SqlCommand("select BT_REFERRING_FACILITY from MST_BILLING_COMPANY where SZ_COMPANY_ID='" + DAO.CompanyId + "'", conn1);
            cmd1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(ds1);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn1.Close();
        }
        string szQuery = "";
        if (ds1.Tables[0].Rows.Count > 0)
        {
            if (ds1.Tables[0].Rows[0]["BT_REFERRING_FACILITY"].ToString().Equals("False"))
            {                
                szQuery = "SELECT TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER, " +
                    " TXN_BILL_TRANSACTIONS.SZ_CASE_ID [SZ_CASE_ID], " +
                    " isnull(OFFI.SZ_PREFIX,'') + SZ_CASE_NO [SZ_CASE_NO], " +
                    " ISNULL(FLT_BILL_AMOUNT,0)[FLT_BILL_AMOUNT],  " +
                    " ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS  " +
                    " WHERE SZ_BILL_ID=TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER),0)[PAID_AMOUNT]," +
                    " ISNULL(FLT_BALANCE,0)[FLT_BALANCE]," +
                     " SZ_BILL_STATUS_NAME  [SZ_BILL_STATUS_NAME]," +
                     " SZ_DOCTOR_NAME  [SZ_DOCTOR_NAME]," +
                     " MST_OFFICE.SZ_OFFICE  [SZ_OFFICE], " +
                    " ISNULL(CONVERT(NVARCHAR(20),(SELECT MAX(DT_DATE_OF_SERVICE) FROM TXN_BILL_TRANSACTIONS_DETAIL WHERE SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER),101),'') [DT_VISIT_DATE]," +
                    "	(ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))[SZ_PATIENT_NAME],			" +
                   "isnull(SZ_VERIFICATION_LINK,'') [SZ_BILL_PATH]" +

                  " FROM TXN_BILL_TRANSACTIONS " +
                  " JOIN MST_BILL_STATUS ON MST_BILL_STATUS.SZ_BILL_STATUS_ID =TXN_BILL_TRANSACTIONS .SZ_BILL_STATUS_ID" +
                  " JOIN MST_DOCTOR ON MST_DOCTOR.SZ_DOCTOR_ID =TXN_BILL_TRANSACTIONS .SZ_DOCTOR_ID" +
                  " JOIN MST_OFFICE ON MST_OFFICE.SZ_OFFICE_ID =MST_DOCTOR.SZ_OFFICE_ID " +
                  " join mst_case_master on mst_case_master.sz_case_id=TXN_BILL_TRANSACTIONS.sz_case_id" +
                  " join MST_PATIENT  on mst_case_master.SZ_PATIENT_ID =MST_PATIENT.SZ_PATIENT_ID " +
                  " left join TXN_PATIENT_OFFICE on (TXN_PATIENT_OFFICE.SZ_PATIENT_ID=mst_case_master.SZ_PATIENT_ID)" +
                  " left join MST_OFFICE OFFI ON (OFFI.SZ_OFFICE_ID = TXN_PATIENT_OFFICE.SZ_OFFICE_ID)          " +
                  " left join (Select max(a.I_VERIFICATION_ID) [VeriId]," +
              "  a.SZ_BILL_NUMBER	from TXN_BILL_VERIFICATION a " +
               " Where SZ_COMPANY_ID='" + DAO.CompanyId + "' " +
               " and I_VERIFICATION_TYPE=1 " +
               " group by SZ_BILL_NUMBER) veri on(veri.SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)" +
                  " WHERE  TXN_BILL_TRANSACTIONS.SZ_COMPANY_ID='"+DAO.CompanyId+"'";

            }
            else
            {
                szQuery = "SELECT TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER,  " +
               " TXN_BILL_TRANSACTIONS.SZ_CASE_ID [SZ_CASE_ID], " +
                " isnull(OFFI.SZ_PREFIX,'') + SZ_CASE_NO [SZ_CASE_NO], " +
                " ISNULL(FLT_BILL_AMOUNT,0)[FLT_BILL_AMOUNT],  " +
                " ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS  " +
                " WHERE SZ_BILL_ID=TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER),0)[PAID_AMOUNT]," +
                " ISNULL(FLT_BALANCE,0)[FLT_BALANCE],                        " +
                 " SZ_BILL_STATUS_NAME  [SZ_BILL_STATUS_NAME],            " +
                 " SZ_DOCTOR_NAME  [SZ_DOCTOR_NAME]," +
                 " MST_OFFICE.SZ_OFFICE  [SZ_OFFICE], " +
                " ISNULL(CONVERT(NVARCHAR(20),(SELECT MAX(DT_DATE_OF_SERVICE) FROM TXN_BILL_TRANSACTIONS_DETAIL WHERE SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER),101),'') [DT_VISIT_DATE]			," +
                " (ISNULL(SZ_PATIENT_FIRST_NAME,'')  +' '+ ISNULL(SZ_PATIENT_LAST_NAME ,''))[SZ_PATIENT_NAME],			" +
              "isnull(SZ_VERIFICATION_LINK,'') [SZ_BILL_PATH]" +
              " FROM TXN_BILL_TRANSACTIONS " +
              " JOIN MST_BILL_STATUS ON MST_BILL_STATUS.SZ_BILL_STATUS_ID =TXN_BILL_TRANSACTIONS .SZ_BILL_STATUS_ID" +
              " JOIN MST_BILLING_DOCTOR ON MST_BILLING_DOCTOR.SZ_DOCTOR_ID =TXN_BILL_TRANSACTIONS .SZ_DOCTOR_ID" +
              " JOIN MST_OFFICE ON MST_OFFICE.SZ_OFFICE_ID =MST_BILLING_DOCTOR.SZ_OFFICE_ID " +
              " join mst_case_master on mst_case_master.sz_case_id=TXN_BILL_TRANSACTIONS.sz_case_id" +
              " join MST_PATIENT  on mst_case_master.SZ_PATIENT_ID =MST_PATIENT.SZ_PATIENT_ID " +
              " left join TXN_PATIENT_OFFICE on (TXN_PATIENT_OFFICE.SZ_PATIENT_ID=mst_case_master.SZ_PATIENT_ID)" +
              " left join MST_OFFICE OFFI ON (OFFI.SZ_OFFICE_ID = TXN_PATIENT_OFFICE.SZ_OFFICE_ID)          " +
              " left join (Select max(a.I_VERIFICATION_ID) [VeriId],"+
		      "  a.SZ_BILL_NUMBER	from TXN_BILL_VERIFICATION a "+
		       " Where SZ_COMPANY_ID='"+DAO.CompanyId+"' "+
		       " and I_VERIFICATION_TYPE=1 "+
		       " group by SZ_BILL_NUMBER) veri on(veri.SZ_BILL_NUMBER = TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)" +
              " WHERE  TXN_BILL_TRANSACTIONS.SZ_COMPANY_ID='"+DAO.CompanyId+"' ";
          
            }

            if ((!DAO.BillNo.Equals("")))
            {
                szQuery = szQuery + " AND TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER = '" + DAO.BillNo + "'";
            }
            if ((!DAO.Fromdate.ToString().Trim().Equals("") && (!DAO.Fromdate.ToString().Equals("1/1/0001 12:00:00 AM"))) && (!DAO.Todate.ToString().Equals("") && (!DAO.Todate.ToString().Trim().Equals("1/1/0001 12:00:00 AM"))))
            {
                szQuery = szQuery + " AND TXN_BILL_TRANSACTIONS.DT_BILL_DATE  between convert(datetime,'" + DAO.Fromdate + "', 101) and convert(datetime,'" + DAO.Todate + "', 101)";
            }
            
            if ((!DAO.PatientName.Equals("")))
            {
                szQuery = szQuery + "AND (MST_PATIENT.SZ_PATIENT_FIRST_NAME like '%" + DAO.PatientName + "%' OR MST_PATIENT.SZ_PATIENT_LAST_NAME like '%" + DAO.PatientName + "%')";
            }
            if ((!DAO.CaseNo.Equals("")))
            {
                szQuery = szQuery + " AND MST_CASE_MASTER.SZ_CASE_NO ='" + DAO.CaseNo + "'";
            }
            if ((!DAO.BillStatus.Equals("")) && (!DAO.BillStatusOr.Equals("")))
            {
                szQuery = szQuery + " AND (MST_BILL_STATUS.SZ_BILL_STATUS_CODE = '" + DAO.BillStatus +"' OR MST_BILL_STATUS.SZ_BILL_STATUS_CODE = '" + DAO.BillStatusOr + "' )";
            }
            else if((!DAO.BillStatus.Equals("")))
            {
                szQuery = szQuery + " AND (MST_BILL_STATUS.SZ_BILL_STATUS_CODE = '" + DAO.BillStatus + "')";
            }
            if (DAO.Days.Equals(""))
            {

            }
            else
            {                
                DateTime DT = new DateTime();
                DT = DateTime.Now;
                DT = DT.AddDays(-Convert.ToInt32(DAO.Days));
                szQuery = szQuery + " and veri.VeriId in (Select I_VERIFICATION_ID from TXN_BILL_VERIFICATION where  DT_VERIFICATION_DATE <= convert(datetime,'" + DT + "', 101) and SZ_COMPANY_ID='" + DAO.CompanyId + "')";
               // szQuery = szQuery + " AND veri.VeriDate <= convert(datetime,'" + DT + "', 101)";
            }

            szQuery = szQuery + " ORDER BY cast(substring(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER,3,(Len(TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER)-2)) as numeric(13)) ASC";
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(szQuery, conn);
                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            finally
            {
                conn.Close();
            }

        }
        return ds;

    }
}
