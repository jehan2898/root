/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_DiagonosisCode.aspx.cs
/*Purpose              :       To Add and Edit Diagonosis Code 
/*Author               :       Sandeep Y
/*Date of creation     :       27 Feb 2000  
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

public partial class Bill_Sys_DiagnosisCode : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DigosisCodeBO _bill_Sys_DigosisCodeBO;
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
            btnSave.Attributes.Add("onclick", "return formValidator('frmtxtDiagonosis','extddlDiagnosisType,txtDiagonosisCode');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmtxtDiagonosis','extddlDiagnosisType,txtDiagonosisCode');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            btnUpdatePreferedList.Attributes.Add("onclick", "return confirm_check();");
            extddlDiagnosisCodeType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                BindGrid();
                btnUpdate.Enabled = false;
            }

            _deleteOpeation = new Bill_Sys_DeleteBO();
            if (_deleteOpeation.checkForDelete(txtCompanyID.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE))
            {
                btnDelete.Visible = false;
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
            cv.MakeReadOnlyPage("Bill_Sys_DiagnosisCode.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

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
            if (chkAddPrefList.Checked == true)
            {
                txtAddToPreferredList.Text = "1";
            }
            else
            {
                txtAddToPreferredList.Text = "0";
            }
            _saveOperation.WebPage = this.Page;
            _saveOperation.Xml_File = "DiagnosisCode.xml";
            _saveOperation.SaveMethod();

            BindGrid();


            lblMsg.Visible = true;
            lblMsg.Text = "Diagonosis Code Saved Successfully ...!";
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
            if (chkAddPrefList.Checked == true)
            {
                txtAddToPreferredList.Text = "1";
            }
            else
            {
                txtAddToPreferredList.Text = "0";
            }
            _editOperation.WebPage = this.Page;
            _editOperation.Xml_File = "DiagnosisCode.xml";
            _editOperation.Primary_Value = Session["DignosisCode"].ToString();
            _editOperation.UpdateMethod();

            extddlDiagnosisCodeType.Text = "NA";
            txtDiagonosisCode.Text = "";
            txtDescription.Text = "";
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
            lblMsg.Visible = false;

            if (Session["PageIndex"] != null && Session["PageIndex"] != "")
            {
                int iIndex = Convert.ToInt32(Session["PageIndex"].ToString());
                grdDiagonosisCode.CurrentPageIndex = iIndex;
            }
            else
            {
                grdDiagonosisCode.CurrentPageIndex = 0;
            }
            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = "Diagonosis Code Updated Successfully ...!";

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
            extddlDiagnosisCodeType.Text = "NA";
            txtDiagonosisCode.Text = "";
            txtDescription.Text = "";
            btnUpdate.Enabled = false;
            btnSave.Enabled = true;
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

    protected void grdDiagonosisCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["PageIndex"] = grdDiagonosisCode.CurrentPageIndex.ToString();
            Session["DignosisCode"] = grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[1].Text;
            if (grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[2].Text != "&nbsp;") { txtDiagonosisCode.Text = grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[2].Text; }
            if (grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[4].Text != "&nbsp;") { txtDescription.Text = grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[4].Text; }
            if (grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[5].Text != "&nbsp;") { extddlDiagnosisCodeType.Text = grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[5].Text; }
            if (grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[6].Text != "&nbsp;")
            {
                if (grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[6].Text.ToLower() == "true")
                {
                    chkAddPrefList.Checked = true;
                }
                else
                {
                    chkAddPrefList.Checked = false;
                }
            }
            if (grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[7].Text != "&nbsp;") { ddlType.SelectedValue = grdDiagonosisCode.Items[grdDiagonosisCode.SelectedIndex].Cells[7].Text; }
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

    protected void grdDiagonosisCode_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDiagonosisCode.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
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

    #endregion

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
            _listOperation.Xml_File = "DiagnosisCode.xml";
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


    #endregion

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfDiagnosisCodes = "";
        try
        {
            for (int i = 0; i < grdDiagonosisCode.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdDiagonosisCode.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_DIAGNOSIS_CODE", "@SZ_DIAGNOSIS_CODE_ID", grdDiagonosisCode.Items[i].Cells[1].Text))
                    {
                        if (szListOfDiagnosisCodes == "")
                        {
                            szListOfDiagnosisCodes = grdDiagonosisCode.Items[i].Cells[2].Text;
                        }
                        else
                        {
                            szListOfDiagnosisCodes = szListOfDiagnosisCodes + " , " + grdDiagonosisCode.Items[i].Cells[2].Text;
                        }
                    }
                }
            }
            if (szListOfDiagnosisCodes != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Diagonosis codes " + szListOfDiagnosisCodes + "  exists.'); ", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Diagnosis codes deleted successfully ...";
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

    protected void btnUpdatePreferedList_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < grdDiagonosisCode.Items.Count; i++)
            {
                CheckBox chkDelete = (CheckBox)grdDiagonosisCode.Items[i].FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    string szDiagonosisCodeID = grdDiagonosisCode.Items[i].Cells[1].Text;
                    arr.Add(szDiagonosisCodeID);
                }
            }
            _bill_Sys_DigosisCodeBO = new Bill_Sys_DigosisCodeBO();
            if (chkAddPrefList.Checked)
            {
                _bill_Sys_DigosisCodeBO.UpdatePreferredListForDiagnosisCode(arr, txtCompanyID.Text, "1");
            }
            else
            {
                _bill_Sys_DigosisCodeBO.UpdatePreferredListForDiagnosisCode(arr, txtCompanyID.Text, "0");
            }

            BindGrid();
            lblMsg.Visible = true;
            lblMsg.Text = "Diagonosis Code Updated Successfully ...!";

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            grdDiagonosisCode.CurrentPageIndex = 0;
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
