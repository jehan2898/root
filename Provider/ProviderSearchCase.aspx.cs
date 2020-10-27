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
using log4net;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using CustomControls.ContextMenuScope;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using Reminders;
using mbs.provider;
using DevExpress.Web;
public partial class Provider_ProviderSearchCase : PageBase
{
    private string szExcelFileNamePrefix = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.con.SourceGrid = grdPatientList;//
        //this.txtSearchBox.SourceGrid = grdPatientList;//
        this.grdPatientList.Page = this.Page;
        //this.grdPatientList.PageNumberList = this.con;//
        //txtOfficeID.Text = "OI000000000000000089";
        btnClearP.Attributes.Add("onclick", "return Clear()");
        Session["Office_ID"] = "";
        if (!IsPostBack)
        {
            Session["ReportFromDate"] = "1/1/2009 12:00:00 AM";
            Session["ReportToDate"] = DateTime.Now.ToString();
            txtCompanyID.Text = ((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
            txtUserID.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
            mbs.provider.ProviderServices obj_provider = new ProviderServices();
            string OfficeIDs = "";
            DataSet dsprov = obj_provider.get_office_id(txtUserID.Text);
            if (dsprov != null)
            {
                if (dsprov.Tables.Count > 0 && dsprov.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsprov.Tables[0].Rows.Count; i++)
                    {
                        if (OfficeIDs == "")
                        {
                            OfficeIDs = dsprov.Tables[0].Rows[i]["SZ_USER_PROVIDER_NAME_ID"].ToString();
                        }
                        else
                        {
                            OfficeIDs = OfficeIDs+"," + dsprov.Tables[0].Rows[i]["SZ_USER_PROVIDER_NAME_ID"].ToString();
                        }
                    }
                }
            }
            txtOfficeID.Text = OfficeIDs;
            txtofcid.Value = OfficeIDs;
            //if (Session["Office_ID"] != null)
            //{
            Session["Office_ID"] = txtOfficeID.Text;
            //}
            extddlCaseStatus.Flag_ID = txtCompanyID.Text.ToString();
            extddlCaseType.Flag_ID = txtCompanyID.Text.ToString();
            //  extddlInsurance.Flag_ID = txtCompanyID.Text.ToString();
            ////extddlLocation.Flag_ID = txtCompanyID.Text.ToString();
            CaseDetailsBO _objCaseDetailsBO = new CaseDetailsBO();
            string szCaseStatausID = _objCaseDetailsBO.GetCaseSatusId(txtCompanyID.Text);
            extddlCaseStatus.Text = szCaseStatausID;
            //fillcontrol();
            //grdPatientList.XGridBindSearch();//

            
        }
        bindGrid();
    }

