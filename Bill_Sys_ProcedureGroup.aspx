<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"
    CodeFile="Bill_Sys_ProcedureGroup.aspx.cs" Inherits="Bill_Sys_ProcedureGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="validation.js"></script>
    <script type="text/javascript">
        function validate()
        {
            var Speciality = document.getElementById('<%=txtProcedureGroup.ClientID %>').value;
            if (Speciality == "")
                {
                return false;
                }
                var specialityPat = /^[a-zA-Z0-9_-]+$/;
                var SpecialityMatch = Speciality.match(specialityPat);
            if (SpecialityMatch == null)
            {
                alert("Your specialty seems incorrect. Please try again.");
                return false;
            }
        }
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

        
    </script>

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
                                        <table border="0" cellpadding="3" cellspacing="0" class="ContentTable" style="width: 100%">
                                            <tr>
                                                <td class="ContentLabel" style="text-align: center; height: 25px;" colspan="4">
                                                    <asp:Label CssClass="message-text" ID="lblMsg" runat="server" Visible="false"></asp:Label>
                                                    <div id="ErrorDiv" style="color: red" visible="true">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    Specialty
                                                </td>
                                                <td style="width: 250px; height: 22px;" align="left">
                                                    &nbsp
                                                    <asp:TextBox ID="txtProcedureGroup" runat="server" CssClass="textboxCSS" MaxLength="50" required=""></asp:TextBox>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                                        ControlToValidate="txtProcedureGroup"
                                                        ValidationExpression="/^[a-zA-Z0-9_-]+$/"
                                                        EnableClientScript="false"
                                                        ErrorMessage="The speciality is not in valid format"
                                                        runat="server" />--%>
                                                    <td class="ContentLabel" style="width: 15%; height: 22px;">
                                                    Set Initial Evaluation
                                                </td>
                                                <td style="width: 35%; height: 22px;" align="left">
                                                    &nbsp
                                                    <asp:CheckBox ID="chkInitialEvaluation" runat="server" Text=" " />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 15%;">
                                                    Auto associate to patient</td>
                                                <td style="width: 250px;" align="left">
                                                    &nbsp
                                                    <asp:CheckBox ID="chkAutoAssociate" runat="server" Text="" />
                                                </td>
                                                <td class="ContentLabel" style="width: 20%;">
                                                    View on Patient Desk&nbsp;
                                                </td>
                                                <td style="width: 35%" align="left">
                                                    &nbsp
                                                    <asp:CheckBox ID="chkPatientDesk" runat="server" Text=" " />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 20%; height: 22px;">
                                                    Include 1500 form</td>
                                                <td style="width: 250px; height: 22px;" align="left">
                                                    &nbsp
                                                    <asp:CheckBox ID="chkinclude_1500" runat="server" Text="" />
                                                </td>
                                                <td class="ContentLabel" style="width: 20%;">
                                                    Associated specialties
                                                </td>
                                                <td style="width: 35%; height: 22px;" align="left">
                                                    &nbsp
                                                    &nbsp;
                                                    <asp:ListBox ID="lstboxSpeciality" runat="server" SelectionMode="Multiple" 
                                                        Width="200px"></asp:ListBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 20%;">
                                                    &nbsp;</td>
                                                <td style="width: 250px" align="left">
                                                    &nbsp
                                                    <asp:CheckBox ID="chkUnit" runat="server" Text="Unit"  />
                                                    <asp:CheckBox ID="chkPrintable" runat="server" Text="Printable"
                                                        />
                                                </td>
                                                <td class="ContentLabel" style="width: 20%;">
                                                    &nbsp;</td>
                                                <td style="width: 35%" align="left">
                                                    &nbsp
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 20%;">
                                                </td>
                                                <td style="width: 250px" align="left">
                                                </td>
                                                <td class="ContentLabel" style="width: 20%;">
                                                    &nbsp;</td>
                                                <td style="width: 35%" align="left">
                                                    &nbsp
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="ContentLabel" style="width: 20%;" >
                                                    
                                                </td>
                                                <td align="left" style="width: 250px">
                                                    &nbsp
                                                    <asp:ListBox ID="lstboxSpeciality1" runat="server" Width="200px" SelectionMode="Multiple" Visible="false">
                                                    </asp:ListBox>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="width: 35%" valign="top" align="left">
                                                    &nbsp
                                                    <%-- </br>
                                                    &nbsp;&nbsp;<asp:CheckBox ID="chkAutoAddVisit" runat="server" Text="Auto Add Visit" OnClick="javascript:ShowAutoTextbox();"  /> </br>
                                                    &nbsp;&nbsp;<asp:Label ID="lbl" runat="server" Text="Number of Times:" Font-Size="Small" style="visibility:hidden;"></asp:Label> &nbsp;
                                                    <asp:TextBox ID="txtAutoVisit" runat="server" Width="40px" style="visibility:hidden;"></asp:TextBox>--%>
                                                </td>
                                            </tr>
                                            <tr style="height:130px">
                                                <td class="ContentLabel" style="width: 20%;">
                                                    Initial Code(s):
                                                </td>
                                                <td align="left" style="width: 250px">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtInitialCodes" runat="server" TextMode="MultiLine" Height="70px" Width="250px"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td class="ContentLabel" >Alert after
                                                                <asp:TextBox ID="txtDaysafterInitial" runat="server"></asp:TextBox>
                                                                days following Initial
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ContentLabel" >Alert after
                                                                <asp:TextBox ID="txtvisitsafterInitial" runat="server"></asp:TextBox>
                                                                visits following Initial
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr style="height:130px">
                                                <td class="ContentLabel" style="width: 20%;">
                                                    Re-eval Code(s):
                                                </td>
                                                <td align="left" style="width: 250px">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtReevalCodes" runat="server" TextMode="MultiLine" Height="80px" Width="250px"></asp:TextBox>
                                                </td>
                                                <td colspan="2">
                                                    <table>
                                                        <tr>
                                                            <td class="ContentLabel" >Alert after
                                                                <asp:TextBox ID="txtDaysafterReeval" runat="server"></asp:TextBox>
                                                                days following Initial
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ContentLabel" >Alert after
                                                                <asp:TextBox ID="txtvisitsafterReeval" runat="server"></asp:TextBox>
                                                                visits following Initial
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ContentLabel" >
                                                                Allowed Re-Eval Count
                                                                <asp:TextBox ID="txtReevalCount" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td align="left">
                                                    <asp:RadioButton ID="rdbRefferal" runat="server" GroupName="Refferal" Text="Refferal" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdbReport" runat="server" GroupName="Refferal" Text="Report" /></td>
                                                <td class="ContentLabel">
                                                    <asp:Label ID="lblDonotHaveNotes" runat="server" Text="   Do Not Have Notes "></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkDonotHaveNotes" runat="server" Text="" TextAlign="Left" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="font-size: 12px; font-family: arial;" valign="bottom">
                                                    <asp:Label ID="lblDailyLimte" runat="server" Text="Daily Limit" Font-Size="12px"> </asp:Label>
                                                    &nbsp; &nbsp;
                                                </td>
                                                <td colspan="3" align="left" style="font-size: 12px; font-family: arial;" valign="bottom">
                                                    <asp:Label ID="lblIElimit" runat="server" Font-Size="12px" Text="IE"> </asp:Label>
                                                    <asp:TextBox ID="txtIELimit" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:CheckBox ID="chkIE" runat="server" Visible="false" />
                                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                                    <asp:Label ID="lblFUlimit" runat="server" Text="FU" Font-Size="12px"> </asp:Label>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtFULimit" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:CheckBox ID="chkFU" runat="server" Visible="false" />
                                                    &nbsp; &nbsp; &nbsp; &nbsp;
                                                    <asp:Label ID="lblClimit" runat="server" Text="C" Font-Size="12px"> </asp:Label>
                                                    &nbsp;
                                                    <asp:TextBox ID="txtCLimit" runat="server" onkeypress="return CheckForInteger(event,'.')"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:CheckBox ID="chkC" runat="server" Visible="false" />
                                                    &nbsp;
                                                    <asp:TextBox ID="txtNodeType" runat="server" Style="visibility: hidden;"></asp:TextBox>
                                                    <asp:TextBox ID="txtReferral" runat="server" Style="visibility: hidden;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblNote" runat="server" Font-Size="13px" Font-Bold="true" Font-Italic="true"
                                                        ForeColor="red" Text="Note: If you enter a value greater than 0 (zero) the daily limit for that visit type will be activated. To remove the limit type, enter 0 (zero) and click update"></asp:Label>
                                                </td>
                                                <td class="ContentLabel" valign="bottom">
                                                    <asp:TextBox ID="txtorder" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:TextBox ID="txtCompanyID" runat="server" Visible="False" Width="10px"></asp:TextBox>
                                                    <asp:Button ID="btnSave" runat="server" Text="Add" Width="80px" CssClass="Buttons"
                                                        OnClick="btnSave_Click" OnClientClick="return validate();"/>
                                                         <%--OnClientClick="return validate();"--%>
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="80px" CssClass="Buttons"
                                                        OnClick="btnUpdate_Click" />
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="Buttons" Text="Delete" OnClick="btnDelete_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" Width="80px" CssClass="Buttons"
                                                        OnClick="btnClear_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" class="TDPart">
                                        <asp:DataGrid ID="grdProcedureGroup" runat="server" OnPageIndexChanged="grdAdjuster_PageIndexChanged"
                                            OnSelectedIndexChanged="grdAdjuster_SelectedIndexChanged" Width="100%" CssClass="GridTable"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="25" PagerStyle-Mode="NumericPages">
                                            <ItemStyle CssClass="GridRow" />
                                            <Columns>
                                                <%--  0--%>
                                                <asp:ButtonColumn CommandName="Select" Text="Select" ItemStyle-CssClass="grid-item-left">
                                                </asp:ButtonColumn>
                                                <%--  1--%>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP_ID" HeaderText="Procedure Group ID"
                                                    Visible="false"></asp:BoundColumn>
                                                <%--  2--%>
                                                <asp:BoundColumn DataField="SZ_PROCEDURE_GROUP" HeaderText="Specialty"></asp:BoundColumn>
                                                <%--  3--%>
                                                <asp:BoundColumn DataField="IS_PRINTABLE" HeaderText="Printable" Visible="false"></asp:BoundColumn>
                                                <%--  4--%>
                                                <asp:BoundColumn DataField="BT_UNIT" HeaderText="Unit" Visible="false"></asp:BoundColumn>
                                                <%--  5--%>
                                                <asp:BoundColumn DataField="I_FOLLOWUP_REPORT" HeaderText="Follow up Days" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  6--%>
                                                <asp:BoundColumn DataField="I_FOLLOWUP_TIMES" HeaderText="Follow up Times" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  7--%>
                                                <asp:BoundColumn DataField="I_INITIAL_FOLLOWUP" HeaderText="Intial Followoup Days"
                                                    Visible="false"></asp:BoundColumn>
                                                <%--  8--%>
                                                <asp:BoundColumn DataField="I_INITIAL_FOLLOWUP_TIMES" HeaderText="Intial Followup Count"
                                                    Visible="false"></asp:BoundColumn>
                                                <%--  9--%>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDelete" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <%--  10--%>
                                                <asp:BoundColumn DataField="BT_IS_IN_ORDER" HeaderText="Is In Order" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  11--%>
                                                <asp:BoundColumn DataField="I_ORDER" HeaderText="Order" Visible="false"></asp:BoundColumn>
                                                <%--  12--%>
                                                <asp:BoundColumn DataField="BT_INITIAL_EVALUATION" HeaderText="Unit" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  13--%>
                                                <asp:BoundColumn DataField="bt_associate_to_patient" HeaderText="Associate" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  14--%>
                                                <asp:BoundColumn DataField="bt_include_1500" HeaderText="Include_1500" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  15--%>
                                                <asp:BoundColumn DataField="mn_ie_daily_limits" HeaderText="mn_ie_daily_limits" DataFormatString="{0:0.00}"
                                                    Visible="false"></asp:BoundColumn>
                                                <%--  16--%>
                                                <asp:BoundColumn DataField="mn_fu_daily_limits" HeaderText="mn_fu_daily_limits" DataFormatString="{0:0.00}"
                                                    Visible="false"></asp:BoundColumn>
                                                <%--  17--%>
                                                <asp:BoundColumn DataField="mn_c_daily_limits" HeaderText="mn_c_daily_limits" DataFormatString="{0:0.00}"
                                                    Visible="false"></asp:BoundColumn>
                                                <%--  18--%>
                                                <asp:BoundColumn DataField="sz_node_type" HeaderText="sz_node_type" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  19--%>
                                                <asp:BoundColumn DataField="SZ_ISREFFERAL" HeaderText="SZ_ISREFFERAL" Visible="false">
                                                </asp:BoundColumn>
                                                <%--  20--%>
                                                <asp:BoundColumn DataField="BT_NOT_HAVE_NOTES" HeaderText="BT_NOT_HAVE_NOTES" Visible="false">
                                                </asp:BoundColumn>
                                                 <%--  21--%>
                                                <asp:BoundColumn DataField="I_REEVAL_ALERT_VISITS" HeaderText="I_REEVAL_ALERT_VISITS" Visible="false">
                                                </asp:BoundColumn>

                                                 <%--  22--%>
                                                <asp:BoundColumn DataField="SZ_REEVAL_CODES" HeaderText="SZ_REEVAL_CODES" Visible="false">
                                                </asp:BoundColumn>

                                                  <%--  23--%>
                                                <asp:BoundColumn DataField="I_INITIAL_ALERT_VISITS" HeaderText="SZ_REEVAL_CODES" Visible="false">
                                                </asp:BoundColumn>

                                                <%--  24--%>
                                                <asp:BoundColumn DataField="SZ_INITIAL_CODES" HeaderText="SZ_REEVAL_CODES" Visible="false">
                                                </asp:BoundColumn>

                                            </Columns>
                                            <HeaderStyle CssClass="GridHeader" />
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
    </table>
</asp:Content>
