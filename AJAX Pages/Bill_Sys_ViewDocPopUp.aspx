<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_ViewDocPopUp.aspx.cs" Inherits="AJAX_Pages_Bill_Sys_ViewDocPopUp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" id="framehead">
    <title>Patient View</title>
    
</head>
 
   <script language="javascript" type="text/javascript">
       function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdViewDoc.ClientID %>");	
            var str = 1;
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {	
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
		    {		
		        
		        
		        
		      
                    f.getElementsByTagName("input").item(i).checked=ival;
                

		                 				
			       
			    }	
			    
			    	
		    }
       }
       
        function OpenDocumentManager(Path)
    {    
       window.open(Path ,'AdditionalData', 'width=1200,height=800,left=30,top=30,scrollbars=1');
    }
    
    
    function confirm_delete()
    {
          var f= document.getElementById("<%=grdViewDoc.ClientID%>");
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {		
				  if(f.getElementsByTagName("input").item(i).name.indexOf('ChkDelete') !=-1)
		            {
		                if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			            {						
			                if(f.getElementsByTagName("input").item(i).checked != false)
			                {
			                    
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
		        
		 

                if(confirm("Are you sure want to Delete?")==true)
				{
				  
				   return true;
				}
				else
				{
					return false;
				}
    }
   </script>

<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" >
                                <tr>
                    <td style="width: 100%;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <contenttemplate>
                                        <table style=" width: 100%;" border="0">
                                            <tbody>
                                                <tr>
                 
                                                    <td align="left">
                                                        Search:<gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true"
                                                            CssClass="search-input">
                                                        </gridsearch:XGridSearchTextBox>
                                                    </td>
                                                    <td  align="right"
                                                        colspan="3">
                                                        Record Count:<%= this.grdViewDoc.RecordCount%>
                                                        | Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td align="left">
                                                      
                                                    </td>
                                                    <td  align="right"
                                                        colspan="3">
                                                       
                                                      <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                            </tbody>
                                        </table>
                                        <xgrid:XGridViewControl ID="grdViewDoc" runat="server"  Width="100%"
                                            CssClass="mGrid" AutoGenerateColumns="false" MouseOverColor="0, 153, 153" ExcelFileNamePrefix=""
                                            ShowExcelTableBorder="true" EnableRowClick="false" ContextMenuID="ContextMenu1"
                                            HeaderStyle-CssClass="GridViewHeader" 
                                            ExportToExcelColumnNames=""
                                            ExportToExcelFields=""
                                            AlternatingRowStyle-BackColor="#EEEEEE" AllowPaging="true" GridLines="None" XGridKey="Bill_Sys_View_Doc"
                                            PageRowCount="10" PagerStyle-CssClass="pgr" DataKeyNames="I_IMAGE_ID,SZ_CASE_ID,I_EVENT_ID,SZ_PROCEDURE_GROUP_ID,File_Path,File_Name"
                                            AllowSorting="true" >
                                            <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                            <Columns>
                                           
                                              
                                                <%--0--%>
                                                <asp:BoundField DataField="DT_UPLOAD_DATE" HeaderText="Upload Date" SortExpression="TU.DT_UPLOADED_DATE">
                                                    <headerstyle horizontalalign="Left"></headerstyle>
                                                    <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                <%--1--%>
                                                   <asp:TemplateField HeaderText="Docs">
                                                                            <itemtemplate>
                                                                                   <a id="lnkframePatient"  runat="server"  href="#" onclick='<%# "OpenDocumentManager(" + "\""+ Eval("File_Path") +"\");" %>' >'<%# DataBinder.Eval(Container, "DataItem.File_Name")%>'</a>
                                                                            </itemtemplate>
                                                                            <itemstyle horizontalalign="Left"></itemstyle>
                                                                        </asp:TemplateField>
                                                 <%--2--%>
                                                <asp:TemplateField HeaderText="">
                                                    <headertemplate>
                                                                              <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                                          </headertemplate>
                                                    <itemtemplate>
                                                                             <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </itemtemplate>
                                                </asp:TemplateField>  
                                                
                                                
                                                <asp:BoundField DataField="I_IMAGE_ID" ItemStyle-Width="85px" HeaderText="I_IMAGE_ID"  Visible="false">	
			                                        <itemstyle horizontalalign="Left"></itemstyle>
			                                    </asp:BoundField>
                                                <asp:BoundField DataField="SZ_CASE_ID"   Visible="false" ItemStyle-Width="105px" HeaderText="SZ_CASE_ID">	
	                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField> 
                                                 <asp:BoundField DataField="I_EVENT_ID"   Visible="false" ItemStyle-Width="105px" HeaderText="I_EVENT_ID">	
	                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField> 
                                                
                                                 <asp:BoundField DataField="SZ_PROCEDURE_GROUP_ID"   Visible="false" ItemStyle-Width="105px" HeaderText="SZ_PROCEDURE_GROUP_ID">	
	                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField> 
                                                   
                                                 <asp:BoundField DataField="DT_UPLOAD_DATE"   Visible="false" ItemStyle-Width="105px" HeaderText="DT_UPLOAD_DATE">	
	                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField>
                                                 <asp:BoundField DataField="File_Path"   Visible="false" ItemStyle-Width="105px" HeaderText="File_Path">	
	                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField> 
                                                 <asp:BoundField DataField="SZ_COMPANY_ID"   Visible="false" ItemStyle-Width="105px" HeaderText="File_Path">	
	                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField> 
                                                
                                                 <asp:BoundField DataField="File_Name"   Visible="false" ItemStyle-Width="105px" HeaderText="File_Name">	
	                                                <itemstyle horizontalalign="Left"></itemstyle>
                                                </asp:BoundField> 
                                                                                                                                
                                            </Columns>
                                        </xgrid:XGridViewControl>
                                        </contenttemplate>
                                        </asp:UpdatePanel>
                                    </td>
                </tr>
                <tr>
                <td>
                <asp:TextBox ID="txtCompnyID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtEventID" runat="server" Visible="false"></asp:TextBox>
               
                </td>
                </tr>
                </table>
    </form>
</body>
</html>