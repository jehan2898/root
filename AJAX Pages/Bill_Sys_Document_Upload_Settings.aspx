<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Document_Upload_Settings.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Document_Upload_Settings"
    Title="Untitled Page" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxcontrol" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">

        function check(evt) {

            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            var h = document.getElementById("<%=hfselectedNode.ClientID%>");
            if (src.checked) {
                uncheckAll();
                src.checked = true;
                var str = src.title.split("(");
                var path = str[1];
                var node = path.split(")");
                var nod_id = node[0];


                h.value = nod_id;


            } else {
                h.value = "";

            }


        }




        function uncheckAll() {
            inputs = document.getElementsByTagName("input");
            for (i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    inputs[i].checked = false;
                }
            }
        }

        function Validate() {


            if (document.getElementById("ctl00_ContentPlaceHolder1_extddlDocSource").value == 'NA') {
                alert("select document source... ");
                return false;
            }

            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDocType").value == '---Select---') {
                alert("select document type... ");
                return false;
            }

            else if (document.getElementById("ctl00_ContentPlaceHolder1_ddlDocType").value == '') {
                alert("select document type... ");
                return false;
            }
            else if (document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value == 'NA') {
                alert('Select Speciality.');
                return false;
            }

            else if (document.getElementById("<%=hfselectedNode.ClientID%>").value == "") {
                alert('Select Node.');
                return false;
            }
            else {
                return true;
            }
        }

    </script>

    <table width="100%" style="background-color: white">
        <tr>
            <td valign="top" style="width: 70%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <table style="border-right: #b5df82 1px solid; border-top: #b5df82 1px solid; border-left: #b5df82 1px solid;
                                            width: 100%; border-bottom: #b5df82 1px solid">
                                            <tbody>
                                                <tr>
                                                    <td style="font-weight: bold; font-size: small; height: 15%; background-color: #b4dd82">
                                                        &nbsp;Document Upload Setting
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="upmsg" runat="server">
                                                            <ContentTemplate>
                                                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                 <asp:UpdateProgress ID="UpdateProgress3" runat="server" >
                                                    <ProgressTemplate>
                                                        <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                            runat="Server">
                                                            <asp:Image ID="img4" runat="server" ImageUrl="Images/rotation.gif" AlternateText="Loading....."
                                                                Height="25px" Width="24px"></asp:Image>
                                                            Loading...</div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <table width="100%">
                                                            <tbody>
                                                                <tr>
                                                                    <td align="center">
                                                                        Document Source
                                                                    </td>
                                                                    <td align="center">
                                                                        Document Type
                                                                    </td>
                                                                    <td align="center">
                                                                        Specialty
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <extddl:ExtendedDropDownList ID="extddlDocSource" runat="server" Width="80%" AutoPost_back="True"
                                                                            Selected_Text="---Select---" Procedure_Name="GET_DOCUMENT_SOURCE_LIST" Flag_Key_Value="GET_DOC_SOURCE_LIST"
                                                                            Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extddlDocSource_extendDropDown_SelectedIndexChanged">
                                                                        </extddl:ExtendedDropDownList>
                                                                    </td>
                                                                    <td align="center">
                                                                        <asp:DropDownList ID="ddlDocType" runat="server" Width="80%" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td align="center">
                                                                        <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="98%" Selected_Text="---Select---"
                                                                            Procedure_Name="SP_MST_PROCEDURE_GROUP" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST"
                                                                            Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extddlSpeciality_extendDropDown_SelectedIndexChanged" AutoPost_back="True"></extddl:ExtendedDropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="3">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="3">
                                                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td valign="top" style="width: 30%;">
            <asp:UpdatePanel ID="upnl1" runat="server">
            <ContentTemplate>
            
                <asp:Panel runat="server" ScrollBars="Both" Height="450px" Width="100%" ID="contentPanel">
                    <asp:TreeView runat="server" Font-Bold="False" Font-Size="Small" Height="450px" Width="100%"
                        ID="tvwmenu" OnTreeNodePopulate="Node_Populate">
                        <Nodes>
                            <asp:TreeNode PopulateOnDemand="True" SelectAction="Expand" Text="Document Manager"
                                Value="0"></asp:TreeNode>
                        </Nodes>
                    </asp:TreeView>
                </asp:Panel>
                </ContentTemplate>
                   <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="ddlDocType"  />
                 
                </Triggers>
            </asp:UpdatePanel>
            </td>
        </tr>
        <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox><asp:HiddenField
            runat="server" ID="hfselectedNode"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hfOrder"></asp:HiddenField>
    </table>
</asp:Content>
