using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Text;
using System.Configuration;

public partial class AJAX_Pages_View_InVoice : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            dddlInvoiceDate.Attributes.Add("onChange", "javascript:SetInvoiceDate();");
            //btnSearch.Attributes.Add("onclick", "return Validate();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlEmployer.Flag_ID = txtCompanyID.Text;
            btnDeleteInvoice.Attributes.Add("onclick", "return confirm_invoice_delete();");
            btnDeleteVisit.Attributes.Add("onclick", "return confirm_visit_delete();");

        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetData();
    }
    public void GetData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            EmployerBO objEmployerBO = new EmployerBO();
            string cases = "";
            if (txtCaseNo.Text != "")
            {

                string[] spiltcases = txtCaseNo.Text.Trim().Split(',');
                for (int i = 0; i < spiltcases.Length; i++)
                {
                    if (cases == "")
                    {
                        cases = "'" + spiltcases[i].ToString() + "'";
                    }
                    else
                    {
                        cases += ",'" + spiltcases[i].ToString() + "'";
                    }
                }

            }
            DataSet ds = objEmployerBO.GetAllInvoice(cases, txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, txtInvoceFromDate.Text, txtInvoceToDate.Text, extddlEmployer.Text, rdlstType.Text);
            grdVisits.DataSource = ds;
            grdVisits.DataBind();
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
    protected void grdVisits_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "OpenPdf")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "openpdf('" + e.Item.Cells[16].Text + "');", true);

        }
        else if (e.CommandName == "OpenToXls")
        {

            string pdfPath = e.Item.Cells[16].Text.Replace(ApplicationSettings.GetParameterValue("DomainName"), ApplicationSettings.GetParameterValue("PhysicalBasePath"));
            string htmlPath = pdfPath.Replace(Path.GetFileName(pdfPath), "") + Path.GetFileNameWithoutExtension(pdfPath) + ".htm";
            string xlspath = pdfPath.Replace(Path.GetFileName(pdfPath), "") + Path.GetFileNameWithoutExtension(pdfPath) + Path.GetFileNameWithoutExtension(pdfPath) + DateTime.Now.ToString("dd_MM_yyyy_ss") + ".xls";

            if (File.Exists(htmlPath))
            {
                
                File.Copy(htmlPath, xlspath, true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + xlspath.Replace(ApplicationSettings.GetParameterValue("PhysicalBasePath"), ApplicationSettings.GetParameterValue("DomainName")).Replace(@"\", @"\\") + "');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('file not found');", true);
            }



        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < grdVisits.Items.Count; i++)
        {

            if (grdVisits.Items[i].Cells[17].Text.ToLower() != "true")
            {
                LinkButton lnk = (LinkButton)grdVisits.Items[i].FindControl("lnkOpen");
                lnk.Visible = false;
                ImageButton lnkxls = (ImageButton)grdVisits.Items[i].FindControl("btnexportToXls");
                lnkxls.Visible = false;
            }
        }

    }

    protected void btnDeleteInvoice_Click(object sender, EventArgs e)
    { //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            for (int i = 0; i < grdVisits.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdVisits.Items[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                    if (grdVisits.Items[i].Cells[17].Text.ToLower() != "true")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Please select visit(s) for which invoice is generated.');", true);
                        return;
                    }
                }
            }
            ArrayList arrList = new ArrayList();
            for (int i = 0; i < grdVisits.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdVisits.Items[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                    EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
                    objEmployerVisitDO.InvoiceNo = grdVisits.Items[i].Cells[11].Text;
                    objEmployerVisitDO.CompanyID = txtCompanyID.Text;
                    arrList.Add(objEmployerVisitDO);
                }
            }
            EmployerBO objEmployerBO = new EmployerBO();
            string returnVal = objEmployerBO.DeleteInvoice(arrList);
            if (returnVal != "")
            {
                this.usrMessage.PutMessage("Invoice Deleted successfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage("Error to delete invoice.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            GetData();
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
    protected void btnDeleteVisit_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {

            for (int i = 0; i < grdVisits.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdVisits.Items[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                    if (grdVisits.Items[i].Cells[17].Text.ToLower() == "true")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('Please select visit(s) for which invoice is not generated.');", true);
                        return;
                    }
                }
            }
            ArrayList arrList = new ArrayList();
            for (int i = 0; i < grdVisits.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)grdVisits.Items[i].FindControl("chkSelect");
                if (chk.Checked)
                {
                    EmployerVisitDO objEmployerVisitDO = new EmployerVisitDO();
                    objEmployerVisitDO.VisitId = grdVisits.Items[i].Cells[3].Text;
                    objEmployerVisitDO.CompanyID = txtCompanyID.Text;
                    arrList.Add(objEmployerVisitDO);
                }
            }
            EmployerBO objEmployerBO = new EmployerBO();
            string returnVal = objEmployerBO.DeleteVisit(arrList);
            if (returnVal != "")
            {
                this.usrMessage.PutMessage("Visit Deleted successfully.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }
            else
            {
                this.usrMessage.PutMessage("Error to delete visit.");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage.Show();
            }
            GetData();
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
    public static string getFileName(string p_szBillNumber)
    {


        String szBillNumber = "";


        szBillNumber = p_szBillNumber;


        String szFileName;


        DateTime currentDate;


        currentDate = DateTime.Now;


        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");


        return szFileName;


    }


    public static string getRandomNumber()
    {


        System.Random objRandom = new Random();


        return objRandom.Next(1, 10000).ToString();


    }


    protected void btnexport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }
    private void ExportToExcel()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdVisits.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 1; i < grdVisits.Columns.Count; i++)
                    {
                        if (grdVisits.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdVisits.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 1; j < grdVisits.Columns.Count; j++)
                {
                    if (grdVisits.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdVisits.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("Invoice_Report") + ".xls";
            StreamWriter sw = new StreamWriter(ApplicationSettings.GetParameterValue("EXCEL_SHEET") + filename);
            sw.Write(strHtml);
            sw.Close();

            //   Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + filename + "');", true);


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

}