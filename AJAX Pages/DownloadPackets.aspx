<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DownloadPackets.aspx.cs" enableEventValidation="false" Inherits="AJAX_Pages_DownloadPackets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../packages/bootstrap.3.3.7/content/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../packages/bootstrap.3.3.7/content/Scripts/bootstrap.min.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:TextBox ID="txtCompanyId" runat="server" Visible="false"></asp:TextBox>
    <table width="100%">
        <tr align="right"><td>
            <asp:LinkButton ID="btnRefresh" runat="server" OnClick="BtnRefresh_ServerClick" CssClass="btn btn-primary" Text='Refresh' style="color:white;">Refresh</asp:LinkButton>
            </td></tr>
          <tr><td>
              <div class="panel panel-primary">All packets   

<%--<asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Always">

            <ContentTemplate>--%>
                              <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" PagerStyle-CssClass="pager"
 HeaderStyle-CssClass="header" RowStyle-CssClass="rows" OnRowDataBound="gvData_RowDataBound" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" Width="100%">
        <Columns>
                        <asp:TemplateField HeaderText="PacketID">
                    <ItemTemplate>
                        <asp:HiddenField ID="hidPacketID" runat="server" Value='<%#Eval("I_PACKET_REQUEST_ID") %>' />
                        <asp:LinkButton ID="btnBills" runat="server" OnClick="btnBills_Click" Text='<%#Eval("I_PACKET_REQUEST_ID") %>' style="color:blue;">PacketID</asp:LinkButton>
                    </ItemTemplate>
					<ItemStyle Width="11%"></ItemStyle>
                </asp:TemplateField>
            <asp:BoundField DataField="SZ_PROCEDURE_GROUP" HeaderText="Speciality" >  <ItemStyle Width="11%"></ItemStyle></asp:BoundField>
            <asp:BoundField DataField="BillCount" HeaderText="Total Bills"> <ItemStyle Width="11%"></ItemStyle></asp:BoundField>
            <asp:BoundField DataField="I_JOB_ID" HeaderText="JobID" >  <ItemStyle Width="11%"></ItemStyle></asp:BoundField>
            <asp:BoundField DataField="CreatedAt" HeaderText="CreatedAt" >  <ItemStyle Width="11%"></ItemStyle></asp:BoundField>
            <asp:BoundField DataField="StateName" HeaderText="Job Status" ><ItemStyle Width="11%"></ItemStyle></asp:BoundField>
            <asp:TemplateField HeaderText="Download Packets">
                  <ItemStyle Width="11%"></ItemStyle>
                    <ItemTemplate>
                        <asp:HiddenField ID="hidFilePath" runat="server" Value='<%#Eval("DownloadLink") %>' />
                        <asp:Button runat="server" ID="btnDownLoad" Text="Download" CssClass="btn btn-success" OnClick="btnDownLoad_Click" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Re Packeting">
                                        <ItemStyle Width="11%"></ItemStyle>
                    <ItemTemplate>

                        <asp:HiddenField ID="hdRequeue" runat="server" Value='<%#Eval("I_JOB_ID") %>' />
                        <asp:Button runat="server" ID="btnRequeue" Text="Re-Packet" OnClick="btnRequeue_Click" CssClass="btn btn-primary" Enabled="false" />
                    </ItemTemplate>
 </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                                       <asp:HiddenField ID="hdEnvelop" runat="server" Value='<%#Eval("SZ_PROCEDURE_GROUP_ID") %>' />
                      <asp:HiddenField ID="hdnCompanyID" runat="server" Value='<%#Eval("SZ_COMPANY_ID") %>' />
                            <div class="dropdown">
    <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">Actions
    <span class="caret"></span></button>
    <ul class="dropdown-menu" style="width:120px;">
      <li style="width:120px;"> <asp:Button runat="server" ID="btnEnvelop" Text="Print Envelope" OnClick="btnPrintEnvelop_Click"  CssClass="btn btn-primary"  Enabled="true" Width="120px" /></li>
      <li style="width:120px;"> <asp:Button runat="server" POMID='<%#Eval("POM_ID") %>' ID="btnPOM" Text="Print POM" OnClick="btnPrintPOM_Click"  CssClass="btn btn-warning" Enabled="true" Width="120px" /></li>
        <li style="width:120px;"> <asp:Button runat="server" ID="btnErrorLog" Text="Error Log"  OnClick="btnPacketDetails_Click" CssClass="btn btn-danger" Enabled="true" Width="120px"/></li>
    </ul>
  </div>
                    </ItemTemplate>
					<ItemStyle Width="11%"></ItemStyle>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
