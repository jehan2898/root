
#Region "Imports Namespace"
Imports System
Imports System.IO
Imports obout_ASPTreeView_2_NET
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports Hangfire
Imports System.Data.SqlClient
Imports Obout.Tree_DB
Imports SystemConfiguration = Common.SystemConfiguration
Imports ShowMessage = GeneralTools
Imports System.Runtime.InteropServices
Imports BusinessNodeTags = Cases.Business.BusinessNodeTags
Imports BusinessCaseMaster = Cases.Business.BusinessCaseMaster
Imports System.Diagnostics
Imports System.Configuration
Imports System.Drawing.Printing.PrintDocument
Imports System.Drawing.Printing.PrintController
Imports log4net
Imports System.Data.Odbc
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Net.Mail
Imports System.Text



#End Region

Partial Class Case_vb_CaseInformation
    Inherits OboutInc.oboutAJAXPage
    Dim _DAO_NOTES_EO As DAO_NOTES_EO
    Dim _DAO_NOTES_BO As DAO_NOTES_BO
    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(Case_vb_CaseInformation))
#Region "Declare Public Variables"
    Public rootId As String = "root"
    Dim oTree As New obout_ASPTreeView_2_NET.Tree()
    Dim oTreeDB As TreeDB
    Dim attachTo As String = ""
    Dim blnSelect As Boolean = False
    Protected _ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Connection_String")
    Public fileName As String
    Dim strCaseID As String
    Dim up_company_id As String
    Dim mail_Case_no As String
#End Region

#Region "Page Constructor"

    Public Sub New()
        Try



            oTreeDB = New TreeDB

            'Dim szTest As String
            'szTest = System.Configuration.ConfigurationManager.AppSettings("UploadFileURI").ToString

            'MSSQL Server
            oTreeDB.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("Connection_String") ' "Driver={SQL Server};Server=172.19.3.205;UID=sa;PWD=sa123;Database=DocMgr;"
            'oTreeDB.ConnectionString = _ConnectionString
            ' declaring the table name:		
            oTreeDB.TableName("tblTags")
            oTreeDB.UpdateIgnoreFields = "NodeID" ' "NodeID,NodeHTML,NodeIcon,NodeLevel";
            ''oTreeDB.KeepNodePosition = True

            ' declaring the name and the type of the fields from the database
            oTreeDB.FieldName_ID("NodeID")
            oTreeDB.FieldName_Parent("ParentID")
            oTreeDB.FieldName_Html("NodeName")
            oTreeDB.FieldName_Icon("NodeIcon")
            oTreeDB.FieldName_LevelIndex("NodeLevel", "numeric")
            oTreeDB.FieldName_Expanded("Expanded", "numeric")
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ChkCaseid As String
        hfUrl.Value = ConfigurationManager.AppSettings("webscanurl").ToString()
        Dim dset As New DataSet

        Dim objBillCompany2 As New Bill_Sys_BillingCompanyObject()

        Dim _bill_Sys_BillingCompanyDetails_BO2 As New Bill_Sys_BillingCompanyDetails_BO
        If Session("Case_ID") Is Nothing Then
            Session("Case_ID") = Request.QueryString("caseid").ToString()
        End If
        objBillCompany2 = _bill_Sys_BillingCompanyDetails_BO2.getCompanyDetailsOfCase(Session("Case_ID").ToString())

        Session("BILLING_COMPANY_OBJECT_NEW") = objBillCompany2
        Dim CompanyID As String = objBillCompany2.SZ_COMPANY_ID
        Dim p_szCompanyName As String = objBillCompany2.SZ_COMPANY_NAME
        Session("cmpid") = CompanyID
        Session("cmpName") = p_szCompanyName

        Dim strNodeList As String()

        strNodeList = nodeid.Value.Split(",")
        Session("NodeList") = strNodeList

        If Not Session("ChkCaseNoTxt") Is Nothing Then
            If Not Session("ChkCase_ID") Is Nothing Then
                ChkCaseid = Session("ChkCase_ID").ToString()
                If ChkCaseid <> Session("Case_ID").ToString() Then
                    If Not Session("ParentLoad") Is Nothing Then
                        Dim reloadparent As String
                        reloadparent = Session("ParentLoad").ToString()
                        If Not Me.Page.ClientScript.IsClientScriptBlockRegistered(reloadparent) Then
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "ss", reloadparent)
                        End If
                    End If
                End If
                Session.Remove("ChkCase_ID")
            End If
            Session.Remove("ChkCaseNoTxt")
        End If

        'If Not Session("ChkCase_ID") Is Nothing Then
        '    ChkCaseid = Session("ChkCase_ID").ToString()
        '    If ChkCaseid <> Session("Case_ID").ToString() Then
        '        If Not Session("ParentLoad") Is Nothing Then
        '            Dim reloadparent As String
        '            reloadparent = Session("ParentLoad").ToString()
        '            If Not Me.Page.ClientScript.IsClientScriptBlockRegistered(reloadparent) Then
        '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ss", reloadparent)
        '            End If
        '        End If
        '    End If
        'End If

        If Not Page.IsPostBack Then
            If Session("DeletAddNode") IsNot Nothing Then
                Dim alertMessage As String = "alert('" & Session("DeletAddNode").ToString() & "');"
                Page.ClientScript.RegisterStartupScript(Page.[GetType](), "showAlert", alertMessage, True)
                Session.Remove("DeletAddNode")
            End If
            If Not Request.QueryString("caseid") Is Nothing Then
                Dim CaseId As String
                Dim CaseNo As String
                Dim cmpid As String
                CaseId = Request.QueryString("caseid")
                CaseNo = Request.QueryString("caseno")

                cmpid = Request.QueryString("cmpid")
                up_company_id = cmpid
                Dim _caseDetailsBO As New CaseDetailsBO
                Dim _bill_Sys_CaseObject As New Bill_Sys_CaseObject
                _bill_Sys_CaseObject.SZ_PATIENT_ID = _caseDetailsBO.GetCasePatientID(CaseId, "")
                _bill_Sys_CaseObject.SZ_CASE_ID = CaseId
                _bill_Sys_CaseObject.SZ_CASE_NO = CaseNo
                _bill_Sys_CaseObject.SZ_PATIENT_NAME = _caseDetailsBO.GetPatientName(_bill_Sys_CaseObject.SZ_PATIENT_ID)
                _bill_Sys_CaseObject.SZ_COMAPNY_ID = cmpid
                Session("CASE_OBJECT") = _bill_Sys_CaseObject
                Session("PassedCaseID") = CaseId
                Session("QStrCaseID") = CaseId
                Session("Case_ID") = CaseId
                Session("Archived") = "0"
                Session("QStrCID") = CaseId
                Session("SelectedID") = CaseId
                Session("DM_User_Name") = CType(Session("USER_OBJECT"), Bill_Sys_UserObject).SZ_USER_NAME
                Session("User_Name") = CType(Session("USER_OBJECT"), Bill_Sys_UserObject).SZ_USER_NAME
                Session("SN") = "0"
                Session("LastAction") = "vb_CaseInformation.aspx"
            End If
        End If


        If Not Session("flag") Is Nothing Then
            If Session("flag") = True Then
                lblMsg.Visible = True
            ElseIf Session("flag") = False Then
                lblMsg.Visible = False
            End If
        End If

        lblPatientName.Text = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject).SZ_PATIENT_NAME

        Dim objLoginBO As New Bill_Sys_LoginBO
        Dim p_szConfigurationID As String = "CO000000000000000001"

        Dim objBillCompany1 As New Bill_Sys_BillingCompanyObject()
        Dim _bill_Sys_BillingCompanyDetails_BO1 As New Bill_Sys_BillingCompanyDetails_BO
        objBillCompany1 = _bill_Sys_BillingCompanyDetails_BO1.getCompanyDetailsOfCase(Session("Case_ID").ToString())

        Session("BILLING_COMPANY_OBJECT_NEW") = objBillCompany1
        Dim p_szCompanyID As String = objBillCompany1.SZ_COMPANY_ID
        Dim dt As DataTable = objLoginBO.GetDocConfigSettings(p_szCompanyID, p_szConfigurationID)
        If dt.Rows.Count = 0 Then
            Session("SYSTEM_DOC_OBJECT") = 0
            btnLoadWithNodes.Visible = False
        Else
            Session("SYSTEM_DOC_OBJECT") = dt.Rows(0)(0).ToString()
            If Session("SYSTEM_DOC_OBJECT") = 1 Then
                btnLoadWithNodes.Visible = True
            Else
                btnLoadWithNodes.Visible = False

            End If

        End If


        If Not Session("Case_ID") Is Nothing Then

            Dim objBillCompany As New Bill_Sys_BillingCompanyObject()

            Dim _bill_Sys_BillingCompanyDetails_BO As New Bill_Sys_BillingCompanyDetails_BO

            objBillCompany = _bill_Sys_BillingCompanyDetails_BO.getCompanyDetailsOfCase(Session("Case_ID").ToString())

            Session("BILLING_COMPANY_OBJECT_NEW") = objBillCompany

            hfCompanyId.Value = objBillCompany.SZ_COMPANY_ID
            hfPatientName.Value = lblPatientName.Text
            hfCaseNo.Value = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject).SZ_CASE_NO
            hfcompanyname.Value = objBillCompany.SZ_COMPANY_NAME


            sNewNodeText.Focus()
            log4net.Config.DOMConfigurator.Configure()

            If log.IsDebugEnabled Then
                log.Debug("Exceuting on-load function .... ")
            End If

            sNewNodeText.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnAddNode.UniqueID + "').click();return false;}} else {return true}; ")

            fileUpload.Focus()
            fileUpload.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnAddFile.UniqueID + "').click();return false;}} else {return true}; ")

            lblSession.Attributes.Add("value", Session("RoleName"))

            'dset = GetNodeDetails()

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "Session", "<script language='javascript'> if(document.getElementById('lblSession').value == 'Admin' || document.getElementById('lblSession').value =='Super Admin')" & _
            '                                   " { document.getElementById('miDelete').style.display='block'; } else { document.getElementById('miDelete').style.display='none'; } </script>)")


            If Not Page.IsPostBack Then
                EasyMenu1.Visible = False
                'hidCaseID.value = "CASE"



                Dim szSN As String = Session("SN")
                If szSN = "1" Then
                    btnLoadWithNodes.Text = " ... Show nodes with documents ... "
                Else
                    btnLoadWithNodes.Text = " ... Show all nodes ... "
                End If

                'If Session("Case_ID") <> Nothing Then
                '    'Session("QStrCaseID") = Request.QueryString("Case_ID")
                '    'Session("Archived") = Request.QueryString("A")
                '    'Session("QStrCID") = Request.QueryString("ID")
                '    'Session("SelectedID") = Request.QueryString("ID")
                '    'Session("DM_User_Name") = Request.QueryString("User_Name")
                '    '                lnkEditCase.PostBackUrl = "~/Search/EditCases.aspx?MOP=E&Case_Id=" & Session("QStrCaseID") & "&ID=" & Session("QStrCID")

                'ElseIf Request.QueryString("OP") = "N" Then
                '    'Session("Archived") = Session("Archived")
                '    Session("QStrCaseID") = Nothing
                '    Session("QStrCID") = Nothing
                '    'Session("SelectedCaseID") = Nothing
                '    FillCaseDropDownList()
                '    ddlCaseID.ClearSelection()
                '    If Request.QueryString("A") = "1" Then
                '        Session("SelectedCaseID") = Nothing
                '        Session("SelectedID") = Nothing
                '    End If
                '    If Request.QueryString("A") = "0" Then
                '        Session("SelectedCaseID") = Nothing
                '        Session("SelectedID") = Nothing
                '    End If
                'End If

                If Session("QStrCaseID") <> Nothing Then

                    ''AddCaseID(Session("QStrCaseID"))
                    AddCaseID(Session("QStrCaseID"), Session("QStrCID"))
                    ''DisplayTreeview(Session("QStrCaseID"))
                    DisplayTreeview(Session("QStrCaseID"), Session("QStrCID"))
                    ''Text1.Value = Session("QStrCaseID")
                    Text1.Value = Session("QStrCID")
                    tdCaseID.Visible = False

                ElseIf Session("SelectedCaseID") <> Nothing Then
                    If Session("SelectedCaseID") <> "--SELECT--" Then
                        FillCaseDropDownList()
                        ddlCaseID.ClearSelection()
                        ddlCaseID.Items.FindByText(Session("SelectedCaseID").ToString()).Selected = True
                        'AddCaseID(Session("SelectedCaseID"))
                        AddCaseID(Session("SelectedCaseID"), Session("SelectedID"))
                        ''DisplayTreeview(Session("SelectedCaseID"))
                        DisplayTreeview(Session("QStrCaseID"), Session("SelectedID"))
                        Text1.Value = Session("SelectedID")
                        tdCaseID.Visible = True
                    End If
                End If
            End If
            Dim szURL As String = Session("LastAction")
            If szURL Is Nothing Or szURL = "" Then
                'szURL = "vb_CaseInformation.aspx?A=0&SN=0" & "&Case_ID=" & Request.QueryString("Case_ID") & "&ID=" & Request.QueryString("ID") & "&User_Name=" & Request.QueryString("User_Name")
                szURL = "vb_CaseInformation.aspx"
                Session("LastAction") = szURL
            End If
        Else
            Dim strError As String = "Session Expired."
            strError = strError.Replace("\n", " ")
            Response.Redirect("..\..\Bill_Sys_ErrorPage.aspx?ErrMsg=" + strError)
        End If



        Dim IsLawFirm As String
        IsLawFirm = CType(Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject).BT_LAW_FIRM
        If IsLawFirm = True Then
            hdnMenu.Value = "True"
            'tdJumpto.Visible = False

            Dim iRem As Integer
            Dim oMenuItemBase As OboutInc.EasyMenu_Pro.MenuItem
            For iRem = 20 To 0 Step -1
                Try
                    oMenuItemBase = CType(EasyMenu1.Components.Item(iRem), OboutInc.EasyMenu_Pro.MenuItem)
                    oMenuItemBase.Disabled = True
                Catch ex As Exception
                End Try
            Next

        Else
            hdnMenu.Value = "False"
        End If

        If Session("DocReadOnly") = True Then
            tdJumpto.Visible = False
            hdnMenu.Value = "True"
            'tdJumpto.Visible = False

            Dim iRem As Integer
            Dim oMenuItemBase As OboutInc.EasyMenu_Pro.MenuItem
            For iRem = 20 To 0 Step -1
                Try
                    oMenuItemBase = CType(EasyMenu1.Components.Item(iRem), OboutInc.EasyMenu_Pro.MenuItem)
                    oMenuItemBase.Disabled = True
                Catch ex As Exception
                End Try
            Next
            Session("DocReadOnly") = False
        End If


        'To Check whether Company is  LawFirm Or Not



        ' End Of Code
    End Sub
