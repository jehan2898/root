using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AJAX_Pages_IntakeSheetProvider : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
        {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            if (!IsPostBack)
            {
                btnSave.Attributes.Add("onclick", "return validate();");

                hdnUserId.Value = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                hdnCompanyId.Value = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                bindgrdIntakeSheet();
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


    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            IntakeSheetProviderDAO Intakedao = new IntakeSheetProviderDAO();
            IntakeSheetProviderBO IntakeBo = new IntakeSheetProviderBO();
            Intakedao.i_id = 0;

            //Intakedao.sz_case_id = hdnCaseId.Value;
            Intakedao.sz_company_id = hdnCompanyId.Value;
            Intakedao.sz_name = txtName.Text;
            Intakedao.sz_address = txtAddress.Text;
            Intakedao.sz_city = txtCity.Text;
            Intakedao.i_state_id =Convert.ToInt32(extddlOfficeState.Text);
            Intakedao.sz_zip = txtZip.Text;
            Intakedao.sz_phone = txtPhone.Text;
            Intakedao.sz_email = txtEmail.Text;
            Intakedao.i_user_id = hdnUserId.Value;

            IntakeBo.update_intake_sheet_provider(Intakedao);
           
            this.usrMessage.PutMessage("Record Saved Sucessfully");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();

            bindgrdIntakeSheet();
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
        try
        {
            IntakeSheetProviderDAO Intakedao = new IntakeSheetProviderDAO();
            IntakeSheetProviderBO IntakeBo = new IntakeSheetProviderBO();

            if (hdnId.Value != null)
            {
                Intakedao.i_id = Convert.ToInt32(hdnId.Value);

                Intakedao.sz_company_id = hdnCompanyId.Value;
                Intakedao.sz_name = txtName.Text;
                Intakedao.sz_address = txtAddress.Text;
                Intakedao.sz_city = txtCity.Text;
                Intakedao.i_state_id = Convert.ToInt32(extddlOfficeState.Text);
                Intakedao.sz_zip = txtZip.Text;
                Intakedao.sz_phone = txtPhone.Text;
                Intakedao.sz_email = txtEmail.Text;
                Intakedao.i_user_id = hdnUserId.Value;

                IntakeBo.update_intake_sheet_provider(Intakedao);

                this.usrMessage.PutMessage("Records Updated Sucessfully");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
                
                bindgrdIntakeSheet();
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
    public void bindgrdIntakeSheet()
    {
        DataSet dsgrd = new DataSet();
        IntakeSheetProviderBO IntakeBo = new IntakeSheetProviderBO();
        IntakeSheetProviderDAO Intakedao = new IntakeSheetProviderDAO();

        Intakedao.sz_company_id = hdnCompanyId.Value;
       

        dsgrd = IntakeBo.select_intake_sheet_provider(Intakedao);

        grdIntakeSheet.DataSource = dsgrd.Tables[0];
        grdIntakeSheet.DataBind();


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
            IntakeSheetProviderDAO Intakedao = new IntakeSheetProviderDAO();
            IntakeSheetProviderBO IntakeBo = new IntakeSheetProviderBO();

            LinkButton lnkid = sender as LinkButton;
            hdnId.Value = lnkid.CommandArgument.ToString();

            Intakedao.i_id = Convert.ToInt32(lnkid.CommandArgument.ToString());
            dsUpdate = IntakeBo.select_intake_sheet_provider_for_update(Intakedao);


            if (dsUpdate != null)
            {
                if (dsUpdate.Tables[0].Rows.Count > 0)
                {

                    txtName.Text = dsUpdate.Tables[0].Rows[0]["sz_name"].ToString();
                    txtAddress.Text = dsUpdate.Tables[0].Rows[0]["sz_address"].ToString();
                    txtCity.Text = dsUpdate.Tables[0].Rows[0]["sz_city"].ToString();
                    extddlOfficeState.Text = dsUpdate.Tables[0].Rows[0]["i_state_id"].ToString();
                    txtZip.Text = dsUpdate.Tables[0].Rows[0]["sz_zip"].ToString();
                    txtPhone.Text = dsUpdate.Tables[0].Rows[0]["sz_phone"].ToString();
                    txtEmail.Text = dsUpdate.Tables[0].Rows[0]["sz_email"].ToString();
                    //Intakedao.sz_company_id = dsUpdate.Tables[0].Rows[0]["sz_company_id"].ToString();

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
}