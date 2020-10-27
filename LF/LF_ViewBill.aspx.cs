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
using mbs.lawfirm;
using System.Data.SqlClient;

public partial class LF_LF_ViewBill : PageBase
{
    String strsqlCon;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    SqlDataReader dr;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {   
        Session["LF_LF_ViewBill"] = "LF_LF_ViewBill";
        this.con.SourceGrid = grdViewBill;
        this.txtSearchBox.SourceGrid = grdViewBill;
        this.grdViewBill.Page = this.Page;
        this.grdViewBill.PageNumberList = this.con;


        grdViewBill.Columns[20].Visible = false;
        grdViewBill.Columns[19].Visible = false;
        grdViewBill.Columns[18].Visible = false;
        if (!IsPostBack)
        {
            txtCaseID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            txtCompanyID.Text = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
                grdViewBill.XGridBind();
                
                
                GetTotalAmount();
            btnUploadFile.Attributes.Add("onClick", "return   checkFile();");
        }
    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdViewBill.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < grdViewBill.Rows.Count; i++)
        {
            string denial = grdViewBill.DataKeys[i][2].ToString();
            string verification = grdViewBill.DataKeys[i][3].ToString();
            string Payment = grdViewBill.DataKeys[i][4].ToString();
            LinkButton minus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkM");
            LinkButton plus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkP");
            LinkButton minus2 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDM");
            LinkButton plus2 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDP");
            LinkButton PayMinus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayM");
            LinkButton PayPlus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayP");

            //plus2.ForeColor=System.Drawing.Color.Red;
            //minus2.ForeColor = System.Drawing.Color.Red;

            if (denial.ToLower() == "1")
            {
                plus2.ForeColor = System.Drawing.Color.Red;
                minus2.ForeColor = System.Drawing.Color.Red;
                plus2.Text = "+";
                minus2.Text = "-";
            }
            else
            {
                plus2.Text = "";
                minus2.Text = "";
            }

            if (verification.ToLower() == "1")
            {
                plus1.ForeColor = System.Drawing.Color.Red;
                minus1.ForeColor = System.Drawing.Color.Red;
                plus1.Text = "+";
                minus1.Text = "-";
            }
            else
            {
                plus1.Text = "";
                minus1.Text = "";

            }

            if (Payment.ToLower() == "1")
            {
                PayPlus.ForeColor = System.Drawing.Color.Red;
                PayMinus.ForeColor = System.Drawing.Color.Red;
                PayPlus.Text = "+";
                PayMinus.Text = "-";
            }
            else
            {
                PayPlus.Text = "";
                PayMinus.Text = "";

            }
        }
    }
    
    protected void grdViewBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = 0 ;
        try
        {
            //For Verification gridview
            if (e.CommandName.ToString() == "PLS")
            {
                for (int i = 0; i < grdViewBill.Rows.Count; i++)
                {
                    LinkButton minus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkM");
                    LinkButton plus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkP");
                    LinkButton Dminus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDM");
                    LinkButton DPlus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDP");
                    LinkButton PayMinus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayM");
                    LinkButton PayPlus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayP");
                    PayMinus.Visible = false;
                    PayPlus.Visible = true;
                    //Dminus1.Visible = false;
                    DPlus1.Visible = true;
                    if (Dminus1.Visible)
                    {
                        Dminus1.Visible = false;
                    }

                    if (minus1.Visible)
                    {
                        minus1.Visible = false;
                        plus1.Visible = true;
                    }
                }
                grdViewBill.Columns[18].Visible = true;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";
                divname = divname + grdViewBill.DataKeys[index][0].ToString();
                GridView gv = (GridView)grdViewBill.Rows[index].FindControl("grdVerification");
                LinkButton plus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkM");
                string billno = "'" + grdViewBill.DataKeys[index][0].ToString() + "'";
                string companyid = grdViewBill.DataKeys[index][1].ToString();
                DataSet dsVeri = GetVerification(companyid, billno);
                gv.DataSource = dsVeri;
                gv.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mp", "ShowChildGrid('" + divname + "') ;", true);
                plus.Visible = false;
                minus.Visible = true;

                LinkButton Dminus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkDM");
                LinkButton DPlus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkDP");
                DPlus.Visible = true;
                if (Dminus.Visible == true)
                {
                    Dminus.Visible = false;
                    DPlus.Visible = true;
                }
                
            }
            if (e.CommandName.ToString() == "MNS")
            {
                grdViewBill.Columns[18].Visible = false;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";
                divname = divname + grdViewBill.DataKeys[index][0].ToString();
                LinkButton plus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkP");
                LinkButton minus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkM");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HideChildGrid('" + divname + "') ;", true);
                plus.Visible = true;
                minus.Visible = false;
                
            }

            //For Denial GridView
            if (e.CommandName.ToString() == "DenialPLS")
            {
                for (int i = 0; i < grdViewBill.Rows.Count; i++)
                {
                    LinkButton minus2 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDM");
                    LinkButton plus2 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDP");
                    LinkButton Vminus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkM");
                    LinkButton Vplus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkP");
                    LinkButton PayMinus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayM");
                    LinkButton PayPlus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayP");
                    Vminus1.Visible = false;
                    Vplus1.Visible = true;
                    PayMinus.Visible = false;
                    PayPlus.Visible = true;
                    if (minus2.Visible)
                    {
                        minus2.Visible = false;
                        plus2.Visible = true;
                    }
                }
                grdViewBill.Columns[19].Visible = true;
                grdViewBill.Columns[18].Visible = false;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div1";
                divname = divname + grdViewBill.DataKeys[index][0].ToString();
                GridView gv = (GridView)grdViewBill.Rows[index].FindControl("grdDenial");
                LinkButton plus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkDP");
                LinkButton minus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkDM");
                string billno = grdViewBill.DataKeys[index][0].ToString();
                string companyid = grdViewBill.DataKeys[index][1].ToString();
                DataSet dsDenial = GetDenialInfo(companyid, billno);
                gv.DataSource = dsDenial;
                gv.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mp", "ShowDenialChildGrid('" + divname + "') ;", true);
                plus.Visible = false;
                minus.Visible = true;

                LinkButton Vminus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkM");
                LinkButton Vplus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkP");
                if (Vminus.Visible == true)
                {
                    Vminus.Visible = false;
                    Vplus.Visible = true;
                }
            }
            if (e.CommandName.ToString() == "DenialMNS")
            {
                grdViewBill.Columns[19].Visible = false;
                grdViewBill.Columns[18].Visible = false;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div";
                divname = divname + grdViewBill.DataKeys[index][0].ToString();
                LinkButton plus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkDP");
                LinkButton minus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkDM");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HideDenialChildGrid('" + divname + "') ;", true);
                plus.Visible = true;
                minus.Visible = false;

            }

            //For Payment gridview
            if (e.CommandName.ToString() == "PaymentPLS")
            {
                for (int i = 0; i < grdViewBill.Rows.Count; i++)
                {
                    LinkButton PayMinus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayM");
                    LinkButton PayPlus = (LinkButton)grdViewBill.Rows[i].FindControl("lnkPayP");
                    LinkButton minus2 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDM");
                    LinkButton plus2 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkDP");
                    LinkButton Vminus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkM");
                    LinkButton Vplus1 = (LinkButton)grdViewBill.Rows[i].FindControl("lnkP");
                    Vminus1.Visible = false;
                    Vplus1.Visible = true;
                    minus2.Visible = false;
                    plus2.Visible = true;
                    if (PayMinus.Visible)
                    {
                        PayMinus.Visible = false;
                        PayPlus.Visible = true;
                    }
                }
                grdViewBill.Columns[20].Visible = true;
                grdViewBill.Columns[18].Visible = false;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div2";
                divname = divname + grdViewBill.DataKeys[index][0].ToString();
                GridView gv = (GridView)grdViewBill.Rows[index].FindControl("grdPayment");
                LinkButton plus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkPayP");
                LinkButton minus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkPayM");
                string billno = grdViewBill.DataKeys[index][0].ToString();
                string companyid = grdViewBill.DataKeys[index][1].ToString();
                DataSet dsPayment = GetPaymentInfo(companyid, billno);
                gv.DataSource = dsPayment;
                gv.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mp", "ShowPaymnentChildGrid('" + divname + "') ;", true);
                plus.Visible = false;
                minus.Visible = true;

                //LinkButton Vminus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkM");
                //LinkButton Vplus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkP");
                //if (Vminus.Visible == true)
                //{
                //    Vminus.Visible = false;
                //    Vplus.Visible = true;
                //}
            }
            if (e.CommandName.ToString() == "PaymentMNS")
            {
                grdViewBill.Columns[20].Visible = false;
                grdViewBill.Columns[19].Visible = false;
                grdViewBill.Columns[18].Visible = false;
                index = Convert.ToInt32(e.CommandArgument);
                string divname = "div2";
                divname = divname + grdViewBill.DataKeys[index][0].ToString();
                LinkButton plus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkPayP");
                LinkButton minus = (LinkButton)grdViewBill.Rows[index].FindControl("lnkPayM");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "HidePaymentChildGrid('" + divname + "') ;", true);
                plus.Visible = true;
                minus.Visible = false;

            }
        }
        catch (Exception)
        {
            throw;
        }

        if (e.CommandName == "Upload")
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "showUploadFilePopup();", true);
        }

        if (e.CommandName == "Scan")
        {
            RedirectToScanApp();
        }
        if (e.CommandName == "DownLoad")
        {
            string CaseId = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
            string CaseNo = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO;
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "OpenDocManager("+CaseNo+","+CaseId+")", true);
        }
    }

    public DataSet GetVerification(string sz_CompanyID, string sz_input_bill_number)
    {
        //bt_operation = 2;
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strConn);
        sqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        String szConfigValue = "";
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("SP_MANAGE_VERIFICATION_ANSWER", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@sz_input_bill_number", sz_input_bill_number);
            sqlCmd.Parameters.AddWithValue("@bt_operation", 2);
            //sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", p_szProcedureGroupid);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
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

    public DataSet GetDenialInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection SqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            SqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("SP_MANAGE_GET_DENIAL_REASON", SqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
            //sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", p_szProcedureGroupid);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        return ds;
    }

    public DataSet GetPaymentInfo(string sz_CompanyID, string SZ_BILL_NUMBER)
    {
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection SqlCon = new SqlConnection(strConn);
        DataSet ds = new DataSet();
        try
        {
            SqlCon.Open();
            SqlCommand sqlCmd;
            sqlCmd = new SqlCommand("SP_MANAGE_GET_PAYMENT_DETAILS", SqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID);
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", SZ_BILL_NUMBER);
            //sqlCmd.Parameters.AddWithValue("@sz_procedure_group_id", p_szProcedureGroupid);
            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);

            da.Fill(ds);
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }
        }
        return ds;
    }

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        try
        {
            mbs.lawfirm.Bill_Sys_Patient_Details obj = new Bill_Sys_Patient_Details();
            ArrayList objAL1 = new ArrayList();
            string path = "";

            objAL1 = obj.UploadFile("Billno", ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID, fuUploadReport.FileName);
            path = objAL1[0].ToString();
            fuUploadReport.SaveAs(path+ fuUploadReport.FileName);
            // Start : Save report under document manager.

            ArrayList objAL = new ArrayList();

            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID);
            objAL.Add("Lawfirm Doc");
            objAL.Add(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);

            objAL.Add(fuUploadReport.FileName);
            objAL.Add(objAL1[1].ToString());
            objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
            objAL.Add(objAL1[2].ToString());

            string ImgId = obj.SaveDocumentData(objAL);
            usrMessage.PutMessage("File Uploaded SuccessFully");
            usrMessage.Show();
        }
        catch (Exception ex)
        {
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.PutMessage(ex.ToString());
            usrMessage.Show();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            
            ArrayList arr1 = new ArrayList();
            string szCompanyID = ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID;
           
            
            for (int i = 0; i < grdViewBill.Rows.Count; i++)
            {
                string szBillNo = grdViewBill.DataKeys[i][0].ToString();
                TextBox txt_LFCaseID = (TextBox)grdViewBill.Rows[i].FindControl("txtLFCaseID");
                TextBox txt_InexNumber = (TextBox)grdViewBill.Rows[i].FindControl("txtInexNumber");
                TextBox txt_Purchaseddate = (TextBox)grdViewBill.Rows[i].FindControl("txtPurchaseddate");
                mbs.lawfirm.LawFirmCase_DAO obj = new mbs.lawfirm.LawFirmCase_DAO();

                string sz_LF_Case_ID = txt_LFCaseID.Text;
                string sz_LF_Index = txt_InexNumber.Text;
                string sz_PurchasedDate = txt_Purchaseddate.Text;

                if (sz_PurchasedDate == null)
                {
                    sz_PurchasedDate = "";
                }

                obj.CaseID = sz_LF_Case_ID;
                obj.IndexNumber = sz_LF_Index;
                obj.DatePurchased = sz_PurchasedDate;
                obj.BillNo = szBillNo;
                obj.CompanyID = szCompanyID;
                arr1.Add(obj);
            }

            mbs.lawfirm.Bill_Sys_Patient_Details obj1 = new Bill_Sys_Patient_Details();
            int j = obj1.UpdateBillInfo(arr1);
            usrMessage.PutMessage("Your changes were made successfully to the server");
            usrMessage.Show();
        }
        catch (Exception io)
        {
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            usrMessage.PutMessage(io.ToString());
            usrMessage.Show();
        }
    }

    public void RedirectToScanApp()
    {
        mbs.lawfirm.Bill_Sys_CollectDocAndZip obj = new Bill_Sys_CollectDocAndZip();
        string companyName = obj.GetCompanyName(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID);
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
        mbs.lawfirm.Bill_Sys_Patient_Details _obj = new Bill_Sys_Patient_Details();

        string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID, "NFLAW");

        szUrl = szUrl + "&Flag=LawFirm" + "&CompanyId=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_COMAPNY_ID + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + companyName + "&CaseId=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_ID;
        szUrl = szUrl + "&PName=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&CaseNo=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_CASE_NO + "&NodeId=" + NodeId + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        // szUrl = szUrl + "&PomID=" + Session["SCANPOMID"].ToString() + "&PName=" + ((Bill_Sys_CaseObject)Session["CASE_OBJECT"]).SZ_PATIENT_NAME + "&NodeId=" + NodeId;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }

    protected void GetTotalAmount()
    {
        sqlCon = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        ds = new DataSet();
        DataTable dtTotal = new DataTable();
        DataRow drTotal;
        bool _return = false;
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_LAW_FIRM_BILLS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@I_START_INDEX", 1);
            sqlCmd.Parameters.AddWithValue("@SZ_ORDER_BY", "");
            sqlCmd.Parameters.AddWithValue("@SZ_SEARCH_TEXT", txtSearchBox.Text);
            sqlCmd.Parameters.AddWithValue("@I_END_INDEX", 100000);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_ID", txtCaseID.Text);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", txtCompanyID.Text);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            sqlda.Fill(ds);
            decimal TotalClaim=0;
            decimal TotalPaid=0;
            decimal TotalBalance=0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TotalClaim = TotalClaim + Convert.ToDecimal(ds.Tables[0].Rows[i]["CLAIM_AMOUNT"].ToString());
                TotalPaid = TotalPaid + Convert.ToDecimal(ds.Tables[0].Rows[i]["PAID_AMOUNT"].ToString());
                TotalBalance = TotalBalance + Convert.ToDecimal(ds.Tables[0].Rows[i]["BALANCE"].ToString());
            }

            dtTotal.Columns.Add("CLAIM_AMOUNT");
            dtTotal.Columns.Add("PAID_AMOUNT");
            dtTotal.Columns.Add("BALANCE");
            drTotal=dtTotal.NewRow();
            drTotal["CLAIM_AMOUNT"]=TotalClaim.ToString();
            drTotal["PAID_AMOUNT"]=TotalPaid.ToString();
            drTotal["BALANCE"] = TotalBalance.ToString();
            dtTotal.Rows.Add(drTotal);

            grdSearchTotal.DataSource = dtTotal;
            grdSearchTotal.DataBind();
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }        
    }
}
