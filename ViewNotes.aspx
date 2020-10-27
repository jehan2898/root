<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewNotes.aspx.cs" Inherits="ViewNotes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function showpopup(path)
        {
          //  alert(path);
            window.location.href = path;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
