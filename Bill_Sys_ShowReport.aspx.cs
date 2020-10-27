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
using System.Text;
using System.IO;
public partial class Bill_Sys_ShowReport : PageBase
{
    private Bill_Sys_ReportBO _reportBO;
    private Bill_Sys_BillingCompanyDetails_BO _BillingCompany;
    string StartDate, EndDate, OfficeId, DocorId, Status;
    protected void Page_Load(object sender, EventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            if (!IsPostBack)
            {
                _BillingCompany = new Bill_Sys_BillingCompanyDetails_BO();
                DataSet ds = new DataSet();
                
                txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
                ds= _BillingCompany.GetBillingCompanyInfo(txtCompanyID.Text);
                lbl_MRIFacilityName.Text = ds.Tables[0].Rows[0][1].ToString();
                lbl_State.Text = ds.Tables[0].Rows[0][5].ToString();
                lbl_City.Text = ds.Tables[0].Rows[0][3].ToString();
                lbl_Zip.Text = ds.Tables[0].Rows[0][4].ToString();
                lbl_Phone.Text = ds.Tables[0].Rows[0][6].ToString();
                //lbl_Fax.Text  = ds.Tables[0].Columns[3].ToString();
                StartDate = Request.QueryString["StartDate"].ToString();
                EndDate = Request.QueryString["EndDate"].ToString();
                OfficeId = Request.QueryString["OfficeId"].ToString();
                DocorId = Request.QueryString["DocorId"].ToString();
                Status = Request.QueryString["Status"].ToString();
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
            base.Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + str2);

        }
        
