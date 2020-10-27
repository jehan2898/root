<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_GroupProcedureCode.aspx.cs" Inherits="Bill_Sys_GroupProcedureCode" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var arrDGCode = [];
            $(document).on("click", '.CheckAdd', function () {
                debugger;
                var data = $(this).attr('data-val');
                var tc = $(this).children().first();
                var dataArray = data.split('~');
                var ProcedureID = dataArray[0];
                var ProcedureCode = dataArray[1];
                var ProcedureDescription = dataArray[2];
                var objTableID = $('[id$=divDiagnosis]');


                if ($('[id$=hdnProcedureCode]').val() != "" && arrDGCode.length == 0) {

                    arrspilt = $('[id$=hdnProcedureCode]').val().split("~--");
                    for (var i = 0; i < arrspilt.length; i++) {

                        var AData = arrspilt[i];

                        if (AData != "") {
                            arrDGCode.push(AData + "~--");

                        }
                    }



                }

                if (tc.prop("checked")) {
                    var objTableID = $('[id$=divGroupProcedureCode]')
                   

                    var DGPush = ProcedureID + '~';
                    if ($('[id$=hdnProcedureCode]').val().indexOf(ProcedureID) == -1) {
                        var str = "";
                        str += "<table><tr><td>&nbsp;&nbsp;&nbsp<a href='#' class='btnDeleteDignosis'><img src='Images/icon_delete.png' /></a></td>";
                        str += "<td>&nbsp;&nbsp;&nbsp;" + ProcedureCode + "</td>";
                        str += "<td>&nbsp;&nbsp;&nbsp;" + ProcedureDescription + "</td>";
                        str += "<td style='display:none;'>&nbsp;&nbsp;&nbsp;" + ProcedureID + "</td>";

                        arrDGCode.push(DGPush + "--");
                        objTableID[0].innerHTML += str;
                        $('[id$=hdnProcedureCode]').val(arrDGCode);
                    }


                }





            });


            $(document).on('click', '.btnDeleteDignosis', function () {
                debugger;
                var dgid = $(this).closest('td').next('td').next('td').next('td').text();


                if ($('[id$=hdnProcedureCode]').val() != "" && arrDGCode.length == 0) {

                    arrspilt = $('[id$=hdnProcedureCode]').val().split("~--");
                    for (var i = 0; i < arrspilt.length; i++) {

                        var AData = arrspilt[i];

                        if (AData != "") {
                            arrDGCode.push(AData + "~--");

                        }
                    }



                }

                for (var i = 0; i < arrDGCode.length; i++) {

                    var arrDGDelete = arrDGCode[i].split("~");

                    if ((arrDGDelete[0].replace("--", "")) == $.trim(dgid)) {

                        var deleteData = arrDGCode[i];
                        arrDGCode = $.grep(arrDGCode, function (n) {
                            return n != deleteData;
                        });


                    }

                }
                $(this).closest('tr').remove();
                $('[id$=hdnProcedureCode]').val("");
                
                $('[id$=hdnProcedureCode]').val(arrDGCode);

               



            });

        })
     
    </script>

    <script type="text/javascript">

        function CloseGridLookup() {
            gridLookup.ConfirmCurrentSelection();
            gridLookup.HideDropDown();
            gridLookup.Focus();
        }

        function Clear() {
            //alert("call");
            document.getElementById("<%=txtGroupName.ClientID%>").value = '';
            document.getElementById("<%=txtGroupAmount.ClientID %>").value = '';
            document.getElementById("<%=hdnProcedureCode.ClientID %>").value = '';
            var divproc = document.getElementById("<%=divGroupProcedureCode.ClientID %>");
            document.getElementById("<%=btnAssign.ClientID %>").disabled = false;
            document.getElementById("<%=btnUpdate.ClientID %>").disabled = true;
            divproc.innerHTML = "<div style='margin-bottom: 10px;background-color:#B4DD82;font-size:medium'>Selected codes</div>";
            var f = document.getElementById("<%= Grid.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = false;
                }
            }
        }
    </script>
    <script type="text/javascript">
       
    </script>
    <script type="text/javascript" src="validation.js"></script>
    <script type="text/javascript">
        function ConfirmDelete() {
            var msg = "Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function ascii_value(c) {
            c = c.charAt(0);
            var i;
            for (i = 0; i < 256; ++i) {
                var h = i.toString(16);
                if (h.length == 1)
                    h = "0" + h;
                h = "%" + h;
                h = unescape(h);
                if (h == c)
                    break;
            }
            return i;
        }

        function clickButton1(e, charis) {
            var keychar;
            if (navigator.appName.indexOf("Netscape") > (-1)) {
                var key = ascii_value(charis);
                if (e.charCode == key || e.charCode == 0) {
                    return true;
                } else {
                    if (e.charCode < 48 || e.charCode > 57) {
                        return false;
                    }
                }
            }
            if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                var key = ""
                if (charis != "") {
                    key = ascii_value(charis);
                }
                if (event.keyCode == key) {
                    return true;
                }
                else {
                    if (event.keyCode < 48 || event.keyCode > 57) {
                        return false;
                    }
                }
            }
        }
        function ShowChildGrid(obj) {

            var div = document.getElementById(obj);

            div.style.display = 'block';
        }
        function HideChildGrid(obj) {
            var div = document.getElementById(obj);
            div.style.display = 'none';
        }

        function OnSave() {
            debugger;
            var sDate = document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupName');
            var specialty = document.getElementById('_ctl0_ContentPlaceHolder1_extddlProCodeGroup');
            var Amount = document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupAmount');

            var SeletedDGCodes = document.getElementById('<%=hdnProcedureCode.ClientID%>');
           
            if (specialty.value == "NA") {
                alert('Please select the specialty to proceed');
                return false;
            }
            else if (sDate.value == "") {
                alert('Please select the Group Name to proceed');
                return false;
            }

            else {


                if (SeletedDGCodes.value == "") {
                    alert("Please Select Procedure Code");
                    return false;
                }
                else {
                    return true;
                }
            }

        }

        function OnDelete() {

            var f = document.getElementById('_ctl0_ContentPlaceHolder1_GrdProcedureGroup');
            var bfFlag = false;
            
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).name.indexOf('chkRemove') != -1) {
                    if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                        if (f.getElementsByTagName("input").item(i).checked != false) {

                            bfFlag = true;
                            break;
                        }
                    }
                }
            }

            if (bfFlag == false) {
                alert('select at least one Group');
                return false;
            }
        }

        function SelectAll(ival) {
            var f = document.getElementById("<%= GrdProcedureGroup.ClientID %>");
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    f.getElementsByTagName("input").item(i).checked = ival;
                }
            }
        }


    </script>
    <script type="text/javascript">
        function CloseGridLookup() {
            gridLookup.ConfirmCurrentSelection();
            gridLookup.HideDropDown();
            gridLookup.Focus();
        }
    </script>
    <%--<script type="text/javascript">   
