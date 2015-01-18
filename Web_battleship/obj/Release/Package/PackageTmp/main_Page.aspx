<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_Page.aspx.cs" Inherits="Web_battleship.WebForm1" EnableSessionState="True" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <p>
        <br />
        Input your nick name for enter:</p>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button_enter" runat="server" OnClick="Button1_Click" Text="Enter" />
    
    </div>
    </form>
</body>
</html>
