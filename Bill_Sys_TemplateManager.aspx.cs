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
//using FSComponent;
using System.Text;
using System.IO;
public partial class Bill_Sys_TemplateManager : PageBase
{
   // private DAO_NOTES_BO _DAO_NOTES_BO;
    //private DAO_NOTES_EO _DAO_NOTES_EO;

    private GeneratePDFBO _generatePDFBO;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            if (!IsPostBack)
            {
                ddlTemplate.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if (Session["CASE_OBJECT"] != null)
                {
                    //////////////////////
                    //CREATE SESSION FOR DOC MANAGER,TEMPLATE MANAGER,Notes,Bills
                    txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;

                    Bill_Sys_Case _bill_Sys_Case = new Bill_Sys_Case();
                    _bill_Sys_Case.SZ_CASE_ID = txtCaseID.Text;

                    Session["CASEINFO"] = _bill_Sys_Case;

                    Session["PassedCaseID"] = txtCaseID.Text;
                    String szURL = "";
                    String szCaseID = Session["PassedCaseID"].ToString();
                    Session["QStrCaseID"] = szCaseID;
                    Session["Case_ID"] = szCaseID;
                    Session["Archived"] = "0";
                    Session["QStrCID"] = szCaseID;
                    Session["SelectedID"] = szCaseID;
                    Session["DM_User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    Session["User_Name"] = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                    Session["SN"] = "0";
                    Session["LastAction"] = "vb_CaseInformation.aspx";


                    Session["SZ_CASE_ID_NOTES"] = txtCaseID.Text;

                    Session["TM_SZ_CASE_ID"] = txtCaseID.Text;

                    GetPatientDeskList();
                    //
                    ///////////////////

                }
            }
            //txtUserID.Text = ((PFS_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
           
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_TemplateManager.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
  
    protected void btnGeneratePDF_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _generatePDFBO = new GeneratePDFBO();
            if (_generatePDFBO.GetTemplateType(ddlTemplate.Text))
            {
                GenerateHillSidePDF();
            }
            else
            {
                #region "Logic to generate Word Document"
                String szFundID = txtCaseID.Text;
                String szPDFURL = "";
                szPDFURL = ConfigurationManager.AppSettings["PDFURL"];
                String szPDFWithPath = "";
                string szPDFName = "";
                GeneratePDFBO _obj = new GeneratePDFBO();
                String szXMLFileName = "";
                String szDefaultPDFFileName = "";
                //if (rdSelectOne.SelectedValue == "MS-Word")
                // {
                if (ddlTemplate.Text != "NA" && ddlTemplate.Text != "")
                {
                    ArrayList _arrayList = new ArrayList();
                    _generatePDFBO = new GeneratePDFBO();
                    string strFilename = _generatePDFBO.GetDocPhysicalPath(Convert.ToInt32(ddlTemplate.Text)).Trim();
                    string strOutPutFile = getFileName(txtCompanyID.Text) + ".doc";
                    //string strOutPutFile = "";
                    if (File.Exists(ConfigurationManager.AppSettings["DOC_INPUT_FILE_PATH"] + txtCompanyID.Text + "\\" + strFilename))
                    {
                        _arrayList.Add(ddlTemplate.Text);
                        _arrayList.Add(ConfigurationManager.AppSettings["DOC_INPUT_FILE_PATH"] + txtCompanyID.Text + "\\" + strFilename);
                        _arrayList.Add(ConfigurationManager.AppSettings["DOC_OUT_FILE_PATH"] + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\GeneratedDoc\\" + strOutPutFile);
                        _arrayList.Add(szFundID);
                        _arrayList.Add(ConfigurationManager.AppSettings["VIEWWORD_DOC"] + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/GeneratedDoc/" + strOutPutFile);

                        _generatePDFBO.SaveDocTemplate(_arrayList);

                        while (true)
                        {
                            string stroutpath = ConfigurationManager.AppSettings["DOC_OUT_FILE_PATH"] + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\GeneratedDoc\\" + strOutPutFile;

                            int i_flag = _generatePDFBO.GetDocFlag(Convert.ToInt32(ddlTemplate.Text), stroutpath, szFundID);
                            if (i_flag == 1)
                            {
                                string popuppage = ConfigurationManager.AppSettings["VIEWWORD_DOC"] + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/GeneratedDoc/" + strOutPutFile;
                                //Response.Write("<script language='javascript'>window.open('popupdoc.aspx?url=" + popuppage + "','name','height=200,width=200,top=400,left=350');</script>");
                                Response.Redirect(popuppage, false);



                                break;
                            }
                            else if (i_flag == 0)
                            {

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>alert('Document does not exists....');</script>");
                    }
                }
                //}
                //else
                //{

                //    switch (ddlTemplate.Text)
                //    {
                //        case "1":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XMLFILE"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDFFILE"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);
                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //  _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);

                //            break;
                //        case "2":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XMLFILE2"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDFFILE2"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //  _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "3":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSC_K_SINGLE_MERGED_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSC_K_SINGLE_MERGED_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //  _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;

                //        case "4":

                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_PAY_OFF_LETTER"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_PAY_OFF_LETTER"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "5":

                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSC_PLTF_SETTLED_FUNDING_K_MERGED"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSC_PLTF_SETTLED_FUNDING_K_MERGED"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "6":

                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSC_PLTF_SETTLED_FUNDING_K_TEMP"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSC_PLTF_SETTLED_FUNDING_K_TEMP"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "7":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFIKAFR_WPAYOFF_MULTIPLE_MERGED_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFIKAFR_WPAYOFF_MULTIPLE_MERGED_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "8":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_MULT_WPAYOFF_MERGED_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_MULT_WPAYOFF_MERGED_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;

                //        case "9":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_SINGLE_MERGED_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_SINGLE_MERGED_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "10":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_AFR_9m"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_AFR_9m"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "11":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_AFR_Multiple_9m"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_AFR_Multiple_9m"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "12":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_Multiple_9m"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_Multiple_9m"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "13":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_MULTIPLE_BUYOUT_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_MULTIPLE_BUYOUT_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "14":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_SINGLE_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_SINGLE_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "15":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_K_SINGLE_BUYOUT_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_K_SINGLE_BUYOUT_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "16":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_PLTF_SETTLED_FUNDING_K"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_PLTF_SETTLED_FUNDING_K"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;

                //        case "17":
                //            szXMLFileName = ConfigurationSettings.AppSettings["XML_LSFI_SHORERD_K_SINGLE_9M"];
                //            szDefaultPDFFileName = ConfigurationSettings.AppSettings["PDF_LSFI_SHORERD_K_SINGLE_9M"];
                //            szPDFName = _obj.GeneratePDF(Session["SZ_CASE_ID"].ToString(), szXMLFileName, szDefaultPDFFileName, szFundID);
                //            szPDFWithPath = szPDFURL + Session["SZ_CASE_ID"].ToString() + "/Packet Document/" + szPDFName;
                //            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szPDFWithPath + "'); ", true);

                //            _DAO_NOTES_EO = new DAO_NOTES_EO();
                //            _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "TEMPLATE_GENERATE";
                //            //    _DAO_NOTES_EO.SZ_ACTIVITY_DESC = szPDFName;
                //            _DAO_NOTES_EO.SZ_ACTIVITY_DESC = ddlTemplate.Selected_Text;

                //            _DAO_NOTES_BO = new DAO_NOTES_BO();
                //            _DAO_NOTES_EO.SZ_USER_ID = txtUserID.Text;
                //            _DAO_NOTES_EO.SZ_CASE_ID = txtCaseID.Text;
                //            _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);
                //            break;
                //        case "18":
                //            Response.Write("<script language='javascript'>alert('Document does not exists....');</script>");
                //            break;
                //    }
                //}
                #endregion
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

    private void GenerateHillSidePDF()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szXMLFileName = ConfigurationManager.AppSettings["HILLSIDE_XML_FILE"].ToString();
            string szPDFFileName = ConfigurationManager.AppSettings["HILLSIDE_PDF_FILE"].ToString();
            PDFValueReplacement.PDFValueReplacement objPDFReplacement = new PDFValueReplacement.PDFValueReplacement();

            string szGeneratedPDFFile = objPDFReplacement.ReplacePDFvalues(szXMLFileName, szPDFFileName, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            String szDefaultPath = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID + "/Packet Document/" + szGeneratedPDFFile;
            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szDefaultPath + "'); ", true);
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
    // To load Patient Details
    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            grdPatientDeskList.DataSource = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            grdPatientDeskList.DataBind();

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

    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }

    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }
   
}
