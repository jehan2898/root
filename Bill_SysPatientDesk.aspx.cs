using AjaxControlToolkit;
using ASP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Bill_SysPatientDesk : Page, IRequiresSessionState
{


    private Bill_Sys_PatientBO _bill_Sys_PatientBO;

    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;

    private DAO_NOTES_BO _DAO_NOTES_BO;

    private DAO_NOTES_EO _DAO_NOTES_EO;

    private Bill_Sys_DeleteBO _deleteOpeation;

    private bool blnTag;

    private bool blnWeekShortNames = true;

    private bool btIsConfig;

    private DataTable dtAllRoomEvents;

    private DataTable dtAllSpecialityEvents;

    private DataTable dtVisitType;

    private ArrayList objAdd;

    private Bill_SysPatientDesk.Calendar_DAO objCalendar;

    private string szDateColor_NA = "#ff6347";

    private string szDateColor_TODAY = "#FFFF80";


    public Bill_SysPatientDesk()
    {
    }

    private void ActiveTab(string p_szID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string pSzID = p_szID;
            string str = pSzID;
            if (pSzID != null)
            {
                string str1 = str;
                string str2 = str1;
                if (str1 != null)
                {
                    switch (str2)
                    {
                        case "grdOne":
                            {
                                this.tabVistInformation.ActiveTabIndex = 0;
                                return;
                            }
                        case "grdTwo":
                            {
                                this.tabVistInformation.ActiveTabIndex = 1;
                                return;
                            }
                        case "grdThree":
                            {
                                this.tabVistInformation.ActiveTabIndex = 2;
                                return;
                            }
                        case "grdFour":
                            {
                                this.tabVistInformation.ActiveTabIndex = 3;
                                return;
                            }
                        case "grdFive":
                            {
                                this.tabVistInformation.ActiveTabIndex = 4;
                                return;
                            }
                        case "grdSix":
                            {
                                this.tabVistInformation.ActiveTabIndex = 5;
                                return;
                            }
                        case "grdSeven":
                            {
                                this.tabVistInformation.ActiveTabIndex = 6;
                                return;
                            }
                        case "grdEight":
                            {
                                this.tabVistInformation.ActiveTabIndex = 7;
                                return;
                            }
                        case "grdNine":
                            {
                                this.tabVistInformation.ActiveTabIndex = 8;
                                return;
                            }
                        case "grdTen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 9;
                                return;
                            }
                        case "grdEleven":
                            {
                                this.tabVistInformation.ActiveTabIndex = 10;
                                return;
                            }
                        case "grdTwelve":
                            {
                                this.tabVistInformation.ActiveTabIndex = 11;
                                return;
                            }
                        case "grdThirteen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 12;
                                return;
                            }
                        case "grdFourteen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 13;
                                return;
                            }
                        case "grdFifteen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 14;
                                return;
                            }
                        case "grdSixteen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 15;
                                return;
                            }
                        case "grdSeventeen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 16;
                                return;
                            }
                        case "grdEighteen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 17;
                                return;
                            }
                        case "grdNineteen":
                            {
                                this.tabVistInformation.ActiveTabIndex = 18;
                                return;
                            }
                        case "grdTwenty":
                            {
                                this.tabVistInformation.ActiveTabIndex = 19;
                                return;
                            }
                        case "grdTwentyOne":
                            {
                                this.tabVistInformation.ActiveTabIndex = 20;
                                return;
                            }
                        case "grdTwentyTwo":
                            {
                                this.tabVistInformation.ActiveTabIndex = 21;
                                return;
                            }
                        case "grdTwentyThree":
                            {
                                this.tabVistInformation.ActiveTabIndex = 22;
                                return;
                            }
                        case "grdTwentyFour":
                            {
                                this.tabVistInformation.ActiveTabIndex = 23;
                                return;
                            }
                        case "grdTwentyFive":
                            {
                                this.tabVistInformation.ActiveTabIndex = 24;
                                return;
                            }
                        case "grdTwentySix":
                            {
                                this.tabVistInformation.ActiveTabIndex = 25;
                                return;
                            }
                        case "grdTwentySeven":
                            {
                                this.tabVistInformation.ActiveTabIndex = 26;
                                return;
                            }
                        case "grdTwentyEight":
                            {
                                this.tabVistInformation.ActiveTabIndex = 27;
                                return;
                            }
                        case "grdTwentyNine":
                            {
                                this.tabVistInformation.ActiveTabIndex = 28;
                                return;
                            }
                        case "grdThirty":
                            {
                                this.tabVistInformation.ActiveTabIndex = 29;
                                return;
                            }
                        case "grdThirtyOne":
                            {
                                this.tabVistInformation.ActiveTabIndex = 30;
                                return;
                            }
                        case "grdThirtyTwo":
                            {
                                this.tabVistInformation.ActiveTabIndex = 31;
                                return;
                            }
                        case "grdThirtyThree":
                            {
                                this.tabVistInformation.ActiveTabIndex = 32;
                                return;
                            }
                        case "grdThirtyFour":
                            {
                                this.tabVistInformation.ActiveTabIndex = 33;
                                return;
                            }
                        case "grdThirtyFive":
                            {
                                this.tabVistInformation.ActiveTabIndex = 34;
                                return;
                            }
                    }
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_DeleteBO billSysDeleteBO = new Bill_Sys_DeleteBO();
        string str = "";
        DataGrid dataGrid = new DataGrid();
        try
        {
            switch (Convert.ToInt32(((Button)sender).CommandArgument))
            {
                case 1:
                    {
                        dataGrid = this.grdOne;
                        break;
                    }
                case 2:
                    {
                        dataGrid = this.grdTwo;
                        break;
                    }
                case 3:
                    {
                        dataGrid = this.grdThree;
                        break;
                    }
                case 4:
                    {
                        dataGrid = this.grdFour;
                        break;
                    }
                case 5:
                    {
                        dataGrid = this.grdFive;
                        break;
                    }
                case 6:
                    {
                        dataGrid = this.grdSix;
                        break;
                    }
                case 7:
                    {
                        dataGrid = this.grdSeven;
                        break;
                    }
                case 8:
                    {
                        dataGrid = this.grdEight;
                        break;
                    }
                case 9:
                    {
                        dataGrid = this.grdNine;
                        break;
                    }
                case 10:
                    {
                        dataGrid = this.grdTen;
                        break;
                    }
                case 11:
                    {
                        dataGrid = this.grdEleven;
                        break;
                    }
                case 12:
                    {
                        dataGrid = this.grdTwelve;
                        break;
                    }
                case 13:
                    {
                        dataGrid = this.grdThirteen;
                        break;
                    }
                case 14:
                    {
                        dataGrid = this.grdFourteen;
                        break;
                    }
                case 15:
                    {
                        dataGrid = this.grdFifteen;
                        break;
                    }
                case 16:
                    {
                        dataGrid = this.grdSixteen;
                        break;
                    }
                case 17:
                    {
                        dataGrid = this.grdSeventeen;
                        break;
                    }
                case 18:
                    {
                        dataGrid = this.grdEighteen;
                        break;
                    }
                case 19:
                    {
                        dataGrid = this.grdNineteen;
                        break;
                    }
                case 20:
                    {
                        dataGrid = this.grdTwenty;
                        break;
                    }
                case 21:
                    {
                        dataGrid = this.grdTwentyOne;
                        break;
                    }
                case 22:
                    {
                        dataGrid = this.grdTwentyTwo;
                        break;
                    }
                case 23:
                    {
                        dataGrid = this.grdTwentyThree;
                        break;
                    }
                case 24:
                    {
                        dataGrid = this.grdTwentyFour;
                        break;
                    }
                case 25:
                    {
                        dataGrid = this.grdTwentyFive;
                        break;
                    }
                case 26:
                    {
                        dataGrid = this.grdTwentySix;
                        break;
                    }
                case 27:
                    {
                        dataGrid = this.grdTwentySeven;
                        break;
                    }
                case 28:
                    {
                        dataGrid = this.grdTwentyEight;
                        break;
                    }
                case 29:
                    {
                        dataGrid = this.grdTwentyNine;
                        break;
                    }
                case 30:
                    {
                        dataGrid = this.grdThirty;
                        break;
                    }
                case 31:
                    {
                        dataGrid = this.grdThirtyOne;
                        break;
                    }
                case 32:
                    {
                        dataGrid = this.grdThirtyTwo;
                        break;
                    }
                case 33:
                    {
                        dataGrid = this.grdThirtyThree;
                        break;
                    }
                case 34:
                    {
                        dataGrid = this.grdThirtyFour;
                        break;
                    }
                case 35:
                    {
                        dataGrid = this.grdThirtyFive;
                        break;
                    }
            }
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                if (((CheckBox)dataGrid.Items[i].FindControl("chkDelete")).Checked)
                {
                    if (dataGrid.Items[i].Cells[22].Text == "True")
                    {
                        this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", "<script language='javascript'> alert('You can not delete billed visits!');</script>");
                        this.LoadTabInformation();
                        this.ActiveTab(dataGrid.ID);
                    }
                    else if (((TextBox)dataGrid.Items[i].FindControl("txtScheduleType")).Text == "OUT" && ((DropDownList)dataGrid.Items[i].Cells[5].FindControl("ddlStatus")).SelectedValue == "2")
                    {
                        this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", "<script language='javascript'> alert('You can not delete completed visit!');</script>");
                        this.LoadTabInformation();
                        this.ActiveTab(dataGrid.ID);
                    }
                    else if (!billSysDeleteBO.deleteRecord("SP_TXN_CALENDAR_EVENT", "@I_EVENT_ID", dataGrid.Items[i].Cells[0].Text))
                    {
                        if (str == "")
                        {
                            str = string.Concat(dataGrid.Items[i].Cells[1].Text, "-", dataGrid.Items[i].Cells[4].Text);
                        }
                        else
                        {
                            string[] text = new string[] { str, " , ", dataGrid.Items[i].Cells[2].Text, "-", dataGrid.Items[i].Cells[4].Text };
                            str = string.Concat(text);
                        }

                        this._DAO_NOTES_EO = new DAO_NOTES_EO();
                        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "EVENT_DELETED";
                        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Event Id : " + dataGrid.Items[i].Cells[0].Text + "deleted.";
                        this._DAO_NOTES_BO = new DAO_NOTES_BO();
                        this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                        this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    }
                }
            }
            if (str != "")
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", string.Concat("alert('Records for Visit ", str, "  exists.'); "), true);
            }
            else
            {
                this.LoadTabInformation();
                this.tabVistInformation.ActiveTabIndex = Convert.ToInt32(((Button)sender).CommandArgument) - 1;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
    }

    private void ConfigPatientDesk()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            foreach (DataRow row in this._bill_Sys_PatientBO.GetConfigPatientDesk(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE).Rows)
            {
                switch (row[0].ToString())
                {
                    case "Treatment Information":
                        {
                            this.tblTreatmentInformation.Visible = true;
                            this.GetTreatmentCountList();
                            continue;
                        }
                    case "Last Treatment":
                        {
                            this.tblLastTreatment.Visible = true;
                            this.GetDoctorTreatment();
                            continue;
                        }
                    case "All Treatments":
                        {
                            this.tblAllTreatment.Visible = true;
                            this.GetDoctorTreatmentList();
                            continue;
                        }
                    case "Billing Information":
                        {
                            this.tblBillingInformation.Visible = true;
                            this.GetBillingInformation();
                            continue;
                        }
                    case "Visit Information":
                        {
                            this.tblVisitInformation.Visible = false;
                            continue;
                        }
                    case "Test Information":
                        {
                            this.tblTestInformation.Visible = false;
                            continue;
                        }
                    case "Re-Schedule Information":
                        {
                            this.btIsConfig = true;
                            continue;
                        }
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

    protected void dg_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        DataGrid dataGrid = (DataGrid)source;
        dataGrid.EditItemIndex = -1;
        this.LoadTabInformation();
        this.ActiveTab(dataGrid.ID);
    }

    protected void dg_EditCommand(object source, DataGridCommandEventArgs e)
    {
        DataGrid itemIndex = (DataGrid)source;
        if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
        {
            string str = "";
            string text = itemIndex.Items[e.Item.ItemIndex].Cells[0].Text;
            string text1 = itemIndex.Items[e.Item.ItemIndex].Cells[29].Text;
            string str1 = itemIndex.Items[e.Item.ItemIndex].Cells[24].Text;
            string text2 = itemIndex.Items[e.Item.ItemIndex].Cells[25].Text;
            string str2 = itemIndex.Items[e.Item.ItemIndex].Cells[26].Text;
            string text3 = itemIndex.Items[e.Item.ItemIndex].Cells[27].Text;
            string str3 = itemIndex.Items[e.Item.ItemIndex].Cells[28].Text;
            string str4 = str;
            string[] d = new string[] { str4, str1, " ", text2, "|", str2, "!", text1, "~", text, "^", text3, str3, "&From=PatientDesk&GRD_ID=", itemIndex.ID };
            str = string.Concat(d);
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", string.Concat("<script language='javascript'>  EDIT_MRI_OutSchedule('", str, "');</script>"));
            this.Session["TestFacilityID"] = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            this.Session["INTERVAL"] = "30";
            this.LoadTabInformation();
            return;
        }
        if (((TextBox)itemIndex.Items[0].FindControl("txtScheduleType")).Text == "OUT")
        {
            if (((DropDownList)itemIndex.Items[e.Item.ItemIndex].Cells[5].FindControl("ddlStatus")).SelectedValue.ToString() == "2")
            {
                this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", "<script language='javascript'> alert('You can not update completed visits!');</script>");
                this.LoadTabInformation();
                this.ActiveTab(itemIndex.ID);
                return;
            }
            ClientScriptManager clientScript = this.Page.ClientScript;
            Type type = base.GetType();
            string[] strArrays = new string[] { "<script language='javascript'>  EDITOutSchedule('", itemIndex.Items[e.Item.ItemIndex].Cells[0].Text, "&GRD_ID=", itemIndex.ID, "');</script>" };
            clientScript.RegisterStartupScript(type, "ss", string.Concat(strArrays));
            this.LoadTabInformation();
            this.ActiveTab(itemIndex.ID);
            return;
        }
        if (itemIndex.Items[e.Item.ItemIndex].Cells[22].Text == "True")
        {
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss", "<script language='javascript'> alert('You can not update billed visits!');</script>");
            this.LoadTabInformation();
            this.ActiveTab(itemIndex.ID);
            return;
        }
        if (itemIndex.Items[e.Item.ItemIndex].Cells[21].Text != "True")
        {
            itemIndex.EditItemIndex = e.Item.ItemIndex;
            DropDownList dropDownList = (DropDownList)itemIndex.Items[e.Item.ItemIndex].Cells[5].FindControl("ddlStatus");
            this.LoadTabInformation();
            DropDownList selectedValue = (DropDownList)itemIndex.Items[e.Item.ItemIndex].Cells[5].FindControl("ddlStatus");
            selectedValue.Enabled = true;
            selectedValue.SelectedValue = dropDownList.SelectedValue;
            this.ActiveTab(itemIndex.ID);
            return;
        }
        ClientScriptManager clientScriptManager = this.Page.ClientScript;
        Type type1 = base.GetType();
        string[] strArrays1 = new string[] { "<script language='javascript'>  EDITSchedule('", itemIndex.Items[e.Item.ItemIndex].Cells[0].Text, "&GRD_ID=", itemIndex.ID, "');</script>" };
        clientScriptManager.RegisterStartupScript(type1, "ss", string.Concat(strArrays1));
        this.LoadTabInformation();
        this.ActiveTab(itemIndex.ID);
    }

    protected void dg_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str;
        Bill_Sys_Upload_VisitReport billSysUploadVisitReport = new Bill_Sys_Upload_VisitReport();
        if (e.CommandName == "Upload")
        {
            if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY.ToString() == "True")
            {
                this.Page.RegisterStartupScript("MM3", "<script type='text/javascript'>alert('Report Upload is not allowed')</script>");
                return;
            }
            DataGrid dataGrid = (DataGrid)source;
            try
            {
                this.Session["UploadReport_DoctorId"] = dataGrid.Items[e.Item.ItemIndex].Cells[20].Text;
                if (dataGrid.Items[e.Item.ItemIndex].Cells[8].Text.Substring(0, 2) == "IE" || dataGrid.Items[e.Item.ItemIndex].Cells[8].Text.Substring(0, 2) == "FU")
                {
                    this.Session["UploadReport_VisitType"] = dataGrid.Items[e.Item.ItemIndex].Cells[8].Text.Substring(0, 2);
                    this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ss12234", "<script language='javascript'>  showPateintFrame();</script>");
                    str = (dataGrid.Items[e.Item.ItemIndex].Cells[29].Text.Substring(0, 1) == "&" || dataGrid.Items[e.Item.ItemIndex].Cells[29].Text.Substring(0, 1) == "n" ? billSysUploadVisitReport.GetDoctorSpecialty(dataGrid.Items[e.Item.ItemIndex].Cells[20].Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString()) : dataGrid.Items[e.Item.ItemIndex].Cells[29].Text);
                    this.Session["UploadReport_ProcedureGroupId"] = str;
                    this.Session["UploadReport_EventId"] = dataGrid.Items[e.Item.ItemIndex].Cells[10].Text;
                }
                else
                {
                    this.Page.RegisterStartupScript("MM3", "<script type='text/javascript'>alert('Report Upload is not allowed')</script>");
                    return;
                }
            }
            catch (Exception exception)
            {
                string message = exception.Message;
                this.Page.RegisterStartupScript("MM3", "<script type='text/javascript'>alert('Report Upload is not allowed')</script>");
                this.Session["UploadReport_ProcedureGroupId"] = "";
                this.Session["UploadReport_DoctorId"] = "";
                this.Session["UploadReport_VisitType"] = "";
                this.Session["UploadReport_EventId"] = "";
            }
        }
        if (e.CommandName == "Scan")
        {
            DataGrid dataGrid1 = (DataGrid)source;
            string str1 = "";
            str1 = (dataGrid1.Items[e.Item.ItemIndex].Cells[29].Text.Substring(0, 1) == "&" || dataGrid1.Items[e.Item.ItemIndex].Cells[29].Text.Substring(0, 1) == "n" ? billSysUploadVisitReport.GetDoctorSpecialty(dataGrid1.Items[e.Item.ItemIndex].Cells[20].Text, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString()) : dataGrid1.Items[e.Item.ItemIndex].Cells[29].Text);
            string text = dataGrid1.Items[e.Item.ItemIndex].Cells[8].Text;
            string text1 = dataGrid1.Items[e.Item.ItemIndex].Cells[0].Text;
            string str2 = "";
            if (text.Contains("<"))
            {
                string[] strArrays = text.Split(new char[] { '<' });
                if (strArrays[0].ToString() == "FU")
                {
                    str2 = "Followup Report";
                }
                if (strArrays[0].ToString() == "IE")
                {
                    str2 = "Initial Report";
                }
            }
            else
            {
                if (text == "FU")
                {
                    str2 = "Followup Report";
                }
                if (text == "IE")
                {
                    str2 = "Initial Report";
                }
            }
            if (str2 != "")
            {
                string nodeType = billSysUploadVisitReport.GetNodeType(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString(), str2, str1);
                this.RedirectToScanApp(nodeType, text1, str1);
                return;
            }
            this.Page.RegisterStartupScript("MM3", "<script type='text/javascript'>alert('Report Upload is not allowed')</script>");
        }
    }

    protected void dg_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        this._bill_Sys_Visit_BO = new Bill_Sys_Visit_BO();
        DataGrid dataGrid = (DataGrid)source;
        TextBox textBox = (TextBox)dataGrid.Items[e.Item.ItemIndex].Cells[19].FindControl("txtEventType");
        DropDownList dropDownList = (DropDownList)dataGrid.Items[e.Item.ItemIndex].Cells[5].FindControl("ddlStatus");
        string text = dataGrid.Items[e.Item.ItemIndex].Cells[3].Text;
        ArrayList arrayLists = new ArrayList();
        arrayLists.Add(textBox.Text);
        arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        arrayLists.Add(text);
        arrayLists.Add(dropDownList.SelectedValue);
        this._bill_Sys_Visit_BO.UpdateCalenderEvent(arrayLists);
        this._DAO_NOTES_EO = new DAO_NOTES_EO();
        this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "EVENT_STATUS_UPDATED";
        this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "EVENT Id : " + arrayLists[0].ToString() + " to  " + arrayLists[3].ToString();
        this._DAO_NOTES_BO = new DAO_NOTES_BO();
        this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
        this._DAO_NOTES_EO.SZ_CASE_ID = (((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
        this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
        dataGrid.EditItemIndex = -1;
        this.LoadTabInformation();
        this.ActiveTab(dataGrid.ID);
    }

    protected void extddlDoctor_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void GetBillingInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdBillingInformation.DataSource = this._bill_Sys_PatientBO.GetPatienDeskList("BILLINGINFORMATION", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdBillingInformation.DataBind();
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

    private void GetDoctorTreatment()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdDoctorTreatment.DataSource = this._bill_Sys_PatientBO.GetPatienDeskList("GETLASTTREATINGDOCTOR", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdDoctorTreatment.DataBind();
            for (int i = 0; i < this.grdDoctorTreatment.Items.Count; i++)
            {
                if (i != 0)
                {
                    Label label = (Label)this.grdDoctorTreatment.Items[i].Cells[0].FindControl("lblDocName");
                    label.Text = "";
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

    private void GetDoctorTreatmentList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdDoctorTreatmentList.DataSource = this._bill_Sys_PatientBO.GetPatienDeskList("GETTREATINGDOCTOR", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdDoctorTreatmentList.DataBind();
            string text = "";
            for (int i = 0; i < this.grdDoctorTreatmentList.Items.Count; i++)
            {
                Label label = (Label)this.grdDoctorTreatmentList.Items[i].Cells[0].FindControl("lblDoctorName");
                if (!(text != "") || !(text == label.Text))
                {
                    text = label.Text;
                }
                else
                {
                    label.Text = "";
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

    private void GetPatientDetailList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdPatientList.DataSource = this._bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private void GetRescheduleList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdReSchedule.DataSource = this._bill_Sys_PatientBO.GetRescheduleList("LIST_OF_RESCHEDULE", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdReSchedule.DataBind();
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

    private void GetTestInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdTestInformation.DataSource = this._bill_Sys_PatientBO.GetVisitInformation(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdTestInformation.DataBind();
            string text = "---";
            foreach (DataGridItem item in this.grdTestInformation.Items)
            {
                if (text == item.Cells[0].Text)
                {
                    text = item.Cells[0].Text;
                    item.Cells[0].Text = "";
                }
                else
                {
                    text = item.Cells[0].Text;
                }
                if (text != "---")
                {
                    continue;
                }
                text = item.Cells[0].Text;
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

    private void GetTreatmentCountList()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdTreatment.DataSource = this._bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTTOTALTREATMENT", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            this.grdTreatment.DataBind();
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

    private void GetVisitInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this._bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        try
        {
            this.grdVisitInformation.DataSource = this._bill_Sys_PatientBO.GetPatientVisitDeskList(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
            this.grdVisitInformation.DataBind();
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

    private void LoadTabInformation()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DropDownList text;
        LinkButton linkButton;
        LinkButton linkButton1;
        DataRow[] dataRowArray;
        int i;
        int year;
        object[] str;
        char[] chrArray;
        try
        {
            Bill_Sys_PatientBO billSysPatientBO = new Bill_Sys_PatientBO();
            DataTable dataTable = new DataTable();
            DataTable patientDeskRoomList = new DataTable();
            if (this.Session["SpecialityList"] == null)
            {
                dataTable = billSysPatientBO.Get_SpecialityList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                patientDeskRoomList = billSysPatientBO.Get_PatientDeskRoomList(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                dataTable.Merge(patientDeskRoomList);
                this.Session["SpecialityList"] = dataTable;
            }
            else
            {
                dataTable = (DataTable)this.Session["SpecialityList"];
            }
            int num = 0;
            DateTime date = DateTime.Today.Date;
            string str1 = date.ToString("MM/dd/yyyy");
            this.dtAllSpecialityEvents = new DataTable();
            this.dtAllRoomEvents = new DataTable();
            if (!((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY)
            {
                this.dtAllSpecialityEvents = billSysPatientBO.Get_Tab_TestInformation_TEMP(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.dtAllRoomEvents = billSysPatientBO.Get_Outschedule_Tab_Information(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.dtAllSpecialityEvents.Merge(this.dtAllRoomEvents);
            }
            else
            {
                this.dtAllRoomEvents = billSysPatientBO.Get_Outschedule_Tab_Information(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                this.dtAllSpecialityEvents.Merge(this.dtAllRoomEvents);
            }
            DataTable dataTable1 = this.dtAllSpecialityEvents.Clone();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    num++;
                    switch (num)
                    {
                        case 1:
                            {
                                this.tabpnlOne.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label = this.lblHeadOne;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label.Text = string.Concat(str);
                                this.grdOne.DataSource = dataTable1;
                                this.grdOne.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdOne);
                                for (int j = 0; j < this.grdOne.Items.Count; j++)
                                {
                                    text = (DropDownList)this.grdOne.Items[j].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdOne.Items[j].Cells[17].Text;
                                    if (this.grdOne.Items[j].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor = (HtmlAnchor)this.grdOne.Items[j].Cells[30].FindControl("lnkView");
                                        htmlAnchor.Visible = false;
                                    }
                                    string text1 = this.grdOne.Items[j].Cells[8].Text;
                                    if (text1.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text1.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdOne.Items[j].Cells[30].FindControl("btnLinkone");
                                            //linkButton1 = (LinkButton)this.grdOne.Items[j].Cells[30].FindControl("lnkScanOne");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor1 = (HtmlAnchor)this.grdOne.Items[j].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor1.Visible = false;
                                        }
                                    }
                                    else if (this.grdOne.Items[j].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdOne.Items[j].Cells[30].FindControl("btnLinkone");
                                        //linkButton1 = (LinkButton)this.grdOne.Items[j].Cells[30].FindControl("lnkScanOne");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor2 = (HtmlAnchor)this.grdOne.Items[j].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor2.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO1 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO1.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str2 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str2.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 2:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwo.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label1 = this.lblHeadTwo;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label1.Text = string.Concat(str);
                                this.grdTwo.DataSource = dataTable1;
                                this.grdTwo.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwo);
                                for (int k = 0; k < this.grdTwo.Items.Count; k++)
                                {
                                    text = (DropDownList)this.grdTwo.Items[k].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwo.Items[k].Cells[17].Text;
                                    string text2 = this.grdTwo.Items[k].Cells[8].Text;
                                    if (this.grdTwo.Items[k].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor3 = (HtmlAnchor)this.grdTwo.Items[k].Cells[30].FindControl("lnkView");
                                        htmlAnchor3.Visible = false;
                                    }
                                    if (text2.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text2.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwo.Items[k].Cells[30].FindControl("btnLinktwo");
                                            //linkButton1 = (LinkButton)this.grdTwo.Items[k].Cells[30].FindControl("lnkScanTwo");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor4 = (HtmlAnchor)this.grdTwo.Items[k].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor4.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwo.Items[k].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwo.Items[k].Cells[30].FindControl("btnLinktwo");
                                        //linkButton1 = (LinkButton)this.grdTwo.Items[k].Cells[30].FindControl("lnkScanTwo");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor5 = (HtmlAnchor)this.grdTwo.Items[k].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor5.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO2 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO2.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str3 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str3.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO3 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO3.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 3:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThree.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label2 = this.lblHeadThree;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label2.Text = string.Concat(str);
                                this.grdThree.DataSource = dataTable1;
                                this.grdThree.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThree);
                                for (int l = 0; l < this.grdThree.Items.Count; l++)
                                {
                                    text = (DropDownList)this.grdThree.Items[l].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThree.Items[l].Cells[17].Text;
                                    string text3 = this.grdThree.Items[l].Cells[8].Text;
                                    if (this.grdThree.Items[l].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor6 = (HtmlAnchor)this.grdThree.Items[l].Cells[30].FindControl("lnkView");
                                        htmlAnchor6.Visible = false;
                                    }
                                    if (text3.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text3.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThree.Items[l].Cells[30].FindControl("btnLinkthree");
                                            //linkButton1 = (LinkButton)this.grdThree.Items[l].Cells[30].FindControl("lnkScanThree");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor7 = (HtmlAnchor)this.grdThree.Items[l].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor7.Visible = false;
                                        }
                                    }
                                    else if (this.grdThree.Items[l].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThree.Items[l].Cells[30].FindControl("btnLinkthree");
                                        //linkButton1 = (LinkButton)this.grdThree.Items[l].Cells[30].FindControl("lnkScanThree");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor8 = (HtmlAnchor)this.grdThree.Items[l].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor8.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO4 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO4.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str4 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str4.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str5 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str5.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 4:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlFour.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label3 = this.lblHeadFour;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label3.Text = string.Concat(str);
                                this.grdFour.DataSource = dataTable1;
                                this.grdFour.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdFour);
                                for (int m = 0; m < this.grdFour.Items.Count; m++)
                                {
                                    text = (DropDownList)this.grdFour.Items[m].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdFour.Items[m].Cells[17].Text;
                                    if (this.grdFour.Items[m].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor9 = (HtmlAnchor)this.grdFour.Items[m].Cells[30].FindControl("lnkView");
                                        htmlAnchor9.Visible = false;
                                    }
                                    string text4 = this.grdFour.Items[m].Cells[8].Text;
                                    if (text4.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text4.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdFour.Items[m].Cells[30].FindControl("btnLinkfour");
                                            //linkButton1 = (LinkButton)this.grdFour.Items[m].Cells[30].FindControl("lnkScanFour");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor10 = (HtmlAnchor)this.grdFour.Items[m].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor10.Visible = false;
                                        }
                                    }
                                    else if (this.grdFour.Items[m].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdFour.Items[m].Cells[30].FindControl("btnLinkfour");
                                        //linkButton1 = (LinkButton)this.grdFour.Items[m].Cells[30].FindControl("lnkScanFour");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor11 = (HtmlAnchor)this.grdFour.Items[m].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor11.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO5 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO5.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str6 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str6.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO6 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO6.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 5:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlFive.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label4 = this.lblHeadFive;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label4.Text = string.Concat(str);
                                this.grdFive.DataSource = dataTable1;
                                this.grdFive.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdFive);
                                for (int n = 0; n < this.grdFive.Items.Count; n++)
                                {
                                    text = (DropDownList)this.grdFive.Items[n].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdFive.Items[n].Cells[17].Text;
                                    if (this.grdFive.Items[n].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor12 = (HtmlAnchor)this.grdFive.Items[n].Cells[30].FindControl("lnkView");
                                        htmlAnchor12.Visible = false;
                                    }
                                    string text5 = this.grdFive.Items[n].Cells[8].Text;
                                    if (text5.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text5.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdFive.Items[n].Cells[30].FindControl("btnLinkfive");
                                            //linkButton1 = (LinkButton)this.grdFive.Items[n].Cells[30].FindControl("lnkScanFive");
                                            //HtmlAnchor htmlAnchor13 = (HtmlAnchor)this.grdFive.Items[n].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor13.Visible = false;
                                        }
                                    }
                                    else if (this.grdFive.Items[n].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdFive.Items[n].Cells[30].FindControl("btnLinkfive");
                                        //linkButton1 = (LinkButton)this.grdFive.Items[n].Cells[30].FindControl("lnkScanFive");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor14 = (HtmlAnchor)this.grdFive.Items[n].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor14.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO7 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO7.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str7 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str7.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str8 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str8.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 6:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlSix.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label5 = this.lblHeadSix;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label5.Text = string.Concat(str);
                                this.grdSix.DataSource = dataTable1;
                                this.grdSix.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdSix);
                                for (int o = 0; o < this.grdSix.Items.Count; o++)
                                {
                                    text = (DropDownList)this.grdSix.Items[o].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdSix.Items[o].Cells[17].Text;
                                    if (this.grdSix.Items[o].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor15 = (HtmlAnchor)this.grdSix.Items[o].Cells[30].FindControl("lnkView");
                                        htmlAnchor15.Visible = false;
                                    }
                                    string text6 = this.grdSix.Items[o].Cells[8].Text;
                                    if (text6.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text6.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdSix.Items[o].Cells[30].FindControl("btnLinksix");
                                            //linkButton1 = (LinkButton)this.grdSix.Items[o].Cells[30].FindControl("lnkScanSix");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor16 = (HtmlAnchor)this.grdSix.Items[o].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor16.Visible = false;
                                        }
                                    }
                                    else if (this.grdSix.Items[o].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdSix.Items[o].Cells[30].FindControl("btnLinksix");
                                        //linkButton1 = (LinkButton)this.grdSix.Items[o].Cells[30].FindControl("lnkScanSix");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor17 = (HtmlAnchor)this.grdSix.Items[o].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor17.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO8 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO8.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str9 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str9.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO9 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO9.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 7:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlSeven.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label6 = this.lblHeadSeven;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label6.Text = string.Concat(str);
                                this.grdSeven.DataSource = dataTable1;
                                this.grdSeven.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdSeven);
                                for (int p = 0; p < this.grdSeven.Items.Count; p++)
                                {
                                    text = (DropDownList)this.grdSeven.Items[p].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdSeven.Items[p].Cells[17].Text;
                                    if (this.grdSeven.Items[p].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor18 = (HtmlAnchor)this.grdSeven.Items[p].Cells[30].FindControl("lnkView");
                                        htmlAnchor18.Visible = false;
                                    }
                                    string text7 = this.grdSeven.Items[p].Cells[8].Text;
                                    if (text7.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text7.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdSeven.Items[p].Cells[30].FindControl("btnLinkseven");
                                            //linkButton1 = (LinkButton)this.grdSeven.Items[p].Cells[30].FindControl("lnkScanSeven");
                                            //HtmlAnchor htmlAnchor19 = (HtmlAnchor)this.grdSeven.Items[p].Cells[30].FindControl("lnkframePatient");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //htmlAnchor19.Visible = false;
                                        }
                                    }
                                    else if (this.grdSeven.Items[p].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdSeven.Items[p].Cells[30].FindControl("btnLinkseven");
                                        //linkButton1 = (LinkButton)this.grdSeven.Items[p].Cells[30].FindControl("lnkScanSeven");
                                        //HtmlAnchor htmlAnchor20 = (HtmlAnchor)this.grdSeven.Items[p].Cells[30].FindControl("lnkframePatient");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //htmlAnchor20.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO10 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO10.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str10 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str10.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str11 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str11.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 8:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlEight.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label7 = this.lblHeadEight;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label7.Text = string.Concat(str);
                                this.grdEight.DataSource = dataTable1;
                                this.grdEight.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdEight);
                                for (int q = 0; q < this.grdEight.Items.Count; q++)
                                {
                                    text = (DropDownList)this.grdEight.Items[q].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdEight.Items[q].Cells[17].Text;
                                    if (this.grdEight.Items[q].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor21 = (HtmlAnchor)this.grdEight.Items[q].Cells[30].FindControl("lnkView");
                                        htmlAnchor21.Visible = false;
                                    }
                                    string text8 = this.grdEight.Items[q].Cells[8].Text;
                                    if (text8.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text8.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdEight.Items[q].Cells[30].FindControl("btnLinkeight");
                                            //linkButton1 = (LinkButton)this.grdEight.Items[q].Cells[30].FindControl("lnkScanEight");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor22 = (HtmlAnchor)this.grdEight.Items[q].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor22.Visible = false;
                                        }
                                    }
                                    else if (this.grdEight.Items[q].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdEight.Items[q].Cells[30].FindControl("btnLinkeight");
                                        //linkButton1 = (LinkButton)this.grdEight.Items[q].Cells[30].FindControl("lnkScanEight");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor23 = (HtmlAnchor)this.grdEight.Items[q].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor23.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO11 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO11.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str12 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str12.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO12 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO12.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 9:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlNine.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label8 = this.lblHeadNine;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label8.Text = string.Concat(str);
                                this.grdNine.DataSource = dataTable1;
                                this.grdNine.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdNine);
                                for (int r = 0; r < this.grdNine.Items.Count; r++)
                                {
                                    text = (DropDownList)this.grdNine.Items[r].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdNine.Items[r].Cells[17].Text;
                                    if (this.grdNine.Items[r].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor24 = (HtmlAnchor)this.grdNine.Items[r].Cells[30].FindControl("lnkView");
                                        htmlAnchor24.Visible = false;
                                    }
                                    string text9 = this.grdNine.Items[r].Cells[8].Text;
                                    if (text9.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text9.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdNine.Items[r].Cells[30].FindControl("btnLinknine");
                                            //linkButton1 = (LinkButton)this.grdNine.Items[r].Cells[30].FindControl("lnkScanNine");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor25 = (HtmlAnchor)this.grdNine.Items[r].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor25.Visible = false;
                                        }
                                    }
                                    else if (this.grdNine.Items[r].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdNine.Items[r].Cells[30].FindControl("btnLinknine");
                                        //linkButton1 = (LinkButton)this.grdNine.Items[r].Cells[30].FindControl("lnkScanNine");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor26 = (HtmlAnchor)this.grdNine.Items[r].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor26.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO13 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO13.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str13 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str13.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str14 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str14.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 10:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label9 = this.lblHeadTen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label9.Text = string.Concat(str);
                                this.grdTen.DataSource = dataTable1;
                                this.grdTen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTen);
                                for (int s = 0; s < this.grdTen.Items.Count; s++)
                                {
                                    text = (DropDownList)this.grdTen.Items[s].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTen.Items[s].Cells[17].Text;
                                    if (this.grdTen.Items[s].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor27 = (HtmlAnchor)this.grdTen.Items[s].Cells[30].FindControl("lnkView");
                                        htmlAnchor27.Visible = false;
                                    }
                                    string text10 = this.grdTen.Items[s].Cells[8].Text;
                                    if (text10.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text10.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTen.Items[s].Cells[30].FindControl("btnLinkten");
                                            //linkButton1 = (LinkButton)this.grdTen.Items[s].Cells[30].FindControl("lnkScanTen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor28 = (HtmlAnchor)this.grdTen.Items[s].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor28.Visible = false;
                                        }
                                    }
                                    else if (this.grdTen.Items[s].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTen.Items[s].Cells[30].FindControl("btnLinkten");
                                        //linkButton1 = (LinkButton)this.grdTen.Items[s].Cells[30].FindControl("lnkScanTen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor29 = (HtmlAnchor)this.grdTen.Items[s].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor29.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO14 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO14.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str15 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str15.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO15 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO15.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 11:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlEleven.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label10 = this.lblHeadEleven;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label10.Text = string.Concat(str);
                                this.grdEleven.DataSource = dataTable1;
                                this.grdEleven.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdEleven);
                                for (int t = 0; t < this.grdEleven.Items.Count; t++)
                                {
                                    text = (DropDownList)this.grdEleven.Items[t].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdEleven.Items[t].Cells[17].Text;
                                    if (this.grdEleven.Items[t].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor30 = (HtmlAnchor)this.grdEleven.Items[t].Cells[30].FindControl("lnkView");
                                        htmlAnchor30.Visible = false;
                                    }
                                    string text11 = this.grdEleven.Items[t].Cells[8].Text;
                                    if (text11.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text11.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdEleven.Items[t].Cells[30].FindControl("btnLinkeleven");
                                            //linkButton1 = (LinkButton)this.grdEleven.Items[t].Cells[30].FindControl("lnkScanEleven");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor31 = (HtmlAnchor)this.grdEleven.Items[t].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor31.Visible = false;
                                        }
                                    }
                                    else if (this.grdEleven.Items[t].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdEleven.Items[t].Cells[30].FindControl("btnLinkeleven");
                                        //linkButton1 = (LinkButton)this.grdEleven.Items[t].Cells[30].FindControl("lnkScanEleven");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor32 = (HtmlAnchor)this.grdEleven.Items[t].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor32.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO16 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO16.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str16 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str16.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str17 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str17.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 12:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwelve.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label11 = this.lblHeadTwelve;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label11.Text = string.Concat(str);
                                this.grdTwelve.DataSource = dataTable1;
                                this.grdTwelve.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwelve);
                                for (int u = 0; u < this.grdTwelve.Items.Count; u++)
                                {
                                    text = (DropDownList)this.grdTwelve.Items[u].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwelve.Items[u].Cells[17].Text;
                                    if (this.grdTwelve.Items[u].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor33 = (HtmlAnchor)this.grdTwelve.Items[u].Cells[30].FindControl("lnkView");
                                        htmlAnchor33.Visible = false;
                                    }
                                    string text12 = this.grdTwelve.Items[u].Cells[8].Text;
                                    if (text12.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text12.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwelve.Items[u].Cells[30].FindControl("btnLinkTwelve");
                                            //linkButton1 = (LinkButton)this.grdTwelve.Items[u].Cells[30].FindControl("lnkScanTwelve");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor34 = (HtmlAnchor)this.grdTwelve.Items[u].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor34.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwelve.Items[u].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwelve.Items[u].Cells[30].FindControl("btnLinkTwelve");
                                        //linkButton1 = (LinkButton)this.grdTwelve.Items[u].Cells[30].FindControl("lnkScanTwelve");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor35 = (HtmlAnchor)this.grdTwelve.Items[u].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor35.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO17 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO17.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str18 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str18.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO18 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO18.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 13:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThirteen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label12 = this.lblHeadThirteen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label12.Text = string.Concat(str);
                                this.grdThirteen.DataSource = dataTable1;
                                this.grdThirteen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThirteen);
                                for (int v = 0; v < this.grdThirteen.Items.Count; v++)
                                {
                                    text = (DropDownList)this.grdThirteen.Items[v].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThirteen.Items[v].Cells[17].Text;
                                    if (this.grdThirteen.Items[v].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor36 = (HtmlAnchor)this.grdThirteen.Items[v].Cells[30].FindControl("lnkView");
                                        htmlAnchor36.Visible = false;
                                    }
                                    string text13 = this.grdThirteen.Items[v].Cells[8].Text;
                                    if (text13.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text13.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThirteen.Items[v].Cells[30].FindControl("btnLinkThirteen");
                                            //linkButton1 = (LinkButton)this.grdThirteen.Items[v].Cells[30].FindControl("lnkScanThirteen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor37 = (HtmlAnchor)this.grdThirteen.Items[v].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor37.Visible = false;
                                        }
                                    }
                                    else if (this.grdThirteen.Items[v].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThirteen.Items[v].Cells[30].FindControl("btnLinkThirteen");
                                        //linkButton1 = (LinkButton)this.grdThirteen.Items[v].Cells[30].FindControl("lnkScanThirteen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor38 = (HtmlAnchor)this.grdThirteen.Items[v].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor38.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO19 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO19.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str19 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str19.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str20 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str20.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 14:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlFourteen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label13 = this.lblHeadFourteen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label13.Text = string.Concat(str);
                                this.grdFourteen.DataSource = dataTable1;
                                this.grdFourteen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdFourteen);
                                for (int w = 0; w < this.grdFourteen.Items.Count; w++)
                                {
                                    text = (DropDownList)this.grdFourteen.Items[w].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdFourteen.Items[w].Cells[17].Text;
                                    if (this.grdFourteen.Items[w].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor39 = (HtmlAnchor)this.grdFourteen.Items[w].Cells[30].FindControl("lnkView");
                                        htmlAnchor39.Visible = false;
                                    }
                                    string text14 = this.grdFourteen.Items[w].Cells[8].Text;
                                    if (text14.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text14.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdFourteen.Items[w].Cells[30].FindControl("btnLinkFourteen");
                                            //linkButton1 = (LinkButton)this.grdFourteen.Items[w].Cells[30].FindControl("btnLinkFourteen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor40 = (HtmlAnchor)this.grdFourteen.Items[w].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor40.Visible = false;
                                        }
                                    }
                                    else if (this.grdFourteen.Items[w].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdFourteen.Items[w].Cells[30].FindControl("btnLinkFourteen");
                                        //linkButton1 = (LinkButton)this.grdFourteen.Items[w].Cells[30].FindControl("btnLinkFourteen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor41 = (HtmlAnchor)this.grdFourteen.Items[w].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor41.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO20 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO20.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str21 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str21.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO21 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO21.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 15:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlFifteen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label14 = this.lblHeadFifteen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label14.Text = string.Concat(str);
                                this.grdFifteen.DataSource = dataTable1;
                                this.grdFifteen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdFifteen);
                                for (int x = 0; x < this.grdFifteen.Items.Count; x++)
                                {
                                    text = (DropDownList)this.grdFifteen.Items[x].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdFifteen.Items[x].Cells[17].Text;
                                    if (this.grdFifteen.Items[x].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor42 = (HtmlAnchor)this.grdFifteen.Items[x].Cells[30].FindControl("lnkView");
                                        htmlAnchor42.Visible = false;
                                    }
                                    string text15 = this.grdFifteen.Items[x].Cells[8].Text;
                                    if (text15.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text15.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdFifteen.Items[x].Cells[30].FindControl("btnLinkFifteen");
                                            //linkButton1 = (LinkButton)this.grdFifteen.Items[x].Cells[30].FindControl("lnkScanFifteen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor43 = (HtmlAnchor)this.grdFifteen.Items[x].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor43.Visible = false;
                                        }
                                    }
                                    else if (this.grdFifteen.Items[x].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdFifteen.Items[x].Cells[30].FindControl("btnLinkFifteen");
                                        //linkButton1 = (LinkButton)this.grdFifteen.Items[x].Cells[30].FindControl("lnkScanFifteen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor44 = (HtmlAnchor)this.grdFifteen.Items[x].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor44.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO22 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO22.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str22 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str22.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str23 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str23.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 16:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlSixteen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label15 = this.lblHeadSixteen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label15.Text = string.Concat(str);
                                this.grdSixteen.DataSource = dataTable1;
                                this.grdSixteen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdSixteen);
                                for (int y = 0; y < this.grdSixteen.Items.Count; y++)
                                {
                                    text = (DropDownList)this.grdSixteen.Items[y].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdSixteen.Items[y].Cells[17].Text;
                                    if (this.grdSixteen.Items[y].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor45 = (HtmlAnchor)this.grdSixteen.Items[y].Cells[30].FindControl("lnkView");
                                        htmlAnchor45.Visible = false;
                                    }
                                    string text16 = this.grdSixteen.Items[y].Cells[8].Text;
                                    if (text16.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text16.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdSixteen.Items[y].Cells[30].FindControl("btnLinksixteen");
                                            //linkButton1 = (LinkButton)this.grdSixteen.Items[y].Cells[30].FindControl("lnkScanSixteen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor46 = (HtmlAnchor)this.grdSixteen.Items[y].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor46.Visible = false;
                                        }
                                    }
                                    else if (this.grdSixteen.Items[y].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdSixteen.Items[y].Cells[30].FindControl("btnLinksixteen");
                                        //linkButton1 = (LinkButton)this.grdSixteen.Items[y].Cells[30].FindControl("lnkScanSixteen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor47 = (HtmlAnchor)this.grdSixteen.Items[y].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor47.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO23 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO23.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str24 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str24.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO24 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO24.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 17:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlSeventeen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label16 = this.lblHeadSeventeen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label16.Text = string.Concat(str);
                                this.grdSeventeen.DataSource = dataTable1;
                                this.grdSeventeen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdSeventeen);
                                for (int a = 0; a < this.grdSeventeen.Items.Count; a++)
                                {
                                    text = (DropDownList)this.grdSeventeen.Items[a].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdSeventeen.Items[a].Cells[17].Text;
                                    if (this.grdSeventeen.Items[a].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor48 = (HtmlAnchor)this.grdSeventeen.Items[a].Cells[30].FindControl("lnkView");
                                        htmlAnchor48.Visible = false;
                                    }
                                    string text17 = this.grdSeventeen.Items[a].Cells[8].Text;
                                    if (text17.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text17.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdSeventeen.Items[a].Cells[30].FindControl("btnLinkseventeen");
                                            //linkButton1 = (LinkButton)this.grdSeventeen.Items[a].Cells[30].FindControl("lnkScanSevenTeen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor49 = (HtmlAnchor)this.grdSeventeen.Items[a].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor49.Visible = false;
                                        }
                                    }
                                    else if (this.grdSeventeen.Items[a].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdSeventeen.Items[a].Cells[30].FindControl("btnLinkseventeen");
                                        //linkButton1 = (LinkButton)this.grdSeventeen.Items[a].Cells[30].FindControl("lnkScanSevenTeen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor50 = (HtmlAnchor)this.grdSeventeen.Items[a].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor50.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO25 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO25.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str25 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str25.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str26 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str26.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 18:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlEighteen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label17 = this.lblHeadEighteen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label17.Text = string.Concat(str);
                                this.grdEighteen.DataSource = dataTable1;
                                this.grdEighteen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdEighteen);
                                for (int b = 0; b < this.grdEighteen.Items.Count; b++)
                                {
                                    text = (DropDownList)this.grdEighteen.Items[b].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdEighteen.Items[b].Cells[17].Text;
                                    if (this.grdEighteen.Items[b].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor51 = (HtmlAnchor)this.grdEighteen.Items[b].Cells[30].FindControl("lnkView");
                                        htmlAnchor51.Visible = false;
                                    }
                                    string text18 = this.grdEighteen.Items[b].Cells[8].Text;
                                    if (text18.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text18.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdEighteen.Items[b].Cells[30].FindControl("btnLinkeighteen");
                                            //linkButton1 = (LinkButton)this.grdEighteen.Items[b].Cells[30].FindControl("lnkScaneighteen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor52 = (HtmlAnchor)this.grdEighteen.Items[b].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor52.Visible = false;
                                        }
                                    }
                                    else if (this.grdEighteen.Items[b].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdEighteen.Items[b].Cells[30].FindControl("btnLinkeighteen");
                                        //linkButton1 = (LinkButton)this.grdEighteen.Items[b].Cells[30].FindControl("lnkScaneighteen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor53 = (HtmlAnchor)this.grdEighteen.Items[b].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor53.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO26 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO26.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str27 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str27.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO27 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO27.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 19:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlNineteen.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label18 = this.lblHeadNineteen;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label18.Text = string.Concat(str);
                                this.grdNineteen.DataSource = dataTable1;
                                this.grdNineteen.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdNineteen);
                                for (int c = 0; c < this.grdNineteen.Items.Count; c++)
                                {
                                    text = (DropDownList)this.grdNineteen.Items[c].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdNineteen.Items[c].Cells[17].Text;
                                    if (this.grdNineteen.Items[c].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor54 = (HtmlAnchor)this.grdNineteen.Items[c].Cells[30].FindControl("lnkView");
                                        htmlAnchor54.Visible = false;
                                    }
                                    string text19 = this.grdNineteen.Items[c].Cells[8].Text;
                                    if (text19.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text19.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdNineteen.Items[c].Cells[30].FindControl("btnLinknineteen");
                                            //linkButton1 = (LinkButton)this.grdNineteen.Items[c].Cells[30].FindControl("lnkScanNineteen");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor55 = (HtmlAnchor)this.grdNineteen.Items[c].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor55.Visible = false;
                                        }
                                    }
                                    else if (this.grdNineteen.Items[c].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdNineteen.Items[c].Cells[30].FindControl("btnLinknineteen");
                                        //linkButton1 = (LinkButton)this.grdNineteen.Items[c].Cells[30].FindControl("lnkScanNineteen");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor56 = (HtmlAnchor)this.grdNineteen.Items[c].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor56.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO28 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO28.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str28 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str28.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str29 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str29.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 20:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwenty.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label19 = this.lblHeadTwenty;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label19.Text = string.Concat(str);
                                this.grdTwenty.DataSource = dataTable1;
                                this.grdTwenty.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwenty);
                                for (int d = 0; d < this.grdTwenty.Items.Count; d++)
                                {
                                    text = (DropDownList)this.grdTwenty.Items[d].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwenty.Items[d].Cells[17].Text;
                                    if (this.grdTwenty.Items[d].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor57 = (HtmlAnchor)this.grdTwenty.Items[d].Cells[30].FindControl("lnkView");
                                        htmlAnchor57.Visible = false;
                                    }
                                    string text20 = this.grdTwenty.Items[d].Cells[8].Text;
                                    if (text20.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text20.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwenty.Items[d].Cells[30].FindControl("btnLinkTwenty");
                                            //linkButton1 = (LinkButton)this.grdTwenty.Items[d].Cells[30].FindControl("lnkScanTwenty");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor58 = (HtmlAnchor)this.grdTwenty.Items[d].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor58.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwenty.Items[d].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwenty.Items[d].Cells[30].FindControl("btnLinkTwenty");
                                        //linkButton1 = (LinkButton)this.grdTwenty.Items[d].Cells[30].FindControl("lnkScanTwenty");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor59 = (HtmlAnchor)this.grdTwenty.Items[d].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor59.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO29 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO29.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str30 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str30.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO30 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO30.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 21:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentyOne.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label20 = this.lblHeadTwentyOne;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label20.Text = string.Concat(str);
                                this.grdTwentyOne.DataSource = dataTable1;
                                this.grdTwentyOne.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentyOne);
                                for (int e = 0; e < this.grdTwentyOne.Items.Count; e++)
                                {
                                    text = (DropDownList)this.grdTwentyOne.Items[e].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentyOne.Items[e].Cells[17].Text;
                                    if (this.grdTwentyOne.Items[e].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor60 = (HtmlAnchor)this.grdTwentyOne.Items[e].Cells[30].FindControl("lnkView");
                                        htmlAnchor60.Visible = false;
                                    }
                                    string text21 = this.grdTwentyOne.Items[e].Cells[8].Text;
                                    if (text21.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text21.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentyOne.Items[e].Cells[30].FindControl("btnLinkTwentyone");
                                            //linkButton1 = (LinkButton)this.grdTwentyOne.Items[e].Cells[30].FindControl("lnkScanTwentyOne");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor61 = (HtmlAnchor)this.grdTwentyOne.Items[e].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor61.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentyOne.Items[e].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentyOne.Items[e].Cells[30].FindControl("btnLinkTwentyone");
                                        //linkButton1 = (LinkButton)this.grdTwentyOne.Items[e].Cells[30].FindControl("lnkScanTwentyOne");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor62 = (HtmlAnchor)this.grdTwentyOne.Items[e].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor62.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO31 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO31.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str31 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str31.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str32 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str32.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 22:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentyTwo.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label21 = this.lblHeadTwentyTwo;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label21.Text = string.Concat(str);
                                this.grdTwentyTwo.DataSource = dataTable1;
                                this.grdTwentyTwo.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentyTwo);
                                for (int f = 0; f < this.grdTwentyTwo.Items.Count; f++)
                                {
                                    text = (DropDownList)this.grdTwentyTwo.Items[f].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentyTwo.Items[f].Cells[17].Text;
                                    if (this.grdTwentyTwo.Items[f].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor63 = (HtmlAnchor)this.grdTwentyTwo.Items[f].Cells[30].FindControl("lnkView");
                                        htmlAnchor63.Visible = false;
                                    }
                                    string text22 = this.grdTwentyTwo.Items[f].Cells[8].Text;
                                    if (text22.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text22.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentyTwo.Items[f].Cells[30].FindControl("btnLinkTwentytwo");
                                            //linkButton1 = (LinkButton)this.grdTwentyTwo.Items[f].Cells[30].FindControl("lnkScanTwentyTwo");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor64 = (HtmlAnchor)this.grdTwentyTwo.Items[f].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor64.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentyTwo.Items[f].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentyTwo.Items[f].Cells[30].FindControl("btnLinkTwentytwo");
                                        //linkButton1 = (LinkButton)this.grdTwentyTwo.Items[f].Cells[30].FindControl("lnkScanTwentyTwo");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor65 = (HtmlAnchor)this.grdTwentyTwo.Items[f].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor65.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO32 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO32.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str33 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str33.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO33 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO33.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 23:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentyThree.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label22 = this.lblHeadTwentyThree;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label22.Text = string.Concat(str);
                                this.grdTwentyThree.DataSource = dataTable1;
                                this.grdTwentyThree.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentyThree);
                                for (int g = 0; g < this.grdTwentyThree.Items.Count; g++)
                                {
                                    text = (DropDownList)this.grdTwentyThree.Items[g].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentyThree.Items[g].Cells[17].Text;
                                    if (this.grdTwentyThree.Items[g].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor66 = (HtmlAnchor)this.grdTwentyThree.Items[g].Cells[30].FindControl("lnkView");
                                        htmlAnchor66.Visible = false;
                                    }
                                    string text23 = this.grdTwentyThree.Items[g].Cells[8].Text;
                                    if (text23.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text23.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentyThree.Items[g].Cells[30].FindControl("btnLinkTwentythree");
                                            //linkButton1 = (LinkButton)this.grdTwentyThree.Items[g].Cells[30].FindControl("lnkScanTwentyThree");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor67 = (HtmlAnchor)this.grdTwentyThree.Items[g].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor67.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentyThree.Items[g].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentyThree.Items[g].Cells[30].FindControl("btnLinkTwentythree");
                                        //linkButton1 = (LinkButton)this.grdTwentyThree.Items[g].Cells[30].FindControl("lnkScanTwentyThree");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor68 = (HtmlAnchor)this.grdTwentyThree.Items[g].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor68.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO34 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO34.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str34 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str34.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str35 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str35.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 24:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentyFour.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label23 = this.lblHeadTwentyFour;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label23.Text = string.Concat(str);
                                this.grdTwentyFour.DataSource = dataTable1;
                                this.grdTwentyFour.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentyFour);
                                for (int h = 0; h < this.grdTwentyFour.Items.Count; h++)
                                {
                                    text = (DropDownList)this.grdTwentyFour.Items[h].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentyFour.Items[h].Cells[17].Text;
                                    if (this.grdTwentyFour.Items[h].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor69 = (HtmlAnchor)this.grdTwentyFour.Items[h].Cells[30].FindControl("lnkView");
                                        htmlAnchor69.Visible = false;
                                    }
                                    string text24 = this.grdTwentyFour.Items[h].Cells[8].Text;
                                    if (text24.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text24.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentyFour.Items[h].Cells[30].FindControl("btnLinkTwentyfour");
                                            //linkButton1 = (LinkButton)this.grdTwentyFour.Items[h].Cells[30].FindControl("lnkScanTwentyFour");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor70 = (HtmlAnchor)this.grdTwentyFour.Items[h].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor70.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentyFour.Items[h].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentyFour.Items[h].Cells[30].FindControl("btnLinkTwentyfour");
                                        //linkButton1 = (LinkButton)this.grdTwentyFour.Items[h].Cells[30].FindControl("lnkScanTwentyFour");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor71 = (HtmlAnchor)this.grdTwentyFour.Items[h].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor71.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO35 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO35.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str36 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str36.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO36 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO36.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 25:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentyFive.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label24 = this.lblHeadTwentyFive;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label24.Text = string.Concat(str);
                                this.grdTwentyFive.DataSource = dataTable1;
                                this.grdTwentyFive.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentyFive);
                                for (int i1 = 0; i1 < this.grdTwentyFive.Items.Count; i1++)
                                {
                                    text = (DropDownList)this.grdTwentyFive.Items[i1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentyFive.Items[i1].Cells[17].Text;
                                    if (this.grdTwentyFive.Items[i1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor72 = (HtmlAnchor)this.grdTwentyFive.Items[i1].Cells[30].FindControl("lnkView");
                                        htmlAnchor72.Visible = false;
                                    }
                                    string text25 = this.grdTwentyFive.Items[i1].Cells[8].Text;
                                    if (text25.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text25.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentyFive.Items[i1].Cells[30].FindControl("btnLinkTwentyfive");
                                            //linkButton1 = (LinkButton)this.grdTwentyFive.Items[i1].Cells[30].FindControl("lnkScanTwentyFive");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor73 = (HtmlAnchor)this.grdTwentyFive.Items[i1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor73.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentyFive.Items[i1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentyFive.Items[i1].Cells[30].FindControl("btnLinkTwentyfive");
                                        //linkButton1 = (LinkButton)this.grdTwentyFive.Items[i1].Cells[30].FindControl("lnkScanTwentyFive");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor74 = (HtmlAnchor)this.grdTwentyFive.Items[i1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor74.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO37 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO37.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str37 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str37.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str38 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str38.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 26:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentySix.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label25 = this.lblHeadTwentySix;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label25.Text = string.Concat(str);
                                this.grdTwentySix.DataSource = dataTable1;
                                this.grdTwentySix.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentySix);
                                for (int j1 = 0; j1 < this.grdTwentySix.Items.Count; j1++)
                                {
                                    text = (DropDownList)this.grdTwentySix.Items[j1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentySix.Items[j1].Cells[17].Text;
                                    if (this.grdTwentySix.Items[j1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor75 = (HtmlAnchor)this.grdTwentySix.Items[j1].Cells[30].FindControl("lnkView");
                                        htmlAnchor75.Visible = false;
                                    }
                                    string text26 = this.grdTwentySix.Items[j1].Cells[8].Text;
                                    if (text26.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text26.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentySix.Items[j1].Cells[30].FindControl("btnLinkTwentysix");
                                            //linkButton1 = (LinkButton)this.grdTwentySix.Items[j1].Cells[30].FindControl("lnkScanTwentySix");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor76 = (HtmlAnchor)this.grdTwentySix.Items[j1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor76.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentySix.Items[j1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentySix.Items[j1].Cells[30].FindControl("btnLinkTwentysix");
                                        //linkButton1 = (LinkButton)this.grdTwentySix.Items[j1].Cells[30].FindControl("lnkScanTwentySix");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor77 = (HtmlAnchor)this.grdTwentySix.Items[j1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor77.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO38 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO38.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str39 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str39.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO39 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO39.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 27:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentySeven.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label26 = this.lblHeadTwentySeven;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label26.Text = string.Concat(str);
                                this.grdTwentySeven.DataSource = dataTable1;
                                this.grdTwentySeven.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentySeven);
                                for (int k1 = 0; k1 < this.grdTwentySeven.Items.Count; k1++)
                                {
                                    text = (DropDownList)this.grdTwentySeven.Items[k1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentySeven.Items[k1].Cells[17].Text;
                                    if (this.grdTwentySeven.Items[k1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor78 = (HtmlAnchor)this.grdTwentySeven.Items[k1].Cells[30].FindControl("lnkView");
                                        htmlAnchor78.Visible = false;
                                    }
                                    string text27 = this.grdTwentySeven.Items[k1].Cells[8].Text;
                                    if (text27.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text27.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentySeven.Items[k1].Cells[30].FindControl("btnLinkTwentyseven");
                                            //linkButton1 = (LinkButton)this.grdTwentySeven.Items[k1].Cells[30].FindControl("lnkScanTwentySeven");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor79 = (HtmlAnchor)this.grdTwentySeven.Items[k1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor79.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentySeven.Items[k1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentySeven.Items[k1].Cells[30].FindControl("btnLinkTwentyseven");
                                        //linkButton1 = (LinkButton)this.grdTwentySeven.Items[k1].Cells[30].FindControl("lnkScanTwentySeven");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor80 = (HtmlAnchor)this.grdTwentySeven.Items[k1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor80.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO40 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO40.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str40 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str40.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str41 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str41.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 28:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentyEight.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label27 = this.lblHeadTwentyEight;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label27.Text = string.Concat(str);
                                this.grdTwentyEight.DataSource = dataTable1;
                                this.grdTwentyEight.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentyEight);
                                for (int l1 = 0; l1 < this.grdTwentyEight.Items.Count; l1++)
                                {
                                    text = (DropDownList)this.grdTwentyEight.Items[l1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentyEight.Items[l1].Cells[17].Text;
                                    if (this.grdTwentyEight.Items[l1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor81 = (HtmlAnchor)this.grdTwentyEight.Items[l1].Cells[30].FindControl("lnkView");
                                        htmlAnchor81.Visible = false;
                                    }
                                    string text28 = this.grdTwentyEight.Items[l1].Cells[8].Text;
                                    if (text28.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text28.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentyEight.Items[l1].Cells[30].FindControl("btnLinkTwentyeight");
                                            //linkButton1 = (LinkButton)this.grdTwentyEight.Items[l1].Cells[30].FindControl("lnkScanTwentyEight");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor82 = (HtmlAnchor)this.grdTwentyEight.Items[l1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor82.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentyEight.Items[l1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentyEight.Items[l1].Cells[30].FindControl("btnLinkTwentyeight");
                                        //linkButton1 = (LinkButton)this.grdTwentyEight.Items[l1].Cells[30].FindControl("lnkScanTwentyEight");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor83 = (HtmlAnchor)this.grdTwentyEight.Items[l1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor83.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO41 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO41.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str42 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str42.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO42 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO42.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 29:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlTwentyNine.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label28 = this.lblHeadTwentyNine;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label28.Text = string.Concat(str);
                                this.grdTwentyNine.DataSource = dataTable1;
                                this.grdTwentyNine.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdTwentyNine);
                                for (int m1 = 0; m1 < this.grdTwentyNine.Items.Count; m1++)
                                {
                                    text = (DropDownList)this.grdTwentyNine.Items[m1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdTwentyNine.Items[m1].Cells[17].Text;
                                    if (this.grdTwentyNine.Items[m1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor84 = (HtmlAnchor)this.grdTwentyNine.Items[m1].Cells[30].FindControl("lnkView");
                                        htmlAnchor84.Visible = false;
                                    }
                                    string text29 = this.grdTwentyNine.Items[m1].Cells[8].Text;
                                    if (text29.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text29.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdTwentyNine.Items[m1].Cells[30].FindControl("btnLinkTwentynine");
                                            //linkButton1 = (LinkButton)this.grdTwentyNine.Items[m1].Cells[30].FindControl("lnkScanTwentyNine");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor85 = (HtmlAnchor)this.grdTwentyNine.Items[m1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor85.Visible = false;
                                        }
                                    }
                                    else if (this.grdTwentyNine.Items[m1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdTwentyNine.Items[m1].Cells[30].FindControl("btnLinkTwentynine");
                                        //linkButton1 = (LinkButton)this.grdTwentyNine.Items[m1].Cells[30].FindControl("lnkScanTwentyNine");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor86 = (HtmlAnchor)this.grdTwentyNine.Items[m1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor86.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO43 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO43.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str43 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str43.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str44 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str44.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 30:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThirty.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label29 = this.lblHeadThirty;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label29.Text = string.Concat(str);
                                this.grdThirty.DataSource = dataTable1;
                                this.grdThirty.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThirty);
                                for (int n1 = 0; n1 < this.grdThirty.Items.Count; n1++)
                                {
                                    text = (DropDownList)this.grdThirty.Items[n1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThirty.Items[n1].Cells[17].Text;
                                    if (this.grdThirty.Items[n1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor87 = (HtmlAnchor)this.grdThirty.Items[n1].Cells[30].FindControl("lnkView");
                                        htmlAnchor87.Visible = false;
                                    }
                                    string text30 = this.grdThirty.Items[n1].Cells[8].Text;
                                    if (text30.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text30.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThirty.Items[n1].Cells[30].FindControl("btnLinkThirty");
                                            //linkButton1 = (LinkButton)this.grdThirty.Items[n1].Cells[30].FindControl("lnkScanThirty");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor88 = (HtmlAnchor)this.grdThirty.Items[n1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor88.Visible = false;
                                        }
                                    }
                                    else if (this.grdThirty.Items[n1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThirty.Items[n1].Cells[30].FindControl("btnLinkThirty");
                                        //linkButton1 = (LinkButton)this.grdThirty.Items[n1].Cells[30].FindControl("lnkScanThirty");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor89 = (HtmlAnchor)this.grdThirty.Items[n1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor89.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO44 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO44.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str45 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str45.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO45 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO45.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 31:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThirtyOne.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label30 = this.lblHeadThirtyOne;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label30.Text = string.Concat(str);
                                this.grdThirtyOne.DataSource = dataTable1;
                                this.grdThirtyOne.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThirtyOne);
                                for (int o1 = 0; o1 < this.grdThirtyOne.Items.Count; o1++)
                                {
                                    text = (DropDownList)this.grdThirtyOne.Items[o1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThirtyOne.Items[o1].Cells[17].Text;
                                    if (this.grdThirtyOne.Items[o1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor90 = (HtmlAnchor)this.grdThirtyOne.Items[o1].Cells[30].FindControl("lnkView");
                                        htmlAnchor90.Visible = false;
                                    }
                                    string text31 = this.grdThirtyOne.Items[o1].Cells[8].Text;
                                    if (text31.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text31.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThirtyOne.Items[o1].Cells[30].FindControl("btnLinkThirtyOne");
                                            //linkButton1 = (LinkButton)this.grdThirtyOne.Items[o1].Cells[30].FindControl("lnkScanThirtyOne");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor91 = (HtmlAnchor)this.grdThirtyOne.Items[o1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor91.Visible = false;
                                        }
                                    }
                                    else if (this.grdThirtyOne.Items[o1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThirtyOne.Items[o1].Cells[30].FindControl("btnLinkThirtyOne");
                                        //linkButton1 = (LinkButton)this.grdThirtyOne.Items[o1].Cells[30].FindControl("lnkScanThirtyOne");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor92 = (HtmlAnchor)this.grdThirtyOne.Items[o1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor92.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO46 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO46.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str46 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str46.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str47 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str47.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 32:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThirtyTwo.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label31 = this.lblHeadThirtyTwo;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label31.Text = string.Concat(str);
                                this.grdThirtyTwo.DataSource = dataTable1;
                                this.grdThirtyTwo.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThirtyTwo);
                                for (int p1 = 0; p1 < this.grdThirtyTwo.Items.Count; p1++)
                                {
                                    text = (DropDownList)this.grdThirtyTwo.Items[p1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThirtyTwo.Items[p1].Cells[17].Text;
                                    if (this.grdThirtyTwo.Items[p1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor93 = (HtmlAnchor)this.grdThirtyTwo.Items[p1].Cells[30].FindControl("lnkView");
                                        htmlAnchor93.Visible = false;
                                    }
                                    string text32 = this.grdThirtyTwo.Items[p1].Cells[8].Text;
                                    if (text32.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text32.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThirtyTwo.Items[p1].Cells[30].FindControl("btnLinkThirtytwo");
                                            //linkButton1 = (LinkButton)this.grdThirtyTwo.Items[p1].Cells[30].FindControl("lnkScanThirtyTwo");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor94 = (HtmlAnchor)this.grdThirtyTwo.Items[p1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor94.Visible = false;
                                        }
                                    }
                                    else if (this.grdThirtyTwo.Items[p1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThirtyTwo.Items[p1].Cells[30].FindControl("btnLinkThirtytwo");
                                        //linkButton1 = (LinkButton)this.grdThirtyTwo.Items[p1].Cells[30].FindControl("lnkScanThirtyTwo");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor95 = (HtmlAnchor)this.grdThirtyTwo.Items[p1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor95.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO47 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO47.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str48 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str48.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO48 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO48.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 33:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThirtyThree.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label32 = this.lblHeadThirtyThree;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label32.Text = string.Concat(str);
                                this.grdThirtyThree.DataSource = dataTable1;
                                this.grdThirtyThree.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThirtyThree);
                                for (int q1 = 0; q1 < this.grdThirtyThree.Items.Count; q1++)
                                {
                                    text = (DropDownList)this.grdThirtyThree.Items[q1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThirtyThree.Items[q1].Cells[17].Text;
                                    if (this.grdThirtyThree.Items[q1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor96 = (HtmlAnchor)this.grdThirtyThree.Items[q1].Cells[30].FindControl("lnkView");
                                        htmlAnchor96.Visible = false;
                                    }
                                    string text33 = this.grdThirtyThree.Items[q1].Cells[8].Text;
                                    if (text33.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text33.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThirtyThree.Items[q1].Cells[30].FindControl("btnLinkThirtythree");
                                            //linkButton1 = (LinkButton)this.grdThirtyThree.Items[q1].Cells[30].FindControl("lnkScanThirtyThree");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor97 = (HtmlAnchor)this.grdThirtyThree.Items[q1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor97.Visible = false;
                                        }
                                    }
                                    else if (this.grdThirtyThree.Items[q1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThirtyThree.Items[q1].Cells[30].FindControl("btnLinkThirtythree");
                                        //linkButton1 = (LinkButton)this.grdThirtyThree.Items[q1].Cells[30].FindControl("lnkScanThirtyThree");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor98 = (HtmlAnchor)this.grdThirtyThree.Items[q1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor98.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO49 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO49.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str49 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str49.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str50 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str50.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 34:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThirtyFour.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label33 = this.lblHeadThirtyFour;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label33.Text = string.Concat(str);
                                this.grdThirtyFour.DataSource = dataTable1;
                                this.grdThirtyFour.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThirtyFour);
                                for (int r1 = 0; r1 < this.grdThirtyFour.Items.Count; r1++)
                                {
                                    text = (DropDownList)this.grdThirtyFour.Items[r1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThirtyFour.Items[r1].Cells[17].Text;
                                    if (this.grdThirtyFour.Items[r1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor99 = (HtmlAnchor)this.grdThirtyFour.Items[r1].Cells[30].FindControl("lnkView");
                                        htmlAnchor99.Visible = false;
                                    }
                                    string text34 = this.grdThirtyFour.Items[r1].Cells[8].Text;
                                    if (text34.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text34.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThirtyFour.Items[r1].Cells[30].FindControl("btnLinkThirtyfour");
                                            //linkButton1 = (LinkButton)this.grdThirtyFour.Items[r1].Cells[30].FindControl("lnkScanThirtyFour");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor100 = (HtmlAnchor)this.grdThirtyFour.Items[r1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor100.Visible = false;
                                        }
                                    }
                                    else if (this.grdThirtyFour.Items[r1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThirtyFour.Items[r1].Cells[30].FindControl("btnLinkThirtyfour");
                                        //linkButton1 = (LinkButton)this.grdThirtyFour.Items[r1].Cells[30].FindControl("lnkScanThirtyFour");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor101 = (HtmlAnchor)this.grdThirtyFour.Items[r1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor101.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO50 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO50.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str51 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str51.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO51 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                calendarDAO51.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        case 35:
                            {
                                dataTable1 = null;
                                dataTable1 = this.dtAllSpecialityEvents.Clone();
                                this.tabpnlThirtyFive.Visible = true;
                                dataRowArray = this.dtAllSpecialityEvents.Select(string.Concat("SPECIALITY='", row[1].ToString(), "'"));
                                for (i = 0; i < (int)dataRowArray.Length; i++)
                                {
                                    dataTable1.ImportRow(dataRowArray[i]);
                                }
                                Label label34 = this.lblHeadThirtyFive;
                                str = new object[] { row[1].ToString(), " (", dataTable1.Rows.Count, ")" };
                                label34.Text = string.Concat(str);
                                this.grdThirtyFive.DataSource = dataTable1;
                                this.grdThirtyFive.DataBind();
                                this.setColumnAccordingScheduleType(ref this.grdThirtyFive);
                                for (int s1 = 0; s1 < this.grdThirtyFive.Items.Count; s1++)
                                {
                                    text = (DropDownList)this.grdThirtyFive.Items[s1].Cells[5].FindControl("ddlStatus");
                                    text.SelectedValue = this.grdThirtyFive.Items[s1].Cells[17].Text;
                                    if (this.grdThirtyFive.Items[s1].Cells[31].Text == "0")
                                    {
                                        HtmlAnchor htmlAnchor102 = (HtmlAnchor)this.grdThirtyFive.Items[s1].Cells[30].FindControl("lnkView");
                                        htmlAnchor102.Visible = false;
                                    }
                                    string text35 = this.grdThirtyFive.Items[s1].Cells[8].Text;
                                    if (text35.Contains("<"))
                                    {
                                        chrArray = new char[] { '<' };
                                        if (text35.Split(chrArray)[0].ToString() == "C")
                                        {
                                            //linkButton = (LinkButton)this.grdThirtyFive.Items[s1].Cells[30].FindControl("btnLinkThirtyfive");
                                            //linkButton1 = (LinkButton)this.grdThirtyFive.Items[s1].Cells[30].FindControl("lnkScanThirtyFive");
                                            //linkButton.Visible = false;
                                            //linkButton1.Visible = false;
                                            //HtmlAnchor htmlAnchor103 = (HtmlAnchor)this.grdThirtyFive.Items[s1].Cells[30].FindControl("lnkframePatient");
                                            //htmlAnchor103.Visible = false;
                                        }
                                    }
                                    else if (this.grdThirtyFive.Items[s1].Cells[8].Text == "C")
                                    {
                                        //linkButton = (LinkButton)this.grdThirtyFive.Items[s1].Cells[30].FindControl("btnLinkThirtyfive");
                                        //linkButton1 = (LinkButton)this.grdThirtyFive.Items[s1].Cells[30].FindControl("lnkScanThirtyFive");
                                        //linkButton.Visible = false;
                                        //linkButton1.Visible = false;
                                        //HtmlAnchor htmlAnchor104 = (HtmlAnchor)this.grdThirtyFive.Items[s1].Cells[30].FindControl("lnkframePatient");
                                        //htmlAnchor104.Visible = false;
                                    }
                                }
                                this.objCalendar = new Bill_SysPatientDesk.Calendar_DAO();
                                Bill_SysPatientDesk.Calendar_DAO calendarDAO52 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                calendarDAO52.ControlIDPrefix = string.Concat("Calendar_", date.ToString("MMM").ToUpper().ToString());
                                Bill_SysPatientDesk.Calendar_DAO str52 = this.objCalendar;
                                date = Convert.ToDateTime(str1);
                                str52.InitialDisplayMonth = date.ToString("MMM").ToUpper().ToString();
                                Bill_SysPatientDesk.Calendar_DAO str53 = this.objCalendar;
                                year = Convert.ToDateTime(str1).Year;
                                str53.InitialDisplayYear = year.ToString();
                                continue;
                            }
                        default:
                            {
                                continue;
                            }
                    }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.tabid.Value == "")
            {
                if (base.Request.QueryString["Status"] != null && base.Request.QueryString["Status"].ToString() == "Report")
                {
                    this.Session["SZ_CASE_ID"] = base.Request.QueryString["case"].ToString();
                    this.Session["Case_ID"] = base.Request.QueryString["case"].ToString();
                    this.Session["QStrCaseID"] = base.Request.QueryString["case"].ToString();
                    this.Session["QStrCID"] = base.Request.QueryString["case"].ToString();
                    this.Session["Archived"] = 0;
                    this.Session["PROVIDERNAME"] = base.Request.QueryString["case"].ToString();
                    CaseDetailsBO caseDetailsBO = new CaseDetailsBO();
                    Bill_Sys_CaseObject billSysCaseObject = new Bill_Sys_CaseObject();
                    billSysCaseObject.SZ_PATIENT_ID = caseDetailsBO.GetCasePatientID(base.Request.QueryString["case"].ToString(), "");
                    billSysCaseObject.SZ_CASE_ID = base.Request.QueryString["case"].ToString();
                    billSysCaseObject.SZ_PATIENT_NAME = base.Request.QueryString["PName"].ToString();
                    billSysCaseObject.SZ_COMAPNY_ID = base.Request.QueryString["cmpid"].ToString();
                    billSysCaseObject.SZ_CASE_NO = base.Request.QueryString["csno"].ToString();
                    this.Session["CASE_OBJECT"] = billSysCaseObject;
                    Bill_Sys_Case billSysCase = new Bill_Sys_Case();
                    billSysCase.SZ_CASE_ID = base.Request.QueryString["case"].ToString();
                    this.Session["CASEINFO"] = billSysCase;
                }
                if (!base.IsPostBack)
                {
                    this.dtVisitType = (new Bill_Sys_Calender()).GET_Visit_Types(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this.Session["VISITTYPE_LIST"] = this.dtVisitType;
                    if (base.Request.QueryString["CaseID"] != null)
                    {
                        this.Session["Case_ID"] = base.Request.QueryString["CaseID"].ToString();
                        this.Session["QStrCaseID"] = base.Request.QueryString["CaseID"].ToString();
                        this.Session["QStrCID"] = base.Request.QueryString["CaseID"].ToString();
                        this.Session["Archived"] = 0;
                        CaseDetailsBO caseDetailsBO1 = new CaseDetailsBO();
                        Bill_Sys_CaseObject billSysCaseObject1 = new Bill_Sys_CaseObject();
                        billSysCaseObject1.SZ_PATIENT_ID = caseDetailsBO1.GetCasePatientID(base.Request.QueryString["CaseID"].ToString(), "");
                        billSysCaseObject1.SZ_CASE_ID = base.Request.QueryString["CaseID"].ToString();
                        billSysCaseObject1.SZ_CASE_NO = caseDetailsBO1.GetCaseNo(billSysCaseObject1.SZ_CASE_ID, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                        billSysCaseObject1.SZ_PATIENT_NAME = caseDetailsBO1.GetPatientName(billSysCaseObject1.SZ_PATIENT_ID);
                        this.Session["CASE_OBJECT"] = billSysCaseObject1;
                    }
                    this.blnTag = true;
                    this.GetPatientDetailList();
                    this.ConfigPatientDesk();
                    this.LoadTabInformation();
                    this._deleteOpeation = new Bill_Sys_DeleteBO();
                    if (this._deleteOpeation.checkForDelete(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE))
                    {
                        this.btnDeleteOne.Visible = false;
                        this.btnDeleteTwo.Visible = false;
                        this.btnDeleteThree.Visible = false;
                        this.btnDeleteFour.Visible = false;
                        this.btnDeleteFive.Visible = false;
                        this.btnDeleteSix.Visible = false;
                        this.btnDeleteSeven.Visible = false;
                        this.btnDeleteEight.Visible = false;
                        this.btnDeleteNine.Visible = false;
                        this.btnDeleteTen.Visible = false;
                        this.btnDeleteEleven.Visible = false;
                        this.btnDeleteTwelve.Visible = false;
                        this.btnDeleteThirteen.Visible = false;
                        this.btnDeleteFourteen.Visible = false;
                        this.btnDeleteFifteen.Visible = false;
                        this.btnDeleteSixteen.Visible = false;
                        this.btnDeleteSeventeen.Visible = false;
                        this.btnDeleteEighteen.Visible = false;
                        this.btnDeleteNineteen.Visible = false;
                        this.btnDeleteTwenty.Visible = false;
                        this.btnDeleteTwentyOne.Visible = false;
                        this.btnDeleteTwentyTwo.Visible = false;
                        this.btnDeleteTwentyThree.Visible = false;
                        this.btnDeleteTwentyFour.Visible = false;
                        this.btnDeleteTwentyFive.Visible = false;
                        this.btnDeleteTwentySix.Visible = false;
                        this.btnDeleteTwentySeven.Visible = false;
                        this.btnDeleteTwentyEight.Visible = false;
                        this.btnDeleteTwentyNine.Visible = false;
                        this.btnDeleteThirty.Visible = false;
                        this.btnDeleteThirtyOne.Visible = false;
                        this.btnDeleteThirtyTwo.Visible = false;
                        this.btnDeleteThirtyThree.Visible = false;
                        this.btnDeleteThirtyFour.Visible = false;
                        this.btnDeleteThirtyFive.Visible = false;
                    }
                    if (this.Session["GRD_ID"] != null)
                    {
                        this.ActiveTab(this.Session["GRD_ID"].ToString());
                        this.Session["GRD_ID"] = null;
                    }
                    if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ROLE.Trim().ToLower().ToString() == "referring office")
                    {
                        this.A1.Visible = false;
                    }
                    this.Session["SN"] = "0";
                    hdnCaseId.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID;
                    hdnCaseNo.Value = ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO;
                    hdn_TestFacility.Value = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY.ToString();
                }
                if (this.Session["CASE_OBJECT"] == null)
                {
                    base.Response.Redirect("Bill_Sys_SearchCase.aspx", false);
                }
                else
                {
                    this.ShowPopupNotes(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID);
                }
                this.lblMsg.Visible = false;
                this.lblMsg.Text = "";
                if (((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY && this.btIsConfig)
                {
                    this.tblRe_Schedule.Visible = true;
                    this.GetRescheduleList();
                    this.btIsConfig = false;
                }
            }
            else
            {
                this.LoadTabInformation();
                this.tabVistInformation.ActiveTabIndex = Convert.ToInt32(this.tabid.Value) - 1;
                this.tabid.Value = "";
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

        if (((Bill_Sys_BillingCompanyObject)this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
        {
            (new Bill_Sys_ChangeVersion(this.Page)).MakeReadOnlyPage("Bill_SysPatientDesk.aspx");
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    public void RedirectToScanApp(string szNODETYPESCAN, string EventID, string PGID)
    {
        string str = ConfigurationManager.AppSettings["webscanurl"].ToString();
        string nodeIDMSTNodes = (new Bill_Sys_BillTransaction_BO()).GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szNODETYPESCAN);
        string[] sZCOMPANYID = new string[] { str, "&Flag=UploadDoc&CompanyId=", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "&CaseId=", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID, "&UserName=", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME, "&CompanyName=", ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME };
        str = string.Concat(sZCOMPANYID);
        Type type = base.GetType();
        string[] sZCASENO = new string[] { "window.open('", str, "&CaseNo=", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_NO, "&PName=", ((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_PATIENT_NAME, "&NodeId=", nodeIDMSTNodes, "&BillNo=", EventID, "&UserId=", ((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID, "&StatusID=", PGID, "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); " };
        string[] strArrays = sZCASENO;
        ScriptManager.RegisterStartupScript(this, type, "starScript", string.Concat(strArrays), true);
    }

    private void setColumnAccordingScheduleType(ref DataGrid p_objDataGrid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataGrid pObjDataGrid = p_objDataGrid;
        try
        {
            if (pObjDataGrid.Items.Count > 0)
            {
                if (((TextBox)pObjDataGrid.Items[0].FindControl("txtScheduleType")).Text == "IN")
                {
                    pObjDataGrid.Columns[1].Visible = false;
                }
                else
                {
                    pObjDataGrid.Columns[7].Visible = false;
                }
                for (int i = 0; i < pObjDataGrid.Items.Count; i++)
                {
                    if (p_objDataGrid.Items[i].Cells[32].Text.ToString() == "AC" || p_objDataGrid.Items[i].Cells[32].Text.ToString() == "CH" || p_objDataGrid.Items[i].Cells[32].Text.ToString() == "PT" || p_objDataGrid.Items[i].Cells[32].Text.ToString() == "PM")
                    {
                        if (!(p_objDataGrid.Items[i].Cells[33].Text.ToString() == "True") || !(p_objDataGrid.Items[i].Cells[34].Text.ToString() == "True") || !(p_objDataGrid.Items[i].Cells[22].Text.ToString() == "False"))
                        {
                            LinkButton linkButton = (LinkButton)p_objDataGrid.Items[i].FindControl("lnkDoctorNotes");
                            linkButton.Visible = false;
                        }
                        else
                        {
                            LinkButton linkButton1 = (LinkButton)p_objDataGrid.Items[i].FindControl("lnkDoctorNotes");
                            linkButton1.Visible = true;
                        }
                    }
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

    public void setTab()
    {
        this.tabVistInformation.ActiveTabIndex = 5;
    }

    private void ShowPopupNotes(string szCaseid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Billing_Sys_ManageNotesBO billingSysManageNotesBO = new Billing_Sys_ManageNotesBO();
        ArrayList arrayLists = new ArrayList();
        try
        {
            arrayLists = billingSysManageNotesBO.GetPopupNotesDesc(szCaseid, ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            for (int i = 0; i < arrayLists.Count; i++)
            {
                base.Response.Write(string.Concat("<script language='javascript'>alert('", arrayLists[i].ToString(), "');</script>"));
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

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (this.Session["UploadReport_DoctorId"] == null || this.Session["UploadReport_VisitType"] == null || this.Session["UploadReport_EventId"] == null || this.Session["UploadReport_ProcedureGroupId"] == null)
            {
                this.Msglbl.Text = "Doctor or VisitType unknown ";
            }
            else if (this.Session["UploadReport_DoctorId"].ToString() != "" && this.Session["UploadReport_VisitType"].ToString() != "" && this.Session["UploadReport_ProcedureGroupId"].ToString() != "" && this.Session["UploadReport_EventId"].ToString() != "")
            {
                Bill_Sys_Upload_VisitReport billSysUploadVisitReport = new Bill_Sys_Upload_VisitReport();
                if (this.ReportUpload.HasFile)
                {
                    ArrayList arrayLists = new ArrayList();
                    arrayLists.Add(((Bill_Sys_CaseObject)this.Session["CASE_OBJECT"]).SZ_CASE_ID.ToString());
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID.ToString());
                    arrayLists.Add(this.Session["UploadReport_DoctorId"].ToString());
                    arrayLists.Add(this.Session["UploadReport_VisitType"].ToString());
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME.ToString());
                    arrayLists.Add(this.ReportUpload.FileName);
                    arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID.ToString());
                    arrayLists.Add(((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                    arrayLists.Add(this.Session["UploadReport_EventId"].ToString());
                    arrayLists.Add(this.Session["UploadReport_ProcedureGroupId"].ToString());
                    string str = billSysUploadVisitReport.Upload_Report_For_Visit(arrayLists);
                    if (str != "Failed")
                    {
                        if (!Directory.Exists(str))
                        {
                            Directory.CreateDirectory(str);
                        }
                        this.ReportUpload.SaveAs(string.Concat(str, this.ReportUpload.FileName));
                        str = "Document Saved Successfully";
                    }
                    this.Msglbl.Text = "Unable to save the Document";
                }
                else
                {
                    this.Msglbl.Text = "No File Selected";
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

    private class Calendar_DAO
    {
        private string szControlIDPrefix;

        private string szInitDisplayMonth;

        private string szInitDisplayYear;

        public string ControlIDPrefix
        {
            get
            {
                return this.szControlIDPrefix;
            }
            set
            {
                this.szControlIDPrefix = value;
            }
        }

        public string InitialDisplayMonth
        {
            get
            {
                return this.szInitDisplayMonth;
            }
            set
            {
                this.szInitDisplayMonth = value;
            }
        }

        public string InitialDisplayYear
        {
            get
            {
                return this.szInitDisplayYear;
            }
            set
            {
                this.szInitDisplayYear = value;
            }
        }

        public Calendar_DAO()
        {
        }
    }
}