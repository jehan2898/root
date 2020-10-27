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


public partial class Bill_Sys_Transpotation : PageBase
{
    private SaveOperation _saveOperation;   
    private EditOperation _editOperation;
    private ListOperation _listOperation;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        btnSave.Attributes.Add("onclick", "return formValidator('frmAttorney','txtName');");
        btnUpdate.Attributes.Add("onclick", "return formValidator('frmAttorney','txtName');");
        
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
       
        btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
        if(!IsPostBack)
        {
            BindGrid();
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
            if (Page.IsValid)
            {
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "Transpotation.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                ClearControl();
                lblMsg.Visible = true;
                lblMsg.Text = "Record Saved Successfully...!";
                
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
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "Transpotation.xml";
                _editOperation.Primary_Value = txtTraspotationID.Text;
                _editOperation.UpdateMethod();
                lblMsg.Visible = true;
                lblMsg.Text = "Record Updated Successfully...!";
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
            _listOperation.Xml_File = "Transpotation.xml";
            _listOperation.LoadList();
            btnSave.Enabled =true;
            btnUpdate.Enabled = false;
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
            txtName.Text = "";
            txtCity.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtEmailID.Text = "";
            txtFax.Text = "";
            extddlState.Text = "NA";
            txtZip.Text = "";
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            lblMsg.Visible = false;
            

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
        Bill_Sys_DeleteBO _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfAttorney = "";
        try
        {
            for (int i = 0; i < grdTranspotaion.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdTranspotaion.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_TRANSPOTATION", "@I_TARNSPOTATION_ID", grdTranspotaion.Items[i].Cells[1].Text))
                    {
                        if (szListOfAttorney == "")
                        {
                            szListOfAttorney = grdTranspotaion.Items[i].Cells[2].Text;
                        }
                        else
                        {
                            szListOfAttorney = "," + grdTranspotaion.Items[i].Cells[2].Text;
                        }
                    }
                }
            }
            if (szListOfAttorney != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for "+ szListOfAttorney + " company is exists.'); ", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Record deleted successfully ...";
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

    protected void grdTranspotaion_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTraspotationID.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[1].Text.ToString();
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[2].Text.ToString() != "&nbsp;") { txtName.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[2].Text; } else { txtName.Text = ""; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[3].Text.ToString() != "&nbsp;") { txtAddress.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[3].Text; } else { txtAddress.Text = ""; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[4].Text.ToString() != "&nbsp;") { txtCity.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[4].Text; } else { txtCity.Text = ""; }

        //if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[5].Text.ToString() != "&nbsp;") { txtAddress.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[5].Text; } else { txtAddress.Text = ""; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[6].Text.ToString() != "&nbsp;") { extddlState.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[6].Text; } else { extddlState.Text = "NA"; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[7].Text.ToString() != "&nbsp;") { txtZip.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[7].Text; } else { txtZip.Text = ""; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[8].Text.ToString() != "&nbsp;") { txtPhoneNo.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[8].Text; } else { txtPhoneNo.Text = ""; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[9].Text.ToString() != "&nbsp;") { txtEmailID.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[9].Text; } else { txtEmailID.Text = ""; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[10].Text.ToString() != "&nbsp;") { txtFax.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[10].Text; } else { txtFax.Text = ""; }
        if (grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[11].Text.ToString() != "&nbsp;") { txtCompanyID.Text = grdTranspotaion.Items[grdTranspotaion.SelectedIndex].Cells[11].Text; } else { txtCompanyID.Text = ""; }
        btnSave.Enabled = false;
        btnUpdate.Enabled = true;

    }
    protected void grdTranspotaion_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

    }
}
