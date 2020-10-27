<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ScheduleEvent.aspx.cs" Inherits="Bill_Sys_ScheduleEvent" %>

<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
	    var dateArray = new Array();
	    
	    function RGB2HTML(red, green, blue)
{
    var decColor = red + 256 * green + 65536 * blue;
    return decColor.toString(16);
}
    function btnAddScedule_onclick() 
    {
        
       if (dateArray.length > 0)
       {
        
        document.getElementById("divid").style.position = "absolute";

        document.getElementById("divid").style.left= "350px";

        document.getElementById("divid").style.top= "150px";
        
        document.getElementById("divid").style.visibility="visible";
        document.getElementById('divid').style.zIndex= '1'; 
        document.getElementById("divid").style.width= "700px";
        document.getElementById("divid").style.height= "500px";
        document.getElementById("frameeditexpanse").style.width= "700px";
        document.getElementById("frameeditexpanse").style.height= "500px";
        //document.getElementById("frameeditexpanse").src="Default2.aspx?_date=" + dateArray.toString() + "&CaseID=" + document.getElementById("_ctl0_ContentPlaceHolder1_hdnCaseID").value ;
        document.getElementById("frameeditexpanse").src="Default2.aspx?_date=" + dateArray.toString() + "&CaseID=" + document.getElementById("<%=hdnCaseID.ClientID%>").value ;
        }
    }
	    
       function setDiv(obj,obj1)
       {
        //var objDct=document.getElementById("_ctl0_ContentPlaceHolder1_tabcontainerPatientVisit_tabPanelScheduledVisit2_ddlDoctorList");
        var objDct=document.getElementById("<%=ddlDoctorList.ClientID%>");
         
        if(objDct.selectedIndex!=-1)
        {
       
        document.getElementById("divid").style.position = "absolute";
        document.getElementById("divid").style.left= "350px";
        document.getElementById("divid").style.top= "150px";
        document.getElementById('divid').style.zIndex= '1'; 
        document.getElementById("divid").style.visibility="visible";
      //alert(document.getElementById("hdnCaseID").value);
        document.getElementById("divid").style.width= "350px";
        document.getElementById("divid").style.height= "180px";
        document.getElementById("frameeditexpanse").style.width= "700px";
        document.getElementById("frameeditexpanse").style.height= "500px";
        //frameeditexpanse
        //document.getElementById("frameeditexpanse").src="Default2.aspx?_time=" + obj1 + "&_date=" + obj + "&CaseID=" + document.getElementById("_ctl0_ContentPlaceHolder1_hdnCaseID").value + "&DoctorID=" + objDct.options[objDct.selectedIndex].value + "&DoctorName=" + objDct.options[objDct.selectedIndex].text;
        document.getElementById("frameeditexpanse").src="Default2.aspx?_time=" + obj1 + "&_date=" + obj + "&CaseID=" + document.getElementById("<%=hdnCaseID.ClientID%>").value + "&DoctorID=" + objDct.options[objDct.selectedIndex].value + "&DoctorName=" + objDct.options[objDct.selectedIndex].text;
        }
        else
        {alert("Please select doctor from list");}
        }
        
        function setHideDiv()
       {
       
        document.getElementById("divid").style.position = "absolute";

        document.getElementById("divid").style.left= "450";

        document.getElementById("divid").style.top= "400";
        document.getElementById('divid').style.zIndex= '-1'; 
         document.getElementById("divid").style.visibility="visible";
         dateArray=null;
        }
       
       function SelectAndClosePopup()
       {
          var button = document.getElementById('<%=btnCls.ClientID%>');
            button.click();
       }
       
    </script>

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
 
