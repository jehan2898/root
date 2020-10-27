using ASP;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Bill_Sys_VisitReportPerPatient : Page, IRequiresSessionState
{

    private SqlConnection sqlCon;

    private SqlCommand sqlCmd;

    private DataSet ds;

    private SqlDataAdapter sqlda;

    private SqlDataReader dr;

    private string CompanyID;

    private DataTable dtTCount;

    private DataTable dtTRecords;

    private DataTable dtCaseWise;

    private bool flag;



    public Bill_Sys_VisitReportPerPatient()
    {
    }

    protected void BindGridCaseWise(DataTable dt)
    {
        decimal num = new decimal(0);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            num = num + Convert.ToDecimal(dt.Rows[i]["Total Amount"].ToString());
        }
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Total Cases");
        dataTable.Columns.Add("Total Patients");
        dataTable.Columns.Add("No Of Visits");
        dataTable.Columns.Add("Total Bills");
        dataTable.Columns.Add("Total Amount");
        DataRow str = dataTable.NewRow();
        str["Total Cases"] = this.dtTCount.Rows[0]["Total Cases"].ToString();
        str["Total Patients"] = this.dtTCount.Rows[0]["Total Patients"].ToString();
        str["No Of Visits"] = this.dtTCount.Rows[0]["No Of Visits"].ToString();
        str["Total Bills"] = this.dtTCount.Rows[0]["Total Bills"].ToString();
        str["Total Amount"] = num.ToString();
        dataTable.Rows.Add(str);
        if (this.dtTCount.Rows.Count > 0)
        {
            GridView gridView = new GridView()
            {
                DataSource = dataTable
            };
            gridView.RowDataBound += new GridViewRowEventHandler(this.gv_rowDataBound);
            gridView.DataBind();
            gridView.Width = Convert.ToInt32(855);
            this.form1.Controls.Add(gridView);
        }
        if (dt.Rows.Count > 0)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                int num1 = 0;
                string str1 = dt.Rows[j]["Case No"].ToString();
                this.dtCaseWise = new DataTable();
                this.dtCaseWise.Columns.Add("Case No");
                this.dtCaseWise.Columns.Add("Patient Name");
                this.dtCaseWise.Columns.Add("Visit Date");
                this.dtCaseWise.Columns.Add("Doctor Name");
                this.dtCaseWise.Columns.Add("Provider Name");
                this.dtCaseWise.Columns.Add("Location");
                this.dtCaseWise.Columns.Add("Speciality");
                this.dtCaseWise.Columns.Add("Insurance Name");
                if (base.Request.QueryString["ShowAmount"].ToString().Equals("on"))
                {
                    this.dtCaseWise.Columns.Add("Total Amount");
                }
                for (int k = j; k < dt.Rows.Count && str1 == dt.Rows[k]["Case No"].ToString(); k++)
                {
                    DataRow dataRow = this.dtCaseWise.NewRow();
                    dataRow["Case No"] = dt.Rows[k]["Case No"].ToString();
                    dataRow["Patient Name"] = dt.Rows[k]["Patient Name"].ToString();
                    dataRow["Visit Date"] = dt.Rows[k]["Event Date"].ToString();
                    dataRow["Doctor Name"] = dt.Rows[k]["Doctor Name"].ToString();
                    dataRow["Provider Name"] = dt.Rows[k]["Provider Name"].ToString();
                    dataRow["Location"] = dt.Rows[k]["Location"].ToString();
                    dataRow["Speciality"] = dt.Rows[k]["Speciality"].ToString();
                    dataRow["Insurance Name"] = dt.Rows[k]["Insurance Name"].ToString();
                    if (base.Request.QueryString["ShowAmount"].ToString().Equals("on"))
                    {
                        dataRow["Total Amount"] = dt.Rows[k]["Total Amount"].ToString();
                    }
                    this.dtCaseWise.Rows.Add(dataRow);
                    num1++;
                    j++;
                }
                GridView gridView1 = new GridView()
                {
                    DataSource = this.dtCaseWise
                };
                gridView1.RowDataBound += new GridViewRowEventHandler(this.gv_rowcreated);
                gridView1.DataBind();
                gridView1.Width = Convert.ToInt32(855);
                this.form1.Controls.Add(gridView1);
                DataTable dataTable1 = new DataTable();
                dataTable1.Columns.Add("No Of Visits");
                DataRow dataRow1 = dataTable1.NewRow();
                dataRow1["No Of Visits"] = num1.ToString();
                dataTable1.Rows.Add(dataRow1);
                GridView gridView2 = new GridView()
                {
                    DataSource = dataTable1
                };
                gridView2.DataBind();
                this.form1.Controls.Add(gridView2);
                if (j != dt.Rows.Count)
                {
                    j--;
                }
            }
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.ExportToExcel();
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

    protected void CreateReport()
    {
        int num = 0;
        int num1 = 0;
        int num2 = 0;
        float single = 0f;
        int i = 0;
        int num3 = 0;
        float single1 = 0f;
        if (this.ds != null && this.ds.Tables[0].Rows.Count > 0)
        {
            while (num3 < this.ds.Tables[0].Rows.Count)
            {
                num = 0;
                if (this.ds.Tables[0].Rows[num3]["sz_case_id"].ToString() != "")
                {
                    for (i = num3; i < this.ds.Tables[0].Rows.Count && this.ds.Tables[0].Rows[num3]["sz_case_id"].ToString() == this.ds.Tables[0].Rows[i]["sz_case_id"].ToString(); i++)
                    {
                        if (num == 0)
                        {
                            num1++;
                            if (float.TryParse(this.ds.Tables[0].Rows[i]["Visit_Amount"].ToString(), out single1))
                            {
                                single = single + single1;
                            }
                        }
                        else if (this.ds.Tables[0].Rows[i]["i_event_id"].ToString() != this.ds.Tables[0].Rows[i - 1]["i_event_id"].ToString())
                        {
                            num1++;
                            if (float.TryParse(this.ds.Tables[0].Rows[i]["Visit_Amount"].ToString(), out single1))
                            {
                                single = single + single1;
                            }
                        }
                        num2++;
                        num++;
                    }
                    HtmlForm htmlForm = this.form1;
                    string[] innerHtml = new string[] { this.form1.InnerHtml, "<table cellpadding='1' cellspacing='0' style='border-collapse:collapse; margin-bottom:50px; border-style:solid; border-width:medium; font-family:Arial; font-size:small; width:100%; page-break-after:always;'><tr style=''><td  style='border-style:solid; border-width:thin; font-weight:bold'>Case #</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[num3]["sz_case_no"].ToString(), "</td><td  style='border-style:solid; border-width:thin; font-weight:bold'>Patient Name</td><td colspan='2' style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[num3]["sz_patient_name"].ToString(), "</td><td  style='border-style:solid;  border-width:thin; font-weight:bold;'>Total Number of Visits</td><td style='border-style:solid;  border-width:thin; text-align:right'>", num1.ToString(), "</td><td  style='border-style:solid; border-width:thin; font-weight:bold; '>Total Visit Amount</td><td style='border-style:solid;  border-width:thin; text-align:right'>", single.ToString("c"), "</td></tr><tr><td colspan='8' style='border-style:solid; border-width:thin;'>&nbsp;</td></tr>" };
                    htmlForm.InnerHtml = string.Concat(innerHtml);
                    this.form1.InnerHtml = string.Concat(this.form1.InnerHtml, "<tr  style='font-weight:bold'><td colspan='9' style='border-style:solid; border-bottom-width:medium;'>Visit Details</td></tr><tr  style='font-weight:bold'><td style='border-style:solid; border-width:thin;'>Visit Date</td><td style='border-style:solid; border-width:thin;'>Doctor Name</td><td style='border-style:solid; border-width:thin;'>Provider Name</td><td style='border-style:solid; border-width:thin;'>Location</td><td style='border-style:solid; border-width:thin;'>Specialty</td><td style='border-style:solid; border-width:thin;'>Insurance</td><td style='border-style:solid; border-width:thin;'>Claim Number</td><td style='border-style:solid; border-width:thin;'>Visit Status</td><td style='border-style:solid; border-width:thin;'>Total Amount</td></tr>");
                    num = 0;
                    for (int j = num3; j < num2 + num3; j++)
                    {
                        if (num == 0 || !(this.ds.Tables[0].Rows[j]["I_EVENT_ID"].ToString() == this.ds.Tables[0].Rows[j - 1]["I_EVENT_ID"].ToString()))
                        {
                            string str = "";
                            float single2 = 0f;
                            float.TryParse(this.ds.Tables[0].Rows[j]["Visit_Amount"].ToString(), out single2);
                            if (this.ds.Tables[0].Rows[j]["DT_EVENT_DATE"].ToString() != "")
                            {
                                try
                                {
                                    if (Convert.ToDateTime(this.ds.Tables[0].Rows[j]["DT_EVENT_DATE"].ToString()) != DateTime.Parse("1/1/1900"))
                                    {
                                        DateTime dateTime = Convert.ToDateTime(this.ds.Tables[0].Rows[j]["DT_EVENT_DATE"].ToString());
                                        str = dateTime.ToString("MM/dd/yyyy");
                                    }
                                }
                                catch
                                {
                                }
                            }
                            string str1 = "";
                            if (this.ds.Tables[0].Rows[j]["I_STATUS"].ToString() != "")
                            {
                                if (this.ds.Tables[0].Rows[j]["I_STATUS"].ToString() == "0")
                                {
                                    str1 = "Scheduled";
                                }
                                else if (this.ds.Tables[0].Rows[j]["I_STATUS"].ToString() == "1")
                                {
                                    str1 = "Re-Scheduled";
                                }
                                else if (this.ds.Tables[0].Rows[j]["I_STATUS"].ToString() == "2")
                                {
                                    str1 = "Completed";
                                }
                                else if (this.ds.Tables[0].Rows[j]["I_STATUS"].ToString() == "2")
                                {
                                    str1 = "No-Show";
                                }
                            }
                            HtmlForm htmlForm1 = this.form1;
                            string[] strArrays = new string[] { this.form1.InnerHtml, "<tr style=''><td style='border-style:solid; border-width:thin;'>", str, "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[j]["SZ_DOCTOR_NAME"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[j]["SZ_OFFICE"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[j]["SZ_LOCATION"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[j]["SZ_PROCEDURE_GROUP"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[j]["SZ_INSURANCE_NAME"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[j]["SZ_CLAIM_NUMBER"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", str1, "</td><td style='border-style:solid; border-width:thin; text-align:right'>", single2.ToString("c"), "</td></tr>" };
                            htmlForm1.InnerHtml = string.Concat(strArrays);
                            num++;
                        }
                    }
                    this.form1.InnerHtml = string.Concat(this.form1.InnerHtml, "<tr><td colspan='9' style='border-style:solid; border-width:thin;'>&nbsp;</td></tr><tr  style='font-weight:bold'><td colspan='9' style='border-style:solid; border-bottom-width:medium;'>Bill Details</td></tr><tr  style='font-weight:bold'><td style='border-style:solid; border-width:thin;'>Bill Number</td><td style='border-style:solid; border-width:thin;'>Specialty</td><td style='border-style:solid; border-width:thin;'>Visit Date</td><td style='border-style:solid; border-width:thin;'>Bill Date</td><td style='border-style:solid; border-width:thin;'>Bill Status</td><td style='border-style:solid; border-width:thin;'>Bill Amount</td><td style='border-style:solid; border-width:thin;'>Paid Amount</td><td style='border-style:solid; border-width:thin;'>Outstanding Amount</td><td style='border-style:solid; border-width:thin;'>Denial Reasons</td></tr>");
                    ArrayList arrayLists = new ArrayList();
                    float single3 = 0f;
                    float single4 = 0f;
                    float single5 = 0f;
                    for (int k = num3; k < num2 + num3; k++)
                    {
                        string str2 = "";
                        string str3 = "";
                        bool flag = false;
                        int num4 = 0;
                        while (num4 < arrayLists.Count)
                        {
                            if (this.ds.Tables[0].Rows[k]["sz_bill_number"].ToString() != arrayLists[num4].ToString())
                            {
                                num4++;
                            }
                            else
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag && this.ds.Tables[0].Rows[k]["sz_bill_number"].ToString() != "")
                        {
                            arrayLists.Add(this.ds.Tables[0].Rows[k]["sz_bill_number"].ToString());
                            if (this.ds.Tables[0].Rows[k]["dt_first_visit_date"].ToString() != "")
                            {
                                try
                                {
                                    if (Convert.ToDateTime(this.ds.Tables[0].Rows[k]["dt_first_visit_date"].ToString()) != DateTime.Parse("1/1/1900"))
                                    {
                                        DateTime dateTime1 = Convert.ToDateTime(this.ds.Tables[0].Rows[k]["dt_first_visit_date"].ToString());
                                        str3 = string.Concat(dateTime1.ToString("MM/dd/yyyy"), " - ");
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (this.ds.Tables[0].Rows[k]["dt_last_visit_date"].ToString() != "")
                            {
                                try
                                {
                                    if (Convert.ToDateTime(this.ds.Tables[0].Rows[k]["dt_last_visit_date"].ToString()) != DateTime.Parse("1/1/1900"))
                                    {
                                        DateTime dateTime2 = Convert.ToDateTime(this.ds.Tables[0].Rows[k]["dt_last_visit_date"].ToString());
                                        str3 = string.Concat(str3, dateTime2.ToString("MM/dd/yyyy"));
                                    }
                                }
                                catch
                                {
                                }
                            }
                            if (this.ds.Tables[0].Rows[k]["dt_bill_date"].ToString() != "")
                            {
                                try
                                {
                                    if (Convert.ToDateTime(this.ds.Tables[0].Rows[k]["dt_bill_date"].ToString()) != DateTime.Parse("1/1/1900"))
                                    {
                                        DateTime dateTime3 = Convert.ToDateTime(this.ds.Tables[0].Rows[k]["dt_bill_date"].ToString());
                                        str2 = dateTime3.ToString("MM/dd/yyyy");
                                    }
                                }
                                catch
                                {
                                }
                            }
                            float single6 = 0f;
                            float single7 = 0f;
                            float single8 = 0f;
                            try
                            {
                                single6 = float.Parse(this.ds.Tables[0].Rows[k]["paid_amount"].ToString());
                            }
                            catch
                            {
                            }
                            try
                            {
                                single7 = float.Parse(this.ds.Tables[0].Rows[k]["flt_bill_amount"].ToString());
                            }
                            catch
                            {
                            }
                            try
                            {
                                single8 = float.Parse(this.ds.Tables[0].Rows[k]["FLT_BALANCE"].ToString());
                            }
                            catch
                            {
                            }
                            string str4 = "";
                            ArrayList arrayLists1 = new ArrayList();
                            for (int l = k; l < num2 + num3; l++)
                            {
                                if (this.ds.Tables[0].Rows[k]["sz_bill_number"].ToString() == this.ds.Tables[0].Rows[l]["sz_bill_number"].ToString() && this.ds.Tables[0].Rows[l]["Denial_Reason"].ToString() != "")
                                {
                                    int num5 = 0;
                                    num5 = 0;
                                    while (num5 < arrayLists1.Count && !(this.ds.Tables[0].Rows[l]["Denial_Reason"].ToString() == arrayLists1[num5].ToString()))
                                    {
                                        num5++;
                                    }
                                    if (num5 == arrayLists1.Count)
                                    {
                                        str4 = string.Concat(str4, " - ", this.ds.Tables[0].Rows[l]["Denial_Reason"].ToString(), ".<br />");
                                        arrayLists1.Add(this.ds.Tables[0].Rows[l]["Denial_Reason"].ToString());
                                    }
                                }
                            }
                            if (str4 != "")
                            {
                                str4 = str4.Substring(0, str4.LastIndexOf("<br />"));
                            }
                            HtmlForm htmlForm2 = this.form1;
                            string[] innerHtml1 = new string[] { this.form1.InnerHtml, "<tr style=''><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[k]["sz_bill_number"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[k]["SZ_PROCEDURE_GROUP"].ToString(), "</td><td style='border-style:solid; border-width:thin;'>", str3, "</td><td style='border-style:solid; border-width:thin;'>", str2, "</td><td style='border-style:solid; border-width:thin;'>", this.ds.Tables[0].Rows[k]["sz_bill_status_name"].ToString(), "</td><td style='border-style:solid; border-width:thin; text-align:right'>", single7.ToString("c"), "</td><td style='border-style:solid; border-width:thin; text-align:right'>", single6.ToString("c"), "</td><td style='border-style:solid; border-width:thin; text-align:right'>", single8.ToString("c"), "</td><td style='border-style:solid; border-width:thin; text-align:left'>", str4, "</td></tr>" };
                            htmlForm2.InnerHtml = string.Concat(innerHtml1);
                            single3 = single3 + single6;
                            single4 = single4 + single7;
                            single5 = single5 + single8;
                        }
                    }
                    HtmlForm htmlForm3 = this.form1;
                    string[] strArrays1 = new string[] { this.form1.InnerHtml, "<tr style=''><td colspan='5' style='border-style:solid; border-width:thin; text-align:right; font-weight:bold;'>Total</td><td style='border-style:solid; border-width:thin; text-align:right; font-weight:bold;'>", single4.ToString("c"), "</td><td style='border-style:solid; border-width:thin; text-align:right; font-weight:bold;'>", single3.ToString("c"), "</td><td style='border-style:solid; border-width:thin; text-align:right; font-weight:bold;'>", single5.ToString("c"), "</td><td style='border-style:solid; border-width:thin; text-align:right; font-weight:bold;'>&nbsp;</td></tr>" };
                    htmlForm3.InnerHtml = string.Concat(strArrays1);
                }
                single = 0f;
                num1 = 0;
                num2 = 0;
                num3 = i;
            }
        }
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
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<table border='1px'>");
            DataTable item = (DataTable)this.Session["EXPORTSpecialityCount"];
            for (int i = 0; i < item.Rows.Count; i++)
            {
                if (i == 0)
                {
                    stringBuilder.Append("<tr>");
                    for (int j = 0; j < item.Columns.Count; j++)
                    {
                        stringBuilder.Append("<td>");
                        stringBuilder.Append(item.Columns[j].Caption);
                        stringBuilder.Append("</td>");
                    }
                    stringBuilder.Append("</tr>");
                }
                stringBuilder.Append("<tr>");
                for (int k = 0; k < item.Columns.Count; k++)
                {
                    stringBuilder.Append("<td>");
                    stringBuilder.Append(item.Rows[i].ItemArray.GetValue(k).ToString());
                    stringBuilder.Append("</td>");
                }
                stringBuilder.Append("</tr>");
            }
            stringBuilder.Append("</table>");
            string str = string.Concat(this.getFileName("EXCEL"), ".xls");
            StreamWriter streamWriter = new StreamWriter(string.Concat(ConfigurationManager.AppSettings["EXCEL_SHEET"], str));
            streamWriter.Write(stringBuilder);
            streamWriter.Close();
            base.Response.Redirect(string.Concat(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"], str), false);
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

    protected void GetData()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_SHOW_PROVIDER_REPORT_NEW", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", this.CompanyID.ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_location_id", base.Request.QueryString["LocationId"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_doctor_id", base.Request.QueryString["DoctorId"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@dt_from_event_date", base.Request.QueryString["FromDate"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@dt_to_event_date", base.Request.QueryString["ToDate"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", base.Request.QueryString["Speciality"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_show_denials", base.Request.QueryString["Showdenials"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_DENIAL_CREATED_DATE_FROM", base.Request.QueryString["DenialFromDate"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@DT_DENIAL_CREATED_DATE_TO", base.Request.QueryString["DenialToDate"].ToString());
                (new SqlDataAdapter(this.sqlCmd)).Fill(this.ds);
            }
            catch (SqlException ex)
            {
                SqlException sqlException = ex;
                this.ds = null;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
            
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        string[] pSzBillNumber = new string[] { p_szBillNumber, "_", this.getRandomNumber(), "_", now.ToString("yyyyMMddHHmmssms") };
        return string.Concat(pSzBillNumber);
    }

    private string getRandomNumber()
    {
        return (new Random()).Next(1, 10000).ToString();
    }

    private void gv_rowcreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Style.Add("page-break-before", "always");
        }
        e.Row.Cells[0].Width = Convert.ToInt32(85);
        e.Row.Cells[1].Width = Convert.ToInt32(171);
        e.Row.Cells[2].Width = Convert.ToInt32(85);
        e.Row.Cells[3].Width = Convert.ToInt32(171);
        e.Row.Cells[4].Width = Convert.ToInt32(171);
        e.Row.Cells[5].Width = Convert.ToInt32(68);
        e.Row.Cells[6].Width = Convert.ToInt32(42);
        e.Row.Cells[7].Width = Convert.ToInt32(171);
    }

    private void gv_rowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Style.Add("page-break-before", "always");
        }
        e.Row.Cells[0].Width = Convert.ToInt32(427);
        e.Row.Cells[1].Width = Convert.ToInt32(427);
        e.Row.Cells[2].Width = Convert.ToInt32(427);
        e.Row.Cells[3].Width = Convert.ToInt32(427);
        e.Row.Cells[4].Width = Convert.ToInt32(427);
    }

    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!base.IsPostBack)
            {
                this.ds = new DataSet();
                this.CompanyID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.GetData();
                this.CreateReport();
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
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_GetTreatment.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void TotalAmount()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        this.dtTCount = new DataTable();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_TOTAL_RECORDS_FOR_VISIT_REPORT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", this.CompanyID.ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_location_id", base.Request.QueryString["LocationId"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_doctor_id", base.Request.QueryString["DoctorId"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@dt_from_event_date", base.Request.QueryString["FromDate"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@dt_to_event_date", base.Request.QueryString["ToDate"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", base.Request.QueryString["Speciality"].ToString());
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet);
                this.dtTCount = dataSet.Tables[0];
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
            
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void TotalRecords()
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        this.dtTRecords = new DataTable();
        try
        {
            try
            {
                this.sqlCon.Open();
                this.sqlCmd = new SqlCommand("SP_GET_VISIT_REPORT_PER_PATIENT", this.sqlCon)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };
                this.sqlCmd.Parameters.AddWithValue("@sz_company_id", this.CompanyID.ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_location_id", base.Request.QueryString["LocationId"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_doctor_id", base.Request.QueryString["DoctorId"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@dt_from_event_date", base.Request.QueryString["FromDate"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@dt_to_event_date", base.Request.QueryString["ToDate"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", base.Request.QueryString["Speciality"].ToString());
                this.sqlCmd.Parameters.AddWithValue("@I_START_INDEX", "1");
                this.sqlCmd.Parameters.AddWithValue("@I_END_INDEX", "100000");
                this.sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
                this.sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", "");
                (new SqlDataAdapter(this.sqlCmd)).Fill(dataSet);
                this.dtTRecords = dataSet.Tables[0];
                this.BindGridCaseWise(this.dtTRecords);
            }
            catch (SqlException ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
            }
        }
        finally
        {
            if (this.sqlCon.State == ConnectionState.Open)
            {
                this.sqlCon.Close();
            }
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
}