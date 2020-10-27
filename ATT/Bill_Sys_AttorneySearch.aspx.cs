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
using DevExpress.Web;
using Componend;
using System.Data.SqlClient;
using System.IO;
using OboutInc.EasyMenu_Pro;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using log4net;
using Ionic.Zip;
using mbs.ApplicationSettings;
using System.Data.OleDb;

public partial class ATT_Bill_Sys_AttorneySearch : PageBase
{
    private Patient_TVBO _patient_tvbo;
    private Bill_Sys_Visit_BO _bill_Sys_Visit_BO;
    private ArrayList objAdd;
    DAO_NOTES_BO _DAO_NOTES_BO;
    DAO_NOTES_EO _DAO_NOTES_EO;
    private Bill_Sys_PatientBO _bill_Sys_PatientBO;
    //protected System.Web.UI.WebControls.PlaceHolder placeHolder1;
    String strConn;
    SqlConnection sqlCon;
    SqlCommand sqlCmd;
    SqlDataAdapter sqlda;
    DataSet ds;
    bool blnTag = false;
    private Boolean blnWeekShortNames = true;
    DataSet dspatientSearch = new DataSet();
    private string szDateColor_NA = "#ff6347";
    private string szDateColor_TODAY = "#FFFF80";
    Bill_Sys_DeleteBO _deleteOpeation;
    Calendar_DAO objCalendar = null;
    private DataTable dtAllSpecialityEvents, dtAllRoomEvents;
    private DataTable dtVisitType;
    private bool btIsConfig = false;
    private mbs.ApplicationSettings.ApplicationSettings_BO objApplicationBO;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["USER_OBJECT"] == null)
        {
            Response.Redirect("../Bill_Sys_Login.aspx",false);
        }
        txtuserid.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_ID;
        txtCompanyId.Text=((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).SZ_COMPANY_ID;
        if (!IsPostBack)
        {
            if (Request.QueryString["dt"] != null)
            {
                URLIntegrationSecurity.Encryption en = new URLIntegrationSecurity.Encryption();
                string szEncryptedData = Server.UrlDecode(Request.QueryString["dt"].ToString());
                lblUser.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                szEncryptedData = szEncryptedData.Replace("~pl~", "+");
                szEncryptedData = szEncryptedData.Replace("~am~", "&");
                szEncryptedData = szEncryptedData.Replace("~dl~", "$");
                szEncryptedData = szEncryptedData.Replace("~cm~", ",");
                szEncryptedData = szEncryptedData.Replace("~cl~", ":");
                szEncryptedData = szEncryptedData.Replace("~scl~", ";");
                szEncryptedData = szEncryptedData.Replace("~qs~", "?");
                szEncryptedData = szEncryptedData.Replace("~at~", "@");
                string szDecryptedData = en.DecryptURLData(szEncryptedData);
                string[] values = szDecryptedData.Split(',');
                string caseid = values[0];
                string caseno = values[1];
                string companyidlf = values[2];
                string billno = values[3];
                string lawfirmid = values[4];
                ViewState["szlawfirmid"] = lawfirmid;
                if (txtCompanyId.Text == lawfirmid)
                {
                    DataSet dsurlintegrator = new DataSet();
                    dsurlintegrator = GetPatientListForAttornyintegrator(companyidlf, caseid, billno, lawfirmid);
                    grdpatientsearchintegrator.DataSource = dsurlintegrator;
                    grdpatientsearchintegrator.DataBind();
                    tblsearchparam.Visible = false;
                    grdPatientSearch.Visible = false;
                    tblsearchfile.Visible = false;
                }
                else
                {
                    Response.Redirect("../Bill_Sys_Login.aspx", false);
                }
            }

            if (Request.QueryString["dt"] == null)
            {
                 lblUser.Text = ((Bill_Sys_UserObject)Session["USER_OBJECT"]).SZ_USER_NAME;
                 BindCompanyName();
                 _patient_tvbo = new Patient_TVBO();

                 string szCmpId = "";
                 if (ddlCompanyName.SelectedItem.Value == null)
                 {
                     szCmpId = "";
                 }
                 else
                 {
                     szCmpId = ddlCompanyName.SelectedItem.Value.ToString();
                 }
                 dspatientSearch = GetPatientListForAttorny(txtuserid.Text, txtCaseNo.Text, txtPatientName.Text, szCmpId);
                 grdPatientSearch.DataSource = dspatientSearch;
                 grdPatientSearch.DataBind();
                 grdpatientsearchintegrator.Visible = false;
             }
        }
    }

    public void BindCompanyName()
    {
        ListEditItem itm1 = new ListEditItem("--- Select Facility ---", null);
        string query = "select SZ_COMPANY_ID, SZ_COMPANY_NAME from MST_BILLING_COMPANY ";
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Connection_String"]);
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.Connection = con;
        DataSet ds1 = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds1);
        con.Open();
        cmd.ExecuteNonQuery();
        ddlCompanyName.DataSource = ds1;
        ddlCompanyName.DataBind();
        con.Close();
        ddlCompanyName.Items.Insert(0, itm1);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        _patient_tvbo = new Patient_TVBO();

        string szCmpId1 = "";
        if (ddlCompanyName.SelectedItem.Value == null)
        {
            szCmpId1 = "";
        }
        else
        {
            szCmpId1 = ddlCompanyName.SelectedItem.Value.ToString();
        }

        dspatientSearch = GetPatientListForAttorny(txtuserid.Text, txtCaseNo.Text, txtPatientName.Text, szCmpId1);
        grdPatientSearch.DataSource = dspatientSearch;
        grdPatientSearch.DataBind();


    }

    protected void grid_PageIndexChanged(object sender, EventArgs e)
    {
        grdPatientSearch.Settings.ShowVerticalScrollBar = false;
        grdPatientSearch.Settings.ShowHorizontalScrollBar = false;
        _patient_tvbo = new Patient_TVBO();

        string szCmpId1 = "";
        if (ddlCompanyName.SelectedItem.Value == null)
        {
            szCmpId1 = "";
        }
        else
        {
            szCmpId1 = ddlCompanyName.SelectedItem.Value.ToString();
        }

        dspatientSearch = GetPatientListForAttorny(txtuserid.Text, txtCaseNo.Text, txtPatientName.Text, szCmpId1);
        grdPatientSearch.DataSource = dspatientSearch;
        grdPatientSearch.DataBind();

    }

    protected void lnkPatientInfo_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("TestString", "<script language='javascript'> popup_Shown(); </script>");
        object keyValue = grdPatientSearch.GetRowValues(grdPatientSearch.FocusedRowIndex, new string[] { grdPatientSearch.KeyFieldName });

        txtCaseID.Text = grdPatientSearch.GetRowValues(grdPatientSearch.FocusedRowIndex, "Case_Id").ToString();
        txtCompanyId.Text = grdPatientSearch.GetRowValues(grdPatientSearch.FocusedRowIndex, "SZ_COMPANY_ID").ToString();
        txtPatientId.Text = grdPatientSearch.GetRowValues(grdPatientSearch.FocusedRowIndex, "SZ_PATIENT_ID").ToString();
        BindControl();
        BindPatientInfo();
    }

    protected void lnkPatientDesk_Click(object sender, EventArgs e)
    {
        Page.RegisterStartupScript("TestString", "<script language='javascript'> popup_ShownDesk(); </script>");
        txtCaseID.Text = grdPatientSearch.GetRowValues(grdPatientSearch.FocusedRowIndex, "Case_Id").ToString();
        txtCompanyId.Text = grdPatientSearch.GetRowValues(grdPatientSearch.FocusedRowIndex, "SZ_COMPANY_ID").ToString();
        txtPatientId.Text = grdPatientSearch.GetRowValues(grdPatientSearch.FocusedRowIndex, "SZ_PATIENT_ID").ToString();

        BindControlPestDesk();
        LoadTabInformation();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string sz_UserName = "";
        string szRemoteAddr = Page.Request.ServerVariables["REMOTE_ADDR"].ToString();
        string DownloadType = "";
        ArrayList _returnPath = new ArrayList();
        string MainDirectory = "";
        string MDir = "";
        string path = "";
        int iCheckBatch = 0;
        string szFile = "";
        string _fullPath = "";
        string szBillno_download = "";
        string szDefaultPath = "";
        string szBasePhysicalPath = getLawFirmPhysicalPath();
        double dblTSize = Convert.ToDouble(ConfigurationSettings.AppSettings["DownLoadSize"].ToString());
        string PhysicalPath = getPhysicalPath();
        double FileSize = 0;
        string dir = "";
        string zipfilename = "";
        MainDirectory = DateTime.Now.ToString("yyyyMMddHHmmssms") + "/";
        DataTable objDTResult = new DataTable();
        string szLawFirmPhysicalPath = getLawFirmPhysicalPath();
        txtcaseidforlawfirm.Text = grdpatientsearchintegrator.GetRowValues(grdpatientsearchintegrator.FocusedRowIndex, "Case Id").ToString();
        txtCaseNo.Text = grdpatientsearchintegrator.GetRowValues(grdpatientsearchintegrator.FocusedRowIndex, "Case #").ToString();
        txtpatientnamelf.Text = grdpatientsearchintegrator.GetRowValues(grdpatientsearchintegrator.FocusedRowIndex, "Patient").ToString();
        txtcompanyidlf.Text = grdpatientsearchintegrator.GetRowValues(grdpatientsearchintegrator.FocusedRowIndex, "sz_company_name").ToString();
        txtbillno.Text = grdpatientsearchintegrator.GetRowValues(grdpatientsearchintegrator.FocusedRowIndex, "bill_no").ToString();
        txtSpecialty.Text = grdpatientsearchintegrator.GetRowValues(grdpatientsearchintegrator.FocusedRowIndex, "Specialty_Id").ToString();
        txtcompanidlaw.Text = grdpatientsearchintegrator.GetRowValues(grdpatientsearchintegrator.FocusedRowIndex, "CompanyId").ToString();
        DataSet dsDocuments = new DataSet();
        objDTResult.Columns.Add("Case ID");
        objDTResult.Columns.Add("Bill #");
        objDTResult.Columns.Add("Company ID");
        objDTResult.Columns.Add("File Path");
        objDTResult.Columns.Add("MainDir");
        objDTResult.Columns.Add("FileSize");
        dsDocuments = GetDocumentsPath(txtbillno.Text, txtcaseidforlawfirm.Text, txtcompanidlaw.Text, txtSpecialty.Text);
        int count = 0;
        string sz_Bill_No_case = "";
        string szBillnumber = txtbillno.Text;
        string sz_Case_Id=txtcaseidforlawfirm.Text;
        string szCmpName = txtcompanyidlf.Text;
        string sz_ProcedureGroupId = txtSpecialty.Text;
        string szlawfirmid = (string)ViewState["szlawfirmid"];
        string sz_Patient_Name = txtpatientnamelf.Text;
            
            try
            {
                sz_Bill_No_case = "'" + szBillnumber + "'";
                
                for (int j = 0; j < dsDocuments.Tables[0].Rows.Count; j++)
                {

                    if (dsDocuments.Tables[0].Rows[j][2].ToString().Equals("Bill"))
                    {
                        try
                        {
                            if (dsDocuments.Tables[0].Rows[j][2].ToString().Equals("Bill"))
                            {
                                if (!Directory.Exists(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/"))
                                {
                                    Directory.CreateDirectory(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/");
                                    DataRow objDR = objDTResult.NewRow();
                                }
                                if (!File.Exists(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim()))
                                {
                                    File.Copy(dsDocuments.Tables[0].Rows[j][6].ToString() + dsDocuments.Tables[0].Rows[j][0].ToString(), szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim(), true);
                                    DataRow objDR = objDTResult.NewRow();
                                    objDR["Case ID"] = sz_Case_Id;
                                    objDR["Bill #"] = szBillnumber;
                                    objDR["File Path"] = szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim();
                                    objDR["Company ID"] = txtcompanidlaw.Text;
                                    objDR["MainDir"] = MainDirectory;
                                    objDTResult.Rows.Add(objDR);
                                    FileInfo info = new FileInfo(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim());

                                    // Now convert to a string in megabytes.
                                    double s = ConvertBytesToMegabytes(info.Length);
                                    FileSize = FileSize + s;
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                                if (!Directory.Exists(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/"))
                                {
                                    Directory.CreateDirectory(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/");

                                }
                                if (!File.Exists(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim()))
                                {
                                    File.Copy(dsDocuments.Tables[0].Rows[j][6].ToString() + dsDocuments.Tables[0].Rows[j][0].ToString(), szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim());
                                    DataRow objDR = objDTResult.NewRow();
                                    objDR["Case ID"] = sz_Case_Id;
                                    objDR["Bill #"] = szBillnumber;
                                    objDR["File Path"] = szCmpName + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim();
                                    objDR["Company ID"] = txtcompanidlaw.Text;
                                    objDR["MainDir"] = MainDirectory;
                                    objDTResult.Rows.Add(objDR);
                                    FileInfo info = new FileInfo(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim());

                                    //Now convert to a string in megabytes.
                                    double s = ConvertBytesToMegabytes(info.Length);
                                    FileSize = FileSize + s;
                                }
                                else
                                {
                                    DataRow objDR = objDTResult.NewRow();
                                    objDR["Case ID"] = sz_Case_Id;
                                    objDR["Bill #"] = szBillnumber;
                                    objDR["File Path"] = szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim();
                                    objDR["Company ID"] = txtcompanidlaw.Text;
                                    objDR["MainDir"] = MainDirectory;
                                    objDTResult.Rows.Add(objDR);
                                    FileInfo info = new FileInfo(szLawFirmPhysicalPath + "Downloads/" + MainDirectory + szCmpName + "/" + sz_Patient_Name + "/" + dsDocuments.Tables[0].Rows[j][2].ToString().Trim() + "/" + dsDocuments.Tables[0].Rows[j][3].ToString().Trim());

                                    // Now convert to a string in megabytes.
                                    double s = ConvertBytesToMegabytes(info.Length);
                                    FileSize = FileSize + s;
                                }

                            }
                        }
                        catch (Exception exCollectDocumentsInner)
                        {

                        }
                    }

                    if (FileSize >= dblTSize)
                    {
                        FileSize = 0.001;

                        MDir = DateTime.Now.ToString("yyyyMMddHHmmssms") + "/"; ;
                        szDefaultPath = "Downloads/" + MDir;
                        string dPath = szBasePhysicalPath + "Downloads/" + MainDirectory;
                        _fullPath = szBasePhysicalPath + szDefaultPath;
                        if (!Directory.Exists(_fullPath))
                        {
                            Directory.CreateDirectory(_fullPath);
                        }
                        if (!Directory.Exists(dPath))
                        {
                            Directory.CreateDirectory(_fullPath);
                            
                        }
                        dir = _fullPath;
                        Directory.SetCurrentDirectory(dPath);
                        ZipFile zip = new ZipFile();
                        DataTable dtTab = new DataTable();
                        DataSet BatchInfoDs = new DataSet();
                        dtTab = objDTResult;
                        BatchInfoDs.Tables.Add(dtTab.Copy());
                        dir = _fullPath;
                        for (int iRowCount = 0; iRowCount < BatchInfoDs.Tables[0].Rows.Count; iRowCount++)
                        {
                            szFile = BatchInfoDs.Tables[0].Rows[iRowCount]["File Path"].ToString().Replace(dir, "");
                            try
                            {
                                zip.AddFile(szFile);
                                if (iRowCount == BatchInfoDs.Tables[0].Rows.Count - 1)
                                {
                                    Directory.SetCurrentDirectory(dPath);
                                    StringBuilder sbXML = new StringBuilder();
                                    StringBuilder sbCSV = new StringBuilder();
                                    DataSet ds1 = new DataSet();
                                    ArrayList files = new ArrayList();
                                    ds1 = GetPateintInfo(sz_Bill_No_case);
                                    files = CreateXml(ds1);
                                    sbXML = (StringBuilder)files[0];
                                    sbCSV = (StringBuilder)files[1];
                                    string xmlName = getFileName();
                                    FileStream fs;
                                    fs = new FileStream(dPath + xmlName, FileMode.OpenOrCreate);
                                    StreamWriter objStreamWriter = new StreamWriter(fs);
                                    objStreamWriter.Write(sbXML.ToString());
                                    objStreamWriter.Close();
                                    //zip.AddFile(xmlName);
                                    try
                                    {
                                        zip.AddFile(xmlName);
                                    }
                                    catch (Exception exAddZip1)
                                    {
                                        
                                    }

                                    string exlName = xmlName.Replace(".xml", ".xls");
                                    //exlName = _fullPath + exlName;

                                    string cvsName = xmlName.Replace(".xml", ".csv");
                                    FileStream fs1;
                                    fs1 = new FileStream(dPath + cvsName, FileMode.OpenOrCreate);
                                    StreamWriter objStreamWriter1 = new StreamWriter(fs1);
                                    objStreamWriter1.Write(sbCSV.ToString());
                                    objStreamWriter1.Close();

                                    //zip.AddFile(cvsName);
                                    try
                                    {
                                        zip.AddFile(cvsName);
                                    }
                                    catch (Exception exAddZip1)
                                    {
                                       
                                    }
                                    DataSet dsXL = new DataSet();
                                    //dsXL = GetXLInfo(sz_Bill_No);

                                    string file_path = ConfigurationSettings.AppSettings["XLPATH"].ToString();
                                    File.Copy(file_path, dPath + exlName);
                                    GenerateXL(ds1.Tables[0], dPath + exlName);
                                    //GenerateXL(dsXL.Tables[0], _fullPath + exlName);
                                    try
                                    {
                                        zip.AddFile(exlName);
                                    }

                                    catch (Exception exxmlName)
                                    {
                                      
                                    }
                                    Directory.SetCurrentDirectory(dPath);
                                    DateTime currentDate1 = new DateTime();
                                    currentDate1 = DateTime.Now;
                                     zipfilename = currentDate1.ToString("yyyyMMddHHmmssms") + ".zip";
                                  
                                    try
                                    {
                                        zip.Save(zipfilename);
                                        _returnPath.Add(dPath + zipfilename);

                                        //string sz_batch_name = "";
                                        //if (DownloadType == "Y")
                                        //{
                                        //    sz_batch_name = SaveBatchBills(getLawFirmLogicalPath() + path, sz_UserName);
                                            
                                        //}
                                        //string[] strSplitArr = sz_Bill_No_case.Split(',');
                                        //for (int cnt = 0; cnt < strSplitArr.Length; cnt++)
                                        //{
                                        //    string szBillNumber = strSplitArr[cnt].Replace("'", "");

                                        //    updateBillStatusNew(szBillNumber.Trim());
                                           
                                        //    //if (DownloadType == "Y")
                                        //    //{
                                        //    //    AddBatchBill(szBillNumber.Trim(), sz_user_id, sz_batch_name, sz_lawfirm_id, "BATCH", sz_ip_addres);
                                        //    //    //log.Debug("Bill Added To Batch" + szBillNumber.Trim());
                                        //    //}

                                        //}

                                        //sz_Bill_No = "";
                                        //sz_Bill_No_case = "";
                                    }
                                    catch (Exception savezip)
                                    {
                                      
                                    }
                                 
                                    MainDirectory = DateTime.Now.ToString("yyyyMMddHHmmssms") + "/";
                                }

                            }
                            catch (Exception exAddZip)
                            {
                              
                            }

                        }

                        int cnt1 = objDTResult.Rows.Count;
                        for (int z = 0; z < cnt1; z++)
                        {
                            objDTResult.Rows.RemoveAt(0);
                        }
                        objDTResult.AcceptChanges();

                        dtTab = objDTResult;
                        dtTab.AcceptChanges();
                        BatchInfoDs.AcceptChanges();
                    }
                    if (FileSize < dblTSize && j == dsDocuments.Tables[0].Rows.Count - 1)
                    {
                        szDefaultPath = "Downloads/" + MDir;

                        _fullPath = szBasePhysicalPath + szDefaultPath;
                        string dPath = szBasePhysicalPath + "Downloads/" + MainDirectory;

                        if (!Directory.Exists(_fullPath))
                        {
                            Directory.CreateDirectory(_fullPath);
                        }

                        if (!Directory.Exists(dPath))
                        {
                            Directory.CreateDirectory(_fullPath);
                        }
                        dir = _fullPath;
                        Directory.SetCurrentDirectory(dPath);
                        ZipFile zip = new ZipFile();
                        DataTable dtTab = new DataTable();
                        DataSet BatchInfoDs = new DataSet();
                        dtTab = objDTResult;
                        BatchInfoDs.Tables.Add(dtTab.Copy());
                        dir = _fullPath;
                        for (int iRowCount = 0; iRowCount < BatchInfoDs.Tables[0].Rows.Count; iRowCount++)
                        {
                            szFile = BatchInfoDs.Tables[0].Rows[iRowCount]["File Path"].ToString().Replace(dir, "");
                            try
                            {
                                zip.AddFile(szFile);


                                if (iRowCount == BatchInfoDs.Tables[0].Rows.Count - 1)
                                {
                                    Directory.SetCurrentDirectory(dPath);
                                    StringBuilder sbXML = new StringBuilder();
                                    StringBuilder sbCSV = new StringBuilder();
                                    DataSet ds1 = new DataSet();
                                    ArrayList files = new ArrayList();
                                    ds1 = GetPateintInfo(sz_Bill_No_case);
                                    files = CreateXml(ds1);
                                    sbXML = (StringBuilder)files[0];
                                    sbCSV = (StringBuilder)files[1];

                                    string xmlName = getFileName();
                                    FileStream fs;
                                    fs = new FileStream(dPath + xmlName, FileMode.OpenOrCreate);
                                    StreamWriter objStreamWriter = new StreamWriter(fs);
                                    objStreamWriter.Write(sbXML.ToString());
                                    objStreamWriter.Close();
                                    //zip.AddFile(xmlName);
                                    try
                                    {
                                        zip.AddFile(xmlName);
                                    }
                                    catch (Exception exAddZip1)
                                    {
                                    }

                                    string exlName = xmlName.Replace(".xml", ".xls");
                                    //exlName = _fullPath + exlName;

                                    string cvsName = xmlName.Replace(".xml", ".csv");
                                    FileStream fs1;
                                    fs1 = new FileStream(dPath + cvsName, FileMode.OpenOrCreate);
                                    StreamWriter objStreamWriter1 = new StreamWriter(fs1);
                                    objStreamWriter1.Write(sbCSV.ToString());
                                    objStreamWriter1.Close();

                                    //zip.AddFile(cvsName);
                                    try
                                    {
                                        zip.AddFile(cvsName);
                                    }
                                    catch (Exception exAddZip1)
                                    {
                                    }
                                    DataSet dsXL = new DataSet();
                                    //dsXL = GetXLInfo(sz_Bill_No);

                                    string file_path = ConfigurationSettings.AppSettings["XLPATH"].ToString();
                                    File.Copy(file_path, dPath + exlName);
                                     GenerateXL(ds1.Tables[0], dPath + exlName);
                                    //GenerateXL(dsXL.Tables[0], _fullPath + exlName);
                                    try
                                    {
                                        zip.AddFile(exlName);
                                    }

                                    catch (Exception exxmlName)
                                    {
                                       
                                    }
                                    //sz_Bill_No = "";
                                    Directory.SetCurrentDirectory(dPath);
                                    DateTime currentDate1 = new DateTime();
                                    currentDate1 = DateTime.Now;
                                    zipfilename = currentDate1.ToString("yyyyMMddHHmmssms") + ".zip";
                                    
                                    try
                                    {
                                        zip.Save(zipfilename);
                                        Saveurlintegrator(szRemoteAddr, txtuserid.Text, DateTime.Now.ToString(), txtcompanidlaw.Text, zipfilename, szBillnumber, txtCaseNo.Text, sz_Case_Id, szlawfirmid);
                                        _returnPath.Add(dPath + zipfilename);

                                        //string sz_batch_name = "";
                                        //if (DownloadType == "Y")
                                        //{
                                        //    sz_batch_name = SaveBatchBills(getLawFirmLogicalPath() + path, sz_UserName);
                                           
                                        //}
                                        //string[] strSplitArr = sz_Bill_No_case.Split(',');
                                        //for (int cnt = 0; cnt < strSplitArr.Length; cnt++)
                                        //{
                                        //    string szBillNumber = strSplitArr[cnt].Replace("'", "");

                                        //    updateBillStatusNew(szBillNumber.Trim());
                                           
                                        //    if (DownloadType == "Y")
                                        //    {
                                        //        //AddBatchBill(szBillNumber.Trim(), sz_user_id, sz_batch_name, sz_lawfirm_id, "BATCH", sz_ip_addres);
                                                
                                        //    }

                                        //}

                                        //sz_Bill_No = "";
                                        //sz_Bill_No_case = "";

                                    }
                                    catch (Exception savezip)
                                    {
                                    }
                                   
                                    MainDirectory = DateTime.Now.ToString("yyyyMMddHHmmssms") + "/"; ;
                                }
                            }
                            catch (Exception exAddZip)
                            {
                               
                            }
                        }

                        int cnt2 = objDTResult.Rows.Count;
                        for (int z = 0; z < cnt2; z++)
                        {
                            objDTResult.Rows.RemoveAt(0);
                        }
                        objDTResult.AcceptChanges();
                        dtTab = objDTResult;
                        dtTab.AcceptChanges();
                        BatchInfoDs.AcceptChanges();
                    }
                    
            }
           
       }
            catch (Exception exCollectDocuments)
            {
               
            }
            
        //}
       
        if (_returnPath.Count < 1)
        {

            ScriptManager.RegisterClientScriptBlock(this, GetType(), "Done", "alert('There are no files available on the server to download.');", true);
        }
        else
        {

            for (int i = 0; i < _returnPath.Count; i++)
            {
                string pathlf = "";
                pathlf = _returnPath[i].ToString().Replace(ConfigurationSettings.AppSettings["BASEPATH_OF_DOWNLOAD"].ToString(), "");
                ScriptManager.RegisterStartupScript(this, typeof(string), "popup" + i.ToString(), "window.open('" + ConfigurationSettings.AppSettings["LFIRMDOCURL"].ToString() + pathlf.Trim() + "'); ", true);
            }


        }
       
   }

    public void AddBatchBill(string sz_bill_no, string sz_user_id, string sz_batch_name, string sz_lawfirm_id, string sz_download_as, string sz_ip_addres)
    {
      
        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        DataSet ds = new DataSet();
        string szCompanyName = "";
        try
        {
            con.Open();
           
            SqlCommand cmd = new SqlCommand("SP_SAVE_BATCH_FOR_BILL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", sz_bill_no);
            cmd.Parameters.AddWithValue("@SZ_USER_ID", sz_user_id);
            cmd.Parameters.AddWithValue("@SZ_BATCH_NAME", sz_batch_name);
            cmd.Parameters.AddWithValue("@SZ_LAWFIRM_ID", sz_lawfirm_id);
            cmd.Parameters.AddWithValue("@SZ_DOWNLOAD_AS", sz_download_as);
            cmd.Parameters.AddWithValue("@SZ_IP_ADDRESS", sz_ip_addres);
            cmd.ExecuteNonQuery();

        }
        catch (Exception exGetCompanyName)
        {
            
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    public void updateBillStatusNew(string szBillNo)
    {
        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_BILLSTATUS_NEW", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNo);
            cmd.ExecuteNonQuery();
        }
        catch (Exception exgetStatusId)
        {
           
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }

    public string getLawFirmLogicalPath()
    {
        String szParamValue = "";
        string strConn = "";
        strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();


            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'LawFirmDoclogicalPath'", sqlCon);
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
            }

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();

            }
        }
        return szParamValue;
    }

    public string SaveBatchBills(string sz_batch_path, string szUserName)
    {
        string Return_sz_batch_path = "";
        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        DataSet ds = new DataSet();
        int iFlag = 0;
        string szCompanyName = "";
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_SAVE_BATCH", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DT_BATCH_DATE", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@SZ_BATCH_PATH", sz_batch_path);
            cmd.Parameters.AddWithValue("@SZ_USER_NAME", szUserName);
            cmd.Parameters.Add("@SZ_NEW_BATCH_NAME", SqlDbType.NVarChar, 50);
            cmd.Parameters["@SZ_NEW_BATCH_NAME"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            Return_sz_batch_path = cmd.Parameters["@SZ_NEW_BATCH_NAME"].Value.ToString();
        }
        catch (Exception exGetCompanyName)
        {
           
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return Return_sz_batch_path;
    }

    public void GenerateXL(DataTable dt, string file)
    {
     

         mbs.ApplicationSettings.ApplicationSettings_BO objAppSettings = this.objApplicationBO;

        // if the application settings havent been loaded then load them again from the database
        if (objAppSettings == null)
            objAppSettings = new mbs.ApplicationSettings.ApplicationSettings_BO();

        string path = ((mbs.ApplicationSettings.ApplicationSettings_DO)objAppSettings.getParameterValue(mbs.ApplicationSettings.ApplicationSettings_BO.KEY_DOMAIN_PATH_FOR_DOCS)).ParameterValue;
        string oleconnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties='Excel 12.0;HDR=Yes;'";
        OleDbConnection olecon = new OleDbConnection(oleconnection);
        try
        {
           
            olecon.Open();
            //string path = ConfigurationSettings.AppSettings["pathinfo"].ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OleDbCommand olecom = new OleDbCommand();
                olecom.CommandType = CommandType.Text;
                olecom.CommandTimeout = 0;
                //olecom.CommandText = "INSERT INTO [Sheet1$]([case_id],[case_no],[patient_name],[insurance_name],[insurance_address],[insurance_city], [insurance_state],[insurance_zip],[insurance_phone], [insurance_email],[patient_address], [patient_Street],[patient_city], [patient_state],[patient_zip], [patient_phone], [date_of_accident],  [policy_number],  [claim_number],[status_name], [attorney_first_name], [attorney_last_name],[attorney_address], [attorney_city],[attorney_state],[attorney_zip],[attorney_fax], [ssn_no],  [date_of_birth],[policy_holder],[bill_number], [provider],[date_of_service], [bill_amount], [paid_amount], [balance]   )values(" + dt.Rows[i]["Case Id"] + "," + dt.Rows[i]["Case no"] + ",'" + dt.Rows[i]["Patient Name"] + "','" + dt.Rows[i]["Ins Name"] + "','" + dt.Rows[i]["Ins Address"] + "','" + dt.Rows[i]["Ins City"] + "','" + dt.Rows[i]["Ins State"] + "','" + dt.Rows[i]["Ins Zip"] + "','" + dt.Rows[i]["Ins Phone"] + "','" + dt.Rows[i]["Ins Email"] + "','" + dt.Rows[i]["Patient Address"] + "','" + dt.Rows[i]["Patient Street"] + "','" + dt.Rows[i]["Patient City"] + "','" + dt.Rows[i]["Patient State"] + "','" + dt.Rows[i]["Patient Zip"] + "','" + dt.Rows[i]["Patient Phone"] + "','" + dt.Rows[i]["Date of accident"] + "','" + dt.Rows[i]["Policy no"] + "','" + dt.Rows[i]["Claim no"] + "','" + dt.Rows[i]["Case Status"] + "','" + dt.Rows[i]["Attorney"] + "','" + dt.Rows[i]["Attorney Name"] + "','" + dt.Rows[i]["Attorney Address"] + "','" + dt.Rows[i]["Attorney City"] + "','" + dt.Rows[i]["Attorney State"] + "','" + dt.Rows[i]["Attorney Zip"] + "','" + dt.Rows[i]["Attorney Fax"] + "','" + dt.Rows[i]["SSN No"] + "','" + dt.Rows[i]["Date of birth"] + "','" + dt.Rows[i]["Policy Holder"] + "','" + dt.Rows[i]["Bill no"] + "','" + dt.Rows[i]["SZ_OFFICE"] + "','" + dt.Rows[i]["Date of service"] + "','" + dt.Rows[i]["Bill"] + "','" + dt.Rows[i]["Paid"] + "','" + dt.Rows[i]["Outstanding"] + "')";//  )"; 
                string SZ_PROVIDER = GetProvider(dt.Rows[i]["SZ_BILL_NUMBER"].ToString());
                string sz_path_link = GetEncryptedURLIntegrationData(path, dt.Rows[i]["SZ_CASE_ID"].ToString(), dt.Rows[i]["SZ_CASE_NO"].ToString(), dt.Rows[i]["SZ_COMPANY_ID"].ToString(), dt.Rows[i]["SZ_BILL_NUMBER"].ToString(), dt.Rows[i]["LAWFIRM_ID"].ToString());
                olecom.CommandText = "INSERT INTO [Sheet1$]([case_id],[case_no],[patient_name],[patient_address],[patient_city],[patient_state], [patient_zip],[patient_phone],[date_of_accident], [date_of_birth],[policy_holder], [policy_number],[claim_number], [insurance_name],[insurance_address], [insurance_city], [insurance_state],  [insurance_zip],  [insurance_email],[status_name], [attorney_name],[attorney_address], [attorney_city], [attorney_state],[attorney_zip],[attorney_fax], [provider],  [bill_number],[start_visit_date],[end_visit_date], [bill_amount],[paid_amount], [balance], [case_type],[patient_id],[location],[sz_path_link])values(" + dt.Rows[i]["SZ_CASE_ID"] + "," + dt.Rows[i]["SZ_CASE_NO"] + ",'" + dt.Rows[i]["SZ_PATIENT_NAME"] + "','" + dt.Rows[i]["SZ_PATIENT_ADDRESS"] + "','" + dt.Rows[i]["SZ_PATIENT_CITY"] + "','" + dt.Rows[i]["SZ_PATIENT_STATE"] + "','" + dt.Rows[i]["SZ_PATIENT_ZIP"] + "','" + dt.Rows[i]["SZ_PATIENT_PHONE"] + "','" + dt.Rows[i]["DT_DATE_OF_ACCIDENT"] + "','" + dt.Rows[i]["DT_DOB"] + "','" + dt.Rows[i]["SZ_POLICY_HOLDER"] + "','" + dt.Rows[i]["SZ_POLICY_NUMBER"] + "','" + dt.Rows[i]["SZ_CLAIM_NUMBER"] + "','" + dt.Rows[i]["SZ_INSURANCE_NAME"] + "','" + dt.Rows[i]["SZ_INSURANCE_ADDRESS"] + "','" + dt.Rows[i]["SZ_INSURANCE_CITY"] + "','" + dt.Rows[i]["SZ_STATE"] + "','" + dt.Rows[i]["SZ_INSURANCE_ZIP"] + "','" + dt.Rows[i]["SZ_INSURANCE_EMAIL"] + "','" + dt.Rows[i]["SZ_STATUS_NAME"] + "','" + dt.Rows[i]["SZ_ATTORNEY_NAME"] + "','" + dt.Rows[i]["SZ_ATTORNEY_ADDRESS"] + "','" + dt.Rows[i]["SZ_ATTORNEY_CITY"] + "','" + dt.Rows[i]["SZ_ATTORNEY_STATE"] + "','" + dt.Rows[i]["SZ_ATTORNEY_ZIP"] + "','" + dt.Rows[i]["SZ_ATTORNEY_FAX"] + "','" + SZ_PROVIDER + "','" + dt.Rows[i]["SZ_BILL_NUMBER"] + "','" + dt.Rows[i]["DT_START_VISIT_DATE"] + "','" + dt.Rows[i]["DT_END_VISIT_DATE"] + "','" + dt.Rows[i]["FLT_BILL_AMOUNT"] + "','" + dt.Rows[i]["FLT_PAID"] + "','" + dt.Rows[i]["FLT_BALANCE"] + "', '" + dt.Rows[i]["SZ_CASE_TYPE_NAME"] + "','" + dt.Rows[i]["sz_remote_case_id"] + "','" + dt.Rows[i]["Loaction"] + "','" + sz_path_link + "')";
                olecom.Connection = olecon;
                olecom.ExecuteNonQuery();
               
            }
        }
        catch (Exception exGenerateXL)
        {
            
        }
        finally
        {
            olecon.Close();

        }

    }

    private string GetEncryptedURLIntegrationData(string path, string caseid, string caseno, string cmpid, string billno, string lawfirmid)
    {
        URLIntegrationSecurity.Encryption en = new URLIntegrationSecurity.Encryption();
        string sURLData = caseid + ',' + caseno + ',' + cmpid + ',' + billno + ',' + lawfirmid;
        string szEncryptedData = en.EncryptURLData(sURLData);
        string finalpath = path + "?dt=" + szEncryptedData;
        return finalpath;
    }     

    public string getFileName()
    {
        String szFileName;
        DateTime currentDate;
        currentDate = DateTime.Now;
        szFileName = "Data" + "_" + currentDate.ToString("yyyyMMddHHmmssms") + ".xml";
        return szFileName;
    }

    public ArrayList CreateXml(DataSet ds)
    {
        StringBuilder sbXML = new StringBuilder();
        StringBuilder sbCSV = new StringBuilder();
        ArrayList Files = new ArrayList();
        sbCSV.Append("");
        sbXML.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sbXML.AppendLine("<FILEDETAILS>");

        try
        {
            sbCSV.Append("case_id;case_no;patient_name;patient_address;patient_city;patient_state;patient_zip;patient_phone;date_of_accident;date_of_birth;policy_holder;policy_number;claim_number;insurance_name;insurance_address;insurance_city;insurance_state;insurance_zip;insurance_email;status_name;attorney_name;attorney_city;attorney_state;attorney_zip;attorney_fax;provider;bill_number;start_visit_date;end_visit_date;bill_amount;paid_amount;balance;");

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sbCSV.AppendLine("");
                sbXML.AppendLine("<FILE>");

                string SZ_CASE_ID = ds.Tables[0].Rows[i]["SZ_CASE_ID"].ToString();
                if (SZ_CASE_ID.Contains(";"))
                {
                    SZ_CASE_ID = SZ_CASE_ID.Replace(";", ",");
                } if (SZ_CASE_ID.Contains("&"))
                {
                    SZ_CASE_ID = SZ_CASE_ID.Replace("&", "amp");
                } if (SZ_CASE_ID.Contains("<"))
                {
                    SZ_CASE_ID = SZ_CASE_ID.Replace("<", "LS");
                } if (SZ_CASE_ID.Contains(">"))
                {
                    SZ_CASE_ID = SZ_CASE_ID.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_CASE_ID>" + SZ_CASE_ID + "</SZ_CASE_ID>");
                sbCSV.Append(SZ_CASE_ID + ";");

                string SZ_CASE_NO = ds.Tables[0].Rows[i]["SZ_CASE_NO"].ToString();
                if (SZ_CASE_NO.Contains(";"))
                {
                    SZ_CASE_NO = SZ_CASE_NO.Replace(";", ",");
                } if (SZ_CASE_NO.Contains("&"))
                {
                    SZ_CASE_NO = SZ_CASE_NO.Replace("&", "amp");
                } if (SZ_CASE_NO.Contains("<"))
                {
                    SZ_CASE_NO = SZ_CASE_NO.Replace("<", "LS");
                } if (SZ_CASE_NO.Contains(">"))
                {
                    SZ_CASE_NO = SZ_CASE_NO.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_CASE_NO>" + SZ_CASE_NO + "</SZ_CASE_NO>");
                sbCSV.Append(SZ_CASE_NO + ";");

                string SZ_PATIENT_NAME = ds.Tables[0].Rows[i]["SZ_PATIENT_NAME"].ToString();
                if (SZ_PATIENT_NAME.Contains(";"))
                {
                    SZ_PATIENT_NAME = SZ_PATIENT_NAME.Replace(";", ",");
                } if (SZ_PATIENT_NAME.Contains("&"))
                {
                    SZ_PATIENT_NAME = SZ_PATIENT_NAME.Replace("&", "amp");
                } if (SZ_PATIENT_NAME.Contains("<"))
                {
                    SZ_PATIENT_NAME = SZ_PATIENT_NAME.Replace("<", "LS");
                } if (SZ_PATIENT_NAME.Contains(">"))
                {
                    SZ_PATIENT_NAME = SZ_PATIENT_NAME.Replace(">", "GT");
                }


                sbXML.AppendLine("<SZ_PATIENT_NAME>" + SZ_PATIENT_NAME + "</SZ_PATIENT_NAME>");
                sbCSV.Append(SZ_PATIENT_NAME + ";");

                string SZ_PATIENT_ADDRESS = ds.Tables[0].Rows[i]["SZ_PATIENT_ADDRESS"].ToString();
                if (SZ_PATIENT_ADDRESS.Contains(";"))
                {
                    SZ_PATIENT_ADDRESS = SZ_PATIENT_ADDRESS.Replace(";", ",");
                } if (SZ_PATIENT_ADDRESS.Contains("&"))
                {
                    SZ_PATIENT_ADDRESS = SZ_PATIENT_ADDRESS.Replace("&", "amp");
                } if (SZ_PATIENT_ADDRESS.Contains("<"))
                {
                    SZ_PATIENT_ADDRESS = SZ_PATIENT_ADDRESS.Replace("<", "LS");
                } if (SZ_PATIENT_ADDRESS.Contains(">"))
                {
                    SZ_PATIENT_ADDRESS = SZ_PATIENT_ADDRESS.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_PATIENT_ADDRESS>" + SZ_PATIENT_ADDRESS + "</SZ_PATIENT_ADDRESS>");
                sbCSV.Append(SZ_PATIENT_ADDRESS + ";");

                string SZ_PATIENT_CITY = ds.Tables[0].Rows[i]["SZ_PATIENT_CITY"].ToString();
                if (SZ_PATIENT_CITY.Contains(";"))
                {
                    SZ_PATIENT_CITY = SZ_PATIENT_CITY.Replace(";", ",");
                } if (SZ_PATIENT_CITY.Contains("&"))
                {
                    SZ_PATIENT_CITY = SZ_PATIENT_CITY.Replace("&", "amp");
                } if (SZ_PATIENT_CITY.Contains("<"))
                {
                    SZ_PATIENT_CITY = SZ_PATIENT_CITY.Replace("<", "LS");
                } if (SZ_PATIENT_CITY.Contains(">"))
                {
                    SZ_PATIENT_CITY = SZ_PATIENT_CITY.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_PATIENT_CITY>" + SZ_PATIENT_CITY + "</SZ_PATIENT_CITY>");
                sbCSV.Append(SZ_PATIENT_CITY + ";");

                string SZ_PATIENT_STATE = ds.Tables[0].Rows[i]["SZ_PATIENT_STATE"].ToString();
                if (SZ_PATIENT_STATE.Contains(";"))
                {
                    SZ_PATIENT_STATE = SZ_PATIENT_STATE.Replace(";", ",");
                } if (SZ_PATIENT_STATE.Contains("&"))
                {
                    SZ_PATIENT_STATE = SZ_PATIENT_STATE.Replace("&", "amp");
                } if (SZ_PATIENT_STATE.Contains("<"))
                {
                    SZ_PATIENT_STATE = SZ_PATIENT_STATE.Replace("<", "LS");
                } if (SZ_PATIENT_STATE.Contains(">"))
                {
                    SZ_PATIENT_STATE = SZ_PATIENT_STATE.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_PATIENT_STATE>" + SZ_PATIENT_STATE + "</SZ_PATIENT_STATE>");
                sbCSV.Append(SZ_PATIENT_STATE + ";");

                string SZ_PATIENT_ZIP = ds.Tables[0].Rows[i]["SZ_PATIENT_ZIP"].ToString();
                if (SZ_PATIENT_ZIP.Contains(";"))
                {
                    SZ_PATIENT_ZIP = SZ_PATIENT_ZIP.Replace(";", ",");
                } if (SZ_PATIENT_ZIP.Contains("&"))
                {
                    SZ_PATIENT_ZIP = SZ_PATIENT_ZIP.Replace("&", "amp");
                } if (SZ_PATIENT_ZIP.Contains("<"))
                {
                    SZ_PATIENT_ZIP = SZ_PATIENT_ZIP.Replace("<", "LS");
                } if (SZ_PATIENT_ZIP.Contains(">"))
                {
                    SZ_PATIENT_ZIP = SZ_PATIENT_ZIP.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_PATIENT_ZIP>" + SZ_PATIENT_ZIP + "</SZ_PATIENT_ZIP>");
                sbCSV.Append(SZ_PATIENT_ZIP + ";");

                string SZ_PATIENT_PHONE = ds.Tables[0].Rows[i]["SZ_PATIENT_PHONE"].ToString();
                if (SZ_PATIENT_PHONE.Contains(";"))
                {
                    SZ_PATIENT_PHONE = SZ_PATIENT_PHONE.Replace(";", ",");
                } if (SZ_PATIENT_PHONE.Contains("&"))
                {
                    SZ_PATIENT_PHONE = SZ_PATIENT_PHONE.Replace("&", "amp");
                } if (SZ_PATIENT_PHONE.Contains("<"))
                {
                    SZ_PATIENT_PHONE = SZ_PATIENT_PHONE.Replace("<", "LS");
                } if (SZ_PATIENT_PHONE.Contains(">"))
                {
                    SZ_PATIENT_PHONE = SZ_PATIENT_PHONE.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_PATIENT_PHONE>" + SZ_PATIENT_PHONE + "</SZ_PATIENT_PHONE>");
                sbCSV.Append(SZ_PATIENT_PHONE + ";");
                string DT_DATE_OF_ACCIDENT = ds.Tables[0].Rows[i]["DT_DATE_OF_ACCIDENT"].ToString();
                if (DT_DATE_OF_ACCIDENT.Contains(";"))
                {
                    DT_DATE_OF_ACCIDENT = DT_DATE_OF_ACCIDENT.Replace(";", ",");
                } if (DT_DATE_OF_ACCIDENT.Contains("&"))
                {
                    DT_DATE_OF_ACCIDENT = DT_DATE_OF_ACCIDENT.Replace("&", "amp");
                } if (DT_DATE_OF_ACCIDENT.Contains("<"))
                {
                    DT_DATE_OF_ACCIDENT = DT_DATE_OF_ACCIDENT.Replace("<", "LS");
                } if (DT_DATE_OF_ACCIDENT.Contains(">"))
                {
                    DT_DATE_OF_ACCIDENT = DT_DATE_OF_ACCIDENT.Replace(">", "GT");
                }
                sbXML.AppendLine("<DT_DATE_OF_ACCIDENT>" + DT_DATE_OF_ACCIDENT + "</DT_DATE_OF_ACCIDENT>");
                sbCSV.Append(DT_DATE_OF_ACCIDENT + ";");
                string DT_DOB = ds.Tables[0].Rows[i]["DT_DOB"].ToString();

                if (DT_DOB.Contains(";"))
                {
                    DT_DOB = DT_DOB.Replace(";", ",");
                } if (DT_DOB.Contains("&"))
                {
                    DT_DOB = DT_DOB.Replace("&", "amp");
                } if (DT_DOB.Contains("<"))
                {
                    DT_DOB = DT_DOB.Replace("<", "LS");
                } if (DT_DOB.Contains(">"))
                {
                    DT_DOB = DT_DOB.Replace(">", "GT");
                }
                sbXML.AppendLine("<DT_DOB>" + DT_DOB + "</DT_DOB>");
                sbCSV.Append(DT_DOB + ";");

                string SZ_POLICY_HOLDER = ds.Tables[0].Rows[i]["SZ_POLICY_HOLDER"].ToString();

                if (SZ_POLICY_HOLDER.Contains(";"))
                {
                    SZ_POLICY_HOLDER = SZ_POLICY_HOLDER.Replace(";", ",");
                } if (SZ_POLICY_HOLDER.Contains("&"))
                {
                    SZ_POLICY_HOLDER = SZ_POLICY_HOLDER.Replace("&", "amp");
                } if (SZ_POLICY_HOLDER.Contains("<"))
                {
                    SZ_POLICY_HOLDER = SZ_POLICY_HOLDER.Replace("<", "LS");
                } if (SZ_POLICY_HOLDER.Contains(">"))
                {
                    SZ_POLICY_HOLDER = SZ_POLICY_HOLDER.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_POLICY_HOLDER>" + SZ_POLICY_HOLDER + "</SZ_POLICY_HOLDER>");
                sbCSV.Append(SZ_POLICY_HOLDER + ";");

                string SZ_POLICY_NUMBER = ds.Tables[0].Rows[i]["SZ_POLICY_NUMBER"].ToString();
                if (SZ_POLICY_NUMBER.Contains(";"))
                {
                    SZ_POLICY_NUMBER = SZ_POLICY_NUMBER.Replace(";", ",");
                } if (SZ_POLICY_NUMBER.Contains("&"))
                {
                    SZ_POLICY_NUMBER = SZ_POLICY_NUMBER.Replace("&", "amp");
                } if (SZ_POLICY_NUMBER.Contains("<"))
                {
                    SZ_POLICY_NUMBER = SZ_POLICY_NUMBER.Replace("<", "LS");
                } if (SZ_POLICY_NUMBER.Contains(">"))
                {
                    SZ_POLICY_NUMBER = SZ_POLICY_NUMBER.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_POLICY_NUMBER>" + SZ_POLICY_NUMBER + "</SZ_POLICY_NUMBER>");
                sbCSV.Append(SZ_POLICY_NUMBER + ";");

                string SZ_CLAIM_NUMBER = ds.Tables[0].Rows[i]["SZ_CLAIM_NUMBER"].ToString();
                if (SZ_CLAIM_NUMBER.Contains(";"))
                {
                    SZ_CLAIM_NUMBER = SZ_CLAIM_NUMBER.Replace(";", ",");
                } if (SZ_CLAIM_NUMBER.Contains("&"))
                {
                    SZ_CLAIM_NUMBER = SZ_CLAIM_NUMBER.Replace("&", "amp");
                } if (SZ_POLICY_NUMBER.Contains("<"))
                {
                    SZ_POLICY_NUMBER = SZ_POLICY_NUMBER.Replace("<", "LS");
                } if (SZ_POLICY_NUMBER.Contains(">"))
                {
                    SZ_POLICY_NUMBER = SZ_POLICY_NUMBER.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_CLAIM_NUMBER>" + SZ_CLAIM_NUMBER + "</SZ_CLAIM_NUMBER>");
                sbCSV.Append(SZ_CLAIM_NUMBER + ";");

                string SZ_INSURANCE_NAME = ds.Tables[0].Rows[i]["SZ_INSURANCE_NAME"].ToString();
                if (SZ_INSURANCE_NAME.Contains(";"))
                {
                    SZ_INSURANCE_NAME = SZ_INSURANCE_NAME.Replace(";", ",");
                } if (SZ_INSURANCE_NAME.Contains("&"))
                {
                    SZ_INSURANCE_NAME = SZ_INSURANCE_NAME.Replace("&", "amp");
                } if (SZ_INSURANCE_NAME.Contains("<"))
                {
                    SZ_INSURANCE_NAME = SZ_INSURANCE_NAME.Replace("<", "LS");
                } if (SZ_INSURANCE_NAME.Contains(">"))
                {
                    SZ_INSURANCE_NAME = SZ_INSURANCE_NAME.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_INSURANCE_NAME>" + SZ_INSURANCE_NAME + "</SZ_INSURANCE_NAME>");
                sbCSV.Append(SZ_INSURANCE_NAME + ";");

                string SZ_INSURANCE_ADDRESS = ds.Tables[0].Rows[i]["SZ_INSURANCE_ADDRESS"].ToString();
                if (SZ_INSURANCE_ADDRESS.Contains(";"))
                {
                    SZ_INSURANCE_ADDRESS = SZ_INSURANCE_ADDRESS.Replace(";", ",");
                } if (SZ_INSURANCE_ADDRESS.Contains("&"))
                {
                    SZ_INSURANCE_ADDRESS = SZ_INSURANCE_ADDRESS.Replace("&", "amp");
                } if (SZ_INSURANCE_ADDRESS.Contains("<"))
                {
                    SZ_INSURANCE_ADDRESS = SZ_INSURANCE_ADDRESS.Replace("<", "LS");
                } if (SZ_INSURANCE_ADDRESS.Contains(">"))
                {
                    SZ_INSURANCE_ADDRESS = SZ_INSURANCE_ADDRESS.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_INSURANCE_ADDRESS>" + SZ_INSURANCE_ADDRESS + "</SZ_INSURANCE_ADDRESS>");
                sbCSV.Append(SZ_INSURANCE_ADDRESS + ";");

                string SZ_INSURANCE_CITY = ds.Tables[0].Rows[i]["SZ_INSURANCE_CITY"].ToString();
                if (SZ_INSURANCE_CITY.Contains(";"))
                {
                    SZ_INSURANCE_CITY = SZ_INSURANCE_CITY.Replace(";", ",");
                } if (SZ_INSURANCE_CITY.Contains("&"))
                {
                    SZ_INSURANCE_CITY = SZ_INSURANCE_CITY.Replace("&", "amp");
                } if (SZ_INSURANCE_CITY.Contains("<"))
                {
                    SZ_INSURANCE_CITY = SZ_INSURANCE_CITY.Replace("<", "LS");
                } if (SZ_INSURANCE_CITY.Contains(">"))
                {
                    SZ_INSURANCE_CITY = SZ_INSURANCE_CITY.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_INSURANCE_CITY>" + SZ_INSURANCE_CITY + "</SZ_INSURANCE_CITY>");
                sbCSV.Append(SZ_INSURANCE_CITY + ";");

                string SZ_STATE = ds.Tables[0].Rows[i]["SZ_STATE"].ToString();
                if (SZ_STATE.Contains(";"))
                {
                    SZ_STATE = SZ_STATE.Replace(";", ",");
                } if (SZ_STATE.Contains("&"))
                {
                    SZ_STATE = SZ_STATE.Replace("&", "amp");
                } if (SZ_STATE.Contains("<"))
                {
                    SZ_STATE = SZ_STATE.Replace("<", "LS");
                } if (SZ_STATE.Contains(">"))
                {
                    SZ_STATE = SZ_STATE.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_STATE>" + SZ_STATE + "</SZ_STATE>");
                sbCSV.Append(SZ_STATE + ";");


                string SZ_INSURANCE_ZIP = ds.Tables[0].Rows[i]["SZ_INSURANCE_ZIP"].ToString();
                if (SZ_INSURANCE_ZIP.Contains(";"))
                {
                    SZ_INSURANCE_ZIP = SZ_INSURANCE_ZIP.Replace(";", ",");
                } if (SZ_INSURANCE_ZIP.Contains("&"))
                {
                    SZ_INSURANCE_ZIP = SZ_INSURANCE_ZIP.Replace("&", "amp");
                } if (SZ_INSURANCE_ZIP.Contains("<"))
                {
                    SZ_INSURANCE_ZIP = SZ_INSURANCE_ZIP.Replace("<", "LS");
                } if (SZ_INSURANCE_ZIP.Contains(">"))
                {
                    SZ_INSURANCE_ZIP = SZ_INSURANCE_ZIP.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_INSURANCE_ZIP>" + SZ_INSURANCE_ZIP + "</SZ_INSURANCE_ZIP>");
                sbCSV.Append(SZ_INSURANCE_ZIP + ";");

                string SZ_INSURANCE_EMAIL = ds.Tables[0].Rows[i]["SZ_INSURANCE_EMAIL"].ToString();
                if (SZ_INSURANCE_EMAIL.Contains(";"))
                {
                    SZ_INSURANCE_EMAIL = SZ_INSURANCE_EMAIL.Replace(";", ",");
                } if (SZ_INSURANCE_EMAIL.Contains("&"))
                {
                    SZ_INSURANCE_EMAIL = SZ_INSURANCE_EMAIL.Replace("&", "amp");
                } if (SZ_INSURANCE_EMAIL.Contains("<"))
                {
                    SZ_INSURANCE_EMAIL = SZ_INSURANCE_EMAIL.Replace("<", "LS");
                } if (SZ_INSURANCE_EMAIL.Contains(">"))
                {
                    SZ_INSURANCE_EMAIL = SZ_INSURANCE_EMAIL.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_INSURANCE_EMAIL>" + SZ_INSURANCE_EMAIL + "</SZ_INSURANCE_EMAIL>");
                sbCSV.Append(SZ_INSURANCE_EMAIL + ";");

                string SZ_STATUS_NAME = ds.Tables[0].Rows[i]["SZ_STATUS_NAME"].ToString();
                if (SZ_STATUS_NAME.Contains(";"))
                {
                    SZ_STATUS_NAME = SZ_STATUS_NAME.Replace(";", ",");
                } if (SZ_STATUS_NAME.Contains("&"))
                {
                    SZ_STATUS_NAME = SZ_STATUS_NAME.Replace("&", "amp");
                } if (SZ_STATUS_NAME.Contains("<"))
                {
                    SZ_STATUS_NAME = SZ_STATUS_NAME.Replace("<", "LS");
                } if (SZ_STATUS_NAME.Contains(">"))
                {
                    SZ_STATUS_NAME = SZ_STATUS_NAME.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_STATUS_NAME>" + SZ_STATUS_NAME + "</SZ_STATUS_NAME>");
                sbCSV.Append(SZ_STATUS_NAME + ";");

                string SZ_ATTORNEY_NAME = ds.Tables[0].Rows[i]["SZ_ATTORNEY_NAME"].ToString();
                if (SZ_ATTORNEY_NAME.Contains(";"))
                {
                    SZ_ATTORNEY_NAME = SZ_ATTORNEY_NAME.Replace(";", ",");
                } if (SZ_ATTORNEY_NAME.Contains("&"))
                {
                    SZ_ATTORNEY_NAME = SZ_ATTORNEY_NAME.Replace("&", "amp");
                } if (SZ_ATTORNEY_NAME.Contains("<"))
                {
                    SZ_ATTORNEY_NAME = SZ_ATTORNEY_NAME.Replace("<", "LS");
                } if (SZ_ATTORNEY_NAME.Contains(">"))
                {
                    SZ_ATTORNEY_NAME = SZ_ATTORNEY_NAME.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_ATTORNEY_NAME>" + SZ_ATTORNEY_NAME + "</SZ_ATTORNEY_NAME>");
                sbCSV.Append(SZ_ATTORNEY_NAME + ";");


                string SZ_ATTORNEY_ADDRESS = ds.Tables[0].Rows[i]["SZ_ATTORNEY_ADDRESS"].ToString();

                if (SZ_ATTORNEY_ADDRESS.Contains(";"))
                {
                    SZ_ATTORNEY_ADDRESS = SZ_ATTORNEY_ADDRESS.Replace(";", ",");
                } if (SZ_ATTORNEY_ADDRESS.Contains("&"))
                {
                    SZ_ATTORNEY_ADDRESS = SZ_ATTORNEY_ADDRESS.Replace("&", "amp");
                } if (SZ_ATTORNEY_ADDRESS.Contains("<"))
                {
                    SZ_ATTORNEY_ADDRESS = SZ_ATTORNEY_ADDRESS.Replace("<", "LS");
                } if (SZ_ATTORNEY_ADDRESS.Contains(">"))
                {
                    SZ_ATTORNEY_ADDRESS = SZ_ATTORNEY_ADDRESS.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_ATTORNEY_ADDRESS>" + SZ_ATTORNEY_ADDRESS + "</SZ_ATTORNEY_ADDRESS>");
                sbCSV.Append(SZ_ATTORNEY_ADDRESS + ";");



                string SZ_ATTORNEY_CITY = ds.Tables[0].Rows[i]["SZ_ATTORNEY_CITY"].ToString();

                if (SZ_ATTORNEY_CITY.Contains(";"))
                {
                    SZ_ATTORNEY_CITY = SZ_ATTORNEY_CITY.Replace(";", ",");
                } if (SZ_ATTORNEY_CITY.Contains("&"))
                {
                    SZ_ATTORNEY_CITY = SZ_ATTORNEY_CITY.Replace("&", "amp");
                } if (SZ_ATTORNEY_CITY.Contains("<"))
                {
                    SZ_ATTORNEY_CITY = SZ_ATTORNEY_CITY.Replace("<", "LS");
                } if (SZ_ATTORNEY_CITY.Contains(">"))
                {
                    SZ_ATTORNEY_CITY = SZ_ATTORNEY_CITY.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_ATTORNEY_CITY>" + SZ_ATTORNEY_CITY + "</SZ_ATTORNEY_CITY>");
                sbCSV.Append(SZ_ATTORNEY_CITY + ";");

                string SZ_ATTORNEY_STATE = ds.Tables[0].Rows[i]["SZ_ATTORNEY_STATE"].ToString();

                if (SZ_ATTORNEY_STATE.Contains(";"))
                {
                    SZ_ATTORNEY_STATE = SZ_ATTORNEY_STATE.Replace(";", ",");
                } if (SZ_ATTORNEY_STATE.Contains("&"))
                {
                    SZ_ATTORNEY_STATE = SZ_ATTORNEY_STATE.Replace("&", "amp");
                } if (SZ_ATTORNEY_STATE.Contains("<"))
                {
                    SZ_ATTORNEY_STATE = SZ_ATTORNEY_STATE.Replace("<", "LS");
                } if (SZ_ATTORNEY_STATE.Contains(">"))
                {
                    SZ_ATTORNEY_STATE = SZ_ATTORNEY_STATE.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_ATTORNEY_STATE>" + SZ_ATTORNEY_STATE + "</SZ_ATTORNEY_STATE>");
                sbCSV.Append(SZ_ATTORNEY_STATE + ";");

                string SZ_ATTORNEY_ZIP = ds.Tables[0].Rows[i]["SZ_ATTORNEY_ZIP"].ToString();

                if (SZ_ATTORNEY_ZIP.Contains(";"))
                {
                    SZ_ATTORNEY_ZIP = SZ_ATTORNEY_ZIP.Replace(";", ",");
                } if (SZ_ATTORNEY_ZIP.Contains("&"))
                {
                    SZ_ATTORNEY_ZIP = SZ_ATTORNEY_ZIP.Replace("&", "amp");
                } if (SZ_ATTORNEY_ZIP.Contains("<"))
                {
                    SZ_ATTORNEY_ZIP = SZ_ATTORNEY_ZIP.Replace("<", "LS");
                } if (SZ_ATTORNEY_ZIP.Contains(">"))
                {
                    SZ_ATTORNEY_ZIP = SZ_ATTORNEY_ZIP.Replace(">", "GT");
                }

                sbXML.AppendLine("<SZ_ATTORNEY_ZIP>" + SZ_ATTORNEY_ZIP + "</SZ_ATTORNEY_ZIP>");
                sbCSV.Append(SZ_ATTORNEY_ZIP + ";");

                string SZ_ATTORNEY_FAX = ds.Tables[0].Rows[i]["SZ_ATTORNEY_FAX"].ToString();

                if (SZ_ATTORNEY_FAX.Contains(";"))
                {
                    SZ_ATTORNEY_FAX = SZ_ATTORNEY_FAX.Replace(";", ",");
                } if (SZ_ATTORNEY_FAX.Contains("&"))
                {
                    SZ_ATTORNEY_FAX = SZ_ATTORNEY_FAX.Replace("&", "amp");
                } if (SZ_ATTORNEY_FAX.Contains("<"))
                {
                    SZ_ATTORNEY_FAX = SZ_ATTORNEY_FAX.Replace("<", "LS");
                } if (SZ_ATTORNEY_FAX.Contains(">"))
                {
                    SZ_ATTORNEY_FAX = SZ_ATTORNEY_FAX.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_ATTORNEY_FAX>" + SZ_ATTORNEY_FAX + "</SZ_ATTORNEY_FAX>");
                sbCSV.Append(SZ_ATTORNEY_FAX + ";");

                string SZ_PROVIDER = GetProvider(ds.Tables[0].Rows[i]["SZ_BILL_NUMBER"].ToString());
                sbXML.AppendLine("<SZ_PROVIDER>" + SZ_PROVIDER + "</SZ_PROVIDER>");
                sbCSV.Append(SZ_PROVIDER + ";");

                string SZ_BILL_NUMBER = ds.Tables[0].Rows[i]["SZ_BILL_NUMBER"].ToString();

                if (SZ_BILL_NUMBER.Contains(";"))
                {
                    SZ_BILL_NUMBER = SZ_BILL_NUMBER.Replace(";", ",");
                } if (SZ_BILL_NUMBER.Contains("&"))
                {
                    SZ_BILL_NUMBER = SZ_BILL_NUMBER.Replace("&", "amp");
                } if (SZ_BILL_NUMBER.Contains("<"))
                {
                    SZ_BILL_NUMBER = SZ_BILL_NUMBER.Replace("<", "LS");
                } if (SZ_BILL_NUMBER.Contains(">"))
                {
                    SZ_BILL_NUMBER = SZ_BILL_NUMBER.Replace(">", "GT");
                }
                sbXML.AppendLine("<SZ_BILL_NUMBER>" + SZ_BILL_NUMBER + "</SZ_BILL_NUMBER>");
                sbCSV.Append(SZ_BILL_NUMBER + ";");

                string DT_START_VISIT_DATE = ds.Tables[0].Rows[i]["DT_START_VISIT_DATE"].ToString();

                if (DT_START_VISIT_DATE.Contains(";"))
                {
                    DT_START_VISIT_DATE = DT_START_VISIT_DATE.Replace(";", ",");
                } if (DT_START_VISIT_DATE.Contains("&"))
                {
                    DT_START_VISIT_DATE = DT_START_VISIT_DATE.Replace("&", "amp");
                } if (DT_START_VISIT_DATE.Contains("<"))
                {
                    DT_START_VISIT_DATE = DT_START_VISIT_DATE.Replace("<", "LS");
                } if (DT_START_VISIT_DATE.Contains(">"))
                {
                    DT_START_VISIT_DATE = DT_START_VISIT_DATE.Replace(">", "GT");
                }
                sbXML.AppendLine("<DT_START_VISIT_DATE>" + DT_START_VISIT_DATE + "</DT_START_VISIT_DATE>");
                sbCSV.Append(DT_START_VISIT_DATE + ";");

                string DT_END_VISIT_DATE = ds.Tables[0].Rows[i]["DT_END_VISIT_DATE"].ToString();

                if (DT_END_VISIT_DATE.Contains(";"))
                {
                    DT_END_VISIT_DATE = DT_END_VISIT_DATE.Replace(";", ",");
                } if (DT_END_VISIT_DATE.Contains("&"))
                {
                    DT_END_VISIT_DATE = DT_END_VISIT_DATE.Replace("&", "amp");
                } if (DT_END_VISIT_DATE.Contains("<"))
                {
                    DT_END_VISIT_DATE = DT_END_VISIT_DATE.Replace("<", "LS");
                } if (DT_END_VISIT_DATE.Contains(">"))
                {
                    DT_END_VISIT_DATE = DT_END_VISIT_DATE.Replace(">", "GT");
                }
                sbXML.AppendLine("<DT_END_VISIT_DATE>" + DT_END_VISIT_DATE + "</DT_END_VISIT_DATE>");
                sbCSV.Append(DT_END_VISIT_DATE + ";");


                string FLT_BILL_AMOUNT = ds.Tables[0].Rows[i]["FLT_BILL_AMOUNT"].ToString();

                if (FLT_BILL_AMOUNT.Contains(";"))
                {
                    FLT_BILL_AMOUNT = FLT_BILL_AMOUNT.Replace(";", ",");
                } if (FLT_BILL_AMOUNT.Contains("&"))
                {
                    FLT_BILL_AMOUNT = FLT_BILL_AMOUNT.Replace("&", "amp");
                } if (FLT_BILL_AMOUNT.Contains("<"))
                {
                    FLT_BILL_AMOUNT = FLT_BILL_AMOUNT.Replace("<", "LS");
                } if (FLT_BILL_AMOUNT.Contains(">"))
                {
                    FLT_BILL_AMOUNT = FLT_BILL_AMOUNT.Replace(">", "GT");
                }
                sbXML.AppendLine("<FLT_BILL_AMOUNT>" + FLT_BILL_AMOUNT + "</FLT_BILL_AMOUNT>");
                sbCSV.Append(FLT_BILL_AMOUNT + ";");

                string FLT_PAID = ds.Tables[0].Rows[i]["FLT_PAID"].ToString();

                if (FLT_PAID.Contains(";"))
                {
                    FLT_PAID = FLT_PAID.Replace(";", ",");
                } if (FLT_PAID.Contains("&"))
                {
                    FLT_PAID = FLT_PAID.Replace("&", "amp");
                } if (FLT_PAID.Contains("<"))
                {
                    FLT_PAID = FLT_PAID.Replace("<", "LS");
                } if (FLT_PAID.Contains(">"))
                {
                    FLT_PAID = FLT_PAID.Replace(">", "GT");
                }
                sbXML.AppendLine("<FLT_PAID>" + FLT_PAID + "</FLT_PAID>");
                sbCSV.Append(FLT_PAID + ";");

                string FLT_BALANCE = ds.Tables[0].Rows[i]["FLT_BALANCE"].ToString();

                if (FLT_BALANCE.Contains(";"))
                {
                    FLT_BALANCE = FLT_BALANCE.Replace(";", ",");
                } if (FLT_BALANCE.Contains("&"))
                {
                    FLT_BALANCE = FLT_BALANCE.Replace("&", "amp");
                } if (FLT_BALANCE.Contains("<"))
                {
                    FLT_BALANCE = FLT_BALANCE.Replace("<", "LS");
                } if (FLT_BALANCE.Contains(">"))
                {
                    FLT_BALANCE = FLT_BALANCE.Replace(">", "GT");
                }
                sbXML.AppendLine("<FLT_BALANCE>" + FLT_BALANCE + "</FLT_BALANCE>");
                sbCSV.Append(FLT_BALANCE + ";");


                sbXML.AppendLine("</FILE>");
                sbCSV.AppendLine("");
            }
            sbXML.AppendLine("</FILEDETAILS>");
            Files.Add(sbXML);
            Files.Add(sbCSV);
        }
        catch (Exception ex)
        {
            //log.Debug("exCompressFile " + ex.Message.ToString());
            //log.Debug("exCompressFile " + ex.StackTrace.ToString());
            throw;
        }
        return Files;
    }

    public string GetProvider(string szbillno)
    {
        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        string strReturn = "";
        SqlDataReader dr;

        try
        {
            con.Open();
           // log.Debug("sp_get_provider_name_using_bill_no @billno'" + szbillno + "'");
            SqlCommand cmd = new SqlCommand("sp_get_provider_name_using_bill_no", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sz_bill_number", szbillno);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                strReturn = dr[0].ToString();
            }
        }
        catch (Exception exGetProvider)
        {
            //log.Debug("exgGetProvider : " + exGetProvider.Message.ToString());
           //log.Debug("exGetProvider : " + exGetProvider.StackTrace.ToString());
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return strReturn;
    }

    public DataSet GetPateintInfo(string BillNO)
    {
        //log.Debug("Get Patient Info;");

        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        DataSet ds = new DataSet();
        string szCompanyName = "";
        try
        {
            con.Open();

            //log.Debug("SP_GET_DOCUMENT_INFO @SZ_BILL_NUMBER'" + BillNO + "'");
            SqlCommand cmd = new SqlCommand("SP_GET_DOCUMENT_INFO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", BillNO);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);

        }
        catch (Exception exGetCompanyName)
        {
            //log.Debug("exGetCompanyName : " + exGetCompanyName.Message.ToString());
            //log.Debug("exGetCompanyName : " + exGetCompanyName.StackTrace.ToString());
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        //log.Debug("Return Data Set : " + ds);
        return ds;
    }

    public double ConvertBytesToMegabytes(long bytes)
    {
        return (bytes / 1024f) / 1024f;
    }

    public string getPhysicalPath()
    {
        String szParamValue = "";
        string strConn = "";
        strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();


            SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = 'DocumentUploadLocationPhysical'", sqlCon);
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["parametervalue"].ToString();
            }

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();

            }
        }
        return szParamValue;
    }

    public string getLawFirmPhysicalPath()
    {
        String szParamValue = "";
        string strConn = "";
        strConn = ConfigurationSettings.AppSettings["Connection_String"].ToString(); // ConfigurationManager.AppSettings["Connection_String"].ToString(); 
        SqlConnection sqlCon;
        sqlCon = new SqlConnection(strConn);
        try
        {
            sqlCon.Open();


            SqlCommand cmdQuery = new SqlCommand("SELECT  LawFirmDocDownloadPath  FROM tblBasePath where BasePathId=( select ParameterValue from tblapplicationsettings where parametername = 'BasePathId')", sqlCon);
            SqlDataReader dr;
            dr = cmdQuery.ExecuteReader();

            while (dr.Read())
            {
                szParamValue = dr["LawFirmDocDownloadPath"].ToString();
            }

        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        finally
        {
            if (sqlCon.State == ConnectionState.Open)
            {
                sqlCon.Close();

            }
        }
        return szParamValue;
    }
     
    
    public DataSet GetDocumentsPath(string szBillNumber, string szCaseId, string szCompanyId, string szSpecialty)
    {
        

        string constring = ConfigurationSettings.AppSettings["Connection_String"].ToString();
        SqlConnection con = new SqlConnection(constring);
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            //("GET_LITIGATION_DOCUMENTS @SZ_COMPANY_ID='" + szCompanyId + "' @SZ_CASE_ID='" + szCaseId + "' @SZ_BILL_NUMBER='" + szBillNumber + "' @SZ_PROCEDURE_GROUP_ID='" + szSpecialty + "'");
            //SqlCommand cmd = new SqlCommand("GET_LITIGATION_DOCUMENTS_NEW", con);
            SqlCommand cmd = new SqlCommand("GET_LITIGATION_DOCUMENTS", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@SZ_COMPANY_ID", szCompanyId);
            cmd.Parameters.AddWithValue("@SZ_CASE_ID", szCaseId);
            cmd.Parameters.AddWithValue("@SZ_BILL_NUMBER", szBillNumber);
            cmd.Parameters.AddWithValue("@SZ_PROCEDURE_GROUP_ID", szSpecialty);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            
        }
        catch (Exception exGetDocumentsPath)
        {
            
        }
        finally
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        return ds;
    }

    private class Calendar_DAO
    {
        private string szInitDisplayMonth = null;
        private string szControlIDPrefix = null;
        private string szInitDisplayYear = null;

        public string InitialDisplayYear
        {
            get
            {
                return szInitDisplayYear;
            }
            set
            {
                szInitDisplayYear = value;
            }
        }

        public string InitialDisplayMonth
        {
            get
            {
                return szInitDisplayMonth;
            }
            set
            {
                szInitDisplayMonth = value;
            }
        }

        public string ControlIDPrefix
        {
            get
            {
                return szControlIDPrefix;
            }
            set
            {
                szControlIDPrefix = value;
            }
        }
    }

    public void BindControl()
    {
        Bill_Sys_PatientDeskList obj = new Bill_Sys_PatientDeskList();
        DataSet Ds = obj.GetPatienDeskList(txtCaseID.Text, txtCompanyId.Text);
        DtlPatientDetails.DataSource = Ds;
        DtlPatientDetails.DataBind();
    }

    public void BindPatientInfo()
    {

        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        DataSet DsPatientInfo = _bill_Sys_PatientBO.GetPatientInfo(txtPatientId.Text, txtCompanyId.Text);
        DtlView.DataSource = DsPatientInfo;
        DtlView.DataBind();
    }

    public void BindControlPestDesk()
    {
        Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
        DataSet Ds = _bill_Sys_PatientBO.GetPatienDeskList("GETPATIENTLIST", txtCaseID.Text, txtCompanyId.Text);
        DtlPatientDesk.DataSource = Ds;
        DtlPatientDesk.DataBind();
    }

    private void LoadTabInformation()
    {
        try
        {
            Bill_Sys_PatientBO _bill_Sys_PatientBO = new Bill_Sys_PatientBO();
            Patient_TVBO _patient_tvbo = new Patient_TVBO();
            DataTable dt = new DataTable();
            DataTable dtRoomList = new DataTable();

            if (Session["SpecialityList"] == null)
            {
                // Get Speciality List
                dt = _bill_Sys_PatientBO.Get_SpecialityList(txtCompanyId.Text);
                // Get Room List
                dtRoomList = _bill_Sys_PatientBO.Get_PatientDeskRoomList(txtCompanyId.Text);
                // Merage two dataset
                dt.Merge(dtRoomList);

                Session["SpecialityList"] = dt;
            }
            else
            {
                dt = (DataTable)Session["SpecialityList"];
            }


            int tabCount = 0;
            string strDate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
            dtAllSpecialityEvents = new DataTable();
            dtAllRoomEvents = new DataTable();
            if (((Bill_Sys_BillingCompanyObject)Session["BILLING_COMPANY_OBJECT"]).BT_REFERRING_FACILITY == false)
            {
                dtAllSpecialityEvents = _patient_tvbo.Get_Tab_TestInformation_TEMP_ATT(txtCaseID.Text, txtCompanyId.Text);

                // Get Room's Events

                dtAllRoomEvents = _patient_tvbo.Get_Outschedule_Tab_Information_ATT(txtCaseID.Text, txtCompanyId.Text);

                dtAllSpecialityEvents.Merge(dtAllRoomEvents);
            }
            else
            {

                // Get Room's Events
                dtAllRoomEvents = _patient_tvbo.Get_Outschedule_Tab_Information_ATT(txtCaseID.Text, txtCompanyId.Text);

                dtAllSpecialityEvents.Merge(dtAllRoomEvents);
            }

            DataTable dtTreatment = dtAllSpecialityEvents.Clone();
            DataRow[] results;
            DropDownList ddl;
            DropDownList ddl1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tabCount = tabCount + 1;
                    switch (tabCount)
                    {
                        case 1:

                            tabVistInformation.TabPages.FindByName("tabpnlOne").Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlOne").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdOne.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdOne"] = dtTreatment;
                            grdOne.DataBind();

                            //setColumnAccordingScheduleType( grdOne);
                            for (int i = 0; i < grdOne.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdOne.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdOne.Columns[1].Visible = false;
                                        grdOne.DataBind();
                                    }
                                    else
                                    {
                                        grdOne.Columns[7].Visible = false;
                                        grdOne.DataBind();
                                    }
                                }
                                // string s = grdOne.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalOne, dtTreatment);
                            break;
                        case 2:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwo").Visible = true;
                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwo").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwo.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwo"] = dtTreatment;
                            grdTwo.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwo);

                            //for (int i = 0; i < grdTwo.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwo.Items[i].Cells[5].FindControl("ddlStatus");
                            //   // ddl1.SelectedValue = grdTwo.Items[i].Cells[17].Text;




                            //}

                            for (int i = 0; i < grdTwo.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwo.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwo.Columns[1].Visible = false;
                                        grdTwo.DataBind();
                                    }
                                    else
                                    {
                                        grdTwo.Columns[7].Visible = false;
                                        grdTwo.DataBind();
                                    }
                                }
                                // string s = grdTwo.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwo, dtTreatment);
                            break;
                        case 3:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThree").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThree").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThree.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThree"] = dtTreatment;
                            grdThree.DataBind();

                            //setColumnAccordingScheduleType(ref grdThree);

                            //for (int i = 0; i < grdThree.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThree.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdThree.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdThree.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThree.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThree.Columns[1].Visible = false;
                                        grdThree.DataBind();
                                    }
                                    else
                                    {
                                        grdThree.Columns[7].Visible = false;
                                        grdThree.DataBind();
                                    }
                                }
                                // string s = grdThree.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThree, dtTreatment);
                            break;
                        case 4:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFour").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlFour").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFour.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdFour"] = dtTreatment;
                            grdFour.DataBind();

                            //setColumnAccordingScheduleType(ref grdFour);

                            //for (int i = 0; i < grdFour.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFour.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFour.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdFour.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFour.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdFour.Columns[1].Visible = false;
                                        grdFour.DataBind();
                                    }
                                    else
                                    {
                                        grdFour.Columns[7].Visible = false;
                                        grdFour.DataBind();
                                    }
                                }
                                // string s = grdFour.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFour, dtTreatment);
                            break;
                        case 5:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFive").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            //lblHeadFive.Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            tabVistInformation.TabPages.FindByName("tabpnlFive").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFive.DataSource = dtTreatment;
                            //viewState for Export to xlxs
                            ViewState["VSgrdFive"] = dtTreatment;
                            grdFive.DataBind();

                            //setColumnAccordingScheduleType(ref grdFive);

                            //for (int i = 0; i < grdFive.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFive.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFive.Items[i].Cells[17].Text;


                            //}

                            for (int i = 0; i < grdFive.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFive.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdFive.Columns[1].Visible = false;
                                        grdFive.DataBind();
                                    }
                                    else
                                    {
                                        grdFive.Columns[7].Visible = false;
                                        grdFive.DataBind();
                                    }
                                }
                                // string s = grdFive.GetRowValues(i, "I_STATUS").ToString();
                                GridViewDataColumn d = new GridViewDataColumn();
                                // ASPxComboBox cmb = (ASPxComboBox)grdFive.FindDetailRowTemplateControl(i, "ddlStatus");
                                // object keyValue = grdFive.GetRowValues(i, new string[] { grdFive. });
                                //cmb.Value = grdOne.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFive, dtTreatment);
                            break;
                        case 6:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSix").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSix").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSix.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdSix"] = dtTreatment;
                            grdSix.DataBind();

                            //setColumnAccordingScheduleType(ref grdSix);

                            //for (int i = 0; i < grdSix.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSix.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSix.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdSix.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSix.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdSix.Columns[1].Visible = false;
                                        grdSix.DataBind();
                                    }
                                    else
                                    {
                                        grdSix.Columns[7].Visible = false;
                                        grdSix.DataBind();
                                    }
                                }
                                // string s = grdSix.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSix, dtTreatment);
                            break;
                        case 7:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSeven").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSeven").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSeven.DataSource = dtTreatment;
                            //viewState for Export to xlxs
                            ViewState["VSgrdSeven"] = dtTreatment;
                            grdSeven.DataBind();

                            //setColumnAccordingScheduleType(ref grdSeven);

                            //for (int i = 0; i < grdSeven.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSeven.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSeven.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdSeven.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSeven.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdSeven.Columns[1].Visible = false;
                                        grdSeven.DataBind();
                                    }
                                    else
                                    {
                                        grdSeven.Columns[7].Visible = false;
                                        grdSeven.DataBind();
                                    }
                                }
                                // string s = grdSeven.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSeven, dtTreatment);
                            break;
                        case 8:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlEight").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlEight").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEight.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdEight"] = dtTreatment;
                            grdEight.DataBind();

                            //setColumnAccordingScheduleType(ref grdEight);

                            //for (int i = 0; i < grdEight.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdEight.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdEight.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdEight.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdEight.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {
                                        //grdFive.Columns[1].Visible = false;
                                        grdEight.Columns[1].Visible = false;
                                        grdEight.DataBind();
                                    }
                                    else
                                    {
                                        grdEight.Columns[7].Visible = false;
                                        grdEight.DataBind();
                                    }
                                }
                                // string s = grdEight.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEight, dtTreatment);
                            break;
                        case 9:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlNine").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlNine").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdNine.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdNine"] = dtTreatment;
                            grdNine.DataBind();

                            //setColumnAccordingScheduleType(ref grdNine);

                            //for (int i = 0; i < grdNine.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdNine.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdNine.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdNine.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdNine.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdNine.Columns[1].Visible = false;
                                        grdNine.DataBind();
                                    }
                                    else
                                    {
                                        grdNine.Columns[7].Visible = false;
                                        grdNine.DataBind();
                                    }
                                }
                                // string s = grdNine.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalNine, dtTreatment);
                            break;
                        case 10:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTen"] = dtTreatment;
                            grdTen.DataBind();

                            //setColumnAccordingScheduleType(ref grdTen);

                            //for (int i = 0; i < grdTen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdTen.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTen.Columns[1].Visible = false;
                                        grdTen.DataBind();
                                    }
                                    else
                                    {
                                        grdTen.Columns[7].Visible = false;
                                        grdTen.DataBind();
                                    }
                                }
                                // string s = grdTen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTen, dtTreatment);
                            break;
                        case 11:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlEleven").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlEleven").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEleven.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdEleven"] = dtTreatment;
                            grdEleven.DataBind();
                            //setColumnAccordingScheduleType(ref grdEleven);

                            //for (int i = 0; i < grdEleven.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdEleven.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdEleven.Items[i].Cells[17].Text;



                            //}
                            for (int i = 0; i < grdEleven.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdEleven.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdEleven.Columns[1].Visible = false;
                                        grdEleven.DataBind();
                                    }
                                    else
                                    {
                                        grdEleven.Columns[7].Visible = false;
                                        grdEleven.DataBind();
                                    }
                                }
                                // string s = grdEleven.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEleven, dtTreatment);
                            break;
                        case 12:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwelve").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwelve").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwelve.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwelve"] = dtTreatment;
                            grdTwelve.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwelve);

                            //for (int i = 0; i < grdTwelve.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwelve.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdTwelve.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwelve.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwelve.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwelve.Columns[1].Visible = false;
                                        grdTwelve.DataBind();
                                    }
                                    else
                                    {
                                        grdTwelve.Columns[7].Visible = false;
                                        grdTwelve.DataBind();
                                    }
                                }
                                // string s = grdEleven.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwelve, dtTreatment);
                            break;
                        case 13:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirteen"] = dtTreatment;
                            grdThirteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirteen);

                            //for (int i = 0; i < grdThirteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdThirteen.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdThirteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirteen.Columns[1].Visible = false;
                                        grdThirteen.DataBind();
                                    }
                                    else
                                    {
                                        grdThirteen.Columns[7].Visible = false;
                                        grdThirteen.DataBind();
                                    }
                                }
                                // string s = grdThirteen.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirteen, dtTreatment);
                            break;
                        case 14:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFourteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlFourteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFourteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdFourteen"] = dtTreatment;
                            grdFourteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdFourteen);

                            //for (int i = 0; i < grdFourteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFourteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFourteen.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdFourteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFourteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdFourteen.Columns[1].Visible = false;
                                        grdFourteen.DataBind();
                                    }
                                    else
                                    {
                                        grdFourteen.Columns[7].Visible = false;
                                        grdFourteen.DataBind();
                                    }
                                }
                                // string s = grdThirteen.GetRowValues(i, "I_STATUS").ToString();
                            }


                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFourteen, dtTreatment);
                            break;
                        case 15:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlFifteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlFifteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdFifteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdFifteen"] = dtTreatment;
                            grdFifteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdFifteen);

                            //for (int i = 0; i < grdFifteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdFifteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdFifteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdFifteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdFifteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdFifteen.Columns[1].Visible = false;
                                        grdFifteen.DataBind();
                                    }
                                    else
                                    {
                                        grdFifteen.Columns[7].Visible = false;
                                        grdFifteen.DataBind();
                                    }
                                }
                                // string s = grdFifteen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalFifteen, dtTreatment);
                            break;
                        case 16:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSixteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSixteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSixteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdSixteen"] = dtTreatment;
                            grdSixteen.DataBind();

                            // setColumnAccordingScheduleType(ref grdSixteen);

                            //for (int i = 0; i < grdSixteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSixteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSixteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdSixteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSixteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdSixteen.Columns[1].Visible = false;
                                        grdSixteen.DataBind();
                                    }
                                    else
                                    {
                                        grdSixteen.Columns[7].Visible = false;
                                        grdSixteen.DataBind();
                                    }
                                }
                                // string s = grdSixteen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSixteen, dtTreatment);
                            break;
                        case 17:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlSeventeen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlSeventeen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdSeventeen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdSeventeen"] = dtTreatment;
                            grdSeventeen.DataBind();

                            //setColumnAccordingScheduleType(ref grdSeventeen);

                            //for (int i = 0; i < grdSeventeen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdSeventeen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdSeventeen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdSeventeen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdSeventeen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdSeventeen.Columns[1].Visible = false;
                                        grdSeventeen.DataBind();
                                    }
                                    else
                                    {
                                        grdSeventeen.Columns[7].Visible = false;
                                        grdSeventeen.DataBind();
                                    }
                                }
                                // string s = grdSeventeen.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalSeventeen, dtTreatment);
                            break;
                        case 18:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlEighteen").Visible = true;


                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlEighteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdEighteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdEighteen"] = dtTreatment;
                            grdEighteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdEighteen);

                            //for (int i = 0; i < grdEighteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdEighteen.Items[i].Cells[5].FindControl("ddlStatus");
                            //    ddl1.SelectedValue = grdEighteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdEighteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdEighteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdEighteen.Columns[1].Visible = false;
                                        grdEighteen.DataBind();
                                    }
                                    else
                                    {
                                        grdEighteen.Columns[7].Visible = false;
                                        grdEighteen.DataBind();
                                    }
                                }
                                // string s = grdEighteen.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalEighteen, dtTreatment);
                            break;
                        case 19:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlNineteen").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlNineteen").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdNineteen.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdNineteen"] = dtTreatment;
                            grdNineteen.DataBind();

                            //setColumnAccordingScheduleType(ref grdNineteen);

                            //for (int i = 0; i < grdNineteen.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdNineteen.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdNineteen.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdNineteen.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdNineteen.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdNineteen.Columns[1].Visible = false;
                                        grdNineteen.DataBind();
                                    }
                                    else
                                    {
                                        grdNineteen.Columns[7].Visible = false;
                                        grdNineteen.DataBind();
                                    }
                                }
                                // string s = grdNineteen.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalNineteen, dtTreatment);
                            break;
                        case 20:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwenty").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwenty").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwenty.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwenty"] = dtTreatment;
                            grdTwenty.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwenty);

                            //for (int i = 0; i < grdTwenty.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwenty.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwenty.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwenty.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwenty.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwenty.Columns[1].Visible = false;
                                        grdTwenty.DataBind();
                                    }
                                    else
                                    {
                                        grdTwenty.Columns[7].Visible = false;
                                        grdTwenty.DataBind();
                                    }
                                }
                                // string s = grdTwenty.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwenty, dtTreatment);
                            break;

                        case 21:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyOne").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyOne").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyOne.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyOne"] = dtTreatment;
                            grdTwentyOne.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyOne);

                            //for (int i = 0; i < grdTwentyOne.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyOne.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyOne.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTwentyOne.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyOne.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyOne.Columns[1].Visible = false;
                                        grdTwentyOne.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyOne.Columns[7].Visible = false;
                                        grdTwentyOne.DataBind();
                                    }
                                }
                                // string s = grdTwentyOne.GetRowValues(i, "I_STATUS").ToString();
                            }
                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyOne, dtTreatment);
                            break;
                        case 22:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyTwo").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyTwo").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyTwo.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyTwo"] = dtTreatment;
                            grdTwentyTwo.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyTwo);

                            //for (int i = 0; i < grdTwentyTwo.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyTwo.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyTwo.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyTwo.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyTwo.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyTwo.Columns[1].Visible = false;
                                        grdTwentyTwo.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyTwo.Columns[7].Visible = false;
                                        grdTwentyTwo.DataBind();
                                    }
                                }
                                // string s = grdTwentyTwo.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyTwo, dtTreatment);
                            break;
                        case 23:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyThree").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyThree").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyThree.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyThree"] = dtTreatment;
                            grdTwentyThree.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyThree);

                            //for (int i = 0; i < grdTwentyThree.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyThree.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyThree.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyThree.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyThree.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyThree.Columns[1].Visible = false;
                                        grdTwentyThree.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyThree.Columns[7].Visible = false;
                                        grdTwentyThree.DataBind();
                                    }
                                }
                                // string s = grdTwentyThree.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyThree, dtTreatment);
                            break;
                        case 24:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFour").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFour").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyFour.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyFour"] = dtTreatment;
                            grdTwentyFour.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyFour);

                            //for (int i = 0; i < grdTwentyFour.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyFour.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyFour.Items[i].Cells[17].Text;


                            //}
                            for (int i = 0; i < grdTwentyFour.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyFour.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyFour.Columns[1].Visible = false;
                                        grdTwentyFour.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyFour.Columns[7].Visible = false;
                                        grdTwentyFour.DataBind();
                                    }
                                }
                                // string s = grdTwentyFour.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyFour, dtTreatment);
                            break;
                        case 25:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFive").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyFive").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyFive.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyFive"] = dtTreatment;
                            grdTwentyFive.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyFive);

                            //for (int i = 0; i < grdTwentyFive.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyFive.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyFive.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyFive.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyFive.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyFive.Columns[1].Visible = false;
                                        grdTwentyFive.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyFive.Columns[7].Visible = false;
                                        grdTwentyFive.DataBind();
                                    }
                                }
                                // string s = grdTwentyFive.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyFive, dtTreatment);
                            break;
                        case 26:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySix").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySix").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentySix.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentySix"] = dtTreatment;
                            grdTwentySix.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentySix);

                            //for (int i = 0; i < grdTwentySix.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentySix.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentySix.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentySix.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentySix.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentySix.Columns[1].Visible = false;
                                        grdTwentySix.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentySix.Columns[7].Visible = false;
                                        grdTwentySix.DataBind();
                                    }
                                }
                                // string s = grdTwentySix.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentySix, dtTreatment);
                            break;
                        case 27:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySeven").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentySeven").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentySeven.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentySeven"] = dtTreatment;
                            grdTwentySeven.DataBind();

                            // setColumnAccordingScheduleType(ref grdTwentySeven);

                            //for (int i = 0; i < grdTwentySeven.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentySeven.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentySeven.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTwentySeven.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentySeven.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentySeven.Columns[1].Visible = false;
                                        grdTwentySeven.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentySeven.Columns[7].Visible = false;
                                        grdTwentySeven.DataBind();
                                    }
                                }
                                // string s = grdTwentySeven.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentySeven, dtTreatment);
                            break;
                        case 28:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyEight").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyEight").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyEight.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyEight"] = dtTreatment;
                            grdTwentyEight.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyEight);

                            //for (int i = 0; i < grdTwentyEight.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyEight.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyEight.Items[i].Cells[17].Text;

                            //}

                            for (int i = 0; i < grdTwentyEight.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyEight.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyEight.Columns[1].Visible = false;
                                        grdTwentyEight.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyEight.Columns[7].Visible = false;
                                        grdTwentyEight.DataBind();
                                    }
                                }
                                // string s = grdTwentyEight.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyEight, dtTreatment);
                            break;
                        case 29:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyNine").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlTwentyNine").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdTwentyNine.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdTwentyNine"] = dtTreatment;
                            grdTwentyNine.DataBind();

                            //setColumnAccordingScheduleType(ref grdTwentyNine);

                            //for (int i = 0; i < grdTwentyNine.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdTwentyNine.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdTwentyNine.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdTwentyNine.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdTwentyNine.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdTwentyNine.Columns[1].Visible = false;
                                        grdTwentyNine.DataBind();
                                    }
                                    else
                                    {
                                        grdTwentyNine.Columns[7].Visible = false;
                                        grdTwentyNine.DataBind();
                                    }
                                }
                                // string s = grdTwentyNine.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalTwentyNine, dtTreatment);
                            break;

                        case 30:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirty").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirty").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirty.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirty"] = dtTreatment;
                            grdThirty.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirty);

                            //for (int i = 0; i < grdThirty.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirty.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirty.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirty.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirty.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirty.Columns[1].Visible = false;
                                        grdThirty.DataBind();
                                    }
                                    else
                                    {
                                        grdThirty.Columns[7].Visible = false;
                                        grdThirty.DataBind();
                                    }
                                }
                                // string s = grdThirty.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirty, dtTreatment);
                            break;

                        case 31:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyOne").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyOne").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyOne.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyOne"] = dtTreatment;
                            grdThirtyOne.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirtyOne);

                            //for (int i = 0; i < grdThirtyOne.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyOne.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyOne.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyOne.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyOne.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyOne.Columns[1].Visible = false;
                                        grdThirtyOne.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyOne.Columns[7].Visible = false;
                                        grdThirtyOne.DataBind();
                                    }
                                }
                                // string s = grdThirtyOne.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();
                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyOne, dtTreatment);
                            break;

                        case 32:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyTwo").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyTwo").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyTwo.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyTwo"] = dtTreatment;
                            grdThirtyTwo.DataBind();

                            // setColumnAccordingScheduleType(ref grdThirtyTwo);

                            //for (int i = 0; i < grdThirtyTwo.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyTwo.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyTwo.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyTwo.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyTwo.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyTwo.Columns[1].Visible = false;
                                        grdThirtyTwo.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyTwo.Columns[7].Visible = false;
                                        grdThirtyTwo.DataBind();
                                    }
                                }
                                // string s = grdThirtyTwo.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyTwo, dtTreatment);
                            break;

                        case 33:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyThree").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyThree").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyThree.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyThree"] = dtTreatment;
                            grdThirtyThree.DataBind();

                            // setColumnAccordingScheduleType(ref grdThirtyThree);

                            //for (int i = 0; i < grdThirtyThree.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyThree.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyThree.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyThree.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyThree.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyThree.Columns[1].Visible = false;
                                        grdThirtyThree.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyThree.Columns[7].Visible = false;
                                        grdThirtyThree.DataBind();
                                    }
                                }
                                // string s = grdThirtyThree.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyThree, dtTreatment);
                            break;

                        case 34:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFour").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFour").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyFour.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyFour"] = dtTreatment;
                            grdThirtyFour.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirtyFour);

                            //for (int i = 0; i < grdThirtyFour.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyFour.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyFour.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyFour.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyFour.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyFour.Columns[1].Visible = false;
                                        grdThirtyFour.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyFour.Columns[7].Visible = false;
                                        grdThirtyFour.DataBind();
                                    }
                                }
                                // string s = grdThirtyFour.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyFour, dtTreatment);
                            break;

                        case 35:
                            dtTreatment = null;
                            dtTreatment = dtAllSpecialityEvents.Clone();
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFive").Visible = true;

                            results = dtAllSpecialityEvents.Select("SPECIALITY='" + row[1].ToString() + "'");
                            foreach (DataRow dr in results)
                                dtTreatment.ImportRow(dr);
                            tabVistInformation.TabPages.FindByName("tabpnlThirtyFive").Text = row[1].ToString() + " (" + dtTreatment.Rows.Count + ")";
                            grdThirtyFive.DataSource = dtTreatment;
                            // ViewState for export to xlxs
                            ViewState["VSgrdThirtyFive"] = dtTreatment;
                            grdThirtyFive.DataBind();

                            //setColumnAccordingScheduleType(ref grdThirtyFive);

                            //for (int i = 0; i < grdThirtyFive.Items.Count; i++)
                            //{

                            //    ddl1 = (DropDownList)grdThirtyFive.Items[i].Cells[5].FindControl("ddlStatus");

                            //    ddl1.SelectedValue = grdThirtyFive.Items[i].Cells[17].Text;

                            //}
                            for (int i = 0; i < grdThirtyFive.VisibleRowCount; i++)
                            {
                                if (i == 0)
                                {
                                    string szType = grdThirtyFive.GetRowValues(i, "SCHEDULE_TYPE").ToString();
                                    if (szType == "IN")
                                    {

                                        grdThirtyFive.Columns[1].Visible = false;
                                        grdThirtyFive.DataBind();
                                    }
                                    else
                                    {
                                        grdThirtyFive.Columns[7].Visible = false;
                                        grdThirtyFive.DataBind();
                                    }
                                }
                                // string s = grdThirtyFive.GetRowValues(i, "I_STATUS").ToString();
                            }

                            objCalendar = new Calendar_DAO();
                            objCalendar.ControlIDPrefix = "Calendar_" + Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayMonth = Convert.ToDateTime(strDate).ToString("MMM").ToUpper().ToString();
                            objCalendar.InitialDisplayYear = Convert.ToDateTime(strDate).Year.ToString();

                            // commented by shailesh 29april2010
                            // Commented the showcalendar function so that it is not visible. 
                            //showCalendar(objCalendar, updpnlCalThirtyFive, dtTreatment);
                            break;
                    }
                }


            }

        }

        catch (Exception ex)
        {
            string strError = ex.Message.ToString();
            strError = strError.Replace("\n", " ");
            Response.Redirect("Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError);
        }
        //}

        //setColumnAccordingScheduleType(ref grdOne);
    }

    public DataSet GetPatientListForAttorny(string sz_att_user_id, string sz_case_no, string sz_patient_name, string sz_companyid)
    {
        DataSet dsearch;

        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "SP_GET_PATIENT_INFO_FOR_ATTORNY";
            sqlCmd.Parameters.AddWithValue("@SZ_USER_ID", sz_att_user_id);
            sqlCmd.Parameters.AddWithValue("@SZ_CASE_NO", sz_case_no);
            sqlCmd.Parameters.AddWithValue("@SZ_PATENT_NAME", sz_patient_name);
            sqlCmd.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_companyid);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlda = new SqlDataAdapter(sqlCmd);
            dsearch = new DataSet();
            sqlda.Fill(dsearch);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            sqlCon.Close();
        }

        return dsearch;
    }


    public void Saveurlintegrator(string sz_ip_address, string sz_user_id, string dt_downloaded, string sz_company_id, string sz_download_filename, string sz_bill_no  ,string sz_case_no ,string sz_case_id, string sz_lawfirm_id)
    {
        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "save_integration_url_downloads";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlCmd.Parameters.AddWithValue("@sz_ip_address", sz_ip_address);
            sqlCmd.Parameters.AddWithValue("@sz_user_id", sz_user_id);
            sqlCmd.Parameters.AddWithValue("@dt_downloaded", dt_downloaded);
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_company_id);
            sqlCmd.Parameters.AddWithValue("@sz_download_filename", sz_download_filename);
            sqlCmd.Parameters.AddWithValue("@sz_bill_no", sz_bill_no);
            sqlCmd.Parameters.AddWithValue("@sz_case_no", sz_case_no);
            sqlCmd.Parameters.AddWithValue("@sz_case_id", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@sz_lawfirm_id", sz_lawfirm_id);
            sqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally { sqlCon.Close(); }
    }

    public DataSet GetPatientListForAttornyintegrator(string sz_companyid, string sz_case_id, string sz_bill_no, string sz_lawfirm_id)
    {
        DataSet dsearch;

        strConn = ConfigurationManager.AppSettings["Connection_String"].ToString();
        try
        {
            sqlCon = new SqlConnection(strConn);
            sqlCon.Open();
            sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_get_bills_for_url_integrator";
            sqlCmd.Parameters.AddWithValue("@sz_company_id", sz_companyid);
            sqlCmd.Parameters.AddWithValue("@sz_case_id", sz_case_id);
            sqlCmd.Parameters.AddWithValue("@sz_bill_number", sz_bill_no);
            sqlCmd.Parameters.AddWithValue("@sz_lawfirm_id", sz_lawfirm_id);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = sqlCon;
            sqlda = new SqlDataAdapter(sqlCmd);
            dsearch = new DataSet();
            sqlda.Fill(dsearch);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            sqlCon.Close();
        }

        return dsearch;
    }

    protected void btnXlsExportOne_Click(object sender, EventArgs e)
    {
        DataTable dtgrdOne = new DataTable();
        dtgrdOne = (DataTable)ViewState["VSgrdOne"];

        grdOne.DataSource = dtgrdOne;
        grdOne.DataBind();

        grdExportOne.WriteXlsToResponse();
    }

    protected void btnXlsExportTwo_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwo = new DataTable();
        dtgrdTwo = (DataTable)ViewState["VSgrdTwo"];

        grdTwo.DataSource = dtgrdTwo;
        grdTwo.DataBind();

        grdExportTwo.WriteXlsToResponse();
    }

    protected void btnXlsExportThree_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThree = new DataTable();
        dtgrdThree = (DataTable)ViewState["VSgrdThree"];

        grdThree.DataSource = dtgrdThree;
        grdThree.DataBind();

        grdExportThree.WriteXlsToResponse();
    }

    protected void btnXlsExportFour_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFour = new DataTable();
        dtgrdFour = (DataTable)ViewState["VSgrdFour"];

        grdFour.DataSource = dtgrdFour;
        grdFour.DataBind();

        grdExportFour.WriteXlsToResponse();
    }

    protected void btnXlsExportFive_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFive = new DataTable();
        dtgrdFive = (DataTable)ViewState["VSgrdFive"];

        grdFive.DataSource = dtgrdFive;
        grdFive.DataBind();

        grdExportFive.WriteXlsToResponse();
    }

    protected void btnXlsExportSix_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSix = new DataTable();
        dtgrdSix = (DataTable)ViewState["VSgrdSix"];

        grdSix.DataSource = dtgrdSix;
        grdSix.DataBind();

        grdExportSix.WriteXlsToResponse();
    }

    protected void btnXlsExportSeven_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSeven = new DataTable();
        dtgrdSeven = (DataTable)ViewState["VSgrdSeven"];

        grdSeven.DataSource = dtgrdSeven;
        grdSeven.DataBind();

        grdExportSeven.WriteXlsToResponse();
    }

    protected void btnXlsExportEight_Click(object sender, EventArgs e)
    {
        DataTable dtgrdEight = new DataTable();
        dtgrdEight = (DataTable)ViewState["VSgrdEight"];

        grdEight.DataSource = dtgrdEight;
        grdEight.DataBind();

        grdExportEight.WriteXlsToResponse();
    }

    protected void btnXlsExportNine_Click(object sender, EventArgs e)
    {
        DataTable dtgrdNine = new DataTable();
        dtgrdNine = (DataTable)ViewState["VSgrdNine"];

        grdNine.DataSource = dtgrdNine;
        grdNine.DataBind();

        grdExportNine.WriteXlsToResponse();
    }

    protected void btnXlsExportTen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTen = new DataTable();
        dtgrdTen = (DataTable)ViewState["VSgrdTen"];

        grdTen.DataSource = dtgrdTen;
        grdTen.DataBind();

        grdExportTen.WriteXlsToResponse();
    }

    protected void btnXlsExportEleven_Click(object sender, EventArgs e)
    {
        DataTable dtgrdEleven = new DataTable();
        dtgrdEleven = (DataTable)ViewState["VSgrdEleven"];

        grdEleven.DataSource = dtgrdEleven;
        grdEleven.DataBind();

        grdExportEleven.WriteXlsToResponse();
    }

    protected void btnXlsExportTwelve_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwelve = new DataTable();
        dtgrdTwelve = (DataTable)ViewState["VSgrdTwelve"];

        grdTwelve.DataSource = dtgrdTwelve;
        grdTwelve.DataBind();

        grdExportTwelve.WriteXlsToResponse();
    }

    protected void btnXlsExportThirteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirteen = new DataTable();
        dtgrdThirteen = (DataTable)ViewState["VSgrdThirteen"];

        grdThirteen.DataSource = dtgrdThirteen;
        grdThirteen.DataBind();

        grdExportThirteen.WriteXlsToResponse();
    }

    protected void btnXlsExportFourteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFourteen = new DataTable();
        dtgrdFourteen = (DataTable)ViewState["VSgrdFourteen"];

        grdFourteen.DataSource = dtgrdFourteen;
        grdFourteen.DataBind();

        grdExportFourteen.WriteXlsToResponse();
    }

    protected void btnXlsExportFifteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdFifteen = new DataTable();
        dtgrdFifteen = (DataTable)ViewState["VSgrdFifteen"];

        grdFifteen.DataSource = dtgrdFifteen;
        grdFifteen.DataBind();

        grdExportFifteen.WriteXlsToResponse();
    }

    protected void btnXlsExportSixteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSixteen = new DataTable();
        dtgrdSixteen = (DataTable)ViewState["VSgrdSixteen"];

        grdSixteen.DataSource = dtgrdSixteen;
        grdSixteen.DataBind();

        grdExportSixteen.WriteXlsToResponse();
    }

    protected void btnXlsExportSeventeen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdSeventeen = new DataTable();
        dtgrdSeventeen = (DataTable)ViewState["VSgrdSeventeen"];

        grdSeventeen.DataSource = dtgrdSeventeen;
        grdSeventeen.DataBind();

        grdExportSeventeen.WriteXlsToResponse();
    }

    protected void btnXlsExportEighteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdEighteen = new DataTable();
        dtgrdEighteen = (DataTable)ViewState["VSgrdEighteen"];

        grdEighteen.DataSource = dtgrdEighteen;
        grdEighteen.DataBind();

        grdExportEighteen.WriteXlsToResponse();
    }

    protected void btnXlsExportNineteen_Click(object sender, EventArgs e)
    {
        DataTable dtgrdNineteen = new DataTable();
        dtgrdNineteen = (DataTable)ViewState["VSgrdNineteen"];

        grdNineteen.DataSource = dtgrdNineteen;
        grdNineteen.DataBind();

        grdExportNineteen.WriteXlsToResponse();
    }

    protected void btnXlsExportTwenty_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwenty = new DataTable();
        dtgrdTwenty = (DataTable)ViewState["VSgrdTwenty"];

        grdTwenty.DataSource = dtgrdTwenty;
        grdTwenty.DataBind();

        grdExportTwenty.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentyOne_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyOne = new DataTable();
        dtgrdTwentyOne = (DataTable)ViewState["VSgrdTwentyOne"];

        grdTwentyOne.DataSource = dtgrdTwentyOne;
        grdTwentyOne.DataBind();

        grdExportTwentyOne.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentyTwo_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyTwo = new DataTable();
        dtgrdTwentyTwo = (DataTable)ViewState["VSgrdTwentyTwo"];

        grdTwentyTwo.DataSource = dtgrdTwentyTwo;
        grdTwentyTwo.DataBind();

        grdExportTwentyTwo.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentyThree_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyThree = new DataTable();
        dtgrdTwentyThree = (DataTable)ViewState["VSgrdTwentyThree"];

        grdTwentyThree.DataSource = dtgrdTwentyThree;
        grdTwentyThree.DataBind();

        grdExportTwentyThree.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentyFour_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyFour = new DataTable();
        dtgrdTwentyFour = (DataTable)ViewState["VSgrdTwentyFour"];

        grdTwentyFour.DataSource = dtgrdTwentyFour;
        grdTwentyFour.DataBind();

        grdExportTwentyFour.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentyFive_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyFive = new DataTable();
        dtgrdTwentyFive = (DataTable)ViewState["VSgrdTwentyFive"];

        grdTwentyFive.DataSource = dtgrdTwentyFive;
        grdTwentyFive.DataBind();

        grdExportTwentyFive.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentySix_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentySix = new DataTable();
        dtgrdTwentySix = (DataTable)ViewState["VSgrdTwentySix"];

        grdTwentySix.DataSource = dtgrdTwentySix;
        grdTwentySix.DataBind();

        grdExportTwentySix.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentySeven_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentySeven = new DataTable();
        dtgrdTwentySeven = (DataTable)ViewState["VSgrdTwentySeven"];

        grdTwentySeven.DataSource = dtgrdTwentySeven;
        grdTwentySeven.DataBind();

        grdExportTwentySeven.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentyEight_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyEight = new DataTable();
        dtgrdTwentyEight = (DataTable)ViewState["VSgrdTwentyEight"];

        grdTwentyEight.DataSource = dtgrdTwentyEight;
        grdTwentyEight.DataBind();

        grdExportTwentyEight.WriteXlsToResponse();
    }

    protected void btnXlsExportTwentyNine_Click(object sender, EventArgs e)
    {
        DataTable dtgrdTwentyNine = new DataTable();
        dtgrdTwentyNine = (DataTable)ViewState["VSgrdTwentyNine"];

        grdTwentyNine.DataSource = dtgrdTwentyNine;
        grdTwentyNine.DataBind();

        grdExportTwentyNine.WriteXlsToResponse();
    }

    protected void btnXlsExportThirty_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirty = new DataTable();
        dtgrdThirty = (DataTable)ViewState["VSgrdThirty"];

        grdThirty.DataSource = dtgrdThirty;
        grdThirty.DataBind();

        grdExportThirty.WriteXlsToResponse();
    }

    protected void btnXlsExportThirtyOne_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyOne = new DataTable();
        dtgrdThirtyOne = (DataTable)ViewState["VSgrdThirtyOne"];

        grdThirtyOne.DataSource = dtgrdThirtyOne;
        grdThirtyOne.DataBind();

        grdExportThirtyOne.WriteXlsToResponse();
    }

    protected void btnXlsExportThirtyTwo_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyTwo = new DataTable();
        dtgrdThirtyTwo = (DataTable)ViewState["VSgrdThirtyTwo"];

        grdThirtyTwo.DataSource = dtgrdThirtyTwo;
        grdThirtyTwo.DataBind();

        grdExportThirtyTwo.WriteXlsToResponse();
    }

    protected void btnXlsExportThirtyThree_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyThree = new DataTable();
        dtgrdThirtyThree = (DataTable)ViewState["VSgrdThirtyThree"];

        grdThirtyThree.DataSource = dtgrdThirtyThree;
        grdThirtyThree.DataBind();

        grdExportThirtyThree.WriteXlsToResponse();
    }

    protected void btnXlsExportThirtyFour_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyFour = new DataTable();
        dtgrdThirtyFour = (DataTable)ViewState["VSgrdThirtyFour"];

        grdThirtyFour.DataSource = dtgrdThirtyFour;
        grdThirtyFour.DataBind();

        grdExportThirtyFour.WriteXlsToResponse();
    }

    protected void btnXlsExportThirtyFive_Click(object sender, EventArgs e)
    {
        DataTable dtgrdThirtyFive = new DataTable();
        dtgrdThirtyFive = (DataTable)ViewState["VSgrdThirtyFive"];

        grdThirtyFive.DataSource = dtgrdThirtyFive;
        grdThirtyFive.DataBind();

        grdExportThirtyFive.WriteXlsToResponse();
    }


}
