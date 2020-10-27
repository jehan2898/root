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

public partial class Bill_Sys_BillReportByProviderFortest : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        this.con.SourceGrid = grdPayment;
        this.txtSearchBox.SourceGrid = grdPayment;
        this.grdPayment.Page = this.Page;
        this.grdPayment.PageNumberList = this.con;
        this.Title = "Bill Provider";

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
            cv.MakeReadOnlyPage("Bill_Sys_BillReportByProvider.aspx");
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
        try
        {
            _reportBO = new Bill_Sys_ReportBO();
            DataTable objDSLocationWise = new DataTable();
            DataSet ds = new DataSet();
            ArrayList arr = new ArrayList();
            arr.Add(txtCompanyID.Text);
            arr.Add(txtProvider.Text);
            arr.Add(extddlLocation.Text);
            arr.Add(txtFromDate.Text);
            arr.Add(txtToDate.Text);
            //ds=_reportBO.GetBillReportByProviderForTest(arr);
            if (ds.Tables.Count > 0)
            {
                objDSLocationWise = (DataTable)ds.Tables[0];
            }
            //grdPayment.XGridDatasetNumber = 2;
            //grdPayment.XGridBindSearch();            
            //objDSLocationWise = (DataTable)grdPayment.XGridDataset;
             

            decimal SumTotalBillAmount = 0;
            decimal SumTotalPaidAmount = 0;
            decimal SumTotalOutstandingAmount = 0;
            for (int i = 0; i < objDSLocationWise.Rows.Count; i++)
            {
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(objDSLocationWise.Rows[i]["TOTAL_BILL_AMOUNT"].ToString().Remove(0, 1));
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(objDSLocationWise.Rows[i]["TOTAL_PAID_AMOUNT"].ToString().Remove(0, 1));
                SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(objDSLocationWise.Rows[i]["TOTAL_OUTSTANDING_AMOUNT"].ToString().Remove(0, 1));
            }

            if (objDSLocationWise.Rows.Count > 0)
            {
                DataRow objDR = objDSLocationWise.NewRow();
                objDR["PROVIDER_NAME"] = "<b>Total</b>";
                objDR["TOTAL_BILL_AMOUNT"] = "<b>$" + SumTotalBillAmount.ToString() +  "</b>";
                objDR["TOTAL_PAID_AMOUNT"] = "<b>$" + SumTotalPaidAmount.ToString() + "</b>";
                objDR["TOTAL_OUTSTANDING_AMOUNT"] = "<b>$" + SumTotalOutstandingAmount.ToString() + "</b>";                
                objDSLocationWise.Rows.InsertAt(objDR, 0);

      
                DataView dv = new DataView(objDSLocationWise);
                Session["DataVew"] = (DataTable)objDSLocationWise;
                grdPayment.DataSource = dv;
                grdPayment.DataBind();
            }

               
            
            // AddAmount();
            if (objDSLocationWise.Rows.Count > 0)
            {
                LinkButton show = (LinkButton)grdPayment.Rows[0].FindControl("lnkshow");
                show.Visible = false;
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
            for (int i = 0; i < grdPayment.Rows.Count; i++)
            {
                SumTotalBillAmount = SumTotalBillAmount + Convert.ToDecimal(grdPayment.Rows[i].Cells[1].Text.Remove(0,1));
                SumTotalPaidAmount = SumTotalPaidAmount + Convert.ToDecimal(grdPayment.Rows[i].Cells[2].Text.Remove(0,1));
                SumTotalOutstandingAmount = SumTotalOutstandingAmount + Convert.ToDecimal(grdPayment.Rows[i].Cells[3].Text.Remove(0,1));
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    protected void grdPayment_ItemCommand(object source, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "OpenDetailReport")
            {
                Session["SZ_OFFICE_ID"] = grdPayment.DataKeys[index][0].ToString();
                Session["sz_From_Date"] = txtFromDate.Text;
                Session["sz_To_Date"] = txtToDate.Text;
                Session["SZ_LOCATION_id"] = extddlLocation.Text;
                Session["sz_Office_Id_Text"] = txtProvider.Text;
                Response.Redirect("Bill_Sys_DetailBillReportProviderForTest.aspx", false);
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
    protected void lnkExportTOExcel_onclick(object source, EventArgs e)
    {        
       //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href='"  + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPayment.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString())+"';",true);

       DataTable ds = (DataTable)Session["DataVew"];
       grdExportToExcel.DataSource = ds;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ClearFields()", true);
    }
}
