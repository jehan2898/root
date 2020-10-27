using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;

public partial class AJAX_Pages_ViewInVoice : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            dddlInvoiceDate.Attributes.Add("onChange", "javascript:SetInvoiceDate();");
            btnDeleteInvoice.Attributes.Add("onclick", "return confirm_invoice_delete();");
            btnDeleteVisit.Attributes.Add("onclick", "return confirm_visit_delete();");
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlEmployer.Flag_ID = txtCompanyID.Text;
            txtCaseId.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
            txtCaseNo.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
            txtCaseNo.Enabled = false;
            BindEmloyer();
            GetData();

        }
    }

    public void BindEmloyer()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            EmployerBO objEmployerBO = new EmployerBO();
            DataSet ds = objEmployerBO.GetCaseEmployer(txtCaseId.Text, txtCompanyID.Text);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    extddlEmployer.Text = ds.Tables[0].Rows[0]["SZ_EMPLOYER_ID"].ToString();
                    extddlEmployer.Enabled = false;

                }
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
    {    //VIEWWORD_DOC
        if (e.CommandName == "OpenPdf")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "openpdf('" + e.Item.Cells[14].Text + "');", true);
        }
        else if(e.CommandName == "OpenToXls")
        {
            string pdfpath = e.Item.Cells[14].Text.Replace(ApplicationSettings.GetParameterValue("DomainName"), ApplicationSettings.GetParameterValue("PhysicalBasePath"));
            string htmlPath = pdfpath.Replace(Path.GetFileName(pdfpath), "") + Path.GetFileNameWithoutExtension(pdfpath) + ".htm";
            string xlspath = pdfpath.Replace(Path.GetFileName(pdfpath), "") + Path.GetFileNameWithoutExtension(pdfpath) + DateTime.Now.ToString("dd_MM_yyyy_ss") + ".xls";
            if (File.Exists(htmlPath))
            {
                File.Copy(htmlPath, xlspath, true);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" +  xlspath.Replace(ApplicationSettings.GetParameterValue("PhysicalBasePath"), ApplicationSettings.GetParameterValue("DomainName")).Replace(@"\", @"\\") + "');", true);
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

            if (grdVisits.Items[i].Cells[15].Text.ToLower() != "true")
            {
                LinkButton lnk = (LinkButton)grdVisits.Items[i].FindControl("lnkOpen");
                lnk.Visible = false;
                ImageButton lnkxls = (ImageButton)grdVisits.Items[i].FindControl("btnexportToxls");
                lnkxls.Visible = false;
            }
        }

    }
    protected void btnDeleteInvoice_Click(object sender, EventArgs e)
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
                    if (grdVisits.Items[i].Cells[15].Text.ToLower() != "true")
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
                    objEmployerVisitDO.InvoiceNo = grdVisits.Items[i].Cells[10].Text;
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

                    if (grdVisits.Items[i].Cells[15].Text.ToLower() == "true")
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
}