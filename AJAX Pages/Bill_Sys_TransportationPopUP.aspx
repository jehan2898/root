<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_Sys_TransportationPopUP.aspx.cs"
    Inherits="AJAX_Pages_Bill_Sys_TransportationPopUP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transportation Info</title>
    <link href="Css/mainmaster.css" rel="stylesheet" type="text/css" />
    <link href="Css/UI.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/mainmaster.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ch.css" type="text/css" />
    <link rel="stylesheet" href="CSS/intake-sheet-ff.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ie.css" type="text/css" />
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
    <link rel="stylesheet" href="CSS/main-ff.css" type="text/css" />

    <script type="text/javascript">
    function confirm_deletetransport()
		 {
		         
		        var f= document.getElementById('grdTransport');	
		        
		        var bfFlag = false;	
		        for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		        {	
		         	
				  if(f.getElementsByTagName("input").item(i).name.indexOf('chkDelete') !=-1)
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
        function val_CheckControls()
        {
 		if(document.getElementById('txtFromDate').value == '')
 	          {
                       alert('Please Select Date');
                       return false;
                 }
        if(document.getElementById("ctl00_ContentPlaceHolder1_extddlTransport").value=='NA' )
		  {
			alert('Please Select Transport Name');
			return false;
		  }
         if( document.getElementById("ctl00_ContentPlaceHolder1_ddlHours").value=0)

		  {

			alert('Please Select Time');
			return false;
		  }
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
         function SelectAll(ival)
       {
            var f= document.getElementById('grdTransport');	
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

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%">
            <tr>
                <td style="width: 100%;">
                    <UserMessage:MessageControl runat="server" ID="MessageControl1"></UserMessage:MessageControl>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                <b>Transport Name </b>
                            </td>
                            <td>
                                <extddl:ExtendedDropDownList ID="extddlTransport" runat="server" Width="110px" Connection_Key="Connection_String"
                                    Flag_Key_Value="GET_TRANSPORT_LIST" Procedure_Name="SP_MST_TRANSPOTATION" Selected_Text="---Select---">
                                </extddl:ExtendedDropDownList>
                            </td>
                            <td>
                                <b>Date </b>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" onkeypress="return CheckForInteger(event,'/')"
                                    MaxLength="10" Width="50%"></asp:TextBox>
                                <asp:ImageButton ID="imgbtnFromDate" runat="server" ImageUrl="~/Images/cal.gif" />
                                <ajaxToolkit:CalendarExtender ID="calExtFromDate" runat="server" TargetControlID="txtFromDate"
                                    PopupButtonID="imgbtnFromDate" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Time</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlHours" runat="server" Width="60px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlMinutes" runat="server" Width="60px">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlTime" runat="server" Width="60px">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right">
                                <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btdelete_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div style="overflow: auto; height: 10%; width: 100%;">
                                    <asp:DataGrid ID="grdTransport" runat="server" Width="100%" CssClass="GridTable"
                                        AutoGenerateColumns="false" DataKeyField="I_TRANS_ID">
                                        <FooterStyle />
                                        <SelectedItemStyle />
                                        <PagerStyle />
                                        <AlternatingItemStyle />
                                        <ItemStyle CssClass="GridRow" />
                                        <Columns>
                                            <asp:BoundColumn DataField="I_TRANS_ID" HeaderText="I_TRANS_ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_TARNSPOTATION_COMPANY_NAME" HeaderText="Transport Name">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_TRANS_ID" HeaderText="SZ_TRANS_ID"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_COMPANY_ID" HeaderText="SZ_COMPANY_ID" Visible="false">
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="SZ_CASE_ID" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DT_TRANS_DATE" HeaderText="Transport Date"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_TRANS_TIME" HeaderText="Time" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SZ_TRANS_TIME_TYPE" HeaderText="Time Type" Visible="false">
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"
                                                        ToolTip="Select All" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle CssClass="GridHeader1" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtCaseID" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
