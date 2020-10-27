<%@ Page Title="" Language="C#" MasterPageFile="~/attorney.master" AutoEventWireup="true" CodeFile="atnotes.aspx.cs" Inherits="atnotes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="MenuControl" Namespace="MenuControl" TagPrefix="cc2" %>
<%@ Register Src="~/UserControl/CheckPageAutharization.ascx" TagName="CheckPageAutharization" TagPrefix="CPA" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XControl" TagPrefix="XCon" Assembly="XControl" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Src="~/UserControl/Bill_Sys_Case.ascx" TagName="Bill_Sys_Case" TagPrefix="CI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js" > </script>
    
  <script type="text/javascript">
   var dateArray = new Array();
        function ConfirmDelete()
        {
            
             var msg = "Do you want to proceed?";
             var result = confirm(msg);
             if(result==true)
             {
                return true;
             }
             else
             {
                return false;
             }
        }
        
    function SelectAll(ival)
       {
            var f= document.getElementById("<%= grdNotes.ClientID %>");	
		    for(var i=0; i<f.getElementsByTagName("input").length ;i++ )
		    {		
		        if(f.getElementsByTagName("input").item(i).type == "checkbox" )		
			    {						
			        f.getElementsByTagName("input").item(i).checked=ival;
			    }			
		    }
       }
       
       function chkReminderPopup_onclick(chk) 
    {
    //alert(chk);
       //if (dateArray.length > 0)
       //{
       if(chk.checked)
       {
           // alert( document.getElementById("divid"));
            document.getElementById("divid").style.position = "absolute";

            document.getElementById("divid").style.left= "350px";

            document.getElementById("divid").style.top= "80px";
            
            document.getElementById("divid").style.visibility="visible";
            document.getElementById('divid').style.zIndex= '1'; 
            document.getElementById("divid").style.width= "730px";
            document.getElementById("divid").style.height= "680px";
            document.getElementById("frameeditexpanse").style.width= "730px";
            document.getElementById("frameeditexpanse").style.height= "680px";
            document.getElementById("frameeditexpanse").src="Bill_Sys_Notes_Reminder.aspx";
            chk.checked = false;
        }
        //}
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
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="50000">
    </asp:ScriptManager>
    
    <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td style="padding-right: 3px; padding-left: 3px; padding-bottom: 3px; width: 100%;
                padding-top: 3px; height: 100%; vertical-align: top;">
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
                                        <asp:DataGrid ID="grdPatientDeskList" Width="100%" CssClass="GridTable" runat="Server"
                                            AutoGenerateColumns="False">
                                            <HeaderStyle CssClass="GridHeader" />
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case #" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Name" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company" HeaderStyle-HorizontalAlign="Left"
                                                    ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ACCIDENT" HeaderText="Accident Date" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="left" DataFormatString="{0:MM/dd/yyyy}"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <a href="#" onclick="return openTypePage('a')">
                                                            <img src="Images/actionEdit.gif" style="border: none;" />
                                                        </a>
                                                    </ItemTemplate>
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
                                    <td style="width: 100%" class="TDPart" style="vertical-align:top;">
                                        &nbsp;
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tbody>
                                                <tr>
                                                    <td class="ContentLabel" style="text-align: center;" colspan="4">
                                                        <asp:Label ID="lblMsg" runat="server" Visible="false" CssClass="message-text"></asp:Label>
                                                        <div id="Div1" style="color: red" visible="true">
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" >
                                                        <div style="color: red" id="ErrorDiv" visible="true">
                                                        </div>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td style="vertical-align: top" class="tablecellLabel">
                                                        <div class="lbl" style="text-align:center;">
                                                            Note Description:</div>
                                                    </td>
                                                    <td class="tablecellControl" style="width:30%">
                                                        <asp:TextBox ID="txtNoteDesc" runat="server" Height="100px" Width="220px" TextMode="MultiLine" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                    <td class="tablecellControl" valign="top">
                                                        <div class="lbl">
                                                            Note Type:</div>
                                                        <extddl:ExtendedDropDownList ID="extddlNType" runat="server" Width="200px" Connection_Key="Connection_String"
                                                         Flag_Key_Value="LIST" Procedure_Name="SP_MST_NOTES_TYPE" Selected_Text="---Select---"  />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tablecellLabel">
                                                        <div class="lbl">
                                                            </div>
                                                    </td>
                                                    <td class="tablecellSpace">
                                                        <asp:CheckBox ID="chkReminderPopup" runat="server" Text="Reminder popup"  />
                                                        <extddl:ExtendedDropDownList ID="extddlNoteType" runat="server" Width="225px" Connection_Key="Connection_String"
                                                            Procedure_Name="SP_MST_NOTES_TYPE" Selected_Text="---Select---" Flag_Key_Value="LIST" Visible="False" >
                                                        </extddl:ExtendedDropDownList>
                                                    </td>
                                                    <td class="tablecellControl">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center" width="100%" colspan="3">
                                                        <asp:TextBox ID="txtCompanyIDForNotes" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                        <asp:TextBox ID="txtUserID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtCompanyID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" Width="80px" CssClass="Buttons"
                                                            Text="Add"></asp:Button>
                                                        <%--                        <asp:Button ID="btnUpdate" runat="server" Text="Update"
                                                Width="80px" cssclass="btn-gray"/>--%>
                                                        <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Width="80px" CssClass="Buttons"
                                                            Text="Clear"></asp:Button>
                                                        <asp:TextBox ID="txtCaseID" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtNoteCode" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="SectionDevider">
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td style="width: 100%; text-align: center;" class="TDPart">
                                        <extddl:ExtendedDropDownList ID="extddlFilter" runat="server" Width="200px" Connection_Key="Connection_String"
                                        Flag_Key_Value="LIST" Procedure_Name="SP_MST_NOTES_TYPE" Selected_Text="---Select---" />
                                        <asp:Button ID="btnFilter" runat="server" Text="Filter" Width="80px" CssClass="Buttons" OnClick="btnFilter_Click" />
                                    </td>
                                </tr>--%>
                                
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table style="vertical-align: middle; width: 100%;">
                                                    <tbody> 
                                                        <tr>
                                                           <td style="vertical-align: middle; width: 50%" align="left"> Search:
                                                               <gridsearch:XGridSearchTextBox ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input"></gridsearch:XGridSearchTextBox>
                                                               <extddl:ExtendedDropDownList ID="extddlFilter" runat="server" Width="200px" Connection_Key="Connection_String"
                                                                Flag_Key_Value="LIST" Procedure_Name="SP_MST_NOTES_TYPE" Selected_Text="GENERAL" AutoPost_back="true" OnextendDropDown_SelectedIndexChanged="extddlFilter_SelectedIndexChanged" />
                                                           </td>
                                                           <td style="width: 50%" align="right">
                                                               Record Count : <%= this.grdNotes.RecordCount%> | Page Count :
                                                               <gridpagination:XGridPaginationDropDown ID="con" runat="server"></gridpagination:XGridPaginationDropDown>
                                                               <asp:LinkButton ID="lnkExportToExcel" OnClick="lnkExportTOExcel_onclick" runat="server" Text="Export TO Excel">
                                                               <img src="Images/Excel.jpg" alt="" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btndelete" runat="server" Text="Hard Delete"  CssClass="Buttons"  OnClick="btnDelete_Click" Visible="false"/>
                                                                <asp:Button ID="softdelete" runat="server" Text="Soft Delete"  CssClass="Buttons"  OnClick="softDelete_Click" Visible="false" />
                                                            </td>                                                                                                                                   
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                        
                                            <td>
                                                <xgrid:XGridViewControl ID="grdNotes" runat="server" Width="100%" CssClass="mGrid"
                                                 MouseOverColor="0, 153, 153" DataKeyNames="I_NOTE_ID"
                                                 EnableRowClick="false" ContextMenuID="ContextMenu1" HeaderStyle-CssClass="GridViewHeader"
                                                 AlternatingRowStyle-BackColor="#EEEEEE"
                                                 ExportToExcelFields="SZ_NOTE_DESCRIPTION,SZ_USER_NAME,DT_ADDED,SZ_NOTE_TYPE"
                                                 ShowExcelTableBorder="true" ExportToExcelColumnNames="Note Description, User Name, Date,Note Type"
                                                 AllowPaging="true" XGridKey="Notes_Save" PageRowCount="50" PagerStyle-CssClass="pgr"
                                                 AllowSorting="true" AutoGenerateColumns="false" GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField HeaderStyle-HorizontalAlign="left" ItemStyle-HorizontalAlign="left" DataField="I_NOTE_ID" HeaderText="Notes ID" Visible="False" />
                                                        <asp:BoundField DataField="SZ_NOTE_DESCRIPTION" HeaderText="Notes Description" SortExpression="SZ_NOTE_DESCRIPTION" />
                                                        <asp:BoundField DataField="SZ_USER_ID" HeaderText="User Name" Visible="False" />
                                                        <asp:BoundField DataField="SZ_USER_NAME" HeaderText="User Name" />
                                                        <asp:BoundField DataField="DT_ADDED" ItemStyle-HorizontalAlign="right" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" SortExpression="DT_ADDED" />
                                                        <asp:BoundField DataField="SZ_NOTE_TYPE" HeaderText="Note Type" SortExpression="SZ_NOTE_TYPE" />
                                                        <asp:BoundField DataField="Deletestatus" HeaderText ="delete status" Visible="false" />
                                                        <%--<asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkdelete" runat="server" />
                                                        </ItemTemplate>
                                                        </asp:TemplateField>--%> 
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="center">
                                                          <HeaderTemplate>
                                                             <asp:CheckBox ID="chkSelectAll" runat="server" tooltip="Select All" onclick="javascript:SelectAll(this.checked);"/>
                                                          </HeaderTemplate>
                                                          <itemtemplate>
                                                          <asp:CheckBox ID="chkDelete" runat="server" />
                                                           </itemtemplate>
                                                        </asp:TemplateField>              
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </xgrid:XGridViewControl>
                                            </td>
                                            
                                        <%--<td>
                                              <asp:DataGrid ID="grdNotes" runat="server" PageSize="3" OnPageIndexChanged="grdNotes_PageIndexChanged"
                                            Width="100%" CssClass="GridTable" AutoGenerateColumns="false" AllowPaging="true"
                                            PagerStyle-Mode="NumericPages" >
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <asp:BoundColumn DataField="I_NOTE_ID" HeaderText="Notes ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_NOTE_DESCRIPTION" HeaderText="Notes Description">
                                                    <HeaderStyle Width="550px" />
                                                    <ItemStyle Width="600px" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_USER_ID" HeaderText="User Name" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_USER_NAME" HeaderText="User Name">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DT_ADDED" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="SZ_NOTE_TYPE" HeaderText="Note Type">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Deletestatus" HeaderText ="delete status" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkdelete" runat="server" />
                                                </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
                                        </asp:DataGrid>
                                        </td>--%>
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
                        </td>
                        <td class="CenterBottom">
                        </td>
                        <td class="RightBottom" style="width: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divpatientID" style="position: absolute; width: 850px; height: 480px; background-color: #DBE6FA;
        visibility: hidden; border-right: silver 1px solid; border-top: silver 1px solid;
        border-left: silver 1px solid; border-bottom: silver 1px solid;">
        <div style="position: relative; text-align: right; background-color: #8babe4;">
            <a onclick="closeTypePage()" style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="framepatientDesk" src="" frameborder="0" height="470px" width="850px"
            visible="false"></iframe>
    </div>
    <div id="divid" style="position: absolute; width: 1000px; height: 900px; background-color: #DBE6FA;
        visibility: hidden;">
        <div style="position: relative; text-align: right; background-color: #8babe4; width: 730px">
            <a onclick="document.getElementById('divid').style.zIndex = '-1'; document.getElementById('divid').style.visibility='hidden'; "
                style="cursor: pointer;" title="Close">X</a>
        </div>
        <iframe id="frameeditexpanse" src="" frameborder="0" height="900px" width="1300px"></iframe>
    </div>
        
</asp:Content>

