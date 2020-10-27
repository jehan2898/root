#Region "Imports Namespace"
Imports System
Imports System.IO
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Configuration
Imports System.Drawing.Printing.PrintDocument
Imports System.Drawing.Printing.PrintController
Imports SubSystems.WebTerMenu
Imports SubSystems.WebTer
Imports SubSystems.RP
Imports mbs.templatemanager

#End Region

Partial Class RTFEditter
    Inherits System.Web.UI.Page
    Private dataset As DataSet
    Public ds As DataSet

    Public ds1 As DataSet
    Dim CID As String
    Dim UserID As String
    Dim signobj As New DigitalSign()
    Dim objSigPlus As New SIGPLUSLib.SigPlus()
    Dim a2() As Byte
    Dim objBillReferralDoc As Bill_Sys_Referral_Doc()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        WebTer1.TerKey = ConfigurationManager.AppSettings("WebTerLicenseKey").ToString()
        Dim HtmString As String

        Dim dtTable As New DataTable
        Dim strReadPath As String
        
        If Not IsPostBack() Then
            Txtfile.Text = Session("rtffileName").ToString
            Dim objCompany As Bill_Sys_BillingCompanyObject
            objCompany = CType(Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject)
            Dim objBillRefDoc As New Bill_Sys_Referral_Doc()

            Dim objCase1 As Bill_Sys_CaseObject


            objCase1 = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject)

            drpReferral.DataSource = objBillRefDoc.BindReferralDoc(objCompany.SZ_COMPANY_ID, objCase1.SZ_CASE_ID)
            drpReferral.DataTextField = "DESCRIPTION"
            drpReferral.DataValueField = "CODE"
            drpReferral.DataBind()
            If Request.Form("hidden") IsNot Nothing Then

                If Not Directory.Exists("C:\\temp\\") Then
                    Directory.CreateDirectory("C:\\temp\\")
                End If
                Dim dt_datetimestamp As DateTime
                Dim sz_datetimestamp As String
                sz_datetimestamp = Convert.ToString(DateTime.Now)
                sz_datetimestamp = sz_datetimestamp.Replace("/", "")
                sz_datetimestamp = sz_datetimestamp.Replace(":", "")
                sz_datetimestamp = sz_datetimestamp.Replace(" ", "")
                sz_datetimestamp = sz_datetimestamp.Replace("AM", "")
                sz_datetimestamp = sz_datetimestamp.Replace("PM", "")

                signobj.SignSave(Request.Form("hidden"), "C:\\temp\\Sign_" + sz_datetimestamp + ".jpg")
                Dim objTManager As New mbs.templatemanager.PacketingScript
                Dim image As String
                image = objTManager.GetImage("C:\\temp\\Sign_" + sz_datetimestamp + ".jpg")
                Dim tm As New mbs.templatemanager.PacketingScript()
                strReadPath = Session("strReadPath")

                Dim FileRead As New FileInfo(strReadPath)
                Dim spTemplateContent As New StringBuilder()
                Dim srTemplateContent As New StreamReader(strReadPath)
                spTemplateContent.Append(srTemplateContent.ReadToEnd())
                dtTable = GetCaseData()
                HtmString = Session("rtffile")
                ' Dim str As String
                'str = HtmString
                'Dim iPlace As Int64 = HtmString.IndexOf("@sz_doctor_sign@")
                'HtmString = HtmString.Remove(iPlace, 9).Insert(iPlace, image)
                If Session("Signature").ToString.Equals("Doctor") Then
                    HtmString = HtmString.Replace("@sz_doctor_sign@", image)
                End If

                If Session("Signature").ToString.Equals("Patient") Then
                    HtmString = HtmString.Replace("@sz_patient_sign@", image)
                End If

                HtmString = tm.ReplaceAll(HtmString, dtTable, Session("strTemplateId"))
                WebTer1.Data = HtmString.ToString()
                srTemplateContent.Close()
                'Page.RegisterStartupScript("mm", "<script type='text/javascript'>window.close()</script>")


            ElseIf Session("DiagnosysCode").ToString().Equals("Diagnosys") Then
                Dim tm As New mbs.templatemanager.PacketingScript()
                Dim DiagnosysCode As String
                strReadPath = Session("strReadPath").ToString()
                Dim FileRead As New FileInfo(strReadPath)
                Dim spTemplateContent As New StringBuilder()
                Dim srTemplateContent As New StreamReader(strReadPath)
                spTemplateContent.Append(srTemplateContent.ReadToEnd())

                dtTable = GetCaseData()
                'HtmString = spTemplateContent.ToString()
                HtmString = Session("rtffile")
                'DiagnosysCode = Session("DiagnosysList").ToString()
                Dim ObjArr As New ArrayList
                ObjArr = Session("DiagnosysList")
                Dim i As Long
                Dim count As Long
                count = ObjArr.Count
                count = count - 1
                For i = 0 To count Step 1
                    DiagnosysCode = DiagnosysCode + ObjArr.Item(i).ToString() + "\par" + " "
                Next i

                HtmString = HtmString.Replace("@sz_diagnosys_code@", DiagnosysCode)

                HtmString = tm.ReplaceAll(HtmString, dtTable, Session("strTemplateId"))
                WebTer1.Data = HtmString.ToString()
                srTemplateContent.Close()


            Else
                Dim TreatmentCode As String
                Session("strTemplateId") = Request.QueryString(1).ToString()
                Dim tm As New mbs.templatemanager.PacketingScript()
                Session("strReadPath") = Request.QueryString(0).ToString()
                strReadPath = Request.QueryString(0).ToString()
                Dim FileRead As New FileInfo(strReadPath)
                Dim spTemplateContent As New StringBuilder()
                Dim srTemplateContent As New StreamReader(strReadPath)
                spTemplateContent.Append(srTemplateContent.ReadToEnd())

                dtTable = GetCaseData()
                HtmString = spTemplateContent.ToString()

                HtmString = tm.ReplaceAll(HtmString, dtTable, Session("strTemplateId"))
                HtmString = ReplaceDiagnosisCode(HtmString)

                Dim ObjArr As New ArrayList
                ObjArr = Session("TreatmentCode")

                'Dim i As Long
                'Dim count As Long
                'count = ObjArr.Count
                'count = count - 1
                'For i = 0 To count Step 1
                '    TreatmentCode = TreatmentCode + ObjArr.Item(i).ToString() + "\par" + " "
                'Next i

                'HtmString = HtmString.Replace("@treatment_table@", TreatmentCode)

                'HtmString = tm.ReplaceAll(HtmString, dtTable, Session("strTemplateId"))



                WebTer1.Data = HtmString.ToString()
                srTemplateContent.Close()
            End If

        Else
            WebTer1.Data = Data.Value
        End If
    End Sub

    Public Function HexString2Bin(ByVal HexString As String) As Byte()
        Dim BinArray() As Byte
        Dim i As Long
        Dim ndx As Long
        Dim lSize As Long

        'get the length of the input string / 2
        lSize = Len(HexString) / 2
        'set our return value
        'HexString2Bin = lSize
        'resize our array
        ReDim BinArray(0 To lSize - 1)

        'loop the string and get the value of each character pair
        For i = 1 To Len(HexString) Step 2
            BinArray(ndx) = Val("&h" & Mid(HexString, i, 2))
            ndx = ndx + 1
        Next i
        Return BinArray
    End Function

    Public Function ReplaceDiagnosisCode(ByVal HtmString As String) As String
        Dim ds As New DataSet()
        Dim _bill_Sys_AssociateDiagnosis As New Bill_Sys_AssociateDiagnosisCodeBO()
        Dim objCase As Bill_Sys_CaseObject
        Dim objCompany As Bill_Sys_BillingCompanyObject
        Dim i As Integer = 0
        Dim strDiagCode As String = ""
        Dim strCheckvalue As String = ""
        Dim strDiagCodeDescription As String = ""
        Dim tb As String = "\pard\par\ansi\deff0\rtf1\trowd\intbl {"
        Dim iRowCount As Integer = 0
        Try
            objCase = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject)
            objCompany = CType(Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject)
            ds = _bill_Sys_AssociateDiagnosis.GetDiagnosisCode(objCase.SZ_CASE_ID, objCompany.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE")
            For i = 0 To ds.Tables(0).Rows.Count() - 1
                strDiagCodeDescription = ds.Tables(0).Rows(i)("SZ_DESCRIPTION")
                If CType((ds.Tables(0).Rows(i)("CHECKED") = True), Boolean) Then
                    strDiagCode = strDiagCode + "{\field\fldpriv{\*\fldinst {\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid5777715 {\*\bkmkstart Check" + (i + 1).ToString() + "} FORMCHECKBOX }{\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid5777715 {\*\datafield 650000001400000006436865636b3200000000000000000000000000}{\*\formfield{\fftype1\ffres25\fftypetxt0\ffhps20{\*\ffname Check" + (i + 1).ToString() + "}\ffdefres1}}}}{\fldrslt }}\sectd \ltrsect \linex0\endnhere\sectlinegrid360\sectdefaultcl\sectrsid9130558\sftnbj {\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid5708167 {\*\bkmkend Check" + (i + 1).ToString() + "}  }"
                    strDiagCode = strDiagCode + "{\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid16649416\charrsid16649416 " + strDiagCodeDescription + "}"
                    strDiagCode = strDiagCode + "\cell"
                End If
            Next
            HtmString = HtmString.Replace("@diagnosis_code@", strDiagCode)

        Catch ex As Exception

        End Try
        Return HtmString
    End Function

    Public Function ReplaceTreatmentCode(ByVal HtmString As String) As String
        Dim ds As New DataSet()
        Dim _bill_Sys_AssociateDiagnosis As New Bill_Sys_AssociateDiagnosisCodeBO()
        Dim objCase As Bill_Sys_CaseObject
        Dim objCompany As Bill_Sys_BillingCompanyObject
        Dim i As Integer = 0
        Dim strDiagCode As String = ""
        Dim strCheckvalue As String = ""
        Dim strDiagCodeDescription As String = ""
        Dim tb As String = "\pard\par\ansi\deff0\rtf1\trowd\intbl {"
        Dim iRowCount As Integer = 0
        Try
            objCase = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject)
            objCompany = CType(Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject)
            ds = _bill_Sys_AssociateDiagnosis.GetDiagnosisCode(objCase.SZ_CASE_ID, objCompany.SZ_COMPANY_ID, "", "GET_DIAGNOSIS_CODE")
            For i = 0 To ds.Tables(0).Rows.Count() - 1
                strDiagCodeDescription = ds.Tables(0).Rows(i)("SZ_DESCRIPTION")
                If CType((ds.Tables(0).Rows(i)("CHECKED") = True), Boolean) Then
                    strDiagCode = strDiagCode + "{\field\fldpriv{\*\fldinst {\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid5777715 {\*\bkmkstart Check" + (i + 1).ToString() + "} FORMCHECKBOX }{\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid5777715 {\*\datafield 650000001400000006436865636b3200000000000000000000000000}{\*\formfield{\fftype1\ffres25\fftypetxt0\ffhps20{\*\ffname Check" + (i + 1).ToString() + "}\ffdefres1}}}}{\fldrslt }}\sectd \ltrsect \linex0\endnhere\sectlinegrid360\sectdefaultcl\sectrsid9130558\sftnbj {\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid5708167 {\*\bkmkend Check" + (i + 1).ToString() + "}  }"
                    strDiagCode = strDiagCode + "{\rtlch\fcs1 \af0\afs18 \ltrch\fcs0 \b\f0\fs24\insrsid16649416\charrsid16649416 " + strDiagCodeDescription + "}"
                    strDiagCode = strDiagCode + "\cell"
                End If
            Next
            HtmString = HtmString.Replace("@diagnosis_code@", strDiagCode)

        Catch ex As Exception

        End Try
        Return HtmString
    End Function

    Private Function GetCaseData() As DataTable
        Dim objTemplate As New mbs.templatemanager.TemplateManager_ColumnsDAO
        Dim objCase As Bill_Sys_CaseObject
        Dim objCompany As Bill_Sys_BillingCompanyObject

        objCase = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject)
        objCompany = CType(Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject)

        objTemplate.CaseID = objCase.SZ_CASE_ID
        objTemplate.CompanyID = objCompany.SZ_COMPANY_ID
        Dim objTManager As New mbs.templatemanager.PacketingScript
        GetCaseData = objTManager.GetCaseData(objTemplate)
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim objRTFToPDF As New Rpn()
        Dim szRTFFile, szPDFFile As String

        Dim strTemplateId As String
        strTemplateId = Session("strTemplateId")
        Rpn.RpsSetLicenseKey(ConfigurationManager.AppSettings("RPNLicenseKey").ToString())

        Dim objCase As Bill_Sys_CaseObject
        Dim objCompany As Bill_Sys_BillingCompanyObject
        Dim objUser As Bill_Sys_UserObject

        objCase = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject)
        objCompany = CType(Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject)
        objUser = CType(Session("USER_OBJECT"), Bill_Sys_UserObject)
        If (objCase Is Nothing) Then
            If (objCompany Is Nothing) Then
                lblErrorMessage.Text = "Your session has expired. Please re-login."
                Return
            End If
            lblErrorMessage.Text = "Your session has expired. Please re-login."
            Return
        End If

        

        Dim objManager As New mbs.templatemanager.TemplateManager(ConfigurationSettings.AppSettings("Connection_String").ToString())

        Try
            If Txtfile.Text <> "" Then
                Dim sFileDir As String
                Dim dtPaths As DataTable

                Dim objTemplate As New mbs.templatemanager.TemplateManager_ColumnsDAO
                objCase = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject)
                objCompany = CType(Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject)
                objTemplate.CaseID = objCase.SZ_CASE_ID
                objTemplate.CompanyID = objCompany.SZ_COMPANY_ID
                objTemplate.TemplateID = strTemplateId
                dtPaths = objManager.GetDocumentManagerNodesForTemplate(objTemplate)
                'ds = objManager.GetTemplatePaths()
                If (Not dtPaths Is Nothing) Then
                    lblErrorMessage.Text = ""
                    Dim sendDoc As String
                    sendDoc = WebTer1.Data
                    Dim sFileName As String = LTrim(RTrim(Txtfile.Text.ToString))

                    Try
                        'save file on disk into all selected nodes of the document manager
                        For index As Integer = 0 To dtPaths.Rows.Count - 1
                            sFileDir = dtPaths.Rows(index).Item(0).ToString()
                            Dim dsNodetype As New DataSet()
                            dsNodetype = objManager.GetNodeType(strTemplateId)

                            Dim dsNodeId As New DataSet()
                            dsNodeId = objManager.GetNodeId(objTemplate.CompanyID, dsNodetype.Tables(0).Rows(0)(0).ToString, objTemplate.CaseID)



                            If Not Directory.Exists(sFileDir) Then
                                Directory.CreateDirectory(sFileDir)
                            End If

                            If drpSaveAsType.SelectedValue = 0 Then
                                szRTFFile = sFileDir & sFileName & ".rtf"
                                Dim strSave As String = WebTer1.Data
                                Dim sw As New StreamWriter(szRTFFile)
                                sw.WriteLine(strSave)
                                sFileDir = dtPaths.Rows(index).Item(1).ToString()
                                objManager.UploadInDocumentManager(objTemplate.CaseID, objTemplate.CompanyID, sFileDir & "/", sFileName & ".rtf", dsNodeId.Tables(0).Rows(0)(0).ToString, objUser.SZ_USER_NAME, objUser.SZ_USER_ID, Page.Request.ServerVariables("REMOTE_ADDR").ToString(), "Template Manager")
                                sw.Close()
                                'saveRecord(Session("Case_Id").ToString() + "/DISCOVERY RESPONSES/" + szRTFFile)
                            Else
                                szRTFFile = sFileDir & sFileName & ".rtf"
                                szPDFFile = sFileDir & sFileName & ".pdf"

                                Dim strSave As String = WebTer1.Data
                                Dim sw As New StreamWriter(szRTFFile)
                                sw.WriteLine(strSave)
                                sw.Close()
                                Dim ds As New DataSet
                                ds = objManager.GetNodeId(objCase.SZ_COMAPNY_ID, "", objCase.SZ_CASE_ID)
                                objRTFToPDF.RpsConvertFile(szRTFFile, szPDFFile)
                                IO.File.Delete(szRTFFile)
                                sFileDir = dtPaths.Rows(index).Item(1).ToString()
                                objManager.UploadInDocumentManager(objTemplate.CaseID, objTemplate.CompanyID, sFileDir & "/", sFileName & ".pdf", dsNodeId.Tables(0).Rows(0)(0).ToString, objUser.SZ_USER_NAME, objUser.SZ_USER_ID, Page.Request.ServerVariables("REMOTE_ADDR").ToString(), "Template Manager")

                                'saveRecord(Session("Case_Id").ToString() + "/DISCOVERY RESPONSES/" + szPDFFile)
                            End If
                        Next
                        ClientScript.RegisterStartupScript(Me.GetType(), "Javascript", "javascript:success(); ", True)
                    Catch exc As Exception    'in case of an error
                        ClientScript.RegisterStartupScript(Me.GetType(), "Javascript", "javascript:failed(); ", True)
                    End Try
                Else
                    lblErrorMessage.Text = "No template path configured on the server."
                    Return
                End If
            Else
                lblErrorMessage.Text = "Enter a name for the document"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSign.Click
        Rpn.RpsSetLicenseKey(ConfigurationManager.AppSettings("RPNLicenseKey").ToString())
        Session("rtffile") = WebTer1.Data
        Session("Signature") = "Doctor"
        Response.Redirect("../Signature.aspx")
    End Sub
    Protected Sub btnPatientSign_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Rpn.RpsSetLicenseKey(ConfigurationManager.AppSettings("RPNLicenseKey").ToString())
        Session("rtffile") = WebTer1.Data
        Session("Signature") = "Patient"
        Response.Redirect("../Signature.aspx")
    End Sub

    Protected Sub btnDiagnosys_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("rtffile") = WebTer1.Data
        btnDiagnosysCode.Enabled = False
        Response.Redirect("../Bill_Sys_AssociateDignosisCodeCaseTemplate.aspx")
    End Sub

    'Protected Sub btnGenerateReferral_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Session("rtffile") = WebTer1.Data
    'End Sub
End Class