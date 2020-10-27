<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ConvertProcedureCode.aspx.cs" Inherits="Bill_Sys_ConvertProcedureCode" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
   <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align:top;">
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
                                    <td style="width: 100%" class="TDPart">
                                    <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                    </td>
                                </tr>  
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>                             
                                <tr>
                                    <td style="width: 100%">
                                   
                          
                               
                                    <table width="100%">
                                        <tr>
                                            <td width="100%" scope="col">
                                                <div class="lbl">
                                                    Procedure codes 
                                                    <div class="div_blockcontent">
                                                        <table width="100%" border="0">
                                                            
                                                            <tr>
                                                                <td class="lbl">
                                                                    Search
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox Width="100%" ID="txtSearchText" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkProcedureCode" runat="server" Text="Procedure Code" />
                                                                    <asp:CheckBox ID="chkProcedureCodeDescription" runat="server" Text="Procedure Code Description" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <asp:Button ID="btnSearch" runat="server" Width="80px" CssClass="Buttons" Text="Search"
                                                                        OnClick="btnSearch_Click"></asp:Button></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tablecellLabel">
                            <div class="lbl">
                                <%--Room Name--%>Specialty</div>
                        </td>
                        <td class="tablecellSpace">
                            <extddl:ExtendedDropDownList ID="extddlVisitRoom" runat="server" Width="254px" Connection_Key="Connection_String" Flag_Key_Value="ROOM_LIST" Procedure_Name="SP_MST_ROOM" Selected_Text="---Select---" Visible="false"/>
                            <cc1:ExtendedDropDownList id="extddlSpeciality" runat="server" Width="255px" Connection_Key="Connection_String" Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="--- Select ---" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"></cc1:ExtendedDropDownList> 
                        </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                 <div style="overflow: scroll; height: 600px; width: 100%;">
                                                                        <asp:DataGrid Width="100%" ID="grdProcedure" CssClass="GridTable" runat="server" AutoGenerateColumns="false"   >
                                                                          
                                                                             <ItemStyle CssClass="GridRow"/>
                                                                            <Columns>
                                                                                <asp:TemplateColumn>
                                                                                  <ItemTemplate>
                                                                                      <asp:CheckBox ID="chkSelect" runat="server" />
                                                                                  </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="Procedure CodeID" Visible="False">
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Procedure Code Description">
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn datafield="FLT_AMOUNT" headertext="Amount" visible="false"></asp:BoundColumn> 
                                                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="false"></asp:BoundColumn>
                                                                                <asp:BoundColumn datafield="SZ_PROCEDURE_GROUP" headertext="Specialty" ></asp:BoundColumn> 
                                                                            </Columns>
                                                                            <HeaderStyle CssClass="GridHeader"/>
                                                                        </asp:DataGrid>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 23px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 23px">
                                                                 
                                                                    <asp:Button ID="btnConToVisit" runat="server" Text="Convert To Visit" Width="130px" CssClass="Buttons" OnClick="btnConToVisit_Click"
                                                                         />
                                                                    <asp:Button ID="btnConToTreatment" runat="server" Text="Convert To Treatment" Width="130px" CssClass="Buttons" OnClick="btnConToTreatment_Click"
                                                                     />
                                                                      <asp:Button ID="btnConToTest" runat="server" Text="Convert To Test" Width="130px" CssClass="Buttons" OnClick="btnConToTest_Click"
                                                                     />
                                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                    <asp:TextBox ID="txtProcedureCode" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                    <asp:TextBox ID="txtProcedureDesc" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                                    <asp:TextBox ID="txtProcedureAmount" runat="server" Width="10px" Visible="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                         
                                                      
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                
                       
                            
                   
                                        
                                    </td>
                                </tr>
                            </table>
    
</td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
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
