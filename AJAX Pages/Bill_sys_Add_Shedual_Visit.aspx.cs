using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DevExpress.Web;
using System.Collections;
using System.Data.SqlClient;

public partial class AJAX_Pages_Bill_sys_Add_Shedual_Visit : PageBase
{
    ArrayList billAppointmetDate;

    //protected HttpApplication ApplicationInstance
    //{
    //    get
    //    {
    //        return this.Context.ApplicationInstance;
    //    }
    //}

    //protected DefaultProfile Profile
    //{
    //    get
    //    {
    //        return (DefaultProfile)this.Context.Profile;
    //    }
    //}

    public AJAX_Pages_Bill_sys_Add_Shedual_Visit()
    {
        this.billAppointmetDate = new ArrayList();
    }

    private void BindDoctorList()
    {
        string str = this.Session["SendPatientToDoctor"].ToString();
        if (str.ToLower() != "false")
        {
            if (str.ToLower() == "true")
            {
                Bill_Sys_Event_BO billSysEventBO = new Bill_Sys_Event_BO();
                DataSet doctorlList = billSysEventBO.GetDoctorlList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.grdDoctor.DataSource=doctorlList;
                this.grdDoctor.DataBind();
            }
            return;
        }
        else
        {
            Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
            DataSet dataSet = billSysCalender.GetDoctorlList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdDoctor.DataSource=dataSet;
            this.grdDoctor.DataBind();
            return;
        }
    }

    private void BindPatientInfo()
    {
        Patient_TVBO patientTVBO = new Patient_TVBO();
        DataSet selectedPatientDataList = patientTVBO.GetSelectedPatientDataList(this.txtCompanyID.Text, this.txtPatientId.Text);
        if (selectedPatientDataList.Tables.Count > 0 && selectedPatientDataList.Tables[0].Rows.Count > 0)
        {
            this.txtPatientFName.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_PATIENT_FIRST_NAME"].ToString();
            this.txtPatientLName.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_PATIENT_LAST_NAME"].ToString();
            this.txtMI.Text = selectedPatientDataList.Tables[0].Rows[0]["MI"].ToString();
            this.txtPatientPhone.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_PATIENT_PHONE"].ToString();
            this.txtPatientAddress.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_PATIENT_ADDRESS"].ToString();
            this.txtCity.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_PATIENT_CITY"].ToString();
            this.txtState.Text = selectedPatientDataList.Tables[0].Rows[0]["STATE"].ToString();
            this.txtBirthdate.Text = selectedPatientDataList.Tables[0].Rows[0]["DT_DOB"].ToString();
            if (selectedPatientDataList.Tables[0].Rows[0]["I_PATIENT_AGE"].ToString() != "0")
            {
                this.txtPatientAge.Text = selectedPatientDataList.Tables[0].Rows[0]["I_PATIENT_AGE"].ToString();
            }
            if (selectedPatientDataList.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString() != "")
            {
                this.txtSocialSecurityNumber.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_SOCIAL_SECURITY_NO"].ToString();
            }
            if (selectedPatientDataList.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString() != "")
            {
                this.extddlInsuranceCompany.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_INSURANCE_ID"].ToString();
            }
            if (selectedPatientDataList.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString() != "")
            {
                this.extddlCaseType.Text = selectedPatientDataList.Tables[0].Rows[0]["SZ_CASE_TYPE_ID"].ToString();
            }
        }
    }

