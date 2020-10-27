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
using log4net;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using CustomControls.ContextMenuScope;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using Reminders;

public partial class Agent_Bill_Sys_Agent_SearchCase : PageBase
{
    private string szExcelFileNamePrefix = null;
    #region PageLoad
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;
    DataTable dt;
    private static ILog log = LogManager.GetLogger("PDFValueReplacement");

    protected void Page_Load(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        log.Debug("Search case page_load");
        String scriptFunc = "";
        String scriptFunc2 = "";
        //Page.Title = (String)GetLocalResourceObject("page_title");
        utxtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        scriptFunc = "<script language=JavaScript> " + "function autoComplete (field, select, property, forcematch) {" + "var found = false;" + "for (var i = 0; i < select.options.length; i++) {" + "if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) {" + "		found=true; break;" + "}" + "}" + "if (found) { " + "select.selectedIndex = i; " + "}else {" + "select.selectedIndex = -1;" + "}" + "if (field.createTextRange) {" + "if (forcematch && !found) {" + "field.value=field.value.substring(0,field.value.length-1); " + "return;" + "}" + "var cursorKeys ='8;46;37;38;39;40;33;34;35;36;45;';" + "if (cursorKeys.indexOf(event.keyCode+';') == -1) {" + "var r1 = field.createTextRange();" + "var oldValue = r1.text;" + "var newValue = found ? select.options[i][property] : oldValue;" + "if (newValue != field.value) {" + "field.value = newValue;" + "var rNew = field.createTextRange();" + "rNew.moveStart('character', oldValue.length) ;" + "rNew.select();" + "}" + "}" + "}" + "} </script>";
        RegisterClientScriptBlock("ClientScript", scriptFunc);
        ajAutoIns.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        btnSoftDelete.Attributes.Add("onclick", "return Validate()");
        //ValidateExportBill
        btnExportToExcel.Attributes.Add("onclick", "return ValidateExportBill()");
        // txtPatientName.Attributes.Add("onKeyUp", "autoComplete(this,this.form.ctl00_ContentPlaceHolder1_extddlPatient,'text',true);");
        // txtInsuranceCompany.Attributes.Add("onKeyUp", "autoComplete(this,this.form.ctl00_ContentPlaceHolder1_extddlInsurance,'text',true);");
        //if (Session["CASE_LIST_GO_BUTTON"] != null)
        //{
        //    utxtCaseNo.Text = Session["CASE_LIST_GO_BUTTON"].ToString();
        //    Session["CASE_LIST_GO_BUTTON"] = null;
        //}


        if (!IsPostBack)
        {
            try
            {
                DataSet dsBit41 = new System.Data.DataSet();
                Bill_Sys_ProcedureCode_BO objPBO_1 = new Bill_Sys_ProcedureCode_BO();
                dsBit41 = objPBO_1.Get_Sys_Key("SS00041", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if (dsBit41.Tables.Count > 0 && dsBit41.Tables[0].Rows.Count > 0)
                {
                    string szBitVal1 = dsBit41.Tables[0].Rows[0][0].ToString();
                    if (szBitVal1 == "0")
                    {
                        Session["SendPatientToDoctor"] = false;

                    }
                    else
                    {
                        Session["SendPatientToDoctor"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Session["SendPatientToDoctor"] = false;
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                using (Utils utility = new Utils())
                {
                    utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                }
                string str2 = "Error Request=" + id + ".Please share with Technical support.";
                base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

            }
        }

        StringBuilder szExportoExcelColumname = new StringBuilder();
        StringBuilder szExportoExcelField = new StringBuilder();
        szExportoExcelColumname.Append("Case #,");
        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        {
            szExportoExcelField.Append("SZ_RECASE_NO,");
            //grdPatientList.Columns[25].Visible = true;
        }
        else
        {
            szExportoExcelField.Append("SZ_CASE_NO,");
        }
        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
        {
            szExportoExcelColumname.Append("Chart No,");
            szExportoExcelField.Append("SZ_CHART_NO,");
        }

        if ((((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_LOCATION) != "1")
        {
            extddlLocation.Visible = false;
            lblLocation.Visible = false;
        }
        else
        {
            extddlLocation.Visible = true;
            lblLocation.Visible = true;
        }
        szExportoExcelColumname.Append("Patient Name,Accident Date,Open Date,Insurance Name,Claim Number,Policy Number,Case Type,Case Status");
        szExportoExcelField.Append("SZ_PATIENT_NAME,DT_DATE_OF_ACCIDENT,DT_DATE_OPEN,SZ_INSURANCE_NAME,SZ_CLAIM_NUMBER,SZ_POLICY_NUMBER,SZ_CASE_TYPE,SZ_STATUS_NAME");
        if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("admin"))
        {
            szExportoExcelColumname.Append(",Total,Paid,Pending");
            szExportoExcelField.Append(",Total,Paid,Pending");

        }
        else
        {
            szExportoExcelColumname.Append(",Patient Phone");
            szExportoExcelField.Append(",SZ_PATIENT_PHONE");
        }



        Bill_Sys_LoginBO _bill_Sys_LoginBO = new Bill_Sys_LoginBO();
        string str = _bill_Sys_LoginBO.getconfiguration(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (str != "" && str != null)
        {
            //grdPatientList.Columns[27].Visible = true;//change 28 to 27
            //grdPatientList.Columns[28].Visible = true;//change 29 to 28

            szExportoExcelColumname.Append(",Case Type");
            szExportoExcelField.Append(",SZ_CASE_TYPE");
            szExportoExcelColumname.Append(",Case Status");
            szExportoExcelField.Append(",SZ_STATUS_NAME");


        }
        else
        {

            //grdPatientList.Columns[27].Visible = false;//change 28 to 27
            //grdPatientList.Columns[28].Visible = false;
        }



        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION == "1")
        {
            //grdPatientList.Columns[29].Visible = true;
            szExportoExcelColumname.Append(",Patient ID");
            szExportoExcelField.Append(",SZ_PATIENT_ID_LHR");
        }
        else
        {
            //grdPatientList.Columns[29].Visible = false;

        }
        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_DATE_OF_FIRST_TREATMENT == "1")
        {
            //grdPatientList.Columns[30].Visible = true;
            szExportoExcelColumname.Append(",Date Of First Treatment");
            szExportoExcelField.Append(",DT_FIRST_TREATMENT");
        }
        else
        {
            //grdPatientList.Columns[30].Visible = false;

        }


        //grdPatientList.ExportToExcelColumnNames = szExportoExcelColumname.ToString();
        ////grdPatientList.Con = xcon;
        //grdPatientList.ExportToExcelFields = szExportoExcelField.ToString();
        //log.Debug("start Xgridbind.");
        //this.con.SourceGrid = grdPatientList;
        //this.txtSearchBox.SourceGrid = grdPatientList;
        //this.grdPatientList.Page = this.Page;
        //this.grdPatientList.PageNumberList = this.con;
        //log.Debug("End Xgridbind.");
        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
        {
            //grdPatientList.Columns[4].Visible = true;//chart no col
            txtChartNo.Visible = true;
            lblChart.Visible = true;
        }
        else
        {
            //grdPatientList.Columns[4].Visible = false;//chart no col
            txtChartNo.Visible = false;
            lblChart.Visible = false;
        }
        if (!IsPostBack)
        { //Taking Login Company Id
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_PROCEDURE_CODE_ON_INTEGRATION != "1")
            {
                lblpatientid.Visible = false;
                txtpatientid.Visible = false;
            }
            else
            {
                lblpatientid.Visible = true;
                txtpatientid.Visible = true;
            }
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            extddlInsurance.Visible = false;
            extddlPatient.Visible = false;
            //  utxtCompanyID.Text = txtCompanyID.Text;
            fillcontrol();

            //  extddlPatient.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            //  extddlInsurance.Flag_ID = txtCompanyID.Text.ToString();
            extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
            CaseDetailsBO _objCaseDetailsBO = new CaseDetailsBO();
            string szCaseStatausID = _objCaseDetailsBO.GetCaseSatusId(txtCompanyID.Text);
            extddlCaseStatus.Text = szCaseStatausID;
            fillcontrol();
            if (Session["CASE_LIST_GO_BUTTON"] != null)
            {
                utxtPatientName.Text = Session["CASE_LIST_GO_BUTTON"].ToString();
                Session["CASE_LIST_GO_BUTTON"] = null;
            }
            //string ConnectionString = ConfigurationSettings.AppSettings["Connection_String"].ToString();
            //SqlConnection con = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("sp_get_patientlist_agent", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);
            //cmd.Parameters.AddWithValue("@sz_user_id", ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //grdPatientList.DataSource = dt;
            //grdPatientList.DataBind();

            
              //grdPatientList.XGridBind();
            //grdPatientList.XGridBindSearch();
            log.Debug("Page_Load grdPatientList.XGridBindSearch() Completed");

            //int i = grdPatientList.Rows.Count;

            clearcontrol();

            //   ExportToExcelFields="SZ_CASE_NO,SZ_CHART_NO,SZ_PATIENT_NAME,DT_DATE_OF_ACCIDENT,DT_DATE_OPEN,SZ_INSURANCE_NAME,SZ_CLAIM_NUMBER,SZ_POLICY_NUMBER,Total,Paid,Pending" ;
            //ExportToExcelColumnNames="Case #,Chart No,Patient Name,Accident Date,Open Date,Insurance Name,Claim Number,Policy Number,Total,Paid,Pending"


        }
        // SoftDelete();

        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        {
            //grdPatientList.Columns[3].Visible = true;
            //grdPatientList.Columns[19].Visible = false;
            //grdPatientList.Columns[20].Visible = false;
            //grdPatientList.Columns[21].Visible = true;
            //grdPatientList.Columns[23].Visible = true;//doctor name column
            //grdPatientList.Columns[24].Visible = true;//office name column
            //Provider name column

        }
        else
        {
            //grdPatientList.Columns[23].Visible = false;//doctor name column
            //grdPatientList.Columns[24].Visible = false;//office name column
            //grdPatientList.Columns[2].Visible = true;
            //grdPatientList.Columns[21].Visible = false;
            //grdPatientList.Columns[22].Visible = true;//location column

        }
        string strlocation = _bill_Sys_LoginBO.getconfigurationlocation(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (strlocation != "" && strlocation != null)
        {
            //grdPatientList.Columns[22].Visible = false;
        }
        ///Check for chart no seting
        //((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE
        //txtCompanyID.Text


        //check for Total ,Paid and Pending
        if (((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ROLE_NAME.ToLower().Equals("admin"))
        {
            //grdPatientList.Columns[12].Visible = true;//total col
            //grdPatientList.Columns[13].Visible = true;//paid col
            //grdPatientList.Columns[14].Visible = true;//pending col
            //grdPatientList.Columns[6].Visible = false;//6
        }
        else
        {
            //grdPatientList.Columns[6].Visible = true;//6
            //grdPatientList.Columns[12].Visible = false;
            //grdPatientList.Columns[13].Visible = false;
            //grdPatientList.Columns[14].Visible = false;
        }
        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SHOW_PATIENT_PHONE == "1")
        {
            //grdPatientList.Columns[6].Visible = true;//6
        }

        //Check for Check in  and Check out
        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHECKINVALUE == "1")
        {
            //grdPatientList.Columns[17].Visible = true;//check in col
            //grdPatientList.Columns[18].Visible = true;//check out col
        }


        DataSet dsBit = new System.Data.DataSet();
        Bill_Sys_ProcedureCode_BO objPBO = new Bill_Sys_ProcedureCode_BO();
        dsBit = objPBO.Get_Sys_Key("SS00040", txtCompanyID.Text);
        if (dsBit.Tables.Count > 0 && dsBit.Tables[0].Rows.Count > 0)
        {
            string szBitVal = dsBit.Tables[0].Rows[0][0].ToString();
            if (szBitVal == "0")
            {
                //grdPatientList.Columns[32].Visible = false;

            }
        }
        //Check For Soft Delete Button and delete checkbox

        if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_SOFT_DELETE == "True")
        {
            //grdPatientList.Columns[33].Visible = true;//delete checkbox//change 26 to 27 then 29// 30 to 31//32 to 33 --atul
            btnSoftDelete.Visible = true;
        }
        ///for Quick Bill Entry  link 
        if (Request.QueryString["Type"] != null)
        {
            if (Request.QueryString["Type"].ToString() == "Quick" && (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NEW_BILL == "True" || ((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL == "True"))
            {
                //grdPatientList.Columns[15].Visible = true;//Bills column

                //for (int i = 0; i < grdPatientList.Rows.Count; i++)
                //{
                //    HyperLink lnkN = (HyperLink)grdPatientList.Rows[i].Cells[15].FindControl("lnkNew");
                //    HyperLink lnkV = (HyperLink)grdPatientList.Rows[i].Cells[15].FindControl("lnkView");
                //    if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_NEW_BILL == "True")
                //    {
                //        lnkN.Visible = true;
                //    }
                //    if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_VIEW_BILL == "True")
                //    {
                //        lnkV.Visible = true;
                //    }

                //}

            }
        }

        // extddlCaseType.Selected_Text = "OPEN";
        // used for  export to excel open not refresh grid
        fillcontrol();
        //grdPatientList.Columns[28].Visible = false;
        //grdPatientList.Columns[29].Visible = false;

        //SetTitle(this.Page);




        if (!IsPostBack)
        {
            if (Session["REMINDER"] != null)
            {
                Session["REMINDER"] = null;
                ReminderBO objReminder = null;
                DataSet dsReminder = null;
                DataSet addReminder = null;
                string strUserId = "";
                DateTime dtCurrent_Date;
                objReminder = new ReminderBO();
                dsReminder = new DataSet();
                strUserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
                dtCurrent_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                dsReminder = objReminder.LoadReminderDetails(strUserId, dtCurrent_Date);
                addReminder = objReminder.LoadReminderDetailsforAdd(strUserId, dtCurrent_Date, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                if (dsReminder.Tables[0].Rows.Count > 0 || dsReminder.Tables[1].Rows.Count > 0 || addReminder.Tables[0].Rows.Count > 0)
                {
                    Page.RegisterStartupScript("ss", "<script language='javascript'> ReminderPopup();</script>");
                }
            }

            //ReminderBO _ReminderBO = new ReminderBO();
            //DataSet dssetting = new DataSet();
            //dssetting = _ReminderBO.IMSetting("SS00015", txtCompanyID.Text);
            //if (dssetting.Tables[0].Rows.Count > 0)
            //{
            //    if (dssetting.Tables[0].Rows[0]["SZ_SYS_SETTING_VALUE"].ToString() == "1")
            //    {
            //        if (Session["IMDETAILS"] != null)
            //        {
            //            Session["IMDETAILS"] = null;
            //            ReminderBO objim = null;
            //            DataSet dsIM = null;
            //            objim = new ReminderBO();
            //            dsIM = new DataSet();
            //            dsIM = objim.LoadCheckimDetails(txtCompanyID.Text);
            //            if (dsIM.Tables[0].Rows.Count > 0)
            //            {
            //                Page.RegisterStartupScript("im", "<script language='javascript'> showimcheckPopup();</script>");
            //            }
            //        }
            //    }
            //}







        }



        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        SoftDelete();
    }

    #region "ExportTOExcel"
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPatientList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);


    }
    #endregion

    //soft delete functionality

    protected void btnSoftDelete_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();
        string sz_Case_id = "";

        try
        {

            //for (int i = 0; i < grdPatientList.Rows.Count; i++)
            //{
            //    CheckBox chkDelete = (CheckBox)grdPatientList.Rows[i].FindControl("chkDelete");
            //    if (chkDelete.Checked)
            //    {
            //        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
            //        {
            //            LinkButton Rlnk = (LinkButton)grdPatientList.Rows[i].Cells[3].FindControl("lnkSelectRCase");
            //            sz_Case_id = (string)Rlnk.Text.ToString();


            //        }
            //        else
            //        {
            //            LinkButton lnk = (LinkButton)grdPatientList.Rows[i].Cells[2].FindControl("lnkSelectCase");
            //            sz_Case_id = (string)lnk.Text.ToString();



            //        }
            //        _caseDetailsBO.SoftDelete(sz_Case_id, txtCompanyID.Text, true);
            //    }
            //}

            //// SearchList();
            //fillcontrol();

            //grdPatientList.XGridBindSearch();
            //log.Debug("grdPatientList.XGridBindSearch() Completed.");
            //clearcontrol();
            //SoftDelete();

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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string sz_Case_id = "";
        string parameter = "";
        CaseDetailsBO _caseDetailsBO = new CaseDetailsBO();

        //for (int i = 0; i < grdPatientList.Rows.Count; i++)
        //{
        //    CheckBox chkDelete = (CheckBox)grdPatientList.Rows[i].FindControl("chkDelete");
        //    if (chkDelete.Checked)
        //    {
        //        if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
        //        {
        //            LinkButton Rlnk = (LinkButton)grdPatientList.Rows[i].Cells[3].FindControl("lnkSelectRCase");
        //            sz_Case_id = (string)Rlnk.Text.ToString();
        //            if (parameter == "")
        //            {
        //                parameter = sz_Case_id;
        //            }
        //            else
        //            {
        //                parameter = parameter + "," + sz_Case_id;
        //            }
        //        }
        //        else
        //        {
        //            LinkButton lnk = (LinkButton)grdPatientList.Rows[i].Cells[2].FindControl("lnkSelectCase");
        //            sz_Case_id = (string)lnk.Text.ToString();
        //            if (parameter == "")
        //            {
        //                parameter = sz_Case_id;
        //            }
        //            else
        //            {
        //                parameter = parameter + "," + sz_Case_id;
        //            }
        //        }

        //    }
        //}
        //if (parameter != "")
        //{
        //    DataSet ds = new DataSet();
        //    ds = _caseDetailsBO.GetBillInfo(parameter, txtCompanyID.Text);

        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        string sz_FilePath = ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString();
        //        string fileName = lfnFileName() + ".xls";
        //        string openpath = sz_FilePath + fileName;
        //        File.Copy(ConfigurationSettings.AppSettings["ExportToExcelPath"].ToString(), openpath.Trim());

        //        XGridView.XGridViewControl _obj = new XGridView.XGridViewControl();
        //        string sz_GeneratedFile = _obj.GenerateXL(ds.Tables[0], sz_FilePath + fileName);
        //        string fileshow = ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + fileName;

        //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href =' " + fileshow + "'", true);
        //    }
        //    else
        //        ScriptManager.RegisterStartupScript(this, GetType(), "ss", "alert('No Bills are available to Export to Excel sheet!');", true);

        //}

    }

    private string lfnFileName()
    {
        System.Random objRandom = new Random();
        DateTime currentDate;
        currentDate = DateTime.Now;
        if (szExcelFileNamePrefix == null)
        {
            szExcelFileNamePrefix = "excel";
        }
        return szExcelFileNamePrefix + "_" + objRandom.Next(1, 10000).ToString() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            //fillcontrol(); //check serarch parameter
            //grdPatientList.XGridBindSearch();
            //clearcontrol();//clear all serach parameter for xml
            //SoftDelete();
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

    protected void btnClear_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            clearcontrol();
            setDefault();
            //grdPatientList.XGridBindSearch();
            SoftDelete();
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
    // fill xml file control
    public void fillcontrol()
    {
        log.Debug("Start FillControl");
        utxtCompanyID.Text = txtCompanyID.Text;
        utxtDateofAccident.Text = txtDateofAccident.Text;
        utxtClaimNumber.Text = txtClaimNumber.Text;
        utxtDateofBirth.Text = txtDateofBirth.Text;
        utxtCaseType.Text = extddlCaseType.Text;
        utxtCaseStatus.Text = extddlCaseStatus.Text;
        if (extddlPatient.Text == "" || extddlPatient.Text == "NA")
        {
            utxtPatientName.Text = txtPatientName.Text;
        }
        else
        {
            utxtPatientName.Text = txtPatientName.Text;
        }
        utxtInsuranceName.Text = txtInsuranceCompany.Text;
        utxtLocation.Text = extddlLocation.Text;
        utxtSSNNo.Text = txtSSNNo.Text;
        utxtCaseNo.Text = txtCaseNo.Text;
        utxtChartNo.Text = txtChartNo.Text;
        utxtUserId.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        log.Debug("End FillControl");
    }

    //clear xml file control
    public void clearcontrol()
    {

        utxtDateofAccident.Text = "";
        utxtClaimNumber.Text = "";
        utxtDateofBirth.Text = "";
        utxtCaseType.Text = "";
        utxtCaseStatus.Text = "";
        utxtPatientName.Text = "";
        utxtInsuranceName.Text = "";
        utxtLocation.Text = "";
        utxtSSNNo.Text = "";
        utxtChartNo.Text = "";
        utxtCaseNo.Text = "";
    }
    // set default value to search paramerter
    public void setDefault()
    {
        //  extddlPatient.Text  ="NA";
        //  extddlCaseStatus.Text  = "NA";
        //  extddlCaseType.Text  = "NA";
        //  extddlInsurance.Text  = "NA";
        //  extddlLocation.Text  = "NA";
        //  txtDateofBirth.Text = "";
        //  txtDateofAccident.Text = "";
        //  txtPatientName.Text = "";
        //  txtInsuranceCompany.Text = "";
        ////  txtClaimNumber.Text = "";
        ////  txtSSNNo.Text = "";
        //  chkJmpCaseDetails.Checked = false;
    }

    // check Bill Soft delete or not if delete show the row strike otherwise normal
    public void SoftDelete()
    {
        //log.Debug("Start SoftDelete()");
        //for (int i = 0; i < grdPatientList.Rows.Count; i++)
        //{
        //    string bt_delete = grdPatientList.DataKeys[i]["BT_DELETE"].ToString();
        //    if (bt_delete == "True")
        //    {
        //        // grdPatientList.Rows[i].Style.Add("text-decoration", "line-through");
        //        for (int j = 0; j < grdPatientList.Rows[i].Cells.Count; j++)
        //        {
        //            grdPatientList.Rows[i].Cells[j].Style.Add("text-decoration", "line-through");
        //        }

        //    }
        //}
        log.Debug("End SoftDelete()");
    }
    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        CaseDetailsBO caseDetailsBO = new CaseDetailsBO();

        if (extddlPatient.Text != "NA")
        {

            if (chkJmpCaseDetails.Checked == true)
            {
                string szCaseID = caseDetailsBO.GetCaseIdByPatientID(txtCompanyID.Text, extddlPatient.Text);
                if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == true)
                {
                    Response.Redirect("../Bill_Sys_ReCaseDetails.aspx?CaseID=" + szCaseID + "&cmp=" + txtCompanyID.Text + "", false);
                }
                else
                {
                    Response.Redirect("../Bill_Sys_CaseDetails.aspx?CaseID=" + szCaseID + "&cmp=" + txtCompanyID.Text + "", false);
                }
            }
            txtPatientName.Text = extddlPatient.Selected_Text;
        }
        else
        {
            txtPatientName.Text = "";
        }
    }
    protected void btnQuickSearch_Click(object sender, EventArgs e)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            // txtChartSearch.Text = txtChartnoSearch.Text;
            //  txtCaseIDSearch.Text = txtCIDSearch.Text;
            //  txtPatientFNameSearch.Text = txtFNameSearch.Text;
            // txtPatientLNameSearch.Text = txtLNameSearch.Text;
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

        }//Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlInsurance_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (extddlInsurance.Text != "NA")
        {
            txtInsuranceCompany.Text = extddlInsurance.Selected_Text; ;
        }
        else
        {
            txtInsuranceCompany.Text = "";
        }
    }


    public DataSet GetPatienView(string caseID, string companyID)
    {
        sqlCon = new SqlConnection(strConn = ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_PATIENTPOP_VIEW", sqlCon);
            sqlCmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["Connection_String"].ToString());
            sqlCmd.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();

            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", caseID);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", companyID);
            sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
    }
    protected void grdPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Patient"))
        {
            string command = e.CommandArgument.ToString();
            string[] strsplitcommand = command.Split(',');
            string caseid = strsplitcommand[0].ToString();
            string companyid = strsplitcommand[1].ToString();
            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "ss", "spanhide();", true);
            }

            // Page.RegisterStartupScript("tw", "<script type='text/javascript'>spanhide();</script>");
            DataSet ds = GetPatienView(caseid, companyid);
            PatientDtlView.DataSource = ds.Tables[0];
            PatientDtlView.DataBind();
            ModalPopupPatientView.Show();



        }

    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalPopupPatientView.Hide();
    }
}
