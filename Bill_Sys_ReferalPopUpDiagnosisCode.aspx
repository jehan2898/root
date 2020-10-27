<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ReferalPopUpDiagnosisCode.aspx.cs" Inherits="Bill_Sys_ReferalPopUpDiagnosisCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css"rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 76%;
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
                                    <td style="width: 102%; height: 106px;" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                
                                            <tr runat="server" id="trDoctorType">
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Diagnosis Type:</td>
                                                <td style="width: 35%; height: 18px;">
                                                    <cc1:ExtendedDropDownList id="extddlDiagnosisType"   runat="server" Width="105px" Connection_Key="Connection_String" Procedure_Name="SP_MST_DIAGNOSIS_TYPE" Selected_Text="--- Select ---" Flag_Key_Value="DIAGNOSIS_TYPE_LIST" ></cc1:ExtendedDropDownList></td>
                                                
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Code :
                                                   </td>
                                                <td style="width: 35%; height: 18px;">
                                                <asp:TextBox ID="txtDiagonosisCode" runat="server" Width="55px" MaxLength="50"></asp:TextBox>
                                                   </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Description :
                                                    </td>
                                                <td style="width: 35%; height: 18px;">
                                                 <asp:TextBox ID="txtDescription" runat="server"  Width="110px"
                                                        MaxLength="50"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="6">
                                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
            <asp:Button ID="btnSeacrh" runat="server" Text="Seacrh" Width="80px"  cssclass="Buttons" OnClick="btnSeacrh_Click" />
                    <asp:Button ID="btnOK" runat="server" Text="Add" Width="80px"  cssclass="Buttons" OnClick="btnOK_Click"/>&nbsp;
                    <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px"  cssclass="Buttons" OnClick="btnCancel_Click"/>--%>
                                                   </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 102%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 102%" class="TDPart">
                                    <div style="height:200px; overflow-y:scroll;">
                                     <asp:DataGrid ID="grdDiagonosisCode" runat="server"  
                             Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages" OnPageIndexChanged="grdDiagonosisCode_PageIndexChanged">
                      
                            <ItemStyle CssClass="GridRow"/>
                            <Columns>   
                              <asp:TemplateColumn>
                       <ItemTemplate>
                       <asp:CheckBox ID="chkAssociateDiagnosisCode" runat="server" />
                       </ItemTemplate>
                       </asp:TemplateColumn>                     
                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE_ID" HeaderText="DIAGNOSIS CODE ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="DIAGNOSIS CODE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SZ_DESCRIPTION" HeaderText="DESCRIPTION" Visible="true"></asp:BoundColumn>
                         <asp:BoundColumn DataField="SZ_DIAGNOSIS_TYPE_ID" HeaderText="COMPANY" Visible="False"></asp:BoundColumn>
                     
                      
                         
                    </Columns>
                            <HeaderStyle CssClass="GridHeader"/>
                        </asp:DataGrid>
                                        </div>
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
    </form>
</body>
</html>
