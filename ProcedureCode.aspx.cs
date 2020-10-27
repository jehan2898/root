using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using DevExpress.Web;
public partial class ProcedureCode1 : PageBase
{
    private ProcedureCode_function _ProcedureCode_function;
    protected void Page_Load(object sender, EventArgs e)
    {
        btnAdd.Attributes.Add("onclick", "return formValidator('aspnetForm','frmProcedureCode','txtcode,cmbSpecialty,ddlType,txtAmount');");
        btnUpdate.Attributes.Add("onclick", "return formValidator('aspnetForm','frmProcedureCode','txtcode,cmbSpecialty,ddlType,txtAmount');");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        loadSpecialty();
        bindgrid();
    }


    private void loadSpecialty()
    {
        DataSet ds = new DataSet();
        Reportfunction obj = new Reportfunction();

       // ASPxListBox box = (ASPxListBox)this.cmbSpecialty.FindControl("lst_specialty");

        ds = obj.loadSpecialty(txtCompanyID.Text);

        cmbSpecialty.TextField = "SZ_PROCEDURE_GROUP";
        cmbSpecialty.ValueField = "SZ_PROCEDURE_GROUP_ID";
        cmbSpecialty.DataSource = ds;
        cmbSpecialty.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select---", "NA");
        cmbSpecialty.Items.Insert(0, Item);
        cmbSpecialty.SelectedIndex = 0;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
         {
            string s_specialty="";
            s_specialty= cmbSpecialty.SelectedItem.Value.ToString();
            int iVisitType = 0;
            if (rbVisitType.SelectedItem.Value.ToString() != "")
            {
                iVisitType = Convert.ToInt32(rbVisitType.SelectedItem.Value.ToString());
            }
            string szAddToPreferred = "0";

            if (cbPreferredList0.Checked)
            {
                szAddToPreferred = "1";
            }
           
            _ProcedureCode_function.Save_Update_ProcedureCode(txtCode.Text, s_specialty, txtShortDescription.Text, txtAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, "", "ADD", szAddToPreferred, txtREVcode.Text, txtPAScode.Text, txtModifierLongDesc.Text, txtModifierDesc.Text, txtRVU.Text);


            usrMessage.PutMessage("Procedure codes save successfully ...");
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

    protected void bindgrid()
    
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ProcedureCode_function vfobj = new ProcedureCode_function();

            DataSet ds = new DataSet();
            ds = vfobj.loadProcedureCode(txtCompanyID.Text);
            grdProcedure.DataSource = ds;
            grdProcedure.DataBind();
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


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _ProcedureCode_function = new ProcedureCode_function();
        try
        {
            int iVisitType = 0;
            if (rbVisitType.SelectedItem.Value.ToString() != "")
            {
                iVisitType = Convert.ToInt32(rbVisitType.SelectedItem.Value.ToString());
            }

            string szAddToPreferred = "0";

            if (cbPreferredList0.Checked)
            {
                szAddToPreferred = "1";
            }

            if (Session["ProcedureCodeID"] != null || Session["ProcedureCodeID"].ToString() != "&nbsp;")
            {
                _ProcedureCode_function.Save_Update_ProcedureCode(txtCode.Text, cmbSpecialty.Text, txtShortDescription.Text, txtAmount.Text, iVisitType, txtModifier.Text, txtCompanyID.Text, Session["ProcedureCodeID"].ToString(), "UPDATE", szAddToPreferred, txtREVcode.Text, txtPAScode.Text, txtModifierLongDesc.Text, txtModifierDesc.Text, txtRVU.Text);
            }           

            usrMessage.PutMessage("Procedure codes Updated successfully ...");
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
}