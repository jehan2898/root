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
using System.IO;
using System.Text;
using log4net;


public partial class Report_POM : PageBase
{
    Bill_Sys_BillTransaction_BO _obj;
    Bill_Sys_ReportBO _objReport;
  
    string CaseID;
    string strLinkPath = "";
    protected void Page_Load(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {
            ddlDateValues.Attributes.Add("onChange", "javascript:SetDate();");
            ddlServiseDate.Attributes.Add("onChange", "javascript:SetServiceDate();");
            ddlPrintDate.Attributes.Add("onChange", "javascript:SetPOMPrintedDate();");
            ddlRecDate.Attributes.Add("onChange", "javascript:SetPOMReceivedDate();");
   

            this.con.SourceGrid = this.grdPomReport;
            this.txtSearchBox.SourceGrid = this.grdPomReport;
            this.grdPomReport.Page = this.Page;
            this.grdPomReport.PageNumberList = this.con;
            if (rdbpombills.SelectedValue == "0")
            {
                grdPomReport.XGridKey = "Report_pom";
            }else
            {
                grdPomReport.XGridKey = "Report_Pom_Other";

            }

            if (!IsPostBack)
            {

                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                extddlSpeciality.Flag_ID = txtCompanyID.Text;
                ddlPrintDate.SelectedValue = "1";
                txtFromPrintedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtToPrintedDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                
                txtFromBillDate.Attributes.Add("onblur", "javascript:return FromDateValidation(this);");
                txtToBillDate.Attributes.Add("onblur", "javascript:return ToDateValidation(this);");
                fillControl();
                //  BindGrid();
               
                grdPomReport.XGridBindSearch();

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        try
        {

            // //DataSet ds = new DataSet();
            // //ds = insertogridview();

            // //grdPomReport.DataSource = ds.Tables[0];
            ///* grdPomReport.Rows[0].Cells[0].Text = ds.Tables[0].Columns["SZ_OFFICE_ID"].ToString();
            // grdPomReport.Rows[0].Cells[1].Text = ds.Tables[0].Columns["SZ_OFFICE"].ToString();
            // grdPomReport.Rows[0].Cells[2].Text = ds.Tables[0].Columns["SZ_OFFICE_CITY"].ToString();
            // grdPomReport.Rows[0].Cells[3].Text = ds.Tables[0].Columns["SZ_OFFICE_STATE"].ToString();
            // grdPomReport.Rows[0].Cells[4].Text = ds.Tables[0].Columns["SZ_OFFICE_ZIP"].ToString();
            // grdPomReport.Rows[0].Cells[5].Text = ds.Tables[0].Columns["SZ_BILLING_CITY"].ToString();
            // */
            //grdPomReport.DataBind();
            if (rdbpombills.SelectedValue == "0")
            {
                fillControl();
                this.con.SourceGrid = grdPomReport;
                this.txtSearchBox.SourceGrid = grdPomReport;
                this.grdPomReport.Page = this.Page;
                this.grdPomReport.PageNumberList = this.con;
                grdPomReport.XGridKey = "Report_pom";
                grdPomReport.XGridBindSearch();
            }
            else
            {
                
                fillControl();
                this.con.SourceGrid = grdPomReport;
                this.txtSearchBox.SourceGrid = grdPomReport;
                this.grdPomReport.Page = this.Page;
                this.grdPomReport.PageNumberList = this.con;
                grdPomReport.XGridKey = "Report_Pom_Other";
                grdPomReport.XGridBindSearch();
            }
            //BindGrid();
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
    public DataSet insertogridview()
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string strsqlCon = ConfigurationManager.AppSettings["Connection_String"].ToString();
        SqlConnection sqlCon = new SqlConnection(strsqlCon);
        DataSet ds = new DataSet();
        try
        {
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("SP_SampleForSearchPOM", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            /* sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", objAL[0].ToString());
             if (objAL[1].ToString() != "NA" && objAL[1].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_BILL_STATUS_ID", objAL[1].ToString()); }
             if (objAL[2].ToString() != "NA" && objAL[2].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", objAL[2].ToString()); }
             if (objAL[3].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_BILL_DATE", objAL[3].ToString()); }
             if (objAL[4].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_BILL_DATE", objAL[4].ToString()); }
             if (objAL[5].ToString() != "NA" && objAL[5].ToString() != "") { sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", objAL[5].ToString()); }
             if (objAL[6].ToString() != "NA" && objAL[6].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_FROM_SERVICE_DATE", objAL[6].ToString()); }
             if (objAL[7].ToString() != "NA" && objAL[7].ToString() != "") { sqlCmd.Parameters.AddWithValue("@DT_TO_SERVICE_DATE", objAL[7].ToString()); }*/
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlCmd);
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
        //Method End

        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        return ds;
    }
    //private void ExportToExcel()
    //{
    //    try
    //    {
    //        StringBuilder strHtml = new StringBuilder();
    //        strHtml.Append("<table border='1px'>");
    //        for (int icount = 0; icount < grdExel.Rows.Count; icount++)
    //        {
    //            if (icount == 0)
    //            {
    //                strHtml.Append("<tr>");
    //                for (int i = 0; i < grdExel.Columns.Count; i++)
    //                {
    //                    if (grdExel.Columns[i].Visible == true)
    //                    {
    //                        strHtml.Append("<td>");
    //                        strHtml.Append(grdExel.Columns[i].HeaderText);
    //                        strHtml.Append("</td>");
    //                    }
    //                    else
    //                    {
    //                        strHtml.Append("<td>");
    //                        strHtml.Append(grdExel.Columns[i].HeaderText);
    //                        strHtml.Append("</td>");
    //                    }
    //                }
    //                strHtml.Append("</tr>");
    //            }

    //            strHtml.Append("<tr>");
    //            for (int j = 0; j < grdExel.Columns.Count; j++)
    //            {
    //                if (grdExel.Columns[j].Visible == true)
    //                {
    //                    strHtml.Append("<td>");
    //                    strHtml.Append(grdExel.Rows[icount].Cells[j].Text);
    //                    strHtml.Append("</td>");
    //                }
    //                else
    //                {
    //                    strHtml.Append("<td>");
    //                    strHtml.Append(grdExel.Rows[icount].Cells[j].Text);
    //                    strHtml.Append("</td>");
    //                }
    //            }
    //            strHtml.Append("</tr>");
    //        }

    //        strHtml.Append("</table>");
    //        string filename = getFileName("EXCEL") + ".xls";
    //        StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["EXCEL_SHEET"] + filename);
    //        sw.Write(strHtml);
    //        sw.Close();
    //        Response.Redirect(ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"] + filename, false);
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}
    protected void btnExport_Click(object sender, EventArgs e)
    {
        // ExportToExcel();
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
    public void BindGrid()
    {
        Bill_Sys_ReportBO objReport = new Bill_Sys_ReportBO();
        DataSet dsReport = new DataSet();
        if (extddlSpeciality.Text.Equals("NA"))
        {
            dsReport = objReport.GetPOM_Report(txtFromBillDate.Text, txtToBillDate.Text, txtFromServiceDate.Text, txtToServiceDate.Text, txtFromPrintedDate.Text, txtToPrintedDate.Text, txtFromRecDate.Text, txtToRecDate.Text, txtBillNo.Text, "NA", txtPatientName.Text, txtCompanyID.Text, txtBillNo.Text, txtPatientName.Text);

        }
        else
        {
            dsReport = objReport.GetPOM_Report(txtFromBillDate.Text, txtToBillDate.Text, txtFromServiceDate.Text, txtToServiceDate.Text, txtFromPrintedDate.Text, txtToPrintedDate.Text, txtFromRecDate.Text, txtToRecDate.Text, txtBillNo.Text, extddlSpeciality.Text, txtPatientName.Text, txtCompanyID.Text, txtBillNo.Text, txtPatientName.Text);
        }
        Session["POMReport"] = dsReport;
        grdPomReport.DataSource = dsReport.Tables[0];
        //  grdExel.DataSource = dsReport.Tables[0];
        //  grdExel.DataBind();
        grdPomReport.DataBind();
    }

    protected void grdPomReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("View"))
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string Path = grdPomReport.DataKeys[index][0].ToString();

            Page.ClientScript.RegisterClientScriptBlock(typeof(GridView), "Msg", "window.open('" + Path + "'); ", true);
        }

        if (e.CommandName.Equals("BillView"))
        {
            String sz_PomId = e.CommandArgument.ToString();
            _objReport = new Bill_Sys_ReportBO();
            DataSet dsBillView = _objReport.GetBillDetails(sz_PomId, txtCompanyID.Text);
            grdPaymentTransaction.DataSource = dsBillView;
            grdPaymentTransaction.DataBind();
            ModalPopupExtender1.Show();

        }
        #region "Sort"
        DataView dv;
        DataSet ds = new DataSet();
        ds = (DataSet)Session["POMReport"];
        dv = ds.Tables[0].DefaultView;
        if (e.CommandName.ToString() == "POMID")
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

        else if (e.CommandName.ToString() == "BillNumber")
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

        else if (e.CommandName.ToString() == "POMGeneratedDate")
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

        else if (e.CommandName.ToString() == "BillStatus")
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

        dv.Sort = txtSort.Text;
        grdPomReport.DataSource = dv;
        //  grdExel.DataSource = dv;
        grdPomReport.DataBind();
        // grdExel.DataBind();
        #endregion

    }

    //protected void btnUploadFile_Click(object sender, EventArgs e)
    //{
    //    Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
    //    try
    //    {
    //        if (!fuUploadReport.HasFile)
    //        {
    //            Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
    //            return;
    //        }
    //        String szDefaultPath = objNF3Template.getPhysicalPath();
    //        int ImageId = 0;
    //        String szDestinationDir = "";

    //        szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
    //        szDestinationDir = szDestinationDir + "/" + Session["UPLOADCASEID"].ToString() + "/No Fault File/Proof of Mailing/";

    //        strLinkPath = szDestinationDir + fuUploadReport.FileName;
    //        if (!Directory.Exists(szDefaultPath + szDestinationDir))
    //        {
    //            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
    //        }
    //        //if (!File.Exists(szDefaultPath + szDestinationDir + fuUploadReport.FileName))
    //        //{
    //        fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
    //        // Start : Save report under document manager.
    //        _obj = new Bill_Sys_BillTransaction_BO();
    //        // String NodeId = _obj.GetNodeID(txtCompanyID.Text, Session["UPLOADCASEID"].ToString(), "NFPRO");
    //        string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFPRO");
    //        ArrayList objAL = new ArrayList();

    //        objAL.Add(Session["UPLOADCASEID"].ToString());
    //        objAL.Add("");
    //        objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

    //        objAL.Add(fuUploadReport.FileName);
    //        objAL.Add(szDestinationDir);
    //        objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
    //        objAL.Add(NodeId);

    //        //objNF3Template.UpdateDocMgr(objAL);
    //        string ImgId = objNF3Template.SaveDocumentData(objAL);
    //        // End :   Save report under document manager.

    //        objNF3Template.UpdateReportPomPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId);

    //        lblMsg.Text = "File Upload Successfully";
    //        lblMsg.Visible = true;
    //        BindGrid();
    //    }
    //    //Page.RegisterStartupScript("ss", "<script language = 'javascript'>alert('Report received successfully.');</script>");


    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}


