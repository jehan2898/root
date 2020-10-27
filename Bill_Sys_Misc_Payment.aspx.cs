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

public partial class Bill_Sys_Misc_Payment : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    Bill_Sys_DeleteBO _deleteOperation;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        btnSave.Attributes.Add("onclick", "return formValidator('frmPaymentTrans','txtChequeDate,txtChequeNumber,txtChequeAmount');");
        btnUpdate.Attributes.Add("onclick", "return formValidator('frmPaymentTrans','txtChequeDate,txtChequeNumber,txtChequeAmount');");
        btnDelete.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?')");
       

        if (!IsPostBack)
        {
            lblPosteddate.Text = DateTime.Today.ToShortDateString();
            txtPaymentDate.Text = lblPosteddate.Text;
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            BindGrid();
        }
    }
    protected void btnLitigation_Click(object sender, EventArgs e)
    {

    }
    protected void btnWriteoff_Click(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
        BindGrid();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (Page.IsValid)
        {
            try
            {
                txtUserID.Text = txtUserID.Text;
                txtDescription.Text = txtDescription.Text.Trim();
                _saveOperation = new SaveOperation();
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "MiscPaymentTransaction.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                usrMessage.PutMessage("Payment added successfully..!");
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        if (Page.IsValid)
        {
            try
            {
                txtUserID.Text = txtUserID.Text;
                txtDescription.Text = txtDescription.Text.Trim();
                _editOperation = new EditOperation();
                _editOperation.WebPage = this.Page;
                _editOperation.Primary_Value = txtPaymentID.Text;
                _editOperation.Xml_File = "MiscPaymentTransaction.xml";
                _editOperation.UpdateMethod();
                BindGrid();
                usrMessage.PutMessage("Selected payment update successfully..!");
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();

    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {

    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        txtUserID.Text = txtUserID.Text;
        _deleteOperation = new Bill_Sys_DeleteBO();
        String szListOfPayment = "";
        try
        {
            for (int i = 0; i < grdPaymentTransaction.Items.Count; i++)
            {
                CheckBox checkDelete = (CheckBox)grdPaymentTransaction.Items[i].FindControl("chkDelete");
                if (checkDelete.Checked)
                {
                    if (_deleteOperation.deleteRecord("SP_TXN_MISC_PAYMENT_TRANSACTIONS", "@I_PAYMENT_ID", grdPaymentTransaction.Items[i].Cells[1].Text))
                    {
                       
                        usrMessage.PutMessage("Selected Payment deleted successfully .....!");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }

                }
            }
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
            _listOperation.Xml_File = "MiscPaymentTransaction.xml";
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
        
        ClearAll();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPaymentTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        txtChequeNumber.Text  = e.Item.Cells[2].Text;
        txtPaymentDate.Text = e.Item.Cells[3].Text ;
        lblPosteddate.Text = e.Item.Cells[3].Text;
        txtChequeDate.Text = e.Item.Cells[4].Text;
        txtChequeAmount.Text = e.Item.Cells[5].Text;
        txtCaseID.Text = e.Item.Cells[7].Text;
        txtUserID.Text = e.Item.Cells[8].Text;
        txtPaymentID.Text = e.Item.Cells[1].Text;
        if (e.Item.Cells[6].Text.Equals("&nbsp;"))
        {
            txtDescription.Text = "";
        }
        else
        {
            txtDescription.Text = e.Item.Cells[6].Text;
        }
        
        btnUpdate.Enabled = true;
        btnSave.Enabled = false;
    }

    public void ClearAll()
    {
        txtPaymentID.Text = "";
        txtChequeNumber.Text = "";
        txtChequeDate.Text = "";
        lblPosteddate.Text = DateTime.Today.ToShortDateString();
        txtPaymentDate.Text = lblPosteddate.Text;
        txtChequeAmount.Text = "";
        txtDescription.Text = "";
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
    }
}
