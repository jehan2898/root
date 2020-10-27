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
using System.Text;
using iTextSharp;
using iTextSharp.text.html;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public partial class AJAX_Pages_Bill_Sys_Billinvoice_Storage : PageBase
{
    string sz_file_path = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        utxtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.con.SourceGrid = grdStorageInvoice;
        this.txtSearchBox.SourceGrid = grdStorageInvoice;
        this.grdStorageInvoice.Page = this.Page;
        this.grdStorageInvoice.PageNumberList = this.con;

        Span2.InnerHtml = "Is this invoice final?";
        btninvoice.Attributes.Add("onclick", "return Invoice();");
        if (!IsPostBack)
        {
            fillcontrol();
            grdStorageInvoice.XGridBindSearch();
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate1();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

        }
    }

    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (extddlPatient.Text != "NA")
        {
            txtPatientName.Text = extddlPatient.Selected_Text;
        }
        else
        {
            txtPatientName.Text = "";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillcontrol();
        grdStorageInvoice.XGridBindSearch();

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string szreturn = "";
            hdnPOMValue.Value = "Yes";
            string szfilename = "";
            string szBillno = "";
            string sz_caseid = "";
            string szRemoteAddr = Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
            btnYes.Attributes.Add("onclick", "YesMassage");
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();
            ArrayList arrinvoicebill = new ArrayList();
            szfilename = getFileName();
            sz_file_path = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/Comman Folder/";
            string strBill = "";
            DataSet dsinvoiceinfo = new DataSet();
            DataSet dsinvoicetransinfo = new DataSet();
            dsinvoicetransinfo = _Bill_Sys_invoice.GetInvoiceTransdetails(txtCompanyID.Text, "Storage");
            double l = 0;
            for (int i = 0; i < grdStorageInvoice.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdStorageInvoice.Rows[i].FindControl("ChkDelete");
                if (chk.Checked == true)
                {
                    szBillno = grdStorageInvoice.DataKeys[i]["SZ_BILL_NUMBER"].ToString();
                    arrinvoicebill.Add(szBillno);

                    if (strBill == "")
                    {
                        strBill = "'" + grdStorageInvoice.DataKeys[i]["SZ_BILL_NUMBER"].ToString() + "'";

                    }
                    else
                    {
                        strBill = strBill + "," + "'" + grdStorageInvoice.DataKeys[i]["SZ_BILL_NUMBER"].ToString() + "'";

                    }
                    if (i == 0)
                    {
                        l = Convert.ToDouble(dsinvoicetransinfo.Tables[0].Rows[0]["mn_cost"]);
                    }
                    else
                    {
                        l = l + Convert.ToDouble(dsinvoicetransinfo.Tables[0].Rows[0]["mn_cost"]);
                    }

                }



            }

            Bill_Sys_invoice _Bill_Sys_invoiceinfo = new Bill_Sys_invoice();
            dsinvoiceinfo = _Bill_Sys_invoiceinfo.GetInvoiceInfo(strBill);


            szreturn = _Bill_Sys_invoice.SaveStorageinvoice(txtCompanyID.Text, utxtUserId.Text, arrinvoicebill, sz_file_path, szfilename, l.ToString(), ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME, szRemoteAddr);
            if (szreturn == "fail")
            {

            }
            else
            {
                string[] values = szreturn.Split(',');
                string returnvalue = values[0];
                string szinvoiceid = values[1];
                InvoiceTest(dsinvoiceinfo, szinvoiceid, sz_file_path, szfilename);
                grdStorageInvoice.XGridBindSearch();
                usrMessage.PutMessage("Invoice Status Generated successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

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
    protected void btnNo_Click(object sender, EventArgs e)
    {
        hdnPOMValue.Value = "No";
        btnNo.Attributes.Add("onclick", "NoMassage");


    }
    public void fillcontrol()
    {

        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        utxtCaseNo.Text = txtCaseNo.Text;
        utxtPatientName.Text = txtPatientName.Text;
        utxtbillnofromdate.Text = txtFromDate.Text;
        utxtbillnotodate.Text = txtToDate.Text;
        txtvisitdate.Text = txtFromDate1.Text;
        if (extddlPatient.Text == "" || extddlPatient.Text == "NA")
        {
            utxtPatientName.Text = txtPatientName.Text;
        }
        else
        {
            utxtPatientName.Text = txtPatientName.Text;
        }
        txtsoftwarefee.Text = ddlsoftwarefee.Text;
        txtinvoicestatus.Text = ddlinvoicestatus.Text;
        utxtbillno.Text = txtBillNo.Text;
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + grdStorageInvoice.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " alert('hi');", true);


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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        string sTemp = "";

        for (int i = 0; i < grdStorageInvoice.Rows.Count; i++)
        {
            sTemp = grdStorageInvoice.DataKeys[i]["INVOICE_STATUS"].ToString();
            if (sTemp != null)
            {
                sTemp = sTemp.Replace("&nbsp;", "");
                if (sTemp.Trim().ToString() == "generated")
                {
                    CheckBox chk = (CheckBox)grdStorageInvoice.Rows[i].FindControl("ChkDelete");
                    chk.Enabled = false;
                }

            }
        }



    }


    protected void InvoiceTest(DataSet dsinvoiceinfo, string szinvoiceid, string szFilePath, string szFileName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_invoice _Bill_Sys_invoice = new Bill_Sys_invoice();

            DataSet dsinvoicetransinfo = new DataSet();
            dsinvoicetransinfo = _Bill_Sys_invoice.GetInvoiceTransdetails(txtCompanyID.Text, "Storage");

            DataSet dscompanyinfo = new DataSet();
            dscompanyinfo = _Bill_Sys_invoice.Getcompanydetails(txtCompanyID.Text);

            DataSet dscompanyaddrss = new DataSet();
            dscompanyaddrss = _Bill_Sys_invoice.Getcompanyaddress("Storage");

            string strBill = "";
            string szfilename;
            MemoryStream m = new MemoryStream();
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 20, 36);
            //create pdf
            float[] widthbase ={ 4f };
            PdfPTable tblBase = new PdfPTable(widthbase);
            tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
            tblBase.DefaultCell.Colspan = 1;
            PdfWriter writer = PdfWriter.GetInstance(document, m);
            document.Open();

            float[] width3 ={ 4f };
            PdfPTable heading3 = new PdfPTable(width3);
            heading3.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            heading3.DefaultCell.Border = Rectangle.NO_BORDER;
            heading3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            heading3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            heading3.DefaultCell.Colspan = 1;


            tblBase.DefaultCell.Border = Rectangle.NO_BORDER;

            heading3.AddCell(new Phrase(dscompanyaddrss.Tables[0].Rows[0]["SZ_COMPANY_INFO"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading3.AddCell(new Phrase(dscompanyaddrss.Tables[0].Rows[0]["SZ_COMPANY_ADDR"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading3.AddCell(new Phrase(dscompanyaddrss.Tables[0].Rows[0]["SZ_COMPANY_ZIP"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading3.AddCell(new Phrase("Phone : " + dscompanyaddrss.Tables[0].Rows[0]["SZ_PHONE_NO"].ToString() + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading3.AddCell(new Phrase("Tax Id : " + dscompanyaddrss.Tables[0].Rows[0]["SZ_TAX_ID"].ToString() + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));


            heading3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            heading3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;

            heading3.AddCell(new Phrase(dscompanyinfo.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading3.AddCell(new Phrase(dscompanyinfo.Tables[0].Rows[0]["SZ_ADDRESS_STREET"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading3.AddCell(new Phrase(dscompanyinfo.Tables[0].Rows[0]["SZ_COMPANY_ADD"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

            tblBase.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
            heading3.AddCell(new Phrase(""));
            tblBase.AddCell(heading3);


            float[] width2 ={ 2f };
            PdfPTable heading2 = new PdfPTable(width2);
            heading2.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            heading2.DefaultCell.Border = Rectangle.NO_BORDER;
            heading2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            heading2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
            heading2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            heading2.AddCell(new Phrase("Storage Invoice Id : " + szinvoiceid + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            //heading2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            //heading2.AddCell(new Phrase("Date : " + currentdate + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            tblBase.AddCell(heading2);


            float[] width1 ={ 1f, 4f, 2f, 3f, 3f, 2f };
            PdfPTable heading1 = new PdfPTable(width1);
            heading1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
            tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
            heading1.DefaultCell.Border = Rectangle.BOX;
            heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            heading1.DefaultCell.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            heading1.AddCell(new Phrase("Bill Number", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading1.AddCell(new Phrase("Patient Name[Case No]", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading1.AddCell(new Phrase("Bill Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading1.AddCell(new Phrase("Date of Service", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading1.AddCell(new Phrase("Transaction Description", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading1.AddCell(new Phrase("Cost", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading1.DefaultCell.BackgroundColor = iTextSharp.text.Color.WHITE;

            int j = 0;
            double n = 0;

            for (int i = 0; i < dsinvoiceinfo.Tables[0].Rows.Count; i++)
            {
                for (int k = 0; k < dsinvoicetransinfo.Tables[0].Rows.Count; k++)
                {
                    if (j >= 40)
                    {
                        #region New Page Code
                        j = 0;
                        tblBase.AddCell(heading1);
                        tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading2.TotalHeight - 1, writer.DirectContent);

                        tblBase = new PdfPTable(widthbase);
                        tblBase.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        tblBase.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        tblBase.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                        tblBase.DefaultCell.Colspan = 1;

                        document.NewPage();
                        heading3 = new PdfPTable(width3);
                        heading3.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        heading3.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        heading3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        tblBase.DefaultCell.Border = Rectangle.NO_BORDER;

                        heading3.AddCell(new Phrase(dscompanyaddrss.Tables[0].Rows[0]["SZ_COMPANY_INFO"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading3.AddCell(new Phrase(dscompanyaddrss.Tables[0].Rows[0]["SZ_COMPANY_ADDR"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading3.AddCell(new Phrase(dscompanyaddrss.Tables[0].Rows[0]["SZ_COMPANY_ZIP"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading3.AddCell(new Phrase("Phone : " + dscompanyaddrss.Tables[0].Rows[0]["SZ_PHONE_NO"].ToString() + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading3.AddCell(new Phrase("Tax Id : " + dscompanyaddrss.Tables[0].Rows[0]["SZ_TAX_ID"].ToString() + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));


                        heading3.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        heading3.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;

                        heading3.AddCell(new Phrase(dscompanyinfo.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 12, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading3.AddCell(new Phrase(dscompanyinfo.Tables[0].Rows[0]["SZ_ADDRESS_STREET"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading3.AddCell(new Phrase(dscompanyinfo.Tables[0].Rows[0]["SZ_COMPANY_ADD"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

                        tblBase.DefaultCell.Border = Rectangle.BOTTOM_BORDER;
                        heading3.AddCell(new Phrase(""));
                        tblBase.AddCell(heading3);

                        heading2 = new PdfPTable(width2);
                        heading2.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                        heading2.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        heading2.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                        heading2.AddCell(new Phrase("Storage Invoice Id : " + szinvoiceid + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        //heading2.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                        //heading2.AddCell(new Phrase("Date : " + currentdate + " ", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        tblBase.AddCell(heading2);

                        heading1 = new PdfPTable(width1);
                        heading1.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                        heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        heading1.DefaultCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_TOP;
                        tblBase.DefaultCell.Border = Rectangle.NO_BORDER;
                        heading1.DefaultCell.Border = Rectangle.BOX;
                        heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                        heading1.DefaultCell.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                        heading1.AddCell(new Phrase("Bill Number", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading1.AddCell(new Phrase("Patient Name[Case No]", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading1.AddCell(new Phrase("Bill Date", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading1.AddCell(new Phrase("Date of Service", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading1.AddCell(new Phrase("Transaction Description", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading1.AddCell(new Phrase("Cost", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
                        heading1.DefaultCell.BackgroundColor = iTextSharp.text.Color.WHITE;
                        #endregion
                    }
                    double sztotalcost = Convert.ToDouble(dsinvoicetransinfo.Tables[0].Rows[k]["mn_cost"]);
                    heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading1.AddCell(new Phrase(dsinvoiceinfo.Tables[0].Rows[i]["SZ_BILL_NUMBER"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading1.AddCell(new Phrase(dsinvoiceinfo.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString() + " " + "[" + dsinvoiceinfo.Tables[0].Rows[i]["SZ_CASE_NO"].ToString() + "]", iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading1.AddCell(new Phrase(dsinvoiceinfo.Tables[0].Rows[i]["DT_BILL_DATE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                    heading1.AddCell(new Phrase(dsinvoiceinfo.Tables[0].Rows[i]["SZ_SERVICE_DATE"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
                    heading1.AddCell(new Phrase(dsinvoicetransinfo.Tables[0].Rows[k]["sz_transaction_desc"].ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
                    heading1.AddCell(new Phrase(sztotalcost.ToString("0.00"), iTextSharp.text.FontFactory.GetFont("Arial", 8, iTextSharp.text.Color.BLACK)));
                    if (i == 0)
                    {
                        n = Convert.ToDouble(dsinvoicetransinfo.Tables[0].Rows[k]["mn_cost"]);
                    }
                    else
                    {
                        n = n + Convert.ToDouble(dsinvoicetransinfo.Tables[0].Rows[k]["mn_cost"]);
                    }


                    j++;
                }


            }

            heading1.DefaultCell.Border = Rectangle.NO_BORDER;
            heading1.AddCell(new Phrase(""));
            heading1.AddCell(new Phrase(""));
            heading1.AddCell(new Phrase(""));
            heading1.AddCell(new Phrase(""));
            heading1.DefaultCell.Border = Rectangle.RECTANGLE;
            heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT;
            heading1.AddCell(new Phrase("Total Storage Cost", iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));
            heading1.DefaultCell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT;
            heading1.AddCell(new Phrase(n.ToString("0.00"), iTextSharp.text.FontFactory.GetFont("Arial", 8, Font.BOLD, iTextSharp.text.Color.BLACK)));

            tblBase.AddCell(heading1);


            tblBase.WriteSelectedRows(0, -1, document.LeftMargin, document.PageSize.Height - document.TopMargin - heading2.TotalHeight - 1, writer.DirectContent);
            document.Close();
            if (!Directory.Exists(ApplicationSettings.GetParameterValue("PhysicalBasePath") + szFilePath))
            {
                Directory.CreateDirectory(ApplicationSettings.GetParameterValue("PhysicalBasePath") + szFilePath);
            }
            //if (!Directory.Exists(sz_file_path))
            //    Directory.CreateDirectory(sz_file_path);
            string szSavePath = ApplicationSettings.GetParameterValue("PhysicalBasePath") + szFilePath + szFileName;
            string szOpenPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szFilePath + szFileName;
            System.IO.File.WriteAllBytes(szSavePath, m.GetBuffer());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + szOpenPath + "');", true);
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
        finally
        {

        }


        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

}
