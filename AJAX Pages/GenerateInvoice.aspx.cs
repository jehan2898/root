using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.IO;
using System.Globalization;
using System.Configuration;
using DevExpress.XtraScheduler.Native;

public partial class AJAX_Pages_GenerateInvoice : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            btnSearch.Attributes.Add("onclick", "return Validate();");
            btnGenerateInvoice.Attributes.Add("onclick", "return CheckSelect();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlEmployer.Flag_ID = txtCompanyID.Text;

            
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void BindGrid()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            EmployerVisitDO objEmployerdo = new EmployerVisitDO();
            objEmployerdo.EmploerID = extddlEmployer.Text;
            objEmployerdo.CompanyID = txtCompanyID.Text;
            string cases = "";
            if (txtCaseNo.Text != "")
            {

                string[] spiltcases = txtCaseNo.Text.Trim().Split(',');
                for (int i = 0; i < spiltcases.Length; i++)
                {
                    if (cases == "")
                    {
                        cases = "'" + spiltcases[i].ToString() + "'";
                    }
                    else
                    {
                        cases += ",'" + spiltcases[i].ToString() + "'";
                    }
                }

            }
            objEmployerdo.CaseNO = cases;
            objEmployerdo.From_Visit_Date = txtFromDate.Text;
            objEmployerdo.To_Visit_Date = txtToDate.Text;
            EmployerBO objBillSysPatientBO = new EmployerBO();
            DataSet ds = objBillSysPatientBO.GetVisit(objEmployerdo);
            grdVisits.DataSource = ds;
            grdVisits.DataBind();

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
    protected void btnGenerateInvoice_Click(object sender, EventArgs e)
    {
        string visit = "";
        string employerId = "";
        EmployerBO objEmployerBO = new EmployerBO ();
        for (int i = 0; i < grdVisits.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)grdVisits.Items[i].Cells[0].FindControl("chkSelect");
            if (chk.Checked)
            {
                if (visit == "")
                {
                    visit =  grdVisits.Items[i].Cells[3].Text.ToString();
                }
                else
                {
                    visit = visit+"," + grdVisits.Items[i].Cells[3].Text.ToString();
                }
                employerId = grdVisits.Items[i].Cells[2].Text.ToString();
            }
        }
  

       string invoiceId= objEmployerBO.GenerateInvoice(visit, employerId, txtCompanyID.Text, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
       if (invoiceId != "")
       {
           GeneratePdf(invoiceId);
       }
       BindGrid();
    }

    public void GeneratePdf(string invoiceID)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EmployerBO objEmployerBO = new EmployerBO();
            DataSet ds = objEmployerBO.GetInvoiceInfo(invoiceID, txtCompanyID.Text);
            string strHtml = "<table width='100%'><tr><td style='width:32%'>";
           // strHtml = strHtml + "<img src='https://gogreenbills.com/images/cal.gif' alt='logo'  height='150' width='140' /></td>";
            strHtml = strHtml + "</td>";
            strHtml = strHtml + "<td style='width:35%'><div style='float:center;'><table width='100%'><tr><td align='center' ><b>" + ds.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString() + "</b></td></tr>";
            strHtml = strHtml + " <tr><td align='center' >" + ds.Tables[0].Rows[0]["SZ_ADDRESS_STREET"].ToString() + "</td></tr>";
            strHtml = strHtml + "<tr><td align='center' >" + ds.Tables[0].Rows[0]["SZ_ADDRESS_CITY"].ToString() + "," + ds.Tables[0].Rows[0]["SZ_ADDRESS_STATE"].ToString() + " " + ds.Tables[0].Rows[0]["SZ_ADDRESS_ZIP"].ToString() + "</td></tr>";
            strHtml = strHtml + "<tr><td align='center' >PH." + ds.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString() + " FAX:" + ds.Tables[0].Rows[0]["SZ_FAX_NO"].ToString() + "</td></tr></table></div></td>";
            strHtml = strHtml + "<td style='width:30%'><div style='float:right;'><table><tr><td><b>INVOICE</b></td></tr> <tr><td>&nbsp;</td></tr><tr><td>DATE:" + ds.Tables[1].Rows[0]["DATE"].ToString() + "</td></tr><tr><td><b>INVOICE #: " + ds.Tables[1].Rows[0]["ID"].ToString() + "</b></td></tr></table></div></td>";
            //strHtml = strHtml + "<div style='float:right;width:20%'><table><tr><td><b>INVOICE</b></td></tr><tr><td>DATE:7/14/2014</td></tr><tr><td><b>INVOICE #: ABM0614 </b></td></tr></table></div>";
            //strHtml = strHtml + "</div>";
            strHtml = strHtml + "<br /><br /><br />";
            //strHtml = strHtml + "<div style='width:100%;height: 210px;'>";
            // strHtml = strHtml + "<div style='float:left;width:80%; height: 190px;'>";

            //strHtml = strHtml + "<div style='float:right;width:20%; height: 190px;'>";
            //strHtml = strHtml + "<table><tr><td>DT</td><td>Drug Test</td><td>21</td></tr>";
            //strHtml = strHtml + "<tr><td>DTC</td><td>Drug Test Collection</td><td>0</td></tr>";
            //strHtml = strHtml + "<tr><td>BAT </td><td>Breath Alcohol Test</td><td>0</td></tr>";
            //strHtml = strHtml + "<tr><td>PHY </td><td>Physical Exam</td><td>0</td></tr>";
            //strHtml = strHtml + "<tr><td>XRAY</td><td>Chest Xray</td><td>0</td></tr>";
            //strHtml = strHtml + "<tr><td>PPD </td><td>Skin Test</td><td>0</td></tr>";
            //strHtml = strHtml + "<tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr><tr></tr>";
            //strHtml = strHtml + "<tr><td align='center' colspan='3'><b>Invoice June 2014 </b></td></tr>";
            //strHtml = strHtml + "</table>";
            //strHtml = strHtml + "</div>";
            strHtml = strHtml + "</tr></table>";

            strHtml = strHtml + "<table width='100%'><tr><td style='width:50%'><div style='float:left;'><table><tr><td><b>BILL TO:</b></td></tr><tr><td><b>" + ds.Tables[2].Rows[0]["SZ_EMPLOYER_NAME"].ToString() + "</b></td></tr></table></div></td>";
            strHtml = strHtml + "<td style='width:50%'><div style='float:right;'> <table width='50%' align='right' >";
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                strHtml = strHtml + "<tr> <td align='right' >" + ds.Tables[3].Rows[i]["SZ_PROCEDURE_CODE"].ToString() + "</td> <td align='right' >" + ds.Tables[3].Rows[i]["SZ_CODE_DESCRIPTION"].ToString() + "</td> <td align='right' >" + ds.Tables[3].Rows[i]["COUNT"].ToString() + " </td>";

            }
            strHtml = strHtml + "<tr><td align='Center' colspan='3'>Invoice " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " " + DateTime.Now.Year + " </td></tr>";
            strHtml = strHtml + "</table></div></td></tr></table>";
            strHtml = strHtml + "<div style='width:100%'>";
            strHtml = strHtml + "<table border='1' width='100%'>";
            strHtml = strHtml + "<thead><tr><th><b>QTY</b></th><th><b>Items</b></th><th><b>First name</b></th><th><b>Last name</b></th><th><b>SSN</b></th><th><b>Date of service</b> </th><th style='width:10%'><b>Unit price</b></th><th style='width:10%'><b>Total</b></th></tr></thead>";

            strHtml = strHtml + "<tbody> ";
            int ifirst = 0;
            int icount = 0;

            for (int i = 0; i < ds.Tables[4].Rows.Count ; i++)
            {
                if (icount > 16 && ifirst != 0)
                {
                    icount = 0;
                    ifirst = 1;
                    strHtml = strHtml + "</tbody>";
                    strHtml = strHtml + "</table>";
                    strHtml = strHtml + "<span style='page-break-before: always;'/>";

                    strHtml = strHtml + "<table border='1' width='100%'>";
                    strHtml = strHtml + "<thead><tr><th><b>QTY</b></th><th><b>Items</b></th><th><b>First name</b></th><th><b>Last name</b></th><th><b>SSN</b></th><th><b>Date of service</b> </th><th style='width:10%'><b>Unit price</b></th><th style='width:10%'><b>Total</b></th></tr></thead>";
                    strHtml = strHtml + "<tbody> ";
                }

                if (icount > 9 && ifirst == 0)
                {
                    icount = 0;
                    ifirst = 1;
                    strHtml = strHtml + "</tbody>";
                    strHtml = strHtml + "</table>";
                    strHtml = strHtml + "<span style='page-break-before: always;'/>";
                    strHtml = strHtml + "<table border='1' width='100%'>";
                    strHtml = strHtml + "<thead><tr><th><b>QTY</b></th><th><b>Items</b></th><th><b>First name</b></th><th><b>Last name</b></th><th><b>SSN</b></th><th><b>Date of service</b> </th><th style='width:10%'><b>Unit price</b></th><th style='width:10%'><b>Total</b></th></tr></thead>";
                    strHtml = strHtml + "<tbody> ";
                }
                strHtml = strHtml + "<tr><td> " + ds.Tables[4].Rows[i]["QTY"].ToString();
                strHtml = strHtml + "<td> " + ds.Tables[4].Rows[i]["ITEMS"].ToString() + " </td><td> " + ds.Tables[4].Rows[i]["FIRST NAME"].ToString() + " </td>";
                strHtml = strHtml + "<td> " + ds.Tables[4].Rows[i]["LAST NAME"].ToString() + " </td>";
                strHtml = strHtml + "<td> " + ds.Tables[4].Rows[i]["SSN"].ToString() + " </td>";
                strHtml = strHtml + "<td> " + ds.Tables[4].Rows[i]["DOS"].ToString() + " </td>";
                strHtml = strHtml + "<td> " + ds.Tables[4].Rows[i]["UNIT PRICE"].ToString() + "</td>";
                strHtml = strHtml + "<td> " + ds.Tables[4].Rows[i]["TOTAL"].ToString() + " </td>";

                icount++;

            }
            strHtml = strHtml + "<tr><td colspan='6' style='border:none'>&nbsp;</td><td><b>Subtotal</b> </td><td> " + ds.Tables[5].Rows[0]["sum"].ToString() + "</td></tr>";
            strHtml = strHtml + "<tr><td colspan='6' style='border:none'>&nbsp;</td><td><b>Total Amt </b></td><td>" + ds.Tables[5].Rows[0]["sum"].ToString() + "</td></tr>";
            strHtml = strHtml + "<tr><td colspan='6' style='border:none'>&nbsp;</td><td><b>Balance Due</b> </td><td> " + ds.Tables[5].Rows[0]["sum"].ToString() + "</td></tr>";
            strHtml = strHtml + "</tbody>";
            strHtml = strHtml + "</table>";



            strHtml = strHtml + "</div>";







            strHtml = strHtml + "<br /> <br /><table width='100%'><tr><td align='center'><b>Please make checks payable to ";
            strHtml = strHtml + ds.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString() + "  ";
            strHtml = strHtml + ds.Tables[0].Rows[0]["SZ_ADDRESS_STREET"].ToString() + " ,";
            strHtml = strHtml + ds.Tables[0].Rows[0]["SZ_ADDRESS_CITY"].ToString() + " ,";
            strHtml = strHtml + ds.Tables[0].Rows[0]["SZ_ADDRESS_STATE"].ToString() + "  ";
            strHtml = strHtml + ds.Tables[0].Rows[0]["SZ_ADDRESS_ZIP"].ToString() + " </b></td></tr></table>";

            SautinSoft.PdfMetamorphosis objHTMToPDF = new SautinSoft.PdfMetamorphosis();


            objHTMToPDF.Serial = "10007706603";





            objHTMToPDF.PageStyle.PageOrientation.Landscape();

            string basePath = ApplicationSettings.GetParameterValue("PhysicalBasePath");
            string SavePath = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME +"/Invoice"+ "/" + invoiceID + "/";
            string htmfilename = getFileName("Invoice_" + invoiceID) + ".htm";


            string pdffilename = getFileName("Invoice_" + invoiceID) + ".pdf";


            if (!Directory.Exists(basePath + SavePath))
            {


                Directory.CreateDirectory(basePath + SavePath);


            }


            StreamWriter sw = new StreamWriter(basePath + SavePath + htmfilename);


            sw.Write(strHtml);


            sw.Close();


            Int32 iTemp;


            iTemp = objHTMToPDF.HtmlToPdfConvertFile(basePath + SavePath + htmfilename, basePath + SavePath + pdffilename);

            EmployerBO obj = new EmployerBO();
            int retuenVal = obj.SaveInvoiceLink(txtCompanyID.Text, invoiceID, ApplicationSettings.GetParameterValue("DocumentManagerURL")+SavePath, pdffilename);
            if (retuenVal != 0)
            {
                ScriptManager.RegisterClientScriptBlock((Page)this, base.GetType(), "Msg", "window.open('" + ApplicationSettings.GetParameterValue("DocumentManagerURL") + SavePath + pdffilename + "'); ", true);
            }
            else
            {
                this.usrMessage.PutMessage("error to generate invoice pdf");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
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
        // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "sandeep", "<script type='text/javascript'>window.location.href='" + @"D:\temp\" + pdffilename + "'</script>");
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public static string getFileName(string p_szBillNumber)
    {


        String szBillNumber = "";


        szBillNumber = p_szBillNumber;


        String szFileName;


        DateTime currentDate;


        currentDate = DateTime.Now;


        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");


        return szFileName;


    }


    public static string getRandomNumber()
    {


        System.Random objRandom = new Random();


        return objRandom.Next(1, 10000).ToString();


    }
}