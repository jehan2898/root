using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using mbs.lawfirm;

public partial class Bill_Sys_Connections : PageBase
{
    private string szCompanyID;
    private readonly int COL_PATIENT_COPY = 8;
    private readonly int COL_PATIENT_ID = 9;
    private readonly int COL_COPIED_PATIENT_ID = 10;

    private readonly int DATAKEY_GRID_PATIENT_ID = 1;
    private readonly int DATAKEY_COPIED_PATIENT_ID = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            btnCpy.Attributes.Add("onclick", "return confirm_Copy();");
            lnkmissingProcode.Visible = false;
            szCompanyID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            Session["DocReadOnly"] = true;
            txtLoginCompanyId.Text = szCompanyID;
            //code to check whether login company allowed connection or not
            DataSet ds1 = CheckItemInddl(szCompanyID);
            if (ds1.Tables[0].Rows.Count == 0)
            {
                UpdatePanelFacility.Visible = false;
                UpdatePanelSrch.Visible = false;

                usrMessage.PutMessage("You have no connections configured in your account");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

                lblMessage.Text = "You have no connections configured in your account";
                lblMessage.Visible = true;
            }
            //end of code

            // Patient Grid

            this.con.SourceGrid = grid;
            this.txtSearchBox.SourceGrid = grid;
            this.grid.Page = this.Page;
            this.grid.PageNumberList = this.con;

            // Visit Grid

            this.VisitGrid.Page = this.Page;
            this.VisitGrid.PageNumberList = this.con;

            if (!IsPostBack)
            {
                extddlBillCompany.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            }
            dvabc.Visible = false;
            tdvisit.Visible = false;
            VisitGrid.Visible = false;

