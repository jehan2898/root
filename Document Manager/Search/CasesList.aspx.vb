Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Partial Class Search_CasesList
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ddl.PopulateDropDownLists("STP_DDL_STATUS", ddlStatus, "Status_Type", "Status_Abr")

        AxpDataGrid1.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("AxpConnectionString")

        ' Important, DataBind called in PreRender
        AxpDataGrid1.UseDataBinding = True
        AxpDataGrid1.EditPerformUpdates = False

        ' We are doing our own database update 
        If AxpDataGrid1.StoredProcCommand Is Nothing Then
            AxpDataGrid1.SQL = "STP_SEARCHCASE"

            'AxpDataGrid1.FormViewColumns.Item(11).Visible = False


            If Request.QueryString("A") = "0" Then
                'AxpDataGrid1.SetGridFreeFormatCellByCol(1, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Search/EditCases.aspx?MOP=E&Case_Id=") & "#CaseId#" & "'><img src='" & Page.ResolveUrl("~/images/actionedit.gif") & "' border='0'></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(1, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Search/EditCases.aspx?MOP=E&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" + "'><img src='" & Page.ResolveUrl("~/images/actionedit.gif") & "' border='0'></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(2, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Search/EditCases.aspx?MOP=D&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" + "'><img src='" & Page.ResolveUrl("~/images/Not-Settled.gif") & "' border='0'></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(3, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Search/EditCases.aspx?MOP=V&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" + "'><img src='" & Page.ResolveUrl("~/images/view_details.gif") & "' border='0'></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(4, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Case/vb_CaseInformation.aspx?A=0&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" & "'><nobr>#CaseId#</nobr></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(5, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Templates/richeditter.asp?A=0&SelDoc=DoctorExam.htm&Case_Id=") & "#Template#" & "&ID=" & "#ID#" & "'><nobr>#Template#</nobr></a>&nbsp;")
            Else
                AxpDataGrid1.SetGridFreeFormatCellByCol(1, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Search/EditCases.aspx?MOP=E&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" + "'><img src='" & Page.ResolveUrl("~/images/actionedit.gif") & "' border='0'></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(2, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Search/EditCases.aspx?MOP=D&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" + "'><img src='" & Page.ResolveUrl("~/images/Not-Settled.gif") & "' border='0'></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(3, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Search/EditCases.aspx?MOP=V&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" + "'><img src='" & Page.ResolveUrl("~/images/view_details.gif") & "' border='0'></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(4, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Case/vb_CaseInformation.aspx?A=1&Case_Id=") & "#CaseId#" & "&ID=" & "#ID#" & "'><nobr>#CaseId#</nobr></a>&nbsp;")
                AxpDataGrid1.SetGridFreeFormatCellByCol(5, "<a class=""esnav"" href='" & Page.ResolveUrl("~/Templates/richeditter.asp?A=1&SelDoc=DoctorExam.htm&Case_Id=") & "#Template#" & "&ID=" & "#ID#" & "'><nobr>#Template#</nobr></a>&nbsp;")
            End If
            If Convert.ToString(Request.QueryString("OPR")) = "D" Then
                AxpDataGrid1.GridFieldsHide = "ID,Edit"
            ElseIf Convert.ToString(Request.QueryString("OPR")) = "E" Then
                AxpDataGrid1.GridFieldsHide = "ID,Remove"
            Else
                AxpDataGrid1.GridFieldsHide = "ID,Edit,Remove"
            End If

        Else
            'clear any params that might have been set
            AxpDataGrid1.StoredProcCommand.Parameters.Clear()
        End If
            Call AxpDataGridBindData()
            Panel1.Visible = True
    End Sub


    Sub AxpDataGridBindData()

        Dim param0 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@strCaseId", OleDbType.VarChar, 50)
        param0.Value = Case_Id.Text

        Dim param1 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@Status", OleDbType.VarChar, 50)
        param1.Value = ddlStatus.SelectedValue

        Dim param2 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@InjuredParty_LastName", OleDbType.VarChar, 50)
        param2.Value = InjuredParty_LastName.Text

        Dim param3 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@InjuredParty_FirstName", OleDbType.VarChar, 50)
        param3.Value = InjuredParty_FirstName.Text

        Dim param4 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@Policy_Number", OleDbType.VarChar, 100)
        param4.Value = Policy_Number.Text

        Dim param5 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@Ins_Claim_Number", OleDbType.VarChar, 100)
        param5.Value = Ins_Claim_Number.Text

        Dim param6 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@IndexOrAAA_Number", OleDbType.VarChar, 100)
        param6.Value = IndexOrAAA_Number.Text

        Dim param7 As OleDbParameter = AxpDataGrid1.StoredProcCommand.Parameters.Add("@IsArchived", OleDbType.VarChar, 100)
        param7.Value = Request.QueryString("A").ToString()

        AxpDataGrid1.Visible = True
        Panel1.Visible = True

    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        AxpDataGrid1.DataBind()
    End Sub

End Class
