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
Imports Hangfire
Imports Aquaforest.PDF
Imports Newtonsoft.Json
Imports System.Threading

#End Region

Partial Class Case_SetOrder
    Inherits System.Web.UI.Page

    Public fileName As String
    Dim strCaseID As String
    Dim aline As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim szCasei = Session("Case_Id")
            'Session("Case_Id") = Request.QueryString("Case_Id").ToString()
            If Not IsPostBack() Then
                Dim myConn As New SqlConnection(ConfigurationManager.AppSettings("Connection_String"))
                Dim sqlcmd As SqlCommand
                Dim str As String
                Dim strNodeList As String()
                Dim strList As String
                'strList = Request.QueryString("NodeList")
                If Session("MergeNodeList") IsNot Nothing Then
                    strNodeList = CType(Session("MergeNodeList"), String())

                    'strNodeList = strList.Split(",")

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

                    BindListBox()


                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindListBox()
        Try
            Dim myConn As New SqlConnection(ConfigurationManager.AppSettings("Connection_String"))
            Dim da As New SqlDataAdapter()
            Dim sqlcmd As New SqlCommand("GET_SELECTED_PDF_DETAILS", myConn)

            Dim ds As New DataSet()
            da.SelectCommand = sqlcmd
            da.SelectCommand.CommandType = CommandType.StoredProcedure
            myConn.Open()
            da.Fill(ds)

            lstPDF.DataSource = ds
            lstPDF.DataTextField = "SZ_FILENAME"
            lstPDF.DataValueField = "SZ_IMAGEID"
            lstPDF.DataBind()

            myConn.Close()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDone.Click
        Dim pdf As PDFValidator
        Dim pdfList As List(Of String)
        pdfList = New List(Of String)
        aline = hidnOrderFiles.Value
        Dim szMessage As String
        Dim filepath As String
        Dim objMergePDF As New PDFConvertor.PDFMerger()
        Dim strNodeList As String()
        Dim strFileNotFound As String
        'strNodeList = nodeid.Value.Split(",")
        strNodeList = aline.Split(",")
        Dim arrFilePath As New ArrayList()
        arrFilePath = getPDFFilePath(strNodeList)
        Dim arrPath As New ArrayList
        Dim strFileName As String
        strFileName = ""
        Dim strName As String
        strName = ""
        Dim szCmpName As String
        'szCmpName = Request.QueryString("CompanyName")
        Dim objMergeFile As PDFConvertor.MergeFile
        'objMergeFile=
        Dim i As Integer
        Dim flag As String
        flag = "true"

        For i = 0 To arrFilePath.Count - 1
            objMergeFile = arrFilePath(i)
            strFileName = objMergeFile.FilePath
            strFileName = objMergeFile.FilePath
            If File.Exists(strFileName) Then
                pdf = New PDFValidator(strFileName)

                If pdf.IsValid = True Then
                    arrPath.Add(arrFilePath(i))
                End If
            End If

            If Not File.Exists(strFileName) Then
                Dim strFiles As String()
                flag = "false"

                strFiles = strFileName.Split("/")
                strFileName = strFiles(strFiles.Length - 1)

                If strName = "" Then
                    strName = strFileName
                Else
                    strName = strName + "," + strFileName
                End If
                'arrFilePath.RemoveAt(i)

            End If


        Next i

        If flag = "false" Then
            'btnDone.Attributes.Add("onclick","check("+strName+")")
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>check(" + strName + ")</script>")
            Page.RegisterStartupScript("TestString", "<script language='javascript'> chek('" + strName + "')</script>")
        Else

            filepath = getSavedLettersFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath") + Session("cmpName").ToString() + "/" + Session("Case_ID").ToString())
            fileName = hidnFile.Value + ".pdf"
            'szMessage = objMergePDF.MergeMultiplePDF(filepath + fileName, arrFilePath)
            'Hangfire changes

            Dim jobid = BackgroundJob.Enqueue(Sub() objMergePDF.MergeMultiplePDF(filepath + fileName, arrPath))
            'szMessage = objMergePDF.MergeMultiplePDF(filepath + fileName, arrPath)
            Dim Message = "Merge request scheduled for Job ID: " + jobid + ".Please visit again after some time."

            'If szMessage = "PDF-NOT-CREATED" Then
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>alert('Cannot create PDF!');window.location.href='" & Request.Url.AbsoluteUri.ToString() & "'</script>")
            'ElseIf szMessage = "SUCCESS" Then
            Dim myConn As New SqlConnection(ConfigurationManager.AppSettings("MyConnectionString"))

            myConn.Open()
            Dim cmdQuery As New SqlCommand("SP_SAVE_MERGED_PDF_IN_MGR", myConn)
            cmdQuery.CommandType = CommandType.StoredProcedure
            Dim szCaseid As String
            szCaseid = Session("Case_Id").ToString()
            szCaseid.Trim()
            With cmdQuery
                .Parameters.Add("@p_szCaseID", SqlDbType.NVarChar)
                .Parameters("@p_szCaseID").Value = szCaseid.Trim()
                .Parameters.Add("@p_szCompanyID", SqlDbType.NVarChar)
                .Parameters("@p_szCompanyID").Value = Session("cmpid")

                .Parameters.Add("@p_szFileName", SqlDbType.NVarChar)
                .Parameters("@p_szFileName").Value = fileName

                .Parameters.Add("@JobID", SqlDbType.NVarChar)
                .Parameters("@JobID").Value = jobid

                .Parameters.Add("@JobStatus", SqlDbType.NVarChar)
                .Parameters("@JobStatus").Value = "Processing"
            End With
            cmdQuery.ExecuteNonQuery()
            cmdQuery.Dispose()
            myConn.Close()
            myConn.Dispose()
            ' ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "ShowMessage('" + Message + "','Merge request submitted','success');", True)

            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "ConfirmSave('btnCancel','Merge request','Merge request submitted','');", True)
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>alert('PDF Merged Successfully!');window.opener.location.href = window.opener.location.href;window.close();</script>")
            'Page.RegisterStartupScript("TestString", "<script language='javascript'>alert('PDF Merged Successfully!');window.parent.document.location.href='./vb_CaseInformation.aspx';window.close();</script>")
            'End If

        End If
    End Sub



    Private Function getSavedLettersFolder(ByVal p_szPath As String) As String
        If IO.Directory.Exists(p_szPath) Then
            If Not IO.Directory.Exists(p_szPath & "/Packeted Doc") Then
                IO.Directory.CreateDirectory(p_szPath & "/Packeted Doc")
            End If
        Else
            IO.Directory.CreateDirectory(p_szPath)
            IO.Directory.CreateDirectory(p_szPath & "/Packeted Doc")
        End If

        Return p_szPath & "/Packeted Doc/"
    End Function

    Private Function getPDFFilePath(ByVal strNodeList As String()) As ArrayList
        Dim myConn As New SqlConnection(ConfigurationManager.AppSettings("Connection_String"))
        Dim sqlcmd As SqlCommand
        Dim str As String
        Dim dr As SqlDataReader
        Dim objMergeList As New ArrayList
        ' Dim szPhysicalPath As String = ApplicationSettings.GetParameterValue("PhysicalBasePath")
        Dim objMergeFile As PDFConvertor.MergeFile


        For Each str In strNodeList
            sqlcmd = New SqlCommand("STP_DSP_IMAGEPHYSICALFILEPATH", myConn)
            sqlcmd.CommandType = CommandType.StoredProcedure
            Dim imageid As String
            imageid = str.Substring(str.IndexOf("-") + 1)
            sqlcmd.Parameters.AddWithValue("@IMAGEID", imageid)

            myConn.Open()
            dr = sqlcmd.ExecuteReader

            Do While (dr.Read)
                objMergeFile = New PDFConvertor.MergeFile
                objMergeFile.FilePath = dr("InputFilePath")
                objMergeFile.FileType = PDFConvertor.MergeFile.FILE_TYPE.TYPE_IMG
                objMergeList.Add(objMergeFile)
            Loop
            myConn.Close()
        Next
        Return objMergeList
    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Page.RegisterStartupScript("TestString", "<script language='javascript'>window.parent.document.location.href='./vb_CaseInformation.aspx';window.close();</script>")
        'Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ss", "<script language='javascript'>window.parent.document.location.href='./vb_CaseInformation.aspx';window.close();</script>")
    End Sub

    Protected Sub btnCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheck.Click

        Dim pdf As PDFValidator
        Dim pdfList As List(Of String)
        pdfList = New List(Of String)

        aline = hidnOrderFiles.Value
        Dim szMessage As String
        Dim filepath As String
        Dim objMergePDF As New PDFConvertor.PDFMerger()
        Dim strNodeList As String()
        Dim strFileNotFound As String
        'strNodeList = nodeid.Value.Split(",")
        strNodeList = aline.Split(",")
        Dim arrFilePath As New ArrayList
        arrFilePath = getPDFFilePath(strNodeList)

        Dim strFileName As String
        strFileName = ""
        Dim strName As String
        strName = ""
        Dim szCmpName As String
        'szCmpName = Request.QueryString("CompanyName")
        Dim objMergeFile As PDFConvertor.MergeFile
        'objMergeFile=
        Dim i As Integer
        Dim flag As String
        flag = "true"
        Dim arr As New ArrayList
        Dim arrPath As New ArrayList

        For i = 0 To arrFilePath.Count - 1
            objMergeFile = arrFilePath(i)
            strFileName = objMergeFile.FilePath
            If File.Exists(strFileName) Then
                pdf = New PDFValidator(strFileName)

                If pdf.IsValid = True Then
                    arrPath.Add(arrFilePath(i))
                    'pdfList.Add(strFileName)
                End If
            End If
        Next i

        'Dim index As Integer

        'For j As Integer = 0 To arr.Count - 1
        '    index = Convert.ToInt32(arr(j))
        '    arrFilePath.RemoveAt(index)
        'Next

        If arrPath.Count > 0 Then
            filepath = getSavedLettersFolder(ApplicationSettings.GetParameterValue("PhysicalBasePath") + Session("cmpName").ToString() + "/" + Session("Case_ID").ToString())
            fileName = hidnFile.Value + ".pdf"
            'szMessage = objMergePDF.MergeMultiplePDF(filepath + fileName, arrPath)
            'Hangfire changes

            Dim jobid = BackgroundJob.Enqueue(Sub() objMergePDF.MergeMultiplePDF(filepath + fileName, arrPath))
            'szMessage = objMergePDF.MergeMultiplePDF(filepath + fileName, arrPath)
            Dim Message = "Merge request scheduled for Job ID: " + jobid + ".Please visit again after some time."
            'If szMessage = "PDF-NOT-CREATED" Then
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>alert('Cannot create PDF!');window.location.href='" & Request.Url.AbsoluteUri.ToString() & "'</script>")
            'ElseIf szMessage = "SUCCESS" Then
            Dim myConn As New SqlConnection(ConfigurationManager.AppSettings("Connection_String"))
                myConn.Open()
                Dim cmdQuery As New SqlCommand("SP_SAVE_MERGED_PDF_IN_MGR", myConn)
                cmdQuery.CommandType = CommandType.StoredProcedure
                Dim szCaseid As String
                szCaseid = Session("Case_Id").ToString()
                szCaseid.Trim()
                With cmdQuery
                    .Parameters.Add("@p_szCaseID", SqlDbType.NVarChar)
                    .Parameters("@p_szCaseID").Value = szCaseid.Trim()
                    .Parameters.Add("@p_szCompanyID", SqlDbType.NVarChar)
                    .Parameters("@p_szCompanyID").Value = Session("cmpid")

                    .Parameters.Add("@p_szFileName", SqlDbType.NVarChar)
                .Parameters("@p_szFileName").Value = fileName

                .Parameters.Add("@JobID", SqlDbType.NVarChar)
                .Parameters("@JobID").Value = jobid

                .Parameters.Add("@JobStatus", SqlDbType.NVarChar)
                .Parameters("@JobStatus").Value = "Processing"
            End With
                cmdQuery.ExecuteNonQuery()
                cmdQuery.Dispose()
                myConn.Close()
                myConn.Dispose()
            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>alert('PDF Merged Successfully!');window.opener.location.href = window.opener.location.href;window.close();</script>")
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "ConfirmSave('btnCancel','Merge request','Merge request submitted','');", True)

            'End If
        Else
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "pdf", "<script type='text/javascript'>alert('No Files available for merge!');window.location.href='" & Request.Url.AbsoluteUri.ToString() & "'</script>")
        End If


    End Sub
End Class
