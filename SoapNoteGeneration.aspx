<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="SoapNoteGeneration.aspx.cs" Inherits="SoapNoteGeneration" %>


<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" src="validation.js"></script>
    <script type="text/javascript">
        function Clear() {

            var strdoctor = document.getElementById("_ctl0_ContentPlaceHolder1_extddlDoctor");
            if (strdoctor != null) {
                document.getElementById("_ctl0_ContentPlaceHolder1_extddlDoctor").value = "NA";
            }

        }
        function SelectAll(ival) {
            var f = document.getElementById("<%= grdSoapNote.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length ; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }
                }
            }
        }

        function validate() {
            if (document.getElementById("_ctl0_ContentPlaceHolder1_extddlDoctor").value == 'NA') {



                alert('Please Select the Doctor');
                return false;
            }
        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>

    <table width="100%" border="0" style="height: 224px">
        <tr>
            <td style="background-color: White; height: 100%">
                <table style="width: 20%">
                    <tr>

                        <td style="height: 80%; width: 448px;">
                            <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <%-- <tr>
                                    <td>
                                        <div style="vertical-align: top" align="left">
                                            <asp:LinkButton Style="visibility: hidden" ID="Button1" runat="server"></asp:LinkButton>
                                            <a style="vertical-align: top; cursor: pointer; height: 240px" id="hlnkShowSearch"
                                                class="Buttons" title="Quick Search" runat="server" visible="false"></a>
                                        </div>
                                    </td>
                                </tr>--%>
                            </table>

                            <%--       <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')">--%>
                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 300px; height: 221px; border: 1px solid #B5DF82;">

                                <tr>
                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2">
                                        <b class="txt3"></b>
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                                        <table style="width: 711px; height: 38px;">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" align="left" style="width: 87px" valign="baseline">Doctor Name:
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch3" valign="middle" colspan="2">
                                                    <cc1:ExtendedDropDownList ID="extddlDoctor" runat="server" Width="240px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GETDOCTORLIST" Procedure_Name="SP_MST_DOCTOR" Selected_Text="---Select---" />
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="FileUploadtoserver" runat="server" />

                                                    <br />
                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Green" Text=""></asp:Label>
                                                    <%--<asp:Label runat="server" id="StatusLabel" text="" />--%>
                                                </td>

                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 711px; height: 77px;">
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" align="left" style="width: 87px" valign="baseline">Location:
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    <asp:RadioButtonList ID="rdlhederfooter" runat="server" RepeatDirection="Horizontal" Width="30%">
                                                        <asp:ListItem Value="0" Selected="True">Header</asp:ListItem>
                                                        <asp:ListItem Value="1">Footer</asp:ListItem>
                                                        <asp:ListItem Value="2">Both</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" align="left" style="width: 87px" valign="bottom">Header Position:
                                                </td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    <asp:RadioButtonList ID="rdlPosition" runat="server" RepeatDirection="Horizontal" Width="30%">
                                                        <asp:ListItem Value="0">Right</asp:ListItem>
                                                        <asp:ListItem Value="1" Selected="True">Center</asp:ListItem>
                                                        <asp:ListItem Value="2">Left</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td-widget-bc-search-desc-ch1" align="left" style="width: 87px" valign="bottom">Footer Position:</td>
                                                <td class="td-widget-bc-search-desc-ch1">
                                                    <asp:RadioButtonList ID="rdlFooterPosition" runat="server" RepeatDirection="Horizontal" Width="30%">
                                                        <asp:ListItem Value="0">Right</asp:ListItem>
                                                        <asp:ListItem Value="1" Selected="True">Center</asp:ListItem>
                                                        <asp:ListItem Value="2">Left</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="height: 61px">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnUpload" Text="Upload & Generate" Width="130px" OnClick="btnUpload_Click" />
                                                <input style="width: 80px" id="btnClear" onclick="Clear();" type="button" value="Clear" />

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>




                        </td>

                    </tr>

                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>

                </table>
                <table width="100%">
                    <tr>
                        <td></td>
                    </tr>
                    <br />
                    <tr>
                        <td style="width: 100%; text-align: right; height: 44px;">

                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 700px; overflow-y: scroll; overflow-x: scroll;">
                                <asp:DataGrid ID="grdSoapNote" runat="server" Width="100%" CssClass="GridTable"
                                    AutoGenerateColumns="false">
                                    <ItemStyle CssClass="GridRow" />
                                    <AlternatingItemStyle BackColor="#c5c1c1" />
                                    <Columns>

                                        <%-- 0 --%>
                                        <asp:BoundColumn DataField="I_ID" HeaderText="I_ID" Visible="false"></asp:BoundColumn>
                                        <%-- 1 --%>
                                        <%--<asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="Company Id" Visible="false" FooterStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                        <%-- 2 --%>
                                        <%--<asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor ID" Visible="false" FooterStyle-HorizontalAlign="Center"></asp:BoundColumn>--%>
                                        <%-- 3 --%>
                                        <asp:BoundColumn DataField="DoctorName" HeaderText="Doctor Name" Visible="true" FooterStyle-HorizontalAlign="Center"></asp:BoundColumn>

                                        <%-- 4 --%>
                                        <asp:BoundColumn DataField="TEMPLATE_NAME" HeaderText="File Name" Visible="true" FooterStyle-HorizontalAlign="Center"></asp:BoundColumn>

                                        <%-- 5 --%>
                                        <asp:BoundColumn DataField="DT_CREATED" HeaderText="Date" Visible="true" FooterStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                        <%-- 6 --%>


                                        <asp:TemplateColumn>

                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" Target="_new" NavigateUrl='<%# Eval("Link_url") %>' Text="Open" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);" ToolTip="Select All" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkDelete" runat="server" />
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

        </tr>
    </table>

</asp:Content>