    protected void btnUploadFile_Click1(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        Boolean flag = false;

        try
        {
            if (!fuUploadReport.HasFile)
            {
                Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                return;
            }
            String szDefaultPath = objNF3Template.getPhysicalPath();
            int ImageId = 0;
            String szDestinationDir = "";

            szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
            //szDestinationDir = szDestinationDir + "/" + txtCaseID.Text + "/No Fault File/Proof of Mailing/";
            DataSet ds = objNF3Template.GetPomCaseId(Session["UPLOADPOMID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                String sz_Case_Id = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                string szSpecID = ds.Tables[0].Rows[i]["sz_speciality_id"].ToString();

                _obj = new Bill_Sys_BillTransaction_BO();
                string szNodeType = _obj.GetNodeType(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szSpecID, sz_Case_Id);
                // String NodeId = _obj.GetNodeID(txtCompanyID.Text, Session["UPLOADCASEID"].ToString(), "NFPRO");
                string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szNodeType);

                RequiredDocuments.Bill_Sys_RequiredDocumentBO bo = new RequiredDocuments.Bill_Sys_RequiredDocumentBO();
                string szNodePath = bo.GetNodePath(NodeId, sz_Case_Id, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                // szDestinationDir = szDestinationDir + "/" + szNodePath + "";
                szDestinationDir = szNodePath + "\\";
                szDestinationDir = szDestinationDir.Replace("\\", "/");
                strLinkPath = szDestinationDir + fuUploadReport.FileName;
                if (!Directory.Exists(szDefaultPath + szDestinationDir))
                {
                    Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                }
                //if (!File.Exists(szDefaultPath + szDestinationDir + fuUploadReport.FileName))
                //{
                fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
                // Start : Save report under document manager.

                ArrayList objAL = new ArrayList();

                objAL.Add(sz_Case_Id);
                objAL.Add("");
                objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                objAL.Add(fuUploadReport.FileName);
                objAL.Add(szDestinationDir);
                objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                objAL.Add(NodeId);

                //objNF3Template.UpdateDocMgr(objAL);
                string ImgId = objNF3Template.SaveDocumentData(objAL);
                // End :   Save report under document manager.
                if (!flag)
                {
                    if (Session["POMSTATUS"].ToString().Equals("1"))
                    {
                        objNF3Template.UpdateReportPomPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId, "VPOMR");
                    }
                    else
                    {
                        objNF3Template.UpdateReportPomPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId, "POM");
                    }
                    flag = true;
                }


            }

            // End :   Save report under document manager.

            // objNF3Template.UpdateReportPomPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId);

            usrMessage.PutMessage("File Upload Successfully");
            usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
            usrMessage.Show();

            // BindGrid();
            fillControl();
            grdPomReport.XGridBindSearch();
        }
        //Page.RegisterStartupScript("ss", "<script language = 'javascript'>alert('Report received successfully.');</script>");


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

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {//Logging Start
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }

