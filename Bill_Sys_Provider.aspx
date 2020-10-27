<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="Bill_Sys_Provider.aspx.cs"
    Inherits="Bill_Sys_Provider" %>


<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/javascript" src="validation.js"></script>
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
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        
                                        <tr>
                                                <td class="ContentLabel" style="text-align:center; height:25px;" colspan="4" >
                                                <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                                                <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                </td> 
                                                </tr> 
                                                 <tr>
                                       <td class="ContentLabel" style="width: 15%">
                                                Provider Name                                          
                                        </td>
                                       <td style="width: 35%">
                                            <asp:TextBox ID="txtProviderName" runat="server" MaxLength="50"></asp:TextBox></td>
                                       <td class="ContentLabel" style="width: 15%">
                                                Provider Type
                                        </td>
                                       <td style="width: 35%">
                                            <asp:TextBox ID="txtProviderType" runat="server"></asp:TextBox></td>
                                    </tr>
                                <tr>
                                   <td class="ContentLabel" style="width: 15%">
                                            Provider Local Address
                                    </td>
                                   <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderLocalAddress" runat="server" MaxLength="50"></asp:TextBox></td>
                                   <td class="ContentLabel" style="width: 15%">
                                            Local City
                                    </td>
                                     <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderLocalCity" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                        <td class="ContentLabel" style="width: 15%">
                                            Local State
                                    </td>
                                    <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderLocalState" runat="server" MaxLength="50"></asp:TextBox></td>
                                   <td class="ContentLabel" style="width: 15%">
                                            Local Zip
                                    </td>
                                   <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderLocalZip" runat="server" MaxLength="50"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" style="width: 15%">
                                     
                                            Perm Address
                                    </td>
                                   <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderPermAddress" runat="server" MaxLength="50"></asp:TextBox></td>
                                    <td class="ContentLabel" style="width: 15%">
                                            Perm City
                                    </td>
                                   <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderPermCity" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                   <td class="ContentLabel" style="width: 15%">
                                            Perm State
                                    </td>
                                     <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderPermState" runat="server" MaxLength="50"></asp:TextBox></td>
                                    <td class="ContentLabel" style="width: 15%">
                                            Perm Zip
                                    </td>
                                    <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderPermZip" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" style="width: 15%">
                                            Perm Phone
                                    </td>
                                     <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderPermPhone" runat="server" MaxLength="50"></asp:TextBox></td>
                                     <td class="ContentLabel" style="width: 15%">
                                            Contact
                                    </td>
                                  <td style="width: 35%">
                                        <asp:TextBox ID="txtProviderContact" runat="server"></asp:TextBox></td>
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
                                                  <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px" cssclass="Buttons"/>
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                            Width="80px" cssclass="Buttons"/>
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px" cssclass="Buttons"/>
                                                   </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                    
                                    <asp:DataGrid ID="grdProvider" runat="server" OnSelectedIndexChanged="grdProvider_SelectedIndexChanged"
                            OnPageIndexChanged="grdProvider_PageIndexChanged" Width="100%" CssClass="GridTable" AutoGenerateColumns="false"  AllowPaging="true" PageSize="10" PagerStyle-Mode="NumericPages">
                         
                        
                            <ItemStyle CssClass="GridRow"/>
                            <Columns>
                                <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                                <asp:BoundColumn HeaderText="PROVIDER ID" DataField="SZ_PROVIDER_ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_NAME" HeaderText="PROVIDER NAME"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_TYPE" HeaderText="PROVIDER TYPE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="COMPANY ID" Visible="false" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_LOCAL_ADDRESS" HeaderText="Local Address" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_LOCAL_CITY" HeaderText="Local City" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_LOCAL_STATE" HeaderText="Local State" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_LOCAL_ZIP" HeaderText="Local Zip" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_CONTACT" HeaderText="Contact" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_PERM_ADDRESS" HeaderText="Address" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_PERM_CITY" HeaderText="City"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_PERM_STATE" HeaderText="State" ></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_PERM_ZIP" HeaderText="Perm Zip" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SZ_PROVIDER_PERM_PHONE" HeaderText="Phone"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <a href="Bill_Sys_Doctor.aspx?ProviderId= <%# DataBinder.Eval(Container.DataItem, "SZ_PROVIDER_ID") %>"
                                            target="_Blank">Assign Doctor</a>
                                        
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader"/>
                        </asp:DataGrid>
                        
                        
                                    
                                        
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
</asp:Content>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Billing System</title>

    <script type="text/javascript" src="validation.js"></script>

    <link href="Css/main.css" type="text/css" rel="Stylesheet" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmProvider" runat="server">
        <div align="center">
            <table class="maintable" cellpadding="0px" cellspacing="0px">
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg">
                        <div align="right">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg" class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg" class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="100%" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
                </tr>
                <tr>
                    <td height="35px" bgcolor="#000000" colspan="5">
                        <div align="left">
                        </div>
                        <div align="left">
                            <span class="pg-bl-usr">Billing company name</span></div>
                    </td>
                    <td width="12%" height="35px" bgcolor="#000000">
                        <div align="right">
                            <span class="usr">Admin</span></div>
                    </td>
                </tr>
                <tr>
                    <td height="20px" colspan="6" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
                            <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td height="18" colspan="6" align="right" background="Images/sub-menu-bg.jpg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th height="29" width="19%" scope="col">
                                    <div align="left">
                                       <a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#EAEAEA" align="center" style="height: 20px">
                        <div align="left" class="band">
                            Provider</div>
                    </td>
                </tr>
                <tr>
                    <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col" colspan="6">
                        <div align="left">
                            <table width="55%" border="0" align="center" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td colspan="6" height="30px">
                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                  
                                   
                                <tr>
                                    <td colspan="6" align="center" style="height: 21px">
                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click"
                                            CssClass="btn-gray" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click"
                                            CssClass="btn-gray" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" OnClick="btnClear_Click"
                                            CssClass="btn-gray" /></td>
                                </tr>
                       
                            </table>
                        </div>
                    </th>
                </tr>
                <tr>
                    <th scope="col" colspan="6">
                        <div align="left">
                        </div>
                        <div align="left" class="band">
                            Provider List</div>
                    </th>
                </tr>
                <tr>
                    <td colspan="6">
                        
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>--%>
