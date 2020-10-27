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

public partial class AJAX_Pages_Bill_Sys_DownloadDesk : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
        {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.con.SourceGrid = grdDownLoadDesk;
        this.txtSearchBox.SourceGrid = grdDownLoadDesk;
        this.grdDownLoadDesk.Page = this.Page;
        this.grdDownLoadDesk.PageNumberList = this.con;
        this.Title = "DownLoad Desk";
        this.grdDownLoadDesk.Page = this.Page;

        //this.grdDownloadCompanyWise.Page=this.Page;
        extddlInsurance.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extddlCaseStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extdlitigate.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        // ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        //DtlView.Visible = false;
        try
        {
            if (!IsPostBack)
            {
                txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                mbs.lawfirm.Bill_Sys_CollectDocAndZip obj = new Bill_Sys_CollectDocAndZip();
                txtStatusID.Text = obj.getStatusId(txtCompanyId.Text, "DNL");
                grdDownLoadDesk.XGridBindSearch();
                DataSet dsCompanyWiseInfo = new DataSet();

                dsCompanyWiseInfo=obj.GetCompanyWiseInfo(txtCompanyId.Text);
                //DtlView.DataSource = dsCompanyWiseInfo;
                //DtlView.DataBind();
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

    protected void grdDownLoadDesk_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList objarr = new ArrayList();
        
        mbs.lawfirm.Bill_Sys_CollectDocAndZip objDAO = new Bill_Sys_CollectDocAndZip();
        string _CompanyName = "";
        int index = 0;
        try
        {
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < grdDownLoadDesk.Rows.Count; i++)
                {
                    LinkButton minus1 = (LinkButton)grdDownLoadDesk.Rows[i].FindControl("lnkM");
                    LinkButton plus1 = (LinkButton)grdDownLoadDesk.Rows[i].FindControl("lnkP");
                    if (minus1.Visible)
                    {
                        minus1.Visible = false;
                        plus1.Visible = true;
                    }
                }

                grdDownLoadDesk.Columns[10].Visible = true;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";

                divname = divname + grdDownLoadDesk.DataKeys[index][1].ToString();
                GridView gv = (GridView)grdDownLoadDesk.Rows[index].FindControl("GridView2");
                LinkButton plus = (LinkButton)grdDownLoadDesk.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdDownLoadDesk.Rows[index].FindControl("lnkM");

                DataSet objds = new DataSet();
                //GetDownloadedBillsInfo
                //objds = objDAO.GetDownloadedBills(grdDownLoadDesk.DataKeys[index][0].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txtStatusID.Text);
                objds = objDAO.GetDownloadedBillsInfo(grdDownLoadDesk.DataKeys[index][0].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txtStatusID.Text, grdDownLoadDesk.DataKeys[index][1].ToString());
                gv.DataSource = objds;
                gv.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mp", "ShowChildGrid('" + divname + "') ;", true);
                plus.Visible = false;
                minus.Visible = true;
            }

            if (e.CommandName.ToString() == "MNS")
            {
                grdDownLoadDesk.Columns[10].Visible = false;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";

                divname = divname + grdDownLoadDesk.DataKeys[index][1].ToString();
                LinkButton plus = (LinkButton)grdDownLoadDesk.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdDownLoadDesk.Rows[index].FindControl("lnkM");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HideChildGrid('" + divname + "') ;", true);
                plus.Visible = true;
                minus.Visible = false;
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
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdDownLoadDesk.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdDownLoadDesk.XGridBindSearch();
    }
 
}