        Bill_Sys_NF3_Template objNF3Template = new Bill_Sys_NF3_Template();
        Boolean flag = false;
        Boolean updateflag = false;
        string ImgId = "";
        try
        {

            if (rdbpombills.SelectedValue == "0")
            {
                if (!fuUploadReport.HasFile)
                {
                    Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                    return;
                }
                String szDefaultPath = objNF3Template.getPhysicalPath();
                int ImageId = 0;
                String szDestinationDir = "";

                //  szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                DataSet ds = objNF3Template.GetPomCaseId(Session["UPLOADPOMID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    String sz_Case_Id = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    string szSpecID = ds.Tables[0].Rows[i]["sz_speciality_id"].ToString();

                    _obj = new Bill_Sys_BillTransaction_BO();
                    string szNodeType = _obj.GetNodeType(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szSpecID, sz_Case_Id);
                    string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szNodeType);
                    RequiredDocuments.Bill_Sys_RequiredDocumentBO bo = new RequiredDocuments.Bill_Sys_RequiredDocumentBO();
                    //   string szNodePath = bo.GetNodePath(NodeId, sz_Case_Id, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    szDestinationDir = "";
                    szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    szDestinationDir = szDestinationDir + "\\POM receive\\" + Session["UPLOADPOMID"].ToString() + "\\";
                    szDestinationDir = szDestinationDir.Replace("\\", "/");
                    strLinkPath = szDestinationDir + fuUploadReport.FileName;
                    if (!updateflag)
                    {
                        if (!Directory.Exists(szDefaultPath + szDestinationDir))
                        {
                            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                        }
                        fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
                        updateflag = true;
                    }

                    // Start : Save report under document manager.

                    ArrayList objAL = new ArrayList();

                    objAL.Add(sz_Case_Id);
                    objAL.Add("");
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                    objAL.Add(fuUploadReport.FileName);
                    objAL.Add(szDestinationDir);
                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                    objAL.Add(NodeId);

                    ImgId = objNF3Template.SaveDocumentData(objAL);


                    // End :   Save report under document manager.
                    if (!flag)
                    {
                        if (Session["POMSTATUS"].ToString().Equals("1"))
                        {
                            objNF3Template.UpdateReportPomPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId, "VPOMR");
                        }
                        else
                        {
                            objNF3Template.UpdateReportPomPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId, "POM");
                        }
                        flag = true;
                    }


                }

                // End :   Save report under document manager.


                usrMessage.PutMessage("File Upload Successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

                //fillControl();

                fillControl();
                this.con.SourceGrid = grdPomReport;
                this.txtSearchBox.SourceGrid = grdPomReport;
                this.grdPomReport.Page = this.Page;
                this.grdPomReport.PageNumberList = this.con;
                grdPomReport.XGridKey = "Report_pom";
                grdPomReport.XGridBindSearch();
            }
            else
            {
                _objReport = new Bill_Sys_ReportBO();
                if (!fuUploadReport.HasFile)
                {
                    Page.RegisterStartupScript("ss", "<script language='javascript'> alert('please select file from upload Report !');showUploadFilePopup();</script>");
                    return;
                }
                String szDefaultPath = objNF3Template.getPhysicalPath();
                int ImageId = 0;
                String szDestinationDir = "";
                DataSet dspomother = new DataSet();
                //  szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                 dspomother = _objReport.GetPomotherCaseId(Session["UPLOADPOMID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                for (int i = 0; i < dspomother.Tables[0].Rows.Count; i++)
                {

                    String sz_Case_Id = dspomother.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                    string szSpecID = dspomother.Tables[0].Rows[i]["sz_speciality_id"].ToString();

                    _obj = new Bill_Sys_BillTransaction_BO();
                    string szNodeType = _obj.GetNodeType(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szSpecID, sz_Case_Id);
                    string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, szNodeType);
                    RequiredDocuments.Bill_Sys_RequiredDocumentBO bo = new RequiredDocuments.Bill_Sys_RequiredDocumentBO();
                    //   string szNodePath = bo.GetNodePath(NodeId, sz_Case_Id, ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    szDestinationDir = "";
                    szDestinationDir = objNF3Template.GetCompanyName(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
                    szDestinationDir = szDestinationDir + "\\POM receive\\" + Session["UPLOADPOMID"].ToString() + "\\";
                    szDestinationDir = szDestinationDir.Replace("\\", "/");
                    strLinkPath = szDestinationDir + fuUploadReport.FileName;
                    if (!updateflag)
                    {
                        if (!Directory.Exists(szDefaultPath + szDestinationDir))
                        {
                            Directory.CreateDirectory(szDefaultPath + szDestinationDir);
                        }
                        fuUploadReport.SaveAs(szDefaultPath + szDestinationDir + fuUploadReport.FileName);
                        updateflag = true;
                    }

                    // Start : Save report under document manager.

                    ArrayList objAL = new ArrayList();

                    objAL.Add(sz_Case_Id);
                    objAL.Add("");
                    objAL.Add(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);

                    objAL.Add(fuUploadReport.FileName);
                    objAL.Add(szDestinationDir);
                    objAL.Add(((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME.ToString());
                    objAL.Add(NodeId);

                    ImgId = objNF3Template.SaveDocumentData(objAL);


                    // End :   Save report under document manager.
                    if (!flag)
                    {
                        if (Session["POMSTATUS"].ToString().Equals("1"))
                        {
                            _objReport.UpdateReportPomOtherPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId, "VPOMR");
                        }
                        else
                        {
                            _objReport.UpdateReportPomOtherPath(fuUploadReport.FileName, szDestinationDir, ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID, Session["UPLOADPOMID"].ToString(), ImgId, "POM");
                        }
                        flag = true;
                    }


                }

                // End :   Save report under document manager.


                usrMessage.PutMessage("File Upload Successfully");
                usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_UserMessage);
                usrMessage.Show();

                fillControl();
                this.con.SourceGrid = grdPomReport;
                this.txtSearchBox.SourceGrid = grdPomReport;
                this.grdPomReport.Page = this.Page;
                this.grdPomReport.PageNumberList = this.con;
                grdPomReport.XGridKey = "Report_Pom_Other";
                grdPomReport.XGridBindSearch();
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



    protected void lnkscan_Click(object sender, EventArgs e)
    {

        int iindex = grdPomReport.SelectedIndex;
        GridViewRow gvRowscan = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRowscan.RowIndex;
        Session["SCANPOMID"] = grdPomReport.DataKeys[index][1].ToString();
        Session["SCANPOMSTATUS"] = grdPomReport.DataKeys[index]["I_VERIFICATION_POM"].ToString();
        RedirectToScanApp(iindex);
    }

    protected void lnkuplaod_Click(object sender, EventArgs e)
    {
        GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        int index = gvRow.RowIndex;
        Session["UPLOADPOMID"] = grdPomReport.DataKeys[index][1].ToString();
        Session["POMSTATUS"] = grdPomReport.DataKeys[index]["I_VERIFICATION_POM"].ToString();

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", "showUploadFilePopup();", true);
    }

    public void RedirectToScanApp(int iindex)
    {
        ArrayList arrSpeId = new ArrayList();
        iindex = (int)grdPomReport.SelectedIndex;
        string szUrl = ConfigurationManager.AppSettings["webscanurl"].ToString();
        _obj = new Bill_Sys_BillTransaction_BO();
        string pomstatus = "";

        if (Session["SCANPOMSTATUS"].ToString().Equals("1"))
        {
            pomstatus = "VPOMR";
        }
        else
        {
            pomstatus = "POM";
        }


        //   String NodeId = _obj.GetNodeID(txtCompanyID.Text, Session["SCANCASEID"].ToString(), "NFPRO");
        string NodeId = _obj.GetNodeIDMST_Nodes(((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID, "NFPRO");
        arrSpeId = _obj.GetSpecialityIdFromPOM(Session["SCANPOMID"].ToString(), ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID);
        string szSpecialityId = "";
        string szProcess = "POM";
        for (int i = 0; i < arrSpeId.Count; i++)
        {
            if (szSpecialityId == "")
            {
                szSpecialityId = arrSpeId[i].ToString();
            }
            else
            {
                szSpecialityId = szSpecialityId + "," + arrSpeId[i].ToString();
            }
        }

        szUrl = szUrl + "&Flag=ReqPom" + "&CompanyId=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID + "&UserName=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME + "&CompanyName=" + ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_NAME;
        szUrl = szUrl + "&PName=" + Session["SCANPOMID"].ToString() + "&CaseNo=POM" + "&NodeId=" + NodeId + "&PomID=" + Session["SCANPOMID"].ToString() + "&UserId=" + ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        szUrl = szUrl + "&Pomstatus=" + pomstatus + "&Speciality=" + szSpecialityId + "&Process=" + szProcess;

        ScriptManager.RegisterStartupScript(this, this.GetType(), "starScript", "window.open('" + szUrl + "', 'Scan_Document','channelmode=no,location=no,toolbar=no,menubar=0,resizable=0,status=no,scrollbars=0, width=600,height=550'); ", true);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        //GridBind();
        ModalPopupExtender1.Hide();

    }

    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ApplicationSettings.GetParameterValue( "FETCHEXCEL_SHEET").ToString() + grdPomReport.ExportToExcel(ApplicationSettings.GetParameterValue("EXCEL_SHEET")) + "';", true);
    }

    protected void fillControl()
    {
        utxtCompanyID.Text = txtCompanyID.Text;
        utxtFromBillDate.Text = txtFromBillDate.Text;
        utxtBillNo.Text = txtBillNo.Text;
        utxtFromPrintedDate.Text = txtFromPrintedDate.Text;
        utxtFromRecDate.Text = txtFromRecDate.Text;
        utxtFromServiceDate.Text = txtFromServiceDate.Text;
        utxtPatientName.Text = txtPatientName.Text;
        utxtToBillDate.Text = txtToBillDate.Text;
        utxtToPrintedDate.Text = txtToPrintedDate.Text;
        utxtToRecDate.Text = txtToRecDate.Text;
        utxtToServiceDate.Text = txtToServiceDate.Text;
        if (extddlSpeciality.Text != "NA")
        {
            utxtSpeciality.Text = extddlSpeciality.Text;
        }
        else
        {
            utxtSpeciality.Text = "";
        }
    }

}
