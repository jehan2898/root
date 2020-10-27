/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_InvoiceItem.aspx.cs
/*Purpose              :       To Add , Edit ,Delete Invoice Item
/*Author               :       Tushar S
/*Date of creation     :       12/07/2010  
/*Modified By          :
/*Modified Date        :
/************************************************************/
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



public partial class Bill_Sys_InvoiceItem : PageBase
{
     private SaveOperation _saveOperation;
     private EditOperation _editOperation;
     private ListOperation _listOperation;
     private Bill_Sys_DeleteBO _deleteOpeation;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
          
           txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
           txtCreatedUserID .Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;

           txtCreatedDate.Text = DateTime.Today.ToString("MM/dd/yyyy");

          // btnSave.Attributes.Add("onclick","return formValidator('frmInvoiceitem','txtItemName,txtItemPrice');");
           btnSave.Attributes.Add("onclick", "return Validation();");
           btnUpdate.Attributes.Add("onclick", "return Validation();");
         
          //btnDelete.Attributes.Add("onclick","return ConfirmDelete();");
          btnDelete.Attributes.Add("onclick", "return Validate('_ctl0_ContentPlaceHolder1_btnDelete')");
           if (!IsPostBack)
            
            {

                BindGrid();
                btnUpdate.Enabled = false;
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

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _listOperation = new ListOperation ();
        try
        {
            
            _listOperation.WebPage = this.Page;
            _listOperation.Xml_File = "InvoiceItem.xml";
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
          
             _saveOperation.WebPage = this.Page;
             _saveOperation.Xml_File = "InvoiceItem.xml";
             _saveOperation.SaveMethod();
             BindGrid();
             
             usrMessage.PutMessage("Item Saved Successfully!");
               ClearControl();
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
  
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            if (Page.IsValid)
            {
              
                txtInvoiceId.Visible = true;
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "InvoiceItem.xml";

                _editOperation.Primary_Value = txtInvoiceId.Text;
                _editOperation.UpdateMethod();
                BindGrid();
                  ClearControl();
                txtInvoiceId.Visible = false;
                usrMessage.PutMessage("Item Updated Successfully!");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
             
             
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

    protected void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtItemName.Text = "";
            txtItemPrice.Text = "";
            btnUpdate.Enabled = false;
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
        try
        {
            _deleteOpeation = new Bill_Sys_DeleteBO();
            string szListOfOperation = "";
             for (int i = 0; i < grdInvoiceItem.Items.Count; i++)
                {
                  CheckBox chkDelete1 = (CheckBox)grdInvoiceItem.Items[i].FindControl("chkSelect");
                     if (chkDelete1.Checked)
                    {
                        if (!_deleteOpeation.deleteRecord("SP_MST_INVOICE_ITEM", "@I_INVOICE_ITEM_ID1", grdInvoiceItem.Items[i].Cells[1].Text))
                      {
                          if (szListOfOperation == "")
                          {
                              szListOfOperation = grdInvoiceItem.Items[i].Cells[2].Text;
                          }
                          else
                          {
                              szListOfOperation = szListOfOperation + " , " + grdInvoiceItem.Items[i].Cells[2].Text;
                          }
                    }
                 }   
              }

              if (szListOfOperation != "")
              {
               

                  usrMessage.PutMessage("Item Deleted Successfully!");
                  usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_SystemMessage );
                  usrMessage.Show(); ClearControl();
              }
              else
              {
                  usrMessage.PutMessage("Item Deleted Successfully!");
                  usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage );
                  usrMessage.Show(); ClearControl();

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
    protected void grdInvoiceItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
       {   //Column 1 
           if (grdInvoiceItem.Items[grdInvoiceItem.SelectedIndex].Cells[1].Text != "&nbsp") 
           { 
               txtInvoiceId.Text = grdInvoiceItem.Items[grdInvoiceItem.SelectedIndex].Cells[1].Text; 
           }
           else { txtInvoiceId .Text = ""; }
            
           //Column 2
           if (grdInvoiceItem.Items[grdInvoiceItem.SelectedIndex].Cells[2].Text != "&nbsp") 
           { 
               txtItemName.Text = grdInvoiceItem.Items[grdInvoiceItem.SelectedIndex].Cells[2].Text; 
           }
            else { txtItemName.Text = ""; }
              //Column 3
           if (grdInvoiceItem.Items[grdInvoiceItem.SelectedIndex].Cells[3].Text.ToString() != "&nbsp;") 
           { 
               txtItemPrice.Text = grdInvoiceItem.Items[grdInvoiceItem.SelectedIndex].Cells[3].Text;
           } 
            else { txtItemPrice.Text = ""; }
            btnSave.Enabled = false;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }
    protected void grdInvoiceItem_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            grdInvoiceItem.CurrentPageIndex = e.NewPageIndex;
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
    
}
