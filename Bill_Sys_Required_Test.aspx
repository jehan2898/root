<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Bill_Sys_Required_Test.aspx.cs" Inherits="Bill_Sys_Required_Test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

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
                                
                               <tr>
                                    <td style="width: 100%; text-align: left;" class="TDPart">
                                       <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%;">
                                            <tr>
                                                <td class="ContentLabel"><b>Case#:-&nbsp;</b>
                                                </td> 
                                                <td style="font-size: 12px;font-family: arial;" align="left" >
                                                    <b><asp:Label ID="lblCaseID" runat="server" Text="Label"></asp:Label></b>
                                                </td> 
                                                <td class="ContentLabel"><b>Patient Name:-&nbsp;</b>
                                                </td> 
                                                <td style="font-size: 12px;font-family: arial;" align="left">
                                                <b><asp:Label ID="lblPatient" runat="server" Text="Label"></asp:Label></b>
                                                </td>
                                                
                                            </tr> 
                                       </table> 
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
                                                    <asp:GridView ID="grdRequiredTest" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
                                                        DataKeyNames="SZ_PROCEDURE_GROUP_ID,REQUIRED_TEST_ID" 
                                                        onrowdatabound="grdRequiredTest_RowDataBound">
                                                        <RowStyle CssClass="GridRow" />
                                                    <Columns>                                        
                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" 
                                                            DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="PROCEDURE GROUP ID"
                                                      Visible="false"  >
<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField ItemStyle-Height="5px" HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkGroup" runat="server" />
                                                        </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Height="5px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="SZ_PROCEDURE_GROUP" HeaderText="PROCEDURE GROUP"><HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                   
                                                    <asp:BoundField HeaderStyle-HorizontalAlign="Left" DataField="REQUIRED_TEST_ID" 
                                                            Visible="False" HeaderText="TEST ID"><HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        </asp:BoundField>
                                                </Columns>
                                                    
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle CssClass="GridHeader" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                     
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Button ID="btnAddRequiredTest" runat="server" Text="Add" Width="80px" 
                                                        CssClass="Buttons" onclick="btnAddRequiredTest_Click" 
                                                        />
                                                    &nbsp;<asp:Button ID="btnClearTest" runat="server" Text="Clear" Width="80px" 
                                                        CssClass="Buttons" onclick="btnClearTest_Click"
                                                       />
                                                </td>
                                            </tr>                                           
                                            
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
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px" visible="false" ></iframe>
    </div>
</asp:Content>
