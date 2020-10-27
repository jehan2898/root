<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CaseMaster.aspx.cs" Inherits="Bill_Sys_CaseMaster" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization" TagPrefix="CPA" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Billing System</title>
    <script type="text/javascript" src="validation.js" ></script>
     <link href="Css/main.css" type="text/css" rel="Stylesheet" />
 <link href="Css/UI.css" rel="stylesheet" type="text/css" />
 
 <script src="calendarPopup.js"></script>

    <script language="javascript">
			var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();		
	
    </script>
    <script type="text/javascript" >
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
<body topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="frmCaseMaster" runat="server">    
          <div align="center">
            <table cellpadding="0" cellspacing="0" class="simple-table">
            		<tr>
			            <td width="9%" height="18" >&nbsp;</td>
		                <td colspan="2" background="Images/header-bg-gray.jpg"><div align="right"><span class="top-menu">Home | Logout</span></div></td>
		                <td width="8%" >&nbsp;</td>
		            </tr>
		            
		            <tr>
		              <td class="top-menu">&nbsp;</td>
		              <td colspan="2" background="Images/header-bg-gray.jpg" class="top-menu">&nbsp;</td>
		              <td class="top-menu" >&nbsp;</td>
	              </tr>
		          <tr>
		              <td class="top-menu">&nbsp;</td>
		              <td colspan="2" background="Images/header-bg-gray.jpg">&nbsp;</td>
		              <td class="top-menu">&nbsp;</td>
	              </tr>
             <tr>
		              <td width="9%" class="top-menu">&nbsp;</td>
	                  <td colspan="2" background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1"  DynamicMenuItemStyleCSS="sublevel1" StaticMenuItemStyleCSS="parentlevel1"  Height="24px"></cc2:WebCustomControl1>
                    	                  </td>
	                  <td width="8%" class="top-menu">&nbsp;</td>
	              </tr>
	             
	              <tr>
		  <td class="top-menu">&nbsp;</td>
		  <td height="35px" bgcolor="#000000"><div align="left"></div>		    
	      <div align="left"><span class="pg-bl-usr">Billing company name</span></div></td>
		  <td width="12%" height="35px" bgcolor="#000000"><div align="right"><span class="usr">Admin</span></div></td>
		  <td class="top-menu">&nbsp;</td>
	  </tr>
	  
	  
	  	<tr>
		  <td class="top-menu">&nbsp;</td>
		  <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center"><span class="message-text"><asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label></span></td>
		  <td class="top-menu">&nbsp;</td>
	  </tr>  
	  	<tr>
		  <td class="top-menu">&nbsp;</td>
		  <td height="18" colspan="2" align="right" background="Images/sub-menu-bg.jpg">
		  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <th width="19%" scope="col" style="height: 29px">
              <div align="left"><a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div></th>
              <th width="81%" scope="col" style="height: 29px"><div align="right"><span class="sub-menu">

              </span></div></th>
            </tr>
          </table>
     </td>
		  <td class="top-menu" colspan="3">&nbsp;</td>
	  </tr>
	  
	  <tr>
	    <td colspan="4" height="409">
		  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="6" class="usercontrol">
                        <CPA:CheckPageAutharization ID="CheckPageAutharization1" runat="server"></CPA:CheckPageAutharization>
                    </td>
                </tr>
                
                     <tr>
              <th width="9%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
              <th scope="col" style="height: 20px"><div align="left" class="band">Case</div></th>
              <th width="8%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
            </tr>
            
             <tr>
              <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
              <div align="left">             
              <table width="53%"  border="0" align="center" cellpadding="0" cellspacing="3">
               <tr>
                <td colspan="6" height="30">
                    <div id="ErrorDiv" style="color: red" visible="true">
                    </div>
                </td>
            </tr>
            
            <tr>
               <%-- <td class="tablecellLabel">
                    <div class="lbl">Case Name </div> </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <asp:TextBox ID="txtCaseName" runat="server" MaxLength="50"></asp:TextBox></td>--%>
                <td class="tablecellLabel">
                  <div class="lbl">  Case Type </div> </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <cc1:ExtendedDropDownList ID="extddlCaseType" runat="server" width="200px" Connection_Key="Connection_String" Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---" />
                    <asp:ImageButton ID="imgbtnCaseType" runat="server" OnClick="imgbtnCaseType_Click" Height="16px" ImageUrl="~/Images/search.jpg" BorderColor="#404040" BorderStyle="Solid" BorderWidth="1px" /></td>    
            
                <td class="tablecellLabel">
                 <div class="lbl">   Provider</div>  </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <cc1:ExtendedDropDownList ID="extddlProvider" width="200px" runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---" />
                    <asp:ImageButton ID="imgbtnProvider" runat="server" Height="16px" ImageUrl="~/Images/search.jpg" OnClick="imgbtnProvider_Click" BorderColor="#404040" BorderStyle="Solid" BorderWidth="1px" /></td>
            </tr>
           
            <tr>
                    <td class="tablecellLabel">
                  <div class="lbl">  Insurance Company </div> </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                     <cc1:ExtendedDropDownList ID="extddlInsuranceCompany" width="200px" runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY" Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---"  />
                    <asp:ImageButton ID="imgbtnInsuranceCompany" runat="server" Height="16px" ImageUrl="~/Images/search.jpg" OnClick="imgbtnInsuranceCompany_Click" BorderStyle="Solid" BorderWidth="1px" /><%--                    <asp:TextBox ID="extddlInsuranceCompany" runat="server" Text="001"></asp:TextBox>--%></td>
            
                <td class="tablecellLabel">
               <div class="lbl">     Case Status</div>  </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <cc1:ExtendedDropDownList ID="extddlCaseStatus" width="200px" runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---" Flag_ID="txtCompanyID.Text.ToString();" />
                    <asp:ImageButton ID="imgbtnCaseStatus" runat="server" Height="16px" ImageUrl="~/Images/search.jpg" OnClick="imgbtnCaseStatus_Click" BorderStyle="Solid" BorderWidth="1px" /></td>
            </tr>
           
          
            <tr>
                    <td class="tablecellLabel">
                  <div class="lbl">  Attorney </div> </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <cc1:ExtendedDropDownList ID="extddlAttorney" width="200px" runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---" />
                    <asp:ImageButton ID="imgbtnAttorney" runat="server" Height="16px" ImageUrl="~/Images/search.jpg" OnClick="imgbtnAttorney_Click" BorderStyle="Solid" BorderWidth="1px" /></td>
           
                <td class="tablecellLabel">
                  <div class="lbl">  Patient</div>  </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <cc1:ExtendedDropDownList ID="extddlPatient" width="200px" runat="server" Connection_Key="Connection_String" Procedure_Name="SP_MST_PATIENT" Flag_Key_Value="PATIENT_LIST" Selected_Text="--- Select ---" />
                    <asp:ImageButton ID="imgbtnPatient" runat="server" Height="16px" ImageUrl="~/Images/search.jpg" OnClick="imgbtnPatient_Click" BorderStyle="Solid" BorderWidth="1px" /></td>
             </tr>
            
             <tr> 
                    <td> <div class="lbl">  Adjuster </div> </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <cc1:ExtendedDropDownList ID="extddlAdjuster" width="200px" runat="server" Connection_Key="Connection_String" Selected_Text="--- Select ---" Flag_Key_Value="GET_ADJUSTER_LIST" Procedure_Name="SP_MST_ADJUSTER" />
                    <asp:ImageButton ID="imgbtnAdjuster" runat="server" Height="16px" ImageUrl="~/Images/search.jpg"  BorderStyle="Solid" BorderWidth="1px" OnClick="imgbtnAdjuster_Click" />
                    </td>
                     
                    <td class="tablecellLabel">
                  <div class="lbl">  Claim Number </div> </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box"></asp:TextBox>
                    </td>
           </tr>
                      
            <tr> 
            
                <td class="tablecellLabel">
                  <div class="lbl">Policy Number</div>  </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="text-box"></asp:TextBox>
                    </td>
                    
                    
                    <td class="tablecellLabel">
                  <div class="lbl">  Date Of Accident </div> </td>
                <td class="tablecellSpace">
                </td>
                <td class="tablecellControl">
                    <asp:TextBox ID="txtDateofAccident" runat="server" onkeypress="return CheckForInteger(event,'/')" MaxLength="10"></asp:TextBox>
                        <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif" />
                    </td>
           </tr>
           <tr> 
            
                <td class="tablecellLabel" style="height: 20px">
                </td>
                <td class="tablecellSpace" style="height: 20px">
                </td>
                <td class="tablecellControl" style="height: 20px">
                    <div class="lbl"> <asp:CheckBox ID="chkAssociateCode" runat="server" Text="Associate Diagnosis Code" />
                        <asp:TextBox ID="txtAssociateDiagnosisCode" runat="server" Visible="False"></asp:TextBox></div>  
                    
                    </td>
                    
                    
                    <td class="tablecellLabel" style="height: 20px">
                  <div class="lbl">  </div> </td>
                <td class="tablecellSpace" style="height: 20px">
                </td>
                <td class="tablecellControl" style="height: 20px">
                   
                    </td>
           </tr>
                <tr>
                <td colspan="6" align="center">
                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
               
               
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" Width="80px" cssclass="btn-gray"/>
                    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                        Width="80px" cssclass="btn-gray"/>
                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="80px" cssclass="btn-gray"/>
                    </td>
            </tr>
              </table> 
              </div> 
              </th> 
              </tr> 
              
                 <tr>
              <th scope="col">
              <div align="left">
               </div><div align="left" class="band">
               Case List</div>
               </th>
            </tr>
              <tr>
                <td >
                    <asp:DataGrid ID="grdCaseMaster" runat="server" 
                        OnPageIndexChanged="grdCaseMaster_PageIndexChanged" OnSelectedIndexChanged="grdCaseMaster_SelectedIndexChanged" >
                        <FooterStyle />
                        <SelectedItemStyle />
                        <PagerStyle />
                        <AlternatingItemStyle />
                        <ItemStyle />
                        <Columns>
                            <asp:ButtonColumn CommandName="Select" Text="Select"></asp:ButtonColumn>
                            <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_CASE_NAME" HeaderText="Case Name" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_CASE_TYPE_ID" HeaderText="Case Type ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_CASE_TYPE_NAME" HeaderText="Case Type"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_PROVIDER_ID" HeaderText="Provider ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_PROVIDER_NAME" HeaderText="Provider Name" ></asp:BoundColumn>                            
                            <asp:BoundColumn DataField="SZ_INSURANCE_ID" HeaderText="Insurance ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Name"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_CASE_STATUS_ID" HeaderText="CASE STATUS ID" Visible="False">
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="SZ_STATUS_NAME" HeaderText="Case Status"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_ATTORNEY_ID" HeaderText="Attorney ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_ATTORNEY_FIRST_NAME" HeaderText="Attorney Name"></asp:BoundColumn>
                             <asp:BoundColumn DataField="SZ_ATTORNEY_ID" HeaderText="Attorney ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_ATTORNEY_FIRST_NAME" HeaderText="Attorney Name"></asp:BoundColumn>
                              <asp:BoundColumn DataField="SZ_PATIENT_ID" HeaderText="patient ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient NAME"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="SZ_ADJUSTER_ID" HeaderText="Adjuster ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_ADJUSTER_NAME" HeaderText="Adjuster Name" ></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="SZ_CLAIM_NUMBER" HeaderText="Claim Number" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_POLICY_NUMBER" HeaderText="Policy Number"  Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Date of Accident"  Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BT_ASSOCIATE_DIAGNOSIS_CODE" HeaderText="Asscoiate"  Visible="False"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle  />
                    </asp:DataGrid>
                </td>
            </tr>
                </table> 
                </td> 
                </tr> 
    
            
           
            
           
           
            
        </table>
    
    </div>
    </form>
</body>
</html>
