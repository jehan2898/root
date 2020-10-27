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
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
public partial class ShowSignImage : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (Request.QueryString["ImageId"] != null && Request.QueryString["Speciality"] != null)
        {
            if (Request.QueryString["Speciality"] =="PT")
            {
                lblProviderSign.Text="Doctor Sign";
            }
            else if (Request.QueryString["Speciality"] == "SYN")
            {
                lblProviderSign.Visible = false;
                DoctorImage.Visible = false;
            }
            string eventid = Request.QueryString["ImageId"].ToString();
            int eventid1 =int.Parse(eventid);
            String strsqlCon;
            SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;
            DataSet ds;
            string StatusId = "";
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            ds = new DataSet();
            try
            {
                conn = new SqlConnection(strsqlCon);
                conn.Open();
                comm = new SqlCommand("sp_get_sign_iamge_path", conn);               
                comm.Parameters.AddWithValue("@I_EVENT_ID", eventid1);               
                comm.Parameters.AddWithValue("@FLAG", Request.QueryString["Speciality"].ToString());                
                comm.CommandType = CommandType.StoredProcedure;

                sqlda = new SqlDataAdapter(comm);
                sqlda.Fill(ds);
                string signpath = "";
                string providerpath = "";
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count>0)
                {
                    signpath = ds.Tables[0].Rows[0][0].ToString();
                    providerpath = ds.Tables[0].Rows[0][1].ToString();
                    string openpath = ds.Tables[1].Rows[0][0].ToString();
                    DoctorImage.ImageUrl = openpath + providerpath;
                    imagepatient.ImageUrl = openpath + signpath;
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
                base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }
            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }

        }

    }
}
