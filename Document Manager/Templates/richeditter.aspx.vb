Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class richEditter
    Inherits System.Web.UI.Page

    Dim Conn As SqlClient.SqlConnection
    Public HtmString2 As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Dim CAID, UID As String
        'Dim InsSql
        Dim docname, docfile As String
        Dim InsNotes2 As String

        Conn = New SqlClient.SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))

        CAID = Request.Params.Get("Case_Id")
        UID = Request.Params.Get("User_Id")

        'Response.Write "CAID="& CAID 


        docfile = Request.Params.Get("SelDoc")
        docname = Request.Params.Get("SelDoc")
        'Response.Write docname

        docname = Mid(docname, 1, InStr(docname, ".") - 1)
        InsNotes2 = "Insert into TBLNotes values ('Document  " & docname & " printed','General',0,'" & CAID & "','" & Now() & "','" & UID & "')"
        'Response.Write InsNotes2
        ' DataConn.Execute InsNotes2
        '-------------------------User Options--------------------

        Dim SWhere As String


        SWhere = "select * from LCJ_VW_CaseSearchDetails where case_id='" & CAID & "'"
        'Response.Write swhere 
        'Response.End

        Dim sCmd As New SqlCommand(SWhere, Conn)

        'rsCase = DataConn.Execute(SWhere)

        Dim drCase As SqlDataReader

        ''if (con.State==ConnectionState.Closed)
        Conn.Open() ''Opening Connection
        drCase = sCmd.ExecuteReader()

        ' While (drCase.Read())



        ' End While

        Dim HtmString As String
        Dim errorMessage As String 'as String
        Dim fpath As String
        Dim HtmStringAll As String
        Dim TextStream As StreamReader
        Dim fso As File

        fpath = Server.MapPath("/lcj/iisdocs/" & docfile)
        'rESPONSE.WRITE fpath
        'Response.end
        ''fso = Server.CreateObject("Scripting.FileSystemObject")

        TextStream = fso.OpenText(fpath)
        HtmString = TextStream.ReadToEnd()
        TextStream.Close()
        'fso = Nothing
        errorMessage = "0"
        HtmStringAll = ""
        HtmString2 = HtmStringAll & ReplaceAll(HtmString, drCase)


        '------------------GET THE HEADER--------------------------------------
        ' HtmString = PacketingHeader(HtmString, rsUser)



    End Sub

    Private Sub getDBConnection()
        Conn = New SqlClient.SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))

    End Sub

    Private Function ReplaceAll(ByVal HtmString As String, ByRef rs As SqlDataReader)

        Dim Bal_Amount
        Dim AMOUNT_INTEREST
        Dim ATTORNEY_FEE
        Dim TOTAL_BILL

        'Read the dataset
        If (rs.Read()) Then


        'Response.Write "Claim_ID="&rs("cid") & "<br>"

        Dim t As String
            t = rs("Case_Id").ToString()
        'response.end
            On Error Resume Next
            HtmString = Replace(HtmString, "NOWDT", FormatDateTime(Now(), 2), 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "TOMORROWDT", FormatDateTime(DateAdd(DateInterval.Day, 1, Now()), 2), 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "CASE_ID", rs("Case_Id").ToString(), 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_NAME_AND_INJURED_PARTY", rs("Provider_Name").ToString() & " <BR> A/A/O " & Trim(UCase(rs("InjuredParty_Name").ToString())), 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "INJUREDPARTY_NAME", Trim(UCase(rs("InjuredParty_Name").ToString())), 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_FIRSTNAME", Trim(UCase(rs("InjuredParty_FirstName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_LASTNAME", Trim(UCase(rs("InjuredParty_LastName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_ADDRESS", Trim(UCase(rs("InjuredParty_Address").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_CITY", Trim(UCase(rs("InjuredParty_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_STATE", Trim(UCase(rs("InjuredParty_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_ZIP", Trim(UCase(rs("InjuredParty_Zip").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_MISC", Trim(UCase(rs("InjuredParty_Misc").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_PHONE", Trim(UCase(rs("InjuredParty_Phone").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INJUREDPARTY_TYPE", Trim(UCase(rs("InjuredParty_Type").ToString())) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "INSUREDPARTY_NAME", Trim(UCase(rs("InsuredParty_Name").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_FIRSTNAME", Trim(UCase(rs("InsuredParty_FirstName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_LASTNAME", Trim(UCase(rs("InsuredParty_LastName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_ADDRESS", Trim(UCase(rs("InsuredParty_Address").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_CITY", Trim(UCase(rs("InsuredParty_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_STATE", Trim(UCase(rs("InsuredParty_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_ZIP", Trim(UCase(rs("InsuredParty_Zip").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_MISC", Trim(UCase(rs("InsuredParty_Misc").ToString())) & " ", 1, -1, vbTextCompare)
            'HtmString = Replace(HtmString, "INSUREDPARTY_PHONE", Trim(UCase(rs("InsuredParty_Phone").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSUREDPARTY_TYPE", Trim(UCase(rs("InsuredParty_Type").ToString())) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "ACCIDENT_DATE", Trim(UCase(rs("Accident_Date").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "ACCIDENT_ADDRESS", Trim(UCase(rs("Accident_Address").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "ACCIDENT_CITY", Trim(UCase(rs("Accident_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "ACCIDENT_STATE", Trim(UCase(rs("Accident_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "ACCIDENT_ZIP", Trim(UCase(rs("Accident_Zip").ToString())) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "POLICY_NUMBER", Trim(UCase(rs("Policy_Number").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INS_CLAIM_NUMBER", Trim(UCase(rs("Ins_Claim_Number").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INDEXORAAA_NUMBER", Trim(UCase(rs("Indexoraaa_Number").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "STATUS", Trim(UCase(rs("Status").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INITIAL_STATUS", Trim(UCase(rs("Initial_Status").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "MEMOFIELD", Trim(rs("Memo").ToString()) & " ", 1, -1, vbTextCompare)


            HtmString = Replace(HtmString, "Attorney_FileNumber", Trim(UCase(rs("Attorney_FileNumber").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_ID", Trim(UCase(rs("Attorney_FileNumber").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Date_Summons_Served", Trim(UCase(rs("Date_Summons_Served").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Date_Answer_Received", Trim(UCase(rs("Date_Answer_Received").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Motion_Date", Trim(UCase(rs("Motion_Date").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Trial_Date", Trim(UCase(rs("Trial_Date").ToString())) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "INSURANCECOMPANY_NAME", Trim(UCase(rs("InsuranceCompany_Name").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_TYPE", Trim(rs("InsuranceCompany_Type").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_ADDRESS", Trim(rs("InsuranceCompany_Local_Address").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "InsuranceCompany_Local_City", Trim(UCase(rs("InsuranceCompany_Local_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_STATE", Trim(UCase(rs("InsuranceCompany_Local_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_ZIP", Trim(UCase(rs("InsuranceCompany_Local_Zip").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_PHONE", Trim(rs("InsuranceCompany_Local_Phone").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_LOCAL_FAX", Trim(rs("InsuranceCompany_Local_Fax").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_ADDRESS", Trim(rs("InsuranceCompany_Perm_Address").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_CITY", Trim(UCase(rs("InsuranceCompany_Perm_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_STATE", Trim(UCase(rs("InsuranceCompany_Perm_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_ZIP", Trim(UCase(rs("InsuranceCompany_Perm_Zip").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_PHONE", Trim(rs("InsuranceCompany_Perm_Phone").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_PERM_FAX", Trim(rs("InsuranceCompany_Perm_Fax").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_CONTACT", Trim(UCase(rs("InsuranceCompany_Contact").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "INSURANCECOMPANY_EMAIL", Trim(UCase(rs("InsuranceCompany_Email").ToString())) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "PROVIDER_NAME", Trim(UCase(rs("Provider_Name").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_TYPE", Trim(rs("PROVIDER_TYPE").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_LOCAL_ADDRESS", Trim(rs("Provider_Local_Address").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_LOCAL_CITY", Trim(UCase(rs("Provider_Local_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_LOCAL_STATE", Trim(UCase(rs("Provider_Local_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_LOCAL_ZIP", Trim(UCase(rs("Provider_Local_Zip").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_LOCAL_PHONE", Trim(rs("Provider_Local_Phone").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_LOCAL_FAX", Trim(rs("Provider_Local_Fax").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_PERM_ADDRESS", Trim(rs("Provider_Perm_Address").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_PERM_CITY", Trim(UCase(rs("Provider_Perm_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_PERM_STATE", Trim(UCase(rs("Provider_Perm_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_PERM_ZIP", Trim(UCase(rs("Provider_Perm_Zip").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_PERM_PHONE", Trim(rs("Provider_Perm_Phone").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_PERM_FAX", Trim(rs("Provider_Perm_Fax").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_CONTACT", Trim(UCase(rs("Provider_Contact").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PROVIDER_EMAIL", Trim(UCase(rs("Provider_Email").ToString())) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "DEFENDANT_ID", Trim(UCase(rs("Defendant_Id").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_NAME", Trim(UCase(rs("Defendant_Name").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_ADDRESS", Trim(UCase(rs("Defendant_Address").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_CITY", Trim(UCase(rs("Defendant_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_STATE", Trim(UCase(rs("Defendant_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_ZIP", Trim(UCase(rs("Defendant_Zip").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_PHONE", Trim(UCase(rs("Defendant_Phone").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_FAX", Trim(UCase(rs("Defendant_Fax").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DEFENDANT_EMAIL", Trim(UCase(rs("Defendant_Email").ToString())) & " ", 1, -1, vbTextCompare)


            HtmString = Replace(HtmString, "ADJUSTER_NAME", Trim(UCase(rs("Adjuster_FirstName").ToString() & " " & rs("ADJUSTER_LASTNAME").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "ADJUSTER_PHONE", Trim(UCase(rs("Adjuster_Phone").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "ADJUSTER_FAX", Trim(UCase(rs("Adjuster_Fax").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "ADJUSTER_EMAIL", Trim(UCase(rs("Adjuster_Email").ToString())) & " ", 1, -1, vbTextCompare)


            HtmString = Replace(HtmString, "Attorney_FirstName", Trim(UCase(rs("Attorney_FirstName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_LastName", Trim(UCase(rs("Attorney_LastName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_Name", Trim(UCase(rs("Attorney_FirstName").ToString() & " " & rs("Attorney_LastName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_Address", Trim(UCase(rs("Attorney_Address").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_City", Trim(UCase(rs("Attorney_City").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_State", Trim(UCase(rs("Attorney_State").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_Zip", Trim(rs("Attorney_Zip").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_Phone", Trim(UCase(rs("Attorney_Phone").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "Attorney_Fax", Trim(rs("Attorney_Fax").ToString()) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "COURT_NAME", Trim(UCase(rs("Court_Name").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "COURT_VENUE", Trim(UCase(rs("Court_Venue").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "COURT_ADDRESS", Trim(UCase(rs("Court_Address").ToString() & " " & rs("Attorney_LastName").ToString())) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "COURT_BASIS", Trim(rs("Court_Basis").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "COURT_MISC", Trim(UCase(rs("Court_Misc").ToString())) & " ", 1, -1, vbTextCompare)


            HtmString = Replace(HtmString, "SETTLEMENT_AMOUNT", FormatNumber(rs("Settlement_Amount").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "SETTLEMENT_INT", FormatNumber(rs("Settlement_Int").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "SETTLEMENT_AF", FormatNumber(rs("Settlement_AF").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "SETTLEMENT_FF", FormatNumber(rs("Settlement_Ff").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "SETTLEMENT_TOTAL", FormatNumber(rs("Settlement_Total").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "SETTLEMENT_DATE", FormatDateTime(rs("Settlement_Date").ToString()) & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "TRANSACTIONS_AMOUNT", FormatNumber(rs("Transactions_amount").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "TRANSACTIONS_FEE", FormatNumber(rs("Transactions_Fee").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "TRANSACTIONS_DATE", FormatDateTime(rs("Transactions_Date").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "TRANSACTIONS_DESCRIPTION", rs("Transactions_Description").ToString() & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "TRANSACTIONS_TYPE", rs("Transactions_Type").ToString() & " ", 1, -1, vbTextCompare)

            HtmString = Replace(HtmString, "CLAIM_AMOUNT", FormatNumber(rs("Claim_Amount").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "PAID_AMOUNT", FormatNumber(rs("Paid_Amount").ToString(), 2) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DATEOFSERVICE_START", FormatDateTime(rs("DateOfService_Start").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DATEOFSERVICE_END", FormatDateTime(rs("DateOfService_End").ToString()) & " ", 1, -1, vbTextCompare)
            HtmString = Replace(HtmString, "DATE_BILLSENT", FormatDateTime(rs("Date_BillSent").ToString()) & " ", 1, -1, vbTextCompare)


            HtmString = Replace(HtmString, "SCANNED_SIGNATURE", "<IMG SRC='sign.GIF'>")
            HtmString = Replace(HtmString, "User_Id", "System")

            Dim stRANS As String

            'stRANS = "select * from TBLTRANSACTIONS where case_id='" & rs("CASE_ID") & "'"
            'rstRANS = Conn.Execute(stRANS)

            Dim p, r, n
            p = Convert.ToDouble(rs("Claim_Amount"))
            r = 1.02
            n = DateDiff(DateInterval.Day, rs.GetDateTime("Date_BillSent"), Now()) / 365

            AMOUNT_INTEREST = (p * (r ^ n)) - p

            ATTORNEY_FEE = 20 / 100 * (Convert.ToDouble(rs("Claim_Amount")) + AMOUNT_INTEREST)
            Dim at_fee
            at_fee = "80.00"
            If CDbl(ATTORNEY_FEE) < CDbl(at_fee) Then
                ATTORNEY_FEE = CDbl(at_fee)
            End If

            TOTAL_BILL = Bal_Amount + AMOUNT_INTEREST + ATTORNEY_FEE

            HtmString = Replace(HtmString, "AMOUNT_INTEREST", FormatCurrency(AMOUNT_INTEREST, 2), 1, -1, 1)
            HtmString = Replace(HtmString, "ATTORNEY_FEE", FormatCurrency(ATTORNEY_FEE, 2), 1, -1, 1)
            HtmString = Replace(HtmString, "TOTAL_BILL", FormatCurrency(TOTAL_BILL, 2), 1, -1, 1)
            ReplaceAll = HtmString
            Err.Clear()


        End If
    End Function

End Class
