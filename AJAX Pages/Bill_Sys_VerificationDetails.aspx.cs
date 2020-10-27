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
using DevExpress.Web;


public partial class AJAX_Pages_Bill_Sys_VerificationDetails : PageBase
{
    string caseId = "";
    string Billno = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["CaseID"] != null && Request.QueryString["billno"] != null && Request.QueryString["CompanyID"] != null)
            {
                caseId = Request.QueryString["CaseID"].ToString();
                Billno = Request.QueryString["billno"].ToString();
                txtCompanyID.Text = Request.QueryString["CompanyID"].ToString();
            }
            DataSet dsverification = new DataSet();
            dsverification = GetVerification(txtCompanyID.Text, "'"+Billno+"'");
            grdVER.DataSource = dsverification;
            grdVER.DataBind();
        }
    }
    public DataSet GetVerification(string sz_CompanyID, string sz_bill_number)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        //bt_operation = 2;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@sz_input_bill_number", sz_bill_number);
            sqlCmd.Parameters.AddWithValue("@bt_operation", 2);
            //sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", p_szProcedureGroupid);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);

        }
        catch (SqlException ex)
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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

}
