<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdProcess.aspx.cs" Inherits="AdminProcess_AdProcess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="validation.js"></script>

    <script type="text/javascript">
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
        function CheckForInteger(e, charis) {
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

    </script>

    <script type="text/javascript">
        function showCheckinPopup(objCaseID, objPatientID) {
            var obj3 = "";
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute';
            document.getElementById('divid2').style.left = '350px';
            document.getElementById('divid2').style.top = '200px';
            document.getElementById('divid2').style.visibility = 'visible';
            document.getElementById('iframeCheckIn').src = "Bill_Sys_CheckinPopup.aspx?CaseID=" + objCaseID + "&PatientID=" + objPatientID + "&CompID=" + obj3 + "";
            return false;
        }
        function showCheckoutPopup(objCaseID) {
            var obj3 = "";
            document.getElementById('divid3').style.zIndex = 1;
            document.getElementById('divid3').style.position = 'absolute';
            document.getElementById('divid3').style.left = '350px';
            document.getElementById('divid3').style.top = '200px';
            document.getElementById('divid3').style.visibility = 'visible';
            document.getElementById('iframeCheckOut').src = "Bill_Sys_CheckoutPopup.aspx?CaseID=" + objCaseID;
            return false;
        }

        function CloseCheckoutPopup() {
            document.getElementById('divid3').style.visibility = 'hidden';
            document.getElementById('iframeCheckOut').src = 'Bill_Sys_SearchCase.aspx';
            //            window.parent.document.location.href='Bill_Sys_CheckOut.aspx';            
        }
        function CloseCheckinPopup() {
            document.getElementById('divid2').style.visibility = 'hidden';
            document.getElementById('iframeCheckIn').src = 'Bill_Sys_SearchCase.aspx';
            //            window.parent.document.location.href='Bill_Sys_CheckOut.aspx';            
        }



        function checkValidate() {
        }
    
    </script>
     <style type="text/css">
        .rep-delivery-opt
        {
            font-family: Calibri;
            text-align: left;
            font-size: 16px;
            font-style: normal;
            width: 20%;
            vertical-align: text-top;
            padding-top: 5px;
            background-color: #EFEFEF;
        }
        .rep-delivery-opt1
        {
            font-family: Calibri;
            text-align: left;
            font-size: 16px;
            font-style: normal;
            width: 20%;
            
            padding-top: 5px;
            background-color: #EFEFEF;
        }
        
        .rep-delivery-interval-itdtext
        {
            width: 70px;
        }
        
        .rep-delivery-desc
        {
            font-family: Calibri;
            text-align: left;
            font-size: 16px;
            font-style: normal;
            width: 80%;
            vertical-align: text-top;
            padding-top: 1px;
            background-color: #E1E1E1;
            padding-left: 5px;
        }
        .style2
        {
            font-family: Calibri;
            text-align: left;
            font-size: 16px;
            font-style: normal;
            width: 20%;
            vertical-align: text-top;
            padding-top: 5px;
            background-color: #EFEFEF;
            height: 167px;
        }
        .style3
        {
            font-family: Calibri;
            text-align: left;
            font-size: 16px;
            font-style: normal;
            width: 80%;
            vertical-align: text-top;
            padding-top: 1px;
            background-color: #E1E1E1;
            padding-left: 5px;
            height: 167px;
        }
        .style4
        {
            width: 450px;
        }
    </style>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

   <div>
   <table style="width: 100%; padding-top: 10px;" cellpadding="0" cellspacing="0">
   <!-- Title -->
     <tr>
                <td class="rep-delivery-opt">
                    <label>
                        Title </label>
                </td>
                <td class="rep-delivery-desc">
                    <table>
                        <tr>
                            <td class="rep-delivery-interval-itdtext">
                                      <asp:TextBox ID="Txttitle" runat="server" Width="400px"></asp:TextBox>
                                      <asp:TextBox ID="Txtcompanyid" Visible="false" runat="server" Width="400px"></asp:TextBox>
                                      <asp:TextBox ID="Txtuserid" Visible="false" runat="server" Width="400px"></asp:TextBox>
                            </td>
                            <td>
                           
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
  <!-- Process -->
     <tr>
                <td class="rep-delivery-opt">
                    <label>
                        Process </label>
                </td>
                <td class="rep-delivery-desc">
                    <table>
                        <tr>
                            <td class="rep-delivery-interval-itdtext">
                              <asp:DropDownList ID="DDLprocess" runat="server" Width="400px" >
                               <asp:ListItem Text="-Select-" Selected="True" ></asp:ListItem>
                               <asp:ListItem Text="Bill Report" Value="0" ></asp:ListItem>
                               <asp:ListItem Text="Document Report" Value="1"  ></asp:ListItem>
                              </asp:DropDownList>
                            </td>
                            <td>
                           
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
  <!-- Description -->
     <tr>
                <td class="rep-delivery-opt1">
                    <label>
                        Description </label>
                </td>
                <td class="rep-delivery-desc">
                    <table>
                        <tr>
                            <td class="rep-delivery-interval-itdtext">
              <dx:ASPxGridView ID="grdDesc" runat="server"   AutoGenerateColumns="false" Width="600px" 
              SettingsCustomizationWindow-Height="90" Settings-VerticalScrollableHeight="90">

             <Columns>
              <%--1--%>
              <dx:GridViewDataColumn  VisibleIndex="1"  Caption="SubTitle" HeaderStyle-HorizontalAlign="Center" Width="200px"
               Settings-AllowAutoFilter="False" Settings-AllowSort="False" >
               <DataItemTemplate>
                <dx:ASPxTextBox ID="txtsubtitle" runat="server"  Width="200px"></dx:ASPxTextBox>
               </DataItemTemplate>
              </dx:GridViewDataColumn>
             
              <%--2--%>
              <dx:GridViewDataColumn   VisibleIndex="2"   Caption="Description" HeaderStyle-HorizontalAlign="Center" Width="400px"
               Settings-AllowAutoFilter="False" Settings-AllowSort="False" >
                <DataItemTemplate>
                <dx:ASPxMemo ID="txtDesc" runat="server" Height="70px"  Width="400px"></dx:ASPxMemo>
               </DataItemTemplate>
               </dx:GridViewDataColumn>
              </Columns>
                                                                                       
         </dx:ASPxGridView>   
         
                            </td>
                            <td>
                           
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
   <!-- Upload File -->
     <tr>
                <td class="rep-delivery-opt">
                    <label>
                        Uplaod File </label>
                </td>
                <td class="rep-delivery-desc">
                    <table>
                        <tr>
                            <td class="rep-delivery-interval-itdtext">
                                <asp:FileUpload ID="Fileupload" runat="server" />    
                            </td>
                            <td>
                           
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
    <!-- Submit Button -->
     <tr>
                <td class="rep-delivery-opt">
                    <label>
                         </label>
                </td>
                <td class="rep-delivery-desc">
                    <table>
                        <tr>
                            <td class="rep-delivery-interval-itdtext">
                                   <dx:ASPxButton runat="server" Text="Submit" ID="btnsubmit" 
                                    meta:resourcekey="btnsubmitResource1" onclick="btnsubmit_Click">
                                </dx:ASPxButton>
                            </td>
                            <td>
                           
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
   </table>
   </div>  
</asp:Content>


