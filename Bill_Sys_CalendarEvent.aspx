<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_CalendarEvent.aspx.cs"
    Inherits="Bill_Sys_CalendarEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Billing System</title>
	<link href="Css/main.css" type="text/css" rel="Stylesheet" />
	<link href="Css/UI.css" rel="stylesheet" type="text/css" />
	<style>
		.css-calendar-grid{
			width: 100%;
			background-color:#999999;
		}
		
		.css-calendar-grid-td{
			width: 14%;
			height: 60px;
			background-color:#CCCCCC;
			text-align:center;
			/*width='14%' height='60px' bgcolor='blue' align='center' style='color:White' */
		}
		
		.css-cal-date{
			color:#000000;
		}
	</style>
	<script type="text/javascript" src="validation.js" ></script>
	<script src="calendarPopup.js"></script>
    <script language="javascript" type="text/javascript">
            var cal1x = new CalendarPopup();
			cal1x.showNavigationDropdowns();
			
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
 
// function OpenDayViewCalender(p_szDate)
//    {
//        alert(document.getElementById("hdnSessionValue").value);
//        document.getElementById("hdnSessionValue").value = p_szDate;
//        
//        document.getElementById("btnSetSession").click();
//    }
//    
    </script>

</head>
<body topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="frmCalendarEvent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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
                           LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1"  DynamicMenuItemStyleCSS="sublevel1" StaticMenuItemStyleCSS="parentlevel1"  Height="24px" ></cc2:WebCustomControl1>
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
              <th width="9%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
              <th scope="col" style="height: 20px"><div align="left" class="band">Appointments - <asp:Label ID="lblDateTime" CssClass="band" runat="server" Text="Label"></asp:Label></div></th>
              <th width="8%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
            </tr>
                  <tr>
              <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
              <div align="left">             
              <table width="82%"  border="0" align="center" cellpadding="0" cellspacing="3">
              
                <tr>
                    <td width="16%" class="tablecellLabel">
                        <div class="lbl">Case ID</div> </td>
                    <td width="2%" class="tablecellSpace">
                    </td>
                    <td width="30%" class="tablecellControl">
                        <asp:Label runat="server" ID="lblCaseID"></asp:Label>
                    </td>
                    <td width="18%" class="tablecellLabel">
                        <div class="lbl">Patient Name</div> </td>
                    <td width="2%" class="tablecellSpace">
                    </td>
                    <td width="32%" class="tablecellControl">
                        <asp:Label runat="server" ID="lblPatientName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                    </td>
                    <td class="tablecellLabel">
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel">
                       <div class="lbl"> Doctor Name</div> </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                        <div><asp:TextBox ID="txtDoctorName" runat="server"></asp:TextBox>&nbsp;
                <cc1:AutoCompleteExtender ID="ACEDoctorName" runat="server" Enabled="true"
                    MinimumPrefixLength="1" ServiceMethod="PROVIDERLIST" ServicePath="google.asmx"
                    TargetControlID="txtDoctorName">
                </cc1:AutoCompleteExtender></div>
                      </td>
                    <td class="tablecellLabel">
                        <div class="lbl">Date</div> </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                        <span><asp:TextBox ID="txtEventDate" MaxLength="10" runat="server" onkeypress="return clickButton1(event,'/')"></asp:TextBox> <asp:ImageButton ID="imgbtnEventDate" runat="server" ImageUrl="~/Images/cal.gif" /></span>
                        
<cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEventDate"                                    
PopupButtonID="imgbtnEventDate" />
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel" style="height: 46px">
                       <div class="lbl">Event Time </div> </td>
                    <td class="tablecellSpace" style="height: 46px">
                    </td>
                    <td class="tablecellControl" style="height: 46px"><span class="lbl">Hours
                        <asp:DropDownList ID="ddlHours" runat="server" Width="60px"></asp:DropDownList>
