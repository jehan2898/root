<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_EORPopup.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_EORPopup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, 
PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>



<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />

     <script src="../js/scan/jquery.min.js" type="text/javascript"></script>
    <script src="../js/scan/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/scan/Scan.js" type="text/javascript"></script>
    <script src="../js/scan/function.js" type="text/javascript"></script>
    <script src="../js/scan/Common.js" type="text/javascript"></script>
    <link href="../Css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('.scanlbl').click(function () {
                debugger;
                var data = $(this).attr('data-val');
                var dataArray = data.split(',');
                var caseid = $('[id$=hdnCaseId]').val();
                var billNo = $('[id$=hdnBillNo]').val();
                var TypeID = dataArray[0];
                var specialtyID = dataArray[1];             
                var I_TYPE_ID = dataArray[2];
                 if (I_TYPE_ID == 'vr') {
                    scanBillVerification(caseid, specialtyID, TypeID, billNo,'5');
                 }
                 else if (I_TYPE_ID == 'EOR') {
                     scanBillEOR(caseid, specialtyID, TypeID, billNo, '5');
                 }
            });
        });
    </script>

    <script type="text/javascript">
      function Clear()
       {
        document.getElementById('txtSaveDate').value='';
        document.getElementById('txtSaveDescription').value='';
       
       }
        function checkedDate()
        {
        
            var year1="";
            year1=document.getElementById('txtSaveDate').value; 
           if((year1.charAt(0)!='' && year1.charAt(1)!='' && year1.charAt(2)=='/' && year1.charAt(3)!='' && year1.charAt(4)!='' && year1.charAt(5) == '/' && year1.charAt(6)!='' && year1.charAt(7)!='' && year1.charAt(8)!='' && year1.charAt(9)!='' && year1.charAt(6)!='0'))
            {
                 return true;   
            }
             else
            {
                alert("Please select EOR date.");
                return false;
            }
        }
        
        function SelectAll(ival)
       {
            var f= document.getElementById('grdVerificationReq');	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		    {		
		        
		        
		        
		        if(f.getElementsByTagName("input").item(i).disabled==false)
		        {
                    f.getElementsByTagName("input").item(i).checked=ival;
                }
			}	
			    
			    	
		    }
       }
       
        function Clear()
       {
        document.getElementById('txtSaveDate').value='';
        document.getElementById('txtSaveDescription').value='';
       }
       
        function confirm_bill_delete()
		 {
		        
		       var f= document.getElementById("grdVerificationReq");
		        var bfFlag = false;
		        var index = document.getElementById("hfindex").value;	
		        //alert(index);
		       
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				    
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') !=-1)
		            {
		               
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
			                   if (i == index)
			                   {
			                        if (confirm("Bille status has been changed POM received? do you want to allow changed bill status..")) {
			                            document.getElementById("hfconfirm").value = 'yes';
			                            
			                        }
			                        else {
			                            document.getElementById("hfconfirm").value = 'no';
			                                
			                        }
			                   }
			                   else 
			                   {
			                        document.getElementById("hfconfirm").value = 'delete';
			                   }
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
		        else {
		        if (confirm("Are you sure want to Delete?")){
		            return true;
		            }
		            else{
		                return false;
		            }
		        }
		}
		
		function showUploadFilePopup()
       {
      
            document.getElementById("pnlUploadFile").style.height='100px';
            document.getElementById("pnlUploadFile").style.visibility = 'visible';
            document.getElementById("pnlUploadFile").style.position = "absolute";
	        document.getElementById("pnlUploadFile").style.top = '100px';
	        document.getElementById("pnlUploadFile").style.left ='200px';
	        document.getElementById("pnlUploadFile").style.zIndex= '0';
       }
       function CloseUploadFilePopup()
       {
            document.getElementById("pnlUploadFile").style.height='0px';
            document.getElementById("pnlUploadFile").style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 95%; background-color: White; margin-left: 23px; border: 2px;"
            border="0">
            <tr>
                <td style="vertical-align: top; width: 100%;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%; border: 0px solid; margin-top: 20px; padding-left: 0px;"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <UserMessage:MessageControl runat="server" ID="usrMessage"></UserMessage:MessageControl>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td class="lbl" valign="top" style="width: 19%">
                                                    Bill Number
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtViewBillNumber" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td class="lbl" valign="top">
                                                    Visit Date
                                                </td>
                                                <td valign="top">
                                                    <asp:TextBox ID="txtvisitdatedenial" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                                        BorderStyle="None" ForeColor="Black" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 19%">
                                                    <asp:Label ID="lblSave" runat="server" Text="Date" CssClass="lbl" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 81%">
                                                    <asp:Label ID="lblSaveDate" runat="server" Text="" CssClass="lbl"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td class="lbl" style="width: 19%">
                                                    <asp:Label ID="lblSaveDatevalue" runat="server" Text="EOR Date" CssClass="lbl"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 81%">
                                                    <asp:TextBox ID="txtSaveDate" runat="server" Width="150px" MaxLength="10" onkeypress="return CheckForInteger(event,'/')"></asp:TextBox>
                                                    <asp:ImageButton ID="imgSavebtnToDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                                    <asp:Label ID="lblValidator1" runat="server" Font-Bold="False" ForeColor="Red" CssClass="lbl"></asp:Label>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSaveDate"
                                                        PopupButtonID="imgSavebtnToDate" />
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                        MaskType="Date" TargetControlID="txtSaveDate" PromptCharacter="_" AutoComplete="true">
                                                    </ajaxToolkit:MaskedEditExtender>
                                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedEditExtender4"
                                                        ControlToValidate="txtSaveDate" EmptyValueMessage="Date is required" InvalidValueMessage="Date is invalid"
                                                        IsValidEmpty="True" TooltipMessage="Input a Date" Visible="true"></ajaxToolkit:MaskedEditValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 19%">
                                                    <asp:Label ID="Description" runat="server" Text="Description" Font-Bold="true" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td style="width: 81%">
                                                    <asp:TextBox ID="txtSaveDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSavesent" runat="server" Text="Save" OnClick="btnSaveDesc_Click" />
                                        <input type="button" id="btnClear" style="width: 60px" value="Clear" onclick="Clear();" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtbillnumber" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtcaseno" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtpatientname" runat="server" Visible="false"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hfindex" Value="no" />
                            <asp:HiddenField runat="server" ID="hfverificationId" />
                            <asp:HiddenField runat="server" ID="hfconfirm" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UP_grdPatientSearch" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td align="left" class="txt2" style="width: 100%">
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UP_grdPatientSearch"
                                                        DisplayAfter="10" DynamicLayout="true">
                                                        <ProgressTemplate>
                                                            <div id="Div1" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img2" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Processing, Please wait...</div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                        DisplayAfter="10" DynamicLayout="true">
                                                        <ProgressTemplate>
                                                            <div id="DivStatus2" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                runat="Server">
                                                                <asp:Image ID="img3" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                    Height="25px" Width="24px"></asp:Image>
                                                                Processing, Please wait...</div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%; border: 0px solid; margin-top: 20px; padding-left: 0px;"
                                            cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="right" valign="top" colspan="2">
                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div style="overflow: scroll;">
                                                        <table>
                                                            <asp:DataGrid ID="grdVerificationReq" runat="server" AutoGenerateColumns="false"
                                                                Width="100%" OnItemCommand="grdVerificationReq_ItemCommand" CssClass="GridTable">
                                                                <ItemStyle CssClass="GridRow" />
                                                                <HeaderStyle CssClass="GridHeader1" />
                                                                <Columns>
                                                                    <%-- 0--%>
                                                                    <asp:ButtonColumn CommandName="Select" Text="Select" Visible="false">
                                                                        <ItemStyle CssClass="grid-item-left" />
                                                                    </asp:ButtonColumn>
                                                                    <%-- 1--%>
                                                                    <asp:BoundColumn DataField="TYPE" HeaderText="Type"></asp:BoundColumn>
                                                                    <%-- 2--%>
                                                                    <asp:BoundColumn DataField="NOTES" HeaderText="Notes"></asp:BoundColumn>
                                                                    <%-- 3--%>
                                                                    <asp:BoundColumn DataField="DATE" HeaderText="Date"></asp:BoundColumn>
                                                                    <%-- 4--%>
                                                                    <asp:BoundColumn DataField="USER" HeaderText="User"></asp:BoundColumn>
                                                                    <%-- 5--%>
                                                                    <asp:BoundColumn DataField="TYPEID" HeaderText="TYPEID" Visible="false"></asp:BoundColumn>
                                                                    <%-- 6--%>
                                                                    <asp:TemplateColumn HeaderText="EOR">
                                                                        <ItemTemplate>
                                                                            <%--<asp:LinkButton ID="lnkscan" runat="server" CausesValidation="false" CommandName="Scan"
                                                                                Text="Scan" OnClick="lnkscan_Click"></asp:LinkButton>/
                                                                            <asp:LinkButton ID="lnkuplaod" runat="server" CausesValidation="false" CommandName="Upload"
                                                                                Text="Upload" OnClick="lnkuplaod_Click"></asp:LinkButton>--%>
                                                                            <a id="caseDetailScan" href="#" runat="server" data-val='<%# Eval("TYPEID")+","+ Eval("SZ_SPECIALTY_ID")+","+ Eval("I_TYPE_ID") %>'
                                                    title="Scan/Upload" class="lbl scanlbl">Scan/Upload</a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <%--7--%>
                                                                    <asp:TemplateColumn>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                                ToolTip="Select All" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkDelete" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <%-- 8--%>
                                                                    <asp:BoundColumn DataField="I_TYPE_ID" Visible="false"></asp:BoundColumn>
                                                                </Columns>
                                                            </asp:DataGrid>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSavesent" />
                                        <%--<asp:AsyncPostBackTrigger ControlID="btnUploadFile" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: white;
                                    border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
                                    <div style="position: relative; text-align: right; background-color: #B5DF82">
                                        <a onclick="CloseUploadFilePopup();" style="cursor: pointer;" title="Close">X</a>
                                    </div>
                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                                        <tr>
                                            <td style="width: 98%;" valign="top">
                                                <table border="0" class="ContentTable" style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            Upload Report :
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="fuUploadReport" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Button ID="btnUploadFile" runat="server" Text="Upload Report" OnClick="btnUploadFile1_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnCaseId" runat="server" />
                        <asp:HiddenField ID="hdnBillNo" runat="server" />
    </form>
    <div id="dialog" style="overflow:hidden";>
    <iframe id="scanIframe" src="" frameborder="0" scrolling="no"></iframe>
</div>
</body>
</html>