#End Region

#Region "Display Treeview Selected Changed Event"

    Protected Sub ddlCaseID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCaseID.SelectedIndexChanged

        Session("SelectedCaseID") = ddlCaseID.SelectedItem.ToString()
        Session("blnLodeFile") = "NO"
        Session("SelectedID") = ddlCaseID.SelectedItem.Value
        ''strCaseID = ddlCaseID.SelectedItem.ToString()
        If Session("SelectedCaseID") <> "--SELECT--" Then
            If blnSelect = False Then
                AddCaseID(Session("SelectedCaseID"), Session("SelectedID"))
                ''DisplayTreeview(Session("SelectedCaseID"))
                DisplayTreeview(Session("SelectedCaseID"), Session("SelectedID"))
                Text1.Value = Session("SelectedID")
                blnSelect = True
            End If
        End If
        '        lnkEditCase.PostBackUrl = "~/Search/EditCases.aspx?MOP=E&Case_Id=" & Session("SelectedCaseID") & "&ID=" & Session("SelectedID")
        ''DisplayTreeview()
    End Sub

#End Region

#Region "Display Treeview Function"

    Public Sub DisplayTreeview(ByVal strCaseID As String, ByVal strCID As String)

        EasyMenu1.Visible = True
        'MSSQL Server
        ''Dim oConn As SqlConnection = New SqlConnection("user id=sa; password=sa123; data source=172.18.3.227;persist security info=False;initial catalog=DocMgr;Min Pool Size=10;Connection Lifetime=120;Connection Timeout=90")
        Dim oConn As SqlConnection = New SqlConnection(_ConnectionString)
        oConn.Open()

        ' read the information from the database
        'Dim sQuery As String = "SELECT NodeID, ParentID, NodeHTML, NodeIcon,NodeLevel,CaseID,Expanded FROM tblTags ORDER BY NodeLevel ASC"
        'Dim sQuery As String = "SELECT NodeID, ParentID, NodeName, NodeIcon,NodeLevel,CaseID,Expanded FROM tblTags WHERE CaseID = " & "'" & strCaseID & "'"

        ''Dim sQuery As String

        ''sQuery = "Select cast(T.NodeID as nvarchar) NodeID, T.ParentID, T.NodeName, T.NodeIcon, T.NodeLevel, T.Expanded" & _
        ''        " from tblTags T Where CaseID='" & strCaseID & "' " & _
        ''        " union " & _
        ''        " Select 'IMG-' + Cast(I.ImageID as nvarchar), T.NodeID, '<a href='''+s.ParameterValue+I.FilePath+I.FileName +''' target=''_blank''>'+I.FileName+'</a>', 'page.gif', T.NodeLevel+1, 1 from tblImages I " & _
        ''        " Inner Join tblImageTag IT on IT.ImageID=i.ImageID " & _
        ''        " Inner Join tblTags T on T.NodeID=IT.TagID and T.CaseID='" & strCaseID & "' left join tblApplicationSettings s on s.parametername='DocumentUploadLocation' order by nodelevel"



        ''Dim oCommand As New SqlCommand(sQuery)

        '' Check which type of list if to be opened:
        Dim szListType As String, szProcedureName As String
        szListType = Session("SN")

        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)

        'Dim objDocObject As New Bill_Sys_DocManagerObject
        'objDocObject = CType(Session("SYSTEM_DOC_OBJECT"), Bill_Sys_DocManagerObject)

        Dim oCommand As New SqlCommand()

        oCommand.Connection = oConn

        If (Not szListType Is Nothing) Then
            If (szListType <> "" And szListType = "1") Then
                oCommand.CommandText = "STP_DSP_GET_CASEDOCUMENTNODELIST"
                oCommand.Parameters.Add("@CASEID", SqlDbType.NVarChar, 255).Value = strCID
                oCommand.Parameters.Add("@ISARCHIVED", SqlDbType.NChar).Value = Session("Archived").ToString()
                oCommand.Parameters.Add("@SZ_COMPANY_ID", SqlDbType.NChar).Value = objBillCompany.SZ_COMPANY_ID
                'oCommand.Parameters.Add("@DOCFILES", SqlDbType.NChar).Value = Session("SYSTEM_DOC_OBJECT").ToString()
            Else
                oCommand.CommandText = "STP_DSP_GET_CASEDOCUMENTNODELIST_LEAFED_NODES"
                oCommand.Parameters.Add("@CASEID", SqlDbType.NVarChar, 255).Value = strCID
                oCommand.Parameters.Add("@ISARCHIVED", SqlDbType.NChar).Value = Session("Archived").ToString()
                oCommand.Parameters.Add("@OPERATION", SqlDbType.NChar).Value = "LEAFED-NODES"
                oCommand.Parameters.Add("@SZ_COMPANY_ID", SqlDbType.NChar).Value = objBillCompany.SZ_COMPANY_ID
                oCommand.Parameters.Add("@DOCFILES", SqlDbType.NChar).Value = Session("SYSTEM_DOC_OBJECT").ToString()

            End If
        Else
            oCommand.CommandText = "STP_DSP_GET_CASEDOCUMENTNODELIST_LEAFED_NODES"
            oCommand.Parameters.Add("@CASEID", SqlDbType.NVarChar, 255).Value = strCID
            oCommand.Parameters.Add("@ISARCHIVED", SqlDbType.NChar).Value = Session("Archived").ToString()
            oCommand.Parameters.Add("@OPERATION", SqlDbType.NChar).Value = "LEAFED-NODES"
            oCommand.Parameters.Add("@SZ_COMPANY_ID", SqlDbType.NChar).Value = objBillCompany.SZ_COMPANY_ID
            oCommand.Parameters.Add("@DOCFILES", SqlDbType.NChar).Value = Session("SYSTEM_DOC_OBJECT").ToString()
        End If
        oCommand.CommandType = CommandType.StoredProcedure
        oCommand.CommandTimeout = 0
        Dim daSqlDataAdapter As New SqlDataAdapter(oCommand)
        Dim Ds As New DataSet()
        daSqlDataAdapter.Fill(Ds)

        Dim oReader As DataTableReader

        If (Session("SN").ToString() = 0) Then

            Dim Ds1 As New DataSet()
            Ds1 = Nodeleval(Ds)
            Dim dt As New DataTable()
            dt.Columns.Add("NodeID")
            dt.Columns.Add("ParentID")
            dt.Columns.Add("NodeName")
            dt.Columns.Add("NodeIcon")
            dt.Columns.Add("NodeLevel")
            dt.Columns.Add("Expanded")
            dt.Columns.Add("BT_DELETE")
            dt.Columns.Add("BT_UPDATE")


            For i As Integer = 0 To Ds1.Tables(0).Rows.Count - 1
                If Ds1.Tables(0).Rows(i)("BT_UPDATE").ToString() = "1" Then
                    Dim drow As DataRow = dt.NewRow()
                    drow("NodeID") = Ds1.Tables(0).Rows(i)("NodeID").ToString()
                    drow("ParentID") = Ds1.Tables(0).Rows(i)("ParentID").ToString()
                    drow("NodeName") = Ds1.Tables(0).Rows(i)("NodeName").ToString()
                    drow("NodeIcon") = Ds1.Tables(0).Rows(i)("NodeIcon").ToString()
                    drow("NodeLevel") = Ds1.Tables(0).Rows(i)("NodeLevel").ToString()
                    drow("Expanded") = Ds1.Tables(0).Rows(i)("Expanded").ToString()
                    drow("BT_DELETE") = Ds1.Tables(0).Rows(i)("BT_DELETE").ToString()
                    drow("BT_UPDATE") = Ds1.Tables(0).Rows(i)("BT_UPDATE").ToString()
                    dt.Rows.Add(drow)
                End If

            Next


            oReader = dt.CreateDataReader()

        Else

            Dim dt1 As New DataTable()
            dt1.Columns.Add("NodeID")
            dt1.Columns.Add("ParentID")
            dt1.Columns.Add("NodeName")
            dt1.Columns.Add("NodeIcon")
            dt1.Columns.Add("NodeLevel")
            dt1.Columns.Add("Expanded")
            dt1.Columns.Add("BT_DELETE")
            dt1.Columns.Add("BT_UPDATE")


            For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1

                Dim drow1 As DataRow = dt1.NewRow()
                drow1("NodeID") = Ds.Tables(0).Rows(i)("NodeID").ToString()
                drow1("ParentID") = Ds.Tables(0).Rows(i)("ParentID").ToString()
                drow1("NodeName") = Ds.Tables(0).Rows(i)("NodeName").ToString()
                drow1("NodeIcon") = Ds.Tables(0).Rows(i)("NodeIcon").ToString()
                drow1("NodeLevel") = Ds.Tables(0).Rows(i)("NodeLevel").ToString()
                drow1("Expanded") = Ds.Tables(0).Rows(i)("Expanded").ToString()
                drow1("BT_DELETE") = Ds.Tables(0).Rows(i)("BT_DELETE").ToString()
                drow1("BT_UPDATE") = Ds.Tables(0).Rows(i)("BT_UPDATE").ToString()
                dt1.Rows.Add(drow1)



            Next


            oReader = dt1.CreateDataReader()
        End If




        ' oReader =  Ds.Tables[0].CreateDataReader


        'oReader = oCommand.ExecuteReader()


        If (oReader.HasRows = False) Then
            oTree.AddRootNode("DocumentManager", Nothing)
            oTree.Add("root", 0, strCaseID, 1, "folder.gif", Nothing)
            attachTo = 0 & ","
            'AddCaseID(strCaseID)
            ''rootId = sNodeId
        Else
            Dim sNodeId As String
            Dim sParentId As String
            Dim sHtml As String
            Dim sIcon As String
            Dim sNodeLevel As String
            Dim sAllNodeId As String
            Dim sBitval As String
            sAllNodeId = ""
            Dim iExpanded As Integer = 1 ' all the nodes will be expanded
            '' rootId = oReader.GetValue(5).ToString()
            ' make a loop through all the records from the database and add them to the ASPTreeView

            oTree.AddRootNode("DocumentManager", Nothing)

            While oReader.Read()
                sNodeId = oReader.GetValue(0).ToString()
                sParentId = oReader.GetValue(1).ToString()
                sHtml = oReader.GetValue(2).ToString()
                sIcon = oReader.GetValue(3).ToString()
                sNodeLevel = oReader.GetValue(4).ToString()
                sBitval = oReader.GetValue(6).ToString()
                If sBitval <> "0" And sBitval <> "NULL" And sBitval <> "" Then
                    sAllNodeId = sAllNodeId + "," + sNodeId
                End If



                If sParentId <> "" And sParentId <> "NULL" Then
                    sAllNodeId = sAllNodeId + "," + sNodeId
                    If sNodeLevel = "1" Then
                        'oTree.Add(rootId, sNodeId, sHtml, iExpanded, sIcon, Nothing)
                        If Session("SYSTEM_DOC_OBJECT").ToString() = "1" And Session("SN") = "0" Then
                            oTree.Add(rootId, sNodeId, sHtml, iExpanded, sIcon, Nothing)
                        Else
                            oTree.Add(rootId, sNodeId, sHtml, 0, sIcon, Nothing)
                            attachTo += sNodeId & ","
                        End If
                    Else
                        'oTree.Add(sParentId, sNodeId, sHtml, iExpanded, sIcon, Nothing)
                        If Session("SYSTEM_DOC_OBJECT").ToString() = "1" And Session("SN") = "0" Then
                            oTree.Add(sParentId, sNodeId, sHtml, iExpanded, sIcon, Nothing)
                        Else
                            oTree.Add(sParentId, sNodeId, sHtml, 0, sIcon, Nothing)
                            attachTo += sNodeId & ","
                        End If

                    End If

                Else
                    ''oTree.AddRootNode(sHtml, True, Nothing)
                    oTree.Add("root", sNodeId, sHtml, iExpanded, sIcon, Nothing)
                    attachTo += sNodeId & ","
                    rootId = sNodeId

                End If
            End While
            hfAllId.Value = sAllNodeId.ToString()




        End If
        oConn.Close()

        ' change this to your local TreeIcons folder
        oTree.FolderIcons = "EasyMenu/icons"
        oTree.FolderStyle = "EasyMenu/icons"
        oTree.FolderScript = "EasyMenu/Styles"

        oTree.ShowIcons = False

        oTree.Width = "200px"
        oTree.ShowIcons = True

        Dim szDragDropNode As String = System.Configuration.ConfigurationManager.AppSettings("DragAndDropEnable")
        Dim szEditNode As String = System.Configuration.ConfigurationManager.AppSettings("EditNodeEnable")

        If szEditNode = "" Or szEditNode Is Nothing Then
            szEditNode = "false"
        End If

        If szDragDropNode = "" Or szDragDropNode Is Nothing Then
            szDragDropNode = "false"
        End If

        If (StrComp(szEditNode, "true", CompareMethod.Text) = 0) Then
            oTree.EditNodeEnable = True
        Else
            oTree.EditNodeEnable = False
        End If

        If (StrComp(szDragDropNode, "true", CompareMethod.Text) = 0) Then
            oTree.DragAndDropEnable = True
        Else
            oTree.DragAndDropEnable = False
        End If

        ' Enabling the server-side events
        oTree.EventList = "OnNodeEdit,OnAddNode,OnRemoveNode,OnNodeSelect,OnNodeDrop"

        'Write treeview to your page.
        TreeView.Text = oTree.HTML()




        EasyMenu1.AttachTo = attachTo
    End Sub

