<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ErrorMessageControl.ascx.cs" Inherits="UserControl_ErrorMessageControl" %>

<div runat="server" id="divOuterMessage">
    <div id="divErrorControl" style="text-align:center;font-family:Arial;font-size:14px;padding-bottom:10px;" runat="server">
        <img id="imgMessage" runat="server" src="" />
        <asp:Label ID="lblUsrMessage" runat = "server" >
        </asp:Label>
    </div>    
</div>
<div id="divDummyArea" runat="server">
    &nbsp;
</div>