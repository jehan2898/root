using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mbs.lawfirm;
using log4net;
using log4net.Config;
using mbs.provider;
using System.Data.SqlClient;
using mbs.provider;

public partial class Provider_Bill_Sys_CaseDetails : PageBase
{
    SqlConnection sqlCon;
    string szCaseId = "";
    string caseno = "";
    string szBillingCmp = "";
    protected string strConn;
    private static ILog log = LogManager.GetLogger("Provider:");
    mbs.provider.ProviderServices obj_provider_services;
    protected void Page_Load(object sender, EventArgs e)
    {
          strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
         {
            mbs.lawfirm.Bill_Sys_Patient_Details obj = new Bill_Sys_Patient_Details();
            mbs.provider.ProviderServices obj_provider = new ProviderServices();
            try
            {
                if (!IsPostBack)
                {
                    Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();

                    if (Session["Office_ID"] != null)
                    {
                        hdnOfficeId.Value = Session["Office_ID"].ToString();
                    }

                    if (Request.QueryString.Count > 0)
                    {
                        szCaseId = Request.QueryString["caseid"].ToString();
                        txtcaseid.Text = szCaseId;
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        szBillingCmp = Request.QueryString["cmpid"].ToString();
                        txtCompanyid.Text = szBillingCmp;
                       // string patientName = Request.QueryString["pname"].ToString();
                        caseno = Request.QueryString["caseno"].ToString();
                        _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId;
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = szBillingCmp;
                        //_bill_Sys_CaseObject.SZ_PATIENT_NAME = patientName;
                        _bill_Sys_CaseObject.SZ_CASE_NO = caseno;
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                        mbs.lawfirm.Bill_Sys_CollectDocAndZip _cNz = new mbs.lawfirm.Bill_Sys_CollectDocAndZip();
                        DataSet Batchds = new DataSet();
                        Batchds = _cNz.GetDowloadAs(szCaseId);
                        if (Batchds.Tables[0].Rows.Count > 0 && Batchds.Tables[0].Rows[0][0].ToString() == "SNGL")
                        {
                            //i=  DtlView.FindControl("abc");
                            //Table tb = (Table)DtlView.Items.FindControl("tblF");   
                            //DtlView1.DataSource = Batchds;
                            //DtlView1.DataBind();
                            //DtlView1.Visible = true;

                        }
                        else
                        {

                        }



                    }
                    _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    DataSet ds = obj_provider.GetPatientInfo_Provider(szCaseId, szBillingCmp);
                    //DataSet ds = _bill_Sys_PatientBO.GetPatientInfo_Provider(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, sz_CmpId);


                    //DataSet ds = obj.GetPatientInformation(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, sz_CmpId);

                    if (ds.Tables[0].Columns.Count == 1)
                    {

                        Session["Check"] = "Not Correct";
                        Response.Redirect("Bill_Sys_SearchCase.aspx");
                    }
                    else
                    {
                        DataTable dt = ds.Tables[0];
                        DtlView.DataSource = ds;
                        DtlView.DataBind();
                    }


                    //To Open Document Manager

                    hdnCaseId.Value = szCaseId;
                    hdnCaseNo.Value = caseno;
                    hdnCompanyId.Value = szBillingCmp.ToString();

                    //End Of Code
                }
            }
            catch (Exception exc)
            {
                usrMessage.PutMessage(exc.ToString());
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string sz_office_ID = Session["Office_ID"].ToString();
        obj_provider_services = new ProviderServices();
        rpt_ViewBills.DataSource = obj_provider_services.GetViewBillInfo(txtcaseid.Text, txtCompanyid.Text, sz_office_ID);
        rpt_ViewBills.DataBind();
        tblViewBills.Visible = true;
    }
   
    protected void rptPatientDeskList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       
    }
    protected void DtlView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
    }

    
}
