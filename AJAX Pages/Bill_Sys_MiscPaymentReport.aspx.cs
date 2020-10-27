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
using System.Text;

public partial class Bill_Sys_MiscPaymentReport : PageBase
{
    Bill_Sys_ReportBO _reportBO;
    InvoiceDAO _InvoiceDAO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlChkDateValues.Attributes.Add("onChange", "javascript:CheckSetDate();");
            txtFromDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
            txtToDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
            txtChkFromDate.Attributes.Add("onblur", "javascript:return CheckFromDateValidation(this);");
            txtChkToDate.Attributes.Add("onblur", "javascript:return CheckToDateValidation(this);");
            extddlUser.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;




            if (!IsPostBack)
            {
                if (Request.QueryString["fromcase"] != null)
                {
                    txtCaseNo.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                    txtCaseNo.ReadOnly = true;

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ExportToExcel();
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

    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder strHtml = new StringBuilder();

            strHtml.Append("<table border='1px'>");
            strHtml.Append("<tr>");
            strHtml.Append("<td>");
            strHtml.Append("<b>" + lblChkAmount.Text + " " + "</b>" + lblChkAmountvalue.Text + "</td>");
            strHtml.Append("</td>");
            strHtml.Append("</tr>");
            strHtml.Append("</table>");

            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdExcelMiscPayment.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdExcelMiscPayment.Columns.Count; i++)
                    {
                        if (grdExcelMiscPayment.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append("<b>");
                            strHtml.Append(grdExcelMiscPayment.Columns[i].HeaderText);
                            strHtml.Append("</b>");
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdExcelMiscPayment.Columns.Count; j++)
                {
                    if (grdExcelMiscPayment.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdExcelMiscPayment.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);

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
    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }   

    public void SumCheckAmount()
    {
        decimal SumOfCheckAmount = 0;
        DataSet Sumds = (DataSet)Session["Excelgrid"];
        for (int i = 0; i < Sumds.Tables[0].Rows.Count; i++)
        {
            SumOfCheckAmount = SumOfCheckAmount + Convert.ToDecimal(Sumds.Tables[0].Rows[i]["RECEIVED"].ToString());
        }
        lblChkAmount.Visible = true;
        lblChkAmountvalue.Visible = true;
        lblChkAmountvalue.Text = '$' + SumOfCheckAmount.ToString("0.00");
    }     

    protected void grdMiscPayment_RowCommand(object sender, DataGridCommandEventArgs e)
    {
        
        _InvoiceDAO = new InvoiceDAO();
        int index = Convert.ToInt32(e.CommandArgument);
        LinkButton lnkInvoice = (LinkButton)grdPayment.Items[index].FindControl("lnkInvoice_Id");
        DataSet ds = new DataSet();
        if (e.CommandName == "Invoice Id")
        {
            ds=_InvoiceDAO.GetMiscPaymentDetails(lnkInvoice.Text,txtCompanyID.Text);
            grdListOfPayment.DataSource = ds;
            grdListOfPayment.DataBind();
            ModalPopupExtender.Show();
             
        }
        //#region "Grid Sorting"
        //if (e.CommandName.ToString() == "Invoice Date")
        //{
        //    if (txtSort.Text == e.CommandArgument + " ASC")
        //    {
        //        txtSort.Text = e.CommandArgument + "  DESC";
        //    }
        //    else
        //    {
        //        txtSort.Text = e.CommandArgument + " ASC";
        //    }

        //}
        //else if (e.CommandName.ToString() == "Patient Name")
        //{
        //    if (txtSort.Text == e.CommandArgument + " ASC")
        //    {
        //        txtSort.Text = e.CommandArgument + "  DESC";
        //    }
        //    else
        //    {
        //        txtSort.Text = e.CommandArgument + " ASC";
        //    }

        //}
       
        //else if (e.CommandName.ToString() == "Case No")
        //{
        //    if (txtSearchOrder.Text == "SZ_SORT_CASE_NO ASC")
        //    {
        //        txtSearchOrder.Text = "SZ_SORT_CASE_NO" + "  DESC";
        //        txtSort.Text = txtSearchOrder.Text;
        //    }
        //    else
        //    {
        //        txtSearchOrder.Text = "SZ_SORT_CASE_NO" + " ASC";
        //        txtSort.Text = txtSearchOrder.Text;
        //    }

        //}
       
      

        //if (grdExcelMiscPayment.Items.Count > 0)
        //{
        //    DataSet ds1 = new DataSet();
        //    DataTable objTable = new DataTable();
        //    ds1 = (DataSet)Session["Excelgrid"];
        //    objTable = ds1.Tables[0].Rows[0].Table;

        //    objTable.DefaultView.Sort = txtSort.Text;
        //    grdPayment.CurrentPageIndex = 0;
        //    grdPayment.DataSource = objTable;
        //    grdPayment.DataBind();
        //    grdExcelMiscPayment.DataSource = objTable;
        //    grdExcelMiscPayment.DataBind();

        //}
        //#endregion
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtender.Hide();
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            _InvoiceDAO = new InvoiceDAO();
            DataSet objDS = new DataSet();
            ArrayList arrlst = new ArrayList();
            arrlst.Add(txtCompanyID.Text);
            arrlst.Add(txtPatientName.Text);
            arrlst.Add(txtCaseNo.Text);
            arrlst.Add(extddlUser.Text);
            arrlst.Add(txtFromDate.Text);
            arrlst.Add(txtToDate.Text);
            arrlst.Add(txtChkFromDate.Text);
            arrlst.Add(txtChkToDate.Text);
            arrlst.Add(txtChkNumber.Text);
            arrlst.Add(txtChkAmount.Text);


            objDS = _InvoiceDAO.GetMiscPaymentDetails(arrlst);
            grdPayment.DataSource = objDS;
            grdPayment.DataBind();

            grdExcelMiscPayment.DataSource = objDS;
            grdExcelMiscPayment.DataBind();
            
            Session["Excelgrid"] = objDS;
            if (objDS.Tables[0].Rows.Count > 0)
            {
                SumCheckAmount();
            }
            else
            {
                lblChkAmountvalue.Visible = false;
                lblChkAmount.Visible = false;
            }

            DataTable objDT = new DataTable();
            objDT.Columns.Add("I_INVOICE_DATE");
            objDT.Columns.Add("I_INVOICE_ID");
            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_PATIENT_NAME");
            objDT.Columns.Add("SZ_CASE_TYPE");
            objDT.Columns.Add("BILLED");
            objDT.Columns.Add("RECEIVED");
            objDT.Columns.Add("OUTSTANDING");
           
            DataRow objDR;
            string sz_provider_name = "NA";


            decimal ForGroupSumTotalBillAmount = 0;
            decimal ForGroupSumTotalPaidAmount = 0;
            decimal ForGroupSumTotalOutstandingAmount = 0;


            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;

            for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                if (objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString().Equals(sz_provider_name))
                {
                    objDR = objDT.NewRow();
                    objDR["I_INVOICE_DATE"] = objDS.Tables[0].Rows[i]["I_INVOICE_DATE"].ToString();
                    objDR["I_INVOICE_ID"] = objDS.Tables[0].Rows[i]["I_INVOICE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CASE_TYPE"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE"].ToString();
                    objDR["BILLED"] = objDS.Tables[0].Rows[i]["BILLED"].ToString();
                    objDR["RECEIVED"] = objDS.Tables[0].Rows[i]["RECEIVED"].ToString();
                    objDR["OUTSTANDING"] = objDS.Tables[0].Rows[i]["OUTSTANDING"].ToString();
                  

                    objDT.Rows.Add(objDR);
                    sz_provider_name = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();

                    ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["BILLED"].ToString());
                    ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["RECEIVED"].ToString());
                    ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["OUTSTANDING"].ToString());

                    SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["BILLED"].ToString());
                    SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["RECEIVED"].ToString());
                    SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["OUTSTANDING"].ToString());

                }
                else
                {
                    if (i != 0)
                    {
                        objDR = objDT.NewRow();
                        objDR["I_INVOICE_DATE"] = "";
                        objDR["I_INVOICE_ID"] = "";
                        objDR["SZ_CASE_NO"] = "";
                        objDR["SZ_PATIENT_NAME"] = "";
                        objDR["SZ_CASE_TYPE"] = "<b>Total by P.c</b>";
                        objDR["BILLED"] = "<b>$" + ForGroupSumTotalBillAmount.ToString() + "</b>";
                        objDR["RECEIVED"] = "<b>$" + ForGroupSumTotalPaidAmount.ToString() + "</b>";
                        objDR["OUTSTANDING"] = "<b>$" + ForGroupSumTotalOutstandingAmount.ToString() + "</b>";
                      
                        objDT.Rows.Add(objDR);
                        ForGroupSumTotalBillAmount = 0.00M;
                        ForGroupSumTotalPaidAmount = 0.00M;
                        ForGroupSumTotalOutstandingAmount = 0.00M;
                    }

                    ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["BILLED"].ToString());
                    ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["RECEIVED"].ToString());
                    ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["OUTSTANDING"].ToString());

                    SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["BILLED"].ToString());
                    SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["RECEIVED"].ToString());
                    SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["OUTSTANDING"].ToString());


                    objDR = objDT.NewRow();
                    objDR["I_INVOICE_DATE"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString() + "</b>";
                    objDR["I_INVOICE_ID"] = "";
                    objDR["SZ_CASE_NO"] = "";
                    objDR["SZ_PATIENT_NAME"] = "";
                    objDR["SZ_CASE_TYPE"] = "";
                    objDR["BILLED"] = "";
                    objDR["RECEIVED"] = "";
                    objDR["OUTSTANDING"] = "";
                  
                    objDT.Rows.Add(objDR);

                    objDR = objDT.NewRow();
                    objDR["I_INVOICE_DATE"] = objDS.Tables[0].Rows[i]["I_INVOICE_DATE"].ToString();
                    objDR["I_INVOICE_ID"] = objDS.Tables[0].Rows[i]["I_INVOICE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CASE_TYPE"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE"].ToString();
                    objDR["BILLED"] = objDS.Tables[0].Rows[i]["BILLED"].ToString();
                    objDR["RECEIVED"] = objDS.Tables[0].Rows[i]["RECEIVED"].ToString();
                    objDR["OUTSTANDING"] = objDS.Tables[0].Rows[i]["OUTSTANDING"].ToString();
                   
                    objDT.Rows.Add(objDR);

                    sz_provider_name = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();

                }
            }

            if (objDS.Tables[0].Rows.Count > 0)
            {

                objDR = objDT.NewRow();
                objDR["I_INVOICE_DATE"] = "";
                objDR["I_INVOICE_ID"] = "";
                objDR["SZ_CASE_NO"] = "";
                objDR["SZ_PATIENT_NAME"] = "";
                objDR["SZ_CASE_TYPE"] = "<b>Total by P.c</b>";
                objDR["BILLED"] = "<b>$" + ForGroupSumTotalBillAmount.ToString() + "</b>";
                objDR["RECEIVED"] = "<b>$" + ForGroupSumTotalPaidAmount.ToString() + "</b>";
                objDR["OUTSTANDING"] = "<b>$" + ForGroupSumTotalOutstandingAmount.ToString() + "</b>";
               

                objDT.Rows.Add(objDR);



                objDR = objDT.NewRow();
                objDR["I_INVOICE_DATE"] = "";
                objDR["I_INVOICE_ID"] = "";
                objDR["SZ_CASE_NO"] = "";
                objDR["SZ_PATIENT_NAME"] = "";
                objDR["SZ_CASE_TYPE"] = "<b>Total</b>";
                objDR["BILLED"] = "<b>$" + SumTotalBillAmount.ToString() + "</b>";
                objDR["RECEIVED"] = "<b>$" + SumTotalPaidAmount.ToString() + "</b>";
                objDR["OUTSTANDING"] = "<b>$" + SumTotalOutstandingAmount.ToString() + "</b>";
                

                objDT.Rows.InsertAt(objDR, 0);
            }


            grdPayment.DataSource = objDT;
            grdPayment.DataBind();

            grdExcelMiscPayment.DataSource = objDT;
            grdExcelMiscPayment.DataBind();
            


            // AddAmount();
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
