<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Bill_Sys_Insurance_Company_Report.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_Insurance_Company_Report"
    Title="Untitled Page" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>


    <script type="text/javascript">
    
         function Clear()
       {
        //alert("call");
            document.getElementById("<%=txtBillNo.ClientID%>").value='';
            document.getElementById("<%=txtNoOfDaysBillSubmitted.ClientID%>").value='';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlSpeciality").value ='NA';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlOffice").value ='NA';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlInsurance").value ='NA';
      }
        
              function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdInsuranceCompany.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       } 
    
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
                   function Validate()
       {
         
          
            var f= document.getElementById("<%= grdInsuranceCompany.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			           if (confirm("Do you want to sent bill to litigation?"))
		              {
		               
		                return true;
		              }
			            return false;
			        }
			    }			
		    }
		       
		    alert('Please select bill no.');
		    return false;
       }
            
                  </script>
<table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
                <table <%--id="mainbodytable"--%> cellpadding="0" cellspacing="0" style="width: 100%" border="0">
                    <tr>
                        <td colspan="3" style="height: 18px">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td   style="height: 100%">
                        </td>
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%; height: 100%;
                                background-color: White;">
                                <tr>
                                    <td>
                                        <table width="100%" border="0">
                                            <tr>
                                                <td style="text-align: left; width: 50%;vertical-align:top;">
                                                <table style="text-align: left; width: 100%;">
                                                    <tr>
                                                    <td style="text-align: left; width: 50%;vertical-align:top;">
                                                            <table border="0" align="left" cellpadding="0" cellspacing="0" style="width: 100%;height: 50%; border: 1px solid #B5DF82;">
                                                                <tr>
                                                                    <td style="width: 50%;" align="center">
                                                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; border-right: 1px solid #B5DF82;
                                                                                border-left: 1px solid #B5DF82; border-bottom: 1px solid #B5DF82" 
                                                                                id="TABLE1" onclick="return TABLE1_onclick()">
                                                                                <tr>
                                                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" colspan="3">
                                                                                        <b class="txt3">Search Parameters</b>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="width: 120px">
                                                                                        Bill No</td>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="width: 110px">
                                                                                            Provider 
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                            Speciality</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="width: 120px; height: 24px;">
                                                                                       <asp:TextBox ID="txtBillNo" runat="server" CssClass="search-input" Width="100%"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="width: 110px; height: 24px;">
                                                                                              <cc1:ExtendedDropDownList ID="extddlOffice" Width="100%" runat="server" Connection_Key="Connection_String"
                                                                                         Procedure_Name="SP_MST_OFFICE" Flag_Key_Value="OFFICE_LIST" Selected_Text="--- Select ---"/>
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="height: 24px">
                                                                                                    <extddl:ExtendedDropDownList ID="extddlSpeciality" runat="server" Width="100%" Selected_Text="---Select---"
                                                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                                                            Connection_Key="Connection_String"></extddl:ExtendedDropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="width: 120px">
                                                                                            Number of Days since bill submitted 
                                                                                    </td>
                                                                                    <td class="td-widget-bc-search-desc-ch" style="width: 110px; text-align: center" colspan="">
                                                                                        Bill Status</td>
                                                                                    <td class="td-widget-bc-search-desc-ch">
                                                                                    Carrier</td>
                                                                                </tr>
                                                                            <tr>
                                                                                <td class="td-widget-bc-search-desc-ch" style="width: 120px">
                                                                                            <asp:TextBox ID="txtNoOfDaysBillSubmitted" runat ="server"     CssClass="text-box"  MaxLength="50" onkeypress="return CheckForInteger(event,' ')" Width="100%"></asp:TextBox></td>
                                                                                <td class="td-widget-bc-search-desc-ch" colspan="" style="width: 110px; text-align: center">
                                                                                      <asp:ListBox ID="lbStatus" runat="server"  style="overflow:scroll;"  SelectionMode="Multiple" Width="100%" ></asp:ListBox></td>
                                                                                <td class="td-widget-bc-search-desc-ch" style="text-align: center">
                                                                                        <extddl:ExtendedDropDownList ID="extddlInsurance" runat="server" Connection_Key="Connection_String"
                                                                                            Flag_Key_Value="INSURANCE_LIST" Procedure_Name="SP_MST_INSURANCE_COMPANY" Selected_Text="---Select---"
                                                                                            Style="float: left" Width="100%" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" class="td-widget-bc-search-desc-ch" colspan="3" style="height: 19px;
                                                                                    text-align: center">
                                                                        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                                                                            <contenttemplate>