    private void BindTimeControl()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            this.ddlHours.Items.Clear();
            this.ddlMinutes.Items.Clear();
            this.ddlTime.Items.Clear();
            this.ddlHours_event.Items.Clear();
            this.ddlMinutes_event.Items.Clear();
            this.ddlTime_event.Items.Clear();
            for (int i = 0; i <= 12; i++)
            {
                if (i <= 9)
                {
                    this.ddlHours.Items.Add(string.Concat("0", i.ToString()));
                    this.ddlHours_event.Items.Add(string.Concat("0", i.ToString()));
                }
                else
                {
                    this.ddlHours.Items.Add(i.ToString());
                    this.ddlHours_event.Items.Add(i.ToString());
                }
            }
            for (int j = 0; j < 60; j++)
            {
                if (j <= 9)
                {
                    this.ddlMinutes.Items.Add(string.Concat("0", j.ToString()));
                    this.ddlMinutes_event.Items.Add(string.Concat("0", j.ToString()));
                }
                else
                {
                    this.ddlMinutes.Items.Add(j.ToString());
                    this.ddlMinutes_event.Items.Add(j.ToString());
                }
            }
            this.ddlTime.Items.Add("AM");
            this.ddlTime_event.Items.Add("AM");
            this.ddlTime.Items.Add("PM");
            this.ddlTime_event.Items.Add("PM");
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string str;
        this.lblMsg.Text = "";
        int num = 4;
        string str1 = "";
        string str2 = "";
        string str3 = "";
        int num1 = 0;
        string str4 = "";
        int num2 = 0;
        string str5 = "";
        int num3 = 0;
        string str6 = "";
        string str7 = "";
        string str8 = "";
        if (this.txtCasID.Text == "")
        {
            this.usrMessage.PutMessage("Please Select Case First ");
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }
        else
        {
            ArrayList arrayLists = new ArrayList();
            string str9 = "30";
            int num4 = Convert.ToInt32(this.ddlMinutes_event.SelectedValue) + Convert.ToInt32(str9);
            int num5 = Convert.ToInt32(this.ddlHours_event.SelectedValue);
            string selectedValue = this.ddlTime_event.SelectedValue;
            if (num4 >= 60)
            {
                num4 = num4 - 60;
                num5++;
                if (num5 <= 12)
                {
                    if (num5 == 12 && this.ddlHours.SelectedValue != "12")
                    {
                        if (selectedValue != "AM")
                        {
                            if (selectedValue == "PM")
                            {
                                selectedValue = "AM";
                            }
                        }
                        else
                        {
                            selectedValue = "PM";
                        }
                    }
                }
                else
                {
                    num5 = num5 - 12;
                    if (this.ddlHours.SelectedValue != "12")
                    {
                        if (selectedValue != "AM")
                        {
                            if (selectedValue == "PM")
                            {
                                selectedValue = "AM";
                            }
                        }
                        else
                        {
                            selectedValue = "PM";
                        }
                    }
                }
            }
            string str10 = num5.ToString().PadLeft(2, '0');
            string str11 = num4.ToString().PadLeft(2, '0');
            string str12 = selectedValue.ToString();
            string str13 = this.Session["SendPatientToDoctor"].ToString();
            if (!(str13.ToLower() == "true") || !this.chkAddToDoctor.Checked)
            {
                for (int i = 0; i < this.grdDoctor.VisibleRowCount; i++)
                {
                    GridViewDataColumn item = (GridViewDataColumn)this.grdDoctor.Columns[0];
                    CheckBox checkBox = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(i, item, "chkSelect");
                    if (checkBox.Checked)
                    {
                        string[] strArrays = new string[1];
                        strArrays[0] = "CODE";
                        string str14 = this.grdDoctor.GetRowValues(i, strArrays).ToString();
                        string[] strArrays1 = new string[1];
                        strArrays1[0] = "DESCRIPTION";
                        string str15 = this.grdDoctor.GetRowValues(i, strArrays1).ToString();
                        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
                        SqlCommand sqlCommand = new SqlCommand("SP_CHECK_INITIALE_VALUATIONEXISTS");
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.Connection.Open();
                        sqlCommand.Parameters.AddWithValue("@SZ_CASE_ID", this.txtCasID.Text);
                        sqlCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        sqlCommand.Parameters.AddWithValue("@SZ_PATIENT_ID", this.txtPatientId.Text);
                        sqlCommand.Parameters.AddWithValue("@SZ_DOCTOR_ID", str14);
                        sqlCommand.Parameters.AddWithValue("@VISIT_DATE", this.txtEventDate.Text);
                        sqlCommand.Parameters.AddWithValue("@VISIT_TYPE", extddlVisitType.Selected_Text);
                        SqlParameter sqlParameter = new SqlParameter("@INITIAL_EXISTS", SqlDbType.Bit);
                        sqlParameter.Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add(sqlParameter);
                        SqlParameter sqlParameter1 = new SqlParameter("@VISIT_EXISTS", SqlDbType.Bit, 20);
                        sqlParameter1.Direction = ParameterDirection.Output;
                        sqlCommand.Parameters.Add(sqlParameter1);
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Connection.Close();
                        bool flag = Convert.ToBoolean(sqlParameter.Value);
                        bool flag1 = Convert.ToBoolean(sqlParameter1.Value);
                        if (!flag1)
                        {
                            this.billAppointmetDate.Add(this.txtEventDate.Text);
                        }
                        if (flag || !(this.extddlVisitType.Selected_Text != "IE"))
                        {
                            if (!flag || !(this.extddlVisitType.Selected_Text == "IE"))
                            {
                                if (!flag1)
                                {
                                    Bill_Sys_Event_DAO billSysEventDAO = new Bill_Sys_Event_DAO();
                                    string[] strArrays2 = new string[1];
                                    strArrays2[0] = "CODE";
                                    string str16 = this.grdDoctor.GetRowValues(i, strArrays2).ToString();
                                    billSysEventDAO.SZ_CASE_ID=this.txtCasID.Text;
                                    billSysEventDAO.DT_EVENT_DATE=this.txtEventDate.Text;
                                    billSysEventDAO.DT_EVENT_TIME=string.Concat(this.ddlHours_event.SelectedValue.ToString(), ".", this.ddlMinutes_event.SelectedValue.ToString());
                                    billSysEventDAO.SZ_EVENT_NOTES=this.txtNotes.Text;
                                    billSysEventDAO.SZ_DOCTOR_ID=str16;
                                    billSysEventDAO.SZ_TYPE_CODE_ID="TY000000000000000003";
                                    billSysEventDAO.SZ_COMPANY_ID=this.txtCompanyID.Text;
                                    billSysEventDAO.DT_EVENT_TIME_TYPE=this.ddlTime_event.SelectedValue;
                                    billSysEventDAO.DT_EVENT_END_TIME=string.Concat(str10.ToString(), ".", str11.ToString());
                                    billSysEventDAO.DT_EVENT_END_TIME_TYPE=str12;
                                    billSysEventDAO.SZ_USER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                                    billSysEventDAO.SZ_VISIT_TYPE=this.extddlVisitType.Text;
                                    arrayLists.Add(billSysEventDAO);
                                }
                                else
                                {
                                    if (num3 != num)
                                    {
                                        str5 = string.Concat(str5, str8, str15, "  ");
                                        num3++;
                                    }
                                    else
                                    {
                                        str5 = string.Concat(str5, str8, str15, "  ");
                                        num3 = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (num2 != num)
                                {
                                    str4 = string.Concat(str4, str7, str15, "  ");
                                    num2++;
                                }
                                else
                                {
                                    str4 = string.Concat(str4, str7, str15, "  ");
                                    num2 = 0;
                                }
                            }
                        }
                        else
                        {
                            if (num1 != num)
                            {
                                str3 = string.Concat(str3, str6, str15, "  ");
                                num1++;
                            }
                            else
                            {
                                str3 = string.Concat(str3, str6, str15, "  ");
                                num1 = 0;
                            }
                        }
                    }
                }
                Bill_Sys_Event_BO billSysEventBO = new Bill_Sys_Event_BO();
                str = billSysEventBO.SaveEvent(arrayLists);
            }
            else
            {
                for (int j = 0; j < this.grdDoctor.VisibleRowCount; j++)
                {
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdDoctor.Columns[0];
                    CheckBox checkBox1 = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(j, gridViewDataColumn, "chkSelect");
                    if (checkBox1.Checked)
                    {
                        Bill_Sys_Event_DAO billSysEventDAO1 = new Bill_Sys_Event_DAO();
                        string[] strArrays3 = new string[1];
                        strArrays3[0] = "CODE";
                        string str17 = this.grdDoctor.GetRowValues(j, strArrays3).ToString();
                        billSysEventDAO1.SZ_CASE_ID=this.txtCasID.Text;
                        billSysEventDAO1.DT_EVENT_DATE=this.txtEventDate.Text;
                        billSysEventDAO1.DT_EVENT_TIME=string.Concat(this.ddlHours_event.SelectedValue.ToString(), ".", this.ddlMinutes_event.SelectedValue.ToString());
                        billSysEventDAO1.SZ_EVENT_NOTES=this.txtNotes.Text;
                        billSysEventDAO1.SZ_DOCTOR_ID=str17;
                        billSysEventDAO1.SZ_TYPE_CODE_ID="TY000000000000000003";
                        billSysEventDAO1.SZ_COMPANY_ID=this.txtCompanyID.Text;
                        billSysEventDAO1.DT_EVENT_TIME_TYPE=this.ddlTime_event.SelectedValue;
                        billSysEventDAO1.DT_EVENT_END_TIME=string.Concat(str10.ToString(), ".", str11.ToString());
                        billSysEventDAO1.DT_EVENT_END_TIME_TYPE=str12;
                        string[] strArrays4 = new string[1];
                        strArrays4[0] = "SZ_USER_ID";
                        billSysEventDAO1.SZ_USER_ID=this.grdDoctor.GetRowValues(j, strArrays4).ToString();
                        billSysEventDAO1.SZ_BILLER_ID=((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                        string[] strArrays5 = new string[1];
                        strArrays5[0] = "DocName";
                        billSysEventDAO1.SZ_DOCTOR_NAME=this.grdDoctor.GetRowValues(j, strArrays5).ToString();
                        string[] strArrays6 = new string[1];
                        strArrays6[0] = "GROUP_CODE";
                        billSysEventDAO1.SZ_GROUP_CODE=this.grdDoctor.GetRowValues(j, strArrays6).ToString();
                        arrayLists.Add(billSysEventDAO1);
                    }
                }
                Bill_Sys_Event_BO billSysEventBO1 = new Bill_Sys_Event_BO();
                str = billSysEventBO1.SaveDocEvent(arrayLists);
            }
            if (!(str2 == "") || !(str3 == "") || !(str4 == "") || !(str5 == ""))
            {
                if (str1 != "")
                {
                    this.Session["CreateBill"] = null;
                    this.Session["CreateBill"] = this.billAppointmetDate;
                }
                if (str2 != "")
                {
                    this.lblMsg.Text = string.Concat(this.lblMsg.Text, str2, " -- Visit for future date cannot be added.<br/>");
                }
                if (str3 != "")
                {
                    if (num1 <= 2)
                    {
                        this.lblMsg.Text = string.Concat(this.lblMsg.Text, " -- Schedule can not be saved for ", str3, " because patient is visiting first time hence there visit type should be Initial Evaluation.<br/>");
                    }
                    else
                    {
                        this.lblMsg.Text = string.Concat(this.lblMsg.Text, "<br/> -- Schedule can not be saved for ", str3, " because patient is visiting first time hence there visit type should be Initial Evaluation.<br/>");
                    }
                }
                if (str4 != "")
                {
                    this.lblMsg.Text = string.Concat(this.lblMsg.Text, " -- Schedule can not be saved for ", str4, " because patient already has Initial Evaluation.<br/>");
                }
                if (str5 != "")
                {
                    this.lblMsg.Text = string.Concat(this.lblMsg.Text, " -- Schedule can not be saved because for ", str5, " because patient already has this visit.<br/>");
                }
            }
            else
            {
                this.Session["CreateBill"] = null;
                this.Session["CreateBill"] = this.billAppointmetDate;
            }
            this.lblMsg.Focus();
            this.lblMsg.Visible = true;
            if (str != "success")
            {
                this.usrMessage.PutMessage(string.Concat("Eroor ", str));
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
                return;
            }
            else
            {
                if (arrayLists.Count >= 1)
                {
                    this.usrMessage.PutMessage("Visit Save successfully..");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    return;
                }
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.grdDoctor.VisibleRowCount; i++)
        {
            GridViewDataColumn item = (GridViewDataColumn)this.grdDoctor.Columns[0];
            CheckBox checkBox = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(i, item, "chkSelect");
            checkBox.Checked = false;
            checkBox.Enabled = true;
        }
        this.hdEnable.Value = "0";
        this.chkAddToDoctor.Enabled = true;
        this.chkAddToDoctor.Checked = true;
        this.extddlVisitType.Enabled = true;
        this.extddlVisitType.Text = "NA";
    }

    protected void btnDeletVisit_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = "";
            for (int i = 0; i < this.grdShowVisit.VisibleRowCount; i++)
            {
                GridViewDataColumn item = (GridViewDataColumn)this.grdShowVisit.Columns[0];
                CheckBox checkBox = (CheckBox)this.grdShowVisit.FindRowCellTemplateControl(i, item, "chkSelect");
                if (checkBox.Checked)
                {
                    string[] strArrays = new string[1];
                    strArrays[0] = "I_EVENT_ID";
                    string str1 = this.grdShowVisit.GetRowValues(i, strArrays).ToString();
                    Bill_Sys_DeleteBO billSysDeleteBO = new Bill_Sys_DeleteBO();
                    if (!billSysDeleteBO.deleteRecord("SP_TXN_CALENDAR_EVENT", "@I_EVENT_ID", str1))
                    {
                        if (str != "")
                        {
                            string[] strArrays1 = new string[1];
                            strArrays1[0] = "DT_EVENT_DATE";
                            str = string.Concat(str, " , ", this.grdShowVisit.GetRowValues(i, strArrays1).ToString());
                        }
                        else
                        {
                            string[] strArrays2 = new string[1];
                            strArrays2[0] = "DT_EVENT_DATE";
                            str = this.grdShowVisit.GetRowValues(i, strArrays2).ToString();
                        }
                    }
                }
            }
            Bill_Sys_Event_BO billSysEventBO = new Bill_Sys_Event_BO();
            DataSet visits = billSysEventBO.GetVisits(this.txtCasID.Text, this.txtCompanyID.Text);
            this.grdShowVisit.DataSource=visits;
            this.grdShowVisit.DataBind();
            if (str == "")
            {
                this.usrMessage1.PutMessage("visit deleted successfully..");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage1.Show();
            }
            else
            {
                this.usrMessage1.PutMessage(string.Concat("Eroor  vsits not deleted for ", str, " event dates"));
                this.usrMessage1.SetMessageType(0);
                this.usrMessage1.Show();
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

    protected void btnTransportdelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ArrayList arrayLists = new ArrayList();
            for (int i = 0; i < this.grdTransport.Items.Count; i++)
            {
                CheckBox checkBox = (CheckBox)this.grdTransport.Items[i].FindControl("chkDelete");
                if (checkBox.Checked)
                {
                    string str = this.grdTransport.DataKeys[i].ToString();
                    arrayLists.Add(str);
                }
            }
            BillSearchDAO billSearchDAO = new BillSearchDAO();
            billSearchDAO.Delete_Trans_Data(arrayLists, this.txtCompanyID.Text);
            this.usrMessage.PutMessage("Delete Successfully ...!");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            DataSet transportinfo = billSearchDAO.getTransportinfo(this.txtCasID.Text, this.txtCompanyID.Text);
            this.grdTransport.DataSource = transportinfo;
            this.grdTransport.DataBind();
            this.divTrans.Style.Add("visibility", "visible");
            this.chkTransportation.Checked = true;
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

    protected void btnTransportsave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        BillSearchDAO billSearchDAO = new BillSearchDAO();
        try
        {
            billSearchDAO.GetInsertTransport(this.extddlTransport.Selected_Text, this.extddlTransport.Text, this.txtCompanyID.Text, this.txtCasID.Text, this.txtFromDate.Text, this.ddlHours.SelectedItem.ToString(), this.ddlMinutes.SelectedItem.ToString(), this.ddlTime.SelectedItem.ToString());
            this.usrMessage.PutMessage("Save Successfully ...!");
            this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            DataSet transportinfo = billSearchDAO.getTransportinfo(this.txtCasID.Text, this.txtCompanyID.Text);
            this.grdTransport.DataSource = transportinfo;
            this.grdTransport.DataBind();
            this.divTrans.Style.Add("visibility", "visible");
            this.chkTransportation.Checked = true;
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
        int activeTabIndex = this.carTabPage.ActiveTabIndex;
        if (activeTabIndex == 1)
        {
            Bill_Sys_Event_BO billSysEventBO = new Bill_Sys_Event_BO();
            DataSet visits = billSysEventBO.GetVisits(this.txtCasID.Text, this.txtCompanyID.Text);
            this.grdShowVisit.DataSource=visits;
            this.grdShowVisit.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.btnAdd.Attributes.Add("onclick", "return Validate()");
            this.btnTransportsave.Attributes.Add("onclick", "return ValidateTrans()");
            this.btnTransportdelete.Attributes.Add("onclick", "return ValidateDelete()");
            this.btnDeletVisit.Attributes.Add("onclick", "return DeleteVisit()");
            string str = this.Session["SendPatientToDoctor"].ToString();
            this.hdEnable.Value = "0";
            if (str.ToLower() != "false")
            {
                this.hdSeting.Value = "1";
                this.chkAddToDoctor.Visible = true;
                this.chkAddToDoctor.Checked = true;
            }
            else
            {
                this.hdSeting.Value = "0";
                this.chkAddToDoctor.Visible = false;
            }
            if (base.Request.QueryString["szcaseid"] != null && base.Request.QueryString["szpid"] != null)
            {
                this.txtCasID.Text = base.Request.QueryString["szcaseid"].ToString();
                this.txtPatientId.Text = base.Request.QueryString["szpid"].ToString();
            }
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.extddlCaseStatus.Flag_ID = this.txtCompanyID.Text;
            this.extddlVisitType.Flag_ID = this.txtCompanyID.Text;
            this.extddlCaseType.Flag_ID = this.txtCompanyID.Text;
            this.extddlInsuranceCompany.Flag_ID = this.txtCompanyID.Text;
            this.extddlTransport.Flag_ID = this.txtCompanyID.Text;
            this.lblMsg.Visible = false;
            this.BindTimeControl();
            this.BindDoctorList();
            this.BindPatientInfo();
            this.chkTransportation.Attributes.Add("onclick", "return showTrans();");
            BillSearchDAO billSearchDAO = new BillSearchDAO();
            DataSet transportinfo = billSearchDAO.getTransportinfo(this.txtCasID.Text, this.txtCompanyID.Text);
            this.grdTransport.DataSource = transportinfo;
            this.grdTransport.DataBind();
        }
    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        string str = "";
        string str1 = "";
        int num = 0;
        while (num < this.grdDoctor.VisibleRowCount)
        {
            GridViewDataColumn item = (GridViewDataColumn)this.grdDoctor.Columns[0];
            CheckBox checkBox = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(num, item, "chkSelect");
            if (!checkBox.Checked)
            {
                num++;
            }
            else
            {
                string[] strArrays = new string[1];
                strArrays[0] = "BT_NOT_HAVE_NOTES";
                str = this.grdDoctor.GetRowValues(num, strArrays).ToString();
                string[] strArrays1 = new string[1];
                strArrays1[0] = "IS_HAVE_LOGIN";
                str1 = this.grdDoctor.GetRowValues(num, strArrays1).ToString();
                break;
            }
        }
        if (!(str != "") || !(str.ToLower() == "true"))
        {
            if (!(str1 != "") || !(str1.ToLower() == "1"))
            {
                for (int i = 0; i < this.grdDoctor.VisibleRowCount; i++)
                {
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdDoctor.Columns[0];
                    CheckBox checkBox1 = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");
                    string[] strArrays2 = new string[1];
                    strArrays2[0] = "IS_HAVE_LOGIN";
                    string str2 = this.grdDoctor.GetRowValues(i, strArrays2).ToString();
                    if (str2.ToLower() != "1")
                    {
                        checkBox1.Enabled = true;
                    }
                    else
                    {
                        checkBox1.Enabled = false;
                        checkBox1.Checked = false;
                    }
                }
                this.chkAddToDoctor.Checked = false;
                this.chkAddToDoctor.Enabled = false;
                this.extddlVisitType.Enabled = true;
                return;
            }
            else
            {
                Bill_Sys_PatientVisitBO billSysPatientVisitBO = new Bill_Sys_PatientVisitBO();
                string visitType = billSysPatientVisitBO.GetVisitType(this.txtCompanyID.Text, "GET_FU_VALUE");
                for (int j = 0; j < this.grdDoctor.VisibleRowCount; j++)
                {
                    GridViewDataColumn item1 = (GridViewDataColumn)this.grdDoctor.Columns[0];
                    CheckBox checkBox2 = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(j, item1, "chkSelect");
                    string[] strArrays3 = new string[1];
                    strArrays3[0] = "IS_HAVE_LOGIN";
                    string str3 = this.grdDoctor.GetRowValues(j, strArrays3).ToString();
                    if (str3.ToLower() == "1")
                    {
                        checkBox2.Enabled = true;
                        this.extddlVisitType.Text = visitType;
                        this.extddlVisitType.Enabled = false;
                    }
                    else
                    {
                        checkBox2.Enabled = false;
                        checkBox2.Checked = false;
                    }
                }
                this.chkAddToDoctor.Checked = true;
                this.chkAddToDoctor.Enabled = true;
                return;
            }
        }
        else
        {
            for (int k = 0; k < this.grdDoctor.VisibleRowCount; k++)
            {
                GridViewDataColumn gridViewDataColumn1 = (GridViewDataColumn)this.grdDoctor.Columns[0];
                CheckBox checkBox3 = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(k, gridViewDataColumn1, "chkSelect");
                string[] strArrays4 = new string[1];
                strArrays4[0] = "IS_HAVE_LOGIN";
                string str4 = this.grdDoctor.GetRowValues(k, strArrays4).ToString();
                if (str4.ToLower() != "1")
                {
                    checkBox3.Enabled = true;
                }
                else
                {
                    checkBox3.Enabled = false;
                    checkBox3.Checked = false;
                }
            }
            this.chkAddToDoctor.Checked = false;
            this.chkAddToDoctor.Enabled = false;
            this.extddlVisitType.Enabled = true;
            return;
        }
    }
}