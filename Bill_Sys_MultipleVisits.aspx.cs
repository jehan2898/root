using AjaxControlToolkit;
using ASP;
using Componend;
using ExtendedDropDownList;
using mbs.dao;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using XMLDMLComponent;

public partial class Bill_Sys_MultipleVisits : PageBase
{
    private ListOperation _listOperation;
    private DAO_NOTES_BO _DAO_NOTES_BO;
    private DAO_NOTES_EO _DAO_NOTES_EO;
    private Bill_Sys_Calender _bill_Sys_Calender;
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAdd;
    private CaseDetailsBO _caseDetailsBO;
    private string _caseno;
    private readonly int COL_CHART_NUMBER = 8;
    private readonly int COL_RFO_CHART_NUMBER = 9;

    public Bill_Sys_MultipleVisits()
    {
    }

    public void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int num = 0;
        bool flag = false;
        try
        {
            this.txtPatientName.Text = this.txtPatientName.Text.Replace("'", "");
            this.txtPatientName.Text = this.txtPatientName.Text.Replace("\"", "");
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SZ_COLOR_CODE");
            dataTable.Columns.Add("SZ_CASE_ID");
            dataTable.Columns.Add("sz_ID");
            dataTable.Columns.Add("SZ_CASE_NAME");
            dataTable.Columns.Add("SZ_CASE_NO");
            dataTable.Columns.Add("SZ_CASE_TYPE_ID");
            dataTable.Columns.Add("BT_DELETE");
            dataTable.Columns.Add("SZ_CHART_NO");
            dataTable.Columns.Add("SZ_PATIENT_ID");
            dataTable.Columns.Add("DT_DATE_OF_ACCIDENT");
            dataTable.Columns.Add("SZ_APPOINTMENT");
            dataTable.Columns.Add("SZ_PATIENT_NAME");
            dataTable.Columns.Add("sz_chart_number");
            dataTable.Columns.Add("i_rfo_chart_no");
            dataTable.Columns.Add("SZ_CASE_TYPE_NAME");
            dataTable.Columns.Add("BT_ASSOCIATE_DIAGNOSIS_CODE");
            dataTable.Columns.Add("SZ_COMPANY_ID");
            dataTable.Columns.Add("SZ_COMPANY_NAME");
            dataTable.Columns.Add("DT_CREATED_DATE");

            if (this.Chkmult.Checked && this.txtPatientName.Text != "" && this.txtCaseNo.Text.Trim().Length == 0 && this.txtChartNumber.Text.Trim().Length == 0 && (this.extddlLocation.Text == "NA" || this.extddlLocation.Text == "") && this.grdCaseMaster.Items.Count > 0)
            {
                for (int i = 0; i < this.grdCaseMaster.Items.Count; i++)
                {
                    DataRow text = dataTable.NewRow();
                    text["SZ_CASE_ID"] = this.grdCaseMaster.Items[i].Cells[2].Text;
                    text["SZ_CASE_NO"] = this.grdCaseMaster.Items[i].Cells[1].Text;
                    text["SZ_PATIENT_ID"] = this.grdCaseMaster.Items[i].Cells[3].Text;
                    text["DT_DATE_OF_ACCIDENT"] = this.grdCaseMaster.Items[i].Cells[6].Text;
                    text["SZ_PATIENT_NAME"] = this.grdCaseMaster.Items[i].Cells[4].Text;
                    text["sz_chart_number"] = this.grdCaseMaster.Items[i].Cells[8].Text;
                    text["i_rfo_chart_no"] = this.grdCaseMaster.Items[i].Cells[9].Text;
                    text["SZ_CASE_TYPE_NAME"] = this.grdCaseMaster.Items[i].Cells[5].Text;
                    text["DT_CREATED_DATE"] = this.grdCaseMaster.Items[i].Cells[7].Text;
                    dataTable.Rows.Add(text);
                }
            }
            string caseList = "";
            if (this.txtCaseNo.Text.Trim().Length != 0)
            {
                caseList = this.getCaseList();
            }
            SQLToDAO sQLToDAO = new SQLToDAO(ConfigurationManager.AppSettings["Connection_String"]);
            AddVisit addVisit = new AddVisit();
            if (this.txtChartNumber.Text.Trim().Length != 0)
            {
                addVisit.sz_chart_number = this.getChartNumberList();
            }
            if (!this.RowLocation.Visible)
            {
                addVisit.sz_company_id = this.txtCompanyID.Text;
                addVisit.sz_case_no = caseList;
                addVisit.sz_patient_name = this.txtPatientName.Text;
                addVisit.sz_location_id = "";
                addVisit.sz_case_status = this.rdoISActivePatient.SelectedValue.ToString();
            }
            else
            {
                addVisit.sz_company_id = this.txtCompanyID.Text;
                addVisit.sz_case_no = caseList;
                addVisit.sz_patient_name = this.txtPatientName.Text;
                addVisit.sz_location_id = extddlLocation.Text;
                addVisit.sz_case_status = this.rdoISActivePatient.SelectedValue.ToString();
            }
            DataTable dataTable1 = new DataTable();
            DataTable dataTable2 = new DataTable();
            DataSet dataSet = new DataSet();
            string[] strArrays = new string[] { "0" };
            if (caseList != "")
            {
                strArrays = this._caseno.Split(new char[] { ',' });
                dataSet = sQLToDAO.LoadDataSet("sp_search_added_visits", "mbs.dao.AddVisit", addVisit, "mbs.dao");
                dataTable1.Columns.Add("sz_case_no");
                dataTable1.Columns.Add("sz_Case_id");
                dataTable1.Columns.Add("sz_patient_id");
                dataTable1.Columns.Add("sz_patient_name");
                dataTable1.Columns.Add("sz_Case_type_name");
                dataTable1.Columns.Add("dt_date_of_accident");
                dataTable1.Columns.Add("dt_created_Date");
                dataTable1.Columns.Add("sz_chart_number");
                dataTable1.Columns.Add("i_rfo_chart_no");
            }
            if (strArrays[0].ToString() != "0")
            {
                for (int j = 0; (int)strArrays.Length > j; j++)
                {
                    if (strArrays[j].ToString() == "")
                    {
                        num++;
                    }
                    else
                    {
                        int num1 = 0;
                        while (dataSet.Tables[0].Rows.Count > num1)
                        {
                            dataSet.Tables[0].Rows[num1]["SZ_CASE_NO"].ToString();
                            if (dataSet.Tables[0].Rows[num1]["SZ_CASE_NO"].ToString() == strArrays[num].ToString())
                            {
                                DataRow item = dataTable1.NewRow();
                                item["sz_case_no"] = dataSet.Tables[0].Rows[num1]["sz_case_no"];
                                item["sz_Case_id"] = dataSet.Tables[0].Rows[num1]["sz_Case_id"];
                                item["sz_patient_id"] = dataSet.Tables[0].Rows[num1]["sz_patient_id"];
                                item["sz_patient_name"] = dataSet.Tables[0].Rows[num1]["sz_patient_name"];
                                item["sz_Case_type_name"] = dataSet.Tables[0].Rows[num1]["sz_Case_type_name"];
                                item["dt_date_of_accident"] = dataSet.Tables[0].Rows[num1]["dt_date_of_accident"];
                                item["dt_created_Date"] = dataSet.Tables[0].Rows[num1]["dt_created_Date"];
                                item["sz_chart_number"] = dataSet.Tables[0].Rows[num1]["sz_chart_number"];
                                item["i_rfo_chart_no"] = dataSet.Tables[0].Rows[num1]["i_rfo_chart_no"];
                                dataTable1.Rows.Add(item);
                                flag = true;
                            }
                            if (!flag)
                            {
                                num1++;
                            }
                            else
                            {
                                dataSet.Tables[0].Rows[num1].Delete();
                                flag = false;
                                dataSet.AcceptChanges();
                                break;
                            }
                        }
                        num++;
                    }
                }
                this.grdCaseMaster.DataSource = dataTable1;
                this.grdCaseMaster.DataBind();
            }
            else if (!this.Chkmult.Checked || !(this.txtPatientName.Text != "") || this.txtCaseNo.Text.Trim().Length != 0 || this.txtChartNumber.Text.Trim().Length != 0 || !(this.extddlLocation.Text == "NA") && !(this.extddlLocation.Text == ""))
            {
                this.grdCaseMaster.DataSource = sQLToDAO.LoadDataSet("sp_search_added_visits", "mbs.dao.AddVisit", addVisit, "mbs.dao");
                this.grdCaseMaster.DataBind();
            }
            else if (this.grdCaseMaster.Items.Count <= 0)
            {
                this.grdCaseMaster.DataSource = sQLToDAO.LoadDataSet("sp_search_added_visits", "mbs.dao.AddVisit", addVisit, "mbs.dao");
                this.grdCaseMaster.DataBind();
            }
            else
            {
                DataSet dataSet1 = sQLToDAO.LoadDataSet("sp_search_added_visits", "mbs.dao.AddVisit", addVisit, "mbs.dao");
                for (int k = 0; k < dataSet1.Tables[0].Rows.Count; k++)
                {
                    int num2 = 0;
                    for (int l = 0; l < dataTable.Rows.Count; l++)
                    {
                        if (dataSet1.Tables[0].Rows[k]["SZ_CASE_ID"].ToString() == dataTable.Rows[l]["SZ_CASE_ID"].ToString())
                        {
                            num2 = 1;
                        }
                    }
                    if (num2 != 1)
                    {
                        DataRow str = dataTable.NewRow();
                        str["SZ_CASE_ID"] = dataSet1.Tables[0].Rows[k]["SZ_CASE_ID"].ToString();
                        str["SZ_CASE_NO"] = dataSet1.Tables[0].Rows[k]["SZ_CASE_NO"].ToString();
                        str["SZ_PATIENT_ID"] = dataSet1.Tables[0].Rows[k]["SZ_PATIENT_ID"].ToString();
                        str["DT_DATE_OF_ACCIDENT"] = dataSet1.Tables[0].Rows[k]["DT_DATE_OF_ACCIDENT"].ToString();
                        str["SZ_PATIENT_NAME"] = dataSet1.Tables[0].Rows[k]["SZ_PATIENT_NAME"].ToString();
                        str["sz_chart_number"] = dataSet1.Tables[0].Rows[k]["sz_chart_number"].ToString();
                        str["i_rfo_chart_no"] = dataSet1.Tables[0].Rows[k]["i_rfo_chart_no"].ToString();
                        str["SZ_CASE_TYPE_NAME"] = dataSet1.Tables[0].Rows[k]["SZ_CASE_TYPE_NAME"].ToString();
                        str["DT_CREATED_DATE"] = dataSet1.Tables[0].Rows[k]["DT_CREATED_DATE"].ToString();
                        dataTable.Rows.Add(str);
                    }
                }
                this.grdCaseMaster.DataSource = dataTable;
                this.grdCaseMaster.DataBind();
            }
            if (this.grdCaseMaster.Items.Count > 0)
            {
                this.btnSave.Enabled = true;
                this.selectAllCheckBox();
            }
            Bill_Sys_BillingCompanyObject billSysBillingCompanyObject = (Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"];
            if (billSysBillingCompanyObject != null)
            {
                if (!billSysBillingCompanyObject.BT_REFERRING_FACILITY)
                {
                    this.grdCaseMaster.Columns[this.COL_CHART_NUMBER].Visible = true;
                    this.grdCaseMaster.Columns[this.COL_RFO_CHART_NUMBER].Visible = false;
                }
                else
                {
                    this.grdCaseMaster.Columns[this.COL_CHART_NUMBER].Visible = false;
                    this.grdCaseMaster.Columns[this.COL_RFO_CHART_NUMBER].Visible = true;
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
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    //private void BindVisiTypeList(ref RadioButtonList listVisitType)
    //{
    //    Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
    //    try
    //    {
    //        listVisitType.Items.Clear();
    //        listVisitType.DataSource = billSysCalender.GET_Visit_Types(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //        listVisitType.DataTextField = "VISIT_TYPE";
    //        listVisitType.DataValueField = "VISIT_TYPE_ID";
    //        listVisitType.DataBind();
    //        listVisitType.Items[2].Selected = true;
    //    }
    //    catch (Exception exception)
    //    {
    //        this.usrMessage.PutMessage(exception.ToString());
    //        this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
    //        this.usrMessage.Show();
    //    }
    //}

    protected void btnClear_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdCaseMaster.CurrentPageIndex = 0;
            this.SearchList();
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

    protected void btnQuickSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtCaseIDSearch.Text = this.txtVisitSearch.Text;
            this.txtPatientFNameSearch.Text = this.txtFNameSearch.Text;
            this.txtPatientLNameSearch.Text = this.txtLNameSearch.Text;
            this.SearchList();
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

    private const int COL_CASE_ID = 2;

    private bool exists(string szPatientID,ArrayList arPatients)
    {
        bool flagAlreadyExist = false;
        for (int i = 0; i < arPatients.Count; i++)
        {
            if (szPatientID == arPatients[i].ToString())
            {
                flagAlreadyExist = true;
                break;
            }
        }
        return flagAlreadyExist;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
            string str = "";
            string str1 = "";
            string szPatientID = null;
            string dt_Opendate=null;
            string dt_Created =null;
            ArrayList arr = new ArrayList();
            foreach (DataGridItem item in this.grdCaseMaster.Items)
            {
                if (!((CheckBox)item.Cells[0].FindControl("chkSelect")).Checked)
                {
                    continue;
                }
                szPatientID = item.Cells[3].Text;
                ArrayList arrayLists = new ArrayList();
                arrayLists.Add(this.txtCompanyID.Text);
                arrayLists.Add(item.Cells[COL_CASE_ID].Text);
                arrayLists.Add(this.ddlDoctor.SelectedValue);
                arrayLists.Add(this.txtAppointdate.Text);
                if (this._bill_Sys_Visit_BO.Checkvisitexists(arrayLists))
                {
                    str = (str != "" ? string.Concat(str, ", ", item.Cells[4].Text) : item.Cells[4].Text);
                    
                    //List of patients who have a visit for the selected date. Avoid adding visit for them
                    arr.Add(szPatientID);
                }
            }

            foreach (DataGridItem item in this.grdCaseMaster.Items)
            {
                if (!((CheckBox)item.Cells[0].FindControl("chkSelect")).Checked)
                {
                    continue;
                }
                szPatientID = item.Cells[3].Text;
                dt_Opendate =item.Cells[7].Text;
                DateTime dtOpen = Convert.ToDateTime(dt_Opendate);
                dt_Created=item.Cells[6].Text;
                DateTime dtcreate = Convert.ToDateTime(dt_Created);
                bool flagAlreadyExist = exists(szPatientID, arr);
                
                if (flagAlreadyExist == false)
                {
                   
                    if (this.lstProcedureCode.Visible || !(Convert.ToDateTime(this.txtAppointdate.Text) > Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"))))
                    {
                        if ((Convert.ToDateTime(this.txtAppointdate.Text) >= Convert.ToDateTime(dtOpen.ToString("MM/dd/yyyy"))) && (Convert.ToDateTime(this.txtAppointdate.Text) >= Convert.ToDateTime(dtcreate.ToString("MM/dd/yyyy"))))
                        {
                            bool flag2 = false;
                            if (!((CheckBox)item.Cells[0].FindControl("chkSelect")).Checked)
                            {
                                continue;
                            }

                            this._bill_Sys_Calender = new Bill_Sys_Calender();
                            this.objAdd = new ArrayList();
                            this.objAdd.Add(item.Cells[2].Text);
                            this.objAdd.Add(this.txtAppointdate.Text);
                            this.objAdd.Add("8.30");
                            this.objAdd.Add("");
                            this.objAdd.Add(this.ddlDoctor.SelectedValue);
                            this.objAdd.Add("TY000000000000000003");
                            this.objAdd.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            this.objAdd.Add("AM");
                            this.objAdd.Add("9.00");
                            this.objAdd.Add("AM");
                            this.objAdd.Add(szPatientID);


                            this._bill_Sys_Calender.SaveEventWithAutoVisitType(this.objAdd, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                            ArrayList arrayLists1 = new ArrayList();
                            arrayLists1.Add(item.Cells[2].Text);
                            arrayLists1.Add(this.ddlDoctor.SelectedValue);
                            arrayLists1.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                            int eventID = this._bill_Sys_Calender.GetEventID(arrayLists1);

                            if (this.lstProcedureCode.Visible)
                            {
                                this._bill_Sys_Calender = new Bill_Sys_Calender();

                                this.objAdd = new ArrayList();
                                this.objAdd.Add(eventID);
                                this.objAdd.Add(false);
                                this.objAdd.Add(this.ddlStatus.SelectedValue);
                                this._bill_Sys_Calender.UPDATE_Event_Status(this.objAdd);
                                foreach (ListItem listItem in this.lstProcedureCode.Items)
                                {
                                    if (!listItem.Selected)
                                    {
                                        continue;
                                    }
                                    this.objAdd = new ArrayList();
                                    this.objAdd.Add(listItem.Value);
                                    this.objAdd.Add(eventID);
                                    this.objAdd.Add(this.ddlStatus.SelectedValue);
                                    this._bill_Sys_Calender.Save_Event_RefferPrcedure(this.objAdd);
                                }
                            }
                            this._DAO_NOTES_EO = new DAO_NOTES_EO();
                            this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "APPOINTMENT_ADDED";
                            this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = string.Concat("Date : ", this.txtAppointdate.Text);
                            this._DAO_NOTES_BO = new DAO_NOTES_BO();
                            this._DAO_NOTES_EO.SZ_USER_ID = ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID;
                            this._DAO_NOTES_EO.SZ_CASE_ID = item.Cells[2].Text;
                            this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                            this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                            flag2 = true;

                            if (flag2)
                            {
                                this.lblMsg.Text = "Appointment(s) scheduled successfully";
                            }
                        }
                        else 
                        {

                            str1 = (str1 != "" ? string.Concat(str1, ", ", item.Cells[4].Text) : item.Cells[4].Text);
                            //this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ss", "<script language='javascript'>alert('Visit date cannot be future date from open date');</script>");
                        }
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "ss", "<script language='javascript'>alert('Visit for future date cannot be added');</script>");
                    }
                }
            }
            
            if (str != "")
            {
                ClientScriptManager clientScript = this.Page.ClientScript;
                Type type = base.GetType();
                string[] text = new string[] { "<script language='javascript'>alert('Visit for ", str, " already scheduled on ", this.txtAppointdate.Text, " ...!');</script>" };
                clientScript.RegisterClientScriptBlock(type, "ss", string.Concat(text));
            }
            if (str1 != "")
            {
                lblDateMsg.Text = "Visit for "+str1+" can be future date from open date.";
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._listOperation = new ListOperation();
        try
        {
            this._listOperation.WebPage = this.Page;
            this._listOperation.Xml_File = "SearchCase.xml";
            this._listOperation.LoadList();
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

    protected void btnSearchPArameter_Click(object sender, EventArgs e)
    {
        this.BindGrid();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            base.Response.Write("Hello");
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

    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(new Bill_Sys_Calender()).CheckReferralExists(this.ddlDoctor.SelectedValue, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID))
        {
            this.RowProcedureCode.Visible = false;
            this.RowVisitStatus.Visible = false;
            return;
        }
        this.RowProcedureCode.Visible = true;
        this.RowVisitStatus.Visible = true;
        this.GetProcedureCode(this.ddlDoctor.SelectedValue);
    }

    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
    }

    protected void extddlInsurance_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._caseDetailsBO = new CaseDetailsBO();
    }

    protected void extddlLocation_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this._caseDetailsBO = new CaseDetailsBO();
            DataSet dataSet = this._caseDetailsBO.DoctorName(extddlLocation.Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.ddlDoctor.DataSource = dataSet;
            this.ddlDoctor.DataTextField = "DESCRIPTION";
            this.ddlDoctor.DataValueField = "CODE";
            this.ddlDoctor.DataBind();
            this.ddlDoctor.Items.Insert(0, "---select---");
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
       
        if (this.txtCaseNo.Text != "")
        {
            this.BindGrid();
            return;
        }
        if (this.txtPatientName.Text != "")
        {
            this.BindGrid();
            return;
        }
        this.grdCaseMaster.DataSource = null;
        this.grdCaseMaster.DataBind();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        this._caseDetailsBO = new CaseDetailsBO();
        this.SearchList();
    }

    private string getCaseList()
    {
        string str = "";
        bool flag = false;
        this.txtCaseNo.Text = this.txtCaseNo.Text.Replace("'", "");
        this.txtCaseNo.Text = this.txtCaseNo.Text.Replace("\"", "");
        string[] strArrays = this.txtCaseNo.Text.Split(new char[] { ',' });
        if ((int)strArrays.Length > 0)
        {
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                if (strArrays[i].Length <= 2)
                {
                    str = string.Concat(str, "'", strArrays[i], "',");
                    this._caseno = string.Concat(this._caseno, ",", strArrays[i]);
                }
                else
                {
                    char[] charArray = strArrays[i].ToCharArray(0, 2);
                    int num = 0;
                    while (num < (int)charArray.Length)
                    {
                        if ((charArray[num] < 'A' || charArray[num] > 'Z') && (charArray[num] < 'a' || charArray[num] > 'z'))
                        {
                            num++;
                        }
                        else
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        strArrays[i] = strArrays[i].Substring(2);
                        flag = false;
                    }
                    str = string.Concat(str, "'", strArrays[i], "',");
                    this._caseno = string.Concat(this._caseno, ",", strArrays[i]);
                }
            }
            str = str.Remove(str.Length - 1);
            str = string.Concat("(", str, ")");
        }
        return str;
    }

    private string getChartNumberList()
    {
        string str = "";
        this.txtChartNumber.Text = this.txtChartNumber.Text.Replace("'", "");
        this.txtChartNumber.Text = this.txtChartNumber.Text.Replace("\"", "");
        string[] strArrays = this.txtChartNumber.Text.Split(new char[] { ',' });
        if ((int)strArrays.Length > 0)
        {
            for (int i = 0; i < (int)strArrays.Length; i++)
            {
                if (strArrays[i].Trim().Length != 0)
                {
                    str = string.Concat(str, "'", strArrays[i], "',");
                }
            }
            str = str.Remove(str.Length - 1);
            str = string.Concat("(", str, ")");
        }
        return str;
    }

    public void getDoctorDefaultList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_DoctorBO billSysDoctorBO = new Bill_Sys_DoctorBO();
            DataSet doctorList = billSysDoctorBO.GetDoctorList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.ddlDoctor.DataSource = doctorList;
            this.ddlDoctor.DataTextField = "DESCRIPTION";
            this.ddlDoctor.DataValueField = "CODE";
            this.ddlDoctor.DataBind();
            this.ddlDoctor.Items.Insert(0, "---select---");
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

    private void GetProcedureCode(string p_szDoctorID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_Calender billSysCalender = new Bill_Sys_Calender();
        try
        {
            this.lstProcedureCode.Items.Clear();
            this.lstProcedureCode.DataSource = billSysCalender.GetReferringDoctorProcedureCodeList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, p_szDoctorID);
            this.lstProcedureCode.DataTextField = "DESCRIPTION";
            this.lstProcedureCode.DataValueField = "CODE";
            this.lstProcedureCode.DataBind();
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

    //protected void grdCaseMaster_ItemDataBound(object sender, DataGridItemEventArgs e)
    //{
        //if (e.Item.ItemType.ToString() != "Header" && e.Item.ItemType.ToString() != "Footer")
        //{
        //    RadioButtonList radioButtonList = (RadioButtonList)e.Item.Cells[7].FindControl("listVisitType");
        //    this.BindVisiTypeList(ref radioButtonList);
        //}
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        base.Title = "Green Your Bills - Add Visits";
        try
        {
            string str = "<script language=JavaScript> function autoComplete (field, select, property, forcematch) {var found = false;for (var i = 0; i < select.options.length; i++) {if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) {\t\tfound=true; break;}}if (found) { select.selectedIndex = i; }else {select.selectedIndex = -1;}if (field.createTextRange) {if (forcematch && !found) {field.value=field.value.substring(0,field.value.length-1); return;}var cursorKeys ='8;46;37;38;39;40;33;34;35;36;45;';if (cursorKeys.indexOf(event.keyCode+';') == -1) {var r1 = field.createTextRange();var oldValue = r1.text;var newValue = found ? select.options[i][property] : oldValue;if (newValue != field.value) {field.value = newValue;var rNew = field.createTextRange();rNew.moveStart('character', oldValue.length) ;rNew.select();}}}} </script>";
            this.txtCaseNo.Attributes.Add("onkeydown", string.Concat("if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('", this.btnSearchPArameter.ClientID, "').click();return false;}} else {return true};"));
            this.txtChartNumber.Attributes.Add("onkeydown", string.Concat("if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('", this.btnSearchPArameter.ClientID, "').click();return false;}} else {return true};"));
            this.RegisterClientScriptBlock("ClientScript", str);
            this.ajAutoName.ContextKey = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.btnSave.Attributes.Add("onClick", "return ShowConfirmation();");
            Bill_Sys_SystemObject item = (Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"];
            if (item != null)
            {
                item.AddVisits_SearchByChartNumber="1";
                if (item.AddVisits_SearchByChartNumber == null)
                {
                    this.trChartNumber.Visible = false;
                }
                else if (item.AddVisits_SearchByChartNumber != "1")
                {
                    this.trChartNumber.Visible = false;
                }
                else
                {
                    this.trChartNumber.Visible = true;
                }
            }
            if (!base.IsPostBack)
            {
                this.btnSave.Enabled = false;
                this.txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                this.extddlLocation.Flag_ID = this.txtCompanyID.Text.ToString();
                //((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY;
                this.txtIsActivePatient.Text = "ACTIVE PATIENT";
                this.RowProcedureCode.Visible = false;
                this.RowVisitStatus.Visible = false;
                this.ddlDoctor.Items.Insert(0, "---select---");
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_LOCATION == "1")
                {
                    this.extddlLocation.Visible = true;
                    this.RowLocation.Visible = true;
                }
                else
                {
                    this.extddlLocation.Visible = false;
                    this.RowLocation.Visible = false;
                    this.getDoctorDefaultList();
                }
                if (((Bill_Sys_SystemObject)this.Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "1")
                {
                    this.trChartNumber.Visible = true;
                }
                else
                {
                    this.trChartNumber.Visible = false;
                }
            }
            this.lblMsg.Text = "";
            lblDateMsg.Text = "";
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
        
        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_Sys_MultipleVisits.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void rdoISActivePatient_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.txtIsActivePatient.Text = "";
            foreach (ListItem item in this.rdoISActivePatient.Items)
            {
                if (!item.Selected)
                {
                    continue;
                }
                string str = item.Value.ToString();
                if (str != "ACTIVE PATIENT")
                {
                    if (str != "All Patient")
                    {
                        continue;
                    }
                    this.SearchList();
                    break;
                }
                else
                {
                    this.txtIsActivePatient.Text = str;
                    this.SearchList();
                    break;
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
        this._listOperation = new ListOperation();
        try
        {
            this.BindGrid();
            if (this.grdCaseMaster.Items.Count > 0)
            {
                this.btnSave.Enabled = true;
                this.selectAllCheckBox();
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

    public void selectAllCheckBox()
    {
        if (this.rdoISActivePatient.SelectedValue.ToString().Equals("ACTIVE PATIENT"))
        {
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", "SelectAll();", true);
        }
    }
}