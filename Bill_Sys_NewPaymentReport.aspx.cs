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

public partial class Bill_Sys_NewPaymentReport : PageBase
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
                extddlUser.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
                {
                    extddlLocation.Visible = true;
                    lblLocationName.Visible = true;
                    extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
                }
                if (Request.QueryString["fromCase"] != null)
                {
                    txtCaseNo.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
                    txtCaseNo.ReadOnly = true;
                    BindGrid();
                }
            }
            txtFromDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
            txtToDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
            txtChkFromDate.Attributes.Add("onblur", "javascript:return CheckFromDateValidation(this);");
            txtChkToDate.Attributes.Add("onblur", "javascript:return CheckToDateValidation(this);");
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
            cv.MakeReadOnlyPage("Bill_Sys_NewPaymentReport.aspx");
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
            arrlst.Add(txtPatientName.Text);
            arrlst.Add(txtCaseNo.Text);
            arrlst.Add(extddlLocation.Text);
            arrlst.Add(txtChkFromDate.Text);
            arrlst.Add(txtChkToDate.Text);
            arrlst.Add(txtChkNumber.Text);
            arrlst.Add(txtChkAmount.Text);
            arrlst.Add(extddlUser.Text);
            DataSet objDS = new DataSet();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                objDS = _reportBO.GetPaymentDetailsMRI(arrlst);
            }
            else
            {
                objDS = _reportBO.GetPaymentDetails(arrlst);
            }

            DataTable objDT = new DataTable();
            objDT.Columns.Add("BILL_DATE");
            objDT.Columns.Add("SZ_BILL_NUMBER");
            objDT.Columns.Add("VISIT_DATE");
            objDT.Columns.Add("PATIENT_NAME");
            objDT.Columns.Add("CASE_TYPE");
            objDT.Columns.Add("BILLED");
            objDT.Columns.Add("RECEIVED");
            objDT.Columns.Add("OUTSTANDING");
            objDT.Columns.Add("SZ_COMMENT");
            objDT.Columns.Add("DOCTOR_NAME");
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
                if (objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString().Equals(sz_provider_name))
                {
                    objDR = objDT.NewRow();
                    objDR["BILL_DATE"] = objDS.Tables[0].Rows[i]["DT_BILL_DATE"].ToString();
                    objDR["SZ_BILL_NUMBER"] = objDS.Tables[0].Rows[i]["SZ_BILL_NUMBER"].ToString();
                    objDR["VISIT_DATE"] = objDS.Tables[0].Rows[i]["DT_FIRST_VISIT_DATE"].ToString();
                    objDR["PATIENT_NAME"] = objDS.Tables[0].Rows[i]["PATIENT_NAME"].ToString();
                    objDR["CASE_TYPE"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE"].ToString();
                    objDR["BILLED"] = objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString();
                    objDR["RECEIVED"] = objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString();
                    objDR["OUTSTANDING"] = objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString();
                    objDR["SZ_COMMENT"] = objDS.Tables[0].Rows[i]["SZ_COMMENT"].ToString();
                    objDR["DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["DOCTOR_NAME"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_provider_name = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();

                    ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0,1));
                    ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                    ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));

                    SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                    SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                    SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));

                }
                else
                {
                    if (i != 0)
                    {
                        objDR = objDT.NewRow();
                        objDR["BILL_DATE"] = "";
                        objDR["SZ_BILL_NUMBER"] = "";
                        objDR["VISIT_DATE"] = "";
                        objDR["PATIENT_NAME"] = "";
                        objDR["CASE_TYPE"] = "<b>Total by P.c</b>";
                        objDR["BILLED"] = "<b>$" + ForGroupSumTotalBillAmount.ToString() + "</b>";
                        objDR["RECEIVED"] = "<b>$" + ForGroupSumTotalPaidAmount.ToString() + "</b>";
                        objDR["OUTSTANDING"] = "<b>$" + ForGroupSumTotalOutstandingAmount.ToString() + "</b>";
                        objDR["SZ_COMMENT"] = objDS.Tables[0].Rows[i]["SZ_COMMENT"].ToString();
                        objDR["DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["DOCTOR_NAME"].ToString();
                        objDT.Rows.Add(objDR);
                        ForGroupSumTotalBillAmount = 0.00M;
                        ForGroupSumTotalPaidAmount = 0.00M;
                        ForGroupSumTotalOutstandingAmount = 0.00M;
                    }

                    ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                    ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                    ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));

                    SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                    SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                    SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));


                    objDR = objDT.NewRow();
                    objDR["BILL_DATE"] = "<b>" + objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString() + "</b>";
                    objDR["SZ_BILL_NUMBER"] = "";
                    objDR["VISIT_DATE"] = "";
                    objDR["PATIENT_NAME"] = "";
                    objDR["CASE_TYPE"] = "";
                    objDR["BILLED"] = "";
                    objDR["RECEIVED"] = "";
                    objDR["OUTSTANDING"] = "";
                    objDR["SZ_COMMENT"] = "";
                    objDR["DOCTOR_NAME"] = "";
                    objDT.Rows.Add(objDR);

                    objDR = objDT.NewRow();
                    objDR["BILL_DATE"] = objDS.Tables[0].Rows[i]["DT_BILL_DATE"].ToString();
                    objDR["SZ_BILL_NUMBER"] = objDS.Tables[0].Rows[i]["SZ_BILL_NUMBER"].ToString();
                    objDR["VISIT_DATE"] = objDS.Tables[0].Rows[i]["DT_FIRST_VISIT_DATE"].ToString();
                    objDR["PATIENT_NAME"] = objDS.Tables[0].Rows[i]["PATIENT_NAME"].ToString();
                    objDR["CASE_TYPE"] = objDS.Tables[0].Rows[i]["SZ_CASE_TYPE"].ToString();
                    objDR["BILLED"] = objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString();
                    objDR["RECEIVED"] = objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString();
                    objDR["OUTSTANDING"] = objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString();
                    objDR["SZ_COMMENT"] = objDS.Tables[0].Rows[i]["SZ_COMMENT"].ToString();
                    objDR["DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["DOCTOR_NAME"].ToString();
                    objDT.Rows.Add(objDR);

                    sz_provider_name = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();

                }
            }

            if (objDS.Tables[0].Rows.Count > 0)
            {

                objDR = objDT.NewRow();
                objDR["BILL_DATE"] = "";
                objDR["SZ_BILL_NUMBER"] = "";
                objDR["VISIT_DATE"] = "";
                objDR["PATIENT_NAME"] = "";
                objDR["CASE_TYPE"] = "<b>Total by P.c</b>";
                objDR["BILLED"] = "<b>$" + ForGroupSumTotalBillAmount.ToString() + "</b>";
                objDR["RECEIVED"] = "<b>$" + ForGroupSumTotalPaidAmount.ToString() + "</b>";
                objDR["OUTSTANDING"] = "<b>$" + ForGroupSumTotalOutstandingAmount.ToString() + "</b>";
                objDR["SZ_COMMENT"] = "";
                objDT.Rows.Add(objDR);

                

                objDR = objDT.NewRow();
                objDR["BILL_DATE"] = "";
                objDR["SZ_BILL_NUMBER"] = "";
                objDR["VISIT_DATE"] = "";
                objDR["PATIENT_NAME"] = "";
                objDR["CASE_TYPE"] = "<b>Total</b>";
                objDR["BILLED"] = "<b>$" + SumTotalBillAmount.ToString() + "</b>";
                objDR["RECEIVED"] = "<b>$" + SumTotalPaidAmount.ToString() + "</b>";
                objDR["OUTSTANDING"] = "<b>$" + SumTotalOutstandingAmount.ToString() + "</b>";
                objDR["SZ_COMMENT"] = "";
                objDT.Rows.InsertAt(objDR,0);
            }


            grdPayment.DataSource = objDT;
            grdPayment.DataBind();

            if (Request.QueryString["fromCase"] == null)
            {
                grdPayment.Columns[3].Visible = true;
            }
            else
            {
                grdPayment.Columns[3].Visible = false;
            }


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
                        strHtml.Append(grdPayment.Items[icount].Cells[j].Text);
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
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(grdPayment.Items[i].Cells[4].Text);
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(grdPayment.Items[i].Cells[5].Text);
                SumTotalOutstandingAmount = SumTotalOutstandingAmount  + Convert.ToDecimal(grdPayment.Items[i].Cells[6].Text);
            }
            lblTotalBillAmount.Text = "Total Bill Amount : <b>" + (SumTotalBillAmount).ToString() + "</b>";
            lblTotalPaidAmount.Text = "Total Paid Amount : <b>" + (SumTotalPaidAmount).ToString() + "</b>";
            lblTotalOutstandingAmount.Text = "Total Outstanding Amount : <b>" + (SumTotalOutstandingAmount).ToString() + "</b>"; ;

            
            
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
