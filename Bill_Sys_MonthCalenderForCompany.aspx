<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_MonthCalenderForCompany.aspx.cs"
    Inherits="Bill_Sys_MonthCalenderForCompany" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Month Calender</title>

    <script type="text/javascript" src="validation.js"></script>

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
 
 function OpenDayViewCalender(p_szDate)
    {
        document.getElementById("hdnSessionValue").value = p_szDate;
        document.getElementById("btnSetSession").click();
    }
 
    </script>

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
</head>
<body topmargin="0" style="text-align: center" bgcolor="#FBFBFB">
    <form id="frmMonthCalenderForCompany" runat="server">
        <div align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">  </asp:ScriptManager>
            <table cellpadding="0" cellspacing="0" class="simple-table">
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td background="Images/header-bg-gray.jpg">
                        <div align="right">
                            <span class="top-menu"></span></div>
                    </td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td background="Images/header-bg-gray.jpg" class="top-menu">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td background="Images/header-bg-gray.jpg">
                        &nbsp;</td>
                    <td class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td background="Images/header-bg-gray.jpg">
                        <cc2:WebCustomControl1 ID="TreeMenuControl1" runat="server" Procedure_Name="SP_MST_MENU"
                            Connection_Key="Connection_String" Width="744px" Xml_Transform_File="TransformXSLT.xsl"
                            LevelMenuItemStylesCSS="sublevel1" Child_Label_CSS="sublevel1" DynamicMenuItemStyleCSS="sublevel1"
                            StaticMenuItemStyleCSS="parentlevel1" Height="24px"></cc2:WebCustomControl1>
                    </td>
                    <td width="8%" class="top-menu">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="35px" bgcolor="#000000">
                                    <div align="left">
                                    </div>
                                    <div align="left">
                                        <span class="pg-bl-usr">Billing company name</span></div>
                                </td>
                                <td width="12%" height="35px" bgcolor="#000000">
                                    <div align="right">
                                        <span class="usr">Admin</span></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="top-menu" style="height: 20px">
                        &nbsp;</td>
                    <td bgcolor="#EAEAEA" align="center" style="height: 20px">
                        <span class="message-text">
                            <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label></span></td>
                    <td class="top-menu" style="height: 20px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="top-menu">
                        &nbsp;</td>
                    <td height="18" align="right" background="Images/sub-menu-bg.jpg">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <th scope="col" style="height: 29px; width: 29%;">
                                    <div align="left">
                                        <a href="Bill_Sys_SearchCase.aspx"><span class="pg">&nbsp;&nbsp;Home</span></a></div>
                                </th>
                                <th width="81%" scope="col" style="height: 29px">
                                    <div align="right">
                                        <span class="sub-menu"></span>
                                    </div>
                                </th>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="top-menu" width="9%">
                        &nbsp;</td>
                    <th scope="col" style="height: 20px">
                        <div align="left" class="band">
                            Appointments - 
                            <asp:Label ID="lblDateTime" CssClass="band" runat="server" ></asp:Label></div>
                    </th>
                </tr>
                <!-- Start : Data Entry -->
                <tr>
                    <td class="top-menu" width="9%">
                        &nbsp;</td>
                    <th width="83%" align="center" valign="top" bgcolor="E5E5E5" scope="col">
                        <div align="left">
                            <table width="82%" border="0" align="center" cellpadding="0" cellspacing="3">
                                <tr>
                                    <td class="top-menu" width="9%">
                                        &nbsp;</td>
                                    <td height="30px" colspan="4">
                                        <div id="ErrorDiv" visible="true" style="color: Red;">
                                                
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="16%" class="tablecellLabel">
                                        <div class="lbl">
                                            Company ID</div>
                                    </td>
                                    <td width="2%" class="tablecellSpace">
                                    </td>
                                    <td width="30%" class="tablecellControl">
                                        <asp:Label runat="server" ID="lblCompanyID"></asp:Label>
                                    </td>
                                    <td width="18%" class="tablecellLabel">
                                        <div class="lbl">
                                            Date</div>
                                    </td>
                                    <td width="2%" class="tablecellSpace">
                                    </td>
                                    <td class="tablecellControl">
                        <span><asp:TextBox ID="txtEventDate" MaxLength="10" runat="server" onkeypress="return clickButton1(event,'/')"></asp:TextBox> 
                        <asp:ImageButton ID="imgbtnEventDate" runat="server" ImageUrl="~/Images/cal.gif" />
                        
<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEventDate"  PopupButtonID="imgbtnEventDate" />
                        
                        </span>
                    </td>
                                </tr>
                                <tr>
                                    <td class="tablecellLabel">
                                        
                                            <div class="lbl">Event Time</div>
                                    </td>
                                    <td class="tablecellSpace">
                                    </td>
                                    <td class="tablecellControl">
                                        <div class="lbl">Hours</div><asp:DropDownList ID="ddlHours" runat="server" Width="60px">
                                        </asp:DropDownList><br />
                                        <div class="lbl">Minutes</div><asp:DropDownList ID="ddlMinutes" runat="server" Width="60px">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlTime" runat="server" Width="60px">
                                        </asp:DropDownList></td>
                                    <td class="tablecellLabel">
                                    </td>
                                    <td class="tablecellSpace">
                                    </td>
                                    <td class="tablecellControl">
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" class="tablecellLabel" style="height: 46px">
                                        <div align="left">
                                            <span class="lbl">Notes</span></div>
                                    </td>
                                    <td class="tablecellSpace" style="height: 46px">
                                    </td>
                                    <td colspan="4" class="tablecellControl" style="height: 146px">
                                        <asp:TextBox ID="txtEventNotes" runat="server" MaxLength="250" Width="100%" Height="140px"
                                            TextMode="MultiLine"></asp:TextBox></td>
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
                                    <td class="tablecellLabel" colspan="6" style="text-align: center;">
                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" OnClick="btnSave_Click"
                                            CssClass="btn-gray" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" Visible="False"
                                            CssClass="btn-gray" />
                                        <asp:Button ID="btnClear" runat="server" Text="Cancel" Width="80px" OnClick="btnClear_Click"
                                            CssClass="btn-gray" />
                                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtTime" runat="server" Width="10px" Visible="false"></asp:TextBox></td>
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
                                    <td class="tablecellControl" style="text-align: right;">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </th>
                </tr>
                <!-- End : Data Entry -->
                <tr>
                    <td class="top-menu" width="9%">
                        &nbsp;</td>
                    <td>
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
                    <td class="top-menu" width="9%">
                        &nbsp;</td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td id="tdMonthCalender" runat="server">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnSetSession" runat="server" Style="width: 0px; height: 0px;" OnClick="btnSetSession_Click" />
        <asp:HiddenField ID="hdnSessionValue" runat="server" Value=""></asp:HiddenField>
    </form>
</body>
</html>
