using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Componend;
using System.Drawing;
using System.Text;

public partial class Bill_Sys_TreatmentReport : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    ListOperation _listOperation;
    Bill_Sys_PatientBO _bill_Sys_PatientBO;
    ArrayList objAdd;
    string case_id, list_case_id;
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;


    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {           
            this.con.SourceGrid = grdCaseMaster;
            this.txtSearchBox.SourceGrid = grdCaseMaster;
            this.grdCaseMaster.Page = this.Page;
            this.grdCaseMaster.PageNumberList = this.con;
            this.Title = "Patient Visit Summary Report";

            ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlCaseType.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            btnViewReport.Attributes.Add("onClick", "return ShowConfirmation();");
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlDateValues1.Attributes.Add("onChange", "javascript:SetDateOpen();");
            ddlDateValues2.Attributes.Add("onChange", "javascript:SetVisitDate();");
            btnViewReport.Attributes.Add("onclick", "return ValidationViewReport();");
            
            if (!IsPostBack)           
            {
              
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlCaseStatus.Flag_ID = this.txtCompanyID.Text.ToString();
                string caseSatusId = new CaseDetailsBO().GetCaseSatusId(this.txtCompanyID.Text);
                this.extddlCaseStatus.Text = caseSatusId;
         
                SearchList();
              
            }
            lblMsg.Text = "";
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
            cv.MakeReadOnlyPage("Bill_Sys_TreatmentReport.aspx");
        }
        #endregion
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void SearchList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
            {

                grdCaseMaster.XGridDatasetNumber = 2;
                grdCaseMaster.XGridBindSearch();
                DataTable objDSLocationWise = new DataTable();
                objDSLocationWise = (DataTable)grdCaseMaster.XGridDataset;
                if (objDSLocationWise.Rows.Count > 0)
                {
                    objDSLocationWise = DisplayLocationInGrid(objDSLocationWise);
                    DataView dv = new DataView(objDSLocationWise);
                    grdCaseMaster.DataSource = dv;
                    grdCaseMaster.DataBind();
                }
                for (int i = 0; i < grdCaseMaster.Rows.Count; i++)
                {
                    string str = grdCaseMaster.Rows[i].Cells[2].Text.ToString();
                    str = str.ToString().Trim();
                    if (str.ToString().Trim() == "&nbsp;" || grdCaseMaster.Rows[i].Cells[1].Text == "Location Name")
                    {
                        ((CheckBox)grdCaseMaster.Rows[i].Cells[0].FindControl("chkSelect")).Visible = false;
                        grdCaseMaster.Rows[i].Cells[1].Font.Bold = true;
                        grdCaseMaster.Rows[i].Cells[4].Font.Bold = true;
                    }
                }
            }
            else
            {
                grdCaseMaster.XGridBindSearch();
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
  
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {           
            string company_id = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

            foreach (GridViewRow di in grdCaseMaster.Rows)
            {
            
                if (((CheckBox)di.FindControl("chkSelect")).Checked)
                {                   
                    case_id = case_id + "'" + grdCaseMaster.DataKeys[di.RowIndex][0].ToString() + "',";
                }
            }
            
                list_case_id = case_id.Substring(0, case_id.Length - 1);
                _bill_Sys_PatientBO = new Bill_Sys_PatientBO();

                DataTable dt = _bill_Sys_PatientBO.Get_SpecialityCountReport(list_case_id, txtCompanyID.Text,txtVisitFromDate.Text,txtVisitTODate.Text);
                Session["SpecialityCount"] = dt;

                Page.RegisterClientScriptBlock("mm", "<script language='javascript'>window.open('../Bill_Sys_GetTreatment.aspx', 'TreatmentData', 'left=30,top=30,scrollbars=1');</script>");                
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

    #region "Display Location wise patient in grid"

    public DataTable DisplayLocationInGrid(DataTable p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        DataTable objDT = new DataTable();
        try
        {                    
            objDT.Columns.Add("SZ_CASE_ID");
            objDT.Columns.Add("SZ_CASE_NO");
            objDT.Columns.Add("SZ_PATIENT_ID");
            objDT.Columns.Add("SZ_PATIENT_NAME");
            objDT.Columns.Add("SZ_CASE_TYPE_NAME");
            objDT.Columns.Add("DT_DATE_OF_ACCIDENT");
            objDT.Columns.Add("DT_CREATED_DATE");
            objDT.Columns.Add("DATE_OF_FIRST_VISIT");
            objDT.Columns.Add("DATE_OF_LAST_VISIT");
            objDT.Columns.Add("SZ_PATIENT_PHONE");


            DataRow objDR;
            string sz_Location_Name = "NA";


            for (int i = 0; i < p_objDS.Rows.Count; i++)
            {
                if (p_objDS .Rows[i]["SZ_LOCATION_NAME"].ToString().Equals(sz_Location_Name))
                {
                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = p_objDS.Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = p_objDS.Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_ID"] = p_objDS.Rows[i]["SZ_PATIENT_ID"].ToString();
                    objDR["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = p_objDS.Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["DT_CREATED_DATE"] = p_objDS.Rows[i]["DT_CREATED_DATE"].ToString();
                    objDR["DATE_OF_FIRST_VISIT"] = p_objDS.Rows[i]["DATE_OF_FIRST_VISIT"].ToString();
                    objDR["DATE_OF_LAST_VISIT"] = p_objDS.Rows[i]["DATE_OF_LAST_VISIT"].ToString();
                    objDR["SZ_PATIENT_PHONE"] = p_objDS.Rows[i]["SZ_PATIENT_PHONE"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Location_Name = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
                }
                else
                {
                    objDR = objDT.NewRow();
                    
                    objDR["SZ_CASE_ID"] = "";
                    objDR["SZ_CASE_NO"] = "Location Name";
                    objDR["SZ_PATIENT_ID"] = "";
                    objDR["SZ_PATIENT_NAME"] = "" + p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString() + "";
                    objDR["SZ_CASE_TYPE_NAME"] = "";
                    objDR["DT_DATE_OF_ACCIDENT"] = "";
                    objDR["DT_CREATED_DATE"] = "";
                    objDR["DATE_OF_FIRST_VISIT"] = "";
                    objDR["DATE_OF_LAST_VISIT"] = "";
                    objDR["SZ_PATIENT_PHONE"] = "";
                    int count = grdCaseMaster.Rows.Count;



                    objDT.Rows.Add(objDR);


                    objDR = objDT.NewRow();
                    objDR["SZ_CASE_ID"] = p_objDS.Rows[i]["SZ_CASE_ID"].ToString();
                    objDR["SZ_CASE_NO"] = p_objDS.Rows[i]["SZ_CASE_NO"].ToString();
                    objDR["SZ_PATIENT_ID"] = p_objDS.Rows[i]["SZ_PATIENT_ID"].ToString();
                    objDR["SZ_PATIENT_NAME"] = p_objDS.Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_CASE_TYPE_NAME"] = p_objDS.Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                    objDR["DT_DATE_OF_ACCIDENT"] = p_objDS.Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                    objDR["DT_CREATED_DATE"] = p_objDS.Rows[i]["DT_CREATED_DATE"].ToString();
                    objDR["DATE_OF_FIRST_VISIT"] = p_objDS.Rows[i]["DATE_OF_FIRST_VISIT"].ToString();
                    objDR["DATE_OF_LAST_VISIT"] = p_objDS.Rows[i]["DATE_OF_LAST_VISIT"].ToString();
                    objDR["SZ_PATIENT_PHONE"] = p_objDS.Rows[i]["SZ_PATIENT_PHONE"].ToString();


                    objDT.Rows.Add(objDR);

                    sz_Location_Name = p_objDS.Rows[i]["SZ_LOCATION_NAME"].ToString();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        return objDT;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        if((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION =="1"))
        {
           DataTable  dt = new DataTable();
            dt =  Get_Patient_Visit_Records(txtCompanyID.Text, Convert.ToInt32(1), Convert.ToInt32(this.grdCaseMaster.RecordCount),txtCaseNumber.Text,txtPatientName.Text,txtFromDate.Text,txtToDate.Text,extddlCaseType.Text);
            if (dt.Rows.Count > 0)
            {
                dt = DisplayLocationInGrid(dt);
            }

            ExportToExcel(dt);
        }
        else
        {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdCaseMaster.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
        }
    }

    protected void grdCaseMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i = Convert.ToInt32(con.SelectedValue);
        grdCaseMaster.SelectablePageIndexChanged(Convert.ToInt16(con.SelectedValue) - 1);
        if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1"))
        {
            SearchList();
            con.SelectedValue = i.ToString();
        }
    }    

    protected void ExportToExcel(DataTable dt)
    {       

        StringBuilder strHtml = new StringBuilder();
        string ColomnName = "";
        strHtml.Append("<table border='1px'>");
        for (int icount = 0; icount < dt.Rows.Count; icount++)
        {
            if (icount == 0)
            {
                strHtml.Append("<tr>");
               foreach (DataColumn column in dt.Columns)   //(int i = 0; i < dt.Rows[0].Columns.Count; i++)
                {
                    if (column.ColumnName != "SZ_CASE_ID" && column.ColumnName != "SZ_PATIENT_ID" && column.ColumnName != "ROWID" && column.ColumnName != "SZ_LOCATION_NAME")
                    {
                        if (column.ColumnName == "SZ_CASE_NO")
                        {
                            ColomnName = "Case No";
                        }
                        else if (column.ColumnName == "SZ_PATIENT_NAME")
                        {
                            ColomnName = "Patient Name";
                        }
                        else if (column.ColumnName == "SZ_CASE_TYPE_NAME")
                        {
                            ColomnName = "Case Type";
                        }
                        else if (column.ColumnName == "DT_DATE_OF_ACCIDENT")
                        {
                            ColomnName = "Date Of Accident";
                        }
                        else if (column.ColumnName == "DT_CREATED_DATE")
                        {
                            ColomnName = "Date Open";
                        }
                        else if (column.ColumnName == "DATE_OF_FIRST_VISIT")
                        {
                            ColomnName = "DATE OF FIRST VISIT";
                        }
                        else if (column.ColumnName == "DATE_OF_LAST_VISIT")
                        {
                            ColomnName = "DATE OF LAST VISIT";
                        }
                        strHtml.Append("<td>");
                        strHtml.Append(ColomnName);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");
            }

            strHtml.Append("<tr>");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (dt.Columns[j].ColumnName != "SZ_CASE_ID" && dt.Columns[j].ColumnName != "SZ_PATIENT_ID" && dt.Columns[j].ColumnName != "ROWID" && dt.Columns[j].ColumnName != "SZ_LOCATION_NAME")
                {
                    strHtml.Append("<td>");
                    if (j == 1 && dt.Rows[icount][5].ToString().Equals("&nbsp;"))
                    {
                        strHtml.Append("<b>Location</b>");
                    }
                    else
                    {
                        strHtml.Append(dt.Rows[icount][j].ToString());
                    }

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

    public DataTable Get_Patient_Visit_Records(string sz_Company_Id, int sz_Start_Index, int sz_End_Index,string sz_CaseNumber,string sz_Patient_Name,string dt_From_Date,string dt_To_Date,string sz_Case_Type)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataTable dt = new DataTable();
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PATIENT_VISIT_SUMMARY_REPORT_SEARCH", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_Company_Id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NUMBER", sz_CaseNumber);
            sqlCmd.Parameters.AddWithValue("@SZ_PATIENT_NAME", sz_Patient_Name);
            sqlCmd.Parameters.AddWithValue("@DT_FROM_DATE", dt_From_Date);
            sqlCmd.Parameters.AddWithValue("@DT_TO_DATE", dt_To_Date);
            if (sz_Case_Type != "---Select---")
            {
                sqlCmd.Parameters.AddWithValue("@SZ_CASE_TYPE", sz_Case_Type);
            }
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", sz_Start_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", sz_End_Index);
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(dt);
        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
       
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return dt;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ClearFields()", true);
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
            SearchList();
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

    protected void btn_ViewAll_Click(object sender, EventArgs e)
    {
        case_id = "";
        list_case_id = "";
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        DataSet ds=new DataSet();
        ArrayList arr = new ArrayList();
        arr.Add(txtCaseNumber.Text);
        arr.Add(txtPatientName.Text);        
        arr.Add(txtCompanyID.Text);
        arr.Add(txtFromDate.Text);
        arr.Add(txtToDate.Text);
        arr.Add(extddlCaseType.Text);
        arr.Add(txtOpenFromDate.Text);
        arr.Add(txtOpenToDate.Text);
        ds = _bill_Sys_PatientBO.Get_Patient_Visit_Summary_Report(arr);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            case_id = case_id + "'" + ds.Tables[0].Rows[i][1].ToString() + "',";
        }
            

        list_case_id = case_id.Substring(0, case_id.Length - 1);
        _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        arr=new ArrayList();
        DataTable dt = _bill_Sys_PatientBO.Get_SpecialityCountReport(list_case_id, txtCompanyID.Text,txtVisitFromDate.Text,txtVisitTODate.Text);
        Session["SpecialityCount"] = dt;

        Page.RegisterClientScriptBlock("mm", "<script language='javascript'>window.open('../Bill_Sys_GetTreatment.aspx', 'TreatmentData', 'left=30,top=30,scrollbars=1');</script>");
    }
}
