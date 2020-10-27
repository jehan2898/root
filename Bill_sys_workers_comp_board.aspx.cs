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

public partial class Bill_sys_workers_comp_board : PageBase
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
            btnSave.Attributes.Add("onclick", "return formValidator('frmworkcomp','txtAdd,txtCity,txtpincode,extddlstate');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmworkcomp','txtAdd,txtCity,txtpincode,extddlstate');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            lblMsg.Visible = false;
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
            base.Response.Redirect(".Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            _saveOperation.Xml_File = "workers_comp_board.xml";
            _saveOperation.SaveMethod();
            BindGrid();
            ClearControl();
            lblMsg.Visible = true;
            lblMsg.Text = "Work Comp Saved Successfully ...!";
           
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
            _editOperation.Primary_Value = Session["WorkCompID"].ToString();
            _editOperation.WebPage = this.Page;

            //_editOperation.Primary_Value = Convert.ToInt32(txtSchemeID.Text);
            _editOperation.Xml_File = "workers_comp_board.xml";
            _editOperation.UpdateMethod();
            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = "Work Comp Updated Successfully ...!";

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
        String szListOfCaseType = "";
        bool _flag = false;
        try
        {
            for (int i = 0; i < grdWorkComp.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdWorkComp.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {

                    _deleteOpeation.deleteRecord("SP_MST_WORKER_COMP_BOARD", "@I_WORK_COMP_ID", grdWorkComp.Items[i].Cells[1].Text);
                    _flag = true;
                }
                //else
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script language='javascript'>  alert('Please select the record from grid !!'); </script>");
                //}
            }
            if (_flag == true)
            {
                BindGrid();
                lblMsg.Visible = true;
                lblMsg.Text = "Work Comp Deleted Successfully ...!";
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script language='javascript'>  alert('Please select the record from grid !!'); </script>");
            }

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
            _listOperation.Xml_File = "workers_comp_board.xml";
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
    protected void grdWorkComp_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["WorkCompID"] = grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[1].Text;
            if (grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[2].Text != "&nbsp;") { txtAdd.Text = grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[2].Text; }
            if (grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[3].Text != "&nbsp;") { txtCity.Text = grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[3].Text; }
            if (grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[6].Text != "&nbsp;") { extddlstate.Text = Convert.ToString(grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[6].Text); }
            else
            {
                extddlstate.Text = "NA";
            }
            if (grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[5].Text != "&nbsp;") { txtpincode.Text = grdWorkComp.Items[grdWorkComp.SelectedIndex].Cells[5].Text; }
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

    private void ClearControl()
    {
        txtAdd.Text = "";
        txtCity.Text = "";
        txtpincode.Text = "";
        extddlstate.Text = "NA";
        btnSave.Enabled = true;
        btnUpdate.Enabled = false;
        

    }
}
