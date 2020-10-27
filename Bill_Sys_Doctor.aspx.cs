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
using System.Data.SqlClient;

public partial class Bill_Sys_Doctor : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private Bill_Sys_DeleteBO _deleteOpeation;
    Bill_Sys_DoctorBO _bill_Sys_DoctorBO;

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage=this.Page;
            this._listOperation.Xml_File="Doctor.xml";
            this._listOperation.LoadList();
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

    private void BindSpecialityGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
        try
        {
            DataSet set = this._bill_Sys_DoctorBO.BindDataGrid(this.txtDoctorID.Text);
            this.grdDoctSpeciality.DataSource = set;
            this.grdDoctSpeciality.DataBind();
            bool flag = false;
            int num = 0;
            while (num < set.Tables[0].Rows.Count)
            {
                this.extddlSpeciality.Text=set.Tables[0].Rows[num]["SZ_PROCEDURE_GROUP_ID"].ToString();
                flag = true;
                break;
            }
            if (!flag)
            {
                this.extddlSpeciality.Text="NA";
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
            this.ClearControl();
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
        this._deleteOpeation = new Bill_Sys_DeleteBO();
        string text = "";
        try
        {
            for (int i = 0; i < this.grdDoctorNameList.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdDoctorNameList.Items[i].FindControl("chkDelete");
                if (box.Checked && !this._deleteOpeation.deleteRecord("SP_MST_DOCTOR", "@SZ_DOCTOR_ID", this.grdDoctorNameList.Items[i].Cells[1].Text))
                {
                    if (text == "")
                    {
                        text = this.grdDoctorNameList.Items[i].Cells[2].Text;
                    }
                    else
                    {
                        text = text + " , " + this.grdDoctorNameList.Items[i].Cells[2].Text;
                    }
                }
            }
            if (text != "")
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Records for Doctor " + text + "  exists.'); ", true);
            }
            else
            {
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Doctor deleted successfully ...";
            }
            this.BindGrid();
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

    protected void btnDeleteSpeciality_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("SZ_SPECIALITY_DOCTOR_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            foreach (DataGridItem item in this.grdDoctSpeciality.Items)
            {
                CheckBox box = (CheckBox)item.FindControl("chkDelete");
                if (!box.Checked)
                {
                    DataRow row = table.NewRow();
                    row["SZ_SPECIALITY_DOCTOR_ID"] = item.Cells[0].Text;
                    row["SZ_PROCEDURE_GROUP_ID"] = item.Cells[1].Text;
                    row["SZ_PROCEDURE_GROUP"] = item.Cells[2].Text;
                    table.Rows.Add(row);
                }
            }
            this.grdDoctSpeciality.DataSource = table;
            this.grdDoctSpeciality.DataBind();
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
        this._saveOperation = new SaveOperation();
        try
        {
           if(rdlstWorkType.SelectedValue != "1" )
            {
                this._bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
                string str = this._bill_Sys_DoctorBO.GetPTOwner(this.txtCompanyID.Text ,  this.extddlOffice.Text, this.extddlSpeciality.Text );
                if(str == "NO")
                {
                    this.lblMsg.Visible = true;
                    this.lblMsg.Text = "Please add owner doctor first.";
                    return;
                }
            }
            if (this.rdlstWorkType.SelectedValue == "0")
            {
                this.txtWorkType.Text = "0";
            }
            else if (this.rdlstWorkType.SelectedValue == "1")
            {
                this.txtWorkType.Text = "1";
            }
            else
            {
                this.txtWorkType.Text = "";
            }
            this._saveOperation.WebPage=this.Page;
            this._saveOperation.Xml_File="Doctor.xml";
            this._saveOperation.SaveMethod();
            this._bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
            this.txtDoctorID.Text = this._bill_Sys_DoctorBO.GetDoctorSpecialityID(this.txtCompanyID.Text);
            this._bill_Sys_DoctorBO.SaveDoctorSpeciality(this.txtDoctorID.Text, this.extddlSpeciality.Text, this.txtCompanyID.Text);
            foreach (ListItem item in this.lstProcedureCode.Items)
            {
                if (item.Selected)
                {
                    string str = item.Value.ToString();
                    this._bill_Sys_DoctorBO.AddProcedureCode(this.txtCompanyID.Text, this.txtDoctorID.Text, str);
                }
            }
            this.BindGrid();
            this.ClearControl();
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Doctor Saved Successfully ...!";
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
        this._editOperation = new EditOperation();
        try
        {
            if (this.rdlstWorkType.SelectedValue == "0")
            {
                this.txtWorkType.Text = "0";
            }
            else if (this.rdlstWorkType.SelectedValue == "1")
            {
                this.txtWorkType.Text = "1";
            }
            else
            {
                this.txtWorkType.Text = "";
            }
            this._editOperation.Primary_Value=this.Session["DoctorID"].ToString();
            this._editOperation.WebPage=this.Page;
            this._editOperation.Xml_File="Doctor.xml";
            this._editOperation.UpdateMethod();
            this._bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
            this._bill_Sys_DoctorBO.SaveDoctorSpeciality(this.txtDoctorID.Text, this.extddlSpeciality.Text, this.txtCompanyID.Text);
            if (this.chkSuperVisingDoctor.Checked)
            {
                this._bill_Sys_DoctorBO.DelProcCode(this.txtCompanyID.Text, this.txtDoctorID.Text);
                foreach (ListItem item in this.lstProcedureCode.Items)
                {
                    if (item.Selected)
                    {
                        string str = item.Value.ToString();
                        this._bill_Sys_DoctorBO.AddProcedureCode(this.txtCompanyID.Text, this.txtDoctorID.Text, str);
                    }
                }
            }
            if (!this.chkSuperVisingDoctor.Checked)
            {
                this._bill_Sys_DoctorBO.DelProcCode(this.txtCompanyID.Text, this.txtDoctorID.Text);
            }
            this.BindGrid();
            this.lblMsg.Visible = true;
            this.lblMsg.Text = "Doctor Updated Successfully ...!";
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

    protected void chkSuperVisingDoctor_CheckedChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_ReferalEvent event2 = new Bill_Sys_ReferalEvent();
        try
        {
            this.lblMsg.Visible = false;
            if (this.extddlSpeciality.Text != "NA")
            {
                DataSet procedureCodeAndDescription = new DataSet();
                if (this.chkSuperVisingDoctor.Checked)
                {
                    if (this.extddlSpeciality.Text != "NA")
                    {
                        string text = this.txtCompanyID.Text;
                        string str2 = this.extddlSpeciality.Text;
                        procedureCodeAndDescription = event2.GetProcedureCodeAndDescription(str2, text);
                        this.lstProcedureCode.DataSource = procedureCodeAndDescription.Tables[0];
                        this.lstProcedureCode.DataTextField = "DESCRIPTION";
                        this.lstProcedureCode.DataValueField = "CODE";
                        this.lstProcedureCode.DataBind();
                        this.lstProcedureCode.Visible = true;
                        this.lblProcedurecode.Visible = true;
                    }
                }
                else
                {
                    this.lstProcedureCode.Items.Clear();
                    this.lstProcedureCode.Visible = false;
                    this.lblProcedurecode.Visible = false;
                }
            }
            else
            {
                this.chkSuperVisingDoctor.Checked = false;
                this.lblMsg.Visible = true;
                this.lblMsg.Text = "Select the Speciality!!!";
                this.lstProcedureCode.Items.Clear();
                this.lstProcedureCode.Visible = false;
                this.lblProcedurecode.Visible = false;
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
            this.txtDoctorName.Text = "";
            this.extddlDoctorType.Text="NA";
            this.txtLicenseNumber.Text = "";
            this.extddlProvider.Text="NA";
            this.txtBillingAdd.Text = "";
            this.txtBillingCity.Text = "";
            this.txtBillingPhone.Text = "";
            this.txtBillingState.Text = "";
            this.txtBillingZip.Text = "";
            this.txtFederalTax.Text = "";
            this.txtNPI.Text = "";
            this.txtOfficeAdd.Text = "";
            this.txtOfficeCity.Text = "";
            this.txtOfficePhone.Text = "";
            this.txtOfficeState.Text = "";
            this.txtOfficeZip.Text = "";
            this.txtWCBAuth.Text = "";
            this.txtWCBRatingCode.Text = "";
            this.txtKOEL.Text = "";
            this.BindSpecialityGrid();
            this.chklstTaxType.Items[0].Selected = false;
            this.chklstTaxType.Items[1].Selected = false;
            this.extddlOffice.Text="NA";
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
            this.lblMsg.Visible = false;
            this.btnDeleteSpeciality.Visible = false;
            this.txtTitle.Text = "";
            this.rdlstWorkType.SelectedValue = null;
            this.chkIsUnBilled.Checked = false;
            this.extddlSpeciality.Text="NA";
            this.lstProcedureCode.Items.Clear();
            this.lstProcedureCode.Visible = false;
            this.lblProcedurecode.Visible = false;
            this.chkSuperVisingDoctor.Checked = false;
            this.chkIsReferral.Checked = false;
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
                this.Session["DoctorTypeID"] = e.Item.Cells[4].Text;
                this.Session["DoctorType"] = e.Item.Cells[3].Text;
                base.Response.Redirect("Bill_Sys_DiagnosisCode.aspx", false);
            }
            if (e.CommandName == "AddProcedure")
            {
                this.Session["DoctorID"] = e.Item.Cells[1].Text;
                base.Response.Redirect("Bill_Sys_DoctorProcedureCode.aspx", false);
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

    protected void grdDoctorNameList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdDoctorNameList.CurrentPageIndex = e.NewPageIndex;
            this.BindGrid();
            this.lblMsg.Visible = false;
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
            this.Session["DoctorID"] = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[1].Text;
            this.txtDoctorID.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[1].Text;
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[2].Text != "&nbsp;")
            {
                this.txtDoctorName.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[2].Text;
            }
            else
            {
                this.txtDoctorName.Text = "";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[5].Text != "&nbsp;")
            {
                this.txtLicenseNumber.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[5].Text;
            }
            else
            {
                this.txtLicenseNumber.Text = "";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[6].Text != "&nbsp;")
            {
                this.extddlProvider.Text=this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[6].Text;
            }
            else
            {
                this.extddlProvider.Text="NA";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[14].Text != "&nbsp;")
            {
                this.txtWCBAuth.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[14].Text;
            }
            else
            {
                this.txtWCBAuth.Text = "";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[15].Text != "&nbsp;")
            {
                this.txtWCBRatingCode.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[15].Text;
            }
            else
            {
                this.txtWCBRatingCode.Text = "";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x10].Text != "&nbsp;")
            {
                this.txtOfficeAdd.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x10].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x11].Text != "&nbsp;")
            {
                this.txtOfficeCity.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x11].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x12].Text != "&nbsp;")
            {
                this.txtOfficeState.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x12].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x13].Text != "&nbsp;")
            {
                this.txtOfficeZip.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x13].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[20].Text != "&nbsp;")
            {
                this.txtOfficePhone.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[20].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x15].Text != "&nbsp;")
            {
                this.txtBillingAdd.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x15].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x16].Text != "&nbsp;")
            {
                this.txtBillingCity.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x16].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x17].Text != "&nbsp;")
            {
                this.txtBillingState.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x17].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x18].Text != "&nbsp;")
            {
                this.txtBillingZip.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x18].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x19].Text != "&nbsp;")
            {
                this.txtBillingPhone.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x19].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1a].Text != "&nbsp;")
            {
                this.txtNPI.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1a].Text;
            }
            else
            {
                this.txtNPI.Text = "";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1b].Text != "&nbsp;")
            {
                this.txtFederalTax.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1b].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1c].Text == "0")
            {
                this.chklstTaxType.Items[0].Selected = true;
                this.chklstTaxType.Items[1].Selected = false;
            }
            else if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1c].Text == "1")
            
                {
                this.chklstTaxType.Items[1].Selected = true;
                this.chklstTaxType.Items[0].Selected = false;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1d].Text != "&nbsp;")
            {
                this.txtKOEL.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1d].Text;
            }
            else
            {
                this.txtKOEL.Text = "";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[30].Text != "&nbsp;")
            {
                this.extddlDoctorType.Text=this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[30].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1f].Text != "&nbsp;")
            {
                this.extddlOffice.Text=this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x1f].Text;
            }
            else
            {
                this.extddlOffice.Text="NA";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x21].Text != "&nbsp;")
            {
                this.txtTitle.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x21].Text;
            }
            else
            {
                this.txtTitle.Text = "";
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x22].Text != "&nbsp;")
            {
                this.rdlstWorkType.SelectedValue = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x22].Text;
            }
            else
            {
                this.rdlstWorkType.SelectedValue = null;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x23].Text == "False")
            {
                this.chkIsReferral.Checked = false;
            }
            else
            {
                this.chkIsReferral.Checked = true;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x24].Text != "&nbsp;")
            {
                this.txtFollowUp.Text = this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x24].Text;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[0x26].Text == "False")
            {
                this.chkIsUnBilled.Checked = false;
            }
            else
            {
                this.chkIsUnBilled.Checked = true;
            }
            if (this.grdDoctorNameList.Items[this.grdDoctorNameList.SelectedIndex].Cells[40].Text == "True")
            {
                this.chkSuperVisingDoctor.Checked = true;
            }
            else
            {
                this.chkSuperVisingDoctor.Checked = false;
            }
            this.btnDeleteSpeciality.Visible = false;
            this.BindSpecialityGrid();
            if (this.chkSuperVisingDoctor.Checked)
            {
                if (this.extddlSpeciality.Text != "NA")
                {
                    Bill_Sys_ReferalEvent event2 = new Bill_Sys_ReferalEvent();
                    DataSet procedureCodeAndDescription = new DataSet();
                    string text = this.txtCompanyID.Text;
                    string str2 = this.extddlSpeciality.Text;
                    procedureCodeAndDescription = event2.GetProcedureCodeAndDescription(str2, text);
                    this.lstProcedureCode.DataSource = procedureCodeAndDescription.Tables[0];
                    this.lstProcedureCode.DataTextField = "DESCRIPTION";
                    this.lstProcedureCode.DataValueField = "CODE";
                    this.lstProcedureCode.DataBind();
                    this.lstProcedureCode.Visible = true;
                    this.lblProcedurecode.Visible = true;
                    this._bill_Sys_DoctorBO = new Bill_Sys_DoctorBO();
                    DataSet procedureCodes = new DataSet();
                    procedureCodes = this._bill_Sys_DoctorBO.GetProcedureCodes(this.txtCompanyID.Text, this.txtDoctorID.Text);
                    if (procedureCodes.Tables[0].Rows.Count != 0)
                    {
                        for (int i = 0; i < procedureCodes.Tables[0].Rows.Count; i++)
                        {
                            string str3 = procedureCodes.Tables[0].Rows[i][0].ToString();
                            foreach (ListItem item in this.lstProcedureCode.Items)
                            {
                                if (item.Value.ToString() == str3)
                                {
                                    item.Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.lblProcedurecode.Visible = false;
                this.lstProcedureCode.Items.Clear();
                this.lstProcedureCode.Visible = false;
            }
            this.btnSave.Enabled = false;
            this.btnUpdate.Enabled = true;
            this.lblMsg.Visible = false;
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

    protected void grdDoctSpeciality_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "Speciality")
            {
                ExtendedDropDownList.ExtendedDropDownList list = (ExtendedDropDownList.ExtendedDropDownList)e.Item.FindControl("FooterextddlSpeciality");
                if (list.Text != "NA")
                {
                    bool flag = false;
                    foreach (DataGridItem item in this.grdDoctSpeciality.Items)
                    {
                        if (list.Text== item.Cells[1].Text)
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        this.SaveDoctorSpecialityInGrid(list.Text, list.Selected_Text);
                    }
                }
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

    protected void grdDoctSpeciality_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((ExtendedDropDownList.ExtendedDropDownList)e.Item.FindControl("FooterextddlSpeciality")).Flag_ID=this.txtCompanyID.Text;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.btnSave.Attributes.Add("onclick", "return formValidator('frmDoctor','txtDoctorName,extddlDoctorType,extddlOffice,extddlSpeciality');");
            this.btnUpdate.Attributes.Add("onclick", "return formValidator('frmDoctor','txtDoctorName,extddlDoctorType,extddlOffice,extddlSpeciality');");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            this.extddlOffice.Flag_ID=this.txtCompanyID.Text;
            this.extddlDoctorType.Flag_ID=this.txtCompanyID.Text;
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && !(((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION != "1"))
            {
                this.extddlOffice.Flag_Key_Value="LOCATION_OFFICE_LIST";
            }
            this.extddlProvider.Flag_ID=this.txtCompanyID.Text;
            this.extddlSpeciality.Flag_ID=this.txtCompanyID.Text;
            if (!base.IsPostBack)
            {
                if (base.Request.QueryString["ProviderId"] != null)
                {
                    this.extddlProvider.Text=base.Request.QueryString["ProviderId"].ToString().Trim();
                    this.extddlProvider.Enabled = false;
                }
                this.BindGrid();
                this.BindSpecialityGrid();
                this.btnUpdate.Enabled = false;
            }
            this._deleteOpeation = new Bill_Sys_DeleteBO();
            if (this._deleteOpeation.checkForDelete(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE))
            {
                this.btnDelete.Visible = false;
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

    private void SaveDoctorSpecialityInGrid(string specialityId, string speciality)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable table = new DataTable();
            table.Columns.Add("SZ_SPECIALITY_DOCTOR_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP_ID");
            table.Columns.Add("SZ_PROCEDURE_GROUP");
            DataRow row = table.NewRow();
            row["SZ_SPECIALITY_DOCTOR_ID"] = "";
            row["SZ_PROCEDURE_GROUP_ID"] = specialityId;
            row["SZ_PROCEDURE_GROUP"] = speciality;
            table.Rows.Add(row);
            foreach (DataGridItem item in this.grdDoctSpeciality.Items)
            {
                row = table.NewRow();
                row["SZ_SPECIALITY_DOCTOR_ID"] = item.Cells[0].Text;
                row["SZ_PROCEDURE_GROUP_ID"] = item.Cells[1].Text;
                row["SZ_PROCEDURE_GROUP"] = item.Cells[2].Text;
                table.Rows.Add(row);
            }
            this.grdDoctSpeciality.DataSource = table;
            this.grdDoctSpeciality.DataBind();
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

