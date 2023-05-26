<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="factory.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>登入</title>
    <link rel="icon" href="img/companyicon.png" type="image/x-icon" />
    <link href="css/bootstrap5.0.min.css" rel="stylesheet" integrity="sha384-giJF6kkoqNQ00vy+HMDP7azOuL0xtbfIcaT9wjKHr8RbDVddVHyTfAAsrekwKmP1" crossorigin="anonymous">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="theme-color" content="#7952b3">
    <link href="css/signin.css" rel="stylesheet">
</head>
<body class="text-center">
    <main class="form-signin">
        <form id="form1" runat="server">
            <img class="mb-4" src="img/Company.jpg" alt="" width="300" height="57">
            <div class="form-floating">
                <asp:TextBox ID="tb_act" runat="server" class="form-control" placeholder="account" required ></asp:TextBox>
                <label for="account">請輸入帳號:</label>
            </div>
            <div class="form-floating">
                <asp:TextBox ID="tb_pwd" runat="server" class="form-control" placeholder="Password" required TextMode="Password"></asp:TextBox>
                <label for="password">請輸入密碼:</label>
            </div>
            <div>
                <asp:CheckBox ID="cb_act" runat="server" Text="記住帳號" />
                <asp:Label ID="lb_error" runat="server" ForeColor="Red"></asp:Label>
            </div>
            <asp:Button ID="btn_login" runat="server" Text="登入" class="w-100 btn btn-lg btn-primary" style="margin-top:10px" OnClick="btn_login_Click"/>
        </form>
    </main>
</body>
</html>
