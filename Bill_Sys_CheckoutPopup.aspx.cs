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
using System.Data.SqlClient;
using log4net;

public partial class Bill_Sys_CheckoutPopup : PageBase
{
    Bill_Sys_CheckoutBO _objCheckoutBO;
    private static ILog log = LogManager.GetLogger("Bill_Sys_CheckoutPopup");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CHECK_IN_CASE_ID"] = Request.QueryString["CaseID"].ToString();
            BindDoctorListGird();
        }
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_CheckoutPopup.aspx");
        }
        #endregion

    }

    public void BindDoctorListGird()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            _objCheckoutBO = new Bill_Sys_CheckoutBO();
            grdDoctorList.DataSource = _objCheckoutBO.getCheckinList(Session["CHECK_IN_CASE_ID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            grdDoctorList.DataBind();
            //<........ Code 1: added code to show the links of 'CH Referal','ot referal','pt referal','emg' if the value in the database is true> by anand
            for (int i = 0; i < grdDoctorList.Items.Count; i++)
            {
                if (grdDoctorList.Items[i].Cells[5].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text!="Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkChIEReferal")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[6].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkOTIEReferal")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[7].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkPTIEReferal")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[8].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkIEEMG")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[9].Text.Equals("0") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkIESupplies")).Visible = false;
                }


                // Links for Followup

                if (grdDoctorList.Items[i].Cells[10].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkChFUReferal")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[11].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkOTFUReferal")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[12].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkPTFUReferal")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[13].Text.Equals("False") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkFUEMG")).Visible = false;
                }
                if (grdDoctorList.Items[i].Cells[14].Text.Equals("0") || grdDoctorList.Items[i].Cells[3].Text != "Completed")
                {
                    ((LinkButton)grdDoctorList.Items[i].Cells[9].FindControl("lnkFUSupplies")).Visible = false;
                }
                 
            }
            //<........Code 1: Ends>
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.Message.ToString());
            log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.InnerException.StackTrace.ToString());
            }
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

    protected void btnRemove_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"].ToString());
        SqlCommand cmd;
        bool blFound = false;

        for (int i = 0; i < grdDoctorList.Items.Count; i++)
        {
            if (((CheckBox)grdDoctorList.Items[i].Cells[0].FindControl("chkSelect")).Checked == true)
            {

                string lblstatus = grdDoctorList.Items[i].Cells[3].Text;
                if (lblstatus == "Completed")
                {
                    blFound = true;
                }
                else
                {
                    string IEVENTID = grdDoctorList.Items[i].Cells[4].Text;
                    try
                    {
                        conn.Open();
                        cmd = new SqlCommand("SP_DELETE_CHECKIN_LIST", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@I_EVENT_ID", IEVENTID);
                        cmd.ExecuteNonQuery();
                        lblCheckinMsg.Visible = false;
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
                    finally
                    {
                        if (conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        if (blFound)
        {
            lblCheckinMsg.Text = "You can't Remove Record because Status is Completed..";
            lblCheckinMsg.Visible = true;
        }
        BindDoctorListGird();
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }


    protected void grdDoctorList_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName == "Open PDF")
            {
                String szFilePath = e.CommandArgument.ToString();
                Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szFilePath + "'); ", true);
            }
        }
        catch (Exception ex)
        {
            log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.Message.ToString());
            log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.StackTrace.ToString());
            if (ex.InnerException != null)
            {
                log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.InnerException.Message.ToString());
                log.Debug("Bill_Sys_CheckoutPopup. Method - BindDoctorList : " + ex.InnerException.StackTrace.ToString());
            }
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
}
