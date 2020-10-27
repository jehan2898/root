<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineLogDetails.aspx.cs" Inherits="AJAX_Pages_OnlineLogDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
            font-family:Arial;
            font-size:12px;
        }

        th, td {
            text-align: left;
            padding: 8px;
        }

        tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        th {
            background-color: #4CAF50;
            color: white;
        }
    </style>
    <script type="text/javascript" src="../js/jquery-1.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:100%">
            <asp:Repeater ID="rptOnlineLog" runat="server">
                <HeaderTemplate>
                    <table class="table" style="margin: 0px; width: 100%;">
                        <thead>
                            <tr>
                                <th>Type</th>
                                <th>Text</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr onmouseover="this.style.backgroundColor='lightgrey'" onmouseout="this.style.backgroundColor=''">
                        <td style="width:20%"><%# Eval("Type")%></td>
                        <td style="width:80%;text-wrap:normal"><%# Eval("Text") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
