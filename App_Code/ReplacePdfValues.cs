using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.Sql;
using log4net;
/// <summary>
/// Summary description for ReplacePdfValues
/// </summary>
public class ReplacePdfValues
{

    private static ILog log = LogManager.GetLogger("PDFValueReplacement");
    private ArrayList objDiagnosisCodes = null, objServices = null;
    private ArrayList objDiagnosisGrid = null;
    private ArrayList objServiceGrid = null;

    private string szProcedureName = "";

    private ArrayList ParseXML(string p_szPath)
    {
        ArrayList objRArrList = new ArrayList();
        try
        {

            objDiagnosisGrid = new ArrayList();
            objServiceGrid = new ArrayList();
            XmlDocument doc1 = new XmlDocument();

            doc1.Load(p_szPath);
            XmlNodeList xmlNodeList;
            xmlNodeList = doc1.GetElementsByTagName("field");
            XmlNodeList xmlProcNameList;
            xmlProcNameList = doc1.GetElementsByTagName("document");

            foreach (XmlNode x in xmlProcNameList)
            {
                szProcedureName = x.Attributes["procedure_name"].Value;
            }

            DAO_Bill_Sys_XMLPDFFormParameters objParameters = null;
            foreach (XmlNode xm in xmlNodeList)
            {
                objParameters = new DAO_Bill_Sys_XMLPDFFormParameters();

                // IsTransactionDiagnosis is an optional attribute and will be applicable only to fields with diagnosis codes
                try
                {
                    objParameters.IsTransactionDiagnosis = xm.Attributes["is_transaction_diagnosis"].Value;
                }
                catch (Exception ex)
                {
                    objParameters.IsTransactionDiagnosis = "0";
                }

                try
                {
                    // this is an optional value and exists only if the form has a service table
                    objParameters.IsService = xm.Attributes["is_service"].Value;
                }
                catch (Exception ex)
                {
                    objParameters.IsService = "0";
                }

                try
                {
                    // this is an optional value and exists only if the form has a service table
                    objParameters.isImage = xm.Attributes["isImage"].Value;
                }
                catch (Exception ex)
                {
                    objParameters.isImage = "0";
                }

                if (objParameters.IsTransactionDiagnosis == "1")
                {
                    string[] szColumnIndexes = xm.Attributes["pdf_field_index"].Value.Split(':');
                    string[] szIndexes = null;//xm.Attributes["pdf_field_index"].Value.Split(',');
                    string[] szColumns = xm.Attributes["db_field_name"].Value.Split(',');
                    string[] szColPDFFieldNames = xm.Attributes["pdf_field_name"].Value.Split(':');
                    string[] szPDFFieldNames = null;

                    DAO_Bill_Sys_XMLPDFFormParameters objParams = new DAO_Bill_Sys_XMLPDFFormParameters();
                    for (int x = 0; x < szColumns.Length; x++)  // Columns. Put 1 arraylist per column
                    {
                        szIndexes = szColumnIndexes[x].Split(',');
                        szPDFFieldNames = szColPDFFieldNames[x].Split(',');
                        objDiagnosisCodes = new ArrayList();
                        for (int i = 0; i < szIndexes.Length; i++)
                        {
                            objParams = new DAO_Bill_Sys_XMLPDFFormParameters();
                            objParams.IsTransactionDiagnosis = objParameters.IsTransactionDiagnosis;
                            objParams.DBFieldName = szColumns[x];
                            objParams.PDFFieldName = szPDFFieldNames[i];
                            objParams.IfBlank = xm.Attributes["if_blank"].Value;
                            objParams.FieldIndex = szIndexes[i];
                            objDiagnosisCodes.Add(objParams);
                        }
                        objDiagnosisGrid.Add(objDiagnosisCodes);
                    }
                }
                else
                {
                    if (objParameters.IsService == "1")
                    {
                        string[] szColumnIndexes = xm.Attributes["pdf_field_index"].Value.Split(':');
                        string[] szIndexes = null;//xm.Attributes["pdf_field_index"].Value.Split(',');
                        string[] szColumns = xm.Attributes["db_field_name"].Value.Split(',');
                        string[] szColPDFFieldNames = xm.Attributes["pdf_field_name"].Value.Split(':');
                        string[] szPDFFieldNames = null;

                        DAO_Bill_Sys_XMLPDFFormParameters objParams = new DAO_Bill_Sys_XMLPDFFormParameters();
                        for (int x = 0; x < szColumns.Length; x++)  // Columns. Put 1 arraylist per column
                        {
                            szIndexes = szColumnIndexes[x].Split(',');
                            szPDFFieldNames = szColPDFFieldNames[x].Split(',');
                            objServices = new ArrayList();
                            for (int i = 0; i < szIndexes.Length; i++)
                            {
                                objParams = new DAO_Bill_Sys_XMLPDFFormParameters();
                                objParams.IsTransactionDiagnosis = objParameters.IsTransactionDiagnosis;
                                objParams.DBFieldName = szColumns[x];
                                objParams.PDFFieldName = szPDFFieldNames[i];
                                objParams.IfBlank = xm.Attributes["if_blank"].Value;
                                objParams.FieldIndex = szIndexes[i];
                                objServices.Add(objParams);
                            }
                            objServiceGrid.Add(objServices);
                        }
                    }
                    else
                    {
                        objParameters.DBFieldName = xm.Attributes["db_field_name"].Value;
                        objParameters.PDFFieldName = xm.Attributes["pdf_field_name"].Value;
                        objParameters.IfBlank = xm.Attributes["if_blank"].Value;
                        objParameters.FieldIndex = xm.Attributes["pdf_field_index"].Value;
                        try
                        {
                            // space_padding is an optional attribute and will be applicable only to fields with multiple columns
                            // in the same text field
                            objParameters.SpacePadding = xm.Attributes["space_padding"].Value;
                        }
                        catch (Exception io)
                        {
                        }

                        try
                        {
                            // is_check_box is an optional attribute and will be applicable only to check boxes
                            objParameters.IsCheckBox = xm.Attributes["is_check_box"].Value;
                        }
                        catch (Exception _xml)
                        {
                        }
                        try
                        {
                            // this holds true only if the field is a checkbox
                            objParameters.IsBit = xm.Attributes["is_bit"].Value;
                        }
                        catch (Exception _xml)
                        {
                        }

                        try
                        {
                            // this holds true only if the field is a checkbox
                            objParameters.FlipIndex = xm.Attributes["flip_index"].Value;
                        }
                        catch (Exception _xml)
                        {
                        }
                        try
                        {
                            // this holds true only if the field is for multiple checkbox
                            objParameters.DBTextFieldName = xm.Attributes["pdf_Textfield_index"].Value;
                        }
                        catch (Exception _xml)
                        {
                        }
                        try
                        {
                            // this holds true only if the field is for multiple checkbox
                            objParameters.DBProcedureName = xm.Attributes["pdf_proc_name"].Value;
                        }
                        catch (Exception _xml)
                        {
                        }
                        try
                        {
                            // this holds true only if the field is for multiple checkbox
                            objParameters.FlagName = xm.Attributes["pdf_flag_name"].Value;
                        }
                        catch (Exception _xml)
                        {
                        }

                        objRArrList.Add(objParameters);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return objRArrList;
    }

    private SqlDataReader getDataSet(string sz_BillNumber)
    {
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        objSqlConn.Open();
        SqlCommand objSqlComm1 = new SqlCommand(szProcedureName, objSqlConn);
        objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlDataReader objDR;
        objSqlComm1.CommandType = CommandType.StoredProcedure;
        objSqlComm1.Parameters.AddWithValue("@SZ_BILL_ID", sz_BillNumber);

        objSqlComm1.Parameters.AddWithValue("@FLAG", "ALL");
        objDR = objSqlComm1.ExecuteReader();
        return objDR;
    }

    private SqlDataReader getDataSet(string SZ_OFFICE_ID, string SZ_CASE_ID)
    {
        SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
        objSqlConn.Open();
        SqlCommand objSqlComm1 = new SqlCommand(szProcedureName, objSqlConn);
        objSqlComm1.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlDataReader objDR;
        objSqlComm1.CommandType = CommandType.StoredProcedure;
        objSqlComm1.Parameters.AddWithValue("@SZ_OFFICE_ID", SZ_OFFICE_ID);
        objSqlComm1.Parameters.AddWithValue("@SZ_CASE_ID", SZ_CASE_ID);

        objSqlComm1.Parameters.AddWithValue("@FLAG", "ALL");
        objDR = objSqlComm1.ExecuteReader();
        return objDR;
    }




    private SqlDataReader getMultipleCheckBox(string flagName, string procedureName, string p_szbillid)
    {
        SqlDataReader rd = null;
        try
        {
            SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
            objSqlConn.Open();
            SqlCommand objSqlComm = new SqlCommand(procedureName, objSqlConn);//("SP_MULTIPLE_CHECKBOX_SELECT_FOR_TEMPLATE_C_FOUR_TWO", objSqlConn);
            objSqlComm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            objSqlComm.CommandType = CommandType.StoredProcedure;
            objSqlComm.Parameters.AddWithValue("@SZ_BILL_ID", p_szbillid);
            objSqlComm.Parameters.AddWithValue("@FLAG", flagName);//"DIAGNOSTIC_TESTS");
            rd = objSqlComm.ExecuteReader();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return rd;
    }

    private SqlDataReader getDiagnosisCodes(string p_szbillid)
    {
        try
        {
            SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
            objSqlConn.Open();
            SqlCommand objSqlComm = new SqlCommand(szProcedureName, objSqlConn);
            objSqlComm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
            SqlDataReader rd;
            objSqlComm.CommandType = CommandType.StoredProcedure;
            objSqlComm.Parameters.AddWithValue("@SZ_BILL_ID", p_szbillid);
            objSqlComm.Parameters.AddWithValue("@FLAG", "GETDIGNOSISCODELIST");
            rd = objSqlComm.ExecuteReader();
            return rd;
        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            return null;
        }
    }

    private SqlDataReader getServices(string p_szbillid)
    {
        SqlDataReader rd = null;
        try
        {
            SqlConnection objSqlConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"]);
            objSqlConn.Open();
            SqlCommand objSqlComm = new SqlCommand(szProcedureName, objSqlConn);
            objSqlComm.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());

            objSqlComm.CommandType = CommandType.StoredProcedure;
            objSqlComm.Parameters.AddWithValue("@SZ_BILL_ID", p_szbillid);
            objSqlComm.Parameters.AddWithValue("@FLAG", "SERVICE_TABLE");
            rd = objSqlComm.ExecuteReader();

        }
        catch (Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        return rd;
    }

    private class DAO_Bill_Sys_XMLPDFFormParameters
    {
        private string szDBFieldName;
        private string szPDFFieldName;
        private string szFieldIndex;
        private string szSpacePadding;
        private string szIfBlank;
        private string szIsCheckBox;
        private string szIsBit;
        private string szFlipIndex;
        private string szIsTransactionDiagnosis;
        private string szIsService;
        private string szDBTextFieldName;
        private string szFlagName;
        private string szProcedureName;

        public string IsService
        {
            get
            {
                return szIsService;
            }
            set
            {
                szIsService = value;
            }
        }

        public string IsBit
        {
            get
            {
                return szIsBit;
            }
            set
            {
                szIsBit = value;
            }
        }

        public string FlipIndex
        {
            get
            {
                return szFlipIndex;
            }
            set
            {
                szFlipIndex = value;
            }
        }

        public string IsCheckBox
        {
            get
            {
                return szIsCheckBox;
            }
            set
            {
                szIsCheckBox = value;
            }
        }

        public string IsTransactionDiagnosis
        {
            get
            {
                return szIsTransactionDiagnosis;
            }
            set
            {
                szIsTransactionDiagnosis = value;
            }
        }

        public string SpacePadding
        {
            get
            {
                return szSpacePadding;
            }
            set
            {
                szSpacePadding = value;
            }
        }

        public string IfBlank
        {
            get
            {
                return szIfBlank;
            }
            set
            {
                szIfBlank = value;
            }
        }

        public string DBFieldName
        {
            get
            {
                return szDBFieldName;
            }
            set
            {
                szDBFieldName = value;
            }
        }

        public string FieldIndex
        {
            get
            {
                return szFieldIndex;
            }
            set
            {
                szFieldIndex = value;
            }
        }


        public string PDFFieldName
        {
            get
            {
                return szPDFFieldName;
            }
            set
            {
                szPDFFieldName = value;
            }
        }

        public string DBTextFieldName
        {
            get
            {
                return szDBTextFieldName;
            }
            set
            {
                szDBTextFieldName = value;
            }
        }

        public string DBProcedureName
        {
            get
            {
                return szProcedureName;
            }
            set
            {
                szProcedureName = value;
            }
        }

        public string FlagName
        {
            get
            {
                return szFlagName;
            }
            set
            {
                szFlagName = value;
            }
        }

        string _isImage = "";
        public string isImage
        {
            get
            {
                return _isImage;
            }
            set
            {
                _isImage = value;
            }
        }


    }

    #region "Merge PDF Logic"

    public string MergePDFFiles(string p_szCompanyID, string p_szCompanyName, string p_szCaseID, string p_szBillID, string p_szFile1, string p_szFile2)
    {
        log4net.Config.XmlConfigurator.Configure();
        try
        {
            log.Debug(" Start Merging OF 1 and 2 Page. ");
            string szDestinationFileName = getFileName(p_szBillID) + ".pdf";
            string iResult;
           
            string szFileName1 = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), p_szCompanyName, p_szCaseID) + p_szFile1;
            string szFileName2 = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), p_szCompanyName, p_szCaseID) + p_szFile2;
            string szDestinationPhysicalFileName = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), p_szCompanyName, p_szCaseID) + szDestinationFileName;

            iResult = MergePDF.MergePDFFiles(szFileName1, szFileName2, szDestinationPhysicalFileName);
            log.Debug(" Merging Completed. Status(0 : no / 1 :yes) = " + iResult);
            log.Debug(" New File Name : " + szDestinationPhysicalFileName);
            return szDestinationFileName;
        }
        catch (Exception ex)
        {
            log.Debug(" Error : " + ex.Message);
            log.Debug(" Error : " + ex.StackTrace);
            throw ex;
        }
    }

    public string Merge3and4Page(string p_szCompanyID, string p_szCompanyName, string p_szCaseID, string p_szBillID, string p_szFile1, string p_szFile2)
    {
        log4net.Config.XmlConfigurator.Configure();
        try
        {
            log.Debug(" Start Merging OF 3 and 4 Page. ");

            string szFileName = getFileName(p_szBillID) + ".pdf";

            string szDestinationFileName = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), p_szCompanyName, p_szCaseID) + szFileName;

            string iResult = "";

            log.Debug(" Source 1 " + p_szFile1);

            log.Debug("Source 2 " + p_szFile2);
            log.Debug("Destination File Path : " + szDestinationFileName);


            iResult = MergePDF.MergePDFFiles(p_szFile1, p_szFile2, szDestinationFileName);




            log.Debug(" Merging Completed. Status(0 : no / 1 :yes) = " + iResult + "p");

            log.Debug(" New File Name : " + szDestinationFileName + "p");
            return szFileName;
        }
        catch (Exception ex)
        {
            log.Debug(" Error : " + ex.Message);
            log.Debug(" Error : " + ex.StackTrace);
            throw ex;
        }
    }

    public string Merge1_2And3_4(string p_szCompanyID, string p_szCompanyName, string p_szCaseID, string p_szBillID, string p_szFile1, string p_szFile2)
    {
        log4net.Config.XmlConfigurator.Configure();
        try
        {
            log.Debug("Creation Of Final PDF. Merageing of 1&2 And 3&4");
            string szFileName = getFileName(p_szBillID) + ".pdf";
            string szPhysicalPathFile1 = getApplicationSetting("DocumentUploadLocationPhysical") + p_szFile1;
            string szPhysicalPathFile2 = getApplicationSetting("TemplateDocsPath") + p_szFile2;
            string szDestinationFileName = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), p_szCompanyName, p_szCaseID) + szFileName;
            string iResult;


            iResult = MergePDF.MergePDFFiles(szPhysicalPathFile1, szPhysicalPathFile2, szDestinationFileName);
            log.Debug(" Merging Completed. Status(0 : no / 1 :yes) = " + iResult);
            log.Debug(" New File Name : " + szDestinationFileName);
            return szFileName;
        }
        catch (Exception ex)
        {
            log.Debug(" Error : " + ex.Message);
            log.Debug(" Error : " + ex.StackTrace);
            throw ex;
        }
    }

    #endregion

    #region  " Merge PDF from AOB , EOB , Denials "

    //private void Mergefiles(string szNf3File, string szCompanyID, string szCaseID)
    //{




    //    for (int counter = 0; counter <= szFileList.Length - 1; counter++)
    //    {
    //        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
    //        string newPdfFilename = "";
    //        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
    //        objMyForm.initialize(KeyForCutePDF);

    //        if (objMyForm == null)
    //        {
    //            // Response.Write("objMyForm not initialized");
    //        }
    //        else
    //        {

    //            objMyForm.mergePDF("C:\\Test.tiff", "C:\\0.tiff", "C:\newPdf.pdf");
    //        }

    //    }
    //}

    //private string[] FileList(string szCompanyID, string szCaseID, string szLastFolder)
    //{
    //    string szDocManager = ConfigurationSettings.AppSettings["DOCMANAGER"].ToString();

    //    string szOutFolder = szDocManager + "\\" + p_szCompanyID + "\\" + p_szCaseID + "\\" + szLastFolder + "\\";
    //    string[] szFileList;
    //    szFileList = Directory.GetFiles(szOutFolder);
    //}

    //private string MerageAllFiles(string szFolderType)
    //{
    //    try
    //    {
    //        string[] szFileList = FileList(szCompanyID, szCaseID, szFolderType);
    //        string szFile1 = "";
    //        string szFile2 = "";

    //        for (int counter = 0; counter <= szFileList.Length - 1; counter++)
    //        {
    //            CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
    //            string newPdfFilename = "";
    //            string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
    //            string szFile1 = "";
    //            string szFile2 = szFileList[counter].ToString();
    //            objMyForm.initialize(KeyForCutePDF);

    //            if (objMyForm == null)
    //            {
    //                // Response.Write("objMyForm not initialized");
    //            }
    //            else
    //            {
    //                if (counter == 0)
    //                { 

    //                }
    //                else
    //                objMyForm.mergePDF("C:\\Test.tiff", "C:\\0.tiff", "C:\newPdf.pdf");
    //            }

    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }

    //}


    #endregion


    #region "Replace PDF Values Logic"

    public string ReplacePDFvalues(string XMLFile, string pdfFileName, string billNumber, string szCompanyID, string szCaseID)
    {
        log4net.Config.XmlConfigurator.Configure();
        log.Debug(" Replace Started. " + "<br/>");
        string outPutFilePath = "";
        ArrayList objListData = ParseXML(XMLFile);
        SqlDataReader objReader = getDataSet(billNumber);
        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
        string newPdfFilename = "";
        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
        objMyForm.initialize(KeyForCutePDF);

        if (objMyForm == null)
        {
            log.Debug(" objMyForm not initialized" + "<br/>");
        }
        else
        {
            log.Debug(" objMyForm  initialized" + "<br/>");
            int nReturn = 0;
            newPdfFilename = getFileName(billNumber) + ".pdf";
            log.Debug("newPdfFilename : " + newPdfFilename + "<br/>");
            outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;
            log.Debug("outPutFilePath : " + outPutFilePath + "<br/>");
            nReturn = objMyForm.openFile(pdfFileName, "");//"http://localhost/billingsystem/WC4_2.pdf"
            log.Debug(" File opened 0=False, 1=True " + nReturn + "<br/>");
            DAO_Bill_Sys_XMLPDFFormParameters objParameters = null;
            int iFieldIndex_Target = 0; String szFieldName_Target = "", szDataValue = "", szTemp = "";

            // For the number of fields in the document which are configured in the XML. Replace them with data.

            if (nReturn == 1)
            {
                log.Debug("objListData Count " + objListData.Count + "<br/>");
                if (objListData.Count > 0)
                {
                    if (objReader.HasRows)
                    {
                        if (objReader.Read())
                        {
                            string[] szMultiColumn = null;
                            string[] szSpacePadding = null;

                            for (int i = 0; i < objListData.Count; i++)
                            {
                                objParameters = (DAO_Bill_Sys_XMLPDFFormParameters)objListData[i];

                                if (objParameters.IsBit == "2" || objParameters.IsBit == "3" || objParameters.IsBit == "4")
                                {
                                    // the pdf_field_index will have multiple indexes when the bit set is 2
                                }
                                else
                                {
                                    iFieldIndex_Target = Int32.Parse(objParameters.FieldIndex);
                                    szFieldName_Target = objMyForm.getFieldName(iFieldIndex_Target);

                                    log.Debug(" Processing ... Field_Index - " + iFieldIndex_Target + " Field Name - " + szFieldName_Target + " -- data name -- " + objParameters.DBFieldName);
                                }

                                try
                                {
                                    // check if the field is a checkbox
                                    if (objParameters.IsCheckBox == "1")
                                    {
                                        String szVal = "";
                                        if (objParameters.IsBit != "3" && objParameters.IsBit != "4")
                                        {
                                            szVal = "" + objReader[objParameters.DBFieldName];
                                        }
                                        if (objParameters.IsBit == "1") // Yes No flip checkbox
                                        {
                                            log.Debug(" Processing ... Flips - " + iFieldIndex_Target + " Field Name - " + szFieldName_Target);
                                            if (szVal == "1" || szVal.ToLower() == "true")
                                            {
                                                objMyForm.setField(szFieldName_Target, "Yes");
                                            }
                                            else
                                            {
                                                int iFlipIndex_Target = Int32.Parse(objParameters.FlipIndex);
                                                String szFlipFieldName_Target = objMyForm.getFieldName(iFlipIndex_Target);
                                                objMyForm.setField(szFlipFieldName_Target, "Yes");
                                            }
                                        }
                                        else
                                        {
                                            if (objParameters.IsBit == "0") // Single checkbox
                                            {
                                                if (szVal == "1" || szVal.ToLower() == "true")
                                                {
                                                    objMyForm.setField(szFieldName_Target, "Yes");
                                                }
                                            }
                                            else
                                            {
                                                if (objParameters.IsBit == "2") // Multiple checkboxes single selection
                                                {
                                                    String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                    int iIndex = Int32.Parse("" + objReader[objParameters.DBFieldName]);
                                                    iFieldIndex_Target = Int32.Parse(szSlipIndex[iIndex]);
                                                    szFieldName_Target = objMyForm.getFieldName(iFieldIndex_Target);
                                                    log.Debug(" Processing - multiple checkboxes single selection - Field_Index - " + iFieldIndex_Target + " -- data field " + objParameters.DBFieldName + " -- data value " + objReader[objParameters.DBFieldName]);
                                                    objMyForm.setField(szFieldName_Target, "Yes");
                                                }
                                                else
                                                {
                                                    if (objParameters.IsBit == "3") // Multiple checkboxes with multiple selection
                                                    {
                                                        String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                        String[] szDbValueName = objParameters.DBFieldName.Split(',');
                                                        String[] szDbTextValueName = objParameters.DBTextFieldName.Split(',');

                                                        SqlDataReader objReaderDiagnosisTests = getMultipleCheckBox(objParameters.FlagName, objParameters.DBProcedureName, billNumber);
                                                        while (objReaderDiagnosisTests.Read())
                                                        {
                                                            for (int ii = 0; ii <= szSlipIndex.Length - 1; ii++)
                                                            {
                                                                if (szDbValueName.GetValue(ii).ToString() == objReaderDiagnosisTests["SZ_TEXT"].ToString())
                                                                {
                                                                    if (objReaderDiagnosisTests["SZ_VALUE"].ToString() == "1")
                                                                    {

                                                                        szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szSlipIndex.GetValue(ii).ToString())));
                                                                        log.Debug(" Processing multiple checkbox - Index" + szSlipIndex.GetValue(ii));
                                                                        objMyForm.setField(szFieldName_Target, "Yes");
                                                                        if (objReaderDiagnosisTests["SZ_DESCRIPTION"].ToString() != "")
                                                                        {
                                                                            szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbTextValueName.GetValue(ii).ToString())));
                                                                            objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION"].ToString());
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (objParameters.IsBit == "4") // Multiple checkboxes with multiple selection and description
                                                        {
                                                            String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                            String[] szDbValueName = objParameters.DBFieldName.Split(',');
                                                            String[] szDbTextValueName = objParameters.DBTextFieldName.Split(':');


                                                            SqlDataReader objReaderDiagnosisTests = getMultipleCheckBox(objParameters.FlagName, objParameters.DBProcedureName, billNumber);
                                                            while (objReaderDiagnosisTests.Read())
                                                            {
                                                                for (int ii = 0; ii <= szSlipIndex.Length - 1; ii++)
                                                                {
                                                                    if (szDbValueName.GetValue(ii).ToString() == objReaderDiagnosisTests["SZ_TEXT"].ToString())
                                                                    {
                                                                        if (objReaderDiagnosisTests["SZ_VALUE"].ToString() == "1")
                                                                        {

                                                                            szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szSlipIndex.GetValue(ii).ToString())));
                                                                            objMyForm.setField(szFieldName_Target, "Yes");
                                                                            String[] szDbSubValueField = szDbTextValueName.GetValue(ii).ToString().Split(',');// Split(",");

                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION1"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(0).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION1"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION2"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(1).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION2"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION3"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(2).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION3"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION4"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(3).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION4"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION5"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(4).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION5"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION6"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(5).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION6"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION7"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(6).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION7"].ToString());
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                            }
                                                        }

                                                    }//

                                                }
                                            }
                                        }
                                    }
                                    else if (objParameters.isImage == "1")
                                    {
                                        String szImagePath = (String)objReader[objParameters.DBFieldName].ToString();
                                        // String szImagePath = "c:\\docsign.jpg";
                                        objMyForm.setImage(szFieldName_Target, szImagePath, 2);
                                    }
                                    else
                                    {
                                        szMultiColumn = objParameters.DBFieldName.Split(',');
                                        szDataValue = "";
                                        if (szMultiColumn.Length > 1)
                                        {
                                            szSpacePadding = objParameters.SpacePadding.Split(',');
                                            for (int y = 0; y < szMultiColumn.Length; y++)
                                            {
                                                szTemp = (String)objReader[szMultiColumn[y]];
                                                if (szTemp.Length < Int32.Parse(szSpacePadding[y]))
                                                {
                                                    //szTemp = szTemp.PadRight(Int32.Parse(szSpacePadding[y]), ' ');
                                                }
                                                szDataValue = szDataValue + szTemp + "      ";
                                            }
                                            if (szDataValue.Trim() == "")
                                            {
                                                szDataValue = objParameters.IfBlank;
                                            }
                                            objMyForm.setField(szFieldName_Target, szDataValue);
                                        }
                                        else
                                        {
                                            String szValue = (String)objReader[objParameters.DBFieldName].ToString();
                                            if (szValue.Trim() == "" || szValue == null)
                                            {
                                                szValue = objParameters.IfBlank;
                                            }
                                            objMyForm.setField(szFieldName_Target, szValue);
                                        }
                                        szMultiColumn = null;
                                        szSpacePadding = null;
                                    }
                                }
                                catch (Exception io)
                                {
                                    log.Debug(io.StackTrace);
                                    log.Debug(io.Data);
                                    log.Debug(" @ -- index - " + iFieldIndex_Target + " field - " + szFieldName_Target + " -- " + io.Message);
                                    //throw;
                                }
                            }
                        }
                    }
                }



                objMyForm.flattenForm = 1;
                objMyForm.saveFile(outPutFilePath);
            }
        }
        return newPdfFilename;
    }

    public string PrintEnvelope(string XMLFile, string pdfFileName, string billNumber, string szCompanyID, string szCaseID)
    {
        log4net.Config.XmlConfigurator.Configure();
        log.Debug(" Replace Started. " + "<br/>");
        string outPutFilePath = "";
        ArrayList objListData = ParseXML(XMLFile);
        SqlDataReader objReader = getDataSet(billNumber, szCaseID);
        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
        string newPdfFilename = "";
        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
        objMyForm.initialize(KeyForCutePDF);

        if (objMyForm == null)
        {
            log.Debug(" objMyForm not initialized" + "<br/>");
        }
        else
        {
            log.Debug(" objMyForm  initialized" + "<br/>");
            int nReturn = 0;
            newPdfFilename = getFileName(billNumber) + ".pdf";
            log.Debug("newPdfFilename : " + newPdfFilename + "<br/>");
            outPutFilePath = getPacketDocumentFolder(getApplicationSetting("DocumentUploadLocationPhysical"), szCompanyID, szCaseID) + newPdfFilename;
            log.Debug("outPutFilePath : " + outPutFilePath + "<br/>");
            nReturn = objMyForm.openFile(pdfFileName, "");//"http://localhost/billingsystem/WC4_2.pdf"
            log.Debug(" File opened 0=False, 1=True " + nReturn + "<br/>");
            DAO_Bill_Sys_XMLPDFFormParameters objParameters = null;
            int iFieldIndex_Target = 0; String szFieldName_Target = "", szDataValue = "", szTemp = "";

            // For the number of fields in the document which are configured in the XML. Replace them with data.

            if (nReturn == 1)
            {
                log.Debug("objListData Count " + objListData.Count + "<br/>");
                if (objListData.Count > 0)
                {
                    if (objReader.HasRows)
                    {
                        if (objReader.Read())
                        {
                            string[] szMultiColumn = null;
                            string[] szSpacePadding = null;

                            for (int i = 0; i < objListData.Count; i++)
                            {
                                objParameters = (DAO_Bill_Sys_XMLPDFFormParameters)objListData[i];

                                if (objParameters.IsBit == "2" || objParameters.IsBit == "3" || objParameters.IsBit == "4")
                                {
                                    // the pdf_field_index will have multiple indexes when the bit set is 2
                                }
                                else
                                {
                                    iFieldIndex_Target = Int32.Parse(objParameters.FieldIndex);
                                    szFieldName_Target = objMyForm.getFieldName(iFieldIndex_Target);

                                    log.Debug(" Processing ... Field_Index - " + iFieldIndex_Target + " Field Name - " + szFieldName_Target + " -- data name -- " + objParameters.DBFieldName);
                                }

                                try
                                {
                                    // check if the field is a checkbox
                                    if (objParameters.IsCheckBox == "1")
                                    {
                                        String szVal = "";
                                        if (objParameters.IsBit != "3" && objParameters.IsBit != "4")
                                        {
                                            szVal = "" + objReader[objParameters.DBFieldName];
                                        }
                                        if (objParameters.IsBit == "1") // Yes No flip checkbox
                                        {
                                            log.Debug(" Processing ... Flips - " + iFieldIndex_Target + " Field Name - " + szFieldName_Target);
                                            if (szVal == "1" || szVal.ToLower() == "true")
                                            {
                                                objMyForm.setField(szFieldName_Target, "Yes");
                                            }
                                            else
                                            {
                                                int iFlipIndex_Target = Int32.Parse(objParameters.FlipIndex);
                                                String szFlipFieldName_Target = objMyForm.getFieldName(iFlipIndex_Target);
                                                objMyForm.setField(szFlipFieldName_Target, "Yes");
                                            }
                                        }
                                        else
                                        {
                                            if (objParameters.IsBit == "0") // Single checkbox
                                            {
                                                if (szVal == "1" || szVal.ToLower() == "true")
                                                {
                                                    objMyForm.setField(szFieldName_Target, "Yes");
                                                }
                                            }
                                            else
                                            {
                                                if (objParameters.IsBit == "2") // Multiple checkboxes single selection
                                                {
                                                    String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                    int iIndex = Int32.Parse("" + objReader[objParameters.DBFieldName]);
                                                    iFieldIndex_Target = Int32.Parse(szSlipIndex[iIndex]);
                                                    szFieldName_Target = objMyForm.getFieldName(iFieldIndex_Target);
                                                    log.Debug(" Processing - multiple checkboxes single selection - Field_Index - " + iFieldIndex_Target + " -- data field " + objParameters.DBFieldName + " -- data value " + objReader[objParameters.DBFieldName]);
                                                    objMyForm.setField(szFieldName_Target, "Yes");
                                                }
                                                else
                                                {
                                                    if (objParameters.IsBit == "3") // Multiple checkboxes with multiple selection
                                                    {
                                                        String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                        String[] szDbValueName = objParameters.DBFieldName.Split(',');
                                                        String[] szDbTextValueName = objParameters.DBTextFieldName.Split(',');

                                                        SqlDataReader objReaderDiagnosisTests = getMultipleCheckBox(objParameters.FlagName, objParameters.DBProcedureName, billNumber);
                                                        while (objReaderDiagnosisTests.Read())
                                                        {
                                                            for (int ii = 0; ii <= szSlipIndex.Length - 1; ii++)
                                                            {
                                                                if (szDbValueName.GetValue(ii).ToString() == objReaderDiagnosisTests["SZ_TEXT"].ToString())
                                                                {
                                                                    if (objReaderDiagnosisTests["SZ_VALUE"].ToString() == "1")
                                                                    {

                                                                        szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szSlipIndex.GetValue(ii).ToString())));
                                                                        log.Debug(" Processing multiple checkbox - Index" + szSlipIndex.GetValue(ii));
                                                                        objMyForm.setField(szFieldName_Target, "Yes");
                                                                        if (objReaderDiagnosisTests["SZ_DESCRIPTION"].ToString() != "")
                                                                        {
                                                                            szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbTextValueName.GetValue(ii).ToString())));
                                                                            objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION"].ToString());
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (objParameters.IsBit == "4") // Multiple checkboxes with multiple selection and description
                                                        {
                                                            String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                            String[] szDbValueName = objParameters.DBFieldName.Split(',');
                                                            String[] szDbTextValueName = objParameters.DBTextFieldName.Split(':');


                                                            SqlDataReader objReaderDiagnosisTests = getMultipleCheckBox(objParameters.FlagName, objParameters.DBProcedureName, billNumber);
                                                            while (objReaderDiagnosisTests.Read())
                                                            {
                                                                for (int ii = 0; ii <= szSlipIndex.Length - 1; ii++)
                                                                {
                                                                    if (szDbValueName.GetValue(ii).ToString() == objReaderDiagnosisTests["SZ_TEXT"].ToString())
                                                                    {
                                                                        if (objReaderDiagnosisTests["SZ_VALUE"].ToString() == "1")
                                                                        {

                                                                            szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szSlipIndex.GetValue(ii).ToString())));
                                                                            objMyForm.setField(szFieldName_Target, "Yes");
                                                                            String[] szDbSubValueField = szDbTextValueName.GetValue(ii).ToString().Split(',');// Split(",");

                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION1"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(0).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION1"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION2"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(1).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION2"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION3"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(2).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION3"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION4"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(3).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION4"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION5"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(4).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION5"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION6"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(5).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION6"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION7"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(6).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION7"].ToString());
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                            }
                                                        }

                                                    }//

                                                }
                                            }
                                        }
                                    }
                                    else if (objParameters.isImage == "1")
                                    {
                                        String szImagePath = (String)objReader[objParameters.DBFieldName].ToString();
                                        // String szImagePath = "c:\\docsign.jpg";
                                        objMyForm.setImage(szFieldName_Target, szImagePath, 2);
                                    }
                                    else
                                    {
                                        szMultiColumn = objParameters.DBFieldName.Split(',');
                                        szDataValue = "";
                                        if (szMultiColumn.Length > 1)
                                        {
                                            szSpacePadding = objParameters.SpacePadding.Split(',');
                                            for (int y = 0; y < szMultiColumn.Length; y++)
                                            {
                                                szTemp = (String)objReader[szMultiColumn[y]];
                                                if (szTemp.Length < Int32.Parse(szSpacePadding[y]))
                                                {
                                                    //szTemp = szTemp.PadRight(Int32.Parse(szSpacePadding[y]), ' ');
                                                }
                                                szDataValue = szDataValue + szTemp + "      ";
                                            }
                                            if (szDataValue.Trim() == "")
                                            {
                                                szDataValue = objParameters.IfBlank;
                                            }
                                            objMyForm.setField(szFieldName_Target, szDataValue);
                                        }
                                        else
                                        {
                                            String szValue = (String)objReader[objParameters.DBFieldName].ToString();
                                            if (szValue.Trim() == "" || szValue == null)
                                            {
                                                szValue = objParameters.IfBlank;
                                            }
                                            objMyForm.setField(szFieldName_Target, szValue);
                                        }
                                        szMultiColumn = null;
                                        szSpacePadding = null;
                                    }
                                }
                                catch (Exception io)
                                {
                                    log.Debug(io.StackTrace);
                                    log.Debug(io.Data);
                                    log.Debug(" @ -- index - " + iFieldIndex_Target + " field - " + szFieldName_Target + " -- " + io.Message);
                                    //throw;
                                }
                            }
                        }
                    }
                }


                objMyForm.flattenForm = 1;
                objMyForm.saveFile(outPutFilePath);
            }
        }
        return newPdfFilename;
    }

    #endregion


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

    private string getPacketDocumentFolder(string p_szPath, string p_szCompanyID, string p_szCaseID)
    {
        p_szPath = p_szPath + p_szCompanyID + "/" + p_szCaseID;
        if (Directory.Exists(p_szPath))
        {
            if (!Directory.Exists(p_szPath + "/Packet Document"))
            {
                Directory.CreateDirectory(p_szPath + "/Packet Document");
            }
        }
        else
        {
            Directory.CreateDirectory(p_szPath);
            Directory.CreateDirectory(p_szPath + "/Packet Document");
        }
        return p_szPath + "/Packet Document/";
    }

    private string getApplicationSetting(String p_szKey)
    {
        SqlConnection myConn = new SqlConnection(ConfigurationSettings.AppSettings["Connection_String"].ToString());
        myConn.Open();
        String szParamValue = "";

        SqlCommand cmdQuery = new SqlCommand("select ParameterValue from tblapplicationsettings where parametername = '" + p_szKey.ToString().Trim() + "'", myConn);
        cmdQuery.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeOut"].ToString());
        SqlDataReader dr;
        dr = cmdQuery.ExecuteReader();

        while (dr.Read())
        {
            szParamValue = dr["parametervalue"].ToString();
        }
        return szParamValue;
    }

    public string PrintEnvelope1(string XMLFile, string pdfFileName, string billNumber, string szCompanyID, string szCaseID, String szInsName, String szInsAddress, String szInsState)
    {
        log4net.Config.XmlConfigurator.Configure();
        log.Debug(" Replace Started. " + "<br/>");
        string outPutFilePath = "";
        ArrayList objListData = ParseXML(XMLFile);
        SqlDataReader objReader = getDataSet(billNumber, szCaseID);
        CUTEFORMCOLib.CutePDFDocumentClass objMyForm = new CUTEFORMCOLib.CutePDFDocumentClass();
        string newPdfFilename = "";
        string KeyForCutePDF = ConfigurationSettings.AppSettings["CutePDFSerialKey"].ToString();
        objMyForm.initialize(KeyForCutePDF);

        if (objMyForm == null)
        {
            log.Debug(" objMyForm not initialized" + "<br/>");
        }
        else
        {
            log.Debug(" objMyForm  initialized" + "<br/>");
            int nReturn = 0;
            newPdfFilename = getFileName(billNumber) + ".pdf";
            log.Debug("newPdfFilename : " + newPdfFilename + "<br/>");
            outPutFilePath = getPacketDocumentFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath"), szCompanyID, szCaseID) + newPdfFilename;
            log.Debug("outPutFilePath : " + outPutFilePath + "<br/>");
            nReturn = objMyForm.openFile(pdfFileName, "");//"http://localhost/billingsystem/WC4_2.pdf"
            log.Debug(" File opened 0=False, 1=True " + nReturn + "<br/>");
            DAO_Bill_Sys_XMLPDFFormParameters objParameters = null;
            int iFieldIndex_Target = 0; String szFieldName_Target = "", szDataValue = "", szTemp = "";

            // For the number of fields in the document which are configured in the XML. Replace them with data.

            if (nReturn == 1)
            {
                log.Debug("objListData Count " + objListData.Count + "<br/>");
                if (objListData.Count > 0)
                {
                    if (objReader.HasRows)
                    {
                        if (objReader.Read())
                        {
                            string[] szMultiColumn = null;
                            string[] szSpacePadding = null;

                            for (int i = 0; i < objListData.Count; i++)
                            {
                                objParameters = (DAO_Bill_Sys_XMLPDFFormParameters)objListData[i];

                                if (objParameters.IsBit == "2" || objParameters.IsBit == "3" || objParameters.IsBit == "4")
                                {
                                    // the pdf_field_index will have multiple indexes when the bit set is 2
                                }
                                else
                                {
                                    iFieldIndex_Target = Int32.Parse(objParameters.FieldIndex);
                                    szFieldName_Target = objMyForm.getFieldName(iFieldIndex_Target);

                                    log.Debug(" Processing ... Field_Index - " + iFieldIndex_Target + " Field Name - " + szFieldName_Target + " -- data name -- " + objParameters.DBFieldName);
                                }

                                try
                                {
                                    // check if the field is a checkbox
                                    if (objParameters.IsCheckBox == "1")
                                    {
                                        String szVal = "";
                                        if (objParameters.IsBit != "3" && objParameters.IsBit != "4")
                                        {
                                            szVal = "" + objReader[objParameters.DBFieldName];
                                        }
                                        if (objParameters.IsBit == "1") // Yes No flip checkbox
                                        {
                                            log.Debug(" Processing ... Flips - " + iFieldIndex_Target + " Field Name - " + szFieldName_Target);
                                            if (szVal == "1" || szVal.ToLower() == "true")
                                            {
                                                objMyForm.setField(szFieldName_Target, "Yes");
                                            }
                                            else
                                            {
                                                int iFlipIndex_Target = Int32.Parse(objParameters.FlipIndex);
                                                String szFlipFieldName_Target = objMyForm.getFieldName(iFlipIndex_Target);
                                                objMyForm.setField(szFlipFieldName_Target, "Yes");
                                            }
                                        }
                                        else
                                        {
                                            if (objParameters.IsBit == "0") // Single checkbox
                                            {
                                                if (szVal == "1" || szVal.ToLower() == "true")
                                                {
                                                    objMyForm.setField(szFieldName_Target, "Yes");
                                                }
                                            }
                                            else
                                            {
                                                if (objParameters.IsBit == "2") // Multiple checkboxes single selection
                                                {
                                                    String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                    int iIndex = Int32.Parse("" + objReader[objParameters.DBFieldName]);
                                                    iFieldIndex_Target = Int32.Parse(szSlipIndex[iIndex]);
                                                    szFieldName_Target = objMyForm.getFieldName(iFieldIndex_Target);
                                                    log.Debug(" Processing - multiple checkboxes single selection - Field_Index - " + iFieldIndex_Target + " -- data field " + objParameters.DBFieldName + " -- data value " + objReader[objParameters.DBFieldName]);
                                                    objMyForm.setField(szFieldName_Target, "Yes");
                                                }
                                                else
                                                {
                                                    if (objParameters.IsBit == "3") // Multiple checkboxes with multiple selection
                                                    {
                                                        String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                        String[] szDbValueName = objParameters.DBFieldName.Split(',');
                                                        String[] szDbTextValueName = objParameters.DBTextFieldName.Split(',');

                                                        SqlDataReader objReaderDiagnosisTests = getMultipleCheckBox(objParameters.FlagName, objParameters.DBProcedureName, billNumber);
                                                        while (objReaderDiagnosisTests.Read())
                                                        {
                                                            for (int ii = 0; ii <= szSlipIndex.Length - 1; ii++)
                                                            {
                                                                if (szDbValueName.GetValue(ii).ToString() == objReaderDiagnosisTests["SZ_TEXT"].ToString())
                                                                {
                                                                    if (objReaderDiagnosisTests["SZ_VALUE"].ToString() == "1")
                                                                    {

                                                                        szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szSlipIndex.GetValue(ii).ToString())));
                                                                        log.Debug(" Processing multiple checkbox - Index" + szSlipIndex.GetValue(ii));
                                                                        objMyForm.setField(szFieldName_Target, "Yes");
                                                                        if (objReaderDiagnosisTests["SZ_DESCRIPTION"].ToString() != "")
                                                                        {
                                                                            szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbTextValueName.GetValue(ii).ToString())));
                                                                            objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION"].ToString());
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (objParameters.IsBit == "4") // Multiple checkboxes with multiple selection and description
                                                        {
                                                            String[] szSlipIndex = objParameters.FieldIndex.Split(',');
                                                            String[] szDbValueName = objParameters.DBFieldName.Split(',');
                                                            String[] szDbTextValueName = objParameters.DBTextFieldName.Split(':');


                                                            SqlDataReader objReaderDiagnosisTests = getMultipleCheckBox(objParameters.FlagName, objParameters.DBProcedureName, billNumber);
                                                            while (objReaderDiagnosisTests.Read())
                                                            {
                                                                for (int ii = 0; ii <= szSlipIndex.Length - 1; ii++)
                                                                {
                                                                    if (szDbValueName.GetValue(ii).ToString() == objReaderDiagnosisTests["SZ_TEXT"].ToString())
                                                                    {
                                                                        if (objReaderDiagnosisTests["SZ_VALUE"].ToString() == "1")
                                                                        {

                                                                            szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szSlipIndex.GetValue(ii).ToString())));
                                                                            objMyForm.setField(szFieldName_Target, "Yes");
                                                                            String[] szDbSubValueField = szDbTextValueName.GetValue(ii).ToString().Split(',');// Split(",");

                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION1"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(0).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION1"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION2"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(1).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION2"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION3"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(2).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION3"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION4"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(3).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION4"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION5"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(4).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION5"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION6"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(5).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION6"].ToString());
                                                                            }
                                                                            if (objReaderDiagnosisTests["SZ_DESCRIPTION7"].ToString() != "")
                                                                            {
                                                                                szFieldName_Target = objMyForm.getFieldName(Convert.ToInt32((szDbSubValueField.GetValue(6).ToString())));
                                                                                objMyForm.setField(szFieldName_Target, objReaderDiagnosisTests["SZ_DESCRIPTION7"].ToString());
                                                                            }

                                                                        }

                                                                    }
                                                                }

                                                            }
                                                        }

                                                    }//

                                                }
                                            }
                                        }
                                    }
                                    else if (objParameters.isImage == "1")
                                    {
                                        String szImagePath = (String)objReader[objParameters.DBFieldName].ToString();
                                        // String szImagePath = "c:\\docsign.jpg";
                                        objMyForm.setImage(szFieldName_Target, szImagePath, 2);
                                    }
                                    else
                                    {
                                        szMultiColumn = objParameters.DBFieldName.Split(',');
                                        szDataValue = "";
                                        if (szMultiColumn.Length > 1)
                                        {
                                            szSpacePadding = objParameters.SpacePadding.Split(',');
                                            for (int y = 0; y < szMultiColumn.Length; y++)
                                            {
                                                szTemp = (String)objReader[szMultiColumn[y]];
                                                if (szTemp.Length < Int32.Parse(szSpacePadding[y]))
                                                {
                                                    //szTemp = szTemp.PadRight(Int32.Parse(szSpacePadding[y]), ' ');
                                                }
                                                szDataValue = szDataValue + szTemp + "      ";
                                            }
                                            if (szDataValue.Trim() == "")
                                            {
                                                szDataValue = objParameters.IfBlank;
                                            }
                                            objMyForm.setField(szFieldName_Target, szDataValue);
                                        }
                                        else
                                        {
                                            String szValue = (String)objReader[objParameters.DBFieldName].ToString();
                                            if (szValue.Trim() == "" || szValue == null)
                                            {
                                                szValue = objParameters.IfBlank;
                                            }
                                            if (szInsName != "" || szInsAddress != "" || szInsState != "")
                                            {
                                                if (szFieldName_Target.Equals("SZ_INSURANCE_COMPANY_NAME"))
                                                {


                                                    objMyForm.setField(szFieldName_Target, szInsName);


                                                }
                                                else if (szFieldName_Target.Equals("SZ_INSURANCE_ADDRESS"))
                                                {


                                                    objMyForm.setField(szFieldName_Target, szInsAddress);



                                                }
                                                else if (szFieldName_Target.Equals("SZ_INSURANCE_CITY_STATE"))
                                                {

                                                    objMyForm.setField(szFieldName_Target, szInsState);

                                                }
                                                else
                                                {
                                                    objMyForm.setField(szFieldName_Target, szValue);
                                                }
                                            }
                                            else
                                            {
                                                objMyForm.setField(szFieldName_Target, szValue);
                                            }
                                        }
                                        szMultiColumn = null;
                                        szSpacePadding = null;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                                }
                            }
                        }
                    }
                }


                objMyForm.flattenForm = 1;
                objMyForm.saveFile(outPutFilePath);
            }
        }
        return newPdfFilename;
    }

    /*    public static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            Console.WriteLine("OK");
            string szXmlfile = "D:/Work/Medical Billing/WebApplication/v1.0.0/BILLING SYSTEM/Config/WC4_2_SQL_To_PDF_Parameters.xml";
            string szBillNumber = "sas0000001";
            string szPDFPath = "http://localhost:9099/BILLINGSYSTEM/c4.2.pdf";
            string szCaseID = "WC-00001";
            string szCompanyID = "CO00023";
            PDFValueReplacement obj = new PDFValueReplacement();
            string szReturn = obj.ReplacePDFvalues(szXmlfile, szPDFPath, szBillNumber, szCompanyID, szCaseID);
        }*/

}
