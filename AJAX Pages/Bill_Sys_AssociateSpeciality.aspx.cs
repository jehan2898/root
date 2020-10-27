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
using System.Data.Sql;

public partial class AJAX_Pages_Bill_Sys_AssociateSpeciality : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.con.SourceGrid = grdAssociateSpec;
        this.txtSearchBox.SourceGrid = grdAssociateSpec;
        this.grdAssociateSpec.Page = this.Page;
        this.grdAssociateSpec.PageNumberList = this.con;

        hdtxtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        extddlSpeciality.Flag_ID = hdtxtCompanyId.Text;
        if (!IsPostBack)
        {
            DataSet ds = new DataSet();
            ds = Get_Speciality();
            lstSpeciality.DataSource = ds.Tables[0];
            lstSpeciality.DataTextField = "description";
            lstSpeciality.DataValueField = "code";
            lstSpeciality.DataBind();
            hdtxtAssSpec.Text = "";
        }
    }

    protected void extddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        lstSpeciality.ClearSelection();
        ArrayList arrds = new ArrayList();
        for (int i = 0; i < grdAssociateSpec.Rows.Count; i++)
        {
            arrds.Add(grdAssociateSpec.DataKeys[i]["Associate_Id"].ToString());
        }
       
        for (int i = 0; i < lstSpeciality.Items.Count; i++)
        {
            foreach (string assospeId in arrds)
            {
                if (assospeId != lstSpeciality.Items[i].Value) continue;
                lstSpeciality.Items[i].Selected = true;
                break;
            }
        }
        
    }

    public void BindGrid()
    {
        divgrd.Visible = true;
        grdAssociateSpec.XGridBindSearch();
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdAssociateSpec.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    public DataSet Get_Speciality()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strcon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection SqlCon = new SqlConnection(strcon);
        DataSet ds = new DataSet();
        SqlCon.Open();
        try
        {
            SqlCommand SqlCmd = new SqlCommand("SP_MST_PROCEDURE_GROUP", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@id", hdtxtCompanyId.Text);
            SqlCmd.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");
            SqlDataAdapter da = new SqlDataAdapter(SqlCmd);
            da.Fill(ds);
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
        finally
        {
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAssociate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < lstSpeciality.Items.Count; i++)
            {
                if (lstSpeciality.Items[i].Selected)
                {
                    arr.Add(lstSpeciality.Items[i].Value);
                }
            }
            string szSpec = extddlSpeciality.Text;
            Insert_AssoSpec(arr, szSpec, hdtxtCompanyId.Text);
            usrMessage.PutMessage("Speciality associated Successfully...");
            usrMessage.Show();
            BindGrid();
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

    public void Insert_AssoSpec(ArrayList arr, string szSpeciality, string szCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string strcon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection SqlCon = new SqlConnection(strcon);
        SqlCon.Open();
        try
        {
            for (int i = 0; i < arr.Count; i++)
            {
                SqlCommand SqlCmd = new SqlCommand("SP_ASSOCIATE_SPECIALITY", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", szSpeciality);
                SqlCmd.Parameters.AddWithValue("@SZ_ASSOCIATE_SPECIALITY", arr[i].ToString());
                SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                SqlCmd.Parameters.AddWithValue("@FLAG", "INSERT");
                SqlCmd.ExecuteNonQuery();
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
        finally
        {
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet Get_Asso_Spec_Data(string szCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string strcon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection SqlCon = new SqlConnection(strcon);
        DataSet ds = new DataSet();
        SqlCon.Open();
        try
        {
            SqlCommand SqlCmd = new SqlCommand("SP_ASSOCIATE_SPECIALITY", SqlCon);
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
            SqlCmd.Parameters.AddWithValue("@FLAG", "GETDATA");
            SqlDataAdapter da = new SqlDataAdapter(SqlCmd);
            da.Fill(ds);
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
        finally
        {
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrID = new ArrayList();
            for (int i = 0; i < grdAssociateSpec.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdAssociateSpec.Rows[i].Cells[4].FindControl("chkDelete");

                if (chk.Checked)
                {
                    arrID.Add(grdAssociateSpec.DataKeys[i]["Associate_Id"].ToString());
                }
            }
            string szSpec = extddlSpeciality.Text;
            Delete_Asso_Spec(arrID, szSpec, hdtxtCompanyId.Text);
            lstSpeciality.ClearSelection();
            ArrayList arrds = new ArrayList();
            for (int i = 0; i < grdAssociateSpec.Rows.Count; i++)
            {
                arrds.Add(grdAssociateSpec.DataKeys[i]["Associate_Id"].ToString());
            }

            for (int i = 0; i < lstSpeciality.Items.Count; i++)
            {
                foreach (string assospeId in arrds)
                {
                    if (assospeId != lstSpeciality.Items[i].Value) continue;
                    lstSpeciality.Items[i].Selected = true;
                    break;
                }
            }
            usrMessage.PutMessage("Speciality deleted Successfully...");
            usrMessage.Show();
            BindGrid();
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

    public void Delete_Asso_Spec(ArrayList arr, string szSpeciality, string szCompanyID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string strcon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection SqlCon = new SqlConnection(strcon);
        SqlCon.Open();
        try
        {
            for (int i = 0; i < arr.Count; i++)
            {
                SqlCommand SqlCmd = new SqlCommand("SP_ASSOCIATE_SPECIALITY", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", szSpeciality);
                SqlCmd.Parameters.AddWithValue("@SZ_ASSOCIATE_SPECIALITY", arr[i].ToString());
                SqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyID);
                SqlCmd.Parameters.AddWithValue("@FLAG", "DELETE");
                SqlCmd.ExecuteNonQuery();
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
        finally
        {
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
