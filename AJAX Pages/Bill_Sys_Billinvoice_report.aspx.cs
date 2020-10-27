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
using System.IO;

public partial class AJAX_Pages_Bill_Sys_Billinvoice_report : PageBase
{
    int iFlag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        utxtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.con.SourceGrid = grdInvoicegenerate;
        this.txtSearchBox.SourceGrid = grdInvoicegenerate;
        this.grdInvoicegenerate.Page = this.Page;
        this.grdInvoicegenerate.PageNumberList = this.con;
        btnpayemtdelete.Attributes.Add("onclick", "return confirm_bill_delete();");
        btninvoicegenerate.Attributes.Add("onclick", "return Confirm_Delete_Code();");
        btndelete.Attributes.Add("onclick", "return Confirm_Delete_Code();");

        if (!IsPostBack)
        {
            txtsoftwarefee.Text = ddlsoftwarefee.Text;
            ddlgeneartedate.Attributes.Add("onChange", "javascript:SetDate();");
            btnSave.Attributes.Add("onclick", "return val_CheckControls();");
            btnUpdate.Attributes.Add("onclick", "return val_CheckControls();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            grdInvoicegenerate.XGridBindSearch();

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtsoftwarefee.Text = ddlsoftwarefee.Text;
        grdInvoicegenerate.XGridBindSearch();
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" +ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET")  + grdInvoicegenerate.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET") ) + "';", true);

    }
    protected void btninvoice_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            double blTotalInvoiceAmount = 0;
            string szinvoiceid = "";
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            ArrayList arrInvoiceid = new ArrayList();
            for (int i = 0; i < grdInvoicegenerate.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdInvoicegenerate.Rows[i].FindControl("ChkDelete");
                if (chk.Checked == true)
                {

                    if (szinvoiceid == "")
                    {
                        szinvoiceid = grdInvoicegenerate.DataKeys[i]["i_invoice_id"].ToString();
                        Session["InvoiceID"] = szinvoiceid;
                    }
                    else
                    {
                        szinvoiceid = szinvoiceid + "," + grdInvoicegenerate.DataKeys[i]["i_invoice_id"].ToString();

                    }

                    blTotalInvoiceAmount = blTotalInvoiceAmount + (Convert.ToDouble(grdInvoicegenerate.DataKeys[i]["MN_INVOICE_AMOUNT"].ToString()));


                }

            }
            if (ddlsoftwarefee.SelectedItem.Text == "Paid")
            {
                DataSet dspaymentamount = new DataSet();
                dspaymentamount = _Bill_Sys_invoice.GetInvoicepaymnetDetails(szinvoiceid);
                grdPaymentTransaction.DataSource = dspaymentamount;
                grdPaymentTransaction.DataBind();
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                txtChequeNumber.Text = "";
                txtpaymentdate.Text = "";
                txtChequeAmount.Text = "";
                txtDescription.Text = "";
            }
            else
            {

                txtChequeNumber.Text = "";
                txtpaymentdate.Text = "";
                txtChequeAmount.Text = "";
                txtDescription.Text = "";
                grdPaymentTransaction.DataSource = null;
                grdPaymentTransaction.DataBind();
                btnUpdate.Enabled = false;
                btnSave.Enabled = true;

            }
            txtTransInvoiceId.Text = szinvoiceid;
            lblBalance.Text = blTotalInvoiceAmount.ToString("0.00");
            sztotalamount.Text = lblBalance.Text;
            hdnBal.Value = lblBalance.Text;

            ModalPopupExtender1.Show();
            grdInvoicegenerate.XGridBindSearch();
            //usrMessage.PutMessage("Payment Status Update successfully");
            //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            //usrMessage.Show();
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
        string sTemp = "";

