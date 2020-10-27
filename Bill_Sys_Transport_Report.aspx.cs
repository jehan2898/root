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

public partial class Bill_Sys_Transport_Report : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (!IsPostBack)
        { ///bind grid 
            string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
            DataSet dsReport = new DataSet();
            string strStartTime = "00.00";
            string strEndTime = "00.00";

            dsReport = objReport.GetTransportSearchReport(szCmpId, "", "", "00.00", "", "00.00", "", "--Selecte--");
            grdAllReports.DataSource = dsReport.Tables[0];
            grdReports.DataSource = dsReport.Tables[0];
            grdAllReports.DataBind();
            grdReports.DataBind();
            Session["ViewData"] = dsReport;
            ///bind time DropDownList
            BindTransportName();
            BindTimeControl();
            
        }




    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        decimal dcStartTime = 0;
        decimal dcEndTime = 0;


           //converting 12 hr time to 24 hr
            if (ddlFromTime.SelectedValue == "PM")
            {
                dcStartTime = Convert.ToDecimal((ddlFromHours.SelectedValue + ".00")) + Convert.ToDecimal(("0." + ddlFromMinutes.SelectedValue));
                dcStartTime = dcStartTime + Convert.ToDecimal(12.00);
            }
            else
            {
                dcStartTime = Convert.ToDecimal((ddlFromHours.SelectedValue + ".00")) + Convert.ToDecimal(("0." + ddlFromMinutes.SelectedValue));
            }

            if (ddlTOTime.SelectedValue == "PM")
            {
                dcEndTime = Convert.ToDecimal((ddlToHours.SelectedValue + ".00")) + Convert.ToDecimal(("0." + ddlTOMinutes.SelectedValue));
                dcEndTime = dcEndTime + Convert.ToDecimal(12.00);
            }
            else
            {
                dcEndTime = Convert.ToDecimal((ddlToHours.SelectedValue + ".00")) + Convert.ToDecimal(("0." + ddlTOMinutes.SelectedValue));
            }
        
            if (dcStartTime <= dcEndTime)
            {
                int i=new int();
                //comparing from date with to date
                if (!txtFromDate.Text.Trim().Equals("") && !txtToDate.Text.Trim().Equals("")) i = DateTime.Compare(Convert.ToDateTime(txtToDate.Text), Convert.ToDateTime(txtFromDate.Text));
                if (i.Equals(1) || i.Equals(0))
                {
                    //bind grid according  Search  condition
                    DataSet ds = new DataSet();
                    Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
                    string strStartTime = ddlFromHours.SelectedItem.ToString() + "." + ddlFromMinutes.SelectedItem.ToString();
                    string strEndTime = ddlToHours.SelectedItem.ToString() + "." + ddlTOMinutes.SelectedItem.ToString();
                    ds = objReport.GetTransportSearchReport(szCmpId, txtFromDate.Text, txtToDate.Text, strStartTime, ddlFromTime.SelectedItem.ToString(), strEndTime, ddlTOTime.SelectedItem.ToString(),ddlTransportName.SelectedItem.ToString());
                    grdAllReports.DataSource = ds.Tables[0];
                    grdAllReports.DataBind();
                    grdReports.DataSource = ds.Tables[0];
                    grdReports.DataBind();
                    
                    Session["ViewData"] = ds;

                }
                else
                {
                    lblMsg.Text = " To date must be greater than From date";
                }
            }
            else
            {
                lblMsg.Text = "Time is not valid";
            }

    
        

    }


    private void BindTimeControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {

            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    ddlFromHours.Items.Add(i.ToString());
                  
                }
                else
                {
                    ddlFromHours.Items.Add("0" + i.ToString());
                  
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlFromMinutes.Items.Add(i.ToString());
                   
                }
                else
                {
                    ddlFromMinutes.Items.Add("0" + i.ToString());
                    
                }
            }
            ddlFromTime.Items.Add("AM");
            ddlFromTime.Items.Add("PM");



            for (int i = 0; i <= 12; i++)
            {
                if (i > 9)
                {
                    ddlToHours.Items.Add(i.ToString());
                    
                }
                else
                {
                    ddlToHours.Items.Add("0" + i.ToString());
                    
                }
            }
            for (int i = 0; i < 60; i++)
            {
                if (i > 9)
                {
                    ddlTOMinutes.Items.Add(i.ToString());
             
                }
                else
                {
                    ddlTOMinutes.Items.Add("0" + i.ToString());
                    
                }
            }
            ddlTOTime.Items.Add("AM");
            ddlTOTime.Items.Add("PM");


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
        ExportToExcel();
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
            for (int icount = 0; icount < grdReports.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdReports.Columns.Count; i++)
                    {
                        if (grdAllReports.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdReports.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdReports.Columns.Count; j++)
                {
                    if (grdReports.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdReports.Items[icount].Cells[j].Text);
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
    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        DataView dv;
        DataSet ds = new DataSet();
        ds = (DataSet)Session["ViewData"];
     dv = ds.Tables[0].DefaultView;

     if (e.CommandName.ToString() == "PatientName")
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
     else if (e.CommandName.ToString() == "PatientPhone")
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

     else if (e.CommandName.ToString() == "PatientCity")
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
     else if (e.CommandName.ToString() == "Office")
     {
         if (txtSort.Text == e.CommandArgument + " ASC")
         {
             txtSort.Text = e.CommandArgument + "  DESC";
         }
         else
         {
             txtSort.Text = e.CommandArgument + " ASC";
         }

     } else if (e.CommandName.ToString() == "Date")
     {
         if (txtSort.Text == e.CommandArgument + " ASC")
         {
             txtSort.Text = e.CommandArgument + "  DESC";
         }
         else
         {
             txtSort.Text = e.CommandArgument + " ASC";
         }

     }else if (e.CommandName.ToString() == "TranspotationCompany")
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
     dv.Sort = txtSort.Text;
     grdAllReports.DataSource = dv;
     grdAllReports.DataBind();
     grdReports.DataSource = dv;
     grdReports.DataBind();

    }
    private void BindTransportName()
    {
        string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
        DataSet dsTransportName = new DataSet();
        dsTransportName = objReport.GetTransportName(szCmpId);
       ddlTransportName.DataSource = dsTransportName.Tables[0];
       ddlTransportName.DataTextField = "SZ_TARNSPOTATION_COMPANY_NAME";
       ddlTransportName.DataValueField = "I_TARNSPOTATION_ID";
       ddlTransportName.DataBind();
       ddlTransportName.Items.Insert(0, "--Selecte--");

    }
}
