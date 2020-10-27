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
using System.IO;
using System.Data;
using System.Data.SqlClient;

public partial class Bill_Sys_AddProcedureCode : PageBase
{
    private Bill_Sys_BillTransaction_BO _bill_Sys_BillTransaction;
    Bill_Sys_CheckoutBO _objCheckoutBO = new Bill_Sys_CheckoutBO();
    protected void Page_Init(object sender, EventArgs e)
    {
        
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string sType = browser.Type;
        string sName = browser.Browser;
        string szCSS;
        string _url = "";
        if (Request.RawUrl.IndexOf("?") > 0)
        {
            _url = Request.RawUrl.Substring(0, Request.RawUrl.IndexOf("?"));
        }
        else
        {
            _url = Request.RawUrl;
        }
        if (browser.Browser.ToLower().Contains("firefox"))
        {
            szCSS = "css/main-ff.css";
        }
        else
        {
            if (browser.Browser.ToLower().Contains("safari") || browser.Browser.ToLower().Contains("apple"))
            {
                szCSS = "css/main-ch.css";
            }
            else
            {
                szCSS = "css/main-ie.css";
            }
        }
        System.Text.StringBuilder b = new System.Text.StringBuilder();
        b.AppendLine("");
        if (_url.Contains("AJAX Pages")) { b.AppendLine("<link rel='shortcut icon' href='../Registration/icon.ico' />"); } else b.AppendLine("<link rel='shortcut icon' href='Registration/icon.ico' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/intake-sheet-ff.css' />");
        b.AppendLine("<link href='Css/mainmaster.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link href='Css/UI.css' rel='stylesheet' type='text/css' />");
        b.AppendLine("<link type='text/css' rel='stylesheet' href='css/style.css' />");
        b.AppendLine("<link href='css/style.css'  rel='stylesheet' type='text/css'  />");
        b.AppendLine("<link href='" + szCSS + "' type='text/css' rel='Stylesheet' />");
        this.masterhead.InnerHtml = b.ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Get("DoctorId").ToString() != "")
            {
                GetProcedureCode(Request.QueryString.Get("DoctorId").ToString());
            }


            DataSet ds1 = Get_Proc_Code(Request.QueryString.Get("EventId").ToString());
            for (int i = 0; i < grdProcedure.Items.Count; i++)
            {   //check checkbox value
                CheckBox chkDelete1 = (CheckBox)grdProcedure.Items[i].FindControl("chkselect");
                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    if (grdProcedure.Items[i].Cells[2].Text == ds1.Tables[0].Rows[j].ItemArray.GetValue(0).ToString())
                    {
                        chkDelete1.Checked = true;
                    }
                }
            }
        }
    }
    private void GetProcedureCode(string doctorId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            DataTable dtProcCode = new DataTable();
            _bill_Sys_BillTransaction = new Bill_Sys_BillTransaction_BO();
            grdProcedure.DataSource = _bill_Sys_BillTransaction.GetDoctorSpecialityProcedureCodeList(doctorId, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdProcedure.DataBind();
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
    protected void btnProceCode_Click(object sender, EventArgs e)
    {
        DataSet dsDelete = _objCheckoutBO.PTNotesPatientTreatmentDelete(Request.QueryString.Get("EventId").ToString());
        for (int i = 0; i < grdProcedure.Items.Count; i++)
        {
            CheckBox chkDelete1 = (CheckBox)grdProcedure.Items[i].FindControl("chkselect");
            if (chkDelete1.Checked)
            {
                string ProcId = grdProcedure.Items[i].Cells[2].Text;
                SaveValues(ProcId);
            }
        }
    }


    public void SaveValues(string index)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            Bill_Sys_CheckoutBO obj = new Bill_Sys_CheckoutBO();
            ArrayList _objArray = new ArrayList();
            _objArray.Add(index);
            _objArray.Add(Request.QueryString.Get("EventId").ToString());
            _objArray.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            save_Procedure_Codes(_objArray);
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


    public void save_Procedure_Codes(ArrayList p_objArrayList)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            String strsqlCon;
            SqlConnection conn;
            SqlDataReader dr;
            SqlCommand comm;
            strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            #region "Save Event Reffer Procedure"
            comm = new SqlCommand();
            comm.CommandText = "sp_insert_proc_code_id";
            comm.CommandType = CommandType.StoredProcedure;
            comm.Connection = conn;
            comm.Parameters.AddWithValue("@SZ_PROC_CODE", p_objArrayList[0].ToString());
            comm.Parameters.AddWithValue("@I_EVENT_ID", Convert.ToInt32(p_objArrayList[1].ToString()));
            comm.Parameters.AddWithValue("@flag", "BT_UPDATE");            
            comm.ExecuteNonQuery();
            #endregion
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

    public DataSet Get_Proc_Code(string i_Event_id)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strsqlCon;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        SqlCommand comm;
        DataSet ds;
        string ProcId = "";
        strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        ds = new DataSet();
        try
        {
            conn = new SqlConnection(strsqlCon);
            conn.Open();
            comm = new SqlCommand("sp_get_Proccode_id", conn);
            comm.Parameters.AddWithValue("@I_EVENT_ID", Request.QueryString.Get("EventId").ToString());
            comm.Parameters.AddWithValue("@flag", "getProcCode");
            comm.Parameters.AddWithValue("@SZ_DOCTOR_ID", "");
            comm.CommandType = CommandType.StoredProcedure;

            sqlda = new SqlDataAdapter(comm);
            sqlda.Fill(ds);

            if (ds.Tables[0].Rows.Count != 0)
            {
                return ds;
            }
            return ds;
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
        return ds;
        
    }
}
