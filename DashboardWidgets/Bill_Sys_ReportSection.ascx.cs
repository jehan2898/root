/***********************************************************/
/*Project Name         :       BILLING System
/*File Name            :       
/*Purpose              :       
/*Author               :       Adarsh Bajpai
/*Date of creation     :       29 Dec 2009
/*Modified By          :
/*Modified Date        :
/************************************************************/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Componend;

public partial class Bill_Sys_DashboardWidgets_ReportSection : System.Web.UI.UserControl
{
    private SaveOperation _saveOperation;
    private EditOperation _editOperation;
    private ListOperation _listOperation;
    Bill_Sys_BillTransaction_BO _billTransactionBO;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            DashBoardBO _obj = new DashBoardBO();
            _billTransactionBO = new Bill_Sys_BillTransaction_BO();

            DayOfWeek day = Convert.ToDateTime(System.DateTime.Today.ToString()).DayOfWeek;
            int days = day - DayOfWeek.Sunday;

            DateTime start = Convert.ToDateTime(System.DateTime.Today.ToString()).AddDays(-days);
            DateTime end = start.AddDays(6);

            lblReport.Text = "<ul style=\"list-style-type:disc;padding-left:60px;\"> <li><a href='" + "http://" + Request.Url.Authority + Request.ApplicationPath + "/Bill_Sys_PaidBills.aspx?Flag=report&Type=R' onclick=\"javascript:OpenPage('Litigation');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), CompanyID, "DOCUMENT_RECEIVED_COUNT") + "</a>" + " Received Report";
            lblReport.Text += "</li>  <li> <a href='" + "http://" + Request.Url.Authority + Request.ApplicationPath + "/Bill_Sys_PaidBills.aspx?Flag=report&Type=P' onclick=\"javascript:OpenPage('MissingInsuranceCompany');\" > " + _obj.getAppoinmentCount(System.DateTime.Today.ToString(), System.DateTime.Today.ToString(), CompanyID, "DOCUMENT_PENDING_COUNT") + "</a> Pending Report </li></ul>";

            
        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("~/Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }

    }
}
