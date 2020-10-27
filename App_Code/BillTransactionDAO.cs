using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using log4net;
using System.Xml;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

/// <summary>
/// Summary description for BillTransactionDAO
/// </summary>
public class BillTransactionDAO
{

    String strsqlCon;
    SqlConnection conn;
    SqlCommand comm;
    Bill_Sys_BillingCompanyDetails_BO _bill_Sys_BillingCompanyDetails_BO;
    private static ILog log = LogManager.GetLogger("BillTransactionDAO");
    String szLatestBillNumber = "";

    public BillTransactionDAO()
    {
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    }

    public void DeleteBillRecord(string p_szBillID, string p_szCompanyID, string p_szStatusOrDelete)
    {
        this.conn = new SqlConnection(this.strsqlCon);
        try
        {
            try
            {
                this.conn.Open();
                this.comm = new SqlCommand("SP_DELETE_BILL_RECORD", this.conn)
                {
                    CommandTimeout = 0
                };
                this.comm.Parameters.AddWithValue("@SZ_BILL_ID", p_szBillID);
                this.comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
                this.comm.Parameters.AddWithValue("@CHANGE_STATUS_DELETE", p_szStatusOrDelete);
                this.comm.Parameters.AddWithValue("@FLAG", "");
                this.comm.CommandTimeout = 0;
                this.comm.CommandType = CommandType.StoredProcedure;
                this.comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        finally
        {

        }
    }
    public Result SaveBillTransaction(BillTransactionEO p_objBillTranEO, ArrayList p_objEvent, ArrayList p_objALEventRefferProcedure, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Information."

            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", p_objBillTranEO.SZ_BILL_ID);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@FLT_BILL_AMOUNT", p_objBillTranEO.FLT_BILL_AMOUNT);
            comm.Parameters.AddWithValue("@FLT_BALANCE", p_objBillTranEO.FLT_BALANCE);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objBillTranEO.SZ_USER_ID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
            #endregion

            // get the Latest Bill Number


            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();


            #region "Save Event Reffer Procedure"
            string szIsRefferal = "false";
            //check doctor is refferal or not. if doctor is refferal then do not add procedure codes in the TXN_CALENDER_EVENT_PRPCEDURE table -- anand
            if (p_objALEventRefferProcedure != null)
            {
                if (p_objALEventRefferProcedure.Count > 0)
                {
                    EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                    objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[0];
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "SP_CHECK_DOCTOR_ISREFFERAL";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                    SqlDataReader dr1 = comm.ExecuteReader();
                    while (dr1.Read())
                    {
                        szIsRefferal = Convert.ToString(dr1[0]);
                    }
                    dr1.Close();
                }
            }
            if (szIsRefferal.ToLower() == "false")
            {
                if (p_objALEventRefferProcedure != null)
                {
                    if (p_objALEventRefferProcedure.Count > 0)
                    {
                        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                        {
                            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                            comm = new SqlCommand();
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandText = "SP_SAVE_PROCEDURECODE_FOR_BILL";
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Connection = conn;
                            comm.Transaction = transaction;
                            comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                            comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                            comm.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                if (p_objALEventRefferProcedure != null)
                {
                    if (p_objALEventRefferProcedure.Count > 0)
                    {
                        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                        {
                            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                            comm = new SqlCommand();
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandText = "SP_DELETE_PROC_CODE_USING_EVENT_ID";
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Connection = conn;
                            comm.Transaction = transaction;

                            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);

                            comm.ExecuteNonQuery();
                        }
                    }
                }

                if (p_objALEventRefferProcedure != null)
                {
                    if (p_objALEventRefferProcedure.Count > 0)
                    {
                        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                        {
                            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                            comm = new SqlCommand();
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandText = "SP_SAVE_PROCEDURECODE_FOR_BILL";
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Connection = conn;
                            comm.Transaction = transaction;
                            comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                            comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                            comm.ExecuteNonQuery();
                        }
                    }
                }
                //if (p_objALEventRefferProcedure != null)
                //{
                //    if (p_objALEventRefferProcedure.Count > 0)
                //    {
                //        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                //        {
                //            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                //            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                //            comm = new SqlCommand();
                //            comm.CommandText = "SP_SAVE_PROCEDURECODE_FOR_BILL";
                //            comm.CommandType = CommandType.StoredProcedure;
                //            comm.Connection = conn;
                //            comm.Transaction = transaction;
                //            comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                //            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                //            comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                //            comm.Parameters.AddWithValue("@flag", "Update");
                //            comm.ExecuteNonQuery();
                //        }
                //    }
                //}
            }

            #endregion

            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());
                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        comm.Parameters.AddWithValue("@FLAG", "ADD");
                        comm.ExecuteNonQuery();
                        dr.Close();
                    }
                }
            }

            #endregion

            #region "Update Event."