// function OpenDayViewCalender(p_szDate)
//    {
//        alert(document.getElementById("hdnSessionValue").value);
//        document.getElementById("hdnSessionValue").value = p_szDate;
//        
//        document.getElementById("btnSetSession").click();
//    }
//    
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="vertical-align: top;">
        <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%; vertical-align: top;">
            <tr>
                <td colspan="6" height="409">
                    <table id="MainBodyTable" width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td scope="col" width="100%" style="height: 20px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td scope="col" style="text-align: center;">
                                <input id="btnAddScedule" type="button" value="Add Schedule" style="visibility: hidden;"
                                    onclick="return btnAddScedule_onclick()" class="Buttons" />
                                <asp:DropDownList ID="ddlInterval" runat="server" Width="60px" Visible="False">
                                    <asp:ListItem Text="0.15" Value="0.15"></asp:ListItem>
                                    <asp:ListItem Text="0.30" Selected="True" Value="0.30"></asp:ListItem>
                                    <asp:ListItem Text="0.45" Value="0.45"></asp:ListItem>
                                    <asp:ListItem Text="0.60" Value="0.60"></asp:ListItem>
                                </asp:DropDownList>
                                <div align="right">
                                    &nbsp;</div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" class="TDPart">
                                <ajaxToolkit:TabContainer ID="tabcontainerPatientVisit" runat="Server" ActiveTabIndex="1"
                                    OnActiveTabChanged="tabcontainerPatientVisit_ActiveTabChanged" AutoPostBack="true">
                                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelScheduledVisit" Height="100%">
                                        <HeaderTemplate>
                                            <div style="width: 150px; text-align: center;" class="lbl">
                                                Month View
                                            </div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" style="background-color: InactiveBorder">
                                                <tr>
                                                    <td width="20%" height="50" align="right">
                                                        <asp:Button ID="imgbtnPrevDate" runat="server" Text="Previous Month" OnClick="imgbtnPrevDate_Click"
                                                            Class="Buttons" />
                                                    </td>
                                                    <td width="30%" align="right">
                                                        <asp:DropDownList ID="ddlMonth" runat="server">
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
                                                        <asp:DropDownList ID="ddlYear" runat="server">
                                                            <asp:ListItem Text="2007" Value="2007"></asp:ListItem>
                                                            <asp:ListItem Text="2008" Value="2008"></asp:ListItem>
                                                            <asp:ListItem Text="2009" Value="2009"></asp:ListItem>
                                                            <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                                            <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                                            <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                                            <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                                            <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                            <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Button ID="btngo" runat="server" Text="Go" OnClick="btngo_Click" Class="Buttons" />
                                                    </td>
                                                    <td width="20%">
                                                        <asp:Button ID="btnNext" runat="server" Text="Next Month" OnClick="btnNext_Click"
                                                            Class="Buttons" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="Cal" runat="server" style="height: 100%;">
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel runat="server" ID="tabPanelScheduledVisit2" TabIndex="1">
                                        <HeaderTemplate>
                                            <div style="width: 150px; text-align: center;" class="lbl">
                                                Day View</div>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%" style="background-color: InactiveBorder">
                                                <tr>
                                                    <td width="20%" height="50" align="right">
                                                        <asp:Button ID="btnPreviousDay" runat="server" Text="Previous Day" Class="Buttons"
                                                            Visible="False" />
                                                    </td>
                                                    <td width="30%" colspan="2" align="right">
                                                    </td>
                                                    <td width="20%">
                                                        <asp:Button ID="btnNextDay" runat="server" Text="Next Day" Class="Buttons" Visible="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 75%">
                                                        <div id="DivDayView" runat="server">
                                                        </div>
                                                    </td>
                                                    <td style="width: 25%; vertical-align: top;">
                                                        <asp:ListBox ID="ddlDoctorList" Style="" runat="server" Width="320px" Height="340px">
                                                        </asp:ListBox>&nbsp;&nbsp;
                                                        <table width="100%">
                                                            <tr>
                                                                <td class="td-widget-bc-search-desc-ch3" valign="top" align="center">
                                                                    <asp:TextBox ID="txtDate" runat="server" MaxLength="10" Width="30%"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif">
                                                                    </asp:ImageButton>
                                                                    <ajaxToolkit:CalendarExtender ID="calExtDateofAccident" runat="server" TargetControlID="txtDate"
                                                                        PopupButtonID="imgbtnDateofAccident">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="btnshowall" runat="server" Text="Show All " OnClick="btnshowall_Click"
                                                                        Class="Buttons" />
                                                                    <asp:Button ID="btnsearchbydoctor" runat="server" Text="Search By Doctor" Class="Buttons"
                                                                        OnClick="btnsearchbydoctor_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <th scope="col" colspan="3">
                                <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                <asp:TextBox ID="txtCaseID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                <asp:HiddenField ID="hdnSessionValue" runat="server" Value=""></asp:HiddenField>
                            </th>
                        </tr>
                        <tr>
                            <th scope="col" colspan="3">
                                <asp:Label ID="lbl25" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl50" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl75" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lbl0" runat="server" Text=""></asp:Label>
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
    <asp:TextBox ID="hdnCaseID" runat="server" Style="visibility: hidden;" Width="10px"></asp:TextBox>
    <div id="divid" style="position: absolute; width: 700px; height: 500px; background-color: #DBE6FA;
        visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4; width: 700px">
            <a onclick="document.getElementById('divid').style.zIndex = '-1'; document.getElementById('divid').style.visibility='hidden';  var button = document.getElementById('<%=btnCls.ClientID%>');     button.click(); " 
style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="600px" width="700px"></iframe>
    </div>
    <input type="button" runat="server" style="visibility: hidden" id="btnHdn" onclick="btnHdn_Click" />
     <div style="visibility: hidden;">
        <asp:Button ID="btnCls" Text="X" BackColor="#B5DF82" BorderStyle="None" runat="server"
            OnClick="txtUpdate_Click" /></div>
</asp:Content>
