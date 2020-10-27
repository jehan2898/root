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
using XGridPagination;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class Bill_Sys_DetailBillReportProviderForTest : PageBase
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
            this.con.SourceGrid = grdPayment;
            this.txtSearchBox.SourceGrid = grdPayment;
            this.grdPayment.Page = this.Page;
            this.grdPayment.PageNumberList = this.con;
            this.Title = "Bill Report By Provider For Test Facility";
            
            if (!IsPostBack)
            {
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
           
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
           


            txtCompanyId.Text = (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            txtOfficeId.Text = Session["SZ_OFFICE_ID"].ToString();
            txtFromDate.Text = Session["sz_From_Date"].ToString();
            txtToDate.Text = Session["sz_To_Date"].ToString();
            txtOffice.Text = Session["sz_Office_Id_Text"].ToString();
            txtLocationId.Text = Session["SZ_LOCATION_id"].ToString();

            
                //grdPayment.XGridDatasetNumber = 2;
                grdPayment.XGridBindSearch();
                //dt = (DataTable)grdPayment.XGridDataset;

                //if (dt.Rows.Count > 0)
                //{
                //    DataTable dt1 = new DataTable();
                //    dt1 = GetSumOfAmount(dt);                    
                //    DataView dv = new DataView(dt1);
                //    grdPayment.DataSource = dv;
                //    grdPayment.DataBind();
                //}                                                 
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

    protected DataTable GetSumOfAmount(DataTable dt)
    {
        
            DataTable objDSLocationWise = new DataTable();
            ArrayList arr = new ArrayList();
            DataSet ds = new DataSet();
            arr.Add(txtCompanyId.Text);
            arr.Add(txtOffice.Text);
            arr.Add(txtLocationId.Text);
            arr.Add(txtFromDate.Text);
            arr.Add(txtToDate.Text);

            //ds = _reportBO.GetDetailBillReportByProviderForTest(arr);
            if (ds.Tables.Count > 0)
            {
                objDSLocationWise = (DataTable)ds.Tables[0];
                Session["DataVew"] = (DataTable)objDSLocationWise;
            }

            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;

            for (int i = 0; i < objDSLocationWise.Rows.Count; i++)
            {
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDSLocationWise.Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDSLocationWise.Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDSLocationWise.Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
            }
            Session["TotalBillAmount"]=SumTotalBillAmount;
            Session["TotalPaidAmount"]=SumTotalPaidAmount;
            Session["TotalOutStandingAmount"] = SumTotalOutstandingAmount;
        
        string str = Session["TotalBillAmount"].ToString();
                    DataRow objDR = dt.NewRow();
                    objDR["SZ_BILL_NUMBER"] = "";
                    objDR["SZ_CASE_NO"] = "";
                    objDR["SZ_PATIENT_NAME"] = "";
                    objDR["DT_BILL_DATE"] = "";
                    objDR["DT_FIRST_LAST_VISIT_DATE"] = "<b>Total</b>";
                    objDR["SZ_CASE_TYPE"] = "";
                    objDR["TOTAL_BILL_AMOUNT"] = "<b>$" + Session["TotalBillAmount"].ToString() + "</b>";
                    objDR["TOTAL_PAID_AMOUNT"] = "<b>$" + Session["TotalPaidAmount"].ToString() + "</b>";
                    objDR["TOTAL_OUTSTANDING_AMOUNT"] = "<b>$" + Session["TotalOutStandingAmount"].ToString() + "</b>";
                    objDR["SZ_COMMENT"] = "";
                    dt.Rows.InsertAt(objDR, 0);

                    return dt;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }   
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPayment.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);

        DataTable dt = new DataTable();
        dt=(DataTable)Session["DataVew"];
        grdExportToExcel.DataSource = dt;
         grdExportToExcel.DataBind();
        StringBuilder strHtml = new StringBuilder();
        strHtml.Append("<table border='1px'>");
        for (int icount = 0; icount < grdExportToExcel.Items.Count; icount++)
        {

            if (icount == 0)
            {
                strHtml.Append("<tr>");

                for (int i = 0; i < grdExportToExcel.Columns.Count; i++)
                {
                    if (grdExportToExcel.Columns[i].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdExportToExcel.Columns[i].HeaderText);
                        strHtml.Append("</td>");
                    }
                }

                strHtml.Append("</tr>");
            }

            strHtml.Append("<tr>");
            for (int j = 0; j < grdExportToExcel.Columns.Count; j++)
            {
                if (grdExportToExcel.Columns[j].Visible == true)
                {
                    strHtml.Append("<td>");

                    strHtml.Append(grdExportToExcel.Items[icount].Cells[j].Text);

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

    protected void grdPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(con.SelectedValue);
        grdPayment.SelectablePageIndexChanged(Convert.ToInt16(con.SelectedValue) - 1);
        BindGrid();
        con.SelectedValue = i.ToString();
        Session["PageIndex"] = con.SelectedValue.ToString();
    }
}
