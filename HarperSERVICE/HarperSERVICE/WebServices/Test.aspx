<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="MemberServices.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="386px" TextMode="MultiLine" 
            Width="1022px"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
