<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_GroupProcedureCodeNew.aspx.cs" Inherits="Bill_Sys_GroupProcedureCodeNew" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <link rel="stylesheet" type="text/css" href="media/css/jquery.dataTables.css">
	<link rel="stylesheet" type="text/css" href="resources1/syntax/shCore.css">
	<link rel="stylesheet" type="text/css" href="resources1/demo.css">
	<style type="text/css" class="init">
	
	</style>
	<script type="text/javascript" language="javascript" src="//code.jquery.com/jquery-1.12.3.min.js">
	</script>
	<script type="text/javascript" language="javascript" src="media/js/jquery.dataTables.js">
	</script>
	<script type="text/javascript" language="javascript" src="resources1/syntax/shCore.js">
	</script>
<script type="text/javascript" language="javascript" >
    $.ajaxSetup({
        cache: false
    });

    var table = $('#employee-grid').DataTable({
        "filter": false,
        "pagingType": "simple_numbers",
        "orderClasses": false,
        "order": [[0, "asc"]],
        "info": false,
        "scrollY": "450px",
        "scrollCollapse": true,
        "bProcessing": true,
        "bServerSide": true,
        "sAjaxSource": "Bill_Sys_GroupProcedureCodeNew.aspx/DataSource",
        "fnServerData": function (sSource, aoData, fnCallback) {
            aoData.push({ "CodeGroup": "PG000000000000001063", "CompanyID": "CO000000000000000135", "Flag": "LIST" });
            $.ajax({
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "type": "GET",
                "url": sSource,
                "data": aoData,
                "success": function (msg) {
                    debugger;
                    var json = jQuery.parseJSON(msg.d);
                    fnCallback(json);
                    $("#tblData").show();
                },
                error: function (xhr, textStatus, error) {
                    debugger;
                    if (typeof console == "object") {
                        console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                    }
                }
            });
        },
        fnDrawCallback: function () {
            $('.image-details').bind("click", showDetails);
        }
    });

    function showDetails() {
        //so something funky with the data
    }
</script>


	</script>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 76%;
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
                                    <td style="width: 100%">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px; text-align: left">
                                                    Speciality : &nbsp;
                                                </td>
                                                <td style="width: 35%; height: 18px; text-align: left" class="ContentLabel">
                                             <cc1:ExtendedDropDownList ID="extddlProCodeGroup" AutoPost_back="true" runat="server"
                                                        Width="90%" Connection_Key="Connection_String" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                        Selected_Text="--- Select ---" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" OnextendDropDown_SelectedIndexChanged="extddlProCodeGroup_extendDropDown_SelectedIndexChanged">
                                                    </cc1:ExtendedDropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px; text-align: left">
                                                    Group Name :
                                                </td>
                                                <td style="width: 35%; height: 18px; text-align: left" class="ContentLabel">
                                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="textboxCSS" MaxLength="50"
                                                        Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; text-align: left">
                                                    Amount :
                                                </td>
                                                <td style="width: 45%; text-align: left" class="ContentLabel">
                                                    <asp:TextBox ID="txtGroupAmount" runat="server" CssClass="textboxCSS" Width="80%"
                                                        MaxLength="50" onkeypress="return clickButton1(event,'.')"></asp:TextBox>
                                                </td>
                                                <td style="width: 5%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td style="font-weight: bold">
                                                    <font color="red">(This will override individual procedure amount)</font>
                                                </td>
                                            </tr>
                                            <tr style="height: 10px">
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="2" style="text-align: center">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnAssign" runat="server" Text="Assign" Width="80px"  />
                                                 <input style="width: 80px" id="btnClear1" type="button" value="Clear" onclick="Clear();"
         runat="server" visible="true" />
                                                    <%--<asp:Button ID="btnRemove" runat="server" Text="Remove" Width="80px" OnClick="btnRemove_Click"
                                                        Enabled="false" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                      <table id="employee-grid"  cellpadding="0" cellspacing="0" border="0" class="display" width="100%">
        <thead>
            <tr>
                <th>Employee name</th>
                <th>Salary</th>
                <th>Age</th>
            </tr>
        </thead>
</table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                         <asp:Button ID="btnAjax" runat="server" OnClientClick="callAjaxMethod(event)"  Text="Call method using Ajax" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                    </td>
                                </tr>
                            </table>
                            <div id="ProcedureDiv" runat="server" visible="false">
                                <table>
                                    <style>
                                        .trGroup {
                                            height:200px;
                                        }
                                                         .dvGroup {
                                            height:200px;
                                            overflow:scroll;
                                        }
                                    </style>
                                    <tr class="trGroup">
                                        <td style="width: 35%">
              
                                        </td>
                                        <td style="vertical-align: top; width: 25%" align="left">

                                        </td>
                                        <td style="width: 6%; vertical-align: top">
                                        </td>
                                        <td style="width: 50%; vertical-align: top">

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td class="RightCenter" style="width: 10px; height: 100%;">
                        </td>
                    </tr>
                    <tr>
                        <td class="LeftBottom">
                            <div>
                                <asp:TextBox ID="txt_ProcedureID" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txt_CompanyID" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txt_ProcedureCode" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txt_CodeDescription" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txt_ProcedureGroupID" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txt_GroupName" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                <asp:TextBox ID="txt_GroupId" runat="server" Width="80%" Visible="false"></asp:TextBox>
                                <asp:HiddenField ID="hdnProcedureCode" runat="server"></asp:HiddenField>
                            </div>
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
    <script src="js/Common.js"></script>
</asp:Content>
