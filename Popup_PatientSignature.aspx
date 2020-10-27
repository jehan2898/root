<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_PatientSignature.aspx.cs" Inherits="Popup_PatientSignature" %>
<%@ Register Src="~/UserControl/ErrorMessageControl.ascx" TagName="MessageControl"
    TagPrefix="UserMessage" %>
    

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient Signature</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C# .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <script language="Javascript">
        function OnClear() {

            var obj = document.getElementById("SigPlusDoctor");
            obj.ClearTablet(); //Clears the signature, in case of error or mistake     
            obj.TabletState = 1;
            //document.FORM1.SigPlusDoctor.ClearTablet(); //Clears the signature, in case of error or mistake 
            //document.FORM1.SigPlusDoctor.TabletState = 1;
        }

        function OnClearPatient() {
            alert('hi5');
            var obj = document.getElementById("SigPlusDoctor");
            obj.ClearTablet(); //Clears the signature, in case of error or mistake     
            obj.TabletState = 1;
            //document.FORM1.SigPlusDoctor.TabletState = 0; 
        }


        function OnSign() {
            alert('hi');
            var obj = document.getElementById("SigPlusDoctor");
            obj.TabletState = 1;
            //document.FORM1.SigPlusDoctor.TabletState = 1; //Turns tablet on
        }

        function OnSignPatient() {
            alert('hi1');
            var obj = document.getElementById("SigPlusDoctor");
            obj.TabletState = 1;
            //document.FORM1.SigPlusDoctor.TabletState = 0; 
        }


        function OnSave() {
            alert('hi2');
            var obj = document.getElementById("SigPlusDoctor");
            alert('hi3');
            //if(document.FORM1.SigPlusDoctor.NumberOfTabletPoints > 0)
            if (obj.NumberOfTabletPoints > 0) {
                obj.TabletState = 0; //Turns tablet off 
                obj.AutoKeyStart();

                obj.AutoKeyFinish();
                obj.EncryptionMode = 2;
                obj.SigCompressionMode = 1;

                var hdoctor = document.getElementById("hidden");
                alert(hdoctor);
                hdoctor.value = obj.SigString;
                }
            else {
                alert("Please Sign Before Continuing...");
                return false;
            }


        }
        function ClosePopup() {
            parent.document.getElementById('divPatientSignature').style.visibility = 'hidden';
            parent.document.getElementById('divPatientSignature').style.zIndex = -1;
            parent.document.getElementById('divDocSignature').style.visibility = 'hidden';
            parent.document.getElementById('divDocSignature').style.zIndex = -1;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <table width="100%">
        <tr>
            <td style="width: 414px" align="center">
                <UserMessage:MessageControl ID="usrMessage" runat="server"></UserMessage:MessageControl>
            </td>
        </tr>
    </table>

    <dx:ASPxPageControl ID="carTabPage" runat="server" ActiveTabIndex="1" EnableHierarchyRecreation="True"
        Width="100%" AutoPostBack="true" >
        <TabPages>
            <dx:TabPage Text="Sign with sign pad" TabStyle-Width="100%">
                <TabStyle Width="100%">
                </TabStyle>
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl1" runat="server">
                        
                         <table border="1" cellpadding="0" align="center">
                            <tr>
                                <td>
                                    Patient Signature..
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <object id="SigPlusDoctor" style="left: 0px; width: 329px; top: 0px; height: 180px"
                                        height="75" classid="clsid:69A40DA3-4D42-11D0-86B0-0000C025864A" name="SigPlusDoctor">
                                        <param name="_Version" value="131095"/>
                                        <param name="_ExtentX" value="8467"/>
                                        <param name="_ExtentY" value="4763"/>
                                        <param name="_StockProps" value="9"/>
                                    </object>
                                </td>
                            </tr>
                        </table>
                         <table align="center" width="70%">
                            <tr>
                                <td style="width: 61px">
                                    <input id="btnSign" onclick="OnSign()" type="button" value="Sign" name="SignBtn"
                                        style="width: 58px; height: 24px"/>
                                </td>
                                <td style="width: 57px">
                                    <input id="btnClear" onclick="OnClear()" type="button" value="Clear" name="ClearBtn"
                                        style="height: 24px; width: 58px;"/>
                                </td>
                                <td style="width: 22%">
                                </td>
                                <td style="width: 82px">
                                    <input id="hidden" type="hidden" name="hidden" style="width: 132px"/>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="4">
                                    <asp:CheckBox ID="chkShow" text="Show sign after submit" runat="server" />
                                        <%--<input id="chkShow" type="checkbox"  value="show" />Show sign after submit--%>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Button id="btnSubmit" 
                                        OnClientClick="OnSave()"
                                        type="button" 
                                        value="Submit"
                                        Text="Submit" 
                                        name="Submit"
                                        style="width: 58px" runat="server" OnClick="btnSubmit_Click" />
                                   

                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="3">
                                    <asp:Label ID="lblMsg" runat="server" Text="Label" ForeColor="Red" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    <asp:Image runat="server" ID="imagepatient" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Text="Sign with cursor" TabStyle-Width="100%">
                <TabStyle Width="100%">
                </TabStyle>
                <ContentCollection>
                    <dx:ContentControl ID="ContentControl2" runat="server">
                        
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    </div>
    </form>
</body>
</html>