        #region "check version readonly or not"
        string app_status = ((Bill_Sys_BillingCompanyObject)Session["APPSTATUS"]).SZ_READ_ONLY.ToString();
        if (app_status.Equals("True"))
        {
            Bill_Sys_ChangeVersion cv = new Bill_Sys_ChangeVersion(this.Page);
            cv.MakeReadOnlyPage("Bill_Sys_ProcedureReport.aspx");
        }
        #endregion
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
        _BillingCompany = new Bill_Sys_BillingCompanyDetails_BO();
        DataSet ds = new DataSet();
        DataTable OBJDTSum = new DataTable();
        DataRow objDRSum;
        string Office_Id="";
        try
        {
            DataTable objDSOfficeWise = new DataTable();
            ds = _BillingCompany.GetOfficeWisePatientInfo(txtCompanyID.Text, StartDate.ToString(), EndDate.ToString(), OfficeId.ToString(), DocorId.ToString(), Status.ToString());
            objDSOfficeWise = DisplayOfficeInGrid(ds);
            grdAllReports.DataSource = objDSOfficeWise;
            grdAllReports.DataBind();
            //Code To Fill Second Grid
            OBJDTSum.Columns.Add("SZ_OFFICE");
            OBJDTSum.Columns.Add("SZ_PATIENT_NAME");
            OBJDTSum.Columns.Add("SZ_DOCTOR_NAME");
            OBJDTSum.Columns.Add("DT_EVENT_DATE");
            OBJDTSum.Columns.Add("SZ_INSURANCE_NAME");
            OBJDTSum.Columns.Add("SZ_OFFICE_STATE");
            OBJDTSum.Columns.Add("SZ_OFFICE_ZIP");
            for(int i=0;i<grdAllReports.Items.Count ;i++)
            {
                string str = grdAllReports.Items[i].Cells[5].Text.ToString();
                
                if (grdAllReports.Items[i].Cells[5].Text.ToString() == "&nbsp;" && grdAllReports.Items[i].Cells[3].Text.ToString() == "&nbsp;")
                {

                    Office_Id = grdAllReports.Items[i].Cells[0].Text.ToString();
                }
                if (grdAllReports.Items[i].Cells[4].Text.ToString() == "&nbsp;" && str.Substring(0, 4) == "<b>T")
                {
                    objDRSum = OBJDTSum.NewRow();
                    objDRSum["SZ_OFFICE"] = Office_Id.ToString();

                    objDRSum["SZ_PATIENT_NAME"] = grdAllReports.Items[i].Cells[0].Text.ToString();
                    objDRSum["SZ_DOCTOR_NAME"] = grdAllReports.Items[i].Cells[1].Text.ToString();
                    objDRSum["DT_EVENT_DATE"] = grdAllReports.Items[i].Cells[2].Text.ToString();
                    objDRSum["SZ_INSURANCE_NAME"] = grdAllReports.Items[i].Cells[3].Text.ToString();
                    //objDRSum["SZ_OFFICE_STATE"] = grdAllReports.Items[i].Cells[4].Text.ToString();
                    objDRSum["SZ_OFFICE_ZIP"] = grdAllReports.Items[i].Cells[5].Text.ToString();
                    OBJDTSum.Rows.Add(objDRSum);
                }
            }

            grdTotalCount.DataSource = OBJDTSum;
            grdTotalCount.DataBind();
            //end Code

         
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



    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        BindGrid();
    //    }
    //    catch (Exception ex)
    //    {
    //        string strError = ex.Message.ToString();
    //        strError = strError.Replace("\n", " ");
    //        Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
    //    }
    //}
    protected void grdAllReports_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        try
        {
            grdAllReports.CurrentPageIndex = e.NewPageIndex;
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
            for (int icount = 0; icount < grdAllReports.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdAllReports.Columns.Count; i++)
                    {
                        if (grdAllReports.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdAllReports.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdAllReports.Columns.Count; j++)
                {
                    if (grdAllReports.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdAllReports.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }

            //To Show Count Frid in Excel file
            for (int icount = 0; icount < grdTotalCount.Items.Count; icount++)
            {
                if (icount == 0)
                {
                    strHtml.Append("<tr>");
                    for (int i = 0; i < grdTotalCount.Columns.Count; i++)
                    {
                        if (grdTotalCount.Columns[i].Visible == true)
                        {
                            strHtml.Append("<td>");
                            strHtml.Append(grdTotalCount.Columns[i].HeaderText);
                            strHtml.Append("</td>");
                        }
                    }

                    strHtml.Append("</tr>");
                }

                strHtml.Append("<tr>");
                for (int j = 0; j < grdTotalCount.Columns.Count; j++)
                {
                    if (grdTotalCount.Columns[j].Visible == true)
                    {
                        strHtml.Append("<td>");
                        strHtml.Append(grdTotalCount.Items[icount].Cells[j].Text);
                        strHtml.Append("</td>");
                    }
                }
                strHtml.Append("</tr>");

            }
            //end
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



    #region "Display Location wise patient in grid"
    public DataTable DisplayOfficeInGrid(DataSet p_objDS)
    {
        string id = string.Format("Id: {0} Uri: {1}", Guid.NewGuid(), HttpContext.Current.Request.Url);
        using (Utils utility = new Utils())
        {
            utility.MethodStart(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
        string MRIShow, MRINS, MRIRS, MRITotal, Show, NS, RS, Total;
        int TotalShow=0, TotalNS=0, TotalRS=0, Totaltotal=0;
        DataSet objDS = new DataSet();
        objDS = p_objDS;
        DataSet objdscount = new DataSet();
        DataSet objdsRowcount = new DataSet();
        DataTable objDT = new DataTable();
        int i;
        _BillingCompany = new Bill_Sys_BillingCompanyDetails_BO();
        try
        {

            

            objDT.Columns.Add("SZ_PATIENT_NAME");
            objDT.Columns.Add("SZ_DOCTOR_NAME");
            objDT.Columns.Add("DT_EVENT_DATE");
            objDT.Columns.Add("SZ_INSURANCE_NAME");
            objDT.Columns.Add("DT_ACCIDENT_DATE");
            objDT.Columns.Add("SZ_PROC_CODE");
            objDT.Columns.Add("SZ_OFFICE");
            objDT.Columns.Add("SZ_OFFICE_ADDRESS");
            objDT.Columns.Add("SZ_OFFICE_CITY");
            objDT.Columns.Add("SZ_OFFICE_STATE");
            objDT.Columns.Add("SZ_OFFICE_ZIP");
            objDT.Columns.Add("Office_Id");
            DataRow objDR;
            string sz_Office_Name = "NA";
            Session["Office_Id"] = "";



            for ( i = 0; i < objDS.Tables[0].Rows.Count; i++)
            {
                if (objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString().Equals(sz_Office_Name))
                {

                    objDR = objDT.NewRow();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    objDR["DT_EVENT_DATE"] = objDS.Tables[0].Rows[i]["DT_EVENT_DATE"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["DT_ACCIDENT_DATE"] = objDS.Tables[0].Rows[i]["DT_ACCIDENT_DATE"].ToString();
                    objDR["SZ_PROC_CODE"] = objDS.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString();
                    objDR["SZ_OFFICE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                    objDR["SZ_OFFICE_ADDRESS"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ADDRESS"].ToString();
                    objDR["SZ_OFFICE_CITY"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_CITY"].ToString();
                    objDR["SZ_OFFICE_STATE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_STATE"].ToString();
                    objDR["SZ_OFFICE_ZIP"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ZIP"].ToString();
                    objDR["Office_Id"] = objDS.Tables[0].Rows[i]["Office_Id"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Office_Name = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                }
                else
                {
                    if (sz_Office_Name != "NA")
                    {
                        MRIShow = "0"; MRINS = "0"; MRIRS = "0"; MRITotal = "0"; Show = "0"; NS = "0"; RS = "0"; Total="0";
                        TotalShow = 0; TotalNS = 0; TotalRS = 0; Totaltotal = 0;
                        //Code To get Room Count
                        objdsRowcount = _BillingCompany.GetCompanyWiseRoomCount(txtCompanyID.Text);
                        //end Of Code
                       
                        objDR = objDT.NewRow();
                        for (int j = 0; j < objdsRowcount.Tables[0].Rows.Count; j++)
                        {
                            //Code to Show Patient Count Type Wise
                            if (Status.ToString() == "2" || Status.ToString() == "NA")
                            {
                                objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "2", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                                MRIShow = objdscount.Tables[0].Rows[0][0].ToString();
                            }
                            if (Status.ToString() == "3" || Status.ToString() == "NA")
                            {
                                objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "3", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                                MRINS = objdscount.Tables[0].Rows[0][0].ToString();
                            }
                            if (Status.ToString() == "1" || Status.ToString() == "NA")
                            {
                                objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "1", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                                MRIRS = objdscount.Tables[0].Rows[0][0].ToString();
                            }

                            objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), Status.ToString(), StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                                MRITotal = objdscount.Tables[0].Rows[0][0].ToString();
                             


                            TotalShow = TotalShow + Convert.ToInt32(MRIShow);
                            TotalNS = TotalNS + Convert.ToInt32(MRINS);
                            TotalRS = TotalRS + Convert.ToInt32(MRIRS);
                            Totaltotal = Totaltotal + Convert.ToInt32(MRITotal);
                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000037", "2");
                            //XrayShow = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000037", "3");
                            //XrayNS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000037", "1");
                            //XrayRS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000037", "");
                            //XrayTotal = objdscount.Tables[0].Rows[0][0].ToString();


                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000075", "2");
                            //CTShow = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000075", "3");
                            //CTNS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000075", "1");
                            //CTRS = objdscount.Tables[0].Rows[0][0].ToString();

                            //objdscount = _BillingCompany.GetTypeWisePatientCount(Session["Office_Id"].ToString(), "PG000000000000000075", "");
                            //CTTotal = objdscount.Tables[0].Rows[0][0].ToString();

                            //TotalShow = Convert.ToInt32(MRIShow) + Convert.ToInt32(XrayShow) + Convert.ToInt32(CTShow);
                            //TotalNS = Convert.ToInt32(MRINS) + Convert.ToInt32(XrayNS) + Convert.ToInt32(CTNS);
                            //TotalRS = Convert.ToInt32(MRIRS) + Convert.ToInt32(XrayRS) + Convert.ToInt32(CTRS);
                            //Totaltatal = Convert.ToInt32(MRITotal) + Convert.ToInt32(XrayTotal) + Convert.ToInt32(CTTotal);


                            objDR[j] = "<b>" + objdsRowcount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";

                            if (j == objdsRowcount.Tables[0].Rows.Count-1)
                            {
                                objDR[5] = "<b>Total – show(" + TotalShow.ToString() + "), NS(" + TotalNS.ToString() + ") , RS(" + TotalRS.ToString() + ") , Total-" + Totaltotal.ToString() + " </b>";
                            }
                            //objDR = objDT.NewRow();
                            //objDR["SZ_PATIENT_NAME"] = "<b>" + objdscount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";
                            //objDR["SZ_DOCTOR_NAME"] = "<b>" + objdscount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";
                            //objDR["DT_EVENT_DATE"] = "<b>" + objdscount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";
                            //objDR["SZ_INSURANCE_NAME"] = "";
                            //objDR["DT_ACCIDENT_DATE"] = "";
                            //objDR["SZ_PROC_CODE"] = "<b>TOTAL – show(" + TotalShow + "), NS(" + TotalNS + ") , RS(" + TotalRS + ") , Total-" + Totaltatal + " </b>";
                            //objDR["SZ_OFFICE"] = "";
                            //objDR["SZ_OFFICE_ADDRESS"] = "";
                            //objDR["SZ_OFFICE_CITY"] = "";
                            //objDR["SZ_OFFICE_STATE"] = "";
                            //objDR["SZ_OFFICE_ZIP"] = "";
                            //objDR["Office_Id"] = "";
                            //objDT.Rows.Add(objDR);
                        }
                            objDT.Rows.Add(objDR);
                    }
                        //End Code
                    if (sz_Office_Name!="NA")
                    {
                        objDR = objDT.NewRow();
                        objDR["SZ_PATIENT_NAME"] = "<p style='page-break-before:always;' >";
                        objDR["SZ_DOCTOR_NAME"] = "";
                        objDR["DT_EVENT_DATE"] = "";
                        objDR["SZ_INSURANCE_NAME"] = "";
                        objDR["DT_ACCIDENT_DATE"] = "";
                        objDR["SZ_PROC_CODE"] = "";
                        objDR["SZ_OFFICE"] = "";
                        objDR["SZ_OFFICE_ADDRESS"] = "";
                        objDR["SZ_OFFICE_CITY"] = "";
                        objDR["SZ_OFFICE_STATE"] = "";
                        objDR["SZ_OFFICE_ZIP"] = "";
                        objDR["Office_Id"] = "</p>";

                        objDT.Rows.Add(objDR);
                    }
                    objDR = objDT.NewRow();
                    objDR["SZ_PATIENT_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString() + "</b>";
                    objDR["SZ_DOCTOR_NAME"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_ADDRESS"].ToString() + "<b>";
                    objDR["DT_EVENT_DATE"] = "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_CITY"].ToString() + "<b>" + "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_STATE"].ToString() + "<b>" + "<b>" + objDS.Tables[0].Rows[i]["SZ_OFFICE_ZIP"].ToString() + "<b>";
                    objDR["SZ_INSURANCE_NAME"] = "";
                    objDR["DT_ACCIDENT_DATE"] = "";
                    objDR["SZ_PROC_CODE"] = "";
                    objDR["SZ_OFFICE"] = "";
                    objDR["SZ_OFFICE_ADDRESS"] = "";
                    objDR["SZ_OFFICE_CITY"] = "";
                    objDR["SZ_OFFICE_STATE"] = "";
                    objDR["SZ_OFFICE_ZIP"] = "";
                    objDR["Office_Id"] = "";
                    int count = grdAllReports.Items.Count;
                    objDT.Rows.Add(objDR);


                    objDR = objDT.NewRow();
                    objDR["SZ_PATIENT_NAME"] = objDS.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                    objDR["SZ_DOCTOR_NAME"] = objDS.Tables[0].Rows[i]["SZ_DOCTOR_NAME"].ToString();
                    objDR["DT_EVENT_DATE"] = objDS.Tables[0].Rows[i]["DT_EVENT_DATE"].ToString();
                    objDR["SZ_INSURANCE_NAME"] = objDS.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                    objDR["DT_ACCIDENT_DATE"] = objDS.Tables[0].Rows[i]["DT_ACCIDENT_DATE"].ToString();
                    objDR["SZ_PROC_CODE"] = objDS.Tables[0].Rows[i]["SZ_PROC_CODE"].ToString();
                    objDR["SZ_OFFICE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                    objDR["SZ_OFFICE_ADDRESS"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ADDRESS"].ToString();
                    objDR["SZ_OFFICE_CITY"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_CITY"].ToString();
                    objDR["SZ_OFFICE_STATE"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_STATE"].ToString();
                    objDR["SZ_OFFICE_ZIP"] = objDS.Tables[0].Rows[i]["SZ_OFFICE_ZIP"].ToString();
                    objDR["Office_Id"] = objDS.Tables[0].Rows[i]["Office_Id"].ToString();
                    objDT.Rows.Add(objDR);
                    sz_Office_Name = objDS.Tables[0].Rows[i]["SZ_OFFICE"].ToString();
                    Session["Office_Id"] = objDS.Tables[0].Rows[i]["Office_Id"].ToString();

                }
            }


            
                    //Code to Show Patient Count Type Wise
            if (i != 0)
            {
                MRIShow = "0"; MRINS = "0"; MRIRS = "0"; MRITotal = "0"; Show = "0"; NS = "0"; RS = "0"; Total = "0";
                TotalShow = 0; TotalNS = 0; TotalRS = 0; Totaltotal = 0;
                //Code To get Room Count
                objdsRowcount = _BillingCompany.GetCompanyWiseRoomCount(txtCompanyID.Text);
                //end Of Code

                objDR = objDT.NewRow();
                for (int j = 0; j < objdsRowcount.Tables[0].Rows.Count; j++)
                {
                    //Code to Show Patient Count Type Wise
                    if (Status.ToString() == "2" || Status.ToString() == "NA")
                    {
                        objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "2", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                        MRIShow = objdscount.Tables[0].Rows[0][0].ToString();
                    }
                    if (Status.ToString() == "3" || Status.ToString() == "NA")
                    {
                        objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "3", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                        MRINS = objdscount.Tables[0].Rows[0][0].ToString();
                    }
                    if (Status.ToString() == "1" || Status.ToString() == "NA")
                    {
                        objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), "1", StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                        MRIRS = objdscount.Tables[0].Rows[0][0].ToString();
                    }
                    
                        objdscount = _BillingCompany.GetTypeWisePatientCount(objDS.Tables[0].Rows[i - 1]["Office_Id"].ToString(), objdsRowcount.Tables[0].Rows[j][1].ToString(), Status.ToString(), StartDate.ToString(), EndDate.ToString(), DocorId.ToString());
                        MRITotal = objdscount.Tables[0].Rows[0][0].ToString();
                    
                    TotalShow = TotalShow + Convert.ToInt32(MRIShow);
                    TotalNS = TotalNS + Convert.ToInt32(MRINS);
                    TotalRS = TotalRS + Convert.ToInt32(MRIRS);
                    Totaltotal = Totaltotal + Convert.ToInt32(MRITotal);
 

                    objDR[j] = "<b>" + objdsRowcount.Tables[0].Rows[j][0].ToString() + " – show(" + MRIShow.ToString() + "), NS(" + MRINS.ToString() + ") , RS(" + MRIRS.ToString() + ") , Total-" + MRITotal.ToString() + " </b>";

                    if (j == objdsRowcount.Tables[0].Rows.Count - 1)
                    {
                        objDR[5] = "<b>Total – show(" + TotalShow.ToString() + "), NS(" + TotalNS.ToString() + ") , RS(" + TotalRS.ToString() + ") , Total-" + Totaltotal.ToString() + " </b>";
                    }
               
                }
                objDT.Rows.Add(objDR);
            }
            //End Code
        
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
        
        return objDT;
        //Method End
        using (Utils utility = new Utils())
        {
            utility.MethodEnd(id, System.Reflection.MethodBase.GetCurrentMethod());
        }
    }
    #endregion
}
