using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using System.IO;
using GeneralTools;
using MG2;
using MG2PDF.DataAccessObject;
using iTextSharp.text;
using gbmodel = gb.mbs.da.model;
using gbservices = gb.mbs.da.services;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using Microsoft.ApplicationBlocks.Data;



public partial class PrintCaseWiseDetails : System.Web.UI.Page
{
    string UserId = "";
    string CaseId = "";
    string companyId = "";
    string PatientID = "";
    string sz_Node_ID, sz_NodeName, sz_CompanyID, sz_CaseID, Logicalpath, sz_UserID, sz_doctorID, sz_Caseno, sz_speciality = "", CheckDC = "", tempPath = "";
    string strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    ArrayList list = new ArrayList();
    ArrayList obj = new ArrayList();
    ArrayList objMg2 = new ArrayList();
    MBS_CASEWISE_MG1 oj = new MBS_CASEWISE_MG1();
    MBS_CASEWISE_MG11 obj11 = new MBS_CASEWISE_MG11();
    Bill_Sys_NF3_Template objNF3Template;


    protected void Page_Load(object sender, EventArgs e)
    {
        UserId = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
        companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        PatientID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_ID;
        if (!IsPostBack)
        {
            #region code copy from Bill_Sys_MG2.aspx Page Load

            btnClear.Attributes.Add("OnClick", "return Clear()");
            btnClearBottom.Attributes.Add("OnClick", "return Clear()");
            string CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();

            extddlSpeciality.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Bill_Sys_DoctorBO _obj1 = new Bill_Sys_DoctorBO();

            DataSet dsDoctorName = _obj1.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            DDLAttendingDoctors.DataSource = dsDoctorName;
            //ListItem objLI = new ListItem("---select---", "NA");
            DDLAttendingDoctors.DataTextField = "DESCRIPTION";
            DDLAttendingDoctors.DataValueField = "CODE";
            DDLAttendingDoctors.DataBind();
            DDLAttendingDoctors.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Doctor--", "NA"));
            Session["I_ID"] = null;


            Bill_Sys_DoctorBO _obj2 = new Bill_Sys_DoctorBO();
            DataSet dsDoctorName1 = _obj2.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            ddlAttendingDoctor.DataSource = dsDoctorName1;
            ddlAttendingDoctor.DataTextField = "DESCRIPTION";
            ddlAttendingDoctor.DataValueField = "CODE";
            ddlAttendingDoctor.DataBind();
            ddlAttendingDoctor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Doctor--", "NA"));
            Session["I_ID"] = null;

            Bill_Sys_DoctorBO _obj3 = new Bill_Sys_DoctorBO();
            DataSet dsDoctorName11 = _obj3.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            ddlDoctor_Name_MG11.DataSource = dsDoctorName11;
            ddlDoctor_Name_MG11.DataTextField = "DESCRIPTION";
            ddlDoctor_Name_MG11.DataValueField = "CODE";
            ddlDoctor_Name_MG11.DataBind();
            ddlDoctor_Name_MG11.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Doctor--", "NA"));
            Session["I_ID"] = null;

            Bill_Sys_DoctorBO _obj4 = new Bill_Sys_DoctorBO();
            DataSet dsDoctorName21 = _obj4.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            ddlMG2Doctor.DataSource = dsDoctorName21;
            ddlMG2Doctor.DataTextField = "DESCRIPTION";
            ddlMG2Doctor.DataValueField = "CODE";
            ddlMG2Doctor.DataBind();
            ddlMG2Doctor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Doctor--", "NA"));
            Session["I_ID"] = null;

            LoadData();
            LoadDataMG1();
            LoadDataMG11();

            #endregion

            DataSet ds = GetNF2Details(CaseId);
            DataSet dsNF2 = GetNF2SavedDetails(CaseId);
            AjaxControlToolkit.TabPanel tabpnl = (AjaxControlToolkit.TabPanel)tabcontainerPatientEntry.FindControl("tabPnlNF2");
            if (dsNF2.Tables.Count > 0)
            {
                if (dsNF2.Tables[0].Rows.Count > 0)
                {
                    btnNF2Print.Enabled = true;
                    for (int i = 0; i < dsNF2.Tables[0].Rows.Count; i++)
                    {
                        if (dsNF2.Tables[0].Rows[i]["s_control_type"].ToString() == "textbox")
                        {
                            pdftextbox.PDFTextBox txtbx = (pdftextbox.PDFTextBox)tabpnl.FindControl(dsNF2.Tables[0].Rows[i]["s_control_name"].ToString());
                            if (txtbx != null)
                            {
                                txtbx.Text = dsNF2.Tables[0].Rows[i]["s_value"].ToString();
                            }
                        }
                        else if (dsNF2.Tables[0].Rows[i]["s_control_type"].ToString() == "checkbox")
                        {
                            pdfcheckbox.PDFCheckBox chkbx = (pdfcheckbox.PDFCheckBox)tabpnl.FindControl(dsNF2.Tables[0].Rows[i]["s_control_name"].ToString());
                            if (chkbx != null)
                            {
                                if (dsNF2.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                                {
                                    chkbx.Checked = true;
                                }
                            }
                        }
                        else if (dsNF2.Tables[0].Rows[i]["s_control_type"].ToString() == "radiobutton")
                        {
                            pdfradiobutton.PDFRadioButton chkbx = (pdfradiobutton.PDFRadioButton)tabpnl.FindControl(dsNF2.Tables[0].Rows[i]["s_control_name"].ToString());
                            if (chkbx != null)
                            {
                                if (dsNF2.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                                {
                                    chkbx.Checked = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ds != null)
                    {
                        btnNF2Print.Enabled = false;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txt_NF2_Insurer_Name.Text = ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                            txt_NF2_Insurer_Address.Text = ds.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString();
                            txt_NF2_Insurer_Claim.Text = ds.Tables[0].Rows[0]["SZ_INSURER_CLAIM_REPRESENTATIVE"].ToString();
                            txt_NF2_Patient_Name.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString();
                            txt_NF2_Patient_Address.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                            txt_NF2_Date.Text = ds.Tables[0].Rows[0]["DT_TODAY_DATE"].ToString();
                            txt_NF2_Policy_Holder.Text = ds.Tables[0].Rows[0]["SZ_POLICY_HOLDER"].ToString();
                            txt_NF2_Policy_Number.Text = ds.Tables[0].Rows[0]["SZ_POLICY_NUMBER"].ToString();
                            txt_NF2_Name.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_NAME"].ToString();
                            txt_NF2_PhoneNo.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
                            txt_NF2_HomeNo.Text = ds.Tables[0].Rows[0]["SZ_HOME_PHONE"].ToString();
                            txt_NF2_Business.Text = ds.Tables[0].Rows[0]["SZ_WORK_PHONE"].ToString();
                            txt_NF2_Address.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
                            txt_NF2_Date_Of_Birth.Text = ds.Tables[0].Rows[0]["DT_BIRTH_DATE"].ToString();
                            txt_NF2_SSN.Text = ds.Tables[0].Rows[0]["SZ_SSN"].ToString();
                            txt_NF2_Date_Time_Of_Accident.Text = ds.Tables[0].Rows[0]["DT_ACCIDENT_DATE"].ToString();
                            txtAccidentTime.Text = ds.Tables[0].Rows[0]["DT_ACCIDENT_TIME"].ToString();
                            txt_NF2_Place_Of_Accident.Text = ds.Tables[0].Rows[0]["SZ_PLACE_OF_ACCIDENT"].ToString();
                            txt_NF2_Accident_Description.Text = ds.Tables[0].Rows[0]["SZ_BRIEF_DESCRIPTION"].ToString();
                            txt_NF2_Injury.Text = ds.Tables[0].Rows[0]["SZ_DESCRIBE_INJURY"].ToString();
                            txt_NF2_Owner_Name.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_POLICY_NAME"].ToString();

                            if (ds.Tables[0].Rows[0]["CHK_YES_DRIVERMOTORVEHICLE"].ToString() == "True")
                            {
                                chkVehicleYes.Checked = true;
                            }
                            else
                            {
                                chkVehicleNo.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["CHK_YES_PASSENGERMOTORVEHICLE"].ToString() == "True")
                            {
                                chkPassengerYes.Checked = true;
                            }
                            else
                            {
                                chkPassengerNo.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["CHK_YES_PEDESTRIAN"].ToString() == "True")
                            {
                                chkPedestrianYes.Checked = true;
                            }
                            else
                            {
                                chkPedestrianNo.Checked = true;
                            }

                            txtCompanyName.Text = ds.Tables[0].Rows[0]["SZ_COMPANY_NAME"].ToString();
                            txtCompanyAddress.Text = ds.Tables[0].Rows[0]["SZ_COMPANY_ADDRESS"].ToString();

                            //if (ds.Tables[0].Rows[0]["CHK_OUT_PATIENT"].ToString() == "True")
                            //{
                            //    chkPatientYes.Checked = true;
                            //}
                            //else
                            //{
                            //    chkPatientNo.Checked = true;
                            //}

                            txt_NF2_DateOfAdmission.Text = ds.Tables[0].Rows[0]["SZ_DATE_OF_ADMISSION"].ToString();
                            txt_NF2_HospitalAddress.Text = ds.Tables[0].Rows[0]["SZ_HOSPITAL_ADDRESS"].ToString();
                            txt_NF2_Amount.Text = ds.Tables[0].Rows[0]["SZ_AMOUNT_HEALTHBILL"].ToString();

                            txt_NF2_LoseTime.Text = ds.Tables[0].Rows[0]["SZ_DID_YOU_LOOSETIME"].ToString();
                            txt_NF2_AbsenceDate.Text = ds.Tables[0].Rows[0]["DT_ABSENCEFROM_WORK_BEGIN"].ToString();
                            txt_NF2_Work.Text = ds.Tables[0].Rows[0]["SZ_RETURNED_TO_WORK"].ToString();
                            txt_NF2_DateReturnedToWork.Text = ds.Tables[0].Rows[0]["DT_RETURN_TO_WORK"].ToString();
                            txt_NF2_AmountOfTime.Text = ds.Tables[0].Rows[0]["SZ_AMOUNT_OF_TIME"].ToString();
                            txt_NF2_Average.Text = ds.Tables[0].Rows[0]["FLT_GROSS_WEEKLY_EARNINGS"].ToString();
                            txt_NF2_NumberOfDays.Text = ds.Tables[0].Rows[0]["I_NUMBEROFDAYS_WORK_WEEKLY"].ToString();
                            txt_NF2_NumberOfHours.Text = ds.Tables[0].Rows[0]["I_NUMBEROFHOURS_WORK_WEEKLY"].ToString();

                            //if (ds.Tables[0].Rows[0]["CHK_YES_RECIEVING_UNEMPLOYMENT"].ToString() == "True")
                            //{
                            //    chkUnemployeementYes.Checked = true;
                            //}
                            //else
                            //{
                            //    chkUnemployeementNo.Checked = true;
                            //}

                            txt_NF2_Employer.Text = ds.Tables[0].Rows[0]["SZ_EMPLOYERONE_NAMEADDRESS"].ToString();
                            txt_NF2_Occupation.Text = ds.Tables[0].Rows[0]["SZ_EMPLOYERONE_OCCUPATION"].ToString();
                            txt_NF2_From.Text = ds.Tables[0].Rows[0]["SZ_EMPLOYERONE_FROM"].ToString();
                            txt_NF2_To.Text = ds.Tables[0].Rows[0]["SZ_EMPLOYERONE_TO"].ToString();
                        }
                    }
                }
            }

        }
    }

    public void FindTheControls(ref List<Control> foundSofar, Control parent, ref List<Control> leafControl)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.HasControls()) //Or whatever that is you checking for 
            {
                foundSofar.Add(c);
                if (c.Controls.Count > 0)
                {
                    this.FindTheControls(ref foundSofar, c, ref leafControl);
                }
            }
            else
            {
                if (c.GetType() == typeof(pdftextbox.PDFTextBox))
                {
                    pdftextbox.PDFTextBox p = (pdftextbox.PDFTextBox)c;
                    list.Add(p);
                    obj.Add(p);
                    objMg2.Add(p);
                }
                if (c.GetType() == typeof(pdfcheckbox.PDFCheckBox))
                {
                    pdfcheckbox.PDFCheckBox p = (pdfcheckbox.PDFCheckBox)c;
                    list.Add(p);
                    obj.Add(p);
                    objMg2.Add(p);
                }

                if (c.GetType() == typeof(pdfradiobutton.PDFRadioButton))
                {
                    pdfradiobutton.PDFRadioButton r = (pdfradiobutton.PDFRadioButton)c;
                    list.Add(r);
                    obj.Add(r);
                    objMg2.Add(r);
                }

                if (c.GetType() == typeof(pdftextbox.PDFTextBox)
                    || c.GetType() == typeof(pdfcheckbox.PDFCheckBox)
                    || c.GetType() == typeof(pdfradiobutton.PDFRadioButton))
                {
                    leafControl.Add(c);
                }
            }
        }
    }
    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }

    public DataSet GetMG2GridDetails(string szCaseId)
    {
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG2_DETAIL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        ASPxGridView1.DataSource = ds;
            //        //ASPxGridView1.KeyFieldName = "CreatedBy";
            //        ASPxGridView1.DataBind();
            //    }
            //}
        }
        catch (SqlException _ex)
        {
            throw new Exception(_ex.Message.ToString());
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

    protected void tabcontainerPatientEntry_ActiveTabChanged(object sender, EventArgs e)
    {
        string caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        DataSet dsAvailableMg2 = GetMG2GridDetails(caseID);

        DataSet dsAvailableMG1 = oj.GetMG1GridDetails(caseID);

        DataSet dsAvailableMG11 = obj11.GetMG11GridDetails(caseID);

        MBS_CASEWISE_MG21 obj2 = new MBS_CASEWISE_MG21();
        DataSet dsAvailableMG21 = obj2.GetMG21GridDetails(caseID);
        ASPxGridView4.DataSource = dsAvailableMG21;
        ASPxGridView4.DataBind();

        int index = tabcontainerPatientEntry.ActiveTabIndex;
        if (index == 1)
        {
            {

                extddlDoctor.Flag_ID = companyId;
                List<Control> foundSofar = new List<Control>();
                List<Control> leafControl = new List<Control>();

                FormsBO oFormBO = new FormsBO();
                DataSet ds = new DataSet();
                ds = oFormBO.loadData(CaseId);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BtnPrint.Enabled = true;
                    AjaxControlToolkit.TabPanel tabpnl = (AjaxControlToolkit.TabPanel)tabcontainerPatientEntry.FindControl("tabC4Auth");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["s_control_type"].ToString() == "textbox")
                        {


                            pdftextbox.PDFTextBox txtbx = (pdftextbox.PDFTextBox)tabpnl.FindControl(ds.Tables[0].Rows[i]["s_control_name"].ToString());
                            //pdftextbox.PDFTextBox txtbx = (pdftextbox.PDFTextBox)tabpnl.FindControl(ds.Tables[0].Rows[i]["s_control_name"].ToString());
                            if (txtbx != null)
                            {
                                txtbx.Text = ds.Tables[0].Rows[i]["s_value"].ToString();
                            }
                        }
                        else if (ds.Tables[0].Rows[i]["s_control_type"].ToString() == "checkbox")
                        {
                            pdfcheckbox.PDFCheckBox chkbx = (pdfcheckbox.PDFCheckBox)tabpnl.FindControl(ds.Tables[0].Rows[i]["s_control_name"].ToString());
                            if (chkbx != null)
                            {
                                if (ds.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                                {
                                    chkbx.Checked = true;
                                }
                            }
                        }
                        else if (ds.Tables[0].Rows[i]["s_control_type"].ToString() == "radiobutton")
                        {
                            pdfradiobutton.PDFRadioButton chkbx = (pdfradiobutton.PDFRadioButton)tabpnl.FindControl(ds.Tables[0].Rows[i]["s_control_name"].ToString());
                            if (chkbx != null)
                            {
                                if (ds.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                                {
                                    chkbx.Checked = true;
                                }
                            }
                        }

                    }

                    extddlDoctor.Text = Txt_C4_AttendingDrName.Text;
                    Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    DataSet patientInfo = _bill_Sys_PatientBO.GetPatientInfo(PatientID, companyId);
                    if (patientInfo.Tables[0].Rows.Count > 0)
                    {
                    }
                    }
                else
                {
                    BtnPrint.Enabled = false;
                    Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
                    DataSet patientInfo = _bill_Sys_PatientBO.GetPatientInfo(PatientID, companyId);
                    if (patientInfo.Tables[0].Rows.Count > 0)
                    {
                        Txt_C4_Patient_Name.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() + patientInfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                        Txt_C4_SSN.Text = patientInfo.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                        Txt_C4_Patient_Address.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString() + "   " + patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STREET"].ToString() + "   ," + patientInfo.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString() + "   ," + patientInfo.Tables[0].Rows[0]["SZ_PATIENT_STATE_NAME"].ToString() + "   ," + patientInfo.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                        Txt_C4_Emp_Name.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString();
                        Txt_C4_Emp_Address.Text = patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString() + "   " + patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString() + "   ," + patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString() + "   ," + patientInfo.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                        Txt_C4_Ins_Carrier_Name.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString();
                        Txt_C4_Ins_Address.Text = patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString() + "   " + patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString() + "   ," + patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString() + "   ," + patientInfo.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                        Txt_C4_DateOfBirth.Text = patientInfo.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString();
                        Txt_C4_wcb_case_number.Text = patientInfo.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
                        Txt_C4_Carrier_case_number.Text = patientInfo.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();
                    }
                }
            }
        }


        if (index == 2)
        {
            LoadDataMG1();  // MG1
        }
        if (index == 3)
        {
            loadMG11Doctor(); // MG1.1
            LoadDataMG11();
        }
        if (index == 4)
        {
            LoadData();  //MG2
        }
        if (index == 5)
        {
            loadMG2Doctor();   //MG2.1
            loadMG21TabData();
        }

        if (index == 6)
        {
            loadMG2Doctor();   // FOR GRID TAB
            loadMG21TabData();
        }

        //else if (index == 3)
        //{

        //    List<Control> foundSofar = new List<Control>();
        //    List<Control> leafControl = new List<Control>();

        //    extddlDoctormg2.Flag_ID = companyId;
        //    FormsBO oFormBO = new FormsBO();
        //    DataSet ds = new DataSet();
        //    ds = oFormBO.getMG2Data(CaseId);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        btnPrintMG2.Enabled = true;
        //        Txt_MG2_Flag.Text = "Update";
        //        AjaxControlToolkit.TabPanel tabpnl = (AjaxControlToolkit.TabPanel)tabcontainerPatientEntry.FindControl("tabPnlMG2");
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (ds.Tables[0].Rows[i]["s_control_type"].ToString() == "textbox")
        //            {
        //                pdftextbox.PDFTextBox txtbx = (pdftextbox.PDFTextBox)tabpnl.FindControl(ds.Tables[0].Rows[i]["s_control_name"].ToString());
        //                if (txtbx != null)
        //                {
        //                    txtbx.Text = ds.Tables[0].Rows[i]["s_value"].ToString();
        //                }
        //            }
        //            else if (ds.Tables[0].Rows[i]["s_control_type"].ToString() == "checkbox")
        //            {
        //                pdfcheckbox.PDFCheckBox chkbx = (pdfcheckbox.PDFCheckBox)tabpnl.FindControl(ds.Tables[0].Rows[i]["s_control_name"].ToString());
        //                if (chkbx != null)
        //                {
        //                    if (ds.Tables[0].Rows[i]["s_value"].ToString() == "checked")
        //                    {
        //                        chkbx.Checked = true;
        //                    }
        //                }
        //            }
        //            else if (ds.Tables[0].Rows[i]["s_control_type"].ToString() == "radiobutton")
        //            {
        //                pdfradiobutton.PDFRadioButton chkbx = (pdfradiobutton.PDFRadioButton)tabpnl.FindControl(ds.Tables[0].Rows[i]["s_control_name"].ToString());
        //                if (chkbx != null)
        //                {
        //                    if (ds.Tables[0].Rows[i]["s_value"].ToString() == "checked")
        //                    {
        //                        chkbx.Checked = true;
        //                    }
        //                }
        //            }

        //        }
        //        extddlDoctormg2.Text = txt_MG2_DoctorsID.Text;
        //    }
        //    else
        //    {
        //        btnPrintMG2.Enabled = false;
        //        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        //        DataSet patientInfo = _bill_Sys_PatientBO.GetPatientInfo(PatientID, companyId);
        //        if (patientInfo.Tables[0].Rows.Count > 0)
        //        {
        //            txt_MG2_PatientName.Text = patientInfo.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString() + patientInfo.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
        //            txt_Mg2_WCBCaseNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
        //            //txt_MG2_DoctorsName.Text = "";
        //            txt_MG2_DoctorsAuthorizationNo.Text = "";
        //            txt_Mg2_CarrierCaseNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_CARRIER_CASE_NO"].ToString();
        //            txt_Mg2_DateofJourney.Text = patientInfo.Tables[0].Rows[0]["DT_INJURY"].ToString();
        //            txt_Mg2_SecurityNumber.Text = patientInfo.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
        //        }
        //    }
        //}
        //else if (index == 2) // MG2  added Date: 17_Aug_2016
        //{
        //    // GetRecord();
        //    //GetPatientDetails();
        //    //getDoctorDefaultList();
        //    ddlNewMG2Doctor.Flag_ID = companyId;
        //    GetMG2PatientDetails();
        //}

    }

    #region C4-Auth
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string OutputFilePath = "";
            string OpenPath = "";
            objNF3Template = new Bill_Sys_NF3_Template();
            
            OpenPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";

            string BaseFilePath = ConfigurationManager.AppSettings["C4AUTHPATH"].ToString();
            objNF3Template = new Bill_Sys_NF3_Template();

