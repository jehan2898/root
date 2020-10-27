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

public partial class CaseDetailsPopup : PageBase
{
    SqlConnection conn;

    protected void Page_Init(object sender, EventArgs e)
    {
       // DivStatus2.Visible = true;
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
        string  flag = Request.QueryString["flag"].ToString().Trim();
        string  caseid = Request.QueryString["CaseID"].ToString();
        string companyid = Request.QueryString["CmpID"].ToString();

        if(flag=="claimno")
        {
            grdDateOfAccList.DataSource=GetPOpupDetails(caseid, companyid, "SP_GET_CLAIM_INFO");
            grdDateOfAccList.DataBind();
        }else
            if(flag=="policyno")
            {
                grdDateOfAccList.DataSource=GetPOpupDetails(caseid, companyid, "SP_GET_POLICY_INFO");
                grdDateOfAccList.DataBind();
            }else
                if(flag=="dtaccident")
                {
                    grdDateOfAccList.DataSource=GetPOpupDetails(caseid, companyid, "SP_GET_ACCIDENT_DATA");
                    grdDateOfAccList.DataBind();
                }else
                    if(flag=="plateno")
                    {
                        grdDateOfAccList.DataSource=GetPOpupDetails(caseid, companyid, "SP_GET_PLATE_NO");
                        grdDateOfAccList.DataBind();
                    }else
                        if(flag=="accidentreportno")
                        {
                            grdDateOfAccList.DataSource=GetPOpupDetails(caseid, companyid, "SP_GET_REPORT_NO");
                            grdDateOfAccList.DataBind();
                        }else
                            if(flag=="adjustername")
                            {
                                grdDateOfAccList.DataSource=GetPOpupDetails(caseid, companyid, "SP_GET_ADJUSTER_INFO");
                                grdDateOfAccList.DataBind();

                            }

    }

    public DataSet GetPOpupDetails(string p_szCaseID, string p_szCompanyID, string Proc_Name)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        try
        {
            string strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            SqlCommand comm = new SqlCommand();
            comm.CommandText = Proc_Name;
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_CASE_ID", p_szCaseID);
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", p_szCompanyID);
          
            SqlDataAdapter sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);
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
        finally { conn.Close(); }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
