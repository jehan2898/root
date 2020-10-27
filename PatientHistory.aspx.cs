using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Componend;

public partial class PatientHistory : PageBase
{
    private DataSet ds, dsCase, dsBills;//dsTreatment,dsBills;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["PassedCaseID"] = Request.QueryString["CaseId"];
        BindGrid();
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("PatientHistory.aspx");
        }
        #endregion
    }

    private DataSet getCaseDataSet()
    {
        DataSet ds=new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        conn.Open();

        SqlCommand comm = new SqlCommand("select PAT.SZ_PATIENT_LAST_NAME + ', ' + PAT.SZ_PATIENT_FIRST_NAME [SZ_PATIENT_NAME] , INS.SZ_INSURANCE_NAME [SZ_INSURANCE],ISNULL(SZ_CLAIM_NUMBER,'UNKOWN') [SZ_CLAIM], ISNULL(PAT.SZ_WCB_NO,'UNKNOWN') [SZ_WCB_NO],DT_DATE_OF_ACCIDENT [DT_ACCIDENT] from MST_CASE_MASTER CM JOIN MST_PATIENT PAT ON PAT.SZ_PATIENT_ID = CM.SZ_PATIENT_ID JOIN MST_INSURANCE_COMPANY INS ON INS.SZ_INSURANCE_ID = CM.SZ_INSURANCE_ID where sz_case_id = '" + Session["PassedCaseID"] + "'", conn);
        SqlDataAdapter sqlda = new SqlDataAdapter(comm);
        sqlda.Fill(ds);
        return ds;
    }

    private DataSet getBills()
    {
        DataSet ds=new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        conn.Open();

        SqlCommand comm = new SqlCommand("SELECT SZ_BILL_NUMBER [SZ_BILL_NUMBER],DT_BILL_DATE [DT_BILL_DATE],ISNULL(FLT_BILL_AMOUNT,0.00) [FLT_BILL_AMOUNT],ISNULL((SELECT SUM(FLT_CHECK_AMOUNT) FROM TXN_PAYMENT_TRANSACTIONS WHERE SZ_BILL_ID=TXN_BILL_TRANSACTIONS.SZ_BILL_NUMBER),0)[PAID_AMOUNT],ISNULL(FLT_WRITE_OFF,0.00)[FLT_WRITE_OFF],BIT_PAID,ISNULL(FLT_AMOUNT_APPLIED,0)[FLT_AMOUNT_APPLIED],ISNULL(FLT_BALANCE,0)[FLT_BALANCE],ISNULL(FLT_INTEREST,0)[FLT_INTEREST],BIT_WRITE_OFF_FLAG = CASE WHEN BIT_WRITE_OFF_FLAG IS NULL OR BIT_WRITE_OFF_FLAG = 0 THEN 0  ELSE  1  END ,  SZ_DOCTOR_ID  FROM TXN_BILL_TRANSACTIONS WHERE TXN_BILL_TRANSACTIONS.SZ_CASE_ID='" + Session["PassedCaseID"] + "' ORDER BY SZ_BILL_NUMBER DESC", conn);
        SqlDataAdapter sqlda = new SqlDataAdapter(comm);
        sqlda.Fill(ds);
        return ds;
    }

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ds = new DataSet();
       // dsTreatment = new DataSet();
        dsCase = new DataSet();
        dsBills = new DataSet();

        try
        {
            //ds.ReadXml(Server.MapPath("XML/Demo_PatientHistory_Denials.xml"));

            //dsTreatment.ReadXml(Server.MapPath("XML/Demo_PatientTreatment.xml"));

            //dsBills.ReadXml(Server.MapPath("XML/Demo_PatientBills.xml"));

            //grdDenials.DataSource = ds;
            //grdDenials.DataBind();

            grdCase.DataSource = getCaseDataSet();
            grdCase.DataBind();
            GetTreatmentList(Session["PassedCaseID"].ToString());
            GetDenialBillList(Session["PassedCaseID"].ToString());
            //grdTreatment.DataSource = dsTreatment;
            //grdTreatment.DataBind();

            grdBilling.DataSource = getBills();
            grdBilling.DataBind();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetTreatmentList(string szcaseID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_PatientBO=new Bill_Sys_PatientBO();

            grdTreatment.DataSource = _bill_Sys_PatientBO.GetTreatmentList(szcaseID);
            grdTreatment.DataBind();
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetDenialBillList(string szcaseID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_PatientBO = new Bill_Sys_PatientBO();

            grdDenials.DataSource = _bill_Sys_PatientBO.GetDenialList(szcaseID);
            grdDenials.DataBind();
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void hlnkAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ScriptManager.RegisterClientScriptBlock(hlnkAssociate, typeof(Button), "Msg", "window.open('Bill_Sys_AssociateDignosisCode.aspx?CaseId=" + Session["PassedCaseID"].ToString() + "','_self'); document.getElementById('ImageDiv').style.visibility = 'hidden'; ", true);
        }
        catch(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

}
