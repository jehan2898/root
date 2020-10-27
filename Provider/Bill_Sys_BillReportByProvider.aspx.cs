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
using System.IO;

public partial class Bill_Sys_BillReportByProvider : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        try
        {
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
                {
                    extddlLocation.Visible = true;
                    lblLocationName.Visible = true;
                    extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
                }
                BindGrid();
                txtFromDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
                txtToDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
            }
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_BillReportByProvider.aspx");
        }
        #endregion
    }
   

    public void BindGrid()
    {
        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            ArrayList arrlst = new ArrayList();
            arrlst.Add(txtCompanyID.Text);
            arrlst.Add(txtFromDate.Text);
            arrlst.Add(txtToDate.Text);
            arrlst.Add(txtProvider.Text);
            arrlst.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString());
            // send parameter Location_Id to function GetBillReportProvider
            arrlst.Add(extddlLocation.Text);
            DataSet objDS = new DataSet();
            objDS = _reportBO.GetBillReportForProvider(arrlst);

            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;
            for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
            }
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].NewRow();
                objDR["PROVIDER_NAME"] = "<b>Total</b>";
                objDR["TOTAL_BILL_AMOUNT"] = "<b>$" + SumTotalBillAmount.ToString() + "</b>";
                objDR["TOTAL_PAID_AMOUNT"] = "<b>$" + SumTotalPaidAmount.ToString() + "</b>";
                objDR["TOTAL_OUTSTANDING_AMOUNT"] = "<b>$" + SumTotalOutstandingAmount.ToString() + "</b>";
                objDS.Tables[0].Rows.InsertAt(objDR, 0);
            }

            grdPayment.DataSource = objDS;

            grdPayment.DataBind();
           
           // AddAmount();
            LinkButton show=null;
            if (objDS.Tables[0].Rows.Count > 0)
            {
                show = (LinkButton)grdPayment.Items[0].FindControl("lnkshow");
                show.Visible = false;
            }
            if(txtFromDate.Text!="")
                Session["ReportFromDate"] = txtFromDate.Text;
            else
                Session["ReportFromDate"] = DateTime.Parse("Jan 1, 2009");
            if(txtToDate.Text!="")
                Session["ReportToDate"] = txtToDate.Text;
            else
                Session["ReportToDate"] = DateTime.Today;
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
    protected void btnSearch_Click1(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ExportToExcel();
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    private void ExportToExcel()
    {
        try
        {
            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdPayment.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdPayment.Columns.Count; i++)
                    {
                        if (grdPayment.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdPayment.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdPayment.Columns.Count; j++)
                {
                    if (grdPayment.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        if (j == 1)
                        {
                            strHtml.Append(grdPayment.Items[icount].Cells[2].Text);
                        }
                        else
                        {
                            strHtml.Append(grdPayment.Items[icount].Cells[j].Text);
                        }
                        strHtml.Append("</td>");
                    }
                }
            }
            strHtml.Append("</tr>");
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();
            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
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

    public void AddAmount()
    {
        decimal SumTotalBillAmount = 0;
        decimal SumTotalPaidAmount = 0;
        decimal SumTotalOutstandingAmount = 0;
        try
        {
            for (int i = 0; i < grdPayment.Items.Count; i++)
            {
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(grdPayment.Items[i].Cells[1].Text.Remove(0,1));
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(grdPayment.Items[i].Cells[2].Text.Remove(0,1));
                SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(grdPayment.Items[i].Cells[3].Text.Remove(0,1));
            }
            Session["SumTotalBillAmount"] = (SumTotalBillAmount).ToString();
            Session["SumTotalPaidAmount"] = (SumTotalPaidAmount).ToString();
            Session["SumTotalOutstandingAmount"] = (SumTotalOutstandingAmount).ToString();
            lblTotalBillAmount.Text = "Total Bill Amount : <b> $" + (SumTotalBillAmount).ToString() + "</b>";
            lblTotalPaidAmount.Text = "Total Paid Amount : <b> $" + (SumTotalPaidAmount).ToString() + "</b>";
            lblTotalOutstandingAmount.Text = "Total Outstanding Amount : <b> $" + (SumTotalOutstandingAmount).ToString() + "</b>"; ;
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }

    protected void grdPayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "OpenDetailReport")
            {
                Session["SZ_OFFICE_ID"] = e.Item.Cells[5].Text;
                Session["sz_From_Date"]=txtFromDate.Text;
                Session["sz_To_Date"]=txtToDate.Text;
                Session["SZ_LOCATION_id"] = extddlLocation.Text;
                Session["sz_Office_Id_Text"] = txtProvider.Text;
                Response.Redirect("~/Provider/Bill_Sys_DetailBillReportProvider.aspx", false);
            }
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
}
