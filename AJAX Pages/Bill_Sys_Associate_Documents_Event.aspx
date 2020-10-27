<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_Associate_Documents_Event.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_Associate_Documents_Event" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
    <link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />

    <script type="text/javascript">
    
     function CloseUploadFilePopup()
       {
            document.getElementById('pnlUploadFile').style.height='0px';
            document.getElementById('pnlUploadFile').style.visibility = 'hidden';  
          //  document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value='';      
       }
    
    function showUploadFilePopup()
       {
       
           var flag= false;
           var f = document.getElementById('ddlreport');
           var n=document.getElementById('table_row_specialty_drpdwn');
           if(n!=null)
           {
               if(document.getElementById('table_row_specialty_drpdwn').visible=true)
               {
                    var m = document.getElementById('extddlSpecialty');
                    if(f.value=='7' || m.value=='NA')
                    {           
                       flag= false;
                    }
                    else
                    {
                       flag=true;
                    }
                    if(flag==true)
                    { 
                        document.getElementById('pnlUploadFile').style.height='100px';
                        document.getElementById('pnlUploadFile').style.visibility = 'visible';
                        document.getElementById('pnlUploadFile').style.position = "absolute";
                        document.getElementById('pnlUploadFile').style.top = '200px';
                        document.getElementById('pnlUploadFile').style.left ='350px';
                        document.getElementById('pnlUploadFile').style.zIndex= '0';
                        return false;
                    }
                    else
                    {
                        alert(" Please Select Speciality and Document Type..");
                    } 
               }
           }
           else
           {
               if(f.value=='7')
                {           
                   flag= false;
                  }
                  else
                  {
                   flag=true;
                  }
                    if(flag==true)
                    { 
                        document.getElementById('pnlUploadFile').style.height='100px';
                        document.getElementById('pnlUploadFile').style.visibility = 'visible';
                        document.getElementById('pnlUploadFile').style.position = "absolute";
	                    document.getElementById('pnlUploadFile').style.top = '200px';
	                    document.getElementById('pnlUploadFile').style.left ='350px';
	                     document.getElementById('pnlUploadFile').style.zIndex= '0';
	                //    document.getElementById('_ctl0_ContentPlaceHolder1_txtGroupDateofService').value=''; 
	                    //document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.height='0px';
                       // document.getElementById('_ctl0_ContentPlaceHolder1_pnlShowNotes').style.visibility = 'hidden';  
                    //    document.getElementById('_ctl0_ContentPlaceHolder1_txtDateofService').value='';   
                      //  MA.length = 0;
                      return false;
                    }
                    
                    else
                    {
                        alert(" Please Select Document Type ..");
                    } 
                    }  
            }
       

  function confirm_update_bill_status()
         {     
               
              
         
                              
                var f= document.getElementById('grdViewDocuments');
		        var bfFlag = false;	
		        var cnt=0;
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkView') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {		
			            				
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {  bfFlag = true;   
			                    cnt=cnt+1;
    		                    
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

         
         function SelectAll(ival)
       {
            var f= document.getElementById('grdViewDocuments');	
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
       
        function SelectAlldelete(ival)
       {
            var f= document.getElementById('grdDelete');	
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
       
        function confirm_delete_document()
       {
            var f= document.getElementById("grdDelete");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        if(f.getElementsByTagName("input").item(i).checked != false)
			        {
			            if (confirm("Are you sure to continue for delete?"))
			            return true;
			            return false;
			        }
			    }			
		    }
		    alert('Please select docuemnt');
		    return false;
       }
    



    </script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="4">
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <UserMessage:MessageControl runat="server" ID="usrMessage" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80%" colspan="3" align="center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table border="0" cellpadding="1" cellspacing="1" style="width: 80%; height: 100%">
                                        <tr id="table_row_specialty_drpdwn" runat="server" visible="false">
                                            <td>
                                                <extddl:ExtendedDropDownList ID="extddlSpecialty" runat="server" Connection_Key="Connection_String"
                                                    Flag_Key_Value="GET_SPECIALTY" AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="extddlSpecialty_SelectedIndexChanged"
                                                    Procedure_Name="SP_MST_SPECIALTY_LHR" Selected_Text="---Select---" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:UpdatePanel ID="upDelete" runat="server">
                                                    <ContentTemplate>
                                                        <ajaxToolkit:ModalPopupExtender ID="mpDelete" runat="server" TargetControlID="lnkbtndelete"
                                                            DropShadow="false" PopupControlID="pnlDelete" BehaviorID="modal1" PopupDragHandleControlID="divDelete">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <asp:Panel ID="pnlDelete" runat="server" Style="display: none; width: 450px; height: 330px;
                                                            background-color: white; border: 1px solid #B5DF82;">
                                                            <div id="divDelete" runat="server">
                                                                <table width="100%" border="0" cellpadding="4" cellspacing="4">
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div style="left: 0px; width: 450px; position: absolute; top: 0px; height: 21px;
                                                                                background-color: #B5DF82; text-align: left" id="Div2">
                                                                                <b>Delete File</b>
                                                                                <div style="position: absolute; top: 0px; right: 0px; height: 21px; background-color: #B5DF82;
                                                                                    border: 1px solid #B5DF82;">
                                                                                    <asp:Button ID="btnbillnotesclose" runat="server" Height="19px" Width="50px" class="GridHeader1"
                                                                                        Text="X" OnClientClick="$find('modal1').hide(); return false;"></asp:Button>
                                                                                </div>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            &nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                <ContentTemplate>
                                                                                    <UserMessage:MessageControl runat="server" ID="usrMessage1" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <div style="overflow: scroll; height: 150px; width: 100%;">
                                                                                <asp:DataGrid ID="grdDelete" Width="90%" runat="Server" AutoGenerateColumns="False"
                                                                                    CssClass="GridTable" OnItemCommand="grdDelete_ItemCommand">
                                                                                    <HeaderStyle CssClass="GridHeader1" />
                                                                                    <ItemStyle CssClass="GridViewHeader" />
                                                                                    <Columns>
                                                                                        <asp:TemplateColumn>
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAlldelete(this.checked);"
                                                                                                    ToolTip="Select All" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkView" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:TemplateColumn HeaderText="File Name" HeaderStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="LKBView" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Filename")%>'></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:BoundColumn DataField="FilePath" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="Filename" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                                                                         <asp:BoundColumn DataField="PhysicalBasePath" HeaderText="PhysicalPath" Visible="false"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="DomainName" HeaderText="DomainName" Visible="false"></asp:BoundColumn>
                                                                                        <%--<asp:TemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                                                                                        <asp:ListItem Value="7" Selected="true">--Select--</asp:ListItem>
                                                                                        <asp:ListItem Value="0">Report</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Referral</asp:ListItem>
                                                                                        <asp:ListItem Value="2">AOB</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Comp Authorization</asp:ListItem>
                                                                                        <asp:ListItem Value="4">HIPPA Consent</asp:ListItem>
                                                                                        <asp:ListItem Value="5">Lien Form</asp:ListItem>
                                                                                        <asp:ListItem Value="6">Misc</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>--%>
                                                                                    </Columns>
                                                                                </asp:DataGrid>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click"></asp:LinkButton>
                                                        <div style="display: none">
                                                            <asp:LinkButton ID="lnkbtndelete" runat="server" Text="Delete" Font-Names="Verdana">
                                                            </asp:LinkButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <div>
                                                    <asp:DataGrid ID="grdViewDocuments" Width="80%" runat="Server" OnItemCommand="grdViewDocuments_ItemCommand"
                                                        AutoGenerateColumns="False" CssClass="GridTable">
                                                        <HeaderStyle CssClass="GridHeader1" />
                                                        <ItemStyle CssClass="GridViewHeader" />
                                                        <Columns>
                                                            <asp:TemplateColumn>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                                        ToolTip="Select All" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkView" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="File Name" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LKBView" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Filename")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn DataField="FilePath" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Filename" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="PhysicalBasePath" HeaderText="PhysicalBasePath" Visible="false"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="DomainName" HeaderText="DomainName" Visible="false"></asp:BoundColumn>
                                                            <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                                                                        <asp:ListItem Value="7" Selected="true">--Select--</asp:ListItem>
                                                                        <asp:ListItem Value="0">Report</asp:ListItem>
                                                                        <asp:ListItem Value="1">Referral</asp:ListItem>
                                                                        <asp:ListItem Value="2">AOB</asp:ListItem>
                                                                        <asp:ListItem Value="3">Comp Authorization</asp:ListItem>
                                                                        <asp:ListItem Value="4">HIPPA Consent</asp:ListItem>
                                                                        <asp:ListItem Value="5">Lien Form</asp:ListItem>
                                                                        <asp:ListItem Value="6">Misc</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnView" runat="server" Text="Received Document" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:UpdateProgress ID="UpdateProgress12" runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <asp:Image ID="img12" runat="server" Style="position: absolute; z-index: 1; left: 50%;
                                            top: 50%" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
                                            Height="60px" Width="60px"></asp:Image>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:Button ID="btnopen" runat="server" Text="Open" Width="80px" OnClick="btnopen_Click" />
                                <asp:Button ID="btnUPdate" runat="server" Text="Save" Width="80px" OnClick="btnUPdate_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                            <asp:ListItem Value="7" Selected="true">--Select--</asp:ListItem>
                            <asp:ListItem Value="0">Report</asp:ListItem>
                            <asp:ListItem Value="1">Referral</asp:ListItem>
                            <asp:ListItem Value="2">AOB</asp:ListItem>
                            <asp:ListItem Value="3">Comp Authorization</asp:ListItem>
                            <asp:ListItem Value="4">HIPPA Consent</asp:ListItem>
                            <asp:ListItem Value="5">Lien Form</asp:ListItem>
                            <asp:ListItem Value="6">Misc</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnupload" runat="server" Text="Upload" Width="80px" />
                    </td>
                </tr>
            </table>
            <%--<table width="100%">
            </table>--%>
            <asp:TextBox ID="txtCaseID" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="txtEventProcId" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtSpecility" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtCompanyID" runat="server" Visible="false" Width="10px"></asp:TextBox>
            <asp:TextBox ID="txtdesc" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtcode" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtprocid" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txtProcDesc" runat="server" Visible="false"></asp:TextBox>
        </div>
        <asp:Panel ID="pnlUploadFile" runat="server" Style="width: 450px; height: 0px; background-color: white;
            border-color: ThreeDFace; border-width: 1px; border-style: solid; visibility: hidden;">
            <div style="position: relative; text-align: right; background-color: #B5DF82;">
                <a onclick="CloseUploadFilePopup();" style="cursor: pointer;" title="Close">X</a>
            </div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
                <tr>
                    <td style="width: 98%;" valign="top">
                        <table border="0" class="ContentTable" style="width: 100%">
                            <tr>
                                <td>
                                    Upload File :
                                </td>
                                <td>
                                    <asp:FileUpload ID="fuUploadReport" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdateProgress ID="UpdateProgress123" runat="server" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <asp:Image ID="img123" runat="server" Style="position: absolute; z-index: 1; left: 50%;
                                                top: 50%" ImageUrl="~/Ajax Pages/Images/loading_transp.gif" AlternateText="Loading....."
                                                Height="60px" Width="60px"></asp:Image>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:Button ID="btnUploadFile" runat="server" Text="Upload File" OnClick="btnUploadFile_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
