<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_BillSearch.aspx.cs" Inherits="Bill_Sys_BillSearch" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <script type="text/javascript" src="validation.js"></script>

    <script language="javascript" type="text/javascript">
            	
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
         
         function confirm_update_bill_status()
         {
                // _ctl0_ContentPlaceHolder1_extddlBillStatus
                if(document.getElementById("_ctl0_ContentPlaceHolder1_extddlBillStatus").value == 'NA') 
                {
                    alert('Select Status');
                    return false;
                }
         
         
                var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdBillSearch");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
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
		            alert('Please select record.');
		            return false;
		        }
		        else
		        {
		            return true;
		        }
         }
         
         function confirm_bill_delete()
		 {
		        
		        var f= document.getElementById("_ctl0_ContentPlaceHolder1_grdBillSearch");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
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
		            alert('Please select record.');
		            return false;
		        }
		        
		 
//		    	if(confirm("Are you sure want to Delete?")==true)
//				{
//				    if(confirm("Delete visit , treatment , and test entries attached with this bill?")==true)
//				    {
//				        document.getElementById("_ctl0_ContentPlaceHolder1_lblVisitStatus").value = "DELETE";
//				    }
//				    else
//				    {
//				        document.getElementById("_ctl0_ContentPlaceHolder1_lblVisitStatus").value = "CHANGE_STATUS";
//				    }
//				    
//					return true;
//				}
//				else
//				{
//					return false;
//				}

                if(confirm("Are you sure want to Delete?")==true)
				{
				    
				   document.getElementById("_ctl0_ContentPlaceHolder1_lblVisitStatus").value = "DELETE";
				   
				   //document.getElementById('_ctl0_ContentPlaceHolder1_lblMsg').innerText = 'Bill deleted Successfully ...';
				   //document.getElementById('_ctl0_ContentPlaceHolder1_lblMsg').style.display = 'none';
				   
				   return true;
				}
				else
				{
					return false;
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
       
       function OpenPOM(objPath)
       {
            if(objPath == '')
            {
                alert('POM Document not uploaded.');
                return false;
            }
            else
            {
                var szPOMPath = '<% String str = ConfigurationSettings.AppSettings["DocumentManagerURL"].ToString(); %>'  ;
                var szPOMPath = '<%=str%>' + objPath
                window.open(szPOMPath);
                return false;
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
                                        <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="return openTypePage('a')">
                                                            <img src="Images/actionEdit.gif" style="border: none;" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill Number
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtBillNumber" runat="server" Width="250px"></asp:TextBox>
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Case No</td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtCaseNO" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill Date From
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                                        PopupButtonID="imgbtnFromDate" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                    Bill Date To
                                                </td>
                                                <td style="width: 35%">
                                                    <asp:TextBox ID="txtToDate" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                                        PopupButtonID="imgbtnToDate" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                    <extddl:ExtendedDropDownList ID="extddlProvider" Width="255px" Visible="false" runat="server"
                                                        Connection_Key="Connection_String" Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST"
                                                        Selected_Text="--- Select ---" />
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                                <td class="ContentLabel" style="width: 15%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" colspan="4">
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False" CssClass="btn"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="80px" OnClick="btnSearch_Click"
                                                        CssClass="Buttons" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Width="80px"
                                                        CssClass="Buttons" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <table>
                                            <tr>
                                               <td width="100%">
                                                    <table width="100%">
                                                        <tr>
                                                            <td valign="top" style="width: 170px"><asp:Label ID="lblTotalBillAmount" runat="server" Width="212px"></asp:Label></td>
                                                            <td valign="top" style="width: 233px"><asp:Label ID="lblTotalOutstandingAmount" runat="server" Width="266px"></asp:Label></td>
                                                            <td class="ContentLabel" style="text-align: left; width: 559px;">
                                                                Bill Status&nbsp;
                                                                <extddl:ExtendedDropDownList ID="extddlBillStatus" runat="server" Width="125px" Connection_Key="Connection_String"
                                                                    Flag_Key_Value="GET_SELECTED_STATUS_LIST" Procedure_Name="SP_MST_BILL_STATUS" Selected_Text="---Select---" />
                                                                &nbsp;<%-- Bill Status Date--%>&nbsp;
                                                             
                                                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="Buttons" Text="Export To Excel"
                                                                    OnClick="btnExportToExcel_Click" Width="98px" /> 
                                                                    <asp:Button ID="btnUpdateStatus" runat="server" CssClass="Buttons" Text="Update Status"
                                                                    OnClick="btnUpdateStatus_Click" Width="91px" />
                                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" CssClass="Buttons" OnClick="btnDelete_Click" /><asp:TextBox ID="txtBillStatusdate" runat="server" Visible="false"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnAppointdate" runat="server" ImageUrl="~/Images/cal.gif"
                                                                    Visible="false" />
                                                                <ajaxToolkit:CalendarExtender ID="calAppointdate" runat="server" PopupButtonID="imgbtnAppointdate"
                                                                    TargetControlID="txtBillStatusdate">
                                                                </ajaxToolkit:CalendarExtender>
                                                                
                                                            </td>
                                                            
                                                        </tr>
                                                        <tr>
                                                        </tr>
                                                     </table>
                                               </td>
                                              
                                               
                                            </tr>

                                            <tr>
                                                <td>
                                                    <div style="overflow: scroll; height: 600px; width: 100%;">
                                                        <asp:DataGrid ID="grdBillSearch" runat="Server" OnItemCommand="grdBillSearch_ItemCommand"
                                                            OnPageIndexChanged="grdBillSearch_PageIndexChanged" OnItemDataBound="grdBillSearch_ItemDataBound"
                                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                                            PageSize="50" PagerStyle-Mode="NumericPages" PagerStyle-Position="Top" OnSelectedIndexChanged="grdBillSearch_SelectedIndexChanged">
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <Columns>
                                                                <%-- 0 --%>
                                                                <asp:TemplateColumn HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkBulkPayment" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <%-- 1 --%>
                                                                <asp:TemplateColumn HeaderText="Bill Number">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                            CommandName="Edit"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 2 --%>
                                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Id" Visible="false"></asp:BoundColumn>
                                                                <%-- 3 --%>
                                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CASE ID" Visible="False"></asp:BoundColumn>
                                                                <%-- 4 --%>
                                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                                <%-- 5 --%>
                                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Speciality"></asp:BoundColumn>
                                                                <%-- 6 --%>
                                                                <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date"></asp:BoundColumn>
                                                                <%-- 7 --%>
                                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" ItemStyle-HorizontalAlign="Center"
                                                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                                                                <%-- 8 --%>
                                                                <asp:BoundColumn DataField="SZ_BILL_STATUS_NAME" HeaderText="Bill Status"></asp:BoundColumn>
                                                                <%-- 9 --%>
                                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" Visible="True"
                                                                    DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                                <%-- 10 --%>
                                                                <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="false"></asp:BoundColumn>
                                                                <%-- 11 --%>
                                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" ItemStyle-HorizontalAlign="Right"
                                                                    DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <%-- 12 --%>
                                                                <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right"
                                                                    DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <%-- 13 --%>
                                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                                                    DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <%-- 14 --%>
                                                                <asp:TemplateColumn HeaderText="Make Payment">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                            CommandName="Make Payment"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 15 --%>
                                                                <asp:TemplateColumn HeaderText="Mark / View" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container,"DataItem.DENIAL")%>
                                                                        <%--<asp:LinkButton ID="lnkDeniel" runat="server" Text="Denial" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                        CommandName="Deniel"></asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 16 --%>
                                                                <asp:TemplateColumn HeaderText="Verification Request" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.Verification")%>
                                                                        <%--<asp:LinkButton ID="lnkDeniel" runat="server" Text="Denial" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                        CommandName="Deniel"></asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 17 --%>
                                                                <asp:TemplateColumn HeaderText="">
                                                                    <ItemTemplate>
                                                                        <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                            target="_blank">Edit W.C. 4.0</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 18 --%>
                                                                <asp:TemplateColumn HeaderText="" Visible="true">
                                                                    <ItemTemplate>
                                                                        <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                            target="_blank">Edit W.C. 4.2</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 19 --%>
                                                                <asp:TemplateColumn HeaderText="" Visible="false">
                                                                    <ItemTemplate>
                                                                        <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                            target="_blank">Edit W.C. 4.3</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 20 --%>
                                                                <asp:BoundColumn DataField="SZ_POM_PATH" HeaderText="View POM"></asp:BoundColumn>
                                                                <%-- 21 --%>
                                                                <asp:TemplateColumn HeaderText="View bill">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                            CommandName="Generate bill"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 22 --%>
                                                                <asp:TemplateColumn HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <%-- 23 --%>
                                                                <asp:BoundColumn DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False"></asp:BoundColumn>
                                                                <%-- 24 --%>
                                                                <asp:BoundColumn DataField="SZ_POM_PATH" HeaderText="SZ_POM_PATH" Visible="False"></asp:BoundColumn>
                                                                <%-- 25 --%>
                                                                <asp:BoundColumn DataField="SZ_BILL_STATUS_CODE" HeaderText="BillStatusCode" Visible="False"></asp:BoundColumn>
                                                            </Columns>
                                                            <ItemStyle CssClass="GridRow" />
                                                        </asp:DataGrid>
                                                        
                                                        
                                                         <asp:DataGrid ID="grdForReport" runat="server" Width="100%" CssClass="GridTable" AutoGenerateColumns="false" Visible="false" >
                                                        <ItemStyle CssClass="GridRow" />
                                                            <HeaderStyle CssClass="GridHeader" />
                                                            <Columns>
                                                                <%-- 0 --%>
                                                                <asp:TemplateColumn HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkBulkPayment" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <%-- 1 --%>
                                                                <asp:TemplateColumn HeaderText="Bill Number" Visible= "False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkSelectCase" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                            CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                            CommandName="Edit"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 2 --%>
                                                                <asp:BoundColumn DataField="SZ_BILL_NUMBER" HeaderText="Bill Number" Visible="true"></asp:BoundColumn>
                                                                <%-- 3 --%>
                                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="CASE ID" Visible="False"></asp:BoundColumn>
                                                                <%-- 4 --%>
                                                                <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case #"></asp:BoundColumn>
                                                                <%-- 5 --%>
                                                                <asp:BoundColumn DataField="SPECIALITY" HeaderText="Speciality"></asp:BoundColumn>
                                                                <%-- 6 --%>
                                                                <asp:BoundColumn DataField="PROC DATE" HeaderText="Visit Date"></asp:BoundColumn>
                                                                <%-- 7 --%>
                                                                <asp:BoundColumn DataField="DT_BILL_DATE" HeaderText="Bill Date" ItemStyle-HorizontalAlign="Center"
                                                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                                                                <%-- 8 --%>
                                                                <asp:BoundColumn DataField="SZ_BILL_STATUS_NAME" HeaderText="Bill Status"></asp:BoundColumn>
                                                                <%-- 9 --%>
                                                                <asp:BoundColumn DataField="FLT_WRITE_OFF" HeaderText="Write Off" Visible="True"
                                                                    DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
                                                                <%-- 10 --%>
                                                                <asp:BoundColumn DataField="BIT_PAID" HeaderText="Paid" Visible="false"></asp:BoundColumn>
                                                                <%-- 11 --%>
                                                                <asp:BoundColumn DataField="FLT_BILL_AMOUNT" HeaderText="Bill Amount" ItemStyle-HorizontalAlign="Right"
                                                                    DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <%-- 12 --%>
                                                                <asp:BoundColumn DataField="PAID_AMOUNT" HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right"
                                                                    DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <%-- 13 --%>
                                                                <asp:BoundColumn DataField="FLT_BALANCE" HeaderText="Balance" ItemStyle-HorizontalAlign="Right"
                                                                    DataFormatString="{0:0.00}"></asp:BoundColumn>
                                                                <%-- 14 --%>
                                                                <asp:TemplateColumn HeaderText="Make Payment" Visible= "false">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPayment" runat="server" Text="Make Payment" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                                            CommandName="Make Payment" Visible="false"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 15 --%>
                                                                <asp:TemplateColumn HeaderText="Mark / View" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container,"DataItem.DENIAL")%>
                                                                        <%--<asp:LinkButton ID="lnkDeniel" runat="server" Text="Denial" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                        CommandName="Deniel"></asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 16 --%>
                                                                <asp:TemplateColumn HeaderText="Verification Request" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container, "DataItem.Verification")%>
                                                                        <%--<asp:LinkButton ID="lnkDeniel" runat="server" Text="Denial" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>'
                                                        CommandName="Deniel"></asp:LinkButton>--%>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 17 --%>
                                                                <asp:TemplateColumn HeaderText="">
                                                                    <ItemTemplate>
                                                                        <a href="Bill_Sys_PatientInformation.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                            target="_blank">Edit W.C. 4.0</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 18 --%>
                                                                <asp:TemplateColumn HeaderText="" Visible="true">
                                                                    <ItemTemplate>
                                                                        <a href="Bill_Sys_PatientInformationC4_2.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                            target="_blank">Edit W.C. 4.2</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 19 --%>
                                                                <asp:TemplateColumn HeaderText="" Visible="false">
                                                                    <ItemTemplate>
                                                                        <a href="Bill_Sys_PatientInformationC4_3.aspx?BillNumber=<%# DataBinder.Eval(Container,"DataItem.SZ_BILL_NUMBER")%>"
                                                                            target="_blank">Edit W.C. 4.3</a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 20 --%>
                                                                <asp:BoundColumn DataField="SZ_POM_PATH" HeaderText="View POM" Visible = "false"></asp:BoundColumn>
                                                                <%-- 21 --%>
                                                                <asp:TemplateColumn HeaderText="View bill" Visible = "false">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkTemplateManager" runat="server" Text="Generate bill" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>'
                                                                            CommandName="Generate bill" Visible = "false"> <img src="Images/grid-doc-mng.gif" style="border:none;" /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <%-- 22 --%>
                                                                <asp:TemplateColumn HeaderText="Delete" Visible = "false">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkDelete" runat="server" Visible = "false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <%-- 23 --%>
                                                                <asp:BoundColumn DataField="I_PAYMENT_STATE" HeaderText="COMMENT" Visible="False"></asp:BoundColumn>
                                                                <%-- 24 --%>
                                                                <asp:BoundColumn DataField="SZ_POM_PATH" HeaderText="SZ_POM_PATH" Visible="False"></asp:BoundColumn>
                                                                 <asp:BoundColumn DataField="SZ_BILL_STATUS_CODE" HeaderText="BillStatusCode" Visible="False"></asp:BoundColumn>
                                                            </Columns>
                                                            <ItemStyle CssClass="GridRow" />
                                        </asp:DataGrid> 
                                                        
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; height: 54px;" class="ContentLabel">
                                        <div style="float: left;">
                                            <asp:Button ID="btnBulkPayment" runat="server" Text="Bulk Payment" Width="100px"
                                                CssClass="Buttons" OnClick="btnBulkPayment_Click" Visible="false"/></div>
                                        <div align="right" style="float: right;">
                                            &nbsp;<asp:HiddenField ID="lblVisitStatus" runat="server" />
                                        </div>
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
    <asp:Panel ID="pnlviewBills" runat="server" Style="width: 98%; height: 0px; background-color: white;
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
                <td style="width: 98%" valign="top">
                    <div style="height: 150px; overflow-y: scroll;">
                        <asp:DataGrid ID="grdViewBills" runat="server" Width="97%" CssClass="GridTable" AutoGenerateColumns="false">
                            <ItemStyle CssClass="GridRow" />
                            <HeaderStyle CssClass="GridHeader" />
                            <Columns>
                                <asp:BoundColumn DataField="VERSION" HeaderText="Version" ItemStyle-HorizontalAlign="left">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="File Path">
                                    <ItemTemplate>
                                        <a href="<%# DataBinder.Eval(Container,"DataItem.PATH")%>" target="_blank">
                                            <%# DataBinder.Eval(Container, "DataItem.FILE_NAME")%>
                                        </a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CREATION_DATE" HeaderText="Date Created" ItemStyle-HorizontalAlign="left"
                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="divid" style="position: absolute; width: 350px; height: 280px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="document.getElementById('divid').style.visibility='hidden';" style="cursor: pointer;"
                title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="Bill_Sys_Denial.aspx" frameborder="0" height="280"
            width="350"></iframe>
    </div>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470" width="850"></iframe>
    </div>
    </div>
</asp:Content>
