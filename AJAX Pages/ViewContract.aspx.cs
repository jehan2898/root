using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class AJAX_Pages_ViewContract : System.Web.UI.Page
{
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;

    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {

        if(!IsPostBack)
        {
            string id = Request.QueryString["procid"].ToString();
            BindGrid(id);
        }
    }
    protected void BindGrid(string procedureID)
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strConn);
        SqlDataReader dr;
        String szConfigValue = "";
        ds = new DataSet();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("proc_get_procedure_contract", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_PROCEDUE_ID", procedureID);
           
            sqlda = new SqlDataAdapter(sqlCmd);

            sqlda.Fill(ds);
            grdContract.DataSource = ds;
            grdContract.DataBind();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
    }
}