<%--            </ContentTemplate>
                                    <Triggers>

                                </Triggers>
        </asp:UpdatePanel>--%>


              </div>
                  </td></tr>
    </table>
        <div id="div1" style="position: absolute; left: 50%; top: 920px; width: 30%; height: 150px; background-color: #DBE6FA; visibility: hidden; border-right: silver 2px solid;
        border-top: silver 2px solid; border-left: silver 2px solid; border-bottom: silver 2px solid;
        text-align: center;">
        <div style="position: relative; width: 40%; height: 20px; text-align: left; float: left;
            font-family: Times New Roman; float: left; background-color: #8babe4; left: 0px;
            top: 0px;">
            Is this the final POM
        </div>
        <br />
        <br />
        <div style="top: 50px; width: 90%; font-family: Times New Roman; text-align: center;">
            <span id="Span2" runat="server"></span>
        </div>
        <br />
        <br />
        <div style="text-align: center;">
            <asp:button id="btnYes" runat="server" cssclass="Buttons"
                text="Yes" width="80px" />
            <asp:button id="btnNo" runat="server" cssclass="Buttons" text="No"
                width="80px" />
        </div>
    </div>
  <asp:HiddenField ID="hdnPOMValue" runat="server" />  
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
<script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
<link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
    rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function ConfirmSave() {
        var Ok = confirm('Are you sure want to Repacket?');
        if (Ok)
            return true;
        else
            return false;
    }

    function ShowPopup(message) {
        $(function () {
            $("#dialog").html(message);
            $("#dialog").dialog({
                title: "Bills",
                buttons: {
                    Close: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
        });
    };

    function POMConformation() {
        document.getElementById('div1').style.zIndex = 1;
        document.getElementById('div1').style.position = 'absolute';
        document.getElementById('div1').style.left = '360px';
        document.getElementById('div1').style.top = '250px';
        document.getElementById('div1').style.visibility = 'visible';
        return false;
    }
    function myFunction(sender) {
        if (sender.getAttribute('pomid').trim() == '') {
            var x;
            if (confirm("Is this Final POM?!") == true) {
                document.getElementById("<%= hdnPOMValue.ClientID%>").value = "Yes";
            } else {
                document.getElementById("<%= hdnPOMValue.ClientID%>").value = "No";
            }
            document.getElementById("demo").innerHTML = x
        }
    }
    function confirm_check() {
        return true;
    }
            function YesMassage()
        {         
        //document.getElementById("<%= hdnPOMValue.ClientID%>").value="Yes";           
                document.getElementById('div1').style.visibility = 'hidden';
                return true;
        }
        function NoMassage()
        {        
        //document.getElementById("<%= hdnPOMValue.ClientID%>").value="No";         
            document.getElementById('div1').style.visibility = 'hidden';
            return true;
        }

</script>
<script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
<div id="dialog" style="display: none"></div>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">


    <style>
        .dropdown-menu {
  width: 100%; 
}

.btn{
 width: 100%;
}
        ol {
    padding: 20px;
}

ul {
    padding: 20px;
}

ol li {
    padding: 5px;
    margin-left: 35px;
}

ul li {
    margin: 5px;
}

        .glyphicon {
    font-size: 30px;
}
        .mydatagrid
{
	width: 100%;
	border: solid 2px black;
	min-width: 100%;
}
.header
{
	background-color: #B4DD82;
	font-family: Arial;
	color: black;
	border: none 0px transparent;
	height: 20px;
	text-align: center;
	font-size: 12px;
}
/* CSS to change the GridLines color */
.mydatagrid, .mydatagrid th, .Grid td
{
    border:1px solid green;
}
.rows
{
	background-color: #fff;
	font-family: Arial;
	font-size: 12px;
	color: #000;
	min-height: 25px;
	text-align: left;
}
.rows:hover
{
	background-color: #EEFCFC;
	font-family: Arial;
	color: black;
	text-align: left;
}
.selectedrow
{
	background-color: #ff8000;
	font-family: Arial;
	color: red;
	font-weight: bold;
	text-align: left;
}
.mydatagrid a /** FOR THE PAGING ICONS  **/
{
	background-color: Transparent;
	padding: 5px 5px 5px 5px;
	color: #fff;
	text-decoration: none;
	font-weight: bold;
}

.mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/
{
	color: #fff;
}

.pager
{
	background-color: #646464;
	font-family: Arial;
	color: White;
	height: 30px;
	text-align: left;
}

.mydatagrid td
{
	padding: 5px;
}
.mydatagrid th
{
	padding: 5px;
}


    </style>
    <style type="text/css">
.table.hovertable {
	font-family: verdana,arial,sans-serif;
	font-size:11px;
	color:#333333;
	border-width: 1px;
	border-color: #999999;
	border-collapse: collapse;
}
.table.hovertable th {
	background-color:#c3dde0;
	border-width: 1px;
	padding: 8px;
	border-style: solid;
	border-color: #a9c6c9;
}
.table.hovertable tr {
	background-color:#d4e3e5;
}
.table.hovertable td {
	border-width: 1px;
	padding: 8px;
	border-style: solid;
	border-color: #a9c6c9;
}
</style>

	<link href="../dist/styles/metro/notify-metro.css" rel="stylesheet" />
	<script src="../dist/notify.js"></script>
	<script src="../dist/styles/metro/notify-metro.js"></script>
    <script>
        function ShowMessage(message,title,style) {
            $.notify({
                title: title,
                text: message,

                image: "<img src='../images/Mail.png'/>"
            }, {
                style: 'metro',
                className: style,
                autoHide: false,
                clickToHide: true
            });
        }
    </script>
</asp:Content>

