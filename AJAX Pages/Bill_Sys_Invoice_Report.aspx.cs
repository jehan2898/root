
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
using PDFValueReplacement;
using MergeDocumentNodes;
using MergeTIFFANDPDF;
using ExtendedDropDownList;
using System.Text;
using System.IO;
using RequiredDocuments;
using log4net;
using System.Data.SqlClient;
using System.Drawing;

public partial class InvoiceReport : PageBase
{
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    InvoiceDAO _InvoiceDAO = new InvoiceDAO();
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;    
    Bill_Sys_DeleteBO _deleteOperation;
    DataSet DS;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            extddlProviderName.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.con.SourceGrid = grdInvoiceReport;
            this.txtSearchBox.SourceGrid = grdInvoiceReport;
            this.grdInvoiceReport.Page = this.Page;
            this.grdInvoiceReport.PageNumberList = this.con;

            btnDelete.Attributes.Add("onclick", "return Validate()");

            if (!IsPostBack)
            {
                if (Request.QueryString["fromCase"] != "False")
                {
                    txtCaseNumber.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                }
                grdInvoiceReport.XGridBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (txtToAmt.Visible == true)
            {
                txtRange.Text = txtToAmt.Text;
            }
            else 
            {
                txtRange.Text = ""; 
            }
            grdInvoiceReport.XGridBind();           
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
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _deleteOpeation = new Bill_Sys_DeleteBO();
            string szListOfOperation = "";
            for (int i = 0; i < grdInvoiceReport.Rows.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdInvoiceReport.Rows[i].FindControl("chkSelect");
                if (chkDelete1.Checked)
                {
                    LinkButton lnkInvoiceID = (LinkButton)grdInvoiceReport.Rows[i].FindControl("lnkSelectCase");
                    _InvoiceDAO.DeleteInvoiceRecord(lnkInvoiceID.Text, txtCompanyId.Text);

                }
            }
            grdInvoiceReport.XGridBind();

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
  
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.location.href ='" + ApplicationSettings.GetParameterValue( "FETCHEXCEL_SHEET") + grdInvoiceReport.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);

    }
   
    protected void grdInvoiceReport_RowCommand(object sender, GridViewCommandEventArgs e)
     {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
         {                                    
             if (e.CommandName == "InvoiceId")
             {
                 int index = Convert.ToInt32(e.CommandArgument);
                 LinkButton lnkInvoiceId = new LinkButton();
                 lnkInvoiceId = (LinkButton)grdInvoiceReport.Rows[index].FindControl("lnkSelectCase");
                 string InvoiceId = lnkInvoiceId.Text;
                 string Case_Id = grdInvoiceReport.DataKeys[index][0].ToString();
                 ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "OpenInvoiceWiseItem(" + InvoiceId + "," + Case_Id + ");", true);                 
             }
             if (e.CommandName == "Generate Invoice")
             {
                 #region "Logic to view bills"                 
                 string InvoiceId = e.CommandArgument.ToString();
                 _InvoiceDAO = new InvoiceDAO();
                 string PDF_Path;
                 PDF_Path = _InvoiceDAO.GetPDFPath(InvoiceId,txtCompanyId.Text);
                 
                 if (PDF_Path.ToString()!="")
                 {
                     //PDF_Path = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + PDF_Path;
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + PDF_Path + "');", true);
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "alert('No Invoice Generated ...!');", true);
                 }

