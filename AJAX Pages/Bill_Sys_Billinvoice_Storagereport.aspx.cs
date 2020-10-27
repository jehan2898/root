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

public partial class AJAX_Pages_Bill_Sys_Billinvoice_Storagereport : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        utxtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.con.SourceGrid = grdStoragegenerate;
        this.txtSearchBox.SourceGrid = grdStoragegenerate;
        this.grdStoragegenerate.Page = this.Page;
        this.grdStoragegenerate.PageNumberList = this.con;
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
            grdStoragegenerate.XGridBindSearch();

        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtsoftwarefee.Text = ddlsoftwarefee.Text;
        grdStoragegenerate.XGridBindSearch();
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + grdStoragegenerate.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);


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
            string szstorageinvoiceid = ""; ;
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            for (int i = 0; i < grdStoragegenerate.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdStoragegenerate.Rows[i].FindControl("ChkDelete");
                if (chk.Checked == true)
                {
                    if (szstorageinvoiceid == "")
                    {
                        szstorageinvoiceid = grdStoragegenerate.DataKeys[i]["storage_invoice_id"].ToString();

                    }
                    else
                    {
                        szstorageinvoiceid = szstorageinvoiceid + "," + grdStoragegenerate.DataKeys[i]["storage_invoice_id"].ToString();

                    }
                    blTotalInvoiceAmount = blTotalInvoiceAmount + (Convert.ToDouble(grdStoragegenerate.DataKeys[i]["MN_STORAGE_INVOICE_AMOUNT"].ToString()));
                }

            }

            if (ddlsoftwarefee.SelectedItem.Text == "Paid")
            {
                DataSet dspaymentamount = new DataSet();
                dspaymentamount = _Bill_Sys_invoice.GetStorageInvoicepaymnetDetails(szstorageinvoiceid);
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
            txtTranstorageId.Text = szstorageinvoiceid;
            lblBalance.Text = blTotalInvoiceAmount.ToString("0.00");
            //sztotalamount.Text = lblBalance.Text;
           
            ModalPopupExtender1.Show();
            grdStoragegenerate.XGridBindSearch();
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

        for (int i = 0; i < grdStoragegenerate.Rows.Count; i++)
        {
            sTemp = grdStoragegenerate.DataKeys[i]["PAID_STATUS"].ToString();
            if (sTemp != null)
            {
                sTemp = sTemp.Replace("&nbsp;", "");
                if (sTemp.Trim().ToString() == "paid")
                {
                    CheckBox chk = (CheckBox)grdStoragegenerate.Rows[i].FindControl("ChkDelete");
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

                    _Bill_Sys_invoice.deletestoragepaymentTransaction(grdPaymentTransaction.Items[i].Cells[1].Text, txtCompanyID.Text);
                }

            }
            usrMessage.PutMessage("Payment deleted successfully ...");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            string szStorageinvoiceid = "";
            string ids = txtTranstorageId.Text;
            string[] arrrStorageInvoiceId = ids.Split(',');
            ArrayList arrIds = new ArrayList();
            for (int i = 0; i < arrrStorageInvoiceId.Length; i++)
            {
                if (szStorageinvoiceid == "")
                {
                    szStorageinvoiceid = "'" + arrrStorageInvoiceId[i].ToString() + "'";

                }
                else
                {
                    szStorageinvoiceid = szStorageinvoiceid + "," + "'" + arrrStorageInvoiceId[i].ToString() + "'";

                }
            }
            DataSet dspaymentamount = new DataSet();
            dspaymentamount = _Bill_Sys_invoice.GetStorageInvoicepaymnetDetails(szStorageinvoiceid);
            grdPaymentTransaction.DataSource = dspaymentamount;
            grdPaymentTransaction.DataBind();
            txtChequeNumber.Text = "";
            txtpaymentdate.Text = "";
            txtChequeAmount.Text = "";
            txtDescription.Text = "";
            ModalPopupExtender1.Show();
            grdStoragegenerate.XGridBindSearch();

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
        try
        {
            string sztotalpaymnetamount;
            string szStorageinvoiceid = "";
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            string ids = txtTranstorageId.Text;
            string[] arrrStorageInvoiceId = ids.Split(',');
            ArrayList arrIds = new ArrayList();
            for (int i = 0; i < arrrStorageInvoiceId.Length; i++)
            {
                arrIds.Add(arrrStorageInvoiceId[i].ToString());
                if (szStorageinvoiceid == "")
                {
                    szStorageinvoiceid = "'" + arrrStorageInvoiceId[i].ToString() + "'";

                }
                else
                {
                    szStorageinvoiceid = szStorageinvoiceid + "," + "'" + arrrStorageInvoiceId[i].ToString() + "'";

                }
            }

            szbalance.Text = lblBalance.Text;
            sztotalpaymnetamount = _Bill_Sys_invoice.Savestoragepaymenttransaction(txtChequeAmount.Text, txtChequeNumber.Text, txtDescription.Text, txtpaymentdate.Text, utxtUserId.Text, arrIds, txtCompanyID.Text,lblBalance.Text);
            string[] values = sztotalpaymnetamount.Split(',');
            string returnvalue = values[0];
            string szstoragepaymentid = values[1];
            txtstoragepaymnetid.Text = szstoragepaymentid;
            DataSet dspaymentamount = new DataSet();
            dspaymentamount = _Bill_Sys_invoice.GetStorageInvoicepaymnetDetails(szStorageinvoiceid);
            grdPaymentTransaction.DataSource = dspaymentamount;
            grdPaymentTransaction.DataBind();
            usrMessage.PutMessage("Your payment saved");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
            txtChequeNumber.Text = "";
            txtpaymentdate.Text = "";
            txtChequeAmount.Text = "";
            txtDescription.Text = "";

            ModalPopupExtender1.Show();
            grdStoragegenerate.XGridBindSearch();
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
        _Bill_Sys_invoice.Updatestorageinvoicepaymnettransaction(txtChequeAmount.Text, txtpaymentdate.Text, txtChequeNumber.Text, txtDescription.Text, txtstoragepaymnetid.Text);

        string szStorageinvoiceid = "";
        string ids = txtTranstorageId.Text;
        string[] arrrStorageInvoiceId = ids.Split(',');
        for (int i = 0; i < arrrStorageInvoiceId.Length; i++)
        {
            if (szStorageinvoiceid == "")
            {
                szStorageinvoiceid = "'" + arrrStorageInvoiceId[i].ToString() + "'";

            }
            else
            {
                szStorageinvoiceid = szStorageinvoiceid + "," + "'" + arrrStorageInvoiceId[i].ToString() + "'";

            }
        }
        DataSet dspaymentamount = new DataSet();
        dspaymentamount = _Bill_Sys_invoice.GetStorageInvoicepaymnetDetails(szStorageinvoiceid);
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
            string szstorageinvoiceidtype = "";
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            for (int i = 0; i < grdStoragegenerate.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdStoragegenerate.Rows[i].FindControl("ChkDelete");
                if (chk.Checked == true)
                {
                    if (ddlsoftwarefee.SelectedItem.Text == "Paid")
                    {
                        if (szstorageinvoiceidtype == "")
                        {
                            szstorageinvoiceidtype = "'" + grdStoragegenerate.DataKeys[i]["storage_invoice_id"].ToString() + "'";

                        }
                        else
                        {
                            szstorageinvoiceidtype = szstorageinvoiceidtype + "," + "'" + grdStoragegenerate.DataKeys[i]["storage_invoice_id"].ToString() + "'";

                        }
                        usrMessage.PutMessage(szstorageinvoiceidtype + " You Can not delete this Paid Invoice...");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                        //string sztype = grdInvoicegenerate.DataKeys[i]["PAID_STATUS"].ToString();
                    }
                    else
                    {
                        string szistoragenvoiceid = grdStoragegenerate.DataKeys[i]["storage_invoice_id"].ToString();
                        _Bill_Sys_invoice.deletestorageinvoice(szistoragenvoiceid, txtCompanyID.Text);
                        usrMessage.PutMessage("Deleted successfully ...");
                        usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                        usrMessage.Show();
                    }

                }
            }
            grdStoragegenerate.XGridBindSearch();
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
        szFileName = "invoice" + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".pdf";
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
            if (!fuUploadReport.HasFile)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " alert('please select file from upload Report !');showUploadFilePopup();", true);
                return;
            }
            szfilename = getFileName();
            sz_file_path = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/Comman Folder/";
            if (!Directory.Exists(ApplicationSettings.GetParameterValue( "PhysicalBasePath").ToString() + sz_file_path))
            {
                Directory.CreateDirectory(ApplicationSettings.GetParameterValue("PhysicalBasePath").ToString() + sz_file_path);
            }
            fuUploadReport.SaveAs(ApplicationSettings.GetParameterValue("PhysicalBasePath").ToString() + sz_file_path + szfilename);
            ModalPopupExtender1.Show();
            //string szSavePath = ConfigurationManager.AppSettings["BASEPATH"].ToString() + sz_file_path + szfilename;
            //System.IO.File.WriteAllBytes(szSavePath);
            usrMessage.PutMessage("File Upload Successfully");
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
