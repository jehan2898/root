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
using PDFValueReplacement;
using System.IO;
using Componend; 


public partial class Bill_Sys_CM_workStatus : PageBase
{
    private SaveOperation _saveOperation;   
    protected void Page_Load(object sender, EventArgs e)
    {
        Bill_Sys_CheckoutBO _obj = new Bill_Sys_CheckoutBO();
        DataSet dsObj = _obj.PatientName(Session["CM_HI_EventID"].ToString());
        Session["CO_CHIRO_CaseID"] = dsObj.Tables[0].Rows[0][1].ToString(); 
        txtEventID.Text = Session["CM_HI_EventID"].ToString();
        if (!IsPostBack)
        {
            LoadData();
            LoadPatientData();
        }
        TXT_DOE.Text = DateTime.Now.ToString("MM/dd/yyy");
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CM_workStatus.aspx");
        }
        #endregion
        
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_CM_PlanOfCare.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (RDO_DISCUSS_LIMIT_PATIENT.SelectedValue.Equals(""))
        {
            txtRDO_DISCUSS_LIMIT_PATIENT.Text = "-1";
        }
        else
        {
            txtRDO_DISCUSS_LIMIT_PATIENT.Text = RDO_DISCUSS_LIMIT_PATIENT.SelectedValue;
        }

        if (RDO_PATIENT_CURRENTLY_WORKING.SelectedValue.Equals(""))
        {
            txtRDO_PATIENT_CURRENTLY_WORKING.Text = "-1";
        }
        else
        {
            txtRDO_PATIENT_CURRENTLY_WORKING.Text = RDO_PATIENT_CURRENTLY_WORKING.SelectedValue;
        }


        if (RDO_PATIENT_MISSED_WORK.SelectedValue.Equals(""))
        {
            txtRDO_PATIENT_MISSED_WORK.Text = "-1";
        }
        else
        {
            txtRDO_PATIENT_MISSED_WORK.Text = RDO_PATIENT_MISSED_WORK.SelectedValue;
        }



        if (RDO_PATIENT_RETURN_TO_WORK.SelectedValue.Equals(""))
        {
            txtRDO_PATIENT_RETURN_TO_WORK.Text = "-1";
        }
        else
        {
            txtRDO_PATIENT_RETURN_TO_WORK.Text = RDO_PATIENT_RETURN_TO_WORK.SelectedValue;
        }
        _saveOperation = new SaveOperation();
        _saveOperation.WebPage = this.Page;
        _saveOperation.Xml_File = "CM_INITIAL_EVAL_WorkStatus.xml";
        _saveOperation.SaveMethod();
        /* Vivek*/
       
       // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_CM_DoctorSign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
      // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_CM_DoctorSign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
        Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();

        //if (obj.ChekCMFollowupSignPath(Convert.ToInt32(txtEventID.Text)))
       
            if (obj.ChekCMINITIALEVALSignPath(Convert.ToInt32(txtEventID.Text)))
            {
                string sz_eventID = txtEventID.Text;
                FillPDFValue(sz_eventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_CM_DoctorSign.aspx','Sign','toolbar=no,directories=no,menubar=no,scrollbars=no,status=no,resizable=no,width=700,height=575'); ", true);
            }
        ///////////////////

    }

    public void LoadData()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVAL_WorkStatus.xml";
            _editOperation.LoadData();


            if (txtRDO_DISCUSS_LIMIT_PATIENT.Text.Equals("-1"))
            {
             
            }
            else
            {
                RDO_DISCUSS_LIMIT_PATIENT.SelectedValue = txtRDO_DISCUSS_LIMIT_PATIENT.Text;
            }

            if (txtRDO_PATIENT_CURRENTLY_WORKING.Text.Equals("-1"))
            {
               
            }
            else
            {
                 RDO_PATIENT_CURRENTLY_WORKING.SelectedValue=txtRDO_PATIENT_CURRENTLY_WORKING.Text;
            }


            if (  txtRDO_PATIENT_MISSED_WORK.Text .Equals("-1"))
            {
             
            }
            else
            {
                 RDO_PATIENT_MISSED_WORK.SelectedValue=txtRDO_PATIENT_MISSED_WORK.Text;
            }



            if ( txtRDO_PATIENT_RETURN_TO_WORK.Text.Equals("-1"))
            {
              
            }
            else
            {
                RDO_PATIENT_RETURN_TO_WORK.SelectedValue = txtRDO_PATIENT_RETURN_TO_WORK.Text;
            }
          

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

    public void LoadPatientData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        EditOperation _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CM_INITIAL_EVAL_PatientInformetion.xml";
            _editOperation.LoadData();

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

    public string FillPDFValue(string EventID, string sz_CompanyID, string sz_CompanyName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string sz_CaseID = Session["CO_CHIRO_CaseID"].ToString();
        PDFValueReplacement.PDFValueReplacement objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
        string pdffilepath;
        string strGenFileName = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings["CM_Initial_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["CM_Initial_PDF_Path"].ToString();

            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
            String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/No Fault File/Medicals/CM/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, sz_CaseID);

            if (File.Exists(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName))
            {
                if (!Directory.Exists(objNF3Template.getPhysicalPath() + szDestinationDir))
                {
                    Directory.CreateDirectory(objNF3Template.getPhysicalPath() + szDestinationDir);
                }
                File.Copy(objNF3Template.getPhysicalPath() + szDefaultPath + strGenFileName, objNF3Template.getPhysicalPath() + szDestinationDir + strGenFileName);
            }

            Bill_Sys_CheckoutBO objCheckoutBO = new Bill_Sys_CheckoutBO();
            ArrayList objAL = new ArrayList();
            objAL.Add(sz_CaseID);
            objAL.Add(strGenFileName);
            objAL.Add(szDestinationDir);
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
            objAL.Add(sz_CompanyID);
            objAL.Add(EventID);
            objCheckoutBO.save_IM_DocMang(objAL);
            // Open file
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            //Response.Redirect(szOpenFilePath, false);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath + "'); ", true);
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
       
        return strGenFileName;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
