<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BroastCastMessageWebform.aspx.cs" Inherits="BroastCastMessageProject.BroastCastMessageWebform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox TextMode="MultiLine" ID="txtMessage" runat="server" Height="85px" Width="410px"></asp:TextBox>
            <br />
            <asp:Button ID="btnSend" runat="server" Text="Gởi" Width="71px" OnClick="btnSend_Click" />
            <br />
            <asp:Label ID="lblNotification" runat="server" Text="" style="font-weight: 700; color: #FF0000"></asp:Label>
        </div>
    </form>
</body>
</html>
