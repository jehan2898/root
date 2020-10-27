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


using System.Data.SqlClient;

public partial class WorkAreaWidget : PageBase
{
    string szCaseId = "";
    protected string strConn;
    private static ILog log = LogManager.GetLogger("WorkAreaWidget");
    protected void Page_Load(object sender, EventArgs e)
   {        

        string sz_CmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        // if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true && ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != e.Item.Cells[28].Text)
        {
            //  Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            mbs.lawfirm.Bill_Sys_Patient_Details obj = new Bill_Sys_Patient_Details();

            try
            {

                if (!IsPostBack)
                {

                    string szBillingCmp="";
                    if (Request.QueryString.Count>0)
                    {
                        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                        szCaseId = Request.QueryString["csid"].ToString();
                        byte[] ar = System.Convert.FromBase64String(szCaseId);
                        szCaseId = System.Text.ASCIIEncoding.ASCII.GetString(ar);
                        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
                        Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
                        szBillingCmp = Request.QueryString["cmp"].ToString();
                        byte[] ar1 = System.Convert.FromBase64String(szBillingCmp);
                        szBillingCmp = System.Text.ASCIIEncoding.ASCII.GetString(ar1);

                        string patientName = Request.QueryString["pname"].ToString();
                        byte[] ar2 = System.Convert.FromBase64String(patientName);
                        patientName = System.Text.ASCIIEncoding.ASCII.GetString(ar2);

                        string caseno = Request.QueryString["caseno"].ToString();
                        byte[] ar4 = System.Convert.FromBase64String(caseno);
                        caseno = System.Text.ASCIIEncoding.ASCII.GetString(ar4);
                        
                        _bill_Sys_CaseObject.SZ_CASE_ID = szCaseId;
                        _bill_Sys_CaseObject.SZ_COMAPNY_ID = szBillingCmp;
                        _bill_Sys_CaseObject.SZ_PATIENT_NAME = patientName;
                        _bill_Sys_CaseObject.SZ_CASE_NO = caseno;
                        Session["CASE_OBJECT"] = _bill_Sys_CaseObject;
                        mbs.lawfirm.Bill_Sys_CollectDocAndZip _cNz = new mbs.lawfirm.Bill_Sys_CollectDocAndZip();
                        DataSet Batchds = new DataSet();
                      Batchds=  _cNz.GetDowloadAs(szCaseId);
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



                    DataSet ds = obj.GetPatientInformation(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, sz_CmpId);                  

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

                    hdnCaseId.Value = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                    hdnCaseNo.Value = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
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


    protected void DtlView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            //_objDesc.LAWFIRM_COMPANY_ID, DateTime.Now.ToString(), _objDesc.USER_ID, _objDesc.IP_ADDRESS
            DAO_PatientList _objDesc = new DAO_PatientList();
            Bill_Sys_CaseObject _bill_Sys_CaseObject = new Bill_Sys_CaseObject();
            ArrayList objAL = new ArrayList();
            mbs.lawfirm.Bill_Sys_CollectDocAndZip cNz = new mbs.lawfirm.Bill_Sys_CollectDocAndZip();
            szCaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            _objDesc.CaseID = szCaseId;
            _objDesc.CompanyId = cNz.GetCompanyId(szCaseId);
            _objDesc.LAWFIRM_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            _objDesc.USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
             _objDesc.IP_ADDRESS    = Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
      
            objAL.Add(_objDesc);

            string path = cNz.CollectAndZip(objAL, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (path == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download.');", true);
            }
            else
            {


                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + ConfigurationSettings.AppSettings["LFIRMDOCURL"].ToString() + path + "'); ", true);
            }
           // ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "openURL('" + ConfigurationSettings.AppSettings["LFIRMDOCURL"].ToString() + path + "','" + path + "');", true);
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
           
        }
    }
}
