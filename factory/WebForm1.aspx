<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="factory.WebForm1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <br>
                車牌號碼: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br>
            <br>
                車道:
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>1號車道</asp:ListItem>
                <asp:ListItem>2號車道</asp:ListItem>
            </asp:DropDownList>
            <br>
            <br>
                公噸: <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br>
            <br>
            <asp:Button ID="Button1" runat="server" Text="儲存" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
<script>
</script>