            if (p_objEvent != null)
            {
                if (p_objEvent.Count > 0)
                {
                    for (int i = 0; i < p_objEvent.Count; i++)
                    {
                        EventEO objEventEO = new EventEO();
                        objEventEO = (EventEO)p_objEvent[i];

                        #region "Check bill is generated for given event id"

                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Transaction = transaction;
                        comm.CommandText = "CHECK_EVENT_BILLED_STATUS";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Parameters.AddWithValue("@I_EVENT_ID", objEventEO.I_EVENT_ID);
                        SqlDataReader dr2 = comm.ExecuteReader();
                        while (dr2.Read())
                        {
                            string szMsg = "";
                            szMsg = Convert.ToString(dr2["msg"]);
                            if (szMsg == "Error")
                            {
                                objResult.msg_code = "ERR";
                                objResult.msg = "You can not add multiple bill for same visit.";
                                objResult.bill_no = "";
                                dr2.Close();
                                dr.Close();
                                transaction.Rollback();
                                return objResult;
                            }
                        }
                        dr2.Close();
                        #endregion



                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Transaction = transaction;
                        comm.CommandText = "UPDATE_EVENT_STATUS";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Parameters.AddWithValue("@I_EVENT_ID", objEventEO.I_EVENT_ID);
                        comm.Parameters.AddWithValue("@BT_STATUS", objEventEO.BT_STATUS);
                        comm.Parameters.AddWithValue("@I_STATUS", objEventEO.I_STATUS);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_BILL_DATE", objEventEO.DT_BILL_DATE);
                        comm.ExecuteNonQuery();
                    }
                }
            }
            #endregion


            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }
    ServerConnection svrConnection = null;
    Server server = null;
    public ServerConnection BeginBillTranaction()
    {



        conn = new SqlConnection(strsqlCon);
        conn.Open();
        svrConnection = new ServerConnection(conn);
        server = new Server(svrConnection);


        // server.ConnectionContext.BeginTransaction();
        return server.ConnectionContext;
    }


    public void CommitBillTranaction()
    {
        server.ConnectionContext.CommitTransaction();
        conn.Close();

    }


    public void RollBackBillTranaction()
    {

        server.ConnectionContext.RollBackTransaction();
        conn.Close();

    }
    //public Result SaveBillTransactions(BillTransactionEO p_objBillTranEO, ArrayList p_objEvent, ArrayList p_objALEventRefferProcedure, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode)
    //{
    //    string Query = "";

    //    Result objResult = new Result();
    //    // BeginBillTranaction();
    //    try
    //    {
    //        // server.ConnectionContext.BeginTransaction();
    //        #region "Save Bill Information."
    //        Query = "";
    //        Query = Query + "Exec SP_TXN_BILL_TRANSACTIONS_NEW ";

    //        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_ID", p_objBillTranEO.SZ_BILL_ID, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@FLT_BILL_AMOUNT", p_objBillTranEO.FLT_BILL_AMOUNT, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@FLT_BALANCE", p_objBillTranEO.FLT_BALANCE, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_USER_ID", p_objBillTranEO.SZ_USER_ID, ",");
    //        Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "ADD", ",");

    //        Query = Query.TrimEnd(',');


    //        var dr = server.ConnectionContext.ExecuteReader(Query);
    //        while (dr.Read())
    //        {
    //            szLatestBillNumber = Convert.ToString(dr[0]);
    //        }
    //        dr.Close();
    //        #endregion

    //        //#region Get Latest Bill Number
    //        //// get the Latest Bill Number
    //        //Query = "";
    //        //Query = Query + "Exec SP_TXN_LATEST_BILL_TRANSACTIONS ";
    //        //Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "GETLATESTBILLID", ",");
    //        //Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID, ",");

    //        //Query = Query.TrimEnd(',');



    //        //var dr = server.ConnectionContext.ExecuteReader(Query);
    //        //while (dr.Read())
    //        //{
    //        //    szLatestBillNumber = Convert.ToString(dr[0]);
    //        //}
    //        //dr.Close();
    //        //server.ConnectionContext.CommitTransaction();
    //        //#endregion

    //        #region Check IsReferrel
    //        string szIsRefferal = "false";
    //        //check doctor is refferal or not. if doctor is refferal then do not add procedure codes in the TXN_CALENDER_EVENT_PRPCEDURE table -- anand
    //        if (p_objALEventRefferProcedure != null)
    //        {
    //            if (p_objALEventRefferProcedure.Count > 0)
    //            {
    //                EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
    //                objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[0];
    //                Query = "";
    //                Query = Query + "Exec SP_CHECK_DOCTOR_ISREFFERAL ";
    //                Query = Query + string.Format("{0}={1}{2}", "@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID, ",");

    //                Query = Query.TrimEnd(',');



    //                var dr1 = server.ConnectionContext.ExecuteReader(Query);
    //                while (dr1.Read())
    //                {
    //                    szIsRefferal = Convert.ToString(dr1[0]);
    //                }
    //                dr1.Close();
    //            }
    //        }
    //        #endregion

    //        #region "Save Event Reffer Procedure"
    //        if (szIsRefferal.ToLower() == "false")
    //        {
    //            if (p_objALEventRefferProcedure != null)
    //            {
    //                if (p_objALEventRefferProcedure.Count > 0)
    //                {
    //                    Query = "";
    //                    for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
    //                    {
    //                        Query = Query + "Exec SP_SAVE_PROCEDURECODE_FOR_BILL ";
    //                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
    //                        objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];

    //                        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE, ",");
    //                        Query = Query + string.Format("{0}={1}{2}", "@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID, ",");
    //                        Query = Query + string.Format("{0}={1}{2}", "@I_STATUS", objEventRefferProcedureEO.I_STATUS, ",");
    //                        if (objEventRefferProcedureEO.SZ_MODIFIER_ID != null)
    //                        {
    //                            if (objEventRefferProcedureEO.SZ_MODIFIER_ID != "&nbsp;" && objEventRefferProcedureEO.SZ_MODIFIER_ID != "" && objEventRefferProcedureEO.SZ_MODIFIER_ID.ToLower() != "na")
    //                            {
    //                                Query = Query + string.Format("{0}={1}{2}", "@SZ_MULTI_MODIFIER", objEventRefferProcedureEO.SZ_MODIFIER_ID, ",");
    //                            }
    //                        }
    //                        Query = Query.TrimEnd(',');
    //                        Query = Query + Environment.NewLine;
    //                        Query = Query + "GO";
    //                        Query = Query + Environment.NewLine;
    //                    }


    //                    server.ConnectionContext.ExecuteNonQuery(Query);
    //                }
    //            }
    //        }
    //        else
    //        {

    //            if (p_objALEventRefferProcedure != null)
    //            {
    //                if (p_objALEventRefferProcedure.Count > 0)
    //                {
    //                    Query = "";
    //                    for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
    //                    {
    //                        Query = Query + "Exec SP_DELETE_PROC_CODE_USING_EVENT_ID ";
    //                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
    //                        objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];

    //                        Query = Query + string.Format("{0}={1}{2}", "@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID, ",");
    //                        Query = Query.TrimEnd(',');
    //                        Query = Query + Environment.NewLine;
    //                        Query = Query + "GO";
    //                        Query = Query + Environment.NewLine;
    //                    }


    //                    server.ConnectionContext.ExecuteNonQuery(Query);
    //                }
    //            }

    //            if (p_objALEventRefferProcedure != null)
    //            {
    //                if (p_objALEventRefferProcedure.Count > 0)
    //                {
    //                    Query = "";
    //                    for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
    //                    {
    //                        Query = Query + "Exec SP_SAVE_PROCEDURECODE_FOR_BILL ";
    //                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
    //                        objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];

    //                        Query = Query + string.Format("{0}='{1}'{2}", "@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE, ",");
    //                        Query = Query + string.Format("{0}={1}{2}", "@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID, ",");
    //                        Query = Query + string.Format("{0}={1}{2}", "@I_STATUS", objEventRefferProcedureEO.I_STATUS, ",");
    //                        if (objEventRefferProcedureEO.SZ_MODIFIER_ID != null)
    //                        {
    //                            if (objEventRefferProcedureEO.SZ_MODIFIER_ID != "&nbsp;" && objEventRefferProcedureEO.SZ_MODIFIER_ID != "" && objEventRefferProcedureEO.SZ_MODIFIER_ID.ToLower() != "na")
    //                            {
    //                                Query = Query + string.Format("{0}={1}{2}", "@SZ_MULTI_MODIFIER", objEventRefferProcedureEO.SZ_MODIFIER_ID, ",");

    //                            }
    //                        }
    //                        Query = Query.TrimEnd(',');
    //                        Query = Query + Environment.NewLine;
    //                        Query = Query + "GO";
    //                        Query = Query + Environment.NewLine;
    //                    }

    //                    server.ConnectionContext.ExecuteNonQuery(Query);
    //                }
    //            }
    //        }

    //        #endregion

    //        #region "Save Diagnosis Code."

    //        if (p_objALDiagCode != null)
    //        {
    //            if (p_objALDiagCode.Count > 0)
    //            {
    //                Query = "";
    //                for (int i = 0; i < p_objALDiagCode.Count; i++)
    //                {
    //                    Query = Query + "Exec SP_TXN_BILL_TRANSACTIONS_DETAIL ";
    //                    BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
    //                    objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];

    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szLatestBillNumber, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@FLAG", "ADDBILLDIAGNOSIS", ",");
    //                    Query = Query.TrimEnd(',');
    //                    Query = Query + Environment.NewLine;
    //                    Query = Query + "GO";
    //                    Query = Query + Environment.NewLine;
    //                }

    //                server.ConnectionContext.ExecuteNonQuery(Query);
    //            }
    //        }

    //        #endregion

    //        #region "Save procedure codes."

    //        if (p_objProcedureCodes != null)
    //        {
    //            if (p_objProcedureCodes.Count > 0)
    //            {
    //                Query = "";
    //                for (int i = 0; i < p_objProcedureCodes.Count; i++)
    //                {
    //                    Query = Query + "Exec SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW ";
    //                    BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

    //                    objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];

    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szLatestBillNumber, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@I_UNIT", objBillProcedureCodeEO.I_UNIT, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID, ",");
    //                    if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
    //                    {
    //                        Query = Query + string.Format("{0}={1}{2}", "@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString(), ",");
    //                    }
    //                    if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
    //                    {
    //                        Query = Query + string.Format("{0}={1}{2}", "@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString(), ",");
    //                    }
    //                    if (objBillProcedureCodeEO.SZ_MODIFIER_ID != null)
    //                    {
    //                        if (objBillProcedureCodeEO.SZ_MODIFIER_ID != "&nbsp;" && objBillProcedureCodeEO.SZ_MODIFIER_ID != "" && objBillProcedureCodeEO.SZ_MODIFIER_ID != "NA")
    //                        {
    //                            Query = Query + string.Format("{0}={1}{2}", "@SZ_MULTI_MODIFIER", objBillProcedureCodeEO.SZ_MODIFIER_ID.ToString(), ",");
    //                        }
    //                    }
    //                    Query = Query + string.Format("{0}={1}{2}", "@FLAG", "'" + "ADD" + "'", ",");
    //                    Query = Query.TrimEnd(',');
    //                    Query = Query + Environment.NewLine;
    //                    Query = Query + "GO";
    //                    Query = Query + Environment.NewLine;
    //                }


    //                server.ConnectionContext.ExecuteNonQuery(Query);
    //            }
    //        }

    //        #endregion

    //        #region "Update Event."

    //        if (p_objEvent != null)
    //        {
    //            if (p_objEvent.Count > 0)
    //            {
    //                Query = "";
    //                for (int i = 0; i < p_objEvent.Count; i++)
    //                {
    //                    Query = Query + "Exec UPDATE_EVENT_STATUS ";
    //                    EventEO objEventEO = new EventEO();
    //                    objEventEO = (EventEO)p_objEvent[i];

    //                    #region "Check bill is generated for given event id"
    //                    string Query1 = "";
    //                    Query1 = Query1 + "Exec CHECK_EVENT_BILLED_STATUS ";
    //                    Query1 = Query1 + string.Format("{0}={1}{2}", "@I_EVENT_ID", objEventEO.I_EVENT_ID, ",");
    //                    Query1 = Query1.TrimEnd(',');


    //                    var dr2 = server.ConnectionContext.ExecuteReader(Query1);
    //                    while (dr2.Read())
    //                    {
    //                        string szMsg = "";
    //                        szMsg = Convert.ToString(dr2["msg"]);
    //                        if (szMsg == "Error")
    //                        {
    //                            objResult.msg_code = "ERR";
    //                            objResult.msg = "You can not add multiple bill for same visit.";
    //                            objResult.bill_no = "";
    //                            dr2.Close();
    //                            return objResult;
    //                        }
    //                    }
    //                    dr2.Close();
    //                    #endregion

    //                    Query = Query + string.Format("{0}={1}{2}", "@I_EVENT_ID", objEventEO.I_EVENT_ID, ",");
    //                    Query = Query + string.Format("{0}={1}{2}", "@BT_STATUS", objEventEO.BT_STATUS, ",");
    //                    Query = Query + string.Format("{0}={1}{2}", "@I_STATUS", objEventEO.I_STATUS, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szLatestBillNumber, ",");
    //                    Query = Query + string.Format("{0}='{1}'{2}", "@DT_BILL_DATE", objEventEO.DT_BILL_DATE, ",");

    //                    Query = Query.TrimEnd(',');
    //                    Query = Query + Environment.NewLine;
    //                    Query = Query + "GO";
    //                    Query = Query + Environment.NewLine;
    //                }


    //                server.ConnectionContext.ExecuteNonQuery(Query);

    //            }
    //        }
    //        #endregion

    //        #region Check Daily Limit
    //        Query = "";
    //        for (int i = 0; i < p_objEvent.Count; i++)
    //        {
    //            Query = Query + "Exec SP_CHECK_DAILY_LIMIT ";
    //            EventEO objEventEO = new EventEO();
    //            objEventEO = (EventEO)p_objEvent[i];

    //            Query = Query + string.Format("{0}={1}{2}", "@I_EVENT_ID", objEventEO.I_EVENT_ID, ",");
    //            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_BILL_NUMBER", szLatestBillNumber, ",");
    //            Query = Query + string.Format("{0}='{1}'{2}", "@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID, ",");
    //            Query = Query.TrimEnd(',');
    //            Query = Query + Environment.NewLine;
    //            Query = Query + "GO";
    //            Query = Query + Environment.NewLine;
    //        }


    //        server.ConnectionContext.ExecuteNonQuery(Query);
    //        #endregion

    //        objResult.bill_no = szLatestBillNumber;
    //        objResult.msg = "Sccuess";
    //        objResult.msg_code = "SCC";



    //        // CommitBillTranaction();


    //    }
    //    catch (Exception ex)
    //    {
    //        objResult.bill_no = szLatestBillNumber;
    //        objResult.msg = ex.Message;
    //        objResult.msg_code = "ERR";
    //        // throw ex;

    //        //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    }
    //    finally
    //    {
    //        // conn.Close();
    //    }

    //    return objResult;
    //}


    public Result SaveBillTransactions(BillTransactionEO p_objBillTranEO, ArrayList p_objEvent, ArrayList p_objALEventRefferProcedure, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode)
    {
        string Query = "";

        Result objResult = new Result();

        try
        {

            Query = @"declare @Events CalEvents
                      declare @Refevent  Refevent
                      declare @Procedurecodes Procedurecodes
                      declare @DiagnosisCode DiagnosisCode
                      declare @TransactionData TransactionData";
            Query = Query + Environment.NewLine;

            Query = Query + String.Format("insert into @TransactionData values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'){8}", p_objBillTranEO.SZ_BILL_ID, p_objBillTranEO.SZ_BILL_NUMBER, p_objBillTranEO.SZ_CASE_ID, p_objBillTranEO.FLT_BILL_AMOUNT, p_objBillTranEO.FLT_BALANCE, p_objBillTranEO.SZ_COMPANY_ID, p_objBillTranEO.SZ_DOCTOR_ID, p_objBillTranEO.SZ_USER_ID, ",");
            Query = Query.TrimEnd(',');
            Query = Query + Environment.NewLine;

            if (p_objEvent != null)
            {
                if (p_objEvent.Count > 0)
                {

                   
                    for (int i = 0; i < p_objEvent.Count; i++)
                    {
                        EventEO Event = (p_objEvent[i] as EventEO);
                        Query = Query + String.Format("insert into @Events values({0},{1},{2},'{3}'){4}", Event.I_EVENT_ID, Event.BT_STATUS, Event.I_STATUS, Event.DT_BILL_DATE, ",");
                        Query = Query.TrimEnd(',');
                        Query = Query + Environment.NewLine;

                    }
                }
            }

            if (p_objALEventRefferProcedure != null)
            {
                if (p_objALEventRefferProcedure.Count > 0)
                {

                    for (int i = 0; i < p_objEvent.Count; i++)
                    {
                        EventRefferProcedureEO refEvent = (p_objALEventRefferProcedure[i] as EventRefferProcedureEO);
                        Query = Query + String.Format("insert into @Refevent values({0},{1},'{2}','{3}'){4}", refEvent.I_EVENT_ID, refEvent.I_STATUS, refEvent.SZ_PROC_CODE, refEvent.SZ_MODIFIER_ID, ",");
                        Query = Query.TrimEnd(',');
                        Query = Query + Environment.NewLine;

                    }


                }
            }

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {

                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO diagnosis = (p_objALDiagCode[i] as BillDiagnosisCodeEO);
                        Query = Query + String.Format("insert into @DiagnosisCode values('{0}'){1}", diagnosis.SZ_DIAGNOSIS_CODE_ID, ",");
                        Query = Query.TrimEnd(',');
                        Query = Query + Environment.NewLine;


                    }


                }
            }
            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {

                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {


                        BillProcedureCodeEO objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        Query = Query + String.Format("insert into @Procedurecodes values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'){11}", i + 1, objBillProcedureCodeEO.SZ_PROCEDURE_ID, objBillProcedureCodeEO.DT_DATE_OF_SERVICE, objBillProcedureCodeEO.SZ_COMPANY_ID, objBillProcedureCodeEO.I_UNIT, objBillProcedureCodeEO.FLT_PRICE, objBillProcedureCodeEO.SZ_DOCTOR_ID, objBillProcedureCodeEO.SZ_TYPE_CODE_ID, objBillProcedureCodeEO.I_GROUP_AMOUNT_ID, objBillProcedureCodeEO.FLT_GROUP_AMOUNT, objBillProcedureCodeEO.SZ_MODIFIER_ID, ",");
                        Query = Query.TrimEnd(',');
                        Query = Query + Environment.NewLine;


                    }




                }
            }

            Query = Query + "exec sp_save_BillTransaction @TransactionData,@Events,@Refevent,@Procedurecodes,@DiagnosisCode";
            Query = Query + Environment.NewLine;

            DataSet ds = server.ConnectionContext.ExecuteWithResults(Query);
            objResult.bill_no = ds.Tables[0].Rows[0][0].ToString();
            objResult.msg = ds.Tables[0].Rows[0][2].ToString();
            objResult.msg_code = ds.Tables[0].Rows[0][1].ToString();
        }
        catch (Exception ex)
        {
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = ex.Message;
            objResult.msg_code = "ERR";
           
        }
        finally
        {
            
        }

        return objResult;
    }

    public Result SaveBillTransactionForTest(BillTransactionEO p_objBillTranEO, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode, string SZ_PROCEDURE_GROUP_ID)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Information."


            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", p_objBillTranEO.SZ_BILL_ID);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@DT_BILL_DATE", p_objBillTranEO.DT_BILL_DATE);
            comm.Parameters.AddWithValue("@FLT_BILL_AMOUNT", p_objBillTranEO.FLT_BILL_AMOUNT);
            comm.Parameters.AddWithValue("@FLT_WRITE_OFF", p_objBillTranEO.FLT_WRITE_OFF);
            comm.Parameters.AddWithValue("@BIT_PAID", p_objBillTranEO.BIT_PAID);
            comm.Parameters.AddWithValue("@FLT_AMOUNT_APPLIED", p_objBillTranEO.FLT_AMOUNT_APPLIED);
            comm.Parameters.AddWithValue("@FLT_BALANCE", p_objBillTranEO.FLT_BALANCE);
            comm.Parameters.AddWithValue("@FLT_INTEREST", p_objBillTranEO.FLT_INTEREST);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", p_objBillTranEO.SZ_READING_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_TYPE", p_objBillTranEO.SZ_TYPE);
            comm.Parameters.AddWithValue("@SZ_TESTTYPE", p_objBillTranEO.SZ_TESTTYPE);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objBillTranEO.SZ_USER_ID);
            comm.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", p_objBillTranEO.SZ_Referring_Company_Id);
            comm.Parameters.AddWithValue("@SZ_PROC_GROUP_ID", SZ_PROCEDURE_GROUP_ID.ToString());
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();

            #endregion

            #region "get the Latest Bill Number"

            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();
            #endregion

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        comm.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        comm.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());

                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            comm.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        if (objBillProcedureCodeEO.i_cyclic_id != "" && objBillProcedureCodeEO.i_cyclic_id != "&nbsp;" && objBillProcedureCodeEO.i_cyclic_id != null)
                        {
                            comm.Parameters.AddWithValue("@i_cyclic_id", objBillProcedureCodeEO.i_cyclic_id);
                        }
                        if (objBillProcedureCodeEO.bt_cyclic_applied != "" && objBillProcedureCodeEO.bt_cyclic_applied != "&nbsp;" && objBillProcedureCodeEO.bt_cyclic_applied != null)
                        {
                            comm.Parameters.AddWithValue("@bt_cyclic_applied", objBillProcedureCodeEO.bt_cyclic_applied);
                        }
                        comm.Parameters.AddWithValue("@FLAG", "ADD");
                        comm.ExecuteNonQuery();
                        dr.Close();
                    }
                }
            }

            #endregion

            #region "DELETE BILL DIAGNOSIS"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);

            comm.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();

            #endregion

            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            objResult.bill_no = "";
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }

    public Result SaveBillTransactionForTest(BillTransactionEO p_objBillTranEO, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode, string SZ_PROCEDURE_GROUP_ID,float contractAmount)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Information."


            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", p_objBillTranEO.SZ_BILL_ID);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@DT_BILL_DATE", p_objBillTranEO.DT_BILL_DATE);
            comm.Parameters.AddWithValue("@FLT_BILL_AMOUNT", p_objBillTranEO.FLT_BILL_AMOUNT);
            comm.Parameters.AddWithValue("@FLT_WRITE_OFF", p_objBillTranEO.FLT_WRITE_OFF);
            comm.Parameters.AddWithValue("@BIT_PAID", p_objBillTranEO.BIT_PAID);
            comm.Parameters.AddWithValue("@FLT_AMOUNT_APPLIED", p_objBillTranEO.FLT_AMOUNT_APPLIED);
            comm.Parameters.AddWithValue("@FLT_BALANCE", p_objBillTranEO.FLT_BALANCE);
            comm.Parameters.AddWithValue("@FLT_INTEREST", p_objBillTranEO.FLT_INTEREST);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", p_objBillTranEO.SZ_READING_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_TYPE", p_objBillTranEO.SZ_TYPE);
            comm.Parameters.AddWithValue("@SZ_TESTTYPE", p_objBillTranEO.SZ_TESTTYPE);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objBillTranEO.SZ_USER_ID);
            comm.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", p_objBillTranEO.SZ_Referring_Company_Id);
            comm.Parameters.AddWithValue("@SZ_PROC_GROUP_ID", SZ_PROCEDURE_GROUP_ID.ToString());
            comm.Parameters.AddWithValue("@FLT_CONTRACT_AMOUNT", contractAmount);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();

            #endregion

            #region "get the Latest Bill Number"

            comm = new SqlCommand();
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();
            #endregion

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        comm.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        comm.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());

                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            comm.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        if (objBillProcedureCodeEO.i_cyclic_id != "" && objBillProcedureCodeEO.i_cyclic_id != "&nbsp;" && objBillProcedureCodeEO.i_cyclic_id != null)
                        {
                            comm.Parameters.AddWithValue("@i_cyclic_id", objBillProcedureCodeEO.i_cyclic_id);
                        }
                        if (objBillProcedureCodeEO.bt_cyclic_applied != "" && objBillProcedureCodeEO.bt_cyclic_applied != "&nbsp;" && objBillProcedureCodeEO.bt_cyclic_applied != null)
                        {
                            comm.Parameters.AddWithValue("@bt_cyclic_applied", objBillProcedureCodeEO.bt_cyclic_applied);
                        }
                        comm.Parameters.AddWithValue("@FLAG", "ADD");
                        comm.ExecuteNonQuery();
                        dr.Close();
                    }
                }
            }

            #endregion

            #region "DELETE BILL DIAGNOSIS"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);

            comm.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();

            #endregion

            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            objResult.bill_no = "";
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }
    public Result UpdateBillTransaction(BillTransactionEO p_objBillTranEO, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode, ArrayList p_objDeletedProcCodes, ArrayList p_objALEventRefferProcedure)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {
            #region "Deleted Procedures Codes from 'TXN_BILL_TRANSACTIONS_DETAIL'"

            if (p_objDeletedProcCodes != null)
            {
                if (p_objDeletedProcCodes.Count > 0)
                {
                    for (int i = 0; i < p_objDeletedProcCodes.Count; i++)
                    {
                        comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_BILL_TXN_DETAIL_ID", p_objDeletedProcCodes[i].ToString());
                        comm.Parameters.AddWithValue("@FLAG", "DELETE");
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion


            #region "Save Bill Information."
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", p_objBillTranEO.SZ_BILL_ID);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@DT_BILL_DATE", p_objBillTranEO.DT_BILL_DATE);
            //    comm.Parameters.AddWithValue("@DT_BILL_DATE_TO", p_objBillTranEO.DT_BILL_DATE_TO);
            comm.Parameters.AddWithValue("@FLT_BILL_AMOUNT", p_objBillTranEO.FLT_BILL_AMOUNT);
            comm.Parameters.AddWithValue("@FLT_WRITE_OFF", p_objBillTranEO.FLT_WRITE_OFF);
            comm.Parameters.AddWithValue("@BIT_PAID", p_objBillTranEO.BIT_PAID);
            comm.Parameters.AddWithValue("@FLT_AMOUNT_APPLIED", p_objBillTranEO.FLT_AMOUNT_APPLIED);
            comm.Parameters.AddWithValue("@FLT_BALANCE", p_objBillTranEO.FLT_BALANCE);
            comm.Parameters.AddWithValue("@FLT_INTEREST", p_objBillTranEO.FLT_INTEREST);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", p_objBillTranEO.SZ_PROVIDER_ID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@BIT_WRITE_OFF_FLAG", p_objBillTranEO.BIT_WRITE_OFF_FLAG);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", p_objBillTranEO.SZ_READING_DOCTOR_ID);
            comm.Parameters.AddWithValue("@BT_IS_SECOND_REQUEST", p_objBillTranEO.BT_IS_SECOND_REQUEST);
            comm.Parameters.AddWithValue("@SZ_SECOND_REQUEST_BILL", p_objBillTranEO.SZ_SECOND_REQUEST_BILL);
            comm.Parameters.AddWithValue("@SZ_TYPE", p_objBillTranEO.SZ_TYPE);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objBillTranEO.SZ_USER_ID);
            //    comm.Parameters.AddWithValue("@SZ_TESTTYPE", p_objBillTranEO.SZ_TESTTYPE);
            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.ExecuteNonQuery();
            #endregion


            #region "Save Diagnosis Code."


            // Start : Delete Diagnosis Code.

            comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
            comm.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();

            // End

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", objBillProcedureCodeEO.SZ_BILL_NUMBER);
                        if (objBillProcedureCodeEO.DT_DATE_OF_SERVICE.CompareTo(Convert.ToDateTime("1/1/1900")) > 0)
                            comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);

                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID == null)
                        {
                            objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                        }

                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT == null)
                        {
                            objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                        }


                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());
                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        if (objBillProcedureCodeEO.FLAG == "ADD")
                        {

                            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);

                        }
                        comm.Parameters.AddWithValue("@FLAG", objBillProcedureCodeEO.FLAG);

                        SqlDataReader dr1 = comm.ExecuteReader();
                        while (dr1.Read())
                        {
                            string szMsg = "";
                            szMsg = Convert.ToString(dr1["msg"]);
                            if (szMsg == "Error")
                            {
                                objResult.msg_code = "ERR";
                                objResult.msg = "Procedure codes already assigned from another case.";
                                objResult.bill_no = "";
                                throw new Exception("Error");
                            }
                        }
                        dr1.Close();

                    }
                }
            }

            #endregion

            #region "Save Event Reffer Procedure"

            if (p_objALEventRefferProcedure != null)
            {
                if (p_objALEventRefferProcedure.Count > 0)
                {
                    for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                    {
                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                        objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        // ---------------------------
                        //comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                        //comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                        //comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                        //comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
                        //comm.Parameters.AddWithValue("@DT_EVENT_DATE", objEventRefferProcedureEO.SZ_EVENT_DATE);
                        //comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
                        //comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);

                        comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                        comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                        comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);


                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }

    public string GetLimit(string szCompanyID, string szvisittype, string szspeciality)
    {

        conn = new SqlConnection(strsqlCon);
        conn.Open();
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            comm = new SqlCommand("SP_GET_LIMIT_DETAIL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@sz_company_id", szCompanyID);
            comm.Parameters.AddWithValue("@sz_visit_type", szvisittype);
            comm.Parameters.AddWithValue("@sz_specialty_id", szspeciality);
            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szReturn = Math.Round(Convert.ToDecimal(dr[0]), 2).ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szReturn = "";
        }
        finally
        {
            conn.Close();
        }

        return szReturn;
    }

    public string GetProcID(string szCompanyID, string szProcID)
    {

        conn = new SqlConnection(strsqlCon);
        conn.Open();
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            comm = new SqlCommand("SP_GET_PROCEDURE_GROUP_ID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", szProcID);

            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szReturn = "";
        }
        finally
        {
            conn.Close();
        }

        return szReturn;
    }

    public string GET_IS_LIMITE(string szCompanyID, string szSpecialtyId)
    {

        conn = new SqlConnection(strsqlCon);
        conn.Open();
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            comm = new SqlCommand("SP_GET_IS_LIMITE_USE", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            comm.Parameters.AddWithValue("@SZ_SPECIALTY_ID", szSpecialtyId);


            dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szReturn = "";
        }
        finally
        {
            conn.Close();
        }

        return szReturn;
    }

    public Result UpdateBillTransactions(BillTransactionEO p_objBillTranEO, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode, ArrayList p_objDeletedProcCodes, ArrayList p_objALEventRefferProcedure, ArrayList p_objEvent)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        comm.CommandTimeout = 0;
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {
            #region "Deleted Procedures Codes from 'TXN_BILL_TRANSACTIONS_DETAIL'"

            if (p_objDeletedProcCodes != null)
            {
                if (p_objDeletedProcCodes.Count > 0)
                {
                    for (int i = 0; i < p_objDeletedProcCodes.Count; i++)
                    {
                        comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_BILL_TXN_DETAIL_ID", p_objDeletedProcCodes[i].ToString());
                        comm.Parameters.AddWithValue("@FLAG", "DELETE");
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion


            #region "Save Bill Information."
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", p_objBillTranEO.SZ_BILL_ID);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@DT_BILL_DATE", p_objBillTranEO.DT_BILL_DATE);
            //    comm.Parameters.AddWithValue("@DT_BILL_DATE_TO", p_objBillTranEO.DT_BILL_DATE_TO);
            comm.Parameters.AddWithValue("@FLT_BILL_AMOUNT", p_objBillTranEO.FLT_BILL_AMOUNT);
            comm.Parameters.AddWithValue("@FLT_WRITE_OFF", p_objBillTranEO.FLT_WRITE_OFF);
            comm.Parameters.AddWithValue("@BIT_PAID", p_objBillTranEO.BIT_PAID);
            comm.Parameters.AddWithValue("@FLT_AMOUNT_APPLIED", p_objBillTranEO.FLT_AMOUNT_APPLIED);
            comm.Parameters.AddWithValue("@FLT_BALANCE", p_objBillTranEO.FLT_BALANCE);
            comm.Parameters.AddWithValue("@FLT_INTEREST", p_objBillTranEO.FLT_INTEREST);
            comm.Parameters.AddWithValue("@SZ_PROVIDER_ID", p_objBillTranEO.SZ_PROVIDER_ID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@BIT_WRITE_OFF_FLAG", p_objBillTranEO.BIT_WRITE_OFF_FLAG);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", p_objBillTranEO.SZ_READING_DOCTOR_ID);
            comm.Parameters.AddWithValue("@BT_IS_SECOND_REQUEST", p_objBillTranEO.BT_IS_SECOND_REQUEST);
            comm.Parameters.AddWithValue("@SZ_SECOND_REQUEST_BILL", p_objBillTranEO.SZ_SECOND_REQUEST_BILL);
            comm.Parameters.AddWithValue("@SZ_TYPE", p_objBillTranEO.SZ_TYPE);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objBillTranEO.SZ_USER_ID);
            //    comm.Parameters.AddWithValue("@SZ_TESTTYPE", p_objBillTranEO.SZ_TESTTYPE);
            comm.Parameters.AddWithValue("@FLAG", "UPDATE");
            comm.ExecuteNonQuery();
            #endregion


            #region "Save Diagnosis Code."


            // Start : Delete Diagnosis Code.

            comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
            comm.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            comm.CommandType = CommandType.StoredProcedure;
            comm.ExecuteNonQuery();

            // End

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", objBillProcedureCodeEO.SZ_BILL_NUMBER);
                        if (objBillProcedureCodeEO.DT_DATE_OF_SERVICE.CompareTo(Convert.ToDateTime("1/1/1900")) > 0)
                            comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);

                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID == null)
                        {
                            objBillProcedureCodeEO.I_GROUP_AMOUNT_ID = "";
                        }

                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT == null)
                        {
                            objBillProcedureCodeEO.FLT_GROUP_AMOUNT = "";
                        }


                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());
                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        if (objBillProcedureCodeEO.FLAG == "ADD")
                        {
                            comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        }
                        //Changes by Anupam (7Mar2011)  --start
                        if (objBillProcedureCodeEO.FLAG == "UPDATE")
                        {
                            comm.Parameters.AddWithValue("@SZ_BILL_TXN_DETAIL_ID", objBillProcedureCodeEO.SZ_BILL_TXN_DETAIL_ID);
                        }
                        //Changes by Anupam (7Mar2011)  --end
                        comm.Parameters.AddWithValue("@FLAG", objBillProcedureCodeEO.FLAG);
                        if (objBillProcedureCodeEO.SZ_MODIFIER_ID != null)
                        {
                            if (objBillProcedureCodeEO.SZ_MODIFIER_ID != "&nbsp;" && objBillProcedureCodeEO.SZ_MODIFIER_ID != "" && objBillProcedureCodeEO.SZ_MODIFIER_ID != "NA")
                            {
                                comm.Parameters.AddWithValue("@SZ_MULTI_MODIFIER", objBillProcedureCodeEO.SZ_MODIFIER_ID);
                            }
                        }

                        SqlDataReader dr1 = comm.ExecuteReader();
                        while (dr1.Read())
                        {
                            string szMsg = "";
                            szMsg = Convert.ToString(dr1["msg"]);
                            if (szMsg == "Error")
                            {
                                objResult.msg_code = "ERR";
                                objResult.msg = "Procedure codes already assigned from another case.";
                                objResult.bill_no = "";
                                throw new Exception("Error");
                            }
                        }
                        dr1.Close();

                    }
                }
            }

            #endregion

            #region "Save Event Reffer Procedure"

            if (p_objALEventRefferProcedure != null)
            {
                if (p_objALEventRefferProcedure.Count > 0)
                {
                    for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                    {
                        EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                        objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_SAVE_REFERRAL_PROC_CODE";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        // ---------------------------
                        //comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                        //comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                        //comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                        //comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
                        //comm.Parameters.AddWithValue("@DT_EVENT_DATE", objEventRefferProcedureEO.SZ_EVENT_DATE);
                        //comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
                        //comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);

                        comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                        comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                        comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);


                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion
            #region Check Daily Limit
            for (int i = 0; i < p_objEvent.Count; i++)
            {
                if (p_objEvent[i].ToString() != "")
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.Transaction = transaction;
                    comm.CommandText = "SP_CHECK_DAILY_LIMIT";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Parameters.AddWithValue("@I_EVENT_ID", p_objEvent[i].ToString().Trim());
                    comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
                    comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
                    comm.ExecuteNonQuery();
                }
            }

            #endregion
            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }


    public DataSet GetBillStaus(string CompanyID)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();

            comm = new SqlCommand("SP_MST_BILL_STATUS_BILL_SEARCH", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", CompanyID);
            comm.Parameters.AddWithValue("@FLAG", "GET_SELECTED_STATUS_LIST");

            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public DataSet GetInsuranceDetails(string CaseID)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();

            comm = new SqlCommand("SP_GET_PRI_SEC_INSURANCE_DETAILS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", CaseID);
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    public Result AddBillTransaction(BillTransactionEO p_objB, DAO_NOTES_EO _DAO_NOTES_EO, ArrayList _objALBillProcedureCodeEO, ArrayList objALBillDiagnosisCodeEO)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Information."

            comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objB.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@DT_BILL_DATE", p_objB.DT_BILL_DATE.ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objB.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objB.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_TYPE", p_objB.SZ_TYPE);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", p_objB.SZ_READING_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", p_objB.SZ_Referring_Company_Id);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objB.SZ_USER_ID);
            comm.Parameters.AddWithValue("@SZ_PROC_GROUP_ID", p_objB.SZ_PROCEDURE_GROUP_ID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
            #endregion

            #region "Get bill No"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objB.SZ_COMPANY_ID);

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();
            #endregion

            #region"Add notes"

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + "XML/ActivityNotesXML.xml");
            XmlNodeList nl = doc.SelectNodes("NOTES/" + _DAO_NOTES_EO.SZ_MESSAGE_TITLE + "/MESSAGE");
            string strMessage = _DAO_NOTES_EO.SZ_ACTIVITY_DESC + " " + nl.Item(0).InnerText;



            comm = new SqlCommand("SP_TXN_NOTES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
            comm.Parameters.AddWithValue("@SZ_CASE_ID", _DAO_NOTES_EO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@SZ_USER_ID", _DAO_NOTES_EO.SZ_USER_ID);
            comm.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
            comm.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
            comm.Parameters.AddWithValue("@IS_DENIED", _DAO_NOTES_EO.IS_DENIED);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _DAO_NOTES_EO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
            #endregion

            #region "Save Diagnosis Code."


            comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            comm.CommandType = CommandType.StoredProcedure;
            comm.Transaction = transaction;
            comm.ExecuteNonQuery();

            if (objALBillDiagnosisCodeEO != null)
            {
                if (objALBillDiagnosisCodeEO.Count > 0)
                {
                    for (int i = 0; i < objALBillDiagnosisCodeEO.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)objALBillDiagnosisCodeEO[i];

                        comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Transaction = transaction;
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Save procedure codes."

            if (_objALBillProcedureCodeEO != null)
            {
                if (_objALBillProcedureCodeEO.Count > 0)
                {
                    for (int i = 0; i < _objALBillProcedureCodeEO.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                        objBillProcedureCodeEO = (BillProcedureCodeEO)_objALBillProcedureCodeEO[i];
                        comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        //sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[0].ToString());
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE.ToString());
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        comm.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        comm.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID != "&nbsp;") comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID);
                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT != "&nbsp;") comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT);
                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != null && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            comm.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        comm.Parameters.AddWithValue("@FLAG", "ADD");
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Transaction = transaction;
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion




            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";
        }
        catch (Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }

    public Result AddBillTransactionMasterBilling(BillTransactionEO p_objB, DAO_NOTES_EO _DAO_NOTES_EO, ArrayList _objALBillProcedureCodeEO, ArrayList objALBillDiagnosisCodeEO)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Information."

            comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objB.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@DT_BILL_DATE", p_objB.DT_BILL_DATE.ToString());
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objB.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objB.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_TYPE", p_objB.SZ_TYPE);
            comm.Parameters.AddWithValue("@SZ_READING_DOCTOR_ID", p_objB.SZ_READING_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_REFERRING_COMPANY_ID", p_objB.SZ_Referring_Company_Id);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objB.SZ_USER_ID);
            comm.Parameters.AddWithValue("@SZ_PROC_GROUP_ID", p_objB.SZ_PROCEDURE_GROUP_ID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
            #endregion

            #region "Get bill No"
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objB.SZ_COMPANY_ID);

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();
            #endregion

            #region"Add notes"

            XmlDocument doc = new XmlDocument();
            doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + "XML/ActivityNotesXML.xml");
            XmlNodeList nl = doc.SelectNodes("NOTES/" + _DAO_NOTES_EO.SZ_MESSAGE_TITLE + "/MESSAGE");
            string strMessage = _DAO_NOTES_EO.SZ_ACTIVITY_DESC + " " + nl.Item(0).InnerText;



            comm = new SqlCommand("SP_TXN_NOTES", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004");
            comm.Parameters.AddWithValue("@SZ_CASE_ID", _DAO_NOTES_EO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@SZ_USER_ID", _DAO_NOTES_EO.SZ_USER_ID);
            comm.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001");
            comm.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", strMessage);
            comm.Parameters.AddWithValue("@IS_DENIED", _DAO_NOTES_EO.IS_DENIED);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", _DAO_NOTES_EO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
            #endregion

            #region "Save Diagnosis Code."


            comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
            comm.Parameters.AddWithValue("@FLAG", "DELETEBILLDIAGNOSIS");
            comm.CommandType = CommandType.StoredProcedure;
            comm.Transaction = transaction;
            comm.ExecuteNonQuery();

            if (objALBillDiagnosisCodeEO != null)
            {
                if (objALBillDiagnosisCodeEO.Count > 0)
                {
                    for (int i = 0; i < objALBillDiagnosisCodeEO.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)objALBillDiagnosisCodeEO[i];

                        comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Transaction = transaction;
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Save procedure codes."

            if (_objALBillProcedureCodeEO != null)
            {
                if (_objALBillProcedureCodeEO.Count > 0)
                {
                    for (int i = 0; i < _objALBillProcedureCodeEO.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();
                        objBillProcedureCodeEO = (BillProcedureCodeEO)_objALBillProcedureCodeEO[i];
                        comm = new SqlCommand("SP_TXN_BILL_TRANSACTIONS_DETAIL", conn);
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        //sqlCmd.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", _arrayList[0].ToString());
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@FL_AMOUNT", objBillProcedureCodeEO.FL_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE.ToString());
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@FLT_FACTOR", objBillProcedureCodeEO.FLT_FACTOR);
                        comm.Parameters.AddWithValue("@DOCT_AMOUNT", objBillProcedureCodeEO.DOCT_AMOUNT);
                        comm.Parameters.AddWithValue("@PROC_AMOUNT", objBillProcedureCodeEO.PROC_AMOUNT);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_CASE_ID", objBillProcedureCodeEO.SZ_CASE_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID != "&nbsp;") comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID);
                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT != "&nbsp;") comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT);
                        if (objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "" && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != null && objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID != "&nbsp;")
                        {
                            comm.Parameters.AddWithValue("@SZ_PATIENT_TREATMENT_ID", objBillProcedureCodeEO.SZ_PATIENT_TREATMENT_ID);
                        }
                        comm.Parameters.AddWithValue("@FLAG", "ADD");
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Transaction = transaction;
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion




            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }

    public void InsertBillSource(string szCompanyID, string szBillNo, string szSource)
    {
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        string szReturn = "";
        SqlDataReader dr;
        try
        {
            string query = "update TXN_BILL_TRANSACTIONS set SZ_BILL_SOURCE='" + szSource + "' where SZ_BILL_NUMBER='" + szBillNo + "' and SZ_COMPANY_ID='" + szCompanyID + "'";
            comm = new SqlCommand(query, conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            szReturn = "";
        }
        finally
        {
            conn.Close();
        }
    }

    public Result SaveBillTransactionWithEvent(BillTransactionEO p_objBillTranEO, ArrayList p_objEvent, ArrayList p_objALEventRefferProcedure, ArrayList p_objProcedureCodes, ArrayList p_objALDiagCode, string szEventID)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {

            #region "Save Bill Information."

            comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_NEW";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@SZ_BILL_ID", p_objBillTranEO.SZ_BILL_ID);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", p_objBillTranEO.SZ_BILL_NUMBER);
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_objBillTranEO.SZ_CASE_ID);
            comm.Parameters.AddWithValue("@FLT_BILL_AMOUNT", p_objBillTranEO.FLT_BILL_AMOUNT);
            comm.Parameters.AddWithValue("@FLT_BALANCE", p_objBillTranEO.FLT_BALANCE);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", p_objBillTranEO.SZ_DOCTOR_ID);
            comm.Parameters.AddWithValue("@SZ_USER_ID", p_objBillTranEO.SZ_USER_ID);
            comm.Parameters.AddWithValue("@FLAG", "ADD");
            comm.ExecuteNonQuery();
            #endregion

            // get the Latest Bill Number


            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "SP_TXN_LATEST_BILL_TRANSACTIONS";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;

            comm.Parameters.AddWithValue("@FLAG", "GETLATESTBILLID");
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();


            #region "Save Event Reffer Procedure"
            string szIsRefferal = "false";
            //check doctor is refferal or not. if doctor is refferal then do not add procedure codes in the TXN_CALENDER_EVENT_PRPCEDURE table -- anand
            if (p_objALEventRefferProcedure != null)
            {
                if (p_objALEventRefferProcedure.Count > 0)
                {
                    EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                    objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[0];
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "SP_CHECK_DOCTOR_ISREFFERAL";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                    SqlDataReader dr1 = comm.ExecuteReader();
                    while (dr1.Read())
                    {
                        szIsRefferal = Convert.ToString(dr1[0]);
                    }
                    dr1.Close();
                }
            }
            if (szIsRefferal.ToLower() == "false")
            {
                if (p_objALEventRefferProcedure != null)
                {
                    if (p_objALEventRefferProcedure.Count > 0)
                    {
                        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                        {
                            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                            comm = new SqlCommand();
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandText = "SP_SAVE_PROCEDURECODE_FOR_BILL";
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Connection = conn;
                            comm.Transaction = transaction;
                            comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                            comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                            comm.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                if (p_objALEventRefferProcedure != null)
                {
                    if (p_objALEventRefferProcedure.Count > 0)
                    {
                        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                        {
                            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                            comm = new SqlCommand();
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandText = "SP_DELETE_PROC_CODE_USING_EVENT_ID";
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Connection = conn;
                            comm.Transaction = transaction;

                            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);

                            comm.ExecuteNonQuery();
                        }
                    }
                }

                if (p_objALEventRefferProcedure != null)
                {
                    if (p_objALEventRefferProcedure.Count > 0)
                    {
                        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                        {
                            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                            comm = new SqlCommand();
                            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                            comm.CommandText = "SP_SAVE_PROCEDURECODE_FOR_BILL";
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Connection = conn;
                            comm.Transaction = transaction;
                            comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                            comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                            comm.ExecuteNonQuery();
                        }
                    }
                }
                //if (p_objALEventRefferProcedure != null)
                //{
                //    if (p_objALEventRefferProcedure.Count > 0)
                //    {
                //        for (int i = 0; i < p_objALEventRefferProcedure.Count; i++)
                //        {
                //            EventRefferProcedureEO objEventRefferProcedureEO = new EventRefferProcedureEO();
                //            objEventRefferProcedureEO = (EventRefferProcedureEO)p_objALEventRefferProcedure[i];
                //            comm = new SqlCommand();
                //            comm.CommandText = "SP_SAVE_PROCEDURECODE_FOR_BILL";
                //            comm.CommandType = CommandType.StoredProcedure;
                //            comm.Connection = conn;
                //            comm.Transaction = transaction;
                //            comm.Parameters.AddWithValue("@SZ_PROC_CODE", objEventRefferProcedureEO.SZ_PROC_CODE);
                //            comm.Parameters.AddWithValue("@I_EVENT_ID", objEventRefferProcedureEO.I_EVENT_ID);
                //            comm.Parameters.AddWithValue("@I_STATUS", objEventRefferProcedureEO.I_STATUS);
                //            comm.Parameters.AddWithValue("@flag", "Update");
                //            comm.ExecuteNonQuery();
                //        }
                //    }
                //}
            }

            #endregion

            #region "Save Diagnosis Code."

            if (p_objALDiagCode != null)
            {
                if (p_objALDiagCode.Count > 0)
                {
                    for (int i = 0; i < p_objALDiagCode.Count; i++)
                    {
                        BillDiagnosisCodeEO objBillDiagnosisCodeEO = new BillDiagnosisCodeEO();
                        objBillDiagnosisCodeEO = (BillDiagnosisCodeEO)p_objALDiagCode[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_DIAGNOSIS_CODE_ID", objBillDiagnosisCodeEO.SZ_DIAGNOSIS_CODE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@FLAG", "ADDBILLDIAGNOSIS");
                        comm.ExecuteNonQuery();
                    }
                }
            }

            #endregion

            #region "Save procedure codes."

            if (p_objProcedureCodes != null)
            {
                if (p_objProcedureCodes.Count > 0)
                {
                    for (int i = 0; i < p_objProcedureCodes.Count; i++)
                    {
                        BillProcedureCodeEO objBillProcedureCodeEO = new BillProcedureCodeEO();

                        objBillProcedureCodeEO = (BillProcedureCodeEO)p_objProcedureCodes[i];
                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.CommandText = "SP_TXN_BILL_TRANSACTIONS_DETAIL_NEW";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Transaction = transaction;
                        comm.Parameters.AddWithValue("@SZ_PROCEDURE_ID", objBillProcedureCodeEO.SZ_PROCEDURE_ID);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_DATE_OF_SERVICE", objBillProcedureCodeEO.DT_DATE_OF_SERVICE);
                        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillProcedureCodeEO.SZ_COMPANY_ID);
                        comm.Parameters.AddWithValue("@I_UNIT", objBillProcedureCodeEO.I_UNIT);
                        comm.Parameters.AddWithValue("@FLT_PRICE", objBillProcedureCodeEO.FLT_PRICE);
                        comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", objBillProcedureCodeEO.SZ_DOCTOR_ID);
                        comm.Parameters.AddWithValue("@SZ_TYPE_CODE_ID", objBillProcedureCodeEO.SZ_TYPE_CODE_ID);
                        if (objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "" && objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@I_GROUP_AMOUNT_ID", objBillProcedureCodeEO.I_GROUP_AMOUNT_ID.ToString());
                        if (objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "" && objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString() != "&nbsp;")
                            comm.Parameters.AddWithValue("@FLT_GROUP_AMOUNT", objBillProcedureCodeEO.FLT_GROUP_AMOUNT.ToString());

                        comm.Parameters.AddWithValue("@FLAG", "ADD");
                        comm.ExecuteNonQuery();
                        dr.Close();
                    }
                }
            }

            #endregion

            #region "Update Event."

            if (p_objEvent != null)
            {
                if (p_objEvent.Count > 0)
                {
                    for (int i = 0; i < p_objEvent.Count; i++)
                    {
                        EventEO objEventEO = new EventEO();
                        objEventEO = (EventEO)p_objEvent[i];

                        #region "Check bill is generated for given event id"

                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Transaction = transaction;
                        comm.CommandText = "CHECK_EVENT_BILLED_STATUS";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Parameters.AddWithValue("@I_EVENT_ID", objEventEO.I_EVENT_ID);
                        SqlDataReader dr2 = comm.ExecuteReader();
                        while (dr2.Read())
                        {
                            string szMsg = "";
                            szMsg = Convert.ToString(dr2["msg"]);
                            if (szMsg == "Error")
                            {
                                objResult.msg_code = "ERR";
                                objResult.msg = "You can not add multiple bill for same visit.";
                                objResult.bill_no = "";
                                dr2.Close();
                                dr.Close();
                                transaction.Rollback();
                                return objResult;
                            }
                        }
                        dr2.Close();
                        #endregion



                        comm = new SqlCommand();
                        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                        comm.Transaction = transaction;
                        comm.CommandText = "UPDATE_EVENT_STATUS";
                        comm.CommandType = CommandType.StoredProcedure;
                        comm.Connection = conn;
                        comm.Parameters.AddWithValue("@I_EVENT_ID", objEventEO.I_EVENT_ID);
                        comm.Parameters.AddWithValue("@BT_STATUS", objEventEO.BT_STATUS);
                        comm.Parameters.AddWithValue("@I_STATUS", objEventEO.I_STATUS);
                        comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
                        comm.Parameters.AddWithValue("@DT_BILL_DATE", objEventEO.DT_BILL_DATE);
                        comm.ExecuteNonQuery();
                    }
                }
            }
            #endregion


            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.Transaction = transaction;
            comm.CommandText = "SP_UPDATE_BILL_INFO";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_EVENT_ID", szEventID);
            comm.Parameters.AddWithValue("@SZ_BILL_NUMBER", szLatestBillNumber);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_objBillTranEO.SZ_COMPANY_ID);
            comm.ExecuteNonQuery();

            transaction.Commit();
            objResult.bill_no = szLatestBillNumber;
            objResult.msg = "Sccuess";
            objResult.msg_code = "SCC";
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            objResult.bill_no = "";
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }

    public Result SaveSecondBillTransactions(string SZ_COMPANY_ID, string SZ_CASE_ID, string SZ_USER_ID, string old_bill_number)
    {
        Result objResult = new Result();
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        comm = new SqlCommand();
        comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlTransaction transaction;
        transaction = conn.BeginTransaction();
        try
        {
            #region getlatestbillnumber
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_get_latest_bill_id";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@sz_company_id", SZ_COMPANY_ID);

            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                szLatestBillNumber = Convert.ToString(dr[0]);
            }
            dr.Close();
            #endregion

            #region copy events
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_copy_events_for_secondary_bill";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@sz_new_bill_number", szLatestBillNumber);
            comm.Parameters.AddWithValue("@sz_old_bill_number", old_bill_number);
            comm.Parameters.AddWithValue("@sz_user_id", SZ_USER_ID);
            comm.ExecuteNonQuery();
            #endregion

            #region copy Bill transaction
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_copy_bill_transaction_detail_for_second";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@sz_new_bill_number", szLatestBillNumber);
            comm.Parameters.AddWithValue("@sz_old_bill_number", old_bill_number);
            comm.Parameters.AddWithValue("@sz_user_id", SZ_USER_ID);
            comm.Parameters.AddWithValue("@sz_company_id", SZ_COMPANY_ID);
            comm.ExecuteNonQuery();
            #endregion

            #region get EventdIDs
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_get_event_ids_from_bills";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Transaction = transaction;
            comm.Parameters.AddWithValue("@sz_new_bill_number", szLatestBillNumber);
            comm.Parameters.AddWithValue("@sz_old_bill_number", old_bill_number);
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            sqlda.Fill(ds);

            #endregion


            if (ds != null && ds.Tables.Count > 1 && (ds.Tables[0].Rows.Count == ds.Tables[1].Rows.Count))
            {
                #region copy Event Procedures
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "sp_copy_event_procedure_for_second";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@i_event_id_old", ds.Tables[0].Rows[i]["I_EVENT_ID"]);
                    comm.Parameters.AddWithValue("@i_event_id_new", ds.Tables[1].Rows[i]["I_EVENT_ID"]);
                    comm.ExecuteNonQuery();
                }
                #endregion

                #region check whether doctor is refferal
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comm = new SqlCommand();
                    comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                    comm.CommandText = "sp_check_doctor_is_refferal";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Transaction = transaction;
                    comm.Parameters.AddWithValue("@i_event_id_new", ds.Tables[1].Rows[i]["I_EVENT_ID"]);
                    comm.ExecuteNonQuery();
                }
                #endregion

                #region copy diagnosis codes
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "sp_copy_diagnosys_code_for_second_Bill";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@sz_new_bill_number", szLatestBillNumber);
                comm.Parameters.AddWithValue("@sz_old_bill_number", old_bill_number);
                comm.ExecuteNonQuery();
                #endregion

                #region copy bill details
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "sp_copy_bill_detail_for_second";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@sz_new_bill_number", szLatestBillNumber);
                comm.Parameters.AddWithValue("@sz_old_bill_number", old_bill_number);
                comm.ExecuteNonQuery();
                #endregion

                #region update associate Procedure
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "sp_check_associate_procedure_complete";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@sz_new_bill_number", szLatestBillNumber);
                comm.Parameters.AddWithValue("@SZ_CASE_ID", SZ_CASE_ID);
                comm.Parameters.AddWithValue("@SZ_COMPANY_ID", SZ_COMPANY_ID);
                comm.ExecuteNonQuery();
                #endregion

                #region update second bill status
                comm = new SqlCommand();
                comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
                comm.CommandText = "SP_SAVE_SECOND_BILL_STATUS";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.Transaction = transaction;
                comm.Parameters.AddWithValue("@SZ_OLD_BILL_NUMBER", old_bill_number);
                comm.Parameters.AddWithValue("@SZ_NEW_BILL_NUMBER", szLatestBillNumber);
                comm.ExecuteNonQuery();
                #endregion

                transaction.Commit();
                objResult.bill_no = szLatestBillNumber;
                objResult.msg = "Sccuess";
                objResult.msg_code = "SCC";
            }
            else
            {
                objResult.msg_code = "ERR";
                objResult.msg = "No event found for copy.";
                objResult.bill_no = "";
                transaction.Rollback();
            }



        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            objResult.bill_no = "";
            transaction.Rollback();
        }
        finally
        {
            conn.Close();
        }
        return objResult;
    }

    public bool checkBillExist(string Bill_Number)
    {
        bool exist = false;
        string SecondBillExist = "";
        conn = new SqlConnection(strsqlCon);
        conn.Open();
        try
        {
            #region getlatestbillnumber
            comm = new SqlCommand();
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandText = "sp_check_second_bill_status";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_bill_number", Bill_Number);
            SqlDataReader dr = comm.ExecuteReader();
            while (dr.Read())
            {
                SecondBillExist = Convert.ToString(dr[0]);
            }
            dr.Close();
            if (SecondBillExist == "1" || SecondBillExist == "True")
            {
                exist = true;
            }
            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            conn.Close();
        }
        return exist;
    }

    public DataSet GetServicesForEvent(int iEventId)
    {
        DataSet ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();

            comm = new SqlCommand("SP_GET_PROCEDURECODES_BYEVENTID", conn);
            comm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_ID", iEventId);

            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

}

