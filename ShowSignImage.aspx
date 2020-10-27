<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowSignImage.aspx.cs" Inherits="ShowSignImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
     
    <div>
    <table width="25%">
    <tr>
        <td>
            <asp:Label ID="lblProviderSign"  Text="Provider Sign" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblPatientSign"  Text="Patient Sign" runat="server"></asp:Label>
        </td> 
    </tr>
        <tr>
        <td>
     <asp:Image runat="server" ID="DoctorImage" />       
           
            
        </td>
        <td>
            <asp:Image runat="server" ID="imagepatient"  />
        </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
