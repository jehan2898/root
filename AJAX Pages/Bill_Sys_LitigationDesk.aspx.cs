using AjaxControlToolkit;
using CUTEFORMCOLib;
using ExtendedDropDownList;
using log4net;
using mbs.bl.litigation;
using mbs.dao;
using MergeTIFFANDPDF;
using PDFValueReplacement;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using XGridPagination;
using XGridSearchTextBox;
using XGridView;
using XMLDMLComponent;
using DevExpress.Web;

public partial class LitigationDesk : Page, IRequiresSessionState
{
    private Bill_Sys_BillTransaction_BO _billTransactionBO;
    private Bill_Sys_BillingCompanyDetails_BO _obj;
    /*private MergeTIFFANDPDF.MergeTIFFANDPDF _objMergeTiffAndPDF;
    protected Button btnAssign;
    protected Button btnClear;
    protected Button btnSearch;
    protected CalendarExtender CalendarExtender1;
    protected CalendarExtender calExtFromDate;
    protected XGridPaginationDropDown con;
    protected DropDownList ddlDateValues;
    protected DropDownList ddlTransferStatus;
    protected ExtendedDropDownList.ExtendedDropDownList extddlCaseStatus;
    protected ExtendedDropDownList.ExtendedDropDownList extddlInsurance;
    protected ExtendedDropDownList.ExtendedDropDownList extddlProvider;
    protected ExtendedDropDownList.ExtendedDropDownList extddlSpeciality;
    protected ExtendedDropDownList.ExtendedDropDownList extddlUserLawFirm;
    protected ExtendedDropDownList.ExtendedDropDownList extdlitigate;
    protected TextBox Flag;
    protected XGridViewControl grdLitigationCompanyWise;
    protected XGridViewControl grdLitigationDesk;
    protected ImageButton imgbtnFromDate;
    protected ImageButton imgbtnToDate;
    protected LinkButton lnkExportToExcel;*/
    private static ILog log = LogManager.GetLogger("Bill_Sys_LitigationDesk.aspx");
    /*protected MaskedEditExtender MaskedEditExtender1;
    protected MaskedEditExtender MaskedEditExtender2;
    protected MaskedEditValidator MaskedEditValidator1;
    protected MaskedEditValidator MaskedEditValidator2;*/
    private Bill_Sys_NF3_Template objNF3Template;
    private PDFValueReplacement.PDFValueReplacement objPDFReplacement;
    /*protected ScriptManager ScriptManager1;
    protected TextBox txtCase_Status;
    protected TextBox txtCaseNumber;
    protected TextBox txtCompanyId;
    protected TextBox txtDenialReason;
    protected TextBox txtFromDate;
    protected TextBox txtInsuranceCompany;
    protected TextBox txtPatientName;
    protected XGridSearchTextBox.XGridSearchTextBox txtSearchBox;
    protected TextBox txtToDate;
    protected UpdatePanel UpdatePanel1;
    protected UpdatePanel UpdatePanel10;
    protected UpdatePanel UpdatePanel2;
    protected UpdatePanel UpdatePanel3;
    protected UpdatePanel UpdatePanel6;
    protected UpdatePanel UpdatePanel8;
    protected UpdateProgress UpdateProgress1;
    protected UserControl_ErrorMessageControl usrMessage;*/