Minutes
<asp:DropDownList ID="ddlMinutes" runat="server" Width="60px"></asp:DropDownList>
<asp:DropDownList ID="ddlTime" runat="server" Width="60px"></asp:DropDownList>
                    </span> </td>
                    <td class="tablecellLabel" style="height: 46px">
                       <div class="lbl"> </div> </td>
                    <td class="tablecellSpace" style="height: 46px">
                    </td>
                    <td class="tablecellControl" style="height: 46px">
                        <div class="lbl"></div>
					</td>
                </tr>
                <tr>
                  <td valign="top" class="tablecellLabel" style="height: 46px"><div align="left"><span class="lbl">Notes</span></div></td>
                  <td class="tablecellSpace" style="height: 46px"></td>
                  <td colspan="4" class="tablecellControl" style="height: 146px"><asp:TextBox ID="txtEventNotes" runat="server" MaxLength="250" Width="100%" Height="140px" TextMode="MultiLine"></asp:TextBox></td>
                  </tr>
                <tr>
                    <td class="tablecellLabel">
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                    </td>
                    <td class="tablecellLabel">
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                    </td>
                </tr>
                <tr>
                    <td class="tablecellLabel" colspan="6" style="text-align:center;">
                      <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>          
                        <asp:Button ID="btnSave" runat="server"  Text="Add" Width="80px" OnClick="btnSave_Click" cssclass="btn-gray"/>
                        <asp:Button ID="btnUpdate" runat="server"  Text="Update"
                            Width="80px" Visible="False" cssclass="btn-gray" />
                        <asp:Button ID="btnClear" runat="server"  Text="Cancel" Width="80px" OnClick="btnClear_Click" cssclass="btn-gray"/>
                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtTime" runat="server" Width="10px" Visible="false" ></asp:TextBox></td>
                </tr>
                 <tr>
                    <td class="tablecellLabel">
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl">
                    </td>
                    <td class="tablecellLabel">
                    </td>
                    <td class="tablecellSpace">
                    </td>
                    <td class="tablecellControl" style="text-align:right;" >
                        </td>
                </tr>
              </table> 
              </div> 
              </th> 
              </tr> 
             <tr>
              <th scope="col" >
              <div align="left">
               </div><div align="left" class="band">
               Calendar</div>
               <div align="right"></div>
               </th>
            </tr>
            
            <tr>
                    <td  >
                        <table  width="100%" bgcolor="gray" >
                            <tr>
                              <td width="20%" height="50" align="right">
                                <asp:Button ID="imgbtnPrevDate" runat="server" Text="Previous Month" OnClick="imgbtnPrevDate_Click" cssclass="btn-gray"/>
                              </td>
                                <td width="30%" align="right">
                                    <asp:DropDownList ID="ddlMonth" runat="Server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="30%" align="left">
                                    <asp:DropDownList ID="ddlYear" runat="Server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Text="2007" Value="2007"></asp:ListItem>
                                        <asp:ListItem Text="2008" Value="2008"></asp:ListItem>
                                        <asp:ListItem Text="2009" Value="2009"></asp:ListItem>
                                        <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                        <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="20%">
                                    <asp:Button ID="btnNext" runat="server" Text="Next Month" OnClick="btnNext_Click" cssclass="btn-gray"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                         <span class="pg"><asp:HyperLink ID="FilterHyperLink" runat="server" style="cursor:pointer;">Filter</asp:HyperLink></span>
            <cc1:PopupControlExtender ID="PopEx" runat="server"
    TargetControlID="FilterHyperLink"
    PopupControlID="FilterPanel"           
    Position="Left"  />
     <asp:Panel ID="FilterPanel" runat="server" Style="display: none;background-color:White; text-align:right;">
                
                    <div class="closePanel">
                            <a onclick="AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup(); return false;" Style="cursor:pointer;" title="Fermer">X</a>
                    </div>
                    <div>Doctor<asp:TextBox ID="txtDoctorSearch" runat="server" autocomplete="off"></asp:TextBox>&nbsp;
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" Enabled="true"
                    MinimumPrefixLength="1" ServiceMethod="PROVIDERLIST" ServicePath="google.asmx"
                    TargetControlID="txtDoctorSearch">
                    </cc1:AutoCompleteExtender></div>
                    <div>Provider<asp:TextBox ID="txtProviderSearch" runat="server" autocomplete="off"></asp:TextBox>&nbsp;
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" Enabled="true"
                    MinimumPrefixLength="1" ServiceMethod="DOCTORLIST" ServicePath="google.asmx"
                    TargetControlID="txtProviderSearch">
                    </cc1:AutoCompleteExtender></div>
                    <div>
                        <asp:Button ID="btnSearch" runat="server" Text="Seacrh" cssclass="btn-gray" OnClick="btnSearch_Click"/>
                        <input type="button" ID="btnCancel" value="Cancel" class="btn-gray" onclick="AjaxControlToolkit.PopupControlBehavior.__VisiblePopup.hidePopup(); return false;"/>
                    </div>
                    
        </asp:Panel>
                    </td>
                </tr>
             <tr>
             <th  rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
             
                    <td scope="col" width="83%" >
                        <table width="100%">
                            <tr>
                                <td id="tdMonthCalender" runat="server">
                                  <table width="100%">
                            <tr>
                                <td id="td1">
                                <table class="css-calendar-grid">
                                <tr>
                                <td class="css-calendar-grid-td">Sunday</td>
                                <td class="css-calendar-grid-td">Monday</td>
                                <td class="css-calendar-grid-td">Tuesday</td>
                                <td class="css-calendar-grid-td">Wednesday</td>
                                <td class="css-calendar-grid-td">Thrusday</td>
                                <td class="css-calendar-grid-td">Friday</td>
                                <td class="css-calendar-grid-td">Saturday</td>
                                </tr>
                                <tr valign="middle">
                                <td width='14%' style="height: 60px;color:WhiteSmoke;">
                                 <table width='100%'>
                                 <tr>
                                 <td style='text-align:left;vertical-align:top;height:100%' width='100%'>
                                 <asp:label runat='server' id='lblDay0' CssClass="css-cal-date"></asp:label>
                                 </td> 
                                 </tr>
                                 <tr>
                                 <td width='100%' style="height: 46px">
                                <table width='100%'>
                                <tr>
                                <td width='100%'>
                               
                                <asp:LinkButton ID="lnkDay0" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay0_Click"> <img src=''  style='cursor:pointer;' /> </asp:LinkButton>
                                </td>
                                </tr>
                                </table>
                                </td>
                                </tr>
                                </table>
                                
                                </td>
                                <td width='14%' style="height: 60px;color:WhiteSmoke;">
                                                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay1'></asp:label></td> </tr><tr><td width='100%' style="height: 46px">
                                <table width='100%'><tr>
                                <td width='100%'>
                                
                                <asp:LinkButton ID="lnkDay1" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay1_Click"><img src=''  style='cursor:pointer;' /> </asp:LinkButton>
                                </td></tr></table></td></tr></table>
                                </td>
                                <td width='14%' style="height: 60px;color:WhiteSmoke;">
                                                                <table width='100%'><tr>
                                                                <td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay2'></asp:label></td> </tr><tr><td width='100%' style="height: 46px">
                                <table width='100%'><tr><td width='100%'>
                                 
                                 <asp:LinkButton ID="lnkDay2" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay2_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton>
                                </td></tr></table></td></tr></table>
                                </td>
                                <td width='14%' style="height: 60px;color:WhiteSmoke; ">
                                                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'>  <asp:label CssClass="css-cal-date" runat='server' id='lblDay3'></asp:label></td> </tr><tr><td width='100%' style="height: 46px">
                                <table width='100%'><tr><td width='100%'>
                                
                                  <asp:LinkButton ID="lnkDay3" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay3_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton>
                                 </td></tr></table></td></tr></table>
                                </td><td width='14%' style="color:WhiteSmoke; height: 60px;">
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay4'></asp:label></td> </tr><tr><td width='100%' style="height: 46px">
                                <table width='100%'><tr><td width='100%' style="height: 19px">
                               
                                    <asp:LinkButton ID="lnkDay4" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay4_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr></table></td><td width='14%' style="color:WhiteSmoke; height: 60px;">
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay5'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                              
                                    <asp:LinkButton ID="lnkDay5" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay5_Click">  <img src=''  style='cursor:pointer;' /> </asp:LinkButton></td></tr></table></td><td width='14%' style="color:WhiteSmoke; height: 60px;">
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay6'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                 
                                  <asp:LinkButton ID="lnkDay6" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay6_Click"><img src=''  style='cursor:pointer;'  /></asp:LinkButton></td></tr></table></td></tr>
                                <tr valign="middle">
                                
                                <td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay7'></asp:label></td></tr><tr><td width='100%'>                       
                                <table width='100%'></table>
                               
                                  <asp:LinkButton ID="lnkDay7" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay7_Click"> <img src=''  style='cursor:pointer;' /> </asp:LinkButton></td></tr></table></td>
                                <td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay8'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                  <asp:LinkButton ID="lnkDay8" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay8_Click"> <img src=''  style='cursor:pointer;' /> </asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay9'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay9" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay9_Click"> <img src=''  style='cursor:pointer;' /> </asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay10'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay10" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay10_Click"> <img src=''  style='cursor:pointer;' /> </asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay11'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay11" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay11_Click">  <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label  CssClass="css-cal-date" runat='server' id='lblDay12'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                    <asp:LinkButton ID="lnkDay12" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay12_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay13'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                  <asp:LinkButton ID="lnkDay13" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay13_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr>
                                <tr valign="middle">
                                <td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay14'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                  <asp:LinkButton ID="lnkDay14" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay14_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'>  <asp:label CssClass="css-cal-date" runat='server' id='lblDay15'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                    <asp:LinkButton ID="lnkDay15" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay15_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay16'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                    <asp:LinkButton ID="lnkDay16" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay16_Click"><img src=''  style='cursor:pointer; text-decoration:none;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay17'></asp:label></td></tr>
                                <tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay17" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay17_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr> 
                                
                                </table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay18'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'><tr><td width='100%'> 
                                    
                                    <asp:LinkButton ID="lnkDay18" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay18_Click"><img src='' style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'>  <asp:label CssClass="css-cal-date" runat='server' id='lblDay19'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'><tr><td width='100%'> 
                                   
                                    <asp:LinkButton ID="lnkDay19" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay19_Click"><img src='' style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay20'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'><tr><td width='100%'> 

                                  <asp:LinkButton ID="lnkDay20" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay20_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr></table></td></tr>
                                <tr valign="middle">
                                <td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay21'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'><tr><td width='100%'> 
                                    
                                  <asp:LinkButton ID="lnkDay21" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay21_Click"><img src='' style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay22'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                    
                                    <asp:LinkButton ID="lnkDay22" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay22_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay23'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                    
                                    <asp:LinkButton ID="lnkDay23" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay23_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay24'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'><tr><td width='100%'> 
                                   
                                    <asp:LinkButton ID="lnkDay24" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay24_Click"><img src='' style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay25'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                    
                                    <asp:LinkButton ID="lnkDay25" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay25_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay26'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay26" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay26_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay27'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                  <asp:LinkButton ID="lnkDay27" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay27_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr>
                                <tr valign="middle">
                                <td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay28'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                  <asp:LinkButton ID="lnkDay28" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay28_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay29'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay29" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay29_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay30'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                    <asp:LinkButton ID="lnkDay30" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay30_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay31'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                    <asp:LinkButton ID="lnkDay31" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay31_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay32'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay32" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay32_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay33'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                              
                                    <asp:LinkButton ID="lnkDay33" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay33_Click">  <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label  CssClass="css-cal-date" runat='server' id='lblDay34'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                  <asp:LinkButton ID="lnkDay34" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay34_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr>
                                <tr valign="middle">
                                <td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay35'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                  <asp:LinkButton ID="lnkDay35" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay35_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay36'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                    <asp:LinkButton ID="lnkDay36" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay36_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay37'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay37" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay37_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'>  <asp:label CssClass="css-cal-date" runat='server' id='lblDay38'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                              
                                    <asp:LinkButton ID="lnkDay38" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay38_Click">  <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay39'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                               
                                    <asp:LinkButton ID="lnkDay39" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay39_Click"> <img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay40'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                    <asp:LinkButton ID="lnkDay40" runat="server"  style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay40_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td><td width='14%' height='60px' style='color:WhiteSmoke;'>
                                <table width='100%'><tr><td style='text-align:left;vertical-align:top;height:100%' width='100%'> <asp:label CssClass="css-cal-date" runat='server' id='lblDay41'></asp:label></td></tr><tr><td width='100%'>
                                <table width='100%'></table>
                                
                                  <asp:LinkButton ID="lnkDay41" runat="server" style='cursor:pointer;color:WhiteSmoke; text-decoration:none;' OnClick="lnkDay41_Click"><img src=''  style='cursor:pointer;' /></asp:LinkButton></td></tr></table></td></tr>
                               </table>  
                               </td>
                                
                            </tr>
          </table>
                                </td>                                
                            </tr>
                        </table>
                    </td>
                    <th  rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
                </tr>
                         <tr>
                         <th scope="col" colspan="3">
                         <asp:Button ID="btnSetSession" runat="server" Visible="false"   style="width:0px;height:0px;" OnClick="btnSetSession_Click" />
            <asp:HiddenField  ID="hdnSessionValue" runat="server" Value="" ></asp:HiddenField>
                         </th>
                         </tr>
		    </table> 
		    </td> 
		    </tr>  
                
            
               
                <!-- Added For Month Calender -->
                
                 
               
                
                <!-- End of code added for Month Calender -->
            </table>
            <!-- Added For Month Calender -->
            
            <!-- End of code added for Month Calender -->
        </div>
    </form>
</body>
</html>
