using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;


public partial class AJAX_Pages_BillingCompany : PageBase
{
    Bill_Sys_BillingCompanyDetails_BO objBillingCompanyDetails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnSave.Attributes.Add("onclick", "return Validate()");
            btnUpdate.Attributes.Add("onclick", "return Validate()");
            btnDelete.Attributes.Add("onclick", "return ValidateDelete()");
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindData();
            txtID.Text = "";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objBillingCompanyDetails = new Bill_Sys_BillingCompanyDetails_BO();           
            ArrayList arrSelect = new ArrayList();
            arrSelect.Add(txtName.Text);
            arrSelect.Add(txtAddress.Text);
            arrSelect.Add(txtCity.Text);
            arrSelect.Add(extddlOfficeState.Selected_Text);
            arrSelect.Add(extddlOfficeState.Text);
            arrSelect.Add(txtZip.Text);
            arrSelect.Add(txtPhone.Text);
            arrSelect.Add(txtFax.Text);
            arrSelect.Add(txtCompanyID.Text);
            arrSelect.Add("ADD");
         int icnt=   objBillingCompanyDetails.SaveJFKBilligCompany(arrSelect);
         
            BindData();
            
            if (icnt > 0)
            {
                usrMessage.PutMessage("record inserted successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            clear();
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
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objBillingCompanyDetails = new Bill_Sys_BillingCompanyDetails_BO();
            ArrayList arrSelect = new ArrayList();
            arrSelect.Add(txtName.Text);
            arrSelect.Add(txtAddress.Text);
            arrSelect.Add(txtCity.Text);
            arrSelect.Add(extddlOfficeState.Selected_Text);
            arrSelect.Add(extddlOfficeState.Text);
            arrSelect.Add(txtZip.Text);
            arrSelect.Add(txtPhone.Text);
            arrSelect.Add(txtFax.Text);
            arrSelect.Add(txtCompanyID.Text);
            arrSelect.Add(txtID.Text);
            arrSelect.Add("UPDATE");
          
          int icnt=  objBillingCompanyDetails.SaveUpdateJFKBilligCompany(arrSelect);
          btnUpdate.Enabled = false;
            if (icnt > 0)
            {
                usrMessage.PutMessage("record updated successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            BindData();
            clear();
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string Id = grdCompany.Items[0].Cells[1].Text;
            objBillingCompanyDetails = new Bill_Sys_BillingCompanyDetails_BO();
            ArrayList arrSelect = new ArrayList();
            arrSelect.Add(Id);
            arrSelect.Add(txtCompanyID.Text);
            arrSelect.Add("DELETE");
            int icnt = objBillingCompanyDetails.DeleteJFKBilligCompany(arrSelect);
            if (icnt > 0)
            {
                usrMessage.PutMessage("record deleted successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            BindData();
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

    public void BindData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            objBillingCompanyDetails = new Bill_Sys_BillingCompanyDetails_BO(); 
            DataSet dsVlaues = new DataSet();
            ArrayList arrSelect = new ArrayList();
            arrSelect.Add(txtCompanyID.Text);
            arrSelect.Add("LIST");
            dsVlaues = objBillingCompanyDetails.SelectJFKBilligCompany(arrSelect);
            grdCompany.DataSource = dsVlaues;
            grdCompany.DataBind();
            if (grdCompany.Items.Count > 0)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
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

    protected void grdCompany_SelectedIndexChanged(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "selectedIndexChanged", "javascriptFunctionCallHere();", true);
            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[1].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[1].Text != "&nbsp;"))
            {
                txtID.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[1].Text;
            }
            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[2].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[2].Text != "&nbsp;"))
            {
                txtName.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[2].Text;
            }

            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[3].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[3].Text != "&nbsp;"))
            {
                txtAddress.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[3].Text;
            }

            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[4].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[4].Text != "&nbsp;"))
            {
                txtCity.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[4].Text;
            }
            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[4].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[4].Text != "&nbsp;"))
            {
                txtCity.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[4].Text;
            }
            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[6].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[6].Text != "&nbsp;"))
            {
                extddlOfficeState.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[6].Text;
            }
            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[7].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[7].Text != "&nbsp;"))
            {
                txtZip.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[7].Text;
            }

            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[8].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[8].Text != "&nbsp;"))
            {
                txtPhone.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[8].Text;
            }
            if ((grdCompany.Items[grdCompany.SelectedIndex].Cells[9].Text != "") || !(grdCompany.Items[grdCompany.SelectedIndex].Cells[9].Text != "&nbsp;"))
            {
                txtFax.Text = grdCompany.Items[grdCompany.SelectedIndex].Cells[9].Text;
            }
            btnUpdate.Enabled = true;
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

    public void clear()
    {
        txtAddress.Text = "";
        txtCity.Text = "";
        txtFax.Text = "";
        txtID.Text = "";
        txtName.Text= "";
      txtPhone.Text = "";
      txtZip.Text = "";
      extddlOfficeState.Text = "NA";
    }
}

    
