/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillIC9Code.aspx.cs
/*Purpose              :       To Add and Edit Bill IC9 Code
/*Author               :       Manoj C
/*Date of creation     :       19 Dec 2008  
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

public partial class Bill_Sys_BillIC9Code : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_Menu _bill_Sys_Menu;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; 

           // btnSave.Attributes.Add("onclick", "return formValidator('frmIC9Code','txtBillNo,extddlIC9Code,txtUnit,txtAmount,txtWriteOff,txtDescription');");
            btnSave.Attributes.Add("onclick", "return Amountvalidate();");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmIC9Code','txtBillNo,extddlIC9Code,txtUnit,txtAmount,txtWriteOff,txtDescription');");
           
            txtBillNo.Text = Session["PassedBillID"].ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
         
            if (!IsPostBack)
            {
                
                BindGrid();        
                btnUpdate.Enabled = false;
                //extddlIC9Code.Text = "IC00001";
                //extddlIC9Code.Text = "NA";
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

    #region "Fetch Method"
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
            _listOperation.Xml_File = "BllIC9Code.xml";
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
    private void CalculateAmount(string id)
    {
        string Elmahid = string.Format("ElmahId: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_Menu = new Bill_Sys_Menu();
        decimal amount;
        try
        {
            amount = _bill_Sys_Menu.GetICcodeAmount(id);
            txtAmount.Text = Convert.ToDecimal(amount * Convert.ToDecimal(txtUnit.Text)).ToString();
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + Elmahid + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(Elmahid, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }    
    private void ClearControl()
    {
        txtAmount.Text = "";      
        txtDescription.Text = "";
        txtUnit.Text = "";
        txtWriteOff.Text = "";
        extddlIC9Code.Text = "NA";
        txtTempAmt.Value = "";
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
    }
    #endregion

    #region "Event Hanlder"   

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
            _saveOperation .Xml_File = "BllIC9Code.xml";
            _saveOperation.SaveMethod();          
            BindGrid();
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
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "BllIC9Code.xml";
            _editOperation.Primary_Value = Session["SZ_BILL_IC9_CODE_ID"].ToString();
            _editOperation.UpdateMethod();           
            BindGrid();
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
    protected void grdIC9Code_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            Session["SZ_BILL_IC9_CODE_ID"] = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[1].Text.ToString();
            if (grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[2].Text.ToString() != "&nbsp;") { extddlIC9Code.Text = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[2].Text.ToString(); }
            if (grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[3].Text.ToString() != "&nbsp;") { txtBillNo.Text = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[3].Text.ToString(); }
            if (grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[4].Text.ToString() != "&nbsp;")  {txtUnit.Text = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[4].Text.ToString();}

            if (grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[5].Text.ToString() != "&nbsp;")  {txtAmount.Text = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[5].Text.ToString();}
            if (grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[6].Text.ToString() != "&nbsp;")  {txtWriteOff.Text = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[6].Text.ToString();}
            if (grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[7].Text.ToString() != "&nbsp;")  {txtDescription.Text = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[7].Text.ToString();}
            if (grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[8].Text.ToString() != "&nbsp;") { txtTempAmt.Value = grdIC9Code.Items[grdIC9Code.SelectedIndex].Cells[8].Text.ToString(); }
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
    protected void grdIC9Code_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdIC9Code.CurrentPageIndex = e.NewPageIndex;
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
 
    #endregion




    protected void extddlIC9Code_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _bill_Sys_Menu = new Bill_Sys_Menu();
        try
        {
            if (txtUnit.Text != "")
            {
                CalculateAmount(extddlIC9Code.Text.ToString());
            }
            else
            {
                txtAmount.Text = _bill_Sys_Menu.GetICcodeAmount(extddlIC9Code.Text.ToString()).ToString();
                txtTempAmt.Value = _bill_Sys_Menu.GetICcodeAmount(extddlIC9Code.Text.ToString()).ToString();
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
}

