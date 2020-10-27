<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ReCaseDetails.aspx.cs" Inherits="Bill_Sys_ReCaseDetails" Title="Green Your Bills - Workarea"%>

<%--<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>--%>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization"
    TagPrefix="CPA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="UserControl/Bill_Sys_AssociateCases.ascx" TagName="Bill_Sys_AssociateCases"
    TagPrefix="ASC" %>
<%@ Register Src="UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl" TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <script type="text/javascript" src="validation.js"></script>
    <script language="javascript" type="text/javascript">
    
    function ShowNotes()
    {
     //   document.getElementById('test').value = 10;
    }
    
    function HideNotes()
    {
     //   document.getElementById('test').value = 20;
    }
    
    function Check(p_szname,p_fieldname)
    {

        if(document.getElementById(""+p_szname).value == 'NA')
        {
            alert('Please Select '+p_fieldname);
            return false;
        }
        else if(document.getElementById(""+p_szname).value == '')
        {
            alert('Please Enter '+p_fieldname);
            return false;
        }
        return true;
    }
    
   
    </script>

    <script type="text/javascript">
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
         
         function ShowGenerateNF2Link()         
         {
            var chkLink = document.getElementById('_ctl0_ContentPlaceHolder1_chkStatusProc');
            if(chkLink.checked)
            {
               return true;
            }
            else
            {
               alert('Please check the status');
               return false;
            }
         }
         
         function showDiagnosisCodePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.height='180px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.visibility = 'visible';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.position = "absolute";
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.top = '300px';
	        document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.left ='620px';
       }
       
       function showAdjusterPanel()
        {
            document.getElementById('divAdjuster').style.visibility='visible';
            document.getElementById('divAdjuster').style.position='absolute';
            document.getElementById('divAdjuster').style.left= '300px'; 
            document.getElementById('divAdjuster').style.top= '300px'; 
            document.getElementById('divAdjuster').style.zIndex=1; 
        }
        
       function CloseDiagnosisCodePopup()
       {
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.height='0px';
            document.getElementById('_ctl0_ContentPlaceHolder1_pnlDiagnosisCode').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
       function GetInsuranceValue(object, arg)
       {
           document.getElementById("<%=hdninsurancecode.ClientID %>").value = arg.get_value();
       }
       function showPateintFrame(objCaseID,objflag,objCompanyID)
        {
	    // alert(objCaseID + ' ' + objCompanyID);
	        var obj3 = "";
            document.getElementById('divfrmPatient').style.zIndex = 1;
            document.getElementById('divfrmPatient').style.position = 'absolute'; 
            document.getElementById('divfrmPatient').style.left= '400px'; 
            document.getElementById('divfrmPatient').style.top= '250px'; 
            document.getElementById('divfrmPatient').style.visibility='visible'; 
            document.getElementById('frmpatient').src="CaseDetailsPopup.aspx?CaseID="+objCaseID+"&cmpId="+ objCompanyID+"&flag="+objflag+"";
            return false;   
        }
        
        function ClosePatientFramePopup()
               {
                 //   alert("");
                   //document.getElementById('divfrmPatient').style.height='0px';
                    document.getElementById('divfrmPatient').style.visibility='hidden';
                   document.getElementById('divfrmPatient').style.top='-10000px';
                    document.getElementById('divfrmPatient').style.left='-10000px';
             }
    </script>

    <script type="text/javascript" src="validation.js"></script>

    <table id="First" style="width: 100%; height: 100%" bgcolor="white">
        <tr>
        <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
            padding-top: 3px; height: 100%">
            <table id="MainBodyTable" style="width: 100%; border: 0;">
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
                        
                        <td style="width: 100%" >
                        <table style="width:100%" >
                           <tr>
                            <td>
                             <asp:Repeater ID="rptPatientDeskList" runat="server" OnItemCommand="rptPatientDeskList_ItemCommand">
                            <HeaderTemplate>
                                <table align="left" cellpadding="0" cellspacing="0" style="width: 100%;
                                        border: #8babe4 1px solid #B5DF82;" >
                                    <tr>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" >
                                          <b>  Case#</b>
                                        </td>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                           <b> Chart No</b>
                                        </td>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                           <b>Patient Name</b> 
                                        </td>
                                        <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                            <b>Insurance Name</b>
                                        </td>
                                        <td bgcolor="#B5DF82"  class="lbl" style="font-weight: bold;">
                                           <b> Accident Date</b>
                                        </td>
                                        <td bgcolor="#B5DF82"  style="height:50%">
                                           
                                        </td>
                                        
                                    </tr>
                                   </HeaderTemplate>
                                   <ItemTemplate>
                                        <tr>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;"> 
                                                  <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;">
                                                <%# DataBinder.Eval(Container,"DataItem.SZ_INSURANCE_NAME")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl"style="border: 1px solid #B5DF82;" >
                                                <%# DataBinder.Eval(Container,"DataItem.DT_ACCIDENT","{0:MM/dd/yyyy}")%>
                                            </td>
                                            <td bgcolor="white" class = "lbl" style="border: 1px solid #B5DF82;">
                                               <asp:LinkButton ID="lnkPatientInfoPDF" ToolTip="Summary Sheet" runat="server" CommandName="genpdf"><img src="Images/actionEdit.gif" style="border: none;"/></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate> 
                              <FooterTemplate></table></FooterTemplate>
                               </asp:Repeater>
                                </td>
                           </tr>
                        </table>
                           
                           
                        </td>
                    </tr>
             
                <tr>
                        <td class="LeftCenter" style="height: 100%">
                        </td>
                        <td class="Center" valign="top">
                            <table style="width: 100%; height: 100%">
                          <%--      <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False" OnItemCommand="grdPatientDeskList_ItemCommand" Visible="false">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_CHART_NO" HeaderText="Chart No." HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <a href="#" onclick="openTypePage('a')">
                                                            <img src="Images/actionEdit.gif" alt="" style="border: none;" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPatientInfoPDF1" ToolTip="Summary Sheet" runat="server" CommandName="genpdf"><img src="Images/actionEdit.gif" style="border: none;"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>--%>
                                   <tr>
                                    <td style="width: 100%;" >
                                        <table class="ContentTable" style="width: 100%; height: 250px;">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <UserMessage:MessageControl runat="server" id="usrMessage" />
                                                   
                                                </td>
                                                <td align="right">
                                                    <div id="divAssociateCaseID" runat="server" style="float: left; text-align: left;">
                                                    </div>
                                                    
                                                </td>
                                               
                                                 
                                                <td align="right" width="65%">
                                                    <asp:LinkButton ID="lnkNF2Envelope" runat="server" CssClass="lbl" OnClick="lnkNF2Envelope_Click">NF2 Envelope</asp:LinkButton>
                                                    <asp:CheckBox ID="chkStatusProc" runat="server" />
                                                    <asp:TextBox Width="70px" ID="txtNF2Date" runat="server" onkeypress="return clickButton1(event,'/')"
                                                        MaxLength="10"></asp:TextBox>
                                                    <asp:ImageButton ID="imgbtnNF2Date" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    &nbsp;
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtNF2Date"
                                                        PopupButtonID="imgbtnNF2Date" Enabled="True" />
                                                    <asp:LinkButton ID="lnkGenerateNF2" runat="server" CssClass="lbl" OnClick="lnkGenerateNF2_Click">NF2 </asp:LinkButton>
                                                    &nbsp; <a id="hlnkShowMemo" href="#" runat="server" title="Add Memo"  class="lbl">Memo</a>
                                                    &nbsp; <a id="hlnkAssignSupplies" href="#" runat="server" title="Assign Supplies"
                                                        visible="false" class="lbl">Supplies</a> <a id="hlnkShowNotes" href="#" runat="server"
                                                            title="Add Note" class="lbl">Add Note</a>
                                                    <!--<img src="Images/actionEdit.gif" style="border-style: none;" /> -->
                                                    <ajaxToolkit:PopupControlExtender ID="PopEx" runat="server" TargetControlID="hlnkShowNotes"
                                                        PopupControlID="pnlShowNotes" Position="Center" OffsetX="-600" OffsetY="-10" />
                                                    <ajaxToolkit:PopupControlExtender ID="PopupCEMemo" runat="server" TargetControlID="hlnkShowMemo"
                                                        PopupControlID="pnlMemo" Position="Center" OffsetX="-600" OffsetY="-10" />
                                                    <ajaxToolkit:PopupControlExtender ID="PopupCEAssignSupplies" runat="server" TargetControlID="hlnkAssignSupplies"
                                                        PopupControlID="pnlAssignSupplies" Position="Center" OffsetX="-600" OffsetY="-10" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="height: 15px; text-align: left;" >
                                                    <%-- <asp:LinkButton ID="hlnkAssociate" runat="server" visible="false" OnClick="hlnkAssociate_Click">Associate Diagnosis Codes</asp:LinkButton> |        
                       <asp:LinkButton ID="hlnkPatientDesk" runat="server"   OnClick="hlnkPatientDesk_Click" >Patient Desk</asp:LinkButton>--%>
                                                    <ajaxToolkit:TabContainer ID="tabcontainerPatientEntry" runat="Server" ActiveTabIndex="0"
                                                        >
                                                        <ajaxToolkit:TabPanel runat="server" ID="tabPnlViewAll" TabIndex="6" Height="800px">
                                                            <HeaderTemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    View All
                                                                </div>
                                                            </HeaderTemplate>
                                                            <ContentTemplate>
                                                                <div align="left">
                                                                <asp:DataList ID="DtlView" runat="server" CssClass="TDPart" BorderWidth="0px" BorderStyle="None" 
                                                                        BorderColor="#DEBA84" RepeatColumns="1" Width="100%">
                                                                        <ItemTemplate>
                                                                        <table id="lastTablel" runat="server" class="td-widget-lf-top-holder-ch" cellpadding="0" cellspacing="0" border="1" bgcolor="white">
                                                                        <tr>
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" border="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Personal Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" >
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    First Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FIRST_NAME")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Middle Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label ID="lblViewMiddleName" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Last Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_LAST_NAME") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    D.O.B
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_DOB") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Gender
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label runat="server" ID="lblViewGender" CssClass="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_CITY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewPatientState" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Home Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;<%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_PHONE")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Work
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    ZIP
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_ZIP")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    &nbsp;
												                                                                    <asp:CheckBox ID="chkViewWrongPhone" Visible="False" Enabled="False" runat="server" Text="Wrong Phone" TextAlign="Left" />
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Email
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_EMAIL")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Extn.
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_WORK_PHONE_EXTENSION")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Attorney
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label ID="lblViewAttorney" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Case Type
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label ID="lblViewCasetype" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Case Status
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewCaseStatus" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    SSN
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_SOCIAL_SECURITY_NO")%>
                                                                                                                </td>
                                                                                                            </tr>
										                                                <tr>
										    	                                              <td class="td-CaseDetails-lf-desc-ch">
											  	                                            <asp:Label ID="lblLocation" Text="Location" runat="server" class="lbl" Style="font-weight: bold;"></asp:Label>
											                                              </td>
											                                              <td class="td-CaseDetails-lf-data-ch">
												                                            <asp:Label runat="server" ID="lblVLocation1" class="lbl" ></asp:Label>
											                                              </td>
											                                              <td><asp:CheckBox ID="chkPatientTransport" Visible="False" Enabled="False" 														runat="server" Text="Transport" TextAlign="Left"></asp:CheckBox></td>
											                                              <td>&nbsp;</td>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Insurance Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Policy Holder
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem,"SZ_POLICY_HOLDER") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_NAME") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Ins. Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label ID="lblViewInsuranceAddress" runat="server" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ADDRESS") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_CITY") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_STATE") %>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    ZIP
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_ZIP") %>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_INSURANCE_PHONE")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                                                    FAX
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX_NUMBER")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch" style="height: 33px">
                                                                                                                    Contact Person
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch" style="height: 33px">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_CONTACT_PERSON")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Claim File#
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                 <a id="lnkViewInsuranceClaimNumber" style="text-decoration: underline; color:Blue;" runat="server" class="lbl"  
                                                                                                                 href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+claimno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_CLAIM_NUMBER")%></a>
                                                                                                                   <%--<ajaxToolkit:PopupControlExtender ID="popExtViewInsuranceClaimNumber" runat="server"  TargetControlID="lnkViewInsuranceClaimNumber"
                                                                                                                   PopupControlID="pnlClaimNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID=""  />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Policy #
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewPolicyNumber" style="text-decoration: underline; color: Blue;" runat="server" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+policyno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_POLICY_NUMBER")%> </a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtViewPolicyNumber" runat="server" TargetControlID="lnkViewPolicyNumber" 
                                                                                                                    PopupControlID="pnlPolicyNumber" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
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
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Accident Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Accident Date
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkDateOfAccList" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                     href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+dtaccident+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "DT_ACCIDENT_DATE")%></a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popExtDateOfAccList" runat="server" TargetControlID="lnkDateOfAccList" 
                                                                                                                    PopupControlID="pnlDateOfAccList" OffsetX="100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Plate Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewAccidentPlatenumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+plateno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' ><%# DataBinder.Eval(Container.DataItem, "SZ_PLATE_NO")%></a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentPlatenumber" runat="server" TargetControlID="lnkViewAccidentPlatenumber" 
                                                                                                                    PopupControlID="pnlPlateNo" OffsetX="-100" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Report Number
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                <a id="lnkViewAccidentReportNumber" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+accidentreportno+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' > <%# DataBinder.Eval(Container.DataItem, "SZ_REPORT_NO")%></a>
                                                                                                                <%--<ajaxToolkit:PopupControlExtender ID="popViewAccidentReportNumber" runat="server" TargetControlID="lnkViewAccidentReportNumber" 
                                                                                                                PopupControlID="pnlReportNO" OffsetX="-300" OffsetY="-200" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_ACCIDENT_CITY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewAccidentState" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Hospital Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_NAME")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Hospital Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_HOSPITAL_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Date Of Admission
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_ADMISSION_DATE")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Additional Patient
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PATIENT_FROM_CAR")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Describe Injury
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_DESCRIBE_INJURY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Patient Type
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblPatientType" class="lbl"></asp:Label>
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
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 446px;">
                                                                                            &nbsp;<b class="txt3">Employer Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_NAME")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Address
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ADDRESS")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    City
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_CITY")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    State
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <asp:Label runat="server" ID="lblViewEmployerState" class="lbl"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    ZIP
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_ZIP")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMPLOYER_PHONE")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Date Of First Treatment
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "DT_FIRST_TREATMENT")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    <asp:Label ID="lblView" runat="server" Text="Chart No." class="lbl" Font-Bold="true"></asp:Label>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    <asp:Label ID="lblViewChartNo" runat="server" class="lbl"></asp:Label>
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
                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="td-widget-lf-top-holder-division-ch">
                                                                                <table align="left" cellpadding="0" cellspacing="0" class="border" style="width: 490px;
                                                                                    border: 1px solid #B5DF82;" id="tblF">
                                                                                    <tr>
                                                                                        <td height="28" align="left" valign="middle" bgcolor="#B5DF82" class="txt2" style="width: 500px;">
                                                                                            &nbsp;<b class="txt3">Adjuster Information</b>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 490px;">
                                                                                            <!-- outer table to hold 2 child tables -->
                                                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0" bgcolor="white">
                                                                                                <tr>
                                                                                                    <td class="td-widget-lf-holder-ch">
                                                                                                        <!-- Table 1 - to hold the actual data -->
                                                                                                        <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Name
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <a id="lnkViewAdjusterName" style="text-decoration: underline; color: Blue;" runat="server" title="Claim List" class="lbl"
                                                                                                                    href="#" onclick='<%# "showPateintFrame(" + "\""+ Eval("SZ_CASE_ID") + "\""+ ",\"+adjustername+\",\"" + Eval("SZ_COMPANY_ID")  +"\");" %>' >
                                                                                                                    <asp:Label runat="server" ID="lblViewAdjusterName" class="lbl"></asp:Label>
                                                                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "SZ_ADJUSTER_NAME")%>--%></a>
                                                                                                                    <%--<ajaxToolkit:PopupControlExtender ID="popViewAdjusterName" runat="server" TargetControlID="lnkViewAdjusterName" 
                                                                                                                    PopupControlID="pnlAdjuster" OffsetX="100" OffsetY="-300" DynamicServicePath="" Enabled="True" ExtenderControlID="" />--%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Phone
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_PHONE")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Extension
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EXTENSION")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    FAX
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_FAX")%>
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-desc-ch">
                                                                                                                    Email
                                                                                                                </td>
                                                                                                                <td class="td-CaseDetails-lf-data-ch">
                                                                                                                    &nbsp;
                                                                                                                    <%# DataBinder.Eval(Container.DataItem, "SZ_EMAIL")%>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td>
                                                                                        <div id="abc" runat="server">
                                                                                        </div>
                                                                                        </td>
                                                                                       
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                       
                                                                            
                                                                        </tr>
                                                                    </table>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                                </div>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                        
                                        </table>
                                        </DIV> </ContentTemplate> </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlPersonalInformation" TabIndex="0"
                                            Height="300px">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Personal Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                First name
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtPatientFName" runat="server"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                Middle</div>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtMI" runat="server"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="5%" class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                Last name
                                                                                            </div>
                                                                                        </td>
                                                                                        <td width="32%">
                                                                                            <asp:TextBox ID="txtPatientLName" runat="server"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox Width="69%" ID="txtDateOfBirth" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                    MaxLength="10"></asp:TextBox>
                                                                                                <asp:ImageButton ID="imgbtnDateofBirth" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"
                                                                                                    PopupButtonID="imgbtnDateofBirth" Enabled="True" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                SSN #
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtSocialSecurityNumber" runat="server"></asp:TextBox></span></td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                Gender</div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlSex" runat="server" Width="153px">
                                                                                                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                                                                                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Date of birth</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Address
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                                &nbsp;</td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="90%" ID="txtPatientAddress" runat="server"></asp:TextBox>
                                                                                <span class="tablecellControl">
                                                                                    <asp:TextBox Visible="False" ID="txtPatientStreet" runat="server"></asp:TextBox>
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    City</div>
                                                                            </td>
                                                                            <td rowspan="3" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="3">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPatientCity" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtState" runat="server" Visible="False"></asp:TextBox>
                                                                                                    <extddl:ExtendedDropDownList ID="extddlPatientState" runat="server" Width="90%" Connection_Key="Connection_String"
                                                                                                        Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST" Selected_Text="--- Select ---"
                                                                                                        OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox ID="txtPatientZip" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPatientPhone" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Work</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtWorkPhone" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Extn.</div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtExtension" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:CheckBox ID="chkWrongPhone" Visible="false" runat="server" Text="Wrong Phone"
                                                                                                    TextAlign="Left" /></td>
                                                                                            <td class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Email</div>
                                                                                            </td>
                                                                                            <td colspan="3">
                                                                                                <asp:TextBox ID="txtPatientEmail" runat="server"></asp:TextBox>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Home phone</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                &nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Attorney</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <extddl:ExtendedDropDownList ID="extddlAttorney" Width="95%" runat="server" Connection_Key="Connection_String"
                                                                                    Procedure_Name="SP_MST_ATTORNEY" Flag_Key_Value="ATTORNEY_LIST" Selected_Text="--- Select ---"
                                                                                    OldText="" StausText="False" />
                                                                            </td>
                                                                            <td>
                                                                                <a id="hlnkShowAtornyInfo" href="#" runat="server" title="Attorney Info" class="lbl">
                                                                                    Info</a>
                                                                                <ajaxToolkit:PopupControlExtender ID="PopupAtornyInfo" runat="server" TargetControlID="hlnkShowAtornyInfo"
                                                                                    PopupControlID="pnlShowAtornyInfo" OffsetX="-600" OffsetY="-200" DynamicServicePath=""
                                                                                    Enabled="True" ExtenderControlID="" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    &nbsp;Case Type</div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <extddl:ExtendedDropDownList ID="extddlCaseType" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_TYPE" Flag_Key_Value="CASETYPE_LIST" Selected_Text="--- Select ---"
                                                                                                    OldText="" StausText="False" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <div class="lbl">
                                                                                                Case Status</div>
                                                                                        </td>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <extddl:ExtendedDropDownList ID="extddlCaseStatus" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_CASE_STATUS" Flag_Key_Value="CASESTATUS_LIST" Selected_Text="--- Select ---"
                                                                                                    Flag_ID="txtCompanyID.Text.ToString();" OldText="" StausText="False" />
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <asp:CheckBox ID="chkTransportation" runat="server" Text="Transport" TextAlign="Left"
                                                                                                Visible="false"></asp:CheckBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                       <td></td>
                                                                       </tr>
                                                                       
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    &nbsp;<asp:Label ID="lblLocationddl" Text="Location" runat="server" class="lbl" ></asp:Label></div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">
                                                                                                <extddl:ExtendedDropDownList ID="extddlLocation" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                                                    Procedure_Name="SP_MST_LOCATION" Flag_Key_Value="LOCATION_LIST" Selected_Text="--- Select ---"
                                                                                                    OldText="" StausText="False" />
                                                                                            </span>
                                                                                        </td>
                                                                                        
                                                                                        
                                                                                        
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                            </td>
                                                                            <td class="tablecellSpace" rowspan="1">
                                                                            </td>
                                                                            <td colspan="4" rowspan="1">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">&nbsp;</span></td>
                                                                                        <td width="13%" class="tablecellLabel" style="height: 16px">
                                                                                            <div class="lbl">
                                                                                                &nbsp;&nbsp;</div>
                                                                                        </td>
                                                                                        <td width="25%" style="height: 16px">
                                                                                            <span class="tablecellControl">&nbsp;&nbsp; </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox Width="70%" ID="txtDateofAccident" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                        MaxLength="10" Visible="False"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="imgbtnDateofAccident" runat="server" ImageUrl="~/Images/cal.gif"
                                                                                                        Visible="False" />
                                                                                                    &nbsp;
                                                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDateofAccident"
                                                                                                        PopupButtonID="imgbtnDateofAccident" Enabled="True" />
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPlatenumber" Visible="False" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtPolicyReport" Visible="False" runat="server" CssClass="text-box"></asp:TextBox>&nbsp;
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <div class="lbl">
                                                                                    Employer Information
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Name
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtEmployerName" runat="server"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Address
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                                &nbsp;</td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox ID="txtEmployerAddress" runat="server" Width="99%"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    City</div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerCity" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerState" runat="server" Visible="False"></asp:TextBox>
                                                                                                    <extddl:ExtendedDropDownList ID="extddlEmployerState" runat="server" Selected_Text="--- Select ---"
                                                                                                        Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE" Connection_Key="Connection_String"
                                                                                                        Width="90%" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel" width="13%">
                                                                                                <div class="lbl">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerZip" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtEmployerPhone" runat="server"></asp:TextBox>
                                                                                                </span>
                                                                                            </td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    &nbsp;<asp:Label ID="lblDateofFirstTreatment" runat="server" Text="Date of First Treatment"
                                                                                                        class="lbl"></asp:Label></div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">&nbsp;<asp:TextBox Width="45%" ID="txtDateofFirstTreatment"
                                                                                                    runat="server" onkeypress="return clickButton1(event,'/')" MaxLength="10"></asp:TextBox><asp:ImageButton
                                                                                                        ID="imgbtnDateofFirstTreatment" runat="server" ImageUrl="~/Images/cal.gif" /><ajaxToolkit:CalendarExtender
                                                                                                            ID="CalendarExtender1" runat="server" TargetControlID="txtDateofFirstTreatment"
                                                                                                            PopupButtonID="imgbtnDateofFirstTreatment" Enabled="True" />
                                                                                                </span>
                                                                                            </td>
                                                                                            <td class="tablecellLabel" style="width: 13%">
                                                                                                <div class="lbl">
                                                                                                    &nbsp;<asp:Label ID="lblChart" runat="server" Text="Chart No." class="lbl"></asp:Label></div>
                                                                                            </td>
                                                                                            <td style="width: 25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtChartNo" runat="server"></asp:TextBox></span></td>
                                                                                            <td class="tablecellLabel">
                                                                                                &nbsp;</td>
                                                                                            <td>
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Phone</div>
                                                                            </td>
                                                                        </tr>
                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlInsuranceInformation" TabIndex="2">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Insurance Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 340px;">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Policy Holder
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtPolicyHolder" runat="server" CssClass="text-box" Width="61%"></asp:TextBox>
                                                                                &nbsp; <a id="lnkSearchInsuranceCompany" href="#" runat="server" title="Search Insurance Company"
                                                                                    class="lbl">Search Insurance Company</a>
                                                                                <ajaxToolkit:PopupControlExtender ID="peSearchInsuranceCompany" runat="server" TargetControlID="lnkSearchInsuranceCompany"
                                                                                    PopupControlID="pnlSearchInsuranceCompany" Position="Center" OffsetX="-220" OffsetY="-10" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Name
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                             <ajaxToolkit:AutoCompleteExtender runat="server" ID="ajAutoIns" EnableCaching="true"
                                                                                    DelimiterCharacters="" MinimumPrefixLength="1" CompletionInterval="500" TargetControlID="txtInsuranceCompany" 
                                                                                    ServiceMethod="GetInsurance" ServicePath="AJAX Pages/PatientService.asmx" UseContextKey="true" ContextKey="SZ_COMPANY_ID" OnClientItemSelected="GetInsuranceValue">
                                                                                </ajaxToolkit:AutoCompleteExtender>
                                                                                 <asp:TextBox ID="txtInsuranceCompany" runat="Server" autocomplete="off" Width="75%" OnTextChanged="txtInsuranceCompany_TextChanged" AutoPostBack="true"/>
                                                                                <extddl:ExtendedDropDownList ID="extddlInsuranceCompany" Width="96%" runat="server" Visible="false"
                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_INSURANCE_COMPANY"
                                                                                    Flag_Key_Value="INSURANCE_LIST" Selected_Text="--- Select ---" AutoPost_back="True"
                                                                                    OnextendDropDown_SelectedIndexChanged="extddlInsuranceCompany_extendDropDown_SelectedIndexChanged"
                                                                                    OldText="" StausText="False" />
                                                                                    <a href="#" id="A1" onclick="showAdjusterPanelAddress()" style="text-decoration: none;">
                                                                                    <img id="img1" src="Images/actionEdit.gif" style="border-style: none;" title="Add Insurance Company Address" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Ins. Address
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                                &nbsp;</td>
                                                                            <td colspan="4">
                                                                            <asp:ListBox Width="100%" ID="lstInsuranceCompanyAddress" AutoPostBack="true" runat="server"
                                                                                    OnSelectedIndexChanged="lstInsuranceCompanyAddress_SelectedIndexChanged"></asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Address</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtInsuranceAddress" runat="server" CssClass="text-box"
                                                                                    ReadOnly="True"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    City</div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsuranceCity" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    State</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsuranceState" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Zip</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtInsuranceZip" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsPhone" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Fax</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <asp:TextBox ID="txtInsFax" runat="server" CssClass="text-box" ReadOnly="True"></asp:TextBox></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Contact Person</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <asp:TextBox Width="80%" ID="txtInsContactPerson" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="text-box"></asp:TextBox></td>
                                                                                            <td class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    <asp:Label ID="lblPolicyNumber" CssClass="lbl" runat="server"> Policy #</asp:Label></div>
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="txtWCBNumber" runat="server" Visible="False"></asp:TextBox>
                                                                                                <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                            </td>
                                                                                            <td class="tablecellLabel">
                                                                                                 WCB #
                                                                                               </td>
                                                                                            <td>
                                                                                             <asp:TextBox ID="txtWCBNo" runat="server"></asp:TextBox>
                                                                                                <asp:TextBox Visible="False" Width="99%" ID="txtInsuranceStreet" runat="server" CssClass="text-box"
                                                                                                    ReadOnly="True"></asp:TextBox></td>
                                                                                      </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Phone<br />
                                                                                    Claim/File #
                                                                                </div>
                                                                            </td>
                                                                        </tr>                                                                        
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <div class="lbl">
                                                                                    Adjuster Information 
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                 <a href="#" id="hlnlShowAdjuster" onclick="showAdjusterPanel()" style="text-decoration: none;">
                                                                                    <img id="imgShowAdjuster" src="Images/actionEdit.gif" style="border-style: none;" title="Add Adjuster" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                            <td class="ContentLabel">
                                                                                <div class="lbl">
                                                                                    Name
                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <extddl:ExtendedDropDownList ID="extddlAdjuster" Width="100%" runat="server" Connection_Key="Connection_String"
                                                                                    Selected_Text="--- Select ---" Flag_Key_Value="GET_ADJUSTER_LIST" Procedure_Name="SP_MST_ADJUSTER"
                                                                                    AutoPost_back="True" OnextendDropDown_SelectedIndexChanged="extddlAdjuster_extendDropDown_SelectedIndexChanged"
                                                                                    OldText="" StausText="False" />
                                                                                    
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Phone</div>
                                                                            </td>
                                                                            <td rowspan="2" class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" rowspan="2">
                                                                                <div class="lbl">
                                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                        <tr>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtAdjusterPhone" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                            <td width="13%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Extension</div>
                                                                                            </td>
                                                                                            <td width="25%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtAdjusterExtension" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                            <td width="5%" class="tablecellLabel">
                                                                                                <div class="lbl">
                                                                                                    Fax</div>
                                                                                            </td>
                                                                                            <td width="32%">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox ID="txtfax" runat="server" ReadOnly="True"></asp:TextBox></span></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td colspan="3" style="height: 37px">
                                                                                                <span class="tablecellControl">
                                                                                                    <asp:TextBox Width="98%" ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox></span>
                                                                                                <div class="lbl">
                                                                                                </div>
                                                                                            </td>
                                                                                            <td class="tablecellLabel" style="height: 37px">
                                                                                                &nbsp;</td>
                                                                                            <td style="height: 37px">
                                                                                                &nbsp;</td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Email</div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel">
                                                                                <div class="lbl">
                                                                               <asp:Label ID="lblassociate" Text="Associate cases" runat = "server" class="lbl"></asp:Label>                                                                                </div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <span class="tablecellControl">
                                                                                    <asp:TextBox ID="txtAssociateCases" runat="server"></asp:TextBox>
                                                                                    <asp:Button ID="btnAssociate" runat="server" OnClick="btnAssociate_Click" Text="Associate Cases"
                                                                                        Width="105px" CssClass="Buttons" /></span>
                                                                                       <asp:Button ID="btnDAssociate" runat="server" OnClick="btnDAssociate_Click" Text="DeAssociate Cases"
                                                                                        Width="120px" CssClass="Buttons" />
                                                                                      <asp:CheckBox ID = "btassociate" runat ="server" Text="Do not Update ins data " />
                                                                                        </td>
                                                                        </tr>
                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlAccidentInformation" TabIndex="1">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Accident Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 280px;">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Accident Date
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox Width="70%" ID="txtATAccidentDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                    MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                                                <asp:ImageButton ID="imgbtnATAccidentDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <ajaxToolkit:CalendarExtender ID="calATAccidentDate" runat="server" TargetControlID="txtATAccidentDate"
                                                                                    PopupButtonID="imgbtnATAccidentDate" Enabled="True" />
                                                                            </td>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Plate Number
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATPlateNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Report Number
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATReportNumber" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Address</td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                City
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATCity" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                State</td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <extddl:ExtendedDropDownList ID="extddlATAccidentState" runat="server" Width="90%"
                                                                                    Connection_Key="Connection_String" Procedure_Name="SP_MST_STATE" Flag_Key_Value="GET_STATE_LIST"
                                                                                    Selected_Text="--- Select ---" OldText="" StausText="False"></extddl:ExtendedDropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Hospital name
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATHospitalName" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Hospital Address</td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox ID="txtATHospitalAddress" runat="server" CssClass="textboxCSS" MaxLength="50"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Date of admission
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                <asp:TextBox Width="70%" ID="txtATAdmissionDate" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                    MaxLength="10" CssClass="cinput"></asp:TextBox>&nbsp;
                                                                                <asp:ImageButton ID="imgbtnATAdmissionDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                                                <ajaxToolkit:CalendarExtender ID="calATAdmissionDate" runat="server" TargetControlID="txtATAdmissionDate"
                                                                                    PopupButtonID="imgbtnATAdmissionDate" Enabled="True" />
                                                                            </td>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 35%" class="ContentLabel">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Additional Patients
                                                                            </td>
                                                                            <td style="width: 35%" colspan="3">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td width="7%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td width="93%">
                                                                                            <asp:TextBox ID="txtATAdditionalPatients" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Describe injury
                                                                            </td>
                                                                            <td style="width: 35%" colspan="3">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td width="7%">
                                                                                            &nbsp;
                                                                                        </td>
                                                                                        <td width="93%">
                                                                                            <asp:TextBox ID="txtATDescribeInjury" runat="server" MaxLength="200" Width="99%"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="ContentLabel" style="width: 15%">
                                                                                Patient Type
                                                                            </td>
                                                                            <td colspan = "3" width="35%">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td width="7%">
                                                                                    &nbsp;
                                                                                    </td>
                                                                                    <td width="93%">
                                                                                        <asp:RadioButtonList ID="rdolstPatientType" runat="server" RepeatDirection="Horizontal" class = "lbl" >
                                                                                            <asp:ListItem Value = "0">Bicyclist</asp:ListItem>
                                                                                            <asp:ListItem Value = "1">Driver</asp:ListItem>
                                                                                            <asp:ListItem Value = "2">Passenger</asp:ListItem>
                                                                                            <asp:ListItem Value = "3">Pedestrian</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                        <asp:TextBox ID="txtPatientType" runat="server" Visible = "false" Width="2%"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlEmployerInformation" TabIndex="6"
                                            Visible="false">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Employer Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 280px;">
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlAdjusterInformation" TabIndex="4"
                                            Visible="false">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Adjuster Information
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div align="left" style="height: 280px;">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxToolkit:TabPanel>
                                        <ajaxToolkit:TabPanel runat="server" ID="tabpnlAcciInfo" TabIndex="5" Visible="false">
                                            <headertemplate>
                                                                <div style="width: 120px;" class="lbl">
                                                                    Accident
                                                                </div>
                                                            </headertemplate>
                                            <contenttemplate>
                                                                <div>
                                                                    <table>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    &nbsp;</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <span class="tablecellControl">
                                                                                    <extddl:ExtendedDropDownList ID="extddlProvider" Width="200px" runat="server" Connection_Key="Connection_String"
                                                                                        Procedure_Name="SP_MST_PROVIDER" Flag_Key_Value="PROVIDER_LIST" Selected_Text="--- Select ---"
                                                                                        Visible="False" OldText="" StausText="False" />
                                                                                    &nbsp; &nbsp;
                                                                                    <asp:CheckBox ID="chkAssociateCode" runat="server" Text="Associate Diagnosis Code"
                                                                                        Visible="False" />&nbsp; </span>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    &nbsp;</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                &nbsp;</div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox Width="70%" ID="txtDateOfInjury" runat="server" onkeypress="return clickButton1(event,'/')"
                                                                                                MaxLength="10" Visible="False"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtCarrierCaseNo" runat="server" Visible="False"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtJobTitle" runat="server" Visible="False"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                &nbsp;</div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtWorkActivites" runat="server" Visible="False"></asp:TextBox>
                                                                                                <asp:TextBox ID="txtPatientAge" runat="server" onkeypress="return clickButton1(event,'')"
                                                                                                    MaxLength="10" Visible="False"></asp:TextBox></span></td>
                                                                                        <td class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <span class="lbl">&nbsp;<asp:TextBox ID="txtAssociateDiagnosisCode" runat="server"
                                                                                                Visible="False" /></span></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div align="left" style="height: 280px;">
                                                                    <table width="100%" height="200" border="0" align="center" cellpadding="0" cellspacing="3">
                                                                        <!-- Start : Data Entry -->
                                                                        <tr>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="26" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Address</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4">
                                                                                <asp:TextBox Width="99%" ID="txtAccidentAddress" runat="server" CssClass="text-box"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="11" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    City</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" valign="top">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="25%" height="26">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtAccidentCity" runat="server" CssClass="text-box"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="13%" class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                State</div>
                                                                                        </td>
                                                                                        <td width="25%">
                                                                                            <span class="tablecellControl">
                                                                                                <asp:TextBox ID="txtAccidentState" runat="server" CssClass="text-box" Visible="false"></asp:TextBox>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td width="5%" class="tablecellLabel">
                                                                                            <div class="lbl">
                                                                                                &nbsp;</div>
                                                                                        </td>
                                                                                        <td width="32%">
                                                                                            <span class="tablecellControl">&nbsp;</span></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td height="11" class="tablecellLabel">
                                                                                <div class="lbl">
                                                                                    Patients from the car</div>
                                                                            </td>
                                                                            <td class="tablecellSpace">
                                                                            </td>
                                                                            <td colspan="4" valign="top">
                                                                                <asp:TextBox Height="100px" Width="98%" ID="txtListOfPatient" runat="server" TextMode="MultiLine"
                                                                                    MaxLength="250"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <!-- End : Data Entry -->
                                                                    </table>
                                                                </div>
                                                            </contenttemplate>
                                        </ajaxToolkit:TabPanel>
                                      </ajaxToolkit:TabContainer> </ajaxToolkit:TabContainer>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ContentLabel" colspan="6">
                                        <asp:TextBox ID="txtCompanyIDForNotes" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                        <%--<asp:TextBox ID="txtChartNo" runat="server" Width="10px" Visible="false"></asp:TextBox>--%>
                                        <asp:Button ID="btnPatientUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                            OnClick="btnPatientUpdate_Click" />
                                        <asp:Button ID="btnPatientClear" runat="server" Text="Clear" Width="80px" CssClass="Buttons" />
                                        <asp:TextBox ID="txtPatientID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!--  <tr>
                                    <td style="width: 100%; height: 24px;" class="SectionDevider">
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        Associate Diagnosis Code
                                    </td>
                                </tr>  -->
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="grdAssociatedDiagnosisCode" CssClass="GridTable" Width="100%" runat="server"
                                Visible="False" AutoGenerateColumns="False">
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="SZ_DIAGNOSIS_SET_ID" HeaderText="Set ID" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DOCTOR_ID" HeaderText="Doctor Code" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DOCTOR_NAME" HeaderText="Doctor Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_DIAGNOSIS_CODE" HeaderText="Diagnosis Code"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <a href="Bill_Sys_AssociateDignosisCode.aspx?caseID=<%=Session["PassedCaseID"].ToString() %>&SetId=<%# DataBinder.Eval(Container.DataItem, "SZ_DIAGNOSIS_SET_ID") %>"
                                                target="_Blank">Add services </a>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Add services
                                        </HeaderTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <a href="Bill_Sys_AssociateDignosisCode.aspx?caseID=<%=Session["PassedCaseID"].ToString() %>&SetId=<%# DataBinder.Eval(Container.DataItem, "SZ_DIAGNOSIS_SET_ID") %>"
                                                target="_Blank">Delete services </a>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Delete services
                                        </HeaderTemplate>
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
                        <td style="width: 100%; height: 2px;" class="SectionDevider">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div align="left" style="vertical-align: top;">
                                <div style="float: left; padding-right: 200px; text-indent: 10px;">
                                    Note Details</div>
                                <div style="vertical-align: top;">
                                    <extddl:ExtendedDropDownList ID="extddlFilter" runat="server" Width="200px" Connection_Key="Connection_String"
                                        Flag_Key_Value="LIST" Procedure_Name="SP_MST_NOTES_TYPE" Text="NTY0002" />
                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" Width="80px" CssClass="Buttons"
                                        OnClick="btnFilter_Click" />
                                    <asp:TextBox ID="txtAccidentID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtUserID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                    <asp:TextBox ID="txtNoteCode" runat="server" Visible="False" Width="10px"></asp:TextBox></div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" class="SectionDevider">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%" >
                            <asp:DataGrid ID="grdNotes" Width="100%" runat="server" CssClass="mGrid" AutoGenerateColumns="False">
                            <PagerStyle CssClass="pgr"/>
                            <AlternatingItemStyle BackColor="#EEEEEE"/>
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:BoundColumn DataField="I_NOTE_ID" HeaderText="NOTES ID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_NOTE_DESCRIPTION" HeaderText="Note Description"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DT_ADDED" HeaderText="Date Added" DataFormatString="{0:dd MMM yyyy} ">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SZ_NOTE_TYPE" HeaderText="Note Type"></asp:BoundColumn>
                                </Columns>
                                <HeaderStyle CssClass="GridViewHeader" BackColor="#B5DF82" Font-Bold="true"/>
                            </asp:DataGrid>
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
    <tr>
        <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
            visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
            border-left: silver 1px solid; border-bottom: silver 1px solid; left: 176px;
            top: -70px;" align="center">
            <div style="position: relative; text-align: right; background-color: #8babe4;">
                <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
            </div>
            <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
                visible="false"></iframe>
        </div>
    </tr>
    </table>
    <asp:UpdatePanel runat="server" ID="updpnlmemo" UpdateMode="Conditional">
        <contenttemplate>
            <asp:Panel ID="pnlMemo" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="IframeMemo" src="Bill_Sys_PopupMemo.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
 </contenttemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <contenttemplate>
            <asp:Panel ID="pnlShowNotes" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="Iframe2" src="Bill_Sys_PopupNotes.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
        
            <asp:Panel ID="pnlShowAtornyInfo" runat="server" Style="width:420px;height:220px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <iframe id="Iframe1" src="Bill_Sys_PopupAttorny.aspx" frameborder="0" height="220px" width="420px"
            visible="false">
            
            
            </iframe>
                   
         </asp:Panel>
         
          <asp:Panel ID="pnlSearchInsuranceCompany" runat="server" Style="width:220px;height:100px;background-color: white;border-color:SteelBlue;border-width:1px;border-style:solid;">
            <table width="100%">
                <tr>
                    <td width="30%"> <div class="lbl">Code</div></td>
                    <td width="70%"><asp:TextBox ID="txtSearchCode" runat="server" width="80%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td width="30%"> <div class="lbl">Name</div></td>
                    <td width="70%"><asp:TextBox ID="txtSearchName" runat="server" width="80%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSearchInsCompany" runat="server" Text="Search" CssClass="Buttons" OnClick="btnSearchInsCompany_Click"/>
                    </td>
                </tr>
            </table>       
         </asp:Panel>
        
        
        </contenttemplate>
    </asp:UpdatePanel>
   
    <%--Assign Supplies--%>
    <asp:Panel ID="pnlAssignSupplies" runat="server" Style="width: 420px; height: 220px;
        background-color: white; border-color: SteelBlue; border-width: 1px; border-style: solid;"
        Visible="false">
        <table width="100%">
            <tr>
                <td width="100%" align="right">
                    <asp:Button ID="btnAssignSupplies" runat="server" Text="Assign Supplies" CssClass="Buttons"
                        OnClick="btnAssignSupplies_Click" />
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:DataGrid ID="grdAssignSupplies" Width="100%" CssClass="GridTable" runat="Server"
                        AutoGenerateColumns="False">
                        <HeaderStyle CssClass="GridHeader" />
                        <ItemStyle CssClass="GridRow" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Assign">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAssignSupplies" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CHECK" HeaderText="CHECK" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="I_SUPPLIES_ID" HeaderText="Supplies ID" Visible="false">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SZ_SUPPLIES_NAME" HeaderText="Supplies Name" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div id="divAdjuster" style="visibility: hidden; width: 600px; left: 150px; top: 400px;
        vertical-align: bottom; position: absolute;">
        <asp:Panel ID="pnlAddAdjuster" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table2" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="LeftCenter" style="height: 100%">
                    </td>
                    <td class="Center" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divAdjuster').style.visibility='hidden';" style="cursor: pointer;"
                                                        title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="Label7" runat="server" Visible="false"></asp:Label>
                                                <div id="AdjusterErrorDiv" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label8" runat="server">Adjuster</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Adjuster Name:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupName" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Phone Number:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupPhone" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Extension:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupExtension" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                FAX:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupFax" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Email:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtAdjusterPopupEmail" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                            </td>
                                            <td style="width: 35%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <asp:Button ID="btnSaveAdjuster" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                    OnClick="btnSaveAdjuster_Click" />
                                                <asp:Button ID="btnClearAdjuster" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                    Visible="false" />
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
            </table>
        </asp:Panel>
    </div>
    <div id="divAddress" style="visibility: hidden; width: 600px; left: 150px; top: 550px;
        vertical-align: bottom; position: absolute;">
        <asp:Panel ID="pnlAddress" runat="server" BackColor="white" BorderColor="steelblue"
            Width="600px">
            <table id="Table1" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="LeftCenter" style="height: 100%">
                    </td>
                    <td class="Center" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                            <tr>
                                <td style="width: 100%" class="TDPart">
                                    <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                        <tr>
                                            <td width="100%" colspan="4">
                                                <div id="divAddressError" style="color: red; font-size: small" width="100%">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <div style="position: relative; text-align: right; background-color: #8babe4;">
                                                    <a onclick="document.getElementById('divAddress').style.visibility='hidden';" style="cursor: pointer;"
                                                        title="Close">X</a>
                                                </div>
                                                <asp:Label CssClass="message-text" ID="Label1" runat="server" Visible="false"></asp:Label>
                                                <div id="Div1" style="color: red" visible="true">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCDMsg" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="text-align: left; height: 25px; font-weight: bold;"
                                                colspan="4">
                                                <asp:Label ID="Label2" runat="server">Address Details</asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Address:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceAddressCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <%--<asp:RequiredFieldValidator ID="reqFieldtxtInsuranceAddressCD" ControlToValidate="txtInsuranceAddressCD" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInsuranceAddressCD"
                                                ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>--%>
                                            <td class="ContentLabel" style="width: 15%">
                                                Street:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceStreetCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                City:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceCityCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                State:</td>
                                            <td style="width: 35%">
                                                <cc1:ExtendedDropDownList ID="extddlInsuranceStateNew" runat="server" Width="80%"
                                                    Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                    Connection_Key="Connection_String"></cc1:ExtendedDropDownList>
                                                <cc1:ExtendedDropDownList ID="extddlInsuranceState" runat="server" Width="255px"
                                                    Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                    Connection_Key="Connection_String" Visible="false"></cc1:ExtendedDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" style="width: 15%">
                                                Zip:</td>
                                            <td style="width: 35%">
                                                <asp:TextBox ID="txtInsuranceZipCD" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
                                            <td class="ContentLabel" style="width: 15%">
                                                Default:
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="IDDefault" runat="server" />
                                            </td>
                                            <td style="width: 35%">
                                                <%--<asp:TextBox ID="txtCompanyID1" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                          --%>
                                                <asp:TextBox ID="txtInsuranceStateNew" runat="server" Visible="false"></asp:TextBox>
                                                <%--<cc1:ExtendedDropDownList ID="extddlInsuranceStateNew" runat="server" Width="80%"
                                                                            Selected_Text="--- Select ---" Flag_Key_Value="GET_STATE_LIST" Procedure_Name="SP_MST_STATE"
                                                                            Connection_Key="Connection_String"></cc1:ExtendedDropDownList>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%-- <td class="ContentLabel">
                                                Default:</td>--%>
                                            <%--<td>
                                                <asp:CheckBox ID="IDDefault" runat="server" />
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td class="ContentLabel" colspan="4">
                                                <%--<asp:Button ID="btnSaveAddress" OnClientClick="return checkAddressDetails()" runat="server"
                                                    Text="Add" Width="80px" CssClass="Buttons" OnClick="btnSaveAddress_Click" />
                                                <asp:Button ID="btnClearAddress" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                    Visible="false" />--%>
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
                <asp:TextBox ID="txtInsuranceCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                <%--<asp:TextBox id="txtInsuranceCompanyAddress" runat="server" Width="10px" Visible="False"></asp:TextBox>--%>
            </table>
        </asp:Panel>
    </div>
   <asp:HiddenField ID="hdninsurancecode" runat="server" />
   <div style="border-right: silver 1px solid; border-top: silver 1px solid; left: 119px;
                visibility: hidden; border-left: silver 1px solid; width: 700px; border-bottom: silver 1px solid;
                position: absolute; top: 682px; height: 280px; background-color: white" id="divfrmPatient" >
                <div style="position: relative; background-color: #B5DF82 ; text-align: right">
                    <a style="cursor: pointer" title="Close" onclick="ClosePatientFramePopup();">X</a>
                </div>
                <iframe id="frmpatient" src="" frameborder="0" width="700" height="370"></iframe>
            </div>
</asp:Content>
