<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Bill_Sys_ShowReport.aspx.cs" Inherits="Bill_Sys_ShowReport" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
 
   
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
</head>
<body>
  <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%" >
        
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
                        <td  valign="top" class="TDPart">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                <tr>
                                    <td style="width: 100%; height: 154px;" >
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 23px;">
                                                    &nbsp;</td>
                                                <td style="width: 19%; height: 23px;">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td class="lbl" style="width: 50%; height: 23px;">
                                                    &nbsp;</td>
                                                <td style="width: 35%; height: 23px;">
                                                    &nbsp;&nbsp;
                                    <asp:Button id="btnExportToExcel" runat="server" cssclass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" Visible="False" /></td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 23px">
                                                </td>
                                                <td style="width: 19%; height: 23px">
                                                </td>
                                                <td class="lbl" style="width: 50%; height: 23px">
                                                    <asp:Label ID="lbl_MRIFacilityName" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                                                <td style="width: 35%; height: 23px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; font-size: 15px; font-family: arial; text-align: right; height: 20px;">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 19%; height: 20px;">
                                                    &nbsp;</td>
                                                <td class="lbl" style="width: 50%; height: 20px;">
                                                    &nbsp; &nbsp;
                                                    <asp:Label ID="lbl_City" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                    <asp:Label ID="lbl_State" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                    <asp:Label ID="lbl_Zip" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                                                <td style="width: 35%; height: 20px;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    </td>
                                                <td style="width: 19%; height: 18px;">
                                                    &nbsp;</td>
                                                <td class="lbl" style="width: 50%; height: 18px;">
                                                    <asp:Label ID="lbl_Phone" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                    &nbsp; &nbsp;
                                                    <asp:Label ID="lbl_Fax" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label></td>
                                                <td style="width: 35%; height: 18px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    &nbsp;<asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                    
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdAllReports" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages"
                                            OnPageIndexChanged="grdAllReports_PageIndexChanged">
                                            <ItemStyle CssClass="GridRow"  />
                                            <Columns>
                                                
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name (Chart Number)[Patient Phone]"  HeaderStyle-Font-Bold="true"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Referring Doctor" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-Font-Bold="true"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit/Time Of Visit" HeaderStyle-Font-Bold="true"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name[Claim Number]" HeaderStyle-Font-Bold="true"> </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident" HeaderStyle-Font-Bold="true"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROC_CODE" HeaderText="Treatment  Codes" HeaderStyle-Font-Bold="true"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Referring Doctor" DataFormatString="{0:MM/dd/yyyy}" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ADDRESS" HeaderText="Date Of Visit/Time Of Visit" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_CITY" HeaderText="Insurance Name[Claim Number]" Visible="false"> </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_STATE" HeaderText="Date Of Accident" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ZIP" HeaderText="Treatment  Codes" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Office_Id" HeaderText="Office Id" Visible="false"></asp:BoundColumn>
                                           </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        <p style="page-break-before:always;" ></p>
                                        <asp:DataGrid ID="grdTotalCount" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages"
                                            OnPageIndexChanged="grdAllReports_PageIndexChanged">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            <asp:BoundColumn DataField="SZ_OFFICE"  ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME"  ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" ></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" > </asp:BoundColumn>
                                               <%-- <asp:BoundColumn DataField="SZ_OFFICE_STATE" ></asp:BoundColumn>--%>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ZIP" > </asp:BoundColumn>
                                                                                        
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        
                                        
                                        <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false"  Visible="false" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name (Chart Number)[Patient Phone]"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Referring Doctor" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_EVENT_DATE" HeaderText="Date Of Visit/Time Of Visit"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name[Claim Number]"> </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT_DATE" HeaderText="Date Of Accident"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PROC_CODE" HeaderText="Treatment  Codes"></asp:BoundColumn>
                                                  <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="Referring Doctor" DataFormatString="{0:MM/dd/yyyy}" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_ADDRESS" HeaderText="Date Of Visit/Time Of Visit" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_CITY" HeaderText="Insurance Name[Claim Number]" Visible="false"> </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_OFFICE_STATE" HeaderText="Date Of Accident" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="SZ_OFFICE_ZIP" HeaderText="Treatment  Codes" Visible="false"></asp:BoundColumn>
                                                 <asp:BoundColumn DataField="Office_Id" HeaderText="Office Id" Visible="false"></asp:BoundColumn>

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
      </form>
</body>
</html>
 
