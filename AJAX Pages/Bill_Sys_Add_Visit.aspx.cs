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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using Componend;
using PDFValueReplacement;
using mbs.LienBills;
using iTextSharp.text.pdf;
using System.Text;
using log4net;

public partial class AJAX_Pages_Bill_Sys_Add_Visit : PageBase
{
    private Patient_TVBO _patient_TVBO;
    private ArrayList billAppointmetDate = new ArrayList();

    private void BindDoctorList()
    {
        string str = this.Session["SendPatientToDoctor"].ToString();
        if (str.ToLower() == "false")
        {
            Bill_Sys_Calender _bill_Sys_Calender = new Bill_Sys_Calender();
            DataSet doctorlList = _bill_Sys_Calender.GetDoctorlList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdDoctor.DataSource = doctorlList;
            this.grdDoctor.DataBind();
        }
        else if (str.ToLower() == "true")
        {
            Bill_Sys_Event_BO objDocList = new Bill_Sys_Event_BO();
            DataSet dsDoctor = objDocList.GetDoctorlList(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdDoctor.DataSource = dsDoctor;
            grdDoctor.DataBind();
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
                if (i > 9)
                {
                    this.ddlHours.Items.Add(i.ToString());
                    this.ddlHours_event.Items.Add(i.ToString());
                }
                else
                {
                    this.ddlHours.Items.Add("0" + i.ToString());
                    this.ddlHours_event.Items.Add("0" + i.ToString());
                }
            }
            for (int j = 0; j < 60; j++)
            {
                if (j > 9)
                {
                    this.ddlMinutes.Items.Add(j.ToString());
                    this.ddlMinutes_event.Items.Add(j.ToString());
                }
                else
                {
                    this.ddlMinutes.Items.Add("0" + j.ToString());
                    this.ddlMinutes_event.Items.Add("0" + j.ToString());
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
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        this.lblMsg.Text = "";
        int num = 4;
        string str = "";
        string str2 = "";
        string str3 = "";
        int num2 = 0;
        string str4 = "";
        int num3 = 0;
        string str5 = "";
        int num4 = 0;
        string str6 = "";
        string str7 = "";
        string str8 = "";
        if (this.txtCaseID.Text != "")
        {
            ArrayList list = new ArrayList();
            string str9 = "30";
            int num5 = Convert.ToInt32(this.ddlMinutes_event.SelectedValue) + Convert.ToInt32(str9);
            int num6 = Convert.ToInt32(this.ddlHours_event.SelectedValue);
            string selectedValue = this.ddlTime_event.SelectedValue;
            if (num5 >= 60)
            {
                num5 -= 60;
                num6++;
                if (num6 > 12)
                {
                    num6 -= 12;
                    if (this.ddlHours.SelectedValue != "12")
                    {
                        if (selectedValue == "AM")
                        {
                            selectedValue = "PM";
                        }
                        else if (selectedValue == "PM")
                        {
                            selectedValue = "AM";
                        }
                    }
                }
                else if ((num6 == 12) && (this.ddlHours.SelectedValue != "12"))
                {
                    if (selectedValue == "AM")
                    {
                        selectedValue = "PM";
                    }
                    else if (selectedValue == "PM")
                    {
                        selectedValue = "AM";
                    }
                }
            }
            string str11 = num6.ToString().PadLeft(2, '0');
            string str12 = num5.ToString().PadLeft(2, '0');
            string str13 = selectedValue.ToString();
            string str14 = "";
            if ((this.Session["SendPatientToDoctor"].ToString().ToLower() == "true") && this.chkAddToDoctor.Checked)
            {
                for (int i = 0; i < this.grdDoctor.VisibleRowCount; i++)
                {
                    GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdDoctor.Columns[0];
                    CheckBox box = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");
                    if (box.Checked)
                    {
                        Bill_Sys_Event_DAO t_dao = new Bill_Sys_Event_DAO();
                        string str16 = this.grdDoctor.GetRowValues(i, new string[] { "CODE" }).ToString();
                        t_dao.SZ_CASE_ID=this.txtCaseID.Text;
                        t_dao.DT_EVENT_DATE=this.txtEventDate.Text;
                        t_dao.DT_EVENT_TIME=this.ddlHours_event.SelectedValue.ToString() + "." + this.ddlMinutes_event.SelectedValue.ToString();
                        t_dao.SZ_EVENT_NOTES=this.txtNotes.Text;
                        t_dao.SZ_DOCTOR_ID=str16;
                        t_dao.SZ_TYPE_CODE_ID="TY000000000000000003";
                        t_dao.SZ_COMPANY_ID=this.txtCompanyID.Text;
                        t_dao.DT_EVENT_TIME_TYPE=this.ddlTime_event.SelectedValue;
                        t_dao.DT_EVENT_END_TIME=str11.ToString() + "." + str12.ToString();
                        t_dao.DT_EVENT_END_TIME_TYPE=str13;
                        t_dao.SZ_USER_ID = grdDoctor.GetRowValues(i, "SZ_USER_ID").ToString();
                        t_dao.SZ_BILLER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                        t_dao.SZ_DOCTOR_NAME = grdDoctor.GetRowValues(i, "DocName").ToString();
                        t_dao.SZ_GROUP_CODE = grdDoctor.GetRowValues(i, "GROUP_CODE").ToString();
                        list.Add(t_dao);
                    }
                }
               

                Bill_Sys_Event_BO objAddEvent = new Bill_Sys_Event_BO();
                str14 = objAddEvent.SaveDocEvent(list);
            }
            else
            {
                for (int j = 0; j < this.grdDoctor.VisibleRowCount; j++)
                {
                    GridViewDataColumn c = (GridViewDataColumn)grdDoctor.Columns[0]; // checkbox column
                    CheckBox chkSelected = (CheckBox)grdDoctor.FindRowCellTemplateControl(j, c, "chkSelect");
                    if (chkSelected.Checked)
                    {
                        string str17 = grdDoctor.GetRowValues(j, "CODE").ToString();
                        string str18 = this.grdDoctor.GetRowValues(j, "DESCRIPTION").ToString();
                        bool flag = false;
                        bool flag2 = false;
                        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings.Get("MyConnectionString"));
                        SqlCommand command = new SqlCommand("SP_CHECK_INITIALE_VALUATIONEXISTS");
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.Connection.Open();
                        command.Parameters.AddWithValue("@SZ_CASE_ID", this.txtCaseID.Text);
                        command.Parameters.AddWithValue("@SZ_COMPANY_ID", ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        command.Parameters.AddWithValue("@SZ_PATIENT_ID", this.txtPatientID.Text);
                        command.Parameters.AddWithValue("@SZ_DOCTOR_ID", str17);
                        command.Parameters.AddWithValue("@VISIT_DATE", this.txtEventDate.Text);
                        SqlParameter parameter = new SqlParameter("@INITIAL_EXISTS", SqlDbType.Bit);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);
                        SqlParameter parameter2 = new SqlParameter("@VISIT_EXISTS", SqlDbType.Bit, 20);
                        parameter2.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter2);
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                        flag = Convert.ToBoolean(parameter.Value);
                        flag2 = Convert.ToBoolean(parameter2.Value);
                        if (!flag2)
                        {
                            this.billAppointmetDate.Add(this.txtEventDate.Text);
                        }
                        if (!flag && (this.extddlVisitType.Selected_Text != "IE"))
                        {
                            if (num2 == num)
                            {
                                str3 = str3 + str6 + str18 + "  ";
                                num2 = 0;
                            }
                            else
                            {
                                str3 = str3 + str6 + str18 + "  ";
                                num2++;
                            }
                        }
                        else if (flag && (this.extddlVisitType.Selected_Text == "IE"))
                        {
                            if (num3 == num)
                            {
                                str4 = str4 + str7 + str18 + "  ";
                                num3 = 0;
                            }
                            else
                            {
                                str4 = str4 + str7 + str18 + "  ";
                                num3++;
                            }
                        }
                        else if (flag2)
                        {
                            if (num4 == num)
                            {
                                str5 = str5 + str8 + str18 + "  ";
                                num4 = 0;
                            }
                            else
                            {
                                str5 = str5 + str8 + str18 + "  ";
                                num4++;
                            }
                        }
                        else
                        {
                            Bill_Sys_Event_DAO t_dao2 = new Bill_Sys_Event_DAO();
                            string str19 = this.grdDoctor.GetRowValues(j, "CODE").ToString();
                            t_dao2.SZ_CASE_ID=this.txtCaseID.Text;
                            t_dao2.DT_EVENT_DATE=this.txtEventDate.Text;
                            t_dao2.DT_EVENT_TIME=this.ddlHours_event.SelectedValue.ToString() + "." + this.ddlMinutes_event.SelectedValue.ToString();
                            t_dao2.SZ_EVENT_NOTES=this.txtNotes.Text;
                            t_dao2.SZ_DOCTOR_ID=str19;
                            t_dao2.SZ_TYPE_CODE_ID="TY000000000000000003";
                            t_dao2.SZ_COMPANY_ID=this.txtCompanyID.Text;
                            t_dao2.DT_EVENT_TIME_TYPE=this.ddlTime_event.SelectedValue;
                            t_dao2.DT_EVENT_END_TIME=str11.ToString() + "." + str12.ToString();
                            t_dao2.DT_EVENT_END_TIME_TYPE=str13;
                            t_dao2.SZ_USER_ID = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID.ToString();
                            t_dao2.SZ_VISIT_TYPE = extddlVisitType.Text;
                            list.Add(t_dao2);
                        }
                    }
                }
                str14 = new Bill_Sys_Event_BO().SaveEvent(list);
            }
            if (((str2 == "") && (str3 == "")) && ((str4 == "") && (str5 == "")))
            {
                this.Session["CreateBill"] = null;
                this.Session["CreateBill"] = this.billAppointmetDate;
            }
            else
            {
                if (str != "")
                {
                    this.Session["CreateBill"] = null;
                    this.Session["CreateBill"] = this.billAppointmetDate;
                }
                if (str2 != "")
                {
                    this.lblMsg.Text = this.lblMsg.Text + str2 + " -- Visit for future date cannot be added.<br/>";
                }
                if (str3 != "")
                {
                    if (num2 > 2)
                    {
                        this.lblMsg.Text = this.lblMsg.Text + "<br/> -- Schedule can not be saved for " + str3 + " is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                    }
                    else
                    {
                        this.lblMsg.Text = this.lblMsg.Text + " -- Schedule can not be saved for " + str3 + " is visiting first time hence there visit type should be Initial Evaluation.<br/>";
                    }
                }
                if (str4 != "")
                {
                    this.lblMsg.Text = this.lblMsg.Text + " -- Schedule can not be saved for " + str4 + " because patient already has Initial Evaluation.<br/>";
                }
                if (str5 != "")
                {
                    this.lblMsg.Text = this.lblMsg.Text + " -- Schedule can not be saved because for " + str5 + " because patient already has this visit.<br/>";
                }
            }
            this.lblMsg.Focus();
            this.lblMsg.Visible = true;
            if (str14 == "success")
            {
                if (list.Count >= 1)
                {
                    usrMessage.PutMessage("Visit Save successfully..");
                    usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    usrMessage.Show();
                    if (chkTransportation.Checked)
                    {

                        BillSearchDAO hdao = new BillSearchDAO();
                        try
                        {
                            hdao.GetInsertTransport(this.extddlTransport.Selected_Text, this.extddlTransport.Text, this.txtCompanyID.Text, this.txtCaseID.Text, this.txtFromDate.Text, this.ddlHours.SelectedItem.ToString(), this.ddlMinutes.SelectedItem.ToString(), this.ddlTime.SelectedItem.ToString());
                            this.usrMessage.PutMessage("Save Successfully ...!");
                            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                            this.usrMessage.Show();
                            DataSet set = new DataSet();
                            set = hdao.getTransportinfo(this.txtCaseID.Text, this.txtCompanyID.Text);
                            this.grdTransport.DataSource = set;
                            this.grdTransport.DataBind();
                            this.divTrans.Style.Add("visibility", "visible");
                            this.tb1.Visible = true;
                            this.tb3.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                            using (Utils utility = new Utils())
                            {
                                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
                            }
                            string errorMessage = "Error Request=" + id + ".Please share with Technical support.";
                            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + errorMessage);

                        }
                        this.divTrans.Style.Add("visibility", "visible");
                        divTransGrd.Style.Add("visibility", "visible");
                    }
                }
            }
            else
            {
                this.usrMessage.PutMessage("Eroor " + str14);
                this.usrMessage.SetMessageType(0);
                this.usrMessage.Show();
            }
            this.tb1.Visible = true;
            this.tb3.Visible = true;
        }
        else
        {
            this.usrMessage.PutMessage("Please Select Case First ");
            this.usrMessage.SetMessageType(0);
            this.usrMessage.Show();
        }


        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.grdDoctor.VisibleRowCount; i++)
        {
            GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdDoctor.Columns[0];
            CheckBox box = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");
            box.Checked = false;
            box.Enabled = true;
        }
        this.hdEnable.Value = "0";
        this.chkAddToDoctor.Enabled = true;
        this.chkAddToDoctor.Checked = true;
        this.extddlVisitType.Enabled = true;
        this.extddlVisitType.Text="NA";
    }

