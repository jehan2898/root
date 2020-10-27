using AjaxControlToolkit;
using ASP;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class LF_Bill_Sys_Reports : Page, IRequiresSessionState
{
    public LF_Bill_Sys_Reports()
    {
    }

    protected void Image1_Click(object sender, EventArgs e)
    {
        this.table1.Visible = false;
        this.btn_Create.Visible = false;
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        this.table1.Visible = false;
        this.btn_Create.Visible = false;
    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        this.table1.Visible = true;
        this.btn_Create.Visible = true;
        this.ddlDateValues.SelectedIndex = 4;
        TextBox textBox = this.txtFromDate;
        int year = DateTime.Now.Year;
        textBox.Text = string.Concat("1/1/", year.ToString());
        TextBox textBox1 = this.txtToDate;
        int num = DateTime.Now.Year;
        textBox1.Text = string.Concat("12/31/", num.ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        base.Title = "Green Your Bills - Law Firm Reports";
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        if (!this.Page.IsPostBack)
        {
            this.table1.Visible = false;
            this.btn_Create.Visible = false;
        }
    }
}