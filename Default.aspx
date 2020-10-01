<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PayamDowlat.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

        </div>
        <asp:FileUpload ID="FUPayamDowlat" runat="server" />
        <asp:GridView ID="DGVpayamDowlat" runat="server">
        </asp:GridView>
        <asp:Button ID="BtnSend" runat="server" OnClick="BtnSend_Click" style="direction: ltr" Text="Send" />
    </form>
</body>
</html>
