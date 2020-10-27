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

public partial class Bill_Sys_Mandatory_Settings : PageBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindListField();
            BindMadatoryField();
        }
    }
    public void BindListField()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            String strConn;
            SqlConnection conn;
            SqlCommand comm;
            SqlDataAdapter da;
            DataSet ds;

            SqlDataReader dr;
            conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "SP_GET_MANDATORY_FIELD";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PAGE", "Bill_Sys_Patient.aspx");

            da = new SqlDataAdapter(comm);
            ds = new DataSet();
            da.Fill(ds);
            lstField.DataSource = ds.Tables[0];
            lstField.DataTextField = "SZ_MANDATORY_COLUMN";
            lstField.DataValueField = "SZ_CONTROL_NAME";
            lstField.DataBind();


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

    public void BindMadatoryField()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataSet dsChek = new DataSet();
            String strConn;
            SqlConnection conn;
            SqlCommand comm;
            SqlDataAdapter da;
            DataSet ds;

            SqlDataReader dr;
            conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            conn.Open();
            comm = new SqlCommand();
            comm.CommandText = "SP_GET_SELECTED_MANDATORY_FIELD";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PAGE", "Bill_Sys_Patient.aspx");
            string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId);
            da = new SqlDataAdapter(comm);

            da.Fill(dsChek);

           
           
            if (dsChek.Tables[0].Rows.Count > 0)
            {

                lstMadatoryField.DataSource = dsChek.Tables[0];
                lstMadatoryField.DataTextField = "SZ_MANDATORY_C0LUMN";
                lstMadatoryField.DataValueField = "SZ_CONTROL_NAME";
                lstMadatoryField.DataBind();
            }
            else
            {
                dsChek = null;
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        int iFlag = 0;
        
        
        foreach (ListItem li in lstField.Items)
        {
            if (li.Selected.Equals(true))
            {   iFlag=0;
            foreach (ListItem li1 in lstMadatoryField.Items)
                {
                    if (li1.Text.ToString().Trim().Equals(li.Text.ToString().Trim()))
                    {
                        iFlag = 1;
                    }
                }
                if (iFlag == 0)
                {
                    lstMadatoryField.Items.Add(li);
                }
           
                
            }
        }
       
        
    }
    protected void saveSelectedDoc_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        SqlConnection conn1;
        SqlCommand comm1;
        conn1 = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        string szCmpId1 = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        try
        {

            
            conn1.Open();
            comm1 = new SqlCommand();
            comm1.CommandText = "SP_MST_MANDATORY";
            comm1.CommandType = CommandType.StoredProcedure;
            comm1.Connection = conn1;
            comm1.Parameters.AddWithValue("@SZ_PAGE", "Bill_Sys_Patient.aspx");
          
            comm1.Parameters.AddWithValue("@SZ_COMPANY_ID", szCmpId1);

            

            comm1.Parameters.AddWithValue("@FLAG", "DELETE");
         
            comm1.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            
            throw;
        }
        finally
        {
            conn1.Close();
        }

                String strConn;
                SqlConnection conn;
                SqlCommand comm;
                int iFlag; 
                //DataTable tblMadatoryField = new DataTable();
                // tblMadatoryField.Columns.Add("SZ_PAGE");
                // tblMadatoryField.Columns.Add("SZ_MANDATORY_C0LUMN");
                // tblMadatoryField.Columns.Add("SZ_COMPANY_ID");
                // tblMadatoryField.Columns.Add("SZ_CONTROL_NAME");

         conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
         string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        foreach (ListItem li in lstMadatoryField.Items)
        {
            try
            {
                
                
               
             
                {
                   // DataRow drMF = tblMadatoryField.NewRow() ;
                   
                    conn.Open();
                    comm = new SqlCommand();
                    comm.CommandText = "SP_MST_MANDATORY";
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Connection = conn;
                    comm.Parameters.AddWithValue("@SZ_PAGE", "Bill_Sys_Patient.aspx");
                    //drMF["SZ_PAGE"]="Bill_Sys_Patient.aspx";
                    comm.Parameters.AddWithValue("@SZ_MANDATORY_C0LUMN",li.Text);
                    //drMF["SZ_MANDATORY_C0LUMN"] = li.Text;
                    //  string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString(); 
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID",szCmpId);
                    //drMF["SZ_COMPANY_ID"] = szCmpId;
                    comm.Parameters.AddWithValue("@SZ_CONTROL_NAME", li.Value);

                    comm.Parameters.AddWithValue("@FLAG", "ADD");
                    //drMF["SZ_CONTROL_NAME"] = li.Value;
                    comm.ExecuteNonQuery();
                    //tblMadatoryField.Rows.Add(drMF);
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
           
            finally
                {
                    conn.Close();
                }
            //Method End
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
        }
        //DataSet dsMadatoryField = new DataSet();
        //dsMadatoryField.Tables.Add(tblMadatoryField);
        //Session["MadatoryField"] = dsMadatoryField;

    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {


        DataTable tblMadatoryField = new DataTable();

        tblMadatoryField.Columns.Add("SZ_MANDATORY_C0LUMN");
        tblMadatoryField.Columns.Add("SZ_CONTROL_NAME");

        foreach (ListItem li in lstMadatoryField.Items)
                {
                    DataRow drMF = tblMadatoryField.NewRow();
                    if (li.Selected.Equals(true))
                    {

                    }
                    else
                    {
                        drMF["SZ_MANDATORY_C0LUMN"] = li.Text;
                        drMF["SZ_CONTROL_NAME"] = li.Value;
                        tblMadatoryField.Rows.Add(drMF);
                    }
                }

               DataSet dsMadatoryField1 = new DataSet();
               dsMadatoryField1.Tables.Add(tblMadatoryField);

               if (dsMadatoryField1.Tables[0].Rows.Count > 0)
               {

                   lstMadatoryField.DataSource = dsMadatoryField1.Tables[0];
                   lstMadatoryField.DataTextField = "SZ_MANDATORY_C0LUMN";
                   lstMadatoryField.DataValueField = "SZ_CONTROL_NAME";
                   lstMadatoryField.DataBind();
               }
               else
               {
                   lstMadatoryField.Items.Clear();
               }
                


                    
    }
}
