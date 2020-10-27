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

public partial class Bill_Sys_NewBillReportBySpeciality : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        try
        {
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
                {
                    extddlLocation.Visible = true;
                    lblLocationName.Visible = true;
                    extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
                }
                extddCaseType.Flag_ID = txtCompanyID.Text.ToString();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_NewBillReportBySpeciality.aspx");
        }
        #endregion
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
            ArrayList arrlst = new ArrayList();
            arrlst.Add(txtCompanyID.Text);
            arrlst.Add(txtFromDate.Text);
            arrlst.Add(txtToDate.Text);
            arrlst.Add(extddlSpeciality.Text);
            arrlst.Add(extddlLocation.Text);
            //added on 24/12/2014
            arrlst.Add(extddCaseType.Text);
            //added on 24/12/2014
            DataSet objDS = new DataSet();
            objDS = _reportBO.GetPaymentReportBySpeciality(arrlst);

            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;
            for (int i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString());
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString());
                SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString());
            }

            if (objDS.Tables[0].Rows.Count > 1)
            {
                DataRow objDR = objDS.Tables[0].NewRow();
                objDR["SPECIALITY"] = "<b>Total</b>";
                objDR["TOTAL_BILL_AMOUNT"] = "<b>$" + SumTotalBillAmount.ToString() + "</b>";
                objDR["TOTAL_PAID_AMOUNT"] = "<b>$" + SumTotalPaidAmount.ToString() + "</b>";
                objDR["TOTAL_OUTSTANDING_AMOUNT"] = "<b>$" + SumTotalOutstandingAmount.ToString() + "</b>";
                objDR["SZ_SPECIALITY_ID"] = "";
                objDS.Tables[0].Rows.InsertAt(objDR, 0);
            }

            grdPayment.DataSource = objDS;
            grdPayment.DataBind();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void btnSearch_Click1(object sender, EventArgs e)
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

    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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

    public void AddAmount()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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

    protected void grdPayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "OpenDetailReport")
            {
                if (extddlLocation.Visible == true && extddlLocation.Text != "" && extddlLocation.Text != "NA")
                {
                    Session["Location_Id"] = extddlLocation.Text;
                }
                else
                {
                    Session["Location_Id"] = "";
                }
                if (e.Item.Cells[5].Text != "&nbsp;")
                {

                    Session["PR_SZ_PROCEDURE_GROUP"] = e.Item.Cells[5].Text;
                    Response.Redirect("Bill_Sys_DetailsPaymentBySpeciality.aspx", false);
                }
                else
                {
                    Session["PR_SZ_PROCEDURE_GROUP"] = "";
                    Response.Redirect("Bill_Sys_DetailsPaymentBySpeciality.aspx", false);
                }
                if (extddCaseType.Visible == true && extddCaseType.Text != "" && extddCaseType.Text != "NA")
                {
                    Session["Case_Type"] = extddCaseType.Text;
                }
                else
                {
                    Session["Case_Type"] = "";
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
}
