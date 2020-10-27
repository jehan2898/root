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
using System.Data.Sql;
using System.Data.SqlClient;

public partial class AJAX_Pages_PatientViewFrame : PageBase
{

    protected void Page_Init(object sender, EventArgs e)
    {
       
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string sType = browser.Type;
        string sName = browser.Browser;
        string szCSS;
        string _url = "";
        if (Request.RawUrl.IndexOf("?") > 0)
        {
            _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
        }
        else
        {
            _url = Request.RawUrl;
        }
        if (browser.Browser.ToLower().Contains("firefox"))
        {
            szCSS = "css/main-ff.css";
        }
        else
        {
            if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
            {
                szCSS = "css/main-ch.css";
            }
            else
            {
                szCSS = "css/main-ie.css";
            }
        }
        System.Text.StringBuilder b = new System.Text.StringBuilder();
        b.AppendLine("");
        if (_url.Contains("AJAX Pages")) { b.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />"); } else b.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        b.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        b.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        b.AppendLine("<link href='" + szCSS + "' type='text/css' rel='Stylesheet' />");
        this.framehead.InnerHtml = b.ToString();
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ss", "spanhide();", true);
        }
        if (Request.QueryString["CaseID"] != null)
        {
            string caseid = Request.QueryString["CaseID"].ToString();
            string companyid = Request.QueryString["cmpId"].ToString();
            DataSet ds = GetPatienView(caseid, companyid);
            PatientDtlView.DataSource = ds.Tables[0];
            PatientDtlView.DataBind();
            int count = PatientDtlView.Items.Count;
         //   DivStatus2.Visible = false;
          PatientDtlView.Visible=true;
        }
    }

    public DataSet GetPatienView(string caseID, string companyID)
    {
        DataSet ds =  new DataSet(); 
        SqlConnection sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }
}
