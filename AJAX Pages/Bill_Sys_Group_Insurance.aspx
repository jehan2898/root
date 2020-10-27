<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_Group_Insurance.aspx.cs" enableEventValidation="false"
    Inherits="AJAX_Pages_Bill_Sys_Group_Insurance" Title="Green Your Bills - Insurance Group" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
        
        function MoveRight()
        {
            var sel = document.getElementById("<%=lstMenu.ClientID%>");
            var objSelDoc = document.getElementById("<%=lstMenusToRole.ClientID%>");
            var selNodeInlist = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>");
            var seltextNode=document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>");
            var listLength = sel.options.length;
            //selNodeInlist.value="";
           
            for (var i = listLength-1; i >= 0; i--) 
            {
                if (sel.options[i].selected) 
                {

                   var no = new Option();
                   no.value = sel.options[i].value;
                   no.text = sel.options[i].text;
                   
                   flag = 0;
                   for(j=0;j<objSelDoc.options.length;j++)
                   {
                        if(no.text == objSelDoc.options[j].text)
                        {
                            flag = 1;
                        }
                   }  
                   if(flag == 0)
                   {
                       objSelDoc[objSelDoc.options.length] = no; 
                       selNodeInlist.value = selNodeInlist.value + no.value+";" ;
                       seltextNode.value=seltextNode.value+no.text+";";
                   }
                   sel.remove(i);
                }
            }
        }
        
        function MoveLeft()
        {
            var check =  document.getElementById("<%=hfLinkSelected.ClientID%>").value;
            
            if(check=="0")
            {
                      
                var sel = document.getElementById("<%=lstMenusToRole.ClientID%>");
                
                var objSel = document.getElementById("<%=lstMenu.ClientID%>");
                var listLength = sel.options.length;
               
                var arrText = new Array();
                var arrValue = new Array();
                var count = 0;
                for (var i = 0; i < listLength; i++) 
                {
                    if(!sel.options[i].selected)
                    {
                      var no = new Option();
                      arrText[count] = sel.options[i].text;
                      arrValue[count] = sel.options[i].value;
                      count++;
                    }
                    else
                    {
                        //alert('test');
                        document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value.replace(sel.options[i].value +";", "");
                        document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value.replace(sel.options[i].text +";", "");
                        var no = new Option();
                        no.text = sel.options[i].text;  //lstmenurole=sel
                        no.value = sel.options[i].value;
                        objSel[objSel.options.length] = no;
                       // alert('test');
                    }
                }
                sel.length = 0;
                for(index = 0; index < arrText.length; index++)
                {
                    var no = new Option();
                    no.value = arrValue[index];
                    no.text = arrText[index];
                    sel[index] = no;
                }
            }
            else
             {
                 //alert('test1');
                document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value = '';
                document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value ='';
                 
                var sel = document.getElementById("<%=lstMenusToRole.ClientID%>");
                
                var objSel = document.getElementById("<%=lstMenu.ClientID%>");
                var listLength = sel.options.length;
               
                var arrText = new Array();
                var arrValue = new Array();
                var count = 0;
                for (var i = 0; i < listLength; i++) 
                {
                    if(!sel.options[i].selected)
                    {
                      var no = new Option();
                      arrText[count] = sel.options[i].text;
                      arrValue[count] = sel.options[i].value;
                      count++;
                    }
                    else
                    {
                        //alert('test');
                        document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value.replace(sel.options[i].value +";", "");
                        document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value.replace(sel.options[i].text +";", "");
                        var no = new Option();
                        no.text = sel.options[i].text;  //lstmenurole=sel
                        no.value = sel.options[i].value;
                        objSel[objSel.options.length] = no;
                       // alert('test');
                    }
                }
                sel.length = 0;
                for(index = 0; index < arrText.length; index++)
                {
                    var no = new Option();
                    no.value = arrValue[index];
                    no.text = arrText[index];
                    sel[index] = no;
                    //alert(arrText[index]);
                    document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeinListbox.ClientID%>").value + ';' +  arrValue[index]+';';
                    document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value = document.getElementById("<%=hfselectedNodeTextinListbox.ClientID%>").value  + ';' +arrText[index]+';';  
                }
                
             }
      }
        
        function Check()
        {
            //alert('test');
            if (document.getElementById("<%=txt_Group.ClientID %>").value == "")
            {
                alert('Insurance group name cannot be left blank');
                return false;
            }
            return true;
        }
        
    function  SearchList()
    {
        var l =  document.getElementById("<%= lstMenu.ClientID %>");
        var tb = document.getElementById("<%= TextBox1.ClientID %>");
        if(tb.value == "")
        {
            ClearSelection(l);
        }
        else{
            for (var i=0; i < l.options.length; i++)
            {
                var length = tb.value.length;
                var string = l.options[i].text.substring(0,length)
                if (string.toLowerCase().match(tb.value.toLowerCase()))
                {
                    l.options[i].selected = true;
                    return false;
                }
                else
                {
                    ClearSelection(l);
                }
            }

        }
    }

    function ClearSelection(lb)

    {
        lb.selectedIndex = -1;
    }
    
	function autoComplete (field, select, property, forcematch) 
	{
		var found = false;
		for (var i = 0; i < select.options.length; i++) 
		{
		
			if (select.options[i][property].toUpperCase().indexOf(field.value.toUpperCase()) == 0) 
			{
				found=true; 
				break;
			}
		}
		
		if (found) 
		{ 
			select.selectedIndex = i; 
		}
		else
		{
			select.selectedIndex = -1;
		}
		if (field.createTextRange) 
		{
			if (forcematch && !found) 
			{
				field.value=field.value.substring(0,field.value.length-1);
				return;
			}
			var cursorKeys ="8;46;37;38;39;40;33;34;35;36;45;"
			if (cursorKeys.indexOf(event.keyCode+";") == -1) 
			{
				var r1 = field.createTextRange();
				var oldValue = r1.text;
				var newValue = found ? select.options[i][property] : oldValue;
				if (newValue != field.value) 
				{
					field.value = newValue;
					var rNew = field.createTextRange();
					rNew.moveStart('character', oldValue.length) ;
					rNew.select();
				}
			}
		}
	} 


        
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table id="First" border="1" class="ContentTable" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr>
           <td style="width: 100%; " class="TDPart" align="left">
              <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 80%">
                 <tr>
                       <td style="width: 357px;font-size: 12px;font-family: arial;" align="left">
                            <b>INSURANCE COMPANY</b>
                       </td>
                       <td style="width: 57px">
                       </td> 
                       <td style="width: 386px">
                       </td>
                  </tr>
                  <tr>
                    <td style="width: 257px; height: 300px;" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'btnleft')">
                    <asp:TextBox  ID="TextBox1"  runat="server" Width="100%"  />
                    <asp:ListBox ID="lstMenu" runat="server" Width="100%" SelectionMode="Multiple" Height="300px" ></asp:ListBox>
                    </td>
                    <td class="ContentLabel" style="width: 57px; text-align:center; height: 300px;">
                          <input type="button" onclick="MoveRight();" value=">>" style="font-size: 12px;color: #000099;font-family: arial;background-color: #8babe4;border: 1px solid #000099;	padding-top:1px;padding-bottom:1px;padding-left:5px;padding-right:5px;" id="btnleft"/>
                          <input style="border-right: #000099 1px solid; padding-right: 5px; border-top: #000099 1px solid;
                            padding-left: 5px; font-size: 12px; padding-bottom: 1px; border-left: #000099 1px solid;
                            color: #000099; padding-top: 1px; border-bottom: #000099 1px solid; font-family: arial;
                            background-color: #8babe4" id="Button1" class="Buttons" title="Move Left"
                            onclick="MoveLeft();" type="button" value="<<"  />
                        <asp:Button ID="btnMoveLeft" runat="server" Text=">>" visible="false"/><%--OnClick="btnMoveLeft_Click"--%> 
                        <asp:Button ID="btnMoveRight" runat="server"  Text="<<" visible="false"/><%--OnClick="btnMoveRight_Click"--%>
                        <%--<input type="button" onclick="javascript:move(this.form.ctl00_ContentPlaceHolder1_lstMenu,this.form.ctl00_ContentPlaceHolder1_lstMenusToRole);" value=">>" />
                           <input type="button" onclick="javascript:move(this.form.ctl00_ContentPlaceHolder1_lstMenusToRole,this.form.ctl00_ContentPlaceHolder1_lstMenu);" value="<<" />--%>
                    </td>
                    <td style="width:286px; height: 300px;">
                         Insurance Group Name :&nbsp;<asp:TextBox ID="txt_Group" runat="server" Width="60%"></asp:TextBox>
                        <asp:ListBox ID="lstMenusToRole" runat="server" Width="100%" EnableViewState="true" SelectionMode="Multiple" Height="300px"  onkeypress="javascript:return WebForm_FireDefaultButton(event, 'Button1')">
                        </asp:ListBox>
                    </td>
                        <asp:HiddenField runat="server" ID="hfselectedNodeinListbox"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hfselectedNodeTextinListbox"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hfLinkSelected" Value="0" />
                </tr>
                <tr>
                    <td align="right">
                        <asp:TextBox ID="txtchkBalance" runat="server" Visible="false"></asp:TextBox>
                        <asp:Label CssClass="message-text" id="lblMsg" runat="server"  Visible="false"></asp:Label>
                    </td>
                    <td style="width: 57px">
                    </td> 
                    <td style="width: 386px" align="center">
                        <asp:Button ID="btn_ADD" runat="server" Text="ADD" CssClass="Buttons" OnClick="btn_ADD_Click" OnClientClick="return Check()"/>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="Buttons" OnClick="btnClear_Click" /> &nbsp;
                    </td>
                 </tr>
                 <tr>
                    <td colspan="3" style="width: 100%">
                        <asp:GridView ID="grdInsuranceGroup" runat="server"  CssClass="mGrid"
                            HeaderStyle-CssClass="GridViewHeader" GridLines="None" AlternatingRowStyle-BackColor="#EEEEEE"
                            PagerStyle-CssClass="pgr" EnableViewState="true" AllowSorting="true" AutoGenerateColumns="false" OnRowCommand ="grdInsuranceGroup_OnRowCommand" DataKeyNames="Insurance Id,sz_Group_Name">
                            <Columns>
                                <asp:BoundField DataField="sz_Group_Id" HeaderText="Group_ID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"></asp:BoundField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderText = "Group Name" HeaderStyle-HorizontalAlign="left" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lb_Group_Name" Text ='<%# DataBinder.Eval(Container,"DataItem.sz_Group_Name")%>' runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'/>
                                    </ItemTemplate>
                               </asp:TemplateField>  
                               <%-- <asp:BoundField DataField="Group Name" HeaderText="Group Name" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"></asp:BoundField>--%>
                                <asp:BoundField DataField="Insurance Id" HeaderText="Insurance_ID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px"></asp:BoundField>
                                <asp:BoundField DataField="Insurance Name" HeaderText="Insurance Companies" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                 </tr>
                 <%--<tr>
                    <td valign="top" align="left" width="350px">
                        <table style="width: 350px;">
                            <tr style="width: 350px;">
                                <td style="width: 400px;" align="right">
                                    &nbsp;
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                 </tr> --%>
             </table>
           </td>
           
        </tr>
        
    </table>
</asp:content>
