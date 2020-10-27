using AjaxControlToolkit;
using ASP;
using DevExpress.Web;
using ExtendedDropDownList;
using Ionic.Zip;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;

public partial class AJAX_Pages_Bill_Sys_BillSearch : Page, IRequiresSessionState
{


    private string BillNo = "";

    private string CasID = "";

    private int iFlag;

    private Bill_Sys_BillTransaction_BO objBillTransactionBO;


    public AJAX_Pages_Bill_Sys_BillSearch()
    {
    }

    private void BindGrid(string szBillno)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
            DataSet dataSet = new DataSet();
            dataSet = billSysBillTransactionBO.GetBillDetail(szBillno);
            this.grdPaymentTransaction.DataSource = dataSet;
            this.grdPaymentTransaction.DataBind();
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

    protected void btn_BillPacket_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayLists = new ArrayList();
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                if (((CheckBox)this.grdBillSearch.Rows[i].Cells[2].FindControl("ChkDelete")).Checked)
                {
                    Bill_Sys_Bill_Packet_Request billSysBillPacketRequest = new Bill_Sys_Bill_Packet_Request()
                    {
                        SZ_CASE_ID = this.grdBillSearch.DataKeys[i]["SZ_CASE_ID"].ToString(),
                        SZ_BILL_NUMBER = this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(),
                        SZ_SPECIALTY = this.grdBillSearch.DataKeys[i]["SPECIALITY"].ToString()
                    };
                    arrayLists.Add(billSysBillPacketRequest);
                }
            }
            Bill_Sys_Upload_VisitReport billSysUploadVisitReport = new Bill_Sys_Upload_VisitReport();
            if ("121" != "")
            {
                string str = billSysUploadVisitReport.CreateBillPacket(this.txtCompanyID.Text, arrayLists);
                if (!str.Contains("ERROR"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Msg", string.Concat("window.open('", ApplicationSettings.GetParameterValue("PACKET_DOC_URL"), str, "'); "), true);
                }
                else
                {
                    string[] strArrays = str.Split(new char[] { ',' });
                    this.usrMessage.PutMessage(string.Concat("Document are not found for bill number", strArrays[1].ToString()));
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btn_Both_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayLists = new ArrayList();
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                if (((CheckBox)this.grdBillSearch.Rows[i].Cells[2].FindControl("ChkDelete")).Checked)
                {
                    Bill_Sys_Bill_Packet_Request billSysBillPacketRequest = new Bill_Sys_Bill_Packet_Request()
                    {
                        SZ_CASE_ID = this.grdBillSearch.DataKeys[i]["SZ_CASE_ID"].ToString(),
                        SZ_BILL_NUMBER = this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(),
                        SZ_SPECIALTY = this.grdBillSearch.DataKeys[i]["SPECIALITY"].ToString()
                    };
                    arrayLists.Add(billSysBillPacketRequest);
                }
            }
            Bill_Sys_Upload_VisitReport billSysUploadVisitReport = new Bill_Sys_Upload_VisitReport();
            if ("121" != "")
            {
                string str = billSysUploadVisitReport.CreateBothPacket(this.txtCompanyID.Text, arrayLists);
                if (!str.Contains("ERROR"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Msg", string.Concat("window.open('", ApplicationSettings.GetParameterValue("PACKET_DOC_URL"), str, "'); "), true);
                }
                else
                {
                    string[] strArrays = str.Split(new char[] { ',' });
                    this.usrMessage.PutMessage(string.Concat("Document are not found for bill number", strArrays[1].ToString()));
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btn_Download_All_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
            DataSet dataSet = new DataSet();
            DataSet dataSet1 = new DataSet();
            dataSet = billSysBillTransactionBO.GetAllBills(this.txtCompanyID.Text, this.txtBillNo.Text, this.txtCasNO.Text, this.extddlBillStatus.Text, this.txtPatientName.Text, this.txtFromDate.Text, this.txtToDate.Text, this.txtVisitDate.Text, this.txtToVisitDate.Text, this.extddlSpeciality.Text, this.txtFromAmount.Text, this.txtToAmt.Text, this.txtSearchBox.Text);
            if (dataSet.Tables[0].Rows.Count <= 0)
            {
                this.usrMessage.PutMessage("Data Not Found For Bills");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            else if (this.rdoDownload.SelectedValue != "1")
            {
                this.CreateALLBillsZip(dataSet, "0");
            }
            else
            {
                this.CreateALLBillsZip(dataSet, "1");
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

    protected void btn_Download_Click(object sender, EventArgs e)
    {
        DataSet dataSet = new DataSet();
        ArrayList arrayLists = new ArrayList();
        dataSet = (this.RadioButtonList1.SelectedValue != "By Speciality" ? this.GetListOfBills("PROVIDER") : this.GetListOfBills("SPECIALITY"));
        this.CreateZip(dataSet);
    }

    protected void btn_Download_id_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
        DataSet dataSet = new DataSet();
        string str = "";
        try
        {
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                if (((CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete")).Checked)
                {
                    str = (str != "" ? string.Concat(str, ",'", this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'") : string.Concat("'", this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'"));
                }
                ///36 to 37-t
                if (this.grdBillSearch.Rows[i].Cells[38].Text.ToString() != "" && this.grdBillSearch.Rows[i].Cells[38].Text.ToString() != "&nbsp;")
                {
                    LinkButton red = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkBillNotes");
                    red.ForeColor = Color.Red;
                }
            }
            dataSet = (this.rdoDownload.SelectedValue != "1" ? billSysBillTransactionBO.GetPathOfBills(str, this.txtCompanyID.Text, "PROVIDER") : billSysBillTransactionBO.GetPathOfBills(str, this.txtCompanyID.Text, "SPECIALITY"));
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                this.CreateBillsZip(dataSet);
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

    protected void btn_PacketDocument_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayLists = new ArrayList();
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                if (((CheckBox)this.grdBillSearch.Rows[i].Cells[2].FindControl("ChkDelete")).Checked)
                {
                    Bill_Sys_Bill_Packet_Request billSysBillPacketRequest = new Bill_Sys_Bill_Packet_Request()
                    {
                        SZ_CASE_ID = this.grdBillSearch.DataKeys[i]["SZ_CASE_ID"].ToString(),
                        SZ_BILL_NUMBER = this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(),
                        SZ_SPECIALTY = this.grdBillSearch.DataKeys[i]["SPECIALITY"].ToString()
                    };
                    arrayLists.Add(billSysBillPacketRequest);
                }
            }
            Bill_Sys_Upload_VisitReport billSysUploadVisitReport = new Bill_Sys_Upload_VisitReport();
            if ("121" != "")
            {
                string str = billSysUploadVisitReport.CreateBillPacketDocument(this.txtCompanyID.Text, arrayLists);
                if (!str.Contains("ERROR"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Msg", string.Concat("window.open('", ApplicationSettings.GetParameterValue("PACKET_DOC_URL"), str, "'); "), true);
                }
                else
                {
                    string[] strArrays = str.Split(new char[] { ',' });
                    this.usrMessage.PutMessage(string.Concat("Document are not found for bill number", strArrays[1].ToString()));
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage.Show();
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnBillSave_Click(object sender, EventArgs e)
    {
        string text = this.txtBillNotes.Text;
        (new BillSearchDAO()).InsertNotes(text, this.lblNotesBillno.Text);
        this.ModalPopupExtender2.Show();
        this.MessageControl1.PutMessage("Save Successfully ...!");
        this.MessageControl1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
        this.MessageControl1.Show();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearPaymentControl();
        this.ModalPopupExtender1.Show();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DAO_NOTES_EO dAONOTESEO;
        this.objBillTransactionBO = new Bill_Sys_BillTransaction_BO();
        Result result = new Result();
        string str = "";
        int num = 0;
        try
        {
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
                {
                    if (((CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete")).Checked)
                    {
                        if (this.grdBillSearch.DataKeys[i]["SZ_BILL_STATUS_CODE"].ToString() == "BLD")
                        {
                            this.objBillTransactionBO.DeleteBillRecord(this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), this.txtCompanyID.Text, "DELETE");
                            dAONOTESEO = new DAO_NOTES_EO()
                            {
                                SZ_MESSAGE_TITLE = "BILL_DELETED",
                                SZ_ACTIVITY_DESC = this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString()
                            };
                            DAO_NOTES_BO dAONOTESBO = new DAO_NOTES_BO();
                            dAONOTESEO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            dAONOTESEO.SZ_CASE_ID = this.grdBillSearch.DataKeys[i]["SZ_CASE_ID"].ToString();
                            dAONOTESEO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                            dAONOTESBO.SaveActivityNotes(dAONOTESEO);
                            this.usrMessage.PutMessage("Bill deleted Successfully ...!");
                            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                            this.usrMessage.Show();
                        }
                        else if (num != 0)
                        {
                            str = string.Concat(str, ",", this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString());
                        }
                        else
                        {
                            str = this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString();
                            num = 1;
                        }
                    }
                    ///36 to 37 -t
                    if (this.grdBillSearch.Rows[i].Cells[38].Text.ToString() != "" && this.grdBillSearch.Rows[i].Cells[38].Text.ToString() != "&nbsp;")
                    {
                        LinkButton red = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkBillNotes");
                        red.ForeColor = Color.Red;
                    }
                }
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable dataTable = new DataTable();
                dataTable = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
            }
            else if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                for (int j = 0; j < this.grdBillSearch.Rows.Count; j++)
                {
                    if (((CheckBox)this.grdBillSearch.Rows[j].FindControl("ChkDelete")).Checked)
                    {
                        if (this.grdBillSearch.DataKeys[j]["SZ_BILL_STATUS_CODE"].ToString() == "BLD")
                        {
                            this.objBillTransactionBO.DeleteReffBill(this.grdBillSearch.DataKeys[j]["SZ_BILL_NUMBER"].ToString(), this.txtCompanyID.Text);
                            dAONOTESEO = new DAO_NOTES_EO()
                            {
                                SZ_MESSAGE_TITLE = "BILL_DELETED",
                                SZ_ACTIVITY_DESC = this.grdBillSearch.DataKeys[j]["SZ_BILL_NUMBER"].ToString()
                            };
                            DAO_NOTES_BO dAONOTESBO1 = new DAO_NOTES_BO();
                            dAONOTESEO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            dAONOTESEO.SZ_CASE_ID = this.grdBillSearch.DataKeys[j]["SZ_CASE_ID"].ToString();
                            dAONOTESEO.SZ_COMPANY_ID = this.txtCompanyID.Text;
                            dAONOTESBO1.SaveActivityNotes(dAONOTESEO);
                            this.usrMessage.PutMessage("Bill deleted Successfully ...!");
                            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                            this.usrMessage.Show();
                        }
                        else if (num != 0)
                        {
                            str = string.Concat(str, ",", this.grdBillSearch.DataKeys[j]["SZ_BILL_NUMBER"].ToString());
                        }
                        else
                        {
                            str = this.grdBillSearch.DataKeys[j]["SZ_BILL_NUMBER"].ToString();
                            num = 1;
                        }
                    }
                }
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable xGridDataset = new DataTable();
                xGridDataset = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][3].ToString()), 2)));
                this.ClearPaymentControl();
                this.BindGrid(this.lblBillNo.Text);
            }
            if (str != "")
            {
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "Done", string.Concat("alert('You can delete bills only if the bill status is Billed. Delete operation aborted.[", str.ToString(), "]');"), true);
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

    protected void btnPaymentDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PaymentModule billSysPaymentModule = new Bill_Sys_PaymentModule();
        string str = "";
        try
        {
            for (int i = 0; i < this.grdPaymentTransaction.Items.Count; i++)
            {
                if (((CheckBox)this.grdPaymentTransaction.Items[i].FindControl("chkDelete")).Checked && !billSysPaymentModule.deleteRecord(this.grdPaymentTransaction.Items[i].Cells[1].Text))
                {
                    str = (str != "" ? string.Concat(",", this.grdPaymentTransaction.Items[i].Cells[2].Text, " ", this.grdPaymentTransaction.Items[i].Cells[4].Text) : string.Concat(this.grdPaymentTransaction.Items[i].Cells[2].Text, " ", this.grdPaymentTransaction.Items[i].Cells[4].Text));
                }
            }
            if (str == "")
            {
                this.usrMessage1.PutMessage("Payment deleted successfully ...");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage1.Show();
            }
            this.BindGrid(this.lblBillNo.Text);
            Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
            decimal num = Convert.ToDecimal(billSysBillingCompanyDetailsBO.GetBalance(this.lblBillNo.Text));
            this.lblBalance.Text = num.ToString();
            this.hdnBal.Value = num.ToString();
            this.txtBal.Text = num.ToString();
            if (Session["FLT_BALANCE"] == null)
                Session.Add("FLT_BALANCE", num);
            else
                Session["FLT_BALANCE"] = num;
            this.ClearPaymentControl();
            this.txtGroupId.Text = this.extddlSpeciality.Text;
            this.txtBillStatusID.Text = this.extddlBillStatus.Text;
            this.grdBillSearch.XGridDatasetNumber = 2;
            this.grdBillSearch.XGridBindSearch();
            DataTable dataTable = new DataTable();
            dataTable = this.grdBillSearch.XGridDataset;
            for (int j = 0; j < this.grdBillSearch.Rows.Count; j++)
            {
                if (this.grdBillSearch.DataKeys[j]["SZ_BILL_NOTES"].ToString() != "" && this.grdBillSearch.DataKeys[j]["SZ_BILL_NOTES"].ToString() != "&nbsp;")
                {
                    LinkButton red = (LinkButton)this.grdBillSearch.Rows[j].FindControl("lnkBillNotes");
                    red.ForeColor = Color.Red;
                }
            }
            this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
            this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
            this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
            this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
            this.BindGrid(this.lblBillNo.Text);
            if (base.Request.QueryString["fromCase"] != null)
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?fromCase=" + base.Request.QueryString["fromCase"] + "&billNo=" + lblBillNo.Text + "&PDELETE=Payment deleted successfully...';", true);
            else
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?billNo=" + lblBillNo.Text + "&PDELETE=Payment deleted successfully...';", true);
            this.Session["LinkClicked"] = null;

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
        this.ModalPopupExtender1.Show();
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void SavePaymentActivityLog(string billNo, string desc)
    {
        DAO_NOTES_EO _DAO_NOTES_EO = null;
        DAO_NOTES_BO _DAO_NOTES_BO = null;
        Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
        _DAO_NOTES_EO = new DAO_NOTES_EO();
        _DAO_NOTES_EO.SZ_MESSAGE_TITLE = "PAYMENT_SAVED";
        _DAO_NOTES_EO.SZ_ACTIVITY_DESC = desc;
        _DAO_NOTES_BO = new DAO_NOTES_BO();
        _DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
        _DAO_NOTES_EO.SZ_CASE_ID = billSysBillingCompanyDetailsBO.GetCaseID(billNo, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        _DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        _DAO_NOTES_BO.SaveActivityNotes(_DAO_NOTES_EO);



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        float principal = 0f;
        float interest = 0f;
        Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
        string str = "";
        decimal num = Convert.ToDecimal(billSysBillingCompanyDetailsBO.GetBalance(this.lblBillNo.Text));
        if (this.chkTypeWriteOff.Checked && this.rdbList.SelectedValue.ToString() == "2")
        {
            Bill_Sys_PaymentModule billSysPaymentModule = new Bill_Sys_PaymentModule();


            //Mangesh
            if (!float.TryParse(txtInterestAmount.Text, out interest))
                interest = 0;
            if (!float.TryParse(txtChequeAmount.Text, out principal))
                principal = 0;

            //if (interest>0)
            //{
            //    interest = float.Parse(this.txtInterestAmount.Text);
            //}
            //else
            //{
            //    single = float.Parse(this.txtChequeAmount.Text);
            //}


            if (this.rdbList.SelectedValue.ToString() == "2")
            {
                billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "WOF");
                this.hdnBillStatusid.Value = this.txtID.Text;
            }
            str = "Write-off";
            string str1 = this.rdbList.SelectedValue.ToString();


            billSysPaymentModule.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtPaymentvalue.Text, str1, str);

            ////Interest entry

            //if(interest>0)
            //{
            //    billSysPaymentModule.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtPaymentvalue.Text, "0", "Save");
            //    SavePaymentActivityLog(this.lblBillNo.Text, "Interest Ammount : $" + str1 + " posted");
            //}

            this.lblBalance.Text = Convert.ToString(Convert.ToDecimal(this.lblBalance.Text) - Convert.ToDecimal(this.txtChequeAmount.Text));
           
            if (Session["FLT_BALANCE"] == null)
                Session.Add("FLT_BALANCE", this.lblBalance.Text);
            else
                Session["FLT_BALANCE"] = this.lblBalance.Text;
            SavePaymentActivityLog(this.lblBillNo.Text, "Write off Ammount : $" + str1 + " posted");

            if (this.checkdate.Value == "1")
            {
                this.ClearPaymentControl();
                this.ddlAll.SelectedIndex = 0;
            }
            this.con.SourceGrid = this.grdBillSearch;
            this.txtSearchBox.SourceGrid = this.grdBillSearch;
            this.grdBillSearch.Page = this.Page;
            this.grdBillSearch.PageNumberList = this.con;
            this.txtGroupId.Text = this.extddlSpeciality.Text;
            this.txtBillStatusID.Text = this.extddlBillStatus.Text;
            this.grdBillSearch.XGridDatasetNumber = 2;
            this.grdBillSearch.XGridBindSearch();
            DataTable dataTable = new DataTable();
            dataTable = this.grdBillSearch.XGridDataset;
            this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
            this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
            this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
            this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
            this.BindGrid(this.lblBillNo.Text);
            this.ModalPopupExtender1.Show();
            this.ddlAll.SelectedIndex = 0;
            return;
        }
        if (this.chkTypeWriteOff.Checked && this.rdbList.SelectedValue.ToString() == "0")
        {
            if (this.rdbList.SelectedValue.ToString() == "0")
            {
                if (num <= Convert.ToDecimal(this.txtChequeAmount.Text))
                {
                    billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                    this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "FBP");
                    this.hdnBillStatusid.Value = this.txtID.Text;
                }
                else
                {
                    this.txtID.Text = this.GetFirstBillStatus(this.lblBillNo.Text, this.txtCompanyID1.Text);
                    this.hdnBillStatusid.Value = this.txtID.Text;
                }

                //Mangesh
                if (!float.TryParse(txtInterestAmount.Text, out interest))
                    interest = 0;
                if (!float.TryParse(txtChequeAmount.Text, out principal))
                    principal = 0;

                //if (interest>0)
                //{
                //    interest = float.Parse(this.txtInterestAmount.Text);
                //}
                //else
                //{
                //    single = float.Parse(this.txtChequeAmount.Text);
                //}

                string str2 = this.rdbList.SelectedValue.ToString();
                str = "Save";
                Bill_Sys_PaymentModule billSysPaymentModule1 = new Bill_Sys_PaymentModule();
                if (this.Session["Type"] != null)
                {
                    if (!(this.Session["Type"].ToString() != "0") || !(this.Session["Type"].ToString() != "3"))
                    {
                        billSysPaymentModule1.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtAll.Text, str2, str);
                        SavePaymentActivityLog(this.lblBillNo.Text, "Write off Ammount : $" + str2 + " posted");

                        ////Interest entry

                        //if (interest > 0)
                        //{
                        //    billSysPaymentModule1.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtAll.Text, "0", "Save");
                        //    SavePaymentActivityLog(this.lblBillNo.Text, "Interest Ammount : $" + str2 + " posted");
                        //}
                    }
                    else
                    {
                        billSysPaymentModule1.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtPaymentvalue.Text, str2, str);
                        SavePaymentActivityLog(this.lblBillNo.Text, "Write off Ammount : $" + str2 + " posted");

                    }
                    if (principal > 0)
                    {
                        this.lblBalance.Text = Convert.ToString(Convert.ToDecimal(this.lblBalance.Text) - Convert.ToDecimal(this.txtChequeAmount.Text));
                        if (Session["FLT_BALANCE"] == null)
                            Session.Add("FLT_BALANCE", this.lblBalance.Text);
                        else
                            Session["FLT_BALANCE"] = this.lblBalance.Text;
                        this.hdnBal.Value = this.lblBalance.Text;
                    }

                    this.usrMessage1.PutMessage("Your payment saved");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage1.Show();
                    if (base.Request.QueryString["fromCase"] != null)
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?fromCase=" + base.Request.QueryString["fromCase"] + "&billNo=" + lblBillNo.Text + "&PADD=Your payment saved';", true);
                    else
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?billNo=" + lblBillNo.Text + "&PADD=Your payment saved';", true);
                    this.Session["LinkClicked"] = null;
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Unable to save your payment'); ", true);
                    if (this.checkdate.Value == "1")
                    {
                        this.ClearPaymentControl();
                        this.ddlAll.SelectedIndex = 0;
                    }
                    this.con.SourceGrid = this.grdBillSearch;
                    this.txtSearchBox.SourceGrid = this.grdBillSearch;
                    this.grdBillSearch.Page = this.Page;
                    this.grdBillSearch.PageNumberList = this.con;
                    this.txtGroupId.Text = this.extddlSpeciality.Text;
                    this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                    this.grdBillSearch.XGridDatasetNumber = 2;
                    this.grdBillSearch.XGridBindSearch();
                    DataTable xGridDataset = new DataTable();
                    xGridDataset = this.grdBillSearch.XGridDataset;
                    this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][0].ToString()), 2)));
                    this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][1].ToString()), 2)));
                    this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][2].ToString()), 2)));
                    this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][3].ToString()), 2)));
                    this.BindGrid(this.lblBillNo.Text);
                    this.ModalPopupExtender1.Show();
                    this.ddlAll.SelectedIndex = 0;
                    return;
                }
            }
        }
        else if (!this.chkTypeWriteOff.Checked || !(this.rdbList.SelectedValue.ToString() == "1"))
        {
            if (this.rdbList.SelectedValue.ToString() == "1")
            {
                billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "LT");
                this.hdnBillStatusid.Value = this.txtID.Text;
                str = "Litigation";
            }
            if (this.rdbList.SelectedValue.ToString() == "2")
            {
                this.txtID.Text = (new Bill_Sys_BillingCompanyDetails_BO()).GetBillStatusID(this.txtCompanyID.Text, "WOF");
                this.hdnBillStatusid.Value = this.txtID.Text;
                str = "Write_off";
            }
            if (this.rdbList.SelectedValue.ToString() == "0")
            {
                if (num <= Convert.ToDecimal(this.txtChequeAmount.Text))
                {
                    this.txtID.Text = new Bill_Sys_BillingCompanyDetails_BO().GetBillStatusID(this.txtCompanyID.Text, "FBP");
                    this.hdnBillStatusid.Value = this.txtID.Text;
                    str = "Save";
                }
                else
                {
                    this.txtID.Text = this.GetFirstBillStatus(this.lblBillNo.Text, this.txtCompanyID1.Text);
                    this.hdnBillStatusid.Value = this.txtID.Text;
                    str = "Save";
                }
            }

            //Mangesh
            if (!float.TryParse(txtInterestAmount.Text, out interest))
                interest = 0;
            if (!float.TryParse(txtChequeAmount.Text, out principal))
                principal = 0;

            //if (interest>0)
            //{
            //    interest = float.Parse(this.txtInterestAmount.Text);
            //}
            //else
            //{
            //    single = float.Parse(this.txtChequeAmount.Text);
            //}


            Bill_Sys_PaymentModule billSysPaymentModule2 = new Bill_Sys_PaymentModule();
            if (this.Session["Type"] != null)
            {
                int num1 = 0;
                num1 = (!(this.Session["Type"].ToString() != "0") || !(this.Session["Type"].ToString() != "3") ? billSysPaymentModule2.BillTrasaction(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtAll.Text, str) : billSysPaymentModule2.BillTrasaction(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtPaymentvalue.Text, str));

                //if(interest>0)
                //{
                //    num1 = (!(this.Session["Type"].ToString() != "0") || !(this.Session["Type"].ToString() != "3") ? billSysPaymentModule2.BillTrasaction(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtAll.Text, "Save") : billSysPaymentModule2.BillTrasaction(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtPaymentvalue.Text, "Save"));
                //}

                if (num1 == 0)
                {
                    this.usrMessage1.PutMessage("Unable to save your payment");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    this.usrMessage1.Show();
                }
                else
                {
                    SavePaymentActivityLog(this.lblBillNo.Text, "Check Ammount : $" + this.txtChequeAmount.Text + " paid");

                    if (principal > 0)
                    {
                        this.lblBalance.Text = Convert.ToString(Convert.ToDecimal(this.lblBalance.Text) - Convert.ToDecimal(this.txtChequeAmount.Text));
                        if (Session["FLT_BALANCE"] == null)
                            Session.Add("FLT_BALANCE", this.lblBalance.Text);
                        else
                            Session["FLT_BALANCE"] = this.lblBalance.Text;
                        this.hdnBal.Value = this.lblBalance.Text;
                    }
                    this.usrMessage1.PutMessage("Your payment saved");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage1.Show();
                    if (base.Request.QueryString["fromCase"] != null)
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?fromCase=" + base.Request.QueryString["fromCase"] + "&billNo=" + lblBillNo.Text + "&PADD=Your payment saved';", true);
                    else
                        ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?billNo=" + lblBillNo.Text + "&PADD=Your payment saved';", true);
                    this.Session["LinkClicked"] = null;
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Unable to save your payment'); ", true);
                }
                if (this.checkdate.Value == "1")
                {
                    this.ClearPaymentControl();
                }
                this.con.SourceGrid = this.grdBillSearch;
                this.txtSearchBox.SourceGrid = this.grdBillSearch;
                this.grdBillSearch.Page = this.Page;
                this.grdBillSearch.PageNumberList = this.con;
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable dataTable1 = new DataTable();
                dataTable1 = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][3].ToString()), 2)));
                this.BindGrid(this.lblBillNo.Text);
                this.ModalPopupExtender1.Show();
                this.ddlAll.SelectedIndex = 0;
                if (this.rdbList.SelectedValue.ToString() == "2")
                {
                    this.lblBalance.Text = "0";
                    if (Session["FLT_BALANCE"] == null)
                        Session.Add("FLT_BALANCE", this.lblBalance.Text);
                    else
                        Session["FLT_BALANCE"] = this.lblBalance.Text;
                }
            }
        }
        else
        {
            if (this.rdbList.SelectedValue.ToString() == "1")
            {
                billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "LT");
                this.hdnBillStatusid.Value = this.txtID.Text;
                str = "Litigation";
            }

            //Mangesh
            if (!float.TryParse(txtInterestAmount.Text, out interest))
                interest = 0;
            if (!float.TryParse(txtChequeAmount.Text, out principal))
                principal = 0;

            //if (interest>0)
            //{
            //    interest = float.Parse(this.txtInterestAmount.Text);
            //}
            //else
            //{
            //    single = float.Parse(this.txtChequeAmount.Text);
            //}

            string str3 = this.rdbList.SelectedValue.ToString();
            str = "Litigation";
            Bill_Sys_PaymentModule billSysPaymentModule3 = new Bill_Sys_PaymentModule();
            if (this.Session["Type"] != null)
            {
                if (!(this.Session["Type"].ToString() != "0") || !(this.Session["Type"].ToString() != "3"))
                {

                    billSysPaymentModule3.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtAll.Text, str3, str);
                    SavePaymentActivityLog(this.lblBillNo.Text, "Write off Ammount : $" + str3 + " posted");

                    //if(interest>0)
                    //{
                    //    billSysPaymentModule3.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtAll.Text, "0", "Save");
                    //}
                }
                else
                {
                    billSysPaymentModule3.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtPaymentvalue.Text, str3, str);
                    SavePaymentActivityLog(this.lblBillNo.Text, "Write off Ammount : $" + str3 + " posted");

                    //if (interest > 0)
                    //{
                    //    billSysPaymentModule3.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "ADD", "", this.Session["Type"].ToString(), this.txtAll.Text, "0", "Save");
                    //}
                }
                if (principal > 0)
                {
                    this.lblBalance.Text = Convert.ToString(Convert.ToDecimal(this.lblBalance.Text) - Convert.ToDecimal(this.txtChequeAmount.Text));
                    if (Session["FLT_BALANCE"] == null)
                        Session.Add("FLT_BALANCE", this.lblBalance.Text);
                    else
                        Session["FLT_BALANCE"] = this.lblBalance.Text;
                    this.hdnBal.Value = this.lblBalance.Text;
                }
                this.usrMessage1.PutMessage("Your payment saved");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage1.Show();
                if (base.Request.QueryString["fromCase"] != null)
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?fromCase=" + base.Request.QueryString["fromCase"] + "&billNo=" + lblBillNo.Text + "&PADD=Your payment saved';", true);
                else
                    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?billNo=" + lblBillNo.Text + "&PADD=Your payment saved';", true);
                this.Session["LinkClicked"] = null;
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "alert('Unable to save your payment'); ", true);
                if (this.checkdate.Value == "1")
                {
                    this.ClearPaymentControl();
                }
                this.con.SourceGrid = this.grdBillSearch;
                this.txtSearchBox.SourceGrid = this.grdBillSearch;
                this.grdBillSearch.Page = this.Page;
                this.grdBillSearch.PageNumberList = this.con;
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable xGridDataset1 = new DataTable();
                xGridDataset1 = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][3].ToString()), 2)));
                this.BindGrid(this.lblBillNo.Text);
                this.ModalPopupExtender1.Show();
                this.ddlAll.SelectedIndex = 0;
                return;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, base.GetType(), "kk11", "Showdiv();", true);
        this.lblTotalBillAmount.Text = "test";
        this.lblTotalBillAmount.Text = "";
        this.lblOutSratingAmount.Text = "";
        string text = "";
        this.txtGroupId.Text = this.extddlSpeciality.Text;
        this.txtBillStatusID.Text = this.extddlBillStatus.Text;
        if (!this.checkboxlst.Items[0].Selected)
        {
            this.txtden.Text = "";
        }
        else
        {
            this.txtden.Text = "den";
        }
        if (!this.checkboxlst.Items[1].Selected)
        {
            this.txtfbp.Text = "";
        }
        else
        {
            this.txtfbp.Text = "fdp";
        }
        if (!this.radioList.Items[0].Selected)
        {
            this.txtvs.Text = "";
        }
        else
        {
            this.txtvs.Text = "vs";
        }
        if (!this.radioList.Items[1].Selected)
        {
            this.txtvr.Text = "";
        }
        else
        {
            this.txtvr.Text = "vr";
        }
        if (this.txtToAmt.Visible && this.txtToAmt.Text != "" && this.txtAmount.Text != "")
        {
            this.txtRange.Text = this.txtToAmt.Text;
            this.txtFromAmount.Text = this.txtAmount.Text;
            text = this.txtAmount.Text;
        }
        else if (this.txtAmount.Text != "" && this.txtAmount.Visible)
        {
            this.txtRange.Text = "";
            if (this.txtAmount.Text != "")
            {
                text = this.txtAmount.Text;
                this.txtAmount.Text = string.Concat(this.ddlAmount.SelectedValue.ToString().Trim(), this.txtAmount.Text);
            }
            this.txtFromAmount.Text = this.txtAmount.Text;
        }
        if (!this.txtAmount.Visible)
        {
            this.txtFromAmount.Text = "";
        }
        this.grdBillSearch.XGridDatasetNumber = 2;
        this.grdBillSearch.XGridBindSearch();
        DataTable dataTable = new DataTable();
        dataTable = this.grdBillSearch.XGridDataset;
        this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
        this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
        this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
        this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
        this.txtAmount.Text = text;
        for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
        {
            if (this.grdBillSearch.Rows[i].Cells[7].Equals("Billed By BSBG"))
            {
                this.grdBillSearch.Rows[i].BackColor = Color.LawnGreen;
            }
            if (this.grdBillSearch.Rows[i].Cells[7].Equals("Billed By KH"))
            {
                this.grdBillSearch.Rows[i].BackColor = Color.Yellow;
            }
            if (this.grdBillSearch.DataKeys[i]["SZ_BILL_NOTES"].ToString() != "" && this.grdBillSearch.DataKeys[i]["SZ_BILL_NOTES"].ToString() != "&nbsp;")
            {
                LinkButton red = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkBillNotes");
                red.ForeColor = Color.Red;
            }
        }
        this.PusSignColorChange();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        decimal num;
        float principal = 0f;
        float interest = 0f;
        string str = "";
        Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
        this.btnSave.Enabled = true;
        this.btnUpdate.Enabled = false;
        try
        {
            decimal num1 = Convert.ToDecimal(billSysBillingCompanyDetailsBO.GetBalance(this.lblBillNo.Text)) + Convert.ToDecimal(this.txtPrev.Text.Trim() == string.Empty ? "0" : this.txtPrev.Text);
            if (this.chkTypeWriteOff.Checked && this.rdbList.SelectedValue.ToString() == "2")
            {
                Bill_Sys_PaymentModule billSysPaymentModule = new Bill_Sys_PaymentModule();
                if (this.rdbList.SelectedValue.ToString() == "2")
                {
                    billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                    this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "WOF");
                    this.hdnBillStatusid.Value = this.txtID.Text;
                    str = "Write-off";
                }

                //Mangesh
                if (!float.TryParse(txtInterestAmount.Text, out interest))
                    interest = 0;
                if (!float.TryParse(txtChequeAmount.Text, out principal))
                    principal = 0;

                string str1 = this.rdbList.SelectedValue.ToString();
                billSysPaymentModule.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, str1, str);
                //Interest entry

                //if (interest > 0)
                //{
                //    billSysPaymentModule.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, "0", "Save");
                //}

                num = Convert.ToDecimal(billSysBillingCompanyDetailsBO.GetBalance(this.lblBillNo.Text));
                this.lblBalance.Text = num.ToString();
                this.hdnBal.Value = this.lblBalance.Text;
                this.txtBal.Text = num.ToString();
                this.usrMessage1.PutMessage("Payment updated successfully.");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage1.Show();
                this.ModalPopupExtender1.Show();
                if (this.checkdate.Value == "1")
                {
                    this.ClearPaymentControl();
                    this.ddlAll.SelectedIndex = 0;
                }
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable dataTable = new DataTable();
                dataTable = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
                this.BindGrid(this.lblBillNo.Text);
                this.ModalPopupExtender1.Show();
            }
            else if (this.chkTypeWriteOff.Checked && this.rdbList.SelectedValue.ToString() == "0")
            {
                Bill_Sys_PaymentModule billSysPaymentModule1 = new Bill_Sys_PaymentModule();
                if (this.rdbList.SelectedValue.ToString() == "0")
                {
                    if (num1 <= Convert.ToDecimal(this.txtChequeAmount.Text))
                    {
                        billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                        this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "FBP");
                        this.hdnBillStatusid.Value = this.txtID.Text;
                        str = "Save";
                    }
                    else
                    {
                        this.txtID.Text = this.GetFirstBillStatus(this.lblBillNo.Text, this.txtCompanyID1.Text);
                        this.hdnBillStatusid.Value = this.txtID.Text;
                        str = "Save";
                    }

                    //Mangesh
                    if (!float.TryParse(txtInterestAmount.Text, out interest))
                        interest = 0;
                    if (!float.TryParse(txtChequeAmount.Text, out principal))
                        principal = 0;

                }
                string str2 = this.rdbList.SelectedValue.ToString();
                billSysPaymentModule1.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, str2, str);

                ////Interst Entry
                //if(interest>0)
                //{
                //    billSysPaymentModule1.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, "0", "Save");
                //}

                num = Convert.ToDecimal(billSysBillingCompanyDetailsBO.GetBalance(this.lblBillNo.Text));
                this.lblBalance.Text = num.ToString();
                this.hdnBal.Value = this.lblBalance.Text;
                this.txtBal.Text = num.ToString();
                this.usrMessage1.PutMessage("Payment updated successfully.");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage1.Show();
                this.ModalPopupExtender1.Show();
                if (this.checkdate.Value == "1")
                {
                    this.ClearPaymentControl();
                    this.ddlAll.SelectedIndex = 0;
                }
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable xGridDataset = new DataTable();
                xGridDataset = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][3].ToString()), 2)));
                this.BindGrid(this.lblBillNo.Text);
                this.ModalPopupExtender1.Show();
            }
            else if (!this.chkTypeWriteOff.Checked || !(this.rdbList.SelectedValue.ToString() == "1"))
            {
                if (this.rdbList.SelectedValue.ToString() == "1")
                {
                    billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                    this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "LT");
                    this.hdnBillStatusid.Value = this.txtID.Text;
                    str = "Litigation";
                }
                if (this.rdbList.SelectedValue.ToString() == "2")
                {
                    billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                    this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "WOF");
                    this.hdnBillStatusid.Value = this.txtID.Text;
                    str = "Write_off";
                }
                if (this.rdbList.SelectedValue.ToString() == "0")
                {
                    if (num1 <= Convert.ToDecimal(this.txtChequeAmount.Text))
                    {
                        billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                        this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "FBP");
                        this.hdnBillStatusid.Value = this.txtID.Text;
                        str = "Save";
                    }
                    else
                    {
                        this.txtID.Text = this.GetFirstBillStatus(this.lblBillNo.Text, this.txtCompanyID1.Text);
                        this.hdnBillStatusid.Value = this.txtID.Text;
                        str = "Save";
                    }
                }

                //Mangesh
                if (!float.TryParse(txtInterestAmount.Text, out interest))
                    interest = 0;
                if (!float.TryParse(txtChequeAmount.Text, out principal))
                    principal = 0;

                if ((new Bill_Sys_PaymentModule()).BillTrasaction(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, str) != 0)
                {
                    ////Interest Entry
                    //if(interest>0)
                    //{
                    //    new Bill_Sys_PaymentModule().BillTrasaction(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, "Save");
                    //}


                    num = Convert.ToDecimal(billSysBillingCompanyDetailsBO.GetBalance(this.lblBillNo.Text));
                    this.lblBalance.Text = num.ToString();
                    this.hdnBal.Value = this.lblBalance.Text;
                    this.txtBal.Text = num.ToString();
                    this.usrMessage1.PutMessage("Payment updated successfully.");
                    this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage1.Show();
                }
                this.ModalPopupExtender1.Show();
                if (this.checkdate.Value == "1")
                {
                    this.ClearPaymentControl();
                    this.ddlAll.SelectedIndex = 0;
                }
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable dataTable1 = new DataTable();
                dataTable1 = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable1.Rows[0][3].ToString()), 2)));
                this.BindGrid(this.lblBillNo.Text);
            }
            else
            {
                Bill_Sys_PaymentModule billSysPaymentModule2 = new Bill_Sys_PaymentModule();
                if (this.rdbList.SelectedValue.ToString() == "1")
                {
                    billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                    this.txtID.Text = billSysBillingCompanyDetailsBO.GetBillStatusID(this.txtCompanyID.Text, "LT");
                    this.hdnBillStatusid.Value = this.txtID.Text;
                    str = "Litigation";
                }

                //Mangesh
                if (!float.TryParse(txtInterestAmount.Text, out interest))
                    interest = 0;
                if (!float.TryParse(txtChequeAmount.Text, out principal))
                    principal = 0;

                string str3 = this.rdbList.SelectedValue.ToString();
                billSysPaymentModule2.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, principal, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, str3, str);

                ////Interest entry
                //if(interest>0)
                //{
                //    billSysPaymentModule2.BillTrasactionForWriteOff(this.lblBillNo.Text, this.txtChequeNumber.Text, this.txtChequeDate.Text, 0, interest, this.txtCompanyID1.Text, this.txtDescription.Text, this.txtUserID.Text, this.txtID.Text, "UPDATE", this.txtPaymentID.Text, this.Session["Type"].ToString(), this.txtAll.Text, "0", "Save");
                //}

                num = Convert.ToDecimal(billSysBillingCompanyDetailsBO.GetBalance(this.lblBillNo.Text));
                this.lblBalance.Text = num.ToString();
                this.hdnBal.Value = this.lblBalance.Text;
                this.txtBal.Text = num.ToString();
                this.usrMessage1.PutMessage("Payment updated successfully.");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage1.Show();
                this.ModalPopupExtender1.Show();
                if (this.checkdate.Value == "1")
                {
                    this.ClearPaymentControl();
                    this.ddlAll.SelectedIndex = 0;
                }
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                this.grdBillSearch.XGridDatasetNumber = 2;
                this.grdBillSearch.XGridBindSearch();
                DataTable xGridDataset1 = new DataTable();
                xGridDataset1 = this.grdBillSearch.XGridDataset;
                this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][0].ToString()), 2)));
                this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][1].ToString()), 2)));
                this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][2].ToString()), 2)));
                this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset1.Rows[0][3].ToString()), 2)));
                this.BindGrid(this.lblBillNo.Text);
                this.ModalPopupExtender1.Show();
            }
            /// 36 to 37 -t
            this.grdBillSearch.Columns[38].Visible = true;
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                if (this.grdBillSearch.DataKeys[i]["SZ_BILL_NOTES"].ToString() != "" && this.grdBillSearch.DataKeys[i]["SZ_BILL_NOTES"].ToString() != "&nbsp;")
                {
                    LinkButton red = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkBillNotes");
                    red.ForeColor = Color.Red;
                }
            }
            ///36 to 37 -t
            this.grdBillSearch.Columns[38].Visible = true;
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
    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_ReportBO billSysReportBO = new Bill_Sys_ReportBO();
        try
        {
            if (this.drdUpdateStatus.Text != "NA")
            {
                ArrayList arrayLists = new ArrayList();
                string str = "";
                bool flag = false;
                for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
                {
                    if (((CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete")).Checked)
                    {
                        if (flag)
                        {
                            str = string.Concat(str, ",'", this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'");
                        }
                        else
                        {
                            str = string.Concat("'", this.grdBillSearch.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'");
                            flag = true;
                        }
                    }
                }
                if (str != "")
                {
                    arrayLists.Add(this.drdUpdateStatus.Text);
                    arrayLists.Add(str);
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    arrayLists.Add(DateTime.Today.ToShortDateString());
                    arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    billSysReportBO.updateBillStatus(arrayLists);
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_STATUS_UPDATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Bill No : " + str + " updated to :" + this.drdUpdateStatus.Selected_Text;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    //this._DAO_NOTES_EO.SZ_CASE_ID = new Bill_Sys_BillingCompanyDetails_BO().GetCaseID(item.sz_bill_no, ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    this.usrMessage.PutMessage("Bill status updated successfully.");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    this.txtGroupId.Text = this.extddlSpeciality.Text;
                    this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                    this.grdBillSearch.XGridDatasetNumber = 2;
                    this.grdBillSearch.XGridBindSearch();
                    DataTable dataTable = new DataTable();
                    dataTable = this.grdBillSearch.XGridDataset;
                    this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
                    this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
                    this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
                    this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
                }
            }
            for (int j = 0; j < this.grdBillSearch.Rows.Count; j++)
            {
                if (this.grdBillSearch.DataKeys[j]["SZ_BILL_NOTES"].ToString() != "" && this.grdBillSearch.DataKeys[j]["SZ_BILL_NOTES"].ToString() != "&nbsp;")
                {
                    LinkButton red = (LinkButton)this.grdBillSearch.Rows[j].FindControl("lnkBillNotes");
                    red.ForeColor = Color.Red;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.fuUploadReport.HasFile)
            {
                Bill_Sys_UploadFile billSysUploadFile = new Bill_Sys_UploadFile();
                ArrayList arrayLists = new ArrayList();
                ArrayList arrayLists1 = new ArrayList();
                ArrayList arrayLists2 = new ArrayList();
                ArrayList arrayLists3 = new ArrayList();
                string str = "";
                arrayLists1.Add(this.Session["CID"].ToString());
                arrayLists.Add(this.lblBillNo.Text);
                arrayLists2.Add(this.Session["Speciality"].ToString());
                billSysUploadFile.sz_bill_no = arrayLists;
                billSysUploadFile.sz_case_id = arrayLists1;
                billSysUploadFile.sz_company_id = this.txtCompanyID.Text;
                if (this.fuUploadReport.FileName != "")
                {
                    str = FileUtilities.FormatUploadedFileName(this.fuUploadReport.FileName);
                }
                billSysUploadFile.sz_FileName = str;
                billSysUploadFile.sz_File = this.fuUploadReport.FileBytes;
                billSysUploadFile.sz_UserId = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                billSysUploadFile.sz_UserName = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
                billSysUploadFile.sz_StatusCode = "";
                billSysUploadFile.sz_flag = "PAY";
                billSysUploadFile.sz_payment_id = this.Session["payment_No"].ToString();
                billSysUploadFile.sz_speciality_id = arrayLists2;
                arrayLists3 = (new FileUpload()).UploadFile(billSysUploadFile);
                Bill_Sys_BillingCompanyDetails_BO billSysBillingCompanyDetailsBO = new Bill_Sys_BillingCompanyDetails_BO();
                ArrayList arrayLists4 = new ArrayList();
                arrayLists4.Add(this.lblBillNo.Text);
                arrayLists4.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                arrayLists4.Add(arrayLists3[0]);
                arrayLists4.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                arrayLists4.Add(this.Session["payment_No"].ToString());
                billSysBillingCompanyDetailsBO.InsertBillPaymentImages(arrayLists4);
                this.BindGrid(this.lblBillNo.Text);
                this.usrMessage1.PutMessage("File Upload Successfully");
                this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage1.Show();
                this.ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", " alert('please select file from upload Report !');showUploadFilePopup();", true);
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

    public void ClearControl()
    {
        this.txtBillNo.Text = "";
        this.txtCasNO.Text = "";
    }

    public void ClearPaymentControl()
    {
        this.txtChequeDate.Text = "";
        this.txtChequeNumber.Text = "";
        this.txtChequeAmount.Text = "";
        this.txtDescription.Text = "";
        this.txtPrev.Text = "";
        txtInterestAmount.Text = "";
        this.txtAll.Text = "";
        //this.ddlAll.SelectedIndex = 0;
    }

    public DataSet GetEORInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_MANAGE_GET_EOR_REASON", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
            }
            catch (SqlException ex)
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void CreateALLBillsZip(DataSet BillList, string szFlag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ZipFile zipFiles = new ZipFile();
        DateTime now = DateTime.Now;
        string str = string.Concat(now.ToString("yyyyMMddHHmmssms"), ".zip");
        DateTime dateTime = DateTime.Now;
        string.Concat(dateTime.ToString("yyyyMMddHHmmssms"), "/");
        if (!Directory.Exists(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName())))
        {
            Directory.CreateDirectory(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName()));
        }
        Directory.SetCurrentDirectory(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName()));
        for (int i = 0; i < BillList.Tables[0].Rows.Count; i++)
        {
            string str1 = string.Concat(BillList.Tables[0].Rows[i]["SZ_CASE_ID"], "/");
            string str2 = "";
            str2 = (szFlag != "1" ? BillList.Tables[0].Rows[i]["sz_office"].ToString() : BillList.Tables[0].Rows[i]["SZ_PROCEDURE_GROUP"].ToString());
            string[] physicalPath = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, str2 };
            if (!Directory.Exists(string.Concat(physicalPath)))
            {
                string[] strArrays = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, str2 };
                Directory.CreateDirectory(string.Concat(strArrays));
            }
            if (!Directory.Exists(BillList.Tables[0].Rows[i]["SZ_BILL_PATH"].ToString()))
            {
                if (File.Exists(BillList.Tables[0].Rows[i]["SZ_BILL_PATH"].ToString()))
                {
                    string str3 = BillList.Tables[0].Rows[i]["SZ_BILL_PATH"].ToString();
                    string[] physicalPath1 = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, str2, "/", BillList.Tables[0].Rows[i]["SZ_BILL_FILE_NAME"].ToString() };
                    File.Copy(str3, string.Concat(physicalPath1), true);
                    try
                    {
                        zipFiles.AddFile(string.Concat(str1, str2, "/", BillList.Tables[0].Rows[i]["SZ_BILL_FILE_NAME"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                    }
                }
            }
            else if (File.Exists(string.Concat(BillList.Tables[0].Rows[i]["SZ_BILL_PATH"].ToString(), "/", BillList.Tables[0].Rows[i]["SZ_BILL_FILE_NAME"].ToString())))
            {
                string str4 = string.Concat(BillList.Tables[0].Rows[i]["SZ_BILL_PATH"].ToString(), "/", BillList.Tables[0].Rows[i]["SZ_BILL_FILE_NAME"].ToString());
                string[] strArrays1 = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, str2, "/", BillList.Tables[0].Rows[i]["SZ_BILL_FILE_NAME"].ToString() };
                File.Copy(str4, string.Concat(strArrays1), true);
                try
                {
                    zipFiles.AddFile(string.Concat(str1, str2, "/", BillList.Tables[0].Rows[i]["SZ_BILL_FILE_NAME"].ToString()));
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }
        }
        try
        {
            zipFiles.Save(str);
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

        Type type = base.GetType();
        string[] item = new string[] { "window.open('", ApplicationSettings.GetParameterValue("BillDownload"), "/", this.GetCompanyName(), str, "')" };
        ScriptManager.RegisterClientScriptBlock(this, type, "kk", string.Concat(item), true);

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void CreateBillsZip(DataSet BillList)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ZipFile zipFiles = new ZipFile();
        DateTime now = DateTime.Now;
        string str = string.Concat(now.ToString("yyyyMMddHHmmssms"), ".zip");
        DateTime dateTime = DateTime.Now;
        string.Concat(dateTime.ToString("yyyyMMddHHmmssms"), "/");
        if (!Directory.Exists(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName())))
        {
            Directory.CreateDirectory(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName()));
        }
        Directory.SetCurrentDirectory(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName()));
        for (int i = 0; i < BillList.Tables[0].Rows.Count; i++)
        {
            string str1 = string.Concat(BillList.Tables[0].Rows[i][4], "/");
            string[] physicalPath = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, BillList.Tables[0].Rows[i][1].ToString() };
            if (!Directory.Exists(string.Concat(physicalPath)))
            {
                string[] strArrays = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, BillList.Tables[0].Rows[i][1].ToString() };
                Directory.CreateDirectory(string.Concat(strArrays));
            }
            if (!Directory.Exists(BillList.Tables[0].Rows[i][2].ToString()))
            {
                if (File.Exists(BillList.Tables[0].Rows[i][2].ToString()))
                {
                    string str2 = BillList.Tables[0].Rows[i][2].ToString();
                    string[] physicalPath1 = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString() };
                    File.Copy(str2, string.Concat(physicalPath1), true);
                    try
                    {
                        zipFiles.AddFile(string.Concat(str1, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString()));
                    }
                    catch (Exception exception)
                    {
                        throw;
                    }
                }
            }
            else if (File.Exists(string.Concat(BillList.Tables[0].Rows[i][2].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString())))
            {
                string str3 = string.Concat(BillList.Tables[0].Rows[i][2].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString());
                string[] strArrays1 = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str1, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString() };
                File.Copy(str3, string.Concat(strArrays1), true);
                try
                {
                    zipFiles.AddFile(string.Concat(str1, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString()));
                }
                catch (Exception exception1)
                {
                    throw;
                }
            }
        }
        try
        {
            zipFiles.Save(str);
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
        Type type = base.GetType();
        string[] item = new string[] { "window.open('", ApplicationSettings.GetParameterValue("BillDownload"), "/", this.GetCompanyName(), str, "')" };
        ScriptManager.RegisterClientScriptBlock(this, type, "kk", string.Concat(item), true);
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void CreateZip(DataSet BillList)
    {
        string str = string.Concat(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, "/");
        ZipFile zipFiles = new ZipFile();
        DateTime now = DateTime.Now;
        string str1 = string.Concat(now.ToString("yyyyMMddHHmmssms"), ".zip");
        DateTime dateTime = DateTime.Now;
        string.Concat(dateTime.ToString("yyyyMMddHHmmssms"), "/");
        if (!Directory.Exists(string.Concat(this.getPhysicalPath(), "BillDownload")))
        {
            Directory.CreateDirectory(string.Concat(this.getPhysicalPath(), "BillDownload"));
        }
        if (!Directory.Exists(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName())))
        {
            Directory.CreateDirectory(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName()));
        }
        if (!Directory.Exists(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str)))
        {
            Directory.CreateDirectory(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str));
        }
        Directory.SetCurrentDirectory(string.Concat(this.getPhysicalPath(), "BillDownload/", this.GetCompanyName()));
        for (int i = 0; i < BillList.Tables[0].Rows.Count; i++)
        {
            string[] physicalPath = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str, BillList.Tables[0].Rows[i][1].ToString() };
            if (!Directory.Exists(string.Concat(physicalPath)))
            {
                string[] strArrays = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str, BillList.Tables[0].Rows[i][1].ToString() };
                Directory.CreateDirectory(string.Concat(strArrays));
            }
            string[] physicalPath1 = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString() };
            if (File.Exists(string.Concat(physicalPath1)))
            {
                zipFiles.AddFile(string.Concat(str, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString()));
            }
            else if (File.Exists(BillList.Tables[0].Rows[i][2].ToString()))
            {
                string str2 = BillList.Tables[0].Rows[i][2].ToString();
                string[] strArrays1 = new string[] { this.getPhysicalPath(), "BillDownload/", this.GetCompanyName(), str, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString() };
                File.Copy(str2, string.Concat(strArrays1));
                zipFiles.AddFile(string.Concat(str, BillList.Tables[0].Rows[i][1].ToString(), "/", BillList.Tables[0].Rows[i][3].ToString()));
            }
        }
        zipFiles.Save(str1);
        Type type = base.GetType();
        string[] item = new string[] { "window.open('", ApplicationSettings.GetParameterValue("BillDownload"), "/", this.GetCompanyName(), str1, "')" };
        ScriptManager.RegisterClientScriptBlock(this, type, "kk", string.Concat(item), true);
    }

    protected void ddlAll_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.ddlAll.SelectedValue == "1")
            {
                BillSearchDAO billSearchDAO = new BillSearchDAO();
                string[] strArrays = billSearchDAO.BillName(this.Session["PAY_MENT_BILL_NO"].ToString(), this.txtCompanyID.Text, "CARRIER").Split(new char[] { ',' });
                this.Session["Type"] = "1";
                this.txtAll.Text = strArrays[0].ToString();
                this.txtPaymentvalue.Text = strArrays[1].ToString();
                this.txtAll.Enabled = false;
                this.ModalPopupExtender1.Show();
            }
            else if (this.ddlAll.SelectedValue == "2")
            {
                BillSearchDAO billSearchDAO1 = new BillSearchDAO();
                string[] strArrays1 = billSearchDAO1.BillName(this.Session["PAY_MENT_BILL_NO"].ToString(), this.txtCompanyID.Text, "LAWFIRM").Split(new char[] { ',' });
                this.txtAll.Text = strArrays1[0].ToString();
                this.txtPaymentvalue.Text = strArrays1[1].ToString();
                this.Session["Type"] = "2";
                this.txtAll.Enabled = false;
                this.ModalPopupExtender1.Show();
            }
            else if (this.ddlAll.SelectedValue == "3")
            {
                this.txtAll.Text = "";
                this.ModalPopupExtender1.Show();
                this.Session["Type"] = "3";
                this.txtAll.Enabled = true;
            }
            else if (this.ddlAll.SelectedValue == "NA")
            {
                this.Session["Type"] = "0";
                this.txtAll.Text = "";
                this.txtAll.Enabled = false;
                this.ModalPopupExtender1.Show();
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

    protected void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.ddlAmount.SelectedValue == "---Select---")
            {
                this.txtToAmt.Visible = false;
                this.txtAmount.Text = "";
                this.txtAmount.Visible = false;
                this.lblfrom.Visible = false;
                this.lblFamt.Visible = false;
                this.lblTamt.Visible = false;
            }
            else if (this.ddlAmount.SelectedValue == "0")
            {
                this.txtToAmt.Visible = true;
                this.txtAmount.Visible = true;
                this.lblFamt.Visible = true;
                this.lblTamt.Visible = true;
                this.lblfrom.Visible = true;
            }
            else if (this.ddlAmount.SelectedValue == ">" || this.ddlAmount.SelectedValue == "<" || this.ddlAmount.SelectedValue == "=")
            {
                this.txtAmount.Visible = true;
                this.lblFamt.Visible = true;
                this.lblfrom.Visible = false;
                this.txtToAmt.Visible = false;
                this.lblTamt.Visible = false;
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

    protected void extddlInsurance_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.extddlInsurance.Text == "NA")
        {
            this.txtInsuranceCompany.Text = "";
            return;
        }
        this.txtInsuranceCompany.Text = this.extddlInsurance.Selected_Text;
    }

    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
        if (this.extddlPatient.Text == "NA")
        {
            this.txtPatientName.Text = "";
            return;
        }
        this.txtPatientName.Text = this.extddlPatient.Selected_Text;
    }

    public string GetCompanyName()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        string str = "";
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GET_COMPANY_NAME", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                str = dataSet.Tables[0].Rows[0][0].ToString();
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return string.Concat(str, "/");
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetDenialInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_MANAGE_GET_DENIAL_REASON", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
            }
            catch (SqlException ex)
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public string GetFirstBillStatus(string szBillno, string szCompanyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_GET_BILL_STATUS", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillno);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANYID", szCompanyId);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader[0].ToString();
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
        }
        finally
        {
            sqlConnection.Close();
        }
        return str;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private DataSet GetInsuraceDetails(string companyId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet;
        SqlConnection sqlConnection = null;
        string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            try
            {
                sqlConnection = new SqlConnection(str);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(string.Concat("Select * from mst_cyclic_insurance where sz_company_id ='", companyId, "'"), sqlConnection)
                {
                    Connection = sqlConnection
                };
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet dataSet1 = new DataSet();
                sqlDataAdapter.Fill(dataSet1);
                dataSet = dataSet1;
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
                return null;
            }
        }
        finally
        {
            sqlConnection.Close();
            //SVN
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected DataSet GetListOfBills(string Flag)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection = new SqlConnection(str);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_GET_BILLS_BY_CASE_ID", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                sqlCommand.Parameters.AddWithValue("@FLAG", Flag);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
            }
            catch (SqlException ex)
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetPatientDeskList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
        try
        {
            DataSet dataSet = new DataSet();
            dataSet = billSysPatientBO.GetPatienDeskList("GETPATIENTLIST", this.CasID);
            DataTable item = dataSet.Tables[0];
            this.DtlView.DataSource = dataSet;
            this.DtlView.DataBind();
            this.DtlView.Visible = true;
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

    public string getPhysicalPath()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = "";
        SqlConnection sqlConnection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = (new SqlCommand("select PhysicalBasePath from tblBasePath where BasePathId=(Select ParameterValue from tblapplicationsettings where parametername = 'BasePathId')", sqlConnection)).ExecuteReader();
                while (sqlDataReader.Read())
                {
                    str = sqlDataReader["PhysicalBasePath"].ToString();
                }
            }
            catch (SqlException ex)
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return str;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetVerification(string sz_CompanyID, string sz_input_bill_number)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlConnection = new SqlConnection(str);
        sqlConnection = new SqlConnection(str);
        DataSet dataSet = new DataSet();
        try
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
                sqlCommand.Parameters.AddWithValue("@sz_input_bill_number", sz_input_bill_number);
                sqlCommand.Parameters.AddWithValue("@bt_operation", 2);
                (new SqlDataAdapter(sqlCommand)).Fill(dataSet);
            }
            catch (SqlException ex)
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
        finally
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        return dataSet;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdBillSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (e.CommandName.ToString() == "Generate bill")
        {
            Bill_Sys_NF3_Template billSysNF3Template = new Bill_Sys_NF3_Template();
            DataSet dataSet = new DataSet();
            dataSet = billSysNF3Template.getBillList(e.CommandArgument.ToString());
            if (dataSet.Tables[0].Rows.Count <= 1)
            {
                if (dataSet.Tables[0].Rows.Count != 1)
                {
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", "alert('No bill generated ...!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "starScript", string.Concat("window.open('", dataSet.Tables[0].Rows[0]["PATH"].ToString(), "');"), true);
                }
            }
        }
        if (e.CommandName == "Bill Notes")
        {
            int num = Convert.ToInt32(e.CommandArgument.ToString());
            this.BillNo = this.grdBillSearch.DataKeys[num]["SZ_BILL_NUMBER"].ToString();
            string text = this.txtBillNotes.Text;
            string insertNotes = (new BillSearchDAO()).GetInsertNotes(text, this.BillNo);
            this.txtBillNotes.Text = insertNotes;
            this.lblNotesBillno.Text = this.BillNo;
            this.ModalPopupExtender2.Show();
        }
        if (e.CommandName == "Make Payment")
        {
            try
            {
                this.Session["LinkClicked"] = true;
                int num1 = Convert.ToInt32(e.CommandArgument.ToString());
                //this.Session["num1"] = this.grdBillSearch.DataKeys[num1];
                this.Session.Add("PROC_DATE", this.grdBillSearch.DataKeys[num1]["PROC_DATE"].ToString());
                string str = this.grdBillSearch.DataKeys[num1]["PROC_DATE"].ToString();
                this.Session["PROC_DATE"] = str;
                this.Session.Add("SZ_BILL_NUMBER", this.grdBillSearch.DataKeys[num1]["SZ_BILL_NUMBER"].ToString());
                lblBillNo.Text = this.BillNo = this.grdBillSearch.DataKeys[num1]["SZ_BILL_NUMBER"].ToString();
                this.Session["PAY_MENT_BILL_NO"] = this.BillNo;
                this.lblPosteddate.Text = DateTime.Today.ToShortDateString();
                this.lblBillNo.Text = this.BillNo;
                this.lblVisitDate.Text = this.grdBillSearch.DataKeys[num1]["PROC_DATE"].ToString();
                Label label = this.lblBalance;
                //this.Session.Add("FLT_BALANCE", this.grdBillSearch.DataKeys[num1]["FLT_BALANCE"].ToString());
                decimal num2 = Convert.ToDecimal(this.grdBillSearch.DataKeys[num1]["FLT_BALANCE"].ToString());
                label.Text = num2.ToString();
                this.hdnBal.Value = this.lblBalance.Text;
                this.txtPaymentDate.Text = this.lblPosteddate.Text;
                this.btnUpdate.Enabled = false;
                this.btnSave.Enabled = true;
                this.txtCompanyID1.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.txtCaseID.Text = this.grdBillSearch.DataKeys[num1]["SZ_CASE_ID"].ToString();
                this.Session["CID"] = this.grdBillSearch.DataKeys[num1]["SZ_CASE_ID"].ToString();
                this.Session["CNO"] = this.grdBillSearch.DataKeys[num1]["SZ_CASE_NO"].ToString();
                this.Session["Speciality"] = this.grdBillSearch.DataKeys[num1]["SPECIALITY_ID"].ToString();
                this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                this.btnSave.Enabled = true;
                this.btnUpdate.Enabled = false;
                this.ClearPaymentControl();
                this.BindGrid(this.BillNo);
                DataSet dataSet1 = (new BillSearchDAO()).paymenttype(this.BillNo, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.ddlAll.DataSource = dataSet1;
                ListItem listItem = new ListItem("---select---", "NA");
                this.ddlAll.DataTextField = "DES";
                this.ddlAll.DataValueField = "ID";
                this.ddlAll.DataBind();
                this.ddlAll.Items.Insert(0, listItem);
                this.ddlAll.Visible = true;
                this.txtAll.Enabled = false;
                this.txtBillNo.Text = this.BillNo;
                this.ModalPopupExtender1.Show();
                //if (base.Request.QueryString["fromCase"] != null)
                //    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?fromCase=" + base.Request.QueryString["fromCase"] + "&billNo=" + lblBillNo.Text + "&SHOW=true';", true);
                //else
                //    ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Done", "window.location='/AJAX%20Pages/Bill_Sys_BillSearch.aspx?billNo=" + lblBillNo.Text + "&&SHOW=true';", true);
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
        if (e.CommandName.ToString() == "OTPT")
        {
            int num3 = Convert.ToInt32(e.CommandArgument.ToString());
            string str1 = this.grdBillSearch.DataKeys[num3]["SZ_BILL_NUMBER"].ToString();
            string str2 = this.grdBillSearch.DataKeys[num3]["SZ_CASE_ID"].ToString();
            Type type = base.GetType();
            string[] strArrays = new string[] { "showOTPTinfoPopup('", str1, "','", str2, "');" };
            ScriptManager.RegisterClientScriptBlock(this, type, "mmupdateproc", string.Concat(strArrays), true);
        }
        int num4 = 0;
        if (e.CommandName.ToString() == "PLS")
        {
            for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
            {
                LinkButton linkButton = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkM");
                LinkButton linkButton1 = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkP");
                if (linkButton.Visible)
                {
                    linkButton.Visible = false;
                    linkButton1.Visible = true;
                }
            }
            //30 -31 and 31- 32 -t
            this.grdBillSearch.Columns[32].Visible = false;
            this.grdBillSearch.Columns[33].Visible = true;
            this.grdBillSearch.Columns[34].Visible = false;
            this.grdBillSearch.Columns[35].Visible = false;
            num4 = Convert.ToInt32(e.CommandArgument);
            string str3 = "div";
            str3 = string.Concat(str3, this.grdBillSearch.DataKeys[num4][0].ToString());
            GridView verification = (GridView)this.grdBillSearch.Rows[num4].FindControl("grdVerification");
            LinkButton linkButton2 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkP");
            LinkButton linkButton3 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkM");
            string str4 = string.Concat("'", this.grdBillSearch.DataKeys[num4][0].ToString(), "'");
            string text1 = this.txtCompanyID.Text;
            verification.DataSource = this.GetVerification(text1, str4);
            verification.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mp", string.Concat("ShowChildGrid('", str3, "') ;"), true);
            linkButton2.Visible = false;
            linkButton3.Visible = true;
            LinkButton linkButton4 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkDM");
            LinkButton linkButton5 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkDP");
            linkButton5.Visible = true;
            if (linkButton4.Visible)
            {
                linkButton4.Visible = false;
                linkButton5.Visible = true;
            }
        }
        if (e.CommandName.ToString() == "MNS")
        {
            /// 30 to 31 -t
            this.grdBillSearch.Columns[32].Visible = true;
            this.grdBillSearch.Columns[33].Visible = false;
            this.grdBillSearch.Columns[34].Visible = false;
            this.grdBillSearch.Columns[35].Visible = false;
            num4 = Convert.ToInt32(e.CommandArgument);
            string str5 = "div";
            str5 = string.Concat(str5, this.grdBillSearch.DataKeys[num4][0].ToString());
            LinkButton linkButton6 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkP");
            LinkButton linkButton7 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkM");
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("HideChildGrid('", str5, "') ;"), true);
            linkButton6.Visible = true;
            linkButton7.Visible = false;
        }
        if (e.CommandName.ToString() == "DenialPLS")
        {
            for (int j = 0; j < this.grdBillSearch.Rows.Count; j++)
            {
                LinkButton linkButton8 = (LinkButton)this.grdBillSearch.Rows[j].FindControl("lnkDM");
                LinkButton linkButton9 = (LinkButton)this.grdBillSearch.Rows[j].FindControl("lnkDP");
                if (linkButton8.Visible)
                {
                    linkButton8.Visible = false;
                    linkButton9.Visible = true;
                }
            }
            //31 to 32 and 30 to 31
            this.grdBillSearch.Columns[32].Visible = false;
            this.grdBillSearch.Columns[33].Visible = false;
            this.grdBillSearch.Columns[34].Visible = true;
            this.grdBillSearch.Columns[35].Visible = false;
            num4 = Convert.ToInt32(e.CommandArgument);
            string str6 = "div1";
            str6 = string.Concat(str6, this.grdBillSearch.DataKeys[num4][0].ToString());
            GridView denialInfo = (GridView)this.grdBillSearch.Rows[num4].FindControl("grdDenial");
            LinkButton linkButton10 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkDP");
            LinkButton linkButton11 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkDM");
            string str7 = this.grdBillSearch.DataKeys[num4][0].ToString();
            string text2 = this.txtCompanyID.Text;
            denialInfo.DataSource = this.GetDenialInfo(text2, str7);
            denialInfo.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mp", string.Concat("ShowDenialChildGrid('", str6, "') ;"), true);
            linkButton10.Visible = false;
            linkButton11.Visible = true;
            LinkButton linkButton12 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkM");
            LinkButton linkButton13 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkP");
            if (linkButton12.Visible)
            {
                linkButton12.Visible = false;
                linkButton13.Visible = true;
            }
        }
        if (e.CommandName.ToString() == "DenialMNS")
        {
            //31-32 and 30 to31
            this.grdBillSearch.Columns[32].Visible = true;
            this.grdBillSearch.Columns[33].Visible = false;
            this.grdBillSearch.Columns[34].Visible = false;
            this.grdBillSearch.Columns[35].Visible = false;
            num4 = Convert.ToInt32(e.CommandArgument);
            string str8 = "div1";
            str8 = string.Concat(str8, this.grdBillSearch.DataKeys[num4][0].ToString());
            LinkButton linkButton14 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkDP");
            LinkButton linkButton15 = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkDM");
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("HideDenialChildGrid('", str8, "') ;"), true);
            linkButton14.Visible = true;
            linkButton15.Visible = false;
        }
        if (e.CommandName.ToString() == "EorPLS")
        {
            HideTemplates(this.grdBillSearch);
            //31 to 32 and 30 to 31
            this.grdBillSearch.Columns[32].Visible = false;
            this.grdBillSearch.Columns[33].Visible = false;
            this.grdBillSearch.Columns[34].Visible = false;
            this.grdBillSearch.Columns[35].Visible = true;
            num4 = Convert.ToInt32(e.CommandArgument);
            string str6 = "div2";
            str6 = string.Concat(str6, this.grdBillSearch.DataKeys[num4][0].ToString());
            GridView EorInfo = (GridView)this.grdBillSearch.Rows[num4].FindControl("grdEOR");
            LinkButton lnkEP = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkEP");
            LinkButton lnkEM = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkEM");
            string str7 = this.grdBillSearch.DataKeys[num4][0].ToString();
            string text2 = this.txtCompanyID.Text;
            EorInfo.DataSource = this.GetEORInfo(text2, str7);
            EorInfo.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mp", string.Concat("ShowDenialChildGrid('", str6, "') ;"), true);
            lnkEP.Visible = false;
            lnkEM.Visible = true;
        }
        if (e.CommandName.ToString() == "EorMNS")
        {
            //31-32 and 30 to31
            this.grdBillSearch.Columns[32].Visible = true;
            this.grdBillSearch.Columns[33].Visible = false;
            this.grdBillSearch.Columns[34].Visible = false;
            this.grdBillSearch.Columns[35].Visible = false;
            num4 = Convert.ToInt32(e.CommandArgument);
            string str8 = "div2";
            str8 = string.Concat(str8, this.grdBillSearch.DataKeys[num4][0].ToString());
            LinkButton lnkEP = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkEP");
            LinkButton lnkEM = (LinkButton)this.grdBillSearch.Rows[num4].FindControl("lnkEM");
            ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("HideDenialChildGrid('", str8, "') ;"), true);
            lnkEP.Visible = true;
            lnkEM.Visible = false;
        }

        if (e.CommandName.ToString() == "verificationDocs")
        {
            int num5 = Convert.ToInt32(e.CommandArgument.ToString());
            base.Response.Redirect(string.Concat("~/AJAX Pages/Bill_sys_openVerificationDocs.aspx?bno='", this.grdBillSearch.DataKeys[num5]["SZ_BILL_NUMBER"].ToString(), "'&doctype=1"));
        }
        if (e.CommandName.ToString() == "DenialDocs")
        {
            base.Response.Redirect("~/AJAX Pages/Bill_sys_openVerificationDocs.aspx?bno='rr1'&doctype='1'");
        }
        for (int k = 0; k < this.grdBillSearch.Rows.Count; k++)
        {
            if (this.grdBillSearch.DataKeys[k]["SZ_BILL_NOTES"].ToString() != "" && this.grdBillSearch.DataKeys[k]["SZ_BILL_NOTES"].ToString() != "&nbsp;")
            {
                LinkButton red = (LinkButton)this.grdBillSearch.Rows[k].FindControl("lnkBillNotes");
                red.ForeColor = Color.Red;
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPaymentTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        this.txtPaymentID.Text = e.Item.Cells[1].Text;
        this.lblBillNo.Text = e.Item.Cells[2].Text;
        this.txtChequeNumber.Text = e.Item.Cells[3].Text;
        this.lblPosteddate.Text = e.Item.Cells[4].Text;
        this.txtChequeDate.Text = e.Item.Cells[5].Text;
        ddlAll.SelectedValue = e.Item.Cells[17].Text;
        txtAll.Text = e.Item.Cells[18].Text;
        this.Session["Type"] = e.Item.Cells[17].Text;
        if (e.Item.Cells[7].Text.ToString() != "Write-off")
        {
            this.chkTypeWriteOff.Checked = false;
        }
        else
        {
            this.chkTypeWriteOff.Checked = true;
            this.rdbList.SelectedValue = "2";
        }
        string str = e.Item.Cells[6].Text.ToString().Replace('$', ' ').Trim();
        if (str != "0.00")
        {
            this.txtChequeAmount.Text = str;
            this.txtPrev.Text = str;
        }
        else
        {
            this.txtChequeAmount.Text = "0";
        }
        str = e.Item.Cells[8].Text.ToString().Replace('$', ' ').Trim();
        if (str != "0.00")
        {
            this.txtInterestAmount.Text = str;
        }
        else
        {
            this.txtInterestAmount.Text = "0";
        }
        if (!e.Item.Cells[9].Text.Equals("&nbsp;"))
        {
            this.txtDescription.Text = e.Item.Cells[9].Text;
        }
        else
        {
            this.txtDescription.Text = "";
        }
        this.btnSave.Enabled = false;
        this.btnUpdate.Enabled = true;
        this.ModalPopupExtender1.Show();
        if (e.CommandName.ToString() == "Denial")
        {
            this.txtbillnumber.Text = e.Item.Cells[2].Text;
            this.ModalPopupExtender1.Hide();
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("window.location.href ='", ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString(), this.grdBillSearch.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()), "';"), true);
    }

    protected void lnkscan_Click(object sender, EventArgs e)
    {
        int selectedIndex = this.grdPaymentTransaction.SelectedIndex;
        DataGridItem parent = (DataGridItem)((TableCell)((LinkButton)sender).Parent).Parent;
        int itemIndex = parent.ItemIndex;
        this.Session["scanpayment_No"] = parent.Cells[1].Text;
        ArrayList arrayLists = new ArrayList();
        arrayLists.Add(this.lblBillNo.Text);
        arrayLists.Add(this.lblVisitDate.Text);
        arrayLists.Add(this.lblBalance.Text);
        arrayLists.Add(this.lblPosteddate.Text);
        arrayLists.Add(this.txtChequeDate.Text);
        arrayLists.Add(this.txtChequeNumber.Text);
        arrayLists.Add(this.txtChequeAmount.Text);
        arrayLists.Add(this.txtDescription.Text);
        arrayLists.Add(this.txtCompanyID1.Text);
        arrayLists.Add(this.txtBal.Text);
        arrayLists.Add(this.txtID.Text);
        if (this.iFlag != 1)
        {
            arrayLists.Add("0");
        }
        else
        {
            arrayLists.Add("1");
        }
        this.Session["modal"] = arrayLists;
        this.RedirectToScanApp(selectedIndex);
    }

    protected void lnkuplaod_Click(object sender, EventArgs e)
    {
        DataGridItem parent = (DataGridItem)((TableCell)((LinkButton)sender).Parent).Parent;
        int itemIndex = parent.ItemIndex;
        this.Session["payment_No"] = parent.Cells[1].Text;
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", "showUploadFilePopup();", true);
        this.BindGrid(this.lblBillNo.Text);
        this.ModalPopupExtender1.Hide();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        if (base.Request.QueryString["billNo"] != null && this.Session["LinkClicked"] == null && !Page.IsPostBack)
            lblBillNo.Text = base.Request.QueryString["billNo"].ToString();
        
        //this.rdbList.Attributes.Add("onClick", "return ValidateInterestAmount()");
        this.txtChequeAmount.Attributes.Add("onKeyPress", "return CheckForInteger(event,'.')");
        this.ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_MG2_Display.ToString() != "1")
        {
            this.grdBillSearch.Columns[28].Visible = false;
        }
        else
        {
            this.grdBillSearch.Columns[28].Visible = true;
        }
        if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_HP1_Display.ToString() != "1")
        {
            this.grdBillSearch.Columns[29].Visible = false;
        }
        else
        {
            this.grdBillSearch.Columns[29].Visible = true;
        }
        // 29 to 30 and 30 to 31-t
        //this.grdBillSearch.Columns[31].Visible = true;
        this.grdBillSearch.Columns[32].Visible = true;
        this.grdBillSearch.Columns[33].Visible = false;
        this.grdBillSearch.XGridDataset = null;
        try
        {
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            this.ddlVisit.Attributes.Add("onChange", "javascript:SetVisitDate();");
            this.btnSave.Attributes.Add("onclick", "return ooValidate();");
            this.btnSearch.Attributes.Add("onclick", "return checkAmountBox();");
            this.btn_Download_id.Attributes.Add("onclick", "return CheckSelect();");
            this.btn_Download_All.Attributes.Add("onclick", "return ConfirmAll();");
            this.btnUpdate.Attributes.Add("onclick", "return val_CheckControls();");
            this.extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.drdUpdateStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnUpdateStatus.Attributes.Add("onclick", "return confirm_update_bill_status();");
            this.Button1.Attributes.Add("onclick", "return confirm_bill_delete();");
            this.btnDelete.Attributes.Add("onclick", "return confirm_Payment_bill_delete();");
            //this.ddlPayemntType.Attributes.Add("onchange", "return Validate();");
            this.con.SourceGrid = this.grdBillSearch;
            this.txtSearchBox.SourceGrid = this.grdBillSearch;
            this.grdBillSearch.Page = this.Page;
            this.grdBillSearch.PageNumberList = this.con;
            this.btn_BillPacket.Attributes.Add("onclick", "return confirm_packet_bill_status();");
            this.btn_PacketDocument.Attributes.Add("onclick", "return confirm_packet_bill_status();");
            this.btn_Both.Attributes.Add("onclick", "return confirm_packet_bill_status();");
            if (base.Request.QueryString["CaseID"] != null)
            {
                Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject();
                CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                billSysCaseObject.SZ_PATIENT_ID = caseDetailsBO.GetCasePatientID(base.Request.QueryString["CaseID"].ToString(), "");
                billSysCaseObject.SZ_CASE_ID = base.Request.QueryString["CaseID"].ToString();
                billSysCaseObject.SZ_PATIENT_NAME = base.Request.QueryString["pname"].ToString();
                billSysCaseObject.SZ_CASE_NO = caseDetailsBO.GetCaseNo(billSysCaseObject.SZ_CASE_ID, base.Request.QueryString["cmpid"].ToString());
                this.Session["CASE_OBJECT"] = billSysCaseObject;
                Bill_Sys_Case billSysCase = new Bill_Sys_Case()
                {
                    SZ_CASE_ID = base.Request.QueryString["CaseID"].ToString()
                };
                this.Session["CASEINFO"] = billSysCase;
            }
            if (!base.IsPostBack)
            {

                DataSet insuraceDetails = this.GetInsuraceDetails(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.Session["InsuranceDetails"] = insuraceDetails;
                this.extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.txtAmount.Visible = false;
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_DELETE_BILLS != "True")
                {
                    this.btnDelete.Visible = false;
                }
                if (base.Request.QueryString["fromCase"] == null)
                {
                    this.btn_Download_Bill.Visible = false;
                    this.ddlDateValues.SelectedValue = "3";
                    string str = DateTime.Now.Month.ToString();
                    string str1 = DateTime.Now.Year.ToString();
                    int num = Convert.ToInt32(str);
                    int num1 = Convert.ToInt32(str1);
                    int num2 = DateTime.DaysInMonth(num1, num);
                    this.txtFromDate.Text = string.Concat(str, "/1/", num1);
                    TextBox textBox = this.txtToDate;
                    object[] objArray = new object[] { str, "/", num2, "/", num1 };
                    textBox.Text = string.Concat(objArray);
                    ScriptManager.RegisterStartupScript(this, base.GetType(), "kk11", "Showdiv();", true);
                    this.grdBillSearch.XGridDatasetNumber = 2;
                    this.grdBillSearch.XGridBindSearch();
                    DataTable dataTable = new DataTable();
                    dataTable = this.grdBillSearch.XGridDataset;
                    this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
                    this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
                    this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
                    this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
                    this.btnClear1.Visible = true;
                    this.Session["Billflag"] = "BillSearch";
                    this.PusSignColorChange();
                }
                else if (base.Request.QueryString["fromCase"].ToString() == "true")
                {
                    this.btn_Download_id.Visible = false;
                    this.btn_Download_All.Visible = false;
                    this.rdoDownload.Visible = false;
                    if (this.Session["CASE_OBJECT"] != null)
                    {
                        this.CasID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                        this.iFlag = 1;
                        this.hdnDenialflag.Value = this.iFlag.ToString();
                        this.txtCasNO.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                        this.txtCasNO.ReadOnly = true;
                        this.btnClear2.Visible = true;
                        this.GetPatientDeskList();
                        ScriptManager.RegisterStartupScript(this, base.GetType(), "kk11", "Showdiv();", true);
                        this.grdBillSearch.XGridDatasetNumber = 2;
                        this.grdBillSearch.XGridBindSearch();
                        DataTable xGridDataset = new DataTable();
                        xGridDataset = this.grdBillSearch.XGridDataset;
                        this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][0].ToString()), 2)));
                        this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][1].ToString()), 2)));
                        this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][2].ToString()), 2)));
                        this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(xGridDataset.Rows[0][3].ToString()), 2)));
                        this.Session["Billflag"] = "BillSearch1";
                        this.PusSignColorChange();
                    }
                }
                this.txtGroupId.Text = this.extddlSpeciality.Text;
                this.txtBillStatusID.Text = this.extddlBillStatus.Text;
                if (this.Session["modal"] != null)
                {
                    ArrayList arrayLists = new ArrayList();
                    arrayLists = (ArrayList)this.Session["modal"];
                    this.lblBillNo.Text = arrayLists[0].ToString();
                    this.lblVisitDate.Text = arrayLists[1].ToString();
                    this.lblBalance.Text = arrayLists[2].ToString();
                    this.lblPosteddate.Text = arrayLists[3].ToString();
                    this.txtChequeDate.Text = arrayLists[4].ToString();
                    this.txtChequeNumber.Text = arrayLists[5].ToString();
                    this.txtChequeAmount.Text = arrayLists[6].ToString();
                    this.txtDescription.Text = arrayLists[7].ToString();
                    this.txtCompanyID1.Text = arrayLists[8].ToString();
                    this.txtBal.Text = arrayLists[9].ToString();
                    this.txtID.Text = arrayLists[10].ToString();
                    this.hdnBillStatusid.Value = this.txtID.Text;
                    this.BindGrid(this.lblBillNo.Text);
                    this.ModalPopupExtender1.Show();
                    this.Session["modal"] = null;
                }
                for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
                {
                    if (this.grdBillSearch.Rows[i].Cells[7].Equals("Billed By BSBG"))
                    {
                        this.grdBillSearch.Rows[i].BackColor = Color.LawnGreen;
                    }
                    if (this.grdBillSearch.Rows[i].Cells[7].Equals("Billed By KH"))
                    {
                        this.grdBillSearch.Rows[i].BackColor = Color.Yellow;
                    }
                    if (this.grdBillSearch.DataKeys[i]["SZ_BILL_NOTES"].ToString() != "" && this.grdBillSearch.DataKeys[i]["SZ_BILL_NOTES"].ToString() != "&nbsp;")
                    {
                        LinkButton red = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkBillNotes");
                        red.ForeColor = Color.Red;
                    }
                }
                ScriptManager.RegisterStartupScript(this, base.GetType(), "kk11", "Hidediv();", true);
                if (base.Request.QueryString["PDELETE"] != null || base.Request.QueryString["PADD"] != null)
                {


                    //DataKey num1 = this.Session["num1"] as DataKey;
                    string str = Session["PROC_DATE"].ToString();
                    this.Session["PROC_DATE"] = str;
                    this.BillNo = Session["SZ_BILL_NUMBER"].ToString();
                    this.Session["PAY_MENT_BILL_NO"] = this.BillNo;
                    this.lblPosteddate.Text = DateTime.Today.ToShortDateString();
                    this.lblBillNo.Text = this.BillNo;
                    this.lblVisitDate.Text = Session["PROC_DATE"].ToString();
                    Label label = this.lblBalance;
                    decimal num2 = Convert.ToDecimal(Session["FLT_BALANCE"].ToString());
                    label.Text = num2.ToString();
                    this.hdnBal.Value = this.lblBalance.Text;
                    this.txtPaymentDate.Text = this.lblPosteddate.Text;
                    this.btnUpdate.Enabled = false;
                    this.btnSave.Enabled = true;
                    this.txtCompanyID1.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.txtCaseID.Text = Session["CID"].ToString();
                    //this.Session["CID"] = num1["SZ_CASE_ID"].ToString();
                    //this.Session["CNO"] = num1["SZ_CASE_NO"].ToString();
                    //this.Session["Speciality"] = num1["SPECIALITY_ID"].ToString();
                    this.txtUserID.Text = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                    this.btnSave.Enabled = true;
                    this.btnUpdate.Enabled = false;
                    this.ClearPaymentControl();
                    this.BindGrid(this.BillNo);
                    DataSet dataSet1 = (new BillSearchDAO()).paymenttype(this.BillNo, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.ddlAll.DataSource = dataSet1;
                    ListItem listItem = new ListItem("---select---", "NA");
                    this.ddlAll.DataTextField = "DES";
                    this.ddlAll.DataValueField = "ID";
                    this.ddlAll.DataBind();
                    this.ddlAll.Items.Insert(0, listItem);
                    this.ddlAll.Visible = true;
                    this.txtAll.Enabled = false;
                    this.txtBillNo.Text = this.BillNo;

                    this.ModalPopupExtender1.Show();

                }

            }
            bool isPostBack = this.Page.IsPostBack;

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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string str = "";
        CheckBox checkBox = null;
        for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
        {
            this.grdBillSearch.DataKeys[i]["SZ_ABBRIVATION_ID"].ToString();
            str = this.grdBillSearch.DataKeys[i]["SZ_BILL_STATUS_NAME"].ToString();
            this.grdBillSearch.DataKeys[i]["SZ_INSURANCE_ID"].ToString();
            if (str != null)
            {
                str = str.Replace("&nbsp;", "");
                if (str.Trim().ToString() == "Transferred" || str.Trim().ToString() == "Bill Downloaded" || str.Trim().ToString() == "Sold" || str.Trim().ToString() == "Loaned" || str.Trim().ToString() == "Bill Rejected By Lawfirm")
                {
                    checkBox = (CheckBox)this.grdBillSearch.Rows[i].FindControl("ChkDelete");
                    checkBox.Enabled = false;
                }
            }//36to 37 -t
            if (this.grdBillSearch.Rows[i].Cells[38].Text.ToString() != "" && this.grdBillSearch.Rows[i].Cells[38].Text.ToString() != "&nbsp;")
            {
                LinkButton red = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkBillNotes");
                red.ForeColor = Color.Red;
            }

            //HPJ1 Form template for casetype-WC
            if(grdBillSearch.DataKeys[i]["SZ_ABBRIVATION_ID"].ToString() == "WC000000000000000001")
            {
                LinkButton lnk = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkHPJ1");
                lnk.Visible = true;
            }
            else
            {
                LinkButton lnkbtn = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkHPJ1");
                lnkbtn.Visible = false;
            }
        }
        this.PusSignColorChange();
        this.visibleDocumentLink();
        if (base.Request.QueryString["PDELETE"] != null && this.Session["LinkClicked"] == null && !Page.IsPostBack)
        {
            this.usrMessage1.PutMessage(base.Request.QueryString["PDELETE"].ToString());
            this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage1.Show();
        }
        if (base.Request.QueryString["PADD"] != null && this.Session["LinkClicked"] == null && !Page.IsPostBack)
        {
            this.usrMessage1.PutMessage(base.Request.QueryString["PADD"].ToString());
            this.usrMessage1.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage1.Show();
        }

    }

    public void PusSignColorChange()
    {
        for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
        {
            string str = this.grdBillSearch.DataKeys[i][10].ToString();
            string str1 = this.grdBillSearch.DataKeys[i][11].ToString();
            string str2 = this.grdBillSearch.DataKeys[i][12].ToString();
            LinkButton red = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkM");
            LinkButton linkButton = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkP");
            LinkButton red1 = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkDM");
            LinkButton linkButton1 = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkDP");
            LinkButton red2 = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkEM");
            LinkButton linkButton2 = (LinkButton)this.grdBillSearch.Rows[i].FindControl("lnkEP");
            if (str.ToLower() == "true" || str == "1")
            {
                linkButton1.ForeColor = Color.Red;
                red1.ForeColor = Color.Red;
            }
            else linkButton1.Visible = false;

            if (str1.ToLower() == "true" || str1 == "1")
            {
                linkButton.ForeColor = Color.Red;
                red.ForeColor = Color.Red;
            }
            else linkButton.Visible = false;

            if (str2.ToLower() == "true" || str2 == "1")
            {
                linkButton2.ForeColor = Color.Red;
                red2.ForeColor = Color.Red;
            }
            else linkButton2.Visible = false;
        }
    }

    public void RedirectToScanApp(int iindex)
    {
        string str = this.Session["Billflag"].ToString();
        iindex = this.grdPaymentTransaction.SelectedIndex;
        string str1 = ConfigurationManager.AppSettings["webscanurl"].ToString();
        string patientName = (new PatientDataBO()).getPatientName(this.Session["CID"].ToString());
        Bill_Sys_BillTransaction_BO billSysBillTransactionBO = new Bill_Sys_BillTransaction_BO();
        string specId = billSysBillTransactionBO.GetSpecId(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.lblBillNo.Text);
        string str2 = "PAY";
        string nodeIDMSTNodes = billSysBillTransactionBO.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFPAY");
        string[] sZCOMPANYID = new string[] { str1, "&Flag=", str, "&CompanyId=", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "&CaseId=", this.Session["CID"].ToString(), "&UserName=", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "&CompanyName=", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME };
        str1 = string.Concat(sZCOMPANYID);
        string[] strArrays = new string[] { str1, "&CaseNo=", this.Session["CNO"].ToString(), "&PName=", patientName, "&NodeId=", nodeIDMSTNodes };
        str1 = string.Concat(strArrays);
        string[] strArrays1 = new string[] { str1, "&PatientId=", this.Session["scanpayment_No"].ToString(), "&UserId=", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "&BillNo=", this.lblBillNo.Text };
        str1 = string.Concat(strArrays1);
        Type type = base.GetType();
        string[] strArrays2 = new string[] { "window.open('", str1, "&Speciality=", specId, "&Process=", str2, "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); " };
        ScriptManager.RegisterStartupScript(this, type, "starScript", string.Concat(strArrays2), true);
    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        int selectedIndex = this.grdBillSearch.PageNumberList.SelectedIndex;
        this.grdBillSearch.XGridBindSearch();
        this.grdBillSearch.PageNumberList.SelectedIndex = selectedIndex;
    }

    protected void txtUpdate1_Click(object sender, EventArgs e)
    {
        this.ModalPopupExtender1.Show();
    }

    protected void txtUpdatepopup_Click(object sender, EventArgs e)
    {
    }

    public void visibleDocumentLink()
    {
        for (int i = 0; i < this.grdBillSearch.Rows.Count; i++)
        {
            string str = this.grdBillSearch.DataKeys[i]["bt_denialDocs"].ToString();
            string str1 = this.grdBillSearch.DataKeys[i]["bt_verDocs"].ToString();
            Label label = (Label)this.grdBillSearch.Rows[i].FindControl("lblDenial");
            Label label1 = (Label)this.grdBillSearch.Rows[i].FindControl("lblVerification");
            if (str.ToLower() == "1")
            {
                label.Visible = true;
            }
            if (str1.ToLower() == "1")
            {
                label1.Visible = true;
            }
        }
    }

    protected void HideTemplates(XGridView.XGridViewControl grdVeriFication)
    {
        for (int i = 0; i < grdVeriFication.Rows.Count; i++)
        {
            LinkButton lnkP = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkP");
            LinkButton lnkDP = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkDP");
            LinkButton lnkEP = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkEP");
            lnkP.Visible = true;
            lnkDP.Visible = true;
            lnkEP.Visible = true;
            LinkButton lnkM = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkM");
            LinkButton lnkDM = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkDM");
            LinkButton lnkEM = (LinkButton)grdVeriFication.Rows[i].FindControl("lnkEM");
            lnkM.Visible = false;
            lnkDM.Visible = false;
            lnkEM.Visible = false;
        }
    }
}