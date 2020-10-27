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
public partial class Bill_Sys_BillReportSpecialityForTest : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAL;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlServiseDate.Attributes.Add("onChange", "javascript:SetServiceDate();");
            if (!IsPostBack)
            {
                //Session["sz_StatusID_For_Test"] = "";
                //Session["sz_SpecialityID_For_Test"] = "";
                //Session["sz_From_Date"] = "";
                //Session["sz_To_Date"] = "";
                //Session["sz_User"] = "";
                
                ddlDateValues.SelectedValue = "1";
                txtFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlUser.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                //_reportBO = new Bill_Sys_ReportBO();
                //DataSet dset = new DataSet();
                //dset = _reportBO.GetPatientList(txtCompanyID.Text);
                //ddlUserList.DataSource = dset.Tables[0];
                //ddlUserList.DataTextField = "SZ_USER_NAME";
                //ddlUserList.DataValueField = "SZ_USER_ID";
                //ddlUserList.DataBind();
                
                if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
                {
                    extddlLocation.Visible = true;
                    lblLocationName.Visible = true;
                    extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
                }
                BindGrid();
                Session["sz_From_Date"] = txtFromDate.Text;
                Session["sz_To_Date"] = txtToDate.Text;
                Session["sz_User"] = "NA";
                Session["sz_From_Service_Date"] = "";
                Session["sz_To_Service_Date"] = "";
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_BillReportSpeciality.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _reportBO = new Bill_Sys_ReportBO();
        int count = 0;
        objAL = new ArrayList();
        DataSet ds = new DataSet();
        try
        {
            objAL.Add(txtCompanyID.Text);
            objAL.Add(extddlBillStatus.Text);
            objAL.Add(extddlSpeciality.Text);
            objAL.Add(txtFromDate.Text);
            objAL.Add(txtToDate.Text);
            //objAL.Add(ddlUserList.SelectedValue.ToString());
            objAL.Add(extddlUser.Text);
            //send location id to function getBillReportSpeciality()
            //objAL.Add(extddlLocation.Text);
            objAL.Add(txtFromServiceDate.Text);
            objAL.Add(txtToServiceDate.Text);
           
            if (chkVerificationsent.Checked)
            {
                objAL.Add("VS");
                count++;
            }

            if (chkVerificationreceived.Checked)
            {
                objAL.Add("VR");
                count++;
            }

            if (chkdenail.Checked)
            {
                objAL.Add("DEN");
                count++;
            }

            if (chkPaidfull.Checked)
            {
                objAL.Add("FBP");
                count++;
            }


         //   DataSet ds = new DataSet();


            if (count == 1)
            {
                ds = _reportBO.getBillReportSpecialityForTestSelect(objAL, "SP_GET_BILL_REPORT_SPECIALITY_FOR_TEST_SELECT");


            }
            else if (count == 4)
            {
                ds = _reportBO.getBillReportSpecialitySelect(objAL, "SP_GET_BILL_REPORT_SPECIALITY_FOR_TEST_ALL");//
            }
            else if (count == 3)
            {
                ds = _reportBO.getBillReportSpecialityForTestThree(objAL);
            }
            else if (count == 2)
            {
                ds = _reportBO.getBillReportSpecialityForTestTwo(objAL);
            }
            else
            {
                ds = _reportBO.getBillReportSpecialityForTest(objAL);

            }

           

            grdAllReports.DataSource = ds.Tables[0];
            grdAllReports.DataBind();
            Session["sz_From_Date"] = txtFromDate.Text;
            Session["sz_To_Date"] = txtToDate.Text;
            Session["sz_User"] = extddlUser.Text;
            Session["sz_From_Service_Date"] = txtFromServiceDate.Text;
            Session["sz_To_Service_Date"] = txtToServiceDate.Text;

            Decimal toatal = 0;
            foreach (DataGridItem dg in grdAllReports.Items)
            {
                if (dg.Cells[4].Text != "&nbsp;")
                toatal = toatal + Convert.ToDecimal(dg.Cells[4].Text);
            }
            lblTotal.Text = toatal.ToString();
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
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
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
            for (int icount = 0; icount < grdAllReports.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdAllReports.Columns.Count; i++)
                    {
                        if (grdAllReports.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdAllReports.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdAllReports.Columns.Count; j++)
                {
                    if (grdAllReports.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdAllReports.Items[icount].Cells[j].Text);
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
    protected void btnSearch_Click(object sender, EventArgs e)
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

    protected void grdAllReports_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (extddlLocation.Visible == true && extddlLocation.Text != "" && extddlLocation.Text != "NA")
            {
                Session["sz_LOCATION_Id"] = extddlLocation.Text;
            }
            else
            {
                Session["sz_LOCATION_Id"] = "";
            }
            if (e.CommandName.ToString() == "View")
            {
                Session["sz_StatusID_For_Test"] = e.Item.Cells[5].Text;
                Session["sz_SpecialityID_For_Test"] = e.Item.Cells[6].Text;
               
                Response.Redirect("~/AJAX Pages/Bill_Sys_BillReportForTest.aspx", false);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}
