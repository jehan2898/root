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
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_Excluding_Billing : PageBase
{
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
            btnDelete.Attributes.Add("onclick", "return callfordelete();");
            btnsave.Attributes.Add("onclick", "return val_CheckControls();");
            btnUpdate.Attributes.Add("onclick", "return val_CheckControls();");
            if (!IsPostBack)
            {
                DataSet dsReadingddlDoctor = new DataSet();
                Bill_Sys_Billing_Provider objDoc = new Bill_Sys_Billing_Provider();
                dsReadingddlDoctor = objDoc.GetReadingDoctorList(txtCompanyID.Text);
                ddlReadingddlDoctor.DataSource = dsReadingddlDoctor;
                ListEditItem itm1 = new ListEditItem("--Select--", null);
                ddlReadingddlDoctor.DataBind();
                ddlReadingddlDoctor.Items.Insert(0, itm1);

                DataSet dsInsurance = new DataSet();
                Bill_Sys_Billing_Provider objInsurance = new Bill_Sys_Billing_Provider();
                dsInsurance = objInsurance.GetInsuranceList(txtCompanyID.Text);
                ddlInsurance.DataSource = dsInsurance;
                ListEditItem itminsurance = new ListEditItem("--Select--", null);
                ddlInsurance.DataBind();
                ddlInsurance.Items.Insert(0, itminsurance);

                DataSet dsCaseType = new DataSet();
                Bill_Sys_Billing_Provider objCaseType = new Bill_Sys_Billing_Provider();
                dsCaseType = objCaseType.GetCaseTypeList(txtCompanyID.Text);
                ddlCaseType.DataSource = dsCaseType;
                ListEditItem itmCaseType = new ListEditItem("--Select--", null);
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, itmCaseType);
                Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
                DataSet ds = new DataSet();
                ds = _Bill_Sys_Billing_Provider.GetExcludingBillDeatils(txtCompanyID.Text);
                grdexcludingbill.DataSource = ds;
                grdexcludingbill.DataBind();
                hdndelete.Value = "";
                btnUpdate.Enabled = false;
            }
            if (hdndelete.Value != "true")
            {
                DataSet ds = new DataSet();
                Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
                ds = _Bill_Sys_Billing_Provider.GetExcludingBillDeatils(txtCompanyID.Text);
                grdexcludingbill.DataSource = ds;
                grdexcludingbill.DataBind();
            }
            else
            {
                hdndelete.Value = "";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
        try
        {
            if (ddlReadingddlDoctor.Value != "")
            {
                if (ddlInsurance.Value != null)
                {
                    if (ddlCaseType.Value != null)
                    {
                        if (lstReadingdoctor.SelectedItem != null)
                        {
                            for (int cntLst = 0; cntLst < lstReadingdoctor.Items.Count; cntLst++)
                            {
                                if (lstReadingdoctor.Items[cntLst].Selected == true)
                                {
                                    _Bill_Sys_Billing_Provider.AddExcludingBill(txtCompanyID.Text, lstReadingdoctor.Items[cntLst].Value.ToString(), lstReadingdoctor.Items[cntLst].Text, ddlInsurance.Value.ToString(), ddlInsurance.Text, ddlCaseType.Value.ToString(), ddlCaseType.Text);

                                }

                            }

                        }
                        DataSet ds = new DataSet();
                        ds = _Bill_Sys_Billing_Provider.GetExcludingBillDeatils(txtCompanyID.Text);
                        grdexcludingbill.DataSource = ds;
                        grdexcludingbill.DataBind();
                        ClearControl();
                        usrMessage.PutMessage("Save Successfully ...!");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }

                }
            }

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public void ClearControl()
    {
        ddlReadingddlDoctor.Text = "";
        ddlInsurance.Text = "";
        ddlCaseType.Text = "";
        lstReadingdoctor.Items.Clear();


    }
    protected void grdexcludingbill_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        btnUpdate.Enabled = true;
        btnsave.Enabled = false;
        if (e.CommandArgs.CommandName == "Select")
        {
            Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
            DataSet dsreadingdoctorlist = new DataSet();
            string szdoctorname = Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_READING_DOCTOR"));
            string[] szDoctorId = szdoctorname.Split('-');
            string szalldoctorids = szDoctorId[0];
            dsreadingdoctorlist = _Bill_Sys_Billing_Provider.GetAllDoctorIDList(szalldoctorids, txtCompanyID.Text);
            lstReadingdoctor.DataTextField = "DESCRIPTION";
            lstReadingdoctor.DataValueField = "CODE";
            lstReadingdoctor.DataSource = dsreadingdoctorlist;
            lstReadingdoctor.DataBind();
            btnUpdate.Enabled = true;

            for (int cntLst = 0; cntLst < lstReadingdoctor.Items.Count; cntLst++)
            {
                for (int i = 0; i < dsreadingdoctorlist.Tables[0].Rows.Count; i++)
                {
                    if (dsreadingdoctorlist.Tables[0].Rows[i]["CODE"].ToString() == lstReadingdoctor.Items[cntLst].Value.ToString())
                    {
                        lstReadingdoctor.Items[cntLst].Selected = true;
                    }
                }
            }
            ddlReadingddlDoctor.Enabled = false;
            lstReadingdoctor.Enabled = false;
            btnremove.Visible = false;
            //object keyValue = grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, new string[] { grdexcludingbill.KeyFieldName });
            try
            {
                if (Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_READING_DOCTOR_ID")) != "&nbsp;") { ddlReadingddlDoctor.Text = Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_READING_DOCTOR_ID")); } else { ddlReadingddlDoctor.Text = ""; }
                if (Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_INSURANCE_ID")) != "&nbsp;") { ddlInsurance.Text = Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_INSURANCE_ID")); } else { ddlInsurance.Text = ""; }
                if (Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_CASE_TYPE_ID")) != "&nbsp;") { ddlCaseType.Text = Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_CASE_TYPE_ID")); } else { ddlCaseType.Text = ""; }
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        btnUpdate.Enabled = false;
        ddlReadingddlDoctor.Enabled = true;
        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
        try
        {
            string szdoctorname = Convert.ToString(grdexcludingbill.GetRowValues(grdexcludingbill.FocusedRowIndex, "SZ_READING_DOCTOR"));
            string[] szDoctorId = szdoctorname.Split('-');
            string szDoctorname = szDoctorId[0];
            for (int i = 0; i < grdexcludingbill.VisibleRowCount; i++)
            {
                ASPxGridView _ASPxGridView = (ASPxGridView)grdid.FindControl("grdexcludingbill");
                GridViewDataColumn c = (GridViewDataColumn)grdexcludingbill.Columns[9];
                CheckBox checkBox = (CheckBox)grdexcludingbill.FindRowCellTemplateControl(i, c, "chkall");
                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        //_Bill_Sys_Billing_Provider.RemoveexcludingBill(txtCompanyID.Text, Convert.ToString(grdexcludingbill.GetRowValues(i, "SZ_READING_DOCTOR_ID")), Convert.ToString(grdexcludingbill.GetRowValues(i, "SZ_INSURANCE_ID")), Convert.ToString(grdexcludingbill.GetRowValues(i, "SZ_CASE_TYPE_ID")));
                        _Bill_Sys_Billing_Provider.RemoveexcludingBill(szDoctorname, txtCompanyID.Text);
                        ClearControl();
                        usrMessage.PutMessage("Delete Successfully ...!");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }
                }



            }

            DataSet ds = new DataSet();
            ds = _Bill_Sys_Billing_Provider.GetExcludingBillDeatils(txtCompanyID.Text);
            grdexcludingbill.DataSource = ds;
            grdexcludingbill.DataBind();
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
        btnsave.Enabled = true;

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
        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
       
        btnsave.Enabled = true;
        try
        {
            if (ddlReadingddlDoctor.Value != null)
            {
                if (ddlInsurance.Value != null)
                {
                    if (ddlCaseType.Value != null)
                    {
                        if (lstReadingdoctor.SelectedItem != null)
                        {
                            for (int cntLst = 0; cntLst < lstReadingdoctor.Items.Count; cntLst++)
                            {
                                if (lstReadingdoctor.Items[cntLst].Selected == true)
                                {
                                    _Bill_Sys_Billing_Provider.UpdateExcludingBill(txtCompanyID.Text, lstReadingdoctor.Items[cntLst].Value.ToString(), lstReadingdoctor.Items[cntLst].Text, ddlInsurance.Value.ToString(), ddlInsurance.Text, ddlCaseType.Value.ToString(), ddlCaseType.Text);
                                }
                            }
                        }
                        DataSet ds = new DataSet();
                        ds = _Bill_Sys_Billing_Provider.GetExcludingBillDeatils(txtCompanyID.Text);
                        grdexcludingbill.DataSource = ds;
                        grdexcludingbill.DataBind();
                        ClearControl();
                        usrMessage.PutMessage("Update Successfully ...!");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }
                }
            }
            btnUpdate.Enabled = false;
            ddlReadingddlDoctor.Enabled = true;

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

    protected void ASPxComboBoxddlReadingddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bill_Sys_Billing_Provider _Bill_Sys_Billing_Provider = new Bill_Sys_Billing_Provider();
        DataSet dsreadingdoctorlist = new DataSet();
        string szdoctorname = ddlReadingddlDoctor.Text;
        string[] szDoctorId = szdoctorname.Split('-');
        string szalldoctorids = szDoctorId[0];
        dsreadingdoctorlist = _Bill_Sys_Billing_Provider.GetAllDoctorList(szalldoctorids, txtCompanyID.Text);
        lstReadingdoctor.DataTextField = "DESCRIPTION";
        lstReadingdoctor.DataValueField = "CODE";
        lstReadingdoctor.DataSource = dsreadingdoctorlist;
        lstReadingdoctor.DataBind();
        btnUpdate.Enabled = false;

        for (int cntLst = 0; cntLst < lstReadingdoctor.Items.Count; cntLst++)
        {
            for (int i = 0; i < dsreadingdoctorlist.Tables[0].Rows.Count; i++)
            {
                if (dsreadingdoctorlist.Tables[0].Rows[i]["CODE"].ToString() == lstReadingdoctor.Items[cntLst].Value.ToString())
                {
                    lstReadingdoctor.Items[cntLst].Selected = true;
                }
            }
        }

    }

    protected void btnRemove_Click(object sender, EventArgs e)
    {

    }


}
