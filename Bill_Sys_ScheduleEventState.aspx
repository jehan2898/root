<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ScheduleEventState.aspx.cs"
    Inherits="Bill_Sys_ScheduleEventState" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    <%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Billing System</title>
	<link href="Css/main.css" type="text/css" rel="Stylesheet" />
	<link href="Css/UI.css" rel="stylesheet" type="text/css" />
	
	 <script>
       function setDiv(obj)
       {
        document.getElementById("divid").style.position = "absolute";

        document.getElementById("divid").style.left= "250px";

        document.getElementById("divid").style.top= "400px";
        
        document.getElementById("divid").style.visibility="visible";
      //alert(document.getElementById("hdnCaseID").value);
        document.getElementById("frameeditexpanse").src="Default2.aspx?_date=" + obj + "&CaseID=" + document.getElementById("hdnCaseID").value ;
        }
        
        function setHideDiv()
       {
       
        document.getElementById("divid").style.position = "absolute";

        document.getElementById("divid").style.left= "450";

        document.getElementById("divid").style.top= "400";
        
         document.getElementById("divid").style.visibility="visible";
        }
    </script>
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
    
    <script language="javascript" type="text/javascript">

        function sandeep(btnControl)
        {
    
                 document.getElementById('txtCPTCode').value=btnControl.value;
                //    __doPostBack('LinkButton1','');
                    window.document.getElementById('Button2').click();
        }
        function ReceiveServerData(arg, context)
        {
        
         var cmd_content = arg.split(',');
         document.getElementById('DivCptCode').innerHTML = cmd_content[1];

        }

    function CallSrv(ddl)
    {
        if (ddl.id == 'txtCPTCode')
        {
       
                try
                {
                //document.getElementById('DivCptCode').style.height=200;
                }
                catch(err){}
                    //document.getElementById('txtRegionOne').value="";
                  CallServer('LoadCPTCode' + ',' + ddl.value, ''); 
                 
        }
        
    }

</script>

</head>
<body topmargin="0" style="text-align:center" bgcolor="#FBFBFB">
    <form id="frmCalendarEvent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Conditional">
            <ContentTemplate>
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
		  <td height="20px" colspan="2" bgcolor="#EAEAEA" align="center"><span class="message-text"><asp:Label CssClass="message-text" id="lblMsg" runat="server"  ></asp:Label></span></td>
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
              <th scope="col" style="height: 20px"><div align="left" class="band">Appointments <%--- <asp:Label ID="lblDateTime" CssClass="band" runat="server" Text="Label"></asp:Label>--%></div></th>
              <th width="8%" rowspan="4" align="left" valign="top" scope="col">&nbsp;</th>
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
                  
                    <div id="Cal" runat="server">
                                      
                    </div>
                    <div>
                        <asp:Label ID="lbl25" runat="server" Text=""></asp:Label>
                             <asp:Label ID="lbl50" runat="server" Text=""></asp:Label>
                             <asp:Label ID="lbl75" runat="server" Text=""></asp:Label>
                             <asp:Label ID="lbl0" runat="server" Text=""></asp:Label>  
                    </div>
                    </td>
                </tr>
             
           
                         <tr>
                         <th scope="col" colspan="3">
                             <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            
                             <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                              <asp:TextBox ID="hdnCaseID" runat="server" style="visibility:hidden;" Width="10px"></asp:TextBox>
            <asp:HiddenField  ID="hdnSessionValue" runat="server" Value="" ></asp:HiddenField>
                         </th>
                         </tr>
                         <tr>
                         <th scope="col" colspan="3" style="text-align:center;">
                         <div>
                       
                             </div>
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
        </ContentTemplate>
        </asp:UpdatePanel>
          <div id="divid" style="position:absolute;width:450px;height:  320px; background-color:#CCCCCC; visibility:hidden; border-right: silver 1px solid; border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid;">
            <div style="position:relative;text-align:right;background-color:Gray;">
                                            <a onclick="document.getElementById('divid').style.visibility='hidden';" Style="cursor:pointer;" title="Close">X</a>
              </div>
           <iframe id="frameeditexpanse" src="Default2.aspx" frameborder="0" height="300px" width="350px">
           </iframe>
                                  
                                   
                                           

                                   <%-- <input type="button" name="Cancel" value="Cancel" onclick="document.getElementById('divid').style.visibility='hidden';"    />--%>
                               </div>  
    </form>
</body>
</html>
