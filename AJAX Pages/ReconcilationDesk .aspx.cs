using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DevExpress.Web;
using DevExpress.Data;
using System.Collections;


public partial class AJAX_Pages_ReconcilationDesk_ : PageBase
{
    string companyId;
    protected void Page_Load(object sender, EventArgs e)
    {
         companyId = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        // BindReconcilationDesk();
         //grdReconcilationdesk.ExpandAll();
         if (!IsPostBack)
         {
             BindCarrier();
             BindSpeciality();
             BindProvidername();
             BindLawfirm();
             BindCaseType();
             //BindReconcilationDesk();
             //grdReconcilationdesk.ExpandAll();
         }
         BindData();
    }

    protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
    {
        if (e.Column.FieldName == "Total")
        {
            //int price = (int)e.GetListSourceFieldValue("FLT_BILL_AMOUNT");
            //int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"));
            e.Value = e.GetListSourceFieldValue("FLT_BILL_AMOUNT");
        }
    }
   
    private void BindCarrier()
    {
        DataSet ds = new DataSet();
       ASPxListBox lstCarrier=(ASPxListBox) this.ddleCarrier.FindControl("lstCarrier");
       ds = loadCarrier(companyId);
       lstCarrier.TextField = "DESCRIPTION";
       lstCarrier.ValueField = "CODE";
       lstCarrier.DataSource = ds;
       lstCarrier.DataBind();
       DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All---","NA");
       lstCarrier.Items.Insert(0, Item);
       //lstCarrier.SelectedIndex = 0;
    }
    private void BindSpeciality()
    {
        DataSet ds = new DataSet();
        ASPxListBox lstSpeciality = (ASPxListBox)this.ddleSpeciality.FindControl("lstSpeciality");
        ds = loadSpeciality(companyId);
        lstSpeciality.TextField = "description";
        lstSpeciality.ValueField = "code";
        lstSpeciality.DataSource = ds;
        lstSpeciality.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All---", "NA");
        lstSpeciality.Items.Insert(0, Item);
        //lstSpeciality.SelectedIndex = 0;
    }

    private void BindProvidername()
    {
        DataSet ds = new DataSet();
        ASPxListBox lstProviderName = (ASPxListBox)this.ddleProviderName.FindControl("lstProviderName");
        ds = loadProvidername(companyId);
        lstProviderName.TextField = "DESCRIPTION";
        lstProviderName.ValueField = "CODE";
        lstProviderName.DataSource = ds;
        lstProviderName.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All---","NA");
        lstProviderName.Items.Insert(0, Item);
        //lstProviderName.SelectedIndex = 0;
    }
    private void BindCaseType()
    {
        DataSet ds = new DataSet();
        ds = loadcaseType(companyId);
        ASPxListBox lstCaseType = (ASPxListBox)this.ddleCaseType.FindControl("lstCaseType");
        lstCaseType.ValueField = "CODE";
        lstCaseType.TextField = "DESCRIPTION";
        lstCaseType.DataSource = ds;
        lstCaseType.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All ---", "NA");
        lstCaseType.Items.Insert(0, Item);
    }
    private void BindLawfirm()
    {
        DataSet ds = new DataSet();
        ds = loadLawfirm();
        ASPxListBox lstLawfirm = (ASPxListBox)this.ddleLawfirm.FindControl("lstLawfirm");
        lstLawfirm.ValueField = "CODE";
        lstLawfirm.TextField = "DESCRIPTION";
        lstLawfirm.DataSource = ds;
        lstLawfirm.DataBind();
        DevExpress.Web.ListEditItem Item = new DevExpress.Web.ListEditItem("---Select All---", "NA");
        lstLawfirm.Items.Insert(0, Item);
        //lstLawfirm.SelectedIndex = 0;
    }

