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
using Componend;
using System.Data.SqlClient;

public partial class Bill_Sys_ComplaintMaster : PageBase
{
    private SaveOperation _saveOperation;

    protected void Page_Load(object sender, EventArgs e)
    {
        extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
    }
    protected void LoadSpeciality(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdSpecialtyComplaints.DataSource = BindGrid(txtCompanyId.Text, extddlSpeciality.Text);
            grdSpecialtyComplaints.DataBind();
            grdSpecialtyComplaints.Columns[2].Visible = false;
            if (grdSpecialtyComplaints.Rows.Count > 0)
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        txtComplaint.Text = txtComplaintcopy.Text;
            txtProcedureGroup.Text = extddlSpeciality.Text;
            if (extddlSpeciality.Text == "-Select-")
            {
                usrMessage.PutMessage("Please select a Specialty");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();               
                return;
            }
            if (txtComplaintcopy.Text == "")
            {
                usrMessage.PutMessage("Please Write Complaint");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();               
                return;
            }
            _saveOperation = new SaveOperation();
            try
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "Complaint.xml";
                _saveOperation.SaveMethod();

                usrMessage.PutMessage("Complaint Saved Successfully ...!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage );
                usrMessage.Show();                   
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
        grdSpecialtyComplaints.DataSource = BindGrid(txtCompanyId.Text, extddlSpeciality.Text);
            grdSpecialtyComplaints.DataBind();
            grdSpecialtyComplaints.Columns[2].Visible = false;
            if (grdSpecialtyComplaints.Rows.Count > 0)
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    public DataSet BindGrid(string p_szCompanyID, string p_szProcedureGroupid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
            SqlConnection sqlCon = new SqlConnection(strConn);
            sqlCon = new SqlConnection(strConn);
            DataSet ds = new DataSet();
            String szConfigValue = "";
            try
            {
                sqlCon.Open();
                SqlCommand sqlCmd;
                sqlCmd = new SqlCommand("sp_get_complaints", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@sz_company_id", p_szCompanyID);
                sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", p_szProcedureGroupid);
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
    public void deleteComplaint(string ID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("sp_delete_complaints", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", ID);
            sqlCmd.ExecuteNonQuery();

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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnDelete_Click1(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            CheckBox chk;
            DataSet dt = BindGrid(txtCompanyId.Text, extddlSpeciality.Text);
            for (int i = 0; i < grdSpecialtyComplaints.Rows.Count; i++)
            {
                chk = (CheckBox)grdSpecialtyComplaints.Rows[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                    deleteComplaint(dt.Tables[0].Rows[i][2].ToString());
                }
            }
            grdSpecialtyComplaints.DataSource = BindGrid(txtCompanyId.Text, extddlSpeciality.Text);
            grdSpecialtyComplaints.DataBind();
            grdSpecialtyComplaints.Columns[2].Visible = false;
            if (grdSpecialtyComplaints.Rows.Count > 0)
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
            usrMessage.PutMessage("Complaints deleted successfully.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show(); 
            
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
}