    public void fillcontrol()
    {
        //log.Debug("Start FillControl");
        utxtCompanyID.Text = txtCompanyID.Text;
        utxtDateofAccident.Text = txtDateofAccident.Text;
        utxtClaimNumber.Text = txtClaimNumber.Text;
        utxtDateofBirth.Text = txtDateofBirth.Text;
        utxtCaseType.Text = extddlCaseType.Text;
        utxtCaseStatus.Text = extddlCaseStatus.Text;
        if (extddlPatient.Text == "" || extddlPatient.Text == "NA")
        {
            utxtPatientName.Text = txtPatientName.Text;
        }
        else
        {
            utxtPatientName.Text = txtPatientName.Text;
        }
        utxtInsuranceName.Text = txtInsuranceCompany.Text;
        //utxtSSNNo.Text = txtSSNNo.Text;
        utxtCaseNo.Text = txtCaseNo.Text;
        utxtPatientID.Text = txtPatientID.Text;
        //log.Debug("End FillControl");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            // string a=txtCaseNo.Text;
            txtCaseNo.Text = txtCaseNo.Text;
            fillcontrol(); //check serarch parameter
            //grdPatientList.XGridBindSearch();//
            // clearcontrol();//clear all serach parameter for xml
            //SoftDelete();
            bindGrid();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            //usrMessage.SetMessageType(UserControl_ErrorMessageControl.DisplayType.Type_ErrorMessage);
            //usrMessage.PutMessage(ex.ToString());
            //usrMessage.Show();
        }
    }

    protected void grdPatientList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Patient"))
        {
            string command = e.CommandArgument.ToString();
            string[] strsplitcommand = command.Split(',');
            string caseid = strsplitcommand[0].ToString();
            string companyid = strsplitcommand[1].ToString();
        }
    }

    protected void extddlPatient_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    { }

    protected void extddlInsurance_extendDropDown_SelectedIndexChanged(object sender, EventArgs e)
    { }

    #region "ExportTOExcel"
    protected void lnkExportTOExcel_onclick(object sender, EventArgs e)
    {
       // ScriptManager.RegisterClientScriptBlock(this, GetType(), "mm", " window.location.href ='" + ConfigurationManager.AppSettings["FETCHEXCEL_SHEET"].ToString() + grdPatientList.ExportToExcel(ConfigurationManager.AppSettings["EXCEL_SHEET"].ToString()) + "';", true);


    }
    #endregion

    private string lfnFileName()
    {
        System.Random objRandom = new Random();
        DateTime currentDate;
        currentDate = DateTime.Now;
        if (szExcelFileNamePrefix == null)
        {
            szExcelFileNamePrefix = "excel";
        }
        return szExcelFileNamePrefix + "_" + objRandom.Next(1, 10000).ToString() + "_" + currentDate.ToString("yyyyMMddHHmmssms");
    }

    private void bindGrid()
    {
        ArrayList patientSearchArr = new ArrayList();
        patientSearchArr.Add(txtCompanyID.Text);
        patientSearchArr.Add(txtOfficeID.Text);
        patientSearchArr.Add(extddlCaseStatus.Text);
        patientSearchArr.Add(txtCaseNo.Text);
        patientSearchArr.Add(extddlCaseType.Text);
        patientSearchArr.Add(extddlPatient.Text);
        patientSearchArr.Add(extddlInsurance.Text);
        patientSearchArr.Add(txtClaimNumber.Text);
        patientSearchArr.Add(txtDateofAccident.Text);
        patientSearchArr.Add(txtDateofBirth.Text);
        grdPatientList.DataSource = GetPatientInfo(patientSearchArr);
        grdPatientList.DataBind();
    }

    public DataSet GetPatientInfo(ArrayList searchArray)
    {
        String strConn;
        SqlConnection conn;
        SqlDataAdapter sqlda;
        strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString();

        DataSet ds = new DataSet();
        conn = new SqlConnection(strConn);
        try
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("sp_get_provider_patient_list", conn);
            comm.CommandType = CommandType.StoredProcedure;
            // dr = new SqlDataReader();
            comm.Parameters.AddWithValue("@SZ_COMPANY_ID", searchArray[0].ToString());
            comm.Parameters.AddWithValue("@SZ_OFFICE_ID", searchArray[1].ToString());
            if (searchArray[2].ToString() != "" && searchArray[2].ToString() != null && searchArray[2].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@SZ_CASE_STATUS", searchArray[2].ToString());
            }
            if (searchArray[3].ToString() != "" && searchArray[3].ToString() != null && searchArray[3].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@SZ_CASE_NO", searchArray[3].ToString());
            }
            if (searchArray[4].ToString() != "" && searchArray[4].ToString() != null && searchArray[4].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@SZ_CASE_TYPE", searchArray[4].ToString());
            }
            if (searchArray[5].ToString() != "" && searchArray[5].ToString() != null && searchArray[5].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@SZ_PATIENT_ID", searchArray[5].ToString());
            }
            if (searchArray[6].ToString() != "" && searchArray[6].ToString() != null && searchArray[6].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@SZ_INSURANCE_ID", searchArray[6].ToString());
            }
            if (searchArray[7].ToString() != "" && searchArray[7].ToString() != null && searchArray[7].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@SZ_CLAIM_NUMBER", searchArray[7].ToString());
            }
            if (searchArray[8].ToString() != "" && searchArray[8].ToString() != null && searchArray[8].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@DT_ACCIDENT_DATE", searchArray[8].ToString());
            }
            if (searchArray[9].ToString() != "" && searchArray[9].ToString() != null && searchArray[9].ToString() != "NA")
            {
                comm.Parameters.AddWithValue("@DT_OF_BIRTH", searchArray[9].ToString());
            }


            sqlda = new SqlDataAdapter(comm);
            ds = new DataSet();
            sqlda.Fill(ds);

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return ds;
    }

    protected void ASPxCallback1_Callback(object sender, CallbackEventArgs e)
    {
        string sUserName = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
        //   BindGrid();
        GridViewDataColumn caseNo = (GridViewDataColumn)grdPatientList.Columns[2];
        caseNo.Visible = true;
        caseNo.VisibleIndex = 0;

        GridViewDataColumn patientName = (GridViewDataColumn)grdPatientList.Columns[3];
        patientName.Visible = true;
        patientName.VisibleIndex = 1;

        GridViewDataColumn caseLnk = (GridViewDataColumn)grdPatientList.Columns[4];
        caseLnk.Visible = false;
        
        GridViewDataColumn patientLnk = (GridViewDataColumn)grdPatientList.Columns[5];
        patientLnk.Visible = false;
        
        bindGrid();
        string sFileName = null;
        string sRoot = getUploadDocumentPhysicalPath();
        string sFolder = ConfigurationSettings.AppSettings["REPORTFOLDER"].ToString();
        string sName = "PatentList";
        sFileName=sUserName + "_" + sName + "_" + DateTime.Now.ToString("MM_dd_yyyyhhmm") + ".xlsx";
        string sFile = sRoot + sFolder + sFileName;
        grdPatientList.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), "All");
        //string sFile = gb.web.utils.DownloadFilesUtils.GetExportPhysicalPath(sUserName, gb.web.utils.DownloadFilesExportTypes.RECONCILIATION_LIST, out sFileName);
        System.IO.Stream stream = new System.IO.FileStream(sFile, System.IO.FileMode.Create);
        grdExport.WriteXlsx(stream);
        stream.Close();

        //GridViewDataHyperLinkColumn hlnk = (GridViewDataHyperLinkColumn)grdPatientList.FindControl("_self");
        
//        GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;


        ArrayList list = new ArrayList();
        //ConfigurationSettings.AppSettings["FETCHEXCEL_SHEET"].ToString() + sFileName;
        list.Add(ConfigurationSettings.AppSettings["FETCHEXCEL_SHEET"].ToString() + sFileName);
        Session["Download_Files"] = list;
        caseNo.Visible = false;
        patientName.Visible = false;
        caseLnk.Visible = true;
        patientLnk.Visible = true;
    }

    private static string getUploadDocumentPhysicalPath()
    {
        string str = "";
        SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        try
        {
            connection.Open();
            SqlDataReader reader = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", connection).ExecuteReader();
            while (reader.Read())
            {
                str = reader["parametervalue"].ToString();
            }
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        return str;
    }
}