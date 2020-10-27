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
using Componend;
using System.Data;
using System.Data.SqlClient;

public partial class AJAX_Pages_NinetyDaysBillingReport : PageBase
{
    string sz_CompanyID = "";
    SqlConnection sqlCon;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.con.SourceGrid = grdNinetyDaysReport;
        this.txtSearchBox.SourceGrid = grdNinetyDaysReport;
        this.grdNinetyDaysReport.Page = this.Page;
        this.grdNinetyDaysReport.PageNumberList = this.con;

        btnLitigate.Attributes.Add("onclick", "return chkLitigate();");
        btnLitigateAll.Attributes.Add("onclick", "return chkLitigateAll();");
        
    //    btnSearch.Attributes.Add("onclick", "return chkForSearch();");
     

       // txtCompId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
       
     //   txtOffice.Text = "New Jersey";
        
        sz_CompanyID = ((Bill_Sys_BillingCompanyObject)(Session["BILLING_COMPANY_OBJECT"])).SZ_COMPANY_ID.ToString();
        //lstMenu.DataSource = _objMenu.GetChildMenu(Convert.ToInt32(extCIddlMainMenu.Text));
        if (!Page.IsPostBack)
        {
            lstMenu.DataSource = Get_Ofiice_Detail(sz_CompanyID);
            lstMenu.DataTextField = "OFFICE";
            lstMenu.DataValueField = "SZ_OFFICE_ID";
            lstMenu.DataBind();
            btnLitigate.Visible = false;
            btnLitigateAll.Visible = false;

            hdlshow.Value = "false";

        }
        string office2, days, chBalance, searchtext;
        office2 = txtOffice.Text;
        chBalance = txtchkBalance.Text;
        days = txtDays.Text;
        searchtext = txtSearchBox.Text;
        if (office2 != "")
        {
            DataSet ds1 = new DataSet();
            ds1 = GetSearchTotal(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, office2, days, chBalance, searchtext);


            DataTable dt = new DataTable();

            dt.Columns.Add("Bill_Amount");
            dt.Columns.Add("Paid_Amount");
            dt.Columns.Add("Outstanding_Amount");
            DataRow dr;
            dr = dt.NewRow();
            //dr["Text"] = "Total Bill Amount";

            if (ds1.Tables[0].Rows[0][1].ToString() == "")
            {
                dr["Bill_Amount"] = "$0";
            }
            else
            {
                dr["Bill_Amount"] = "$" + ds1.Tables[0].Rows[0][1].ToString();
            }
            if (ds1.Tables[0].Rows[0][3].ToString() == "")
            {
                dr["Paid_Amount"] = "$0";
            }
            else
            {
                dr["Paid_Amount"] = "$" + ds1.Tables[0].Rows[0][3].ToString();
            }
            if (ds1.Tables[0].Rows[0][5].ToString() == "")
            {
                dr["Outstanding_Amount"] = "$0";
            }
            else
            {
                dr["Outstanding_Amount"] = "$" + ds1.Tables[0].Rows[0][5].ToString();
            }
          
            dt.Rows.Add(dr);


            grdSearchTotal.DataSource = dt;

            grdSearchTotal.DataBind();
        }
        
    

    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdNinetyDaysReport.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }
    protected void btnLitigate_onclick(object sender, EventArgs e)
    {
        
        string szListOfBillIDs = "";
        ArrayList objAL = new ArrayList();
        Boolean flag = false;
        for (int i = 0; i < grdNinetyDaysReport.Rows.Count; i++)
        {

           string str=   grdNinetyDaysReport.Rows[i].Cells[2].Text;
            //check checkbox value
            CheckBox chkDelete1 = (CheckBox)grdNinetyDaysReport.Rows[i].FindControl("ChkDelete");
            if (chkDelete1.Checked)
            {
                if (flag == false)
                {   //only for 1st time 
                    szListOfBillIDs = "'" + grdNinetyDaysReport.DataKeys[i]["Bill_no"].ToString() + "'";
                    flag = true;
                }
                else
                {
                    szListOfBillIDs = szListOfBillIDs + ",'" + grdNinetyDaysReport.DataKeys[i]["Bill_no"].ToString() + "'";
                }
            }
        }
        if (szListOfBillIDs != "")
        { 
            // taking all bill no to update there bill status
            objAL.Add(szListOfBillIDs);
            objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            updateBillStatus1(objAL);
            string  strDay = txtDays.Text;
            txtDays.Text = strDay;
            grdNinetyDaysReport.XGridBindSearch();


            //lblTotalBillAmount.Text = " Total Bill Amount = $" + Convert.ToString(Math.Round(Convert.ToDecimal(dt.Rows[0][0].ToString()), 2));
            //lblOutSratingAmount.Text = "Total Outstanding Amount = $" + Convert.ToString(Math.Round(Convert.ToDecimal(dt.Rows[0][1].ToString()), 2));

            //usrMessage.PutMessage("Bill status updated successfully.");
            //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            //usrMessage.Show();

        }
    }

    public void updateBillStatus1(ArrayList objAL)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection sqlCon;
        SqlCommand sqlCmd;
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strsqlCon);
        try
        {
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_UPDATE_BILL_STATUS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_ID_LIST", "'"+objAL[0].ToString()+"'");
            sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", "LT");
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[1].ToString());
           
            sqlCmd.ExecuteNonQuery();
        }
        catch (SqlException ex)
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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    
    public DataTable Get_Ofiice_Detail(string CompanyId)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataTable ds = new DataTable();

        try
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;


            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            
          
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Get Office Detail"
            comm = new SqlCommand();
            comm.CommandTimeout = 0;
            comm.CommandText = "sp_get_office_detail_from_user";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", CompanyId);
            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            if (ds.Rows.Count != 0)
            {
                return ds;
            }


            #endregion
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            using (Utils utility = new Utils())
            {
                utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
            }
            string str2 = "Error Request ID=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return ds;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //tblSrch.Visible = true;
        string office = hfselectedNodeinListbox.Value.Substring(0,hfselectedNodeinListbox.Value.LastIndexOf(','));
        
        string officeText = hfselectedNodeTextinListbox.Value;
        string[] officevalue,officetext;
       
        txtCompId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        officevalue = hfselectedNodeinListbox.Value.Split(',');
        string str = "";
        string code1 = "";
        str = txtDays.Text;
        txtDays.Text = str;
        officetext = hfselectedNodeTextinListbox.Value.Split(';');

        for (int i = 0; officevalue.Length > i; i++)
        {
            if (i == 0)
            {
                code1 = "'" + officevalue[i].ToString();// +"''," + code1;

            }
            else
            {
                code1 = code1 + "','" + officevalue[i].ToString();
            }
            // code1 = "''' " + code[i].ToString() + "''," + code1;
        }
        if (code1 != "")
        {
            code1 = code1 + "'";
            txtOffice.Text = code1;
        }
        lstMenusToRole.Items.Clear();
        lstMenu.Items.Clear();
        lstMenu.DataSource = Get_Ofiice_Detail(sz_CompanyID);
        lstMenu.DataTextField = "OFFICE";
        lstMenu.DataValueField = "SZ_OFFICE_ID";
        lstMenu.DataBind();

        for (int i = 0; i < officevalue.Length; i++)
        {
            if (officevalue[i] != "")
            {
                ListItem l = new ListItem(officetext[i], officevalue[i]);
                lstMenu.Items.Remove(l);
                lstMenusToRole.Items.Add(l);
                lstMenusToRole.DataBind();
                //txtOffice.Text = txtOffice.Text + officevalue[i];
            }
        }
     
        //lstMenusToRole.DataBind();
        if (chkBalance.Checked == true)
        {
            txtchkBalance.Text = "1";
            showvisible(Convert.ToBoolean(hdlshow.Value.ToString()));
            grdNinetyDaysReport.XGridBindSearch();

            string office2, days, chBalance, searchtext;
            office2 = txtOffice.Text;
            chBalance = txtchkBalance.Text;
            days = txtDays.Text;
            searchtext = txtSearchBox.Text;
            DataSet ds1 = new DataSet();
            ds1 = GetSearchTotal(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, office2, days, chBalance, searchtext);
            DataTable dt = new DataTable();

            dt.Columns.Add("Bill_Amount");
            dt.Columns.Add("Paid_Amount");
            dt.Columns.Add("Outstanding_Amount");
            DataRow dr;
            dr = dt.NewRow();
            //dr["Text"] = "Total Bill Amount";

            if (ds1.Tables[0].Rows[0][1].ToString() == "")
            {
                dr["Bill_Amount"] = "$0";
            }
            else
            {
                dr["Bill_Amount"] = "$" + ds1.Tables[0].Rows[0][1].ToString();
            }
            if (ds1.Tables[0].Rows[0][3].ToString() == "")
            {
                dr["Paid_Amount"] = "$0";
            }
            else
            {
                dr["Paid_Amount"] = "$" + ds1.Tables[0].Rows[0][3].ToString();
            }
            if (ds1.Tables[0].Rows[0][5].ToString() == "")
            {
                dr["Outstanding_Amount"] = "$0";
            }
            else
            {
                dr["Outstanding_Amount"] = "$" + ds1.Tables[0].Rows[0][5].ToString();
            }
          

            dt.Rows.Add(dr);


            grdSearchTotal.DataSource = dt;

            grdSearchTotal.DataBind();

        }
        else
        {
            txtchkBalance.Text = "";
            showvisible(Convert.ToBoolean(hdlshow.Value.ToString()));
            grdNinetyDaysReport.XGridBindSearch();

            string office2, days, chBalance, searchtext;
            office2 = txtOffice.Text;
            chBalance = txtchkBalance.Text;
            days = txtDays.Text;
            searchtext = txtSearchBox.Text;
            DataSet ds1 = new DataSet();
            ds1=GetSearchTotal(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, office2, days, chBalance, searchtext);


            //DataTable dt = new DataTable();
            //dt.Columns.Add("Text");
            //dt.Columns.Add("Amount");
            //DataRow dr;
            //dr = dt.NewRow();
            //dr["Text"] = "Total Bill Amount";
            //dr["Amount"] = "$" + ds1.Tables[0].Rows[0][1].ToString();
            //dt.Rows.Add(dr);

            //DataRow dr1;
            //dr1 = dt.NewRow();
            //dr1["Text"] = "Total Paid";
            //dr1["Amount"] = "$" + ds1.Tables[0].Rows[0][3].ToString();
            //dt.Rows.Add(dr1);

            //DataRow dr2;
            //dr2 = dt.NewRow();
            //dr2["Text"] = "Total Outstanding";
            //dr2["Amount"] = "$" + ds1.Tables[0].Rows[0][5].ToString();
            //dt.Rows.Add(dr2);
            //grdSearchTotal.DataSource = dt;

            //grdSearchTotal.DataBind();

            DataTable dt = new DataTable();
            dt.Columns.Add("Bill_Amount");
            dt.Columns.Add("Paid_Amount");
            dt.Columns.Add("Outstanding_Amount");
            DataRow dr;
            dr = dt.NewRow();
            //dr["Text"] = "Total Bill Amount";
          
            if (ds1.Tables[0].Rows[0][1].ToString() == "")
            {
                dr["Bill_Amount"] = "$0";
            }
            else
            {
                dr["Bill_Amount"] = "$" + ds1.Tables[0].Rows[0][1].ToString();
            }
            if (ds1.Tables[0].Rows[0][3].ToString()=="")
            {
                dr["Paid_Amount"] = "$0";
            }else
	        {
                dr["Paid_Amount"] = "$" + ds1.Tables[0].Rows[0][3].ToString();
	        }
            if (ds1.Tables[0].Rows[0][5].ToString()=="")
            {
                dr["Outstanding_Amount"] = "$0"; 
            }
            else
            {
                dr["Outstanding_Amount"] = "$" + ds1.Tables[0].Rows[0][5].ToString();
            }
          
            dt.Rows.Add(dr);


            grdSearchTotal.DataSource = dt;

            grdSearchTotal.DataBind();
        }
        if (grdNinetyDaysReport.Rows.Count == 0)
        {
            btnLitigate.Visible = false;
            btnLitigateAll.Visible = false;
        }
        else
        {
            btnLitigate.Visible = true;
            btnLitigateAll.Visible = true;
        }

       

    }

    //public DataSet litigateAll(string sz_day, string officeId, string compId,string chkBal)
    //{
    //    try
    //    {
    //        String strsqlCon;
    //        SqlConnection conn;
    //        SqlDataAdapter sqlda;
    //        SqlCommand comm;
    //        DataSet ds;

    //        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
    //        ds = new DataSet();

    //        conn = new SqlConnection(strsqlCon);
    //        conn.Open();
    //        #region "records from office and days"
    //        comm = new SqlCommand();
    //        comm.CommandText = "sp_get_bills_to_litigate_all";
    //        comm.CommandType = CommandType.StoredProcedure;
    //        comm.Connection = conn;
    //        comm.CommandTimeout = 0;
    //        comm.Parameters.AddWithValue("@SZ_COMPANY_ID", compId);
    //        comm.Parameters.AddWithValue("@SZ_OFFICE_ID", officeId);
    //        comm.Parameters.AddWithValue("@sz_Day", sz_day);
    //        comm.Parameters.AddWithValue("@bt_flt_balance", chkBal);
            
    //        sqlda = new SqlDataAdapter(comm);
    //        sqlda.Fill(ds);

    //        if (ds.Tables[0].Rows.Count!= 0)
    //        {
    //            return ds;
    //        }
    //        return ds;

    //        #endregion
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

    protected void btnLitigateAll_onclick(object sender, EventArgs e)
    {
        //DataSet ds = new DataSet();
        
        if (chkBalance.Checked == true)
        {
             litigateAll(txtDays.Text, txtOffice.Text, sz_CompanyID,"1",txtSearchBox.Text);

             DataSet ds1 = new DataSet();
             ds1 = GetSearchTotal(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txtOffice.Text, txtDays.Text, "1", txtSearchBox.Text);

             DataTable dt = new DataTable();
             dt.Columns.Add("Bill_Amount");
             dt.Columns.Add("Paid_Amount");
             dt.Columns.Add("Outstanding_Amount");
             DataRow dr;
             dr = dt.NewRow();
             //dr["Text"] = "Total Bill Amount";

             if (ds1.Tables[0].Rows[0][1].ToString() == "")
             {
                 dr["Bill_Amount"] = "$0";
             }
             else
             {
                 dr["Bill_Amount"] = "$" + ds1.Tables[0].Rows[0][1].ToString();
             }
             if (ds1.Tables[0].Rows[0][3].ToString() == "")
             {
                 dr["Paid_Amount"] = "$0";
             }
             else
             {
                 dr["Paid_Amount"] = "$" + ds1.Tables[0].Rows[0][3].ToString();
             }
             if (ds1.Tables[0].Rows[0][5].ToString() == "")
             {
                 dr["Outstanding_Amount"] = "$0";
             }
             else
             {
                 dr["Outstanding_Amount"] = "$" + ds1.Tables[0].Rows[0][5].ToString();
             }
          
             dt.Rows.Add(dr);


             grdSearchTotal.DataSource = dt;

             grdSearchTotal.DataBind();

            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    updateBillStatus(ds);
            //}
        }
        else
        {
             litigateAll(txtDays.Text, txtOffice.Text, sz_CompanyID,"",txtSearchBox.Text);

             DataSet ds1 = new DataSet();
             ds1 = GetSearchTotal(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, txtOffice.Text, txtDays.Text, "", txtSearchBox.Text);

             DataTable dt = new DataTable();
             dt.Columns.Add("Bill_Amount");
             dt.Columns.Add("Paid_Amount");
             dt.Columns.Add("Outstanding_Amount");
             DataRow dr;
             dr = dt.NewRow();
             //dr["Text"] = "Total Bill Amount";

             if (ds1.Tables[0].Rows[0][1].ToString() == "")
             {
                 dr["Bill_Amount"] = "$0";
             }
             else
             {
                 dr["Bill_Amount"] = "$" + ds1.Tables[0].Rows[0][1].ToString();
             }
             if (ds1.Tables[0].Rows[0][3].ToString() == "")
             {
                 dr["Paid_Amount"] = "$0";
             }
             else
             {
                 dr["Paid_Amount"] = "$" + ds1.Tables[0].Rows[0][3].ToString();
             }
             if (ds1.Tables[0].Rows[0][5].ToString() == "")
             {
                 dr["Outstanding_Amount"] = "$0";
             }
             else
             {
                 dr["Outstanding_Amount"] = "$" + ds1.Tables[0].Rows[0][5].ToString();
             }
          
             dt.Rows.Add(dr);


             grdSearchTotal.DataSource = dt;

             grdSearchTotal.DataBind();
            //if (ds.Tables[0].Rows.Count != 0)
            //{
            //    updateBillStatus(ds);
            //}
        }
       
    }
    public void updateBillStatus(DataSet ds)
    {
        string szBillIDs = "";
        ArrayList obj = new ArrayList();
        Boolean flagAll = false;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (flagAll == false)
            {   //only for 1st time 
                szBillIDs = "'" + ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + "'"; 
                flagAll = true;
               
            }
            else
            {
                szBillIDs = szBillIDs + ",'" + ds.Tables[0].Rows[i].ItemArray.GetValue(0).ToString() + "'"; 
            }
        }
        if (szBillIDs != "")
        {
            // taking all bill no to update there bill status
            obj.Add(szBillIDs);
            obj.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            updateBillStatus1(obj);
            string strDay = txtDays.Text;
            txtDays.Text = strDay;
            grdNinetyDaysReport.XGridBindSearch();

        }
    }

    public DataSet GetSearchTotal(string companyid, string office,string days, string chBalance, string searchtext)
    {
        //Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_90DAYS_TOTAL", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;

            sqlCmd.Parameters.AddWithValue("@sz_company_id", companyid);
            sqlCmd.Parameters.AddWithValue("@sz_office", office);
            sqlCmd.Parameters.AddWithValue("@sz_Day", days);
            sqlCmd.Parameters.AddWithValue("@bt_flt_balance", chBalance);
            sqlCmd.Parameters.AddWithValue("@sz_search_text", searchtext);

            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
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
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();
            }
        }
        return ds;
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public int litigateAll(string sz_day, string officeId, string compId, string chkBal,string sz_search_text)
    {
            String strsqlCon;
            SqlConnection conn;
            SqlDataAdapter sqlda;
            SqlCommand comm;
            DataSet ds;
            int ireturn = 0;

            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            ds = new DataSet();

            conn = new SqlConnection(strsqlCon);
            try

            {


             
                conn.Open();
                #region "records from office and days"
                comm = new SqlCommand();
                comm.CommandText = "sp_litigate_all_for_ninety_days_report";
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                comm.CommandTimeout = 0;
                comm.Parameters.AddWithValue("@sz_company_id", compId);
                comm.Parameters.AddWithValue("@sz_office", officeId);
                comm.Parameters.AddWithValue("@sz_Day", sz_day);
                comm.Parameters.AddWithValue("@bt_flt_balance", chkBal);
                comm.Parameters.AddWithValue("@sz_search_text", sz_search_text);

                ireturn = comm.ExecuteNonQuery();

                grdNinetyDaysReport.XGridBindSearch();
                #endregion
            }
            catch (Exception ex)
            {
                ireturn = 0;
                throw;
            }
            finally
            {
                if (conn.State==ConnectionState.Open)
                {
                    conn.Close(); 
                }
                //
            }
            return ireturn;
    }
    private void showvisible(bool showflag)
    {
        grdNinetyDaysReport.Columns[5].Visible = showflag;
        grdNinetyDaysReport.Columns[6].Visible = showflag;
        grdNinetyDaysReport.Columns[7].Visible = showflag;
        grdNinetyDaysReport.Columns[8].Visible = showflag;
        grdNinetyDaysReport.Columns[9].Visible = showflag;
        grdNinetyDaysReport.Columns[10].Visible = showflag;
        grdNinetyDaysReport.Columns[15].Visible = showflag;
        grdNinetyDaysReport.Columns[17].Visible = showflag;
        grdNinetyDaysReport.Columns[18].Visible = showflag;
        grdNinetyDaysReport.Columns[19].Visible = showflag;
        grdNinetyDaysReport.Columns[20].Visible = showflag;
        grdNinetyDaysReport.Columns[21].Visible = showflag;
        grdNinetyDaysReport.Columns[22].Visible = showflag;
        grdNinetyDaysReport.Columns[23].Visible = showflag;
  
    }

    protected void lnkshow_onclick(object sender, EventArgs e)
    {
       // grdNinetyDaysReport.XGridBindSearch();
        if (hdlshow.Value != "")
        {
            if (hdlshow.Value.ToString() == "false")
            {
                hdlshow.Value = "";
                hdlshow.Value = "true";
                lnkshow.Text = "Hide Details";
            }
            else
            {
                hdlshow.Value = "";
                hdlshow.Value = "false";
                lnkshow.Text = "Show Details";
            }
        }
        else
        {
            hdlshow.Value = "";
            hdlshow.Value = "false";
            lnkshow.Text = "Show Details";
        }
    }
}

