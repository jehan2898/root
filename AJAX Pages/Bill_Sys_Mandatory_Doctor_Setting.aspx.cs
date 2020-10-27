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
using DevExpress.Web;
public partial class AJAX_Pages_Bill_Sys_Mandatory_Doctor_Setting : PageBase
{
    Bill_Sys_Mandatory_Doctor_setting _Bill_Sys_Mandatory_Doctor_setting = new Bill_Sys_Mandatory_Doctor_setting();
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnsave.Attributes.Add("onclick", "return val_CheckControls();");
            btnDelete.Attributes.Add("onclick", "return callfordelete();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseType.Flag_ID = txtCompanyID.Text;

            if (!IsPostBack)
            {
                btnUpdate.Enabled = false;
                hdndelete.Value = "";
            }

            if (hdndelete.Value != "true")
            {
                DataSet dsdoctorinfo = new DataSet();
                dsdoctorinfo = _Bill_Sys_Mandatory_Doctor_setting.GetDoctorsettingInfo(txtCompanyID.Text);
                grddoctorsetting.DataSource = dsdoctorinfo;
                grddoctorsetting.DataBind();
            }
            else
            {
                hdndelete.Value = "";
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szcasetype = "";
            string sznpi = "";
            string szDoctor_License_Number = "";
            string szWCB_Authorization_Number = "";
            string szWCB_Rating_Code = "";
            if (chknpi.Checked)
            {
                sznpi = "1";
            }
            else
            {
                sznpi = "0";

            }
            if (chkdoclicensenumber.Checked)
            {
                szDoctor_License_Number = "1";
            }
            else
            {
                szDoctor_License_Number = "0";

            }
            if (chkwcbauthnumber.Checked)
            {
                szWCB_Authorization_Number = "1";
            }
            else
            {
                szWCB_Authorization_Number = "0";

            }
            if (Chkwcbratingcode.Checked)
            {
                szWCB_Rating_Code = "1";
            }
            else
            {
                szWCB_Rating_Code = "0";

            }
            int iReturn = 0;
            iReturn = _Bill_Sys_Mandatory_Doctor_setting.SaveDoctorSettinginfo(txtCompanyID.Text, extddlCaseType.Text, sznpi, szDoctor_License_Number, szWCB_Authorization_Number, szWCB_Rating_Code, extddlCaseType.Selected_Text);

            if (iReturn > 0)
            {
                DataSet dsdoctorinfo = new DataSet();
                dsdoctorinfo = _Bill_Sys_Mandatory_Doctor_setting.GetDoctorsettingInfo(txtCompanyID.Text);
                grddoctorsetting.DataSource = dsdoctorinfo;
                grddoctorsetting.DataBind();
                ClearControl();
                usrMessage.PutMessage("Save Successfully...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            else
            {
                usrMessage.PutMessage("Fail To Save...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();

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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnsave.Enabled = true;
        string szcasetype = "";
        string sznpi = "";
        string szDoctor_License_Number = "";
        string szWCB_Authorization_Number = "";
        string szWCB_Rating_Code = "";
        try
        {
            if (extddlCaseType.Text != null)
            {
                if (chknpi.Checked)
                {
                    sznpi = "1";
                }
                else
                {
                    sznpi = "0";

                }
                if (chkdoclicensenumber.Checked)
                {
                    szDoctor_License_Number = "1";
                }
                else
                {
                    szDoctor_License_Number = "0";

                }
                if (chkwcbauthnumber.Checked)
                {
                    szWCB_Authorization_Number = "1";
                }
                else
                {
                    szWCB_Authorization_Number = "0";

                }
                if (Chkwcbratingcode.Checked)
                {
                    szWCB_Rating_Code = "1";
                }
                else
                {
                    szWCB_Rating_Code = "0";

                }
                _Bill_Sys_Mandatory_Doctor_setting.UpdateDoctorsettingInfo(txtCompanyID.Text, extddlCaseType.Text, sznpi, szDoctor_License_Number, szWCB_Authorization_Number, szWCB_Rating_Code, Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "I_SETTING_ID")));
                DataSet dsdoctorinfo = new DataSet();
                dsdoctorinfo = _Bill_Sys_Mandatory_Doctor_setting.GetDoctorsettingInfo(txtCompanyID.Text);
                grddoctorsetting.DataSource = dsdoctorinfo;
                grddoctorsetting.DataBind();
                ClearControl();
                usrMessage.PutMessage("Update Successfully...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
                btnUpdate.Enabled = false;
                extddlCaseType.Enabled = true;
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnUpdate.Enabled = false;
        try
        {
            
            for (int i = 0; i < grddoctorsetting.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grddoctorsetting");
                GridViewDataColumn c = (GridViewDataColumn)grddoctorsetting.Columns[9];
                CheckBox checkBox = (CheckBox)grddoctorsetting.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        _Bill_Sys_Mandatory_Doctor_setting.RemoveMandatorySetting(txtCompanyID.Text, Convert.ToString(grddoctorsetting.GetRowValues(i, "SZ_CASE_TYPE_ID")));
                        ClearControl();
                        usrMessage.PutMessage("Delete Successfully ...!");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }
                }
            }
            DataSet dsdoctorinfo = new DataSet();
            dsdoctorinfo = _Bill_Sys_Mandatory_Doctor_setting.GetDoctorsettingInfo(txtCompanyID.Text);
            grddoctorsetting.DataSource = dsdoctorinfo;
            grddoctorsetting.DataBind();

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
        btnsave.Enabled = true;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void grddoctorsetting_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnUpdate.Enabled = true;
        btnsave.Enabled = false;
        extddlCaseType.Enabled = false;
        if (e.CommandArgs.CommandName == "Select")
        {
            try
            {
                if (Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "SZ_CASE_TYPE_ID")) != "&nbsp;")
                {
                    extddlCaseType.Text = Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "SZ_CASE_TYPE_ID"));
                }
                else
                {
                    extddlCaseType.Text = "NA";
                }
                if (Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_NPI")) != "&nbsp;")
                {
                    string sznpi = Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_NPI"));
                    if (sznpi == "Mandatory")
                    {
                        chknpi.Checked = true;
                    }
                    else
                    {
                        chknpi.Checked = false;
                    }

                }
                if (Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_DOC_LICENSE_NUMBER")) != "&nbsp;")
                {
                    string szdoclicensenumber = Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_DOC_LICENSE_NUMBER"));
                    if (szdoclicensenumber == "Mandatory")
                    {
                        chkdoclicensenumber.Checked = true;
                    }
                    else
                    {
                        chkdoclicensenumber.Checked = false;
                    }

                }
                if (Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_WCB_AUTHORIZATION_NUMBER")) != "&nbsp;")
                {
                    string szwcbauthnumber = Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_WCB_AUTHORIZATION_NUMBER"));
                    if (szwcbauthnumber == "Mandatory")
                    {
                        chkwcbauthnumber.Checked = true;
                    }
                    else
                    {
                        chkwcbauthnumber.Checked = false;
                    }

                }
                if (Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_WCB_RATING_CODE")) != "&nbsp;")
                {
                    string szwcbratingcode = Convert.ToString(grddoctorsetting.GetRowValues(grddoctorsetting.FocusedRowIndex, "BT_WCB_RATING_CODE"));
                    if (szwcbratingcode == "Mandatory")
                    {
                        Chkwcbratingcode.Checked = true;
                    }
                    else
                    {
                        Chkwcbratingcode.Checked = false;
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
    public void ClearControl()
    {
        extddlCaseType.Text = "NA";
        chknpi.Checked = false;
        chkwcbauthnumber.Checked = false;
        Chkwcbratingcode.Checked = false;
        chkdoclicensenumber.Checked = false;


    }
}