#End Region

#Region "The server side method that will handle the TREEVIEW CASEID event"

    Public Function AddCaseID(ByVal strCaseID As String, ByVal strID As String) As String
        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)



        ''Dim myConnection As New SqlConnection("Data Source=172.18.3.227;Initial Catalog=DocMgr;User ID=sa;Password=sa123")
        Dim myConnection As New SqlConnection(_ConnectionString)
        Try
            myConnection.Open()

            Dim myCommand As New SqlCommand("STP_DSP_TREEVIEWCASEID", myConnection)
            'myCommand.CommandTimeout=0;
            myCommand.CommandType = CommandType.StoredProcedure

            ''myCommand.Parameters.Add("@NODEID", SqlDbType.Int).Value = 
            myCommand.Parameters.Add("@PARENTID", SqlDbType.Int).Value = DBNull.Value
            myCommand.Parameters.Add("@NODENAME", SqlDbType.NVarChar, 300).Value = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject).SZ_CASE_NO   'strCaseID
            myCommand.Parameters.Add("@CASEID", SqlDbType.NVarChar, 50).Value = strID
            myCommand.Parameters.Add("@DOCTYPEID", SqlDbType.Int).Value = DBNull.Value
            myCommand.Parameters.Add("@NODEICON", SqlDbType.NVarChar, 50).Value = "Folder.gif"
            myCommand.Parameters.Add("@NODELEVEL", SqlDbType.Int).Value = 1
            myCommand.Parameters.Add("@EXPANDED", SqlDbType.Bit).Value = 1
            'myCommand.Parameters.Add("@CASETYPE", SqlDbType.VarChar).Value = ""
            myCommand.Parameters.Add("@NODEIDOUT", SqlDbType.Int)
            myCommand.Parameters.Add("@SZ_COMPANY_ID", SqlDbType.NChar).Value = objBillCompany.SZ_COMPANY_ID
            myCommand.Parameters("@NODEIDOUT").Direction = ParameterDirection.Output
            myCommand.ExecuteNonQuery()
            ViewState("TagIDold") = myCommand.Parameters("@NODEIDOUT").Value.ToString()

        Catch ex As SqlException
            Response.Write(ex.Message)
        End Try

        Return strCaseID
    End Function
#End Region

#Region "Uploding File Function"

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim SaveLocation As String
        Dim uploadnode() As String
        Dim strFullPath As String = ""
        Dim nodeid As String

        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)

        If log.IsDebugEnabled Then
            log.Debug("In File upload .... ")
        End If

        Try
            If Not fileUpload.PostedFile Is Nothing And fileUpload.PostedFile.ContentLength > 0 Then

                Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocationPhysical") ' + Session("SelectedCaseID").ToString() + "/"

                Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()

                'objBusinessNodeTags.FetchNodeRootPath(HiddenField1.Value)
                'SaveLocation = strBaseUploadFolder + hidnSelected.Value + "/"
                'SaveLocation = strBaseUploadFolder + objBusinessNodeTags.FetchNodeRootPath(HiddenField1.Value)

                SaveLocation = objBusinessNodeTags.FetchNodeRootPath(HiddenField1.Value)
                nodeid = HiddenField1.Value
                uploadnode = SaveLocation.Split(New Char() {"/"c})
                Dim len As Integer = uploadnode(0).Length

                Dim str1 As String = SaveLocation.Substring(len + 1)


                Dim FileName As String = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
                If Not FileName Is Nothing Then
                    FileName = FileUtilities.FormatUploadedFileName(FileName)
                End If


                Dim strPath As String ' = Server.MapPath(strBaseUploadFolder)
                strPath = strBaseUploadFolder
                SaveLocation = objBillCompany.SZ_COMPANY_NAME + "/" + SaveLocation
                strPath = strPath + SaveLocation
                ''Dim objAccess As System.Security.AccessControl.DirectorySecurity
                ''objAccess.AccessRightType = Acc System.Security.AccessControl.AccessControlType.Allow

                If Not System.IO.Directory.Exists(strPath) Then
                    System.IO.Directory.CreateDirectory(strPath)
                End If

                'Dim BaseFolder As String = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
                'SaveLocation = Server.MapPath(strFolderName) & "\" & fn

                strFullPath = SaveLocation + (((FileName).TrimStart()).TrimEnd())
                If log.IsDebugEnabled Then
                    log.Debug("Uploading file .... " & strFullPath)
                End If
                fileUpload.PostedFile.SaveAs(strPath + FileName)
                HiddenField1.Value = strFullPath
                Session("SaveLocation") = strFullPath


                ''***********************For OCR DATA********************************

                Dim strText As String = ""
                Dim strFileType = (FileName.ToString().Split(".").GetValue(FileName.ToString().Split(".").Length - 1).ToString()).ToUpper()

                'If strFileType = "TIF" Or strFileType = "PNG" Or strFileType = "JPG" Or strFileType = "GIF" Or strFileType = "BMP" Then
                '    ' Instantiate the MODI.Document object 
                '    Dim md As New MODI.Document()
                '    Dim strOCRPath As String = Server.MapPath(strBaseUploadFolder + SaveLocation + FileName)
                '    Try
                '        ' The Create method grabs the picture from 
                '        ' disk snd prepares for OCR. 
                '        md.Create(strPath + FileName)
                '        ' Do the OCR. 
                '        md.OCR(MODI.MiLANGUAGES.miLANG_ENGLISH, True, True)

                '        ' This string will contain the text. 
                '        strText = String.Empty

                '        ' Get the first (and only image) 
                '        Dim image As MODI.Image = DirectCast(md.Images(0), MODI.Image)
                '        ' Get the layout. 
                '        Dim layout As MODI.Layout = image.Layout
                '        For j As Integer = 0 To layout.Words.Count - 1
                '            ' Loop through the words. 
                '            ' Get this word. 
                '            Dim word As MODI.Word = DirectCast(layout.Words(j), MODI.Word)
                '            ' Add a blank space to separate words. 
                '            If strText.Length > 0 Then
                '                strText += " "
                '            End If
                '            ' Add the word. 
                '            strText += word.Text
                '        Next
                '        ' Close the MODI.Document object.
                '        md.Close(False)

                '    Catch ex As Exception
                '        md.Close(False)
                '        md = Nothing
                '    End Try

                'Else
                '    strText = Nothing
                'End If

                ''***********************For OCR DATA********************************

                ''Dim strOCRDATA As String = OCRData(strPath + FileName)

                objBusinessNodeTags.FILENAME = (((FileName).TrimStart()).TrimEnd()) 'added by shailesh 09feb2010
                objBusinessNodeTags.FILEPATH = SaveLocation 'strFullPath
                objBusinessNodeTags.OCRDATA = strText
                objBusinessNodeTags.STATUS = True
                objBusinessNodeTags.OperationType = "INSERT"
                objBusinessNodeTags.SaveNodeTags()

                Dim ImgID As String = objBusinessNodeTags.IMAGEID
                If ImgID <> "-1" Then
                    Session("IMAGEID") = objBusinessNodeTags.IMAGEID
                    FetchDataFromControls()
                    Dim builder As New StringBuilder
                    builder.Append("<table><tr><td><b>Comapny Name - </b>" + objBillCompany.SZ_COMPANY_NAME + "</td><td></td></tr>")
                    builder.Append("<tr><td><b>Case No - </b>" + CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject).SZ_CASE_NO + "</td><td></td>")
                    builder.Append("<tr><td><b>Patient Name - </b> " + lblPatientName.Text + "</td><td></td></tr>")
                    builder.Append("<tr><td><b> File Name - </b>  " + objBusinessNodeTags.FILENAME + "</td><td></td></tr>")
                    builder.Append("<tr><td><b> File has been uploaded on node <b>" + str1 + "</td><td></td></tr>")
                    builder.Append("<tr><td colspan=2>=======================================================================</td></tr>")
                    builder.Append("<tr><td colspan=2>" + ConfigurationManager.AppSettings("Footer").ToString() + "</td></tr>")
                    sendMail(builder.ToString(), objBillCompany.SZ_COMPANY_ID.ToString().Trim(), nodeid)
                    'Dim body As String = "<b>Comapny Name - </b> " + objBillCompany.SZ_COMPANY_NAME + "<br><b>Patient Name - </b> " + lblPatientName.Text + "<br> <b> File Name - </b>  " + objBusinessNodeTags.FILENAME + "<br><b> File has been uploaded on node <b>" + str1 + "</b><br><br><br>This mail has been generated as system"
                    ' sendMail(body, objBillCompany.SZ_COMPANY_ID.ToString().Trim(), nodeid)

                End If


                ''Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "sandeep", "<script type='text/javascript'>alert('File successfully uploaded');window.location.href='" & System.Configuration.ConfigurationManager.AppSettings("UploadFileURI").ToString() & "'</script>")
                Page.RegisterStartupScript("TestString", "<script language='javascript'> var sURL = unescape(window.location.pathname);alert('File successfully uploaded');window.location.href = sURL; </script>")
                'Response.Redirect(Request.Url.AbsoluteUri.ToString() & "&Upload=true")
            Else
                ShowMessage.MessageBox.ShowMessage("Please select a file to upload.")
            End If
            ''Session("blnLodeFile") = "YES"
        Catch ex As Exception
            ' Close the MODI.Document object.
            log.Debug(ex.ToString)
            ShowMessage.MessageBox.ShowMessage(ex.Message)
        End Try
    End Sub



#End Region

#Region "sending mail"
    Public Sub sendMail(ByVal msg As String, ByVal companyid As String, ByVal nodeid As String)
        Try
            Dim from As String = ConfigurationManager.AppSettings("From").ToString()
            'Replace this with your own correct Gmail Address
            Dim [to] As String = GetMailID(companyid, nodeid)
            If (Not [to] Is Nothing) Then
                Dim mail As New System.Net.Mail.MailMessage()
                Dim cc As String = ConfigurationManager.AppSettings("cc").ToString()
                If (Not cc.Equals("")) Then
                    mail.CC.Add(cc)
                End If
                'Replace this with the Email Address to whom you want to send the mail

                mail.[To].Add([to])

                Dim subject As String = ConfigurationManager.AppSettings("Subject").ToString()
                Dim logo As String = ConfigurationManager.AppSettings("Logo").ToString()
                mail.From = New MailAddress(from, logo, System.Text.Encoding.UTF8)
                mail.Subject = subject
                mail.SubjectEncoding = System.Text.Encoding.UTF8
                mail.Body = msg
                mail.BodyEncoding = System.Text.Encoding.UTF8
                mail.IsBodyHtml = True

                mail.Priority = MailPriority.High

                Dim client As New SmtpClient()
                'Add the Creddentials- use your own email id and password
                Dim pwd As String = ConfigurationManager.AppSettings("Password").ToString()
                client.Credentials = New System.Net.NetworkCredential(from, pwd)
                '  client.Port = CInt(ConfigurationManager.AppSettings("Port").ToString())
                client.Port = Convert.ToInt16(ConfigurationManager.AppSettings("Port").ToString())


                ' Gmail works on this port
                client.Host = ConfigurationManager.AppSettings("Host").ToString()


                client.EnableSsl = True
                'Gmail works on Server Secured Layer
                Try
                    client.Send(mail)
                Catch ex As Exception
                    '  Dim ex2 As Exception = ex
                    '  Dim errorMessage As String = String.Empty
                    '  While ex2 IsNot Nothing
                    'errorMessage += ex2.ToString()
                    'ex2 = ex2.InnerException
                    'End While
                    'HttpContext.Current.Response.Write(errorMessage)
                    Throw ex
                End Try

            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub





    Public Function GetMailID(ByVal sz_Company_id As String, ByVal nodeid As String) As String
        Dim str As String
        Dim sqlcon As SqlConnection = New SqlConnection(ConfigurationManager.AppSettings("Connection_String").ToString())
        Try

            Dim sqlcmd As New SqlCommand("Get_upload_doc_Email", sqlcon)
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcon.Open()
            sqlcmd.Parameters.AddWithValue("@sz_company_id", sz_Company_id)
            sqlcmd.Parameters.AddWithValue("@sz_node_id", nodeid)
            Dim caseid As String
            caseid = Session("Case_ID").ToString()
            sqlcmd.Parameters.AddWithValue("@sz_Case_id", caseid)
            str = DirectCast(sqlcmd.ExecuteScalar(), String)
        Catch ex As Exception
            Throw ex
        Finally
            If sqlcon.State = ConnectionState.Open Then
                sqlcon.Close()
            End If
        End Try
        Return str

    End Function