                 #endregion
             }

             if (e.CommandName == "Make Payment")
             {
                 string InvoiceId = e.CommandArgument.ToString();
                 lblPosteddate.Text = DateTime.Today.ToShortDateString();
                 txtPaymentDate.Text = lblPosteddate.Text;
                 lblInvoiceId.Text = InvoiceId.ToString();
                 btnUpdate.Enabled = false;
                 btnSave.Enabled = true;
                 txtCompanyID1.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                 txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
                 txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                 BindGrid();
                 ModalPopupExtender1.Show();
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

    protected void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (ddlAmount.SelectedValue == "Range")
            {
                txtToAmt.Visible = true;
                lblfrom.Visible = true;
                lblTo.Visible = true;               
            }
            else
            {
                txtToAmt.Visible = false;
                lblfrom.Visible = false;
                lblTo.Visible = false;

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



    //Make Payment PopUp Functions

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearAll();
        BindGrid();
        ModalPopupExtender1.Show();
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
                txtCaseID.Text = txtCaseID.Text;
                ArrayList objarr = new ArrayList();
                objarr.Add(txtPaymentID.Text);
                objarr.Add(txtCaseID.Text);
                objarr.Add(txtChequeNumber.Text);
                objarr.Add(txtChequeDate.Text);
                objarr.Add(txtChequeAmount.Text);
                objarr.Add(txtCompanyID1.Text);
                objarr.Add(txtUserID.Text);
                objarr.Add(txtDescription.Text);
                objarr.Add(lblInvoiceId.Text);
                objarr.Add("ADD");
                _InvoiceDAO.SavePayment(objarr);
                //_saveOperation = new SaveOperation();
                //_saveOperation.WebPage = this.Page;
                //_saveOperation.Xml_File = "MiscPaymentTransaction.xml";
                //_saveOperation.SaveMethod();
                BindGrid();
                
                ModalPopupExtender1.Show();

                usrMessage1.PutMessage("Payment added successfully..!");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage1.Show();
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

        if (Page.IsValid)
        {
            try
            {
                txtUserID.Text = txtUserID.Text;
                txtDescription.Text = txtDescription.Text.Trim();

                ArrayList objarr = new ArrayList();
                objarr.Add(txtPaymentID.Text);
                objarr.Add(txtCaseID.Text);
                objarr.Add(txtChequeNumber.Text);
                objarr.Add(txtChequeDate.Text);
                objarr.Add(txtChequeAmount.Text);
                objarr.Add(txtCompanyID1.Text);
                objarr.Add(txtUserID.Text);
                objarr.Add(txtDescription.Text);
                objarr.Add(lblInvoiceId.Text);
                objarr.Add("UPDATE");
                _InvoiceDAO.SavePayment(objarr);


                //_editOperation = new EditOperation();
                //_editOperation.WebPage = this.Page;
                //_editOperation.Primary_Value = txtPaymentID.Text;
                //_editOperation.Xml_File = "MiscPaymentTransaction.xml";
                //_editOperation.UpdateMethod();

                BindGrid();
                ModalPopupExtender1.Show();
                usrMessage1.PutMessage("Selected payment update successfully..!");
                usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage1.Show();
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

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAll();
    }        

    protected void btnPaymentDelete_Click(object sender, EventArgs e)
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

                        usrMessage1.PutMessage("Selected Payment deleted successfully .....!");
                        usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage1.Show();
                        _InvoiceDAO.UpdatePayment(txtCompanyID1.Text, lblInvoiceId.Text);
                    }

                }
            }
            
            BindGrid();
            ModalPopupExtender1.Show();
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
            DataSet ds = new DataSet();
            DS = new DataSet();
            ds = _InvoiceDAO.GetPayment(txtCaseID.Text, txtCompanyID1.Text,lblInvoiceId.Text);
            grdPaymentTransaction.DataSource = ds;
            grdPaymentTransaction.DataBind();
            DS = _InvoiceDAO.GetBalance(txtCompanyID1.Text, lblInvoiceId.Text);
            decimal Balance =Convert.ToDecimal(DS.Tables[0].Rows[0][0].ToString());
            lblBalance.Text = string.Format("{0:C}",Balance);
            //_listOperation.WebPage = this.Page;
            //_listOperation.Xml_File = "MiscPaymentTransaction.xml";
            //_listOperation.LoadList();
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
        ClearAll();

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPaymentTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtChequeNumber.Text = e.Item.Cells[2].Text;
            txtPaymentDate.Text = e.Item.Cells[3].Text;
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
            ModalPopupExtender1.Show();
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
        txtCaseNumber.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
        grdInvoiceReport.XGridBind();
    }

    //End Make Payment PopUp Functions
}