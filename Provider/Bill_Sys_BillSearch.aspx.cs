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

public partial class Provider_Bill_Sys_BillSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btnClearP.Attributes.Add("onclick", "return Clear()");

            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            if (Request.QueryString["caseid"] != null)
            {
                txtCaseID.Text = Request.QueryString["caseid"].ToString();
                Session["CASE_OBJECT"] = txtCaseID.Text;
            }
            this.con.SourceGrid = grdBillSearch;
            this.txtSearchBox.SourceGrid = grdBillSearch;
            this.grdBillSearch.Page = this.Page;
            this.grdBillSearch.PageNumberList = this.con;
            if (!IsPostBack)
            {
                if (Session["Office_ID"] != null)
                {
                    hdnOfficeId.Value = Session["Office_ID"].ToString();
                }
                txtOfficeID.Text = Session["Office_ID"].ToString();
                extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
                txtGroupId.Text = extddlSpeciality.Text;
                txtBillStatusID.Text = extddlBillStatus.Text;
                if (Request.QueryString["fromCase"] != null)
                {
                    if (Request.QueryString["fromCase"].ToString().Trim() == "true")
                    {
                        if (Session["CASE_OBJECT"] != null)
                        {
                            lnkpatientdesk.Visible = true;
                            lnkCaseInfo.Visible = true;
                            txtCaseID.Text = Request.QueryString["caseid"].ToString();
                            txtCaseNo.Text = Request.QueryString["caseno"].ToString();
                            txtCaseNo.ReadOnly = true;
                            grdBillSearch.XGridBindSearch();
                        }
                    }
                }
                else
                {
                    ddlDateValues.SelectedValue = "3";
                    string month = System.DateTime.Now.Month.ToString();
                    string year = System.DateTime.Now.Year.ToString();
                    int Month = Convert.ToInt32(month);
                    int Year = Convert.ToInt32(year);
                    int LastDay = System.DateTime.DaysInMonth(Year, Month);
                    txtFromDate.Text = month + "/" + "1" + "/" + Year;
                    txtToDate.Text = month + "/" + LastDay + "/" + Year;
                    grdBillSearch.XGridBindSearch();
                    linkbillsearch.Visible = false;
                }
                hdnCaseId.Value = txtCaseID.Text;
                hdnCaseNo.Value = txtCaseNo.Text;
                hdnCompanyId.Value = txtCompanyID.Text;
            }

        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string sz = extddlBillStatus.Selected_Text;
            grdBillSearch.XGridBindSearch();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            //usrMessage.PutMessage(ex.ToString());
            //usrMessage.Show();
        }
    }
    #region "ExportTOExcel"
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdBillSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);


    }
    #endregion
    protected void grdBillSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
}
