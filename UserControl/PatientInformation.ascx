<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientInformation.ascx.cs"
    Inherits="PatientInformation" %>

<script type="text/javascript" language="javascript">
        function spanhide() {
   //  alert("call");
     document.getElementById('ctl00_ContentPlaceHolder1_UserPatientInfoControl_rptPatientDeskList_ctl00_provider').style.display= 'none';
      //  alert("ok");
        document.getElementById('ctl00_ContentPlaceHolder1_UserPatientInfoControl_rptPatientDeskList_ctl00_Location').style.display= 'none';
    //   alert('done');
         document.getElementById('dataProvider').style.display= 'none';
    //     alert('done1');
          document.getElementById('dataLocation').style.display= 'none';
          
     //   
   // alert("hide done");
       }
       
</script>

<table style="width: 100%">
    <tr>
        <td>
            <asp:Repeater ID="rptPatientDeskList" runat="server">
                <HeaderTemplate>
                    <table align="left" cellpadding="0" cellspacing="0" style="width: 100%; border: #8babe4 1px solid #B5DF82;">
                        <tr>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                <b>Case#</b>
                            </td>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblheader" runat="server">
                                <b>Chart No</b>
                            </td>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                <b>Patient Name</b>
                            </td>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="tblRemoteCaseid"
                                runat="server">
                                <b>Patient ID</b>
                            </td>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                <b>Insurance Name</b>
                            </td>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;">
                                <b>Accident Date</b>
                            </td>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="provider" runat="server">
                                <b>Provider Name</b>
                            </td>
                            <td bgcolor="#B5DF82" class="lbl" style="font-weight: bold;" id="Location" runat="server">
                                <b>Location</b>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                            <%# DataBinder.Eval(Container,"DataItem.SZ_CASE_ID")%>
                        </td>
                        <td bgcolor="white" class="lbl" id="tblvalue" runat="server" style="border: 1px solid #B5DF82">
                            <%# DataBinder.Eval(Container,"DataItem.SZ_CHART_NO")%>
                        </td>
                        <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                            <%# DataBinder.Eval(Container,"DataItem.SZ_PATIENT_NAME")%>
                        </td>
                        <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;" id="tblRemoteValue"
                            runat="server">
                            <%# DataBinder.Eval(Container, "DataItem.SZ_PATIENT_ID_LHR")%>
                        </td>
                        <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                            <%# DataBinder.Eval(Container,"DataItem.SZ_INSURANCE_NAME")%>
                        </td>
                        <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;">
                            <%# DataBinder.Eval(Container,"DataItem.DT_ACCIDENT","{0:MM/dd/yyyy}")%>
                        </td>
                        <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;" id="dataProvider">
                            <%# DataBinder.Eval(Container, "DataItem.Provider Name")%>
                        </td>
                        <td bgcolor="white" class="lbl" style="border: 1px solid #B5DF82;" id="dataLocation">
                            <%# DataBinder.Eval(Container, "DataItem.Location")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