public class BillTransactionEO
{
    private string _SZ_BILL_ID = "";
    public string SZ_BILL_ID
    {
        get { return _SZ_BILL_ID; }
        set { _SZ_BILL_ID = value; }
    }

    private string _SZ_BILL_NUMBER = "";
    public string SZ_BILL_NUMBER
    {
        get { return _SZ_BILL_NUMBER; }
        set { _SZ_BILL_NUMBER = value; }
    }


    private string _SZ_CASE_ID = "";
    public string SZ_CASE_ID
    {
        get { return _SZ_CASE_ID; }
        set { _SZ_CASE_ID = value; }
    }

    private DateTime _DT_BILL_DATE;
    public DateTime DT_BILL_DATE
    {
        get { return _DT_BILL_DATE; }
        set { _DT_BILL_DATE = value; }
    }

    private DateTime _DT_BILL_DATE_TO;
    public DateTime DT_BILL_DATE_TO
    {
        get { return _DT_BILL_DATE_TO; }
        set { _DT_BILL_DATE_TO = value; }
    }

    private String _FLT_BILL_AMOUNT;
    public String FLT_BILL_AMOUNT
    {
        get { return _FLT_BILL_AMOUNT; }
        set { _FLT_BILL_AMOUNT = value; }
    }

    private Boolean _BIT_PAID;
    public Boolean BIT_PAID
    {
        get { return _BIT_PAID; }
        set { _BIT_PAID = value; }
    }
    private String _FLT_AMOUNT_APPLIED;
    public String FLT_AMOUNT_APPLIED
    {
        get { return _FLT_AMOUNT_APPLIED; }
        set { _FLT_AMOUNT_APPLIED = value; }
    }


