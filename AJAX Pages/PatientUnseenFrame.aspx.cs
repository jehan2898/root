using System;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;
using System.Text;

public partial class AJAX_Pages_PatientUnseenFrame : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string CaseID = Request.QueryString["CaseID"].ToString();
            string Status = Request.QueryString["Status"].ToString();
            txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            BindData(CaseID, txtCompanyId.Text,Status);
        }

    }

    protected void BindData(string caseid,string compnayid,string Status)
    {
        try
        {
          Bill_Sys_Event_BO objEventbo = new Bill_Sys_Event_BO();
            DataSet ds = objEventbo.GetCaseWiserReport(caseid, compnayid, Status);
            string specialty = "";
            StringBuilder bl = new StringBuilder();
            DataTable dtspecialty = ds.Tables[0].DefaultView.ToTable(true, "sz_procedure_group");
       bl.Append("<table border='1'  style='border: 1px solid blue' >  ");
            bl.Append(" <tr>");
           
            // bl.Append("<table border='1'  style='border: 0.1px solid blue' 'border-collapse: collapse'>  ");
      
            for (int i = 0; i < dtspecialty.Rows.Count; i++)
            {
                bl.Append(" <td valign = 'top' >");
                bl.Append("<table > ");
                bl.Append(" <tr> ");
                DataRow[] dr = ds.Tables[0].Select("sz_procedure_group='" + dtspecialty.Rows[i][0].ToString() + "'");
                bl.Append("<td valign='top' bgcolor='#E5E7E9'>" + dtspecialty.Rows[i][0].ToString() + "</td> </tr>");

                for (int j = 0; j < dr.Length; j++)
                {

                    bl.Append(" <tr> ");


                    bl.Append("<td valign='top'>" + dr[j][2].ToString() + "</td></tr> ");
                }

                bl.Append("</table> ");
                bl.Append(" </td >");
            }
            bl.Append(" </tr> ");
            bl.Append("</table> ");
            dataDiv.InnerHtml = bl.ToString();

        }
        catch (Exception ex)
        {

           
        }
    }
}
