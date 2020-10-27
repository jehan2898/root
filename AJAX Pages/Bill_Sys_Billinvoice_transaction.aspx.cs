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

public partial class AJAX_Pages_Bill_Sys_Billinvoice_transaction : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtmncost.Text = txttransactioncost.Value;
        this.con.SourceGrid = grdInvoiceTransaction;
        this.txtSearchBox.SourceGrid = grdInvoiceTransaction;
        this.grdInvoiceTransaction.Page = this.Page;
        this.grdInvoiceTransaction.PageNumberList = this.con;
        btndelete.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        if (!IsPostBack)
        {
            btninvoiceupdate.Enabled = false;
            ClearControl();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            grdInvoiceTransaction.XGridBindSearch();

        }
        

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btninvoiceupdate.Enabled = false;
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            _Bill_Sys_invoice.saveinvoicetransaction(txtCompanyID.Text, txttransactionname.Text, txttransactioncost.Value,ddltype.SelectedItem.ToString());
            ClearControl();
            grdInvoiceTransaction.XGridBindSearch();
            usrMessage.PutMessage("Invoice Transaction Save successfully");
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
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdInvoiceTransaction.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);


    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string sztransid = txtinvoicetransid.Text;
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            _Bill_Sys_invoice.UpdateinvoiceTransaction(txtCompanyID.Text, sztransid, txttransactionname.Text, txttransactioncost.Value, ddltype.SelectedItem.ToString());
            ClearControl();
            grdInvoiceTransaction.XGridBindSearch();
            usrMessage.PutMessage("Invoice Transaction Update successfully");
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
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szinvoicetransid;
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            for (int i = 0; i < grdInvoiceTransaction.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdInvoiceTransaction.Rows[i].FindControl("ChkDelete");
                if (chk.Checked == true)
                {
                    szinvoicetransid = grdInvoiceTransaction.DataKeys[i]["INVOICE_TRANS_ID"].ToString();
                    _Bill_Sys_invoice.deleteinvoiceTransaction(txtCompanyID.Text, szinvoicetransid);
                }

            }
            btninvoiceupdate.Enabled = false;
            ClearControl();
            grdInvoiceTransaction.XGridBindSearch();
            usrMessage.PutMessage("Invoice Transaction Deleted successfully");
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
    

    protected void grdInvoiceTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            try
            {
                if (grdInvoiceTransaction.DataKeys[index]["INVOICE_DESC"].ToString() != "&nbsp;") 
                {
                    txttransactionname.Text = grdInvoiceTransaction.DataKeys[index]["INVOICE_DESC"].ToString(); 
                }
                else
                {
                    txttransactionname.Text = ""; 
                }
                if (grdInvoiceTransaction.DataKeys[index]["INVOICE_COST"].ToString() != "&nbsp;")
                {
                    txttransactioncost.Attributes.Add("value", Convert.ToDouble(grdInvoiceTransaction.DataKeys[index]["INVOICE_COST"]).ToString());
                   
                   
                }
                else
                {
                    txttransactioncost.Attributes.Add("value", "");
                    
                }
              
                if (grdInvoiceTransaction.DataKeys[index]["sz_company_id"].ToString() != "&nbsp;")
                {
                    txtCompanyID.Text = grdInvoiceTransaction.DataKeys[index]["sz_company_id"].ToString();
                }
                else
                {
                    txtCompanyID.Text = "";
                }
                if (grdInvoiceTransaction.DataKeys[index]["INVOICE_TRANS_ID"].ToString() != "&nbsp;")
                {
                    txtinvoicetransid.Text = grdInvoiceTransaction.DataKeys[index]["INVOICE_TRANS_ID"].ToString();
                }
                else
                {
                    txtinvoicetransid.Text = "";
                }

                    if (grdInvoiceTransaction.DataKeys[index]["TYPE"].ToString() != "&nbsp;")
                    {
                        if (grdInvoiceTransaction.DataKeys[index]["TYPE"].ToString() == "Storage")
                       {

                           ddltype.Text = "1";
                       }
                       if (grdInvoiceTransaction.DataKeys[index]["TYPE"].ToString() == "Software")
                       {

                           ddltype.Text = "0";
                       }
                          
                    }
                    else
                    {
                        ddltype.Text = "";
                    }

                btnSave.Enabled = false;
                btninvoiceupdate.Enabled = true;
                
                
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

    protected void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txttransactionname.Text = "";
            txttransactioncost.Attributes.Add("value", "");
            txtmncost.Text = "";
            ddltype.SelectedIndex = 0;
            btnSave.Enabled = true;
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
