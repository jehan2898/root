<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ViewDocuments.aspx.cs"
    Inherits="Bill_Sys_ViewDocuments" %>

<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript" src="Registration/validation.js"></script>

    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">

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

    </script>

    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
              <tr>
                        <td align="center" >
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <contenttemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </contenttemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                <tr>
                    <td>
                        <div style="overflow: scroll; height: 300px; width: 100%;">
                            <asp:DataGrid ID="grdViewDocuments" Width="100%" CssClass="GridTable" runat="Server"
                                OnItemCommand="grdViewDocuments_ItemCommand" AutoGenerateColumns="False">
                                <HeaderStyle CssClass="GridHeader" />
                                <ItemStyle CssClass="GridRow" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkView" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="File Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LKBView" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Filename")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="FilePath" HeaderText="Filepath" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Filename" HeaderText="Filename" Visible="false"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlreport" runat="server" Width="100px">
                                                <asp:ListItem Value="0">Report</asp:ListItem>
                                                <asp:ListItem Value="1">Referral</asp:ListItem>
                                                <asp:ListItem Value="2">AOB</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Button ID="btnView" runat="server" CssClass="Buttons" Text="Received Document"
                            OnClick="btnView_Click" Visible="false" />
                           
                            
                    </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:DropDownList ID="ddlDoctor" runat="server" Width="200px" ></asp:DropDownList>&nbsp;
                 <asp:Button ID="btnDoctor" runat="server" Text="Assign Reading Doctor" CssClass="Buttons" Width="200px" OnClick="btnDoctor_Click"   />                                                
                </td>
                </tr>
                <tr>
                    <asp:TextBox ID="txtEventProcId" runat="server" Visible="false"></asp:TextBox><asp:TextBox ID="txtSpecility" runat="server" Visible="false"></asp:TextBox><asp:TextBox ID="txtCaseId" runat="server" Visible="false"></asp:TextBox></tr>
            </table>
        </div>
    </form>
</body>
</html>