            if (IsPostBack)
            {
                if (Session["SHOW_LINKS"] != null)
                {
                    string s = Session["SHOW_LINKS"].ToString();
                    if (s.ToLower().Equals("true"))
                    {
                        lnkmissingProcode.Visible = true;
                    }
                    else
                    {
                        lnkmissingProcode.Visible = false;
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
        string sTemp = "";
        System.Web.UI.WebControls.CheckBox chk = null;
        for (int i = 0; i < grid.Rows.Count; i++)
        {
            sTemp = grid.DataKeys[i][DATAKEY_COPIED_PATIENT_ID].ToString(); // get the copied patient's id
            if (sTemp != null)
            {
                sTemp = sTemp.Replace("&nbsp;", "");
                if (sTemp.Trim().Length == 0)
                {

                }
                else
                {

                    bool b = CheckCopY(sTemp);
                    if (CheckCopY(sTemp))
                    {
                        
                        HtmlAnchor an = null;
                        an = (HtmlAnchor)grid.Rows[i].FindControl("aCpy");
                        an.Visible = true;
                        chk = (System.Web.UI.WebControls.CheckBox)grid.Rows[i].FindControl("ChkCpy");
                        chk.Enabled = false;
                    }

                }
            }
            else
            {
                if (CheckCopY(sTemp))
                {
                    chk = (System.Web.UI.WebControls.CheckBox)grid.Rows[i].FindControl("ChkCpy");
                    chk.Enabled = false;
                }
            }
        }
    }

    private void BindPatients()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            hdnCompanyid.Value = extddlBillCompany.Text;
            mbs.bl.connections.MyConnectionDAO obj = new mbs.bl.connections.MyConnectionDAO();
            obj.CompanyConnectionFrom = szCompanyID;
            obj.CompanyConnectionTo = extddlBillCompany.Text;
            obj.CaseID = "";
            mbs.bl.connections.MyConnections objService = new mbs.bl.connections.MyConnections();

            if (extddlBillCompany.Text != "NA")
            {
                objService.GetConnectingPatientInformation(obj);
                objService.GetIsCopyAllowed(obj);
            }
            if (objService.isShowVisits == true)
            {
                //Missing procedure link gets visible
                lnkmissingProcode.Visible = true;
                Session["SHOW_LINKS"] = "true";
            }
            else
            {
                lnkmissingProcode.Visible = false;
                Session["SHOW_LINKS"] = "false";
            }
            if (objService.isCopyAllowed == true)
            {
                //copy button get visible
                btnCpy.Visible = true;
                grid.Columns[COL_PATIENT_COPY].Visible = true;
            }
            else
            {
                btnCpy.Visible = false;
                grid.Columns[COL_PATIENT_COPY].Visible = false;
            }

            pnlSrch.Visible = true;
            txtCompanyId.Text = extddlBillCompany.Text;
            txtLoginCompanyId.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            grid.XGridBindSearch();

            if (extddlBillCompany.Text == "NA")
            {
                pnlSrch.Visible = false;
                lnkmissingProcode.Visible = false;
                btnCpy.Visible = false;
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
    //protected void extddlBillCompany_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindPatients();
    //}

    protected void btnCpy_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            mbs.bl.connections.MyConnectionDAO obj = new mbs.bl.connections.MyConnectionDAO();
            obj.CompanyConnectionFrom = szCompanyID;
            obj.CompanyConnectionTo = extddlBillCompany.Text;

            // get the userid from the session
            obj.UserID = "";
            Bill_Sys_UserObject objUser = (Bill_Sys_UserObject)Session["USER_OBJECT"];

            mbs.bl.connections.MyConnections objService = new mbs.bl.connections.MyConnections();

            ArrayList objList = new ArrayList();
            mbs.bl.connections.MyConnectionDAO objL = null;

            System.Web.UI.WebControls.CheckBox chk = null;

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                chk = (System.Web.UI.WebControls.CheckBox)grid.Rows[i].FindControl("ChkCpy");
                if (chk.Checked == true)
                {
                    objL = new mbs.bl.connections.MyConnectionDAO();
                    objL.PatientID = grid.DataKeys[i][DATAKEY_GRID_PATIENT_ID].ToString(); // get patient id

                    objL.CompanyConnectionFrom = szCompanyID; // this company is allowed to copy from CompanyConnectionTo
                    objL.CompanyConnectionTo = extddlBillCompany.Text; // patient from this company will be copied to CompanyConnectionFrom
                    objL.UserID = objUser.SZ_USER_ID; // get user id
                    objList.Add(objL);
                }
            }

            string status= objService.CopyPatientToConnection(objList);
            BindPatients();
            if (status == "0")
            {
                usrMessage.PutMessage(" Selected patient(s) have been copied to your account");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            }
            else
            {
                usrMessage.PutMessage(" Selected patient(s) already exists in your account");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            }
            usrMessage.Show();
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

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grid.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);
    }

    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (e.CommandName.ToString() == "CaseNO")
            {
                mbs.bl.connections.MyConnectionDAO obj = new mbs.bl.connections.MyConnectionDAO();
                obj.CaseID = e.CommandArgument.ToString();
                obj.CompanyConnectionFrom = szCompanyID;
                obj.CompanyConnectionTo = extddlBillCompany.Text;

                mbs.bl.connections.MyConnections objService = new mbs.bl.connections.MyConnections();
                DtlView.DataSource = objService.GetConnectingPatientInformation(obj);
                DtlView.DataBind();

                if (objService.isShowVisits == true)
                {
                    //Visit grid set to bind
                    txtcaseId.Text = obj.CaseID;
                    txtLoginCompanyId.Text = obj.CompanyConnectionFrom;
                    txtCompanyId.Text = obj.CompanyConnectionTo;

                    tdvisit.Visible = true;
                    VisitGrid.XGridBind();
                    VisitGrid.Visible = true;
                }
                dvabc.Visible = true;
                txtCompanyId.Text = extddlBillCompany.Text;
            }
            int index = 0;
            if (e.CommandName.ToString() == "ClientSideButton")
            {
                string caseid = "", caseno = "";

                index = Convert.ToInt32(e.CommandArgument.ToString());
                caseid = grid.DataKeys[index][0].ToString();
                caseno = grid.DataKeys[index][3].ToString();

                Session["DocreadOnly"] = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "MMss1231", "<script type='text/javascript'>window.open('../Document Manager/case/vb_CaseInformation.aspx?caseid=" + caseid + "&caseno=" + caseno + "', 'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');</script>", false);

            }

            if (e.CommandName.ToString()=="COPYDOC")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "click", "showCopyDocPopup();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "MMss1231", "showCopyDocPopup();", false);
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

    public DataSet CheckItemInddl(string connectionFrom)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        String strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon = null;
        SqlCommand sqlCmd;
        SqlDataAdapter sqlda;
        DataSet ds = null;
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_GET_COMPANY_CONNECTIONS", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@ID", connectionFrom);
            sqlCmd.Parameters.AddWithValue("@FLAG", "");
            sqlda = new SqlDataAdapter(sqlCmd);
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
            if (sqlCon != null)
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
        return ds;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    public Boolean CheckCopY(string szCopiedPatientId)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        String strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon = null;
        SqlCommand sqlCmd;
        SqlDataReader dr;
        bool b = false;
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand("SP_CHECK_COPIED", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@SZ_COPIED_FROM_PATIENT", szCopiedPatientId);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPPANY_ID", txtLoginCompanyId.Text);
            dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                b = true;
            }
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
            if (sqlCon != null)
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }

        return b;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void txtUpdate_Click(object sender, EventArgs e)
    {
        int Page_index = grid.PageNumberList.SelectedIndex;
        grid.PageIndex = Page_index;
        grid.XGridBind();
        con.SelectedIndex = Page_index;
        //img.Visible = false;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindPatients();
    }
}