#End Region

#Region "For Printing Document"

    Protected Sub printButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles printButton.Click

        Dim MyProcess As Process

        Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()

        If selectedID.Value.Split("-").Length > 1 Then
            MyProcess = New Process
            Dim strFilePath As String = objBusinessNodeTags.FetchImageFilePath(selectedID.Value)
            Dim strFileType = (strFilePath.ToString().Split(".").GetValue(strFilePath.ToString().Split(".").Length - 1).ToString()).ToUpper()

            If strFileType = "PDF" Or strFileType = "DOC" Or strFileType = "TXT" Or strFileType = "XLS" Or strFileType = "DOCX" Or strFileType = "XLSX" Or strFileType = "TIF" Or strFileType = "PNG" Or strFileType = "JPG" Or strFileType = "GIF" Or strFileType = "BMP" Then
                Try
                    MyProcess.StartInfo.CreateNoWindow = False
                    MyProcess.StartInfo.Verb = "print"
                    MyProcess.StartInfo.FileName = Server.MapPath(strFilePath)
                    MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                    MyProcess.Start()
                    ''KillUnStrProcess(MyProcess.ProcessName)
                    MyProcess.Close()
                    MyProcess = Nothing

                Catch ex As SqlException
                    Response.Write(ex.Message)
                End Try
            Else
                GeneralTools.MessageBox.ShowMessage("The selected file type is not valid. You can print only .DOC, PDF, TXT & .XLS files")
            End If
        Else
            Dim strFilePath As String = objBusinessNodeTags.FetchNodeRootPath(Convert.ToInt32(selectedID.Value))
            Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
            strFilePath = strBaseUploadFolder + strFilePath
            strFilePath = strFilePath.Replace("\", "/")
            GetFilesInDirectory(Server.MapPath(strFilePath))
        End If

    End Sub
#End Region

#Region "Get Files In Directory AND Kill Open Process for Unstructured"

    Public Sub KillUnStrProcess(ByVal processName As String)
        Dim myproc As New System.Diagnostics.Process()
        ''Get all instances of proc that are open, attempt to close them.
        Try
            For Each thisproc As Process In Process.GetProcessesByName(processName)
                If Not thisproc.CloseMainWindow() Then
                    thisproc.Kill()
                End If
            Next
        Catch ex As SqlException
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub GetFilesInDirectory(ByVal sourcePath As String)
        Dim sourceDir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(sourcePath)
        'The source directory must exist for our code to run
        If (sourceDir.Exists) Then

            'loop through all the files of the current directory
            'and copy them if overwrite=true or they do not exist
            Dim file As System.IO.FileInfo
            Dim MyProcess As Process
            For Each file In sourceDir.GetFiles()
                ''file.CopyTo(System.IO.Path.Combine(sourceDir.FullName, file.Name), True)
                MyProcess = New Process

                Dim strFileType As String = (file.Name.ToString().Split(".").GetValue(file.Name.ToString().Split(".").Length - 1).ToString()).ToUpper()

                If strFileType = "PDF" Or strFileType = "DOC" Or strFileType = "TXT" Or strFileType = "XLS" Or strFileType = "DOCX" Or strFileType = "XLSX" Or strFileType = "TIF" Or strFileType = "PNG" Or strFileType = "JPG" Or strFileType = "GIF" Or strFileType = "BMP" Then
                    Try
                        MyProcess.StartInfo.CreateNoWindow = False
                        MyProcess.StartInfo.Verb = "print"
                        MyProcess.StartInfo.FileName = sourceDir.FullName & file.Name
                        MyProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
                        MyProcess.Start()
                        ''KillUnStrProcess(MyProcess.ProcessName)
                        MyProcess.Close()
                        MyProcess = Nothing

                    Catch ex As SqlException
                        Response.Write(ex.Message)
                    End Try
                Else
                    GeneralTools.MessageBox.ShowMessage("The selected file type is not valid. You can print only .DOC, PDF, TXT & .XLS files")
                End If
            Next
            'loop through all the subfolders and call this method recursively
            Dim dir As System.IO.DirectoryInfo
            For Each dir In sourceDir.GetDirectories()
                GetFilesInDirectory(dir.FullName + "\")
            Next

        Else

        End If
    End Sub
#End Region

#Region "Function for Fetch File Path"

    'Private Function FetchFilePath() As String
    '    ''Dim myConnection As New SqlConnection("Data Source=172.18.3.227;Initial Catalog=DocMgr;User ID=sa;Password=sa123")
    '    ''Dim myConnection As New SqlConnection(_ConnectionString)
    '    ''Dim intNodeID As Integer = Convert.ToInt32(selectedID.Value)
    '    Try
    '        Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()
    '        Dim strFilePath As String = Server.MapPath(objBusinessNodeTags.FetchImageFilePath(selectedID.Value))
    '        'myConnection.Open()
    '        'Dim strQuery As String = "SELECT MAX(NodeID) FROM tblTAGS"
    '        'Dim myCommand As New SqlCommand("STP_DSP_IMAGEFILEPATH", myConnection)
    '        'myCommand.CommandType = CommandType.StoredProcedure


    '        'myCommand.Parameters.Add("@TAGID", SqlDbType.Int).Value = intNodeID
    '        'myCommand.Parameters.Add("@IMAGEID", SqlDbType.Int)
    '        'myCommand.Parameters.Add("@FILEPATH", SqlDbType.NVarChar, 255)

    '        'myCommand.Parameters("@IMAGEID").Direction = ParameterDirection.Output
    '        'myCommand.Parameters("@FILEPATH").Direction = ParameterDirection.Output
    '        'myCommand.ExecuteNonQuery()

    '        'Return myCommand.Parameters("@FILEPATH").Value

    '    Catch ex As SqlException
    '        Response.Write(ex.Message)
    '    End Try

    '    ''Return True
    'End Function
#End Region

#Region "The server side method that will handle the OnAddNode event"

    Public Function OnAddNode(ByVal parentId As String, ByVal childId As String, ByVal textOrHTML As String, ByVal expanded As String, ByVal image As String, ByVal subTreeURL As String, ByVal CaseID As String) As String
        Dim sResult As String = 1
        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)
        Dim NodeName As String = ((textOrHTML.TrimStart()).TrimEnd()) 'code altered by shailesh 09feb2010
        Try
            '    If textOrHTML = "FILE" Then
            '        textOrHTML = "<a href='" + Session("SaveLocation").ToString() + "' target='_blank'>" + Session("SaveLocation").ToString().Split("/").GetValue(Session("SaveLocation").ToString().Split("/").Length - 1) + "</a>"
            '    End If
            '    ' specify the type of action that the Tree_DB component should take:
            '    oTreeDB.EventType = "Add"
            '    ' prepare tha data for the Tree_DB object - the data should be sent using this format:
            '    oTreeDB.EventData = parentId + "|" + childId + "|" + NodeName + "|" + image + "|" + expanded 'code altered by shailesh 09feb2010

            '    oTreeDB.UseAdditionalData = True
            '    oTreeDB.AdditionalFields = "CaseID"
            '    oTreeDB.AdditionalFieldTypes = "text"
            '    oTreeDB.AdditionalFieldDatas = CaseID

            '    ' process the information and returning the result:

            '    '  sResult = oTreeDB.Process()

            '    If textOrHTML = "FILE" Then FetchDataFromControls()

            '    'If (sResult <> "1") Then
            '    '    Throw New Exception(sResult)

            '    ' Start : Update CaseType of newly added Entry From tblTags.

            '    Dim myDelConn As New SqlClient.SqlConnection(Common.SystemConfiguration.ConnectionString)
            '    log.Debug(myDelConn)
            '    myDelConn.Open()
            '    log.Debug("OnAddNode-Connection open.")

            '    Dim szQuery As String = ""
            '    szQuery = " Update tblTags set SZ_COMPANY_ID='" & objBillCompany.SZ_COMPANY_ID & "' where NodeID='" & sResult & "'"
            '    Dim sqlDelete As New SqlClient.SqlCommand(szQuery, myDelConn)
            '    sqlDelete.ExecuteNonQuery()
            '    log.Debug("OnAddNode-Connection Close")
            '    myDelConn.Close()
            '    log.Debug("OnAddNode-Connection Close")
            '    ' End : Update CaseType of newly added Entry From tblTags.

            '    'End Ifw

            Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
            conConnection.Open()
            Dim msg As String
            msg = ""
            Dim cmdCommand As SqlCommand = New SqlCommand("proc_add_new_node", conConnection)

            cmdCommand.CommandType = CommandType.StoredProcedure
            cmdCommand.Parameters.AddWithValue("@CompanyID", objBillCompany.SZ_COMPANY_ID)
            cmdCommand.Parameters.AddWithValue("@NodeID", parentId)
            cmdCommand.Parameters.AddWithValue("@CaseID", CaseID)
            cmdCommand.Parameters.AddWithValue("@newnodename", NodeName)
            Dim oReader As SqlDataReader
            oReader = cmdCommand.ExecuteReader()


            If (oReader.Read()) Then
                msg = oReader.Item(0)
                oReader.Close()

            End If
            Session("DeletAddNode") = msg
            LogActivity("NODE_ADDED", "Node " + NodeName + " added to parent id " + parentId + "")


            Return 2

            Session("blnLodeFile") = "YES"
            Me.Response.AppendHeader("Refresh", 0)
            '    log.Debug("OnAddNode-Method completed")





        Catch ex As Exception
            log.Debug(ex.ToString())
            Return sResult
        End Try
    End Function

#End Region

#Region "The server side method that will handle the OnNodeEdit event"

    Public Function OnNodeEdit(ByVal id As String, ByVal text As String, ByVal prevText As String) As String
        ' specify the type of action that the Tree_DB component should take:
        oTreeDB.EventType = "Edit"
        ' prepare tha data for the Tree_DB object - the data should be sent using this format:
        oTreeDB.EventData = id + "|" + text
        ' process the information and returning the result:
        Dim sResult As String = oTreeDB.Process()
        If (sResult <> "1") Then
            Throw New Exception(sResult)
        End If
        Return sResult
    End Function
#End Region

#Region "Delete Physical copy of folder"

    Private Function DeletePhysicalFolder(ByVal p_szNodeID As String) As Boolean

        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)

        Dim szBasePath As String = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocationPhysical")


        ' commented by shailesh 30jan2010
        'szBasePath = szBasePath & objBillCompany.SZ_COMPANY_NAME.ToString() & "/" & getPhysicalPathOfFolder(p_szNodeID)
        szBasePath = szBasePath & getPhysicalPathOfFolder(p_szNodeID, objBillCompany.SZ_COMPANY_ID.ToString())
        szBasePath = Replace(szBasePath, "/", "\")

        If (log.IsDebugEnabled) Then
            log.Debug("[DELETE FOLDER] [" & Session("DM_User_Name") & "] Deleting folder at : " & szBasePath)
        End If

        Try
            If System.IO.Directory.Exists(szBasePath) Then
                '  
                'removeDirectory(szBasePath)
                Dim p As System.Reflection.PropertyInfo
                Dim o As Object
                Dim m As System.Reflection.MethodInfo
                p = GetType(System.Web.HttpRuntime).GetProperty("FileChangesMonitor", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Public Or System.Reflection.BindingFlags.Static)
                o = p.GetValue(Nothing, Nothing)
                m = o.GetType().GetMethod("Stop", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
                m.Invoke(o, New Object() {})

                'added by shailesh 30jan2010
                If System.IO.Directory.Exists(szBasePath) Then
                    System.IO.Directory.Move(szBasePath, szBasePath + ".deleted")
                End If
                'System.IO.Directory.Delete(szBasePath, True)
                If log.IsDebugEnabled Then
                    log.Debug("[DELETE NODE] [" & Session("DM_User_Name") & "] File at " & szBasePath & " deleted successfully")
                End If
            End If

        Catch ex As Exception
            If log.IsDebugEnabled Then
                log.Debug("[DELETE FOLDER]  - D2 ")
                log.Debug(ex)
            End If
        End Try
        Return True
    End Function

    Private Function DeletePhysicalFile(ByVal p_szNodeID As String) As Boolean
        Dim szBasePath As String = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocationPhysical")
        Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
        Dim cmdCommand As SqlCommand = New SqlCommand()
        conConnection.Open()
        Dim szQuery As String = "select filepath + [filename] from tbldocimages where imageid = " & p_szNodeID

        cmdCommand.CommandText = szQuery
        cmdCommand.CommandType = CommandType.Text
        cmdCommand.Connection = conConnection

        Dim oReader As SqlDataReader
        oReader = cmdCommand.ExecuteReader()


        If (oReader.Read()) Then
            szBasePath = szBasePath & oReader.Item(0)
            szBasePath = Replace(szBasePath, "/", "\")
            oReader.Close()

        End If

        If log.IsDebugEnabled Then
            log.Debug("[DELETE NODE] [" & Session("DM_User_Name") & "] Deleting file at : " & szBasePath)
        End If

        Try
            Dim cmdCommand2 As SqlCommand = New SqlCommand()

            Dim szQuery2 As String = "SELECT COUNT(*) FROM tbldocimages where Filename=(select filename from tbldocimages where imageid=" & p_szNodeID & ") and FilePath=(select filepath from tbldocimages where imageid=" & p_szNodeID & ") and isnull(BT_HIDE,0)=0"
            cmdCommand2.CommandText = szQuery2
            cmdCommand2.CommandType = CommandType.Text
            cmdCommand2.Connection = conConnection

            ' Dim oReader2 As SqlDataReader
            Dim oReader2 As Integer = cmdCommand2.ExecuteScalar()
            '    oReader2.Read()
            If oReader2 > 1 Then

            Else
                'added by shailesh 30jan2010
                If System.IO.File.Exists(szBasePath) Then
                    System.IO.File.Move(szBasePath, szBasePath + ".deleted")
                End If
                'System.IO.File.Delete(szBasePath)
            End If
            If log.IsDebugEnabled Then
                log.Debug("[DELETE NODE] [" & Session("DM_User_Name") & "] File at " & szBasePath & " deleted successfully")
            End If
        Catch ex As Exception
            If log.IsDebugEnabled Then
                log.Debug("[DELETE NODE]  - D1 ")
                log.Debug(ex)
            End If
        End Try
        Return True
    End Function

    Private Function getPhysicalPathOfFolder(ByVal p_NodeID As String, ByVal CompanyID As String) As String
        Try

            Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
            Dim cmdCommand As SqlCommand = New SqlCommand()
            conConnection.Open()
            Dim szQuery As String = "SP_WS_GET_FULL_PATH"
            Dim szPath As String = ""

            cmdCommand.CommandText = szQuery
            cmdCommand.CommandType = CommandType.StoredProcedure
            cmdCommand.Parameters.Add("@SZ_NODEID", SqlDbType.VarChar, 10).Value = p_NodeID
            cmdCommand.Parameters.Add("@SZ_COMPANY_ID", SqlDbType.VarChar, 10).Value = CompanyID
            cmdCommand.Connection = conConnection

            Dim oReader As SqlDataReader
            oReader = cmdCommand.ExecuteReader()

            If (oReader.Read()) Then
                szPath = oReader.Item(0)
            End If
            Return szPath
        Catch ex As Exception

        End Try
    End Function

#End Region

#Region " The server side method that will handle the OnRemoveNode event"
    Sub LogActivity(Title As String, Desc As String)
        Me._DAO_NOTES_EO = New DAO_NOTES_EO()
        Me._DAO_NOTES_EO.SZ_MESSAGE_TITLE = Title
        Me._DAO_NOTES_EO.SZ_ACTIVITY_DESC = Desc
        Me._DAO_NOTES_BO = New DAO_NOTES_BO()
        Me._DAO_NOTES_EO.SZ_USER_ID = CType(Me.Session("USER_OBJECT"), Bill_Sys_UserObject).SZ_USER_ID
        Me._DAO_NOTES_EO.SZ_CASE_ID = CType(Me.Session("CASE_OBJECT"), Bill_Sys_CaseObject).SZ_CASE_ID
        Me._DAO_NOTES_EO.SZ_COMPANY_ID = CType(Me.Session("BILLING_COMPANY_OBJECT"), Bill_Sys_BillingCompanyObject).SZ_COMPANY_ID
        Me._DAO_NOTES_BO.SaveActivityNotes(Me._DAO_NOTES_EO)
    End Sub
    Public Function OnRemoveNode(ByVal id As String) As String
        Try

            Dim sResult As String = 1
            If log.IsDebugEnabled Then
                log.Debug("[DELETE NODE] Node to be deleted : " & id)
            End If
            Dim sz_CompanyID As String = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject).SZ_COMPANY_ID.ToString()
            Dim sz_id As String = ""
            If id.Contains("-") = True Then
                sz_id = id.Split("-").GetValue(1)
            Else
                sz_id = id

            End If
            Dim sz_caseId As String = Session("SelectedID")

            Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()

            If id.Split("-").Length > 1 Then
                ' IF the node being deleted is an image/file node and not a folder
                DeletePhysicalFile(Convert.ToInt32(id.Split("-").GetValue(1)))
                ProcessNode(id.Split("-").GetValue(1).ToString(), sz_CompanyID, "F", sz_caseId)
                LogActivity("NODE_DELETED", "Node ID : " + id.Split("-").GetValue(1).ToString() + " deleted .")
                'commented by shailesh 30jan2010
                'objBusinessNodeTags.DeleteImageNode("F", Convert.ToInt32(id.Split("-").GetValue(1))) 
                'DelImgNode((id.Split("-").GetValue(1)))
            Else
                'commented by shailesh 30jan2010
                'objBusinessNodeTags.DeleteImageNode("N", Convert.ToInt32(id)) 
                DeletePhysicalFolder(id)
                ProcessNode(id.ToString(), sz_CompanyID, "D", sz_caseId)
                LogActivity("NODE_DELETED", "Node ID : " + id + " deleted .")

                ' specify the type of action that the Tree_DB component should take:
                'oTreeDB.EventType = "Remove" shailesh
                ' prepare tha data for the Tree_DB object - the data should be sent using this format:
                'oTreeDB.EventData = id shailesh
                ' process the information and returning the result:


                sResult = oTreeDB.Process()
                If (sResult <> "1") Then
                    'Throw New Exception(sResult)
                End If
            End If
            objBusinessNodeTags = Nothing

            ' Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "sandeep", "<script type='text/javascript'>alert('File successfully uploaded');window.location.href='" & System.Configuration.ConfigurationManager.AppSettings("UploadFileURI").ToString() & "'</script>")
            Return sResult
        Catch ex As Exception

        End Try
    End Function


#End Region

#Region "The server side method that will handle the OnNodeDrop event"
    Public Function OnNodeDrop(ByVal src As String, ByVal dst As String) As String
        ' specify the type of action that the Tree_DB component should take:
        'Try
        If src.StartsWith("IMG") Then
            CopyNodeFolders(dst, src)
            ''Page.RegisterStartupScript("PageRefresh", "<script language='javascript'> var sURL = unescape(window.location.pathname);alert('Moved Successfully');window.location.href = sURL; </script>")
            Return 1

        Else
            oTreeDB.EventType = "UpdateLevel"
            ' prepare tha data for the Tree_DB object - the data should be sent using this format:
            oTreeDB.EventData = dst + "," + src
            ' process the information and returning the result:
            CopyNodeFolders(dst, src)
            Dim sResult As String = oTreeDB.Process()

            If (sResult <> "1") Then

                Throw New Exception(sResult)
            End If

            Return sResult
        End If
        'Catch ex As Exception

        'Finally
        '    Page.RegisterStartupScript("PageRefresh", "<script language='javascript'> var sURL = unescape(window.location.pathname);alert('Moved Successfully');window.location.href = sURL; </script>")
        'End Try
    End Function
#End Region

#Region "Function for Save TagID and ImagesID"

    Private Sub FetchDataFromControls()

        Dim objBusinessNodeTags As New BusinessNodeTags()
        ViewState("TagID") = selectedID.Value

        Dim TagID As Integer = ViewState("TagID")
        Dim ImageID As Integer = Session("IMAGEID")

        objBusinessNodeTags.IMAGEID = ImageID
        objBusinessNodeTags.TAGID = TagID
        objBusinessNodeTags.LOGINID = Session("UserName")
        objBusinessNodeTags.OperationType = "INSERT"
        objBusinessNodeTags.SaveImagesTags()

    End Sub

#End Region

#Region "Function for Moving And Copy Nodes and Folders"

    Public Sub CopyNodeFolders(ByVal strdest As String, ByVal strsrc As String)
        Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()

        Try
            '' For Moving Single File from Source to Destination
            If strsrc.StartsWith("IMG") Then

                Dim strSrcPath As String = objBusinessNodeTags.FetchImageFilePath(strsrc)
                Dim strDestPath As String = objBusinessNodeTags.FetchNodeRootPath(Convert.ToInt32(strdest))

                Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation") ' + Session("SelectedCaseID").ToString() + "/"

                If System.IO.Directory.Exists(Server.MapPath(strBaseUploadFolder + strDestPath)) = False Then
                    System.IO.Directory.CreateDirectory(Server.MapPath(strBaseUploadFolder + strDestPath))
                End If

                strSrcPath = strSrcPath.Replace("\", "/")
                Dim arrFile As Array = strSrcPath.Split("/")

                System.IO.File.Move(Server.MapPath(strSrcPath), Server.MapPath(strBaseUploadFolder + strDestPath + arrFile.GetValue(arrFile.Length - 1)))

                objBusinessNodeTags.IMAGEID = strsrc.Split("-").GetValue(strsrc.Split("-").Length - 1)
                objBusinessNodeTags.FILENAME = arrFile.GetValue(arrFile.Length - 1)
                objBusinessNodeTags.FILEPATH = strDestPath
                objBusinessNodeTags.STATUS = True
                objBusinessNodeTags.OperationType = "UPDATE"
                objBusinessNodeTags.SaveNodeTags()

                objBusinessNodeTags.IMAGEID = strsrc.Split("-").GetValue(strsrc.Split("-").Length - 1)
                objBusinessNodeTags.TAGID = Convert.ToInt32(strdest)
                objBusinessNodeTags.LOGINID = Session("UserName")
                objBusinessNodeTags.OperationType = "UPDATE"
                objBusinessNodeTags.SaveImagesTags()

            Else

                Dim tbSrcPath As DataTable = GetRootPath(strsrc, strdest, Session("SelectedID").ToString())
                Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")

                For Each dr As DataRow In tbSrcPath.Rows

                    If dr(4).ToString() = "F" Then
                        If System.IO.Directory.Exists(Server.MapPath(strBaseUploadFolder & dr(3))) = False Then
                            System.IO.Directory.CreateDirectory(Server.MapPath(strBaseUploadFolder & dr(3)))
                        End If

                        System.IO.File.Move(Server.MapPath(dr(2).ToString + dr(0).ToString()), Server.MapPath(strBaseUploadFolder & dr(3).ToString + dr(0).ToString()))

                        objBusinessNodeTags.IMAGEID = dr(5)
                        objBusinessNodeTags.FILENAME = dr(0)
                        objBusinessNodeTags.FILEPATH = dr(3) 'strFullPath
                        'objBusinessNodeTags.OCRDATA = "null"
                        objBusinessNodeTags.STATUS = True
                        objBusinessNodeTags.OperationType = "UPDATE"
                        objBusinessNodeTags.SaveNodeTags()
                    End If
                Next
            End If
        Catch ex As SqlException
            Response.Write(ex.Message)

        End Try

    End Sub
#End Region

#Region "FillCaseDropDownList"

    Private Sub FillCaseDropDownList()
        Dim objBusinessCaseMaster As BusinessCaseMaster = New BusinessCaseMaster()
        ddlCaseID.DataSource = objBusinessCaseMaster.GetDataCaseDropDownList(Session("Archived").ToString())
        ddlCaseID.DataTextField = "CaseID"
        ddlCaseID.DataValueField = "ID"
        ddlCaseID.DataBind()
    End Sub

#End Region

#Region "btnPasteNode_Click"
    Protected Sub btnPasteNode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPasteNode.Click
        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)
        log.Debug("Start btnPasteNode_Click")
        Try
            ''Session("SelectedID") = 44126
            Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()

            If SourceID.Value.StartsWith("IMG") Then

                log.Debug("The node starts with IMG.")
                Dim pNID As Integer
                ''Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
                Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocationPhysical")
                log.Debug("Upload Folder" + strBaseUploadFolder)

                Dim strTargetRootPath As String = objBillCompany.SZ_COMPANY_NAME + "/" + objBusinessNodeTags.FetchNodeRootPath(Convert.ToInt32(selectedID.Value))
                log.Debug("TargetRootPath :" + strTargetRootPath)

                Dim strSourceRootPath As String = objBusinessNodeTags.FetchImageFilePath(SourceID.Value)
                log.Debug("")
                '' Dim strSourceRootPath As String = objBusinessNodeTags.FetchImageFilePath(Session("SourceFileID"))

                If System.IO.Directory.Exists(strBaseUploadFolder + strTargetRootPath) = False Then
                    System.IO.Directory.CreateDirectory(strBaseUploadFolder + strTargetRootPath)
                End If

                strSourceRootPath = strSourceRootPath.Replace("\", "/")
                Dim arrFile As Array = strSourceRootPath.Split("/")

                ''System.IO.File.Copy(Server.MapPath(strSourceRootPath), Server.MapPath(strBaseUploadFolder + strTargetRootPath + arrFile.GetValue(arrFile.Length - 1)))
                System.IO.File.Copy(strSourceRootPath, strBaseUploadFolder + strTargetRootPath + arrFile.GetValue(arrFile.Length - 1))

                CopyNodesData("F", Convert.ToInt32(selectedID.Value), strTargetRootPath, Convert.ToInt32(SourceID.Value.ToString().Split("-").GetValue(1)), 0, Session("UserName"), Session("SelectedID"), pNID)
                ''CopyNodesData("F", Convert.ToInt32(selectedID.Value), strTargetRootPath, Convert.ToInt32(Session("SourceFileID").ToString().Split("-").GetValue(1)), 0, Session("UserName"), pNID)

            Else
                Dim tbSrcPath As DataTable = GetRootPath(SourceID.Value, selectedID.Value, Session("SelectedID"))

                Dim dv As DataView = tbSrcPath.DefaultView

                tbSrcPath.Rows(0)("ParentID") = Convert.ToInt32(selectedID.Value)

                For i As Integer = 0 To tbSrcPath.Rows.Count - 1

                    Dim dr As DataRow = tbSrcPath.Rows(i)

                    If dr(4).ToString() = "F" Then
                        Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
                        strBaseUploadFolder = strBaseUploadFolder + objBillCompany.SZ_COMPANY_NAME + "/"
                        If System.IO.Directory.Exists(Server.MapPath(strBaseUploadFolder & dr(3))) = False Then
                            System.IO.Directory.CreateDirectory(Server.MapPath(strBaseUploadFolder & dr(3)))
                        End If

                        If System.IO.File.Exists(Server.MapPath(strBaseUploadFolder & dr(3).ToString + dr(0).ToString())) = False Then
                            If System.IO.File.Exists(Server.MapPath(dr(2).ToString + dr(0).ToString())) = True Then
                                System.IO.File.Copy(Server.MapPath(dr(2).ToString + dr(0).ToString()), Server.MapPath(strBaseUploadFolder & dr(3).ToString + dr(0).ToString()))
                                Dim pNID As Integer
                                CopyNodesData(dr("Type"), dr("ParentID"), dr("TargetPath"), dr("ImageID"), dr("TagID"), Session("UserName"), Session("SelectedID"), pNID)
                            End If
                        End If
                    Else
                        Dim pNodeID As Integer
                        CopyNodesData(dr("Type"), dr("ParentID"), dr("TargetPath"), dr("ImageID"), dr("TagID"), Session("UserName"), Session("SelectedID"), pNodeID)

                        dv = tbSrcPath.DefaultView
                        dv.RowFilter = "ParentID='" & dr("TagID") & "'"

                        Dim Count = dv.Count - 1

                        For x As Integer = Count To 0 Step -1
                            dv.Item(x)("ParentID") = pNodeID
                        Next
                        dv.RowFilter = ""

                        dv = tbSrcPath.DefaultView
                        dv.RowFilter = "TagID='" & dr("TagID") & "' and type='F'"

                        Count = dv.Count - 1
                        For y As Integer = Count To 0 Step -1
                            dv.Item(y)("ParentID") = pNodeID
                        Next
                        dv.RowFilter = ""
                    End If
                Next
            End If
        Catch ex As Exception
        Finally
            'Session("SourceFolderID") = Nothing
            'Session("SourceFileID") = Nothing
            'Session("NodeType") = Nothing
            Page.RegisterStartupScript("TestString", "<script language='javascript'> var sURL = unescape(window.location.pathname);alert('Node Pasted Successfully');window.location.href = sURL; </script>")
        End Try

    End Sub
#End Region

#Region "btnCopyToCase_ServerClick"

    Protected Sub btnCopyToCase_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyToCase.ServerClick

        Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)

        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)

        Try
            conConnection.Open()
            '  Dim strQuery As String = "SELECT C.SZ_CASE_ID, C.SZ_CASE_ID, ISNULL(T.NODEID,0) AS NODEID, ISNULL(T.NODENAME,'NA') AS NODENAME FROM MST_CASE_MASTER C LEFT JOIN TBLTAGS T ON T.CASEID = C.SZ_CASE_ID And T.PARENTID Is NULL  WHERE C.SZ_CASE_ID = '" & txtCopyToCase.Value & "' AND NODETYPE IS NULL"
            Dim strQuery As String = "SP_CHECK_COPY_TO_CASE"

            Dim cmdCommand As New SqlCommand(strQuery, conConnection)
            cmdCommand.CommandType = CommandType.StoredProcedure
            cmdCommand.Parameters.AddWithValue("@SZ_DESTINATION_CASE_ID", txtCopyToCase.Value)
            cmdCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillCompany.SZ_COMPANY_ID)
            Dim drNode As SqlDataReader = cmdCommand.ExecuteReader()

            If log.IsDebugEnabled Then
                log.Debug("[COPY TO CASE] Source ID : " & Session("Case_ID"))
                log.Debug("[COPY TO CASE] Target ID : " & txtCopyToCase.Value)
            End If

            Dim intID As String
            Dim intNodeID As Integer
            While drNode.Read()
                intID = drNode.Item(0)
                If log.IsDebugEnabled Then
                    log.Debug("[COPY TO CASE] Copying to case with ID  : " & intID)
                End If
                intNodeID = CInt(drNode.Item(2))
                If log.IsDebugEnabled Then
                    log.Debug("[COPY TO CASE] Target Node ID in case -- " & intID & " : " & intNodeID)
                End If
            End While
            drNode.Close()

            If intNodeID = 0 Then
                lblMessage.Text = "Not Copy"
                Page.RegisterStartupScript("TestString", "<script language='javascript'> var sURL = unescape(window.location.pathname); alert('Case are not exist in DataBase table'); window.location.href = sURL; </script>")
                Return
                'Exit Sub
            End If

            Dim tbSrcPath As DataTable = GetRootPath(SourceID.Value, intNodeID, CStr(intID))
            Dim dv As DataView = tbSrcPath.DefaultView

            tbSrcPath.Rows(0)("ParentID") = Convert.ToInt32(intNodeID)

            For i As Integer = 0 To tbSrcPath.Rows.Count - 1

                Dim dr As DataRow = tbSrcPath.Rows(i)

                If dr(4).ToString() = "F" Then
                    Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
                    strBaseUploadFolder = strBaseUploadFolder + objBillCompany.SZ_COMPANY_NAME + "/"
                    If System.IO.Directory.Exists(Server.MapPath(strBaseUploadFolder & dr(3))) = False Then
                        If (log.IsDebugEnabled) Then
                            log.Debug("[COPY TO CASE] Directory does not exists.")
                        End If
                        System.IO.Directory.CreateDirectory(Server.MapPath(strBaseUploadFolder & dr(3)))
                        If (log.IsDebugEnabled) Then
                            log.Debug("[COPY TO CASE] Created directory : " & Server.MapPath(strBaseUploadFolder & dr(3)))
                        End If
                    Else
                        If (log.IsDebugEnabled) Then
                            log.Debug("[COPY TO CASE] Directory already exists " & Server.MapPath(strBaseUploadFolder & dr(3)))
                        End If
                    End If

                    Try
                        If System.IO.File.Exists(Server.MapPath(strBaseUploadFolder & dr(3).ToString + dr(0).ToString())) = False Then
                            If System.IO.File.Exists(Server.MapPath(dr(2).ToString + dr(0).ToString())) = True Then
                                Try
                                    System.IO.File.Copy(Server.MapPath(dr(2).ToString + dr(0).ToString()), Server.MapPath(strBaseUploadFolder & dr(3).ToString + dr(0).ToString()))
                                    Dim pNID As Integer
                                    CopyNodesData(dr("Type"), dr("ParentID"), dr("TargetPath"), dr("ImageID"), dr("TagID"), Session("UserName"), intID, pNID)
                                Catch ex As Exception
                                    If (log.IsDebugEnabled) Then
                                        log.Debug("[COPY TO CASE] p1")
                                        log.Debug(ex.StackTrace)
                                    End If
                                End Try
                            End If
                        End If
                    Catch ex As Exception
                        If (log.IsDebugEnabled) Then
                            log.Debug("[COPY TO CASE] p2")
                            log.Debug(ex.StackTrace)
                        End If
                    End Try
                Else
                    Dim pNodeID As Integer
                    CopyNodesData(dr("Type"), dr("ParentID"), dr("TargetPath"), dr("ImageID"), dr("TagID"), Session("UserName"), intID, pNodeID)

                    dv = tbSrcPath.DefaultView
                    dv.RowFilter = "ParentID='" & dr("TagID") & "'"

                    Dim Count = dv.Count - 1

                    For x As Integer = Count To 0 Step -1
                        dv.Item(x)("ParentID") = pNodeID
                    Next
                    dv.RowFilter = ""

                    dv = tbSrcPath.DefaultView
                    dv.RowFilter = "TagID='" & dr("TagID") & "' and type='F'"

                    Count = dv.Count - 1
                    For y As Integer = Count To 0 Step -1
                        dv.Item(y)("ParentID") = pNodeID
                    Next
                    dv.RowFilter = ""
                End If
            Next
        Catch ex As Exception
            If (log.IsDebugEnabled) Then
                log.Debug("[COPY TO CASE] p3")
                log.Debug(ex.StackTrace)
            End If
        Finally
            'Session("SourceFolderID") = Nothing
            'Session("SourceFileID") = Nothing
            'Session("NodeType") = Nothing
            Dim szURL As String = Session("LastAction")
            'unescape(window.location.pathname)
            'Page.RegisterStartupScript("TestString", "<script language='javascript'> var sURL = '" & szURL & "';alert('Node Moved Successfully');window.location.href = sURL; </script>")
            Page.RegisterStartupScript("TestString", "<script language='javascript'> var sURL = '" & szURL & "';window.location.href = sURL; </script>")
        End Try
    End Sub
