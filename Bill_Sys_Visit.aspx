<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_Visit.aspx.cs" Inherits="Bill_Sys_Visit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript">
        function ValidationForVisit()
        {
            if(document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddVisit_txtVisitCode').value == "" || document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddVisit_txtVisitDescription').value == "")
            {
                document.getElementById('ErrorDiv').innerHTML = "Enter required fileds.";
                return false;
            }
            else
                return true;
        }
        
        function ValidationForTreatment()
        {
            if(document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTreatment_txtTreatmentCode').value == "" || document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlTreatmentVisit_txtTreatmentDescription').value == "")
            {
                document.getElementById('ErrorDiv').innerHTML = "Enter required fileds.";
                return false;
            }
            else
                return true;
        }
        
        function ValidationForTest()
        {
            if(document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTest_txtTestCode').value == "" || document.getElementById('_ctl0_ContentPlaceHolder1_tabcontainerPatientTreatment_tabpnlAddTest_txtTestDescription').value == "")
            {
                document.getElementById('ErrorDiv').innerHTML = "Enter required fileds.";
                return false;
            }
            else
                return true;
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
                                                <td class="ContentLabel" style="text-align: left;" colspan="4">
                                                    <ajaxToolkit:TabContainer ID="tabcontainerPatientTreatment" runat="Server" ActiveTabIndex="0"
                                                        CssClass="ajax__tab_theme">
                                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlAddVisit" TabIndex="0">
                                                            <HeaderTemplate>
                                                                <div style="width: 80px; height: 200px;" class="lbl">
                                                                    Visit</div>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div align="left">
                                                                    <table width="53%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Visit Code</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <asp:TextBox ID="txtVisitCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Visit Description</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <asp:TextBox ID="txtVisitDescription" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Room Name</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <extddl:ExtendedDropDownList ID="extddlVisitRoom" runat="server" Width="150px" Connection_Key="Connection_String" Flag_Key_Value="ROOM_LIST" Procedure_Name="SP_MST_ROOM" Selected_Text="---Select---"  />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="3">
                                                                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                                                <asp:TextBox ID="txtVisitID" runat="server" Width="250px" MaxLength="50" Visible="false"
                                                                                    Text="VISITS"></asp:TextBox>
                                                                                <asp:Button ID="btnVisitSave" runat="server" OnClick="btnVisitSave_Click" Text="Add"
                                                                                    Width="80px" CssClass="Buttons" />
                                                                                <asp:Button ID="btnVisitUpdate" runat="server" OnClick="btnVisitUpdate_Click" Text="Update"
                                                                                    Width="80px" CssClass="Buttons" Visible="false" />
                                                                                <asp:Button ID="btnVisitClear" runat="server" OnClick="btnVisitClear_Click" Text="Clear"
                                                                                    Width="80px" CssClass="Buttons" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlAddTreatment" TabIndex="0">
                                                            <HeaderTemplate>
                                                                <div style="width: 80px; height: 200px;" class="lbl">
                                                                    Treatments</div>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div align="left">
                                                                    <table width="53%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <tr>
                                                                            <td colspan="3" height="30">
                                                                                <div id="Div1" style="color: red" visible="true">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Treatment Code</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <asp:TextBox ID="txtTreatmentCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Treatment Description</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <asp:TextBox ID="txtTreatmentDescription" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Room Name</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <extddl:ExtendedDropDownList ID="extddlTreatmentRoom" runat="server" Width="150px" Connection_Key="Connection_String" Flag_Key_Value="ROOM_LIST" Procedure_Name="SP_MST_ROOM" Selected_Text="---Select---"  />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="3">
                                                                                <asp:TextBox ID="txtTreatmentID" runat="server" Width="250px" MaxLength="50" Visible="false"
                                                                                    Text="TREATMENTS"></asp:TextBox>
                                                                                <asp:Button ID="btnTreatmentSave" runat="server" OnClick="btnTreatmentSave_Click"
                                                                                    Text="Add" Width="80px" CssClass="Buttons" />
                                                                                <asp:Button ID="btnTreatmentUpdate" runat="server" OnClick="btnTreatmentUpdate_Click"
                                                                                    Text="Update" Visible="false" Width="80px" CssClass="Buttons" />
                                                                                <asp:Button ID="btnTreatmentClear" runat="server" OnClick="btnTreatmentClear_Click"
                                                                                    Text="Clear" Width="80px" CssClass="Buttons" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlAddTest" TabIndex="0">
                                                            <HeaderTemplate>
                                                                <div style="width: 80px; height: 200px;" class="lbl">
                                                                    Test</div>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div align="left">
                                                                    <table width="53%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <tr>
                                                                            <td colspan="3" height="30">
                                                                                <div id="Div2" style="color: red" visible="true">
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Test Code</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <asp:TextBox ID="txtTestCode" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Test Description</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <asp:TextBox ID="txtTestDescription" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Room Name</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td class="tablecellControl">
                                                                                <extddl:ExtendedDropDownList ID="extddlTestRoom" runat="server" Width="150px" Connection_Key="Connection_String" Flag_Key_Value="ROOM_LIST" Procedure_Name="SP_MST_ROOM" Selected_Text="---Select---"  />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="3">
                                                                                <asp:TextBox ID="txtTestID" runat="server" Width="250px" MaxLength="50" Visible="false"
                                                                                    Text="TEST"></asp:TextBox>
                                                                                <asp:Button ID="btnTestSave" runat="server" OnClick="btnTestSave_Click" Text="Add"
                                                                                    Width="80px" CssClass="Buttons" />
                                                                                <asp:Button ID="btnTestUpdate" runat="server" OnClick="btnTestUpdate_Click" Text="Update"
                                                                                    Width="80px" CssClass="Buttons" Visible="false" />
                                                                                <asp:Button ID="btnTestClear" runat="server" OnClick="btnTestClear_Click" Text="Clear"
                                                                                    Width="80px" CssClass="Buttons" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                    </ajaxToolkit:TabContainer>
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
                                        <asp:DataGrid ID="grdVisit" runat="server" OnDeleteCommand="grdVisit_DeleteCommand"
                                            OnPageIndexChanged="grdVisit_PageIndexChanged" OnSelectedIndexChanged="grdVisit_SelectedIndexChanged"
                                            Visible="false" Width="100%" CssClass="GridTable" AutoGenerateColumns="false">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="SZ_VISIT_ID" HeaderText="Visit ID" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_VISIT_CODE" HeaderText="Visit Code"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_VISIT_DESCRIPTION" HeaderText="Description"></asp:BoundColumn>
                                                <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
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
<body topmargin="0" style="text-align: center" bgcolor="#FBFBFB">
    <form id="frmVisit" runat="server">
        <div align="center">
            <table cellpadding="0" cellspacing="0" class="simple-table">
                <tr>
                    <td width="9%" height="18">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <div align="right">
                            <span class="top-menu"></span>
                        </div>
                    </td>
                    <td width="8%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu" style="height: 17px">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg" class="top-menu" style="height: 17px">
                        &nbsp;</td>
                    <td class="top-menu" style="height: 17px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="9%" class="top-menu">
                        &nbsp;</td>
                    <td colspan="2" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
                    <td width="8%" class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="35px" bgcolor="#000000">
                        <div align="left">
                        </div>
                        <div align="left">
                            <span class="pg-bl-usr">Billing company name</span></div>
                    </td>
                    <td width="12%" height="35px" bgcolor="#000000">
                        <div align="right">
                            <span class="usr">Admin</span></div>
                    </td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center">
                        <span class="message-text">
                            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label></span></td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="18" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th width="19%" scope="col" style="height: 29px">
                                    <div align="left">
                                        <a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div>
                                </th>
                                <th width="81%" scope="col" style="height: 29px">
                                    <div align="right">
                                        <span class="sub-menu"></span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                    <td class="top-menu" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                   
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="3" class="usercontrol">
                                    <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                                </td>
                            </tr>
                            <tr>
                                <th width="9%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                                <th scope="col" style="height: 20px">
                                    <div align="left" class="band">
                                        Visit</div>
                                </th>
                                <th width="8%" rowspan="4" align="left" valign="top" scope="col">
                                    &nbsp;</th>
                            </tr>
                            <tr>
                                <td colspan="3" height="30">
                                    <div id="ErrorDiv" style="color: red" visible="true">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th width="83%" valign="top" bgcolor="E5E5E5" scope="col" align="left">
                                 
                                   
                                </th>
                            </tr>
                            <tr>
                                <td>
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>--%>
