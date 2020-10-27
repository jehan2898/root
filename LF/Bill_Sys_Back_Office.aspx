<%@ Page Language="C#" MasterPageFile="~/LF/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Back_Office.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000" />
    <div align="center">
        <asp:UpdatePanel ID="panel1" runat="server" UpdateMode="conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <table style="border-right: #b5df82 1px; border-top: #b5df82 1px; font-size: 1.1em;
                    border-left: #b5df82 1px; width: 100%; color: #000000; border-bottom: #b5df82 1px;
                    font-family: Arial, Helvetica, sans-serif; background-color: white; text-decoration: none"
                    id="MainTable">
                    <tbody>
                        <tr>
                            <td style="padding-right: 1em; padding-left: 1em; padding-bottom: 1em; vertical-align: top;
                                width: 40%; padding-top: 1em; text-align: left">
                                <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                    width: 95%; border-bottom: #b5df82 1px solid" id="Dataupload">
                                    <tbody>
                                        <tr>
                                            <td style="font-weight: bold; font-size: 1em; color: #000000; font-family: Arial, Helvetica, sans-serif;
                                                background-color: #b5df82; text-align: left; text-decoration: none" valign="middle"
                                                align="left" colspan="3" height="28">
                                                CSV File Upload
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Select File type:
                                            </td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="filedrpdwn" runat="server" AutoPostBack="false">
                                                    <asp:ListItem Selected="true" Value="csv">CSV File</asp:ListItem>
                                                    <asp:ListItem Value="excel">MS Excel Workbook(2003/2007)</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Upload file:
                                            </td>
                                            <td colspan="2">
                                                <asp:FileUpload ID="flUpload" runat="server"></asp:FileUpload>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%; text-align: center">
                                                <asp:Button Style="width: 90%" ID="btnUpload" OnClick="btnUpload_Click" runat="server"
                                                    Text="Upload"></asp:Button>
                                            </td>
                                            <td style="width: 33%; text-align: center">
                                                <asp:Button Style="width: 90%" ID="Button1" OnClick="Button1_Click" runat="server"
                                                    Text="Process"></asp:Button>
                                            </td>
                                            <td style="width: 33%; text-align: center">
                                                <asp:Button Style="width: 90%" ID="Button2" OnClick="Button2_Click" runat="server"
                                                    Text="Save"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; text-decoration: underline" colspan="2">
                                                Status&nbsp;Messages:
                                            </td>
                                            <td style="text-align: right" colspan="1">
                                                <asp:Label ID="LblDataUpload" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="overflow: hidden; color: maroon; text-align: left" id="Status" colspan="3"
                                                runat="Server">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table style="width: 85%; position: relative" id="tbl3">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress_weekly" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="panel1">
                                                    <ProgressTemplate>
                                                        <asp:Image ID="img2" runat="server" Style="position: absolute; z-index: 1; left: 50%;
                                                            top: 50%" ImageUrl="~/LF/Images/simple-loading2.gif" AlternateText="Loading.....">
                                                        </asp:Image>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <br />
                                <br />
                                <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; vertical-align: top;
                                    border-left: #b5df82 1px solid; width: 95%; border-bottom: #b5df82 1px solid"
                                    id="Table1">
                                    <tbody>
                                        <tr>
                                            <td style="font-weight: bold; font-size: 1em; color: #000000; font-family: Arial, Helvetica, sans-serif;
                                                background-color: #b5df82; text-align: left; text-decoration: none" valign="middle"
                                                align="left" colspan="3" height="28">
                                                Zip File Upload
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Upload Zip file:
                                            </td>
                                            <td colspan="2">
                                                <asp:FileUpload ID="zpupload" runat="server"></asp:FileUpload>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%; text-align: center">
                                                <asp:Button Style="width: 90%" ID="zipbtn" OnClick="zipbtn_Click" runat="server"
                                                    Text="Upload Zip File"></asp:Button>
                                            </td>
                                            <td style="width: 33%; text-align: center">
                                                <asp:Button Style="width: 90%" ID="zipbtnsave" OnClick="zipbtnsave_Click" runat="server"
                                                    Text="Apply Changes" Visible="false"></asp:Button>
                                            </td>
                                            <td style="width: 33%; text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; text-decoration: underline" colspan="2">
                                                Status&nbsp;Messages:
                                            </td>
                                            <td style="text-align: right" colspan="1">
                                                <asp:Label ID="LblDocsUpload" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="overflow: hidden; color: maroon; text-align: left" id="zipstatus" colspan="3"
                                                runat="Server">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td style="padding-right: 1em; padding-left: 1em; padding-bottom: 1em; vertical-align: top;
                                width: 60%; padding-top: 1em">
                                <table style="width: 100%; text-align: left" id="Table2" border="0">
                                    <tbody>
                                        <tr>
                                            <td style="width: 50%" valign="top" align="right">
                                                <asp:LinkButton ID="btnExportToExcelValid" OnClick="btnExportToExcelValid_Click" runat="server" Text="Export TO Excel" Visible="false">
                                                <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/>  Export To Excel</asp:LinkButton>
                                                <%--<asp:Button ID="btnExportToExcelValid" runat="server" Text="Export To Excel" OnClick="btnExportToExcelValid_Click" Visible="false" />--%>
                                                <asp:GridView ID="grdValidateData" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                                    PageSize="50">
                                                    <HeaderStyle BackColor="#B5DF82" HorizontalAlign="center" Font-Bold="true" Font-Size="1em"
                                                        Font-Names="Arial, Helvetica, sans-serif" ForeColor="#000000" />
                                                    <RowStyle HorizontalAlign="center" Font-Size="1em" Font-Names="Arial, Helvetica, sans-serif" />
                                                </asp:GridView>
                                                
                                            
                                            </td>
                                            <td style="width: 50%" valign="top" align="right">
                                                <asp:UpdatePanel ID="grdpnl" runat="server">
                                                    <ContentTemplate>
                                                            <asp:LinkButton ID="btnExportToExcelInvalid" OnClick="btnExportToExcelInvalid_Click" runat="server" Text="Export TO Excel" Visible="false">
                                                            <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/>  Export To Excel</asp:LinkButton>
                                                            <%--<asp:Button ID="btnExportToExcelInvalid" runat="server" Text="Export To Excel" OnClick="btnExportToExcelInvalid_Click" Visible="false" />--%>
                                                            <asp:GridView ID="grdInValidateData" runat="server" AllowPaging="true" AutoGenerateColumns="true"
                                                            PageSize="20">
                                                            <HeaderStyle BackColor="#B5DF82" HorizontalAlign="center" Font-Bold="true" Font-Size="1em"
                                                                Font-Names="Arial, Helvetica, sans-serif" ForeColor="#000000" />
                                                            <RowStyle HorizontalAlign="center" Font-Size="1em" Font-Names="Arial, Helvetica, sans-serif" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click">
                                                        </asp:AsyncPostBackTrigger>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <ajaxToolkit:PopupControlExtender ID="PopDataUpload" runat="server" TargetControlID="LblDataUpload"
                    Position="Right" PopupControlID="pnlDataUpload">
                </ajaxToolkit:PopupControlExtender>
                <ajaxToolkit:PopupControlExtender ID="PopDocsUpload" runat="server" TargetControlID="LblDocsUpload"
                    Position="Right" PopupControlID="pnlDocsUpload">
                </ajaxToolkit:PopupControlExtender>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnUpload" />
                <asp:AsyncPostBackTrigger ControlID="zipbtnsave" EventName="Click" />
                <asp:PostBackTrigger ControlID="zipbtn" />
                <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:Panel ID="pnlDataUpload" runat="server" Visible="true">
        <table border="0" cellpadding="0" cellspacing="0" style="width: auto; font-family: Arial, Helvetica, sans-serif;
            font-size: 1.1em; color: #000000; text-decoration: none; background-color: white;
            border-style: solid; border-width: 5px; border-color: #5998C9">
            <tr>
                <td colspan="2" style="width: 100%; font-weight: bold; text-align: center; background-color: #5998C9;">
                    Format 1
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    File Type
                </td>
                <td style="text-align: left; font-weight: bold">
                    CSV File
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    File Extension
                </td>
                <td style="text-align: left; font-weight: bold">
                    .CSV
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    File Structure
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; color: Maroon; font-weight: bold">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CaseID;BillNumber;LawFirmCaseID;IndexNumber;PurchaseDate;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Eg: c100;sa58;c5068;12;02/03/2010;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; font-weight: bold">
                    <br />
                    &nbsp;NOTE:
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;&nbsp;- Each Parameter should be seperated by SemiColon ";"
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;&nbsp;- Purchase Date should be entered in "mm/dd/yyyy" format&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 100%; font-weight: bold; text-align: center; background-color: #5998C9;">
                    Format 2
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    File Type
                </td>
                <td style="text-align: left; font-weight: bold">
                    MSEXCEL File (2003/2007)
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    File Extension
                </td>
                <td style="text-align: left; font-weight: bold">
                    .XLS or .XLSX
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    File Structure
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;The name of the Worksheet should be "Sheet1".
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;First row should contain headers for each column.
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-First Column should be CaseID
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-Second Column should be BillNumber
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-Third Column should be LawFirmCaseID
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-Fourth Column should be Index Number
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-Fifth Column should be Purchase Date.
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-Sixth Column should be Trial Date
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-Seventh Column should be LawFirm Status
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left; font-weight: bold">
                    <br />
                    &nbsp;NOTE:
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left;">
                    &nbsp;&nbsp;- Blank rows between data are not allowed.
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDocsUpload" runat="server" Visible="true">
        <table border="0" cellpadding="0" cellspacing="0" style="width: auto; font-family: Arial, Helvetica, sans-serif;
            font-size: 1.1em; color: #000000; text-decoration: none; background-color: white;
            border-style: solid; border-width: 5px; border-color: #5998C9">
            <tr>
                <td colspan="2" style="width: 100%; font-weight: bold; text-align: center; background-color: #5998C9;">
                    Format 1
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    File Type
                </td>
                <td style="text-align: left; font-weight: bold">
                    &nbsp;&nbsp;ZIP File
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    File Extension
                </td>
                <td style="text-align: left; font-weight: bold">
                    &nbsp;&nbsp;.ZIP
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    File Structure
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;>&nbsp;-CASEID Folder
                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td colspan="1" style="text-align: left;">
                    -PDF Files
                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td colspan="1" style="text-align: left;">
                    -Other Files and Folders
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;>&nbsp;-CASEID Folder
                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td colspan="1" style="text-align: left;">
                    -PDF Files
                </td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td colspan="1" style="text-align: left;">
                    -Other Files and Folders
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
