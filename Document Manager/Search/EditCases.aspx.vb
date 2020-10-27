Imports BLCaseMaster = Cases.Business.BusinessCaseMaster
Imports CasePlanTiffData = Cases.DataSet.CasePlanTiffData
Imports System.Data
Imports System.Data.SQLClient
Imports SystemConfiguration = Common.SystemConfiguration
Imports System.IO
Imports System.Diagnostics

Partial Class Search_EditCases
    Inherits System.Web.UI.Page

#Region "Variable Declaration"
    Protected _ConnectionString As String = SystemConfiguration.ConnectionString
#End Region

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objBLCaseMaster As New BLCaseMaster()

        If dgCasePlainTiff.Items.Count <> 0 Then
            FetchDataFromControls(objBLCaseMaster)
            objBLCaseMaster.SaveData(CType(ViewState("objCasePlanTiffData"), DataSet))

            'CasePlantiff(objBLCaseMaster.ID)
            Session("Msg") = objBLCaseMaster.Message
            Response.Redirect("../ErrorPages/frmOperationSuccess.aspx")
        Else
            'lblMessage.Visible = True
            'lblMessage.Text = "Pleased add atleast on client name! Click Add Plaintiff"
            Page.RegisterStartupScript("Message", "<script language='javascript'> alert('Please add at least one client using Add Plaintiff link');</script>")

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lnkPlaintiff.Attributes.Add("OnClick", "return OnclickShowPlainTiff();")
        btnAddClients.Attributes.Add("onClick", "return CheckNull();")
        'lblMessage.Visible = False
        If (Request.QueryString.Count > 0) Then
            If Convert.ToString(Request.QueryString("MOP")) = "E" Then
                lblPageHeader.Text = "Home - Edit Case"
            ElseIf Convert.ToString(Request.QueryString("MOP")) = "D" Then
                lblPageHeader.Text = "Home - Delete Case"
            ElseIf Convert.ToString(Request.QueryString("MOP")) = "V" Then
                lblPageHeader.Text = "Home - View Case Details"
                dgCasePlainTiff.Columns(3).Visible = False
                dgCasePlainTiff.Columns(4).Visible = False
                btnAddClients.Visible = False
                btnReset.Visible = False
                btnSave.Visible = False
                lnkPlaintiff.Visible = False
            End If

            If Not IsPostBack Then
                PopulateControlsWithData()
            End If
        Else
            'lnkPlaintiff.Attributes.Add("OnClick", "return OnclickPlainTiff();")
            If IsNothing(ViewState("objCasePlanTiffData")) Then
                Dim objBLCaseMaster As New BLCaseMaster()
                Dim objCasePlanTiffData As CasePlanTiffData = New CasePlanTiffData()
                objCasePlanTiffData = objBLCaseMaster.GetPlaintiffData(0)
                ViewState("objCasePlanTiffData") = objCasePlanTiffData
            End If
        End If

            'If Not IsPostBack Then
            '    Dim objBLCaseMaster As New BLCaseMaster()
            '    Dim objCasePlanTiffData As CasePlanTiffData = New CasePlanTiffData()

            '    objCasePlanTiffData = objBLCaseMaster.GetPlaintiffData(0)
            '    ViewState("objCasePlanTiffData") = objCasePlanTiffData

            'End If

    End Sub

    Private Sub PopulateControlsWithData()
        Dim objBLCaseMaster As New BLCaseMaster()
        objBLCaseMaster.ID = Request.QueryString("ID").ToString()
        objBLCaseMaster.GetData()
        txtCaseID.Text = objBLCaseMaster.CaseID
        'txtClientFirstName.Text = objBLCaseMaster.ClientFirstName
        'txtClientLastName.Text = objBLCaseMaster.ClientLastName
        txtCaseDescription.Text = objBLCaseMaster.Description
        txtProvider.Text = objBLCaseMaster.Provider
        txtInsuranceCompany.Text = objBLCaseMaster.InsuranceCompany
        txtAccidentDate.Text = objBLCaseMaster.AccidentDate
        txtClaimNo.Text = objBLCaseMaster.InsuranceClaimNo
        txtClaimAmount.Text = objBLCaseMaster.ClaimAmount
        txtPaidAmount.Text = objBLCaseMaster.PaidAmount
        txtClaimAmount.Text = objBLCaseMaster.ClaimAmount
        txtServiceStartDate.Text = objBLCaseMaster.DateofServiceStart
        txtServiceEndDate.Text = objBLCaseMaster.DateofServiceEnd
        Try
            ddlStatus.Items.FindByText(objBLCaseMaster.Status).Selected = True
        Catch ex As Exception
        End Try

        Dim objCasePlanTiffData As CasePlanTiffData = New CasePlanTiffData()
        'If Convert.ToString(ViewState("OperationType")) = "UPDATEPLAINTIFF" Then
        '    objCasePlanTiffData = CType(ViewState("objCasePlanTiffData"), DataSet)
        'Else
        objCasePlanTiffData = objBLCaseMaster.GetPlaintiffData(Request.QueryString("ID").ToString())
        'End If

        dgCasePlainTiff.DataSource = objCasePlanTiffData
        dgCasePlainTiff.DataBind()
        ViewState("objCasePlanTiffData") = objCasePlanTiffData

        If dgCasePlainTiff.Items.Count = 0 Then

            dgCasePlainTiff.Visible = False

        End If

    End Sub

    Private Sub FetchDataFromControls(ByRef pBLCaseMaster As BLCaseMaster)
        If Convert.ToString(Request.QueryString("MOP")) = "E" Then
            pBLCaseMaster.OperationType = "UPDATE"
            pBLCaseMaster.ID = Request.QueryString("ID").ToString()
        ElseIf Convert.ToString(Request.QueryString("MOP")) = "D" Then
            pBLCaseMaster.OperationType = "DELETE"
            pBLCaseMaster.ID = Request.QueryString("ID").ToString()
        Else
            pBLCaseMaster.OperationType = "INSERT"
        End If


        pBLCaseMaster.CaseID = txtCaseID.Text
        pBLCaseMaster.ClientFirstName = txtClientFirstName.Text
        pBLCaseMaster.ClientLastName = txtClientLastName.Text
        pBLCaseMaster.Description = txtCaseDescription.Text
        pBLCaseMaster.Provider = txtProvider.Text
        pBLCaseMaster.InsuranceCompany = txtInsuranceCompany.Text
        pBLCaseMaster.AccidentDate = txtAccidentDate.Text
        pBLCaseMaster.InsuranceClaimNo = txtClaimNo.Text
        pBLCaseMaster.ClaimAmount = txtClaimAmount.Text
        pBLCaseMaster.PaidAmount = txtPaidAmount.Text
        pBLCaseMaster.ClaimAmount = txtClaimAmount.Text
        pBLCaseMaster.DateofServiceStart = txtServiceStartDate.Text
        pBLCaseMaster.DateofServiceEnd = txtServiceEndDate.Text
        pBLCaseMaster.Status = ddlStatus.SelectedItem.Text

    End Sub

    Protected Sub btnAddClients_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddClients.Click
        dgCasePlainTiff.Visible = True

        Dim objCasePlanTiffData As DataSet = New DataSet()
        objCasePlanTiffData = CType(ViewState("objCasePlanTiffData"), DataSet)

        If hndOperationType.Value = "UPDATEPLAINTIFF" And ViewState("PlaintiffID") <> "&nbsp;" Then
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_ID) = ViewState("PlaintiffID")
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_FirstName) = txtClientFirstName.Text
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_LastName) = txtClientLastName.Text
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_LoginID) = Session("UserName")
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_Operation) = "UPDATE"
        ElseIf hndOperationType.Value = "UPDATEPLAINTIFF" And ViewState("PlaintiffID") = "&nbsp;" Then
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_FirstName) = txtClientFirstName.Text
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_LastName) = txtClientLastName.Text
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_LoginID) = Session("UserName")
            objCasePlanTiffData.Tables(0).Rows(dgCasePlainTiff.SelectedIndex).Item(CasePlanTiffData.FIELD_Operation) = "INSERT"
            'drFilter(1) = Request.QueryString("ID")
        Else
            Dim drFilter As DataRow = objCasePlanTiffData.Tables(CasePlanTiffData.Table_CasePlanTiff).NewRow()
            drFilter(2) = txtClientFirstName.Text
            drFilter(3) = txtClientLastName.Text
            drFilter(4) = Session("UserName")
            drFilter(7) = "INSERT"
            objCasePlanTiffData.Tables(CasePlanTiffData.Table_CasePlanTiff).Rows.Add(drFilter)
            objCasePlanTiffData.Tables(CasePlanTiffData.Table_CasePlanTiff).AcceptChanges()
        End If



        ViewState("objCasePlanTiffData") = objCasePlanTiffData

        dgCasePlainTiff.DataSource = objCasePlanTiffData
        dgCasePlainTiff.DataBind()
        txtClientFirstName.Text = ""
        txtClientLastName.Text = ""
        hndOperationType.Value = "Add"
        btnAddClients.Text = "Add Client"

    End Sub

    Public Sub DeleteCasePlantiff(ByVal id As Integer)

        Dim conConnection As SqlConnection = New SqlConnection(Common.SystemConfiguration.ConnectionString)
        Dim cmdCommand As SqlCommand = New SqlCommand()
        Try

            conConnection.Open()
            cmdCommand.CommandText = "STP_UID_CASEPLANTIFF"
            cmdCommand.CommandType = CommandType.StoredProcedure

            cmdCommand.Parameters.Add("@OPERATION", SqlDbType.NVarChar).Value = "DELETE"
            cmdCommand.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id
            cmdCommand.Parameters.Add("@CASEID", SqlDbType.NVarChar).Value = 0
            cmdCommand.Parameters.Add("@FIRSTNAME", SqlDbType.NVarChar, 255).Value = ""
            cmdCommand.Parameters.Add("@LASTNAME", SqlDbType.NVarChar).Value = ""
            cmdCommand.Parameters.Add("@LOGINID", SqlDbType.NVarChar).Value = ""
            cmdCommand.Parameters("@ID").Direction = ParameterDirection.InputOutput
            cmdCommand.Connection = conConnection

            cmdCommand.ExecuteNonQuery()

        Catch ex As Exception
        Finally
            If conConnection.State = ConnectionState.Open Then
                conConnection.Close()
            End If

            conConnection = Nothing
            cmdCommand = Nothing
        End Try

    End Sub

   
    Protected Sub dgCasePlainTiff_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCasePlainTiff.SelectedIndexChanged
        Page.RegisterStartupScript("Display", "<script>OnclickPlainTiff();</script>")
        txtClientFirstName.Text = IIf(dgCasePlainTiff.SelectedItem.Cells(1).Text = "&nbsp;", "", dgCasePlainTiff.SelectedItem.Cells(1).Text)
        txtClientLastName.Text = IIf(dgCasePlainTiff.SelectedItem.Cells(2).Text = "&nbsp;", "", dgCasePlainTiff.SelectedItem.Cells(2).Text)
        ViewState("PlaintiffID") = dgCasePlainTiff.SelectedItem.Cells(0).Text
        hndOperationType.Value = "UPDATEPLAINTIFF"
        'trFN.Visible = True
        'trLN.Visible = True
        'trButton.Visible = True
        btnAddClients.Text = "Update"
        ''Page.RegisterStartupScript("Display", "<script language='javascript'> function display(){document.getElementById('trFN').style.display='block'; document.getElementById('trLN').style.display= 'block'; document.getElementById('trButton').style.display ='block'; return false;}</script>")



    End Sub

    Protected Sub dgCasePlainTiff_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgCasePlainTiff.DeleteCommand
        If e.Item.Cells(0).Text <> "&nbsp;" Then
            DeleteCasePlantiff(Convert.ToInt32(e.Item.Cells(0).Text))
        End If

        Dim objCasePlanTiffData As DataSet = New DataSet()
        objCasePlanTiffData = CType(ViewState("objCasePlanTiffData"), DataSet)
        objCasePlanTiffData.Tables(0).Rows.RemoveAt(e.Item.ItemIndex)
        dgCasePlainTiff.DataSource = objCasePlanTiffData
        dgCasePlainTiff.DataBind()
        ViewState("objCasePlanTiffData") = objCasePlanTiffData

    End Sub

    Protected Sub dgCasePlainTiff_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgCasePlainTiff.ItemDataBound
        'If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
        '    Dim objEditLink As LinkButton
        '    objEditLink = e.Item.Cells(3).Controls(0)
        '    objEditLink.Attributes.Add("onclick", "return OnclickPlainTiff();")
        'End If
    End Sub
End Class
