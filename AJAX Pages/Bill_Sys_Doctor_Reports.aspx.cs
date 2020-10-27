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
using DevExpress.Web;
using System.Data;


public partial class AJAX_Pages_Bill_Sys_Doctor_Reports : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

        if (!IsPostBack)
        {
            TabDoctor.Visible = false;

        }
        else
        {
            Bill_Sys_Doctor_Summary_Report _Bill_Sys_Doctor_Summary_Report = new Bill_Sys_Doctor_Summary_Report();
           
            DataSet dsDoctorsummary = new DataSet();
            dsDoctorsummary = _Bill_Sys_Doctor_Summary_Report.Get_Doctor_Summary_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text);
            //dsDoctorsummary.Tables[0].Rows.Add();
            //for (int n = 0; n < dsDoctorsummary.Tables[0].Columns.Count; n++)
            //{
            //    if (n == 0)
            //    {
            //        dsDoctorsummary.Tables[0].Rows[dsDoctorsummary.Tables[0].Rows.Count - 1][n] = "Total";
            //    }
            //    if (n >= 3)
            //    {
            //        string str1 = "";
            //        double sumHori = 0.0;
            //        for (int m = 0; m < dsDoctorsummary.Tables[0].Rows.Count; m++)
            //        {
            //            if (m != dsDoctorsummary.Tables[0].Rows.Count - 1)
            //            {
            //                str1 = dsDoctorsummary.Tables[0].Rows[m][n].ToString();
            //                str1 = str1.Replace("$", "");
            //                sumHori = sumHori + Convert.ToDouble(str1);
            //            }
            //        }
            //        if (n == 7)
            //        {
            //            int iCount = Convert.ToInt32(sumHori);
            //            dsDoctorsummary.Tables[0].Rows[dsDoctorsummary.Tables[0].Rows.Count - 1][n] = iCount;
            //        }
            //        else
            //        {
            //            dsDoctorsummary.Tables[0].Rows[dsDoctorsummary.Tables[0].Rows.Count - 1][n] = string.Format("{0:C}", sumHori);
            //        }

            //    }
            //}
            grdsummary.DataSource = dsDoctorsummary;
            grdsummary.DataBind();
            Session["DoctorSummmary"] = dsDoctorsummary;

            DataSet dsDetailssummary = new DataSet();
            dsDetailssummary = _Bill_Sys_Doctor_Summary_Report.Get_Details_Summary_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text);
            dsDetailssummary.Tables[0].Rows.Add();
            for (int n = 0; n < dsDetailssummary.Tables[0].Columns.Count; n++)
            {
                if (n == 0)
                {
                    dsDetailssummary.Tables[0].Rows[dsDetailssummary.Tables[0].Rows.Count - 1][n] = "Total";
                }
                if (n >= 7)
                {
                    string str = "";
                    double sumHori1 = 0.0;
                    for (int m = 0; m < dsDetailssummary.Tables[0].Rows.Count; m++)
                    {
                        if (m != dsDetailssummary.Tables[0].Rows.Count - 1)
                        {
                            str = dsDetailssummary.Tables[0].Rows[m][n].ToString();
                            str = str.Replace("$", "");
                            sumHori1 = sumHori1 + Convert.ToDouble(str);
                        }
                    }


                    dsDetailssummary.Tables[0].Rows[dsDetailssummary.Tables[0].Rows.Count - 1][n] = string.Format("{0:C}", sumHori1);


                }
            }
            grdDoctorDetailsReport.DataSource = dsDetailssummary;
            grdDoctorDetailsReport.DataBind();
            Session["DetailsSummmary"] = dsDetailssummary;
        }

    }

    protected void btnXlsExport1_Click(object sender, EventArgs e)
    {

        DataSet ds = (DataSet)Session["DoctorSummmary"];
        grdsummary.DataSource = ds;
        grdsummary.DataBind();
        grdExportDoctroSummary.FileName = "Doctor_summary(" + DateTime.Now.ToString("ddMMMMyyyy") + ")";
        grdExportDoctroSummary.WriteXlsxToResponse();
    }
    protected void btnReportSearch_Click(object sender, EventArgs e)
    {
        TabDoctor.Visible = true;
        TabDoctor.ActiveTabIndex = 0;
        Bill_Sys_Doctor_Summary_Report _Bill_Sys_Doctor_Summary_Report = new Bill_Sys_Doctor_Summary_Report();
        DataSet dsDoctorsummary = new DataSet();
        dsDoctorsummary = _Bill_Sys_Doctor_Summary_Report.Get_Doctor_Summary_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text);
        //dsDoctorsummary.Tables[0].Rows.Add();
        //for (int n = 0; n < dsDoctorsummary.Tables[0].Columns.Count; n++)
        //{
        //    if (n == 0)
        //    {
        //        dsDoctorsummary.Tables[0].Rows[dsDoctorsummary.Tables[0].Rows.Count - 1][n] = "Total";
        //    }
        //    if (n >= 3)
        //    {
        //        string str1 = "";
        //        double sumHori = 0.0;
        //        for (int m = 0; m < dsDoctorsummary.Tables[0].Rows.Count; m++)
        //        {
        //            if (m != dsDoctorsummary.Tables[0].Rows.Count - 1)
        //            {
        //                str1 = dsDoctorsummary.Tables[0].Rows[m][n].ToString();
        //                str1 = str1.Replace("$", "");
        //                sumHori = sumHori + Convert.ToDouble(str1);
        //            }
        //        }
        //        if (n == 7)
        //        {
        //            int iCount = Convert.ToInt32(sumHori);
        //            dsDoctorsummary.Tables[0].Rows[dsDoctorsummary.Tables[0].Rows.Count - 1][n] = iCount;
        //        }
        //        else
        //        {
        //            dsDoctorsummary.Tables[0].Rows[dsDoctorsummary.Tables[0].Rows.Count - 1][n] = string.Format("{0:C}", sumHori);
        //        }

        //    }
        //}
        grdsummary.DataSource = dsDoctorsummary;
        grdsummary.DataBind();
        Session["DoctorSummmary"] = dsDoctorsummary;

        DataSet dsDetailssummary = new DataSet();
        dsDetailssummary = _Bill_Sys_Doctor_Summary_Report.Get_Details_Summary_Report(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text);
        dsDetailssummary.Tables[0].Rows.Add();
        for (int n = 0; n < dsDetailssummary.Tables[0].Columns.Count; n++)
        {
            if (n == 0)
            {
                dsDetailssummary.Tables[0].Rows[dsDetailssummary.Tables[0].Rows.Count - 1][n] = "Total";
            }
            if (n >= 7)
            {
                string str = "";
                double sumHori1 = 0.0;
                for (int m = 0; m < dsDetailssummary.Tables[0].Rows.Count; m++)
                {
                    if (m != dsDetailssummary.Tables[0].Rows.Count - 1)
                    {
                        str = dsDetailssummary.Tables[0].Rows[m][n].ToString();
                        str = str.Replace("$", "");
                        sumHori1 = sumHori1 + Convert.ToDouble(str);
                    }
                }
                dsDetailssummary.Tables[0].Rows[dsDetailssummary.Tables[0].Rows.Count - 1][n] = string.Format("{0:C}", sumHori1);


            }
        }
        grdDoctorDetailsReport.DataSource = dsDetailssummary;
        grdDoctorDetailsReport.DataBind();
        Session["DetailsSummmary"] = dsDetailssummary;

    }

    protected void btnXlsExportDetails_Click(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)Session["DetailsSummmary"];
        grdDoctorDetailsReport.DataSource = ds;
        grdDoctorDetailsReport.DataBind();
        grdexportDetailsreport.FileName = "Details_Report(" + DateTime.Now.ToString("ddMMMMyyyy") + ")";
        grdexportDetailsreport.WriteXlsxToResponse();
    }
   


}
