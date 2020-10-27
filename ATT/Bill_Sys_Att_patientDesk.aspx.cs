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
using DevExpress.Web;
using Componend;
using System.Data.SqlClient;
using System.IO;
using OboutInc.EasyMenu_Pro;


public partial class ATT_Bill_Sys_Att_patientDesk : PageBase
{
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAdd;

    DAO_NOTES_BO _DAO_NOTES_BO;
    DAO_NOTES_EO _DAO_NOTES_EO;

    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    //protected System.Web.UI.WebControls.PlaceHolder placeHolder1;

    bool blnTag = false;
    private Boolean blnWeekShortNames = true;

    private string szDateColor_NA = "#ff6347";
    private string szDateColor_TODAY = "#FFFF80";
    Bill_Sys_DeleteBO _deleteOpeation;
    Calendar_DAO objCalendar = null;
    private DataTable dtAllSpecialityEvents, dtAllRoomEvents;
    private DataTable dtVisitType;
    private bool btIsConfig = false;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtCaseID.Text = Request.QueryString["Caseid"].ToString();
            txtCompanyId.Text = Request.QueryString["cmp"].ToString();
            txtPatientId.Text = Request.QueryString["pid"].ToString();
            BindControl();
            LoadTabInformation();

        }

    }
    public void BindControl()
    {
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        DataSet Ds = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", txtCaseID.Text, txtCompanyId.Text);
        DtlPatientDesk.DataSource = Ds;
        DtlPatientDesk.DataBind();
    }
    private void LoadTabInformation()
    {
        try
        {
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            Patient_TVBO _patient_tvbo = new Patient_TVBO();
            DataTable dt = new DataTable();
            DataTable dtRoomList = new DataTable();

            if (Session["SpecialityList"] == null)
            {
                // Get Speciality List
                dt = _bill_Sys_PatientBO.Get_SpecialityList(txtCompanyId.Text);
                // Get Room List
                dtRoomList = _bill_Sys_PatientBO.Get_PatientDeskRoomList(txtCompanyId.Text);
                // Merage two dataset
                dt.Merge(dtRoomList);

                Session["SpecialityList"] = dt;
            }
            else
            {
                dt = (DataTable)Session["SpecialityList"];
            }


            int tabCount = 0;
            string strDate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
            dtAllSpecialityEvents = new DataTable();
            dtAllRoomEvents = new DataTable();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
            {
                dtAllSpecialityEvents = _patient_tvbo.Get_Tab_TestInformation_TEMP_ATT(txtCaseID.Text, txtCompanyId.Text);

                // Get Room's Events

                dtAllRoomEvents = _patient_tvbo.Get_Outschedule_Tab_Information_ATT(txtCaseID.Text, txtCompanyId.Text);

                dtAllSpecialityEvents.Merge(dtAllRoomEvents);
            }
            else
            {

                // Get Room's Events
                dtAllRoomEvents = _patient_tvbo.Get_Outschedule_Tab_Information_ATT(txtCaseID.Text, txtCompanyId.Text);

                dtAllSpecialityEvents.Merge(dtAllRoomEvents);
            }

            DataTable dtTreatment = dtAllSpecialityEvents.Clone();
            DataRow[] results;
            DropDownList ddl;
            DropDownList ddl1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tabCount = tabCount + 1;
                    switch (tabCount)
                    {
                        case 1:

                            tabVistInformation.TabPages.FindByName("tabpnlOne").Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlOne").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdOne.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdOne"] = dtTreatment;
                            grdOne.DataBind();

                            //setColumnAccordingScheduleType( grdOne);
                            for (int i = 0; i < grdOne.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdOne.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdOne.Columns[1].Visible = false;
                                        grdOne.DataBind();
                                    }
                                    else
                                    {
                                        grdOne.Columns[7].Visible = false;
                                        grdOne.DataBind();
                                    }
                                }
                                // string s = grdOne.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalOne, dtTreatment);
                            break;
                        case 2:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwo").Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwo").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwo.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwo"] = dtTreatment;
                            grdTwo.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwo);

                            //for (int i = 0; i < grdTwo.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwo.Items[i].Cells[5].FindControl("ddlStatus");
                            //   // ddl1.SelectedValue = grdTwo.Items[i].Cells[17].Text;




                            //}

                            for (int i = 0; i < grdTwo.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwo.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwo.Columns[1].Visible = false;
                                        grdTwo.DataBind();
                                    }
                                    else
                                    {
                                        grdTwo.Columns[7].Visible = false;
                                        grdTwo.DataBind();
                                    }
                                }
                                // string s = grdTwo.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwo, dtTreatment);
                            break;
                        case 3:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThree").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThree").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThree.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThree"] = dtTreatment;
                            grdThree.DataBind();

                            //setColumnAccordingScheduleType(ref grdThree);

                            //for (int i = 0; i < grdThree.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThree.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdThree.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdThree.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThree.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThree.Columns[1].Visible = false;
                                        grdThree.DataBind();
                                    }
                                    else
                                    {
                                        grdThree.Columns[7].Visible = false;
                                        grdThree.DataBind();
                                    }
                                }
                                // string s = grdThree.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThree, dtTreatment);
                            break;
                        case 4:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFour").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlFour").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFour.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdFour"] = dtTreatment;
                            grdFour.DataBind();

                            //setColumnAccordingScheduleType(ref grdFour);

                            //for (int i = 0; i < grdFour.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFour.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFour.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdFour.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFour.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdFour.Columns[1].Visible = false;
                                        grdFour.DataBind();
                                    }
                                    else
                                    {
                                        grdFour.Columns[7].Visible = false;
                                        grdFour.DataBind();
                                    }
                                }
                                // string s = grdFour.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFour, dtTreatment);
                            break;
                        case 5:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFive").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            //lblHeadFive.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            tabVistInformation.TabPages.FindByName("tabpnlFive").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFive.DataSource = dtTreatment;
                            //viewState for Export to xlxs
                            ViewState["VSgrdFive"] = dtTreatment;
                            grdFive.DataBind();

                            //setColumnAccordingScheduleType(ref grdFive);

                            //for (int i = 0; i < grdFive.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFive.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFive.Items[i].Cells[17].Text;


                            //}

                            for (int i = 0; i < grdFive.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFive.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdFive.Columns[1].Visible = false;
                                        grdFive.DataBind();
                                    }
                                    else
                                    {
                                        grdFive.Columns[7].Visible = false;
                                        grdFive.DataBind();
                                    }
                                }
                                // string s = grdFive.GetRowValues(i, "I_STATUS").ToString();
                                GridViewDataColumn d = new GridViewDataColumn();
                                // ASPxComboBox cmb = (ASPxComboBox)grdFive.FindDetailRowTemplateControl(i, "ddlStatus");
                                // object keyValue = grdFive.GetRowValues(i, new string[] { grdFive. });
                                //cmb.Value = grdOne.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFive, dtTreatment);
                            break;
                        case 6:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSix").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSix").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSix.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdSix"] = dtTreatment;
                            grdSix.DataBind();

                            //setColumnAccordingScheduleType(ref grdSix);

                            //for (int i = 0; i < grdSix.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSix.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSix.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdSix.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSix.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdSix.Columns[1].Visible = false;
                                        grdSix.DataBind();
                                    }
                                    else
                                    {
                                        grdSix.Columns[7].Visible = false;
                                        grdSix.DataBind();
                                    }
                                }
                                // string s = grdSix.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSix, dtTreatment);
                            break;
                        case 7:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSeven").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSeven").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSeven.DataSource = dtTreatment;
                            //viewState for Export to xlxs
                            ViewState["VSgrdSeven"] = dtTreatment;
                            grdSeven.DataBind();

                            //setColumnAccordingScheduleType(ref grdSeven);

                            //for (int i = 0; i < grdSeven.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSeven.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSeven.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdSeven.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSeven.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdSeven.Columns[1].Visible = false;
                                        grdSeven.DataBind();
                                    }
                                    else
                                    {
                                        grdSeven.Columns[7].Visible = false;
                                        grdSeven.DataBind();
                                    }
                                }
                                // string s = grdSeven.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSeven, dtTreatment);
                            break;
                        case 8:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlEight").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlEight").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEight.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdEight"] = dtTreatment;
                            grdEight.DataBind();

                            //setColumnAccordingScheduleType(ref grdEight);

                            //for (int i = 0; i < grdEight.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdEight.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdEight.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdEight.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdEight.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdEight.Columns[1].Visible = false;
                                        grdEight.DataBind();
                                    }
                                    else
                                    {
                                        grdEight.Columns[7].Visible = false;
                                        grdEight.DataBind();
                                    }
                                }
                                // string s = grdEight.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEight, dtTreatment);
                            break;
                        case 9:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlNine").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlNine").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdNine.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdNine"] = dtTreatment;
                            grdNine.DataBind();

                            //setColumnAccordingScheduleType(ref grdNine);

                            //for (int i = 0; i < grdNine.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdNine.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdNine.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdNine.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdNine.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdNine.Columns[1].Visible = false;
                                        grdNine.DataBind();
                                    }
                                    else
                                    {
                                        grdNine.Columns[7].Visible = false;
                                        grdNine.DataBind();
                                    }
                                }
                                // string s = grdNine.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalNine, dtTreatment);
                            break;
                        case 10:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTen"] = dtTreatment;
                            grdTen.DataBind();

                            //setColumnAccordingScheduleType(ref grdTen);

                            //for (int i = 0; i < grdTen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdTen.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTen.Columns[1].Visible = false;
                                        grdTen.DataBind();
                                    }
                                    else
                                    {
                                        grdTen.Columns[7].Visible = false;
                                        grdTen.DataBind();
                                    }
                                }
                                // string s = grdTen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTen, dtTreatment);
                            break;
                        case 11:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlEleven").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlEleven").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEleven.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdEleven"] = dtTreatment;
                            grdEleven.DataBind();
                            //setColumnAccordingScheduleType(ref grdEleven);

                            //for (int i = 0; i < grdEleven.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdEleven.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdEleven.Items[i].Cells[17].Text;



                            //}
                            for (int i = 0; i < grdEleven.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdEleven.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdEleven.Columns[1].Visible = false;
                                        grdEleven.DataBind();
                                    }
                                    else
                                    {
                                        grdEleven.Columns[7].Visible = false;
                                        grdEleven.DataBind();
                                    }
                                }
                                // string s = grdEleven.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEleven, dtTreatment);
                            break;
                        case 12:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwelve").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwelve").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwelve.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwelve"] = dtTreatment;
                            grdTwelve.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwelve);

                            //for (int i = 0; i < grdTwelve.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwelve.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdTwelve.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwelve.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwelve.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwelve.Columns[1].Visible = false;
                                        grdTwelve.DataBind();
                                    }
                                    else
                                    {
                                        grdTwelve.Columns[7].Visible = false;
                                        grdTwelve.DataBind();
                                    }
                                }
                                // string s = grdEleven.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwelve, dtTreatment);
                            break;
                        case 13:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirteen"] = dtTreatment;
                            grdThirteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirteen);

                            //for (int i = 0; i < grdThirteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdThirteen.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdThirteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirteen.Columns[1].Visible = false;
                                        grdThirteen.DataBind();
                                    }
                                    else
                                    {
                                        grdThirteen.Columns[7].Visible = false;
                                        grdThirteen.DataBind();
                                    }
                                }
                                // string s = grdThirteen.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirteen, dtTreatment);
                            break;
                        case 14:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFourteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlFourteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFourteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdFourteen"] = dtTreatment;
                            grdFourteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdFourteen);

                            //for (int i = 0; i < grdFourteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFourteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFourteen.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdFourteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFourteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdFourteen.Columns[1].Visible = false;
                                        grdFourteen.DataBind();
                                    }
                                    else
                                    {
                                        grdFourteen.Columns[7].Visible = false;
                                        grdFourteen.DataBind();
                                    }
                                }
                                // string s = grdThirteen.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFourteen, dtTreatment);
                            break;
                        case 15:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFifteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlFifteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFifteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdFifteen"] = dtTreatment;
                            grdFifteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdFifteen);

                            //for (int i = 0; i < grdFifteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFifteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFifteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdFifteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFifteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdFifteen.Columns[1].Visible = false;
                                        grdFifteen.DataBind();
                                    }
                                    else
                                    {
                                        grdFifteen.Columns[7].Visible = false;
                                        grdFifteen.DataBind();
                                    }
                                }
                                // string s = grdFifteen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFifteen, dtTreatment);
                            break;
                        case 16:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSixteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSixteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSixteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdSixteen"] = dtTreatment;
                            grdSixteen.DataBind();

                            // setColumnAccordingScheduleType(ref grdSixteen);

                            //for (int i = 0; i < grdSixteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSixteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSixteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdSixteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSixteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdSixteen.Columns[1].Visible = false;
                                        grdSixteen.DataBind();
                                    }
                                    else
                                    {
                                        grdSixteen.Columns[7].Visible = false;
                                        grdSixteen.DataBind();
                                    }
                                }
                                // string s = grdSixteen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSixteen, dtTreatment);
                            break;
                        case 17:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSeventeen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSeventeen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSeventeen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdSeventeen"] = dtTreatment;
                            grdSeventeen.DataBind();

                            //setColumnAccordingScheduleType(ref grdSeventeen);

                            //for (int i = 0; i < grdSeventeen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSeventeen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSeventeen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdSeventeen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSeventeen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdSeventeen.Columns[1].Visible = false;
                                        grdSeventeen.DataBind();
                                    }
                                    else
                                    {
                                        grdSeventeen.Columns[7].Visible = false;
                                        grdSeventeen.DataBind();
                                    }
                                }
                                // string s = grdSeventeen.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSeventeen, dtTreatment);
                            break;
                        case 18:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlEighteen").Visible = true;


                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlEighteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEighteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdEighteen"] = dtTreatment;
                            grdEighteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdEighteen);

                            //for (int i = 0; i < grdEighteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdEighteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdEighteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdEighteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdEighteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdEighteen.Columns[1].Visible = false;
                                        grdEighteen.DataBind();
                                    }
                                    else
                                    {
                                        grdEighteen.Columns[7].Visible = false;
                                        grdEighteen.DataBind();
                                    }
                                }
                                // string s = grdEighteen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEighteen, dtTreatment);
                            break;
                        case 19:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlNineteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlNineteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdNineteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdNineteen"] = dtTreatment;
                            grdNineteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdNineteen);

                            //for (int i = 0; i < grdNineteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdNineteen.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdNineteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdNineteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdNineteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdNineteen.Columns[1].Visible = false;
                                        grdNineteen.DataBind();
                                    }
                                    else
                                    {
                                        grdNineteen.Columns[7].Visible = false;
                                        grdNineteen.DataBind();
                                    }
                                }
                                // string s = grdNineteen.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalNineteen, dtTreatment);
                            break;
                        case 20:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwenty").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwenty").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwenty.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwenty"] = dtTreatment;
                            grdTwenty.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwenty);

                            //for (int i = 0; i < grdTwenty.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwenty.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwenty.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwenty.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwenty.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwenty.Columns[1].Visible = false;
                                        grdTwenty.DataBind();
                                    }
                                    else
                                    {
                                        grdTwenty.Columns[7].Visible = false;
                                        grdTwenty.DataBind();
                                    }
                                }
                                // string s = grdTwenty.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwenty, dtTreatment);
                            break;

                        case 21:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyOne").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyOne").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyOne.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyOne"] = dtTreatment;
                            grdTwentyOne.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyOne);

                            //for (int i = 0; i < grdTwentyOne.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyOne.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyOne.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTwentyOne.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyOne.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyOne.Columns[1].Visible = false;
                                        grdTwentyOne.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyOne.Columns[7].Visible = false;
                                        grdTwentyOne.DataBind();
                                    }
                                }
                                // string s = grdTwentyOne.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyOne, dtTreatment);
                            break;
                        case 22:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyTwo").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyTwo").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyTwo.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyTwo"] = dtTreatment;
                            grdTwentyTwo.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyTwo);

                            //for (int i = 0; i < grdTwentyTwo.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyTwo.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyTwo.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyTwo.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyTwo.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyTwo.Columns[1].Visible = false;
                                        grdTwentyTwo.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyTwo.Columns[7].Visible = false;
                                        grdTwentyTwo.DataBind();
                                    }
                                }
                                // string s = grdTwentyTwo.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyTwo, dtTreatment);
                            break;
                        case 23:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyThree").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyThree").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyThree.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyThree"] = dtTreatment;
                            grdTwentyThree.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyThree);

                            //for (int i = 0; i < grdTwentyThree.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyThree.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyThree.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyThree.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyThree.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyThree.Columns[1].Visible = false;
                                        grdTwentyThree.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyThree.Columns[7].Visible = false;
                                        grdTwentyThree.DataBind();
                                    }
                                }
                                // string s = grdTwentyThree.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyThree, dtTreatment);
                            break;
                        case 24:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFour").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFour").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyFour.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyFour"] = dtTreatment;
                            grdTwentyFour.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyFour);

                            //for (int i = 0; i < grdTwentyFour.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyFour.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyFour.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdTwentyFour.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyFour.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyFour.Columns[1].Visible = false;
                                        grdTwentyFour.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyFour.Columns[7].Visible = false;
                                        grdTwentyFour.DataBind();
                                    }
                                }
                                // string s = grdTwentyFour.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyFour, dtTreatment);
                            break;
                        case 25:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFive").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFive").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyFive.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyFive"] = dtTreatment;
                            grdTwentyFive.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyFive);

                            //for (int i = 0; i < grdTwentyFive.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyFive.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyFive.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyFive.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyFive.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyFive.Columns[1].Visible = false;
                                        grdTwentyFive.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyFive.Columns[7].Visible = false;
                                        grdTwentyFive.DataBind();
                                    }
                                }
                                // string s = grdTwentyFive.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyFive, dtTreatment);
                            break;
                        case 26:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySix").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySix").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentySix.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentySix"] = dtTreatment;
                            grdTwentySix.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentySix);

                            //for (int i = 0; i < grdTwentySix.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentySix.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentySix.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentySix.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentySix.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentySix.Columns[1].Visible = false;
                                        grdTwentySix.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentySix.Columns[7].Visible = false;
                                        grdTwentySix.DataBind();
                                    }
                                }
                                // string s = grdTwentySix.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentySix, dtTreatment);
                            break;
                        case 27:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySeven").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySeven").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentySeven.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentySeven"] = dtTreatment;
                            grdTwentySeven.DataBind();

                            // setColumnAccordingScheduleType(ref grdTwentySeven);

                            //for (int i = 0; i < grdTwentySeven.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentySeven.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentySeven.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTwentySeven.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentySeven.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentySeven.Columns[1].Visible = false;
                                        grdTwentySeven.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentySeven.Columns[7].Visible = false;
                                        grdTwentySeven.DataBind();
                                    }
                                }
                                // string s = grdTwentySeven.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentySeven, dtTreatment);
                            break;
                        case 28:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyEight").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyEight").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyEight.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyEight"] = dtTreatment;
                            grdTwentyEight.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyEight);

                            //for (int i = 0; i < grdTwentyEight.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyEight.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyEight.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTwentyEight.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyEight.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyEight.Columns[1].Visible = false;
                                        grdTwentyEight.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyEight.Columns[7].Visible = false;
                                        grdTwentyEight.DataBind();
                                    }
                                }
                                // string s = grdTwentyEight.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyEight, dtTreatment);
                            break;
                        case 29:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyNine").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyNine").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyNine.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyNine"] = dtTreatment;
                            grdTwentyNine.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyNine);

                            //for (int i = 0; i < grdTwentyNine.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyNine.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyNine.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyNine.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyNine.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyNine.Columns[1].Visible = false;
                                        grdTwentyNine.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyNine.Columns[7].Visible = false;
                                        grdTwentyNine.DataBind();
                                    }
                                }
                                // string s = grdTwentyNine.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyNine, dtTreatment);
                            break;

                        case 30:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirty").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirty").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirty.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirty"] = dtTreatment;
                            grdThirty.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirty);

                            //for (int i = 0; i < grdThirty.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirty.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirty.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirty.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirty.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirty.Columns[1].Visible = false;
                                        grdThirty.DataBind();
                                    }
                                    else
                                    {
                                        grdThirty.Columns[7].Visible = false;
                                        grdThirty.DataBind();
                                    }
                                }
                                // string s = grdThirty.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirty, dtTreatment);
                            break;

                        case 31:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyOne").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyOne").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyOne.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyOne"] = dtTreatment;
                            grdThirtyOne.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirtyOne);

                            //for (int i = 0; i < grdThirtyOne.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyOne.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyOne.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyOne.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyOne.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyOne.Columns[1].Visible = false;
                                        grdThirtyOne.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyOne.Columns[7].Visible = false;
                                        grdThirtyOne.DataBind();
                                    }
                                }
                                // string s = grdThirtyOne.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyOne, dtTreatment);
                            break;

                        case 32:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyTwo").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyTwo").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyTwo.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyTwo"] = dtTreatment;
                            grdThirtyTwo.DataBind();

                            // setColumnAccordingScheduleType(ref grdThirtyTwo);

                            //for (int i = 0; i < grdThirtyTwo.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyTwo.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyTwo.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyTwo.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyTwo.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyTwo.Columns[1].Visible = false;
                                        grdThirtyTwo.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyTwo.Columns[7].Visible = false;
                                        grdThirtyTwo.DataBind();
                                    }
                                }
                                // string s = grdThirtyTwo.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyTwo, dtTreatment);
                            break;

                        case 33:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyThree").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyThree").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyThree.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyThree"] = dtTreatment;
                            grdThirtyThree.DataBind();

                            // setColumnAccordingScheduleType(ref grdThirtyThree);

                            //for (int i = 0; i < grdThirtyThree.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyThree.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyThree.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyThree.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyThree.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyThree.Columns[1].Visible = false;
                                        grdThirtyThree.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyThree.Columns[7].Visible = false;
                                        grdThirtyThree.DataBind();
                                    }
                                }
                                // string s = grdThirtyThree.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyThree, dtTreatment);
                            break;

                        case 34:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFour").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFour").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyFour.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyFour"] = dtTreatment;
                            grdThirtyFour.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirtyFour);

                            //for (int i = 0; i < grdThirtyFour.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyFour.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyFour.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyFour.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyFour.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyFour.Columns[1].Visible = false;
                                        grdThirtyFour.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyFour.Columns[7].Visible = false;
                                        grdThirtyFour.DataBind();
                                    }
                                }
                                // string s = grdThirtyFour.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyFour, dtTreatment);
                            break;

                        case 35:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFive").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFive").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyFive.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyFive"] = dtTreatment;
                            grdThirtyFive.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirtyFive);

                            //for (int i = 0; i < grdThirtyFive.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyFive.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyFive.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyFive.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyFive.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyFive.Columns[1].Visible = false;
                                        grdThirtyFive.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyFive.Columns[7].Visible = false;
                                        grdThirtyFive.DataBind();
                                    }
                                }
                                // string s = grdThirtyFive.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyFive, dtTreatment);
                            break;
                    }
                }


            }

        }

        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
        //}

        //setColumnAccordingScheduleType(ref grdOne);
    }
    //private void setColumnAccordingScheduleType(ASPxGridView p_objDataGrid)
    //{
    //    ASPxGridView objDataGrid = p_objDataGrid;
    //    try
    //    {
    //        if (objDataGrid.> 0)
    //        {
    //            if (((ASPxTextBox)objDataGrid.Items[0].FindControl("txtScheduleType")).Text == "IN")
    //            {
    //                objDataGrid.Columns[1].Visible = false; // OFFICE NAME
    //            }

    //            else
    //            {
    //                objDataGrid.Columns[7].Visible = false; // VISIT TYPE NAME
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}
    private class Calendar_DAO
    {
        private string szInitDisplayMonth = null;
        private string szControlIDPrefix = null;
        private string szInitDisplayYear = null;

        public string InitialDisplayYear
        {
            get
            {
                return szInitDisplayYear;
            }
            set
            {
                szInitDisplayYear = value;
            }
        }

        public string InitialDisplayMonth
        {
            get
            {
                return szInitDisplayMonth;
            }
            set
            {
                szInitDisplayMonth = value;
            }
        }

        public string ControlIDPrefix
        {
            get
            {
                return szControlIDPrefix;
            }
            set
            {
                szControlIDPrefix = value;
            }
        }
    }

    //protected void btnXlsExport_Click(object sender, EventArgs e)
    //{
    //    string i = tabVistInformation.ActiveTabPage.ToString();
    //    int j = i.IndexOf("(");
    //    string m = i.Substring(j);
    //    i = i.Replace(m, "").Trim();

    //    if (i == "Orthopedic")
    //    {
    //        DataTable dtgrdOne = new DataTable();
    //        dtgrdOne = (DataTable)ViewState["VSgrdOne"];

    //        grdOne.DataSource = dtgrdOne;
    //        grdOne.DataBind();

    //        grdExportOne.WriteXlsToResponse();
    //    }
    //    if (i == "Physiotheropy")
    //    {
    //        DataTable dtgrdTwo = new DataTable();
    //        dtgrdTwo = (DataTable)ViewState["VSgrdTwo"];

    //        grdTwo.DataSource = dtgrdTwo;
    //        grdTwo.DataBind();

    //        grdExportTwo.WriteXlsToResponse();
    //    }
    //    if (i == "DULOSCOPY")
    //    {
    //        DataTable dtgrdThree = new DataTable();
    //        dtgrdThree = (DataTable)ViewState["VSgrdThree"];

    //        grdThree.DataSource = dtgrdThree;
    //        grdThree.DataBind();

    //        grdExportThree.WriteXlsToResponse();
    //    }
    //    if (i == "CT-SCAN")
    //    {
    //        DataTable dtgrdFour = new DataTable();
    //        dtgrdFour = (DataTable)ViewState["VSgrdFour"];

    //        grdFour.DataSource = dtgrdFour;
    //        grdFour.DataBind();

    //        grdExportFour.WriteXlsToResponse();
    //    }

    //    if (i == "CH")
    //    {
    //        DataTable dtgrdFive = new DataTable();
    //        dtgrdFive = (DataTable)ViewState["VSgrdFive"];

    //        grdFive.DataSource = dtgrdFive;
    //        grdFive.DataBind();
    //        grdExportFive.WriteXlsToResponse();
    //    }
    //    if (i == "PT")
    //    {
    //        DataTable dtgrdSix = new DataTable();
    //        dtgrdSix = (DataTable)ViewState["VSgrdSix"];

    //        grdSix.DataSource = dtgrdSix;
    //        grdSix.DataBind();

    //        grdExportSix.WriteXlsToResponse();
    //    }
    //    if (i == "AC")
    //    {
    //        DataTable dtgrdSeven = new DataTable();
    //        dtgrdSeven = (DataTable)ViewState["VSgrdSeven"];

    //        grdSeven.DataSource = dtgrdSeven;
    //        grdSeven.DataBind();

    //        grdExportSeven.WriteXlsToResponse();
    //    }
    //    if (i == "ROM")
    //    {
    //        DataTable dtgrdEight = new DataTable();
    //        dtgrdEight = (DataTable)ViewState["VSgrdEight"];

    //        grdEight.DataSource = dtgrdEight;
    //        grdEight.DataBind();

    //        grdExportEight.WriteXlsToResponse();
    //    }
    //    if (i == "GreenBills Diagnostic Facility-CT-SCAN")
    //    {
    //        DataTable dtgrdNine = new DataTable();
    //        dtgrdNine = (DataTable)ViewState["VSgrdNine"];

    //        grdNine.DataSource = dtgrdNine;
    //        grdNine.DataBind();

    //        grdExportNine.WriteXlsToResponse();
    //    }
    //    if (i == "GreenBills Diagnostic Facility-MRI")
    //    {
    //        DataTable dtgrdTen = new DataTable();
    //        dtgrdTen = (DataTable)ViewState["VSgrdTen"];

    //        grdTen.DataSource = dtgrdTen;
    //        grdTen.DataBind();

    //        grdExportTen.WriteXlsToResponse();
    //    }
    //    if (i == "GreenBills Diagnostic Facility-XRAY")
    //    {
    //        DataTable dtgrdEleven = new DataTable();
    //        dtgrdEleven = (DataTable)ViewState["VSgrdEleven"];

    //        grdEleven.DataSource = dtgrdEleven;
    //        grdEleven.DataBind();

    //        grdExportEleven.WriteXlsToResponse();

    //    }



    //    else
    //    {

    //    }



    //}

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Bill_Sys_Att_SearchCase.aspx", false);
    }

    protected void btnXlsExportOne_Click(object sender, EventArgs e)
    {
        DataTable dtgrdOne = new DataTable();
        dtgrdOne = (DataTable)ViewState["VSgrdOne"];

        grdOne.DataSource = dtgrdOne;
        grdOne.DataBind();

        grdExportOne.WriteXlsToResponse();
    }
    protected void btnXlsExportTwo_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwo = new DataTable();
        dtgrdTwo = (DataTable)ViewState["VSgrdTwo"];

        grdTwo.DataSource = dtgrdTwo;
        grdTwo.DataBind();

        grdExportTwo.WriteXlsToResponse();
    }
    protected void btnXlsExportThree_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThree = new DataTable();
        dtgrdThree = (DataTable)ViewState["VSgrdThree"];

        grdThree.DataSource = dtgrdThree;
        grdThree.DataBind();

        grdExportThree.WriteXlsToResponse();
    }
    protected void btnXlsExportFour_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFour = new DataTable();
        dtgrdFour = (DataTable)ViewState["VSgrdFour"];

        grdFour.DataSource = dtgrdFour;
        grdFour.DataBind();

        grdExportFour.WriteXlsToResponse();
    }
    protected void btnXlsExportFive_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFive = new DataTable();
        dtgrdFive = (DataTable)ViewState["VSgrdFive"];

        grdFive.DataSource = dtgrdFive;
        grdFive.DataBind();

        grdExportFive.WriteXlsToResponse();
    }
    protected void btnXlsExportSix_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSix = new DataTable();
        dtgrdSix = (DataTable)ViewState["VSgrdSix"];

        grdSix.DataSource = dtgrdSix;
        grdSix.DataBind();

        grdExportSix.WriteXlsToResponse();
    }
    protected void btnXlsExportSeven_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSeven = new DataTable();
        dtgrdSeven = (DataTable)ViewState["VSgrdSeven"];

        grdSeven.DataSource = dtgrdSeven;
        grdSeven.DataBind();

        grdExportSeven.WriteXlsToResponse();
    }
    protected void btnXlsExportEight_Click(object sender, EventArgs e)
    {
        DataTable dtgrdEight = new DataTable();
        dtgrdEight = (DataTable)ViewState["VSgrdEight"];

        grdEight.DataSource = dtgrdEight;
        grdEight.DataBind();

        grdExportEight.WriteXlsToResponse();
    }
    protected void btnXlsExportNine_Click(object sender, EventArgs e)
    {
        DataTable dtgrdNine = new DataTable();
        dtgrdNine = (DataTable)ViewState["VSgrdNine"];

        grdNine.DataSource = dtgrdNine;
        grdNine.DataBind();

        grdExportNine.WriteXlsToResponse();
    }
    protected void btnXlsExportTen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTen = new DataTable();
        dtgrdTen = (DataTable)ViewState["VSgrdTen"];

        grdTen.DataSource = dtgrdTen;
        grdTen.DataBind();

        grdExportTen.WriteXlsToResponse();
    }
    protected void btnXlsExportEleven_Click(object sender, EventArgs e)
    {
        DataTable dtgrdEleven = new DataTable();
        dtgrdEleven = (DataTable)ViewState["VSgrdEleven"];

        grdEleven.DataSource = dtgrdEleven;
        grdEleven.DataBind();

        grdExportEleven.WriteXlsToResponse();
    }
    protected void btnXlsExportTwelve_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwelve = new DataTable();
        dtgrdTwelve = (DataTable)ViewState["VSgrdTwelve"];

        grdTwelve.DataSource = dtgrdTwelve;
        grdTwelve.DataBind();

        grdExportTwelve.WriteXlsToResponse();
    }
    protected void btnXlsExportThirteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirteen = new DataTable();
        dtgrdThirteen = (DataTable)ViewState["VSgrdThirteen"];

        grdThirteen.DataSource = dtgrdThirteen;
        grdThirteen.DataBind();

        grdExportThirteen.WriteXlsToResponse();
    }
    protected void btnXlsExportFourteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFourteen = new DataTable();
        dtgrdFourteen = (DataTable)ViewState["VSgrdFourteen"];

        grdFourteen.DataSource = dtgrdFourteen;
        grdFourteen.DataBind();

        grdExportFourteen.WriteXlsToResponse();
    }
    protected void btnXlsExportFifteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFifteen = new DataTable();
        dtgrdFifteen = (DataTable)ViewState["VSgrdFifteen"];

        grdFifteen.DataSource = dtgrdFifteen;
        grdFifteen.DataBind();

        grdExportFifteen.WriteXlsToResponse();
    }
    protected void btnXlsExportSixteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSixteen = new DataTable();
        dtgrdSixteen = (DataTable)ViewState["VSgrdSixteen"];

        grdSixteen.DataSource = dtgrdSixteen;
        grdSixteen.DataBind();

        grdExportSixteen.WriteXlsToResponse();
    }
    protected void btnXlsExportSeventeen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSeventeen = new DataTable();
        dtgrdSeventeen = (DataTable)ViewState["VSgrdSeventeen"];

        grdSeventeen.DataSource = dtgrdSeventeen;
        grdSeventeen.DataBind();

        grdExportSeventeen.WriteXlsToResponse();
    }
    protected void btnXlsExportEighteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdEighteen = new DataTable();
        dtgrdEighteen = (DataTable)ViewState["VSgrdEighteen"];

        grdEighteen.DataSource = dtgrdEighteen;
        grdEighteen.DataBind();

        grdExportEighteen.WriteXlsToResponse();
    }
    protected void btnXlsExportNineteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdNineteen = new DataTable();
        dtgrdNineteen = (DataTable)ViewState["VSgrdNineteen"];

        grdNineteen.DataSource = dtgrdNineteen;
        grdNineteen.DataBind();

        grdExportNineteen.WriteXlsToResponse();
    }
    protected void btnXlsExportTwenty_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwenty = new DataTable();
        dtgrdTwenty = (DataTable)ViewState["VSgrdTwenty"];

        grdTwenty.DataSource = dtgrdTwenty;
        grdTwenty.DataBind();

        grdExportTwenty.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentyOne_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyOne = new DataTable();
        dtgrdTwentyOne = (DataTable)ViewState["VSgrdTwentyOne"];

        grdTwentyOne.DataSource = dtgrdTwentyOne;
        grdTwentyOne.DataBind();

        grdExportTwentyOne.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentyTwo_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyTwo = new DataTable();
        dtgrdTwentyTwo = (DataTable)ViewState["VSgrdTwentyTwo"];

        grdTwentyTwo.DataSource = dtgrdTwentyTwo;
        grdTwentyTwo.DataBind();

        grdExportTwentyTwo.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentyThree_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyThree = new DataTable();
        dtgrdTwentyThree = (DataTable)ViewState["VSgrdTwentyThree"];

        grdTwentyThree.DataSource = dtgrdTwentyThree;
        grdTwentyThree.DataBind();

        grdExportTwentyThree.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentyFour_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyFour = new DataTable();
        dtgrdTwentyFour = (DataTable)ViewState["VSgrdTwentyFour"];

        grdTwentyFour.DataSource = dtgrdTwentyFour;
        grdTwentyFour.DataBind();

        grdExportTwentyFour.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentyFive_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyFive = new DataTable();
        dtgrdTwentyFive = (DataTable)ViewState["VSgrdTwentyFive"];

        grdTwentyFive.DataSource = dtgrdTwentyFive;
        grdTwentyFive.DataBind();

        grdExportTwentyFive.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentySix_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentySix = new DataTable();
        dtgrdTwentySix = (DataTable)ViewState["VSgrdTwentySix"];

        grdTwentySix.DataSource = dtgrdTwentySix;
        grdTwentySix.DataBind();

        grdExportTwentySix.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentySeven_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentySeven = new DataTable();
        dtgrdTwentySeven = (DataTable)ViewState["VSgrdTwentySeven"];

        grdTwentySeven.DataSource = dtgrdTwentySeven;
        grdTwentySeven.DataBind();

        grdExportTwentySeven.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentyEight_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyEight = new DataTable();
        dtgrdTwentyEight = (DataTable)ViewState["VSgrdTwentyEight"];

        grdTwentyEight.DataSource = dtgrdTwentyEight;
        grdTwentyEight.DataBind();

        grdExportTwentyEight.WriteXlsToResponse();
    }
    protected void btnXlsExportTwentyNine_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyNine = new DataTable();
        dtgrdTwentyNine = (DataTable)ViewState["VSgrdTwentyNine"];

        grdTwentyNine.DataSource = dtgrdTwentyNine;
        grdTwentyNine.DataBind();

        grdExportTwentyNine.WriteXlsToResponse();
    }
    protected void btnXlsExportThirty_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirty = new DataTable();
        dtgrdThirty = (DataTable)ViewState["VSgrdThirty"];

        grdThirty.DataSource = dtgrdThirty;
        grdThirty.DataBind();

        grdExportThirty.WriteXlsToResponse();
    }
    protected void btnXlsExportThirtyOne_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyOne = new DataTable();
        dtgrdThirtyOne = (DataTable)ViewState["VSgrdThirtyOne"];

        grdThirtyOne.DataSource = dtgrdThirtyOne;
        grdThirtyOne.DataBind();

        grdExportThirtyOne.WriteXlsToResponse();
    }
    protected void btnXlsExportThirtyTwo_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyTwo = new DataTable();
        dtgrdThirtyTwo = (DataTable)ViewState["VSgrdThirtyTwo"];

        grdThirtyTwo.DataSource = dtgrdThirtyTwo;
        grdThirtyTwo.DataBind();

        grdExportThirtyTwo.WriteXlsToResponse();
    }
    protected void btnXlsExportThirtyThree_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyThree = new DataTable();
        dtgrdThirtyThree = (DataTable)ViewState["VSgrdThirtyThree"];

        grdThirtyThree.DataSource = dtgrdThirtyThree;
        grdThirtyThree.DataBind();

        grdExportThirtyThree.WriteXlsToResponse();
    }
    protected void btnXlsExportThirtyFour_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyFour = new DataTable();
        dtgrdThirtyFour = (DataTable)ViewState["VSgrdThirtyFour"];

        grdThirtyFour.DataSource = dtgrdThirtyFour;
        grdThirtyFour.DataBind();

        grdExportThirtyFour.WriteXlsToResponse();
    }
    protected void btnXlsExportThirtyFive_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyFive = new DataTable();
        dtgrdThirtyFive = (DataTable)ViewState["VSgrdThirtyFive"];

        grdThirtyFive.DataSource = dtgrdThirtyFive;
        grdThirtyFive.DataBind();

        grdExportThirtyFive.WriteXlsToResponse();
    }




}