    private String _FLT_BALANCE;
    public String FLT_BALANCE
    {
        get { return _FLT_BALANCE; }
        set { _FLT_BALANCE = value; }
    }

    private String _FLT_INTEREST;
    public String FLT_INTEREST
    {
        get { return _FLT_INTEREST; }
        set { _FLT_INTEREST = value; }
    }


    private string _SZ_PROVIDER_ID = "";
    public string SZ_PROVIDER_ID
    {
        get { return _SZ_PROVIDER_ID; }
        set { _SZ_PROVIDER_ID = value; }
    }

    private string _SZ_COMPANY_ID = "";
    public string SZ_COMPANY_ID
    {
        get { return _SZ_COMPANY_ID; }
        set { _SZ_COMPANY_ID = value; }
    }

    private Boolean _BIT_WRITE_OFF_FLAG;
    public Boolean BIT_WRITE_OFF_FLAG
    {
        get { return _BIT_WRITE_OFF_FLAG; }
        set { _BIT_WRITE_OFF_FLAG = value; }
    }

    private string _SZ_DOCTOR_ID = "";
    public string SZ_DOCTOR_ID
    {
        get { return _SZ_DOCTOR_ID; }
        set { _SZ_DOCTOR_ID = value; }
    }

    private string _SZ_READING_DOCTOR_ID = "";
    public string SZ_READING_DOCTOR_ID
    {
        get { return _SZ_READING_DOCTOR_ID; }
        set { _SZ_READING_DOCTOR_ID = value; }
    }

