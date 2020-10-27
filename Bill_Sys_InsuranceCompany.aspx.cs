/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       Bill_Sys_InsuranceCompany.aspx.cs
/*Purpose              :       To Add and Edit Insurace Company
/*Author               :       Sandeep Y
/*Date of creation     :       22 Dec 2008  
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

public partial class Bill_Sys_InsuranceCompany : PageBase
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    private CaseDetailsBO _caseDetailsBO;
    private DataTable dt = new DataTable();
    private Bill_Sys_DeleteBO _deleteOpeation;
    DataSet dsPriorty = new DataSet();
    public Bill_Sys_InsuranceCompany()
    {
    }

    private void AddAddressDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.grdInsuranceAddress.Items.Count <= 0)
            {
                this.dt.Columns.Add("SZ_INS_ADDRESS_ID");
                this.dt.Columns.Add("SZ_INSURANCE_ADDRESS");
                this.dt.Columns.Add("SZ_INSURANCE_STREET");
                this.dt.Columns.Add("SZ_INSURANCE_CITY");
                this.dt.Columns.Add("SZ_INSURANCE_STATE");
                this.dt.Columns.Add("SZ_INSURANCE_ZIP");
                this.dt.Columns.Add("I_UNIQUE_ID");
                this.dt.Columns.Add("SZ_INSURANCE_STATE_ID");
                this.dt.Columns.Add("I_DEFAULT");
            }
            else
            {
                this.dt = (DataTable)this.Session["INSURANCE_ADDRESS_TABLE"];
            }
            DataRow text = this.dt.NewRow();
            text["SZ_INSURANCE_ADDRESS"] = this.txtInsuranceAddress.Text;
            text["SZ_INSURANCE_STREET"] = this.txtInsuranceStreet.Text;
            text["SZ_INSURANCE_CITY"] = this.txtInsuranceCity.Text;
            text["SZ_INSURANCE_ZIP"] = this.txtInsuranceZip.Text;
            text["I_UNIQUE_ID"] = this.grdInsuranceAddress.Items.Count + 1;
            if (this.extddlState.Text != "NA")
            {
                text["SZ_INSURANCE_STATE_ID"] = this.extddlState.Text;
                text["SZ_INSURANCE_STATE"] = this.extddlState.Selected_Text;
            }
            else
            {
                text["SZ_INSURANCE_STATE_ID"] = -1;
                text["SZ_INSURANCE_STATE"] = "";
            }
            if (!this.chkDefault.Checked)
            {
                text["I_DEFAULT"] = 0;
            }
            else
            {
                text["I_DEFAULT"] = 1;
            }
            this.dt.Rows.Add(text);
            this.Session["INSURANCE_ADDRESS_TABLE"] = this.dt;
            this.grdInsuranceAddress.DataSource = this.dt;
            this.grdInsuranceAddress.DataBind();
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
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage = this.Page;
            this._listOperation.Xml_File = "InsuranceCompany.xml";
            this._listOperation.LoadList();
        }
        catch (Exception ex)
        {
            Exception exception = ex;
            if (exception.Message == "Invalid CurrentPageIndex value. It must be >= 0 and < the PageCount.")
            {
                this.grdInsuranceCompanyList.CurrentPageIndex = 0;
                this.BindGrid();
            }
            else
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

    protected void btnAddAddress_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            this.grdInsuranceAddress.Visible = true;
            if (this.txtInsuranceCompanyID.Text == null || this.txtInsuranceCompanyID.Text == "")
            {
                this.AddAddressDetails();
                this.ClearAddressDetails();
            }
            else if (this.txtInsuranceCompanyID.Text != "")
            {
                this.AddAddressDetails();
                if (!this.chkDefault.Checked)
                {
                    this.txtDefault.Text = "0";
                }
                else
                {
                    this.txtDefault.Text = "1";
                }
                this._saveOperation = new SaveOperation();
                _saveOperation.WebPage = this.Page;
                _saveOperation.Xml_File = "InsuranceCompanyAddress.xml";
                
                this._saveOperation.SaveMethod();
                this._caseDetailsBO = new CaseDetailsBO();
                DataSet insuranceAddressDetails = this._caseDetailsBO.GetInsuranceAddressDetails(this.Session["InsuranceCompanyID"].ToString());
                this.Session["INSURANCE_ADDRESS_TABLE"] = "";
                this.Session["INSURANCE_ADDRESS_TABLE"] = insuranceAddressDetails.Tables[0];
                this.grdInsuranceAddress.DataSource = insuranceAddressDetails;
                this.grdInsuranceAddress.DataBind();
                this.ClearAddressDetails();
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

    protected void btnClearAddress_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ClearAddressDetails();
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
        string str = "";
        try
        {
            for (int i = 0; i < this.grdInsuranceCompanyList.Items.Count; i++)
            {
                if (((CheckBox)this.grdInsuranceCompanyList.Items[i].FindControl("chkDelete")).Checked && !this._deleteOpeation.deleteRecord("SP_MST_INSURANCE_COMPANY", "@SZ_INSURANCE_ID", this.grdInsuranceCompanyList.Items[i].Cells[1].Text))
                {
                    str = (str != "" ? string.Concat(" , ", this.grdInsuranceCompanyList.Items[i].Cells[2].Text) : this.grdInsuranceCompanyList.Items[i].Cells[2].Text);
                }
            }
            if (str == "")
            {
                //this.lblMsg.Visible = true;
                //this.lblMsg.Text = "Insurance Company deleted successfully ...";
                usrMessage.PutMessage("Insurance Company deleted successfully ...");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();
            }
            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("alert('Records for Insurance Company ", str, "  is exists.'); "), true);
            }
            this.dt.Clear();
            this.grdInsuranceAddress.DataSource = this.dt;
            this.grdInsuranceAddress.DataBind();
            this.BindGrid();
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

    protected void btnDeleteInsuranceAddress_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayLists = new ArrayList();
            this.dt = new DataTable();
            this.dt.Columns.Add("SZ_INS_ADDRESS_ID");
            this.dt.Columns.Add("SZ_INSURANCE_ADDRESS");
            this.dt.Columns.Add("SZ_INSURANCE_STREET");
            this.dt.Columns.Add("SZ_INSURANCE_CITY");
            this.dt.Columns.Add("SZ_INSURANCE_STATE");
            this.dt.Columns.Add("SZ_INSURANCE_ZIP");
            this.dt.Columns.Add("I_UNIQUE_ID");
            this.dt.Columns.Add("SZ_INSURANCE_STATE_ID");
            this.dt.Columns.Add("I_DEFAULT");
            this._deleteOpeation = new Bill_Sys_DeleteBO();
            string str = "";
            for (int i = 0; i < this.grdInsuranceAddress.Items.Count; i++)
            {
                if (!((CheckBox)this.grdInsuranceAddress.Items[i].FindControl("chkInsuranceAddressDelete")).Checked)
                {
                    DataRow text = this.dt.NewRow();
                    text["SZ_INS_ADDRESS_ID"] = this.grdInsuranceAddress.Items[i].Cells[1].Text;
                    text["SZ_INSURANCE_ADDRESS"] = this.grdInsuranceAddress.Items[i].Cells[2].Text;
                    text["SZ_INSURANCE_STREET"] = this.grdInsuranceAddress.Items[i].Cells[3].Text;
                    text["SZ_INSURANCE_CITY"] = this.grdInsuranceAddress.Items[i].Cells[4].Text;
                    text["SZ_INSURANCE_STATE"] = this.grdInsuranceAddress.Items[i].Cells[5].Text;
                    text["SZ_INSURANCE_ZIP"] = this.grdInsuranceAddress.Items[i].Cells[6].Text;
                    text["I_UNIQUE_ID"] = this.grdInsuranceAddress.Items[i].Cells[7].Text;
                    text["SZ_INSURANCE_STATE_ID"] = this.grdInsuranceAddress.Items[i].Cells[8].Text;
                    text["I_DEFAULT"] = this.grdInsuranceAddress.Items[i].Cells[10].Text;
                    this.dt.Rows.Add(text);
                }
                else if (this.grdInsuranceAddress.Items[i].Cells[1].Text != "")
                {
                    if (this._deleteOpeation.deleteRecord("SP_MST_INSURANCE_ADDRESS", "@SZ_INS_ADDRESS_ID", this.grdInsuranceAddress.Items[i].Cells[1].Text, "CHECK_DELETE", this.txtCompanyID.Text))
                    {
                        arrayLists.Add(this.grdInsuranceAddress.Items[i].Cells[1].Text);
                    }
                    else
                    {
                        DataRow dataRow = this.dt.NewRow();
                        dataRow["SZ_INS_ADDRESS_ID"] = this.grdInsuranceAddress.Items[i].Cells[1].Text;
                        dataRow["SZ_INSURANCE_ADDRESS"] = this.grdInsuranceAddress.Items[i].Cells[2].Text;
                        dataRow["SZ_INSURANCE_STREET"] = this.grdInsuranceAddress.Items[i].Cells[3].Text;
                        dataRow["SZ_INSURANCE_CITY"] = this.grdInsuranceAddress.Items[i].Cells[4].Text;
                        dataRow["SZ_INSURANCE_STATE"] = this.grdInsuranceAddress.Items[i].Cells[5].Text;
                        dataRow["SZ_INSURANCE_ZIP"] = this.grdInsuranceAddress.Items[i].Cells[6].Text;
                        dataRow["I_UNIQUE_ID"] = this.grdInsuranceAddress.Items[i].Cells[7].Text;
                        dataRow["SZ_INSURANCE_STATE_ID"] = this.grdInsuranceAddress.Items[i].Cells[8].Text;
                        dataRow["I_DEFAULT"] = this.grdInsuranceAddress.Items[i].Cells[10].Text;
                        this.dt.Rows.Add(dataRow);
                        str = (str != "" ? string.Concat(" , ", this.grdInsuranceAddress.Items[i].Cells[2].Text) : this.grdInsuranceAddress.Items[i].Cells[2].Text);
                    }
                }
            }
            if (str != "")
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("alert('Records for Insurance Address ", str, "  is exists.'); "), true);
            }
            this._deleteOpeation = new Bill_Sys_DeleteBO();
            for (int j = 0; j < arrayLists.Count; j++)
            {
                this._deleteOpeation.deleteRecord("SP_MST_INSURANCE_ADDRESS", "@SZ_INS_ADDRESS_ID", arrayLists[j].ToString(), "DELETE", this.txtCompanyID.Text);
            }
            this.Session["DEL_ADDRESS_LIST"] = arrayLists;
            this.Session["INSURANCE_ADDRESS_TABLE"] = this.dt;
            this.grdInsuranceAddress.DataSource = this.dt;
            this.grdInsuranceAddress.DataBind();
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
            if (!this.chkPriority.Checked)
            {
                this.txtPriorityBilling.Text = "0";
            }
            else
            {
                this.txtPriorityBilling.Text = "1";
            }
            if (!this.chkPaperAuthorization.Checked)
            {
                this.txtPaperAuthorization.Text = "0";
            }
            else
            {
                this.txtPaperAuthorization.Text = "1";
            }
            if (!this.chk1500Form.Checked)
            {
                this.txt1500Form.Text = "0";
            }
            else
            {
                this.txt1500Form.Text = "1";
            }

            this._saveOperation.WebPage = this.Page;
            this._saveOperation.Xml_File = "InsuranceCompany.xml";
            this._saveOperation.SaveMethod();
            this._caseDetailsBO = new CaseDetailsBO();
            this.txtInsuranceCompanyID.Text = this._caseDetailsBO.GetLatestInsuranceCompanyID();
            if (this.grdInsuranceAddress.Items.Count > 0)
            {
                for (int i = 0; i < this.grdInsuranceAddress.Items.Count; i++)
                {
                    if (this.grdInsuranceAddress.Items[i].Cells[2].Text != "&nbsp;")
                    {
                        this.txtInsuranceAddress.Text = this.grdInsuranceAddress.Items[i].Cells[2].Text;
                    }
                    if (this.grdInsuranceAddress.Items[i].Cells[3].Text != "&nbsp;")
                    {
                        this.txtInsuranceStreet.Text = this.grdInsuranceAddress.Items[i].Cells[3].Text;
                    }
                    if (this.grdInsuranceAddress.Items[i].Cells[4].Text != "&nbsp;")
                    {
                        this.txtInsuranceCity.Text = this.grdInsuranceAddress.Items[i].Cells[4].Text;
                    }
                    if (this.grdInsuranceAddress.Items[i].Cells[8].Text != "&nbsp;")
                    {
                        this.extddlState.Text = this.grdInsuranceAddress.Items[i].Cells[8].Text;
                    }
                    if (this.grdInsuranceAddress.Items[i].Cells[6].Text != "&nbsp;")
                    {
                        this.txtInsuranceZip.Text = this.grdInsuranceAddress.Items[i].Cells[6].Text;
                    }
                    if (this.grdInsuranceAddress.Items[i].Cells[10].Text != "&nbsp;")
                    {
                        this.txtDefault.Text = this.grdInsuranceAddress.Items[i].Cells[10].Text;
                    }
                    this._saveOperation.WebPage = this.Page;
                    this._saveOperation.Xml_File = "InsuranceCompanyAddress.xml";
                    this._saveOperation.SaveMethod();
                }
            }
            this._deleteOpeation = new Bill_Sys_DeleteBO();
            if (this._deleteOpeation.checkForDelete(this.txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE))
            {
                this.btnDelete.Visible = false;
            }
            this.BindGrid();
            this.ClearControl();
            // this.lblMsg.Visible = true;
            //  this.lblMsg.Text = " Insurance Company Saved successfully ! ";
            usrMessage.PutMessage(" Insurance Company Saved successfully ! ");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            base.Response.Write("<script>window.opener.location.replace('Bill_Sys_CaseMaster.aspx')</script>");

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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._editOperation = new EditOperation();
        this._saveOperation = new SaveOperation();
        try
        {
            if (!this.chkPriority.Checked)
            {
                this.txtPriorityBilling.Text = "0";
            }
            else
            {
                this.txtPriorityBilling.Text = "1";
            }
            if (!this.chkPaperAuthorization.Checked)
            {
                this.txtPaperAuthorization.Text = "0";
            }
            else
            {
                this.txtPaperAuthorization.Text = "1";
            }
            if (!this.chk1500Form.Checked)
            {
                this.txt1500Form.Text = "0";
            }
            else
            {
                this.txt1500Form.Text = "1";
            }
            this._editOperation.Primary_Value = this.Session["InsuranceCompanyID"].ToString();
            this._editOperation.WebPage = this.Page;
            this._editOperation.Xml_File = "InsuranceCompany.xml";
            this._editOperation.UpdateMethod();
            this.grdInsuranceCompanyList.CurrentPageIndex = 0;
            this.BindGrid();
            // this.lblMsg.Visible = true;
            //this.lblMsg.Text = " Insurance Company Updated successfully ! ";

            usrMessage.PutMessage(" Insurance Company Updated successfully ! ");
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

    protected void btnUpdateAddress_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdInsuranceAddress.Visible = true;
            if (this.txtInsuranceAddressID.Text == "")
            {
                DataTable item = (DataTable)this.Session["INSURANCE_ADDRESS_TABLE"];
                for (int i = 0; i < item.Rows.Count; i++)
                {
                    if (this.grdInsuranceAddress.Items[i].Cells[1].Text != "" && this.grdInsuranceAddress.Items[i].Cells[1].Text == this.txtInsuranceAddressID.Text)
                    {
                        DataRow text = item.Rows[i];
                        text["SZ_INSURANCE_ADDRESS"] = this.txtInsuranceAddress.Text;
                        text["SZ_INSURANCE_STREET"] = this.txtInsuranceStreet.Text;
                        text["SZ_INSURANCE_CITY"] = this.txtInsuranceCity.Text;
                        text["SZ_INSURANCE_ZIP"] = this.txtInsuranceZip.Text;
                        if (this.extddlState.Text == "NA")
                        {
                            text["SZ_INSURANCE_STATE"] = "";
                        }
                        else
                        {
                            text["SZ_INSURANCE_STATE_ID"] = this.extddlState.Text;
                            text["SZ_INSURANCE_STATE"] = this.extddlState.Selected_Text;
                        }
                        if (!this.chkDefault.Checked)
                        {
                            text["I_DEFAULT"] = 0;
                            this.txtDefault.Text = "0";
                        }
                        else
                        {
                            text["I_DEFAULT"] = 1;
                            this.txtDefault.Text = "1";
                        }
                        text.AcceptChanges();
                    }
                }
            }
            else
            {
                string str = this.txtInsuranceAddressID.Text;
                DataTable dataTable = (DataTable)this.Session["INSURANCE_ADDRESS_TABLE"];
                int num = 0;
                while (num < dataTable.Rows.Count)
                {
                    if (this.grdInsuranceAddress.Items[num].Cells[1].Text != "" && this.grdInsuranceAddress.Items[num].Cells[1].Text == this.txtInsuranceAddressID.Text)
                    {
                        DataRow selectedText = dataTable.Rows[num];
                        selectedText["SZ_INSURANCE_ADDRESS"] = this.txtInsuranceAddress.Text;
                        selectedText["SZ_INSURANCE_STREET"] = this.txtInsuranceStreet.Text;
                        selectedText["SZ_INSURANCE_CITY"] = this.txtInsuranceCity.Text;
                        selectedText["SZ_INSURANCE_ZIP"] = this.txtInsuranceZip.Text;
                        if (this.extddlState.Text == "NA")
                        {
                            selectedText["SZ_INSURANCE_STATE"] = "";
                        }
                        else
                        {
                            selectedText["SZ_INSURANCE_STATE_ID"] = this.extddlState.Text;
                            selectedText["SZ_INSURANCE_STATE"] = this.extddlState.Selected_Text;
                        }
                        if (!this.chkDefault.Checked)
                        {
                            selectedText["I_DEFAULT"] = 0;
                            this.txtDefault.Text = "0";
                        }
                        else
                        {
                            selectedText["I_DEFAULT"] = 1;
                            this.txtDefault.Text = "1";
                        }
                        selectedText.AcceptChanges();
                        this._editOperation = new EditOperation();
                        this.txtInsuranceCompanyID.Text = this.Session["InsuranceCompanyID"].ToString();
                        this._editOperation.Primary_Value = this.txtInsuranceAddressID.Text;
                        this._editOperation.WebPage = this.Page;
                        this._editOperation.Xml_File = "InsuranceCompanyAddress.xml";
                        this._editOperation.UpdateMethod();
                        break;
                    }
                    else if (this.grdInsuranceAddress.Items[num].Cells[7].Text != this.txtInsuranceAddressID.Text)
                    {
                        num++;
                    }
                    else
                    {
                        DataRow dataRow = dataTable.Rows[num];
                        dataRow["SZ_INSURANCE_ADDRESS"] = this.txtInsuranceAddress.Text;
                        dataRow["SZ_INSURANCE_STREET"] = this.txtInsuranceStreet.Text;
                        dataRow["SZ_INSURANCE_CITY"] = this.txtInsuranceCity.Text;
                        dataRow["SZ_INSURANCE_STATE"] = this.extddlState.Selected_Text;
                        dataRow["SZ_INSURANCE_ZIP"] = this.txtInsuranceZip.Text;
                        if (this.extddlState.Text == "NA")
                        {
                            dataRow["SZ_INSURANCE_STATE"] = "";
                        }
                        else
                        {
                            dataRow["SZ_INSURANCE_STATE_ID"] = this.extddlState.Text;
                            dataRow["SZ_INSURANCE_STATE"] = this.extddlState.Selected_Text;
                        }
                        if (!this.chkDefault.Checked)
                        {
                            dataRow["I_DEFAULT"] = 0;
                            this.txtDefault.Text = "0";
                        }
                        else
                        {
                            dataRow["I_DEFAULT"] = 1;
                            this.txtDefault.Text = "1";
                        }
                        dataRow.AcceptChanges();
                        break;
                    }
                }
                dataTable.AcceptChanges();
                this.Session["INSURANCE_ADDRESS_TABLE"] = null;
                this.Session["INSURANCE_ADDRESS_TABLE"] = dataTable;
                this.grdInsuranceAddress.DataSource = dataTable;
                this.grdInsuranceAddress.DataBind();
                for (int j = 0; j < this.grdInsuranceAddress.Items.Count; j++)
                {
                    if (str == this.grdInsuranceAddress.Items[j].Cells[1].Text)
                    {
                        this.grdInsuranceAddress.Items[j].Cells[10].Text = "1";
                    }
                    else if (str != this.grdInsuranceAddress.Items[j].Cells[7].Text)
                    {
                        this.grdInsuranceAddress.Items[j].Cells[10].Text = "0";
                    }
                    else
                    {
                        this.grdInsuranceAddress.Items[j].Cells[10].Text = "1";
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

    private void ClearAddressDetails()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtInsuranceAddress.Text = "";
            this.txtInsuranceStreet.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsuranceZip.Text = "";
            this.txtInsuranceAddressID.Text = "";
            this.extddlState.Text = "NA";
            this.btnAddAddress.Enabled = true;
            this.btnUpdateAddress.Enabled = false;
            this.chkDefault.Checked = false;
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
            this.txtInsuranceCompanyID.Text = "";
            this.txtInsuranceName.Text = "";
            this.txtInsCode.Text = "";
            this.txtInsuranceAddress.Text = "";
            this.txtInsuranceStreet.Text = "";
            this.txtInsuranceCity.Text = "";
            this.txtInsuranceZip.Text = "";
            this.txtInsuranceState.Text = "";
            this.txtInsurancePhone.Text = "";
            this.txtInsuranceEmail.Text = "";
            this.txtZeusID.Text = "";
            this.extddlState.Text = "NA";
            this.txtFaxNumber.Text = "";
            this.txtContactPerson.Text = "";
            this.ClearAddressDetails();
            this.btnSave.Enabled = true;
            this.btnUpdate.Enabled = false;
            this.lblMsg.Visible = false;
            this.dt.Clear();
            this.grdInsuranceAddress.DataSource = null;
            this.grdInsuranceAddress.DataBind();
            this.grdInsuranceAddress.Visible = false;
            chk1500Form.Checked = false;
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

    protected void grdInsuranceAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!(this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[1].Text != "") || !(this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[1].Text != "&nbsp;"))
            {
                this.txtInsuranceAddressID.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[7].Text;
            }
            else
            {
                this.txtInsuranceAddressID.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[1].Text;
            }
            if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[2].Text == "&nbsp;")
            {
                this.txtInsuranceAddress.Text = "";
            }
            else
            {
                this.txtInsuranceAddress.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[2].Text;
            }
            if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[3].Text == "&nbsp;")
            {
                this.txtInsuranceStreet.Text = "";
            }
            else
            {
                this.txtInsuranceStreet.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[3].Text;
            }
            if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[4].Text == "&nbsp;")
            {
                this.txtInsuranceCity.Text = "";
            }
            else
            {
                this.txtInsuranceCity.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[4].Text;
            }
            if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[5].Text == "&nbsp;")
            {
                this.txtInsuranceState.Text = "";
            }
            else
            {
                this.txtInsuranceState.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[5].Text;
            }
            if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[6].Text == "&nbsp;")
            {
                this.txtInsuranceZip.Text = "";
            }
            else
            {
                this.txtInsuranceZip.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[6].Text;
            }
            if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[10].Text != "&nbsp;")
            {
                if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[10].Text == "1")
                {
                    this.chkDefault.Checked = true;
                }
                else if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[10].Text == "0")
                {
                    this.chkDefault.Checked = false;
                }
            }
            if (this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[8].Text == "&nbsp;")
            {
                this.extddlState.Text = "NA";
            }
            else
            {
                this.extddlState.Text = this.grdInsuranceAddress.Items[this.grdInsuranceAddress.SelectedIndex].Cells[8].Text;
            }
            this.btnAddAddress.Enabled = false;
            this.btnUpdateAddress.Enabled = true;
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

    protected void grdInsuranceCompanyList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
    }

    protected void grdInsuranceCompanyList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "NameSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            else if (e.CommandName.ToString() == "InsuranceCodeSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            else if (e.CommandName.ToString() == "AddressSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            else if (e.CommandName.ToString() == "CitySearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
            }
            else if (e.CommandName.ToString() == "StateSearch")
            {
                if (this.txtSearchOrder.Text != string.Concat(e.CommandArgument, " ASC"))
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " ASC");
                }
                else
                {
                    this.txtSearchOrder.Text = string.Concat(e.CommandArgument, " DESC");
                }
                this.BindGrid();
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

    protected void grdInsuranceCompanyList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdInsuranceCompanyList.CurrentPageIndex = e.NewPageIndex;
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

    protected void grdInsuranceCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        string str1 = "";
        string str2 = "";
        try
        {
            this.ClearAddressDetails();
            this.ClearControl();
            this.Session["InsuranceCompanyID"] = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[1].Text;
            this.txtInsuranceCompanyID.Text = this.Session["InsuranceCompanyID"].ToString();
            if (this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[11].Text != "&nbsp;")
            {
                this.txtInsuranceName.Text = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[11].Text;
            }
            if (this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[14].Text != "&nbsp;")
            {
                this.txtInsCode.Text = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[14].Text;
            }
            if (this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[9].Text != "&nbsp;")
            {
                this.txtInsurancePhone.Text = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[9].Text;
            }
            if (this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[10].Text != "&nbsp;")
            {
                this.txtInsuranceEmail.Text = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[10].Text;
            }
            if (this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[15].Text != "&nbsp;")
            {
                this.txtZeusID.Text = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[15].Text;
            }
            if (this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[16].Text == "&nbsp;")
            {
                this.txtContactPerson.Text = "";
            }
            else
            {
                this.txtContactPerson.Text = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[16].Text;
            }
            if (this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[17].Text == "&nbsp;")
            {
                this.txtFaxNumber.Text = "";
            }
            else
            {
                this.txtFaxNumber.Text = this.grdInsuranceCompanyList.Items[this.grdInsuranceCompanyList.SelectedIndex].Cells[17].Text;
            }
            this._caseDetailsBO = new CaseDetailsBO();
            this.dsPriorty = this._caseDetailsBO.GetPriorityBit(this.txtInsuranceCompanyID.Text, this.txtCompanyID.Text);
            if (this.dsPriorty.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < this.dsPriorty.Tables[0].Rows.Count; i++)
                {
                    str = this.dsPriorty.Tables[0].Rows[i]["BT_PRIORITY_BILLING"].ToString();
                    str1 = this.dsPriorty.Tables[0].Rows[i]["BT_PAPER_AUTHORIZATION"].ToString();
                    str2 = this.dsPriorty.Tables[0].Rows[i]["BT_1500_FORM"].ToString();
                }
            }
            if (str != "1")
            {
                this.chkPriority.Checked = false;
            }
            else
            {
                this.chkPriority.Checked = true;
            }
            if (str1 != "1")
            {
                this.chkPaperAuthorization.Checked = false;
            }
            else
            {
                this.chkPaperAuthorization.Checked = true;
            }
            if (str2 != "1")
            {
                this.chk1500Form.Checked = false;
            }
            else
            {
                this.chk1500Form.Checked = true;
            }
            DataSet insuranceAddressDetails = this._caseDetailsBO.GetInsuranceAddressDetails(this.Session["InsuranceCompanyID"].ToString());
            this.Session["INSURANCE_ADDRESS_TABLE"] = "";
            this.Session["INSURANCE_ADDRESS_TABLE"] = insuranceAddressDetails.Tables[0];
            this.grdInsuranceAddress.DataSource = insuranceAddressDetails;
            this.grdInsuranceAddress.DataBind();
            this.grdInsuranceAddress.Visible = true;
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
            string Elmahstr2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + Elmahstr2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        this.Session["Flag"] = null;
        base.OnUnload(e);
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
            this.btnSave.Attributes.Add("onclick", "return formValidator('frmInsuranceCompany','txtInsuranceName','txtInsCode');");
            this.btnUpdate.Attributes.Add("onclick", "return formValidator('frmInsuranceCompany','txtInsuranceName','txtInsCode');");
            this.btnAddAddress.Attributes.Add("onclick", "return formValidator('frmInsuranceCompany','txtInsuranceAddress')");
            this.btnUpdateAddress.Attributes.Add("onclick", "return formValidator('frmInsuranceCompany','txtInsuranceAddress')");
            this.txtInsuranceEmail.Attributes.Add("onfocusout", "return isEmail('frmInsuranceCompany','txtInsuranceEmail');");
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnDelete.Attributes.Add("onclick", "return ConfirmDelete();");
            if (this.Session["Flag"] != null && this.Session["Flag"].ToString() == "true")
            {
                this.grdInsuranceCompanyList.Visible = false;
                this.btnUpdate.Visible = false;
            }
            else if (!base.IsPostBack)
            {
                this.BindGrid();
                this.btnUpdate.Enabled = false;
                this.btnUpdateAddress.Enabled = false;
                this.ClearAddressDetails();
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
        
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_InsuranceCompany.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}

