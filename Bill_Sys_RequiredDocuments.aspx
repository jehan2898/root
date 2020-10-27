<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_RequiredDocuments.aspx.cs" Inherits="Bill_Sys_RequiredDocuments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

     <script src="js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="js/scan/Scan.js" type="text/javascript"></script>
    <script src="js/scan/function.js" type="text/javascript"></script>
    <script src="js/scan/Common.js" type="text/javascript"></script>
    <link href="Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="validation.js"></script>

     <script type="text/javascript">
          $(document).ready(function () {
              $('.scanlbl').click(function () {
                  var data = $(this).attr('data-val');
                  var caseid = $('[id$=hdnCaseId]').val();
                  var dataArray = data.split(',');
                  var txtData = $(this).closest('tr').find('.txtNote').val();
                  var ddlData = $(this).closest('tr').find('.ddlAssign').find(":selected").val();                
                  var userName = $(this).closest('tr').find('.ddlAssign').find(":selected").text();               
                  if (ddlData == 'NA') {
                      ddlData = '';
                      userName = '';
                  }
                  var docid = dataArray[1];
                  if (docid == "") {
                      docid = "0";
                  }
          
                  scanReqDoc(caseid, docid, dataArray[2], dataArray[3], txtData, ddlData, userName, '4');
              });
          });
    </script>

    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td class="LeftTop">
                        </td>
                        <td class="CenterTop">
                        </td>
                        <td class="RightTop">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <%--<tr>
                                    <td style="width: 100%; text-align:right;" class="TDPart">
                                    <a href="#" runat="server" id="hlnkShowSearch"></a> &nbsp;
                                    </td>
                                </tr> 
                                <tr>
                                    <td style="width: 100%; text-align: left;" class="TDPart">
                                        <asp:DataGrid ID="grdShowCaseDetails" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false">
                                            <ItemStyle CssClass="GridRow" />
                                            <HeaderStyle CssClass="GridHeader" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_LAST_NAME" HeaderText="Last Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_FIRST_NAME" HeaderText="First Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_SSN" HeaderText="SSN #"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_FUNDINGSTATUS" HeaderText="Funding Status"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="TOTAL_FUNDING" HeaderText="Total Fundings"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_UPDATED_DATE" HeaderText="Updated Date" DataFormatString="{0:MM/dd/yyyy}">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="Updated By"></asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                               --%>
                               <tr>
                                    
                                    <td style="width: 100%; text-align: left;" class="TDPart">
                                        <!-- This should be a reserved space for system messages. -->
                                        <UserMessage:MessageControl runat="server" id="usrMessage" />
                                    
                                        <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <a href="#" onclick="return openTypePage('a')">
                                                            <img src="Images/actionEdit.gif" style="border: none;"/>
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr> 
                                <tr>
                                    <td style="width: 77%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; text-align: left;" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:DataGrid ID="grdDocumentGrid" runat="server" Width="100%" CssClass="GridTable" 
                                                        AutoGenerateColumns="False" OnItemDataBound="grdDocumentGrid_ItemDataBound" OnItemCommand = "grdDocumentGrid_ItemCommand" OnSelectedIndexChanged="grdDocumentGrid_SelectedIndexChanged1">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <Columns>
                                                            <%-- 0 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID" Visible="false"></asp:BoundColumn>
                                                            <%-- 1 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_CASE_DOCUMENT_ID" HeaderText="Case Document ID" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%-- 2 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="Case Type ID" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%-- 3 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_DOC_TYPE_ID" HeaderText="Document Type ID" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%--<asp:TemplateColumn HeaderText="Document Type">
                                                                <ItemTemplate>
                                                                    <a href="#">
                                                                       <a  target="_blank"  href="Document Manager/case/vb_CaseInformation.aspx" > <%# DataBinder.Eval(Container, "DataItem.SZ_DOC_TYPE") %></a>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>--%>
                                                            <%-- 4 --%>
                                                             <asp:TemplateColumn HeaderText="Document Type">
                                                                <ItemTemplate>
                                                                     <asp:LinkButton Text = '<%# DataBinder.Eval(Container, "DataItem.SZ_DOC_TYPE") %>' ID="LinkButton1" runat="server" CausesValidation="false" CommandArgument= '<%# DataBinder.Eval(Container, "DataItem.i_image_id") %>' CommandName="Show Doc"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>  
                                                            <%-- 5 --%>
                                                            <asp:TemplateColumn HeaderText="Received" ItemStyle-HorizontalAlign="center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRecieved" runat="server" Enabled="false" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" />
                                                            </asp:TemplateColumn>
                                                            <%-- 6 --%>
                                                            <asp:TemplateColumn HeaderText="Notes">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtNotes" runat="server" Width="120px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%-- 7 --%>
                                                            <asp:TemplateColumn HeaderText="Assigned To">
                                                                <ItemTemplate>
                                                                    <extddl:ExtendedDropDownList ID="extddlAssignTo" runat="server" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="GET_USER_LIST" Procedure_Name="SP_GET_LIST_MST_USER" Selected_Text="---Select---"     Width="100px"></extddl:ExtendedDropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%-- 8 --%>
                                                            <asp:BoundColumn DataField="DT_ASSIGN_ON" HeaderText="Assigned On" DataFormatString="{0:MM/dd/yyyy}">
                                                            </asp:BoundColumn>
                                                            <%-- 9 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_UPDATED_BY" HeaderText="Last Updated By" Visible="false">
                                                            </asp:BoundColumn>
                                                            <%-- 10 --%>
                                                            <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="Last Updated By"></asp:BoundColumn>
                                                            <%-- 11 --%>
                                                            <asp:BoundColumn DataField="DT_UPDATED_ON" HeaderText="Last Updated On" DataFormatString="{0:MM/dd/yyyy}">
                                                            </asp:BoundColumn>
                                                            <%-- 12 --%>
                                                            <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                                            <%-- 13 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_ASSIGN_TO" HeaderText="Assign To" Visible="false"></asp:BoundColumn>
                                                            <%-- 14 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_NOTES" HeaderText="Notes" Visible="false"></asp:BoundColumn>
                                                            <%-- 15 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="I_RECIEVED" HeaderText="Recieved" Visible="false"></asp:BoundColumn>
                                                            <%-- 16 --%>
                                                            <asp:TemplateColumn HeaderText="Scan">
                                                                <ItemTemplate>
                                                                    <a 
                                                                        id="caseDetailScan" 
                                                                        href="#" runat="server" data-val='<%# Eval("SZ_CASE_ID")+","+ Eval("SZ_CASE_DOCUMENT_ID")+","+ Eval("i_document_type_id")+","+ Eval("i_node_id") %>'
                                                                        title="Scan/Upload" 
                                                                        class="lbl scanlbl">Scan/Upload</a>
                                                                     <asp:LinkButton runat="server" CausesValidation="false" CommandName="Select" Text="Scan" Visible="false"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>                                                          
                                                            <%-- 17 --%>
                                                            <asp:TemplateColumn HeaderText="Upload Document" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:FileUpload ID="fileuploadDocument" runat="server" Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%-- 18 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="SZ_DOC_TYPE" HeaderText="Doc Type" Visible="false"></asp:BoundColumn>
                                                            <%-- 19 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="i_document_type_id" HeaderText="Doc Type ID" Visible="false"></asp:BoundColumn>
                                                            <%-- 20 --%><%-- false --%>
                                                            <asp:BoundColumn DataField="i_node_id" HeaderText="Node ID" Visible="false"></asp:BoundColumn>
                                                            <%-- 21 --%>
                                                            <asp:TemplateColumn HeaderText="View" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink Text="View" Target="_blank" ID="hyUndo" runat = "server" NavigateUrl='<%# "Bill_Sys_UndoRequiredDocuments.aspx?cid=" + DataBinder.Eval(Container.DataItem, "SZ_CASE_ID") + "&txnid=" + DataBinder.Eval(Container.DataItem, "SZ_CASE_DOCUMENT_ID") + "&docid=" + DataBinder.Eval(Container.DataItem, "i_document_type_id")%>'/>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%-- 22 --%>
                                                            <asp:BoundColumn DataField="sz_document_link" HeaderText="View" Visible="true"></asp:BoundColumn>
                                                            <%-- 23 --%>
                                                            <asp:TemplateColumn HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="delDocument" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <%-- 24 --%>
                                                            <asp:BoundColumn DataField="i_image_id" HeaderText="i_image_id" Visible="false"></asp:BoundColumn>
                                                            
                                                            
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; height: 23px;">
                                                    <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtUserID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>&nbsp;
                                                    <asp:TextBox ID="txtDocID" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtRecieved" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtImageId" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtNotes" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtAssignTo" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtAssignOn" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnFileUpload" runat="server" Text="Upload" Width="80px" CssClass="Buttons" OnClick="btnFileUpload_Click" Visible="false" />
                                                    <asp:Button ID="btnDocUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons" OnClick="btnDocUpdate_Click" />
                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" CssClass="Buttons" OnClick="btnDeleteDocument_Click" />
                                                    <asp:TextBox ID="txtDocTypeId" runat="server" Visible="false" Width="10px"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnCaseId" runat="server" />
                                                    <asp:HiddenField ID="hdnPatientId" runat="server" />
                                                </td>
                                            </tr>
                                            
                                             <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                                <contenttemplate>
                                                    <asp:Panel ID="pnlShowSearch" runat="server" Style="width:490px;height:295px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
                                                    <iframe id="iframeSearch" src="PFS_Sys_CommonSearch.aspx" frameborder="0" height="295px" width="490px" visible="false">
                                                    
                                                    
                                                    </iframe>                                   
                                                 </asp:Panel>
                                                </contenttemplate>
                                            </asp:UpdatePanel>--%>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" colspan="6">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom" style="height: 7px">
                        </td>
                        <td class="CenterBottom" style="height: 7px">
                        </td>
                        <td class="RightBottom" style="width: 10px; height: 7px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid; left: 0px; top: -433px;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px" visible="false" ></iframe>
    </div>
     <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
</div>
</asp:Content>
