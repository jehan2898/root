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
using XMLDMLComponent;
using mbs.dao;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using log4net;
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_AssignSysSettings : PageBase
{
    private Bill_Sys_SystemObject _bill_Sys_SystemObject;
    private static ILog log = LogManager.GetLogger("Assign_Sys_Setting");

    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.txtSystemKeyValue.Text = "";
        this.ddlSchTime.SelectedValue = "AM";
        this.extddlSysKeys.Text = "NA";
        this.extddlUserLawFirm.Text = "NA";
        this.chkChartNumber.Checked = false;
        this.extddlSysKeys.Enabled = true;
        this.btnUpdate.Enabled = false;
        this.extdllUserName.Text = "NA";
        this.btnSave.Enabled = true;
        this.extdlLDateFormate.Text = "NA";
        this.Session["Setting_Id"] = null;
        extddlVisitType.Text = "NA";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_DeleteBO ebo = new Bill_Sys_DeleteBO();
        string str = "";
        try
        {
            for (int i = 0; i < this.grdSettings.Rows.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdSettings.Rows[i].FindControl("chkDelete");
                if (box.Checked && !ebo.deleteRecord("SP_MST_SYS_SETTINGS", "@SZ_SYS_SETTING_ID", this.grdSettings.DataKeys[i]["SZ_SYS_SETTING_ID"].ToString()))
                {
                    if (str == "")
                    {
                        str = this.grdSettings.Rows[i].Cells[1].Text.ToString();
                    }
                    else
                    {
                        str = str + " , " + this.grdSettings.Rows[i].Cells[1].Text.ToString();
                    }
                }
            }
            if (str != "")
            {
                this.usrMessage.PutMessage("Records for System Settings " + str + "  exists.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage("key deleted successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                this.SetDefaultSettigns();
                this.grdSettings.XGridBindSearch();
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        if (this.Session["Bit"] != null)
        {
            string text = "";
            if (this.Session["Bit"].ToString() == "1")
            {
                text = this.extddlUserLawFirm.Text;
                log.Debug("Add button click - szValue = " + text);
            }
            else if (this.Session["Bit"].ToString() == "2")
            {
                if (this.chkChartNumber.Checked)
                {
                    text = "1";
                }
                else
                {
                    text = "0";
                }
                log.Debug("Add button click - szValue = " + text);
            }
            else if (this.Session["Bit"].ToString() == "3")
            {
                text = this.extdllUserName.Text;
                log.Debug("Add button click - szValue = " + text);
            }
            else if (this.Session["Bit"].ToString() == "4")
            {
                text = this.extddlTreatmentAddress.Selected_Text;
                log.Debug("Add button click - szValue = " + text);
            }
            else if (this.Session["Bit"].ToString() == "5")
            {
                text = this.extdlPOS.Text;
                log.Debug("Add button click - szValue = " + text);
            }
            else if (this.Session["Bit"].ToString() == "6")
            {

                string szreturn = GetIniEval();
                if (szreturn != "0")
                {
                    this.usrMessage.PutMessage("You have Initial Evaluation for specialty so you can't set default visit type ");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
                    return;
                }

                if (this.chkChartNumber.Checked)
                {
                    text = "1";
                }
                else
                {
                    text = "0";
                }
                log.Debug("Add button click - szValue = " + text);
            }
            else if (this.Session["Bit"].ToString() == "0")
            {
                text = this.txtSystemKeyValue.Text;
                log.Debug("Add button click - szValue = " + text);
                try
                {
                    Convert.ToDateTime(text + " " + this.ddlSchTime.SelectedValue.ToString());
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
            Bill_Sys_Settings settings = new Bill_Sys_Settings();
            log.Debug("Give call to Save_Key_Settings method in App_code - szValue = " + text);
            int num = 0;
            if (this.Session["Bit"].ToString() == "6")
            {



                num = settings.Save_Key_Settings(this.extddlSysKeys.Text, text, this.txtCompanyID.Text, extddlVisitType.Text);

            }
            else
            {
                num = settings.Save_Key_Settings(this.extddlSysKeys.Text, text, this.txtCompanyID.Text, this.ddlSchTime.SelectedValue.ToString());
            }
            //log.Debug("Save_Key_Settings method returns - i = " + num);
            if (num != 0)
            {
                this.usrMessage.PutMessage("key added successfully.");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                if (this.extddlSysKeys.Text == "SS00012")
                {
                    if (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID == text)
                    {
                        this.Session["IPAdmin"] = "True";
                    }
                    else
                    {
                        this.Session["IPAdmin"] = "False";
                    }
                }
                this.grdSettings.XGridBindSearch();
                this.SetDefaultSettigns();
            }
            else
            {
                this.usrMessage.PutMessage("key  already exists or erro in transaction");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnSaveEmail_OnClick(object sender, EventArgs e)
    {
        this.lblmsgEmail.Text = "";
        if (this.extddlLawFirm.Text != "NA")
        {
            if (this.txtEmailAddress.Text != "")
            {
                DataSet set = new DataSet();
                string str = ConfigurationManager.AppSettings["Connection_String"];
                SQLToDAO odao = new SQLToDAO(str);
                FileTransferEmailNotifications_DAO s_dao = new FileTransferEmailNotifications_DAO();
                s_dao.sz_company_id = this.txtCompanyID.Text;
                s_dao.sz_law_firm_company_id = this.extddlLawFirm.Text;
                s_dao.sz_user_id = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                s_dao.sz_email = this.txtEmailAddress.Text;
                s_dao.i_flag = 0;
                set = odao.LoadDataSet("SP_MANAGE_FILE_TRANSFER_NOTIFICATIONS", "mbs.dao.FileTransferEmailNotifications_DAO", s_dao, "mbs.dao");
                if ((set.Tables.Count >= 1) && (set.Tables[0].Rows.Count >= 1))
                {
                    this.txtEmailAddress.Text = set.Tables[0].Rows[0][0].ToString();
                }
                this.lblmsgEmail.Text = "Record Inserted Successfully";
            }
            else
            {
                this.lblmsgEmail.Text = "Please Enter Email Address";
            }
        }
        else
        {
            this.lblmsgEmail.Text = "Please Select LawFirm";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string szValue = "";
        if (this.Session["Bit"].ToString() == "1")
        {
            szValue = this.extddlUserLawFirm.Text;
        }
        else if (this.Session["Bit"].ToString() == "3")
        {
            szValue = this.extdllUserName.Text;
        }
        else if (this.Session["Bit"].ToString() == "4")
        {
            szValue = this.extdlLDateFormate.Text;
            switch (szValue)
            {
                case "NA":
                case "":
                    szValue = this.extddlTreatmentAddress.Selected_Text;
                    break;
            }
        }
        else if (this.Session["Bit"].ToString() == "5")
        {
            szValue = this.extdlPOS.Text;
        }
        else if (this.Session["Bit"].ToString() == "6")
        {
            if (this.chkChartNumber.Checked)
            {
                szValue = "1";
            }
            else
            {
                szValue = "0";
            }
        }
        else if (this.Session["Bit"].ToString() == "2")
        {
            if (this.chkChartNumber.Checked)
            {
                szValue = "1";
            }
            else
            {
                szValue = "0";
            }
        }
        if (this.Session["Bit"].ToString() == "0")
        {
            szValue = this.txtSystemKeyValue.Text;
            try
            {
                Convert.ToDateTime(szValue + " " + this.ddlSchTime.SelectedValue.ToString());
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
        string str3 = this.Session["Setting_Id"].ToString();
        Bill_Sys_Settings settings = new Bill_Sys_Settings();
        string szsubVal = "";
        if (this.Session["Bit"].ToString() == "6")
        {
            szsubVal = extddlVisitType.Text;
        }
        else
        {
            szsubVal = this.ddlSchTime.SelectedValue.ToString();
        }

        if (settings.Update_Key_Settings(this.extddlSysKeys.Text, szValue, str3, szsubVal) == 1)
        {
            if (this.extddlSysKeys.Selected_Text == "IP Admin")
            {
                this.UpdateIPAdmin(szValue);
            }
            this.usrMessage.PutMessage("key updated successfully.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            this.SetDefaultSettigns();
            if (this.extddlSysKeys.Text == "SS00012")
            {
                if (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID == szValue)
                {
                    this.Session["IPAdmin"] = "True";
                }
                else
                {
                    this.Session["IPAdmin"] = "False";
                }
            }
            this.grdSettings.XGridBindSearch();
        }
        else
        {
            this.usrMessage.PutMessage("key  already exists or erro in transaction");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.usrMessage.Show();
        }
        this.btnClear_Click(sender, e);
        this.SetDefaultSettigns();

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        if (!this.FileUploadControl.HasFile)
        {
            this.usrMessage.PutMessage("please select file");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            this.usrMessage.Show();
        }
        else
        {
            this.Session["FilePath"] = this.FileUploadControl.FileName;
            string str = "";
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            connection.Open();
            SqlDataReader reader = new SqlCommand("sp_get_file_path_name_for_upload", connection).ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    str = reader[0].ToString();
                }
            }
            if (this.FileUploadControl.HasFile && (this.FileUploadControl.PostedFile.ContentLength < 10240000))
            {
                string str3 = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.Session["tId"] = str3;
                str = str.Replace("/", @"\");
                if (!Directory.Exists(str + str3))
                {
                    Directory.CreateDirectory(str + str3);
                }
                if (this.chkUpload.Checked)
                {
                    this.FileUploadControl.SaveAs(str + "/" + str3 + "/" + this.FileUploadControl.FileName);
                    this.lblUpload.Visible = true;
                    this.lblUpload.Text = "File Uploaded..";
                }
                else if (!this.chkUpload.Checked)
                {
                    if (File.Exists(str + @"\" + str3 + @"\" + this.FileUploadControl.FileName))
                    {
                        this.lblUpload.Visible = true;
                        this.lblUpload.Text = "file exists";
                    }
                    else
                    {
                        this.FileUploadControl.SaveAs(str + "/" + str3 + "/" + this.FileUploadControl.FileName);
                        this.lblUpload.Visible = true;
                        this.lblUpload.Text = "File Uploaded..";
                    }
                }
            }
        }
    }

    protected void extddlSysKeys_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.extddlSysKeys.Text == "NA")
        {
            this.Session["Bit"] = null;
            log.Debug("Session[Bit] = null");
        }
        else
        {
            Bill_Sys_Settings objset = new Bill_Sys_Settings();
            string str = objset.GET_KEY_BIT(this.extddlSysKeys.Text);
            if (str.ToLower() == "true")
            {
                if (this.extddlSysKeys.Text == "SS00060")
                {
                    this.Session["Bit"] = "6";
                    this.lblSubValue.Visible = true;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = true;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    this.extdlPOS.Visible = false;
                    extddlVisitType.Visible = true;
                    log.Debug("Session[Bit] = 6");
                }
                else
                {
                    this.Session["Bit"] = "0";
                    this.lblSubValue.Visible = true;
                    this.ddlSchTime.Visible = true;
                    this.txtSystemKeyValue.Visible = true;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extdlPOS.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    log.Debug("Session[Bit] = 0");
                }
            }
            else if (str.ToLower() == "false")
            {
                this.lblSubValue.Visible = false;
                this.ddlSchTime.Visible = false;
                this.txtSystemKeyValue.Visible = false;
                if (this.extddlSysKeys.Text == "SS00003")
                {
                    this.Session["Bit"] = "1";
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = true;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    this.extdlPOS.Visible = false;
                    extddlVisitType.Visible = false;
                    log.Debug("Session[Bit] = 1");
                }
                else if (this.extddlSysKeys.Text == "SS00012")
                {
                    this.Session["Bit"] = "3";
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = true;
                    this.extdlPOS.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    extddlVisitType.Visible = false;
                    log.Debug("Session[Bit] = 3");
                }
                else if (this.extddlSysKeys.Text == "SS00013")
                {
                    this.Session["Bit"] = "4";
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = true;
                    this.extdlPOS.Visible = false;
                    extddlVisitType.Visible = false;
                    log.Debug("Session[Bit] = 4");
                }
                else if (this.extddlSysKeys.Text == "SS00010")
                {
                    this.Session["Bit"] = "5";
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    this.extdlPOS.Visible = true;
                    extddlVisitType.Visible = false;
                    log.Debug("Session[Bit] = 5");
                }

                else
                {
                    this.Session["Bit"] = "2";
                    this.chkChartNumber.Visible = true;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    this.extdlPOS.Visible = false;
                    extddlVisitType.Visible = false;
                    log.Debug("Session[Bit] = 2");
                }
            }
        }
    }

    protected void extddlTreatmentAddress_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void extdlPOS_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void grdSettings_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int num = Convert.ToInt32(e.CommandArgument.ToString());
            string str = this.grdSettings.DataKeys[num]["BT"].ToString();
            string str2 = this.grdSettings.DataKeys[num]["SZ_SYS_SETTING_VALUE"].ToString();
            string str3 = this.grdSettings.DataKeys[num]["SZ_SYS_SETTING_KEY_ID"].ToString();
            this.Session["Setting_Id"] = this.grdSettings.DataKeys[num]["SZ_SYS_SETTING_ID"].ToString();
            if (str.ToLower() == "true")
            {
                if (str3 != "SS00060")
                {
                    this.lblSubValue.Visible = true;
                    this.ddlSchTime.Visible = true;
                    this.txtSystemKeyValue.Visible = true;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.txtSystemKeyValue.Text = str2;
                    this.ddlSchTime.SelectedValue = this.grdSettings.DataKeys[num]["SZ_SYS_SUB_SETTING_VALUE"].ToString();
                    this.Session["Bit"] = "0";
                    extddlVisitType.Visible = false;
                }
                else
                {
                    this.lblSubValue.Visible = true;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = true;
                    this.extddlUserLawFirm.Visible = true;
                    this.extdllUserName.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    extddlVisitType.Visible = true;
                    this.Session["Bit"] = "6";
                    extddlVisitType.Text = this.grdSettings.DataKeys[num]["SZ_SYS_SUB_SETTING_VALUE"].ToString();
                    if (str2.Trim() == "1")
                    {
                        this.chkChartNumber.Checked = true;
                    }
                    else
                    {
                        this.chkChartNumber.Checked = false;
                    }
                }
            }
            else
            {
                if ((str.ToLower() == "false") && (str3 == "SS00003"))
                {
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = true;
                    this.extdllUserName.Visible = false;
                    this.extddlUserLawFirm.Text = str2;
                    this.extdllUserName.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    extddlVisitType.Visible = false;
                    this.Session["Bit"] = "1";
                }
                if ((str.ToLower() == "false") && (str3 == "SS00012"))
                {
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = true;
                    this.extdlLDateFormate.Visible = false;
                    this.extdllUserName.Text = str2;
                    extddlVisitType.Visible = false;
                    this.Session["Bit"] = "3";
                }
                if ((str.ToLower() == "false") && (str3 == "SS00010"))
                {
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = false;
                    extddlVisitType.Visible = false;
                    this.extdlPOS.Visible = true;
                    this.extddlTreatmentAddress.Selected_Text = str2;
                    this.Session["Bit"] = "5";
                }
                if ((str.ToLower() == "false") && (str3 == "SS00013"))
                {
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = false;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    this.extddlTreatmentAddress.Visible = true;
                    extddlVisitType.Visible = false;
                    this.extddlTreatmentAddress.Selected_Text = str2;
                    this.Session["Bit"] = "4";
                }
                else if ((((str.ToLower() == "false") && (str3 != "SS00013")) && ((str3 == "SS00012") && (str3 == "SS00003"))) && (str3 != "SS00010"))
                {
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = true;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    extddlVisitType.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    if (str2.Trim() == "1")
                    {
                        this.chkChartNumber.Checked = true;
                    }
                    else
                    {
                        this.chkChartNumber.Checked = false;
                    }
                    this.Session["Bit"] = "2";
                }
                else if ((((str.ToLower() == "false") && (str3 != "SS00013")) && ((str3 != "SS00012") && (str3 != "SS00003"))) && (str3 != "SS00010"))
                {
                    this.lblSubValue.Visible = false;
                    this.ddlSchTime.Visible = false;
                    this.txtSystemKeyValue.Visible = false;
                    this.chkChartNumber.Visible = true;
                    this.extddlUserLawFirm.Visible = false;
                    this.extdllUserName.Visible = false;
                    extddlVisitType.Visible = false;
                    this.extdlLDateFormate.Visible = false;
                    if (str2.Trim() == "1")
                    {
                        this.chkChartNumber.Checked = true;
                    }
                    else
                    {
                        this.chkChartNumber.Checked = false;
                    }
                    this.Session["Bit"] = "2";
                }
            }
            this.extddlSysKeys.Text = str3;
            this.extddlSysKeys.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.btnSave.Enabled = false;
        }
    }

    protected void OnextendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblmsgEmail.Text = "";
        this.txtEmailAddress.Text = "";
        DataSet set = new DataSet();
        string str = ConfigurationManager.AppSettings["Connection_String"];
        SQLToDAO odao = new SQLToDAO(str);
        FileTransferEmailNotifications_DAO s_dao = new FileTransferEmailNotifications_DAO();
        s_dao.sz_company_id = this.txtCompanyID.Text;
        s_dao.sz_law_firm_company_id = this.extddlLawFirm.Text;
        s_dao.i_flag = 1;
        set = odao.LoadDataSet("SP_MANAGE_FILE_TRANSFER_NOTIFICATIONS", "mbs.dao.FileTransferEmailNotifications_DAO", s_dao, "mbs.dao");
        if ((set.Tables.Count >= 1) && (set.Tables[0].Rows.Count >= 1))
        {
            this.txtEmailAddress.Text = set.Tables[0].Rows[0][0].ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = this.grdSettings;
        this.txtSearchBox.SourceGrid = this.grdSettings;
        this.grdSettings.Page = this.Page;
        this.grdSettings.PageNumberList = this.con;
        this.btnDelete.Attributes.Add("onclick", "return confirm_delete();");
        this.btnSave.Attributes.Add("onclick", "return  Check();");
        this.btnClear.Attributes.Add("onclick", "return  Clear();");
        this.btnUpdate.Attributes.Add("onclick", "return  Check();");
        if (!this.Page.IsPostBack)
        {
            this.Session["Bit"] = null;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlSysKeys.Flag_ID = this.txtCompanyID.Text;
            this.extddlUserLawFirm.Flag_ID = this.txtCompanyID.Text;
            this.extdllUserName.Flag_ID = this.txtCompanyID.Text;
            this.extdlPOS.Flag_ID = this.txtCompanyID.Text;
            this.extdlLDateFormate.Flag_ID = this.txtCompanyID.Text;
            this.extddlTreatmentAddress.Flag_ID = this.txtCompanyID.Text;
            extddlVisitType.Flag_ID = this.txtCompanyID.Text;

            this.grdSettings.XGridBindSearch();
            this.btnUpdate.Enabled = false;
            //if (Session["CurrentTable"] == null)
            //{
            //    SetInitialRow();
            //}
            //else
            //{
            //    grdIpAdress.DataSource = Session["CurrentTable"];
            //    grdIpAdress.DataBind();
            //}
            //SetPreviousData();
            DataSet ds = BindIpAddressGrid();
            grdIpAdress.DataSource = ds;
            grdIpAdress.DataBind();

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            GridViewDataColumn col1 = (GridViewDataColumn)grdIpAdress.Columns["txtIpAddress"];
                            //GridViewDataColumn col1 = grdIpAdress.Columns["txtIpAddress"] as GridViewDataColumn; // textbox column
                            TextBox txtIpAddress = (TextBox)grdIpAdress.FindRowCellTemplateControl(i, col1, "txtIpAddress");
                            if (txtIpAddress != null)
                            {
                                txtIpAddress.Text = ds.Tables[0].Rows[i]["sz_ip_address"].ToString();
                            }
                        }
                        //grdIpAdress.DataSource = ds;
                        //grdIpAdress.DataBind();
                    }
                }
            }
            else
            {
                SetInitialRow();
            }
            
        }
    }

    protected void SetDefaultSettigns()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_LoginBO nbo = new Bill_Sys_LoginBO();
        try
        {
            this._bill_Sys_SystemObject = (Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"];
            this._bill_Sys_SystemObject.SZ_DEFAULT_LAW_FIRM = nbo.getDefaultSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00003");
            this._bill_Sys_SystemObject.SZ_CHART_NO = nbo.getDefaultSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00004");
            this._bill_Sys_SystemObject.SZ_LOCATION = nbo.getDefaultSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00007");
            this._bill_Sys_SystemObject.SZ_CHECKINVALUE = nbo.getDefaultSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00008");
            this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_PHONE = nbo.getDefaultSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00018");
            this._bill_Sys_SystemObject.SZ_SHOW_PATIENT_SIGNATURE_FOR_NF3 = nbo.getDefaultSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00019");
            this._bill_Sys_SystemObject.SZ_SHOW_DOCTOR_SIGNATURE_FOR_NF3 = nbo.getDefaultSettings(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "SS00020");
            this.Session["SYSTEM_OBJECT"] = this._bill_Sys_SystemObject;
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

    protected void UpdateIPAdmin(string szValue)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE MST_USERS SET BT_IP_ENABLED=0 WHERE SZ_USER_ID='" + szValue + "'";
            command.CommandTimeout = 0;
            command.CommandType = CommandType.Text;
            connection.Open();
            command.ExecuteNonQuery();
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
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected string GetIniEval()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        string szReturn = "0";
        try
        {
            SqlCommand command = new SqlCommand("SP_GET_INITIAL_EVALUATION_COUNT", connection);
            command.CommandTimeout = 0;
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            command.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            SqlDataReader dr;
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                szReturn = dr[0].ToString();
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return szReturn;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAddIpAddress_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }


    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Column1", typeof(string)));
        dr = dt.NewRow();
        dr["Column1"] = string.Empty;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        Session["CurrentTable"] = dt;

        grdIpAdress.DataSource = dt;
        grdIpAdress.DataBind();
    }

    protected void btnSaveIdAddress_Click(object sender, EventArgs e)
    {
        SaveIpAddress();
        DataSet ds = BindIpAddressGrid();
        grdIpAdress.DataSource = ds;
        grdIpAdress.DataBind();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GridViewDataColumn col1 = (GridViewDataColumn)grdIpAdress.Columns["txtIpAddress"];
                        //GridViewDataColumn col1 = grdIpAdress.Columns["txtIpAddress"] as GridViewDataColumn; // textbox column
                        TextBox txtIpAddress = (TextBox)grdIpAdress.FindRowCellTemplateControl(i, col1, "txtIpAddress");
                        if (txtIpAddress != null)
                        {
                            txtIpAddress.Text = ds.Tables[0].Rows[i]["sz_ip_address"].ToString();
                        }
                    }
                    //grdIpAdress.DataSource = ds;
                    //grdIpAdress.DataBind();
                }
            }
        }
    }

    private void SaveIpAddress()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            for (int i = 0; i < this.grdIpAdress.VisibleRowCount; i++)
            {
                if (Convert.ToInt32(grdIpAdress.GetRowValues(i, "i_id")) == 0)
                {
                    GridViewDataColumn col1 = grdIpAdress.Columns["txtIpAddress"] as GridViewDataColumn;
                    //CheckBox chk = grdIpAdress.FindRowCellTemplateControl(i, col1, "chkDeleteIp") as CheckBox;
                    TextBox box = grdIpAdress.FindRowCellTemplateControl(i, col1, "txtIpAddress") as TextBox;

                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "sp_insert_ip_address";
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@sz_company_id", txtCompanyID.Text);
                    command.Parameters.AddWithValue("@sz_ip_address", box.Text);
                    command.Parameters.AddWithValue("@sz_user_id", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    command.Parameters.AddWithValue("@IsActive", "1");
                    command.ExecuteNonQuery();
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
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdIpAdress_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string id_ = grdIpAdress.GetRowValues(e.VisibleIndex, "Column1").ToString();

        Session["i_id"]= id_;

        if (e.CommandArgs.CommandName == "Delete")
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "sp_delete_ip_address";
                command.CommandTimeout = 0;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@sz_ip_address", id_);
                //command.Parameters.Add("")
                command.Parameters.AddWithValue("@sz_company_id", txtCompanyID.Text);
                command.ExecuteNonQuery();
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
            finally { connection.Close(); }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet BindIpAddressGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        DataSet ds = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "sp_get_ip_addresses";
            command.CommandTimeout = 0;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@sz_company_id", txtCompanyID.Text);
            SqlDataAdapter adap = new SqlDataAdapter();
            adap = new SqlDataAdapter(command);
           
            adap.Fill(ds);

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
        finally { connection.Close(); }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void AddNewRowToGrid()
    {
        DataSet ds = new DataSet();
        ds = BindIpAddressGrid();
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        DataRow dr = dt.NewRow();
        if (ds.Tables[0].Rows.Count>0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                
                //dr["i_id"] = DBNull.Value;
                //dr["i_id"] = 0;
                dr["sz_ip_address"] = string.Empty;
                //dr["dt_created"] = DBNull.Value;
                //dr["sz_user_id"] = string.Empty;
                dr["IsActive"] = DBNull.Value;
                dt.Rows.Add(dr);
                break;
            }
            grdIpAdress.DataSource = dt;
            grdIpAdress.DataBind();
        }
        else
        {
            //dr["i_id"] = DBNull.Value;
            //dr["i_id"] = 0;
            dr["sz_ip_address"] = string.Empty;
            //dr["dt_created"] = DBNull.Value;
            //dr["sz_user_id"] = string.Empty;
            dr["IsActive"] = DBNull.Value;
            dt.Rows.Add(dr);
                
            grdIpAdress.DataSource = dt;
            grdIpAdress.DataBind();
        }

        
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GridViewDataColumn col1 = (GridViewDataColumn)grdIpAdress.Columns["txtIpAddress"];
                        //GridViewDataColumn col1 = grdIpAdress.Columns["txtIpAddress"] as GridViewDataColumn; // textbox column
                        TextBox txtIpAddress = (TextBox)grdIpAdress.FindRowCellTemplateControl(i, col1, "txtIpAddress");
                        if (txtIpAddress != null)
                        {
                            txtIpAddress.Text = ds.Tables[0].Rows[i]["sz_ip_address"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void grdIpAdress_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        string sID = grdIpAdress.GetRowValues(e.VisibleIndex, "i_id").ToString();
        //BindProcCodeGrid(szBillno);
        if (sID != "")
        {
            DeleteIPAddress(Convert.ToInt32(sID));
        }
        DataSet ds = BindIpAddressGrid();
        grdIpAdress.DataSource = ds;
        grdIpAdress.DataBind();
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        GridViewDataColumn col1 = (GridViewDataColumn)grdIpAdress.Columns["txtIpAddress"];
                        //GridViewDataColumn col1 = grdIpAdress.Columns["txtIpAddress"] as GridViewDataColumn; // textbox column
                        TextBox txtIpAddress = (TextBox)grdIpAdress.FindRowCellTemplateControl(i, col1, "txtIpAddress");
                        if (txtIpAddress != null)
                        {
                            txtIpAddress.Text = ds.Tables[0].Rows[i]["sz_ip_address"].ToString();
                        }
                    }
                    //grdIpAdress.DataSource = ds;
                    //grdIpAdress.DataBind();
                }
            }
        }

    }
    protected void DeleteIPAddress(int I_ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //string Ip = this.htid.Get("tid").ToString();

        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "sp_delete_ip_address";
            command.CommandTimeout = 0;
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@i_id", I_ID);
            command.Parameters.AddWithValue("@sz_company_id", txtCompanyID.Text);
            command.ExecuteNonQuery();
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
        finally { connection.Close();
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
