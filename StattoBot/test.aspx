<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="StattoBot.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Fuzzy matching algorithms</h1>
        <br /><br /><br />
        <asp:Label ID="SourceLabel" runat="server" Text="Source: "></asp:Label>
        <asp:TextBox ID="SourceTextBox" runat="server"></asp:TextBox>
        <br /><br /><br />
        <asp:Button ID="SubmitButton" runat="server" Text="Fuzzy Match" OnClick="Button1_Click" />

        <br /><br /><br />
        <asp:Label ID="MatchResult" runat="server" Text=""></asp:Label>
        <br /><br /><br />
        <asp:Label ID="ResultLabel" runat="server" Text=""></asp:Label>

    </div>
    </form>
</body>
</html>
