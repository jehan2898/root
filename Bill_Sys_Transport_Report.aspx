<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Transport_Report.aspx.cs" Inherits="Bill_Sys_Transport_Report"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>

    <script language="javascript" type="text/javascript">

        function SaveExistPatient()
        {
            document.getElementById('_ctl0_ContentPlaceHolder1_btnOK').click(); 
            document.getElementById('divid2').style.visibility='hidden';
        }
        function CancelExistPatient()
        {
            document.getElementById('divid2').style.visibility='hidden';  
        }

        function openExistsPage()
        {
            document.getElementById('divid2').style.zIndex = 1;
            document.getElementById('divid2').style.position = 'absolute'; 
            document.getElementById('divid2').style.left= '360px'; 
            document.getElementById('divid2').style.top= '250px'; 
            document.getElementById('divid2').style.visibility='visible';           
            return false;            
        }

        function showAdjusterPanel()
        {
            document.getElementById('divAdjuster').style.visibility='visible';
            document.getElementById('divAdjuster').style.position='absolute';
            document.getElementById('divAdjuster').style.left= '300px'; 
            document.getElementById('divAdjuster').style.top= '250px'; 
            document.getElementById('divAdjuster').style.zIndex=1; 
        }
        function showInsurancePanel()
        {
            document.getElementById('divInsurance').style.visibility='visible';
            document.getElementById('divInsurance').style.position='absolute';
            document.getElementById('divInsurance').style.left= '300px'; 
            document.getElementById('divInsurance').style.top= '150px'; 
            document.getElementById('divInsurance').style.zIndex=1; 
            document.getElementById('extddlAttorney').style.visibility='hidden'; 
        }

        function showAttorneyPanel()
        {
            document.getElementById('divAttorney').style.visibility='visible';
            document.getElementById('divAttorney').style.position='absolute';
            document.getElementById('divAttorney').style.left= '200px'; 
            document.getElementById('divAttorney').style.top= '150px'; 
            document.getElementById('divAttorney').style.zIndex=1; 
        }


         function showAdjusterPanelAddress()
        {
            document.getElementById('divAddress').style.visibility='visible';
            document.getElementById('divAddress').style.position='absolute';
            document.getElementById('divAddress').style.left= '300px'; 
            document.getElementById('divAddress').style.top= '300px'; 
            document.getElementById('divAddress').style.zIndex=1; 
            
            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value='';
			    
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceCity").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceZip").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceStreet").value='';
			    
            
        }


        function lfnShowHide(p_szSource)
        {
            if(p_szSource == 'button'){
                document.getElementById('divNavigate').style.visibility='hidden';
                return;
            }
            
            var hid = document.getElementById('_ctl0_ContentPlaceHolder1_hidIsSaved').value;
            if(hid == '0'){
                document.getElementById('divNavigate').style.visibility='hidden';
            }else{
                document.getElementById('divNavigate').style.visibility='visible';
            }
        }
        
        function ascii_value(c)
        {
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
    
        function clickButton1(e,charis)
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
  

function DisableKeyValidation(control,e)
{
        if(navigator.appName.indexOf("Netscape")>(-1))
        {  
            if(control.charCode == 39 || control.charCode==39)
            {
                return false;
            }
               
        }
        if (navigator.appName.indexOf("Microsoft Internet Explorer")>(-1))
        {  
            if(event.keyCode == 39)
                {
                    return false;
                } 
        }
      
} 



function confirmstatus()
         {
            //       _ctl0_ContentPlaceHolder1_extddlInsuranceCompany 
      
                if(document.getElementById("_ctl0_ContentPlaceHolder1_extddlInsuranceCompany").value == 'NA') 
                {
                    alert('Select Insurance Company');
                    
                }
                else
                {
                document.getElementById('divAddress').style.visibility='visible';
                document.getElementById('divAddress').style.position='absolute';
                document.getElementById('divAddress').style.left= '300px'; 
                document.getElementById('divAddress').style.top= '300px'; 
                document.getElementById('divAddress').style.zIndex=1; 
                document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").style.backgroundColor = "";
		    	document.getElementById('divAddressError').innerHTML='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode")
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value='';
			    
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceCityCode").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceZipCode").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceStreetCode").value='';
			    document.getElementById("_ctl0_ContentPlaceHolder1_extddlStateCode").value='NA';; 
			      document.getElementById("_ctl0_ContentPlaceHolder1_IDDefault").checked=false; 
               }
                
         }