    private Boolean _BT_IS_SECOND_REQUEST;
    public Boolean BT_IS_SECOND_REQUEST
    {
        get { return _BT_IS_SECOND_REQUEST; }
        set { _BT_IS_SECOND_REQUEST = value; }
    }



    private string _SZ_TYPE = "";
    public string SZ_TYPE
    {
        get { return _SZ_TYPE; }
        set { _SZ_TYPE = value; }
    }

    private string _SZ_TESTTYPE = "";
    public string SZ_TESTTYPE
    {
        get { return _SZ_TESTTYPE; }
        set { _SZ_TESTTYPE = value; }
    }

    private String _FLT_WRITE_OFF;
    public String FLT_WRITE_OFF
    {
        get { return _FLT_WRITE_OFF; }
        set { _FLT_WRITE_OFF = value; }
    }


    private string _SZ_SECOND_REQUEST_BILL = "";
    public string SZ_SECOND_REQUEST_BILL
    {
        get { return _SZ_SECOND_REQUEST_BILL; }
        set { _SZ_SECOND_REQUEST_BILL = value; }
    }



    private string _FLAG = "";
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }

    private String _SZ_USER_ID = "";
    public String SZ_USER_ID
    {
        get { return _SZ_USER_ID; }
        set { _SZ_USER_ID = value; }
    }

    private String ReferringCompanyID = "";
    public String SZ_Referring_Company_Id
    {
        get { return ReferringCompanyID; }
        set { ReferringCompanyID = value; }
    }

    private string _SZ_PROCEDURE_GROUP_ID = "";
    public string SZ_PROCEDURE_GROUP_ID
    {
        get { return _SZ_PROCEDURE_GROUP_ID; }
        set { _SZ_PROCEDURE_GROUP_ID = value; }
    }




}

