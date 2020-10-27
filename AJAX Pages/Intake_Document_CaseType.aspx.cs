using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DevExpress.Web;

public partial class AJAX_Pages_Intake_Document_CaseType : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    { //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnUpdate.Visible = false;
            if (!IsPostBack)
            {
                this.btnDelete.Attributes.Add("onclick", "return CheckDelete();");
                this.btnSave.Attributes.Add("onclick", "return checkValue();");
                hdnUserId.Value = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                hdnCompanyId.Value = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //hdnCaseId.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                this.ddlCaseType.Flag_ID = hdnCompanyId.Value;
                Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();
                Intakedao.sz_case_type_id = ddlCaseType.Text;
                Intakedao.sz_company_id = hdnCompanyId.Value;
                bindgrdIntakeDocCaseTyp();


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
    }

    public void bindgrdIntakeDocCaseTyp()
    {
         DataSet dsgrd = new DataSet();
         Intake_Documents_BO IntakeBo = new Intake_Documents_BO();
         Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();

          Intakedao.sz_company_id = hdnCompanyId.Value;
          Intakedao.sz_case_type_id = ddlCaseType.Text;

          dsgrd = IntakeBo.select_intake_document(Intakedao);

          grdIntakeDocCaseTyp.DataSource = dsgrd.Tables[0];
          grdIntakeDocCaseTyp.DataBind();
          

    }

    protected void lnkid_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            DataSet dsUpdate = new DataSet();
            Intake_Documents_BO IntakeBo = new Intake_Documents_BO();
            Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();

            LinkButton lnkid = sender as LinkButton;
            hdnId.Value = lnkid.CommandArgument.ToString();
            
            Intakedao.i_id = Convert.ToInt32(lnkid.CommandArgument.ToString());
            dsUpdate = IntakeBo.select_intake_document_for_update(Intakedao);


            if (dsUpdate != null)
            {
                if (dsUpdate.Tables[0].Rows.Count > 0)
                {
                    ddlCaseType.Text = dsUpdate.Tables[0].Rows[0]["sz_case_type_id"].ToString();
                    Intakedao.sz_company_id  = dsUpdate.Tables[0].Rows[0]["sz_company_id"].ToString();
                    txtDocument.Text = dsUpdate.Tables[0].Rows[0]["sz_name"].ToString();

                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }

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
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();
        Intake_Documents_BO IntakeBo = new Intake_Documents_BO();
        try
        {

            Intakedao.i_id = 0;

            Intakedao.sz_case_id = hdnCaseId.Value;
            Intakedao.sz_company_id = hdnCompanyId.Value;
            Intakedao.sz_case_type_id = ddlCaseType.Text;
            //Intakedao.CaseType = ddlCaseType.Text;
            Intakedao.sz_name = txtDocument.Text;
            Intakedao.i_user_id = hdnUserId.Value;

            IntakeBo.update_intake_document(Intakedao);

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Saved Successfully')", true);
            this.usrMessage.PutMessage("Record Saved Sucessfully");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            
            bindgrdIntakeDocCaseTyp();


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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();
        Intake_Documents_BO IntakeBo = new Intake_Documents_BO();

        try
        {
            if (hdnId.Value != null)
            {
                Intakedao.i_id = Convert.ToInt32(hdnId.Value);

                Intakedao.sz_case_id = hdnCaseId.Value;
                Intakedao.sz_company_id = hdnCompanyId.Value;
                Intakedao.sz_case_type_id = ddlCaseType.Text;
                //Intakedao.CaseType = ddlCaseType.Text;
                Intakedao.sz_name = txtDocument.Text;
                Intakedao.i_user_id = hdnUserId.Value;

                IntakeBo.update_intake_document(Intakedao);

                this.usrMessage.PutMessage("Records Updated Sucessfully");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                btnSave.Visible = false;
                
                bindgrdIntakeDocCaseTyp();
            }

        }
        catch(Exception ex)
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           
            for (int i = 0; i < this.grdIntakeDocCaseTyp.VisibleRowCount; i++)
            {

                GridViewDataColumn col = (GridViewDataColumn)grdIntakeDocCaseTyp.Columns[0];
                CheckBox box = (CheckBox)this.grdIntakeDocCaseTyp.FindRowCellTemplateControl(i, col, "chkdel");

                if (box.Checked == true)
                {

                    Intake_Documents_BO IntakeBo = new Intake_Documents_BO();
                    Intake_Documents_DAO Intakedao = new Intake_Documents_DAO();

                    Intakedao.sz_company_id = hdnCompanyId.Value.ToString();
                    Intakedao.i_id = Convert.ToInt32(grdIntakeDocCaseTyp.GetRowValues(i, "i_id").ToString());
                    IntakeBo.delete_intake_document(Intakedao);
                    
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
                    this.usrMessage.PutMessage("Records Deleted Sucessfully");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                }
            }

            bindgrdIntakeDocCaseTyp();
           
        }
        catch(Exception ex)
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