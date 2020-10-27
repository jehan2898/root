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
using DevExpress.XtraPivotGrid;
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_Balance_Report : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        try
        {
            this.con.SourceGrid = this.grdBalanceBill;
            this.txtSearchBox.SourceGrid = this.grdBalanceBill;
            this.grdBalanceBill.Page = this.Page;
            this.grdBalanceBill.PageNumberList = this.con;

            if (!IsPostBack)
            {
                extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.drdUpdateStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.btnUpdateStatus.Attributes.Add("onclick", "return confirm_update_bill_status();");
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                hdnClick.Value = "0";
                //btnSearch.Attributes.Add("onclick", "return ClickOnButton();");
                //grdBalanceBill.SettingsPager.Mode= GridViewPagerMode.ShowAllRecords;
            }
            if (hdnClick.Value == "0")
            {
                //BindGrid();
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

    protected void btnSearch_Click(object sender, EventArgs e)
 {
        BindGrid();
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            //Bill_Sys_Balance_Report objBalance = new Bill_Sys_Balance_Report();
            //ArrayList paramlist = new ArrayList();
            //paramlist.Add(txtCompanyID.Text.ToString());
            //paramlist.Add(txtFromDate.Text.ToString());
            //paramlist.Add(txtToDate.Text.ToString());
            //paramlist.Add(extddlCaseType.Text);
            //paramlist.Add(extddlBillStatus.Text);
            //paramlist.Add(ddlAmount.Text);
            //paramlist.Add(txtAmount.Text);
            //paramlist.Add(txtToAmt.Text);

            //DataSet dsBill = new DataSet();
            //dsBill = objBalance.GetBalanceReportData(paramlist);
            //grdBalanceBill.DataSource = dsBill;
            //grdBalanceBill.DataBind();

            //grdBalanceBill.Column[8].DisplayFormat.FormatType = FormatType.Numeric;
            //colPayment.DisplayFormat.FormatString = "c2";

            // Session["BalanceBill"] = dsBill;

            txtBillStatusID.Text = extddlBillStatus.Text;
            txtBillAmount.Text = ddlAmount.Text;
            grdBalanceBill.XGridBindSearch();
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

    //protected void btnXlsExport_Click(object sender, EventArgs e)
    //{
    //    //DataSet ds = (DataSet)Session["BalanceBill"];
    //    //grdBalanceBill.DataSource = ds;
    //    //grdBalanceBill.DataBind();
    //    grdBalanceBill.XGridBindSearch();
    //    gridExport.FileName = Session["UserName"].ToString() + "_Report(" + DateTime.Now.ToString("ddMMMMyyyy") + ")";
    //    gridExport.WriteXlsToResponse();
    //}
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("window.location.href ='", ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString(), this.grdBalanceBill.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()), "';"), true);
     }
    protected void ddlAmount_SelectedIndexChanged(object sender,EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if(this.ddlAmount.SelectedValue == "NA")
            {
                this.txtToAmt.Visible =false;
                this.txtAmount.Text = "";
                this.txtAmount.Visible = false;
                this.lblfrom.Visible = false;
                this.lblFamt.Visible = false;
                this.lblTamt.Visible = false;


            }
             else if(this.ddlAmount.SelectedValue =="0")
            {
                this.txtToAmt.Visible = true;
                this.txtAmount.Visible = true;
                this.lblFamt.Visible = true;
                this.lblTamt.Visible = true;
                this.lblfrom.Visible = true;
            }
            else if(this.ddlAmount.SelectedValue == "1" || this.ddlAmount.SelectedValue =="2" || this.ddlAmount.SelectedValue == "3")
            {
                this.txtAmount.Visible = true;
                this.lblFamt.Visible = true;
                this.lblfrom.Visible = false;
                this.txtToAmt.Visible = false;
                this.lblTamt.Visible = false;
            }
        }
        catch(Exception ex)
        {

        }
    }
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_ReportBO billSysReportBO = new Bill_Sys_ReportBO();
        try
        {
            if (this.drdUpdateStatus.Text != "NA")
            {
                ArrayList arrayLists = new ArrayList();
                string str = "";
                bool flag = false;
                for (int i = 0; i < this.grdBalanceBill.Rows.Count; i++)
                {
                   
                    if (((CheckBox)this.grdBalanceBill.Rows[i].FindControl("chkSelect")).Checked)
                    {
                        if (flag)
                        {
                            str = string.Concat(str, ",'", this.grdBalanceBill.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'");
                        }
                        else
                        {
                            str = string.Concat("'", this.grdBalanceBill.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'");
                            flag = true;
                        }
                    }
                
            }

                if (str != "")
                {
                    arrayLists.Add(this.drdUpdateStatus.Text);
                    arrayLists.Add(str);
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayLists.Add(DateTime.Today.ToShortDateString());
                    arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    billSysReportBO.updateBillStatus(arrayLists);
                    this.usrMessage.PutMessage("Bill status updated successfully.");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();

                    this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                    BindGrid();

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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}