          //  String szBasePhysicalPath = objNF3Template.getPhysicalPath();
          //  string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
            string BasePath = objNF3Template.getPhysicalPath();
            BasePath = BasePath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
            string newPdfFilename = "";
            newPdfFilename = "C4_Auth_" + getFileName(CaseId) + ".pdf";
            OutputFilePath = BasePath + newPdfFilename;
            OpenPath = OpenPath + newPdfFilename;
            FormsBO oFormBO = new FormsBO();
            DataSet dsPdfValue = oFormBO.loadData(CaseId);
            PdfReader pdfReader = new PdfReader(BaseFilePath);
            //PdfReader.unethicalreading = true; 
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            if (dsPdfValue.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsPdfValue.Tables[0].Rows.Count; i++)
                {
                    if (dsPdfValue.Tables[0].Rows[i]["s_control_type"].ToString() == "textbox")
                    {
                        pdfFormFields.SetField(dsPdfValue.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), dsPdfValue.Tables[0].Rows[i]["s_value"].ToString());

                    }
                    else if (dsPdfValue.Tables[0].Rows[i]["s_control_type"].ToString() == "checkbox")
                    {

                        if (dsPdfValue.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                        {
                            pdfFormFields.SetField(dsPdfValue.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), "1");
                        }
                    }
                    else if (dsPdfValue.Tables[0].Rows[i]["s_control_type"].ToString() == "radiobutton")
                    {

                        if (dsPdfValue.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                        {
                            pdfFormFields.SetField(dsPdfValue.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), "1");
                        }
                    }
                }
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + "');", true);
            }
        }
        catch (Exception ex)
        {
            usrMSGC4.PutMessage(ex.ToString());
            usrMSGC4.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMSGC4.Show();
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            List<Control> foundSofar = new List<Control>();
            List<Control> leafControl = new List<Control>();


            for (int i = 0; i < this.tabC4Auth.Controls.Count; i++)
            {
                Control control = this.tabC4Auth.Controls[i];
                FindTheControls(ref foundSofar, control, ref leafControl);
            }

            ArrayList arr = new ArrayList();
            FormsBO ObjForm = new FormsBO();
            Txt_C4_AttendingDrName.Text = extddlDoctor.Text;
            if (extddlDoctor.Text != "NA")
            {
                Txt_C4_AttendingDrName_SelectedText.Text = extddlDoctor.Selected_Text;
            }
            else
            {
                Txt_C4_AttendingDrName_SelectedText.Text = "";
            }
            bool blnToBeAdded = false;
            for (int j = 0; j < leafControl.Count; j++)
            {
                FormsBO obj2 = new FormsBO();
                obj2.i_case_id = CaseId;
                obj2.sz_company_id = companyId;
                obj2.s_control_name = leafControl[j].ID;

                if (leafControl[j].GetType() == typeof(pdftextbox.PDFTextBox))
                {
                    pdftextbox.PDFTextBox oTextBox = (pdftextbox.PDFTextBox)obj[j];
                    obj2.s_control_type = "textbox";
                    obj2.s_value = oTextBox.Text;
                    if (oTextBox.Text.Trim().Length == 0)
                        blnToBeAdded = false;
                    else
                        blnToBeAdded = true;
                    obj2.s_pdf_control_name = oTextBox.AssociatedPDFControlName;
                    obj2.sz_user_id = UserId;
                }
                else
                {
                    if (leafControl[j].GetType() == typeof(pdfcheckbox.PDFCheckBox))
                    {
                        pdfcheckbox.PDFCheckBox oCheckBox = (pdfcheckbox.PDFCheckBox)obj[j];
                        obj2.s_control_type = "checkbox";
                        if (oCheckBox.Checked)
                        {
                            obj2.s_value = "checked";
                            blnToBeAdded = true;
                        }
                        else
                        {
                            obj2.s_value = "unchecked";
                            blnToBeAdded = false;
                        }
                        obj2.s_pdf_control_name = oCheckBox.AssociatedPDFControlName;
                        obj2.sz_user_id = UserId;
                    }
                    else
                    {
                        if (leafControl[j].GetType() == typeof(pdfradiobutton.PDFRadioButton))
                        {
                            pdfradiobutton.PDFRadioButton oRadio = (pdfradiobutton.PDFRadioButton)obj[j];
                            obj2.s_control_type = "radiobutton";
                            if (oRadio.Checked)
                            {
                                obj2.s_value = "checked";
                                blnToBeAdded = true;
                            }
                            else
                            {
                                obj2.s_value = "unchecked";
                                blnToBeAdded = false;
                            }
                            obj2.s_pdf_control_name = oRadio.AssociatedPDFControlName;
                            obj2.sz_user_id = UserId;
                        }
                    }
                }
                if (blnToBeAdded)
                    arr.Add(obj2);
            }
            ObjForm.deleteC4ByCaseID(arr);
            ObjForm.saveC4Pdf(arr);
            BtnPrint.Enabled = true;
            //MessageBox.ShowMessage("Information Saved Successfully !!");
            usrMSGC4.PutMessage("Record Saved Successfully");
            usrMSGC4.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMSGC4.Show();
        }
        catch (Exception ex)
        {
            usrMSGC4.PutMessage(ex.ToString());
            usrMSGC4.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMSGC4.Show();
        }

    }

    protected void extddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string DoctorID = "";
        DoctorID = extddlDoctor.Text;
        FormsBO oFormBO = new FormsBO();
        DataSet ds = new DataSet();
        ds = oFormBO.GetDoctorInfo(DoctorID);
        if (ds.Tables[0].Rows.Count == 0)
        {
            Txt_C4_Doc_Address.Text = "";
            Txt_C4_Doc_Provider_Auth_No.Text = "";
            Txt_C4_TelePhn_No.Text = "";
            Txt_C4_Fax_No.Text = "";
        }
        else
        {
            Txt_C4_Doc_Address.Text = ds.Tables[0].Rows[0]["SZ_OFFICE_ADDRESS"].ToString() + "   " + ds.Tables[0].Rows[0]["SZ_OFFICE_CITY"].ToString() + "   ," + ds.Tables[0].Rows[0]["SZ_OFFICE_STATE"].ToString() + "   ," + ds.Tables[0].Rows[0]["SZ_OFFICE_ZIP"].ToString();
            Txt_C4_Doc_Provider_Auth_No.Text = ds.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
            Txt_C4_TelePhn_No.Text = ds.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString();
            Txt_C4_Fax_No.Text = ds.Tables[0].Rows[0]["SZ_OFFICE_FAX"].ToString();
        }

    }
    #endregion

    #region NF-2
    private DataSet GetNF2Details(string sz_billNumber)
    {
        SqlConnection sqlConnection = new SqlConnection();
        try
        {
            sqlConnection = new SqlConnection(strsqlCon);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SP_NF2_TEMPLATE";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@SZ_BILL_ID", sz_billNumber);
            sqlCommand.Parameters.AddWithValue("@FLAG", "");

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally { sqlConnection.Close(); }
    }

    protected void btnAddNF2Details_Click(object sender, EventArgs e)
    {
        try
        {

            List<Control> foundSofar = new List<Control>();
            List<Control> leafControl = new List<Control>();
            FormsBO formDetail = new FormsBO();
            ArrayList arrList = new ArrayList();
            ArrayList arrListAdd = new ArrayList();
            for (int i = 0; i < this.tabPnlNF2.Controls.Count; i++)
            {
                Control control = this.tabPnlNF2.Controls[i];
                FindTheControls(ref foundSofar, control, ref leafControl);
            }

            bool blnToBeAdded = false;
            for (int j = 0; j < leafControl.Count; j++)
            {
                if (leafControl[j].GetType() == typeof(pdftextbox.PDFTextBox))
                {
                    formDetail = new FormsBO();
                    pdftextbox.PDFTextBox oTextBox = (pdftextbox.PDFTextBox)list[j];
                    formDetail.i_case_id = CaseId;
                    formDetail.sz_company_id = companyId;
                    formDetail.sz_user_id = UserId;
                    formDetail.s_control_name = leafControl[j].ID;
                    formDetail.s_control_type = "textbox";
                    formDetail.s_value = oTextBox.Text;
                    if (oTextBox.Text.Trim().Length == 0)
                        blnToBeAdded = false;
                    else
                        blnToBeAdded = true;
                    formDetail.s_pdf_control_name = oTextBox.AssociatedPDFControlName;
                }
                else
                {
                    if (leafControl[j].GetType() == typeof(pdfcheckbox.PDFCheckBox))
                    {
                        pdfcheckbox.PDFCheckBox oCheckBox = (pdfcheckbox.PDFCheckBox)list[j];

                        formDetail = new FormsBO();
                        formDetail.i_case_id = CaseId;
                        formDetail.sz_company_id = companyId;
                        formDetail.sz_user_id = UserId;
                        formDetail.s_control_name = leafControl[j].ID;
                        if (oCheckBox.Checked)
                        {
                            formDetail.s_value = "checked";
                            blnToBeAdded = true;
                        }
                        else
                        {
                            formDetail.s_value = "unchecked";
                            blnToBeAdded = false;
                        }
                        formDetail.s_control_type = "checkbox";
                        formDetail.s_pdf_control_name = oCheckBox.AssociatedPDFControlName;
                    }
                    else
                    {
                        if (leafControl[j].GetType() == typeof(pdfradiobutton.PDFRadioButton))
                        {
                            pdfradiobutton.PDFRadioButton oRadio = (pdfradiobutton.PDFRadioButton)list[j];

                            formDetail = new FormsBO();
                            formDetail.i_case_id = CaseId;
                            formDetail.sz_company_id = companyId;
                            formDetail.sz_user_id = UserId;
                            formDetail.s_control_name = leafControl[j].ID;

                            if (oRadio.Checked)
                            {
                                formDetail.s_value = "checked";
                                blnToBeAdded = true;
                            }
                            else
                            {
                                formDetail.s_value = "unchecked";
                                blnToBeAdded = false;
                            }

                            formDetail.s_control_type = "radiobutton";
                            formDetail.s_pdf_control_name = oRadio.AssociatedPDFControlName;
                        }
                    }
                }
                if (blnToBeAdded)
                    arrList.Add(formDetail);
            }
            DeleteExistingNF2Detais(CaseId, companyId);
            SaveNF2FormDetails(arrList);
            btnNF2Print.Enabled = true;
            usrMsgNF2.PutMessage("Record Saved Successfully");
            usrMsgNF2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMsgNF2.Show();
        }
        catch (Exception ex)
        {
            usrMsgNF2.PutMessage(ex.Message);
            usrMsgNF2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMsgNF2.Show();
        }
    }

    private void SaveNF2FormDetails(ArrayList arrList)
    {
        SqlTransaction sqlTransaction = null;
        SqlConnection sqlConnection = new SqlConnection();

        try
        {
            sqlConnection = new SqlConnection(strsqlCon);
            sqlConnection.Open();
            sqlTransaction = sqlConnection.BeginTransaction();
            for (int j = 0; j < arrList.Count; j++)
            {
                FormsBO objFormsBO = new FormsBO();
                objFormsBO = (FormsBO)arrList[j];


                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = "sp_save_nf2_form_details";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = 0;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.Parameters.AddWithValue("@i_case_id", objFormsBO.i_case_id);
                sqlCommand.Parameters.AddWithValue("@sz_company_id", objFormsBO.sz_company_id);
                sqlCommand.Parameters.AddWithValue("@s_control_name", objFormsBO.s_control_name);
                sqlCommand.Parameters.AddWithValue("@s_value", objFormsBO.s_value);
                sqlCommand.Parameters.AddWithValue("@s_control_type", objFormsBO.s_control_type);
                sqlCommand.Parameters.AddWithValue("@sz_user_id", objFormsBO.sz_user_id);
                sqlCommand.Parameters.AddWithValue("@s_pdf_control_name", objFormsBO.s_pdf_control_name);
                sqlCommand.Parameters.AddWithValue("@flag", "ADD");
                sqlCommand.ExecuteNonQuery();
            }
            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            throw ex;
        }

        finally
        {
            sqlConnection.Close();
        }
    }

    private DataSet GetNF2SavedDetails(string CaseId)
    {
        SqlConnection sqlConnection = new SqlConnection();
        try
        {
            sqlConnection = new SqlConnection(strsqlCon);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "SP_GET_NF2_DETAILS_FROM_CASEID";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", CaseId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally { sqlConnection.Close(); }
    }

    protected void btnNF2Print_Click(object sender, EventArgs e)
    {
        try
        {
            string OutputFilePath = "";
            string OpenPath = "";
            OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";
            string BaseFilePath = ConfigurationManager.AppSettings["NF2_Temp"].ToString();
            string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
            BasePath = BasePath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
            string newPdfFilename = "";
            newPdfFilename = "NF2_" + getFileName(CaseId) + ".pdf";
            OutputFilePath = BasePath + newPdfFilename;
            OpenPath = OpenPath + newPdfFilename;
            FormsBO oFormBO = new FormsBO();
            DataSet dsPdfValue = GetNF2SavedDetails(CaseId);
            PdfReader pdfReader = new PdfReader(BaseFilePath);
            //PdfReader.unethicalreading = true; 
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            if (dsPdfValue.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsPdfValue.Tables[0].Rows.Count; i++)
                {
                    if (dsPdfValue.Tables[0].Rows[i]["s_control_type"].ToString() == "textbox")
                    {
                        pdfFormFields.SetField(dsPdfValue.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), dsPdfValue.Tables[0].Rows[i]["s_value"].ToString());

                    }
                    else if (dsPdfValue.Tables[0].Rows[i]["s_control_type"].ToString() == "checkbox")
                    {

                        if (dsPdfValue.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                        {
                            pdfFormFields.SetField(dsPdfValue.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), "Yes");
                        }
                    }
                    else if (dsPdfValue.Tables[0].Rows[i]["s_control_type"].ToString() == "radiobutton")
                    {

                        if (dsPdfValue.Tables[0].Rows[i]["s_value"].ToString() == "checked")
                        {
                            pdfFormFields.SetField(dsPdfValue.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), "Yes");
                        }
                    }
                }
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + "');", true);
            }
        }
        catch (Exception ex)
        {
            usrMsgNF2.PutMessage(ex.Message);
            usrMsgNF2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMsgNF2.Show();
        }
    }

    private void DeleteExistingNF2Detais(string CaseId, string companyId)
    {
        SqlTransaction sqlTransaction = null;
        SqlConnection sqlConnection = new SqlConnection();

        try
        {
            sqlConnection = new SqlConnection(strsqlCon);
            sqlConnection.Open();
            sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "sp_delete_nf2_exixting_form_details";
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Transaction = sqlTransaction;
            sqlCommand.Parameters.AddWithValue("@i_case_id", CaseId);
            sqlCommand.Parameters.AddWithValue("@sz_company_id", companyId);
            sqlCommand.ExecuteNonQuery();

            sqlTransaction.Commit();
        }
        catch (Exception ex)
        {
            sqlTransaction.Rollback();
            throw ex;
        }

        finally
        {
            sqlConnection.Close();
        }
    }
    #endregion

    #region MG2.1
    //protected void extddlDoctormg2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string DoctorIDmg2 = "";
    //    DoctorIDmg2 = extddlDoctormg2.Text;
    //    FormsBO oFormBO = new FormsBO();
    //    DataSet dsDoctor = new DataSet();
    //    dsDoctor = oFormBO.GetDoctorInfo(DoctorIDmg2);
    //    if (dsDoctor.Tables[0].Rows.Count == 0)
    //    {
    //        txt_MG2_DoctorsAuthorizationNo.Text = "";
    //    }
    //    else
    //    {
    //        txt_MG2_DoctorsAuthorizationNo.Text = dsDoctor.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
    //    }

    //}

    //protected void btnPrintMG2_Click(object sender, EventArgs e)
    //{
    //    try
    //    { 
    //    string OutputFilePath = "";
    //    string OpenPath = "";
    //    OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";
    //    string BaseFilePath = ConfigurationManager.AppSettings["MG2PATH"].ToString();
    //    string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
    //    BasePath = BasePath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";
    //    OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";
    //    if (!Directory.Exists(BasePath))
    //    {
    //        Directory.CreateDirectory(BasePath);
    //    }
    //    string newPdfFilename = "";
    //    newPdfFilename = "MG2_" + getFileName(CaseId) + ".pdf";
    //    OpenPath = OpenPath + newPdfFilename;
    //    OutputFilePath = BasePath + newPdfFilename;
    //    FormsBO oFormBO = new FormsBO();
    //    DataSet dsMG2PdfData = oFormBO.getMG2Data(CaseId);
    //    PdfReader pdfReaderMg2 = new PdfReader(BaseFilePath);
    //    //PdfReader.unethicalreading = true; 
    //    PdfStamper pdfStamperMg2 = new PdfStamper(pdfReaderMg2, new FileStream(OutputFilePath, FileMode.Create));
    //    AcroFields pdfFormFields = pdfStamperMg2.AcroFields;
    //    if (dsMG2PdfData.Tables[0].Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dsMG2PdfData.Tables[0].Rows.Count; i++)
    //        {
    //            if (dsMG2PdfData.Tables[0].Rows[i]["s_control_type"].ToString() == "textbox")
    //            {
    //                pdfFormFields.SetField(dsMG2PdfData.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), dsMG2PdfData.Tables[0].Rows[i]["s_value"].ToString());

    //            }
    //            else if (dsMG2PdfData.Tables[0].Rows[i]["s_control_type"].ToString() == "checkbox")
    //            {

    //                if (dsMG2PdfData.Tables[0].Rows[i]["s_value"].ToString() == "checked")
    //                {
    //                    pdfFormFields.SetField(dsMG2PdfData.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), "1");
    //                }
    //            }
    //            else if (dsMG2PdfData.Tables[0].Rows[i]["s_control_type"].ToString() == "radiobutton")
    //            {

    //                if (dsMG2PdfData.Tables[0].Rows[i]["s_value"].ToString() == "checked")
    //                {
    //                    pdfFormFields.SetField(dsMG2PdfData.Tables[0].Rows[i]["s_pdf_control_name"].ToString(), "1");
    //                }
    //            }
    //        }
    //        pdfStamperMg2.FormFlattening = true;
    //        pdfStamperMg2.Close();
    //        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + "');", true);

    //    }
    //    }
    //    catch (Exception ex)
    //    {
    //        UsermsgMG21.PutMessage(ex.ToString());
    //        UsermsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        UsermsgMG21.Show();

    //    }
    //}

    //protected void btnSaveMG2_Click(object sender, EventArgs e)
    //{
    //    try
    //    { 
    //    List<Control> foundSofar = new List<Control>();
    //    List<Control> leafControl = new List<Control>();

    //    for (int i = 0; i < this.tabPnlMG2.Controls.Count; i++)
    //    {
    //        Control control = this.tabPnlMG2.Controls[i];
    //        FindTheControls(ref foundSofar, control, ref leafControl);
    //    }

    //    ArrayList arr = new ArrayList();
    //    FormsBO ObjForm = new FormsBO();
    //    txt_MG2_DoctorsID.Text = extddlDoctormg2.Text;
    //    if (extddlDoctormg2.Text != "NA")
    //    {
    //        txt_MG2_DoctorsName.Text = extddlDoctormg2.Selected_Text;
    //    }
    //    else
    //    {
    //        txt_MG2_DoctorsName.Text = "";
    //    }

    //    bool blnToBeAdded = false;
    //    for (int j = 0; j < leafControl.Count; j++)
    //    {
    //        FormsBO obj2 = new FormsBO();
    //        obj2.i_case_id = CaseId;
    //        obj2.sz_company_id = companyId;
    //        obj2.s_control_name = leafControl[j].ID;

    //        if (leafControl[j].GetType() == typeof(pdftextbox.PDFTextBox))
    //        {
    //            pdftextbox.PDFTextBox oTextBox = (pdftextbox.PDFTextBox)objMg2[j];
    //            obj2.s_control_type = "textbox";
    //            obj2.s_value = oTextBox.Text;
    //            if (oTextBox.Text.Trim().Length == 0)
    //                blnToBeAdded = false;
    //            else
    //                blnToBeAdded = true;

    //            obj2.s_pdf_control_name = oTextBox.AssociatedPDFControlName;
    //            obj2.sz_user_id = UserId;
    //        }
    //        else if (leafControl[j].GetType() == typeof(pdfcheckbox.PDFCheckBox))
    //        {
    //            if (leafControl[j].GetType() == typeof(pdfcheckbox.PDFCheckBox))
    //            {
    //                pdfcheckbox.PDFCheckBox oCheckBox = (pdfcheckbox.PDFCheckBox)objMg2[j];
    //                obj2.s_control_type = "checkbox";
    //                if (oCheckBox.Checked)
    //                {
    //                    obj2.s_value = "checked";
    //                    blnToBeAdded = true;
    //                }
    //                else
    //                {
    //                    obj2.s_value = "unchecked";
    //                    blnToBeAdded = false;
    //                }
    //                obj2.s_pdf_control_name = oCheckBox.AssociatedPDFControlName;
    //                obj2.sz_user_id = UserId;
    //            }
    //        }
    //        else
    //        {
    //            if (leafControl[j].GetType() == typeof(pdfradiobutton.PDFRadioButton))
    //            {
    //                pdfradiobutton.PDFRadioButton oRadio = (pdfradiobutton.PDFRadioButton)objMg2[j];
    //                if (oRadio.Checked)
    //                {
    //                    obj2.s_value = "checked";
    //                    blnToBeAdded = true;
    //                }
    //                else
    //                {
    //                    obj2.s_value = "unchecked";
    //                    blnToBeAdded = false;
    //                }

    //                obj2.s_control_type = "radiobutton";
    //                obj2.s_pdf_control_name = oRadio.AssociatedPDFControlName;
    //                obj2.sz_user_id = UserId;
    //            }
    //        }
    //        if (blnToBeAdded)
    //            arr.Add(obj2);
    //    }

    //    ObjForm.saveMG2Pdf(arr);
    //    UsermsgMG21.PutMessage("Record Saved Successfully");
    //    UsermsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
    //    UsermsgMG21.Show();
    //    btnPrintMG2.Enabled = true;
    //    }
    //    catch(Exception ex)
    //    {
    //        UsermsgMG21.PutMessage(ex.ToString());
    //        UsermsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        UsermsgMG21.Show();
    //    }

    //}
    #endregion

    #region MG2 Print Date: 17-08-2016
    public void GetMG2PatientDetails()
    {

        try
        {

            Bill_Sys_PatientBO oj = new Bill_Sys_PatientBO();
            DataSet ds = oj.GetPatientInfo(PatientID, companyId);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TxtFirstName.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
                    TxtMiddleName.Text = ds.Tables[0].Rows[0]["MI"].ToString();
                    TxtLastName.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
                    TxtPatientAddress.Text = ds.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_PATIENT_STREET"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_PATIENT_STATE_NAME"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_PATIENT_ZIP"].ToString();
                    TxtSocialSecurityNo.Text = ds.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                    TxtEmployerNameAdd.Text = ds.Tables[0].Rows[0]["SZ_EMPLOYER_NAME"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_EMPLOYER_ADDRESS"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_EMPLOYER_CITY"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_EMPLOYER_STATE"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_EMPLOYER_ZIP"].ToString();
                    TxtInsuranceNameAdd.Text = ds.Tables[0].Rows[0]["SZ_INSURANCE_NAME"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_INSURANCE_ADDRESS"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_INSURANCE_CITY"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_INSURANCE_STATE"].ToString() + " ," + ds.Tables[0].Rows[0]["SZ_INSURANCE_ZIP"].ToString();
                    Txtwcbcasenumber.Text = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();
                    TxtWCBCaseNumber2.Text = ds.Tables[0].Rows[0]["SZ_WCB_NO"].ToString();

                    if (ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString() != "" && ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString() != "01/01/1900")
                    {
                        TxtDateofInjury.Text = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString();
                        TxtDateofInjury2.Text = ds.Tables[0].Rows[0]["DT_DATE_OF_ACCIDENT"].ToString();
                    }

                    else
                    {
                        TxtDateofInjury.Text = "";
                        TxtDateofInjury2.Text = "";
                    }

                    TxtPatientName.Text = TxtFirstName.Text + " " + TxtMiddleName.Text + " " + TxtLastName.Text;
                    TxtCarrierCaseNo.Text = ds.Tables[0].Rows[0]["SZ_CLAIM_NUMBER"].ToString();


                    TxtPatientAddress.Text = TxtPatientAddress.Text.Trim().Replace(", ,", ",");
                    TxtEmployerNameAdd.Text = TxtEmployerNameAdd.Text.Trim().Replace(", ,", ",");
                    TxtInsuranceNameAdd.Text = TxtInsuranceNameAdd.Text.Trim().Replace(",,", ",");


                }
            }
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
        finally
        {

        }


    }
    public void save_mg2_details()
    {
        try
        {
            gbmodel.patient.form.MG2 objMG2 = new gbmodel.patient.form.MG2();

            objMG2.Employer = new gbmodel.employer.Employer();
            objMG2.Carrier = new gbmodel.carrier.Carrier();
            objMG2.Provider = new gbmodel.provider.Provider();
            objMG2.Patient = new gbmodel.patient.Patient();
            objMG2.Patient.Address = new gbmodel.common.Address();


            gbmodel.account.Account oAccount = new gbmodel.account.Account();
            oAccount.ID = companyId;

            objMG2.Account = new gbmodel.account.Account();
            objMG2.Account.ID = oAccount.ID;


            objMG2.Patient.CaseID = Convert.ToInt32(CaseId);

            objMG2.User = new gbmodel.user.User();
            objMG2.User.ID = UserId;

            objMG2.Doctor = new gbmodel.physician.Physician();
            //objMG2.Doctor.Name = ddlNewMG2Doctor.Text;

            objMG2.ApprovalRequest = TxtApproval.Text;
            objMG2.DateOfService = TxtDateofService.Text;
            objMG2.DatesOfDeniedRequest = TxtDateofApplicable.Text;
            if (Chkdid.Checked)
                objMG2.ChkDid = "1";
            else
                objMG2.ChkDid = "0";
            if (Chkdidnot.Checked)
                objMG2.ChkDidNot = "1";
            else
                objMG2.ChkDidNot = "0";
            objMG2.ContactDate = TxtSpoke.Text;
            objMG2.PersonContacted = Txtspecktoanyone.Text;

            if (ChkAcopy.Checked)
                objMG2.ChkCopySent = "1";
            else
                objMG2.ChkCopySent = "0";
            objMG2.FaxEmail = TxtAddressRequired.Text;

            if (ChkIAmnot.Checked)
                objMG2.ChkCopyNotSent = "1";
            else
                objMG2.ChkCopyNotSent = "0";
            objMG2.IndicatedFaxEmail = Txtaboveon.Text;
            objMG2.Provider.SignDate = TxtProviderDate.Text;

            if (ChktheSelf.Checked)
                objMG2.ChkNoticeGiven = "1";
            else
                objMG2.ChkNoticeGiven = "0";
            objMG2.PrintCarrierEmployerNoticeName = TxtByPrintNameD.Text;
            objMG2.NoticeTitle = TxtTitleD.Text;
            objMG2.NoticeCarrierSignDate = TxtDateD.Text;
            objMG2.CarrierDenial = TxtSectionE.Text;

            if (ChkGranted.Checked)
                objMG2.ChkGranted = "1";
            else
                objMG2.ChkGranted = "0";
            if (ChkGrantedinPart.Checked)
                objMG2.ChkGrantedInParts = "1";
            else
                objMG2.ChkGrantedInParts = "0";
            if (ChkWithoutPrejudice.Checked)
                objMG2.ChkWithoutPrejudice = "1";
            else
                objMG2.ChkWithoutPrejudice = "0";
            if (ChkDenied.Checked)
                objMG2.ChkDenied = "1";
            else
                objMG2.ChkDenied = "0";
            if (ChkBurden.Checked)
                objMG2.ChkBurden = "1";
            else
                objMG2.ChkBurden = "0";
            if (ChkSubstantially.Checked)
                objMG2.ChkSubstantiallySimilar = "1";
            else
                objMG2.ChkSubstantiallySimilar = "0";
            objMG2.MedicalProfessional = TxtMedicalProfes.Text;
            if (ChkMadeE.Checked)
                objMG2.ChkMedicalArbitrator = "1";
            else
                objMG2.ChkMedicalArbitrator = "0";
            if (ChkChairE.Checked)
                objMG2.ChkWCBHearing = "1";
            else
                objMG2.ChkWCBHearing = "0";
            objMG2.PrintCarrierEmployerResponseName = TxtByPrintNameE.Text;
            objMG2.ResponseTitle = TxtTitleE.Text;
            //cmd.Parameters.AddWithValue("@sz_signature_E",TxtSignatureE .Text );
            objMG2.ResponseCarrierSignDate = TxtDateE.Text;

            objMG2.PrintDenialCarrierName = TxtByPrintNameF.Text;
            objMG2.DenialTitle = TxtTitleF.Text;
            //cmd.Parameters.AddWithValue("@sz_signature_F", TxtSignatureF.Text);
            objMG2.DenialCarrierSignDate = TxtDateF.Text;


            if (ChkIrequestG.Checked)
                objMG2.ChkRequestWC = "1";
            else
                objMG2.ChkRequestWC = "0";
            if (ChkMadeG.Checked)
                objMG2.ChkMedicalArbitratorByWC = "1";
            else
                objMG2.ChkMedicalArbitratorByWC = "0";
            if (ChkChairG.Checked)
                objMG2.ChkWCBHearingByWC = "1";
            else
                objMG2.ChkWCBHearingByWC = "0";
            //cmd.Parameters.AddWithValue("@sz_claimant_signature",TxtClairmantSignature .Text );
            objMG2.ClaimantSignDate = TxtClaimantDate.Text;

            objMG2.Patient.WCBNumber = Txtwcbcasenumber.Text;
            objMG2.Carrier.CaseNumber = TxtCarrierCaseNo.Text;
            objMG2.Patient.DOA = TxtDateofInjury.Text;
            objMG2.Patient.FirstName = TxtFirstName.Text;
            objMG2.Patient.MiddleName = TxtMiddleName.Text;
            objMG2.Patient.LastName = TxtLastName.Text;
            objMG2.Patient.Address.AddressLines = TxtPatientAddress.Text;
            objMG2.Employer.Name = TxtEmployerNameAdd.Text;
            objMG2.Carrier.Name = TxtInsuranceNameAdd.Text;
            //objMG2.Doctor.Name = ddlNewMG2Doctor.Selected_Text;//DDLAttendingDoctors.Text
            objMG2.Provider.WCBNumber = TxtIndividualProvider.Text;
            objMG2.Doctor.PhoneNo = TxtTelephone.Text;
            objMG2.Doctor.FaxNo = TxtFaxNo.Text;


            //objMG2.guidelineSection = ddlGuidline.SelectedItem.Text;
            if (ddlGuidline.SelectedItem.Text != "--Select--")
            {
                ArrayList ar = new ArrayList();
                string spt = ddlGuidline.SelectedItem.Text;
                string[] wrd = spt.Split('-');
                foreach (string word in wrd)
                {
                    ar.Add(word);
                }
                objMG2.BodyInitial = ar[0].ToString();
                objMG2.GuidelineSection = ar[1].ToString();
            }
            else
            {
                objMG2.BodyInitial = TxtGuislineChar.Text;

                if (TxtGuidline1.Text == "")
                    TxtGuidline1.Text = " ";
                if (TxtGuidline2.Text == "")
                    TxtGuidline2.Text = " ";
                if (TxtGuidline3.Text == "")
                    TxtGuidline3.Text = " ";
                if (TxtGuidline4.Text == "")
                    TxtGuidline4.Text = " ";
                if (TxtGuidline5.Text == "")
                    TxtGuidline5.Text = " ";

                objMG2.GuidelineSection = TxtGuidline1.Text + "" + TxtGuidline2.Text + "" + TxtGuidline3.Text + "" + TxtGuidline4.Text + "" + TxtGuidline5.Text; ;
            }
            objMG2.Patient.SSN = TxtSocialSecurityNo.Text;
            //objMG2.sz_BillNo = sz_BillNo; -- not required in this situation
            objMG2.Patient.Patient_ID = PatientID;


            //MBS_MG2 oj = new MBS_MG2();
            //oj.save_MG2_details(objMG2);

            gbservices.patient.form.SrvMG2 srvMG2 = new gbservices.patient.form.SrvMG2();
            srvMG2.Create(objMG2);

            usrMessage.PutMessage("Record Saved Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }

    }
    private void PrintMG2FormsPDF()
    {

        try
        {
            string sReturnfilepath = "";
            string OpenPath = "";
            OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + CaseId + "/Packet Document/";

            gbmodel.account.Account oAccount = new gbmodel.account.Account();
            oAccount.ID = companyId;
            oAccount.Name = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
            gbmodel.patient.Patient oPatient = new gbmodel.patient.Patient();
            oPatient.CaseID = Convert.ToInt32(CaseId);
            gbservices.patient.form.SrvMG2 srvMG2 = new gbservices.patient.form.SrvMG2();

            DataSet dsMG2 = new DataSet();
            dsMG2 = srvMG2.Select(oPatient, oAccount);

            if (dsMG2 != null && dsMG2.Tables[0].Rows.Count > 0)
            {
                sReturnfilepath = srvMG2.Print(oAccount, oPatient);
            }

            else
            {
                save_mg2_details();
                sReturnfilepath = srvMG2.Print(oAccount, oPatient);
            }

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + sReturnfilepath + "');", true);
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }

    }
    protected void btnMG2NewSaveBottom_Click(object sender, EventArgs e)
    {
        try
        {
            save_mg2_details();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }

    }
    protected void btnMG2NewPrinBottom_Click(object sender, EventArgs e)
    {
        try
        {
            PrintMG2FormsPDF();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
    }
    protected void btnMG2Newsave_Click(object sender, EventArgs e)
    {
        try
        {
            save_mg2_details();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
    }
    protected void btnPrintMG2New_Click(object sender, EventArgs e)
    {
        try
        {
            PrintMG2FormsPDF();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
    }

    #endregion

    protected void btnSaveTopMG1_Click(object sender, EventArgs e)
    {
        try
        {
            SaveMG1Data();
        }
        catch (Exception ex)
        {
            MessageControl2.PutMessage(ex.Message);
            MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            MessageControl2.Show();
        }

    }
    private void SaveMG1Data()
    {
        int I_ID = 0;
        if (Session["I_ID"] != null)
        {
            I_ID = Convert.ToInt32(Session["I_ID"].ToString());
        }
        //NEW CODE

        string bodyInitial, guidelineSection = "";
        string sz_Caseno = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        string sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        string sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        string sz_UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        AddMG1CasewiseDAO objMG1 = new AddMG1CasewiseDAO();


        objMG1.sz_CompanyID = sz_CompanyID;
        objMG1.sz_CaseID = sz_CaseID;
        objMG1.sz_UserID = sz_UserID;

        objMG1.date_request_submitted = txtMG1DateSubmitted.Text;
        objMG1.ProcedureRequest = txtMG1Procedure.Text;
        objMG1.dateOfService = txtMG1DateOfService.Text;
        objMG1.datesOfDeniedRequest = TxtDateofApplicable.Text;
        objMG1.sz_comments = txtMG1Comments.Text;


        if (chkMG1Approval.Checked)
            objMG1.chkDid = "Yes";
        else
            objMG1.chkDid = "No";


        if (ChkMG1DidNotContact.Checked)
            objMG1.chkDidNot = "Yes";
        else
            objMG1.chkDidNot = "No";
        objMG1.contactDate = txtMG1TelephoneTwo.Text;
        objMG1.personContacted = txtMG1SpokenTo.Text;

        if (chkMG1CopyOfForm.Checked)
            objMG1.chkCopySent = "Yes";
        else
            objMG1.chkCopySent = "No";
        objMG1.providerSignDate = txtMG1DateC.Text;    //DateC

        objMG1.faxEmail = txtMG1CopyOfForm.Text;

        //D.
        if (chkMG1Granted.Checked)
            objMG1.chkGranted = "1";
        else
            objMG1.chkGranted = "0";

        if (chkMG1GrantedPrejudice.Checked)
            objMG1.chkWithoutPrejudice = "1";
        else
            objMG1.chkWithoutPrejudice = "0";
        if (chkMG1DeniedDenial.Checked)
            objMG1.chkDenied = "1";
        else
            objMG1.chkDenied = "0";

        objMG1.CarrierOnReverse = txtCarrierReverse.Text;

        objMG1.medicalProfessional = txtMG1MedicalProfessional.Text;

        objMG1.printCarrierEmployerNoticeName = txtMG1PrintNameOne.Text; // printD
        objMG1.noticeTitle = txtMG1Title.Text; // TitleD
        objMG1.noticeCarrierSignDate = txtMG1DateD.Text; //DateD

        //E
        if (chkMG1MedicalArbitrator.Checked)
            objMG1.chkMedicalArbitratorByWC = "Yes";
        else
            objMG1.chkMedicalArbitratorByWC = "No";

        objMG1.responseCarrierSignDate = txtMG1DateE.Text;
        objMG1.dt_supporting_medical_on = txtMG1MedicalReport.Text;

        // F
        if (chkMG1Certify.Checked)
            objMG1.ChkCertify = "Yes";
        else
            objMG1.ChkCertify = "No";

        objMG1.carrierDenial = txtMG1Denied.Text;

        objMG1.printDenialCarrierName = txtMG1PrintNameTwo.Text;   //printF
        objMG1.denialTitle = txtMG1TitleTwo.Text;                  // TitleF
        //cmd.Parameters.AddWithValue("@sz_signature_F", TxtSignatureF.Text);
        objMG1.denialCarrierSignDate = txtMG1DateF.Text;          // DenielF  

        objMG1.WCBCaseNumber = txtMG1WCBCaseNumber.Text;
        objMG1.carrierCaseNumber = txtMG1CarrierCaseNo.Text;
        objMG1.dateOfInjury = txtMG1DateOfInjury.Text;
        objMG1.firstName = txtMG1PatientName.Text;
        objMG1.socialSecurityNumber = txtMG1socialSecurityNo.Text;
        objMG1.patientAddress = txtMG1PatientAddress.Text;
        objMG1.employerNameAddress = txtMG1EmpNameAndAddress.Text;
        objMG1.insuranceNameAddress = txtMG1InsuranceCarrier.Text;

        objMG1.attendingDoctorNameAddress = ddlAttendingDoctor.SelectedItem.Text;

        objMG1.sz_DoctorID = ddlAttendingDoctor.SelectedValue;//DDLAttendingDoctors.Text

        objMG1.providerWCBNumber1 = txtMG1WCBAuthOne.Text;
        objMG1.providerWCBNumber2 = txtMG1WCBAuthTwo.Text;
        objMG1.providerWCBNumber3 = txtMG1WCBAuthThree.Text;
        objMG1.providerWCBNumber4 = txtMG1WCBAuthFour.Text;
        objMG1.providerWCBNumber5 = txtMG1WCBAuthFive.Text;
        objMG1.providerWCBNumber6 = txtMG1WCBAuthSix.Text;
        objMG1.providerWCBNumber7 = txtMG1WCBAuthSeven.Text;
        objMG1.providerWCBNumber8 = txtMG1WCBAuthEight.Text;

        objMG1.doctorPhone = txtMG1Telephone.Text;
        objMG1.doctorFax = txtMG1FaxNo.Text;

        if (ddlMG1GuidelineReference.SelectedItem.Text != "--Select--")
        {
            ArrayList ar = new ArrayList();
            string spt = ddlMG1GuidelineReference.SelectedItem.Value;
            if (spt != "")
            {
                string[] wrd = spt.Split('-');
                objMG1.bodyInitial = wrd[0].ToString();
                if (wrd.Length == 2)
                {
                    string sLast = wrd[1].ToString();
                    for (int i = 0; i < sLast.Length; i++)
                    {
                        if (i == 0)
                        {
                            objMG1.guidelineSection1 = sLast[i].ToString();
                        }
                        if (i == 1)
                        {
                            objMG1.guidelineSection2 = sLast[i].ToString();
                        }
                        if (i == 2)
                        {
                            objMG1.guidelineSection3 = sLast[i].ToString();
                        }
                        if (i == 3)
                        {
                            objMG1.guidelineSection4 = sLast[i].ToString();
                        }
                    }
                }
            }

            objMG1.bodyInitial = txtMG1Guideline.Text;
            objMG1.guidelineSection1 = txtMG1GuidelineOne.Text;
            objMG1.guidelineSection2 = txtMG1GuidelineTwo.Text;
            objMG1.guidelineSection3 = txtMG1GuidelineThree.Text;
            objMG1.guidelineSection4 = txtMG1GuidelineFour.Text;
        }
        else
        {
            objMG1.bodyInitial = txtMG1Guideline.Text;
            objMG1.guidelineSection1 = txtMG1GuidelineOne.Text;
            objMG1.guidelineSection2 = txtMG1GuidelineTwo.Text;
            objMG1.guidelineSection3 = txtMG1GuidelineThree.Text;
            objMG1.guidelineSection4 = txtMG1GuidelineFour.Text;

        }

        objMG1.I_ID = I_ID;
        objMG1.sz_procedure_group_id = extddlSpeciality.Text;

        MBS_CASEWISE_MG1 oj = new MBS_CASEWISE_MG1();
        string i_ids = oj.SaveMG1(objMG1);

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["Connection_String"].ToString());

        if (i_ids != "0")
        {
            Session["I_ID"] = i_ids;
        }
        try
        {
            MessageControl2.PutMessage("Records Saved Successfully.");
            MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            MessageControl2.Show();

        }
        catch (SqlException ex)
        {
            MessageControl2.PutMessage(ex.Message);
            MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            MessageControl2.Show();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
    protected void btnPrintTopMG1_Click(object sender, EventArgs e)
    {
        try
        {
            PrintMG1();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }

    }
    protected void PrintMG1()
    {
        if (txtMG1InsuranceCarrier.Text.Trim() != "" || txtMG1PatientName.Text.Trim() != ",")
        {
            try
            {
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                //string i_Id = hdnMG21_Id.Value.ToString();
                SaveMG1Data();
                string i_Id = Session["I_ID"].ToString();
                MBS_CASEWISE_MG1 objMBS = new MBS_CASEWISE_MG1();
                DataSet dsMG1 = objMBS.GetMG1Record(sz_CompanyID, sz_CaseID, i_Id);

                if (dsMG1.Tables[0].Rows.Count > 0)
                {
                    string OutputFilePath = "";
                    string OpenPath = "";
                    OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
                    string BaseFilePath = ConfigurationManager.AppSettings["MG1_PDF_FILE"].ToString();
                    string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
                    BasePath = BasePath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
                    if (!Directory.Exists(BasePath))
                    {
                        Directory.CreateDirectory(BasePath);
                    }
                    string newPdfFilename = "";
                    newPdfFilename = "MG1_" + getFileName(sz_CaseID) + ".pdf";
                    OutputFilePath = BasePath + newPdfFilename;
                    OpenPath = OpenPath + newPdfFilename;
                    FormsBO oFormBO = new FormsBO();
                    DataSet dsPdfValue = objMBS.GetMG1Record(sz_CompanyID, sz_CaseID, i_Id); ;
                    PdfReader pdfReader = new PdfReader(BaseFilePath);
                    PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
                    AcroFields pdfFormFields = pdfStamper.AcroFields;

                    if (dsMG1.Tables[0] != null)
                    {

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField1[0]", dsPdfValue.Tables[0].Rows[0]["sz_wcb_case_no"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField1[1]", dsPdfValue.Tables[0].Rows[0]["sz_carrier_case_no"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField1[2]", dsPdfValue.Tables[0].Rows[0]["sz_date_of_injury"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[3]", dsPdfValue.Tables[0].Rows[0]["sz_patient_name"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[2]", dsPdfValue.Tables[0].Rows[0]["sz_security_no"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[7]", dsPdfValue.Tables[0].Rows[0]["sz_patient_address"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[0]", dsPdfValue.Tables[0].Rows[0]["sz_employee_name_address"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[1]", dsPdfValue.Tables[0].Rows[0]["sz_insurance_name_address"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[4]", dsPdfValue.Tables[0].Rows[0]["sz_attending_doctor_name_address"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_2\\.0[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider1"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_2\\.1[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_2\\.2[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_3[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_4\\.0[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider5"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_4\\.1[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider6"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_4\\.2[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider7"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_4\\.3[0]", dsPdfValue.Tables[0].Rows[0]["sz_individual_provider8"].ToString());


                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[6]", dsPdfValue.Tables[0].Rows[0]["sz_teltphone_no"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[5]", dsPdfValue.Tables[0].Rows[0]["sz_fax_no"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DATE_REQUEST_SUBMITTED[0]", dsPdfValue.Tables[0].Rows[0]["dt_date_request_submitted"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TreatmentProcedure_Requested[0]", dsPdfValue.Tables[0].Rows[0]["sz_procedure_Requested"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10\\.1[0]", dsPdfValue.Tables[0].Rows[0]["sz_Guidline_Char"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_8[0]", dsPdfValue.Tables[0].Rows[0]["sz_Guidline1"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_9[0]", dsPdfValue.Tables[0].Rows[0]["sz_Guidline2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10\\.0[0]", dsPdfValue.Tables[0].Rows[0]["sz_Guidline3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10\\.2[0]", dsPdfValue.Tables[0].Rows[0]["sz_Guidline4"].ToString());
                        //pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10.3[0]", dsPdfValue.Tables[0].Rows[0]["sz_Guidline_Char"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[4]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Text2[0]", dsPdfValue.Tables[0].Rows[0]["sz_wcb_case_file"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Text2[1]", dsPdfValue.Tables[0].Rows[0]["sz_comments"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Check_Box3[0]", dsPdfValue.Tables[0].Rows[0]["bt_did"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Check_Box4[0]", dsPdfValue.Tables[0].Rows[0]["bt_not_did"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].by_telephone_to_discuss_this_request_before_making_it__I_contacted_the_carrier_by_telephone_on_date[0]", dsPdfValue.Tables[0].Rows[0]["sz_spoke"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].able_to_speak_to_anyone[0]", dsPdfValue.Tables[0].Rows[0]["sz_spoke_anyone"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Check_Box5[0]", dsPdfValue.Tables[0].Rows[0]["bt_a_copy"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].o_A_copy_of_this_form_was_sent_to_the_carrieremployerselfinsured_employerSpecial_Fund_by__fax_email[0]", dsPdfValue.Tables[0].Rows[0]["sz_fund_by"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField3[0]", dsPdfValue.Tables[0].Rows[0]["dt_provider_signature_date"].ToString() == "01/01/1900" ? "" : dsPdfValue.Tables[0].Rows[0]["dt_provider_signature_date"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].CheckBox1[0]", dsPdfValue.Tables[0].Rows[0]["bt_granted"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].CheckBox1[1]", dsPdfValue.Tables[0].Rows[0]["bt_without_prejudice"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].CheckBox1[2]", dsPdfValue.Tables[0].Rows[0]["bt_denied"].ToString());

                        //pdfFormFields.SetField("topmostSubform[0].Page1[0].Text10[0]", dsPdfValue.Tables[0].Rows[0]["sz_carrier_reverse"].ToString()); // Missing 

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Name_of_the_Medical_Professional_who_Reviewed_the_Denial[0]", dsPdfValue.Tables[0].Rows[0]["sz_if_applicable"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[10]", dsPdfValue.Tables[0].Rows[0]["sz_print_name_D"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[11]", dsPdfValue.Tables[0].Rows[0]["sz_title_D"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField3[2]", dsPdfValue.Tables[0].Rows[0]["dt_date_D"].ToString());


                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Check_Box11[0]", dsPdfValue.Tables[0].Rows[0]["sz_section_E"].ToString()); // add column name here 
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].denial__Supporting_medical_reports_dated[0]", dsPdfValue.Tables[0].Rows[0]["dt_supporting_medical_on"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField3[1]", dsPdfValue.Tables[0].Rows[0]["dt_date_E"].ToString());

                        // 
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Check_Box12[0]", dsPdfValue.Tables[0].Rows[0]["sz_certify"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].Date_4[0]", dsPdfValue.Tables[0].Rows[0]["dt_initial_denied"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[9]", dsPdfValue.Tables[0].Rows[0]["sz_print_name_F"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField2[8]", dsPdfValue.Tables[0].Rows[0]["sz_title_F"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField3[3]", dsPdfValue.Tables[0].Rows[0]["dt_date_F"].ToString());

                    }
                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + "');", true);
                }
                else
                {
                    usrMessage.PutMessage("Please Save Record First.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
                }

            }
            catch (Exception ex)
            {
                usrMessage.PutMessage(ex.ToString());
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            finally
            {

            }
        }
    }
    protected void ddlAttendingDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sz_doctorID = ddlAttendingDoctor.SelectedValue.ToString();
        FillDoctorInfo1(sz_doctorID);
    }

    private void FillDoctorInfo1(string sz_doctorID)
    {
        DataSet dsdocInfo = GetDoctorInfo(sz_doctorID);
        if (dsdocInfo.Tables.Count > 0)
        {
            if (dsdocInfo.Tables[0].Rows.Count > 0)
            {
                TxtIndividualProvider.Text = dsdocInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
                TxtTelephone.Text = dsdocInfo.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString();
                TxtFaxNo.Text = dsdocInfo.Tables[0].Rows[0]["SZ_OFFICE_FAX"].ToString();
            }
        }
    }

    private DataSet GetDoctorInfo(string sz_doctorID)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_Get_Doctor_info", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_DOCTOR_ID", sz_doctorID);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
    }
    //protected void ddlMG1GuidelineReference_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlMG1GuidelineReference.SelectedItem.Text != "--Select--")
    //    {
    //        string stln = ddlMG1GuidelineReference.SelectedItem.Text;
    //        ArrayList ar = new ArrayList();
    //        for (int i = 0; i < stln.Length; i++)
    //        {
    //            ar.Add(stln[i]);
    //        }

    //        txtMG1Guideline.Text = ar[0].ToString();
    //        txtMG1GuidelineOne.Text = ar[2].ToString();
    //        txtMG1GuidelineTwo.Text = ar[3].ToString();
    //        txtMG1GuidelineThree.Text = ar[4].ToString();
    //        txtMG1GuidelineFour.Text = ar[5].ToString();
    //        //txtMG1GuidelineFive.Text = ar[6].ToString();
    //    }
    //    else
    //    {
    //        txtMG1Guideline.Text = "";
    //        txtMG1GuidelineOne.Text = "";
    //        txtMG1GuidelineTwo.Text = "";
    //        txtMG1GuidelineThree.Text = "";
    //        txtMG1GuidelineFour.Text = "";
    //        //txtMG1GuidelineFive.Text = "";
    //    }
    //}
    protected void btnSaveMG1_Click(object sender, EventArgs e)
    {
        try
        {
            SaveMG1Data();
        }
        catch (Exception ex)
        {
            MessageControl2.PutMessage(ex.Message);
            MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            MessageControl2.Show();
        }

    }
    protected void btnPrintMG1_Click(object sender, EventArgs e)
    {
        try
        {
            PrintMG1();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }

    }
    protected void btnMG11_save_top_Click(object sender, EventArgs e)
    {
        try
        {
            SaveMG11Data();
        }
        catch (Exception ex)
        {
            UserMessageMG11.PutMessage(ex.Message);
            UserMessageMG11.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            UserMessageMG11.Show();
        }
    }

    private void SaveMG11Data()
    {
        try
        {
            int I_ID = 0;
            if (Session["I_ID"] != null)
            {
                I_ID = Convert.ToInt32(Session["I_ID"].ToString());
            }
            string sz_Caseno = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
            string sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            string sz_UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            AddMG11CaseWiseDAO objMG11 = new AddMG11CaseWiseDAO();

            objMG11.I_ID = I_ID;
            objMG11.sz_CompanyID = sz_CompanyID;
            objMG11.sz_CaseID = sz_CaseID;
            objMG11.sz_UserID = sz_UserID;
            objMG11.WCBCaseNumber = txtWCBNumber.Text;
            objMG11.carrierCaseNumber = txtCasenumber_MG11.Text;
            objMG11.dateOfInjury = txtInjuryDate_MG11.Text;
            objMG11.firstName = txtPatientName_MG11.Text;
            objMG11.MiddleName = txtMiddleName_MG11.Text;
            objMG11.lastName = txtLastName_MG11.Text;
            objMG11.socialSecurityNumber = txtSocialNumber_MG11.Text;
            objMG11.patientAddress = txtPatient_Address_MG11.Text;
            objMG11.DoctorWCBNumber = txtDoctor_WCB.Text;
            objMG11.TreatmentOne = txt_Treatment_One.Text;
            objMG11.GuidelineOne = txtGuideline_One.Text;
            objMG11.GuidelineBoxOne = txtGuideline_BoxOne.Text;
            objMG11.GuidelineBoxTwo = txtGuideline_BoxTwo.Text;
            objMG11.GuidelineBoxThree = txtGuideline_BoxThree.Text;
            objMG11.GuidelineBoxfour = txtGuideline_BoxFour.Text;
            objMG11.dateOfServiceOne = txt_DateOfService.Text;
            objMG11.sz_Comments = txtComments_One.Text;

            objMG11.TreatmentTwo = txt_Treatement_two.Text;
            objMG11.GuidelineTwo = txt_Guideline_two.Text;
            objMG11.GuidelineFive = txt_Guideline_Box_Five.Text;
            objMG11.GuidelineBoxSix = txtGuideline_Box_Six.Text;
            objMG11.GuidelineBoxSeven = txt_Guideline_Box_Seven.Text;
            objMG11.GuidelineBoxEight = txt_Guideline_Box_Eight.Text;
            objMG11.dateOfServiceTwo = txt_Date_of_Service_two.Text;
            objMG11.sz_Comments_two = txt_Comments_two.Text;

            objMG11.TreatmentThree = txt_Treatment_three.Text;
            objMG11.GuidelineThree = txt_Guideline_three.Text;
            objMG11.GuidelineBoxNine = txt_Guideline_BoxNine.Text;
            objMG11.GuidelineBoxTen = txt_Guideline_Box_ten.Text;
            objMG11.GuidelineBoxeleven = txtGuideline_Box_eleven.Text;
            objMG11.GuidelineBoxtwelve = txt_Guideline_Box_twelve.Text;
            objMG11.dateOfServiceThree = txt_Date_Of_Service_three.Text;
            objMG11.sz_Comments_three = txt_Comments_three.Text;

            objMG11.TreatmentFour = txt_Treatment_four.Text;
            objMG11.GuidelineFour = txt_Guideline_four.Text;
            objMG11.GuidelineThirteen = txt_Guideline_Box_thirteen.Text;
            objMG11.GuidelineBoxfourteen = txt_Guideline_Box_fourteen.Text;
            objMG11.GuidelineBoxfifteen = txt_Guideline_Box_fifteen.Text;
            objMG11.GuidelineBoxsixteen = txt_Guideline_Sixteen.Text;
            objMG11.dateOfServiceFour = txt_Date_Of_Service_four.Text;
            objMG11.sz_Comments_four = txt_Comments_four.Text;

            objMG11.Carrier_One = txt_Carrier_One.Text;
            objMG11.Carrier_two = txt_Carrier_two.Text;

            objMG11.Carrier_three = txt_Carrier_three.Text;
            objMG11.CarrierDate = txt_Date.Text;
            objMG11.Employer = txt_employer.Text;
            objMG11.MedicalProfessional = txt_Medical_Professional.Text;
            objMG11.PrintNameOne = txt_Print_Name_One.Text;
            objMG11.TitleOne = txt_Title_One.Text;
            objMG11.EmployerDate = txt_Date_employer_One.Text;

            objMG11.MedicalDate = txt_Date_Medical.Text;

            objMG11.ProviderDate = txt_Provider_Date.Text;

            objMG11.Provider_request = txt_Provider_request.Text;

            objMG11.Print_Name_two = txt_Print_Name_two.Text;
            objMG11.Title_two = txt_Title_two.Text;
            objMG11.EmployerDate_two = txt_Date_employer_two.Text;

            objMG11.sz_DoctorID = ddlDoctor_Name_MG11.SelectedValue;
            objMG11.sz_Doctor_Name = ddlDoctor_Name_MG11.SelectedItem.Text;

            if (cbGranted_One.Checked)
                objMG11.cbGranted_One = "1";
            else
                objMG11.cbGranted_One = "0";

            if (cbGrantedPrejudice_One.Checked)
                objMG11.CbGrantedPrejudice_One = "1";
            else
                objMG11.CbGrantedPrejudice_One = "0";

            if (cbDenied_One.Checked)
                objMG11.CbDenied_One = "1";
            else
                objMG11.CbDenied_One = "0";

            if (cbGranted_two.Checked)
                objMG11.cbGranted_Two = "1";
            else
                objMG11.cbGranted_Two = "0";

            if (cbGrantedPrejudice_two.Checked)
                objMG11.CbGrantedPrejudice_Two = "1";
            else
                objMG11.CbGrantedPrejudice_Two = "0";

            if (cbDenied_two.Checked)
                objMG11.CbDenied_Two = "1";
            else
                objMG11.CbDenied_Two = "0";

            if (cbGranted_three.Checked)
                objMG11.cbGranted_Three = "1";
            else
                objMG11.cbGranted_Three = "0";

            if (cbGrantedPrejudice_three.Checked)
                objMG11.CbGrantedPrejudice_Three = "1";
            else
                objMG11.CbGrantedPrejudice_Three = "0";

            if (cbDenied_three.Checked)
                objMG11.CbDenied_Three = "1";
            else
                objMG11.CbDenied_Three = "0";

            if (cbGranted_four.Checked)
                objMG11.cbGranted_Four = "1";
            else
                objMG11.cbGranted_Four = "0";

            if (cbGrantedPrejudice_four.Checked)
                objMG11.CbGrantedPrejudice_Four = "1";
            else
                objMG11.CbGrantedPrejudice_Four = "0";

            if (cbDenied_four.Checked)
                objMG11.CbDenied_Four = "1";
            else
                objMG11.CbDenied_Four = "0";

            if (cb_Contact_One.Checked)
                objMG11.CbContactOne = "1";
            else
                objMG11.CbContactOne = "0";

            if (cb_Contact_two.Checked)
                objMG11.CbContacttwo = "1";
            else
                objMG11.CbContacttwo = "0";

            if (cb_Carrier_One.Checked)
                objMG11.CbCarrier_One = "1";
            else
                objMG11.CbCarrier_One = "0";

            if (cbProvider.Checked)
                objMG11.CBProvider = "1";
            else
                objMG11.CBProvider = "0";

            if (cb_Medical_request_two.Checked)
                objMG11.Medical_request_two = "1";
            else
                objMG11.Medical_request_two = "0";

            if (cb_Medical_request_three.Checked)
                objMG11.MedicalDate_three = "1";
            else
                objMG11.MedicalDate_three = "0";

            if (cb_Medical_request_four.Checked)
                objMG11.CBMedical_request_four = "1";
            else
                objMG11.CBMedical_request_four = "0";

            if (cb_Medical_request_five.Checked)
                objMG11.MedicalDate_five = "1";
            else
                objMG11.MedicalDate_five = "0";

            if (cb_Provider_request.Checked)
                objMG11.CBProvider_request = "1";
            else
                objMG11.CBProvider_request = "0";

            if (cb_request_two.Checked)
                objMG11.CB_request_two = "1";
            else
                objMG11.CB_request_two = "0";

            if (cb_request_three.Checked)
                objMG11.CB_request_three = "1";
            else
                objMG11.CB_request_three = "0";

            if (cb_request_four.Checked)
                objMG11.CB_request_four = "1";
            else
                objMG11.CB_request_four = "0";

            if (cb_request_five.Checked)
                objMG11.CB_request_five = "1";
            else
                objMG11.CB_request_five = "0";

            string i_ids = obj11.SaveMG11(objMG11);

            if (i_ids != "0")
            {
                Session["I_ID"] = i_ids;
            }

            UserMessageMG11.PutMessage("Data saved successfully");
            UserMessageMG11.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            UserMessageMG11.Show();
        }
        catch (Exception ex)
        {
            UserMessageMG11.PutMessage(ex.Message);
            UserMessageMG11.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            UserMessageMG11.Show();
        }
    }

    protected void PrintMG11()
    {
        if (txtMG2_InsuCarrAddress.Text.Trim() != "" || txtMG2_PatientName.Text.Trim() != ",")
        {
            try
            {
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                SaveMG11Data();
                string i_Id = Session["I_ID"].ToString();
                MBS_CASEWISE_MG11 objMBS = new MBS_CASEWISE_MG11();
                DataTable dt_MG11 = objMBS.GetMG11Records(sz_CompanyID, sz_CaseID, i_Id);

                if (dt_MG11.Rows.Count > 0)
                {
                    string OutputFilePath = "";
                    string OpenPath = "";
                    OpenPath = ConfigurationManager.AppSettings["VIEWWORD_DOC"].ToString() + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
                    string BaseFilePath = ConfigurationManager.AppSettings["MG1_1_PDF_FILE"].ToString();
                    string BasePath = ConfigurationManager.AppSettings["BASEPATH"].ToString();
                    BasePath = BasePath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
                    if (!Directory.Exists(BasePath))
                    {
                        Directory.CreateDirectory(BasePath);
                    }
                    string newPdfFilename = "";
                    newPdfFilename = "MG1.1_" + getFileName(sz_CaseID) + ".pdf";
                    OutputFilePath = BasePath + newPdfFilename;
                    OpenPath = OpenPath + newPdfFilename;
                    FormsBO oFormBO = new FormsBO();
                    DataTable dsPdfValue = objMBS.GetMG11Records(sz_CompanyID, sz_CaseID, i_Id); ;
                    PdfReader pdfReader = new PdfReader(BaseFilePath);
                    PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
                    AcroFields pdfFormFields = pdfStamper.AcroFields;

                    if (dt_MG11 != null)
                    {
                        pdfFormFields.SetField("txtPatientName", dt_MG11.Rows[0]["PatientName"].ToString());
                        pdfFormFields.SetField("txtWCBCaseNumber", dt_MG11.Rows[0]["WCBCaseNumber"].ToString());
                        pdfFormFields.SetField("txtCarrierCaseNumber", dt_MG11.Rows[0]["carrierCaseNumber"].ToString());
                        pdfFormFields.SetField("txtDateofInjury", dt_MG11.Rows[0]["dateOfInjury"].ToString());
                        pdfFormFields.SetField("txtPatientSocialSecurityNumber", dt_MG11.Rows[0]["socialSecurityNumber"].ToString());
                        pdfFormFields.SetField("txtDoctorWCBNumber", dt_MG11.Rows[0]["DoctorWCBNumber"].ToString());
                        pdfFormFields.SetField("txtDoctorName", dt_MG11.Rows[0]["SZ_Doctor_Name"].ToString());

                        pdfFormFields.SetField("Text76", dt_MG11.Rows[0]["TreatmentOne"].ToString());
                        pdfFormFields.SetField("Text71", dt_MG11.Rows[0]["GuidelineOne"].ToString());
                        pdfFormFields.SetField("Text72", dt_MG11.Rows[0]["GuidelineBoxOne"].ToString());
                        pdfFormFields.SetField("Text73", dt_MG11.Rows[0]["GuidelineBoxTwo"].ToString());
                        pdfFormFields.SetField("Text74", dt_MG11.Rows[0]["GuidelineBoxThree"].ToString());
                        pdfFormFields.SetField("Text75", dt_MG11.Rows[0]["GuidelineBoxfour"].ToString());
                        pdfFormFields.SetField("Text70", dt_MG11.Rows[0]["dateOfServiceOne"].ToString());
                        pdfFormFields.SetField("Text4", dt_MG11.Rows[0]["sz_Comments"].ToString());

                        pdfFormFields.SetField("Text6", dt_MG11.Rows[0]["TreatmentTwo"].ToString());
                        pdfFormFields.SetField("Text7", dt_MG11.Rows[0]["GuidelineTwo"].ToString());
                        pdfFormFields.SetField("Text8", dt_MG11.Rows[0]["GuidelineFive"].ToString());
                        pdfFormFields.SetField("Text9", dt_MG11.Rows[0]["GuidelineBoxSix"].ToString());
                        pdfFormFields.SetField("Text10", dt_MG11.Rows[0]["GuidelineBoxSeven"].ToString());
                        pdfFormFields.SetField("Text11", dt_MG11.Rows[0]["GuidelineBoxEight"].ToString());
                        pdfFormFields.SetField("Text12", dt_MG11.Rows[0]["dateOfServiceTwo"].ToString());
                        pdfFormFields.SetField("Text16", dt_MG11.Rows[0]["sz_Comments_two"].ToString());

                        pdfFormFields.SetField("Text18", dt_MG11.Rows[0]["TreatmentThree"].ToString());
                        pdfFormFields.SetField("Text19", dt_MG11.Rows[0]["GuidelineThree"].ToString());
                        pdfFormFields.SetField("Text20", dt_MG11.Rows[0]["GuidelineBoxNine"].ToString());
                        pdfFormFields.SetField("Text21", dt_MG11.Rows[0]["GuidelineBoxTen"].ToString());
                        pdfFormFields.SetField("Text22", dt_MG11.Rows[0]["GuidelineBoxeleven"].ToString());
                        pdfFormFields.SetField("Text23", dt_MG11.Rows[0]["GuidelineBoxtwelve"].ToString());
                        pdfFormFields.SetField("Text24", dt_MG11.Rows[0]["dateOfServiceThree"].ToString());
                        pdfFormFields.SetField("Text28", dt_MG11.Rows[0]["sz_Comments_three"].ToString());

                        pdfFormFields.SetField("Text30", dt_MG11.Rows[0]["TreatmentFour"].ToString());
                        pdfFormFields.SetField("31", dt_MG11.Rows[0]["GuidelineFour"].ToString());
                        pdfFormFields.SetField("Text32", dt_MG11.Rows[0]["GuidelineThirteen"].ToString());
                        pdfFormFields.SetField("Text33", dt_MG11.Rows[0]["GuidelineBoxfourteen"].ToString());
                        pdfFormFields.SetField("Text34", dt_MG11.Rows[0]["GuidelineBoxfifteen"].ToString());
                        pdfFormFields.SetField("Text35", dt_MG11.Rows[0]["GuidelineBoxsixteen"].ToString());
                        pdfFormFields.SetField("Text36", dt_MG11.Rows[0]["dateOfServiceFour"].ToString());
                        pdfFormFields.SetField("Text40", dt_MG11.Rows[0]["sz_Comments_four"].ToString());

                        pdfFormFields.SetField("Text44", dt_MG11.Rows[0]["Carrier_One"].ToString());
                        pdfFormFields.SetField("Text45", dt_MG11.Rows[0]["Carrier_two"].ToString());
                        pdfFormFields.SetField("Text47", dt_MG11.Rows[0]["Carrier_three"].ToString());
                        pdfFormFields.SetField("Text48", dt_MG11.Rows[0]["CarrierDate"].ToString());
                        pdfFormFields.SetField("Text49", dt_MG11.Rows[0]["Employer"].ToString());
                        pdfFormFields.SetField("Text50", dt_MG11.Rows[0]["MedicalProfessional"].ToString());
                        pdfFormFields.SetField("Text51", dt_MG11.Rows[0]["PrintNameOne"].ToString());
                        pdfFormFields.SetField("Text52", dt_MG11.Rows[0]["TitleOne"].ToString());
                        pdfFormFields.SetField("Text53", dt_MG11.Rows[0]["EmployerDate"].ToString());
                        pdfFormFields.SetField("Text55", dt_MG11.Rows[0]["MedicalDate"].ToString());
                        pdfFormFields.SetField("Text60", dt_MG11.Rows[0]["ProviderDate"].ToString());
                        pdfFormFields.SetField("Text62", dt_MG11.Rows[0]["Provider_request"].ToString());
                        pdfFormFields.SetField("Text67", dt_MG11.Rows[0]["Print_Name_two"].ToString());
                        pdfFormFields.SetField("Text68", dt_MG11.Rows[0]["Title_two"].ToString());
                        pdfFormFields.SetField("Text69", dt_MG11.Rows[0]["EmployerDate_two"].ToString());

                        pdfFormFields.SetField("Text1", dt_MG11.Rows[0]["cbGranted_One"].ToString());
                        pdfFormFields.SetField("Text2", dt_MG11.Rows[0]["CbGrantedPrejudice_One"].ToString());
                        pdfFormFields.SetField("Text3", dt_MG11.Rows[0]["CbDenied_One"].ToString());
                        pdfFormFields.SetField("Text13", dt_MG11.Rows[0]["cbGranted_Two"].ToString());
                        pdfFormFields.SetField("Text14", dt_MG11.Rows[0]["CbGrantedPrejudice_Two"].ToString());
                        pdfFormFields.SetField("Text15", dt_MG11.Rows[0]["CbDenied_Two"].ToString());
                        pdfFormFields.SetField("Text25", dt_MG11.Rows[0]["cbGranted_Three"].ToString());
                        pdfFormFields.SetField("Text26", dt_MG11.Rows[0]["CbGrantedPrejudice_Three"].ToString());
                        pdfFormFields.SetField("Text27", dt_MG11.Rows[0]["CbDenied_Three"].ToString());
                        pdfFormFields.SetField("Text37", dt_MG11.Rows[0]["cbGranted_Four"].ToString());
                        pdfFormFields.SetField("Text38", dt_MG11.Rows[0]["CbGrantedPrejudice_Four"].ToString());
                        pdfFormFields.SetField("Text39", dt_MG11.Rows[0]["CbDenied_Four"].ToString());

                        if (dt_MG11.Rows[0]["CbContactOne"].ToString() == "1")
                        {
                            pdfFormFields.SetField("Text42", "Yes");
                        }
                        else
                        {
                            pdfFormFields.SetField("Text42", "No");
                        }
                        // pdfFormFields.SetField("Text42", dt_MG11.Rows[0]["CbContactOne"].ToString());

                        if (dt_MG11.Rows[0]["CbContacttwo"].ToString() == "1")
                        {
                            pdfFormFields.SetField("Text43", "Yes");
                        }
                        else
                        {
                            pdfFormFields.SetField("Text43", "No");
                        }
                        // pdfFormFields.SetField("Text43", dt_MG11.Rows[0]["CbContacttwo"].ToString());

                        if (dt_MG11.Rows[0]["CbCarrier_One"].ToString() == "1")
                        {
                            pdfFormFields.SetField("Text46", "Yes");
                        }
                        else
                        {
                            pdfFormFields.SetField("Text46", "No");
                        }
                        //pdfFormFields.SetField("Text46", dt_MG11.Rows[0]["CbCarrier_One"].ToString());

                        if (dt_MG11.Rows[0]["CBProvider"].ToString() == "1")
                        {
                            pdfFormFields.SetField("Text54", "Yes");
                        }
                        else
                        {
                            pdfFormFields.SetField("Text54", "No");
                        }
                        //pdfFormFields.SetField("Text54", dt_MG11.Rows[0]["CBProvider"].ToString());
                        pdfFormFields.SetField("Text56", dt_MG11.Rows[0]["Medical_request_two"].ToString());
                        pdfFormFields.SetField("Text57", dt_MG11.Rows[0]["MedicalDate_three"].ToString());
                        pdfFormFields.SetField("Text58", dt_MG11.Rows[0]["CBMedical_request_four"].ToString());
                        pdfFormFields.SetField("Text59", dt_MG11.Rows[0]["MedicalDate_five"].ToString());

                        if (dt_MG11.Rows[0]["CBProvider_request"].ToString() == "1")
                        {
                            pdfFormFields.SetField("Text61", "Yes");
                        }
                        else
                        {
                            pdfFormFields.SetField("Text61", "No");
                        }

                        //pdfFormFields.SetField("Text61", dt_MG11.Rows[0]["CBProvider_request"].ToString());
                        pdfFormFields.SetField("Text63", dt_MG11.Rows[0]["CB_request_two"].ToString());
                        pdfFormFields.SetField("Text64", dt_MG11.Rows[0]["CB_request_three"].ToString());
                        pdfFormFields.SetField("Text65", dt_MG11.Rows[0]["CB_request_four"].ToString());
                        pdfFormFields.SetField("Text66", dt_MG11.Rows[0]["CB_request_five"].ToString());

                    }
                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + "');", true);
                }
                else
                {
                    UserMessageMG11.PutMessage("Please Save Record First.");
                    UserMessageMG11.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    UserMessageMG11.Show();
                }

            }
            catch (Exception ex)
            {
                UserMsgMG21.PutMessage(ex.ToString());
                UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                UserMsgMG21.Show();
            }
            finally
            {

            }
        }
    }
    protected void btnPrint_MG11_top_Click(object sender, EventArgs e)
    {
        try
        {
            PrintMG11();
        }
        catch (Exception ex)
        {
            UserMsgMG21.PutMessage(ex.ToString());
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMsgMG21.Show();
        }
    }
    protected void btnMG11_save_top_Click1(object sender, EventArgs e)
    {
        SaveMG11Data();
    }
    protected void ddlDoctor_Name_MG11_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sz_doctorID = ddlDoctor_Name_MG11.SelectedValue.ToString();
        FillDoctorInfo_MG11(sz_doctorID);
    }

    private void FillDoctorInfo_MG11(string sz_doctorID)
    {
        DataSet dsdocInfo = GetDoctorInfo(sz_doctorID);
        if (dsdocInfo.Tables.Count > 0)
        {
            if (dsdocInfo.Tables[0].Rows.Count > 0)
            {
                txtDoctor_WCB.Text = dsdocInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
            }
        }
    }
    protected void btnMG11_save_bottom_Click(object sender, EventArgs e)
    {
        try
        {
            SaveMG11Data();
        }
        catch (Exception ex)
        {
            UserMessageMG11.PutMessage(ex.Message);
            UserMessageMG11.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            UserMessageMG11.Show();
        }

    }
    protected void btnPrint_MG11_bottom_Click(object sender, EventArgs e)
    {
        try
        {
            PrintMG11();
        }
        catch (Exception ex)
        {
            UserMsgMG21.PutMessage(ex.ToString());
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMsgMG21.Show();
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            SaveData();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.Message);
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }

    }
    private void SaveData()
    {
        int I_ID = 0;
        if (Session["I_ID"] != null)
        {
            I_ID = Convert.ToInt32(Session["I_ID"].ToString());
        }
        //NEW CODE

        string bodyInitial, guidelineSection = "";
        string sz_Caseno = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
        string sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        string sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        string sz_UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
        AddMG2Casewise objMG2 = new AddMG2Casewise();


        objMG2.sz_CompanyID = sz_CompanyID;
        objMG2.sz_CaseID = sz_CaseID;
        objMG2.sz_UserID = sz_UserID;
        if (DDLAttendingDoctors.SelectedValue.ToString() != "NA")
        {
            objMG2.attendingDoctorNameAddress = DDLAttendingDoctors.SelectedItem.Text;
        }
        else
        {
            objMG2.attendingDoctorNameAddress = "";
        }
        objMG2.approvalRequest = TxtApproval.Text;
        objMG2.dateOfService = TxtDateofService.Text;
        objMG2.datesOfDeniedRequest = TxtDateofApplicable.Text;
        if (Chkdid.Checked)
            objMG2.chkDid = "1";
        else
            objMG2.chkDid = "0";
        if (Chkdidnot.Checked)
            objMG2.chkDidNot = "1";
        else
            objMG2.chkDidNot = "0";
        //objMG2.contactDate = txtMG1TelephoneTwo.Text;
        objMG2.contactDate = TxtSpoke.Text;
        objMG2.personContacted = Txtspecktoanyone.Text;

        if (ChkAcopy.Checked)
            objMG2.chkCopySent = "1";
        else
            objMG2.chkCopySent = "0";
        objMG2.faxEmail = TxtAddressRequired.Text;

        if (ChkIAmnot.Checked)
            objMG2.chkCopyNotSent = "1";
        else
            objMG2.chkCopyNotSent = "0";
        objMG2.indicatedFaxEmail = Txtaboveon.Text;
        objMG2.providerSignDate = TxtProviderDate.Text;

        if (ChktheSelf.Checked)
            objMG2.chkNoticeGiven = "1";
        else
            objMG2.chkNoticeGiven = "0";
        objMG2.printCarrierEmployerNoticeName = TxtByPrintNameD.Text;
        objMG2.noticeTitle = TxtTitleD.Text;
        objMG2.noticeCarrierSignDate = TxtDateD.Text;
        objMG2.carrierDenial = TxtSectionE.Text;

        if (ChkGranted.Checked)

            objMG2.chkGranted = "1";
        else
            objMG2.chkGranted = "0";
        if (ChkGrantedinPart.Checked)
            objMG2.chkGrantedInParts = "1";
        else
            objMG2.chkGrantedInParts = "0";
        if (ChkWithoutPrejudice.Checked)
            objMG2.chkWithoutPrejudice = "1";
        else
            objMG2.chkWithoutPrejudice = "0";
        if (ChkDenied.Checked)
            objMG2.chkDenied = "1";
        else
            objMG2.chkDenied = "0";
        if (ChkBurden.Checked)
            objMG2.chkBurden = "1";
        else
            objMG2.chkBurden = "0";
        if (ChkSubstantially.Checked)
            objMG2.chkSubstantiallySimilar = "1";
        else
            objMG2.chkSubstantiallySimilar = "0";
        objMG2.medicalProfessional = TxtMedicalProfes.Text;
        if (ChkMadeE.Checked)
            objMG2.chkMedicalArbitrator = "1";
        else
            objMG2.chkMedicalArbitrator = "0";
        if (ChkChairE.Checked)
            objMG2.chkWCBHearing = "1";
        else
            objMG2.chkWCBHearing = "0";
        objMG2.printCarrierEmployerResponseName = TxtByPrintNameE.Text;
        objMG2.responseTitle = TxtTitleE.Text;

        objMG2.responseCarrierSignDate = TxtDateE.Text;

        objMG2.printDenialCarrierName = TxtByPrintNameF.Text;
        objMG2.denialTitle = TxtTitleF.Text;

        objMG2.denialCarrierSignDate = TxtDateF.Text;


        if (ChkIrequestG.Checked)
            objMG2.chkRequestWC = "1";
        else
            objMG2.chkRequestWC = "0";
        if (ChkMadeG.Checked)
            objMG2.chkMedicalArbitratorByWC = "1";
        else
            objMG2.chkMedicalArbitratorByWC = "0";
        if (ChkChairG.Checked)
            objMG2.chkWCBHearingByWC = "1";
        else
            objMG2.chkWCBHearingByWC = "0";

        objMG2.claimantSignDate = TxtClaimantDate.Text;

        objMG2.WCBCaseNumber = Txtwcbcasenumber.Text;
        objMG2.carrierCaseNumber = TxtCarrierCaseNo.Text;
        objMG2.dateOfInjury = TxtDateofInjury.Text;
        objMG2.firstName = TxtFirstName.Text;
        objMG2.middleName = TxtMiddleName.Text;
        objMG2.lastName = TxtLastName.Text;
        objMG2.patientAddress = TxtPatientAddress.Text;
        objMG2.employerNameAddress = TxtEmployerNameAdd.Text;
        objMG2.insuranceNameAddress = TxtInsuranceNameAdd.Text;
        objMG2.sz_DoctorID = DDLAttendingDoctors.SelectedValue;//DDLAttendingDoctors.Text
        objMG2.providerWCBNumber = TxtIndividualProvider.Text;
        objMG2.doctorPhone = TxtTelephone.Text;
        objMG2.doctorFax = TxtFaxNo.Text;

        if (ddlGuidline.SelectedItem.Text != "--Select--")
        {
            ArrayList ar = new ArrayList();
            string spt = ddlGuidline.SelectedItem.Value;
            string[] wrd = spt.Split('-');
            foreach (string word in wrd)
            {
                ar.Add(word);
            }
            objMG2.bodyInitial = ar[0].ToString();
            objMG2.guidelineSection = ar[1].ToString();
            objMG2.bodyInitial = TxtGuislineChar.Text;
            objMG2.guidelineSection = TxtGuidline1.Text + "" + TxtGuidline2.Text + "" + TxtGuidline3.Text + "" + TxtGuidline4.Text + "" + TxtGuidline5.Text;
        }
        else
        {
            objMG2.bodyInitial = TxtGuislineChar.Text;

            if (TxtGuidline1.Text == "")
                TxtGuidline1.Text = " ";
            if (TxtGuidline2.Text == "")
                TxtGuidline2.Text = " ";
            if (TxtGuidline3.Text == "")
                TxtGuidline3.Text = " ";
            if (TxtGuidline4.Text == "")
                TxtGuidline4.Text = " ";
            if (TxtGuidline5.Text == "")
                TxtGuidline5.Text = " ";

            objMG2.guidelineSection = TxtGuidline1.Text + "" + TxtGuidline2.Text + "" + TxtGuidline3.Text + "" + TxtGuidline4.Text + "" + TxtGuidline5.Text; ;
        }
        objMG2.socialSecurityNumber = TxtSocialSecurityNo.Text;

        objMG2.I_ID = I_ID;

        MBS_CASEWISE_MG2 oj = new MBS_CASEWISE_MG2();
        string i_ids = oj.SaveMG2(objMG2);

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());

        if (i_ids != "0")
        {
            Session["I_ID"] = i_ids;
        }
        try
        {
            #region save procedure codes

            if (Session["I_ID"].ToString() != "")
            {
                #region previously saved procedure codes

                DataSet dsprevproccode = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_save_mg2_procedure_codes",
                            new SqlParameter("@I_ID", Session["I_ID"].ToString()),
                            new SqlParameter("@SZ_CASE_ID", ""),
                            new SqlParameter("@SZ_COMPANY_ID", ""),
                            new SqlParameter("@SZ_PROCEDURE_ID", ""),
                            new SqlParameter("@SZ_TYPE_CODE_ID", ""),
                            new SqlParameter("@SZ_PROCEDURE_CODE", ""),
                            new SqlParameter("@SZ_CODE_DESCRIPTION", ""),
                            new SqlParameter("@FLT_AMOUNT", ""),
                            new SqlParameter("@FLAG", "DELETE"));

                #endregion

                #region save procedure codes

                foreach (DataGridItem j in grdProcedure.Items)
                {
                    CheckBox chkSelect = (CheckBox)j.FindControl("chkselect");
                    if (chkSelect.Checked)
                    {
                        double amt = 0;
                        try
                        { amt = Convert.ToDouble(j.Cells[5].Text.ToString()); }
                        catch
                        { amt = 0; }

                        DataSet dssaveproccode = SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "sp_save_mg2_procedure_codes",
                            new SqlParameter("@I_ID", Session["I_ID"].ToString()),
                            new SqlParameter("@SZ_CASE_ID", sz_CaseID),
                            new SqlParameter("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID),
                            new SqlParameter("@SZ_PROCEDURE_ID", j.Cells[1].Text.ToString()),
                            new SqlParameter("@SZ_TYPE_CODE_ID", j.Cells[2].Text.ToString()),
                            new SqlParameter("@SZ_PROCEDURE_CODE", j.Cells[3].Text.ToString()),
                            new SqlParameter("@SZ_CODE_DESCRIPTION", j.Cells[4].Text.ToString()),
                            new SqlParameter("@FLT_AMOUNT", amt),
                            new SqlParameter("@FLAG", "INSERT")
                            );
                    }
                }

                #endregion
            }

            #endregion

            usrMessage.PutMessage("Records Saved Successfully.");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

        }
        catch (SqlException ex)
        {
            usrMessage.PutMessage(ex.Message);
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        {

            try
            {
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                SaveData();
                string i_Id = Session["I_ID"].ToString();
                MBS_CASEWISE_MG2 objMBS = new MBS_CASEWISE_MG2();
                DataTable flg = objMBS.GetMG2Record(sz_CompanyID, sz_CaseID, i_Id);
                if (flg.Rows.Count > 0)
                {
                    PrintPDF();
                }
                else
                {
                    usrMessage.PutMessage("Please Save Record First.");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    usrMessage.Show();
                }

            }
            catch (Exception ex)
            {
                usrMessage.PutMessage(ex.ToString());
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            finally
            {

            }
        }
    }

    private void PrintPDF()
    {
        try
        {
            objNF3Template = new Bill_Sys_NF3_Template();
            string i_id = "";
            if (Session["I_ID"] != null)
            {
                i_id = Session["I_ID"].ToString();
            }

            MBS_CASEWISE_MG2 obj = new MBS_CASEWISE_MG2();
            //string CmpName = obj.GetPDFPath(sz_CompanyID, sz_CaseID, extddlSpeciality.Selected_Text);
            string CmpName = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME.ToString();
            Logicalpath = "MG2_" + i_id + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff");
            CmpName = CmpName + "/" + sz_CaseID + "/" + "Packet Document";

            tempPath = obj.generateMG2New(sz_CompanyID, sz_CaseID, i_id, CmpName, Logicalpath);
            tempPath = tempPath.Replace('\\', '/');
            SaveLogicalPath();
            string phpath = objNF3Template.getPhysicalPath();
            phpath = phpath.Replace('\\', '/');
            string str = ApplicationSettings.GetParameterValue("DocumentManagerURL");
            string openpath = tempPath.Replace(phpath, str);

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + openpath + "');", true);
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
        finally
        {

        }

    }

    private void SaveLogicalPath()
    {
        //FindNodeType();

        //try
        //{
        //    //string strGenFileName = sz_BillNo + "_" + sz_CaseID + "_" + DateTime.Now.ToString("MMddyyyyhhmmssffff")+"MG2.pdf";

        //    sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
        //    sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();

        //    string BillPath = hdCmpName.Value.ToString() + "\\" + hdLogicalpath.Value.ToString() + ".pdf";


        //    string sz_UserName = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_NAME.ToString();
        //    sz_UserID = ((Bill_Sys_UserObject)(Session["USER_OBJECT"])).SZ_USER_ID.ToString();

        //    MBS_CaseMG2 oj = new MBS_CaseMG2();
        //    oj.SaveLogicalPath(hdID.Value.ToString(), sz_CompanyID, sz_CaseID, BillPath);

        //    oj.SaveUploadDocumentMG2(sz_CaseID, sz_CompanyID, hdLogicalpath.Value.ToString() + ".pdf", BillPath, hdNodeName.Value.ToString(), sz_UserName, sz_UserID, hdNodeID.Value.ToString());


        //}
        //catch (Exception ex)
        //{
        //    //usrMessage.PutMessage(ex.ToString());
        //    //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
        //    //usrMessage.Show();
        //}
        //finally
        //{

        //}
    }
    protected void DDLAttendingDoctors_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sz_doctorID = DDLAttendingDoctors.SelectedValue.ToString();
        FillDoctorInfo(sz_doctorID);
    }

    private void FillDoctorInfo(string sz_doctorID)
    {
        if (DDLAttendingDoctors.SelectedItem.Value.ToString() == "NA")
        {
            TxtIndividualProvider.Text = "";
            TxtTelephone.Text = "";
            TxtFaxNo.Text = "";
        }
        else
        {
            DataSet dsdocInfo = GetDoctorInfo(sz_doctorID);
            if (dsdocInfo.Tables.Count > 0)
            {
                if (dsdocInfo.Tables[0].Rows.Count > 0)
                {
                    TxtIndividualProvider.Text = dsdocInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
                    TxtTelephone.Text = dsdocInfo.Tables[0].Rows[0]["SZ_OFFICE_PHONE"].ToString();
                    TxtFaxNo.Text = dsdocInfo.Tables[0].Rows[0]["SZ_OFFICE_FAX"].ToString();
                }
            }
        }
    }
    //protected void ddlGuidline_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlGuidline.SelectedItem.Text != "--Select--")
    //    {
    //        string stln = ddlGuidline.SelectedItem.Text;
    //        ArrayList ar = new ArrayList();
    //        for (int i = 0; i < stln.Length; i++)
    //        {
    //            ar.Add(stln[i]);
    //        }

    //        TxtGuislineChar.Text = ar[0].ToString();
    //        TxtGuidline1.Text = ar[2].ToString();
    //        TxtGuidline2.Text = ar[3].ToString();
    //        TxtGuidline3.Text = ar[4].ToString();
    //        TxtGuidline4.Text = ar[5].ToString();
    //        TxtGuidline5.Text = ar[6].ToString();
    //    }
    //    else
    //    {
    //        TxtGuislineChar.Text = "";
    //        TxtGuidline1.Text = "";
    //        TxtGuidline2.Text = "";
    //        TxtGuidline3.Text = "";
    //        TxtGuidline4.Text = "";
    //        TxtGuidline5.Text = "";
    //    }
    //}
    protected void extddlSpeciality_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdSpeciality.Value = extddlSpeciality.Selected_Text;
            DataSet dsProcCode = new DataSet();
            dsProcCode = GetDoctorSpecialityProcedureCodeList(extddlSpeciality.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdProcedure.DataSource = dsProcCode;
            grdProcedure.DataBind();
        }
        catch (Exception ex)
        { }
    }

    private DataSet GetDoctorSpecialityProcedureCodeList(string sz_Speciality, string sz_CompanyId)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("GET_DOCT_SPECIALITY_PROCEDURECODE_MG2", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_SPECIALITY", sz_Speciality);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyId);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
    }
    protected void btnSaveBottom_Click(object sender, EventArgs e)
    {
        try
        {
            SaveData();
        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.Message);
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();
        }

    }
    protected void btnPrinBottom_Click(object sender, EventArgs e)
    {
        if (TxtInsuranceNameAdd.Text.Trim() != "" || TxtInsuranceNameAdd.Text.Trim() != ",")
        {

            try
            {
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                SaveData();
                string i_Id = Session["I_ID"].ToString();
                MBS_CASEWISE_MG2 objMBS = new MBS_CASEWISE_MG2();
                DataTable flg = objMBS.GetMG2Record(sz_CompanyID, sz_CaseID, i_Id);
                if (flg.Rows.Count > 0)
                {
                    PrintPDF();
                }
            }
            catch (Exception ex)
            {
                usrMessage.PutMessage(ex.ToString());
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                usrMessage.Show();
            }
            finally
            {

            }
        }
    }
    protected void btnTopMG2Save_Click(object sender, EventArgs e)
    {
        try
        {
            SaveMG21Data();
        }
        catch (Exception ex)
        {
            UserMsgMG21.PutMessage(ex.Message);
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMsgMG21.Show();
        }
    }

    private void SaveMG21Data()
    {
        try
        {

            int I_MG21_ID = 0;
            if (Session["I_MG21_ID"] != null)
            {
                I_MG21_ID = Convert.ToInt32(Session["I_MG21_ID"].ToString());
            }

            string sz_Caseno = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO.ToString();
            string sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            string sz_UserID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
            MG21CasewiseDAO objMG2 = new MG21CasewiseDAO();

            objMG2.I_ID = I_MG21_ID;
            objMG2.sz_CompanyID = sz_CompanyID;
            objMG2.sz_CaseID = sz_CaseID;
            objMG2.sz_UserID = sz_UserID;

            objMG2.WCBCaseNumber = txtMG2_WCBCaseNo.Text;
            objMG2.carrierCaseNumber = txtMG2_CarrierCaseNo.Text;
            objMG2.dateOfInjury = txtMG2_DateOfInjury.Text;
            objMG2.firstName = txtMG2_PatientFirstName.Text;
            objMG2.middleName = txtMG2_PatientMiddleName.Text;
            objMG2.lastName = txtMG2_PatientLastName.Text;
            objMG2.sz_doctor_Name = ddlMG2Doctor.SelectedItem.Text;
            objMG2.sz_doctor_id = ddlMG2Doctor.SelectedValue.ToString();
            objMG2.patientAddress = txtMG2_PatientAddress.Text;
            objMG2.socialSecurityNumber = txtMG2_SocialSecurityNo.Text;
            objMG2.insuranceNameAddress = txtMG2_InsuCarrAddress.Text;
            objMG2.sz_doctorWCBAuth_number = txtMg2_DoctorWCBAuthNo.Text;

            objMG2.sz_guidelines_reference2 = txtMG2_GudRef1.Text;
            objMG2.sz_guidelines_reference22 = txtMG2_GudRef2.Text;
            objMG2.sz_guidelines_reference23 = txtMG2_GudRef3.Text;
            objMG2.sz_guidelines_reference24 = txtMG2_GudRef4.Text;
            objMG2.sz_guidelines_reference25 = txtMG2_GudRef5.Text;


            objMG2.sz_DateOfService2 = txtMG2_DateOfServiceWCB.Text;
            objMG2.sz_DateOfPrevious2 = txtMG2_DateOfPrevDenied.Text;
            objMG2.approvalRequest2 = txtMG2_ApprovalRequest.Text;
            objMG2.sz_MedicalNecessity2 = txtMG2_MedicalNecessity.Text;

            objMG2.sz_guidelines_reference3 = txtMG2_GudRef11.Text;
            objMG2.sz_guidelines_reference32 = txtMG2_GudRef12.Text;
            objMG2.sz_guidelines_reference33 = txtMG2_GudRef13.Text;
            objMG2.sz_guidelines_reference34 = txtMG2_GudRef14.Text;
            objMG2.sz_guidelines_reference35 = txtMG2_GudRef15.Text;

            objMG2.sz_DateOfService3 = txtMG2_DateOfService2.Text;
            objMG2.sz_DateOfPrevious3 = txtMG2_DateOfPrev2.Text;
            objMG2.approvalRequest3 = txtMG2_ApprovalRequest2.Text;
            objMG2.sz_MedicalNecessity3 = txtMG2_MedicalNecessity2.Text;

            objMG2.sz_guidelines_reference4 = txtMG2_GudRef21.Text;
            objMG2.sz_guidelines_reference42 = txtMG2_GudRef22.Text;
            objMG2.sz_guidelines_reference43 = txtMG2_GudRef23.Text;
            objMG2.sz_guidelines_reference44 = txtMG2_GudRef24.Text;
            objMG2.sz_guidelines_reference45 = txtMG2_GudRef25.Text;

            objMG2.sz_DateOfService4 = txtMG2_DateOfService3.Text;
            objMG2.sz_DateOfPrevious4 = txtMG2_DateOfPrev3.Text;
            objMG2.approvalRequest4 = txtMG2_ApprovalRequest3.Text;
            objMG2.sz_MedicalNecessity4 = txtMG2_MedicalNecessity3.Text;

            objMG2.sz_guidelines_reference5 = txtMG2_GudRef31.Text;
            objMG2.sz_guidelines_reference52 = txtMG2_GudRef32.Text;
            objMG2.sz_guidelines_reference53 = txtMG2_GudRef33.Text;
            objMG2.sz_guidelines_reference54 = txtMG2_GudRef34.Text;
            objMG2.sz_guidelines_reference55 = txtMG2_GudRef35.Text;

            objMG2.sz_DateOfService5 = txtMG2_DateOfService4.Text;
            objMG2.sz_DateOfPrevious5 = txtMG2_DateOfPrev4.Text;
            objMG2.approvalRequest5 = txtMG2_ApprovalRequest4.Text;
            objMG2.sz_MedicalNecessity5 = txtMG2_MedicalNecessity4.Text;

            objMG2.WCBCaseNumber2 = txtMG2_WCBCaseNo1.Text;
            objMG2.dateOfInjury2 = txtMG2_DateOfInjury1.Text;

            if (chkMG2_I.Checked)
                objMG2.bt_did = 1;
            else
                objMG2.bt_did = 0;
            if (chkMG2_Did.Checked)
                objMG2.bt_not_did = 1;
            else
                objMG2.bt_not_did = 0;

            objMG2.dt_Tele_Date = txtMG2_TelephoneDate.Text;
            objMG2.sz_spoke_anyone = txtMG2_PersonSpoke.Text;

            if (chkMG2_Copy.Checked)
                objMG2.bt_a_copy = 1;
            else
                objMG2.bt_a_copy = 0;

            objMG2.sz_Fax = txtMG2_Fax.Text;
            objMG2.providerSignDate = txtMg2ProviderDate.Text;

            if (chkMG2_Employer.Checked)
                objMG2.bt_employer = 1;
            else
                objMG2.bt_employer = 0;
            if (chkMG2_RequestCarrier2.Checked)
                objMG2.bt_CarrierReq2 = 1;
            else
                objMG2.bt_CarrierReq2 = 0;
            if (chkMG2_RequestCarrier3.Checked)
                objMG2.bt_CarrierReq3 = 1;
            else
                objMG2.bt_CarrierReq3 = 0;
            if (chkMG2_RequestCarrier4.Checked)
                objMG2.bt_CarrierReq4 = 1;
            else
                objMG2.bt_CarrierReq4 = 0;
            if (chkMG2_RequestCarrier5.Checked)
                objMG2.bt_CarrierReq5 = 1;
            else
                objMG2.bt_CarrierReq5 = 0;
            objMG2.sz_CarrierPrintName = txtMG2Carrier_PrintName.Text;
            objMG2.sz_CarrierPrintTitle = txtMG2CarrierPrint_Title.Text;
            objMG2.sz_CarrierPrintDate = txtMG2Carrier_PrintDate.Text;

            if (chkMg2_Grant1.Checked)
                objMG2.bt_granted2 = 1;
            else
                objMG2.bt_granted2 = 0;
            if (chkMg2_GrantPart1.Checked)
                objMG2.bt_granted_in_part2 = 1;
            else
                objMG2.bt_granted_in_part2 = 0;
            if (chkMg2_Denied.Checked)
                objMG2.bt_denied2 = 1;
            else
                objMG2.bt_denied2 = 0;
            if (chkMg2_Burden.Checked)
                objMG2.bt_burden2 = 1;
            else
                objMG2.bt_burden2 = 0;
            if (chkMg2_Substantially.Checked)
                objMG2.bt_substantialy2 = 1;
            else
                objMG2.bt_substantialy2 = 0;
            if (chkMG2_WithoutPre.Checked)
                objMG2.bt_without_prejudice2 = 1;
            else
                objMG2.bt_without_prejudice2 = 0;


            if (chkMg2_Grant2.Checked)
                objMG2.bt_granted3 = 1;
            else
                objMG2.bt_granted3 = 0;

            if (chkMg2_GrantPart2.Checked)
                objMG2.bt_granted_in_part3 = 1;
            else
                objMG2.bt_granted_in_part3 = 0;
            if (chkMg2_Denied1.Checked)
                objMG2.bt_denied3 = 1;
            else
                objMG2.bt_denied3 = 0;
            if (chkMg2_Burden1.Checked)
                objMG2.bt_burden3 = 1;
            else
                objMG2.bt_burden3 = 0;
            if (chkMg2_Substantially1.Checked)
                objMG2.bt_substantialy3 = 1;
            else
                objMG2.bt_substantialy3 = 0;
            if (chkMG2_WithoutPre1.Checked)
                objMG2.bt_without_prejudice3 = 1;
            else
                objMG2.bt_without_prejudice3 = 0;

            if (chkMg2_Grant3.Checked)
                objMG2.bt_granted4 = 1;
            else
                objMG2.bt_granted4 = 0;

            if (chkMg2_GrantPart3.Checked)
                objMG2.bt_granted_in_part4 = 1;
            else
                objMG2.bt_granted_in_part4 = 0;
            if (chkMg2_Denied2.Checked)
                objMG2.bt_denied4 = 1;
            else
                objMG2.bt_denied4 = 0;
            if (chkMg2_Burden2.Checked)
                objMG2.bt_burden4 = 1;
            else
                objMG2.bt_burden4 = 0;
            if (chkMg2_Substantially2.Checked)
                objMG2.bt_substantialy4 = 1;
            else
                objMG2.bt_substantialy4 = 0;
            if (chkMG2_WithoutPre2.Checked)
                objMG2.bt_without_prejudice4 = 1;
            else
                objMG2.bt_without_prejudice4 = 0;

            if (chkMg2_Grant4.Checked)
                objMG2.bt_granted5 = 1;
            else
                objMG2.bt_granted5 = 0;

            if (chkMg2_GrantPart4.Checked)
                objMG2.bt_granted_in_part5 = 1;
            else
                objMG2.bt_granted_in_part5 = 0;
            if (chkMg2_Denied3.Checked)
                objMG2.bt_denied5 = 1;
            else
                objMG2.bt_denied5 = 0;
            if (chkMg2_Burden3.Checked)
                objMG2.bt_burden5 = 1;
            else
                objMG2.bt_burden5 = 0;
            if (chkMg2_Substantially3.Checked)
                objMG2.bt_substantialy5 = 1;
            else
                objMG2.bt_substantialy5 = 0;
            if (chkMG2_WithoutPre3.Checked)
                objMG2.bt_without_prejudice5 = 1;
            else
                objMG2.bt_without_prejudice5 = 0;

            objMG2.sz_Carrier = txtMG2_Carrier.Text;
            objMG2.sz_NameOfMedProfessional = txtMG2_ProfessinalName.Text;

            if (chkMG2_MedicalArb.Checked)
                objMG2.bt_byMedArb = 1;
            else
                objMG2.bt_byMedArb = 0;
            if (chkMG2_Chair.Checked)
                objMG2.bt_byChair = 1;
            else
                objMG2.bt_byChair = 0;
            objMG2.sz_print_name_D = txtMG2_PrintName.Text;
            objMG2.sz_title_D = txtMG2_Title.Text;
            objMG2.dt_date_D = txtMG2_Date.Text;

            if (chkMG2_DRequest2.Checked)
                objMG2.bt_DenIRequest2 = 1;
            else
                objMG2.bt_DenIRequest2 = 0;
            if (chkMG2_DRequest3.Checked)
                objMG2.bt_DenIRequest3 = 1;
            else
                objMG2.bt_DenIRequest3 = 0;
            if (chkMG2_DRequest4.Checked)
                objMG2.bt_DenIRequest4 = 1;
            else
                objMG2.bt_DenIRequest4 = 0;
            if (chkMG2_DRequest5.Checked)
                objMG2.bt_DenIRequest5 = 1;
            else
                objMG2.bt_DenIRequest5 = 0;
            objMG2.sz_print_name_Den = txtMG2_DenPrintName.Text;
            objMG2.sz_title_Den = txtMG2_DenTitle.Text;
            objMG2.dt_date_Den = txtMG2_DenDate.Text;

            if (chkMG2_Irequest.Checked)
                objMG2.bt_IRequest = 1;
            else
                objMG2.bt_IRequest = 0;
            if (chkMG2_RequestNo2.Checked)
                objMG2.bt_IRequest2 = 1;
            else
                objMG2.bt_IRequest2 = 0;
            if (chkMG2_RequestNo3.Checked)
                objMG2.bt_IRequest3 = 1;
            else
                objMG2.bt_IRequest3 = 0;
            if (chkMG2_RequestNo4.Checked)
                objMG2.bt_IRequest4 = 1;
            else
                objMG2.bt_IRequest4 = 0;
            if (chkMgG2_RequestNo5.Checked)
                objMG2.bt_IRequest5 = 1;
            else
                objMG2.bt_IRequest5 = 0;
            if (chkMG2_MedicalAbr.Checked)
                objMG2.bt_byMedArb2 = 1;
            else
                objMG2.bt_byMedArb2 = 0;
            if (chkMG2_WCB.Checked)
                objMG2.bt_Atwcb = 1;
            else
                objMG2.bt_Atwcb = 0;

            objMG2.claimantSignDate = txtMG2_ClaimDate.Text;
            objMG2.sz_BillNo = "";
            objMG2.PatientID = "";
            objMG2.sz_pdf_url = "";
            objMG2.sz_procedure_group_id = "";

            MBS_CASEWISE_MG21 oj = new MBS_CASEWISE_MG21();
            string i_ids = oj.SaveMG21(objMG2);
            if (i_ids != "0")
            {
                Session["I_MG21_ID"] = i_ids;
            }


            UserMsgMG21.PutMessage("Your changes were made successfully to the server.");
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            UserMsgMG21.Show();
        }

        catch (SqlException ex)
        {
            UserMsgMG21.PutMessage(ex.Message);
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            UserMsgMG21.Show();
        }

    }
    protected void btnTopMG2Print_Click(object sender, EventArgs e)
    {
        try
        {
            Print_MG21();
        }
        catch (Exception ex)
        {
            UserMsgMG21.PutMessage(ex.Message.ToString());
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMsgMG21.Show();
        }
    }

    protected void Print_MG21()
    {
        if (txtMG2_InsuCarrAddress.Text.Trim() != "" || txtMG2_PatientName.Text.Trim() != ",")
        {
            try
            {
                objNF3Template = new Bill_Sys_NF3_Template();
                sz_CompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
                sz_CaseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
                //string i_Id = hdnMG21_Id.Value.ToString();
                SaveMG21Data();
                string i_Id = Session["I_MG21_ID"].ToString();
                MBS_CASEWISE_MG21 objMBS = new MBS_CASEWISE_MG21();
                DataSet dsMG21 = objMBS.GetMG21Record(sz_CompanyID, sz_CaseID, i_Id);

                if (dsMG21.Tables[0].Rows.Count > 0)
                {
                    string OutputFilePath = "";
                    string OpenPath = "";
                    OpenPath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
                    string BaseFilePath = ConfigurationManager.AppSettings["MG2_1_PDF_FILE"].ToString();
                    string BasePath = objNF3Template.getPhysicalPath();
                    BasePath = BasePath + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME + "/" + sz_CaseID + "/Packet Document/";
                    if (!Directory.Exists(BasePath))
                    {
                        Directory.CreateDirectory(BasePath);
                    }
                    string newPdfFilename = "";
                    newPdfFilename = "MG2.1_" + getFileName(sz_CaseID) + ".pdf";
                    OutputFilePath = BasePath + newPdfFilename;
                    OpenPath = OpenPath + newPdfFilename;
                    FormsBO oFormBO = new FormsBO();
                    DataSet dsPdfValue = objMBS.GetMG21Record(sz_CompanyID, sz_CaseID, i_Id); ;
                    PdfReader pdfReader = new PdfReader(BaseFilePath);
                    PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(OutputFilePath, FileMode.Create));
                    AcroFields pdfFormFields = pdfStamper.AcroFields;

                    if (dsMG21.Tables[0] != null)
                    {
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[0]", dsPdfValue.Tables[0].Rows[0]["PatientName"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[2]", dsPdfValue.Tables[0].Rows[0]["sz_wcb_case_no"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[3]", dsPdfValue.Tables[0].Rows[0]["sz_carrier_case_no"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[5]", dsPdfValue.Tables[0].Rows[0]["sz_date_of_injury"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[1]", dsPdfValue.Tables[0].Rows[0]["sz_doctor_Name"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[6]", dsPdfValue.Tables[0].Rows[0]["sz_doctorWCBAuth_number"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField3[4]", dsPdfValue.Tables[0].Rows[0]["sz_security_no"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.0[0]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.1[0]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference22"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10[0]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference23"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.2[0]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference24"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.3[0]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference25"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField2[0]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfService2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField4[0]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfPrevious2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[0].Approval_Requested_for__one_request_type_per_form_1[0]", dsPdfValue.Tables[0].Rows[0]["sz_ApprovalRequest2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[0].TextField2[0]", dsPdfValue.Tables[0].Rows[0]["sz_MedicalNecessity2"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.0[1]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.1[1]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference32"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10[1]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference33"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.2[1]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference34"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.3[1]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference35"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField2[1]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfService3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField4[1]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfPrevious3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[1].Approval_Requested_for__one_request_type_per_form_1[3]", dsPdfValue.Tables[0].Rows[0]["sz_ApprovalRequest3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[1].TextField2[1]", dsPdfValue.Tables[0].Rows[0]["sz_MedicalNecessity3"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.0[2]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.1[2]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference42"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10[2]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference43"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.2[2]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference44"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.3[2]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference45"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField2[2]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfService4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField4[2]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfPrevious4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[2].Approval_Requested_for__one_request_type_per_form_1[6]", dsPdfValue.Tables[0].Rows[0]["sz_ApprovalRequest4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[2].TextField2[2]", dsPdfValue.Tables[0].Rows[0]["sz_MedicalNecessity4"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.0[3]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.1[3]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference52"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].undefined_10[3]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference53"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.2[3]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference54"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].The_undersigned_requests_approval_to_VARY_from_the_WCB_Medical_Treatment_Guidelines_as_indicated_below\\.3[3]", dsPdfValue.Tables[0].Rows[0]["sz_guidelines_reference55"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page1[0].DateTimeField2[3]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfService5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].TextField4[3]", dsPdfValue.Tables[0].Rows[0]["sz_DateOfPrevious5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[3].Approval_Requested_for__one_request_type_per_form_1[9]", dsPdfValue.Tables[0].Rows[0]["sz_approval_request5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page1[0].#area[3].TextField2[3]", dsPdfValue.Tables[0].Rows[0]["sz_MedicalNecessity5"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField1[0]", dsPdfValue.Tables[0].Rows[0]["PatientName"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField1[1]", dsPdfValue.Tables[0].Rows[0]["sz_wcb_case_no2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField1[2]", dsPdfValue.Tables[0].Rows[0]["sz_date_of_injury2"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[4]", dsPdfValue.Tables[0].Rows[0]["bt_did"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[5]", dsPdfValue.Tables[0].Rows[0]["bt_not_did"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].DateTimeField3[0]", dsPdfValue.Tables[0].Rows[0]["dt_Tele_Date"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[0]", dsPdfValue.Tables[0].Rows[0]["sz_spoke_anyone"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[10]", dsPdfValue.Tables[0].Rows[0]["bt_a_copy"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[1]", dsPdfValue.Tables[0].Rows[0]["sz_Fax"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].DateTimeField3[4]", dsPdfValue.Tables[0].Rows[0]["dt_provider_signature_date"].ToString());

                        if (dsPdfValue.Tables[0].Rows[0]["bt_employer"].ToString() == "1")
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box91[0]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box91[0]", "0");
                        }

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[6]", dsPdfValue.Tables[0].Rows[0]["bt_CarrierReq2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[9]", dsPdfValue.Tables[0].Rows[0]["bt_CarrierReq3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[8]", dsPdfValue.Tables[0].Rows[0]["bt_CarrierReq4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[7]", dsPdfValue.Tables[0].Rows[0]["bt_CarrierReq5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField2[1]", dsPdfValue.Tables[0].Rows[0]["sz_CarrierPrintName"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField2[0]", dsPdfValue.Tables[0].Rows[0]["sz_CarrierPrintTitle"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].DateTimeField3[3]", dsPdfValue.Tables[0].Rows[0]["sz_CarrierPrintDate"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[11]", dsPdfValue.Tables[0].Rows[0]["bt_granted2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[14]", dsPdfValue.Tables[0].Rows[0]["bt_granted_in_part2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[13]", dsPdfValue.Tables[0].Rows[0]["bt_denied2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[12]", dsPdfValue.Tables[0].Rows[0]["bt_burden2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[20]", dsPdfValue.Tables[0].Rows[0]["bt_substantialy2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[15]", dsPdfValue.Tables[0].Rows[0]["bt_without_prejudice2"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[33]", dsPdfValue.Tables[0].Rows[0]["bt_granted3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[36]", dsPdfValue.Tables[0].Rows[0]["bt_granted_in_part3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[35]", dsPdfValue.Tables[0].Rows[0]["bt_denied3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[34]", dsPdfValue.Tables[0].Rows[0]["bt_burden3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[38]", dsPdfValue.Tables[0].Rows[0]["bt_substantialy3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[37]", dsPdfValue.Tables[0].Rows[0]["bt_without_prejudice3"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[27]", dsPdfValue.Tables[0].Rows[0]["bt_granted4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[30]", dsPdfValue.Tables[0].Rows[0]["bt_granted_in_part4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[29]", dsPdfValue.Tables[0].Rows[0]["bt_denied4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[28]", dsPdfValue.Tables[0].Rows[0]["bt_burden4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[32]", dsPdfValue.Tables[0].Rows[0]["bt_substantialy4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[31]", dsPdfValue.Tables[0].Rows[0]["bt_without_prejudice4"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[21]", dsPdfValue.Tables[0].Rows[0]["bt_granted5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[24]", dsPdfValue.Tables[0].Rows[0]["bt_granted_in_part5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[23]", dsPdfValue.Tables[0].Rows[0]["bt_denied5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[22]", dsPdfValue.Tables[0].Rows[0]["bt_burden5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[26]", dsPdfValue.Tables[0].Rows[0]["bt_substantialy5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[25]", dsPdfValue.Tables[0].Rows[0]["bt_without_prejudice5"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[2]", dsPdfValue.Tables[0].Rows[0]["sz_Carrier"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].Approval_Requested_for__one_request_type_per_form_1[5]", dsPdfValue.Tables[0].Rows[0]["sz_NameOfMedProfessional"].ToString());

                        if (dsPdfValue.Tables[0].Rows[0]["bt_byMedArb"].ToString() == "1")
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[3]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[3]", "0");
                        }

                        if (dsPdfValue.Tables[0].Rows[0]["bt_byChair"].ToString() == "1")
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[2]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[2]", "0");
                        }
                        //pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[3]", dsPdfValue.Tables[0].Rows[0]["bt_byMedArb"].ToString());
                        //pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[2]", dsPdfValue.Tables[0].Rows[0]["bt_byChair"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField2[4]", dsPdfValue.Tables[0].Rows[0]["sz_print_name_D"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField2[3]", dsPdfValue.Tables[0].Rows[0]["sz_title_D"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].DateTimeField3[1]", dsPdfValue.Tables[0].Rows[0]["dt_date_D"].ToString());

                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[0]", dsPdfValue.Tables[0].Rows[0]["bt_DenIRequest2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[1]", dsPdfValue.Tables[0].Rows[0]["bt_DenIRequest3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[2]", dsPdfValue.Tables[0].Rows[0]["bt_DenIRequest4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[3]", dsPdfValue.Tables[0].Rows[0]["bt_DenIRequest5"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField2[7]", dsPdfValue.Tables[0].Rows[0]["sz_print_name_Den"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].TextField2[6]", dsPdfValue.Tables[0].Rows[0]["sz_title_Den"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].DateTimeField3[2]", dsPdfValue.Tables[0].Rows[0]["dt_date_Den"].ToString());

                        if (dsPdfValue.Tables[0].Rows[0]["bt_IRequest"].ToString() == "1")
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box91[1]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box91[1]", "0");
                        }
                        //pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box91[1]", dsPdfValue.Tables[0].Rows[0]["bt_IRequest"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[16]", dsPdfValue.Tables[0].Rows[0]["bt_IRequest2"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[17]", dsPdfValue.Tables[0].Rows[0]["bt_IRequest3"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[18]", dsPdfValue.Tables[0].Rows[0]["bt_IRequest4"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].CheckBox1[19]", dsPdfValue.Tables[0].Rows[0]["bt_IRequest5"].ToString());

                        if (dsPdfValue.Tables[0].Rows[0]["bt_byMedArb2"].ToString() == "1")
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[1]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[1]", "0");
                        }
                        if (dsPdfValue.Tables[0].Rows[0]["bt_Atwcb"].ToString() == "1")
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[0]", "1");
                        }
                        else
                        {
                            pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[0]", "0");
                        }
                        //pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[1]", dsPdfValue.Tables[0].Rows[0]["bt_byMedArb2"].ToString());
                        //pdfFormFields.SetField("topmostSubform[0].Page2[0].Check_Box65[0]", dsPdfValue.Tables[0].Rows[0]["bt_Atwcb"].ToString());
                        pdfFormFields.SetField("topmostSubform[0].Page2[0].DateTimeField3[5]", dsPdfValue.Tables[0].Rows[0]["dt_claimant_date"].ToString());



                    }
                    pdfStamper.FormFlattening = true;
                    pdfStamper.Close();
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "window.open('" + OpenPath + "');", true);
                }
                else
                {
                    UserMsgMG21.PutMessage("Please Save Record First.");
                    UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                    UserMsgMG21.Show();
                }

            }
            catch (Exception ex)
            {
                UserMsgMG21.PutMessage(ex.Message.ToString());
                UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
                UserMsgMG21.Show();
            }
            finally
            {

            }
        }
    }
    protected void ddlMG2Doctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sz_doctorID = ddlMG2Doctor.SelectedValue.ToString();
        FillDoctorInfo_MG21(sz_doctorID);
    }

    private void FillDoctorInfo_MG21(string sz_doctorID)
    {
        DataSet dsdocInfo = GetDoctorInfo(sz_doctorID);
        if (dsdocInfo.Tables.Count > 0)
        {
            if (dsdocInfo.Tables[0].Rows.Count > 0)
            {
                txtMg2_DoctorWCBAuthNo.Text = dsdocInfo.Tables[0].Rows[0]["SZ_WCB_AUTHORIZATION"].ToString();
            }
        }
    }
    //protected void ddl2MG2GuideRef_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl2MG2GuideRef.SelectedItem.Text != "--Select--")
    //    {
    //        string stln = ddl2MG2GuideRef.SelectedItem.Text;
    //        ArrayList ar = new ArrayList();
    //        for (int i = 0; i < stln.Length; i++)
    //        {
    //            ar.Add(stln[i]);
    //        }

    //        txtMG2_GudRef1.Text = ar[0].ToString();
    //        txtMG2_GudRef2.Text = ar[2].ToString();
    //        txtMG2_GudRef3.Text = ar[3].ToString();
    //        txtMG2_GudRef4.Text = ar[4].ToString();
    //        txtMG2_GudRef5.Text = ar[5].ToString();
    //        txtMG2_GudRef6.Text = ar[6].ToString();
    //    }
    //    else
    //    {
    //        txtMG2_GudRef1.Text = "";
    //        txtMG2_GudRef2.Text = "";
    //        txtMG2_GudRef3.Text = "";
    //        txtMG2_GudRef4.Text = "";
    //        txtMG2_GudRef5.Text = "";
    //        txtMG2_GudRef6.Text = "";
    //    }
    //}
    //protected void ddl3MG2_GudRef_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl3MG2_GudRef.SelectedItem.Text != "--Select--")
    //    {
    //        string stln = ddl3MG2_GudRef.SelectedItem.Text;
    //        ArrayList ar = new ArrayList();
    //        for (int i = 0; i < stln.Length; i++)
    //        {
    //            ar.Add(stln[i]);
    //        }

    //        txtMG2_GudRef11.Text = ar[0].ToString();
    //        txtMG2_GudRef12.Text = ar[2].ToString();
    //        txtMG2_GudRef13.Text = ar[3].ToString();
    //        txtMG2_GudRef14.Text = ar[4].ToString();
    //        txtMG2_GudRef15.Text = ar[5].ToString();
    //        txtMG2_GudRef16.Text = ar[6].ToString();
    //    }
    //    else
    //    {
    //        txtMG2_GudRef11.Text = "";
    //        txtMG2_GudRef12.Text = "";
    //        txtMG2_GudRef13.Text = "";
    //        txtMG2_GudRef14.Text = "";
    //        txtMG2_GudRef15.Text = "";
    //        txtMG2_GudRef16.Text = "";
    //    }
    //}
    //protected void ddl4MG2_GudRef_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl4MG2_GudRef.SelectedItem.Text != "--Select--")
    //    {
    //        string stln = ddl4MG2_GudRef.SelectedItem.Text;
    //        ArrayList ar = new ArrayList();
    //        for (int i = 0; i < stln.Length; i++)
    //        {
    //            ar.Add(stln[i]);
    //        }

    //        txtMG2_GudRef21.Text = ar[0].ToString();
    //        txtMG2_GudRef22.Text = ar[2].ToString();
    //        txtMG2_GudRef23.Text = ar[3].ToString();
    //        txtMG2_GudRef24.Text = ar[4].ToString();
    //        txtMG2_GudRef25.Text = ar[5].ToString();
    //        txtMG2_GudRef26.Text = ar[6].ToString();
    //    }
    //    else
    //    {
    //        txtMG2_GudRef21.Text = "";
    //        txtMG2_GudRef22.Text = "";
    //        txtMG2_GudRef23.Text = "";
    //        txtMG2_GudRef24.Text = "";
    //        txtMG2_GudRef25.Text = "";
    //        txtMG2_GudRef26.Text = "";
    //    }
    //}
    //protected void ddl5MG2_GudRef_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddl5MG2_GudRef.SelectedItem.Text != "--Select--")
    //    {
    //        string stln = ddl5MG2_GudRef.SelectedItem.Text;
    //        ArrayList ar = new ArrayList();
    //        for (int i = 0; i < stln.Length; i++)
    //        {
    //            ar.Add(stln[i]);
    //        }

    //        txtMG2_GudRef31.Text = ar[0].ToString();
    //        txtMG2_GudRef32.Text = ar[2].ToString();
    //        txtMG2_GudRef33.Text = ar[3].ToString();
    //        txtMG2_GudRef34.Text = ar[4].ToString();
    //        txtMG2_GudRef35.Text = ar[5].ToString();
    //        txtMG2_GudRef36.Text = ar[6].ToString();
    //    }
    //    else
    //    {
    //        txtMG2_GudRef31.Text = "";
    //        txtMG2_GudRef32.Text = "";
    //        txtMG2_GudRef33.Text = "";
    //        txtMG2_GudRef34.Text = "";
    //        txtMG2_GudRef35.Text = "";
    //        txtMG2_GudRef36.Text = "";
    //    }
    //}
    protected void btnMG2Save_Click(object sender, EventArgs e)
    {
        try
        {
            SaveMG21Data();
        }
        catch (Exception ex)
        {
            UserMsgMG21.PutMessage(ex.Message);
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMsgMG21.Show();
        }
    }
    protected void btnMG2Print_Click(object sender, EventArgs e)
    {
        try
        {
            Print_MG21();
        }
        catch (Exception ex)
        {
            UserMsgMG21.PutMessage(ex.Message.ToString());
            UserMsgMG21.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMsgMG21.Show();
        }

    }

    protected void ASPxGridView4_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
    {
        string MGType = ASPxGridView4.GetRowValues(e.VisibleIndex, "Type").ToString();

        if (MGType == "MG1")
        {
            Session["I_ID"] = ASPxGridView4.GetRowValues(e.VisibleIndex, "I_ID").ToString();

            string i_id = Session["I_ID"].ToString();
            GetRecordMG1();
        }
        else if (MGType == "MG1.1")
        {
            Session["I_ID"] = ASPxGridView4.GetRowValues(e.VisibleIndex, "I_ID").ToString();

            string i_id = Session["I_ID"].ToString();
            GetRecordMG11();
        }
        else if (MGType == "MG2")
        {
            Session["I_ID"] = ASPxGridView4.GetRowValues(e.VisibleIndex, "I_ID").ToString();

            string i_id = Session["I_ID"].ToString();
            GetRecord();

            DataSet dsProcCode;
            try
            {
                hdSpeciality.Value = extddlSpeciality.Selected_Text;
                dsProcCode = new DataSet();
                dsProcCode = GetDoctorSpecialityProcedureCodeList(extddlSpeciality.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                grdProcedure.DataSource = dsProcCode;
                grdProcedure.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            DataSet dsGetProcCodes = GetProcedureCodes(i_id);
            if (dsGetProcCodes != null)
            {
                if (dsGetProcCodes.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < grdProcedure.Items.Count; i++)
                    {
                        CheckBox chk = (CheckBox)grdProcedure.Items[i].FindControl("chkselect");
                        for (int j = 0; j < dsGetProcCodes.Tables[0].Rows.Count; j++)
                        {
                            if (grdProcedure.DataKeys[i].ToString().Equals(dsGetProcCodes.Tables[0].Rows[j]["SZ_TYPE_CODE_ID"].ToString()))
                            {
                                chk.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        else if (MGType == "MG2.1")
        {
            Session["I_MG21_ID"] = ASPxGridView4.GetRowValues(e.VisibleIndex, "I_ID").ToString();

            string i_id = Session["I_MG21_ID"].ToString();
            GetRecordMG21();
        }
    }

    private void GetRecordMG1()
    {
        try
        {
            MBS_CASEWISE_MG1 oj = new MBS_CASEWISE_MG1();
            string sz_CaseID12 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_CompanyID12 = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            DataTable dt = oj.GetMG1Records(sz_CompanyID12, sz_CaseID12, Session["I_ID"].ToString());

            if (dt.Rows.Count > 0)
            {
                //TextBoxes of A
                txtMG1WCBCaseNumber.Text = dt.Rows[0]["sz_wcb_case_no"].ToString();
                txtMG1CarrierCaseNo.Text = dt.Rows[0]["sz_carrier_case_no"].ToString();
                txtMG1DateOfInjury.Text = dt.Rows[0]["sz_date_of_injury"].ToString();
                txtMG1PatientName.Text = dt.Rows[0]["sz_patient_name"].ToString();
                txtMG1socialSecurityNo.Text = dt.Rows[0]["sz_security_no"].ToString();
                txtMG1PatientAddress.Text = dt.Rows[0]["sz_patient_address"].ToString();
                txtMG1EmpNameAndAddress.Text = dt.Rows[0]["sz_employee_name_address"].ToString();
                txtMG1InsuranceCarrier.Text = dt.Rows[0]["sz_insurance_name_address"].ToString();

                //TextBoxes of B
                for (int i = 0; i < ddlAttendingDoctor.Items.Count; i++)
                {
                    if (ddlAttendingDoctor.Items[i].Value == dt.Rows[0]["sz_doctor_ID"].ToString())
                    {
                        ddlAttendingDoctor.SelectedValue = ddlAttendingDoctor.Items[i].Value;
                        break;
                    }
                }

                txtMG1WCBAuthOne.Text = dt.Rows[0]["sz_individual_provider1"].ToString();
                txtMG1WCBAuthTwo.Text = dt.Rows[0]["sz_individual_provider2"].ToString();
                txtMG1WCBAuthThree.Text = dt.Rows[0]["sz_individual_provider3"].ToString();
                txtMG1WCBAuthFour.Text = dt.Rows[0]["sz_individual_provider4"].ToString();
                txtMG1WCBAuthFive.Text = dt.Rows[0]["sz_individual_provider5"].ToString();
                txtMG1WCBAuthSix.Text = dt.Rows[0]["sz_individual_provider6"].ToString();
                txtMG1WCBAuthSeven.Text = dt.Rows[0]["sz_individual_provider7"].ToString();
                txtMG1WCBAuthEight.Text = dt.Rows[0]["sz_individual_provider8"].ToString();

                txtMG1Telephone.Text = dt.Rows[0]["sz_teltphone_no"].ToString();
                txtMG1FaxNo.Text = dt.Rows[0]["sz_fax_no"].ToString();


                //TextBoxes of C
                txtMG1DateSubmitted.Text = dt.Rows[0]["dt_date_request_submitted"].ToString();
                txtMG1Procedure.Text = dt.Rows[0]["sz_procedure_Requested"].ToString();

                txtMG1Guideline.Text = dt.Rows[0]["sz_Guidline_Char"].ToString();
                txtMG1GuidelineOne.Text = dt.Rows[0]["sz_Guidline1"].ToString();
                txtMG1GuidelineTwo.Text = dt.Rows[0]["sz_Guidline2"].ToString();
                txtMG1GuidelineThree.Text = dt.Rows[0]["sz_Guidline3"].ToString();
                txtMG1GuidelineFour.Text = dt.Rows[0]["sz_Guidline4"].ToString();

                txtMG1DateOfService.Text = dt.Rows[0]["sz_wcb_case_file"].ToString();
                txtMG1Comments.Text = dt.Rows[0]["sz_comments"].ToString();
                txtMG1TelephoneTwo.Text = dt.Rows[0]["sz_spoke"].ToString();
                txtMG1SpokenTo.Text = dt.Rows[0]["sz_spoke_anyone"].ToString();
                txtMG1CopyOfForm.Text = dt.Rows[0]["sz_fund_by"].ToString();
                txtMG1DateC.Text = dt.Rows[0]["dt_provider_signature_date"].ToString();

                //TextBoxes of D
                txtCarrierReverse.Text = dt.Rows[0]["sz_carrier_reverse"].ToString();
                txtMG1MedicalProfessional.Text = dt.Rows[0]["sz_if_applicable"].ToString();
                txtMG1PrintNameOne.Text = dt.Rows[0]["sz_print_name_D"].ToString();
                txtMG1Title.Text = dt.Rows[0]["sz_title_D"].ToString();
                txtMG1DateD.Text = dt.Rows[0]["dt_date_D"].ToString();

                //TextBoxes of E
                txtMG1MedicalReport.Text = dt.Rows[0]["dt_supporting_medical_on"].ToString();
                txtMG1DateE.Text = dt.Rows[0]["dt_date_E"].ToString();

                //TextBoxes of F
                txtMG1Denied.Text = dt.Rows[0]["dt_initial_denied"].ToString();
                txtMG1PrintNameTwo.Text = dt.Rows[0]["sz_print_name_F"].ToString();
                txtMG1TitleTwo.Text = dt.Rows[0]["sz_title_F"].ToString();
                txtMG1DateF.Text = dt.Rows[0]["dt_date_F"].ToString();

                //CheckBoxes C

                if (dt.Rows[0]["bt_did"].ToString() == "Yes")
                    chkMG1Approval.Checked = true;
                else
                    chkMG1Approval.Checked = false;

                if (dt.Rows[0]["bt_not_did"].ToString() == "Yes")
                    ChkMG1DidNotContact.Checked = true;
                else
                    ChkMG1DidNotContact.Checked = false;

                if (dt.Rows[0]["bt_a_copy"].ToString() == "Yes")
                    chkMG1CopyOfForm.Checked = true;
                else
                    chkMG1CopyOfForm.Checked = false;

                //D

                if (dt.Rows[0]["bt_granted"].ToString() == "1")
                    chkMG1Granted.Checked = true;
                else
                    chkMG1Granted.Checked = false;

                if (dt.Rows[0]["bt_without_prejudice"].ToString() == "1")
                    chkMG1GrantedPrejudice.Checked = true;
                else
                    chkMG1GrantedPrejudice.Checked = false;

                if (dt.Rows[0]["bt_denied"].ToString() == "1")
                    chkMG1DeniedDenial.Checked = true;
                else
                    chkMG1DeniedDenial.Checked = false;

                //E 
                if (dt.Rows[0]["sz_section_E"].ToString() == "Yes")
                    chkMG1MedicalArbitrator.Checked = true;
                else
                    chkMG1MedicalArbitrator.Checked = false;

                // F 
                if (dt.Rows[0]["sz_certify"].ToString() == "Yes")
                    chkMG1Certify.Checked = true;
                else
                    chkMG1Certify.Checked = false;

                tabcontainerPatientEntry.ActiveTabIndex = 2;



            }
        }
        catch (Exception ex)
        {
            MessageControl2.PutMessage(ex.ToString());
            MessageControl2.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            MessageControl2.Show();
        }
        finally
        {

        }
    }

    private void GetRecordMG11()
    {
        try
        {
            string sz_CaseID12 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_CompanyID12 = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            DataTable dt = obj11.GetMG11Records(sz_CompanyID12, sz_CaseID12, Session["I_ID"].ToString());

            if (dt.Rows.Count > 0)
            {
                txtWCBNumber.Text = dt.Rows[0]["WCBCaseNumber"].ToString();
                txtCasenumber_MG11.Text = dt.Rows[0]["carrierCaseNumber"].ToString();
                txtInjuryDate_MG11.Text = dt.Rows[0]["dateOfInjury"].ToString();
                txtPatientName_MG11.Text = dt.Rows[0]["firstName"].ToString();
                txtMiddleName_MG11.Text = dt.Rows[0]["MiddleName"].ToString();
                txtLastName_MG11.Text = dt.Rows[0]["lastName"].ToString();
                txtSocialNumber_MG11.Text = dt.Rows[0]["socialSecurityNumber"].ToString();
                txtPatient_Address_MG11.Text = dt.Rows[0]["patientAddress"].ToString();
                txtDoctor_WCB.Text = dt.Rows[0]["DoctorWCBNumber"].ToString();
                txt_Treatment_One.Text = dt.Rows[0]["TreatmentOne"].ToString();
                txtGuideline_One.Text = dt.Rows[0]["GuidelineOne"].ToString();
                txtGuideline_BoxOne.Text = dt.Rows[0]["GuidelineBoxOne"].ToString();
                txtGuideline_BoxTwo.Text = dt.Rows[0]["GuidelineBoxTwo"].ToString();
                txtGuideline_BoxThree.Text = dt.Rows[0]["GuidelineBoxThree"].ToString();
                txtGuideline_BoxFour.Text = dt.Rows[0]["GuidelineBoxfour"].ToString();
                txt_DateOfService.Text = dt.Rows[0]["dateOfServiceOne"].ToString();
                txtComments_One.Text = dt.Rows[0]["sz_Comments"].ToString();
                txt_Treatement_two.Text = dt.Rows[0]["TreatmentTwo"].ToString();
                txt_Guideline_two.Text = dt.Rows[0]["GuidelineTwo"].ToString();
                txt_Guideline_Box_Five.Text = dt.Rows[0]["GuidelineFive"].ToString();
                txtGuideline_Box_Six.Text = dt.Rows[0]["GuidelineBoxSix"].ToString();
                txt_Guideline_Box_Seven.Text = dt.Rows[0]["GuidelineBoxSeven"].ToString();
                txt_Guideline_Box_Eight.Text = dt.Rows[0]["GuidelineBoxEight"].ToString();
                txt_Date_of_Service_two.Text = dt.Rows[0]["dateOfServiceTwo"].ToString();
                txt_Comments_two.Text = dt.Rows[0]["sz_Comments_two"].ToString();
                txt_Treatment_three.Text = dt.Rows[0]["TreatmentThree"].ToString();
                txt_Guideline_three.Text = dt.Rows[0]["GuidelineThree"].ToString();
                txt_Guideline_BoxNine.Text = dt.Rows[0]["GuidelineBoxNine"].ToString();
                txt_Guideline_Box_ten.Text = dt.Rows[0]["GuidelineBoxTen"].ToString();
                txtGuideline_Box_eleven.Text = dt.Rows[0]["GuidelineBoxeleven"].ToString();
                txt_Guideline_Box_twelve.Text = dt.Rows[0]["GuidelineBoxtwelve"].ToString();
                txt_Date_Of_Service_three.Text = dt.Rows[0]["dateOfServiceThree"].ToString();
                txt_Comments_three.Text = dt.Rows[0]["sz_Comments_three"].ToString();
                txt_Treatment_four.Text = dt.Rows[0]["TreatmentFour"].ToString();
                txt_Guideline_four.Text = dt.Rows[0]["GuidelineFour"].ToString();
                txt_Guideline_Box_thirteen.Text = dt.Rows[0]["GuidelineThirteen"].ToString();
                txt_Guideline_Box_fourteen.Text = dt.Rows[0]["GuidelineBoxfourteen"].ToString();
                txt_Guideline_Box_fifteen.Text = dt.Rows[0]["GuidelineBoxfifteen"].ToString();
                txt_Guideline_Sixteen.Text = dt.Rows[0]["GuidelineBoxsixteen"].ToString();
                txt_Date_Of_Service_four.Text = dt.Rows[0]["dateOfServiceFour"].ToString();
                txt_Comments_four.Text = dt.Rows[0]["sz_Comments_four"].ToString();
                txt_Carrier_One.Text = dt.Rows[0]["Carrier_One"].ToString();
                txt_Carrier_two.Text = dt.Rows[0]["Carrier_two"].ToString();
                txt_Carrier_three.Text = dt.Rows[0]["Carrier_three"].ToString();
                txt_Date.Text = dt.Rows[0]["CarrierDate"].ToString();
                txt_employer.Text = dt.Rows[0]["Employer"].ToString();
                txt_Medical_Professional.Text = dt.Rows[0]["MedicalProfessional"].ToString();
                txt_Print_Name_One.Text = dt.Rows[0]["PrintNameOne"].ToString();
                txt_Title_One.Text = dt.Rows[0]["TitleOne"].ToString();
                txt_Date_employer_One.Text = dt.Rows[0]["EmployerDate"].ToString();
                txt_Date_Medical.Text = dt.Rows[0]["MedicalDate"].ToString();
                txt_Provider_Date.Text = dt.Rows[0]["ProviderDate"].ToString();
                txt_Provider_request.Text = dt.Rows[0]["Provider_request"].ToString();
                txt_Print_Name_two.Text = dt.Rows[0]["Print_Name_two"].ToString();
                txt_Title_two.Text = dt.Rows[0]["Title_two"].ToString();
                txt_Date_employer_two.Text = dt.Rows[0]["EmployerDate_two"].ToString();

                for (int i = 0; i < ddlDoctor_Name_MG11.Items.Count; i++)
                {
                    if (ddlDoctor_Name_MG11.Items[i].Value == dt.Rows[0]["sz_doctor_ID"].ToString())
                    {
                        ddlDoctor_Name_MG11.SelectedValue = ddlDoctor_Name_MG11.Items[i].Value;
                        break;
                    }
                }


                if (dt.Rows[0]["cbGranted_One"].ToString() == "1")
                    cbGranted_One.Checked = true;
                else
                    cbGranted_One.Checked = false;

                if (dt.Rows[0]["CbGrantedPrejudice_One"].ToString() == "1")
                    cbGrantedPrejudice_One.Checked = true;
                else
                    cbGrantedPrejudice_One.Checked = false;

                if (dt.Rows[0]["CbDenied_One"].ToString() == "1")
                    cbDenied_One.Checked = true;
                else
                    cbDenied_One.Checked = false;

                if (dt.Rows[0]["cbGranted_Two"].ToString() == "1")
                    cbGranted_two.Checked = true;
                else
                    cbGranted_two.Checked = false;

                if (dt.Rows[0]["CbGrantedPrejudice_Two"].ToString() == "1")
                    cbGrantedPrejudice_two.Checked = true;
                else
                    cbGrantedPrejudice_two.Checked = false;

                if (dt.Rows[0]["CbDenied_Two"].ToString() == "1")
                    cbDenied_two.Checked = true;
                else
                    cbDenied_two.Checked = false;

                if (dt.Rows[0]["cbGranted_Three"].ToString() == "1")
                    cbGranted_three.Checked = true;
                else
                    cbGranted_three.Checked = false;

                if (dt.Rows[0]["CbGrantedPrejudice_Three"].ToString() == "1")
                    cbGrantedPrejudice_three.Checked = true;
                else
                    cbGrantedPrejudice_three.Checked = false;

                if (dt.Rows[0]["CbDenied_Three"].ToString() == "1")
                    cbDenied_three.Checked = true;
                else
                    cbDenied_three.Checked = false;

                if (dt.Rows[0]["cbGranted_Four"].ToString() == "1")
                    cbGranted_four.Checked = true;
                else
                    cbGranted_four.Checked = false;

                if (dt.Rows[0]["CbGrantedPrejudice_Four"].ToString() == "1")
                    cbGrantedPrejudice_four.Checked = true;
                else
                    cbGrantedPrejudice_four.Checked = false;

                if (dt.Rows[0]["CbDenied_Four"].ToString() == "1")
                    cbDenied_four.Checked = true;
                else
                    cbDenied_four.Checked = false;

                if (dt.Rows[0]["CbContactOne"].ToString() == "1")
                    cb_Contact_One.Checked = true;
                else
                    cb_Contact_One.Checked = false;

                if (dt.Rows[0]["CbContacttwo"].ToString() == "1")
                    cb_Contact_two.Checked = true;
                else
                    cb_Contact_two.Checked = false;

                if (dt.Rows[0]["CbCarrier_One"].ToString() == "1")
                    cb_Carrier_One.Checked = true;
                else
                    cb_Carrier_One.Checked = false;

                if (dt.Rows[0]["CBProvider"].ToString() == "1")
                    cbProvider.Checked = true;
                else
                    cbProvider.Checked = false;

                if (dt.Rows[0]["Medical_request_two"].ToString() == "1")
                    cb_Medical_request_two.Checked = true;
                else
                    cb_Medical_request_two.Checked = false;

                if (dt.Rows[0]["MedicalDate_three"].ToString() == "1")
                    cb_Medical_request_three.Checked = true;
                else
                    cb_Medical_request_three.Checked = false;

                if (dt.Rows[0]["CBMedical_request_four"].ToString() == "1")
                    cb_Medical_request_four.Checked = true;
                else
                    cb_Medical_request_four.Checked = false;

                if (dt.Rows[0]["MedicalDate_five"].ToString() == "1")
                    cb_Medical_request_five.Checked = true;
                else
                    cb_Medical_request_five.Checked = false;

                if (dt.Rows[0]["CBProvider_request"].ToString() == "1")
                    cb_Provider_request.Checked = true;
                else
                    cb_Provider_request.Checked = false;

                if (dt.Rows[0]["CB_request_two"].ToString() == "1")
                    cb_request_two.Checked = true;
                else
                    cb_request_two.Checked = false;

                if (dt.Rows[0]["CB_request_three"].ToString() == "1")
                    cb_request_three.Checked = true;
                else
                    cb_request_three.Checked = false;

                if (dt.Rows[0]["CB_request_four"].ToString() == "1")
                    cb_request_four.Checked = true;
                else
                    cb_request_four.Checked = false;

                if (dt.Rows[0]["CB_request_five"].ToString() == "1")
                    cb_request_five.Checked = true;
                else
                    cb_request_five.Checked = false;
            }
            //carTabPage.ActiveTabIndex = 1;
            tabcontainerPatientEntry.ActiveTabIndex = 3;

        }
        catch (Exception ex)
        {
            UserMessageMG11.PutMessage(ex.ToString());
            UserMessageMG11.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMessageMG11.Show();
        }
        finally
        {

        }
    }

    public void GetRecord()
    {
        try
        {
            MBS_CASEWISE_MG2 oj = new MBS_CASEWISE_MG2();
            string sz_CaseID12 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_CompanyID12 = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            DataTable dt = oj.GetMG2Record(sz_CompanyID12, sz_CaseID12, Session["I_ID"].ToString());

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["sz_guidelines_reference"].ToString() == "" || dt.Rows[0]["sz_guidelines_reference"].ToString() == "NA")
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("--Select Doctor--", "NA");
                    if (DDLAttendingDoctors.SelectedItem.Value.ToString()!="NA")
                    {
                        DDLAttendingDoctors.Items.Insert(0, item);
                        DDLAttendingDoctors.SelectedIndex = 0;
                    }
                    else
                    {
                        DDLAttendingDoctors.SelectedIndex = 0;
                    }
                }
                else
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("--Select Doctor--", "NA");
                    if (!DDLAttendingDoctors.Items.Contains(item))
                    {
                        DDLAttendingDoctors.Items.Insert(0, item);
                        DDLAttendingDoctors.SelectedItem.Text = dt.Rows[0]["sz_guidelines_reference"].ToString();
                    }
                    
                }
                TxtApproval.Text = dt.Rows[0]["sz_approval_request"].ToString();
                TxtDateofService.Text = dt.Rows[0]["sz_wcb_case_file"].ToString();
                TxtDateofApplicable.Text = dt.Rows[0]["sz_applicable"].ToString();
                if (dt.Rows[0]["bt_did"].ToString() == "1")
                    Chkdid.Checked = true;
                else
                    Chkdid.Checked = false;
                if (dt.Rows[0]["bt_not_did"].ToString() == "1")
                    Chkdidnot.Checked = true;
                else
                    Chkdidnot.Checked = false;
                TxtSpoke.Text = dt.Rows[0]["sz_spoke"].ToString();
                Txtspecktoanyone.Text = dt.Rows[0]["sz_spoke_anyone"].ToString();
                if (dt.Rows[0]["bt_a_copy"].ToString() == "1")
                    ChkAcopy.Checked = true;
                else
                    ChkAcopy.Checked = false;
                TxtAddressRequired.Text = dt.Rows[0]["sz_fund_by"].ToString();
                if (dt.Rows[0]["bt_equipped"].ToString() == "1")
                    ChkIAmnot.Checked = true;
                else
                    ChkIAmnot.Checked = false;
                Txtaboveon.Text = dt.Rows[0]["sz_indicated"].ToString();

                if (dt.Rows[0]["dt_provider_signature_date"].ToString() != "" && dt.Rows[0]["dt_provider_signature_date"].ToString() != "01/01/1900")
                    TxtProviderDate.Text = dt.Rows[0]["dt_provider_signature_date"].ToString();
                else
                    TxtProviderDate.Text = "";

                if (dt.Rows[0]["bt_self_insurrer"].ToString() == "1")
                    ChktheSelf.Checked = true;
                else
                    ChktheSelf.Checked = false;
                TxtByPrintNameD.Text = dt.Rows[0]["sz_print_name_D"].ToString();
                TxtTitleD.Text = dt.Rows[0]["sz_title_D"].ToString();

                if (dt.Rows[0]["dt_date_D"].ToString() != "" && dt.Rows[0]["dt_date_D"].ToString() != "01/01/1900")
                    TxtDateD.Text = dt.Rows[0]["dt_date_D"].ToString();
                else
                    TxtDateD.Text = "";

                TxtSectionE.Text = dt.Rows[0]["sz_section_E"].ToString();
                if (dt.Rows[0]["bt_granted"].ToString() == "1")
                    ChkGranted.Checked = true;
                else
                    ChkGranted.Checked = false;
                if (dt.Rows[0]["bt_granted_in_part"].ToString() == "1")
                    ChkGrantedinPart.Checked = true;
                else
                    ChkGrantedinPart.Checked = false;
                if (dt.Rows[0]["bt_without_prejudice"].ToString() == "1")
                    ChkWithoutPrejudice.Checked = true;
                else
                    ChkWithoutPrejudice.Checked = false;
                if (dt.Rows[0]["bt_denied"].ToString() == "1")
                    ChkDenied.Checked = true;
                else
                    ChkDenied.Checked = false;
                if (dt.Rows[0]["bt_burden"].ToString() == "1")
                    ChkBurden.Checked = true;
                else
                    ChkBurden.Checked = false;
                if (dt.Rows[0]["bt_substantialy"].ToString() == "1")
                    ChkSubstantially.Checked = true;
                else
                    ChkSubstantially.Checked = false;
                TxtMedicalProfes.Text = dt.Rows[0]["sz_if_applicable"].ToString();
                if (dt.Rows[0]["bt_made_E"].ToString() == "1")
                    ChkMadeE.Checked = true;
                else
                    ChkMadeE.Checked = false;
                if (dt.Rows[0]["bt_chair_E"].ToString() == "1")
                    ChkChairE.Checked = true;
                else
                    ChkChairE.Checked = false;
                TxtByPrintNameE.Text = dt.Rows[0]["sz_print_name_E"].ToString();
                TxtTitleE.Text = dt.Rows[0]["sz_title_E"].ToString();

                if (dt.Rows[0]["dt_date_E"].ToString() != "" && dt.Rows[0]["dt_date_E"].ToString() != "01/01/1900")
                    TxtDateE.Text = dt.Rows[0]["dt_date_E"].ToString();
                else
                    TxtDateE.Text = "";

                TxtByPrintNameF.Text = dt.Rows[0]["sz_print_name_F"].ToString();
                TxtTitleF.Text = dt.Rows[0]["sz_title_F"].ToString();

                if (dt.Rows[0]["dt_date_F"].ToString() != "" && dt.Rows[0]["dt_date_F"].ToString() != "01/01/1900")
                    TxtDateF.Text = dt.Rows[0]["dt_date_F"].ToString();
                else
                    TxtDateF.Text = "";

                if (dt.Rows[0]["bt_i_request"].ToString() == "1")
                    ChkIrequestG.Checked = true;
                else
                    ChkIrequestG.Checked = false;
                if (dt.Rows[0]["bt_made_G"].ToString() == "1")
                    ChkMadeG.Checked = true;
                else
                    ChkMadeG.Checked = false;
                if (dt.Rows[0]["bt_chair_G"].ToString() == "1")
                    ChkChairG.Checked = true;
                else
                    ChkChairG.Checked = false;

                if (dt.Rows[0]["dt_claimant_date"].ToString() != "" && dt.Rows[0]["dt_claimant_date"].ToString() != "01/01/1900")
                    TxtClaimantDate.Text = dt.Rows[0]["dt_claimant_date"].ToString();
                else
                    TxtClaimantDate.Text = "";
                Txtwcbcasenumber.Text = dt.Rows[0]["sz_wcb_case_no"].ToString();
                TxtCarrierCaseNo.Text = dt.Rows[0]["sz_carrier_case_no"].ToString();
                TxtDateofInjury.Text = dt.Rows[0]["sz_date_of_injury"].ToString();
                TxtFirstName.Text = dt.Rows[0]["sz_patient_firstname"].ToString();
                TxtMiddleName.Text = dt.Rows[0]["sz_patient_middlename"].ToString();
                TxtLastName.Text = dt.Rows[0]["sz_patient_lastname"].ToString();
                TxtPatientAddress.Text = dt.Rows[0]["sz_patient_address"].ToString();
                TxtEmployerNameAdd.Text = dt.Rows[0]["sz_employee_name_address"].ToString();
                TxtInsuranceNameAdd.Text = dt.Rows[0]["sz_insurance_name_address"].ToString();

                for (int i = 0; i < DDLAttendingDoctors.Items.Count; i++)
                {
                    if (DDLAttendingDoctors.Items[i].Text == dt.Rows[0]["sz_doctor_ID"].ToString())
                    {
                        DDLAttendingDoctors.SelectedValue = DDLAttendingDoctors.Items[i].Value;
                        break;
                    }
                }

                CheckDC = dt.Rows[0]["sz_doctor_ID"].ToString();
                if (dt.Rows[0]["sz_guidelines_reference"].ToString() == "" || dt.Rows[0]["sz_guidelines_reference"].ToString() == "NA")
                { TxtIndividualProvider.Text = ""; }
                else
                {
                    TxtIndividualProvider.Text = dt.Rows[0]["sz_individual_provider"].ToString();;
                }
                TxtTelephone.Text = dt.Rows[0]["sz_teltphone_no"].ToString();
                TxtFaxNo.Text = dt.Rows[0]["sz_fax_no"].ToString();
                TxtGuislineChar.Text = dt.Rows[0]["sz_Guidline_Char"].ToString();

                TxtWCBCaseNumber2.Text = dt.Rows[0]["sz_wcb_case_no"].ToString();

                if (dt.Rows[0]["sz_date_of_injury"].ToString() != "" && dt.Rows[0]["sz_date_of_injury"].ToString() != "01/01/1900")
                    TxtDateofInjury2.Text = dt.Rows[0]["sz_date_of_injury"].ToString();
                else
                    TxtDateofInjury2.Text = "";

                TxtPatientName.Text = TxtFirstName.Text + " " + TxtMiddleName.Text + " " + TxtLastName.Text;


                TxtSocialSecurityNo.Text = dt.Rows[0]["sz_security_no"].ToString();

                string filepath = dt.Rows[0]["sz_pdf_url"].ToString();
                if (filepath != "")
                {
                    filepath = filepath.Replace("\\", "/");
                    lblmsg.Visible = true;
                    lblmsg2.Visible = true;
                    hyLinkOpenPDF.Visible = true;
                    string str = ConfigurationManager.AppSettings["DocumentManagerURL"].ToString();
                    //hyLinkOpenPDF.NavigateUrl = "http://localhost/MBSScans/" + filepath;
                    //hyLinkOpenPDF.NavigateUrl = "https://www.gogreenbills.com/MBSScans/"+filepath;
                    hyLinkOpenPDF.NavigateUrl = str + "/" + filepath;
                }

                for (int i = 0; i < ddlGuidline.Items.Count; i++)
                {
                    if (ddlGuidline.Items[i].Text == dt.Rows[0]["sz_Guidline_Char"].ToString() + "-" + dt.Rows[0]["sz_Guidline"].ToString())
                    {
                        ddlGuidline.SelectedIndex = i;
                        break;
                    }
                    else if (ddlGuidline.Items[i].Text != dt.Rows[0]["sz_Guidline_Char"].ToString() + "-" + dt.Rows[0]["sz_Guidline"].ToString())
                    {
                        if (ddlGuidline.SelectedIndex!=0)
                        {
                            ddlGuidline.SelectedIndex = 0;
                        }
                    }
                }

                string stln = dt.Rows[0]["sz_Guidline"].ToString();
                ArrayList ar = new ArrayList();
                for (int i = 0; i < stln.Length; i++)
                {
                    ar.Add(stln[i]);
                }

                if (ar.Count > 0)
                {
                    TxtGuidline1.Text = ar[0].ToString();
                }
                if (ar.Count > 1)
                {
                    TxtGuidline2.Text = ar[1].ToString();
                }
                if (ar.Count > 2)
                {
                    TxtGuidline3.Text = ar[2].ToString();
                }
                if (ar.Count > 3)
                {
                    TxtGuidline4.Text = ar[3].ToString();
                }
                if (ar.Count > 4)
                {
                    TxtGuidline5.Text = ar[4].ToString();
                }

                extddlSpeciality.Text = dt.Rows[0]["sz_procedure_group_id"].ToString(); ;
            }

            tabcontainerPatientEntry.ActiveTabIndex = 4;

        }
        catch (Exception ex)
        {
            usrMessage.PutMessage(ex.ToString());
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.Show();
        }
        finally
        {

        }
    }

    public DataSet GetProcedureCodes(string sI_ID)
    {
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG2_PROC_CODE_FROM_IDENTITY", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_I_ID", sI_ID);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
            }
        }
        catch (SqlException _ex)
        {
            _ex.Message.ToString();
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

    private void GetRecordMG21()
    {
        try
        {
            MBS_CASEWISE_MG21 obj2 = new MBS_CASEWISE_MG21();
            string sz_CaseID12 = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
            string sz_CompanyID12 = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString();
            DataTable dt = obj2.GetMG21Records(sz_CompanyID12, sz_CaseID12, Session["I_MG21_ID"].ToString());

            if (dt.Rows.Count > 0)
            {
                txtMG2_WCBCaseNo.Text = dt.Rows[0]["sz_wcb_case_no"].ToString();
                txtMG2_CarrierCaseNo.Text = dt.Rows[0]["sz_carrier_case_no"].ToString();
                txtMG2_DateOfInjury.Text = dt.Rows[0]["sz_date_of_injury"].ToString();
                txtMG2_PatientFirstName.Text = dt.Rows[0]["sz_patient_firstname"].ToString();
                txtMG2_PatientMiddleName.Text = dt.Rows[0]["sz_patient_middlename"].ToString();
                txtMG2_PatientLastName.Text = dt.Rows[0]["sz_patient_lastname"].ToString();

                for (int i = 0; i < ddlMG2Doctor.Items.Count; i++)
                {
                    if (ddlMG2Doctor.Items[i].Value == dt.Rows[0]["sz_doctor_id"].ToString())
                    {
                        ddlMG2Doctor.SelectedValue = ddlMG2Doctor.Items[i].Value;
                        break;
                    }
                }

                txtMG2_PatientAddress.Text = dt.Rows[0]["sz_patient_address"].ToString();
                txtMG2_SocialSecurityNo.Text = dt.Rows[0]["sz_security_no"].ToString();
                txtMG2_InsuCarrAddress.Text = dt.Rows[0]["sz_insurance_name_address"].ToString();
                txtMg2_DoctorWCBAuthNo.Text = dt.Rows[0]["sz_doctorWCBAuth_number"].ToString();

                txtMG2_GudRef1.Text = dt.Rows[0]["sz_guidelines_reference2"].ToString();
                txtMG2_GudRef2.Text = dt.Rows[0]["sz_guidelines_reference22"].ToString();
                txtMG2_GudRef3.Text = dt.Rows[0]["sz_guidelines_reference23"].ToString();
                txtMG2_GudRef4.Text = dt.Rows[0]["sz_guidelines_reference24"].ToString();
                txtMG2_GudRef5.Text = dt.Rows[0]["sz_guidelines_reference25"].ToString();

                txtMG2_DateOfServiceWCB.Text = dt.Rows[0]["sz_DateOfService2"].ToString();
                txtMG2_DateOfPrevDenied.Text = dt.Rows[0]["sz_DateOfPrevious2"].ToString();
                txtMG2_ApprovalRequest.Text = dt.Rows[0]["sz_ApprovalRequest2"].ToString();
                txtMG2_MedicalNecessity.Text = dt.Rows[0]["sz_MedicalNecessity2"].ToString();

                txtMG2_GudRef11.Text = dt.Rows[0]["sz_guidelines_reference3"].ToString();
                txtMG2_GudRef12.Text = dt.Rows[0]["sz_guidelines_reference32"].ToString();
                txtMG2_GudRef13.Text = dt.Rows[0]["sz_guidelines_reference33"].ToString();
                txtMG2_GudRef14.Text = dt.Rows[0]["sz_guidelines_reference34"].ToString();
                txtMG2_GudRef15.Text = dt.Rows[0]["sz_guidelines_reference35"].ToString();

                txtMG2_DateOfService2.Text = dt.Rows[0]["sz_DateOfService3"].ToString();
                txtMG2_DateOfPrev2.Text = dt.Rows[0]["sz_DateOfPrevious3"].ToString();
                txtMG2_ApprovalRequest2.Text = dt.Rows[0]["sz_ApprovalRequest3"].ToString();
                txtMG2_MedicalNecessity2.Text = dt.Rows[0]["sz_MedicalNecessity3"].ToString();

                txtMG2_GudRef21.Text = dt.Rows[0]["sz_guidelines_reference4"].ToString();
                txtMG2_GudRef22.Text = dt.Rows[0]["sz_guidelines_reference42"].ToString();
                txtMG2_GudRef23.Text = dt.Rows[0]["sz_guidelines_reference43"].ToString();
                txtMG2_GudRef24.Text = dt.Rows[0]["sz_guidelines_reference44"].ToString();
                txtMG2_GudRef25.Text = dt.Rows[0]["sz_guidelines_reference45"].ToString();

                txtMG2_DateOfService3.Text = dt.Rows[0]["sz_DateOfService4"].ToString();
                txtMG2_DateOfPrev3.Text = dt.Rows[0]["sz_DateOfPrevious4"].ToString();
                txtMG2_ApprovalRequest3.Text = dt.Rows[0]["sz_ApprovalRequest4"].ToString();
                txtMG2_MedicalNecessity3.Text = dt.Rows[0]["sz_MedicalNecessity4"].ToString();

                txtMG2_GudRef31.Text = dt.Rows[0]["sz_guidelines_reference5"].ToString();
                txtMG2_GudRef32.Text = dt.Rows[0]["sz_guidelines_reference52"].ToString();
                txtMG2_GudRef33.Text = dt.Rows[0]["sz_guidelines_reference53"].ToString();
                txtMG2_GudRef34.Text = dt.Rows[0]["sz_guidelines_reference54"].ToString();
                txtMG2_GudRef35.Text = dt.Rows[0]["sz_guidelines_reference55"].ToString();

                txtMG2_DateOfService4.Text = dt.Rows[0]["sz_DateOfService5"].ToString();
                txtMG2_DateOfPrev4.Text = dt.Rows[0]["sz_DateOfPrevious5"].ToString();
                txtMG2_ApprovalRequest4.Text = dt.Rows[0]["sz_approval_request5"].ToString();
                txtMG2_MedicalNecessity4.Text = dt.Rows[0]["sz_MedicalNecessity5"].ToString();

                txtMG2_PatientName.Text = dt.Rows[0]["PatientName"].ToString();
                txtMG2_WCBCaseNo1.Text = dt.Rows[0]["sz_wcb_case_no2"].ToString();
                txtMG2_DateOfInjury1.Text = dt.Rows[0]["sz_date_of_injury2"].ToString();

                if (dt.Rows[0]["bt_did"].ToString() == "1")
                    chkMG2_I.Checked = true;
                else
                    chkMG2_I.Checked = false;

                if (dt.Rows[0]["bt_not_did"].ToString() == "1")
                    chkMG2_Did.Checked = true;
                else
                    chkMG2_Did.Checked = false;


                txtMG2_TelephoneDate.Text = dt.Rows[0]["dt_Tele_Date"].ToString();
                txtMG2_PersonSpoke.Text = dt.Rows[0]["sz_spoke_anyone"].ToString();

                if (dt.Rows[0]["bt_a_copy"].ToString() == "1")
                    chkMG2_Copy.Checked = true;
                else
                    chkMG2_Copy.Checked = false;

                txtMG2_Fax.Text = dt.Rows[0]["sz_Fax"].ToString();
                txtMg2ProviderDate.Text = dt.Rows[0]["dt_provider_signature_date"].ToString();

                if (dt.Rows[0]["bt_employer"].ToString() == "1")
                    chkMG2_Employer.Checked = true;
                else
                    chkMG2_Employer.Checked = false;

                if (dt.Rows[0]["bt_CarrierReq2"].ToString() == "1")
                    chkMG2_RequestCarrier2.Checked = true;
                else
                    chkMG2_RequestCarrier2.Checked = false;

                if (dt.Rows[0]["bt_CarrierReq3"].ToString() == "1")
                    chkMG2_RequestCarrier3.Checked = true;
                else
                    chkMG2_RequestCarrier3.Checked = false;

                if (dt.Rows[0]["bt_CarrierReq4"].ToString() == "1")
                    chkMG2_RequestCarrier4.Checked = true;
                else
                    chkMG2_RequestCarrier4.Checked = false;

                if (dt.Rows[0]["bt_CarrierReq5"].ToString() == "1")
                    chkMG2_RequestCarrier5.Checked = true;
                else
                    chkMG2_RequestCarrier5.Checked = false;

                txtMG2Carrier_PrintName.Text = dt.Rows[0]["sz_CarrierPrintName"].ToString();
                txtMG2CarrierPrint_Title.Text = dt.Rows[0]["sz_CarrierPrintTitle"].ToString();
                txtMG2Carrier_PrintDate.Text = dt.Rows[0]["sz_CarrierPrintDate"].ToString();

                if (dt.Rows[0]["bt_granted2"].ToString() == "1")
                    chkMg2_Grant1.Checked = true;
                else
                    chkMg2_Grant1.Checked = false;

                if (dt.Rows[0]["bt_granted_in_part2"].ToString() == "1")
                    chkMg2_GrantPart1.Checked = true;
                else
                    chkMg2_GrantPart1.Checked = false;

                if (dt.Rows[0]["bt_denied2"].ToString() == "1")
                    chkMg2_Denied.Checked = true;
                else
                    chkMg2_Denied.Checked = false;

                if (dt.Rows[0]["bt_burden2"].ToString() == "1")
                    chkMg2_Burden.Checked = true;
                else
                    chkMg2_Burden.Checked = false;

                if (dt.Rows[0]["bt_substantialy2"].ToString() == "1")
                    chkMg2_Substantially.Checked = true;
                else
                    chkMg2_Substantially.Checked = false;

                if (dt.Rows[0]["bt_without_prejudice2"].ToString() == "1")
                    chkMG2_WithoutPre.Checked = true;
                else
                    chkMG2_WithoutPre.Checked = false;


                if (dt.Rows[0]["bt_granted3"].ToString() == "1")
                    chkMg2_Grant2.Checked = true;
                else
                    chkMg2_Grant2.Checked = false;

                if (dt.Rows[0]["bt_granted_in_part3"].ToString() == "1")
                    chkMg2_GrantPart2.Checked = true;
                else
                    chkMg2_GrantPart2.Checked = false;

                if (dt.Rows[0]["bt_denied3"].ToString() == "1")
                    chkMg2_Denied1.Checked = true;
                else
                    chkMg2_Denied1.Checked = false;

                if (dt.Rows[0]["bt_burden3"].ToString() == "1")
                    chkMg2_Burden1.Checked = true;
                else
                    chkMg2_Burden1.Checked = false;

                if (dt.Rows[0]["bt_substantialy3"].ToString() == "1")
                    chkMg2_Substantially1.Checked = true;
                else
                    chkMg2_Substantially1.Checked = false;

                if (dt.Rows[0]["bt_without_prejudice3"].ToString() == "1")
                    chkMG2_WithoutPre1.Checked = true;
                else
                    chkMG2_WithoutPre1.Checked = false;

                if (dt.Rows[0]["bt_granted4"].ToString() == "1")
                    chkMg2_Grant3.Checked = true;
                else
                    chkMg2_Grant3.Checked = false;

                if (dt.Rows[0]["bt_granted_in_part4"].ToString() == "1")
                    chkMg2_GrantPart3.Checked = true;
                else
                    chkMg2_GrantPart3.Checked = false;

                if (dt.Rows[0]["bt_denied4"].ToString() == "1")
                    chkMg2_Denied2.Checked = true;
                else
                    chkMg2_Denied2.Checked = false;

                if (dt.Rows[0]["bt_burden4"].ToString() == "1")
                    chkMg2_Burden2.Checked = true;
                else
                    chkMg2_Burden2.Checked = false;

                if (dt.Rows[0]["bt_substantialy4"].ToString() == "1")
                    chkMg2_Substantially2.Checked = true;
                else
                    chkMg2_Substantially2.Checked = false;

                if (dt.Rows[0]["bt_without_prejudice4"].ToString() == "1")
                    chkMG2_WithoutPre2.Checked = true;
                else
                    chkMG2_WithoutPre2.Checked = false;

                if (dt.Rows[0]["bt_granted5"].ToString() == "1")
                    chkMg2_Grant4.Checked = true;
                else
                    chkMg2_Grant4.Checked = false;

                if (dt.Rows[0]["bt_granted_in_part5"].ToString() == "1")
                    chkMg2_GrantPart4.Checked = true;
                else
                    chkMg2_GrantPart4.Checked = false;

                if (dt.Rows[0]["bt_denied5"].ToString() == "1")
                    chkMg2_Denied3.Checked = true;
                else
                    chkMg2_Denied3.Checked = false;

                if (dt.Rows[0]["bt_burden5"].ToString() == "1")
                    chkMg2_Burden3.Checked = true;
                else
                    chkMg2_Burden3.Checked = false;

                if (dt.Rows[0]["bt_substantialy5"].ToString() == "1")
                    chkMg2_Substantially3.Checked = true;
                else
                    chkMg2_Substantially3.Checked = false;

                if (dt.Rows[0]["bt_without_prejudice5"].ToString() == "1")
                    chkMG2_WithoutPre3.Checked = true;
                else
                    chkMG2_WithoutPre3.Checked = false;

                txtMG2_Carrier.Text = dt.Rows[0]["sz_Carrier"].ToString();
                txtMG2_ProfessinalName.Text = dt.Rows[0]["sz_NameOfMedProfessional"].ToString();

                if (dt.Rows[0]["bt_byMedArb"].ToString() == "1")
                    chkMG2_MedicalArb.Checked = true;
                else
                    chkMG2_MedicalArb.Checked = false;

                if (dt.Rows[0]["bt_byChair"].ToString() == "1")
                    chkMG2_Chair.Checked = true;
                else
                    chkMG2_Chair.Checked = false;


                txtMG2_PrintName.Text = dt.Rows[0]["sz_print_name_D"].ToString();
                txtMG2_Title.Text = dt.Rows[0]["sz_title_D"].ToString();
                txtMG2_Date.Text = dt.Rows[0]["dt_date_D"].ToString();

                if (dt.Rows[0]["bt_DenIRequest2"].ToString() == "1")
                    chkMG2_DRequest2.Checked = true;
                else
                    chkMG2_DRequest2.Checked = false;

                if (dt.Rows[0]["bt_DenIRequest3"].ToString() == "1")
                    chkMG2_DRequest3.Checked = true;
                else
                    chkMG2_DRequest3.Checked = false;

                if (dt.Rows[0]["bt_DenIRequest4"].ToString() == "1")
                    chkMG2_DRequest4.Checked = true;
                else
                    chkMG2_DRequest4.Checked = false;

                if (dt.Rows[0]["bt_DenIRequest5"].ToString() == "1")
                    chkMG2_DRequest5.Checked = true;
                else
                    chkMG2_DRequest5.Checked = false;

                txtMG2_DenPrintName.Text = dt.Rows[0]["sz_print_name_Den"].ToString();
                txtMG2_DenTitle.Text = dt.Rows[0]["sz_title_Den"].ToString();
                txtMG2_DenDate.Text = dt.Rows[0]["dt_date_Den"].ToString();

                if (dt.Rows[0]["bt_IRequest"].ToString() == "1")
                    chkMG2_Irequest.Checked = true;
                else
                    chkMG2_Irequest.Checked = false;

                if (dt.Rows[0]["bt_IRequest2"].ToString() == "1")
                    chkMG2_RequestNo2.Checked = true;
                else
                    chkMG2_RequestNo2.Checked = false;

                if (dt.Rows[0]["bt_IRequest3"].ToString() == "1")
                    chkMG2_RequestNo3.Checked = true;
                else
                    chkMG2_RequestNo3.Checked = false;

                if (dt.Rows[0]["bt_IRequest4"].ToString() == "1")
                    chkMG2_RequestNo4.Checked = true;
                else
                    chkMG2_RequestNo4.Checked = false;

                if (dt.Rows[0]["bt_IRequest5"].ToString() == "1")
                    chkMgG2_RequestNo5.Checked = true;
                else
                    chkMgG2_RequestNo5.Checked = false;

                if (dt.Rows[0]["bt_byMedArb2"].ToString() == "1")
                    chkMG2_MedicalAbr.Checked = true;
                else
                    chkMG2_MedicalAbr.Checked = false;

                if (dt.Rows[0]["bt_Atwcb"].ToString() == "1")
                    chkMG2_WCB.Checked = true;
                else
                    chkMG2_WCB.Checked = false;

                txtMG2_ClaimDate.Text = dt.Rows[0]["dt_claimant_date"].ToString();

                tabcontainerPatientEntry.ActiveTabIndex = 5;
            }
        }
        catch (Exception ex)
        {
            UserMessageMG11.PutMessage(ex.ToString());
            UserMessageMG11.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            UserMessageMG11.Show();
        }
        finally
        {

        }
    }

    private void LoadData()
    {
        string sz_caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        DataSet dsRecord = GetInitialRecords(sz_caseID);

        if (dsRecord.Tables.Count > 0)
        {
            if (dsRecord.Tables[0].Rows.Count > 0)
            {
                Txtwcbcasenumber.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                TxtCarrierCaseNo.Text = dsRecord.Tables[0].Rows[0]["CARRIER_CASE_NUMBE"].ToString();

                TxtDateofInjury.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
                TxtFirstName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString();
                TxtMiddleName.Text = dsRecord.Tables[0].Rows[0]["MIDDLE NAME"].ToString();
                TxtLastName.Text = dsRecord.Tables[0].Rows[0]["LAST NAME"].ToString();
                TxtSocialSecurityNo.Text = dsRecord.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                TxtPatientAddress.Text = dsRecord.Tables[0].Rows[0]["PATIENT ADDRESS"].ToString();
                TxtEmployerNameAdd.Text = dsRecord.Tables[0].Rows[0]["EMPLOYER INFO"].ToString();
                TxtInsuranceNameAdd.Text = dsRecord.Tables[0].Rows[0]["INSURANCE INFO"].ToString();

                TxtPatientName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString();
                TxtWCBCaseNumber2.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                TxtDateofInjury2.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
            }
        }

    }

    private void LoadDataMG1()
    {
        string sz_caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        MBS_CASEWISE_MG1 obj1 = new MBS_CASEWISE_MG1();
        DataSet dsRecord = obj1.GetInitialRecordsMG1(sz_caseID);

        if (dsRecord.Tables.Count > 0)
        {
            if (dsRecord.Tables[0].Rows.Count > 0)
            {
                txtMG1WCBCaseNumber.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                txtMG1CarrierCaseNo.Text = dsRecord.Tables[0].Rows[0]["CARRIER_CASE_NUMBE"].ToString();

                txtMG1DateOfInjury.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
                txtMG1PatientName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString() + " " + dsRecord.Tables[0].Rows[0]["MIDDLE NAME"].ToString() + " " + dsRecord.Tables[0].Rows[0]["LAST NAME"].ToString(); ;
                //TxtMiddleName.Text = dsRecord.Tables[0].Rows[0]["MIDDLE NAME"].ToString();
                //TxtLastName.Text = dsRecord.Tables[0].Rows[0]["LAST NAME"].ToString();
                txtMG1socialSecurityNo.Text = dsRecord.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                txtMG1PatientAddress.Text = dsRecord.Tables[0].Rows[0]["PATIENT ADDRESS"].ToString();
                txtMG1EmpNameAndAddress.Text = dsRecord.Tables[0].Rows[0]["EMPLOYER INFO"].ToString();
                txtMG1InsuranceCarrier.Text = dsRecord.Tables[0].Rows[0]["INSURANCE INFO"].ToString();

                TxtPatientName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString();
                TxtWCBCaseNumber2.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                TxtDateofInjury2.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();

            }
        }

    }

    private void loadMG11Doctor()
    {
        Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();
        DataSet dsMG21DoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        ddlDoctor_Name_MG11.DataSource = dsMG21DoctorName;
        System.Web.UI.WebControls.ListItem objLI = new System.Web.UI.WebControls.ListItem("---select---", "NA");
        ddlDoctor_Name_MG11.DataTextField = "DESCRIPTION";
        ddlDoctor_Name_MG11.DataValueField = "CODE";
        ddlDoctor_Name_MG11.DataBind();
    }

    private void LoadDataMG11()
    {
        string sz_caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        MBS_CASEWISE_MG11 obj1 = new MBS_CASEWISE_MG11();
        DataSet dsRecord = obj1.GetInitialRecordsMG11(sz_caseID);

        if (dsRecord.Tables.Count > 0)
        {
            if (dsRecord.Tables[0].Rows.Count > 0)
            {
                txtWCBNumber.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                txtCasenumber_MG11.Text = dsRecord.Tables[0].Rows[0]["CARRIER_CASE_NUMBE"].ToString();

                txtInjuryDate_MG11.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
                txtPatientName_MG11.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString();
                txtMiddleName_MG11.Text = dsRecord.Tables[0].Rows[0]["MIDDLE NAME"].ToString();
                txtLastName_MG11.Text = dsRecord.Tables[0].Rows[0]["LAST NAME"].ToString();
                txtSocialNumber_MG11.Text = dsRecord.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                txtPatient_Address_MG11.Text = dsRecord.Tables[0].Rows[0]["PATIENT ADDRESS"].ToString();

            }
        }

    }

    private void loadMG2Doctor()
    {
        Bill_Sys_DoctorBO _obj = new Bill_Sys_DoctorBO();
        DataSet dsMG21DoctorName = _obj.GetDoctorList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        ddlMG2Doctor.DataSource = dsMG21DoctorName;
        System.Web.UI.WebControls.ListItem objLI = new System.Web.UI.WebControls.ListItem("---select---", "NA");
        ddlMG2Doctor.DataTextField = "DESCRIPTION";
        ddlMG2Doctor.DataValueField = "CODE";
        ddlMG2Doctor.DataBind();
    }

    private void loadMG21TabData()
    {
        string sz_caseID = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString();
        DataSet dsRecord = GetInitialRecords(sz_caseID);

        if (dsRecord.Tables.Count > 0)
        {
            if (dsRecord.Tables[0].Rows.Count > 0)
            {
                txtMG2_WCBCaseNo.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();
                txtMG2_CarrierCaseNo.Text = dsRecord.Tables[0].Rows[0]["CARRIER_CASE_NUMBE"].ToString();

                txtMG2_DateOfInjury.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
                txtMG2_PatientFirstName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString();
                txtMG2_PatientMiddleName.Text = dsRecord.Tables[0].Rows[0]["MIDDLE NAME"].ToString();
                txtMG2_PatientLastName.Text = dsRecord.Tables[0].Rows[0]["LAST NAME"].ToString();

                txtMG2_SocialSecurityNo.Text = dsRecord.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
                txtMG2_PatientAddress.Text = dsRecord.Tables[0].Rows[0]["PATIENT ADDRESS"].ToString();

                txtMG2_InsuCarrAddress.Text = dsRecord.Tables[0].Rows[0]["INSURANCE INFO"].ToString();

                txtMG2_PatientName.Text = dsRecord.Tables[0].Rows[0]["FIRST NAME"].ToString() + "" + dsRecord.Tables[0].Rows[0]["MIDDLE NAME"].ToString() + "" + dsRecord.Tables[0].Rows[0]["LAST NAME"].ToString();
                txtMG2_WCBCaseNo1.Text = dsRecord.Tables[0].Rows[0]["WCB_CASE_NO"].ToString();

                txtMG2_DateOfInjury1.Text = dsRecord.Tables[0].Rows[0]["DATE_OF_INJURY"].ToString();
            }
        }

    }

    private DataSet GetInitialRecords(string sz_caseId)
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["Connection_String"].ToString());
        DataSet ds = new DataSet();

        try
        {
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_GET_MG2_INITIAL_DETAILS", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", sz_caseId);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
    }
}

