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
using XMLDMLComponent;
using mbs.dao;


public partial class Bill_Sys_ReferralScheduleReport : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        try
        {
            ajAutoPatient.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            if (!IsPostBack)
            {
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;                               
                extddlSpeciality.Flag_ID = txtCompanyID.Text;
                extddlDoctor.Flag_ID = txtCompanyID.Text;
                extddlOffice.Flag_ID = txtCompanyID.Text;
               //BindGrid();
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
            cv.MakeReadOnlyPage("Bill_Sys_ReferralScheduleReport.aspx");
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
            DataSet ds = new DataSet();
            ArrayList objAL = new ArrayList();
            if (Page.IsPostBack)
            {
                objAL.Add(txtFromDate.Text);
                objAL.Add(txtToDate.Text);
            }
            else
            {
                objAL.Add("01/01/1900");
                objAL.Add(DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"));
            }
            objAL.Add(extddlDoctor.Text);
            objAL.Add(ddlStatus.SelectedValue.ToString());
            objAL.Add(txtCompanyID.Text);
            objAL.Add(extddlSpeciality.Text);

            string szCaseList = "";
            string szProcDescList = "";

            if (txtCaseNo.Text.Length != 0)
            {
                szCaseList = getCaseList();
            }

            if ((!lstProcedureDesc.SelectedValue.ToString().Equals("")) && (!ddlProcedureCode.SelectedValue.ToString().Equals("NA")))
            {
                szProcDescList = getProcDescList();
            }

            //_reportBO = new Bill_Sys_ReportBO();
            //grdPayment.DataSource = _reportBO.Get_Referral_Schedule_Report(objAL);
            //grdPayment.DataBind();

            //Connection.ConnectionString str = new Connection.ConnectionString();
            String szConnection = System.Configuration.ConfigurationManager.AppSettings["Connection_String"];
            XMLDMLComponent.SQLToDAO objDao = new XMLDMLComponent.SQLToDAO(szConnection);
            mbs.dao.ScheduleReport sReport = new ScheduleReport();
            sReport.from_date = txtFromDate.Text;
            sReport.to_date = txtToDate.Text;
            sReport.sz_company_id = txtCompanyID.Text;
            sReport.sz_case_no = szCaseList;
            sReport.sz_Patient_Name = txtPatientName.Text;
            sReport.i_status = ddlStatus.Text;
            sReport.sz_doctor_id = extddlDoctor.Text;
            sReport.SZ_PROCEDURE_GROUP_ID = extddlSpeciality.Text;
            sReport.SZ_PROCEDURE_CODE = ddlProcedureCode.Text;
            sReport.SZ_PROCEDURE_CODE_DESC = szProcDescList;
            sReport.sz_Provider_id = extddlOffice.Text;
            if (chkRfferal.Checked)
            {
                sReport.IsRefferalDoc = "1";
            }
            else
            {
                sReport.IsRefferalDoc = "NA";
            }
          

           
            ds = objDao.LoadDataSet("SP_REFFERAL_SCHEDULE_REPORT", "mbs.dao.ScheduleReport", sReport, "mbs.dao");

            DataView dv1;
            dv1 = ds.Tables[0].DefaultView;

            Session["grdPayment"] = ds;

            grdPayment.DataSource = dv1;
            grdPayment.DataBind();
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

    //private string getCaseList()
    //{
    //    string sList = "";
    //    Boolean Flag=false;
    //    txtCaseNo.Text = txtCaseNo.Text.Replace("'", "");
    //    txtCaseNo.Text = txtCaseNo.Text.Replace("\"", "");

    //    string[] sArr = txtCaseNo.Text.Split(',');

    //    if (sArr.Length > 0)
    //    {             
    //        for (int i = 0; i < sArr.Length; i++)
    //        {
    //            char[] ch = sArr[i].ToCharArray(0, 2);
    //            for (int j = 0; j < ch.Length; j++) {

    //                if ((ch[j] >= 'A' && ch[j] <= 'Z') || (ch[j] >= 'a' && ch[j] <= 'z'))
    //                { 
    //                       Flag = true;    
    //                       break;    
    //                }    
    //         }
    //         if (Flag == true)
    //         {
    //             sArr[i] = sArr[i].Substring(2);
    //             Flag = false;
    //         }
    //         sList = sList + "'" + sArr[i] + "',";

    //       }
    //        sList = sList.Remove(sList.Length - 1);
    //        sList = "(" + sList + ")";
    //    }

    //    return sList;
    //}

    private string getCaseList()
    {
        string sList = "";
        Boolean Flag = false;
        txtCaseNo.Text = txtCaseNo.Text.Replace("'", "");
        txtCaseNo.Text = txtCaseNo.Text.Replace("\"", "");

        string[] sArr = txtCaseNo.Text.Split(',');

        if (sArr.Length > 0)
        {
            for (int i = 0; i < sArr.Length; i++)
            {
                if (sArr[i].Length > 2)
                {
                    char[] ch = sArr[i].ToCharArray(0, 2);
                    for (int j = 0; j < ch.Length; j++)
                    {

                        if ((ch[j] >= 'A' && ch[j] <= 'Z') || (ch[j] >= 'a' && ch[j] <= 'z'))
                        {
                            Flag = true;
                            break;
                        }
                    }
                    if (Flag == true)
                    {
                        sArr[i] = sArr[i].Substring(2);
                        Flag = false;
                    }
                    sList = sList + "'" + sArr[i] + "',";
                }
                else
                {
                    sList = sList + "'" + sArr[i] + "',";
                }
            }
            sList = sList.Remove(sList.Length - 1);
            sList = "(" + sList + ")";
        }

        return sList;
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

    #region "Export to Excel"

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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
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
            for (int icount = 0; icount < grdPayment.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdPayment.Columns.Count; i++)
                    {
                        if (grdPayment.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdPayment.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdPayment.Columns.Count; j++)
                {
                    if (grdPayment.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdPayment.Items[icount].Cells[j].Text);
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

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

    #endregion


    
    protected void extddlSpeciality_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (extddlSpeciality.Text != "NA")
        {
            lblProcedureCode.Visible = true;
            ddlProcedureCode.Visible = true;
            _reportBO = new Bill_Sys_ReportBO();
            DataSet DS = new DataSet();
            DS = _reportBO.GetProcedureCode(txtCompanyID.Text, extddlSpeciality.Text);
            ddlProcedureCode.DataSource = DS;
            ddlProcedureCode.DataValueField = "Code";
            ddlProcedureCode.DataTextField = "Description";
            ddlProcedureCode.DataBind();
            ListItem objTest = new ListItem();
            objTest.Text = "--- Select ---";
            objTest.Value = "NA";
            ddlProcedureCode.Items.Insert(0, objTest);
        }
        else 
        {
            ddlProcedureCode.SelectedIndex = 0;            
            lblProcedureCode.Visible = false;
            ddlProcedureCode.Visible = false;
            lblProcedureDesc.Visible = false;
            lstProcedureDesc.Visible = false;
        }
    }

    protected void ddlProcedureCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ddlProcedureCode.SelectedItem.ToString().Equals("--- Select ---"))
        {
            lblProcedureDesc.Visible = true;
            lstProcedureDesc.Visible = true;
            _reportBO = new Bill_Sys_ReportBO();
            DataSet DS = new DataSet();

            DS = _reportBO.GetProcedureDesc(txtCompanyID.Text, ddlProcedureCode.SelectedItem.ToString());
            lstProcedureDesc.DataSource = DS;
            lstProcedureDesc.DataTextField = DS.Tables[0].Columns[0].ToString();
            lstProcedureDesc.DataValueField = DS.Tables[0].Columns[1].ToString();
            lstProcedureDesc.DataBind();
        }
        else
        {
            ddlProcedureCode.SelectedIndex = 0;          
            lblProcedureDesc.Visible = false;
            lstProcedureDesc.Visible = false;
        }
    }


    private string getProcDescList()
    {
        string sList = "";

         
            sList =  lstProcedureDesc.SelectedValue.ToString();
        
            return sList;
    }



    protected void grdPayment_ItemCommand(object source, DataGridCommandEventArgs e)
    {                  
            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["grdPayment"];
            dv = ds.Tables[0].DefaultView;

            if (e.CommandName.ToString() == "Case")
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
            else if (e.CommandName.ToString() == "Patient Name")
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
            else if (e.CommandName.ToString() == "Attorney")
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
            
            else if (e.CommandName.ToString() == "Accident Date")
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

        else if (e.CommandName.ToString() == "Visit Date")
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
            grdPayment.DataSource = dv;
            grdPayment.DataBind();                  
        }
    }

