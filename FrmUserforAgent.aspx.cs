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
using System.Data;
using System.Data.SqlClient;
using DevExpress.Web;
using Visit_Application;

public partial class FrmUserforAgent : PageBase
{
    string Sqlcon = ConfigurationManager.AppSettings["Connection_String"];
    string sz_userid = "", sz_companyid="";
    FillPatientGrid obj = new FillPatientGrid();
    protected void Page_Load(object sender, EventArgs e)
    {
        sz_companyid = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        sz_userid = Request.QueryString["UserID"].ToString();
        if (!IsPostBack)
        {
            grdoffice.DataSource = obj.FillOffice(sz_companyid);
            grdoffice.DataBind();
            checkPreviousdata();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        obj.DeleteUserOffice(sz_companyid, sz_userid);
        //ArrayList _arrayList = new ArrayList();
        string check = "No";
        try
        {
            for (int i = 0; i < grdoffice.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdoffice");
                GridViewDataColumn c = (GridViewDataColumn)grdoffice.Columns[0];
                CheckBox checkBox = (CheckBox)grdoffice.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        string of_id = Convert.ToString(grdoffice.GetRowValues(i, "CODE"));
                        //_arrayList.Add(Convert.ToString(grdoffice.GetRowValues(i, "CODE")));
                        obj.InsertUserOffice(sz_companyid, sz_userid, of_id);
                        check = "Yes";
                    }

                }
                              
            }
         if(check=="Yes")
             ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Office Added Successfully.');", true);
         else
             ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Select Atleast one record.');", true);
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

    private void checkPreviousdata()
    {
        DataTable dt = new DataTable();
        dt = obj.GetOfficeID(sz_companyid, sz_userid);
        for (int j = 0; j < dt.Rows.Count; j++)
        {
            for (int i = 0; i < grdoffice.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdoffice");
                GridViewDataColumn c = (GridViewDataColumn)grdoffice.Columns[0];
                CheckBox checkBox = (CheckBox)grdoffice.FindRowCellTemplateControl(i, c, "chkall");
                if (dt.Rows[j]["sz_office_id"].ToString() == Convert.ToString(grdoffice.GetRowValues(i, "CODE")))
                {
                    checkBox.Checked = true;
                }

            }
        }
    }
}
