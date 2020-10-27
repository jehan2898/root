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

public partial class Bill_Sys_Packeting1 : PageBase
{
    Bill_Sys_RequiredDocumentBO _ReqDocBO;
    REQUIREDDOCUMENT_EO _ReqDocEO;
    string PacketID;
    Bill_Sys_RequiredDocumentBO objDocumentBO = new Bill_Sys_RequiredDocumentBO();
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
        Bill_Sys_RequiredDocumentBO objDocumentBO = new Bill_Sys_RequiredDocumentBO();
        PacketID = objDocumentBO.Get_PacketId(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        if (!IsPostBack)
        {
            ddlDateValues.SelectedValue = "1";
            txtFromDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtToDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            
            txtPacketId.Text = objDocumentBO.Get_PacketId(txtCompanyID.Text);
            extddlSpeciality.Flag_ID = txtCompanyID.Text;
   

            if (((Bill_Sys_SystemObject)Session["SYSTEM_OBJECT"]).SZ_CHART_NO == "0")
            {
                
                grdPacketing.Columns[3].Visible=false;
            }
            BindGrid();
        }
    }

    #region "ExportToExcel"
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            ExportToExcel();
            UpdateProgress1.Visible = false;            
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




    private void ExportToExcel()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try

        {
             
            StringBuilder strHtml = new StringBuilder();
            strHtml.Append("<table border='1px'>");
            for (int icount = 0; icount < grdExel.Rows.Count; icount++)
            {
                if (icount == 0)
                {                    
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdExel.Columns.Count; i++)
                    {
                        if (grdExel.Columns[i].Visible == true)
                        {                            
                                strHtml.Append("<td>");
                                strHtml.Append(grdExel.Columns[i].HeaderText);
                                strHtml.Append("</td>");                             
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdExel.Columns.Count; j++)
                {
                    if (grdExel.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdExel.Rows[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            strHtml.Append("</table>");
            string filename = getFileName("EXCEL") + ".xls";
            StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
            sw.Write(strHtml);
            sw.Close();

            Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);


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

    private string getFileName(string p_szBillNumber)
    {
        String szBillNumber = "";
        szBillNumber = p_szBillNumber;
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = p_szBillNumber + "_" + getRandomNumber() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
        return szFileName;
    }
    private string getRandomNumber()
    {
        System.Random objRandom = new Random();
        return objRandom.Next(1, 10000).ToString();
    }

    #endregion

    private void BindGrid()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        _ReqDocBO = new Bill_Sys_RequiredDocumentBO();
       
        try
        {
               DataSet objds = new DataSet();
               if ((txtFromDate.Text != "" && txtToDate.Text != "") && (extddlSpeciality.Text == "NA" || extddlSpeciality.Text==""))
            {
                
                    objds = _ReqDocBO.GetBillReportsByDate(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text);
                
            }
           else  if ((txtFromDate.Text == "" && txtToDate.Text == "") && (extddlSpeciality.Text != "NA"))
            {
                 
                    objds = _ReqDocBO.GetBillReportsBySpecialty(txtCompanyID.Text, extddlSpeciality.Text);
                
            }
           else  if ((txtFromDate.Text != "" && txtToDate.Text != "") && (extddlSpeciality.Text != "NA"))
            {
                 
                    objds = _ReqDocBO.GetBillReportsByDateAndSpecialty(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, extddlSpeciality.Text);
                 
            }
           else   
            {
                objds = _ReqDocBO.GetBillReports(txtCompanyID.Text, txtFromDate.Text, txtToDate.Text, extddlSpeciality.Text);
            }
            //objds.Tables[0].Columns[13].ColumnMapping= System.Data.MappingType.Hidden; 
            grdPacketing.DataSource = objds;
            grdExel.DataSource = objds;
            grdPacketing.DataBind();
            grdExel.DataBind();
            Session["DataBind"] = objds;
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
        try
        {
            BindGrid();
            System.Threading.Thread.Sleep(500);
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
    protected void grdPacketing_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdPacketing.PageIndex = e.NewPageIndex;
            //grdPacketing.CurrentPageIndex = e.NewPageIndex;
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);
        }
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    //protected void grdPacketing_ItemCommand(object source, DataGridCommandEventArgs e)
    //{
    //    _ReqDocBO = new Bill_Sys_RequiredDocumentBO();
    //    _ReqDocEO = new REQUIREDDOCUMENT_EO();
    //    ArrayList objarr = new ArrayList();
    //    string szOpenFilePath = "";
    //    string _CompanyName = "";
    //    try
    //    {
    //        #region "Create Packet"
    //        if (e.CommandName.ToString() == "CreatePacket")
    //        {
    //            if (!txtPacketId.Text.Equals(""))
    //            {
    //                _CompanyName = Convert.ToString(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
    //                objarr.Add(e.Item.Cells[13].Text);//caseID
    //                objarr.Add(e.Item.Cells[14].Text);//Bill Number
    //                objarr.Add(txtCompanyID.Text);//companyID
    //                objarr.Add(_CompanyName);//companyName
    //                _ReqDocEO = _ReqDocBO.CheckExists(objarr);
    //                Session["PacketDoc"] = objarr;

    //                if (_ReqDocEO.SZ_ERROR_MSG != "")
    //                {
    //                    msgPatientExists.InnerHtml = _ReqDocEO.SZ_ERROR_MSG;
    //                    Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
    //                }
    //                else
    //                {
    //                    _ReqDocEO = _ReqDocBO.MergeDocument(objarr);
    //                    ///set paket bill status id to bill number --atul
    //                    Bill_Sys_RequiredDocumentBO objDocumentBO1 = new Bill_Sys_RequiredDocumentBO();
    //                    objDocumentBO1.Set_PacketId(txtCompanyID.Text, e.Item.Cells[14].Text, txtPacketId.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
    //                    BindGrid();
    //                }

    //                if (_ReqDocEO.SZ_OPEN_FILE_PATH != "")
    //                {
    //                    szOpenFilePath = _ReqDocEO.SZ_OPEN_FILE_PATH;
    //                    szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePath;
    //                    Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
    //                }
              
                   
    //            }
    //            else
    //            {
    //                 msgPatientExists.InnerHtml = "You don't have bill status for packeting,you can not create packet.Please enter bill status for packeting.";
    //                Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");

    //            }

    //      }
    //        #endregion

    //      #region "Grid Sort"

    //      DataView dv;
    //        DataSet ds = new DataSet();
    //        ds = (DataSet)Session["DataBind"];
    //        dv = ds.Tables[0].DefaultView;

    //        if (e.CommandName.ToString() == "Bill No")
    //        {
    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + "  DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }

    //        }
    //        else if (e.CommandName.ToString() == "Bill Date")
    //        {
    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }

    //        }
    //        else if (e.CommandName.ToString() == "Chart No")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }

    //        }
    //        else if (e.CommandName.ToString() == "Patient Name")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }

    //        }
    //        else if (e.CommandName.ToString() == "Reffering Office")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }

    //        }
    //        else if (e.CommandName.ToString() == "Insurance Company")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }
    //        }
    //        else if (e.CommandName.ToString() == "Insurance Claim No")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }
    //        }
    //        else if (e.CommandName.ToString() == "Speciality")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }
    //        }
    //        else if (e.CommandName.ToString() == "Case No")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }
    //        }
    //        else if (e.CommandName.ToString() == "Bill Amt")
    //        {

    //            if (txtSort.Text == e.CommandArgument + " ASC")
    //            {
    //                txtSort.Text = e.CommandArgument + " DESC";
    //            }
    //            else
    //            {
    //                txtSort.Text = e.CommandArgument + " ASC";
    //            }
    //        }

    //        dv.Sort = txtSort.Text;
    //        grdPacketing.DataSource = dv;
    //        grdExel.DataSource = dv;
    //        grdExel.DataBind();
    //        grdPacketing.DataBind();
    //      #endregion


    //    }
    //    catch (Exception ex)
    //    {
            
    //        throw;
    //    }
       
    //}

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
                  szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePath;
                  Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
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

    //Wrote Function To Call Function To Save Request For Packeting
    protected void btnSend_Click(object sender, EventArgs e)
    {
        Bill_Sys_RequiredDocumentBO _RequsredDocumentBO = new Bill_Sys_RequiredDocumentBO();
        ArrayList objArrayL = new ArrayList(); 
        for (int i = 0; i < grdPacketing.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)grdPacketing.Rows[i].Cells[0].FindControl("chkSelect");
            if (chk.Checked == true)
            {
                Bill_Sys_Bill_Packet_Request _BillPacketRequest = new Bill_Sys_Bill_Packet_Request();
                _BillPacketRequest.SZ_CASE_ID = grdPacketing.DataKeys[i][0].ToString();
                _BillPacketRequest.SZ_BILL_NUMBER = grdPacketing.DataKeys[i][1].ToString();
                objArrayL.Add(_BillPacketRequest);
            }
        }                    
        _RequsredDocumentBO.GetBillPacketRequest(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, objArrayL);
        System.Threading.Thread.Sleep(500);
    }
    //end

    protected void grdPacketing_RowCommand(object sender, GridViewCommandEventArgs e)
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
        int index = 0;
        try
        {
            #region "Create Packet"
            if (e.CommandName.ToString() == "CreatePacket")
            {
                if (!txtPacketId.Text.Equals(""))
                {
                    
                    index =Convert.ToInt32(e.CommandArgument);                
                    _CompanyName = Convert.ToString(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME);
                    objarr.Add(grdPacketing.DataKeys[index][0].ToString());//caseID
                    objarr.Add(grdPacketing.DataKeys[index][1].ToString());//Bill Number
                    objarr.Add(txtCompanyID.Text);//companyID
                    objarr.Add(_CompanyName);//companyName
                    _ReqDocEO = _ReqDocBO.CheckExists(objarr);
                    Session["PacketDoc"] = objarr;

                    if (_ReqDocEO.SZ_ERROR_MSG != "")
                    {
                        msgPatientExists.InnerHtml = _ReqDocEO.SZ_ERROR_MSG;
                        //Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "openExistsPage();", true);
                    }
                    else
                    {
                        _ReqDocEO = _ReqDocBO.MergeDocument(objarr);
                        ///set paket bill status id to bill number --atul
                        Bill_Sys_RequiredDocumentBO objDocumentBO1 = new Bill_Sys_RequiredDocumentBO();
                        objDocumentBO1.Set_PacketId(txtCompanyID.Text, grdPacketing.DataKeys[index][1].ToString(), txtPacketId.Text, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID);
                        BindGrid();
                    }

                    if (_ReqDocEO.SZ_OPEN_FILE_PATH != "")
                    {
                        szOpenFilePath = _ReqDocEO.SZ_OPEN_FILE_PATH;
                        szOpenFilePath = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString() + szOpenFilePath;
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "Msg", "window.open('" + szOpenFilePath.ToString() + "'); ", true);
                    }


                }
                else
                {
                    msgPatientExists.InnerHtml = "You don't have bill status for packeting,you can not create packet.Please enter bill status for packeting.";
                    Page.RegisterStartupScript("mm", "<script language='javascript'>openExistsPage();</script>");

                }

            }
            #endregion

            #region "Grid Sort"

            DataView dv;
            DataSet ds = new DataSet();
            ds = (DataSet)Session["DataBind"];
            dv = ds.Tables[0].DefaultView;

            if (e.CommandName.ToString() == "Bill No")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + "  DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Bill Date")
            {
                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Chart No")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Patient Name")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Reffering Office")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }

            }
            else if (e.CommandName.ToString() == "Insurance Company")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            else if (e.CommandName.ToString() == "Insurance Claim No")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            else if (e.CommandName.ToString() == "Speciality")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            else if (e.CommandName.ToString() == "Case No")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }
            else if (e.CommandName.ToString() == "Bill Amt")
            {

                if (txtSort.Text == e.CommandArgument + " ASC")
                {
                    txtSort.Text = e.CommandArgument + " DESC";
                }
                else
                {
                    txtSort.Text = e.CommandArgument + " ASC";
                }
            }

            dv.Sort = txtSort.Text;
            grdPacketing.DataSource = dv;
            grdExel.DataSource = dv;
            grdExel.DataBind();
            grdPacketing.DataBind();
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
}