    private void BindReconcilationDesk()
    {
        DataSet ds = new DataSet();
        ds = ReconcilationDeskgrid(companyId);
        grdReconcilationdesk.DataSource = ds;
        grdReconcilationdesk.DataBind();
        
    }
    public DataSet loadcaseType(string companyid)
    {//Logging Start
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
        finally
        { 
            if(conn.State==ConnectionState.Open)
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
    public DataSet loadCarrier( string companyid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strConn;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        DataSet ds=new DataSet();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_MST_INSURANCE_COMPANY", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG","INSURANCE_LIST");
            comm.Parameters.AddWithValue("@ID",companyid);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
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

    public DataSet loadSpeciality(string companyid)
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
            SqlCommand comm = new SqlCommand("SP_MST_PROCEDURE_GROUP", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG","GET_PROCEDURE_GROUP_LIST");
            comm.Parameters.AddWithValue("@id",companyid);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            da.Fill(ds);
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

    public DataSet loadProvidername(string companyid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        string strConn;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_MST_OFFICE", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@ID",companyid);
            cmd.Parameters.AddWithValue("@FLAG","OFFICE_LIST");
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            da.Fill(ds);
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

    public DataSet loadLawfirm()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds=new DataSet();
        string strConn;
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("SP_MST_LEGAL_LOGIN", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@FLAG", "GET_USER_LIST");
            SqlDataAdapter da=new SqlDataAdapter(comm);
            da.Fill(ds);
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

    public DataSet ReconcilationDeskgrid(string companyid)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds = new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();

        SqlConnection conn = new SqlConnection(strConn);

        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("sp_get_reconcile_desk", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_company_id",companyid);
            SqlDataAdapter da=new SqlDataAdapter(comm);
            da.Fill(ds);
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

    public DataSet SearchForReconcilationDesk(string Companyid,string Lawfirm,string insurance,string speciality,string provider,string casetype,string fromdt,string todt)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        DataSet ds=new DataSet();
        string strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("sp_get_reconcile_desk",conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@sz_company_id",Companyid);
            comm.Parameters.AddWithValue("@sz_lawfirm_ids",Lawfirm);
            comm.Parameters.AddWithValue("@sz_insurance_ids",insurance);
            comm.Parameters.AddWithValue("@sz_speciality",speciality);
            comm.Parameters.AddWithValue("@sz_provider",provider);
            comm.Parameters.AddWithValue("@sz_CaseTypeIDS", casetype);
            comm.Parameters.AddWithValue("@dt_reconcilation_from_date",fromdt);
            comm.Parameters.AddWithValue("@dt_reconcilation_to_date", todt);
            SqlDataAdapter da=new SqlDataAdapter(comm);
            da.Fill(ds);

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    public void BindData()
    {
        DataSet ds = new DataSet();
        string slawfirm1 = "";
        Control lawfirm = ddleLawfirm.FindControl("lstLawfirm");
        if (lawfirm != null)
        {
            string slawfirm = "";
            foreach (ListEditItem Item in ((ASPxListBox)lawfirm).SelectedItems)
            {
                if (slawfirm.Equals("") && Item.Value.ToString() != "NA")
                {
                    slawfirm = Item.Value.ToString();
                }
                else if (slawfirm != null && Item.Value.ToString() != "NA")
                {
                    slawfirm = slawfirm + "," + Item.Value.ToString();
                }
            }
            slawfirm1 = slawfirm;
        }
        string scarrier1 = "";
        Control carrier = ddleCarrier.FindControl("lstCarrier");
        if (carrier != null)
        {
            string scarrier = "";
            foreach (ListEditItem Item in ((ASPxListBox)carrier).SelectedItems)
            {
                if (scarrier.Equals("") && Item.Value.ToString() != "NA")
                {
                    scarrier = Item.Value.ToString();
                }
                else if (scarrier != null && Item.Value.ToString() != "NA")
                {
                    scarrier = scarrier + "," + Item.Value.ToString();
                }
            }
            scarrier1 = scarrier;
        }

        //Provider
        string sprovider1 = "";
        Control provider = ddleProviderName.FindControl("lstProviderName");
        if (provider != null)
        {
            string sprovider = "";
            foreach (ListEditItem Item in ((ASPxListBox)provider).SelectedItems)
            {
                if (sprovider.Equals("") && Item.Value.ToString() != "NA")
                {
                    sprovider = Item.Value.ToString();
                }
                else if (sprovider != null && Item.Value.ToString() != "NA")
                {
                    sprovider = sprovider + "," + Item.Value.ToString();
                }

            }
            sprovider1 = sprovider;
        }

        //Speciality
        string sspeciality1 = "";
        Control Speciality = this.ddleSpeciality.FindControl("lstSpeciality");
        if (Speciality != null)
        {
            string sspeciality = "";
            foreach (ListEditItem Item in ((ASPxListBox)Speciality).SelectedItems)
            {
                if (sspeciality.Equals("") && Item.Value.ToString() != "NA")
                {
                    sspeciality = Item.Value.ToString();
                }
                else if (sspeciality != null && Item.Value.ToString() != "NA")
                {
                    sspeciality = sspeciality + "," + Item.Value.ToString();
                }
            }
            sspeciality1 = sspeciality;
        }

        //CaseType
        string scasetype1 = "";
        Control Casetype = this.ddleCaseType.FindControl("lstCaseType");
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
        //string speciality = ddlSpecialty.SelectedItem.Value.ToString();
        //string Provider = ddlProviderName.SelectedItem.Value.ToString();
        string szFrmDate = "";
        string Todate = "";
        if (dtfromdate.Value != null && dttodate.Value != null)
        {
            DateTime dtFrmdate = Convert.ToDateTime(dtfromdate.Value);
            DateTime dtTodate = Convert.ToDateTime(dttodate.Value);
            szFrmDate = dtFrmdate.ToString("MM/dd/yyyy");
            Todate = dtTodate.ToString("MM/dd/yyyy");
        }
        ds = SearchForReconcilationDesk(companyId, slawfirm1, scarrier1, sspeciality1, sprovider1, scasetype1, szFrmDate, Todate);
        grdReconcilationdesk.DataSource = ds;
        grdReconcilationdesk.DataBind();
        grdReconcilationdesk.ExpandAll();
    }
    //protected void grdReconcilationdesk_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
    //{

    //}
    protected void btnETE_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            string Path = ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString() + "/";
            string fileName= "ReconcilationDesk" + DateTime.Now.ToString("ddMMyyyysstt")+".xls";
            Path = Path + fileName;
            System.IO.Stream stream = new System.IO.FileStream(Path, System.IO.FileMode.Create);
            grdExport.WriteXls(stream);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + fileName + "'); ", true);
            //PivotGridExporterDoctor.ExportXlsToResponse(@"D:\work\xls\2.xls", true);
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

    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
     //   BindGrid();
        BindData();
        string sFileName = null;
        string sFile = gb.web.utils.DownloadFilesUtils.GetExportPhysicalPath(sUserName, gb.web.utils.DownloadFilesExportTypes.RECONCILIATION_LIST, out sFileName);
        System.IO.Stream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdExport.WriteXlsx(stream);
        stream.Close();

        ArrayList list = new ArrayList();
        list.Add(gb.web.utils.DownloadFilesUtils.GetExportRelativeURL(sFileName));
        Session["Download_Files"] = list;
    }
    
}