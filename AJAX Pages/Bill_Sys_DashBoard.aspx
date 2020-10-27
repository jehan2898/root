<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_DashBoard.aspx.cs" Inherits="Bill_Sys_DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <script language="javascript" type="text/javascript">
        function OpenPage(obj) {
            // alert(obj);
        }

        function OpenReport(obj) {
            //alert(obj);
            document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value = obj;
            //alert(document.getElementById('_ctl0_ContentPlaceHolder1_hdnSpeciality').value);
            document.getElementById('_ctl0_ContentPlaceHolder1_btnSpecial').click();

        }
       
    </script>
    <div id="Update_div" style="">

    </div>
    <div id="divDashBoard" style="background-color:white">
        
       <!--start-->
        <table id="Table1" style="margin:30px; margin-top:0px; margin-bottom:0px; background-color: white; width: 92%; border-style:solid; border-width:1px; border-color:#B5DF82; padding-right: 3px; padding-left: 3px; padding-bottom: 3px;
            height: 100%;">
           <caption>&nbsp;&nbsp;</caption>
            <tr>
                <td style="width:100%;">
                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                    <table id="TblMissingSpecialty" runat="server" border="0" cellspacing="0" cellpadding="0"
                       style="width: 99%; height: 130px; float: left; position: relative; left: 0px;
                       top: 0px;vertical-align:top" visible="false">
                        <tr>
                            <td class="TDHeading" style="width: 99%" valign="top">
                                                        Missing Specialty
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99%" class="TDPart" valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            You have
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                               </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99%; height: 10px;" class="SectionDevider">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
              <td style="width:100%;">
                <asp:UpdatePanel ID="UpdatePnl_Today" runat="server">
                   <ContentTemplate>
                     <table id="TblToday" runat="server" cellspacing="0" cellpadding="0" visible="false"
                        style="margin:1px; float:left; position:relative; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                        <tr style="height:2.5em">
                            <td colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:Left;  vertical-align:middle;">
                                <table border="0" id="Table8" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                    <tr>
                                        <td style="padding-left:3px; width:98%; text-align:center; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                        Today's&nbsp;Appointment
                                        </td>
                                        <td style="width:2%;" title="Refresh Today's Appointment">
                                            <asp:ImageButton width="1.00em" height="1.00em" ImageUrl="Images/refresh.gif" ID="ImgBtn_Today" runat="server" ImageAlign="Right" />        
                                        </td>
                                    </tr>
                                </table>                               
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em"> 
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">
                                No&nbsp;Show
                            </td>
                            <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                <asp:Label ID="LblNoshow_Today2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em"> 
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">
                                   Scheduled
                              </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                  <asp:Label ID="LblScheduled_Today2" runat="server"></asp:Label>
                      
                            <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_Today">
                                <ProgressTemplate>
                         
                                        <asp:Image ID="img1" runat="server" style="position: absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                            
                           
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                                    </td>  
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                          <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">
                                Completed
                            </td>
                         <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                   <asp:Label ID="LblComplete_Today2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td ID="Referral_today" runat="server" style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">
	                        </td>
                            <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                            font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                              <asp:Label ID="LblNewReferral_Today2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                    </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePnl_Weekly" runat="server">
                        <ContentTemplate>
                      <table id="TblWeekly" runat="server" cellpadding="0" cellspacing="0" visible="false"
                        style="margin:1px; float:left; position:relative; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                        <tr style="height:2.5em">
                            <td colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                <table id="Table6" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                    <tr>
                                        <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                            Weekly&nbsp;Appointment
                                        </td>
                                        <td style="width:2%" title="Refresh Weekly Appointment">
                                            <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif" ID="ImgBtn_Weekly" runat="server" ImageAlign="right"/>
                                        </td>
                                    </tr>
                                </table>
                           </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    No&nbsp;Show
                            </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                <asp:Label ID="LblNoshow_Weekly2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">   
	                                     Scheduled
                            </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                <asp:Label ID="LblScheduled_Weekly2" runat="server"></asp:Label>
                                    <asp:UpdateProgress ID="UpdateProgress_weekly" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_Weekly">
                                        <ProgressTemplate>
                                        <asp:Image ID="img2" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Completed
                            </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                <asp:Label ID="LblComplete_Weekly2" runat="server"></asp:Label>
                            </td>
                        </tr>
                             <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                    </table>   
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePnl_Status" runat="server" Visible="true" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table id="TblBillStat" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                style="margin:1px; position:relative; float:left; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                                <tr style="height:2.5em">
                                    <td  colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                       <table id="Table7" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                                    Bill&nbsp;Status
                                                </td>
                                                <td style="width:2%" title="Refresh Bill Status">
                                                    <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif"  ID="ImgBtn_Status" runat="server" ImageAlign="right"/>         
                                                </td>
                                            </tr>
                                       </table>
                                    </td>
                                  </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">   
	                                     Paid&nbsp;Bills
                                    </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblPaid_Status2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Unpaid&nbsp;Bills
                                    </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblUnPaid_Status2" runat="server"></asp:Label>
                                   <asp:UpdateProgress ID="UpdateProgress_billstatus" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_Status">
                                        <ProgressTemplate>
                                        <asp:Image ID="img3" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </td>
                                    
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                  </asp:UpdatePanel>
                   <asp:UpdatePanel ID="UpdatePnl_Desk" runat="server" Visible="true" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table id="Tbl_Desk" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                style="margin:1px; position:relative; float:left; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                                <tr style="height:2.5em">
                                    <td  colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                       <table id="Table10" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                                        color: #000000;	text-decoration: none; ">
                                                    Desk
                                                </td>
                                                <td style="width:2%" title="Refresh Desk">
                                                    <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif"  ID="ImgBtn_Desk" runat="server" ImageAlign="right"/>         
                                                </td>
                                            </tr>
                                       </table>
                                    </td>
                                  </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">  
	                                      Bills&nbsp;Due&nbsp;For&nbsp;Litigation
                                    </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblBillsDue_Desk2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-
	                                    <asp:UpdateProgress ID="UpdateProgress_desk" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_Desk">
                                        <ProgressTemplate>
                                        <asp:Image ID="img4" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
	                                    </td>
	                                    
	                                    
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em;">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                  </asp:UpdatePanel> 
                  <asp:UpdatePanel ID="UpdatePnl_missingdoc" runat="server">
                        <ContentTemplate>
                      <table id="Table2" runat="server" cellpadding="0" cellspacing="0" visible="true"
                        style="margin:1px; float:left; position:relative; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                        <tr style="height:2.5em">
                            <td colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                <table id="Tbl_missingdoc" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                    <tr>
                                        <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                            Missing&nbsp;Documents
                                        </td>
                                        <td style="width:2%" title="Refresh Weekly Appointment">
                                            <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif" ID="ImgBtn_missingdoc" runat="server" ImageAlign="right"/>
                                        </td>
                                    </tr>
                                </table>
                           </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Missing Documents
                            </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                <asp:Label ID="Lblshow_missingdoc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-
                        <asp:UpdateProgress ID="UpdateProgress_missingdoc" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_missingdoc">
                            <ProgressTemplate>
                            <asp:Image ID="img_missingdoc" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                Height="25px" Width="24px"></asp:Image>
                            </ProgressTemplate>
                        </asp:UpdateProgress></td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>                       
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                        <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                            <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                        </tr>
                    </table>   
                    </ContentTemplate>
                    </asp:UpdatePanel>
                  <asp:UpdatePanel ID="UpadatePanel_MissingInf" runat="server" Visible="true" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table id="TblMissingInf" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                style="margin:1px; position:relative; float:left; border: solid 1px #B5DF82;width:33%; border-collapse:collapse;">
                                <tr style="height:2.5em">
                                    <td  colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                       <table id="Table12" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                                    Missing Information (on open cases)
                                                </td>
                                                <td style="width:2%" title="Refresh Missing Information">
                                                    <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif"  ID="ImgBtn_MissingInf" runat="server" ImageAlign="right"/>         
                                                </td>
                                            </tr>
                                       </table>
                                    </td>
                                  </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">  
	                                      Insurance&nbsp;Company
                                    </td>
                                     <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                              <asp:Label ID="LblCompany_MissingInf2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Attorney
                                    </td>
                                     <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                              <asp:Label ID="LblAttorney_MissingInf2" runat="server"></asp:Label>
                                    <asp:UpdateProgress ID="UpdateProgress_missinginf" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpadatePanel_MissingInf">
                                        <ProgressTemplate>
                                        <asp:Image ID="img5" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </td>
                                    
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Claim&nbsp;Number
                                    </td>
                                     <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                              <asp:Label ID="LblClaim_MissingInf2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Report&nbsp;Number
                                    </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblReport_MissingInf2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Policy&nbsp;Holder
                                    </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblPolicyHldr_MissingInf2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="Missing_Inf" runat="server" style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    <asp:Label id="Missing_Inflbl" style="font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em;" runat="server"></asp:Label>
	                                    
                                    </td>
                                    <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblUNSENTNF2_MissingInf2" runat="server"></asp:Label>
                                    </td>
                                </tr>                                
                            </table>
                        </ContentTemplate>
                  </asp:UpdatePanel>
                   <asp:UpdatePanel ID="UpdatePnl_Report" runat="server" Visible="true" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table id="TblReport" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                style="margin:1px; position:relative; float:left; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                                <tr style="height:2.5em">
                                    <td  colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                       <table id="Table14" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                                    Report Section
                                                </td>
                                                <td style="width:2%" title="Refresh Report Section">
                                                    <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif"  ID="ImgBtn_Report" runat="server" ImageAlign="right"/>         
                                                </td>
                                            </tr>
                                       </table>
                                    </td>
                                  </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Received&nbsp;Report
                                    </td>
                                <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblReceived_Report2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Pending&nbsp;Report
                                    </td>
                                    <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblPending_Report2" runat="server"></asp:Label>
                                   <asp:UpdateProgress ID="UpdateProgress_report" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_Report">
                                        <ProgressTemplate>
                                        <asp:Image ID="img6" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </td>
                                    
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                  </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePnl_Procedure" runat="server" Visible="true" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table id="TblProcedure" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                style="margin:1px; position:relative; float:left; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                                <tr style="height:2.5em">
                                    <td  colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                       <table id="Table16" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                                    Procedure Status
                                                </td>
                                                <td style="width:2%" title="Refresh Procedure Status">
                                                    <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif"  ID="ImgBtn_Procedure" runat="server" ImageAlign="right"/>         
                                                </td>
                                            </tr>
                                       </table>
                                    </td>
                                  </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">  
	                                      Billed&nbsp;Procedure&nbsp;Codes
                                    </td>
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblBilled_Procedure2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Unbilled&nbsp;Procedure&nbsp;Codes
                                    
                                    <asp:UpdateProgress ID="UpdateProgress_procedure" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_Procedure">
                                        <ProgressTemplate>
                                        <asp:Image ID="img7" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </td>
                                    
                           <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblUnBilled_Procedure2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                  </asp:UpdatePanel>
                  <asp:UpdatePanel ID="UpdatePnl_Visits" runat="server" Visible="true" ChildrenAsTriggers="true">
                        <ContentTemplate>   
                            <table id="Tbl_Visits" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                style="margin:1px; position:relative; float:left; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                                <tr style="height:2.5em">
                                    <td  colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                       <table id="Table5" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                                    Visits
                                                </td>
                                                <td style="width:2%" title="Refresh Procedure Status">
                                                    <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif"  ID="ImgBtn_Visits" runat="server" ImageAlign="right"/>         
                                                </td>
                                            </tr>
                                       </table>
                                    </td>
                                  </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">  
	                                     Total&nbsp;Visits
                                    </td>
                                     <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblTotal_Visits2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">  
	                                    Billed&nbsp;Visits
	                                </td>
                                     <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblBilled_Visits2" runat="server"></asp:Label>
                                        <asp:UpdateProgress ID="UpdateProgress_visits" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_Visits">
                                            <ProgressTemplate>
                                                <asp:Image ID="img8" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                Height="25px" Width="24px"></asp:Image>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                    
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">  
	                                    Unbilled&nbsp;Visits
                                    </td>
                                     <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblUnBilled_Visits2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                 <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                 <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                               
                            </table>       
                      <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="LblTotal_Visits2"
                        PopupControlID="pnlTotalVisit"  Position="Right" />
                      <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="LblBilled_Visits2"
                        PopupControlID="pnlBilledVisit" Position="Right" />
                      <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="LblUnBilled_Visits2"
                        PopupControlID="pnlUnBilledVisit" Position="Right" />  
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     <asp:UpdatePanel ID="UpdatePnl_PateintsVisits" runat="server" Visible="true" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <table id="TblPatientVisits" runat="server" cellpadding="0" cellspacing="0" visible="false"
                                style="margin:1px; position:relative; float:left; border: solid 1px #B5DF82; width:33%; border-collapse:collapse;">
                                <tr style="height:2.5em">
                                    <td  colspan="2" style="width:100%; background-color:#B5DF82; font-weight:bold; text-align:center; vertical-align:middle">
                                       <table id="Table20" runat="server" cellpadding="0" cellspacing="0" style="width:100%">
                                            <tr>
                                                <td style="padding-left:3px; width:98%; font-family: Arial, Helvetica, sans-serif; font-size: 1.1em;
	                                        color: #000000;	text-decoration: none; ">
                                                    Patient Visits
                                                </td>
                                                <td style="width:2%" title="Refresh Patient Visit">
                                                    <asp:ImageButton width="1em" height="1em" ImageUrl="Images/refresh.gif"  ID="ImgBtn_PateintsVisits" runat="server" ImageAlign="right"/>         
                                                </td>
                                            </tr>
                                       </table>
                                    </td>
                                  </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Patients&nbsp;Scheduled
                                    </td>
                                    <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblScheduled_PateintsVisits2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Patients&nbsp;No&nbsp;Show
                                    </td>
                                    <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblNoshow_PateintsVisits2" runat="server"></asp:Label>
                                       <asp:UpdateProgress ID="UpdateProgress_Patientsvisits" runat="server" DisplayAfter="0" AssociatedUpdatePanelId="UpdatePnl_PateintsVisits">
                                        <ProgressTemplate>
                                        <asp:Image ID="img9" runat="server" style="position:absolute; z-index:1; left: 50%; top: 50%" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                            Height="25px" Width="24px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </td>
                                
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Patients&nbsp;Rescheduled
                                    </td>
                                    <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblRescheduled_PateintsVisits2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td style="padding:3px; width:70%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">    
	                                    Patients&nbsp;Visit&nbsp;Completed
                                    </td>
                                    <td style="padding:3px; width:30%; text-align:right; vertical-align:top; font-family:Verdana;
                                        font-weight:normal; font-size:1.05em; border-bottom: 1px solid #e1e1e1;">
                                        <asp:Label ID="LblComplete_PateintsVisits2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>
                                <tr style="border-bottom:solid 1px #B5DF82; height:1.5em">
                                    <td colspan="2" style="padding:3px; width:100%; text-align:left; vertical-align:top; font-family:Verdana;
	                                    font-weight:normal; font-size:1.00em; border-bottom: 1px solid #e1e1e1;">-</td>
                                </tr>                                
                            </table>
                        </ContentTemplate>
                  </asp:UpdatePanel>    
                </td>
            </tr>
 
        </table>
       <!--end-->
        
     <%--   <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
            height: 100%; visibility:collapse" visible="false">
            <tr>
                <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;">
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
                                            <table id="tblMissingSpeciality" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 99%; height: 130px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Missing Speciality</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    You have
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblMissingSpecialityText" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td style="width: 99%; height: 10px;" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" id="tblDailyAppointment" runat="server" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 170px; float: left; position: relative; vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Today's Appointment</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        <asp:Label ID="lblAppointmentToday" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblWeeklyAppointment" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%">
                                                        Weekly &nbsp;Appointment</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart">
                                                        <asp:Label ID="lblAppointmentWeek" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblBillStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; vertical-align: top; float: left; position: relative;"
                                                visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Bill Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                               <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblDesk" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 33%;
                                                height: 195px; float: left; position: relative;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%;" valign="top" >
                                                        Desk</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have&nbsp;
                                                        <asp:Label ID="lblDesk" runat="server"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblMissingInfo" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Missing Information</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%;" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblMissingInformation" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblReportSection" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Report Section</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblReport" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblBilledUnbilledProcCode" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Procedure Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblProcedureStatus" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="tblVisits" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 33%;
                                                height: 195px; float: left; position: relative; left: 0px; top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Visits</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        <asp:Label ID="lblVisits" runat="server" visible="true"></asp:Label>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    You have
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                      <%--               <ul style="list-style-type: disc; padding-left: 60px;">
                                                                        <li>
                                                                            <a id="hlnkTotalVisit" href="#" runat="server">
                                                                            <asp:Label ID="lblTotalVisit" runat="server"></asp:Label></a>&nbsp;Total Visit
                                                                        </li>
                                                                        <li>
                                                                            <a id="hlnkBilledVisit" href="#" runat="server">
                                                                            <asp:Label ID="lblBilledVisit" runat="server"></asp:Label></a>&nbsp;Billed Visit
                                                                        </li>
                                                                        <li><a id="hlnkUnBilledVisit" href="#" runat="server">
                                                                            <asp:Label ID="lblUnBilledVisit" runat="server"></asp:Label></a>&nbsp;UnBilled Visit
                                                                        </li>
                                                                    </ul>
                                                                    <ajaxToolkit:PopupControlExtender ID="PopExTotalVisit" runat="server" TargetControlID="hlnkTotalVisit"
                                                                        PopupControlID="pnlTotalVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                                    <ajaxToolkit:PopupControlExtender ID="PopExBilledVisit" runat="server" TargetControlID="hlnkBilledVisit"
                                                                        PopupControlID="pnlBilledVisit" Position="Center" OffsetX="100" OffsetY="10" />
                                                                    <ajaxToolkit:PopupControlExtender ID="PopExUnBilledVisit" runat="server" TargetControlID="hlnkUnBilledVisit"
                                                                        PopupControlID="pnlUnBilledVisit" Position="Center" OffsetX="100" OffsetY="10" /> --%>
                                                                <%--</td>         
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%; height: 10px;" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                                    <table id="tblPatientVisitStatus" runat="server" border="0" cellpadding="0" cellspacing="0"
                                                style="width: 33%; height: 195px; float: left; position: relative; left: 0px;
                                                top: 0px;vertical-align:top" visible="false">
                                                <tr>
                                                    <td class="TDHeading" style="width: 99%" valign="top">
                                                        Patient Visit Status</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="TDPart" valign="top">
                                                        You have &nbsp;<asp:Label ID="lblPatientVisitStatus" runat="server"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 99%" class="SectionDevider">
                                                    </td>
                                                </tr>
                                            </table>
                   
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="RightCenter" style="width: 10px; height: 100%;">
                </td>
            </tr>
            <tr>
                <td class="LeftBottom">
                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                </td>
                <td class="CenterBottom">
                </td>
                <td class="RightBottom" style="width: 10px">
                </td>
            </tr>
        </table>  --%>
        <table id="table_space" runat="server">
        <tr>
        <td>
        &nbsp;&nbsp;
        </td>
        </tr>
        </table>
    </div>
    <asp:Panel ID="pnlTotalVisit" runat="server" Visible="true">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataGrid ID="grdTotalVisit" runat="server" Width="25px" CssClass="GridTable"
                            AutoGenerateColumns="false">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    
        <asp:Panel ID="pnlBilledVisit" runat="server" visible="true"> 
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataGrid ID="grdVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
      
        <asp:Panel ID="pnlUnBilledVisit" runat="server" visible="true">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <asp:DataGrid ID="grdUnVisit" runat="server" Width="25px" CssClass="GridTable" AutoGenerateColumns="false">
                            <ItemStyle CssClass="GridRow" />
                            <Columns>
                                <asp:BoundColumn DataField="Speciality" HeaderText="Speciality"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SP_COUNT" HeaderText="Count" ItemStyle-HorizontalAlign="Right">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle CssClass="GridHeader" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    <asp:Button visible="false" ID="btnSpecial" runat="server" OnClick="btnSpecial_Click" Text="Special"
        Width="0px" Height="0px" />
    <asp:HiddenField ID="hdnSpeciality" runat="server" />
</asp:Content>
