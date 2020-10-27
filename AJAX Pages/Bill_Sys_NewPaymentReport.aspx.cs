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
using System.Data;
using System.Data.SqlClient;
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
        drpcheckdate.Attributes.Add("onChange", "javascript:SetCheckDate();");
        ddlPaymentDateValues.Attributes.Add("onChange", "javascript:SetPaymentDate();");
        ddlvisitdate.Attributes.Add("onChange", "javascript:SetVisitDate();");
        try
        {
            if (!IsPostBack)
            {
                extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlUser.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlInsurance.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                chkColletion.Checked = false;
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
                    chkReportOnly.Checked = false;
                    chkReportOnly.Visible = false;
                    BindGrid();
                }
                else
                {
                    chkReportOnly.Checked = true;
                    chkReportOnly.Visible = true;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            string szCollection = "";
            if (chkColletion.Checked == true)
            {
                szCollection = "1";
            }
            else
            {
                szCollection = "0";
            }
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
            arrlst.Add(extddlInsurance.Text);
            arrlst.Add(txtPayFromDate.Text);
            arrlst.Add(txtPayToDate.Text);
            arrlst.Add(extddlCaseType.Text);
            arrlst.Add(szCollection);
            arrlst.Add(txtVisitDate.Text);
            arrlst.Add(txtToVisitDate.Text);
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
            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_PAYMENT");
            objDT.Columns.Add("SZ_DESCRIPTION");
            objDT.Columns.Add("SZ_PATH");
            objDT.Columns.Add("SZ_DENAIL");
            objDT.Columns.Add("CPT");
            objDT.Columns.Add("FLT_WRITE_OFF");
            objDT.Columns.Add("SZ_INSURANCE_COMPANY");
            objDT.Columns.Add("PaidInPeriod");
            //added on 22/12/2014
            objDT.Columns.Add("specialty");
            objDT.Columns.Add("dt_transferred_on");
            objDT.Columns.Add("mn_transferred_amount");
            objDT.Columns.Add("SZ_COMPANY_NAME");
            objDT.Columns.Add("PROVIDER_NAME");
            
            //added on 22/12/2014
            DataRow objDR;
            string sz_provider_name = "NA";


            decimal ForGroupSumTotalBillAmount = 0;
            decimal ForGroupSumTotalPaidAmount = 0;
            decimal ForGroupSumTotalOutstandingAmount = 0;
            decimal ForGroupSumTotalwriteoffAmount = 0;
            decimal ForGroupSumTotalChkAmt = 0;

            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;
            decimal SumTotalwriteoffAmount = 0;
            decimal SumTotalChkAmt = 0;

            if (objDS.Tables[0].Rows.Count > 0)
            {
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
                        objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                        objDR["SZ_PAYMENT"] = objDS.Tables[0].Rows[i]["SZ_PAYMENT"].ToString();
                        objDR["SZ_DESCRIPTION"] = objDS.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        objDR["SZ_PATH"] = objDS.Tables[0].Rows[i]["SZ_PATH"].ToString();
                        objDR["SZ_DENAIL"] = objDS.Tables[0].Rows[i]["SZ_DENAIL"].ToString();
                        objDR["CPT"] = objDS.Tables[0].Rows[i]["CPT"].ToString();
                        objDR["FLT_WRITE_OFF"] = objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString();
                        objDR["SZ_INSURANCE_COMPANY"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_COMPANY"].ToString();
                        objDR["PaidInPeriod"] = objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString();
                        //added on 22/12/2014
                        objDR["specialty"] = objDS.Tables[0].Rows[i]["specialty"].ToString();
                        objDR["dt_transferred_on"] = objDS.Tables[0].Rows[i]["dt_transferred_on"].ToString();
                        objDR["mn_transferred_amount"] = objDS.Tables[0].Rows[i]["mn_transferred_amount"].ToString();
                        objDR["SZ_COMPANY_NAME"] = objDS.Tables[0].Rows[i]["SZ_COMPANY_NAME"].ToString();
                        objDR["PROVIDER_NAME"] = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();
                        
                        objDT.Rows.Add(objDR);
                        sz_provider_name = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();

                        ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalwriteoffAmount = ForGroupSumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));
                        ForGroupSumTotalChkAmt = ForGroupSumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));

                        SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalwriteoffAmount = SumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));
                        SumTotalChkAmt = SumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));

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
                            objDR["SZ_CASE_NO"] = "";
                            objDR["SZ_PAYMENT"] = "";
                            objDR["DOCTOR_NAME"] = "";
                            objDR["SZ_DESCRIPTION"] = "";
                            objDR["SZ_PATH"] = "";
                            objDR["SZ_DENAIL"] = "";
                            objDR["CPT"] = "";
                            objDR["FLT_WRITE_OFF"] = "<b>$" + ForGroupSumTotalwriteoffAmount.ToString() + "</b>";
                            objDR["PaidInPeriod"] = "<b>$" + ForGroupSumTotalChkAmt.ToString() + "</b>";
                            objDR["SZ_INSURANCE_COMPANY"] = "";
                            //added on 22/12/2014
                            objDR["specialty"] = "";
                            objDR["dt_transferred_on"] = "";
                            objDR["mn_transferred_amount"] = "";
                            objDR["SZ_COMPANY_NAME"] = "";
                            objDR["PROVIDER_NAME"] = "";
                            
                            objDT.Rows.Add(objDR);
                            ForGroupSumTotalBillAmount = 0.00M;
                            ForGroupSumTotalPaidAmount = 0.00M;
                            ForGroupSumTotalOutstandingAmount = 0.00M;
                            ForGroupSumTotalwriteoffAmount = 0.00M;
                            ForGroupSumTotalChkAmt = 0.00M;
                        }

                        ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalwriteoffAmount = ForGroupSumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));
                        ForGroupSumTotalChkAmt = ForGroupSumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));

                        SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalChkAmt = SumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));
                        SumTotalwriteoffAmount = SumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));


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
                        objDR["SZ_CASE_NO"] = "";
                        objDR["SZ_PAYMENT"] = "";
                        objDR["SZ_DESCRIPTION"] = "";
                        objDR["SZ_PATH"] = "";
                        objDR["SZ_DENAIL"] = "";
                        objDR["CPT"] = "";
                        objDR["FLT_WRITE_OFF"] = "";
                        objDR["SZ_INSURANCE_COMPANY"] = "";
                        objDR["PaidInPeriod"] = "";
                        //added on 22/12/2014
                        objDR["specialty"] = "";
                        objDR["dt_transferred_on"] = "";
                        objDR["mn_transferred_amount"] = "";
                        objDR["SZ_COMPANY_NAME"] = "";
                        objDR["PROVIDER_NAME"] = "";
                        
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
                        objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                        objDR["SZ_PAYMENT"] = objDS.Tables[0].Rows[i]["SZ_PAYMENT"].ToString();
                        objDR["SZ_DESCRIPTION"] = objDS.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        objDR["SZ_PATH"] = objDS.Tables[0].Rows[i]["SZ_PATH"].ToString();
                        objDR["SZ_DENAIL"] = objDS.Tables[0].Rows[i]["SZ_DENAIL"].ToString();
                        objDR["CPT"] = objDS.Tables[0].Rows[i]["CPT"].ToString();
                        objDR["FLT_WRITE_OFF"] = objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString();
                        objDR["SZ_INSURANCE_COMPANY"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_COMPANY"].ToString();
                        objDR["PaidInPeriod"] = objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString();
                        //added on 22/12/2014
                        objDR["specialty"] = objDS.Tables[0].Rows[i]["specialty"].ToString();
                        objDR["dt_transferred_on"] = objDS.Tables[0].Rows[i]["dt_transferred_on"].ToString();
                        objDR["mn_transferred_amount"] = objDS.Tables[0].Rows[i]["mn_transferred_amount"].ToString();
                        objDR["SZ_COMPANY_NAME"] = objDS.Tables[0].Rows[i]["SZ_COMPANY_NAME"].ToString();
                        objDR["PROVIDER_NAME"] = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();
                        
                        objDT.Rows.Add(objDR);

                        sz_provider_name = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();

                    }
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
                objDR["SZ_CASE_NO"] = "";
                objDR["SZ_PAYMENT"] = "";
                objDR["DOCTOR_NAME"] = "";
                objDR["SZ_DESCRIPTION"] = "";
                objDR["SZ_PATH"] = "";
                objDR["SZ_DENAIL"]="";
                objDR["CPT"] = "";
                objDR["FLT_WRITE_OFF"] = "<b>$" + ForGroupSumTotalwriteoffAmount.ToString() + "</b>";
                objDR["PaidInPeriod"] = "<b>$" + ForGroupSumTotalChkAmt.ToString() + "</b>";
                objDR["SZ_INSURANCE_COMPANY"] = "";
                //added on 22/12/2014
                objDR["specialty"] = "";
                objDR["dt_transferred_on"] = "";
                objDR["mn_transferred_amount"] = "";
                objDR["SZ_COMPANY_NAME"] = "";
                objDR["PROVIDER_NAME"] = "";
                
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
                objDR["SZ_CASE_NO"] = "";
                objDR["SZ_PAYMENT"] = "";
                objDR["DOCTOR_NAME"] = "";
                objDR["SZ_DESCRIPTION"] = "";
                objDR["SZ_PATH"] = "";
                objDR["SZ_DENAIL"]="";
                objDR["CPT"] = "";
                objDR["FLT_WRITE_OFF"] = "<b>$" + SumTotalwriteoffAmount.ToString() + "</b>";
                objDR["PaidInPeriod"] = "<b>$" + SumTotalChkAmt.ToString() + "</b>";
                objDR["SZ_INSURANCE_COMPANY"] = "";
                //added on 22/12/2014
                objDR["specialty"] = "";
                objDR["dt_transferred_on"] = "";
                objDR["mn_transferred_amount"] = "";
                objDR["SZ_COMPANY_NAME"] = "";
                objDR["PROVIDER_NAME"] = "";
                objDT.Rows.InsertAt(objDR,0);
            }


            grdPayment.DataSource = objDT;
            grdPayment.DataBind();

            if (Request.QueryString["fromCase"] == null)
            {
                grdPayment.Columns[1].Visible = true; //move patient name from 3 to 1
            }
            else
            {
                grdPayment.Columns[1].Visible = false; 
            }

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                grdPayment.Columns[4].Visible = true;
            }
            else
            {
                grdPayment.Columns[4].Visible = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    public void BindGrid2()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _reportBO = new Bill_Sys_ReportBO();
        try
        {
            string szCollection = "";
            if (chkColletion.Checked == true)
            {
                szCollection = "1";
            }
            else
            {
                szCollection = "0";
            }

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
            arrlst.Add(extddlInsurance.Text);
            arrlst.Add(txtPayFromDate.Text);
            arrlst.Add(txtPayToDate.Text);
            arrlst.Add(extddlCaseType.Text);
            arrlst.Add(szCollection);
            arrlst.Add(txtVisitDate.Text);
            arrlst.Add(txtToVisitDate.Text);
            DataSet objDS = new DataSet();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                objDS = GetPaymentDetailsMRI(arrlst);
            }
            else
            {
                objDS = GetPaymentDetails(arrlst);
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
            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_PAYMENT");
            objDT.Columns.Add("SZ_DESCRIPTION");
            objDT.Columns.Add("SZ_PATH");
            objDT.Columns.Add("SZ_DENAIL");
            objDT.Columns.Add("CPT");
            objDT.Columns.Add("FLT_WRITE_OFF");
            objDT.Columns.Add("SZ_INSURANCE_COMPANY");
            objDT.Columns.Add("PaidInPeriod");
            //added on 22/12/2014
            objDT.Columns.Add("specialty");
            objDT.Columns.Add("dt_transferred_on");
            objDT.Columns.Add("mn_transferred_amount");
            objDT.Columns.Add("SZ_COMPANY_NAME");
            objDT.Columns.Add("PROVIDER_NAME");
            
            //added on 22/12/2014


            DataRow objDR;
            string sz_provider_name = "NA";


            decimal ForGroupSumTotalBillAmount = 0;
            decimal ForGroupSumTotalPaidAmount = 0;
            decimal ForGroupSumTotalOutstandingAmount = 0;
            decimal ForGroupSumTotalwriteoffAmount = 0;
            decimal ForGroupSumTotalChkAmt = 0;

            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;
            decimal SumTotalwriteoffAmount = 0;
            decimal SumTotalChkAmt = 0;

            if (objDS.Tables[0].Rows.Count > 0)
            {
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
                        objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                        objDR["SZ_PAYMENT"] = objDS.Tables[0].Rows[i]["SZ_PAYMENT"].ToString();
                        objDR["SZ_DESCRIPTION"] = objDS.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        objDR["SZ_PATH"] = objDS.Tables[0].Rows[i]["SZ_PATH"].ToString();
                        objDR["SZ_DENAIL"] = objDS.Tables[0].Rows[i]["SZ_DENAIL"].ToString();
                        objDR["CPT"] = objDS.Tables[0].Rows[i]["CPT"].ToString();
                        objDR["FLT_WRITE_OFF"] = objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString();
                        objDR["SZ_INSURANCE_COMPANY"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_COMPANY"].ToString();
                        objDR["PaidInPeriod"] = objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString();
                        //added on 22/12/2014
                        objDR["specialty"] = objDS.Tables[0].Rows[i]["specialty"].ToString();
                        objDR["dt_transferred_on"] = objDS.Tables[0].Rows[i]["dt_transferred_on"].ToString();
                        objDR["mn_transferred_amount"] = objDS.Tables[0].Rows[i]["mn_transferred_amount"].ToString();
                        objDR["SZ_COMPANY_NAME"] = objDS.Tables[0].Rows[i]["SZ_COMPANY_NAME"].ToString();
                        objDR["PROVIDER_NAME"] = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();
                        
                        objDT.Rows.Add(objDR);
                        sz_provider_name = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();

                        ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalwriteoffAmount = ForGroupSumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));
                        ForGroupSumTotalChkAmt = ForGroupSumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));

                        SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalwriteoffAmount = SumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));
                        SumTotalChkAmt = SumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));

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
                            objDR["SZ_CASE_NO"] = "";
                            objDR["SZ_PAYMENT"] = "";
                            objDR["DOCTOR_NAME"] = "";
                            objDR["SZ_DESCRIPTION"] = "";
                            objDR["SZ_PATH"] = "";
                            objDR["SZ_DENAIL"] = "";
                            objDR["CPT"] = "";
                            objDR["FLT_WRITE_OFF"] = "<b>$" + ForGroupSumTotalwriteoffAmount.ToString() + "</b>";
                            objDR["PaidInPeriod"] = "<b>$" + ForGroupSumTotalChkAmt.ToString() + "</b>";
                            objDR["SZ_INSURANCE_COMPANY"] = "";
                            //added on 22/12/2014
                            objDR["specialty"] = "";
                            objDR["dt_transferred_on"] = "";
                            objDR["mn_transferred_amount"] = "";
                            objDR["SZ_COMPANY_NAME"] = "";
                            objDR["PROVIDER_NAME"] = "";
                            
                            //added on 22/12/2014


                            objDT.Rows.Add(objDR);
                            ForGroupSumTotalBillAmount = 0.00M;
                            ForGroupSumTotalPaidAmount = 0.00M;
                            ForGroupSumTotalOutstandingAmount = 0.00M;
                            ForGroupSumTotalwriteoffAmount = 0.00M;
                            ForGroupSumTotalChkAmt = 0.00M;
                        }

                        ForGroupSumTotalBillAmount = ForGroupSumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalPaidAmount = ForGroupSumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalOutstandingAmount = ForGroupSumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        ForGroupSumTotalwriteoffAmount = ForGroupSumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));
                        ForGroupSumTotalChkAmt = ForGroupSumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));


                        SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
                        SumTotalwriteoffAmount = SumTotalwriteoffAmount + Convert.ToDecimal(objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString().Remove(0, 1));
                        SumTotalChkAmt = SumTotalChkAmt + Convert.ToDecimal(objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString().Remove(0, 1));


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
                        objDR["SZ_CASE_NO"] = "";
                        objDR["SZ_PAYMENT"] = "";
                        objDR["SZ_DESCRIPTION"] = "";
                        objDR["SZ_PATH"] = "";
                        objDR["SZ_DENAIL"] = "";
                        objDR["CPT"] = "";
                        objDR["FLT_WRITE_OFF"] = "";
                        objDR["SZ_INSURANCE_COMPANY"] = "";
                        objDR["PaidInPeriod"] = "";
                        //added on 22/12/2014
                        objDR["specialty"] = "";
                        objDR["dt_transferred_on"] = "";
                        objDR["mn_transferred_amount"] = "";
                        objDR["SZ_COMPANY_NAME"] = "";
                        objDR["PROVIDER_NAME"] = "";

                        //added on 22/12/2014
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
                        objDR["SZ_CASE_NO"] = objDS.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                        objDR["SZ_PAYMENT"] = objDS.Tables[0].Rows[i]["SZ_PAYMENT"].ToString();
                        objDR["SZ_DESCRIPTION"] = objDS.Tables[0].Rows[i]["SZ_DESCRIPTION"].ToString();
                        objDR["SZ_PATH"] = objDS.Tables[0].Rows[i]["SZ_PATH"].ToString();
                        objDR["SZ_DENAIL"] = objDS.Tables[0].Rows[i]["SZ_DENAIL"].ToString();
                        objDR["CPT"] = objDS.Tables[0].Rows[i]["CPT"].ToString();
                        objDR["FLT_WRITE_OFF"] = objDS.Tables[0].Rows[i]["FLT_WRITE_OFF"].ToString();
                        objDR["SZ_INSURANCE_COMPANY"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_COMPANY"].ToString();
                        objDR["PaidInPeriod"] = objDS.Tables[0].Rows[i]["PaidInPeriod"].ToString();
                        //added on 22/12/2014
                        objDR["specialty"] = objDS.Tables[0].Rows[i]["specialty"].ToString();
                        objDR["dt_transferred_on"] = objDS.Tables[0].Rows[i]["dt_transferred_on"].ToString();
                        objDR["mn_transferred_amount"] = objDS.Tables[0].Rows[i]["mn_transferred_amount"].ToString();
                        objDR["SZ_COMPANY_NAME"] = objDS.Tables[0].Rows[i]["SZ_COMPANY_NAME"].ToString();
                        objDR["PROVIDER_NAME"] = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();

                        objDT.Rows.Add(objDR);

                        sz_provider_name = objDS.Tables[0].Rows[i]["PROVIDER_NAME"].ToString();

                    }
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
                objDR["SZ_CASE_NO"] = "";
                objDR["SZ_PAYMENT"] = "";
                objDR["DOCTOR_NAME"] = "";
                objDR["SZ_DESCRIPTION"] = "";
                objDR["SZ_PATH"] = "";
                objDR["SZ_DENAIL"] = "";
                objDR["CPT"] = "";
                objDR["FLT_WRITE_OFF"] = "<b>$" + ForGroupSumTotalwriteoffAmount.ToString() + "</b>";
                objDR["PaidInPeriod"] = "<b>$" + ForGroupSumTotalChkAmt.ToString() + "</b>";
                objDR["SZ_INSURANCE_COMPANY"] = "";
                //added on 22/12/2014
                objDR["specialty"] = "";
                objDR["dt_transferred_on"] = "";
                objDR["mn_transferred_amount"] = "";
                objDR["SZ_COMPANY_NAME"] = "";
                objDR["PROVIDER_NAME"] = "";
                
                //added on 22/12/2014
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
                objDR["SZ_CASE_NO"] = "";
                objDR["SZ_PAYMENT"] = "";
                objDR["DOCTOR_NAME"] = "";
                objDR["SZ_DESCRIPTION"] = "";
                objDR["SZ_PATH"] = "";
                objDR["SZ_DENAIL"] = "";
                objDR["CPT"] = "";
                objDR["FLT_WRITE_OFF"] = "<b>$" + SumTotalwriteoffAmount.ToString() + "</b>";
                objDR["PaidInPeriod"] = "<b>$" + SumTotalChkAmt.ToString() + "</b>";
                objDR["SZ_INSURANCE_COMPANY"] = "";
                //added on 22/12/2014
                objDR["specialty"] = "";
                objDR["dt_transferred_on"] = "";
                objDR["mn_transferred_amount"] = "";
                objDR["SZ_COMPANY_NAME"] = "";
                objDR["PROVIDER_NAME"] = "";
                
                //added on 22/12/2014
                objDT.Rows.InsertAt(objDR, 0);
            }


            grdPayment2.DataSource = objDT;
            grdPayment2.DataBind();

            if (Request.QueryString["fromCase"] == null)
            {
                grdPayment.Columns[1].Visible = true; //move patient name from 3 to 1
            }
            else
            {
                grdPayment.Columns[1].Visible = false;
            }

            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            {
                grdPayment.Columns[4].Visible = false;
            }
            else
            {
                grdPayment.Columns[4].Visible = false;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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
            if (chkReportOnly.Checked)
            {
                BindGrid2();
                grdPayment.Visible = false;
                grdPayment2.Visible = true;
            }
            else
            {
                BindGrid();
                grdPayment.Visible = true;
                grdPayment2.Visible = false;
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
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
           

            if (chkReportOnly.Checked)
            {
                ExportToExcel2();
            }
            else
            {
                ExportToExcel();
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
            for (int icount = 0; icount < grdPayment.Items.Count  ; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdPayment.Columns.Count -1   ; i++)
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
                for (int j = 0; j < grdPayment.Columns.Count-1  ; j++)
                {
                    if (grdPayment.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        LinkButton lnk = (LinkButton)grdPayment.Items[icount].Cells[j].FindControl("lnkBill_No");
                        if (j==2) { strHtml.Append(lnk.Text); } else { strHtml.Append(grdPayment.Items[icount].Cells[j].Text); }
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

    private void ExportToExcel2()
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
            for (int icount = 0; icount < grdPayment2.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdPayment2.Columns.Count - 1; i++)
                    {
                        if (grdPayment2.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdPayment2.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdPayment2.Columns.Count - 1; j++)
                {
                    if (grdPayment2.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        LinkButton lnk = (LinkButton)grdPayment2.Items[icount].Cells[j].FindControl("lnkBill_No");
                        if (j == 2) { strHtml.Append(lnk.Text); } else { strHtml.Append(grdPayment2.Items[icount].Cells[j].Text); }
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
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(grdPayment.Items[i].Cells[5].Text); 
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(grdPayment.Items[i].Cells[6].Text);
                SumTotalOutstandingAmount = SumTotalOutstandingAmount  + Convert.ToDecimal(grdPayment.Items[i].Cells[7].Text);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void grdPayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "BillNo")
        {  
            string[] args = e.CommandArgument.ToString().Split(',');
            String sz_BillNo = args[0];                          
            _reportBO = new Bill_Sys_ReportBO();
            DataSet dsBillView = _reportBO.GetPaymentDetails(sz_BillNo, txtCompanyID.Text);
            int index = 0;
            index = Convert.ToInt32(args[1]);
            lblBillDate.Text = grdPayment.Items[index].Cells[0].Text.ToString();
            lblBillNo.Text = sz_BillNo.ToString();
            lblCaseNo.Text = grdPayment.Items[index].Cells[14].Text.ToString();//change 13 to 14
            String[] strarr = grdPayment.Items[index].Cells[1].Text.Split('[');
            if (strarr.Length != 0)
            {
                lblPatientName.Text = strarr[0].ToString();
            }
            else
            {
                lblPatientName.Text = grdPayment.Items[index].Cells[1].Text.ToString();
            }
            if (dsBillView.Tables[0].Rows.Count >= 1)
            {
                grdListOfPayment.DataSource = dsBillView;
                grdListOfPayment.DataBind();
            }
                ModalPopupExtender.Show();
            
        }
    }


    protected void grdPayment2_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "BillNo")
        {
            string[] args = e.CommandArgument.ToString().Split(',');
            String sz_BillNo = args[0];
            _reportBO = new Bill_Sys_ReportBO();
            DataSet dsBillView = _reportBO.GetPaymentDetails(sz_BillNo, txtCompanyID.Text);
            int index = 0;
            index = Convert.ToInt32(args[1]);
            lblBillDate.Text = grdPayment2.Items[index].Cells[0].Text.ToString();
            lblBillNo.Text = sz_BillNo.ToString();
            lblCaseNo.Text = grdPayment2.Items[index].Cells[14].Text.ToString();//change 13 to 14
            String[] strarr = grdPayment2.Items[index].Cells[1].Text.Split('[');
            if (strarr.Length != 0)
            {
                lblPatientName.Text = strarr[0].ToString();
            }
            else
            {
                lblPatientName.Text = grdPayment2.Items[index].Cells[1].Text.ToString();
            }
            if (dsBillView.Tables[0].Rows.Count >= 1)
            {
                grdListOfPayment.DataSource = dsBillView;
                grdListOfPayment.DataBind();
            }
            ModalPopupExtender.Show();

        }
    }


    protected void btnClose_Click(object sender, EventArgs e)
    {
        //GridBind();
        ModalPopupExtender.Hide();
        grdListOfPayment.DataSource = null;
        grdListOfPayment.DataBind();
        lblBillDate.Text = "";
        lblBillNo.Text = "";
        lblCaseNo.Text = "";
        lblPatientName.Text = "";

    }

    public DataSet GetPaymentDetailsMRI(ArrayList arrPayment)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_PAYMENT_REPORT_MRI_NEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[1].ToString());
            sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrPayment[4].ToString());
            if (arrPayment[5].ToString() != "" && arrPayment[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arrPayment[5].ToString()); }
            sqlCmd.Parameters.AddWithValue("@CHECKFROMDATE", arrPayment[6].ToString());
            sqlCmd.Parameters.AddWithValue("@CHECKTODATE", arrPayment[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKNUMBER", arrPayment[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKAMOUNT", arrPayment[9].ToString());
            if (arrPayment[10].ToString() != "" && arrPayment[10].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", arrPayment[10].ToString());
            if (arrPayment[11].ToString() != "" && arrPayment[11].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", arrPayment[11].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYFROMDATE", arrPayment[12].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYTODATE", arrPayment[13].ToString());
            if (arrPayment[14].ToString() != "" && arrPayment[14].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arrPayment[14].ToString());
            if (arrPayment[15].ToString() != "" && arrPayment[15].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_COLLECTIONS", arrPayment[15].ToString());
            if (arrPayment.Count > 16)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_FROMDATE", arrPayment[16].ToString());
            }
            if (arrPayment.Count > 17)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TODATE", arrPayment[17].ToString());
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetPaymentDetails(ArrayList arrPayment)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection  sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_PAYMENT_REPORT_NEW", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", arrPayment[0].ToString());
            sqlCmd.Parameters.AddWithValue("@FROMDATE", arrPayment[1].ToString());
            sqlCmd.Parameters.AddWithValue("@TODATE", arrPayment[2].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", arrPayment[3].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", arrPayment[4].ToString());
            if (arrPayment[5].ToString() != "" && arrPayment[5].ToString() != "NA") { sqlCmd.Parameters.AddWithValue("@SZ_LOCATION_ID", arrPayment[5].ToString()); }
            sqlCmd.Parameters.AddWithValue("@CHECKFROMDATE", arrPayment[6].ToString());
            sqlCmd.Parameters.AddWithValue("@CHECKTODATE", arrPayment[7].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKNUMBER", arrPayment[8].ToString());
            sqlCmd.Parameters.AddWithValue("@SZ_CHECKAMOUNT", arrPayment[9].ToString());
            if (arrPayment[10].ToString() != "" && arrPayment[10].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", arrPayment[10].ToString());
            if (arrPayment[11].ToString() != "" && arrPayment[11].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_INSURANCE_COMPANY_NAME", arrPayment[11].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYFROMDATE", arrPayment[12].ToString());
            sqlCmd.Parameters.AddWithValue("@PAYTODATE", arrPayment[13].ToString());
            if (arrPayment[14].ToString() != "" && arrPayment[14].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE_ID", arrPayment[14].ToString());
            if (arrPayment[15].ToString() != "" && arrPayment[15].ToString() != "NA") sqlCmd.Parameters.AddWithValue("@SZ_COLLECTIONS", arrPayment[15].ToString());
            if (arrPayment.Count > 16)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_FROMDATE", arrPayment[16].ToString());
            }
            if (arrPayment.Count > 17)
            {
                sqlCmd.Parameters.AddWithValue("@SZ_VISIT_TODATE", arrPayment[17].ToString());
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            da.Fill(ds);

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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
