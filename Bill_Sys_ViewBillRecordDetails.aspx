<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_ViewBillRecordDetails.aspx.cs" Inherits="Bill_Sys_ViewBillRecordDetails" Title="Welcome" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"  TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <script type="text/javascript" src="validation.js"></script>
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
  <script language="javascript" type="text/javascript">

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
        function UpdateStatus()
           {  
           var bfFlag = false;	
           var msg = "";
           var flag = false;
               if(document.getElementById('_ctl0_ContentPlaceHolder1_extddlBillStatus').value == 'NA')
               {
                    msg = 'Please select Bill Status .. !';
                    flag = true;
               }
               else
               {
 
               }   
              // alert(document.getElementById("_ctl0_ContentPlaceHolder1_grdBillReportDetails"));
               var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdBillReportDetails");
		       
		
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
		       // alert(f.getElementsByTagName("input").length);
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkUpdateStatus') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    bfFlag = true;
			                }			                
			            }
			        }			
		        }
		        if(bfFlag == false)
		        {
		            alert(msg + ' Please select Bill no..!');
		            flag = false;
		            return false;
		        }
		        else
		        {
		            if(flag == true)
		            {
		                alert(msg);
		            }
		            return true;
		        }    
           }

  
        function showviewBills()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.height='180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.position = "absolute";
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.top = '250px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.left ='400px';
       }
        
       function CloseviewBills()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlviewBills').style.visibility = 'hidden';  
       }
      

    </script>
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
                                      <table style="width: 100%">
                                          <tr>
                                              <td  style="text-align: left;">
                                                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                              </td>
                                          </tr>
                                        <tr>
                                            <td style="width: 100%;text-align:left;" align="left" class="ContentLabel">
                                                Bill Status&nbsp;
                                             <%-- <cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="170px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_SELECTED_STATUS_LIST" Procedure_Name="SP_MST_BILL_STATUS" Selected_Text="---Select---" />--%>
                                                         <cc1:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="170px" Connection_Key="Connection_String"
                                                        Flag_Key_Value="GET_STATUS_LIST_NOT_TRF_DNL" Procedure_Name="SP_MST_BILL_STATUS" Selected_Text="---Select---" />
                                                       
                                                    &nbsp;
                                                     <%-- Bill Status Date--%>&nbsp;
                                                      <asp:TextBox ID="txtBillStatusdate" runat="server" onkeypress="return clickButton1(event,'/')" Visible="false"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnAppointdate" runat="server" ImageUrl="~/Images/cal.gif" Visible="false"  />
                                                    <ajaxToolkit:CalendarExtender ID="calAppointdate" runat="server" PopupButtonID="imgbtnAppointdate"
                                                        TargetControlID="txtBillStatusdate" >
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;
                                                <asp:Button ID="btnUpdateStatus" runat="server" CssClass="Buttons" Text="Update Status" OnClick="btnUpdateStatus_Click" />
                                                <asp:TextBox ID="txtSearchOrder" runat="server" Visible="false" Text="SZ_CASE_NO ASC"></asp:TextBox>
                                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel" OnClick="btnExportToExcel_Click" /><%-- <asp:TextBox ID=" runat="server" Visible="false" Width="10px"></asp:TextBox>--%></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:DataGrid ID="grdBillReportDetails" AutoGenerateColumns="false" runat="server" Width="100%" CssClass="GridTable"
                                                 OnItemCommand="grdBillReportDetails_ItemCommand">
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        <%--0--%>
                                                        <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundColumn>
                                                        <%--1--%>
                                                        <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="SZ_PATIENT_ID" Visible="false"></asp:BoundColumn>
                                                        <%--2--%>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkUpdateStatus" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--3--%>
                                                        <asp:TemplateColumn HeaderText="Bill ID">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>'
                                                                    CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_ID")%>'
                                                                    CommandName="view"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                        <%--4--%>
                                                        <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #" Visible="false"></asp:BoundColumn>
                                                        <%--5--%>
                                                        <asp:BoundColumn DataField="SZ_BILL_ID" HeaderText="SZ_BILL_ID" Visible="false"></asp:BoundColumn>
                                                        <%--6--%>
                                                       <asp:TemplateColumn HeaderText="Case #">
                                                           <HeaderTemplate>
                                                               <asp:LinkButton ID="lnkCaseSearch" runat="server" CommandName="CaseSearch" CommandArgument="SZ_CASE_NO"
                                                                   Font-Bold="true" Font-Size="12px">Case #</asp:LinkButton>
                                                           </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDocumentManager" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                    CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_NO")%>'
                                                                    CommandName="Document Manager"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                        <%--7--%>
                                                        <asp:TemplateColumn>
                                                            <HeaderTemplate>
                                                                <asp:LinkButton ID="lnkPatientNameSearch" runat="server" CommandName="PatientNameSearch" CommandArgument="SZ_PATIENT_NAME" Font-Bold="true" Font-Size="12px">Patient Name</asp:LinkButton>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_NAME")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--8--%>
                                                        <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name" Visible="false"></asp:BoundColumn>
                                                        <%--9--%>
                                                        <asp:BoundColumn DataField="SZ_SPECIALITY" HeaderText="Spciality"></asp:BoundColumn>
                                                        <%--10--%>
                                                        <asp:BoundColumn DataField="SZ_PROVIDER" HeaderText="Provider"></asp:BoundColumn>
                                                        <%--11--%>
                                                        <asp:BoundColumn DataField="SZ_BILL_AMOUNT" HeaderText="Bill Amount" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                        <%--12--%>
                                                        <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date"></asp:BoundColumn>
                                                        <%--13--%>
                                                        <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                        <%--14--%>
                                                        <asp:BoundColumn DataField="DT_BILL_STATUS_DATE" HeaderText="Bill Staus Date" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                        <%--15--%>
                                                        <asp:BoundColumn DataField="SZ_STATUS" HeaderText="Status"></asp:BoundColumn>
                                                        <%--16--%>
                                                        <asp:TemplateColumn HeaderText="View bill">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                    CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateColumn>
                                                         <%--17--%>
                                                        <asp:BoundColumn DataField="SZ_BILL_ID" HeaderText="BillId1"></asp:BoundColumn>
                                                        <%--18--%>
                                                        <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="CaseNo1"></asp:BoundColumn>
                                                        <%--19--%>
                                                        <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                         <%--20--%>
                                                         <asp:BoundColumn DataField="SZ_BILL_STATUS_CODE" HeaderText="STATUS_CODE" Visible="false"></asp:BoundColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </asp:DataGrid>
                                               
                                            </td>
                                        </tr>
                                      </table>
                                       Total Amount : <asp:Label ID="lblTotal" runat="server" />
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
      <asp:Panel ID="pnlviewBills" runat="server" Style="width: 450px; height: 0px; background-color: white;
        border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
            <tr>
                <td align="right" valign="top">
                   <table width="100%">
                        <tr>
                            <td width="80%" align="left">
                                List of Bills
                            </td>
                            <td width="20%" align="right">
                                <a onclick="CloseviewBills();" style="cursor: pointer;" title="Close">X</a>
                            </td>
                        </tr>
                   </table>
                </td>
            </tr>
            <tr>
                <td style="width: 102%" valign="top">
                    <div style="height: 150px; overflow-y: scroll;">
                        <asp:DataGrid ID="grdViewBills" runat="server" Width="97%" CssClass="GridTable"
                            AutoGenerateColumns="false" >
                            <ItemStyle CssClass="GridRow" />
                            <HeaderStyle CssClass="GridHeader" />
                            <Columns>
                                <asp:BoundColumn DataField="VERSION" HeaderText="Version" ItemStyle-HorizontalAlign="left"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="File Path"> 
                                    <ItemTemplate>
                                        <a href="<%# DataBinder.Eval(Container,"DataItem.PATH")%>"
                                            target="_blank"><%# DataBinder.Eval(Container, "DataItem.FILE_NAME")%></a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CREATION_DATE" HeaderText="Date Created" ItemStyle-HorizontalAlign="left" DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

