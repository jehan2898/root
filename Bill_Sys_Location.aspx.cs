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
public partial class Bill_Sys_Location : PageBase
{
    private SaveOperation _saveOperation;
    private ListOperation _listOperation;
    private EditOperation _editOperation;
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
            // TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;
            btnSave.Attributes.Add("onclick", "return formValidator('frmlocation','txtLocation');");



            btnUpdate.Attributes.Add("onclick", "return formValidator('frmlocation','txtLocation');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            ///btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            if (!IsPostBack)
            {

                BindGrid();
                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION == "1")
                {
                    if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_PROVIDER_DISPLAY_NAME != "1")
                    {

                        lblDisPlayName.Visible = false;
                        lblShowDisPlayName.Visible = false;
                        txtDisPlayName.Visible = false;
                        chkDispayName.Visible = false;
                    }
                    else
                    {
                        lblDisPlayName.Visible = true;
                        lblShowDisPlayName.Visible = true;
                        txtDisPlayName.Visible = true;
                        chkDispayName.Visible = true;
                    }
                }
                else
                {
                    lblDisPlayName.Visible = false;
                    lblShowDisPlayName.Visible = false;
                    txtDisPlayName.Visible = false;
                    chkDispayName.Visible = false;
                }
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
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_Location.aspx");
        }
        #endregion
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
            if (chkDispayName.Checked)
            {
                txtShowDisplayName.Text = "1";

            }
            else
            {
                txtShowDisplayName.Text = "0";

            }
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "Location.xml";
            _saveOperation.SaveMethod();
            BindGrid();
            ClearControl();
            lblMsg.Visible = true;
           
            lblMsg.Text = "Loction Saved Successfully...!";
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
            if (chkDispayName.Checked)
            {
                txtShowDisplayName.Text = "1";

            }
            else
            {
                txtShowDisplayName.Text = "0";

            }
            if (Session["LocationID"].ToString() != "")
            {
                _editOperation.Primary_Value = Session["LocationID"].ToString();
                _editOperation.WebPage = this.Page;

                //_editOperation.Primary_Value = Convert.ToInt32(txtSchemeID.Text);
                _editOperation.Xml_File = "Location.xml";
                _editOperation.UpdateMethod();
                BindGrid();
                lblMsg.Visible = true;
             
                lblMsg.Text = "Location Updated Successfully...!";
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szLocation = "";
        try
        {
            for (int i = 0; i < grdLocation.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdLocation.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_LOCATION", "@SZ_LOCATION_ID", grdLocation.Items[i].Cells[1].Text))
                    {
                        if (szLocation == "")
                        {
                            szLocation = grdLocation.Items[i].Cells[2].Text;
                        }
                        else
                        {
                            szLocation = szLocation + " , " + grdLocation.Items[i].Cells[2].Text;
                        }
                    }
                }
            }
            if (szLocation != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Location " + szLocation + "  exists.'); ", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Location deleted successfully ...";
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
            _listOperation.Xml_File = "Location.xml";
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
    protected void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            txtLocation.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            txtDisPlayName.Text = "";
            extddlOfficeState.Text = "";
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            lblMsg.Visible = false;
            txtDisPlayName.Text = "";
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
    protected void grdLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["LocationID"] = grdLocation.Items[grdLocation.SelectedIndex].Cells[1].Text;
            if (grdLocation.Items[grdLocation.SelectedIndex].Cells[2].Text != "&nbsp;")
            {
                txtLocation.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[2].Text;
                //txtAddress.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[3].Text;
                //txtCity.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[4].Text;
                // extddlOfficeState.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[5].Text;
                // txtZip.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[6].Text;

            }

            if (grdLocation.Items[grdLocation.SelectedIndex].Cells[3].Text != "&nbsp;")
            {
                txtAddress.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[3].Text;

            }
            else
            {
                txtAddress.Text = "";
            }
            if (grdLocation.Items[grdLocation.SelectedIndex].Cells[4].Text != "&nbsp;")
            {
                txtCity.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[4].Text;
            }
            else
            {
                txtCity.Text = "";
            }

            if (grdLocation.Items[grdLocation.SelectedIndex].Cells[5].Text != "&nbsp;")
            {
                extddlOfficeState.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[5].Text;
            }
            else
            {
                extddlOfficeState.Text = "";
            }

            if (grdLocation.Items[grdLocation.SelectedIndex].Cells[6].Text != "&nbsp;")
            {

                txtZip.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[6].Text;
            }
            else
            {
                txtZip.Text = "";
            }



            if (txtDisPlayName.Visible)
            {
                    if (grdLocation.Items[grdLocation.SelectedIndex].Cells[7].Text != "&nbsp;")
                    {

                        txtDisPlayName.Text = grdLocation.Items[grdLocation.SelectedIndex].Cells[7].Text;
                    }
                    else
                    {
                        txtDisPlayName.Text = "";
                    }
            }
            if (chkDispayName.Visible)
            {
                if (grdLocation.Items[grdLocation.SelectedIndex].Cells[8].Text != "&nbsp;")
                {
                    if (grdLocation.Items[grdLocation.SelectedIndex].Cells[8].Text.ToLower() == "true")
                    {
                        chkDispayName.Checked = true;
                    }
                    else
                    {
                        chkDispayName.Checked = false;
                    }
                }
                else
                {
                    chkDispayName.Checked = false;
                }
            }





            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
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
    protected void grdLocation_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdLocation.CurrentPageIndex = e.NewPageIndex;
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
