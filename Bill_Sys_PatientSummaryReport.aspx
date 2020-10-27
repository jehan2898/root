<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bill_Sys_PatientSummaryReport.aspx.cs" Inherits="Bill_Sys_PatientSummaryReport" Title="Green Bills - Patient Summary Report"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                                      <table style="width: 100%">
                                          <tr>
                                              <td class="ContentLabel" style="text-align: left;">
                                                 <b> Patient Summary Report </b>
                                              </td>
                                          </tr>
                                        <tr>
                                            <td style="width: 100%" align="right">
                                                <asp:Button ID="btnPrint" runat="server" CssClass="Buttons" Text="Print Report" OnClick="btnPrint_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <asp:DataGrid ID="grdPatientSummaryReport" AutoGenerateColumns="false" runat="server" Width="100%" CssClass="GridTable" >
                                                    <ItemStyle CssClass="GridRow" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="SZ_CASE_ID" HeaderText="Case ID" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_CASE_NO" HeaderText="Case No" ></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_PATIENT_NAME" HeaderText="Patient Name"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_INSURANCE_NAME" HeaderText="Insurance Company"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SZ_ATTORNEY_NAME" HeaderText="Attorney"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DT_DATE_OF_ACCIDENT" HeaderText="Accident Date" ></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DATE_OF_FIRST_TREATMENT" HeaderText="Date of First Treatment" ></asp:BoundColumn>
                                                         <asp:BoundColumn DataField="DATE_OF_LAST_TREATMENT" HeaderText="Date of Last Treatment" ></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkPrint" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHeader" />
                                                </asp:DataGrid>
                                            </td>
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

</asp:Content>

