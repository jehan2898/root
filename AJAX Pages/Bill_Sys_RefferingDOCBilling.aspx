<%@ Page Title="Green Your Bills - Reffering Doctor" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_RefferingDOCBilling.aspx.cs" 
Inherits="AJAX_Pages_Bill_Sys_RefferingDOCBilling"  %>

<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="extddl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<%@ Register Namespace="XGridPagination" TagPrefix="gridpagination" Assembly="XGridPagination" %>
<%@ Register Namespace="XGridSearchTextBox" TagPrefix="gridsearch" Assembly="XGridSearchTextBox" %>
<%@ Register Namespace="CustomControls.ContextMenuScope" TagPrefix="cMenu" Assembly="XGridContextMenu" %>
<%@ Register Namespace="XGridHyperlinkField" TagPrefix="xlink" Assembly="XGridHyperlinkField" %>
<%@ Register Namespace="XGridView" TagPrefix="xgrid" Assembly="XGridViewControl" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="36000">
    </asp:ScriptManager>

    <script type="text/javascript">


        function SelectAll(ival) {
            var f = document.getElementById("<%= grdRffDoc.ClientID %>");
            var str = 1;
            for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                if (f.getElementsByTagName("input").item(i).type == "checkbox") {



                    if (f.getElementsByTagName("input").item(i).disabled == false) {
                        f.getElementsByTagName("input").item(i).checked = ival;
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

        function validate() {

            if (document.getElementById("<%=txtDoctorName.ClientID%>").value == '') {
                alert('Please Select Doctor Name');
                return false;
            } else if (document.getElementById("ctl00_ContentPlaceHolder1_extddlDoctorType").value == 'NA') {
                alert('Please Select Speciality ');
                return false;
            } else if (document.getElementById("ctl00_ContentPlaceHolder1_extddlOffice").value == 'NA') {
                alert('Please Select Office ');
                return false;
            } else {
                return true;
            }



        }
        function Clear() {

            document.getElementById("<%=txtDoctorName.ClientID%>").value = '';

            document.getElementById("<%=txtLicenseNumber.ClientID%>").value = '';
            document.getElementById("<%=txtAssignNumber.ClientID%>").value = '';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlDoctorType").value = 'NA';
            document.getElementById("ctl00_ContentPlaceHolder1_extddlOffice").value = 'NA';
            document.getElementById("<%=btnSave.ClientID%>").disabled = false;
            document.getElementById("<%=btnUpdate.ClientID%>").disabled = true;




        }

        function ConfirmDelete() {
            var msg = "Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {

                return true;
            }
            else {
                return false;
            }
        }

        function Check() {
            var msg = "Assign # alredy assign to another doctor \n Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {
                //document.getElementById("<%=hdVlaue.ClientID%>").value="1";
                //alert( document.getElementById("<%=hdVlaue.ClientID%>").value);
                document.getElementById('ctl00_ContentPlaceHolder1_test').click();
                // return true;

            }
            else {
                document.getElementById("<%=hdVlaue.ClientID%>").value = "0";
                ALERT(document.getElementById("<%=hdVlaue.ClientID%>").value);
            }
        }

        function CheckUpdate() {
            var msg = "Assign # alredy assign to another doctor \n Do you want to proceed?";
            var result = confirm(msg);
            if (result == true) {

                //alert( document.getElementById("<%=hdVlaue.ClientID%>").value);
                document.getElementById('ctl00_ContentPlaceHolder1_CheckUpdate').click();
                // return true;

            }
            else {

            }
        }
       
    </script>

    <table width="100%" style="background-color: white">
        <tr>
            <td valign="top" style="width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <contenttemplate>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 100%">
                                        <table style="border-right: #b5df82 1px solid; border-left: #b5df82 1px solid; width: 50%;
                                            border-bottom: #b5df82 1px solid" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'ctl00_ContentPlaceHolder1_btnSearch')"
                                            cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td class="txt2" valign="middle" align="left" bgcolor="#b5df82" colspan="3" height="28">
                                                        <b class="txt3"></b>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                        <td colspan="3">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <UserMessage:MessageControl runat="server" ID="usrMessage" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                                                <tr>
                                                   <td class="td-widget-bc-search-desc-ch">
                                                        Doctor Name
                                                    </td>
                                                   <td class="td-widget-bc-search-desc-ch">
                                                        Doctor License Number
                                                    </td>
                                                   <td class="td-widget-bc-search-desc-ch">
                                                        Assign No
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtDoctorName" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtLicenseNumber" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtAssignNumber" runat="server" MaxLength="50" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td-widget-bc-search-desc-ch">
                                                        Speciality
                                                    </td>
                                                   <td class="td-widget-bc-search-desc-ch">
                                                        Office Name
                                                    </td>
                                                    <td class="td-widget-bc-search-desc-ch">
                                                        NPI
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <cc1:ExtendedDropDownList ID="extddlDoctorType" runat="server" Width="83%" Selected_Text="--- Select ---"
                                                            Flag_Key_Value="GET_PROCEDURE_GROUP_LIST" Procedure_Name="SP_MST_PROCEDURE_GROUP"
                                                            Connection_Key="Connection_String"></cc1:ExtendedDropDownList>
                                                    </td>
                                                    <td align="center">
                                                        <cc1:ExtendedDropDownList ID="extddlProvider" runat="server" Width="83%" Selected_Text="--- Select ---"
                                                            Flag_Key_Value="PROVIDER_LIST" Procedure_Name="SP_MST_PROVIDER" Connection_Key="Connection_String"
                                                            Visible="false"></cc1:ExtendedDropDownList>
                                                        <cc1:ExtendedDropDownList ID="extddlOffice" runat="server" Width="83%" Selected_Text="--- Select ---"
                                                            Flag_Key_Value="OFFICE_LIST" Procedure_Name="SP_MST_OFFICE_REFF" Connection_Key="Connection_String">
                                                        </cc1:ExtendedDropDownList>
                                                    </td>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtNPI" runat="server" MaxLength="20" CssClass="textboxCSS"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td colspan="3">
                                                &nbsp;
                                                </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <asp:Button Style="width: 80px" ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"></asp:Button>
                                                        <asp:Button Style="width: 80px" ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                                                       <%-- <asp:Button Style="width: 80px" ID="btnClear" runat="server" Text="Clear"  OnClick="btnClear_Click"></asp:Button>--%>
                                                        <input type="button" runat="server" id="btnClear1" onclick="Clear();"  style="width: 80px" value="Clear"  />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 130%">
                                        <table style="vertical-align: middle; width: 100%">
                                            <tbody>
                                            
                                             <tr>
                                                                <td style="width: 413px" class="txt2" valign="middle" align="left" bgcolor="#b5df82"
                                                                    colspan="6" height="28">
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" Text="Referring Doctor"></asp:Label>
                                                                  
                                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
                                                                        DisplayAfter="10">
                                                                        <ProgressTemplate>
                                                                            <div id="DivStatus4" style="text-align: center; vertical-align: bottom;" class="PageUpdateProgress"
                                                                                runat="Server">
                                                                                <asp:Image ID="img4" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                    Height="25px" Width="24px"></asp:Image>
                                                                                Loading...</div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                <tr>
                                                    <td style="vertical-align: middle; width: 30%" align="left">
                                                        <asp:Label ID="Label2" runat="server" Text="Search:" Font-Size="Small" Font-Bold="True"></asp:Label><gridsearch:XGridSearchTextBox
                                                            ID="txtSearchBox" runat="server" AutoPostBack="true" CssClass="search-input">
                                                        </gridsearch:XGridSearchTextBox>
                                                    </td>
                                                    <td style="vertical-align: middle; width: 30%" align="left">
                                                    </td>
                                                    <td style="vertical-align: middle; width: 40%; text-align: right" align="right" colspan="2">
                                                        Record Count:<%= this.grdRffDoc.RecordCount%>
                                                        | Page Count:
                                                        <gridpagination:XGridPaginationDropDown ID="con" runat="server">
                                                        </gridpagination:XGridPaginationDropDown>
                                                         <asp:LinkButton ID="lnkExportToExcel1" runat="server" OnClick="lnkExportTOExcel_onclick"
                                                            Text="Export TO Excel">
                                            <img alt="" src="Images/Excel.jpg" style="border:none;"  height="15px" width ="15px" title = "Export TO Excel"/></asp:LinkButton>
                                                   
                                                        
                                                      
                                                                               
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td colspan="4" align="right">
                                                <asp:Button Style="width: 80px" ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" ></asp:Button>
                                               
                                                </td>
                                                </tr>
                                                <tr>
                                                <td colspan="4" style="width:100%" >
                                                
                                                 <xgrid:XGridViewControl ID="grdRffDoc" runat="server" Height="148px" Width="100%"
                                                                        CssClass="mGrid"  AllowSorting="true"
                                                                        PagerStyle-CssClass="pgr" PageRowCount="50" DataKeyNames="SZ_DOCTOR_ID,SZ_OFFICE_ID,SZ_PROCEDURE_GROUP_ID"
                                                                        XGridKey="Bill_Sys_Billing_Rff_Doc" GridLines="None" AllowPaging="true" AlternatingRowStyle-BackColor="#EEEEEE"
                                                                        HeaderStyle-CssClass="GridViewHeader" ContextMenuID="ContextMenu1" EnableRowClick="false"
                                                                        ShowExcelTableBorder="true" ExcelFileNamePrefix="ReffDoc" MouseOverColor="0, 153, 153"
                                                                        AutoGenerateColumns="false" ExportToExcelColumnNames="Doctor Name,Doctor License Number,Assign #,Office Name,Address,City,State,Zip,Phone"
                                                                        ExportToExcelFields="SZ_DOCTOR_NAME,SZ_DOCTOR_LICENSE_NUMBER,SZ_ASSIGN_NUMBER,SZ_OFFICE,SZ_OFFICE_ADDRESS,SZ_OFFICE_CITY,SZ_OFFICE_STATE,SZ_OFFICE_ZIP,SZ_OFFICE_PHONE" OnRowCommand="grdRffDoc_RowCommand">
                                                                        <HeaderStyle CssClass="GridViewHeader"></HeaderStyle>
                                                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                                                        <AlternatingRowStyle BackColor="#EEEEEE"></AlternatingRowStyle>
                                                                        <Columns>
                                                                         <%-- 0--%>
                                                                           <asp:TemplateField HeaderText="">
                                                                               
                                                                                <itemtemplate>
                                                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName="Select" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' ></asp:LinkButton>
                                                                            </itemtemplate>
                                                                            </asp:TemplateField>
                                                                            <%-- 1--%>
                                                                        <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                          visible="false" headertext="Doctor ID" DataField="SZ_DOCTOR_ID" />
                                                                                <%-- 2--%>
                                                                          <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                 headertext="Doctor Name" DataField="SZ_DOCTOR_NAME"  SortExpression="SZ_DOCTOR_NAME"  />
                                                                            <%-- 3--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                visible="false" headertext="Doctor Type" DataField="SZ_DOCTOR_TYPE" />
                                                                                
                                                                                    <%-- 4--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                visible="false" headertext="Doctor ID" DataField="SZ_DOCTOR_TYPE_ID" />
                                                                                
                                                                                  <%-- 5--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                headertext="Doctor License Number" DataField="SZ_DOCTOR_LICENSE_NUMBER" SortExpression="SZ_DOCTOR_LICENSE_NUMBER"  />
                                                                                
                                                                                   <%-- 6--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="Provider ID" DataField="SZ_PROVIDER_ID" />
                                                                                
                                                                                 <%-- 7--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                headertext="Assign #" DataField="SZ_ASSIGN_NUMBER" SortExpression="SZ_ASSIGN_NUMBER" />
                                                                                
                                                                                    <%-- 8--%>
                                                                            <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"
                                                                                headertext="Office Name" DataField="SZ_OFFICE" 
                                                                                SortExpression="(SELECT SZ_OFFICE +  Isnull((SELECT '-' +SZ_LOCATION_NAME FROM MST_LOCATION WHERE SZ_LOCATION_ID = MST_OFFICE.SZ_LOCATION_ID) ,'') FROM MST_OFFICE WHERE SZ_OFFICE_ID=MST_DOCTOR.SZ_OFFICE_ID)"  />
                                                                                
                                                                               <%-- 9--%>
                                                                              <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                               visible="false"     headertext="Address" DataField="SZ_OFFICE_ADDRESS" SortExpression="SZ_OFFICE_ADDRESS"  />
                                                                        
                                                                            
                                                                                <%-- 10--%>
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="Provider ID" DataField="SZ_PROVIDER_ID" />
                                                                             
                                                                              
                                                                           
                                                                             
                                                                               <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                               visible="false"   headertext="City" DataField="SZ_OFFICE_CITY" SortExpression="SZ_OFFICE_CITY" />
                                                                             
                                                                               <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                               visible="false"    headertext="State" DataField="SZ_OFFICE_STATE" SortExpression="SZ_OFFICE_STATE" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                              visible="false"   headertext="Zip" DataField="SZ_OFFICE_ZIP" SortExpression="SZ_OFFICE_ZIP" />
                                                                                
                                                                                <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                visible="false" headertext="Phone" DataField="SZ_OFFICE_PHONE"  SortExpression="SZ_OFFICE_PHONE"/>
                                                                               
                                                                               <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="WCB Auth" DataField="SZ_WCB_AUTHORIZATION" />
                                                                             
                                                                               <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="WCB Auth" DataField="SZ_WCB_RATING_CODE" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="WCB Code" DataField="SZ_WCB_RATING_CODE" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                visible="false"    headertext="Address" DataField="SZ_BILLING_ADDRESS" />
                                                                             
                                                                              <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                visible="false"  headertext="Billing City" DataField="SZ_BILLING_CITY" />
                                                                             
                                                                              <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                 visible="false" headertext="Billing State" DataField="SZ_BILLING_STATE" />
                                                                             
                                                                              <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                               visible="false"  headertext="Billing Zip" DataField="SZ_BILLING_ZIP" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                                visible="false"  headertext="Billing Phone" DataField="SZ_BILLING_PHONE" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="true"   headertext="NPI" DataField="SZ_NPI" />
                                                                             
                                                                               <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="Federal Tax" DataField="SZ_FEDERAL_TAX_ID" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="Tax Type" DataField="BIT_TAX_ID_TYPE" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="KOEL" DataField="FLT_KOEL" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="KOEL" DataField="SZ_PROCEDURE_GROUP_ID" />
                                                                             
                                                                             <asp:BoundField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="left"
                                                                             visible="false"   headertext="Office ID" DataField="SZ_OFFICE_ID" />
                                                                             
                                                                               <asp:TemplateField HeaderText="">
                                                                                <headertemplate>
                                                                              <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAll(this.checked);"  ToolTip="Select All" />
                                                                          </headertemplate>
                                                                                <itemtemplate>
                                                                             <asp:CheckBox ID="ChkDelete" runat="server" />
                                                                            </itemtemplate>
                                                                            </asp:TemplateField>
                                                                                
                                                                        </Columns>
                                                                        </xgrid:XGridViewControl>
                                                </td>
                                                </tr>
                                            </tbody>
                                        </table>
            </td>
            </tr> </tbody> </table> </contenttemplate>
                </asp:UpdatePanel>
                <asp:TextBox ID="txtCompanyID" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtUserId" runat="server" Visible="false"></asp:TextBox>
                <asp:HiddenField runat="server" ID="hdVlaue" />
                <asp:HiddenField runat="server" ID="hdCheck" />
            </td>
        </tr>
    </table>
    <asp:Button ID="test" runat="server" Style="visibility: hidden;" OnClick="test_click" />
    <asp:Button ID="CheckUpdate" runat="server" Style="visibility: hidden;" OnClick="CheckUpdate_click" />
</asp:Content>