public class BillProcedureCodeEO
{

    private string _SZ_BILL_TXN_DETAIL_ID = "";
    public string SZ_BILL_TXN_DETAIL_ID
    {
        get { return _SZ_BILL_TXN_DETAIL_ID; }
        set { _SZ_BILL_TXN_DETAIL_ID = value; }
    }

    private string _SZ_DIAGNOSIS_CODE_ID = "";
    public string SZ_DIAGNOSIS_CODE_ID
    {
        get { return _SZ_DIAGNOSIS_CODE_ID; }
        set { _SZ_DIAGNOSIS_CODE_ID = value; }
    }

    private string _SZ_PROCEDURE_ID = "";
    public string SZ_PROCEDURE_ID
    {
        get { return _SZ_PROCEDURE_ID; }
        set { _SZ_PROCEDURE_ID = value; }
    }

    private String _FL_AMOUNT;
    public String FL_AMOUNT
    {
        get { return _FL_AMOUNT; }
        set { _FL_AMOUNT = value; }
    }

    private string _SZ_BILL_NUMBER = "";
    public string SZ_BILL_NUMBER
    {
        get { return _SZ_BILL_NUMBER; }
        set { _SZ_BILL_NUMBER = value; }
    }

    private DateTime _DT_DATE_OF_SERVICE;
    public DateTime DT_DATE_OF_SERVICE
    {
        get { return _DT_DATE_OF_SERVICE; }
        set { _DT_DATE_OF_SERVICE = value; }
    }

