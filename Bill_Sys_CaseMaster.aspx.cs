/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_BillStatus.aspx.cs
/*Purpose              :       To Add and Edit Case Master 
/*Author               :       Sandeep Y
/*Date of creation     :       11 Dec 2008  
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

public partial class Bill_Sys_CaseMaster : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            imgbtnDateofAccident.Attributes.Add("onclick", "cal1x.select(document.forms[0].txtDateofAccident,'imgbtnDateofAccident','MM/dd/yyyy'); return false;");
            TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;
            btnSave.Attributes.Add("onclick", "return formValidator('frmCaseMaster','txtCaseName,extddlCaseType,extddlProvider,extddlInsuranceCompany,extddlCaseStatus,extddlAttorney,extddlAdjuster,txtClaimNumber,txtPolicyNumber,txtDateofAccident');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmCaseMaster','txtCaseName,extddlCaseType,extddlProvider,extddlInsuranceCompany,extddlCaseStatus,extddlAttorney,extddlAdjuster,txtClaimNumber,txtPolicyNumber,txtDateofAccident');");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            extddlProvider.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlAttorney.Flag_ID = txtCompanyID.Text.ToString();
            extddlPatient.Flag_ID = txtCompanyID.Text.ToString();
            extddlAdjuster.Flag_ID = txtCompanyID.Text.ToString();
            extddlInsuranceCompany.Flag_ID = txtCompanyID.Text.ToString();
            if (!IsPostBack)
            {
                if (Session["PassedCaseID"] != null)
                {
                    _editOperation = new EditOperation();
                    _editOperation.Xml_File = "CaseMaster.xml";
                    _editOperation.WebPage = this.Page;
                    _editOperation.Primary_Value = Session["PassedCaseID"].ToString();
                    _editOperation.LoadData();
                    Session["CaseID"] = Session["PassedCaseID"].ToString();
                    Session["PassedCaseID"] = null;
                    btnUpdate.Enabled = true;
                    btnSave.Enabled = false;
                }
                else
                {
                    btnUpdate.Enabled = false;
                }
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
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CaseMaster.aspx");
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
            if (chkAssociateCode.Checked == true)
            {
                txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                txtAssociateDiagnosisCode.Text = "0";
            }
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "CaseMaster.xml";
                _saveOperation.SaveMethod();
                ClearControl();
                BindGrid();
                lblMsg.Visible = true;
                lblMsg.Text = "Case Saved Successfully ...!";
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
            if (chkAssociateCode.Checked == true)
            {
                txtAssociateDiagnosisCode.Text = "1";
            }
            else
            {
                txtAssociateDiagnosisCode.Text = "0";
            }
                _editOperation.Primary_Value = Session["CaseID"].ToString();
                _editOperation.WebPage = this.Page;                
                _editOperation.Xml_File = "CaseMaster.xml";
                _editOperation.UpdateMethod();
                //ClearControl();
                BindGrid();
                lblMsg.Visible = true;
                lblMsg.Text = "Case Updated Successfully ...!";
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
            _listOperation.Xml_File = "CaseMaster.xml";
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

    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
          //  txtCaseName.Text = "";
            extddlAttorney.Text = "NA";
            extddlCaseStatus.Text = "NA";
            extddlCaseType.Text = "NA";
            extddlProvider.Text = "NA";
            extddlPatient.Text = "NA";
            extddlInsuranceCompany.Text = "NA";
            txtClaimNumber.Text = "";
            txtPolicyNumber.Text = "";
            txtDateofAccident.Text = "";
            extddlAdjuster.Text = "NA";
            Session["CaseID"] = "";
            chkAssociateCode.Checked = false;
            txtAssociateDiagnosisCode.Text = "";
            btnSave.Enabled = true;
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

    protected void grdCaseMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _editOperation = new EditOperation();
        try
        {
            //_editOperation.WebPage = this.Page;
            //_editOperation.Xml_File = "CaseMaster.xml";    
            //_editOperation.Primary_Value = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[1].Text.ToString();
            //_editOperation.LoadData();

            Session["CaseID"] = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[1].Text;
           // txtCaseName.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[2].Text;
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[3].Text != "&nbsp;") { extddlCaseType.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[3].Text; }
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[5].Text != "&nbsp;") {extddlProvider.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[5].Text;}
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[7].Text != "&nbsp;") {extddlInsuranceCompany.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[7].Text;}
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[9].Text != "&nbsp;") {extddlCaseStatus.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[9].Text;}
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[11].Text != "&nbsp;"){ extddlAttorney.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[11].Text;}
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[15].Text != "&nbsp;") { extddlPatient.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[15].Text; }

            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[3].Text != "&nbsp;") extddlAdjuster.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[17].Text;
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[19].Text != "" && grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[19].Text != "&nbsp;")
            {
                txtClaimNumber.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[19].Text;
            }
            else { txtClaimNumber.Text = ""; }
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[20].Text != "" && grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[20].Text != "&nbsp;")
            {
                txtPolicyNumber.Text = grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[20].Text;
            }
            else { txtPolicyNumber.Text = ""; }
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[21].Text != "" && grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[21].Text != "&nbsp;")
            {
                txtDateofAccident.Text = Convert.ToDateTime(grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[21].Text).ToShortDateString();
            }
            else { txtDateofAccident.Text = ""; }
            if (grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[22].Text != "" && grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[22].Text != "&nbsp;")
            {
                chkAssociateCode.Checked =Convert.ToBoolean(grdCaseMaster.Items[grdCaseMaster.SelectedIndex].Cells[22].Text.ToString());
            }
            else { chkAssociateCode.Checked =false; }
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

    protected void grdCaseMaster_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdCaseMaster.CurrentPageIndex = e.NewPageIndex;
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

    protected void imgbtnCaseType_Click(object sender, ImageClickEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            string szURL = "Bill_Sys_CaseType.aspx";
            Session["Flag"] = "true";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

    protected void imgbtnProvider_Click(object sender, ImageClickEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            string szURL = "Bill_Sys_Provider.aspx";
            Session["Flag"] = "true";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

    protected void imgbtnInsuranceCompany_Click(object sender, ImageClickEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            string szURL = "Bill_Sys_InsuranceCompany.aspx";
            Session["Flag"] = "true";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

    protected void imgbtnCaseStatus_Click(object sender, ImageClickEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            string szURL = "Bill_Sys_CaseStatus.aspx";
            Session["Flag"] = "true";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

    protected void imgbtnAttorney_Click(object sender, ImageClickEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            string szURL = "Bill_Sys_Attorney.aspx";
            Session["Flag"] = "true";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

    protected void imgbtnPatient_Click(object sender, ImageClickEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            string szURL = "Bill_Sys_Patient.aspx";
            Session["Flag"] = "true";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

    protected void imgbtnAdjuster_Click(object sender, ImageClickEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            lblMsg.Visible = false;
            string szURL = "Bill_Sys_Adjuster.aspx";
            Session["Flag"] = "true";
            Response.Write("<script language='javascript'>window.open('" + szURL + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30');</script>");
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

