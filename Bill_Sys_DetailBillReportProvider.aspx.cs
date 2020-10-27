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

public partial class Bill_Sys_DetailBillReportProvider : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                if(Request.QueryString.Count==5)
                {
                    Session["SZ_OFFICE_ID"] = Request.QueryString["OffID"].ToString();
                    Session["sz_From_Date"] = Request.QueryString["FromDate"].ToString();
                    Session["sz_To_Date"] = Request.QueryString["ToDate"].ToString();
                    Session["SZ_LOCATION_id"] = Request.QueryString["Location"].ToString();
                    Session["sz_Office_Id_Text"] = Request.QueryString["Provider"].ToString();
                }

               BindGrid();
            }
        }
        catch (Exception ex)
        {
            
            lblMsg.Visible = true;
            
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
            cv.MakeReadOnlyPage("Bill_Sys_DetailBillReportProvider.aspx");
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
            arrlst.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (Session["SZ_OFFICE_ID"].ToString() != "&nbsp;")
            {
                arrlst.Add(Session["SZ_OFFICE_ID"].ToString());
            }
            else
            {
                arrlst.Add("");
            }
            //Added Parameter Date , Location For Searching:- TUSHAR
            arrlst.Add(Session["sz_From_Date"].ToString());
            arrlst.Add(Session["sz_To_Date"].ToString());
            arrlst.Add(Session["SZ_LOCATION_id"].ToString());
            arrlst.Add(Session["sz_Office_Id_Text"].ToString());
            //end Of Code
            DataSet objDS = new DataSet();
            objDS = _reportBO.GetDetailBillReportBySpeciality(arrlst);

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
                objDR["SZ_BILL_NUMBER"] = "";
                objDR["SZ_CASE_NO"] = "";
                objDR["SZ_PATIENT_NAME"] = "";
                objDR["DT_BILL_DATE"] = "";
                objDR["DT_FIRST_LAST_VISIT_DATE"] = "<b>Total</b>";
                objDR["SZ_CASE_TYPE"] = "";
                objDR["TOTAL_BILL_AMOUNT"] = "<b>$" + SumTotalBillAmount.ToString() + "</b>";
                objDR["TOTAL_PAID_AMOUNT"] = "<b>$" + SumTotalPaidAmount.ToString() + "</b>";
                objDR["TOTAL_OUTSTANDING_AMOUNT"] = "<b>$" + SumTotalOutstandingAmount.ToString() + "</b>";
                objDR["SZ_COMMENT"] = "";
                objDS.Tables[0].Rows.InsertAt(objDR, 0);
            }

            grdPayment.DataSource = objDS;
            grdPayment.DataBind();
        }
        catch (Exception ex)
        {
            
            lblMsg.Visible = true;
           
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
           
            lblMsg.Visible = true;
           
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
           
            lblMsg.Visible = true;
           
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
            
            lblMsg.Visible = true;
            
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
}