    private string _SZ_COMPANY_ID = "";
    public string SZ_COMPANY_ID
    {
        get { return _SZ_COMPANY_ID; }
        set { _SZ_COMPANY_ID = value; }
    }

    private string _SZ_CASE_ID = "";
    public string SZ_CASE_ID
    {
        get { return _SZ_CASE_ID; }
        set { _SZ_CASE_ID = value; }
    }

    private string _I_UNIT;
    public string I_UNIT
    {
        get { return _I_UNIT; }
        set { _I_UNIT = value; }
    }

    private String _FLT_PRICE;
    public String FLT_PRICE
    {
        get { return _FLT_PRICE; }
        set { _FLT_PRICE = value; }
    }

    private String _FLT_FACTOR;
    public String FLT_FACTOR
    {
        get { return _FLT_FACTOR; }
        set { _FLT_FACTOR = value; }
    }

    private String _DOCT_AMOUNT;
    public String DOCT_AMOUNT
    {
        get { return _DOCT_AMOUNT; }
        set { _DOCT_AMOUNT = value; }
    }

    private String _PROC_AMOUNT;
    public String PROC_AMOUNT
    {
        get { return _PROC_AMOUNT; }
        set { _PROC_AMOUNT = value; }
    }

    private String _SZ_DOCTOR_ID = "";
    public String SZ_DOCTOR_ID
    {
        get { return _SZ_DOCTOR_ID; }
        set { _SZ_DOCTOR_ID = value; }
    }

