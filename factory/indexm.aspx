<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="indexm.aspx.cs" Inherits="factory.indexm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>選單</title>
        <script src="js/jquery-3.5.1.slim.min.js"></script>
        <style>
            .c {
                background:aliceblue;
                border:1px solid rgba(0, 0, 0, 0.125);
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/phone/Factory_KY.aspx';"> 
                <strong> 
                    <asp:Label ID="Label1" runat="server" Text="觀音廠"></asp:Label>
                </strong>
            </div>
            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/phone/Factory_BL.aspx';"> 
                <strong> 
                    <asp:Label ID="Label2" runat="server" Text="八里廠"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/phone/Factory_QX.aspx';"> 
                <strong> 
                    <asp:Label ID="Label3" runat="server" Text="全興廠"></asp:Label>
                </strong>
            </div>
            
            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/phone/Factory_ZB.aspx';"> 
                <strong> 
                    <asp:Label ID="Label4" runat="server" Text="彰濱廠"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/phone/Factory_KH.aspx';"> 
                <strong> 
                    <asp:Label ID="Label5" runat="server" Text="高雄廠"></asp:Label>
                </strong>
            </div>
            
            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/phone/Factory_LD.aspx';"> 
                <strong> 
                    <asp:Label ID="Label6" runat="server" Text="龍德廠"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/phone/Factory_LZ.aspx';"> 
                <strong> 
                    <asp:Label ID="Label7" runat="server" Text="利澤廠"></asp:Label>
                </strong>
            </div>
            
            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/phone/Factory_HL.aspx';"> 
                <strong> 
                    <asp:Label ID="Label8" runat="server" Text="花蓮廠"></asp:Label>
                </strong>
            </div>
        </div>
    </div>
</asp:Content>