    public void BindgrdLitigationdesk()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdLitigationCompanyWise.XGridBind();
            this.grdLitigationDesk.XGridBindSearch();
            if (this.ddlTransferStatus.Text != "1")
            {
                for (int j = 0; j < this.grdLitigationDesk.Rows.Count; j++)
                {
                    if (this.grdLitigationDesk.Rows[j].Cells[12].Text.ToString().Equals("True"))
                    {
                        CheckBox box = (CheckBox) this.grdLitigationDesk.Rows[j].FindControl("chkSelect");
                        box.Enabled = false;
                    }
                }
                this.extddlUserLawFirm.Enabled = true;
            }
            else
            {
                this.extddlUserLawFirm.Enabled = false;
            }
            for (int i = 0; i < this.grdLitigationCompanyWise.Rows.Count; i++)
            {
                Label label = (Label) this.grdLitigationCompanyWise.Rows[i].FindControl("lblAmount");
                HyperLink link = (HyperLink) this.grdLitigationCompanyWise.Rows[i].FindControl("hamount");
                LinkButton button = (LinkButton) this.grdLitigationCompanyWise.Rows[i].FindControl("lnkPlus");
                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                        label.Visible = true;
                        link.Visible = false;
                        button.Visible = false;
                        break;

                    default:
                        label.Visible = false;
                        link.Visible = true;
                        button.Visible = true;
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
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Litigation litigation = new Litigation();
            LitigationDAO ndao = null;
            Bill_Sys_BillingCompanyObject obj2 = (Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"];
            Bill_Sys_UserObject obj3 = (Bill_Sys_UserObject) this.Session["USER_OBJECT"];
            ArrayList list = new ArrayList();
            bool flag = false;
            for (int i = 0; i < this.grdLitigationDesk.Rows.Count; i++)
            {
                if (((CheckBox) this.grdLitigationDesk.Rows[i].FindControl("chkSelect")).Checked)
                {
                   
                        ndao = new LitigationDAO();
                    ndao.CaseID=this.grdLitigationDesk.DataKeys[i][0].ToString();
                    ndao.BillNumber=this.grdLitigationDesk.DataKeys[i][2].ToString();
                    ndao.LawFirmID=this.extddlUserLawFirm.Text;
                    ndao.UserID=obj3.SZ_USER_ID;
                    ndao.TransferDate=DateTime.Today.ToString("MM/dd/yyyy");
                    ndao.CompanyID=obj2.SZ_COMPANY_ID;
                    if (grdLitigationDesk.Rows[i].Cells[12].Text.ToString().ToUpper().Equals("FALSE"))
                    {
                        list.Add(ndao);
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            string sBillNumber = "";
            for (int j = 0; j < list.Count; j++)
            {
                ndao = new LitigationDAO();
                ndao = (LitigationDAO) list[j];
                if (j == 0)
                {
                    sBillNumber = "'" + ndao.BillNumber + "'";
                }
                else
                {
                    sBillNumber = sBillNumber + ",'" + ndao.BillNumber + "'";
                }
            }
            if (list.Count > 0)
            {
                string szBatchId = litigation.saveLitigationBills(list);
                DataSet set = new DataSet();
                set = this.GetTransferBillDetails(this.txtCompanyId.Text, sBillNumber, this.extddlUserLawFirm.Text);
                this.SendEmailNotification(set.Tables[0], szBatchId);
                this.BindgrdLitigationdesk();
                this.usrMessage.PutMessage("Your cases were assigned to the law-firms");
                this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                this.usrMessage.Show();
            }

            if (flag)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Already Transfered!!!')", true);
                this.BindgrdLitigationdesk();
                //base.Server.Transfer("~/Bill_Sys_LitigationDesk.aspx");
                // Response.Redirect(Request.RawUrl);
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mm", "ClearFields()", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.grdLitigationDesk.XGridBindSearch();
            if (this.ddlTransferStatus.Text != "1")
            {
                for (int i = 0; i < this.grdLitigationDesk.Rows.Count; i++)
                {
                    CheckBox box = (CheckBox)this.grdLitigationDesk.Rows[i].FindControl("chkSelect");
                    box.Enabled = true;
                    if (this.grdLitigationDesk.Rows[i].Cells[11].Text.ToString().Equals("True"))
                    {
                        box.Enabled = false;
                    }
                }
                this.extddlUserLawFirm.Enabled = true;
            }
            else
            {
                this.extddlUserLawFirm.Enabled = false;
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

    protected void ContextMenu1_ItemCommand(object sender, CommandEventArgs e)
    {
        int rowIndex = this.grdLitigationDesk.RightClickedRow.RowIndex;
    }

    protected void createcsv(DataTable dt, string strFilePath)
    {
        StreamWriter writer = new StreamWriter(strFilePath, false);
        int count = dt.Columns.Count;
        for (int i = 0; i < count; i++)
        {
            writer.Write(dt.Columns[i]);
            if (i < (count - 1))
            {
                writer.Write(",");
            }
        }
        writer.Write(writer.NewLine);
        foreach (DataRow row in dt.Rows)
        {
            for (int j = 0; j < count; j++)
            {
                if (!Convert.IsDBNull(row[j]))
                {
                    if (row[j].ToString().Trim() == "")
                    {
                        writer.Write("NULL");
                    }
                    else
                    {
                        writer.Write(row[j].ToString().Replace(',', ' '));
                    }
                }
                else
                {
                    writer.Write("NULL");
                }
                if (j < (count - 1))
                {
                    writer.Write(",");
                }
            }
            writer.Write(writer.NewLine);
        }
        writer.Close();
    }

    private DataSet GetBills(string szCaseId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_GET_TRANSFERRED_BILLS_USING_CASE_ID", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetData(string sz_CompanyID, string LfID)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_LITIGATION_DESK_LAWFIRM_AMOUNT_NEW", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@LawfirmID", LfID);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetDenialInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_GET_DENIAL_REASON", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string getFileName(string p_szBillNumber)
    {
        DateTime now = DateTime.Now;
        return (p_szBillNumber + "_" + this.getRandomNumber() + "_" + now.ToString("yyyyMMddHHmmssms"));
    }

    private string getRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 0x2710).ToString();
    }

    private DataSet GetTransferBillDetails(string sCompanyID, string sBillNumber, string sAssignLawfirmId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet dataSet = new DataSet();
        SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_GET_TRANSFER_BILL_DETAILS", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sCompanyID);
            selectCommand.Parameters.AddWithValue("@SZ_BILL_NUMBER", sBillNumber);
            selectCommand.Parameters.AddWithValue("@sz_assigned_lawfirm_id", sAssignLawfirmId);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet GetVerification(string sz_CompanyID, string sz_input_bill_number)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string connectionString = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        connection = new SqlConnection(connectionString);
        DataSet dataSet = new DataSet();
        try
        {
            connection.Open();
            SqlCommand selectCommand = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            selectCommand.Parameters.AddWithValue("@sz_input_bill_number", sz_input_bill_number);
            selectCommand.Parameters.AddWithValue("@bt_operation", 2);
            new SqlDataAdapter(selectCommand).Fill(dataSet);
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
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return dataSet;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public DataSet loadcaseType(string companyid)
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
            SqlCommand comm = new SqlCommand("SP_MST_CASE_TYPE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@ID", companyid);
            comm.Parameters.AddWithValue("@FLAG", "CASETYPE_LIST");
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
    protected void grdLitigationCompanyWise_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        int num = 0;
        try
        {
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < this.grdLitigationCompanyWise.Rows.Count; i++)
                {
                    LinkButton button = (LinkButton) this.grdLitigationCompanyWise.Rows[i].FindControl("lnkMinus");
                    LinkButton button2 = (LinkButton) this.grdLitigationCompanyWise.Rows[i].FindControl("lnkPlus");
                    if (button.Visible)
                    {
                        button.Visible = false;
                        button2.Visible = true;
                    }
                }
                this.grdLitigationCompanyWise.Columns[3].Visible = true;
                num = Convert.ToInt32(e.CommandArgument);
                string str = "div";
                str = str + this.grdLitigationCompanyWise.DataKeys[num][0].ToString();
                GridView view = (GridView) this.grdLitigationCompanyWise.Rows[num].FindControl("grdVerification");
                LinkButton button3 = (LinkButton) this.grdLitigationCompanyWise.Rows[num].FindControl("lnkPlus");
                LinkButton button4 = (LinkButton) this.grdLitigationCompanyWise.Rows[num].FindControl("lnkMinus");
                string text = this.txtCompanyId.Text;
                string lfID = this.grdLitigationCompanyWise.DataKeys[num][0].ToString();
                DataSet data = this.GetData(text, lfID);
                view.DataSource = data;
                view.DataBind();
                ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
                button3.Visible = false;
                button4.Visible = true;
            }
            if (e.CommandName.ToString() == "MNS")
            {
                this.grdLitigationCompanyWise.Columns[3].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str4 = "div";
                str4 = str4 + this.grdLitigationCompanyWise.DataKeys[num][0].ToString();
                LinkButton button5 = (LinkButton) this.grdLitigationCompanyWise.Rows[num].FindControl("lnkPlus");
                LinkButton button6 = (LinkButton) this.grdLitigationCompanyWise.Rows[num].FindControl("lnkMinus");
                ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mm", "HideChildGrid('" + str4 + "') ;", true);
                button5.Visible = true;
                button6.Visible = false;
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

    protected void grdLitigationDesk_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.grdLitigationDesk.PageIndex = e.NewPageIndex;
        this.BindgrdLitigationdesk();
    }

    protected void grdLitigationDesk_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        new ArrayList();
        Litigation litigation = new Litigation();
        int num = 0;
        try
        {
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < this.grdLitigationDesk.Rows.Count; i++)
                {
                    LinkButton button = (LinkButton) this.grdLitigationDesk.Rows[i].FindControl("lnkM");
                    LinkButton button2 = (LinkButton) this.grdLitigationDesk.Rows[i].FindControl("lnkP");
                    if (button.Visible)
                    {
                        button.Visible = false;
                        button2.Visible = true;
                    }
                }
                this.grdLitigationDesk.Columns[18].Visible = true;
                this.grdLitigationDesk.Columns[19].Visible = false;
                this.grdLitigationDesk.Columns[20].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str = "div";
                str = str + this.grdLitigationDesk.DataKeys[num][2].ToString();
                GridView view = (GridView) this.grdLitigationDesk.Rows[num].FindControl("GridView2");
                LinkButton button3 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkP");
                LinkButton button4 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkM");
                this.grdLitigationDesk.DataKeys[num][0].ToString();
                DataSet set = new DataSet();
                set = litigation.GetLitigatedBillsInfo(this.grdLitigationDesk.DataKeys[num][2].ToString(), ((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, this.extdlitigate.Text);
                view.DataSource = set;
                view.DataBind();
                ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mp", "ShowChildGrid('" + str + "') ;", true);
                button3.Visible = false;
                button4.Visible = true;
            }
            if (e.CommandName.ToString() == "MNS")
            {
                this.grdLitigationDesk.Columns[18].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str2 = "div";
                str2 = str2 + this.grdLitigationDesk.DataKeys[num][2].ToString();
                LinkButton button5 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkP");
                LinkButton button6 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkM");
                ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mm", "HideChildGrid('" + str2 + "') ;", true);
                button5.Visible = true;
                button6.Visible = false;
            }
            if (e.CommandName.ToString() == "VerifPLS")
            {
                for (int j = 0; j < this.grdLitigationDesk.Rows.Count; j++)
                {
                    LinkButton button7 = (LinkButton) this.grdLitigationDesk.Rows[j].FindControl("lnkVM");
                    LinkButton button8 = (LinkButton) this.grdLitigationDesk.Rows[j].FindControl("lnkVP");
                    if (button7.Visible)
                    {
                        button7.Visible = false;
                        button8.Visible = true;
                    }
                }
                this.grdLitigationDesk.Columns[19].Visible = true;
                this.grdLitigationDesk.Columns[18].Visible = false;
                this.grdLitigationDesk.Columns[20].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str3 = "div2";
                str3 = str3 + this.grdLitigationDesk.DataKeys[num][2].ToString();
                GridView view2 = (GridView) this.grdLitigationDesk.Rows[num].FindControl("grdVerification_Gird");
                LinkButton button9 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkVP");
                LinkButton button10 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkVM");
                string str4 = "'" + this.grdLitigationDesk.DataKeys[num][2].ToString() + "'";
                string text = this.txtCompanyId.Text;
                DataSet verification = this.GetVerification(text, str4);
                view2.DataSource = verification;
                view2.DataBind();
                ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mp", "ShowChildGrid('" + str3 + "') ;", true);
                button9.Visible = false;
                button10.Visible = true;
                LinkButton button11 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkDM");
                LinkButton button12 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkDP");
                button12.Visible = true;
                if (button11.Visible)
                {
                    button11.Visible = false;
                    button12.Visible = true;
                }
                LinkButton button13 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkM");
                LinkButton button14 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkP");
                button14.Visible = true;
                if (button13.Visible)
                {
                    button13.Visible = false;
                    button14.Visible = true;
                }
            }
            if (e.CommandName.ToString() == "VerifMNS")
            {
                this.grdLitigationDesk.Columns[19].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str6 = "div2";
                str6 = str6 + this.grdLitigationDesk.DataKeys[num][2].ToString();
                LinkButton button15 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkVP");
                LinkButton button16 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkVM");
                ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mm", "HideChildGrid('" + str6 + "') ;", true);
                button15.Visible = true;
                button16.Visible = false;
            }
            if (e.CommandName.ToString() == "DenialPLS")
            {
                for (int k = 0; k < this.grdLitigationDesk.Rows.Count; k++)
                {
                    LinkButton button17 = (LinkButton) this.grdLitigationDesk.Rows[k].FindControl("lnkDM");
                    LinkButton button18 = (LinkButton) this.grdLitigationDesk.Rows[k].FindControl("lnkDP");
                    if (button17.Visible)
                    {
                        button17.Visible = false;
                        button18.Visible = true;
                    }
                }
                this.grdLitigationDesk.Columns[20].Visible = true;
                this.grdLitigationDesk.Columns[19].Visible = false;
                this.grdLitigationDesk.Columns[18].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str7 = "div1";
                str7 = str7 + this.grdLitigationDesk.DataKeys[num][2].ToString();
                GridView view3 = (GridView) this.grdLitigationDesk.Rows[num].FindControl("grdDenial");
                LinkButton button19 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkDP");
                LinkButton button20 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkDM");
                string str8 = this.grdLitigationDesk.DataKeys[num][2].ToString();
                string str9 = this.txtCompanyId.Text;
                DataSet denialInfo = this.GetDenialInfo(str9, str8);
                view3.DataSource = denialInfo;
                view3.DataBind();
                ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mp", "ShowDenialChildGrid('" + str7 + "') ;", true);
                button19.Visible = false;
                button20.Visible = true;
                LinkButton button21 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkVM");
                LinkButton button22 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkVP");
                if (button21.Visible)
                {
                    button21.Visible = false;
                    button22.Visible = true;
                }
            }
            if (e.CommandName.ToString() == "DenialMNS")
            {
                this.grdLitigationDesk.Columns[18].Visible = false;
                this.grdLitigationDesk.Columns[19].Visible = false;
                num = Convert.ToInt32(e.CommandArgument);
                string str10 = "div";
                str10 = str10 + this.grdLitigationDesk.DataKeys[num][2].ToString();
                LinkButton button23 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkDP");
                LinkButton button24 = (LinkButton) this.grdLitigationDesk.Rows[num].FindControl("lnkDM");
                button23.Visible = true;
                button24.Visible = false;
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

    private ArrayList lfnGetListWithPDFName(ArrayList objAL, string p_szPDFFileName, string p_szDiagPosKey, string p_szAddDiagNextPage, int p_iDiagCount)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        ArrayList list=new ArrayList();
        try
        {
            for (int i = 0; i < (objAL.Count - 1); i++)
            {
                if (((p_szDiagPosKey == "CK_0000004") && (p_szAddDiagNextPage != "CI_0000003")) && ((p_iDiagCount >= 5) && objAL[i].ToString().Equals("AOB")))
                {
                    objAL[i] = p_szPDFFileName;
                }
                if (((p_szDiagPosKey == "CK_0000005") && (p_szAddDiagNextPage != "CI_0000003")) && ((p_iDiagCount >= 5) && objAL[i].ToString().Equals("EOB")))
                {
                    objAL[i] = p_szPDFFileName;
                }
            }
            for (int j = 0; j < objAL.Count; j++)
            {
                if ((objAL[j].ToString().Equals("AOB") || objAL[j].ToString().Equals("EOB")) || objAL[j].ToString().ToLower().Equals("denials"))
                {
                    objAL.RemoveAt(j);
                    j--;
                }
            }
            list = objAL;
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
        return list;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    private string lfnMergePDF(string p_szSource1, string p_szSource2, string p_szDestinationFileName)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string str2="";
        try
        {
            CutePDFDocumentClass class2 = new CutePDFDocumentClass();
            int num = 0;
            string str = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
            class2.initialize(str);
            if (((class2 != null) && File.Exists(p_szSource1)) && File.Exists(p_szSource2))
            {
                num = class2.mergePDF(p_szSource1, p_szSource2, p_szDestinationFileName);
            }
            if (num == 0)
            {
                return "FAIL";
            }
            str2 = "SUCCESS";
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
        return str2;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + this.grdLitigationDesk.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    private void BindCaseType()
    {
        DataSet ds = new DataSet();
        ds = loadcaseType(this.txtCompanyId.Text);
        ASPxListBox lstCaseType = (ASPxListBox)this.ddleCaseType.FindControl("lstCaseType");
        lstCaseType.ValueField = "CODE";
        lstCaseType.TextField = "DESCRIPTION";
        lstCaseType.DataSource = ds;
        lstCaseType.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All ---", "NA");
        lstCaseType.Items.Insert(0, Item);
    }

    private string GetCasetype()
    {
        string scasetype1="";
        Control Casetype = ddleCaseType.FindControl("lstCaseType");
        if (Casetype != null)
        {
            string scasetype = "";
            foreach (ListEditItem Item in ((ASPxListBox)Casetype).SelectedItems)
            {
                if (scasetype.Equals("") && Item.Value.ToString() != "NA")
                {
                    scasetype = Item.Value.ToString();
                }
                else if (scasetype != null && Item.Value.ToString() != "NA")
                {
                    scasetype = scasetype + "," + Item.Value.ToString();
                }
            }
            scasetype1 = scasetype;
        }
        return scasetype1;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        this.grdLitigationCompanyWise.Page=this.Page;
        this.con.SourceGrid=this.grdLitigationDesk;
        this.txtSearchBox.SourceGrid=this.grdLitigationDesk;
        this.grdLitigationDesk.Page=this.Page;
        this.grdLitigationDesk.PageNumberList=this.con;
        base.Title = "Litigation Desk";
        txtCasetype.Text = GetCasetype();
        this.extddlCaseStatus.Flag_ID=((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlInsurance.Flag_ID=((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extdlitigate.Flag_ID=((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.extddlProvider.Flag_ID=((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.btnAssign.Attributes.Add("onclick", "return formValidator('aspnetForm','extddlUserLawFirm');");
        this.extddlSpeciality.Flag_ID=((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.drdRevertStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        this.btnAssign.Attributes.Add("onclick", "return LitigationConformation();");
        this.ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
       
        try
        {
            if (!this.Page.IsPostBack)
            {
                this.txtCompanyId.Text = ((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                log.Debug(this.txtCompanyId.Text);
                BindCaseType();
                this.BindgrdLitigationdesk();
            }
            if (((Bill_Sys_BillingCompanyObject) this.Session["APPSTATUS"]).SZ_READ_ONLY.ToString().Equals("True"))
            {
                new Bill_Sys_ChangeVersion(this.Page).MakeReadOnlyPage("Bill_Sys_LitigationDesk.aspx");
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.ddlTransferStatus.Text != "1")
        {
            for (int i = 0; i < this.grdLitigationDesk.Rows.Count; i++)
            {
                if (ddlTransferStatus.Text == "0")
                {
                    CheckBox box = (CheckBox)this.grdLitigationDesk.Rows[i].FindControl("chkSelect");
                    box.Enabled = true;
                }
                else if (this.grdLitigationDesk.Rows[i].Cells[12].Text.ToString().Equals("True"))
                {
                    if (ddlTransferStatus.Text == "2")
                    {
                        CheckBox box = (CheckBox)this.grdLitigationDesk.Rows[i].FindControl("chkSelect");
                        box.Enabled = true;
                    }
                    else
                    {
                        CheckBox box = (CheckBox)this.grdLitigationDesk.Rows[i].FindControl("chkSelect");
                        box.Enabled = false;
                    }

                }

            }
        }
    }

    public bool SendEmailNotification(DataTable dt, string szBatchId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            string str = ConfigurationManager.AppSettings["Connection_String"];
            SQLToDAO odao = new SQLToDAO(str);
            FileTransferEmailNotifications_DAO s_dao = new FileTransferEmailNotifications_DAO();
            s_dao.sz_law_firm_company_id=this.extddlUserLawFirm.Text;
            s_dao.sz_company_id=((Bill_Sys_BillingCompanyObject) this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            s_dao.i_flag=1;
            DataSet set = new DataSet();
            set = odao.LoadDataSet("SP_MANAGE_FILE_TRANSFER_NOTIFICATIONS", "mbs.dao.FileTransferEmailNotifications_DAO", s_dao, "mbs.dao");
            new ArrayList();
            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            string str2 = "<table width='100%'><tr><td style='width:12%' colspan='2'>Dear " + this.extddlUserLawFirm.Selected_Text + ",</td><td style='width:12%'></td></tr><tr><td style='width:12%'></td><td colspan='2'>New Files have been transferred to your office on " + DateTime.Now.ToString("MM/dd/yyyy") + ". These files will be available for 15 days only. Please acknowledge the receipt and provide internal case ids generated against these bills back to us. You can reach green.your.bills.support@procomsys.com in case of any problems</td></tr><tr><td style='width:12%'></td><td colspan='2'></td></tr><tr><td style='width:12%' colspan='2'>Thanks & Regards</td><td></td></tr><tr><td style='width:12%' colspan='2'>Green Bills Support</td><td></td></tr></table>";
            string str3 = this.getFileName("Litigation_Desk") + ".csv";
            string strFilePath = ConfigurationManager.AppSettings["BASEPATH"].ToString() + str3;
            if ((set.Tables.Count > 0) && (set.Tables[0].Rows.Count > 0))
            {
                string userName = ConfigurationManager.AppSettings["HOSTMAIL"].ToString();
                string password = ConfigurationManager.AppSettings["HOSPASSWORD"].ToString();
                this.createcsv(dt, strFilePath);
                client.Credentials = new NetworkCredential(userName, password);
                client.EnableSsl = true;
                client.Host = "smtp.gmail.com";
                client.Port = 0x24b;
                message.IsBodyHtml = true;
                message.From = new MailAddress(userName);
                Attachment item = null;
                item = new Attachment(strFilePath);
                message.Attachments.Add(item);
                item.ContentDisposition.Inline = false;
                message.IsBodyHtml = true;
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    if ((set.Tables[0].Rows[i][0].ToString() == "") || (set.Tables[0].Rows[i][0].ToString() == null))
                    {
                        ScriptManager.RegisterClientScriptBlock((Page) this, base.GetType(), "mm", "alert('set email id's to lawfirm.');", true);
                    }
                    else
                    {
                        message.To.Add(set.Tables[0].Rows[i][0].ToString());
                    }
                }
                string str8 = ConfigurationManager.AppSettings["Subject"].ToString();
                message.Bcc.Add(userName);
                message.Subject = str8;
                message.Body = str2;
                client.EnableSsl = true;
                client.Send(message);
                message.Dispose();
            }
            if (File.Exists(strFilePath))
            {
                File.Delete(strFilePath);
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
        return true;

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    DAO_NOTES_EO _DAO_NOTES_EO = null;
    DAO_NOTES_BO _DAO_NOTES_BO = null;
    protected void btnRevert_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        Bill_Sys_ReportBO billSysReportBO = new Bill_Sys_ReportBO();
        try
        {
            if (this.drdRevertStatus.Text != "NA")
            {
                ArrayList arrayLists = new ArrayList();
                string str = "";
                bool flag = false;
                for (int i = 0; i < this.grdLitigationDesk.Rows.Count; i++)
                {
                    if (((CheckBox)this.grdLitigationDesk.Rows[i].FindControl("chkSelect")).Checked)
                    {
                        if (flag)
                        {
                            str = string.Concat(str, ",'", this.grdLitigationDesk.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'");
                        }
                        else
                        {
                            str = string.Concat("'", this.grdLitigationDesk.DataKeys[i]["SZ_BILL_NUMBER"].ToString(), "'");
                            flag = true;
                        }
                    }

                }
                if (str != "")
                {
                    arrayLists.Add(this.drdRevertStatus.Text);
                    arrayLists.Add(str);
                    arrayLists.Add(((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    billSysReportBO.revertBillStatus(arrayLists);
                    this._DAO_NOTES_EO = new DAO_NOTES_EO();
                    this._DAO_NOTES_EO.SZ_MESSAGE_TITLE = "BILL_STATUS_UPDATED";
                    this._DAO_NOTES_EO.SZ_ACTIVITY_DESC = "Bill No : " + str + " updated to :" + this.drdRevertStatus.Selected_Text;
                    this._DAO_NOTES_BO = new DAO_NOTES_BO();
                    this._DAO_NOTES_EO.SZ_USER_ID = (((Bill_Sys_UserObject)this.Session["USER_OBJECT"]).SZ_USER_ID);
                    //this._DAO_NOTES_EO.SZ_CASE_ID = new Bill_Sys_BillingCompanyDetails_BO().GetCaseID(item.sz_bill_no, ((Bill_Sys_BillingCompanyObject)HttpContext.Current.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    this._DAO_NOTES_EO.SZ_COMPANY_ID = ((Bill_Sys_BillingCompanyObject)this.Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                    this._DAO_NOTES_BO.SaveActivityNotes(this._DAO_NOTES_EO);
                    this.usrMessage.PutMessage("Bill status Revert successfully.");
                    this.usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                    this.usrMessage.Show();
                    //this.txtGroupId.Text = this.extddlSpeciality.Text;
                    //this.txtBillStatusID.Text = this.drdUpdateStatus.Text;
                    // this.grdLitigationDesk.XGridDatasetNumber = 2;
                    this.grdLitigationDesk.XGridBindSearch();
                    DataTable dataTable = new DataTable();
                    dataTable = this.grdLitigationDesk.XGridDataset;
                    //this.lblTotalBillAmount.Text = string.Concat(" Total Bill Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][0].ToString()), 2)));
                    //this.lblOutSratingAmount.Text = string.Concat("Total Outstanding Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][1].ToString()), 2)));
                    //this.lblTotalWOF.Text = string.Concat("Total WriteOff Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][2].ToString()), 2)));
                    //this.lblPdAmount.Text = string.Concat("Total Paid Amount = $", Convert.ToString(Math.Round(Convert.ToDecimal(dataTable.Rows[0][3].ToString()), 2)));
                }
            }
            //for (int j = 0; j < this.grdLitigationDesk.Rows.Count; j++)
            //{
            //    if (this.grdLitigationDesk.DataKeys[j]["SZ_BILL_NOTES"].ToString() != "" && this.grdLitigationDesk.DataKeys[j]["SZ_BILL_NOTES"].ToString() != "&nbsp;")
            //    {
            //        //LinkButton red = (LinkButton)this.grdLitigationDesk.Rows[j].FindControl("lnkBillNotes");
            //        //red.ForeColor = Color.Red;
            //    }
            //}
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

    /*  protected HttpApplication ApplicationInstance
      {
          get
          {
              return this.Context.ApplicationInstance;
          }
      }

      protected DefaultProfile Profile
      {
          get
          {
              return (DefaultProfile) this.Context.Profile;
          }
      }*/
}