    private String _SZ_TYPE_CODE_ID = "";
    public String SZ_TYPE_CODE_ID
    {
        get { return _SZ_TYPE_CODE_ID; }
        set { _SZ_TYPE_CODE_ID = value; }
    }

    private String _SZ_PATIENT_TREATMENT_ID = "";
    public String SZ_PATIENT_TREATMENT_ID
    {
        get { return _SZ_PATIENT_TREATMENT_ID; }
        set { _SZ_PATIENT_TREATMENT_ID = value; }
    }


    private string _I_GROUP_AMOUNT_ID;
    public string I_GROUP_AMOUNT_ID
    {
        get { return _I_GROUP_AMOUNT_ID; }
        set { _I_GROUP_AMOUNT_ID = value; }
    }


    private String _FLT_GROUP_AMOUNT;
    public String FLT_GROUP_AMOUNT
    {
        get { return _FLT_GROUP_AMOUNT; }
        set { _FLT_GROUP_AMOUNT = value; }
    }

    private string _SZ_MODIFIER_ID;
    public string SZ_MODIFIER_ID
    {
        get { return _SZ_MODIFIER_ID; }
        set { _SZ_MODIFIER_ID = value; }
    }

    private string _FLAG = "";
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }

    private string _i_cyclic_id = "";
    public string i_cyclic_id
    {
        get { return _i_cyclic_id; }
        set { _i_cyclic_id = value; }
    }

    private string _bt_cyclic_applied = "";
    public string bt_cyclic_applied
    {
        get { return _bt_cyclic_applied; }
        set { _bt_cyclic_applied = value; }
    }

}

public class BillDiagnosisCodeEO
{
    private string _SZ_DIAGNOSIS_CODE_ID = "";
    public string SZ_DIAGNOSIS_CODE_ID
    {
        get { return _SZ_DIAGNOSIS_CODE_ID; }
        set { _SZ_DIAGNOSIS_CODE_ID = value; }
    }

    private string _FLAG = "";
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }
}

public class EventEO
{
    private string _I_EVENT_ID;
    public string I_EVENT_ID
    {
        get { return _I_EVENT_ID; }
        set { _I_EVENT_ID = value; }
    }

    private string _BT_STATUS;
    public string BT_STATUS
    {
        get { return _BT_STATUS; }
        set { _BT_STATUS = value; }
    }

    private string _I_STATUS;
    public string I_STATUS
    {
        get { return _I_STATUS; }
        set { _I_STATUS = value; }
    }

    private string _SZ_BILL_NUMBER;
    public string SZ_BILL_NUMBER
    {
        get { return _SZ_BILL_NUMBER; }
        set { _SZ_BILL_NUMBER = value; }
    }

    private DateTime _DT_BILL_DATE;
    public DateTime DT_BILL_DATE
    {
        get { return _DT_BILL_DATE; }
        set { _DT_BILL_DATE = value; }
    }

    private string _FLAG = "";
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }


}

public class EventRefferProcedureEO
{
    private string _SZ_PROC_CODE = "";
    public string SZ_PROC_CODE
    {
        get { return _SZ_PROC_CODE; }
        set { _SZ_PROC_CODE = value; }
    }

    private string _I_EVENT_ID;
    public string I_EVENT_ID
    {
        get { return _I_EVENT_ID; }
        set { _I_EVENT_ID = value; }
    }

    private string _I_STATUS;
    public string I_STATUS
    {
        get { return _I_STATUS; }
        set { _I_STATUS = value; }
    }

    private DateTime _DT_RESCHEDULE_DATE;
    public DateTime DT_RESCHEDULE_DATE
    {
        get { return _DT_RESCHEDULE_DATE; }
        set { _DT_RESCHEDULE_DATE = value; }
    }

    private String _DT_RESCHEDULE_TIME;
    public String DT_RESCHEDULE_TIME
    {
        get { return _DT_RESCHEDULE_TIME; }
        set { _DT_RESCHEDULE_TIME = value; }
    }

    private string _DT_RESCHEDULE_TIME_TYPE = "";
    public string DT_RESCHEDULE_TIME_TYPE
    {
        get { return _DT_RESCHEDULE_TIME_TYPE; }
        set { _DT_RESCHEDULE_TIME_TYPE = value; }
    }

    private string _I_RESCHEDULE_ID;
    public string I_RESCHEDULE_ID
    {
        get { return _I_RESCHEDULE_ID; }
        set { _I_RESCHEDULE_ID = value; }
    }

    private string _SZ_EVENT_DATE;
    public string SZ_EVENT_DATE
    {
        get { return _SZ_EVENT_DATE; }
        set { _SZ_EVENT_DATE = value; }
    }

    private string _SZ_MODIFIER_ID;
    public string SZ_MODIFIER_ID
    {
        get { return _SZ_MODIFIER_ID; }
        set { _SZ_MODIFIER_ID = value; }
    }

    private string _FLAG = "";
    public string FLAG
    {
        get { return _FLAG; }
        set { _FLAG = value; }
    }
}

public class Result
{
    private string _bill_no;
    public string bill_no
    {
        get { return _bill_no; }
        set { _bill_no = value; }
    }

    private string _msg;
    public string msg
    {
        get { return _msg; }
        set { _msg = value; }
    }

    private string _msg_code;
    public string msg_code
    {
        get { return _msg_code; }
        set { _msg_code = value; }
    }
}