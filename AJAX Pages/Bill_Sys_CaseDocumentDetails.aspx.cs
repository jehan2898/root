using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;


public partial class AJAX_Pages_Bill_Sys_CaseDocumentDetails : PageBase
{
    Bill_Sys_NF3_Template objNF3Template;
    private Bill_Sys_SystemObject objSessionSystem;
    private Bill_Sys_BillingCompanyObject objSessionBillingCompany;
    private Bill_Sys_BillingCompanyObject objSessionCompanyAppStatus;
    private Bill_Sys_UserObject objSessionUser;
    string strLinkPath = null;
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        objSessionSystem = (Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"];
        objSessionBillingCompany = (Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"];
        objSessionCompanyAppStatus = (Bill_Sys_BillingCompanyObject)Session["APPSTATUS"];
        objSessionUser = (Bill_Sys_UserObject)Session["USER_OBJECT"];
        btnOpenDocument.Attributes.Add("onClick", "return CheckDocument();");
        if (!IsPostBack)
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlSpecialty.Flag_ID = txtCompanyID.Text;
            Session["Refresh"] = "1";
            chkrefreshpaidbills.Checked = true;
            if (Request.QueryString["CaseID"] != null && Request.QueryString["EProcid"] != null)
            {
                txtCaseID.Text = Request.QueryString["CaseID"].ToString();
                txtEventProcID.Text = Request.QueryString["EProcid"].ToString();
                txtProcGroup.Text = Request.QueryString["ProcGroup"].ToString();
                txtProcGroupID.Text = Request.QueryString["ProcGroupID"].ToString();
            }

            if (txtProcGroup.Text == "OT" || txtProcGroup.Text == "")
            {
                extddlSpecialty.Visible = true;
            }
            else
            {
                extddlSpecialty.Visible = false;
                extddlSpecialty.Text = txtProcGroupID.Text;
                hdnmdltxtSpeciality.Text = extddlSpecialty.Selected_Text;
            }

            try
            {
                Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                grdViewDocuments.DataSource = _bill_Sys_ProcedureCode_BO.GetDocsNew(txtCaseID.Text, txtCompanyID.Text, txtEventProcID.Text);
                grdViewDocuments.DataBind();



                DataSet sdFiles = new DataSet();
                sdFiles = GetFiledDocTypeByProcID(txtEventProcID.Text);
                if (sdFiles.Tables.Count > 0)
                {
                    if (sdFiles.Tables[0].Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < grdViewDocuments.Items.Count; i1++)
                        {
                            for (int j = 0; j < sdFiles.Tables[0].Rows.Count; j++)
                            {


                                if (grdViewDocuments.Items[i1].Cells[3].Text.Trim() == sdFiles.Tables[0].Rows[j][0].ToString().Trim())
                                {
                                    DropDownList drp = (DropDownList)grdViewDocuments.Items[i1].FindControl("ddlreport");
                                    CheckBox chk = (CheckBox)grdViewDocuments.Items[i1].FindControl("chkView");
                                    drp.Text = sdFiles.Tables[0].Rows[j][1].ToString().Trim();
                                    chk.Checked = true;
                                }
                            }
                        }
                    }

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
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetFiledDocTypeByProcID(string szProcId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        SqlConnection conn;
        SqlCommand comm;

        string constring = ConfigurationManager.AppSettings["Connection_String"].ToString();
        conn = new SqlConnection(constring);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();

            comm = new SqlCommand("SP_GET_SZ_DOCUMENT_TYPE_ID_USING_PROC_ID", conn);

            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@I_EVENT_PROC_ID", szProcId);

            SqlDataAdapter da = new SqlDataAdapter(comm);

            da.Fill(ds);


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
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void grdViewDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string PathNotFound = " ";
        if (e.CommandName == "View")
        {
            string path = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
            string physicalpath = ConfigurationManager.AppSettings["FILE_URL"].ToString();
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
    protected void extddlSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnmdltxtSpeciality.Text = extddlSpecialty.Selected_Text;

    }
    protected void btnUPdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        bool docSelected = true;
        try
        {
            bool chkDocCategory = true;


            if (extddlSpecialty.Visible && extddlSpecialty.Text == "NA")
            {

                ScriptManager.RegisterClientScriptBlock(this, GetType(), "ds42d", "alert('Please Select Specialty.');", true);

                return;
            }
            for (int i = 0; i < grdViewDocuments.Items.Count; i++)
            {
                if (((CheckBox)(grdViewDocuments.Items[i].Cells[0].FindControl("chkView"))).Checked == true)
                {
                    docSelected = true;
                    if (((DropDownList)(grdViewDocuments.Items[i].Cells[4].FindControl("ddlreport"))).SelectedValue == "8")
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
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Medicals/" + hdnmdltxtSpeciality.Text + "/";
                        }
                        else if (drpReport.SelectedValue == "1")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Medicals/" + hdnmdltxtSpeciality.Text + "/Referral/";
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
                        else if (drpReport.SelectedValue == "7")
                        {
                            szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/MISC/";
                        }


                        strLinkPath = drg.Cells[2].Text + drg.Cells[3].Text;
                        if (!Directory.Exists(szDefaultPath + szDestinationDir))
                        {
                            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                        }



                        Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                        DataSet dscode = new DataSet();
                        dscode = _Bill_Sys_FileType_Settings.GET_IMAGE_ID(txtEventProcID.Text, drg.Cells[3].Text, txtCompanyID.Text);
                        if (dscode.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dscode.Tables[0].Rows.Count; i++)
                            {
                                ArrayList arr = new ArrayList();
                                arr.Add(dscode.Tables[0].Rows[i]["I_ID"].ToString());
                                arr.Add(txtEventProcID.Text);
                                _Bill_Sys_FileType_Settings.LhrDeleteDocuments(arr);


                            }

                            for (int i = 0; i < dscode.Tables[0].Rows.Count; i++)
                            {
                                // Bill_Sys_FileType_Settings _Bill_Sys_FileType_Settings = new Bill_Sys_FileType_Settings();
                                DataSet dsimageid = new DataSet();
                                dsimageid = _Bill_Sys_FileType_Settings.GET_IMAGE_ID_LHR(dscode.Tables[0].Rows[i]["I_IMAGE_ID"].ToString());
                                string sz_path = ConfigurationManager.AppSettings["BASEPATH"].ToString() + dscode.Tables[0].Rows[0]["SZ_FILE_PATH"].ToString() + dscode.Tables[0].Rows[0]["SZ_FILE_NAME"].ToString();
                                string szFinal = sz_path.Replace("/", "\\");
                                if (dsimageid.Tables[0].Rows.Count > 0)
                                {


                                }
                                else
                                {
                                    ArrayList arr = new ArrayList();
                                    arr.Add(dscode.Tables[0].Rows[i]["I_IMAGE_ID"].ToString());
                                    arr.Add(drg.Cells[3].Text);
                                    _Bill_Sys_FileType_Settings.Deletelhrdocuments(arr);
                                    string FinalPath = "";
                                    //szDefaultPath;
                                    try
                                    {
                                        System.IO.File.Move(@szFinal, @szFinal + ".deleted");
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
                            objAL.Add(hdnmdltxtSpeciality.Text);
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
                            else if (drpReport.SelectedValue == "7")
                            {
                                if (File.Exists(szDefaultPath + strLinkPath))
                                {
                                    string szImgID = "";
                                    string szDestinationDir2 = "";
                                    szDestinationDir2 = drg.Cells[2].Text.Replace("/", "\\");
                                    szImgID = objNF3Template.GetImageID(drg.Cells[3].Text, drg.Cells[2].Text, szDestinationDir2);
                                    ImageId = Convert.ToInt32(szImgID);
                                }
                            }

                            if ((ImageId.ToString().Trim() != "0") && (ImageId.ToString().Trim() != ""))
                            {
                                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                _bill_Sys_ProcedureCode_BO.assignLHRDocument(objAL, ImageId, drpReport.SelectedValue, Convert.ToInt32(txtEventProcID.Text));
                            }

                            // End :   Save report under document manager.
                            //}
                            if (drpReport.SelectedValue == "0")
                            {
                                Bill_Sys_ReferalEvent _bill_Sys_ReferalEvent;
                                ArrayList arrOBJ;

                                _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
                                _bill_Sys_ProcedureCode_BO.UpdateReportProcedureCodeList(Convert.ToInt32(txtEventProcID.Text), strLinkPath, ImageId);
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
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
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

    protected void OnCheckedChanged_paidbillsrefesh(object sender, EventArgs e)
    {
        if (chkrefreshpaidbills.Checked)
        {
            Session["Refresh"] = "1";

        }
        else
        {
            Session["Refresh"] = "0";
        }

    }
    protected void btnOpenDocument_Click(object sender, EventArgs e)
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
                    ScriptManager.RegisterStartupScript(this, typeof(string), "popup" + i.ToString(), " window.open('" + fullpath + "');", true);
                }
            }
        }
    }
    
}