function checkAddressDetails()
    {
       if(document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").value=='')
       {
            document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").focus();
			document.getElementById("_ctl0_ContentPlaceHolder1_txtInsuranceAddressCode").style.backgroundColor = "#ffff99";
			document.getElementById('divAddressError').innerHTML='Enter the mandatory field';
			return false;
       }
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





function TABLE5_onclick() {

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
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: left; height: 25px;" colspan="4">
                                                    <%--<asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>--%>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                    <b>Transport&nbsp; Report&nbsp;</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    From Date&nbsp; &nbsp;</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    To Date&nbsp;
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                        CssClass="text-box" MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="calExtToDate" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    From Time &nbsp;</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFromHours" runat="server" Width="40px">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlFromMinutes" runat="server" Width="40px">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlFromTime" runat="server" Width="45px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    To Time&nbsp;</td>
                                                <td style="width: 35%; height: 18px;">
                                                    &nbsp;<asp:DropDownList ID="ddlToHours" runat="server" Width="40px">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlTOMinutes" runat="server" Width="40px">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlTOTime" runat="server" Width="45px">
                                                    </asp:DropDownList>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                    Transport Name</td>
                                                <td style="width: 35%; height: 18px;">
                                                    &nbsp;<asp:DropDownList ID="ddlTransportName" runat="server" Width="62%">
                                                    </asp:DropDownList></td>
                                                <td class="ContentLabel" style="width: 15%; height: 18px;">
                                                </td>
                                                <td style="width: 35%; height: 18px;">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4" style="height: 22px">
                                                    <asp:TextBox ID="txtSort" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" OnClick="btnExportToExcel_Click"
                                                        Text="Export To Excel" />&nbsp;
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <div style="overflow: scroll; height: 600px">
                                            <asp:DataGrid ID="grdAllReports" runat="server" AutoGenerateColumns="false" Width="100%"
                                                CssClass="GridTable" OnItemCommand="grdAllReports_ItemCommand">
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="I_RFO_CHART_NO" HeaderText="CHART NO"></asp:BoundColumn>
                                                    <%--<asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="PATIENT NAME"  ></asp:BoundColumn>--%>
                                                    <asp:TemplateColumn HeaderText="PATIENT NAME" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlPatientName" runat="server" CommandName="PatientName" CommandArgument="SZ_PATIENT_NAME"
                                                                Font-Bold="true" Font-Size="12px">PATIENT NAME</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="PATIENT PHONE" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlPatientPhone" runat="server" CommandName="PatientPhone" CommandArgument="SZ_PATIENT_PHONE"
                                                                Font-Bold="true" Font-Size="12px">PATIENT PHONE</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_PHONE")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--<asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="PATIENT PHONE" ></asp:BoundColumn>--%>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="PATIENT ADDRESS"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="CITY" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlPatientCity" runat="server" CommandName="PatientCity" CommandArgument="CITY"
                                                                Font-Bold="true" Font-Size="12px">CITY</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.CITY")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="OFFICE" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlPatientOffice" runat="server" CommandName="Office" CommandArgument="SZ_OFFICE"
                                                                Font-Bold="true" Font-Size="12px">OFFICE</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_OFFICE")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--<asp:BoundColumn DataField="SZ_OFFICE" HeaderText="OFFICE" ></asp:BoundColumn>--%>
                                                    <asp:TemplateColumn HeaderText="DATE AND TIME OF APPOINTMENT" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlDate" runat="server" CommandName="Date" CommandArgument="DATE_TIME1"
                                                                Font-Bold="true" Font-Size="12px">DATE AND TIME OF APPOINTMENT</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.DATE_TIME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--<asp:BoundColumn DataField="DATE_TIME" HeaderText="DATE AND TIME OF APPOINTMENT" ></asp:BoundColumn>--%>
                                                    <asp:TemplateColumn HeaderText="TRANSPORT NAME" Visible="true">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnlPatientTranspotation" runat="server" CommandName="TranspotationCompany"
                                                                CommandArgument="SZ_TARNSPOTATION_COMPANY_NAME" Font-Bold="true" Font-Size="12px">TRANSPORT NAME</asp:LinkButton>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container, "DataItem.SZ_TARNSPOTATION_COMPANY_NAME")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <%--<asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_NAME" HeaderText="TARNSPOTATION COMPANY" ></asp:BoundColumn>--%>
                                                    <asp:BoundColumn DataField="SZ_EVENT_NOTES" HeaderText="NOTES" ></asp:BoundColumn>
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:DataGrid>
                                        </div>
                                    </td>
                                </tr>
                                
                                
                                  <tr>
                                    <td style="width: 100%" class="TDPart">
                                        
                                            <asp:DataGrid ID="grdReports" runat="server" AutoGenerateColumns="false" Width="100%"
                                                CssClass="GridTable" OnItemCommand="grdAllReports_ItemCommand" Visible="false">
                                                <ItemStyle CssClass="GridRow" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="I_RFO_CHART_NO" HeaderText="CHART NO"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="PATIENT NAME"  ></asp:BoundColumn>
                                                    
                                                    
                                                    <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="PATIENT PHONE" ></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="PATIENT ADDRESS"></asp:BoundColumn>
                                                    
                                                       <asp:BoundColumn DataField="CITY" HeaderText="CITY"></asp:BoundColumn>
                                                    
                                                    <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="OFFICE" ></asp:BoundColumn>
                                                    
                                                    <asp:BoundColumn DataField="DATE_TIME" HeaderText="DATE AND TIME OF APPOINTMENT" ></asp:BoundColumn>
                                              
                                                    <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_NAME" HeaderText="TRANSPORT NAME" ></asp:BoundColumn>
                                                    
                                                    <asp:BoundColumn DataField="SZ_EVENT_NOTES" HeaderText="NOTES" ></asp:BoundColumn>           
                                                                                                        
                                                </Columns>
                                                <HeaderStyle CssClass="GridHeader" />
                                            </asp:DataGrid>
                                        
                                    </td>
                                </tr>
                                <%--   <tr>
                                    <td style="width: 100%" class="TDPart">
                                   
                                            <asp:DataGrid ID="grdTransportReport" runat ="server" Width="100%" CssClass="GridTable"  AutoGenerateColumns="false" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                            
                                       
                                                <asp:BoundColumn DataField="I_RFO_CHART_NO" HeaderText="CHART NO"></asp:BoundColumn>
                                                
                                               
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="PATIENT NAME" ></asp:BoundColumn>
                                                
                                                <asp:BoundColumn DataField="SZ_PATIENT_PHONE" HeaderText="PATIENT PHONE" ></asp:BoundColumn>
                              
                                                <asp:BoundColumn DataField="SZ_PATIENT_ADDRESS" HeaderText="PATIENT ADDRESS" ></asp:BoundColumn>
                                               
                                                <asp:BoundColumn DataField="SZ_OFFICE" HeaderText="OFFICE" ></asp:BoundColumn>
                                                
                                                 <asp:BoundColumn DataField="DATE_TIME" HeaderText="DATE AND TIME OF APPOINTMENT" ></asp:BoundColumn>
                                                
                                                
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>--%>
                                <tr>
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
    </div>
</asp:Content>
