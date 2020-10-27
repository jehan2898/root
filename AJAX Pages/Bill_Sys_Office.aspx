<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="AJAX_Pages_Bill_Sys_Office" CodeFile="~/AJAX Pages/Bill_Sys_Office.aspx.cs" title="Green Your Bills-Office" %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">
    function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdOfficeList.ClientID %>");	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		    {		
		        
		        
		        
		        if(f.getElementsByTagName("input").item(i).disabled==false)
		        {
                    f.getElementsByTagName("input").item(i).checked=ival;
                }

//			                    str=str+1;	
//			        
//			                     if (str < 10)
//		                        {
//		                            var statusnameid1 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl0"+str+"_lblStatus");
//		                           
//		                           alert(statusnameid1.innerHTML);
//		                              statusname  = statusnameid1.innerHTML;
//		                            
//		                              
//		                                    if(statusname.toLowerCase() != "transferred")
//		                                    {  alert(str); 
//		                                         f.getElementsByTagName("input").item(i).checked=ival; 
//        		                                
//		                                    }
//		                           }else
//		                            {
//		                                var statusnameid2 = document.getElementById("ctl00_ContentPlaceHolder1_grdBillSearch_ctl"+str+"_lblStatus");
//		                                    statusname  = statusnameid2.innerHTML;
//		                                      alert(statusname);
//		                                    if (statusname.toLowerCase() != "transferred")
//		                                    {  
//		                                         f.getElementsByTagName("input").item(i).checked=ival;
//		                                    }
//			                        }        
//			                 				
			       
			    }	
			    
			    	
		    }
       }
       
            function validate()
            {
                 
                   if(document.getElementById("<%=txtOffice.ClientID%>").value=='')
                   {
                        alert('Please Select Office Name');
                        return false;
                   }
                   else
                   {
                    var objCheckBox_SoftFee = document.getElementById('<%= chkSoftwareFee.ClientID %>');
                    var obj_txtSoftwareFee =document.getElementById('<%= txtSoftwareFee.ClientID %>');
                        if(objCheckBox_SoftFee.checked ) 
                        {
                             if(obj_txtSoftwareFee.value=="0" || obj_txtSoftwareFee.value=="0.00" || obj_txtSoftwareFee.value=="0.0")
                                        {
                                             alert('Software fee value must be greater than 0 ');
                                            return false;
                                        } 
                                    else
                                        {
                                            return true;
                                        } 
                             //alert(objCheckBox_SoftFee.checked);
                              //return false;
                        }
                        else
                        {
                            return true;
                        }
                  
                   }
                   
                         
            }
           function Copy()
        {
            var objCheckBox = document.getElementById('<%= chkSameAddress.ClientID %>');
            if(objCheckBox.checked)
            {
               document.getElementById('<%= txtBillingAdd.ClientID %>').value =  document.getElementById('<%= txtOfficeAdd.ClientID %>').value;
               document.getElementById('<%= txtBillingCity.ClientID %>').value =  document.getElementById('<%= txtOfficeCity.ClientID %>').value;
               document.getElementById('ctl00_ContentPlaceHolder1_extddlBillingState').value =  document.getElementById('ctl00_ContentPlaceHolder1_extddlOfficeState').value;
               document.getElementById('ctl00_ContentPlaceHolder1_txtBillingZip').value =  document.getElementById('ctl00_ContentPlaceHolder1_txtOfficeZip').value;
               document.getElementById('ctl00_ContentPlaceHolder1_txtBillingPhone').value =  document.getElementById('ctl00_ContentPlaceHolder1_txtOfficePhone').value;
            }
            else
            {
               document.getElementById('ctl00_ContentPlaceHolder1_txtBillingAdd').value =  "";
               document.getElementById('ctl00_ContentPlaceHolder1_txtBillingCity').value =  "";
               document.getElementById('ctl00_ContentPlaceHolder1_extddlBillingState').value =  "NA";
               document.getElementById('ctl00_ContentPlaceHolder1_txtBillingZip').value =  "";
               document.getElementById('ctl00_ContentPlaceHolder1_txtBillingPhone').value =  "";
            }
        }
            
             function ConfirmDelete()
        {
             var msg = "Do you want to proceed?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
        }
        
        function Clear()
        {
                    
              document.getElementById("<%=txtOffice.ClientID%>").value='';
              document.getElementById('<%= txtOfficeAdd.ClientID %>').value='';
              document.getElementById('<%= txtOfficeCity.ClientID %>').value='';
              document.getElementById('ctl00_ContentPlaceHolder1_extddlOfficeState').value="NA";
              document.getElementById("<%=txtOfficeZip.ClientID%>").value='';
              document.getElementById("<%=txtOfficePhone.ClientID%>").value='';
              document.getElementById("<%=txtFax.ClientID%>").value='';
              document.getElementById("<%=txtEmail.ClientID%>").value='';
              document.getElementById("<%=txtOfficeCode.ClientID%>").value = '';
              
            if( document.getElementById("<%=txtPrefix.ClientID%>")!=null)
            {
             document.getElementById("<%=txtPrefix.ClientID%>").value='';
            }
            
             if( document.getElementById("<%=chkSameAddress.ClientID%>")!=null)
            {
             document.getElementById("<%=chkSameAddress.ClientID%>").checked=false;
            }
              
             if( document.getElementById("<%=txtBillingAdd.ClientID%>")!=null)
             {
                document.getElementById("<%=txtBillingAdd.ClientID%>").value='';
             }
             if( document.getElementById("<%=txtBillingCity.ClientID%>")!=null)
             {
                document.getElementById("<%=txtBillingCity.ClientID%>").value='';
             }
             if( document.getElementById("<%=txtBillingZip.ClientID%>")!=null)
             {
                document.getElementById("<%=txtBillingZip.ClientID%>").value='';
             }
             if( document.getElementById("<%=txtBillingPhone.ClientID%>")!=null)
             {
                document.getElementById("<%=txtBillingPhone.ClientID%>").value='';
             }
             if( document.getElementById("<%=txtNPI.ClientID%>")!=null)
             {
                document.getElementById("<%=txtNPI.ClientID%>").value='';
             }
             if( document.getElementById("<%=txtFederalTax.ClientID%>")!=null)
             {
                document.getElementById("<%=txtFederalTax.ClientID%>").value='';
             }
              if(document.getElementById('ctl00_ContentPlaceHolder1_extddlBillingState')!=null)
              {
                document.getElementById('ctl00_ContentPlaceHolder1_extddlBillingState').value="NA";
              }
              
              if(document.getElementById('ctl00_ContentPlaceHolder1_extddlLocation')!=null)
              {
                document.getElementById('ctl00_ContentPlaceHolder1_extddlLocation').value="NA";
              }
              // added by Kapil 05 Jan 2012
              if( document.getElementById("<%=chkSoftwareFee.ClientID%>")!=null)
                {
                 document.getElementById("<%=chkSoftwareFee.ClientID%>").checked=false;
                }
                 document.getElementById('<%= txtSoftwareFee.ClientID %>').value = '';

                 document.getElementById("<%=btnSave.ClientID%>").disabled = false;
                 document.getElementById("<%=btnUpdate.ClientID%>").disabled = true;
                 //document.getElementById("<%=btnUpdate.ClientID%>").disabled = true;
        }
        // added by Kapil 05 Jan 2012
          function SoftwareFee()
        {
            var objCheckBox_SoftFee = document.getElementById('<%= chkSoftwareFee.ClientID %>');
            if(objCheckBox_SoftFee.checked)
            {
               document.getElementById('<%= txtSoftwareFee.ClientID %>').value ="30"; 
                 document.getElementById('<%= txtSoftwareFee.ClientID %>').disabled = false;                      
            }
            else
            {               
               document.getElementById('<%= txtSoftwareFee.ClientID %>').value ='0.00';
                document.getElementById('<%= txtSoftwareFee.ClientID %>').disabled = true; 
            }
        }
       
    </script>

    <table width="100%" style="background-color: white">
        <tr>
            <td valign="top" style="width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <contenttemplate>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%">
                                        <table style="border-right: #b5df82 1px solid; border-left: #b5df82 1px solid; width: 50%;
                                            border-bottom: #b5df82 1px solid" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                            cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="txt2" valign="middle" align="left" bgcolor="#b5df82" colspan="3" height="28">
                                                        <b class="txt3"></b>
                                                        
                                                        <asp:Label ID="lblHeader" runat="server"  Text="Office Master" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                            <ContentTemplate>
                                                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width:33%;" >
                                                      </td>
                                                    <td align="center" style="width:33%;">
                                                        <asp:Label ID="lblLocation" class="lbl" runat="server" Text="Location" Font-Bold="true" Font-Size="12px"></asp:Label></td>
                                                    <td align="center" style="width:33%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" style="width:33%;">
                                                       </td>
                                                    <td align="center" style="width:33%;">
                                                        <cc1:ExtendedDropDownList ID="extddlLocation" runat="server" Width="90%" AutoPost_back="True"
                                                            Selected_Text="--- Select ---" Flag_Key_Value="LOCATION_LIST" Procedure_Name="SP_MST_LOCATION"
                                                            Connection_Key="Connection_String" OnextendDropDown_SelectedIndexChanged="extddlLocation_extendDropDown_SelectedIndexChanged">
                                                        </cc1:ExtendedDropDownList>
                                                    </td>
                                                    <td align="center" style="width:33%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblName" runat="server" Text="Provider Name" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblAddress" runat="server" Text="Provider Address" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblCity" runat="server" Text="Provider City" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtOffice" runat="server" MaxLength="50" CssClass="textboxCSS" ></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtOfficeAdd" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtOfficeCity" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblState" runat="server" Text="Provider State" Font-Bold="true" Font-Size="12px" ></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblZip" runat="server" Text="Provider Zip" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblPhone" runat="server" Text="Provider Phone" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <cc1:ExtendedDropDownList ID="extddlOfficeState" runat="server" Width="83%" Selected_Text="--- Select ---"
                                                            Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String">
                                                        </cc1:ExtendedDropDownList>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtOfficeZip" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtOfficePhone" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFax" runat="server" Text="Provider Fax" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblEmail" runat="server" Text="Provider Email" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblSoftwareFee" runat="server" Text="Software Fee" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblPrefix" runat="server" Text="Prefix" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtFax" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>&nbsp;
                                                    </td>
                                                    <td align="left" >
                                                        <asp:CheckBox ID="chkSoftwareFee" onclick="javascript:SoftwareFee();"   runat="server" >
                                                        </asp:CheckBox> &nbsp;&nbsp;
                                                   <%-- </td>
                                                    <td align="center">--%>
                                                        <asp:TextBox ID="txtSoftwareFee" runat="server"  MaxLength="30" Width="100px" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtPrefix" runat="server" Width="32px" MaxLength="2" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            
                                          
                                                <tr>
                                                    <td style="height: 19px" align="center">
                                                    </td>
                                                    
                                                    <td style="height: 19px" align="center">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="a"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="test@domain.com"
                                                            ControlToValidate="txtEmail"></asp:RegularExpressionValidator>&nbsp;</td>
                                                    <td style="height: 19px" align="center">
                                                    </td>
                                                     
                                                </tr>
                                                <tr>                                                    
                                                    <%--<td align="center" colspan="0">
                                                        <asp:CheckBox ID="chkSoftwareFee" onclick="javascript:SoftwareFee();"  runat="server" Text="Check here Software fee" Font-Size="12px">
                                                        </asp:CheckBox>
                                                    </td>--%>
                                                   <%--<td align="center">
                                                        <asp:TextBox ID="txtSoftwareFee" runat="server"  MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <asp:CheckBox ID="chkSameAddress" onclick="javascript:Copy();" runat="server" Text="Check here if same as Provider Address" Font-Size="12px">
                                                        </asp:CheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblBillingAdd" runat="Server" Text="Billing Address" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblBillingCity" runat="Server" Text="Billing City" Font-Bold="true" Font-Size="12px" ></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblBillingState" runat="Server" Text="Billing State" Font-Bold="true" Font-Size="12px" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtBillingAdd" runat="server" MaxLength="250" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtBillingCity" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <cc1:ExtendedDropDownList ID="extddlBillingState" runat="server" Width="80%" Selected_Text="--- Select ---"
                                                            Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                            CssClass="textboxCSS"></cc1:ExtendedDropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblBillingZip" runat="Server" Text="Billing Zip" Font-Bold="true" Font-Size="12px" ></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblBillingPhone" runat="Server" Text="Billing Phone" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblNPI" runat="Server" Text="NPI" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtBillingZip" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtBillingPhone" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtNPI" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFederalTax" runat="Server" Text="Federal Tax" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Label ID="lblOfficeCode" runat="Server" Text="Office Code" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtFederalTax" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtOfficeCode" runat="server" MaxLength="2" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td colspan="3">
                                                &nbsp;
                                                </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <asp:Button Style="width: 80px" ID="btnSave" OnClick="btnSave_Click" runat="server"
                                                            Text="Save" ValidationGroup="a" CausesValidation="true"></asp:Button>
                                                        <asp:Button Style="width: 80px" ID="btnUpdate" OnClick="btnUpdate_Click" runat="server"
                                                            Text="Update"></asp:Button>
                                                        <%--<asp:Button Style="width: 80px" ID="btnClear" OnClick="btnClear_Click" runat="server"
                                                            Text="Clear"></asp:Button>--%>
                                                       <input type="button" runat="server" id="btnClear1" onclick="Clear();"  style="width: 80px"
                                                                                                    value="Clear"  />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 130%">
                                        <table style="vertical-align: middle; width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 413px" class="txt2" valign="middle" align="left" bgcolor="#b5df82"
                                                        colspan="6" height="28">
                                                        <asp:Label ID="Label1" runat="server" Text="Office" Font-Bold="True" Font-Size="Small"></asp:Label>
                                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                            DisplayAfter="10">
                                                            <ProgressTemplate>
                                                                <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                    runat="Server">
                                                                    <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                        Height="25px" Width="24px"></asp:Image>
                                                                    Loading...</div>
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 30%" align="left">
                                                        <asp:Label ID="Label2" runat="server" Text="Search:" Font-Bold="True" Font-Size="Small"></asp:Label><gridsearch:XGridSearchTextBox
                                                            ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                                                        </gridsearch:XGridSearchTextBox>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 30%" align="left">
                                                    </td>
                                                    <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                                        Record Count:<%= this.grdOfficeList.RecordCount%>| Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                        
                                                           <asp:LinkButton ID="lnkExportToExcel1" runat="server" OnClick="lnkExportTOExcel_onclick"
                                                            Text="Export TO Excel">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                        
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="4">
                                                        <asp:Button Style="width: 80px" ID="btnDelete" OnClick="btnDelete_Click" runat="server"
                                                            Text="Delete"></asp:Button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" colspan="4">
                                                        <xgrid:XGridViewControl ID="grdOfficeList" runat="server" Height="148px" Width="100%"
                                                            CssClass="mGrid" OnRowCommand="grdOfficeList_RowCommand" AllowSorting="true"
                                                            PagerStyle-CssClass="pgr" PageRowCount="50" DataKeyNames="SZ_OFFICE_ID,SZ_OFFICE,SZ_OFFICE_ADDRESS,SZ_OFFICE_CITY,SZ_OFFICE_STATE_ID,SZ_OFFICE_ZIP,SZ_OFFICE_PHONE,SZ_BILLING_ADDRESS,SZ_BILLING_CITY,SZ_BILLING_STATE_ID,SZ_BILLING_ZIP,SZ_BILLING_PHONE,SZ_NPI,SZ_FEDERAL_TAX_ID,SZ_OFFICE_FAX,SZ_OFFICE_EMAIL,SZ_PREFIX,SZ_LOCATION_ID,SZ_LOCATION_NAME,IS_SOFTWARE_FEE_ADDED,MN_SOFTWARE_FEE,sz_place_of_service"
                                                            XGridKey="Bill_Sys_Mst_Office" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                            HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                            ShowExcelTableBorder="true" ExcelFileNamePrefix="Off" MouseOverColor="0, 153, 153"
                                                            AutoGenerateColumns="false" ExportToExcelColumnNames="Name,Address,City,State,Zip,Phone"
                                                             ExportToExcelFields="SZ_OFFICE,SZ_OFFICE_ADDRESS,SZ_OFFICE_CITY,SZ_OFFICE_STATE_ID,SZ_OFFICE_ZIP,SZ_OFFICE_PHONE">
                                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                                            <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                            <Columns>
                                                                <%-- 0--%>
                                                                <asp:TemplateField HeaderText="">
                                                                    <itemtemplate>
                                                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ></asp:LinkButton>
                                                                            </itemtemplate>
                                                                </asp:TemplateField>
                                                                <%-- 1--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Office ID" DataField="SZ_OFFICE_ID" />
                                                                <%-- 2--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                    headertext="Name" DataField="SZ_OFFICE" SortExpression="SZ_OFFICE" />
                                                                <%-- 3--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                     headertext="Address" DataField="SZ_OFFICE_ADDRESS" SortExpression="SZ_OFFICE_ADDRESS" />
                                                                <%-- 4--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                     headertext="City" DataField="SZ_OFFICE_CITY" SortExpression="SZ_OFFICE_CITY"/>
                                                                <%-- 5--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                     headertext="State" DataField="sz_office_state"  SortExpression="sz_office_state"/>
                                                                <%-- 6--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                     headertext="Zip" DataField="SZ_OFFICE_ZIP"  SortExpression="SZ_OFFICE_ZIP"/>
                                                                <%-- 7--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                     headertext="Phone" DataField="SZ_OFFICE_PHONE" SortExpression="SZ_OFFICE_PHONE" />
                                                                <%-- 8--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                    visible="false" headertext="Billing City" DataField="SZ_BILLING_CITY" />
                                                                <%-- 9--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Billing State" DataField="SZ_BILLING_STATE_ID" />
                                                                <%-- 9--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Billing Zip" DataField="SZ_BILLING_ZIP" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="NPI" DataField="SZ_NPI" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Tax ID" DataField="SZ_FEDERAL_TAX_ID" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Fax" DataField="SZ_OFFICE_FAX" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Email" DataField="SZ_OFFICE_EMAIL" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Perfix" DataField="SZ_PREFIX" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Location" DataField="SZ_LOCATION_ID" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Location" DataField="SZ_LOCATION_NAME" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                    visible="false" headertext="Billing Address" DataField="SZ_BILLING_ADDRESS" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Billing Phone" DataField="SZ_BILLING_PHONE" />
                                                                    <%--column IS_SOFTWARE_FEE_ADDED,MN_SOFTWARE_FEE added by Kapil 05 Jan 2012--%>
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                    visible="false" headertext="BT Software fee " DataField="IS_SOFTWARE_FEE_ADDED" />
                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right"
                                                                     headertext="Software Fee" DataField="MN_SOFTWARE_FEE" DataFormatString="{0:C}" />  
                                                                     
                                                                      <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                    visible="false" headertext="Office Code" DataField="sz_place_of_service" />
                                                                       
                                                                <asp:TemplateField HeaderText="" >
                                                                    <headertemplate>
                                                                              <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                                          </headertemplate>
                                                                    <itemtemplate>
                                                                             <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </itemtemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </xgrid:XGridViewControl>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </contenttemplate>
                </asp:UpdatePanel>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtBillingOfficeFlag" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
