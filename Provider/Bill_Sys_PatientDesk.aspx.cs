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
using mbs.provider;
using OboutInc.EasyMenu_Pro;
using Componend;
using log4net;

public partial class Provider_Bill_Sys_PatientDesk : PageBase
{
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAdd;
    mbs.provider.ProviderServices obj_provider_services;
    mbs.provider.Calendar_DAO objCalendar;
    DAO_NOTES_BO _DAO_NOTES_BO;
    DAO_NOTES_EO _DAO_NOTES_EO;

    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    //protected System.Web.UI.WebControls.PlaceHolder placeHolder1;
    bool blnTag = false;
    private Boolean blnWeekShortNames = true;
    private string szDateColor_NA = "#ff6347";
    private string szDateColor_TODAY = "#FFFF80";
    Bill_Sys_DeleteBO _deleteOpeation;
    private DataTable dtAllSpecialityEvents, dtAllRoomEvents;
    private DataTable dtVisitType;
    private bool btIsConfig = false;
    private static ILog log = LogManager.GetLogger("Bill_Sys_PatientDesk");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            log.Debug("In page load ");
            if (!IsPostBack)
            {
                txtCaseID.Text = Request.QueryString["caseid"].ToString();
                log.Debug("Case id : "+ txtCaseID.Text);
                txtCompanyId.Text = Request.QueryString["cmpid"].ToString();
                log.Debug("Company id : " + txtCompanyId.Text);
                txtcaseno.Text = Request.QueryString["caseno"].ToString();
                log.Debug("Case No : " + txtcaseno.Text);
                hdnCaseId.Value = txtCaseID.Text;
                hdnCaseNo.Value = txtcaseno.Text;
                hdnCompanyId.Value = txtCompanyId.Text;

                if (Session["Office_ID"] != null)
                {
                    txtOffice_ID.Text = Session["Office_ID"].ToString();
                    hdnOfficeId.Value = txtOffice_ID.Text;
                }
                GetPatientDetailList();
                //UserPatientInfoControl.GetPatienDeskList(txtCaseID.Text, txtCompanyId.Text);
                log.Debug("before call  LoadTabInformation method");
                LoadTabInformation();
                log.Debug("after call  LoadTabInformation method");
                if (Session["GRD_ID"] != null)
                {
                    log.Debug("before call active tab method ");
                    ActiveTab(Session["GRD_ID"].ToString());
                    log.Debug("after call active tab method ");
                    Session["GRD_ID"] = null;
                }
            }
            log.Debug("out of !IsPostBack");
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_PatientDesk. Method - Page_Load : " + ex.Message.ToString());
            log.Debug("Bill_Sys_PatientDesk. Method - Page_Load : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_PatientDesk. Method - Page_Load : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_PatientDesk. Method - Page_Load : " + ex.InnerException.StackTrace.ToString());
            }
        }
        
    }

    private void GetPatientDetailList()
    {
        obj_provider_services = new ProviderServices();
        
        try
        {
            grdPatientList.DataSource = obj_provider_services.GetPatientDeskList("GETPATIENTLIST", txtCaseID.Text, txtCompanyId.Text);                     
            grdPatientList.DataBind();

        }
        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }

    }
    private void LoadTabInformation()
    {
        try
        {
            log.Debug("In LoadTabInformation method");
             _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            obj_provider_services = new ProviderServices();

            DataTable dt = new DataTable();
            DataTable dtRoomList = new DataTable();

            if (Session["SpecialityList"] == null)
            {
                // Get Speciality List
                dt = obj_provider_services.Get_SpecialityList(txtCompanyId.Text);
                log.Debug(" Speciality List count : "+dt.Rows.Count);
                log.Debug("Get Speciality List");
                // Get Room List
                dtRoomList = obj_provider_services.Get_PatientDeskRoomList(txtCompanyId.Text);
                log.Debug("Get Room List");
                log.Debug(" Room List count : " + dtRoomList.Rows.Count);
                // Merage two dataset
                dt.Merge(dtRoomList);
                log.Debug("Merage two dataset");
                log.Debug(" Merge of room and specialty  count : " + dt.Rows.Count);
                Session["SpecialityList"] = dt;
            }
            else
            {
                log.Debug("if seesion of speciality is not null");
                dt = (DataTable)Session["SpecialityList"];
                log.Debug(" SpecialityList  count : " + dt.Rows.Count);
            }


            int tabCount = 0;
            string strDate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");

            dtAllSpecialityEvents = new DataTable();
            dtAllRoomEvents = new DataTable();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
            {
                log.Debug("company is not reffering");
                dtAllSpecialityEvents = obj_provider_services.Get_Tab_TestInformation_TEMP(txtCaseID.Text, txtCompanyId.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);

                // Get Room's Events
                log.Debug("Get Room's Events,SP: SP_GET_TAB_TESTINFORMATION_TEMP_PROVIDER ");
                log.Debug(" Speciality Events  count : " + dtAllSpecialityEvents.Rows.Count);

                dtAllRoomEvents = obj_provider_services.Get_Outschedule_Tab_Information(txtCaseID.Text, txtCompanyId.Text);
                log.Debug("Get Room's Events,SP: SP_GET_OUSCHEDULE_TAB_INFORMATION ");
                log.Debug(" Room's Events  count : " + dtAllRoomEvents.Rows.Count);

                dtAllSpecialityEvents.Merge(dtAllRoomEvents);
                log.Debug("merge dtAllSpecialityEvents and dtAllRoomEvents");
                log.Debug("merge dtAllSpecialityEvents and dtAllRoomEvents count : " + dtAllSpecialityEvents.Rows.Count);
            }
            else
            {
                log.Debug("company is reffering");
                // Get Room's Events
                dtAllRoomEvents = _bill_Sys_PatientBO.Get_Outschedule_Tab_Information_LHR(txtCaseID.Text, txtCompanyId.Text,((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                log.Debug("Get Room's Events,SP: SP_GET_OUSCHEDULE_TAB_INFORMATION ");
                dtAllSpecialityEvents.Merge(dtAllRoomEvents);
                log.Debug("merge dtAllSpecialityEvents and dtAllRoomEvents");
            }

            DataTable dtTreatment = dtAllSpecialityEvents.Clone();
            DataRow[] results;
            DropDownList ddl;
            DropDownList ddl1;
            LinkButton lnkUP;
            LinkButton lnkSCN;
            ExtendedDropDownList.ExtendedDropDownList extDoct;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tabCount = tabCount + 1;
                    log.Debug("tab Count : " + tabCount);
                    switch (tabCount)
                    {
                        case 1:
                            log.Debug("start case 1");
                            tabpnlOne.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            log.Debug("start case 1 : SPECIALITY = " + row[1].ToString());
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadOne.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            log.Debug("start case 1 : Label Head  = " + lblHeadOne.Text);
                            grdOne.DataSource = dtTreatment;
                            grdOne.DataBind();

                           setColumnAccordingScheduleType(ref grdOne);

                            for (int i = 0; i < grdOne.Items.Count; i++)
                            {
                                ddl1 = (DropDownList)grdOne.Items[i].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdOne.Items[i].Cells[15].Text;
                                log.Debug("start case 1 : ddl status  = " + ddl1.SelectedValue);
                            }

                            objCalendar = new mbs.provider.Calendar_DAO(); ;
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalOne, dtTreatment);
                            log.Debug("End case 1");
                            break;
                        case 2:
                            log.Debug("start case 2 ");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwo.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            log.Debug("start case 2 : SPECIALITY = " + row[1].ToString());
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwo.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            log.Debug("start case 2 : Label Head  = " + lblHeadOne.Text);
                            grdTwo.DataSource = dtTreatment;
                            grdTwo.DataBind();

                            setColumnAccordingScheduleType(ref grdTwo);

                            for (int i = 0; i < grdTwo.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwo.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdTwo.Items[i].Cells[15].Text;
                                log.Debug("start case 1 : ddl status  = " + ddl1.SelectedValue);
                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwo, dtTreatment);
                            log.Debug("End case 2");
                            break;
                        case 3:
                            log.Debug("start case 3");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThree.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThree.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThree.DataSource = dtTreatment;
                            grdThree.DataBind();

                            setColumnAccordingScheduleType(ref grdThree);

                            for (int i = 0; i < grdThree.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThree.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdThree.Items[i].Cells[15].Text;


                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThree, dtTreatment);
                            log.Debug("End case 3");
                            break;
                        case 4:
                            log.Debug("start case 4");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlFour.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadFour.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFour.DataSource = dtTreatment;
                            grdFour.DataBind();

                            setColumnAccordingScheduleType(ref grdFour);

                            for (int i = 0; i < grdFour.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdFour.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdFour.Items[i].Cells[15].Text;


                            }


                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFour, dtTreatment);
                            log.Debug("End case 4");
                            break;
                        case 5:
                            log.Debug("start case 5");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlFive.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadFive.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFive.DataSource = dtTreatment;
                            grdFive.DataBind();

                            setColumnAccordingScheduleType(ref grdFive);

                            for (int i = 0; i < grdFive.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdFive.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdFive.Items[i].Cells[15].Text;

                            }
                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFive, dtTreatment);
                            log.Debug("End case 5");
                            break;
                        case 6:
                            log.Debug("start case 6");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlSix.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadSix.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSix.DataSource = dtTreatment;
                            grdSix.DataBind();

                            setColumnAccordingScheduleType(ref grdSix);

                            for (int i = 0; i < grdSix.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdSix.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdSix.Items[i].Cells[15].Text;


                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSix, dtTreatment);
                            log.Debug("End case 6");
                            break;
                        case 7:
                            log.Debug("start case 7");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlSeven.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadSeven.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSeven.DataSource = dtTreatment;
                            grdSeven.DataBind();

                            setColumnAccordingScheduleType(ref grdSeven);

                            for (int i = 0; i < grdSeven.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdSeven.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdSeven.Items[i].Cells[15].Text;

                            }


                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSeven, dtTreatment);
                            log.Debug("End case 7");
                            break;
                        case 8:
                            log.Debug("start case 8");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlEight.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadEight.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEight.DataSource = dtTreatment;
                            grdEight.DataBind();

                            setColumnAccordingScheduleType(ref grdEight);

                            for (int i = 0; i < grdEight.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdEight.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdEight.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEight, dtTreatment);
                            log.Debug("End case 8");
                            break;
                        case 9:
                            log.Debug("start case 9");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlNine.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadNine.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdNine.DataSource = dtTreatment;
                            grdNine.DataBind();

                            setColumnAccordingScheduleType(ref grdNine);

                            for (int i = 0; i < grdNine.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdNine.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdNine.Items[i].Cells[15].Text;

                            }
                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalNine, dtTreatment);
                            log.Debug("End case 9");
                            break;
                        case 10:
                            log.Debug("start case 10");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTen.DataSource = dtTreatment;
                            grdTen.DataBind();

                            setColumnAccordingScheduleType(ref grdTen);

                            for (int i = 0; i < grdTen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTen.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdTen.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTen, dtTreatment);
                            log.Debug("End case 10");
                            break;
                        case 11:
                            log.Debug("start case 11");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlEleven.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadEleven.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEleven.DataSource = dtTreatment;
                            grdEleven.DataBind();
                            setColumnAccordingScheduleType(ref grdEleven);

                            for (int i = 0; i < grdEleven.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdEleven.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdEleven.Items[i].Cells[15].Text;


                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEleven, dtTreatment);
                            log.Debug("End case 11");
                            break;
                        case 12:
                            log.Debug("start case 12");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwelve.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwelve.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwelve.DataSource = dtTreatment;
                            grdTwelve.DataBind();

                            setColumnAccordingScheduleType(ref grdTwelve);

                            for (int i = 0; i < grdTwelve.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwelve.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdTwelve.Items[i].Cells[15].Text;

                            }
                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwelve, dtTreatment);
                            log.Debug("End case 12");
                            break;
                        case 13:
                            log.Debug("start case 13");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThirteen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThirteen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirteen.DataSource = dtTreatment;
                            grdThirteen.DataBind();

                            setColumnAccordingScheduleType(ref grdThirteen);

                            for (int i = 0; i < grdThirteen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThirteen.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdThirteen.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirteen, dtTreatment);
                            log.Debug("End case 13");
                            break;
                        case 14:
                            log.Debug("start case 14");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlFourteen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadFourteen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFourteen.DataSource = dtTreatment;
                            grdFourteen.DataBind();

                            setColumnAccordingScheduleType(ref grdFourteen);

                            for (int i = 0; i < grdFourteen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdFourteen.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdFourteen.Items[i].Cells[15].Text;

                            }


                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFourteen, dtTreatment);
                            log.Debug("End case 14");
                            break;
                        case 15:
                            log.Debug("start case 15");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlFifteen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadFifteen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFifteen.DataSource = dtTreatment;
                            grdFifteen.DataBind();

                            setColumnAccordingScheduleType(ref grdFifteen);

                            for (int i = 0; i < grdFifteen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdFifteen.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdFifteen.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFifteen, dtTreatment);
                            log.Debug("End case 15");
                            break;
                        case 16:
                            log.Debug("start case 16");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlSixteen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadSixteen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSixteen.DataSource = dtTreatment;
                            grdSixteen.DataBind();

                            setColumnAccordingScheduleType(ref grdSixteen);

                            for (int i = 0; i < grdSixteen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdSixteen.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdSixteen.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSixteen, dtTreatment);
                            log.Debug("End case 16");
                            break;
                        case 17:
                            log.Debug("start case 17");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlSeventeen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadSeventeen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSeventeen.DataSource = dtTreatment;
                            grdSeventeen.DataBind();

                            setColumnAccordingScheduleType(ref grdSeventeen);

                            for (int i = 0; i < grdSeventeen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdSeventeen.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdSeventeen.Items[i].Cells[15].Text;


                            }
                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSeventeen, dtTreatment);
                            log.Debug("End case 17");
                            break;
                        case 18:
                            log.Debug("start case 18");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlEighteen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadEighteen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEighteen.DataSource = dtTreatment;
                            grdEighteen.DataBind();

                            setColumnAccordingScheduleType(ref grdEighteen);

                            for (int i = 0; i < grdEighteen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdEighteen.Items[i].Cells[5].FindControl("ddlStatus");
                                ddl1.SelectedValue = grdEighteen.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEighteen, dtTreatment);
                            log.Debug("End case 18");
                            break;
                        case 19:
                            log.Debug("start case 19");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlNineteen.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadNineteen.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdNineteen.DataSource = dtTreatment;
                            grdNineteen.DataBind();

                            setColumnAccordingScheduleType(ref grdNineteen);

                            for (int i = 0; i < grdNineteen.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdNineteen.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdNineteen.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalNineteen, dtTreatment);
                            log.Debug("End case 19");
                            break;
                        case 20:
                            log.Debug("start case 20");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwenty.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwenty.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwenty.DataSource = dtTreatment;
                            grdTwenty.DataBind();

                            setColumnAccordingScheduleType(ref grdTwenty);

                            for (int i = 0; i < grdTwenty.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwenty.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwenty.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            
                            log.Debug("End case 20");
                            break;

                        case 21:
                            log.Debug("start case 21");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentyOne.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentyOne.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyOne.DataSource = dtTreatment;
                            grdTwentyOne.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentyOne);

                            for (int i = 0; i < grdTwentyOne.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentyOne.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentyOne.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 21");
                            break;
                        case 22:
                            log.Debug("start case 22");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentyTwo.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentyTwo.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyTwo.DataSource = dtTreatment;
                            grdTwentyTwo.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentyTwo);

                            for (int i = 0; i < grdTwentyTwo.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentyTwo.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentyTwo.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 22");
                            //showCalendar(objCalendar, updpnlCalTwentyTwo, dtTreatment);
                            break;
                        case 23:
                            log.Debug("start case 23");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentyThree.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentyThree.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyThree.DataSource = dtTreatment;
                            grdTwentyThree.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentyThree);

                            for (int i = 0; i < grdTwentyThree.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentyThree.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentyThree.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 23");
                            break;
                        case 24:
                            log.Debug("start case 24");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentyFour.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentyFour.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyFour.DataSource = dtTreatment;
                            grdTwentyFour.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentyFour);

                            for (int i = 0; i < grdTwentyFour.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentyFour.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentyFour.Items[i].Cells[15].Text;


                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 24");
                            break;
                        case 25:
                            log.Debug("start case 25");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentyFive.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentyFive.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyFive.DataSource = dtTreatment;
                            grdTwentyFive.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentyFive);

                            for (int i = 0; i < grdTwentyFive.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentyFive.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentyFive.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 25");
                            break;
                        case 26:
                            log.Debug("start case 26");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentySix.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentySix.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentySix.DataSource = dtTreatment;
                            grdTwentySix.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentySix);

                            for (int i = 0; i < grdTwentySix.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentySix.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentySix.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 26");
                            break;

                        case 27:
                            log.Debug("start case 27");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentySeven.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentySeven.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentySeven.DataSource = dtTreatment;
                            grdTwentySeven.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentySeven);

                            for (int i = 0; i < grdTwentySeven.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentySeven.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentySeven.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 27");
                            break;
                        case 28:
                            log.Debug("start case 28");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentyEight.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentyEight.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyEight.DataSource = dtTreatment;
                            grdTwentyEight.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentyEight);

                            for (int i = 0; i < grdTwentyEight.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentyEight.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentyEight.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 28");
                            break;
                        case 29:
                            log.Debug("start case 29");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlTwentyNine.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadTwentyNine.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyNine.DataSource = dtTreatment;
                            grdTwentyNine.DataBind();

                            setColumnAccordingScheduleType(ref grdTwentyNine);

                            for (int i = 0; i < grdTwentyNine.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdTwentyNine.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdTwentyNine.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 29");
                            break;

                        case 30:
                            log.Debug("start case 30");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThirty.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThirty.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirty.DataSource = dtTreatment;
                            grdThirty.DataBind();

                            setColumnAccordingScheduleType(ref grdThirty);

                            for (int i = 0; i < grdThirty.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThirty.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdThirty.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 30");
                            break;

                        case 31:
                            log.Debug("start case 31");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThirtyOne.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThirtyOne.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyOne.DataSource = dtTreatment;
                            grdThirtyOne.DataBind();

                            setColumnAccordingScheduleType(ref grdThirtyOne);

                            for (int i = 0; i < grdThirtyOne.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThirtyOne.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdThirtyOne.Items[i].Cells[15].Text;


                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 31");
                            break;

                        case 32:
                            log.Debug("start case 32");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThirtyTwo.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThirtyTwo.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyTwo.DataSource = dtTreatment;
                            grdThirtyTwo.DataBind();

                            setColumnAccordingScheduleType(ref grdThirtyTwo);

                            for (int i = 0; i < grdThirtyTwo.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThirtyTwo.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdThirtyTwo.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 32");
                            break;

                        case 33:
                            log.Debug("start case 33");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThirtyThree.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThirtyThree.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyThree.DataSource = dtTreatment;
                            grdThirtyThree.DataBind();

                            setColumnAccordingScheduleType(ref grdThirtyThree);

                            for (int i = 0; i < grdThirtyThree.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThirtyThree.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdThirtyThree.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            log.Debug("End case 33");
                            break;

                        case 34:
                            log.Debug("start case 34");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThirtyFour.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThirtyFour.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyFour.DataSource = dtTreatment;
                            grdThirtyFour.DataBind();

                            setColumnAccordingScheduleType(ref grdThirtyFour);

                            for (int i = 0; i < grdThirtyFour.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThirtyFour.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdThirtyFour.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            log.Debug("End case 34");
                            break;

                        case 35:
                            log.Debug("start case 35");
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabpnlThirtyFive.Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            lblHeadThirtyFive.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyFive.DataSource = dtTreatment;
                            grdThirtyFive.DataBind();

                            setColumnAccordingScheduleType(ref grdThirtyFive);

                            for (int i = 0; i < grdThirtyFive.Items.Count; i++)
                            {

                                ddl1 = (DropDownList)grdThirtyFive.Items[i].Cells[5].FindControl("ddlStatus");

                                ddl1.SelectedValue = grdThirtyFive.Items[i].Cells[15].Text;

                            }

                            objCalendar = new mbs.provider.Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                           
                            log.Debug("end case 35");
                            break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Load Grid : " + ex.Message.ToString());
            log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Load Grid : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Load Grid : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Load Grid : " + ex.InnerException.StackTrace.ToString());
            }
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
    private void setColumnAccordingScheduleType(ref DataGrid p_objDataGrid)
    {
        DataGrid objDataGrid = p_objDataGrid;
        try
        {
            if (objDataGrid.Items.Count > 0)
            {
                if (((TextBox)objDataGrid.Items[0].FindControl("txtScheduleType")).Text == "IN")
                {
                    objDataGrid.Columns[1].Visible = false; // OFFICE NAME
                }
                else
                {
                    objDataGrid.Columns[7].Visible = false; // VISIT TYPE NAME
                }
            }
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_PatientDesk. Method - setColumnAccordingScheduleType : " + ex.Message.ToString());
            log.Debug("Bill_Sys_PatientDesk. Method - setColumnAccordingScheduleType: " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_PatientDesk. Method - setColumnAccordingScheduleType : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_PatientDesk. Method - setColumnAccordingScheduleType : " + ex.InnerException.StackTrace.ToString());
            }

            throw;
        }
    }
    private void ActiveTab(string p_szID)
    {
        try
        {
            log.Debug("In active tab method");
            switch (p_szID)
            {

                
                case "grdOne":
                    tabVistInformation.ActiveTabIndex = 0;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwo":
                    tabVistInformation.ActiveTabIndex = 1;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThree":
                    tabVistInformation.ActiveTabIndex = 2;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdFour":
                    tabVistInformation.ActiveTabIndex = 3;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdFive":
                    tabVistInformation.ActiveTabIndex = 4;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdSix":
                    tabVistInformation.ActiveTabIndex = 5;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdSeven":
                    tabVistInformation.ActiveTabIndex = 6;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdEight":
                    tabVistInformation.ActiveTabIndex = 7;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdNine":
                    tabVistInformation.ActiveTabIndex = 8;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTen":
                    tabVistInformation.ActiveTabIndex = 9;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdEleven":
                    tabVistInformation.ActiveTabIndex = 10;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwelve":
                    tabVistInformation.ActiveTabIndex = 11;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThirteen":
                    tabVistInformation.ActiveTabIndex = 12;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdFourteen":
                    tabVistInformation.ActiveTabIndex = 13;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdFifteen":
                    tabVistInformation.ActiveTabIndex = 14;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdSixteen":
                    tabVistInformation.ActiveTabIndex = 15;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdSeventeen":
                    tabVistInformation.ActiveTabIndex = 16;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdEighteen":
                    tabVistInformation.ActiveTabIndex = 17;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdNineteen":
                    tabVistInformation.ActiveTabIndex = 18;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwenty":
                    tabVistInformation.ActiveTabIndex = 19;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentyOne":
                    tabVistInformation.ActiveTabIndex = 20;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentyTwo":
                    tabVistInformation.ActiveTabIndex = 21;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentyThree":
                    tabVistInformation.ActiveTabIndex = 22;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentyFour":
                    tabVistInformation.ActiveTabIndex = 23;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentyFive":
                    tabVistInformation.ActiveTabIndex = 24;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentySix":
                    tabVistInformation.ActiveTabIndex = 25;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentySeven":
                    tabVistInformation.ActiveTabIndex = 26;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentyEight":
                    tabVistInformation.ActiveTabIndex = 27;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdTwentyNine":
                    tabVistInformation.ActiveTabIndex = 28;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThirty":
                    tabVistInformation.ActiveTabIndex = 29;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThirtyOne":
                    tabVistInformation.ActiveTabIndex = 30;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThirtyTwo":
                    tabVistInformation.ActiveTabIndex = 31;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThirtyThree":
                    tabVistInformation.ActiveTabIndex = 32;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThirtyFour":
                    tabVistInformation.ActiveTabIndex = 33;
                    log.Debug("p_szID" + p_szID);
                    break;
                case "grdThirtyFive":
                    tabVistInformation.ActiveTabIndex = 34;
                    log.Debug("p_szID" + p_szID);
                    break;
            }

        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Active tab : " + ex.Message.ToString());
            log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Active tab : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Active tab : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_PatientDesk. Method - Page_Load Active tab : " + ex.InnerException.StackTrace.ToString());
            }
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
    }
}
