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
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using log4net;
public partial class AJAX_Pages_Bill_Sys_Associate_Documents_Event : PageBase
{
    string caseid = "";
    string EventProcID = "";
    string szSpeciality = "";
    private ArrayList _arrayList;
    private DataSet _ds;
    private string strCaseType = "";
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    Bill_Sys_NF3_Template objNF3Template;
    string strLinkPath = null;
    private Bill_Sys_BillingCompanyObject objSessionBillingCompany;
    private Bill_Sys_UserObject objSessionUser;
    Bill_Sys_DeleteBO _deleteBO = new Bill_Sys_DeleteBO();
    //Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
    private static ILog log = LogManager.GetLogger("EditAll");
    string szRoomId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        objSessionUser = (Bill_Sys_UserObject)Session["USER_OBJECT"];
        txtCompanyID.Text = objSessionBillingCompany.SZ_COMPANY_ID;
        btnUPdate.Attributes.Add("onclick", "return confirm_update_bill_status();");
        btnupload.Attributes.Add("onclick", "return showUploadFilePopup();");
        btndelete.Attributes.Add("onClick", "return confirm_delete_document();");

        if (!IsPostBack)
        {
            szRoomId = Session["GETROOMID"].ToString();
            if (szRoomId == "All")
            {
                table_row_specialty_drpdwn.Visible = true;
            }
            else
            {
                table_row_specialty_drpdwn.Visible = false;
            }

            caseid = Request.QueryString["caseid"].ToString();
            string view = Request.QueryString[1].ToString();
            EventProcID = Request.QueryString["EProcid"].ToString();
            szSpeciality = Request.QueryString["spc"].ToString();
            txtEventProcId.Text = EventProcID;
            txtSpecility.Text = szSpeciality;
            txtCaseID.Text = caseid;
            extddlSpecialty.Flag_ID = txtCompanyID.Text;
            string patientname = Request.QueryString["patientname"].ToString();
            string dateofservice = Request.QueryString["dateofservice"].ToString();
            string lhrcode = Request.QueryString["lhrcode"].ToString();
            string caseno = Request.QueryString["caseno"].ToString();
            if (view == "YES")
            {
                btnView.Visible = false;
            }
            else
            {
                btnView.Visible = false;
            }
            GetViewDoc(caseid, objSessionBillingCompany.SZ_COMPANY_ID, EventProcID);
            _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();

            grdDelete.DataSource = _bill_Sys_ProcedureCode_BO.GetLHRDocs(caseid, objSessionBillingCompany.SZ_COMPANY_ID, EventProcID);
            grdDelete.DataBind();

            

        }
    }


    protected void btnReceivedDocument_Click(object sender, EventArgs e)
    {

    }
    protected void grdViewDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string PathNotFound = " ";
        if (e.CommandName == "View")
        {
            string path = e.Item.Cells[5].Text;
            string physicalpath = e.Item.Cells[4].Text;
            string filepath = e.Item.Cells[2].Text;
            string filename = e.Item.Cells[3].Text;
            string fullpath = path + filepath + filename;
            string fullphysicalpath = physicalpath + filepath + filename;
            if (File.Exists(fullphysicalpath))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
            }
            else
            {
                usrMessage.PutMessage("File is not available \n" + PathNotFound);
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
        }

    }
    protected void grdViewDocuments_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void GetViewDoc(string caseId, string companyID, string proc_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
            grdViewDocuments.DataSource = _bill_Sys_ProcedureCode_BO.GetLHRDocs(caseId, companyID, proc_id);
            grdViewDocuments.DataBind();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnUPdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        bool docSelected = false;
        try
        {
            bool chkDocCategory = true;
            for (int i = 0; i < grdViewDocuments.Items.Count; i++)
            {
                if (((CheckBox)(grdViewDocuments.Items[i].Cells[0].FindControl("chkView"))).Checked == true)
                {
                    docSelected = true;
                    if (((DropDownList)(grdViewDocuments.Items[i].Cells[4].FindControl("ddlreport"))).SelectedValue == "7")
                    {
                        chkDocCategory = false;
                    }
                }
            }
            if (chkDocCategory == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds42d", "alert('Please Select Document Category.');", true);
                return;
            }


            // ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds4", "alert('Doctor assign successfully.');", true);
            if (docSelected == true)
            {
                objNF3Template = new Bill_Sys_NF3_Template();
                String szDefaultPath = objNF3Template.getPhysicalPath();
                int ImageId = 0;
                string PathNotFound = "";
                foreach (DataGridItem drg in grdViewDocuments.Items)
                {
                    CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkView");
                    DropDownList drpReport = (DropDownList)drg.Cells[0].FindControl("ddlreport");
                    if (drp.Checked == true)
                    {
                        String szDestinationDir = "";

                        //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                        if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
                        {
                            //szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            szDestinationDir = objNF3Template.GetCompanyName(objSessionBillingCompany.SZ_COMPANY_ID);
                        }
                        else
                        {
                            //szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            szDestinationDir = objNF3Template.GetCompanyName(objSessionBillingCompany.SZ_COMPANY_ID);

                        }
                        // szDestinationDir = szDestinationDir + "/" + txtCaseId.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/";
                        if (drpReport.SelectedValue == "0")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/";
                        }
                        else if (drpReport.SelectedValue == "1")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/Referral/";
                        }

                        else if (drpReport.SelectedValue == "2")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/AOB/";
                        }
                        else if (drpReport.SelectedValue == "3")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Comp Authorization/";
                        }
                        else if (drpReport.SelectedValue == "4")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/HIPPA Consent/";
                        }
                        else if (drpReport.SelectedValue == "5")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Lien Form/";
                        }
                        else if (drpReport.SelectedValue == "6")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/MISC/";
                        }
                        strLinkPath = drg.Cells[2].Text + drg.Cells[3].Text;
                        if (!Directory.Exists(szDefaultPath + szDestinationDir))
                        {
                            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                        }
                        if (File.Exists(szDefaultPath + strLinkPath))
                        {

                            File.Copy(szDefaultPath + strLinkPath, szDefaultPath + szDestinationDir + drg.Cells[3].Text, true);
                            ArrayList objAL = new ArrayList();
                            //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                            if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
                            {
                                objAL.Add(objSessionBillingCompany.SZ_COMPANY_ID);
                            }
                            else
                            {
                                //objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                                objAL.Add(objSessionBillingCompany.SZ_COMPANY_ID);
                            }

                            objAL.Add(txtCaseID.Text);
                            objAL.Add(drg.Cells[3].Text);
                            objAL.Add(szDestinationDir);
                            //objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                            objAL.Add(objSessionUser.SZ_USER_NAME);
                            objAL.Add(txtSpecility.Text);
                            // ImageId = objNF3Template.saveReportInDocumentManager(objAL);

                            if (drpReport.SelectedValue == "0")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager(objAL);
                            }
                            else if (drpReport.SelectedValue == "1")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_Referral(objAL);
                            }
                            else if (drpReport.SelectedValue == "2")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_AOB(objAL);
                            }
                            else if (drpReport.SelectedValue == "3")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_NFCA(objAL);
                            }
                            else if (drpReport.SelectedValue == "4")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_NFHC(objAL);
                            }
                            else if (drpReport.SelectedValue == "5")
                            {
                                ImageId = objNF3Template.saveReportInDocumentManager_NFLF(objAL);
                            }
                            else if (drpReport.SelectedValue == "6")
                            {
                                ImageId = saveReportInDocumentManager_NFMIS(objAL);
                            }

                            if ((ImageId.ToString().Trim() != "0") && (ImageId.ToString().Trim() != ""))
                            {
                                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                _bill_Sys_ProcedureCode_BO.assignLHRDocument(objAL, ImageId, drpReport.SelectedValue, Convert.ToInt32(txtEventProcId.Text));
                            }

                            // End :   Save report under document manager.
                            //}
                            if (drpReport.SelectedValue == "0")
                            {
                                Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
                                ArrayList arrOBJ;

                                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(txtEventProcId.Text), strLinkPath, ImageId);
                            }
                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds7", "alert('Document Received  successfully.');", true);
                        }
                        else
                        {
                            if (PathNotFound == "")
                            {
                                PathNotFound = szDefaultPath + strLinkPath + ",\n";
                            }
                            else
                            {
                                PathNotFound = PathNotFound + szDefaultPath + strLinkPath + ",\n";
                            }
                        }
                    }
                }
                if (PathNotFound != "")
                {
                    usrMessage.PutMessage("These file are not available.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                    // ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds10", "alert('These file are not available');", true);
                    return;
                }
            }
            string notAdded = "";
            if (notAdded == "")
                usrMessage.PutMessage(" Document Saved  successfully.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds28", "alert('Saved  successfully.');", true);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            bool chkDocCategory = true;

            if (ddlreport.SelectedValue == "7")
            {
                chkDocCategory = false;
            }

            if (chkDocCategory == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds42d", "alert('Please Select Document Category.');", true);
                return;
            }

            objNF3Template = new Bill_Sys_NF3_Template();
            if (!fuUploadReport.HasFile)
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                return;
            }

            String szDefaultPath = objNF3Template.getPhysicalPath();
            int ImageId = 0;
            string PathNotFound = "";
            //foreach (DataGridItem drg in grdViewDocuments.Items)
            {
                // CheckBox drp = (CheckBox)drg.Cells[0].FindControl("chkView");
                // DropDownList drpReport = (DropDownList)drg.Cells[0].FindControl("ddlreport");

                String szDestinationDir = "";

                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
                {
                    //szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    szDestinationDir = objNF3Template.GetCompanyName(objSessionBillingCompany.SZ_COMPANY_ID);
                }
                else
                {
                    //szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    szDestinationDir = objNF3Template.GetCompanyName(objSessionBillingCompany.SZ_COMPANY_ID);

                }
                // szDestinationDir = szDestinationDir + "/" + txtCaseId.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/";
                if (ddlreport.SelectedValue == "0")
                {
                    szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/";
                }
                else if (ddlreport.SelectedValue == "1")
                {
                    szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Medicals/" + txtSpecility.Text + "/Referral/";
                }

                else if (ddlreport.SelectedValue == "2")
                {
                    szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/AOB/";
                }
                else if (ddlreport.SelectedValue == "3")
                {
                    szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Comp Authorization/";
                }
                else if (ddlreport.SelectedValue == "4")
                {
                    szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/HIPPA Consent/";
                }
                else if (ddlreport.SelectedValue == "5")
                {
                    szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Lien Form/";
                }
                else if (ddlreport.SelectedValue == "6")
                {
                    szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/MISC/";
                }
                //   strLinkPath = drg.Cells[2].Text + drg.Cells[3].Text;
                if (!Directory.Exists(szDefaultPath + szDestinationDir))
                {
                    Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                }

                Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings1 = new Bill_Sys_FileType_Settings();
                DataSet dscode1 = new DataSet();
                dscode1 = _Bill_Sys_FileType_Settings1.GET_IMAGE_ID(txtEventProcId.Text, fuUploadReport.FileName, txtCompanyID.Text);
                if (dscode1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dscode1.Tables[0].Rows.Count; i++)
                    {
                        ArrayList arr = new ArrayList();
                        arr.Add(dscode1.Tables[0].Rows[i]["I_ID"].ToString());
                        arr.Add(txtEventProcId.Text);
                        _Bill_Sys_FileType_Settings1.LhrDeleteDocuments(arr);
                    }

                    for (int i = 0; i < dscode1.Tables[0].Rows.Count; i++)
                    {
                        // Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                        DataSet dsimageid = new DataSet();
                        dsimageid = _Bill_Sys_FileType_Settings1.GET_IMAGE_ID_LHR(dscode1.Tables[0].Rows[i]["I_IMAGE_ID"].ToString());
                        string sz_path = ConfigurationManager.AppSettings["BASEPATH"].ToString() + dscode1.Tables[0].Rows[0]["SZ_FILE_PATH"].ToString() + dscode1.Tables[0].Rows[0]["SZ_FILE_NAME"].ToString();
                        string szFinal = sz_path.Replace("/", "\\");
                        if (dsimageid.Tables[0].Rows.Count > 0)
                        {

                        }
                        else
                        {
                            ArrayList arr = new ArrayList();
                            arr.Add(dscode1.Tables[0].Rows[i]["I_IMAGE_ID"].ToString());
                            arr.Add(dscode1.Tables[0].Rows[0]["SZ_FILE_NAME"].ToString());
                            _Bill_Sys_FileType_Settings1.Deletelhrdocuments(arr);
                            string FinalPath = "";
                            //szDefaultPath;
                            System.IO.File.Move(@szFinal, @szFinal + ".deleted");
                        }
                    }
                }

                fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
               
                ArrayList objAL = new ArrayList();
                //if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
                {
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                }
                else
                {
                    //objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    objAL.Add(objSessionBillingCompany.SZ_COMPANY_ID);
                }
                //objAL.Add(fuUploadReport.FileName);
                objAL.Add(txtCaseID.Text);
                objAL.Add(fuUploadReport.FileName);
                //objAL.Add(drg.Cells[3].Text);
                objAL.Add(szDestinationDir);
                //objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME);
                objAL.Add(objSessionUser.SZ_USER_NAME);
                objAL.Add(txtSpecility.Text);
                //ImageId = objNF3Template.saveReportInDocumentManager(objAL);

                if (ddlreport.SelectedValue == "0")
                {
                    ImageId = objNF3Template.saveReportInDocumentManager(objAL);
                }
                else if (ddlreport.SelectedValue == "1")
                {
                    ImageId = objNF3Template.saveReportInDocumentManager_Referral(objAL);
                }
                else if (ddlreport.SelectedValue == "2")
                {
                    ImageId = objNF3Template.saveReportInDocumentManager_AOB(objAL);
                }
                else if (ddlreport.SelectedValue == "3")
                {
                    ImageId = objNF3Template.saveReportInDocumentManager_NFCA(objAL);
                }
                else if (ddlreport.SelectedValue == "4")
                {
                    ImageId = objNF3Template.saveReportInDocumentManager_NFHC(objAL);
                }
                else if (ddlreport.SelectedValue == "5")
                {
                    ImageId = objNF3Template.saveReportInDocumentManager_NFLF(objAL);
                }
                else if (ddlreport.SelectedValue == "6")
                {
                    ImageId = saveReportInDocumentManager_NFMIS(objAL);
                }

                if ((ImageId.ToString().Trim() != "0") && (ImageId.ToString().Trim() != ""))
                {
                    _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                    _bill_Sys_ProcedureCode_BO.assignLHRDocument(objAL, ImageId, ddlreport.SelectedValue, Convert.ToInt32(txtEventProcId.Text));
                }

                //Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                //DataSet dscode = new DataSet();
                //dscode = _Bill_Sys_FileType_Settings.GET_Visit_Info(txtEventProcId.Text, txtCompanyID.Text);
                //if (dscode.Tables[0].Rows.Count > 0)
                //{
                //    ArrayList arr = new ArrayList();
                //    arr.Add(dscode.Tables[0].Rows[0]["i_event_id"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["i_event_proc_id"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_remote_procedure_code"]).ToString();
                //    arr.Add(fuUploadReport.FileName);
                //    // arr.Add(dscode.Tables[0].Rows[0]["sz_remote_document"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_company_id"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_remote_case_id"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["dt_visit_date"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_visit_time"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_visit_time_type"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_remote_procedure_group"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_remote_procedure_desc"]).ToString();
                //    arr.Add(dscode.Tables[0].Rows[0]["sz_remote_appointment_id"]).ToString();

                //    //_Bill_Sys_FileType_Settings.LhrDocuments(dscode.Tables[0].Rows[0]["i_event_id"].ToString(), dscode.Tables[0].Rows[0]["i_event_proc_id"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_procedure_code"].ToString, fuUploadReport.FileName.ToString(), dscode.Tables[0].Rows[0]["sz_company_id"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_case_id"].ToString(), dscode.Tables[0].Rows[0]["dt_visit_date"].ToString(), dscode.Tables[0].Rows[0]["sz_visit_time"].ToString(), dscode.Tables[0].Rows[0]["sz_visit_time_type"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_procedure_group"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_procedure_desc"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_appointment_id"].ToString());
                //    _Bill_Sys_FileType_Settings.LhrDocuments(arr);

                //}

                Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                DataSet dscode = new DataSet();
                dscode = _Bill_Sys_FileType_Settings.GET_Visit_Info(txtEventProcId.Text, txtCompanyID.Text);
                if (dscode.Tables[0].Rows.Count > 0)
                {
                    ArrayList arr1 = new ArrayList();
                    arr1.Add(dscode.Tables[0].Rows[0]["i_event_id"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["i_event_proc_id"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_remote_procedure_code"]).ToString();
                    arr1.Add(fuUploadReport.FileName);
                    // arr.Add(dscode.Tables[0].Rows[0]["sz_remote_document"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_company_id"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_remote_case_id"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["dt_visit_date"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_visit_time"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_visit_time_type"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_remote_procedure_group"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_remote_procedure_desc"]).ToString();
                    arr1.Add(dscode.Tables[0].Rows[0]["sz_remote_appointment_id"]).ToString();

                    //_Bill_Sys_FileType_Settings.LhrDocuments(dscode.Tables[0].Rows[0]["i_event_id"].ToString(), dscode.Tables[0].Rows[0]["i_event_proc_id"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_procedure_code"].ToString, fuUploadReport.FileName.ToString(), dscode.Tables[0].Rows[0]["sz_company_id"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_case_id"].ToString(), dscode.Tables[0].Rows[0]["dt_visit_date"].ToString(), dscode.Tables[0].Rows[0]["sz_visit_time"].ToString(), dscode.Tables[0].Rows[0]["sz_visit_time_type"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_procedure_group"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_procedure_desc"].ToString(), dscode.Tables[0].Rows[0]["sz_remote_appointment_id"].ToString());
                    _Bill_Sys_FileType_Settings.Check_and_Insert_Visit_Doc(arr1);

                }

                if (ddlreport.SelectedValue == "0")
                {
                    Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
                    ArrayList arrOBJ;

                    _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                    _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(txtEventProcId.Text), strLinkPath, ImageId);
                }
            }
            UpdateReportProcedureCodeList();
            //need to change
            //string strpath = ConfigurationManager.AppSettings["BASEPATH"].ToString() + "\\" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + txtCaseID.Text + "\\" + "Intake" + "\\" + "Misc" + "\\" + fuUploadReport.FileName;
            //string szDesDir = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + txtCaseID.Text + "\\" + "Intake" + "\\" + "Misc" + "\\";

            //string szpa = ConfigurationManager.AppSettings["BASEPATH"].ToString() + "\\" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "\\" + txtCaseID.Text + "\\" + "Intake" + "\\" + "Misc";

            string strpath = ConfigurationManager.AppSettings["BASEPATH"].ToString() + ConfigurationManager.AppSettings["MiscCommanFolder"].ToString() + "\\" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "\\" + txtCaseID.Text + "\\" + ConfigurationManager.AppSettings["MiscFolder"].ToString() + "\\" + fuUploadReport.FileName;
            string szDesDir = ConfigurationManager.AppSettings["MiscCommanFolder"].ToString() + "\\" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "\\" + txtCaseID.Text + "\\" + ConfigurationManager.AppSettings["MiscFolder"].ToString() + "\\";
            string szpa = ConfigurationManager.AppSettings["BASEPATH"].ToString() + ConfigurationManager.AppSettings["MiscCommanFolder"].ToString() + "\\" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "\\" + txtCaseID.Text + "\\" + ConfigurationManager.AppSettings["MiscFolder"].ToString();

            if (!File.Exists(strpath))
            {
                if (!Directory.Exists(szpa))
                {
                    Directory.CreateDirectory(szpa);
                }
                //System.IO.File.Copy(szDefaultPath, strpath);
                fuUploadReport.SaveAs(strpath);
                
                ArrayList objAL = new ArrayList();
                if (objSessionBillingCompany.BT_REFERRING_FACILITY == true)
                {
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                }
                else
                {
                    objAL.Add(objSessionBillingCompany.SZ_COMPANY_ID);
                }

                objAL.Add(txtCaseID.Text);
                objAL.Add(fuUploadReport.FileName);
                objAL.Add(szDesDir);
                objAL.Add(objSessionUser.SZ_USER_NAME);
                objAL.Add(txtSpecility.Text);

                int IId = objNF3Template.saveReportInDocumentManager_INTAKE_MISC(objAL);
            }


            usrMessage.PutMessage("Document Upload  successfully.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            GetViewDoc(txtCaseID.Text, txtCompanyID.Text, txtEventProcId.Text);
            // ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds28", "alert('Upload  successfully.');", true);


        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    public void UpdateReportProcedureCodeList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_UPADTE_LHR_PROC_TYPE", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", extddlSpecialty.Text);
            sqlCmd.Parameters.AddWithValue("@I_EVENT_PROC_ID", txtEventProcId.Text);

            sqlCmd.ExecuteNonQuery();

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }


    public int saveReportInDocumentManager_NFMIS(ArrayList objAL)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        int returnValue = 0;
        SqlConnection sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_SAVE_REPORT_IN_DM_NFMIS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", objAL[1].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_NAME", objAL[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_FILE_PATH", objAL[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_USER_NAME", objAL[4].ToString());
            if (objAL[5].ToString().Equals("X-RAY"))
            {
                sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", "XRAY");
            }
            else
            {

                sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", objAL[5].ToString());
            }
            SqlParameter sqlReturn = new SqlParameter();
            sqlReturn.Direction = ParameterDirection.ReturnValue;

            sqlCmd.Parameters.Add(sqlReturn);
            sqlCmd.ExecuteNonQuery();
            returnValue = (int)sqlReturn.Value;
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return returnValue;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnopen_Click(object sender, EventArgs e)
    {
       
        string path = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
        string physicalpath = ConfigurationManager.AppSettings["FILE_URL"].ToString();
        for (int i = 0; i < grdViewDocuments.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)grdViewDocuments.Items[i].FindControl("chkView");
            if (chk.Checked)
            {

                string filepath = grdViewDocuments.Items[i].Cells[2].Text;
                string filename = grdViewDocuments.Items[i].Cells[3].Text;
                string fullpath = path + filepath + filename;
                string fullphysicalpath = physicalpath + filepath + filename;
                if (File.Exists(fullphysicalpath))
                {
                    //  ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');\n winref.close()", true);
                    // ScriptManager.RegisterStartupScript(this, typeof(string), "1", "winref = window.open('" + fullpath + "');", true);

                    ScriptManager.RegisterStartupScript(this, typeof(string), "popup" + i.ToString(), " window.open('" + fullpath + "');", true);

                }
            }

        }


    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        Bill_Sys_Upload_VisitReport obj = new Bill_Sys_Upload_VisitReport();
        for (int i = 0; i < grdViewDocuments.Items.Count; i++)
        {
            string szDoctype = obj.GetFiledDocType(grdViewDocuments.Items[i].Cells[3].Text, txtEventProcId.Text);
            if (szDoctype != "")
            {
                DropDownList drp = (DropDownList)grdViewDocuments.Items[i].FindControl("ddlreport");
                CheckBox chk = (CheckBox)grdViewDocuments.Items[i].FindControl("chkView");
                drp.Text = szDoctype;
                chk.Checked = true;
            }
        }
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        log.Debug("Start Delete button event.");
        string szImageID = "";
        DataSet dsImageID = new DataSet();
        for (int i = 0; i < grdDelete.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)grdDelete.Items[i].FindControl("chkView");
            // DropDownList drp = (DropDownList)grdDelete.Items[i].FindControl("ddlreport");//dropdown
            if (chk.Checked)
            {
                try
                {
                    string filename = grdDelete.Items[i].Cells[3].Text;
                    string szEventProcId = txtEventProcId.Text;
                    dsImageID = _deleteBO.GetImageIDLhr(szEventProcId, filename);
                   // need to change
                 //   string szMISCpath = objSessionBillingCompany.SZ_COMPANY_NAME + "\\" + txtCaseID.Text + "\\" + "Intake" + "\\" + "MISC" + "\\";
                    string szMISCpath = ConfigurationManager.AppSettings["MiscCommanFolder"].ToString() + "\\" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "\\" + txtCaseID.Text + "\\" + ConfigurationManager.AppSettings["MiscFolder"].ToString() + "\\";
                    string Basepath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
                    string fullpath = grdDelete.Items[i].Cells[2].Text;
                    string szFullfilePath = Basepath + fullpath + filename;
                    string FinalPath = szFullfilePath.Replace("/", "\\");
                    log.Debug("filename : " + filename);
                    log.Debug("szEventProcId :" + szEventProcId);
                    log.Debug("szMISCpath :" + szMISCpath);

                    DataSet dsResult = new DataSet();
                    if (filename != "" && szEventProcId != "" && szMISCpath != "")
                    {
                        if (dsImageID.Tables[0].Rows.Count > 0)
                        {
                            for (int k = 0; dsImageID.Tables[0].Rows.Count > k; k++)
                            {
                                szImageID = dsImageID.Tables[0].Rows[k][0].ToString();
                                log.Debug("szImageID :" + szImageID);
                                dsResult = _deleteBO.DeleteDocuments(szEventProcId, filename, szImageID, szMISCpath);
                                if (dsResult.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                                {
                                    System.IO.Directory.Move(FinalPath, FinalPath + ".deleted");
                                    log.Debug("dsResult : SUCCESS");
                                    /// ScriptManager.RegisterStartupScript(this.Page, GetType(), "ss", "<script language='javascript'> alert('File successfully Deleted..') </script>", true);
                                    /// 
                                    //Page.RegisterStartupScript("Test", "<script language='javascript'> alert('File successfully Deleted..') </script>");
                                    usrMessage1.PutMessage("File successfully Deleted.");
                                    usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                                    usrMessage1.Show();

                                }
                            }
                        }
                        else
                        {
                            dsResult = _deleteBO.DeleteDocuments(szEventProcId, filename, szImageID, szMISCpath);
                            if (dsResult.Tables[0].Rows[0][0].ToString() == "SUCCESS")
                            {
                                if (Directory.Exists(FinalPath))
                                {
                                    System.IO.Directory.Move(FinalPath, FinalPath + ".deleted");
                                    log.Debug("dsResult : SUCCESS");
                                    grdDelete.DataSource = _bill_Sys_ProcedureCode_BO.GetLHRDocs(txtCaseID.Text, objSessionBillingCompany.SZ_COMPANY_ID, txtEventProcId.Text);
                                    grdDelete.DataBind();
                                    //Page.RegisterStartupScript("Test", "<script language='javascript'> alert('File successfully Deleted..') </script>");
                                    //ScriptManager.RegisterStartupScript(this.Page, GetType(), "ss", "<script language='javascript'> alert('File successfully Deleted..') </script>", true);
                                    usrMessage1.PutMessage("File successfully Deleted.");
                                    usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                                    usrMessage1.Show();
                                }
                                else
                                {
                                    usrMessage1.PutMessage("File Not Found");
                                    usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                                    usrMessage1.Show();
                                }
                            }
                        }

                    }
                    else
                    {
                        log.Debug("Failed to delete file!");
                    }
                    GetViewDoc(txtCaseID.Text, objSessionBillingCompany.SZ_COMPANY_ID, szEventProcId);
                    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO1 = new Bill_Sys_ProcedureCode_BO();
                    grdDelete.DataSource = _bill_Sys_ProcedureCode_BO1.GetLHRDocs(txtCaseID.Text, txtCompanyID.Text, txtEventProcId.Text);
                    grdDelete.DataBind();
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    using (Utils utility = new Utils())
                    {
                        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                    }
                    string str2 = "Error Request=" + id + ".Please share with Technical support.";
                    base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

                }
            }



        }
        mpDelete.Show();

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        string szCaseID = txtCaseID.Text;
        string szEventProcID = txtEventProcId.Text;
        //GetViewDoc(szCaseID, objSessionBillingCompany.SZ_COMPANY_ID, szEventProcID);
        Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        grdDelete.DataSource = _bill_Sys_ProcedureCode_BO.GetLHRDocs(szCaseID, objSessionBillingCompany.SZ_COMPANY_ID, szEventProcID);
        grdDelete.DataBind();
        mpDelete.Show();
    }


    protected void grdDelete_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string PathNotFound = " ";
        if (e.CommandName == "View")
        {
            string path = e.Item.Cells[5].Text; ;
            string physicalpath = e.Item.Cells[4].Text; 
            string filepath = e.Item.Cells[2].Text;
            string filename = e.Item.Cells[3].Text;
            string fullpath = path + filepath + filename;
            string fullphysicalpath = physicalpath + filepath + filename;
            if (File.Exists(fullphysicalpath))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
            }
            else
            {
                usrMessage1.PutMessage("File is not available \n" + PathNotFound);
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage1.Show();
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + fullpath + "');", true);
        }
    }
    //protected void lnkbtnDelete_Click(object sender, EventArgs e)
    //{
    //    objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
    //    string szCaseID = txtCaseID.Text;
    //    string szEventProcID = txtEventProcId.Text;
    //    //GetViewDoc(szCaseID, objSessionBillingCompany.SZ_COMPANY_ID, szEventProcID);
    //    Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
    //    grdDelete.DataSource = _bill_Sys_ProcedureCode_BO.GetLHRDocs(szCaseID, objSessionBillingCompany.SZ_COMPANY_ID, szEventProcID);
    //    grdDelete.DataBind();
    //    mpDelete.Show();
    //}

    protected void extddlSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSpecility.Text = extddlSpecialty.Selected_Text;
        string s = extddlSpecialty.Text;
    }
}
