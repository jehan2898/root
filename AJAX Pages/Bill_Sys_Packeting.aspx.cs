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
using RequiredDocuments;
using System.Text;
using System.IO;

public partial class Bill_Sys_Packeting : PageBase
{
    Bill_Sys_RequiredDocumentBO _ReqDocBO;
    REQUIREDDOCUMENT_EO _ReqDocEO;
    string PacketID;
    Bill_Sys_RequiredDocumentBO objDocumentBO = new Bill_Sys_RequiredDocumentBO();

    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            this.con.SourceGrid = grdPacketing;
            this.txtSearchBox.SourceGrid = grdPacketing;
            this.grdPacketing.Page = this.Page;
            this.grdPacketing.PageNumberList = this.con;
            this.Title = "Bill Packet";


            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlServiceDateValues.Attributes.Add("onChange", "javascript:SetServiceDate();");

            Bill_Sys_RequiredDocumentBO objDocumentBO = new Bill_Sys_RequiredDocumentBO();
            PacketID = objDocumentBO.Get_PacketId(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            if (!IsPostBack)
            {
                ddlDateValues.SelectedValue = "1";
                txtFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                extddlBillStatus.Flag_ID = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;

                txtPacketId.Text = objDocumentBO.Get_PacketId(txtCompanyID.Text);
                extddlSpeciality.Flag_ID = txtCompanyID.Text;


                if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
                {

                    grdPacketing.Columns[2].Visible = false;
                }
                BindGrid();
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

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdPacketing.XGridBindSearch();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            BindGrid();
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

    protected void grdPacketing_RowCommand(object source, GridViewCommandEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        _ReqDocBO = new Bill_Sys_RequiredDocumentBO();
        _ReqDocEO = new REQUIREDDOCUMENT_EO();
        ArrayList objarr = new ArrayList();
        string szOpenFilePath = "";
        string _CompanyName = "";
        int RowIndex;

        try
        {
            #region "Create Packet"
            if (e.CommandName.ToString() == "CreatePacket")
            {
                RowIndex = Convert.ToInt32(e.CommandArgument.ToString());

                if (!txtPacketId.Text.Equals(""))
                {
                    _CompanyName = Convert.ToString(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
                    objarr.Add(grdPacketing.DataKeys[RowIndex][0].ToString());//caseID
                    objarr.Add(grdPacketing.DataKeys[RowIndex][1].ToString());//Bill Number
                    objarr.Add(txtCompanyID.Text);//companyID
                    objarr.Add(_CompanyName);//companyName
                    _ReqDocEO = _ReqDocBO.CheckExists(objarr);
                    Session["PacketDoc"] = objarr;
                    Session["BillNo"] = grdPacketing.DataKeys[RowIndex][1].ToString();


                    if (_ReqDocEO.SZ_ERROR_MSG != "")
                    {
                        msgPatientExists.InnerHtml = _ReqDocEO.SZ_ERROR_MSG;
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "openExistsPage();", true);
                        //Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
                    }
                    else
                    {
                        _ReqDocEO = _ReqDocBO.MergeDocument(objarr);
                        ///set paket bill status id to bill number --atul
                        Bill_Sys_RequiredDocumentBO objDocumentBO1 = new Bill_Sys_RequiredDocumentBO();
                        objDocumentBO1.Set_PacketId(txtCompanyID.Text, grdPacketing.DataKeys[RowIndex][1].ToString(), txtPacketId.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        BindGrid();
                    }

                    if (_ReqDocEO.SZ_OPEN_FILE_PATH != "")
                    {
                        szOpenFilePath = _ReqDocEO.SZ_OPEN_FILE_PATH;
                        szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szOpenFilePath;
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                    }
                }
                else
                {
                    msgPatientExists.InnerHtml = "You don't have bill status for packeting,you can not create packet.Please enter bill status for packeting.";
                    Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
                }
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
            string str2 = "Error Request=" + id + ".Please share with Technical support.";
            base.Response.Redirect("../Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }

        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (Session["PacketDoc"] != null)
            {
                _ReqDocBO = new Bill_Sys_RequiredDocumentBO();
                _ReqDocEO = new REQUIREDDOCUMENT_EO();
                ArrayList objarr = new ArrayList();
                string szOpenFilePath = "";
                objarr = (ArrayList)(Session["PacketDoc"]);

                _ReqDocEO = _ReqDocBO.MergeDocument(objarr);

                if (_ReqDocEO.SZ_OPEN_FILE_PATH != "")
                {
                    szOpenFilePath = _ReqDocEO.SZ_OPEN_FILE_PATH;
                    szOpenFilePath = ApplicationSettings.GetParameterValue("DocumentManagerURL") + szOpenFilePath;
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "window.open('" + szOpenFilePath.ToString() + "');", true);
                }
                string szBillNO = Session["BillNo"].ToString();
                Bill_Sys_RequiredDocumentBO objDocumentBO1 = new Bill_Sys_RequiredDocumentBO();
                objDocumentBO1.Set_PacketId(txtCompanyID.Text, szBillNO, txtPacketId.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                BindGrid();
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

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue("FETCHEXCEL_SHEET") + grdPacketing.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "ClearFields()", true);
    }
}