#End Region

#Region "CopyFolderFile"
    Private Sub CopyFolderFile(ByVal SourceFolder As String, ByVal DestinationFolder As String)
        SourceFolder = SourceFolder.TrimEnd("\"c)
        DestinationFolder = DestinationFolder.TrimEnd("\"c)

        Try

            If Not System.IO.Directory.Exists(DestinationFolder) Then
                System.IO.Directory.CreateDirectory(DestinationFolder)
            End If

            Dim oSource As New System.IO.DirectoryInfo(SourceFolder)
            For Each oDirectory As System.IO.DirectoryInfo In oSource.GetDirectories()
                System.IO.Directory.CreateDirectory(DestinationFolder + "\" + oDirectory.Name)
                CopyFolderFile(oDirectory.FullName, DestinationFolder + "\" + oDirectory.Name)
            Next

            For Each oFile As System.IO.FileInfo In oSource.GetFiles()
                System.IO.File.Copy(oFile.FullName, DestinationFolder + "\" + oFile.Name, True)
            Next

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
#End Region

#Region "CopyNodesData"

    Private Sub CopyNodesData(ByVal pNodeType As Char, ByVal pParentID As Integer, ByVal pFilePath As String, ByVal pImageID As Integer, ByVal pTagID As Integer, ByVal pLoginID As String, ByVal pCaseID As String, ByRef pNodeID As Integer) ', ByRef pNodeLevel As Integer)

        Dim objBillCompany As New Bill_Sys_BillingCompanyObject
        objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)

        If pLoginID Is Nothing Then
            pLoginID = ""
        End If

        Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
        Dim cmdCommand As SqlCommand = New SqlCommand()
        Try

            conConnection.Open()
            cmdCommand.CommandText = "STP_InsertCaseandImagesWhileCopying"
            cmdCommand.CommandType = CommandType.StoredProcedure
            cmdCommand.Parameters.Add("@NODETYPE", SqlDbType.Char).Value = pNodeType
            cmdCommand.Parameters.Add("@PARENTID", SqlDbType.Int).Value = pParentID
            If (pFilePath.Contains(objBillCompany.SZ_COMPANY_NAME)) Then
                cmdCommand.Parameters.Add("@FILEPATH", SqlDbType.NVarChar, 255).Value = pFilePath '//objBillCompany.SZ_COMPANY_NAME + "/" + pFilePath
            Else
                cmdCommand.Parameters.Add("@FILEPATH", SqlDbType.NVarChar, 255).Value = objBillCompany.SZ_COMPANY_NAME + "/" + pFilePath
            End If

            cmdCommand.Parameters.Add("@IMAGEID", SqlDbType.Int).Value = pImageID
            cmdCommand.Parameters.Add("@TAGID", SqlDbType.Int).Value = pTagID
            cmdCommand.Parameters.Add("@LOGINID", SqlDbType.NVarChar, 255).Value = pLoginID
            cmdCommand.Parameters.Add("@CASEID", SqlDbType.NVarChar, 255).Value = pCaseID
            cmdCommand.Parameters.Add("@SZ_COMPANY_ID", SqlDbType.NChar).Value = objBillCompany.SZ_COMPANY_ID
            cmdCommand.Parameters.Add("@NODEID", SqlDbType.Int, 100).Value = pNodeID
            'cmdCommand.Parameters.Add("@NODELEVEL", SqlDbType.Int, 100).Value = pNodeLevel
            cmdCommand.Parameters("@NODEID").Direction = ParameterDirection.InputOutput
            'cmdCommand.Parameters("@NODELEVEL").Direction = ParameterDirection.InputOutput
            cmdCommand.Connection = conConnection

            cmdCommand.ExecuteNonQuery()
            pNodeID = cmdCommand.Parameters("@NODEID").Value
            'pNodeLevel = cmdCommand.Parameters("@NODELEVEL").Value
        Catch ex As Exception
            If (log.IsDebugEnabled) Then
                log.Debug(ex.Message)
            End If
        Finally
            If conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
            conConnection = Nothing
            cmdCommand = Nothing
        End Try
    End Sub

#End Region

#Region "GetRootPathxx"
    'Private Function GetRootPath(ByVal pSourceNodeID As Integer, ByVal pTargetNodeID As Integer, ByVal pCaseID As String) As DataTable

    '    Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
    '    Dim cmdCommand As SqlCommand = New SqlCommand()
    '    Try

    '        conConnection.Open()
    '        cmdCommand.CommandText = "STP_GETROOTFOLDERTOCOPY"
    '        cmdCommand.CommandType = CommandType.StoredProcedure
    '        cmdCommand.Parameters.Add("@NODEID", SqlDbType.Int, 100).Value = pSourceNodeID
    '        cmdCommand.Parameters.Add("@CASEID", SqlDbType.VarChar, 255).Value = pCaseID
    '        cmdCommand.Connection = conConnection

    '        Dim dt As New DataTable()
    '        Dim DA As New SqlDataAdapter(cmdCommand)
    '        DA.Fill(dt)

    '        Dim dtFinal As New DataTable()


    '        If dt.Rows.Count > 0 Then

    '            dtFinal.Columns.Add("FileName", System.Type.GetType("System.String"))
    '            dtFinal.Columns.Add("SourceRootPath", System.Type.GetType("System.String"))
    '            dtFinal.Columns.Add("SourcePath", System.Type.GetType("System.String"))
    '            dtFinal.Columns.Add("TargetPath", System.Type.GetType("System.String"))
    '            dtFinal.Columns.Add("Type", System.Type.GetType("System.String"))
    '            dtFinal.Columns.Add("ImageID", System.Type.GetType("System.Int32"))
    '            dtFinal.Columns.Add("TagID", System.Type.GetType("System.Int32"))
    '            dtFinal.Columns.Add("NodeName", System.Type.GetType("System.String"))
    '            dtFinal.Columns.Add("ParentID", System.Type.GetType("System.Int32"))

    '            Dim ParentNodeID As Integer = 0
    '            Dim SearchNode As Integer = pSourceNodeID

    '            Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()
    '            For Each dr As DataRow In dt.Rows
    '                If ParentNodeID = 0 Then ParentNodeID = dr("ParentID")

    '                Dim dr1 As DataRow = dtFinal.NewRow()
    '                Dim strTargetRootPath As String = objBusinessNodeTags.FetchNodeRootPath(pTargetNodeID)
    '                Dim strSourceRootPath As String = objBusinessNodeTags.FetchNodeRootPath(ParentNodeID)
    '                Dim strTargetPath As String = objBusinessNodeTags.FetchNodeRootPath(dr(0))
    '                Dim strSourcePath As String = objBusinessNodeTags.FetchNodeRootPath(Convert.ToInt32(dr(0)))
    '                Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
    '                strTargetPath = strBaseUploadFolder + strTargetPath

    '                If SearchNode = dr("ParentID") Or pSourceNodeID = dr("NodeID") Then
    '                    dr1("FileName") = ""
    '                    dr1("SourceRootPath") = ""
    '                    dr1("SourcePath") = strBaseUploadFolder + strSourcePath
    '                    dr1("TargetPath") = strTargetPath.Replace(strSourceRootPath, strTargetRootPath)
    '                    dr1("Type") = "D"
    '                    dr1("ImageID") = 0
    '                    dr1("TagID") = dr("NodeID")
    '                    dr1("NodeName") = dr("NodeName")
    '                    dr1("ParentID") = dr("ParentID")
    '                    dtFinal.Rows.Add(dr1)
    '                    AddChildDocuments(dtFinal, dr("NodeID"), pTargetNodeID, ParentNodeID)
    '                    If SearchNode = dr("ParentID") Then 'And SearchNode <> dr("NodeID") Then
    '                        SearchNode = dr("NodeID")
    '                    End If
    '                End If
    '            Next

    '        End If

    '        Return dtFinal

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If conConnection.State = ConnectionState.Open Then
    '            conConnection.Close()
    '        End If

    '        conConnection = Nothing
    '        cmdCommand = Nothing
    '    End Try
    'End Function
#End Region

#Region "GetRootPath"
    Private Function GetRootPath(ByVal pSourceNodeID As Integer, ByVal pTargetNodeID As Integer, ByVal pCaseID As String) As DataTable
        Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
        Dim cmdCommand As SqlCommand = New SqlCommand()
        Try
            Dim dt As New DataTable()
            Dim dtFinal As New DataTable()
            conConnection.Open()
            cmdCommand.CommandText = "Select * from tblTags Where NodeID=" & pSourceNodeID
            cmdCommand.CommandType = CommandType.Text
            cmdCommand.Connection = conConnection

            Dim DA As New SqlDataAdapter(cmdCommand)
            DA.Fill(dt)

            If dt.Rows.Count > 0 Then FillNodeHeirachy(pSourceNodeID, dt)

            If dt.Rows.Count > 0 Then

                dtFinal.Columns.Add("FileName", System.Type.GetType("System.String"))
                dtFinal.Columns.Add("SourceRootPath", System.Type.GetType("System.String"))
                dtFinal.Columns.Add("SourcePath", System.Type.GetType("System.String"))
                dtFinal.Columns.Add("TargetPath", System.Type.GetType("System.String"))
                dtFinal.Columns.Add("Type", System.Type.GetType("System.String"))
                dtFinal.Columns.Add("ImageID", System.Type.GetType("System.Int32"))
                dtFinal.Columns.Add("TagID", System.Type.GetType("System.Int32"))
                dtFinal.Columns.Add("NodeName", System.Type.GetType("System.String"))
                dtFinal.Columns.Add("ParentID", System.Type.GetType("System.Int32"))

                Dim ParentNodeID As Integer = 0

                Dim objBusinessNodeTags As BusinessNodeTags = New BusinessNodeTags()
                For Each dr As DataRow In dt.Rows
                    If ParentNodeID = 0 Then ParentNodeID = dr("ParentID")

                    Dim dr1 As DataRow = dtFinal.NewRow()
                    Dim strTargetRootPath As String = objBusinessNodeTags.FetchNodeRootPath(pTargetNodeID)
                    Dim strSourceRootPath As String = objBusinessNodeTags.FetchNodeRootPath(ParentNodeID)
                    Dim strTargetPath As String = objBusinessNodeTags.FetchNodeRootPath(dr(0))
                    Dim strSourcePath As String = objBusinessNodeTags.FetchNodeRootPath(Convert.ToInt32(dr(0)))
                    Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
                    strTargetPath = strBaseUploadFolder + strTargetPath
                    dr1("FileName") = ""
                    dr1("SourceRootPath") = ""
                    dr1("SourcePath") = strBaseUploadFolder + strSourcePath
                    'dr1("TargetPath") = strTargetPath.Replace(strSourceRootPath, strTargetRootPath)
                    dr1("TargetPath") = strSourcePath.Replace(strSourceRootPath, strTargetRootPath)
                    dr1("Type") = "D"
                    dr1("ImageID") = 0
                    dr1("TagID") = dr("NodeID")
                    dr1("NodeName") = dr("NodeName")
                    dr1("ParentID") = dr("ParentID")
                    dtFinal.Rows.Add(dr1)

                    AddChildDocuments(dtFinal, dr("NodeID"), pTargetNodeID, dr("ParentID"), dr1("TargetPath"))
                Next
            End If
            Return dtFinal
        Catch ex As Exception
            log.Debug("Inner :" & ex.StackTrace)
            log.Debug("Inner :" & ex.Message)
            ' Throw ex
        Finally
            If conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If
            conConnection = Nothing
            cmdCommand = Nothing
        End Try
    End Function
#End Region

#Region "FillNodeHeirachy"
    Private Sub FillNodeHeirachy(ByVal ParentNodeID As Integer, ByRef dt As DataTable)
        Dim flag As Boolean = False
        Dim dttmp As New DataTable()

        dttmp = GetChildNodeData(ParentNodeID)

        Dim totCount As Integer = dttmp.Rows.Count
        Dim CurrIndex As Integer = 0

        While CurrIndex < totCount
            Dim dr As DataRow = dttmp.Rows(CurrIndex)
            Dim dr1 As DataRow = dt.NewRow()
            dr1(0) = dr(0)
            dr1(1) = dr(1)
            dr1(2) = dr(2)
            dr1(3) = dr(3)
            dr1(4) = dr(4)
            dr1(5) = dr(5)
            dr1(6) = dr(6)
            dr1(7) = dr(7)
            dr1(8) = dr(8)
            dt.Rows.Add(dr1)
            Dim dtt As New DataTable

            dtt = GetChildNodeData(dr(0))

            For Each drr As DataRow In dtt.Rows
                Dim tr As DataRow = dttmp.NewRow()
                tr(0) = drr(0)
                tr(1) = drr(1)
                tr(2) = drr(2)
                tr(3) = drr(3)
                tr(4) = drr(4)
                tr(5) = drr(5)
                tr(6) = drr(6)
                tr(7) = drr(7)
                tr(8) = drr(8)
                dttmp.Rows.Add(tr)
                totCount = totCount + 1
            Next

            CurrIndex = CurrIndex + 1

        End While
    End Sub
#End Region

#Region "Add Chile Documents"

    Private Sub AddChildDocuments(ByRef pdtFinal As DataTable, ByVal pNodeID As Integer, ByVal pTargetNodeID As Integer, ByVal pSoruceNodeID As Integer, ByVal pTargetPath As String)
        Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
        Dim cmdCommand As SqlCommand = New SqlCommand()
        Try

            conConnection.Open()
            cmdCommand.CommandText = "STP_GETCHILDDOCSFORMOVEORCOPY"
            cmdCommand.CommandType = CommandType.StoredProcedure
            cmdCommand.Parameters.Add("@NODEID", SqlDbType.Int, 100).Value = pNodeID
            log.Debug("Got1 " & pNodeID & " " & pTargetNodeID & " " & pSoruceNodeID)
            cmdCommand.Parameters.Add("@TARGETNODEID", SqlDbType.Int, 100).Value = pTargetNodeID
            cmdCommand.Parameters.Add("@SOURCENODEID", SqlDbType.Int, 100).Value = pSoruceNodeID
            cmdCommand.Connection = conConnection

            Dim dt As New DataTable()
            Dim DA As New SqlDataAdapter(cmdCommand)
            DA.Fill(dt)

            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim dr1 As DataRow = pdtFinal.NewRow()
                    Dim strBaseUploadFolder = Common.SystemConfiguration.GetApplicationParameters("DocumentUploadLocation")
                    dr1("FileName") = dr(0)
                    dr1("SourceRootPath") = dr(1)
                    dr1("SourcePath") = dr(2)
                    dr1("TargetPath") = pTargetPath
                    dr1("Type") = "F"
                    dr1("ImageID") = dr("ImageID")
                    dr1("TagID") = dr("TagID")
                    dr1("ParentID") = pSoruceNodeID
                    pdtFinal.Rows.Add(dr1)
                Next
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If

            conConnection = Nothing
            cmdCommand = Nothing
        End Try
    End Sub
#End Region

#Region "GetChildNodeData"
    Private Function GetChildNodeData(ByVal pNodeID As Integer) As DataTable
        Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
        Dim cmdCommand As SqlCommand = New SqlCommand()
        Try

            conConnection.Open()
            cmdCommand.CommandText = "Select * from tblTags Where ParentID='" & pNodeID & "'"
            cmdCommand.CommandType = CommandType.Text
            cmdCommand.Connection = conConnection

            Dim DA As New SqlDataAdapter(cmdCommand)
            Dim dtt As New DataTable
            DA.Fill(dtt)

            Return dtt
        Catch ex As Exception
            Throw ex
        Finally
            If conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If

            conConnection = Nothing
            cmdCommand = Nothing
        End Try
    End Function
#End Region

#Region "Button click to change node list"
    Protected Sub btnLoadWithNodes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadWithNodes.Click
        Dim szURL As String
        Dim szSN As String = Session("SN") 'Request("SN")
        If szSN = "1" Then
            btnLoadWithNodes.Text = " ... Show nodes with documents ... "
            Session("SN") = "0"
        Else
            btnLoadWithNodes.Text = " ... Show all nodes ... "
            Session("SN") = "1"
        End If
        'szURL = "vb_CaseInformation.aspx?A=0&SN=" & szSN & "&Case_ID=" & Request.QueryString("Case_ID") & "&ID=" & Request.QueryString("ID") & "&User_Name=" & Request.QueryString("User_Name")
        szURL = "vb_CaseInformation.aspx"
        Session("LastAction") = szURL
        'Response.Redirect("vb_CaseInformation.aspx?A=0&SN=" & szSN & "&Case_ID=" & Request.QueryString("Case_ID") & "&ID=" & Request.QueryString("ID") & "&User_Name=" & Request.QueryString("User_Name"))
        Response.Redirect("vb_CaseInformation.aspx")
    End Sub
#End Region

    Protected Function getCaseInformation() As String
        'Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
        'Dim cmdCommand As SqlCommand = New SqlCommand()
        'conConnection.Open()
        'Dim szQuery As String = "select 'For : ' + provider_name + ' A/S/O ' + t.injuredparty_firstname + ' ' + t.injuredparty_lastname [Info] from tblcase t join tblprovider p on p.provider_id = t.provider_id where case_id = '" & Session("QStrCaseID") & "'"
        'cmdCommand.CommandText = szQuery
        'cmdCommand.CommandType = CommandType.Text
        'cmdCommand.Connection = conConnection

        'Dim oReader As SqlDataReader
        'oReader = cmdCommand.ExecuteReader()

        'Dim szInfo As String = ""
        'If (oReader.Read()) Then
        '    szInfo = oReader.Item(0)
        'End If

        'Return szInfo
        Return Nothing
    End Function

    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click

        Dim PatientID As String
        Dim Previous_PatientID As String
        Dim case_no As String
        Dim case_obj As String
        Dim case_id As String
        Dim previous_case_id As String
        Dim previous_case_obj As String
        Dim previous_case_no As String
        Dim company_id As String
        Dim objCaseobject As New Bill_Sys_CaseObject
        Dim _bill_Sys_Case As New Bill_Sys_Case
        Dim objCaseid As New CaseDetailsBO
        Dim urlscript As String

        Try

            previous_case_no = CType(Session("CASE_OBJECT"), Bill_Sys_CaseObject).SZ_CASE_NO
            previous_case_obj = lblPatientName.Text
            previous_case_id = Session("Case_ID").ToString()
            case_no = txtCaseSearch.Value
            Session("ChkCaseNoTxt") = case_no
            company_id = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject).SZ_COMPANY_ID

            Session("ChkCase_ID") = previous_case_id
            case_id = objCaseid.GetCaseID(case_no, company_id)
            case_obj = objCaseid.GetCaseName(case_id, company_id)
            PatientID = objCaseid.GetCasePatientID(case_id, "")
            Previous_PatientID = objCaseid.GetCasePatientID(previous_case_id, "")

            If case_id = "" Or IsNothing(case_id) And case_obj = "" Or IsNothing(case_obj) Then

                objCaseobject.SZ_CASE_NO = previous_case_no
                objCaseobject.SZ_PATIENT_NAME = previous_case_obj
                objCaseobject.SZ_PATIENT_ID = Previous_PatientID
                objCaseobject.SZ_CASE_ID = previous_case_id
                objCaseobject.SZ_COMAPNY_ID = company_id
                _bill_Sys_Case.SZ_CASE_ID = previous_case_id
                Session("CASE_OBJECT") = objCaseobject
                Session("CASEINFO") = _bill_Sys_Case
                Session("Case_ID") = previous_case_id
                Session("Case_ID") = previous_case_id
                Session("QStrCaseID") = previous_case_id
                Session("QStrCID") = previous_case_id
                Session("SN") = "0"
                Session("flag") = True
                urlscript = "<script language='javascript'>" + " window.opener.location.href  = window.opener.location.href" + "</script>"
                Session("ParentLoad") = urlscript
                lblMsg.Visible = True
                Response.Redirect("vb_CaseInformation.aspx")

            ElseIf case_id <> "" And case_obj <> "" Then


                objCaseobject.SZ_CASE_NO = case_no
                objCaseobject.SZ_PATIENT_NAME = case_obj
                objCaseobject.SZ_PATIENT_ID = PatientID
                objCaseobject.SZ_CASE_ID = case_id
                objCaseobject.SZ_COMAPNY_ID = company_id
                _bill_Sys_Case.SZ_CASE_ID = case_id
                Session("CASEINFO") = _bill_Sys_Case
                Session("CASE_OBJECT") = objCaseobject
                Session("Case_ID") = case_id
                Session("QStrCaseID") = case_id
                Session("QStrCID") = case_id
                Session("SN") = "0"
                Session("flag") = False
                urlscript = "<script language='javascript'>" + " window.opener.location.href  = window.opener.location.href" + "</script>"
                Session("ParentLoad") = urlscript
                lblMsg.Visible = False
                Response.Redirect("vb_CaseInformation.aspx")
            End If

        Catch ex As Exception

        End Try


    End Sub

    'Protected Sub btnRenameNode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameNode.Click
    '    Try
    '        Dim oConn As SqlConnection = New SqlConnection(_ConnectionString)
    '        Dim company_id As String
    '        Dim case_id As String
    '        Dim node_id As String
    '        Dim new_node_name As String
    '        Dim dr As SqlDataReader

    '        oConn.Open()
    '        company_id = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject).SZ_COMPANY_ID
    '        case_id = Session("Case_ID").ToString()
    '        node_id = selectedID.Value
    '        new_node_name = txtRenameNode.Value

    '        Dim myCommand As New SqlCommand("StoredProcedureName", oConn)
    '        myCommand.CommandType = CommandType.StoredProcedure

    '        myCommand.Parameters.Add("@CASEID", SqlDbType.NVarChar, 50).Value = case_id
    '        myCommand.Parameters.Add("@COMPANYID", SqlDbType.NVarChar, 50).Value = company_id
    '        myCommand.Parameters.Add("@NODEID", SqlDbType.NVarChar, 50).Value = node_id
    '        myCommand.Parameters.Add("@NODENAME", SqlDbType.NVarChar, 50).Value = new_node_name
    '        myCommand.ExecuteNonQuery()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    '#Region "Get Node Type"

    '    Public Function GetNodeDetails() As DataSet
    '        Dim ds As New DataSet
    '        Try
    '            Dim objBillCompany As New Bill_Sys_BillingCompanyObject
    '            objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)
    '            Dim strCaseId As String
    '            strCaseId = Session("Case_ID").ToString()
    '            Dim oConn As SqlConnection = New SqlConnection(_ConnectionString)
    '            oConn.Open()
    '            Dim oCommand As New SqlCommand()
    '            oCommand.CommandType = CommandType.StoredProcedure
    '            oCommand.Connection = oConn
    '            oCommand.CommandText = "STP_DSP_GET_CASEDOCUMENTNODETYPE"
    '            oCommand.Parameters.AddWithValue("@CASEID", strCaseId)
    '            oCommand.Parameters.AddWithValue("@ISARCHIVED", "0")
    '            oCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", objBillCompany.SZ_COMPANY_ID)
    '            Dim da As New SqlDataAdapter(oCommand)
    '            da.Fill(ds)
    '            oConn.Close()
    '        Catch ex As Exception

    '        End Try

    '        Return ds
    '    End Function
    '#End Region

    'Public Sub DelImgNode(ByVal ImgFileID As String)
    '    Dim oConn As SqlConnection = New SqlConnection(_ConnectionString)
    '    Try
    '        oConn.Open()
    '        Dim oCommand As New SqlCommand("DELETE FROM TBLDOCIMAGES where IMAGEID = '" + ImgFileID + "'", oConn)
    '        oCommand.ExecuteNonQuery()

    '    Catch ex As Exception

    '    Finally
    '        oConn.Close()
    '    End Try
    'End Sub

    Private Function ProcessNode(ByVal p_NodeID As String, ByVal sz_CompanyID As String, ByVal sz_Flag As String, ByVal sz_CaseId As String) As String
        Try
            Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
            conConnection.Open()
            Dim cmdCommand As SqlCommand = New SqlCommand("SP_HIDE_NODES", conConnection)

            cmdCommand.CommandType = CommandType.StoredProcedure
            cmdCommand.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID)
            cmdCommand.Parameters.AddWithValue("@I_NODEID", p_NodeID)
            cmdCommand.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseId)
            cmdCommand.Parameters.AddWithValue("@FLAG", sz_Flag)
            Dim objOutParameter As SqlParameter = New SqlParameter("@sz_output", SqlDbType.VarChar, 500)
            objOutParameter.Direction = ParameterDirection.Output
            cmdCommand.Parameters.Add(objOutParameter)

            cmdCommand.ExecuteNonQuery()

            Dim sz_ResultValue As String = objOutParameter.Value
            Session("DeletAddNode") = sz_ResultValue
            If (sz_Flag = "F") Then
                If sz_ResultValue.Contains("successfully") Then
                    Dim msg As String
                    msg = ""

                    Dim cmdCommand2 As SqlCommand = New SqlCommand()

                    Dim szQuery As String = "select  [filename] from tbldocimages where imageid = " & p_NodeID

                    cmdCommand2.CommandText = szQuery
                    cmdCommand2.CommandType = CommandType.Text
                    cmdCommand2.Connection = conConnection

                    Dim oReader As SqlDataReader
                    oReader = cmdCommand2.ExecuteReader()


                    If (oReader.Read()) Then
                        msg = oReader.Item(0)

                        oReader.Close()

                    End If
                    msg = "Delete-DocMgr File " & msg & "is delete by user " & CType(Session("USER_OBJECT"), Bill_Sys_UserObject).SZ_USER_NAME & "on " & DateTime.Now.ToString("MM/dd/yyyy") & " with imgeid" + p_NodeID
                    '' Session("DeletAddNode") = msg

                    Dim conConnection2 As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
                    conConnection2.Open()

                    Dim comm As SqlCommand = New SqlCommand("SP_TXN_NOTES", conConnection2)
                    comm.CommandType = CommandType.StoredProcedure
                    comm.CommandTimeout = 0
                    comm.Parameters.AddWithValue("@SZ_NOTE_CODE", "NOT1004")
                    comm.Parameters.AddWithValue("@SZ_CASE_ID", sz_CaseId)
                    comm.Parameters.AddWithValue("@SZ_USER_ID", CType(Session("USER_OBJECT"), Bill_Sys_UserObject).SZ_USER_ID)
                    comm.Parameters.AddWithValue("@SZ_NOTE_TYPE", "NTY0001")
                    comm.Parameters.AddWithValue("@SZ_NOTE_DESCRIPTION", msg)
                    comm.Parameters.AddWithValue("@IS_DENIED", "")
                    comm.Parameters.AddWithValue("@SZ_COMPANY_ID", sz_CompanyID)
                    comm.Parameters.AddWithValue("@FLAG", "ADD")
                    comm.ExecuteNonQuery()


                End If

            End If
            If (sz_ResultValue <> "") Then
                lblNodeID.Text = sz_ResultValue
            End If




        Catch ex As Exception
            Dim sr As String = ex.ToString()
        End Try
    End Function


    Public Function Nodeleval(ByVal ds As DataSet) As DataSet
        Dim str As String = "IMG-"
        Dim expression1 As String = "NodeID LIKE '" & str & "%'"
        Dim foundrows As DataRow()
        foundrows = ds.Tables(0).[Select](expression1)
        For Each dr As DataRow In foundrows
            Dim id As String = dr.ItemArray.GetValue(0).ToString()
            Dim Parenetid As String = dr.ItemArray.GetValue(1).ToString()



            While Parenetid <> ""
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i)("NodeID").ToString() = id AndAlso ds.Tables(0).Rows(i)("ParentID").ToString() = Parenetid Then
                        ds.Tables(0).Rows(i)("BT_UPDATE") = "1"
                        id = Parenetid

                        Dim expression2 As String = "NodeID LIKE '" & id & "%'"
                        Dim foundrows1 As DataRow()
                        foundrows1 = ds.Tables(0).[Select](expression2)
                        Dim dr1 As DataRow = foundrows1(0)
                        Parenetid = dr1.ItemArray.GetValue(1).ToString()
                        Exit For
                    End If
                Next
            End While

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i)("ParentID").ToString() = "" Then
                    ds.Tables(0).Rows(i)("BT_UPDATE") = "1"
                    Exit For
                End If

            Next
        Next

        Return ds
    End Function
    Protected Sub btnMergePDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMergePDF.Click
        Try
            Dim myConn As New SqlConnection(ConfigurationManager.AppSettings("MyConnectionString"))
            Dim sqlcmd As SqlCommand
            Dim str As String
            Dim strNodeList As String()

            'strNodeList = nodeid.Value.Split(",")
            strNodeList = hdnids.Value.Split(",")
            Session("MergeNodeList") = strNodeList

            sqlcmd = New SqlCommand("DELETE FROM TXN_SET_ORDER", myConn)
            myConn.Open()
            sqlcmd.ExecuteNonQuery()
            myConn.Close()


            For Each str In strNodeList
                'REJECT NODES WHICH ARE NOT IMAGES
                If str.Contains("IMG") Then
                    sqlcmd = New SqlCommand("INSERT_TXN_SET_ORDER", myConn)
                    sqlcmd.CommandType = CommandType.StoredProcedure
                    Dim imageid As String
                    imageid = str.Substring(str.IndexOf("-") + 1)
                    sqlcmd.Parameters.AddWithValue("@IMAGEID", imageid)

                    myConn.Open()
                    sqlcmd.ExecuteNonQuery()
                    myConn.Close()
                End If
            Next
            sqlcmd.Dispose()

            Dim objBillCompany As New Bill_Sys_BillingCompanyObject
            objBillCompany = CType(Session("BILLING_COMPANY_OBJECT_NEW"), Bill_Sys_BillingCompanyObject)
            Dim p_szCompanyID As String = objBillCompany.SZ_COMPANY_ID
            Dim p_szCompanyName As String = objBillCompany.SZ_COMPANY_NAME
            frameeditexpanse1.Src = "./SetOrder.aspx"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>window.onload = function(){DisplayFrame();};</script>")


            'Dim objBillCompany1 As New Bill_Sys_BillingCompanyObject()
            'Dim _bill_Sys_BillingCompanyDetails_BO1 As New Bill_Sys_BillingCompanyDetails_BO
            'objBillCompany1 = _bill_Sys_BillingCompanyDetails_BO1.getCompanyDetailsOfCase(Session("Case_ID").ToString())
            'Session("BILLING_COMPANY_OBJECT_NEW") = objBillCompany1
            'Dim p_szCompanyID As String = objBillCompany1.SZ_COMPANY_ID

            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>ShowSetorder('" + Session("Case_ID").ToString() + "','" + p_szCompanyID + "','" + p_szCompanyName + "');</script>")

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnREMergePDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnREMergePDF.Click
        Try
            Dim jobid As String = hdnRemerge.Value
            hdnRemerge.Value = ""
            BackgroundJob.Requeue(jobid)
        Catch ex As Exception

        End Try
    End Sub
End Class
