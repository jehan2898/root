/***********************************************************/
/*Project Name         :       Medical Billing System
/*Description          :       Revert Report
/*Author               :       Atul j
/*Date of creation     :        April 2010 
/*Modified By (Last)   :       Atul Jadhav 
/*Modified By (S-Last) :
/*Modified Date        :      30 April 2010 
/************************************************************/


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

public partial class Bill_Sys_Received_Report : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnRevert.Attributes.Add("onclick", "return ChekOne();");
        lblMsg.Text = "";
        if (!IsPostBack)
        {//bind office name to ddlOffice
            DataSet dsOfficeName = new DataSet();
            string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString(); 
            Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
            dsOfficeName=objReport.GetOfficeName(szCmpId);
            ddlOffice.DataSource = dsOfficeName.Tables[0];
            ddlOffice.DataTextField = "SZ_OFFICE";
            ddlOffice.DataValueField = "SZ_OFFICE_ID";
            ddlOffice.DataBind();   
            ddlOffice.Items.Insert(0, "--select--");
            //bind gride
            DataSet dsOfficeName1 = new DataSet();
            dsOfficeName1 = objReport.GetReceived_Report(szCmpId, "", "", "", "--select--");


            #region" default viwe of grid sor by ascending order of date"
            DataView dv1;
            dv1 = dsOfficeName1.Tables[0].DefaultView;
            dv1.Sort = "DATE_OF_REFERRAL_PROC,PATIENT_NAME ASC";
            grdReceivedeport.DataSource = dv1;
            grdExel.DataSource = dv1;
            Session["OfficeData"] = dsOfficeName1;
            grdReceivedeport.DataBind();
            grdExel.DataBind();
            
            #endregion
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                grdReceivedeport.Columns[2].Visible = false;
                grdExel.Columns[2].Visible = false;
            }
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtSort.Text = "";     
        int i=new int();
        //bind grid according  Search  condition
        if (!txtFromDate.Text.Trim().Equals("") && !txtToDate.Text.Trim().Equals("")) i = DateTime.Compare(Convert.ToDateTime(txtToDate.Text), Convert.ToDateTime(txtFromDate.Text));
        if (i.Equals(1) || i.Equals(0))
        {
           

            string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
            DataSet dsOfficeName1 = new DataSet();
            dsOfficeName1 = objReport.GetReceived_Report(szCmpId, txtFromDate.Text, txtToDate.Text, txtPatientName.Text, ddlOffice.SelectedValue);
            #region" default viwe of grid sor by ascending order of date"
            DataView dv2;
            dv2 = dsOfficeName1.Tables[0].DefaultView;
            dv2.Sort = "DATE_OF_REFERRAL_PROC ,PATIENT_NAME ASC";

            grdReceivedeport.DataSource = dv2;
            grdExel.DataSource = dv2;
            Session["OfficeData"] = dsOfficeName1;
            grdReceivedeport.DataBind();
            grdExel.DataBind();

            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                grdReceivedeport.Columns[2].Visible = false;
                grdExel.Columns[2].Visible = false;
            }
            #endregion


        }
        else
        {
            lblMsg.Text = " To date must be greater than From date";
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
            for (int icount = 0; icount < grdExel.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdExel.Columns.Count; i++)
                    {
                        if (grdExel.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdExel.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdExel.Columns.Count; j++)
                {
                    if (grdExel.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdExel.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue( "EXCEL_SHEET") + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + filename, false);


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
    protected void grdReceivedeport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #region "Sort grid and rever report"
    protected void grdReceivedeport_ItemCommand(object source, DataGridCommandEventArgs e)
    {

        if (e.CommandName.ToString() == "btnlnkView")
        {
            //String szDefaultPath = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
            string strPath = e.Item.Cells[8].Text.ToString();
            if (strPath.Equals("&nbsp;"))
            {
                lblMsg.Text = "LinkPath is not stored";

            }else{
                
               
               //string szUrl = szDefaultPath + strPath;
          
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + strPath + "');", true);
            }
        }

       
        DataView dv;
        DataSet ds = new DataSet();
        ds = (DataSet)Session["OfficeData"];
        dv = ds.Tables[0].DefaultView;

        if (e.CommandName.ToString() == "OfficeName")
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
        else if (e.CommandName.ToString() == "PatientName")
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
        else if (e.CommandName.ToString() == "DateOfReferral")
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
        else if (e.CommandName.ToString() == "CompletedDate")
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
        else if (e.CommandName.ToString() == "CaseNo")
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
        else if (e.CommandName.ToString() == "ChartNo")
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
        grdReceivedeport.DataSource = dv;
        grdExel.DataSource = dv;
        grdReceivedeport.DataBind();
        grdExel.DataBind();
        if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
        {
            grdReceivedeport.Columns[2].Visible = false;
            grdExel.Columns[2].Visible = false;
        }
    }
    protected void btnRevert_Click(object sender, EventArgs e)
    {

        //revert the recored 
        for (int j = 0; j < grdReceivedeport.Items.Count; j++)
        {
            CheckBox chkDelete1 = (CheckBox)grdReceivedeport.Items[j].FindControl("chkUpdateStatus");
            if (chkDelete1.Checked)
            {
      
                Bill_Sys_ReportBO objUpdateReport = new Bill_Sys_ReportBO();
                objUpdateReport.RevertReport(Convert.ToInt32(grdReceivedeport.Items[j].Cells[10].Text.Trim().ToString()));
            }
           
        }
        

       

        //binding grid agin whith new value
            string szCmpId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
            DataSet dsOfficeName1 = new DataSet();
            dsOfficeName1 = objReport.GetReceived_Report(szCmpId, txtFromDate.Text, txtToDate.Text, txtPatientName.Text, ddlOffice.SelectedValue);

            DataView dv1;
            dv1 = dsOfficeName1.Tables[0].DefaultView;
            dv1.Sort = "DATE_OF_REFERRAL_PROC,PATIENT_NAME ASC";

            grdReceivedeport.DataSource = dv1;
            grdExel.DataSource = dv1;
            Session["OfficeData"] = dsOfficeName1;
            grdReceivedeport.DataBind();
            grdExel.DataBind();
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1"))
            {
                grdReceivedeport.Columns[2].Visible = false;
                grdExel.Columns[2].Visible = false;
            }
           



        }
    #endregion
    }
