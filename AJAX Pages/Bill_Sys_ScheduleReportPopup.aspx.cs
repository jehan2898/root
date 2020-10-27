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
using System.Data.SqlClient;
using gb.web.utils;
using XGridView;
using System.IO;
using System.Data.OleDb;
using DevExpress.Web;

public partial class AJAX_Pages_Bill_Sys_ScheduleReportPopup : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        ddlNoShowDate.Attributes.Add("onChange", "javascript:SetNoShowDate();");
        this.ddlInitialRevalDate.Attributes.Add("onChange", "javascript:SetInitialRevalDate();");
        string szvisitdate = Request.QueryString["visitdate"].ToString();
        btn_Unseensearch.Attributes.Add("onclick", "return ValidateSearch();");
        btnInitialRevalSearch.Attributes.Add("onclick", "return Vlidate();");
        txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

        this.con.SourceGrid = this.grdUnseenReport;
        this.txtSearchBox.SourceGrid = this.grdUnseenReport;
        this.grdUnseenReport.Page = this.Page;
        this.grdUnseenReport.PageNumberList = this.con;

        if (!IsPostBack)
        {
            txtFromDate.Text = szvisitdate;
            txtToDate.Text = szvisitdate;
            //txtToDateRange.Text = szNoShowDate;
            //txtFromDateRange.Text = szNoShowDate;         
            extddlDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            carTabPage.Visible = false;
         //   this.extddlNoShowspeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlNoShowProvider.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlNoShowDoctor.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.ajAutoIns.ContextKey = this.txtCompanyId.Text;
            this.ajAutoOffice.ContextKey = this.txtCompanyId.Text;
            extddlCaseType.Flag_ID = txtCompanyId.Text;

            extddlCaseStatus.Flag_ID = txtCompanyId.Text;
            string caseSatusId = new CaseDetailsBO().GetCaseSatusId(this.txtCompanyId.Text);
            this.extddlCaseStatus.Text = caseSatusId;
            lblCnt.Text = "0";
            BindMissingSpecialty();
        }

    }
    protected void btnsearch_OnClick(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            carTabPage.Visible = true;
            Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            DataSet dsVisits = new DataSet();
            dsVisits = objGetVisits.GetDoctorSpecialtyVisits(txtCompanyId.Text, txtFromDate.Text, txtToDate.Text, extddlSpeciality.Text, extddlDoctor.Text, "DOC");
            grdDoctorVisits.DataSource = dsVisits;
            grdDoctorVisits.DataBind();
            Session["DoctorAllVisits"] = dsVisits;
            carTabPage.ActiveTabIndex = 0;
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
    protected void carTabPage_ActiveTabChanged(object source, DevExpress.Web.TabControlEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {

            carTabPage.Visible = true;
            int iActiveIndex = carTabPage.ActiveTabIndex;
            if (iActiveIndex == 1)
            {
                Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
                DataSet dsSpecVisits = new DataSet();
                dsSpecVisits = objGetVisits.GetDoctorSpecialtyVisits(txtCompanyId.Text, txtFromDate.Text, txtToDate.Text, extddlSpeciality.Text, extddlDoctor.Text, "PROC");
                grdSpecVisits.DataSource = dsSpecVisits;
                grdSpecVisits.DataBind();
                Session["SpecAllVisits"] = dsSpecVisits;
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
    protected void btnXlsExport1_Click(object sender, EventArgs e)
    {
        carTabPage.Visible = true;
        DataSet ds = (DataSet)Session["DoctorAllVisits"];
        grdDoctorVisits.DataSource = ds;
        grdDoctorVisits.DataBind();
        grdExportDoctorallvisits.FileName = "Doctor_Vist_Report(" + DateTime.Now.ToString("ddMMMMyyyy") + ")";
        grdExportDoctorallvisits.WriteXlsToResponse();
    }
    protected void btnXlsExportSpec_Click(object sender, EventArgs e)
    {
        carTabPage.Visible = true;
        DataSet ds = (DataSet)Session["SpecAllVisits"];
        grdSpecVisits.DataSource = ds;
        grdSpecVisits.DataBind();
        grdExportSpecialtylvisits.FileName = "Speciality_Vist_Report(" + DateTime.Now.ToString("ddMMMMyyyy") + ")";
        grdExportSpecialtylvisits.WriteXlsToResponse();
    }
    //public void GetData()
    //{
    //    string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
    //    using (Utils utility = new Utils())
    //    {
    //        utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
    //    }

    //    try
    //    {
    //        ArrayList aList = new ArrayList();
    //        aList.Add(rblNoshow.SelectedValue.ToString());
    //        aList.Add(txtCompanyId.Text);          
    //        aList.Add(txtFromDateRange.Text); 
    //        aList.Add(txtToDateRange.Text);
    //        aList.Add(extddlNoShowProvider.Text);
    //        aList.Add(extddlNoShowDoctor.Text);
    //        Bill_Sys_Event_BO objBill_sys_report = new Bill_Sys_Event_BO();
    //        DataSet dsValues = new DataSet();
    //        dsValues = objBill_sys_report.GetVisitByType(aList);
    //        grdNoShow.DataSource = dsValues;
    //        grdNoShow.DataBind();
    //        StringBuilder bh = new StringBuilder();
    //        bh.Append("<table border='1' width='100%'> <tr>");
    //        for (int j = 0; j < dsValues.Tables[1].Rows.Count; j++)
    //        {
    //            bh.Append("<td style='width: 30px;'><b>" + dsValues.Tables[1].Rows[j][0].ToString() + "</b></td>");
    //        }
    //        bh.Append(" </tr> </table>");
    //        GridViewDataColumn m = (GridViewDataColumn)this.grdNoShow.Columns[8];
    //        HtmlGenericControl lblh = (HtmlGenericControl)this.grdNoShow.FindHeaderTemplateControl(m, "lblHeader");
    //         if (lblh != null)
    //        {
    //            lblh.InnerHtml = bh.ToString();
    //        }

    //        for (int i = 0; i < grdNoShow.VisibleRowCount; i++)
    //        {
    //            GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdNoShow.Columns[8];
    //            HtmlGenericControl lbl = (HtmlGenericControl)this.grdNoShow.FindRowCellTemplateControl(i, gridViewDataColumn, "lblNoShow");
    //          string caseid=  this.grdNoShow.GetRowValues(i, new string[] { "SZ_case_id" }).ToString();
    //            StringBuilder builder = new StringBuilder();
    //            builder.Append("  <table border='1' width='100%' ><tr>");
    //            //for (int j = 0; j < dsValues.Tables[1].Rows.Count; j++)
    //            //{
    //            //    builder.Append("<td>" + dsValues.Tables[1].Rows[j][0].ToString() + "</td>");
    //            //}
                
    //            builder.Append("</tr><tr>");
    //            for (int j = 0; j < dsValues.Tables[1].Rows.Count; j++)
    //            {

    //                DataRow[] dr = dsValues.Tables[2].Select("sz_case_id= '"+ caseid + "' and  sz_procedure_group='" + dsValues.Tables[1].Rows[j][0].ToString() + "'");
    //                if (dr != null)
    //                {
    //                    if (dr.Length > 0)
    //                    {
    //                        builder.Append("<td style='width: 30px;'>" + dr[0]["COUNT"].ToString() + "</td>");
    //                    }
    //                    else
    //                    {
    //                        DataRow[] dr1 = dsValues.Tables[3].Select("sz_case_id= '" + caseid + "' and  sz_procedure_group='" + dsValues.Tables[1].Rows[j][0].ToString() + "'");
    //                        if (dr1 == null )
    //                        {
    //                            builder.Append("<td bgcolor='gray' style='width: 30px;'>0 </td>");
    //                        } else
    //                        {
    //                            if (dr1.Length > 0)
    //                            {
    //                                if (dr1[0]["COUNT"].ToString() == "0")
    //                                {
    //                                    builder.Append("<td bgcolor='gray' style='width: 30px;'> </td>");
    //                                }
    //                                else
    //                                {
    //                                    builder.Append("<td style='width: 30px;'>0</td>");
    //                                }
    //                            }
    //                            else
    //                            {
    //                                builder.Append("<td bgcolor='gray' style='width: 30px;'> </td>");
    //                            }
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    builder.Append("<td style='width: 30px;'>0</td>");
    //                }
    //            }
    //            builder.Append("</tr> </table>");
    //            lbl.InnerHtml = builder.ToString();
               
    //        }
    //        ViewState["Dataset"] = dsValues;
    //    }
    //    catch (Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        using (Utils utility = new Utils())
    //        {
    //            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
    //        }
    //        string str2 = "Error Request=" + id + ".Please share with Technical support.";
    //        base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

    //    }
    //    //Method End
    //    using (Utils utility = new Utils())
    //    {
    //        utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
    //    }

    //}
    public void GetData1()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ArrayList aList = new ArrayList();
            aList.Add(rblNoshow.SelectedValue.ToString());
            aList.Add(txtCompanyId.Text);
            aList.Add(txtFromDateRange.Text);
            aList.Add(txtToDateRange.Text);
            aList.Add(extddlNoShowProvider.Text);
            aList.Add(extddlNoShowDoctor.Text);
            Bill_Sys_Event_BO objBill_sys_report = new Bill_Sys_Event_BO();
            DataSet ds = new DataSet();
            ds = objBill_sys_report.GetVisitByType(aList);
            //DataSet ds = (DataSet)ViewState["Dataset"];
            DataTable dt = new DataTable();
            dt.Columns.Add("Case #");
            dt.Columns.Add("Patient Name");
            dt.Columns.Add("DOA");
            dt.Columns.Add("Date Of First Visit");
            dt.Columns.Add("Date Of Last Visit");
            dt.Columns.Add("Case Type");
            dt.Columns.Add("Carrier");

            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dt.Columns.Add(ds.Tables[1].Rows[i][0].ToString());
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Case #"] = ds.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                //
                dr["Patient Name"] = " <a target='_self' href='#' onclick=\"showPateintFrame('" + ds.Tables[0].Rows[i]["SZ_case_id"].ToString() + "')\">" + ds.Tables[0].Rows[i]["PatientName"].ToString() + "</a>";
                dr["DOA"] = ds.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                dr["Date Of First Visit"] = ds.Tables[0].Rows[i]["FirstVisitDate"].ToString();
                dr["Date Of Last Visit"] = ds.Tables[0].Rows[i]["LastVisitDate"].ToString();
                dr["Case Type"] = ds.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
                dr["Carrier"] = ds.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString().Replace('\'', ' ');

                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                {
                    string specialty = ds.Tables[1].Rows[j][0].ToString();
                    DataRow[] dr2 = ds.Tables[2].Select("sz_case_id= '" + ds.Tables[0].Rows[i]["SZ_case_id"].ToString() + "' and  sz_procedure_group='" + specialty + "'");
                    if (dr2 != null)
                    {
                        if (dr2.Length > 0)
                        {
                            dr[specialty] = dr2[0]["COUNT"].ToString();
                        }
                        else
                        {
                            DataRow[] dr1 = ds.Tables[3].Select("sz_case_id= '" + ds.Tables[0].Rows[i]["SZ_case_id"].ToString() + "' and  sz_procedure_group='" + specialty + "'");
                            if (dr1 == null)
                            {
                                dr[specialty] = "0";
                            }
                            else
                            {
                                if (dr1.Length > 0)
                                {
                                    if (dr1[0]["COUNT"].ToString() == "0")
                                    {
                                        dr[specialty] = "<span style='bgcolor: #d3d3d3; font - weight:bold'>NA</span>";
                                    }
                                    else
                                    {
                                        dr[specialty] = "0";
                                    }
                                }
                                else
                                {
                                    dr[specialty] = "<span style='bgcolor: #d3d3d3; font - weight:bold'>NA</span>";
                                }
                            }
                        }
                    }
                    else
                    {
                        dr[specialty] = "<span style='bgcolor: #d3d3d3; font - weight:bold'>NA</span>";
                    }
                }
                dt.Rows.Add(dr);
            }
            grdNoShow.DataSource = dt;
            grdNoShow.DataBind();
            

            ViewState["Dataset"] = ds;
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
    protected void BtnSearchVisit_Click(object sender, EventArgs e)
    {
        GetData1(); 
    }
    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        //GetData();
        //string sFileName = null;
        //string sFile = GetExportPhysicalPath(sUserName, gb.web.utils.DownloadFilesExportTypes.UNPROCESS_BILLS, out sFileName);
        //System.IO.Stream stream1 = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        //grdExport.WriteXlsx(stream1);
        //stream1.Close();
        //ArrayList list = new ArrayList();
        //list.Add(gb.web.utils.DownloadFilesUtils.GetExportRelativeURL(sFileName));
        //Session["Download_Files"] = list;
        DataSet ds = (DataSet)ViewState["Dataset"];
        DataTable dt = new DataTable();
        dt.Columns.Add("Case #");
        dt.Columns.Add("Patient Name");
        dt.Columns.Add("DOA");
        dt.Columns.Add("Date Of First Visit");
        dt.Columns.Add("Date Of Last Visit");
        dt.Columns.Add("Case Type");
        dt.Columns.Add("Carrier");

        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        {
            dt.Columns.Add(ds.Tables[1].Rows[i][0].ToString());
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Case #"] = ds.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
            dr["Patient Name"] = ds.Tables[0].Rows[i]["PatientName"].ToString();
            dr["DOA"] = ds.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
            dr["Date Of First Visit"] = ds.Tables[0].Rows[i]["FirstVisitDate"].ToString();
            dr["Date Of Last Visit"] = ds.Tables[0].Rows[i]["LastVisitDate"].ToString();
            dr["Case Type"] = ds.Tables[0].Rows[i]["SZ_CASE_TYPE_NAME"].ToString();
            dr["Carrier"] = ds.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString().Replace('\'',' ');

            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
            {
              string specialty= ds.Tables[1].Rows[j][0].ToString();
                DataRow[] dr2 = ds.Tables[2].Select("sz_case_id= '" + ds.Tables[0].Rows[i]["SZ_case_id"].ToString() + "' and  sz_procedure_group='" + specialty + "'");
                if (dr2 != null)
                {
                    if (dr2.Length > 0)
                    {
                        dr[specialty] = dr2[0]["COUNT"].ToString() ;
                    }
                    else
                    {
                        DataRow[] dr1 = ds.Tables[3].Select("sz_case_id= '" + ds.Tables[0].Rows[i]["SZ_case_id"].ToString() + "' and  sz_procedure_group='" + specialty + "'");
                        if (dr1 == null)
                        {
                            dr[specialty] ="0";
                        }
                        else
                        {
                            if (dr1.Length > 0)
                            {
                                if (dr1[0]["COUNT"].ToString() == "0")
                                {
                                    dr[specialty] = "-";
                                }
                                else
                                {
                                    dr[specialty] = "0";
                                }
                            }
                            else
                            {
                                dr[specialty] = "-";
                            }
                        }
                    }
                }
                else
                {
                    dr[specialty] = "-";
                }
            }
            dt.Rows.Add(dr);
        }

        string str = ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString();
        string str2 = this.lfnFileName(rblNoshow.SelectedItem.Text) + ".xls";
        File.Copy(ConfigurationSettings.AppSettings["ExportToExcelPath"].ToString(), (str + str2).Trim());
        GenerateXL(dt, str+ str2);

        ArrayList list = new ArrayList();
        list.Add(gb.web.utils.DownloadFilesUtils.GetExportRelativeURL(str2));
        Session["Download_Files"] = list;



    }

    public string GenerateXL(DataTable dt, string file)
    {
     //   DataColumn column = null;
        OleDbConnection oleDbConnection = new OleDbConnection(string.Concat("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=", file, ";Extended Properties='Excel 12.0;HDR=Yes;'"));
        try
        {
            try
            {
                oleDbConnection.Open();
                StringBuilder stringBuilder = new StringBuilder();
                int num = 0;
                string str = "";
                foreach (DataColumn column in dt.Columns)
                {
                    if (num != 0)
                    {
                        stringBuilder.Append(string.Concat(", [", column.ColumnName.Replace(" ", "_").Replace("#", "No"), "]"));
                        str = string.Concat(str, ", ", column.ColumnName.Replace(" ", "_").Replace("#", "No"), " nvarchar(50)");
                    }
                    else
                    {
                        stringBuilder.Append(string.Concat("[", column.ColumnName.Replace(" ", "_").Replace("#", "No"), "]"));
                        str = string.Concat(column.ColumnName.Replace(" ", "_").Replace("#", "No"), " nvarchar(50)");
                    }
                    num++;
                }
                str = str.Replace('-', '_');
                OleDbCommand oleDbCommand = new OleDbCommand()
                {
                    Connection = oleDbConnection,
                    CommandText = "drop table [Sheet1$]"
                };
                oleDbCommand.ExecuteNonQuery();
                oleDbCommand.CommandText = string.Concat("create table [Sheet1$](", str, ") ");
                oleDbCommand.ExecuteNonQuery();
                foreach (DataRow row in dt.Rows)
                {
                    num = 0;
                    StringBuilder stringBuilder1 = new StringBuilder();
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        if (num == 0)
                        {
                            if (!(row[dataColumn.ColumnName] is long))
                            {
                                stringBuilder1.Append(string.Concat("'", row[dataColumn.ColumnName], "'"));
                            }
                            else
                            {
                                stringBuilder1.Append(string.Concat("'", row[dataColumn.ColumnName], "'"));
                            }
                        }
                        else if (!(row[dataColumn.ColumnName] is long))
                        {
                            stringBuilder1.Append(string.Concat(",'", row[dataColumn.ColumnName], "'"));
                        }
                        else
                        {
                            stringBuilder1.Append(string.Concat(",'", row[dataColumn.ColumnName], "'"));
                        }
                        num++;
                    }
                    OleDbCommand oleDbCommand1 = new OleDbCommand()
                    {
                        CommandType = CommandType.Text,
                        CommandTimeout = 0
                    };
                    stringBuilder = stringBuilder.Replace('-', '_');
                    
                    object[] objArray = new object[] { "INSERT INTO [Sheet1$] (", stringBuilder, ")values(", stringBuilder1, ")" };
                    oleDbCommand1.CommandText = string.Concat(objArray);
                    oleDbCommand1.Connection = oleDbConnection;
                    oleDbCommand1.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        finally
        {
            oleDbConnection.Close();
        }
        return file;
    }

    private string lfnFileName(string type)
    {
        Random random = new Random();
        DateTime now = DateTime.Now;
        
        return ("Visits (" + type + ")_" + random.Next(1, 0x2710).ToString() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }
    public static string GetExportPhysicalPath(string sUserName, DownloadFilesExportTypes type, out string sFileName)
    {
        string sRoot = getUploadDocumentPhysicalPath();
        string sFolder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();
        sFileName = getFileName(sUserName);
        return sRoot + sFolder + sFileName;
    }
    private static string getUploadDocumentPhysicalPath()
    {
        string str = "";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", connection).ExecuteReader();
            while (reader.Read())
            {
                str = reader["parametervalue"].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str; 
    }
    private static string getFileName(string sUserName)
    {
        return sUserName + "Bill_Sys_ScheduleReportPopup" + DateTime.Now.ToString("MMddyyyyss") + ".xls";
    }
    public void Bind_Grid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            StringBuilder specialtyID = new StringBuilder();
            for (int i = 0; i < grdSpeciality.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdSpeciality.Columns[0];
                CheckBox chk = (CheckBox)grdSpeciality.FindRowCellTemplateControl(i, c, "chkall1");

                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        if (specialtyID.Length <= 0)
                        {
                            specialtyID.Append("'" + grdSpeciality.GetRowValues(i, "code").ToString() + "'");
                        }
                        else
                        {
                            specialtyID.Append(",'" + grdSpeciality.GetRowValues(i, "code").ToString() + "'");
                        }
                       
                    }
                }
            }
            txtSpeciality.Text = specialtyID.ToString();
            //carTabPage.Visible = true;
            //Bill_Sys_Event_BO objGetVisits = new Bill_Sys_Event_BO();
            //DataSet dsVisits = new DataSet();
            //dsVisits = objGetVisits.GetUnseenReport(txtCompanyId.Text, txtdays.Text);
            //grdUnseenReport.DataSource = dsVisits;
            //grdUnseenReport.DataBind();
            //ViewState["griddata"] = dsVisits;
            //Session["DoctorAllVisits"] = dsVisits;
            //carTabPage.ActiveTabIndex = 0;
            txtCount.Text = txtdays.Text;
            txtUnseenDate.Text = txtdate.Text;
            grdUnseenReport.XGridBindSearch();
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Bind_Grid();
    }
    //protected void ASPxCallback2_Callback(object sender, CallbackEventArgs e)
    //{
    //    string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
    //    //Bind_Grid();
    //    string sFileName = null;
    //    string sFile = GetExportPhysicalPath1(sUserName, gb.web.utils.DownloadFilesExportTypes.UNPROCESS_BILLS, out sFileName);
    //    System.IO.Stream stream1 = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
    //    grdUnseenExport1.WriteXlsx(stream1);
    //    stream1.Close();
    //    ArrayList list = new ArrayList();
    //    list.Add(gb.web.utils.DownloadFilesUtils.GetExportRelativeURL(sFileName));
    //    Session["Download_Files"] = list;
    //}
    public static string GetExportPhysicalPath1(string sUserName, DownloadFilesExportTypes type, out string sFileName)
    {
        string sRoot = getUploadDocumentPhysicalPath1();
        string sFolder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();
        sFileName = getFileName1(sUserName);
        return sRoot + sFolder + sFileName;
    }
    private static string getUploadDocumentPhysicalPath1()
    {
        string str = "";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", connection).ExecuteReader();
            while (reader.Read())
            {
                str = reader["parametervalue"].ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str;
    }
    private static string getFileName1(string sUserName)
    {
        return sUserName + "Bill_Sys_ScheduleReportPopup" + DateTime.Now.ToString("MMddyyyyss") + ".xls";
    }
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, base.GetType(), "mm", string.Concat("window.location.href ='", ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString(), this.grdUnseenReport.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()), "';"), true);
    }
    private void BindMissingSpecialty()
    {
        DataSet ds = new DataSet();
        ds = loadMissingSpeciality(this.txtCompanyId.Text);
        //ASPxListBox lstMissingSpecialty = (ASPxListBox)this.grdSpeciality.FindControl("lstMissingSpecialty");
        //lstMissingSpecialty.ValueField = "CODE";
        //lstMissingSpecialty.TextField = "DESCRIPTION";
        grdInitialRevalSpeciality.DataSource = ds;
        grdSpeciality.DataSource = ds;
        grdSpeciality.DataBind();
        grdInitialRevalSpeciality.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All ---", "NA");
        //lstMissingSpecialty.Items.Insert(0, Item);

        //  lstMissingSpecialty.SelectAll();
    }
    public DataSet loadMissingSpeciality(string companyid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_MST_PROCEDURE_GROUP", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", "GET_PROCEDURE_GROUP_LIST");
            comm.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
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
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    // code fro initial Reval
    protected void btnInitialReevalSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            GetInitialRevalData();
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
    public void GetInitialRevalData()
    {

        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            DataSet dsValues = new DataSet();
            for (int i = 0; i < grdInitialRevalSpeciality.VisibleRowCount; i++)
            {
                GridViewDataColumn c = (GridViewDataColumn)grdInitialRevalSpeciality.Columns[0];
                CheckBox chk = (CheckBox)grdInitialRevalSpeciality.FindRowCellTemplateControl(i, c, "CheckBox2");
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        //SpecialityPDFFill obj = new SpecialityPDFFill();
                        //obj.SZ_SPECIALITY_NAME = grdSpeciality.GetRowValues(i, "description").ToString();
                        //obj.SZ_SPECIALITY_CODE = grdSpeciality.GetRowValues(i, "code").ToString();
                        ArrayList aList = new ArrayList();
                        aList.Add(grdInitialRevalSpeciality.GetRowValues(i, "code").ToString());
                        aList.Add(txtCompanyId.Text);
                        if (txtINS1.Text == "")
                        {
                            hdnInsurace.Value = "";
                        }
                        aList.Add(hdnInsurace.Value);
                        if (txtOffice1.Text == "")
                        {
                            hdnOffice.Value = "";
                        }
                        aList.Add(hdnOffice.Value);
                        aList.Add(extddlCaseType.Text);
                        aList.Add(txtupdateFromDate.Text);
                        aList.Add(txtupdateToDate.Text);
                        aList.Add(rblInitialReeval.SelectedValue.ToString());
                        aList.Add(extddlCaseStatus.Text);
                        Bill_sys_report objBill_sys_report = new Bill_sys_report();
                        DataSet temp = new DataSet();
                        temp = objBill_sys_report.geIntialRevalCases(aList);
                        if (dsValues.Tables.Count <= 0)
                        {
                            if (temp.Tables.Count > 0 && temp.Tables[0].Rows.Count > 0)
                            {
                                dsValues = temp;
                            }

                        }
                        else
                        {
                            if (temp.Tables.Count > 0 && temp.Tables[0].Rows.Count > 0)
                            {
                                dsValues.Tables[0].Merge(temp.Tables[0], true);
                            }


                        }
                    }
                }
            }
                if (dsValues.Tables.Count > 0 && dsValues.Tables[0].Rows.Count > 0)
                {
                    lblCnt.Text = dsValues.Tables[0].Rows.Count.ToString();
                    grdVisits.DataSource = dsValues;
                    grdVisits.DataBind();
                    grdExPort.DataSource = dsValues;
                    grdExPort.DataBind();
                    Session["Dataset"] = dsValues;
                }else

                {
                    lblCnt.Text = "0";
                    grdVisits.DataSource = null;
                    grdVisits.DataBind();
                    grdExPort.DataSource = null;
                    grdExPort.DataBind();
                    Session["Dataset"] = dsValues;
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
    protected void btnInitialReevalexport_Click(object sender, EventArgs e)
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
            for (int icount = 0; icount < grdExPort.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 1; i < grdExPort.Columns.Count; i++)
                    {
                        if (grdExPort.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdExPort.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 1; j < grdExPort.Columns.Count; j++)
                {
                    if (grdExPort.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdExPort.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("Invoice_Report") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            //   Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename + "');", true);


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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["Dataset"];
            dv = ds.Tables[0].DefaultView;
            if (e.CommandName.ToString() == "case")
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
            else if (e.CommandName.ToString() == "Name")
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
            else if (e.CommandName.ToString() == "Phone")
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
            else if (e.CommandName.ToString() == "DOA")
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

            else if (e.CommandName.ToString() == "Provider")
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
            else if (e.CommandName.ToString() == "LastDate")
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
            if (e.CommandName.ToString() == "CaseType")
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
            else if (e.CommandName.ToString() == "Doctor")
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
            else if (e.CommandName.ToString() == "Insurance")
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

            else if (e.CommandName.ToString() == "CPhone")
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
            else if (e.CommandName.ToString() == "WPhone")
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
            grdVisits.DataSource = dv;
            grdExPort.DataSource = dv;

            grdVisits.DataBind();
            grdExPort.DataBind();

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




    protected void grdVisits_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        grdVisits.SelectedIndex = e.NewPageIndex;
        grdVisits.DataBind();
    }
}
