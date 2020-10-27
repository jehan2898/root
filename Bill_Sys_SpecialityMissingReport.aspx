<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_SpecialityMissingReport.aspx.cs" Inherits="Bill_Sys_SpecialityMissingReport" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
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

    <div id="diveserch" language="javascript" onkeypress="javascript:return WebForm_FireDefaultButton(event, '_ctl0_ContentPlaceHolder1_btnSearch')">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                    padding-top: 3px; height: 100%; vertical-align: top;">
                    <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td class="LeftTop">
                            </td>
                            <td class="CenterTop" style="width: 1047px">
                            </td>
                            <td class="RightTop">
                            </td>
                        </tr>
                        <tr>
                            <td class="LeftCenter" style="height: 100%">
                            </td>
                            <td class="Center" valign="top" style="width: 1047px">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                    <tr>
                                        <td style="width: 100%" class="TDPart">
                                            <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                                <tr>
                                                    <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                        <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                        <div id="ErrorDiv" style="color: red" visible="true">
                                                        </div>
                                                        <b>Missing Specialty Report </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 13%;float:left;">
                                                        Specialty  </td>
                                                    <td style="width: 19%">
                                                        <%--<cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                        Selected_Text="---Select---" Width="140px" />--%>
                                                        <cc1:ExtendedDropDownList ID="extddlSpeciality" runat="server" Connection_Key="Connection_String"
                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST_FOR_MS_REPORT" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                            Selected_Text="---Select---" Width="140px" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 12%;vertical-align:top;">
                                                        &nbsp;Show Only Patients Who Have Completed</td>
                                                    <td style="width: 35%">
                                                     
                                                      <%--<asp:CheckBoxList ID="chklist" runat="server" OnSelectedIndexChanged="chklist_SelectedIndexChanged" ></asp:CheckBoxList>--%>
                                                      <asp:ListBox ID="listProcedure" runat="server" Height="100px" Width="169px" style="overflow:scroll;"></asp:ListBox>
                                                 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" style="width: 13%">
                                                        <asp:Label ID="lblLocationName" runat="server" CssClass="lbl" Text="Location Name"
                                                            Visible="False" Width="96px"></asp:Label></td>
                                                    <td style="width: 19%">
                                                        <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="65%" Connection_Key="Connection_String"
                                                            Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION" Selected_Text="---Select---"
                                                            Visible="false" />
                                                    </td>
                                                    <td class="ContentLabel" style="width: 12%">
                                                    </td>
                                                    <td style="width: 35%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ContentLabel" colspan="4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td class="ContentLabel" style="text-align: left;">
                                                                        Total Count :
                                                                        <asp:Label ID="lblTotalCount" runat="server" Font-Bold="true" Font-Size="10"></asp:Label>
                                                                    </td>
                                                                    <td align="right">
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                            OnClick="btnSearch_Click" />&nbsp;<asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                                            OnClick="btnExportToExcel_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td style="width: 100%" class="TDPart">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 100%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            <asp:DataGrid ID="grdMissingSpeciality" runat="server" Width="100%" CssClass="GridTable"
                                                                AutoGenerateColumns="false">
                                                                <ItemStyle CssClass="GridRow" />
                                                                <Columns>
                                                                    <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="PATIENT NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="Patient Address"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="Patient Phone"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="CASE_TYPE" Visible="false" HeaderText="Case Type"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Accident Date"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="No Of days" HeaderText="No Of days"></asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="MISSING_SPECIALITY" HeaderText="Missing Specialities"
                                                                        ItemStyle-Width="110px"></asp:BoundColumn>
                                                                </Columns>
                                                                <HeaderStyle CssClass="GridHeader" />
                                                            </asp:DataGrid>
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
                            <td class="CenterBottom" style="width: 1047px">
                            </td>
                            <td class="RightBottom" style="width: 10px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
