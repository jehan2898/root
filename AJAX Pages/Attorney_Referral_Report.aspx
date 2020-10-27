<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Attorney_Referral_Report.aspx.cs" Inherits="AJAX_Pages_Attorney_Referral_Report" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ExtendedDropDownList" Namespace="ExtendedDropDownList" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
  <script language="javascript" type="text/javascript">

      function Clear(s,e) {
          
          cddlvisitdate.SetValue('0');
          cddlvisitdate.SetText('All');
          cntdtfromdate.SetDate(null);
          cntdttodate.SetDate(null);
          clstattorney.UnselectAll();
          e.processOnServer = false; 
      }


      function redirect(LawFirmID) {
          window.open('../Get_Treatment.aspx?assignedLFid=' + LawFirmID + '&visitfromdate=' + cntdtfromdate.GetText() + '&visittodate=' + cntdttodate.GetText(),'tretment', 'left=30,top=30,scrollbars=1');
      }
      function OnIndexChnage(s, e) {

          // var dropDown = window[listBox.cplsb_datevalues];
          var lastType = null;
          lastType = cddlvisitdate.GetValue().toString();
          getWeek();

          //            var lastType = null;
          //            lastType = clsb_datevalues.GetValue().toString();
          //            // var lastType = listBox.GetSelectedItems().toString();
          getWeek();
          if (lastType != "NA") {

              if (lastType == "1") {
                  var tDate = getDate('today');

                  cntdtfromdate.SetText(tDate);
                  cntdttodate.SetText(tDate);

              } else if (lastType == "0") {
                  cntdtfromdate.SetDate(null);
                  cntdttodate.SetDate(null);
              } else if (lastType == "2") {

                  cntdtfromdate.SetText(getWeek('startweek'));
                  cntdttodate.SetText(getWeek('endweek'));

              } else if (lastType == "3") {

                  cntdtfromdate.SetText(getDate('monthstart'));
                  cntdttodate.SetText(getDate('monthend'));

              } else if (lastType == "4") {

                  cntdtfromdate.SetText(getDate('quarterstart'));
                  cntdttodate.SetText(getDate('quarterend'));

              } else if (lastType == "5") {

                  cntdtfromdate.SetText(getDate('yearstart'));
                  cntdttodate.SetText(getDate('yearend'));

              } else if (lastType == "6") {

                  cntdtfromdate.SetText(getLastWeek('startweek'));
                  cntdttodate.SetText(getLastWeek('endweek'));

              } else if (lastType == "7") {

                  cntdtfromdate.SetText(lastmonth('startmonth'));
                  cntdttodate.SetText(lastmonth('endmonth'));

              } else if (lastType == "8") {

                  cntdtfromdate.SetText(lastyear('startyear'));
                  cntdttodate.SetText(lastyear('endyear'));

              } else if (lastType == "9") {

                  cntdtfromdate.SetText(quarteryear('startquaeter'));
                  cntdttodate.SetText(quarteryear('endquaeter'));

              }
          } else {
              cntdtfromdate.SetDate(null);
              cntdttodate.SetDate(null);
          }
      }


      function getLastWeek(type) {
          var d = new Date();
          d.setDate(d.getDate() - 7);
          var day = d.getDay();
          d.setDate(d.getDate() - day);
          if (type == 'startweek')
              return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
          if (type == 'endweek') {
              d.setDate(d.getDate() + 6);
              return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
          }
      }
      function lastmonth(type) {

          var d = new Date();
          var t_date = d.getDate();      // Returns the day of the month
          var t_mon = d.getMonth() + 1;      // Returns the month as a digit
          var t_year = d.getFullYear();

          if (type == 'startmonth') {
              if (t_mon == 1) {
                  var y = t_year - 1;
                  return ('12/1/' + y);

              }
              else {
                  var m = t_mon - 1;
                  return (m + '/1/' + t_year);
              }
          }
          else if (type == 'endmonth') {
              if (t_mon == 1) {
                  var y = t_year - 1;
                  return ('12/31/' + y);
              } else {
                  var m = t_mon - 1;
                  var d = daysInMonth(t_mon - 1, t_year);
                  return (m + '/' + d + '/' + t_year);
              }
          }

      }

      function quarteryear(type) {
          var d = new Date();
          var t_date = d.getDate();      // Returns the day of the month
          var t_mon = d.getMonth() + 1;      // Returns the month as a digit
          var t_year = d.getFullYear();

          if (type == 'startquaeter') {
              if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
                  var y = t_year - 1;
                  return ('10/1/' + y);
              }
              else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                  return ('1/1/' + t_year);

              } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                  return ('4/1/' + t_year);


              } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                  return ('7/1/' + t_year);

              }

          } else if (type == 'endquaeter') {
              if (t_mon == 1 || t_mon == 2 || t_mon == 3) {
                  //
                  var y = t_year - 1;
                  return ('12/31/' + y);
              }
              else if (t_mon == 4 || t_mon == 5 || t_mon == 6) {
                  return ('3/31/' + t_year);

              } else if (t_mon == 7 || t_mon == 8 || t_mon == 9) {
                  return ('6/30/' + t_year);


              } else if (t_mon == 10 || t_mon == 11 || t_mon == 12) {
                  return ('9/30/' + t_year);
              }

          }

      }

      function lastyear(type) {
          var d = new Date();

          var t_year = d.getFullYear();
          if (type == 'startyear') {
              y = t_year - 1;
              return ('1/1/' + y);
          }
          else if (type == 'endyear') {
              y = t_year - 1;
              return ('12/31/' + y);
          }
      }



      function getDate(type) {
          var d = new Date();
          var t_date = d.getDate();      // Returns the day of the month
          var t_mon = d.getMonth();      // Returns the month as a digit
          var t_year = d.getFullYear();  // Returns 4 digit year

          var q_start = 0;
          var q_end = 0;
          if ((t_mon + 1) >= 1 && (t_mon + 1) <= 3) {
              q_start = 1;
              q_end = 3;
          }
          else if ((t_mon + 1) >= 4 && (t_mon + 1) <= 6) {
              q_start = 4;
              q_end = 6;
          }
          else if ((t_mon + 1) >= 7 && (t_mon + 1) <= 9) {
              q_start = 7;
              q_end = 9;
          }
          else if ((t_mon + 1) >= 10 && (t_mon + 1) <= 12) {
              q_start = 10;
              q_end = 12;
          }

          if (type == 'today')
              return (getFormattedDate(t_date, t_mon, t_year));
          if (type == 'monthstart')
              return (getFormattedDate(1, t_mon, t_year));
          if (type == 'monthend')
              return (getFormattedDate(daysInMonth(t_mon + 1, t_year), t_mon, t_year));
          if (type == 'quarterstart') {
              return (getFormattedDateForMonth(1, q_start, t_year));
          }
          if (type == 'quarterend') {
              return (getFormattedDateForMonth(daysInMonth(q_end), q_end, t_year));
          }
          if (type == 'yearstart')
              return (getFormattedDate(1, 0, t_year));
          if (type == 'yearend')
              return (getFormattedDate(31, 11, t_year));
      }

      function daysInMonth(month, year) {
          var m = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
          if (month != 2) return m[month - 1];
          if (year % 4 != 0) return m[1];
          if (year % 100 == 0 && year % 400 != 0) return m[1];
          return m[1] + 1;
      }

      function getFormattedDate(day, month, year) {
          return '' + (month + 1) + '/' + day + '/' + year;
      }

      function getFormattedDateForMonth(day, month, year) {
          return '' + (month) + '/' + day + '/' + year;
      }

      function getWeek(type) {
          var d = new Date();
          var day = d.getDay();
          d.setDate(d.getDate() - day);
          if (type == 'startweek')
              return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
          if (type == 'endweek') {
              d.setDate(d.getDate() + 6);
              return (getFormattedDate(d.getDate(), d.getMonth(), d.getFullYear()));
          }
      }
        
   
  </script>
  
  <asp:scriptmanager id="ScriptManager1" runat="server" asyncpostbacktimeout="36000">
    </asp:scriptmanager>
    <asp:updatepanel id="UpdatePanel2" runat="server">
        <contenttemplate>
                     <table id="First" border="0" cellpadding="0" cellspacing="0" style="width: 100%;
        height: 100%">
        <tr>
            <td>
                <table id="MainBodyTable" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 99%" colspan="3">
                            <table width="50%" style="height: 50%; border: 1px solid #B5DF82;">
                                <tr>
                                    <td height="28" align="left" bgcolor="#B5DF82" class="txt2" colspan="3" valign="middle">
                                        <table width="40%" style="border-right: 1px solid #B5DF82; border-left: 1px solid #B5DF82;
                                            border-bottom: 1px solid #B5DF82">
                                            <tr>
                                                <td height="28" style="background-color: #B5DF82;" class="txt2" colspan="3">
                                                    <asp:Label Font-Bold="true" Font-Size="Small" ID="txtHeading" Text="Attorney Referral Report" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                           
                                         </table>
                                 </td>
                             </tr>

                              <tr>
                                 <td class="Attorney_report">
                                 <dxe:ASPxLabel ID="lbldtvisit" runat="server" Text="Date Of Visit"  CssClass="td-widget-bc-search-desc-ch"></dxe:ASPxLabel> </td>
                                 <td class="Attorney_report">
                                      <dxe:ASPxLabel ID="lblfrom" runat="server" Text="From" CssClass="td-widget-bc-search-desc-ch"></dxe:ASPxLabel>                                                  
                                 </td>
                                 <td class="Attorney_report">
                                   <dxe:ASPxLabel runat="server" Text="To" CssClass="td-widget-bc-search-desc-ch" ID="lbldtto"></dxe:ASPxLabel>
                                 </td>
                             </tr>

                             <tr>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="ddlvisitdate" EnableSynchronization="False"  SelectedIndex="0"
                                       ValueType="System.String" ClientInstanceName="cddlvisitdate" >
                                        <Items>
                                            
                                            <dxe:ListEditItem Text="All" Value="0"  Selected="true"/>
                                            <dxe:ListEditItem Text="Today" Value="1" /> 
                                            <dxe:ListEditItem Text="This Week" Value="2" />
                                            <dxe:ListEditItem Text="This Month" Value="3" />
                                            <dxe:ListEditItem Text="This Quarter" Value="4" />
                                            <dxe:ListEditItem Text="This Year" Value="5" />
                                            <dxe:ListEditItem Text="Last Week" Value="6" />                                            
                                            <dxe:ListEditItem Text="Last Month" Value="7" />
                                            <dxe:ListEditItem Text="Last Quarter" Value="9" />
                                            <dxe:ListEditItem Text="Last Year" Value="8" />
                                        </Items>
                                       <ItemStyle>
                                            <HoverStyle BackColor="#F6F6F6">
                                            </HoverStyle>
                                            </ItemStyle>
                                    <ClientSideEvents SelectedIndexChanged="OnIndexChnage" /> 
                                       </dxe:ASPxComboBox>
                                </td>
                                <td>
                                        <dxe:ASPxDateEdit  runat="server" ClientInstanceName="cntdtfromdate" 
                                            ID="dtfromdate" EditFormat="Custom" EditFormatString="MM/dd/yyyy" 
                                            EnableTheming="False">
                                        </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                        <dxe:ASPxDateEdit runat="server" ClientInstanceName="cntdttodate" ID="dttodate" 
                                            EditFormat="Custom" EditFormatString="MM/dd/yyyy" EnableTheming="False">
                                        </dxe:ASPxDateEdit>
                                </td>
                             </tr>
                              <tr>
                                <td> &nbsp;</td>
                             </tr>
                            
                             <tr>
                                <td align="center" colspan="5px"    >
                                    <dxe:ASPxLabel ID="lblattorney" runat="server" Text="Select Attorney" CssClass="td-widget-bc-search-desc-ch">
                                    </dxe:ASPxLabel>
                                
                                </td>
                             
                             </tr>
                            
                             <tr>
                                <td colspan="5px" align="center" width="100px" >
                                    <dxe:ASPxListBox runat="server" ID="lstattorny" ClientInstanceName="clstattorney" Width="300px" ValueType="System.String" SelectionMode="Multiple">
                                        <Items>
                                           
                                        </Items>
                                    
                                    </dxe:ASPxListBox>
                                
                                </td>
                                               
                             </tr>
                           <tr>
                            <td>
                                  &nbsp;
                            </td>
                           </tr>
                             <tr>
                                <td align="center" colspan="4px">
                                    <table>
                                    <tr>
                                       
                                            <td align="center" colspan="1.5px" >
                                                <dxe:ASPxButton ID="btnsearch" runat="server" Width="80px"  Text="Search"  Font-Size="Medium"
                                                    onclick="btnsearch_Click"></dxe:ASPxButton>

                                            </td>
                                            <td align="left" >
                                            <dxe:ASPxButton ID="btnclear" runat="server" Width="80px" Text="Clear" Font-Size="Medium" ClientInstanceName="btnclear">
                                            <ClientSideEvents Click="Clear" />
                                            </dxe:ASPxButton>
                                <%--<input style="width: 80px" id="btnClear" onclick="Clear();" type="button" value="Clear"
                                                    runat="server" />--%>
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
                <td> &nbsp;
                
                 <asp:updateprogress id="UpdateProgress123" runat="server" associatedupdatepanelid="UpdatePanel2" displayafter="10">
                                                                        <progresstemplate>
                                                                            <div id="DivStatus123" style="vertical-align: bottom; position: absolute; top: 350px;
                                                                               left: 600px" runat="Server">
                                                                                      <asp:Image ID="img123" runat="server" ImageUrl="~/Images/rotation.gif" AlternateText="Loading....."
                                                                                                                Height="25px" Width="24px"></asp:Image>
                                                                             Loading...</div>
                                                                         </progresstemplate>
                                                                      </asp:updateprogress>
                                                            
                </td>
                </tr>
                <tr>
                       <td style="border: 1px solid #B5DF82;">
                             <table style="width: 100%">
                                <tr>
                                
                                    <td style="width: 100%">
                                        <div  style="height: 400px; width: 100%;  overflow: scroll;">
    
                                            <dx:ASPxGridView ID="grdattorneyreport" Border-BorderColor="#B5DF82" KeyFieldName="SZ_ASSIGNED_LAWFIRM_ID" runat="server" ClientInstanceName="cgrdattorneyreport" AutoGenerateColumns="false" Width="100%"
                                                SettingsPager-PageSize="20" >
                                                    <Columns>
                                                        <dx:GridViewDataColumn  CellStyle-Border-BorderColor="#B5DF82" Caption="Attorney"  FieldName="SZ_COMPANY_NAME" Settings-AllowSort="False" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center" >
                                                        <HeaderStyle BackColor="#B5DF82" />
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn Caption="COMPANYID" FieldName="SZ_ASSIGNED_LAWFIRM_ID" HeaderStyle-Font-Bold="true" Visible="false" 
                                                         Settings-AllowSort="False">
                                                          
                                                         </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn CellStyle-Border-BorderColor="#B5DF82" Caption="View" HeaderStyle-Font-Bold="true"  HeaderStyle-HorizontalAlign="Center" VisibleIndex="1"
                                                         Settings-AllowSort="False">
                                                         <HeaderStyle BackColor="#B5DF82" />
                                                           <DataItemTemplate>
                                                               <%-- <a onclick="redirect();" href="#"> <%# Eval("COUNT")%> </a>--%>
                                                                <a id="lnkPatient" href="#"  onclick='<%# "redirect(" + "\""+ Eval("SZ_ASSIGNED_LAWFIRM_ID")+"\");" %>' >
                                                                     <%# Eval("COUNT")%> </a>
                                                           </DataItemTemplate>
                                                         </dx:GridViewDataColumn>
                                                         
                                                    </Columns>
                                                  <%-- <ClientSideEvents RowClick="function(s, e) {  
                                                    var value= s.GetRowKey(e.visibleIndex);
                                                    alert(value);}" />  --%>
                                                                               
                                            </dx:ASPxGridView> 
                                      </div>
                                    
                                    
                                    </td>
                                </tr>
                        
                        </table>
                 
                       </td>
               </tr>
    </table>
        </contenttemplate>
    </asp:updatepanel>
</asp:Content>