        for (int i = 0; i < grdInvoicegenerate.Rows.Count; i++)
        {
            sTemp = grdInvoicegenerate.DataKeys[i]["PAID_STATUS"].ToString();
            if (sTemp != null)
            {
                sTemp = sTemp.Replace("&nbsp;", "");
                if (sTemp.Trim().ToString() == "paid")
                {
                    CheckBox chk = (CheckBox)grdInvoicegenerate.Rows[i].FindControl("ChkDelete");
                    chk.Enabled = false;
                }

            }
        }






    }

    protected void btnPaymentDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            for (int i = 0; i < grdPaymentTransaction.Items.Count; i++)
            {
                CheckBox chkDelete1 = (CheckBox)grdPaymentTransaction.Items[i].FindControl("chkDelete");
                if (chkDelete1.Checked)
                {

                    _Bill_Sys_invoice.deletesoftwarepaymentTransaction(grdPaymentTransaction.Items[i].Cells[1].Text, txtCompanyID.Text);
                }

            }
            usrMessage.PutMessage("Payment deleted successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            string szinvoiceid = "";
            string ids = txtTransInvoiceId.Text;
            string[] arrrInvoiceId = ids.Split(',');
            ArrayList arrIds = new ArrayList();
            for (int i = 0; i < arrrInvoiceId.Length; i++)
            {
                if (szinvoiceid == "")
                {
                    szinvoiceid = "'" + arrrInvoiceId[i].ToString() + "'";

                }
                else
                {
                    szinvoiceid = szinvoiceid + "," + "'" + arrrInvoiceId[i].ToString() + "'";

                }
            }

            DataSet dspaymentamount = new DataSet();
            dspaymentamount = _Bill_Sys_invoice.GetInvoicepaymnetDetails(szinvoiceid);
            grdPaymentTransaction.DataSource = dspaymentamount;
            grdPaymentTransaction.DataBind();
            txtChequeNumber.Text = "";
            txtpaymentdate.Text = "";
            txtChequeAmount.Text = "";
            txtDescription.Text = "";
            ModalPopupExtender1.Show();
            grdInvoicegenerate.XGridBindSearch();


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
        string sztotalpaymnetamount;
        string szinvoiceid = "";
        Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
        string ids = txtTransInvoiceId.Text;
        string[] arrrInvoiceId = ids.Split(',');
        ArrayList arrIds = new ArrayList();
        for (int i = 0; i < arrrInvoiceId.Length; i++)
        {
            arrIds.Add(arrrInvoiceId[i].ToString());
            if (szinvoiceid == "")
            {
                szinvoiceid = "'" + arrrInvoiceId[i].ToString() + "'";

            }
            else
            {
                szinvoiceid = szinvoiceid + "," + "'" + arrrInvoiceId[i].ToString() + "'";

            }
        }
        hdnBal.Value = lblBalance.Text;
        sztotalpaymnetamount = _Bill_Sys_invoice.Saveinvoicepaymenttransaction(txtChequeAmount.Text, txtChequeNumber.Text, txtDescription.Text, txtpaymentdate.Text, utxtUserId.Text, arrIds, txtCompanyID.Text, lblBalance.Text);
        string[] values = sztotalpaymnetamount.Split(',');
        string returnvalue = values[0];
        string szsoftwarepaymentid = values[1];
        txtsoftwarepaymnetid.Text = szsoftwarepaymentid;
        DataSet dspaymentamount = new DataSet();
        dspaymentamount = _Bill_Sys_invoice.GetInvoicepaymnetDetails(szinvoiceid);
        grdPaymentTransaction.DataSource = dspaymentamount;
        grdPaymentTransaction.DataBind();
        txtChequeNumber.Text = "";
        txtpaymentdate.Text = "";
        txtChequeAmount.Text = "";
        txtDescription.Text = "";
        ModalPopupExtender1.Show();
        grdInvoicegenerate.XGridBindSearch();
        grdPaymentTransaction.Visible = true;

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();

        _Bill_Sys_invoice.Updateinvoicepaymnettransaction(txtChequeAmount.Text, txtpaymentdate.Text, txtChequeNumber.Text, txtDescription.Text, txtsoftwarepaymnetid.Text);
        string szinvoiceid = "";
        string ids = txtTransInvoiceId.Text;
        string[] arrrInvoiceId = ids.Split(',');
        for (int i = 0; i < arrrInvoiceId.Length; i++)
        {
            if (szinvoiceid == "")
            {
                szinvoiceid = "'" + arrrInvoiceId[i].ToString() + "'";

            }
            else
            {
                szinvoiceid = szinvoiceid + "," + "'" + arrrInvoiceId[i].ToString() + "'";

            }
        }
        DataSet dspaymentamount = new DataSet();
        dspaymentamount = _Bill_Sys_invoice.GetInvoicepaymnetDetails(szinvoiceid);
        grdPaymentTransaction.DataSource = dspaymentamount;
        grdPaymentTransaction.DataBind();
        txtChequeNumber.Text = "";
        txtpaymentdate.Text = "";
        txtChequeAmount.Text = "";
        txtDescription.Text = "";
        ModalPopupExtender1.Show();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtChequeNumber.Text = "";
        txtpaymentdate.Text = "";
        txtChequeAmount.Text = "";
        txtDescription.Text = "";
        ModalPopupExtender1.Show();
    }
    protected void grdPaymentTransaction_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Select")
        {
            txtPaymentID.Text = e.Item.Cells[1].Text;
            txtChequeNumber.Text = e.Item.Cells[2].Text;
            txtpaymentdate.Text = e.Item.Cells[3].Text;
            string szamt = e.Item.Cells[4].Text;
            szamt = szamt.Replace('$', ' ');
            szamt = szamt.Trim();
            txtChequeAmount.Text = szamt;
            txtDescription.Text = e.Item.Cells[5].Text;
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            ModalPopupExtender1.Show();
        }
        else
        {
            ModalPopupExtender1.Hide();

        }
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szinvoiceidtype = "";
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            for (int i = 0; i < grdInvoicegenerate.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdInvoicegenerate.Rows[i].FindControl("ChkDelete");
                if (chk.Checked == true)
                {
                    if (ddlsoftwarefee.SelectedItem.Text == "Paid")
                    {
                        if (szinvoiceidtype == "")
                        {
                            szinvoiceidtype = "'" + grdInvoicegenerate.DataKeys[i]["i_invoice_id"].ToString() + "'";

                        }
                        else
                        {
                            szinvoiceidtype = szinvoiceidtype + "," + "'" + grdInvoicegenerate.DataKeys[i]["i_invoice_id"].ToString() + "'";

                        }
                        usrMessage.PutMessage(szinvoiceidtype + " You Can not delete this Paid Invoice...");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                        //string sztype = grdInvoicegenerate.DataKeys[i]["PAID_STATUS"].ToString();
                    }
                    else
                    {
                        string szinvoiceid = grdInvoicegenerate.DataKeys[i]["i_invoice_id"].ToString();
                        _Bill_Sys_invoice.deletesoftwareinvoice(szinvoiceid, txtCompanyID.Text);
                        usrMessage.PutMessage("Deleted successfully ...");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }

                }
            }
            grdInvoicegenerate.XGridBindSearch();
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

    protected void lnkuplaod_Click(object sender, EventArgs e)
    {


        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;
        int index = it.ItemIndex;
        Session["payment_No"] = it.Cells[1].Text;

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "showUploadFilePopup();", true);
        ModalPopupExtender1.Hide();

    }
    private string getFileName()
    {
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = "invoice" + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf"; ;
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
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
            string sz_file_path = "";
            string szfilename = "";
            Bill_Sys_invoice objInvoice = new Bill_Sys_invoice();
            DataSet dsinvoiceid = new DataSet();
            DataSet dspaymentamount = new DataSet();
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();


            FileUpload _FileUpload = new FileUpload();
            ArrayList arr = new ArrayList();


            if (!fuUploadReport.HasFile)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " alert('please select file from upload Report !');showUploadFilePopup();", true);
                return;
            }

            dsinvoiceid = objInvoice.GetsoftwareInvoiceId(Session["payment_No"].ToString(), txtCompanyID.Text);


            string strBillNo = "";
            string strCaseID = "";
            string strsSpecID = "";
            sz_file_path = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/Comman Folder/";
            if (dsinvoiceid.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsinvoiceid.Tables[0].Rows.Count; i++)
                {


                    strBillNo = dsinvoiceid.Tables[0].Rows[i][0].ToString();
                    strCaseID = dsinvoiceid.Tables[0].Rows[i][1].ToString();
                    strsSpecID = dsinvoiceid.Tables[0].Rows[i][2].ToString();
                    Bill_Sys_SoftwareInVoice_UploadFile _objUploadFile = new Bill_Sys_SoftwareInVoice_UploadFile();

                    _objUploadFile.sz_bill_no = strBillNo;
                    _objUploadFile.sz_case_id = strCaseID;
                    _objUploadFile.sz_company_id = txtCompanyID.Text;
                    _objUploadFile.sz_FileName = fuUploadReport.FileName;

                    _objUploadFile.sz_UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                    _objUploadFile.sz_UserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString();
                    _objUploadFile.sz_StatusCode = "";
                    _objUploadFile.sz_flag = "PAY";

                    _objUploadFile.sz_payment_id = Session["payment_No"].ToString();
                    _objUploadFile.sz_speciality_id = strsSpecID;

                    _objUploadFile.sz_File_PhysicalPath = sz_file_path;
                    arr.Add(_objUploadFile);


                }
            }

            _FileUpload.InvoiceUploadFile(arr);

            if (!Directory.Exists(ApplicationSettings.GetParameterValue("BASEPATH")  + sz_file_path))
            {
                Directory.CreateDirectory(ApplicationSettings.GetParameterValue("BASEPATH")+ sz_file_path);
            }
            fuUploadReport.SaveAs(ApplicationSettings.GetParameterValue("BASEPATH") + sz_file_path + fuUploadReport.FileName);


            dspaymentamount = _Bill_Sys_invoice.GetInvoicepaymnetDetails(Session["InvoiceID"].ToString());
            grdPaymentTransaction.DataSource = dspaymentamount;
            grdPaymentTransaction.DataBind();

            usrMessage.PutMessage("File Upload Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            ModalPopupExtender1.Show();
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


    protected void lnkscan_Click(object sender, EventArgs e)
    {
        int iindex = grdPaymentTransaction.SelectedIndex;
        LinkButton btn = (LinkButton)sender;
        TableCell tc = (TableCell)btn.Parent;
        DataGridItem it = (DataGridItem)tc.Parent;
        Session["scan_invoice_payment_No"] = it.Cells[1].Text;
        int index = it.ItemIndex;

        RedirectToScanApp(iindex);


    }
    public void RedirectToScanApp(int iindex)
    {
        // Session["Billflag"] = "BillInvoiceReport";

        string szFlag = "BillInvoiceReport";
        string sz_InvoicePaymentID = Session["scan_invoice_payment_No"].ToString();

        string sz_file_path = "";
        string sz_caseno = sz_InvoicePaymentID;
        string sz_SoftwareInvoiceReport = "SoftwareInvoiceReport";

        iindex = (int)grdPaymentTransaction.SelectedIndex;
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
        PatientDataBO _obj_patientBo = new PatientDataBO();
        //   string sz_PatientName = _obj_patientBo.getPatientName(Session["CID"].ToString());
        Bill_Sys_BillTransaction_BO _obj = new Bill_Sys_BillTransaction_BO();

        //  string szSpecialityId = _obj.GetSpecId(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, lblBillNo.Text);
        string szProcess = "PAY";

        sz_file_path = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/Comman Folder/";

        szUrl = szUrl + "&Flag=" + szFlag + "&SzInvoicePaymentID=" + sz_InvoicePaymentID + "&SzCompanyID=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&SzFilePath=" + sz_file_path + "&CaseNo=" + sz_caseno + "&Process=" + szProcess + "&PName=" + sz_SoftwareInvoiceReport;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }
}
