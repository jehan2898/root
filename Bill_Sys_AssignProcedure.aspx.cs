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
using System.Data.Sql;
using System.Data.SqlClient;
public partial class Bill_Sys_AssignProcedure : PageBase
{
    private Bill_Sys_ProcedureCode_BO _bill_Sys_ProcedureCode_BO;
    private SaveOperation _saveOperation;
    private ListOperation _listOperation;
    SqlConnection con;
    SqlCommand cmd;
    ArrayList savearr;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnSave.Attributes.Add("onclick", "return formValidator('frmAssignProcCode','extddlDoctor,lstProcCode');");
            btnSave.Attributes.Add("onclick", "check()");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                if (Session["CASE_OBJECT"] != null)
                {
                    txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                }

                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    extddlDoctor.Procedure_Name = "SP_MST_BILLING_DOCTOR";
                    extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                else
                {
                    extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                FillListBox();
                BindGrid();
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
       
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_AssignProcedure.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extddlDoctor.Text != "NA")
            {
                GetProcedureCode(extddlDoctor.Text);
            }
            else
            {
                ClearControl();
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

    private void GetProcedureCode(string p_szDoctorID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        try
        {

            lstProcCode.DataSource = _bill_Sys_ProcedureCode_BO.GetDoctorSpecialityProcedureCodes(extddlDoctor.Text, txtCompanyID.Text);
            lstProcCode.DataTextField = "DESCRIPTION";
            lstProcCode.DataValueField = "CODE";
            lstProcCode.DataBind();

            ListItem lst = new ListItem("--Select--", "0");       
         
            lstProcCode.Items.Insert(0, lst);

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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            extddlDoctor.Text = "NA";
            lstProcCode.Items.Clear();
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
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation();
        try
        {
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "AssignProcedureCodeXML.xml";
            _listOperation.LoadList();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _saveOperation = new SaveOperation();
        try
        {

            foreach (ListItem lstItem in lstProcCode.Items)
            {
                if (lstItem.Selected == true)
                {
                    txtProcCode.Text = lstItem.Value;
                    _saveOperation.WebPage = this.Page;
                    _saveOperation.Xml_File = "AssignProcedureCodeXML.xml";
                    _saveOperation.SaveMethod();
                }
                
            }
            ArrayList objsave = new ArrayList();
            for (int j = 0; j < chklist.Items.Count; j++)
            {
                if (chklist.Items[j].Selected == true)
                {
                    objsave.Add(chklist.Items[j].Value);
                }
            }
           // //
            SaveCheckbox(objsave);
            BindGrid();
            ClearControl();
            usrMessage.PutMessage("Record added sucessfully");
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
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
        _bill_Sys_ProcedureCode_BO = new Bill_Sys_ProcedureCode_BO();
        try
        {
            foreach (DataGridItem grdItem in grdAssignProcCode.Items)
            {
                CheckBox chkDel = (CheckBox)grdItem.FindControl("chkDelete");

                if (chkDel.Checked)
                {
                    _bill_Sys_ProcedureCode_BO.DeleteAssignProcCode(grdItem.Cells[1].Text);
                }
                
            }

            BindGrid();
            ClearControl();
            usrMessage.PutMessage("Record delete sucessfully");
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ClearControl();
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

    public void FillListBox()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            cmd = new SqlCommand("SP_GET_ASSIGNPROCEDURE_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SZ_COMPANY_ID", txtCompanyID.Text);
            cmd.Parameters.Add("@FLAG", "GET");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
          ArrayList objlst = new ArrayList();
            while (dr.Read())
            {
               //hdllistbox.Value = hdllistbox.Value + "," + dr.GetValue(0).ToString();
                ListItem lst = new ListItem();
                lst.Text = dr.GetValue(1).ToString();
                lst.Value = dr.GetValue(0).ToString();
                chklist.Items.Add(lst);
                
            }
            
           // chklist.DataSource = objlst;
           // chklist.DataBind();
           
            savearr = GetSaveList();
            for (int i = 0; i < chklist.Items.Count; i++)
            {
                for (int j = 0; j < savearr.Count; j++)
                {
                    if (chklist.Items[i].Value.Equals(savearr[j]))
                    {
                        chklist.Items[i].Selected = true;

                    }
                }
                
            }
            //GetSaveList();
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
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }


    }
    public void SaveCheckbox(ArrayList objarrsave)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            getdelete();
            con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            con.Open();
            for (int j = 0; j < objarrsave.Count; j++)
            {
                cmd = new SqlCommand("SP_GET_ASSIGNPROCEDURE_LIST", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SZ_COMPANY_ID", txtCompanyID.Text);
                cmd.Parameters.Add("@SZ_CASE_ID", txtCaseID.Text);
                cmd.Parameters.Add("@SZ_PROCEDURE_GROUP_ID", objarrsave[j].ToString());
                cmd.Parameters.Add("@SZ_CREATED_USER_ID", ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                cmd.Parameters.Add("@FLAG", "SAVE");
               
                cmd.ExecuteNonQuery();
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
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public ArrayList  GetSaveList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList objarrlst = new ArrayList();
        try
        {
           
            con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            cmd = new SqlCommand("SP_GET_ASSIGNPROCEDURE_LIST", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SZ_COMPANY_ID", txtCompanyID.Text);
            cmd.Parameters.Add("@SZ_CASE_ID", txtCaseID.Text);
            cmd.Parameters.Add("@FLAG", "GETSAVELIST");
            con.Open();
            SqlDataReader objdr = cmd.ExecuteReader();
           
            while (objdr.Read())
            {
                objarrlst.Add(objdr.GetValue(0));
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
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
           
        }
        return objarrlst;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void getdelete()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
            cmd = new SqlCommand();
            cmd.CommandText = "delete from TXN_ASSIGNPROCEDURE_GROUP where SZ_COMPANY_ID = '" + txtCompanyID.Text + "' AND SZ_CASE_ID = '" + txtCaseID.Text + "'";
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
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
