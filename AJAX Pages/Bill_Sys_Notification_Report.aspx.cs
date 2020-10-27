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
using mbs.bl;

public partial class Bill_Sys_Notification_Report : PageBase
{
    Bill_Sys_ReportBO _Bill_Sys_ReportBO;
    mbs.bl.litigation.Notification _Notification;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdMissingDocument.Page = this.Page;
            this.con.SourceGrid = grdMissingDocument;
            this.txtSearchBox.SourceGrid = grdMissingDocument;
            this.grdMissingDocument.Page = this.Page;
            this.grdMissingDocument.PageNumberList = this.con;
            this.Title = "Missing Document Report";

            txtUserId.Text=((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            if (!IsPostBack)
            {

                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlGroup.Flag_ID = txtCompanyID.Text;                
                ExtdropdwnLocation.Flag_ID=txtCompanyID.Text;
                if (Convert.ToInt32(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY) == 1)
                {
                    grdMissingDocument.Columns[4].Visible = true;
                    grdMissingDocument.Columns[5].Visible = true;
                }
                else
                {
                    grdMissingDocument.Columns[2].Visible = true;
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

    protected void OnextendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstNodeDescription.Items.Clear();
        _Bill_Sys_ReportBO = new Bill_Sys_ReportBO();
        DataSet ds = new DataSet();
        _Notification = new mbs.bl.litigation.Notification();
        ds = _Notification.GetNodeDescForMissingNotification(txtCompanyID.Text, extddlGroup.Text);

        for (int i = 0; i < ds.Tables[0].Rows.Count;i++)
        {
            ListItem lt = new ListItem();
            lt.Text = ds.Tables[0].Rows[i][0].ToString();
            lt.Value = ds.Tables[0].Rows[i][1].ToString();
            lstNodeDescription.Items.Add(lt);
        }
        for (int j = 0; j < lstNodeDescription.Items.Count;j++)
        {
            lstNodeDescription.Items[j].Selected = true;
        }
        
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtLocation.Text=ExtdropdwnLocation.Text;
            txtGroupName.Text = extddlGroup.Text;
            txtNodeDescription.Text = "";
            string code = "";
            for (int j = 0; j < lstNodeDescription.Items.Count; j++)
            {
                if(lstNodeDescription.Items[j].Selected==true)
                {
                    if (txtNodeDescription.Text == "")
                    {
                        txtNodeDescription.Text =   lstNodeDescription.Items[j].Value.ToString() ;
                    //    code = "'''" + lstNodeDescription.Items[j].Value.ToString() + "''";
                    }
                    else
                    {
                        txtNodeDescription.Text = txtNodeDescription.Text +  "','" + lstNodeDescription.Items[j].Value.ToString();
                    }
                }
            }

            if (txtNodeDescription.Text != "" && txtGroupName.Text!="")
            {
                grdMissingDocument.XGridBindSearch();
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

    protected void lnkExportToExcel_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdMissingDocument.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
}