<asp:UpdateProgress id="UpdateProgress123" runat="server" DynamicLayout="true" DisplayAfter="10" AssociatedUpdatePanelID="UpdatePanel2"><ProgressTemplate>
<DIV style="VERTICAL-ALIGN: bottom; TEXT-ALIGN: center" id="Div1" class="PageUpdateProgress" runat="Server">&nbsp;</DIV>
</ProgressTemplate>
</asp:UpdateProgress> <asp:Button style="WIDTH: 80px" id="btnSearch" onclick="btnSearch_Click" runat="server" Text="Search"></asp:Button> &nbsp;<INPUT style="WIDTH: 80px" id="btnClear1" onclick="Clear();" type=button value="Clear" runat="server"  />&nbsp; 
</contenttemplate>
                                                                        </asp:UpdatePanel>
                                                                        
                                                                        </td>
                                                                            </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                        </td>
                                                    <td style="text-align: right; width: 50%;vertical-align:text-top;">
                                                    </td>
                                                    </tr>
                                                    </table>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="14px"></asp:TextBox>
                                                    <asp:TextBox ID="txtReceived" runat="server" Visible="False" Width="14px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                           
                                </tr>
                                <tr>
                                
                                    <td style="width: 100%; height: auto;">
                                        <div style="width: 100%;">
                                            <table style="height: auto; width: 100%; border: 1px solid #B5DF82;" class="txt2"
                                                align="left" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 413px">
                                                        <b class="txt3">Insurance Report</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 1017px;">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <contenttemplate>
                                                                <table style="vertical-align: middle; width: 100%;">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                                                Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                                                    CssClass="search-input">
                                                                                </gridsearch:XGridSearchTextBox>
                                                                            </td>
                                                                            <td style="vertical-align: middle; width: 30%" align="left">
                                                                             
                                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
                                                                <ProgressTemplate>
                                                                    <div id="Div10" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                        runat="Server">
                                                                        <asp:Image ID="img40" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                            Height="25px" Width="24px"></asp:Image>
                                                                        Loading...</div>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>                                                       
                                                                            </td>
                                                                            <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                                                                Record Count:<%= this.grdInsuranceCompany.RecordCount %>
                                                                                | Page Count:
                                                                                <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                                                </gridpagination:XGridPaginationDropDown>
                                                                                <asp:LinkButton ID="lnkExportToExcel" runat="server" onclick="lnkExportTOExcel_onclick"
                                                                                    Text="Export TO Excel">
                                    <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                        <asp:Button ID="btnLitigantion" runat="server" Text="Litigate"  OnClick="btnLitigantion_Click" /> 
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <xgrid:XGridViewControl ID="grdInsuranceCompany" runat="server"  Width="100%"
                                                                     ExcelFileNamePrefix="ExcelLitigation" ShowExcelTableBorder="true" 
                                                                     ExportToExcelColumnNames="Case #,Bill No,Patient Name,Provider Name,Speciality,Carrier,Date Bill Generated,Bill Amt,POM Received Date,Number Of days since Bill Generated" 
                                                                     ExportToExcelFields="SZ_CASE_NO,SZ_BILL_NUMBER,SZ_PATIENT_NAME,SZ_PROVIDER_NAME,SPECIALITY,SZ_INSURANCE_NAME,DT_BILL_DATE,FLT_BILL_AMOUNT,P_R_D,DATE_DIFF"
                                                                     CssClass="mGrid"  AutoGenerateColumns="false"
                                                                     DataKeyNames="SZ_BILL_NUMBER"
                                                                    MouseOverColor="0, 153, 153" 
                                                                    EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                                     AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="InsuranceCompanyReport"
                                                                    PageRowCount="50" PagerStyle-CssClass="pgr" 
                                                                    AllowSorting="true">
                                                                    <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                                                    <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                    <Columns>
                                                                        <asp:BoundField DataField="SZ_CASE_ID" HeaderText="Case Id" Visible="False">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                        <asp:BoundField DataField="SZ_CASE_NO" HeaderText="Case#" SortExpression="(ISNULL(rtrim(SZ_case_PREFIX),'''')+MCM.SZ_CASE_NO)">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                           <asp:BoundField DataField="SZ_BILL_NUMBER" HeaderText="Bill No" SortExpression="TBT.SZ_BILL_NUMBER">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                     
                                                                        <asp:BoundField DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" SortExpression="MP.SZ_PATIENT_LAST_NAME">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                        
                                                                              <asp:BoundField DataField="SZ_PROVIDER_NAME" HeaderText="Provider" SortExpression="MO.sz_office">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                        
                                                                              <asp:BoundField DataField="SPECIALITY" HeaderText="Speciality" SortExpression="MPG.SZ_PROCEDURE_GROUP">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                        <asp:BoundField DataField="SZ_INSURANCE_NAME" HeaderText="Carrier" SortExpression="MIC.SZ_INSURANCE_NAME">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                        
                                                                         
                                                                        <asp:BoundField DataField="DT_BILL_DATE" HeaderText="Date Bill Generated" SortExpression="TBT.DT_BILL_DATE">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                        <asp:BoundField DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amt" SortExpression="TBT.FLT_BILL_AMOUNT">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                         
                                                                        <asp:BoundField DataField="P_R_D" HeaderText="POM Received Date" SortExpression="(SELECT DT_POM_DATE FROM TXN_BILL_POM where I_POM_ID in( select I_POM_ID from TXN_BILL_TRANSACTIONS where SZ_BILL_NUMBER= TBT.SZ_BILL_NUMBER))">
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                        
                                                                         
                                                                        <asp:BoundField DataField="DATE_DIFF" HeaderText="Number Of days since Bill Generated"  SortExpression="datediff(day,TBT.DT_BILL_DATE,GETDATE())" >
                                                                            <headerstyle horizontalalign="Left"></headerstyle>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:BoundField>
                                                                    <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                                        </HeaderTemplate>
                                                                          <itemtemplate>
                                                                              <asp:CheckBox ID="ChkLitigantion" runat="server" ></asp:CheckBox>
                                                                            </itemtemplate>
                                                                         </asp:TemplateField>
                                                                        
                                                                        
                                                                        
                                                                          <asp:TemplateField visible="false">
                                                                            <itemtemplate>                                            
                                                         <tr>
                                                    <td colspan="100%">
                                                        <div id="div<%# Eval("SZ_CASE_ID") %>" style="display: none; position: relative;left: 25px;">
                                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" 
                                                                EmptyDataText="No Record Found" Width="80%" CellPadding="4" ForeColor="#333333"
                                                                GridLines="None">
                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Bill No" HeaderText="Bill No." ItemStyle-Width="25px"></asp:BoundField>
                                                                    <asp:BoundField DataField="Total Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Billed ($)">
                                                                     <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Total Paid Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Paid ($)">
                                                                     <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Total Litigation Amount" ItemStyle-Width="85px" DataFormatString="{0:C}" HeaderText="Litigated ($)">
                                                                     <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="Lawfirm Claim" ItemStyle-Width="105px" HeaderText="Lawfirm Claim">
                                                                       <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField>     
                                                                    <asp:BoundField DataField="SZ_LAW_FIRM_CASE_ID" ItemStyle-Width="105px" HeaderText="Lawfirm Case #">
                                                                       <itemstyle horizontalalign="Right"></itemstyle>
                                                                    </asp:BoundField> 
                                                                      <asp:BoundField DataField="DT_PURCHASED_DATE" ItemStyle-Width="105px" HeaderText="Purchase Date">
                                                                         <itemstyle horizontalalign="Right"></itemstyle>
                                                                      </asp:BoundField>                                                                    
                                                                </Columns>
                                                          </asp:GridView>
                                                        </div>
                                                   </td>
                                                </tr> 
                                                 </itemtemplate>
                                                                        </asp:TemplateField> 
                                                                    </Columns>
                                                                </xgrid:XGridViewControl>
                                                            </contenttemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 10px; height: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
