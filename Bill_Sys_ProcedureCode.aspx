<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ProcedureCode.aspx.cs" Inherits="Bill_Sys_ProcedureCode" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    
     <script type="text/javascript">
        function ConfirmDelete()
        {
             var msg = "Do you want to proceed?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
        }
    </script>
    

    <script language="javascript" type="text/javascript">
            	
         function ascii_value(c){
             c = c . charAt (0);
             var i;
             for (i = 0; i < 256; ++ i)
             {
                  var h = i . toString (16);
                  if (h . length == 1)
                    h = "0" + h;
                   h = "%" + h;
                  h = unescape (h);
                  if (h == c)
                    break;
             }
             return i;
        }

        function CheckForInteger(e,charis)
        {
                var keychar;
                if(navigator.appName.indexOf("Netscape")>(-1))
                {    
                    var key = ascii_value(charis);
                    if(e.charCode == key || e.charCode==0){
                    return true;
                   }else{
                         if (e.charCode < 48 || e.charCode > 57)
                         {             
                                return false;
                         } 
                     }
                 }
            if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
            {          
                var key=""
               if(charis!="")
               {         
                     key = ascii_value(charis);
                }
                if(event.keyCode == key)
                {
                    return true;
                }
                else
                {
                         if (event.keyCode < 48 || event.keyCode > 57)
                          {             
                             return false;
                          }
                }
            }
            
            
         }
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
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Code</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtProcedureCode" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Speciality&nbsp;</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <cc1:ExtendedDropDownList ID="extddlProCodeGroup" runat="server" Width="90%" Connection_Key="Connection_String"
                                                        Procedure_Name="SP_MST_PROCEDURE_GROUP" Selected_Text="--- Select ---" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST">
                                                    </cc1:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Description
                                                </td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtProcedureDesc" runat="server" CssClass="textboxCSS" MaxLength="300"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Amount&nbsp;</td>
                                                <td style="width: 35%" class="ContentLabel">
                                                    <asp:TextBox ID="txtProcedureAmount" runat="server" CssClass="textboxCSS" MaxLength="20"
                                                        onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="textboxCSS" Visible="false">
                                                        <asp:ListItem Value="0"> --Select--</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000001">Visits</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000002">Treatments</asp:ListItem>
                                                        <asp:ListItem Value="TY000000000000000003" Selected="True" >Test</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="height:auto;">
                                                <td class="ContentLabel" style="width: 15%">
                                                    Visit Type</td>
                                                <td style="width: 35%" align="left"  class="ContentLabel">
                                                    <asp:RadioButtonList ID="rdoVisitType" runat="server" style="float:right; margin-right:90px;" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="65px" >
                                                    <asp:ListItem Text="IE" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="FU" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="tablecellLabel">
                                                </td>
                                                <td class="tablecellSpace">
                                                </td>
                                                <td class="tablecellControl">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="TDPart">
                                                    <asp:DataGrid ID="grdAmount" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"
                                                        AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" Visible="false">
                                                        <ItemStyle CssClass="GridRow" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_AMOUNT_ID" HeaderText="SZ_PROCEDURE_AMOUNT_ID"
                                                                Visible="False"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="SZ_CASE_TYPE_ID" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="text-box" MaxLength="10" Text='<%# DataBinder.Eval(Container.DataItem, "SZ_AMOUNT") %>'
                                                                        onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="SZ_PROCEDURE_CODE_ID" HeaderText="SZ_PROCEDURE_CODE_ID"
                                                                Visible="false"></asp:BoundColumn>
                                                        </Columns>
                                                        <HeaderStyle CssClass="GridHeader" />
                                                    </asp:DataGrid></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                <asp:TextBox ID="txtVisitType" runat="server" Visible="False" Width="10px">0</asp:TextBox>
                                                    <asp:TextBox ID="txtVisitID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtRoomId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtSearchOrder" runat="server" Visible="False" Width="10px"></asp:TextBox> 
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                                        Width="80px" CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px"
                                                        CssClass="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TDPart" style="width: 100%; text-align:right; height: 44px;">
                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="overflow: scroll; height: 400px; width: 99%;">
                                            <asp:DataGrid ID="grdProcedure" runat="server" OnPageIndexChanged="grdProcedure_PageIndexChanged"
                                                OnSelectedIndexChanged="grdProcedure_SelectedIndexChanged" Width="100%" CssClass="GridTable"
                                                AutoGenerateColumns="false" PagerStyle-Mode="NumericPages" OnItemCommand="grdProcedure_ItemCommand">
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-Width="5%"></asp:ButtonColumn>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="PROCEDURE ID" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code" ItemStyle-Width="7%" Visible="false"></asp:BoundColumn>
                                                   
                                                    <asp:TemplateColumn HeaderText="Procedure Code" ItemStyle-Width="7%">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlCode" runat="server" CommandName="CodeSearch" CommandArgument="SZ_PROCEDURE_CODE" Font-Bold="true" Font-Size="12px">Procedure Code</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_CODE")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn> 
                                                   <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="description" visible="false"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Description">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlDescription" runat="server" CommandName="DescriptionSearch" CommandArgument="SZ_CODE_DESCRIPTION" Font-Bold="true" Font-Size="12px">Description</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_CODE_DESCRIPTION")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    
                                                    <%--<asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>--%>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="SZ_PROCEDURE_GROUP_ID"
                                                        Visible="False"></asp:BoundColumn>
                                                         <asp:TemplateColumn HeaderText="Speciality" ItemStyle-Width="7%">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlSpeciality" runat="server" CommandName="SpecialitySearch" CommandArgument="SZ_PROCEDURE_GROUP" Font-Bold="true" Font-Size="12px">Speciality</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn> 
                                                     <asp:BoundColumn DataField="I_VISIT_TYPE" HeaderText="VisitType" Visible="false"    ></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_CODE_ID" HeaderText="SZ_TYPE_CODE_ID" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE_ID" HeaderText="Type" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_ROOM_ID" HeaderText="Room" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_TYPE" HeaderText="Type" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_ROOM" HeaderText="Room" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" Visible="True" DataFormatString="{0:F2}"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" Visible="false"></asp:BoundColumn>
                                                   
                                                    <asp:ButtonColumn CommandName="Delete" Text="Delete" Visible="false"></asp:ButtonColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="RightCenter" style="height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
