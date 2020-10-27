using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class AJAX_Pages_EmplioyerCaseVisit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                hdnCompanyID.Value = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                hdnCompanyName.Value = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
                hdnUserID.Value = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                hdnUserName.Value = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME;
                hdnUrl.Value = ConfigurationManager.AppSettings["webscanurl"].ToString();
                extddlEmployer.Flag_ID = txtCompanyID.Text;
                txtCaseId.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                txtCaseNo.Text = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                btnSave.Attributes.Add("onclick", "return Validate();");
                btnDeleteVisit.Attributes.Add("onclick", "return confirm_visit_delete();");
                BindEmloyer();
                GetData();
                BindProcedureCodes();
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
    public void BindProcedureCodes()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            EmployerBO objEmployerBO = new EmployerBO();
            lstProcedureCode.Items.Clear();
            DataSet ds = objEmployerBO.GetEmployeProcedure(extddlEmployer.Text, txtCompanyID.Text);
            lstProcedureCode.DataSource = ds;
            lstProcedureCode.DataTextField = "DESCRIPTION";
            lstProcedureCode.DataValueField = "CODE";
            lstProcedureCode.DataBind();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            
            
            string[] arrDates = txtdate.Text.Trim().Split(',');
            ArrayList arrCases = new ArrayList();
            for (int i = 0; i < arrDates.Length-1 ; i++)
            {
               
                EmployerVisitDO objCases = new EmployerVisitDO();
                objCases.CaseID = txtCaseId.Text;
                objCases.CompanyID = txtCompanyID.Text;
                
                objCases.UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                objCases.EmploerID = extddlEmployer.Text;
                objCases.VisitDate = arrDates[i].ToString();
                objCases.EmployerVisitProcedure = new List<EmployerVisitProcedure>();

                foreach (ListItem lstitem in this.lstProcedureCode.Items)
                {
                    if (lstitem.Selected)
                    {
                        string lstVal = lstitem.Value;
                        string[] spiltVal = lstVal.Split('~');

                        EmployerVisitProcedure objEmployerVisitProcedure = new EmployerVisitProcedure();
                        objEmployerVisitProcedure.ProcedureCode = spiltVal[0].ToString();
                        objEmployerVisitProcedure.ProcedureGroupId = spiltVal[1].ToString();
                        objCases.EmployerVisitProcedure.Add(objEmployerVisitProcedure);

                    }
                }
                arrCases.Add(objCases);
                
            }

            EmployerBO objEmployerBO = new EmployerBO();
            string returnSave = objEmployerBO.SaveVisit(arrCases);
            if (returnSave != "")
            {
                this.usrMessage2.PutMessage("Visit save Sucessfully.");
                this.usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage2.Show();
            }
            else
            {
                this.usrMessage2.PutMessage("can not save the visit");
                this.usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage2.Show();
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

            DataSet ds = objEmployerBO.GetAllInvoice(txtCaseNo.Text, txtCompanyID.Text, "", "", "", "", extddlEmployer.Text, "2");
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
                this.usrMessage2.PutMessage("Visit Deleted successfully.");
                this.usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage2.Show();
            }
            else
            {
                this.usrMessage2.PutMessage("Error to delete visit.");
                this.usrMessage2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                this.usrMessage2.Show();
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
    //public void GetData()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("CASE_NO");
    //    dt.Columns.Add("Pname");
    //    dt.Columns.Add("VisitDate");
    //    dt.Columns.Add("INSURANCE");
    //    dt.Columns.Add("EMPLOYER");
    //    dt.Columns.Add("ProcedureCode");


    //    DataRow dr = dt.NewRow();
    //    dr["CASE_NO"] = "1";
    //    dr["Pname"] = "xyz";
    //    dr["VisitDate"] = "12/5/2015";
    //    dr["INSURANCE"] = "fjfk";
    //    dr["EMPLOYER"] = "jjdoljlhn";
    //    dr["ProcedureCode"] = "23435";

    //    dt.Rows.Add(dr);
    //    grdVisits.DataSource = dt;
    //    grdVisits.DataBind();
    //}
    protected void grdVisits_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

         
            string btValue=e.Item.Cells[19].Text;
            if (btValue.ToLower() == "true")
            {
                HtmlAnchor htmlAnchor = (HtmlAnchor)e.Item.FindControl("lnkView");
                htmlAnchor.Visible = true;

            }
            else
            {
                HtmlAnchor htmlAnchor = (HtmlAnchor)e.Item.FindControl("lnkView");
                htmlAnchor.Visible = false;
            }

           

        }    
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        GetData();

    }
}