    protected void btnSearchPatientList_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.SearchPatientGrid();
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
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.grdTransport.Items.Count; i++)
            {
                CheckBox box = (CheckBox)this.grdTransport.Items[i].FindControl("chkDelete");
                if (box.Checked)
                {
                    string str = this.grdTransport.DataKeys[i].ToString();
                    list.Add(str);
                }
            }
            BillSearchDAO hdao = new BillSearchDAO();
            hdao.Delete_Trans_Data(list, this.txtCompanyID.Text);
            this.usrMessage.PutMessage("Delete Successfully ...!");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            DataSet set = new DataSet();
            set = hdao.getTransportinfo(this.txtCaseID.Text, this.txtCompanyID.Text);
            this.grdTransport.DataSource = set;
            this.grdTransport.DataBind();
            this.divTrans.Style.Add("visibility", "visible");
            divTransGrd.Style.Add("visibility", "visible");
            this.tb1.Visible = true;
            this.tb3.Visible = true;
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

        BillSearchDAO hdao = new BillSearchDAO();
        try
        {
            hdao.GetInsertTransport(this.extddlTransport.Selected_Text, this.extddlTransport.Text, this.txtCompanyID.Text, this.txtCaseID.Text, this.txtFromDate.Text, this.ddlHours.SelectedItem.ToString(), this.ddlMinutes.SelectedItem.ToString(), this.ddlTime.SelectedItem.ToString());
            this.usrMessage.PutMessage("Save Successfully ...!");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            this.usrMessage.Show();
            DataSet set = new DataSet();
            set = hdao.getTransportinfo(this.txtCaseID.Text, this.txtCompanyID.Text);
            this.grdTransport.DataSource = set;
            this.grdTransport.DataBind();
            this.divTrans.Style.Add("visibility", "visible");
            this.tb1.Visible = true;
            this.tb3.Visible = true;
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

    protected void grdPatientList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdPatientList.CurrentPageIndex = e.NewPageIndex;
            this.SearchPatientGrid();
        }
        catch(Exception ex)
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

    protected void grdPatientList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[1].Text != "&nbsp;")
            {
                this.txtPatientID.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[1].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[2].Text != "&nbsp;")
            {
                this.txtPatientFName.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[2].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[11].Text != "&nbsp;")
            {
                this.txtMI.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[11].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[3].Text != "&nbsp;")
            {
                this.txtPatientLName.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[3].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[9].Text != "&nbsp;")
            {
                this.txtPatientPhone.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[9].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[5].Text != "&nbsp;")
            {
                this.txtPatientAddress.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[5].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[20].Text != "&nbsp;")
            {
                this.txtState.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[20].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[15].Text != "&nbsp;")
            {
                this.txtBirthdate.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[15].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[4].Text != "&nbsp;")
            {
                this.txtPatientAge.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[4].Text;
            }
            if (this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[13].Text != "&nbsp;")
            {
                this.txtSocialSecurityNumber.Text = this.grdPatientList.Items[this.grdPatientList.SelectedIndex].Cells[13].Text;
            }
            DataSet caseDetails = new Billing_Sys_ManageNotesBO().GetCaseDetails(this.txtPatientID.Text);
            if (caseDetails.Tables[0].Rows.Count > 0)
            {
                this.txtCaseID.Text = caseDetails.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID != caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString())
                {
                    this.extddlInsuranceCompany.Flag_ID=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    this.extddlCaseType.Flag_ID=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                }
                else
                {
                    this.extddlInsuranceCompany.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this.extddlCaseType.Flag_ID=((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                }
                this.extddlCaseType.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                this.extddlInsuranceCompany.Text=caseDetails.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            }
            this.tb1.Visible = true;
            this.tb2.Visible = true;
            this.tb3.Visible = true;
            this.chkTransportation.Visible = true;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkTransportation.Attributes.Add("onclick", "return showTrans();");
        if (!base.IsPostBack)
        {
            this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this._patient_TVBO = new Patient_TVBO();
            this.Session["PatientDataList"] = this._patient_TVBO.GetPatientDataList(this.txtCompanyID.Text);
            this.btnAdd.Attributes.Add("onclick", "return Validate()");
            this.btnTransportsave.Attributes.Add("onclick", "return ValidateTrans()");
            this.btnTransportdelete.Attributes.Add("onclick", "return ValidateDelete()");
            this.extddlCaseStatus.Flag_ID=this.txtCompanyID.Text;
            this.extddlCaseType.Flag_ID=this.txtCompanyID.Text;
            this.extddlInsuranceCompany.Flag_ID=this.txtCompanyID.Text;
            this.extddlTransport.Flag_ID=this.txtCompanyID.Text;
            this.extddlVisitType.Flag_ID=this.txtCompanyID.Text;
            string str = this.Session["SendPatientToDoctor"].ToString();
            this.hdEnable.Value = "0";
            if (str.ToLower() == "false")
            {
                this.hdSeting.Value = "0";
                this.chkAddToDoctor.Visible = false;
            }
            else
            {
                this.hdSeting.Value = "1";
                this.chkAddToDoctor.Visible = true;
                this.chkAddToDoctor.Checked = true;
            }
            this.BindTimeControl();
            this.BindDoctorList();
            this.chkTransportation.Visible = false;
        }
        this.tb1.Visible = false;
    }

    private void SearchPatientGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            DataTable table = new DataTable();
            DataSet set = new DataSet();
            set = (DataSet)this.Session["PatientDataList"];
            table = set.Tables[0].Clone();
            if ((this.txtPatientFirstName.Text != "") && (this.txtPatientLastName.Text == ""))
            {
                DataRow[] rowArray = set.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + this.txtPatientFirstName.Text + "%'");
                for (int i = 0; i < rowArray.Length; i++)
                {
                    table.ImportRow(rowArray[i]);
                }
            }
            else if ((this.txtPatientLastName.Text != "") && (this.txtPatientFirstName.Text == ""))
            {
                DataRow[] rowArray2 = set.Tables[0].Select("SZ_PATIENT_LAST_NAME LIKE '" + this.txtPatientLastName.Text + "%'");
                for (int j = 0; j < rowArray2.Length; j++)
                {
                    table.ImportRow(rowArray2[j]);
                }
            }
            else if ((this.txtPatientLastName.Text != "") && (this.txtPatientFirstName.Text != ""))
            {
                DataRow[] rowArray3 = set.Tables[0].Select("SZ_PATIENT_FIRST_NAME LIKE '" + this.txtPatientFirstName.Text + "%' AND SZ_PATIENT_LAST_NAME LIKE '" + this.txtPatientLastName.Text + "%'");
                for (int k = 0; k < rowArray3.Length; k++)
                {
                    table.ImportRow(rowArray3[k]);
                }
            }
            this.grdPatientList.DataSource = table;
            this.grdPatientList.DataBind();
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

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        string str = "";
        string str2 = "";
        for (int i = 0; i < this.grdDoctor.VisibleRowCount; i++)
        {
            GridViewDataColumn gridViewDataColumn = (GridViewDataColumn)this.grdDoctor.Columns[0];
            CheckBox box = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(i, gridViewDataColumn, "chkSelect");
            if (box.Checked)
            {
                str = this.grdDoctor.GetRowValues(i, new string[] { "BT_NOT_HAVE_NOTES" }).ToString();
                str2 = this.grdDoctor.GetRowValues(i, new string[] { "IS_HAVE_LOGIN" }).ToString();
                break;
            }
        }
        if ((str != "") && (str.ToLower() == "1"))
        {
            for (int j = 0; j < this.grdDoctor.VisibleRowCount; j++)
            {
                GridViewDataColumn column2 = (GridViewDataColumn)this.grdDoctor.Columns[0];
                CheckBox box2 = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(j, column2, "chkSelect");
                if (this.grdDoctor.GetRowValues(j, new string[] { "IS_HAVE_LOGIN" }).ToString().ToLower() == "1")
                {
                    box2.Enabled = false;
                    box2.Checked = false;
                }
                else
                {
                    box2.Enabled = true;
                }
            }
            this.chkAddToDoctor.Checked = false;
            this.chkAddToDoctor.Enabled = false;
            this.extddlVisitType.Enabled = true;
        }
        else if ((str2 != "") && (str2.ToLower() == "1"))
        {
            string visitType = new Bill_Sys_PatientVisitBO().GetVisitType(this.txtCompanyID.Text, "GET_FU_VALUE");
            for (int k = 0; k < this.grdDoctor.VisibleRowCount; k++)
            {
                GridViewDataColumn column3 = (GridViewDataColumn)this.grdDoctor.Columns[0];
                CheckBox box3 = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(k, column3, "chkSelect");
                if (this.grdDoctor.GetRowValues(k, new string[] { "IS_HAVE_LOGIN" }).ToString().ToLower() != "1")
                {
                    box3.Enabled = false;
                    box3.Checked = false;
                }
                else
                {
                    box3.Enabled = true;
                    this.extddlVisitType.Text=visitType;
                    this.extddlVisitType.Enabled = false;
                }
            }
            this.chkAddToDoctor.Checked = true;
            this.chkAddToDoctor.Enabled = true;
        }
        else
        {
            for (int m = 0; m < this.grdDoctor.VisibleRowCount; m++)
            {
                GridViewDataColumn column4 = (GridViewDataColumn)this.grdDoctor.Columns[0];
                CheckBox box4 = (CheckBox)this.grdDoctor.FindRowCellTemplateControl(m, column4, "chkSelect");
                if (this.grdDoctor.GetRowValues(m, new string[] { "IS_HAVE_LOGIN" }).ToString().ToLower() == "1")
                {
                    box4.Enabled = false;
                    box4.Checked = false;
                }
                else
                {
                    box4.Enabled = true;
                }
            }
            this.chkAddToDoctor.Checked = false;
            this.chkAddToDoctor.Enabled = false;
            this.extddlVisitType.Enabled = true;
        }
    }

  }
