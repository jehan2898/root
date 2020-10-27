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
using System.Data.SqlClient;
using PDFValueReplacement;
using Componend;
using System.IO;


public partial class Bill_Sys_CO_PTNotes : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
    PDFValueReplacement.PDFValueReplacement objValueReplacement;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtCompanyName.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
       
        txtEventID.Text = Request.QueryString[0];
        DataSet dsObj = _objCheckoutBO.PatientName(txtEventID.Text);
        txtPatientName.Text = dsObj.Tables[0].Rows[0][0].ToString();
        txtCaseNo.Text = dsObj.Tables[0].Rows[0][2].ToString();
        txtDAO.Text = dsObj.Tables[0].Rows[0][3].ToString();
        txtInsCmp.Text = dsObj.Tables[0].Rows[0][4].ToString();
        txtClaimNumber.Text = dsObj.Tables[0].Rows[0][5].ToString();
        txtCaseID.Text = dsObj.Tables[0].Rows[0][1].ToString();
        txtDate.Text = System.DateTime.Now.ToString("MM/dd/yyyy");

        if (!Page.IsPostBack)
        {
            string EventID = _objCheckoutBO.PTNotesPatientInformation(txtEventID.Text, "SP_CO_CHECK_EVENT_ID");
            
            if (EventID == "1")
            {
                css_btnSave.Visible = false;
                btnUpdate.Visible = true;
                LoadData();
                DataSet ds = _objCheckoutBO.PTNotesPatientTreatment(txtEventID.Text, txtCompanyID.Text);
                
                    for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                    {
                        string  j = ds.Tables[0].Rows[i]["INDEX"].ToString();
                       
                        if (j == "1")
                        {
                            chkColdpack.Checked = true;
                        }
                       if(j=="2")
                        {
                            chkElectricalMS.Checked = true;
                        }
                        if (j == "3")
                        {
                            chkTherapeuticEx.Checked = true;
                        }
                        if (j =="4")
                        {
                             chkHotpack.Checked = true;
                        }
                        if (j == "5")
                        {
                              chkUltraSound.Checked = true;
                        }
                        if (j == "6")
                        {
                            chkMyofascialR.Checked = true;
                        }
                        if (j == "7")
                        {
                            chkTENS.Checked = true;
                        }
                        if (j == "8")
                        {
                            chkTherapeuticM.Checked = true;
                        }
                        if (j == "9")
                        {
                            chkParaffin.Checked = true;
                        }
                        if (j == "10")
                        {
                            chkPTEval.Checked = true;
                        }
                        if (j == "11")
                        {
                            chkInitialVisite.Checked = true;
                        }
                        if (j == "12")
                        {
                            ChkNewPatient.Checked = true;
                        }
                        if (j == "13")
                        {
                            ChkRemovalofTissues.Checked = true;
                        }
                        if (j == "14")
                        {
                            chkBalanceCoord.Checked = true;
                        }
                    
                    }
                
            }
            else
            {
                btnUpdate.Visible = false;
                css_btnSave.Visible = true;
            }
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CO_PTNotes.aspx");
        }
        #endregion

    }

    private void LoadData()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CO_PTNotes.xml";
            _editOperation.LoadData();
            if (txtDate.Text != "")
                txtDate.Text = Convert.ToDateTime(txtDate.Text).ToShortDateString();
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

    protected void css_btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _saveOperation = new SaveOperation();
        ArrayList _objArray = new ArrayList();
        try
        {
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "CO_PTNotes.xml";
            _saveOperation.SaveMethod();


            if (chkColdpack.Checked)
            {
                SaveValues(1);
            }
            if (chkElectricalMS.Checked)
            {
                SaveValues(2);
            }
            if (chkTherapeuticEx.Checked)
            {
                SaveValues(3);
            }
            if (chkHotpack.Checked)
            {
                SaveValues(4);
            }
            if (chkUltraSound.Checked)
            {
                SaveValues(5);
            }
            if (chkMyofascialR.Checked)
            {
                SaveValues(6);
            }
            if (chkTENS.Checked)
            {
                SaveValues(7);
            }
            if (chkTherapeuticM.Checked)
            {
                SaveValues(8);
            }
            if (chkParaffin.Checked)
            {
                SaveValues(9);
            }
            if (chkPTEval.Checked)
            {
                SaveValues(10);
            }
            if (chkInitialVisite.Checked)
            {
                SaveValues(11);
            }
            if (ChkNewPatient.Checked)
            {
                SaveValues(12);
            }
            if (ChkRemovalofTissues.Checked)
            {
                SaveValues(13);
            }
            if (chkBalanceCoord.Checked)
            {
                SaveValues(14);
            }

            Session["EventID"] = txtEventID.Text;
            Session["ChkOutCaseID"] = txtCaseID.Text;
            //Response.Redirect("Bill_Sys_CheckOut.aspx", false);
           // Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_CO_Signs.aspx'); ", true);
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('Bill_Sys_CO_PT_Notes_Signature.aspx'); ", true);
            
            //FillPDFValue(txtEventID.Text, txtCompanyID.Text, txtCompanyName.Text); //vivek 9/2/2010
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {

            DataSet dsDelete = _objCheckoutBO.PTNotesPatientTreatmentDelete(txtEventID.Text);
              
            _editOperation.Primary_Value = txtEventID.Text;
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "CO_UpdatePTNotes.xml";
            _editOperation.UpdateMethod();


            if (chkColdpack.Checked)
            {
                SaveValues(1);

            }
            if (chkElectricalMS.Checked)
            {
                SaveValues(2);
            }
            if (chkTherapeuticEx.Checked)
            {
                SaveValues(3);
            }
            if (chkHotpack.Checked)
            {
                SaveValues(4);
            }
            if (chkUltraSound.Checked)
            {
                SaveValues(5);
            }
            if (chkMyofascialR.Checked)
            {
                SaveValues(6);
            }
            if (chkTENS.Checked)
            {
                SaveValues(7);
            }
            if (chkTherapeuticM.Checked)
            {
                SaveValues(8);
            }
            if (chkParaffin.Checked)
            {
                SaveValues(9);
            }
            if (chkPTEval.Checked)
            {
                SaveValues(10);
            }
            if (chkInitialVisite.Checked)
            {
                SaveValues(11);
            }
            if (ChkNewPatient.Checked)
            {
                SaveValues(12);
            }
            if (ChkRemovalofTissues.Checked)
            {
                SaveValues(13);
            }
            if (chkBalanceCoord.Checked)
            {
                SaveValues(14);
            }
            FillPDFValue(txtEventID.Text, txtCompanyID.Text, txtCompanyName.Text);
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
        objValueReplacement = new PDFValueReplacement.PDFValueReplacement();
        string pdffilepath;
        string strGenFileName = "";
        try
        {
            Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
            string xmlpath = ConfigurationManager.AppSettings["PT_XML_Path"].ToString();
            string pdfpath = ConfigurationManager.AppSettings["PT_PDF_Path"].ToString();

            String szDefaultPath = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + txtCaseID.Text + "/Packet Document/";
            String szDestinationDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + txtCaseID.Text + "/No Fault File/Medicals/PT/";

            strGenFileName = objValueReplacement.ReplacePDFvalues(xmlpath, pdfpath, EventID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, txtCaseID.Text);

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
            objAL.Add(txtCaseID.Text);
            objAL.Add(strGenFileName);
            objAL.Add(szDestinationDir);
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
            objAL.Add(txtCompanyID.Text);
            objAL.Add(txtEventID.Text);
            objCheckoutBO.save_PT_DocMang(objAL);
            
            // Open file
            String szOpenFilePath = "";
            szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szDestinationDir + strGenFileName;
            //Response.Redirect("Bill_Sys_CheckOut.aspx", false);
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return strGenFileName;
    }

    public void SaveValues(int index)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            ArrayList _objArray = new ArrayList();
            _objArray.Add(index);
            _objArray.Add(txtEventID.Text);
            _objArray.Add(txtCompanyID.Text);
            obj.saveProcedureCodes(_objArray);
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

   


}
