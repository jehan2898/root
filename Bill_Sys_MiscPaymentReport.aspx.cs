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
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        ddlChkDateValues.Attributes.Add("onChange", "javascript:CheckSetDate();");
        txtFromDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
        txtToDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
        txtChkFromDate.Attributes.Add("onblur", "javascript:return CheckFromDateValidation(this);");
        txtChkToDate.Attributes.Add("onblur", "javascript:return CheckToDateValidation(this);");
        if (!IsPostBack)
        {
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (Request.QueryString["fromcase"] != null)
            {
                txtCaseNo.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                txtCaseNo.ReadOnly = true;
                
            }
            BindGrid();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            ArrayList arrlst= new ArrayList();
            arrlst.Add(txtCompanyID.Text);
            arrlst.Add(txtFromDate.Text);
            arrlst.Add(txtToDate.Text);
            arrlst.Add(txtPatientName.Text);
            arrlst.Add(txtCaseNo.Text);
           // arrlst.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            arrlst.Add(txtChkFromDate.Text);
            arrlst.Add(txtChkToDate.Text);
            arrlst.Add(txtChkNumber.Text);
            arrlst.Add(txtChkAmount.Text);
            DataSet ds = new DataSet();
            ds = _reportBO.GetMiscPaymentDetails(arrlst);
            grdPayment.DataSource = ds;
            grdPayment.DataBind();

            grdExcelMiscPayment.DataSource = ds;
            grdExcelMiscPayment.DataBind();
            Session["Excelgrid"] = ds;
            if (ds.Tables[0].Rows.Count > 0)
            {
                SumCheckAmount();
            }
            else
            {
                lblChkAmountvalue.Visible = false;
                lblChkAmount.Visible = false;
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
                            strHtml.Append(grdExcelMiscPayment.Columns[i].HeaderText);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
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

    protected void grdPayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        #region "Grid Sorting"
        if (e.CommandName.ToString() == "Payment Date")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Patient Name")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Case Type")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Case No")
        {
            if (txtSearchOrder.Text == "SZ_SORT_CASE_NO ASC")
            {
                txtSearchOrder.Text = "SZ_SORT_CASE_NO" + "  DESC";
                txtSort.Text = txtSearchOrder.Text;
            }
            else
            {
                txtSearchOrder.Text = "SZ_SORT_CASE_NO" + " ASC";
                txtSort.Text = txtSearchOrder.Text;
            }

        }
        else if (e.CommandName.ToString() == "Check Number")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Check Date")
        {
            if (txtSort.Text == e.CommandArgument + " ASC")
            {
                txtSort.Text = e.CommandArgument + "  DESC";
            }
            else
            {
                txtSort.Text = e.CommandArgument + " ASC";
            }

        }
        else if (e.CommandName.ToString() == "Check Amount")
        {
            if (txtSearchOrder.Text == "SZ_CHECK_AMOUNT_SUM ASC")
            {
                txtSearchOrder.Text = "SZ_CHECK_AMOUNT_SUM" + "  DESC";
                txtSort.Text = txtSearchOrder.Text;
            }
            else
            {
                txtSearchOrder.Text = "SZ_CHECK_AMOUNT_SUM" + " ASC";
                txtSort.Text = txtSearchOrder.Text;
            }
        }

        if (grdExcelMiscPayment.Items.Count > 0)
        {
            DataSet ds1 = new DataSet();
            DataTable objTable = new DataTable();
            ds1 = (DataSet)Session["Excelgrid"];
            objTable = ds1.Tables[0].Rows[0].Table;

            objTable.DefaultView.Sort = txtSort.Text;
            grdPayment.CurrentPageIndex = 0;
            grdPayment.DataSource = objTable;
            grdPayment.DataBind();
            grdExcelMiscPayment.DataSource  = objTable;
            grdExcelMiscPayment.DataBind();
            
        }
    #endregion
    }

    public void SumCheckAmount()
    {
        decimal SumOfCheckAmount=0;
        DataSet Sumds = (DataSet)Session["Excelgrid"];
        for (int i = 0; i < Sumds.Tables[0].Rows.Count; i++)
        {
            SumOfCheckAmount = SumOfCheckAmount + Convert.ToDecimal(Sumds.Tables[0].Rows[i]["SZ_CHECK_AMOUNT_SUM"].ToString());
        }
        lblChkAmount.Visible = true;
        lblChkAmountvalue.Visible = true;
        lblChkAmountvalue.Text = '$' +   SumOfCheckAmount.ToString("0.00");
    }
}
