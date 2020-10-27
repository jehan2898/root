<%@ page language="C#" autoeventwireup="true" CodeFile="bill_sys_add_shedual_visit.aspx.cs" inherits="AJAX_Pages_Bill_sys_Add_Shedual_Visit" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral,PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
    <link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />

    <script type="text/javascript">
        function SelectAllDpctor(ival) {
            var f = document.getElementById('carTabPage_grdDoctor');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }

        function SelectAllTrans(ival) {
            var f = document.getElementById('carTabPage_grdTransport');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }



                }


            }
        }
          
          
          
           function SelectAllVisits(ival) {
            var f = document.getElementById('carTabPage_grdShowVisit');
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {

                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
                    }

                }
            }
        }
        function ValidateDocotr(ival) 
       {
            var hdSeting1 = document.getElementById('hdSeting');
          
           if(hdSeting1.value=="1")
           {    
               hdEnable1 = document.getElementById('hdEnable');
               if(hdEnable1.value=="0")
               {  hdEnable1.value="1";
                  var button = document.getElementById('btnCls');
                 button.click();
               
                  return true;
               }else
               {
                   var f = document.getElementById('carTabPage_grdDoctor');
                       var str = 1;
                       var chkval='0';
                          for (var i = 0; i < f.getElementsByTagName("input").length; i++) 
                          {
                              if (f.getElementsByTagName("input").item(i).type == "checkbox") 
                              {
                                if(f.getElementsByTagName("input").item(i).checked)
                                {
                                 chkval='1';
                                  break;
                                }
                              }
                          }
                          
                          if(chkval=='0')
                          {
                            ClearGrid();
                          }
               }
                
            }
        }
          
        function showTrans() {


            var chk = document.getElementById('carTabPage_chkTransportation');


            if (chk.checked) {

                var f = document.getElementById('carTabPage_divTrans');

                f.style.visibility = 'visible'
            } else {

                var f = document.getElementById('carTabPage_divTrans');

                f.style.visibility = 'hidden'
            }

        }


        function Validate() {
            var f = document.getElementById('carTabPage_grdDoctor');
            var flag = 0;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {

                        flag = 1;
                        break;
                    }
                }
            }

            if (flag == 0) {
                alert('Please select Doctor.');
                return false;
            } else {
                var d = document.getElementById('carTabPage_txtEventDate');

                if (d.value == "") {

                    alert('Please select visit Date.');
                    return false;
                } else {
                    var t = document.getElementById('carTabPage_ddlHours_event');
                    if (t.value == '00') {
                        alert('please select visit time');
                        return false;
                    } else{
                    
                             if(document.getElementById("carTabPage_extddlVisitType").value== 'NA') 
                                {
                                    alert('Please Select Visit Type');
                                    return false;
                                }
                                else{
                                     return true;
                                    }
                            }
            }}
        }
        function ValidateTrans() {

            if (document.getElementById('carTabPage_txtFromDate').value == "") {
                alert('Plese enter Date');
                return false;
            } else {

                if (document.getElementById('carTabPage_ddlHours').value == "00") {
                    alert('Plese select Time');
                    return false;
                } else {



                    if (document.getElementById('carTabPage_extddlTransport').value == "NA") {
                        alert('Plese select Transport Name');
                        return false;
                    } else {
                        return true;
                    }

                }

            }


        }


        function ValidateDelete() {
            
            var f = document.getElementById('carTabPage_grdTransport');
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {

                        if (confirm("Are you sure to continue for  delete?")) {
                            return true;
                        }

                        else {
                            return false;
                        }

                    }
                }
            }

            alert('Please select Recored.');
            return false;
        }
        
        
          function DeleteVisit() {
            
            var f = document.getElementById('carTabPage_grdShowVisit');
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                    if (f.getElementsByTagName("input").item(i).checked != false) {

                        if (confirm("Are you sure to continue for  delete?")) {
                            return true;
                        }

                        else {
                            return false;
                        }

                    }
                }
            }

            alert('Please select Recored.');
            return false;
        }
        
          
   function ClearGrid() {
            
             var val = navigator.userAgent.toLowerCase();
             if(val.indexOf("msie") > -1)
             {
               
                var button1 = document.getElementById('btnClear1');
                 button1.click();
              
             }else
             {
                 var f = document.getElementById("carTabPage_grdDoctor");
                 var str = 1;
                    for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                        if (f.getElementsByTagName("input").item(i).type == "checkbox") {
                            f.getElementsByTagName("input").item(i).checked = false;
                             f.getElementsByTagName("input").item(i).disabled = false;                      
                    
                           }
                        }
              var visitype=document.getElementById("carTabPage_extddlVisitType");
              visitype.disabled = false;
              visitype.value='NA';
                var chk = document.getElementById('carTabPage_chkAddToDoctor'); 
                chk.disabled = false;
                chk.checked=true;
             
              var  hdEnable1 = document.getElementById('hdEnable');
              hdEnable1.value="0";
              return true;
           }   
        }
        
     
       

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
        </asp:ScriptManager>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100%;">
                        <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="0" EnableHierarchyRecreation="True"
                            OnActiveTabChanged="carTabPage_ActiveTabChanged" AutoPostBack="true" Width="100%"
                            Height="600">
                            <TabPages>
                                <dx:TabPage Text="Add">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 70%; vertical-align: top">
                                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-family: Arial; font-size: 12px; text-align: left;" valign="top" align="left">
                                                                </td>
                                                                <td valign="top" align="left">
                                                                    <input id="btnClear" type="button" style="width: 50PX" onclick="javascript:ClearGrid();"
                                                                        name="Clear" value="Clear" visible="true" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-family: Arial; font-size: 12px; text-align: left;" valign="top" align="left">
                                                                    Doctor
                                                                </td>
                                                                <td style="font-family: Arial; font-size: 12px; text-align: left;" valign="top" align="left">
                                                                    <dx:ASPxGridView ID="grdDoctor" runat="server" KeyFieldName="CODE" AutoGenerateColumns="false"
                                                                        Width="100%" SettingsPager-PageSize="1000" Settings-VerticalScrollableHeight="120">
                                                                        <Columns>
                                                                            <%--0--%>
                                                                            <dx:GridViewDataColumn Caption="All" Width="12%" CellStyle-HorizontalAlign="Center">
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllDpctor(this.checked);"
                                                                                        Text="All" ToolTip="Select All" />
                                                                                </HeaderTemplate>
                                                                                <DataItemTemplate>
                                                                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="javascript:ValidateDocotr(this.checked);" />
                                                                                </DataItemTemplate>
                                                                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                                            </dx:GridViewDataColumn>
                                                                            <%--1--%>
                                                                            <dx:GridViewDataColumn FieldName="CODE" Caption="CODE" HeaderStyle-HorizontalAlign="Center"
                                                                                Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                            <%--2--%>
                                                                            <dx:GridViewDataColumn FieldName="Specialty" Caption="Specialty" HeaderStyle-HorizontalAlign="Center"
                                                                                Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                            <%--3--%>
                                                                            <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="Doctor" HeaderStyle-HorizontalAlign="Center">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                            <%--4--%>
                                                                            <dx:GridViewDataColumn FieldName="SZ_USER_ID" Caption="SZ_USER_ID" HeaderStyle-HorizontalAlign="Center"
                                                                                Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                            <%--5--%>
                                                                            <dx:GridViewDataColumn FieldName="GROUP_CODE" Caption="GROUP_CODE" HeaderStyle-HorizontalAlign="Center"
                                                                                Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                            <%--6--%>
                                                                            <dx:GridViewDataColumn FieldName="DocName" Caption="DocName" HeaderStyle-HorizontalAlign="Center"
                                                                                Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                            <%--7--%>
                                                                            <dx:GridViewDataColumn FieldName="BT_NOT_HAVE_NOTES" Caption="BT_NOT_HAVE_NOTES"
                                                                                HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                            <%--8--%>
                                                                            <dx:GridViewDataColumn FieldName="IS_HAVE_LOGIN" Caption="IS_HAVE_LOGIN" HeaderStyle-HorizontalAlign="Center"
                                                                                Visible="false">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            </dx:GridViewDataColumn>
                                                                        </Columns>
                                                                        <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                                                                        <SettingsBehavior AllowFocusedRow="false" />
                                                                        <SettingsBehavior AllowSelectByRowClick="true" />
                                                                        <SettingsPager Position="Bottom" />
                                                                        <%-- <SettingsCustomizationWindow Height="100px"></SettingsCustomizationWindow>--%>
                                                                    </dx:ASPxGridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Date
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtEventDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                        MaxLength="10"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEventDate"
                                                                        PopupButtonID="ImageButton1" />
                                                                </td>
                                                                <td>
                                                                    Time
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlHours_event" runat="server" Width="60px">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlMinutes_event" runat="server" Width="60px">
                                                                    </asp:DropDownList>
                                                                    <asp:DropDownList ID="ddlTime_event" runat="server" Width="60px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Visit Type:
                                                                </td>
                                                                <td>
                                                                    <extddl:ExtendedDropDownList ID="extddlVisitType" runat="server" Width="200px" AutoPost_back="false"
                                                                        Selected_Text="---Select---" Procedure_Name="SP_GET_VISIT_TYPE_LIST" Flag_Key_Value="GET_VISIT_TYPE"
                                                                        Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Notes
                                                                </td>
                                                                <td colspan="3">
                                                                    <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="85%" Height="52px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td colspan="3" align="left">
                                                                    <asp:CheckBox ID="chkAddToDoctor" runat="server" Text="Can be finalized only by the doctor" />
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td colspan="3" align="left">
                                                                    <asp:CheckBox ID="chkTransportation" runat="server" Text="Transport" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="left" style="width: 100%">
                                                                    <div id="divTrans" runat="server" style="width: 100%; visibility: hidden; overflow: scroll;
                                                                        height: 400px;">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    Transport Name
                                                                                </td>
                                                                                <td>
                                                                                    <extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="110px" Connection_Key="Connection_String"
                                                                                        Flag_Key_Value="GET_TRANSPORT_LIST" Procedure_Name="SP_MST_TRANSPOTATION" Selected_Text="---Select---">
                                                                                    </extddl:ExtendedDropDownList>
                                                                                </td>
                                                                                <td>
                                                                                    Date
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                                                                        MaxLength="10"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                                                        PopupButtonID="imgbtnFromDate" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Time
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlHours" runat="server" Width="60px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:DropDownList ID="ddlMinutes" runat="server" Width="60px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:DropDownList ID="ddlTime" runat="server" Width="60px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="4">
                                                                                    <asp:Button ID="btnTransportsave" runat="server" Text="Save" OnClick="btnTransportsave_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right" colspan="4">
                                                                                    <asp:Button ID="btnTransportdelete" runat="server" Text="Delete" OnClick="btnTransportdelete_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="4" align="center">
                                                                                    <div style="overflow: auto; height: 10%; width: 100%;">
                                                                                        <asp:DataGrid ID="grdTransport" runat="server" Width="100%" CssClass="GridTable"
                                                                                            AutoGenerateColumns="false" DataKeyField="I_TRANS_ID">
                                                                                            <FooterStyle />
                                                                                            <SelectedItemStyle />
                                                                                            <PagerStyle />
                                                                                            <AlternatingItemStyle />
                                                                                            <ItemStyle CssClass="GridRow" />
                                                                                            <Columns>
                                                                                                <asp:BoundColumn DataField="I_TRANS_ID" HeaderText="I_TRANS_ID" Visible="false"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_NAME" HeaderText="Transport Name">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TRANS_ID" HeaderText="SZ_TRANS_ID"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="DT_TRANS_DATE" HeaderText="Transport Date"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TRANS_TIME" HeaderText="Time" Visible="false"></asp:BoundColumn>
                                                                                                <asp:BoundColumn DataField="SZ_TRANS_TIME_TYPE" HeaderText="Time Type" Visible="false">
                                                                                                </asp:BoundColumn>
                                                                                                <asp:TemplateColumn HeaderText="Delete">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllTrans(this.checked);"
                                                                                                            ToolTip="Select All" />
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateColumn>
                                                                                            </Columns>
                                                                                            <HeaderStyle CssClass="GridHeader1" />
                                                                                        </asp:DataGrid>
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 30%; vertical-align: top;">
                                                        <b>Patient Info</b>
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    First name
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPatientFName" runat="server" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Middle
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMI" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Last name
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPatientLName" runat="server" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Phone
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPatientPhone" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Address
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPatientAddress" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    City
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCity" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    State
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtState" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Birthdate
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBirthdate" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Age&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPatientAge" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SS #
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSocialSecurityNumber" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Insurance
                                                                </td>
                                                                <td>
                                                                    <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" runat="server" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY" Selected_Text="--- Select ---"
                                                                        Width="150px" Enabled="False" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Case Type
                                                                </td>
                                                                <td>
                                                                    <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Connection_Key="Connection_String"
                                                                        Flag_Key_Value="CASETYPE_LIST" Procedure_Name="SP_MST_CASE_TYPE" Selected_Text="---Select---"
                                                                        Width="150px" Enabled="False"></extddl:ExtendedDropDownList>
                                                                    <extddl:ExtendedDropDownList ID="extddlCaseStatus" Width="150px" runat="server" Connection_Key="Connection_String"
                                                                        Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                        Flag_ID="txtCompanyID.Text.ToString();" Visible="false" Enabled="False" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%-- <div runat="server" style="height: 5px">
                                            </div>--%>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="View">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 100%; font-family: Arial; font-size: 12px; text-align: center;"
                                                        valign="top" align="center">
                                                        <UserMessage:MessageControl runat="server" ID="usrMessage1"></UserMessage:MessageControl>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%; font-family: Arial; font-size: 12px; text-align: right;"
                                                        valign="top" align="left">
                                                        <dx:ASPxGridView ID="grdShowVisit" runat="server" KeyFieldName="I_EVENT_ID" AutoGenerateColumns="false"
                                                            Width="100%" SettingsPager-PageSize="50" SettingsCustomizationWindow-Height="300"
                                                            Settings-VerticalScrollableHeight="300">
                                                            <Columns>
                                                                <%--0--%>
                                                                <dx:GridViewDataColumn Caption="All" Width="12%" CellStyle-HorizontalAlign="Center">
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllVisits(this.checked);"
                                                                            Text="All" ToolTip="Select All" />
                                                                    </HeaderTemplate>
                                                                    <DataItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </DataItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" />
                                                                </dx:GridViewDataColumn>
                                                                <%--1--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_CASE_ID" Caption="CasID" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--2--%>
                                                                <dx:GridViewDataColumn FieldName="I_EVENT_ID" Caption="EventID" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--3--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_DOCTOR_ID" Caption="SZ_DOCTOR_ID" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--4--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_DOCTOR_NAME" Caption="Doctor Name" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--5--%>
                                                                <dx:GridViewDataColumn FieldName="DT_EVENT_DATE" Caption="Visit Date" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--6--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP" Caption="Speciality" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--7--%>
                                                                <dx:GridViewDataColumn FieldName="STATUS" Caption="    Visit Status" HeaderStyle-HorizontalAlign="Center">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--8--%>
                                                                <dx:GridViewDataColumn FieldName="SZ_PROCEDURE_GROUP_ID" Caption="SZ_PROCEDURE_GROUP_ID"
                                                                    HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--9--%>
                                                                <dx:GridViewDataColumn FieldName="GROUP_CODE" Caption="GROUP_CODE" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--10--%>
                                                                <dx:GridViewDataColumn FieldName="DocName" Caption="DocName" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--11--%>
                                                                <dx:GridViewDataColumn FieldName="BT_NOT_HAVE_NOTES" Caption="BT_NOT_HAVE_NOTES"
                                                                    HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                                <%--12--%>
                                                                <dx:GridViewDataColumn FieldName="IS_HAVE_LOGIN" Caption="IS_HAVE_LOGIN" HeaderStyle-HorizontalAlign="Center"
                                                                    Visible="false">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                </dx:GridViewDataColumn>
                                                            </Columns>
                                                            <Settings ShowVerticalScrollBar="true" ShowFilterRow="false" ShowGroupPanel="false" />
                                                            <SettingsBehavior AllowFocusedRow="false" />
                                                            <SettingsBehavior AllowSelectByRowClick="true" />
                                                            <SettingsPager Position="Bottom" />
                                                            <SettingsCustomizationWindow Height="100px"></SettingsCustomizationWindow>
                                                        </dx:ASPxGridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%; font-family: Arial; font-size: 12px; text-align: center;"
                                                        valign="top" align="center">
                                                        <asp:Button ID="btnDeletVisit" Text="Delete" runat="server" OnClick="btnDeletVisit_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtCasID" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtPatientId" runat="server" Visible="false"></asp:TextBox>
            <asp:HiddenField ID="hdSeting" runat="server" />
            <asp:HiddenField ID="hdEnable" runat="server" />
            <div style="visibility: hidden;">
                <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                    OnClick="txtUpdate_Click" />
                <asp:Button ID="btnClear1" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
                    OnClick="btnClear_Click" /></div>
        </div>
    </form>
</body>
</html>
