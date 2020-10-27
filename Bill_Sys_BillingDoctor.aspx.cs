/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_Doctor.aspx.cs
/*Purpose              :       To Add and Edit Doctor
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

public partial class Bill_Sys_BillingDoctor : PageBase
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
            //TreeMenuControl1.ROLE_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE; ;

            btnSave.Attributes.Add("onclick", "return formValidator('frmDoctor','txtDoctorName,extddlDoctorType,extddlOffice');");
            btnUpdate.Attributes.Add("onclick", "return formValidator('frmDoctor','txtDoctorName,extddlDoctorType,extddlOffice');");            
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            extddlDoctorType.Flag_ID = txtCompanyID.Text;
            extddlProvider.Flag_ID = txtCompanyID.Text;
            extddlOffice.Flag_ID = txtCompanyID.Text;
            extddlBillingOffice.Flag_ID = txtCompanyID.Text;
            if (!IsPostBack)
            {
                if (Request.QueryString["ProviderId"] != null)
                {
                    extddlProvider.Text = Request.QueryString["ProviderId"].ToString().Trim();
                    extddlProvider.Enabled = false;
                }
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
            cv.MakeReadOnlyPage("Bill_Sys_BillingDoctor.aspx");
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
          
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "BillingDoctor.xml";
                _saveOperation.SaveMethod();
                BindGrid();
                ClearControl();
                lblMsg.Visible = true;
                lblMsg.Text = "Billing Doctor Saved Successfully ...!";
            
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
       
                _editOperation.Primary_Value = Session["DoctorID"].ToString();
                _editOperation.WebPage = this.Page;
                _editOperation.Xml_File = "BillingDoctor.xml";
                _editOperation.UpdateMethod();
                BindGrid();
                lblMsg.Visible = true;
                lblMsg.Text = "Billing Doctor Updated Successfully ...!";
           
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
            _listOperation.Xml_File = "BillingDoctor.xml";
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

    protected void grdDoctorNameList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Session["DoctorID"] = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[1].Text;
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[2].Text != "&nbsp;") { txtDoctorName.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[2].Text; } else { txtDoctorName.Text = ""; }
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[4].Text != "&nbsp;") { extddlDoctorType.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[4].Text; } else { extddlDoctorType.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[5].Text != "&nbsp;") {txtLicenseNumber.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[5].Text;}else { txtLicenseNumber.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[6].Text != "&nbsp;") { extddlProvider.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[6].Text;}else { extddlProvider.Text="NA";}

            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[14].Text != "&nbsp;") { txtWCBAuth.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[14].Text; }else { txtWCBAuth.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[15].Text != "&nbsp;") { txtWCBRatingCode.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[15].Text; }else { txtWCBRatingCode.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[16].Text != "&nbsp;") { txtOfficeAdd.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[16].Text; }else { txtOfficeAdd.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[17].Text != "&nbsp;") { txtOfficeCity.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[17].Text; }else { txtOfficeCity.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[18].Text != "&nbsp;") { txtOfficeState.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[18].Text; }else { txtOfficeState.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[19].Text != "&nbsp;") { txtOfficeZip.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[19].Text; }else { txtOfficeZip.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[20].Text != "&nbsp;") { txtOfficePhone.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[20].Text; }else { txtOfficePhone.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[21].Text != "&nbsp;") { txtBillingAdd.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[21].Text; }else { txtBillingAdd.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[22].Text != "&nbsp;") { txtBillingCity.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[22].Text; }else { txtBillingCity.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[23].Text != "&nbsp;") { txtBillingState.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[23].Text; }else { txtBillingState.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[24].Text != "&nbsp;") { txtBillingZip.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[24].Text; }else { txtBillingZip.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[25].Text != "&nbsp;") { txtBillingPhone.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[25].Text; }else { txtBillingPhone.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[26].Text != "&nbsp;") { txtNPI.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[26].Text; }else { txtNPI.Text="";}
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[27].Text != "&nbsp;") { txtFederalTax.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[27].Text; } else { txtFederalTax.Text = ""; }
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[28].Text == "False")
            {                
                chklstTaxType.Items[0].Selected = true;
                chklstTaxType.Items[1].Selected = false;
            }
            else
            {
                chklstTaxType.Items[1].Selected = true;
                chklstTaxType.Items[0].Selected = false;
            }
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[29].Text != "&nbsp;") { txtKOEL.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[29].Text; }
            else { txtKOEL.Text = ""; }

            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[30].Text != "&nbsp;") { extddlOffice.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[30].Text; }
            else { extddlOffice.Text = "NA"; }


            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[32].Text != "&nbsp;") { txtAssignNumber.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[32].Text; } else { txtAssignNumber.Text = ""; }

            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[33].Text != "&nbsp;") { extddlBillingState.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[33].Text; } else { extddlBillingState.Text = "NA"; }
            if (grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[34].Text != "&nbsp;") { extddlBillingOffice.Text = grdDoctorNameList.Items[grdDoctorNameList.SelectedIndex].Cells[34].Text; } else { extddlBillingOffice.Text = "NA"; }


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

    protected void grdDoctorNameList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

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

    protected void grdDoctorNameList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdDoctorNameList.CurrentPageIndex = e.NewPageIndex;
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

    protected void grdDoctorNameList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "AddDiagnosis")
            {
                Session["DoctorTypeID"] = e.Item.Cells[4].Text;
                Session["DoctorType"] = e.Item.Cells[3].Text;
                Response.Redirect("Bill_Sys_DiagnosisCode.aspx", false);
            }
            if (e.CommandName == "AddProcedure")
            {
                Session["DoctorID"] = e.Item.Cells[1].Text;
                Response.Redirect("Bill_Sys_DoctorProcedureCode.aspx", false);
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
    
    private void ClearControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            txtDoctorName.Text = "";
            extddlDoctorType.Text = "NA";
            txtLicenseNumber.Text = "";
            extddlProvider.Text = "NA";
            extddlBillingState.Text = "NA";
            txtBillingAdd.Text = "";
            txtBillingCity.Text = "";
            txtBillingPhone.Text = "";
            txtBillingState.Text = "";
            txtBillingZip.Text = "";
            txtFederalTax.Text = "";
            txtNPI.Text = "";
            txtOfficeAdd.Text = "";
            txtOfficeCity.Text = "";
            txtOfficePhone.Text = "";
            txtOfficeState.Text = "";
            txtOfficeZip.Text = "";
            txtWCBAuth.Text = "";
            txtWCBRatingCode.Text = "";
            txtKOEL.Text = "";
            chklstTaxType.Items[0].Selected = false;
            chklstTaxType.Items[1].Selected = false;
            extddlOffice.Text = "NA";
            extddlBillingOffice.Text = "NA";
            txtAssignNumber.Text = "";
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
        _deleteOpeation = new Bill_Sys_DeleteBO();
        String szListOfDoctor = "";
        try
        {
            for (int i = 0; i < grdDoctorNameList.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdDoctorNameList.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {
                    if (!_deleteOpeation.deleteRecord("SP_MST_BILLING_DOCTOR", "@SZ_DOCTOR_ID", grdDoctorNameList.Items[i].Cells[1].Text))
                    {
                        if (szListOfDoctor == "")
                        {
                            szListOfDoctor = grdDoctorNameList.Items[i].Cells[2].Text;
                        }
                        else
                        {
                            szListOfDoctor = szListOfDoctor + " , " + grdDoctorNameList.Items[i].Cells[2].Text;
                        }
                    }
                }
            }
            if (szListOfDoctor != "")
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Doctor " + szListOfDoctor + "  exists.'); ", true);
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Doctor deleted successfully ...";
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
}