// for check all checkbox   
        function CheckAll(Checkbox) {   
            var GridVwHeaderCheckbox = document.getElementById("<%=grdGroupProcedureCode.ClientID %>");   
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {   
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;   
            }   
        }   
    </script> --%>
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
                                                    Specialty : &nbsp;
                                                </td>
                                                <td style="width: 35%; height: 18px; text-align: left" class="ContentLabel">
                                                    <cc1:ExtendedDropDownList ID="extddlProCodeGroup" AutoPost_back="true" runat="server"
                                                        Width="90%" Connection_Key="Connection_String" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                        Selected_Text="--- Select ---" Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" OnextendDropDown_SelectedIndexChanged="extddlProCodeGroup_extendDropDown_SelectedIndexChanged">
                                                    </cc1:ExtendedDropDownList>
                                                </td>
                                                <%--<td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Group Name:
                                                </td>
                                                <td style="width: 35%; height: 18px;" class="ContentLabel">
                                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="textboxCSS" MaxLength="50"
                                                        Width="80%"></asp:TextBox>
                                                </td>--%>
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
                                                    <asp:Button ID="btnAssign" runat="server" Text="Assign" Width="80px" OnClick="btnAssign_Click" />
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
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
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
                                            <div style="height: 200px; width: 100%; overflow-y: scroll">
                                                <br />
                                                <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" AutoGenerateColumns="False"
                                                    HeaderStyle-BackColor="#B4DD82" KeyFieldName="SZ_PROCEDURE_ID" Width="100%" OnLoad="Grid_Load">
                                                    <Columns>
                                                        <dx:GridViewDataColumn Width="12%" CellStyle-HorizontalAlign="Center">
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                            <DataItemTemplate>
                                                                <asp:CheckBox ID="chkAssign" runat="server" class="CheckAdd" data-val='<%# Eval("SZ_PROCEDURE_ID")+"~"+ Eval("SZ_PROCEDURE_CODE")+"~"+ Eval("SZ_CODE_DESCRIPTION") %>' />
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataTextColumn FieldName="SZ_PROCEDURE_ID" Caption="PROCEDURE ID" Visible="false" />
                                                        <dx:GridViewDataTextColumn FieldName="SZ_PROCEDURE_CODE" Caption="Procedure Code" />
                                                        <dx:GridViewDataTextColumn FieldName="SZ_CODE_DESCRIPTION" Caption="Procedure Description" />
                                                        <dx:GridViewDataTextColumn FieldName="FLT_AMOUNT" Caption="Amount" Visible="false" />
                                                    </Columns>
                                                    <Settings ShowFilterRow="True" />
                                                </dx:ASPxGridView>
                                                <asp:DataGrid ID="grdGroupProcedureCode" runat="server" Width="100%" HeaderStyle-BackColor="#B4DD82"
                                                    AutoGenerateColumns="false" OnPageIndexChanged="grdGroupProcedureCode_PageIndexChanged"
                                                    DataKeyField="SZ_PROCEDURE_ID">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <%--<asp:CheckBox ID="chkAssign" runat="server"  class="CheckAdd" data-val='<%# Eval("SZ_PROCEDURE_ID")+","+ Eval("SZ_PROCEDURE_CODE")+","+ Eval("SZ_CODE_DESCRIPTION") %>' />--%>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="SZ_PROCEDURE_ID" HeaderText="PROCEDURE ID" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_CODE_DESCRIPTION" HeaderText="Procedure Description">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="FLT_AMOUNT" HeaderText="Amount" DataFormatString="{0:0.00}"
                                                            Visible="false"></asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td style="vertical-align: top; width: 25%" align="left">
                                            <div id="divGroupProcedureCode" runat="server" style="padding-left: 30px" align="left"
                                                width="80%" class="dvGroup"> 
                                                <div class="page-title" style="padding-left: 15px; margin-bottom: 10px; background-color: #B4DD82;
                                                    font-size: medium">
                                                    Selected codes</div>
                                            </div>
                                        </td>
                                        <td style="width: 6%; vertical-align: top">
                                        </td>
                                        <td style="width: 50%; vertical-align: top">
                                            <div style="width: 80%" class="dvGroup">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="vertical-align: middle; width: 100%;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="vertical-align: middle; width: 30%" align="left">
                                                                        <asp:Label ID="lbl_search" runat="server" Text="Search : " Visible="false"></asp:Label><gridsearch:XGridSearchTextBox
                                                                            ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input"
                                                                            Visible="false">
                                                                        </gridsearch:XGridSearchTextBox>
                                                                    </td>
                                                                    <td style="vertical-align: middle; width: 30%" align="left">
                                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanel1">
                                                                            <ProgressTemplate>
                                                                                <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                    runat="Server">
                                                                                    <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                        Height="25px" Width="24px"></asp:Image>
                                                                                    Loading...
                                                                                </div>
                                                                            </ProgressTemplate>
                                                                        </asp:UpdateProgress>
                                                                    </td>
                                                                    <td style="width: 50%" align="right">
                                                                        <asp:Label ID="lbl_record_count" runat="server" Text="Record Count : " Visible="false"></asp:Label>
                                                                        <%= this.GrdProcedureGroup.RecordCount%>
                                                                        |<asp:Label ID="lbl_Page_Count" runat="server" Text="Page Count : " Visible="false"></asp:Label>
                                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server" Visible="false">
                                                                        </gridpagination:XGridPaginationDropDown>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <xgrid:XGridViewControl ID="GrdProcedureGroup" runat="server" Width="100%" CssClass="mGrid"
                                                                        DataKeyNames="SZ_PROCEDURE_GROUP_NAME,FLT_AMOUNT" MouseOverColor="0, 153, 153"
                                                                        EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                        AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" XGridKey="BindProcedure_Group"
                                                                        PageRowCount="50" PagerStyle-CssClass="pgr" AllowSorting="true" AutoGenerateColumns="false"
                                                                        GridLines="None" OnRowCommand="GrdProcedureGroup_RowCommand">
                                                                        <AlternatingRowStyle BackColor="#EEEEEE" />
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <asp:TemplateField HeaderText="Select" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10px">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        CommandName="Select" CausesValidation="false"> </asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                <ItemStyle HorizontalAlign="Left" Width="10px"></ItemStyle>
                                                                            </asp:TemplateField>
                                                                            <%--1--%>
                                                                            <asp:TemplateField HeaderText="Group Name" SortExpression="" ItemStyle-Width="200px">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkP" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="PLS" Font-Size="15px" Text="+" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                                                    <asp:LinkButton ID="lnkM" Font-Underline="false" runat="server" CausesValidation="false"
                                                                                        CommandName="MNS" Text="-" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                        Font-Size="15px" Visible="false"></asp:LinkButton>
                                                                                    <%# DataBinder.Eval(Container, "DataItem.SZ_PROCEDURE_GROUP_NAME")%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="200px" />
                                                                            </asp:TemplateField>
                                                                            <%--2--%>
                                                                            <asp:BoundField DataField="FLT_AMOUNT" HeaderText="Amount" ItemStyle-Width="110px">
                                                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                            </asp:BoundField>
                                                                            <%--3--%>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%">
                                                                                            <div id="div<%# Eval("SZ_PROCEDURE_GROUP_NAME") %>" style="display: none; position: relative;
                                                                                                left: 25px;">
                                                                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                                                                                    Width="50%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                                                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                                    <Columns>
                                                                                                        <asp:BoundField DataField="SZ_PROCEDURE_ID" HeaderText="Procedure ID" Visible="false">
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="SZ_PROCEDURE_CODE" HeaderText="Procedure Code" HeaderStyle-HorizontalAlign="Left">
                                                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="SZ_CODE_DESCRIPTION" HeaderText="Description" HeaderStyle-HorizontalAlign="Left">
                                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                        <asp:BoundField DataField="FLT_AMOUNT" HeaderText="Amount" DataFormatString="{0:C}">
                                                                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                                        </asp:BoundField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--4--%>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" ToolTip="Select All" onclick="javascript:SelectAll(this.checked);" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkRemove" runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                        <PagerStyle CssClass="pgr" />
                                                                    </xgrid:XGridViewControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="ContentLabel" colspan="2" style="text-align: left">
                                                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="80px" OnClick="btnRemove_Click"
                                                                        Enabled="true" />
                                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" OnClick="btnUpdate_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
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
</asp:Content>
