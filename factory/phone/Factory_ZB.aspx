<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Factory_ZB.aspx.cs" Inherits="factory.phone.Factory_ZB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>彰濱廠選單</title>
        <script src="../js/jquery-3.5.1.slim.min.js"></script>
        <style>
            .c {
                background:aliceblue;
                border:1px solid rgba(0, 0, 0, 0.125);
            }
            .auto-style1 {
                font-size: large;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container-fluid">
            <div id="phone" style="background:antiquewhite;height:35px;padding-top:4px">
                <strong>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                    <asp:HyperLink ID="HyperLink2" runat="server" ForeColor="Maroon" CssClass="auto-style1">彰濱廠</asp:HyperLink>
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:Label>
                </strong> 
            </div>
        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/power/Power_H.aspx?F=ZB-T1HIST';"> 
                <strong> 
                    <asp:Label ID="Label1" runat="server" Text="日用電量"></asp:Label>
                </strong>
            </div>
            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/factory_index/index.aspx?F=ZB-T1HIST';"> 
                <strong> 
                    <asp:Label ID="Label2" runat="server" Text="月用電量"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/Mill/Mill_report.aspx?F=ZB-T1HIST&M=1&B=Power';"> 
                <strong> 
                    <asp:Label ID="Label5" runat="server" Text="磨機日報#1#2"></asp:Label>
                </strong>
            </div>

            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/Mill/Mill_report.aspx?F=ZB-T1HIST&M=3&B=Power';"> 
                <strong> 
                    <asp:Label ID="Label6" runat="server" Text="磨機日報#3#4"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/Mill/Mill_report.aspx?F=ZB-T1HIST&M=5&B=Power';"> 
                <strong> 
                    <asp:Label ID="Label7" runat="server" Text="磨機日報#5#6"></asp:Label>
                </strong>
            </div>

            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/Mill/Mill_report.aspx?F=ZB-T1HIST&M=7&B=Power';"> 
                <strong> 
                    <asp:Label ID="Label8" runat="server" Text="磨機日報#7#8"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding:25px 0px 0px 0px" onclick="location.href='/Mill/Mill_cycle.aspx?F=ZB-T1HIST&M=1';"> 
                <strong> 
                    <asp:Label ID="Label11" runat="server" Text="循環提運機#1#2"></asp:Label>
                </strong>
            </div>

            <div class="c col-5" style="margin-left:10px;padding:25px 0px 0px 0px" onclick="location.href='/Mill/Mill_cycle.aspx?F=ZB-T1HIST&M=3';"> 
                <strong> 
                    <asp:Label ID="Label12" runat="server" Text="循環提運機#3#4"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding:25px 0px 0px 0px" onclick="location.href='/Mill/Mill_cycle.aspx?F=ZB-T1HIST&M=5';"> 
                <strong> 
                    <asp:Label ID="Label13" runat="server" Text="循環提運機#5#6"></asp:Label>
                </strong>
            </div>

            <div class="c col-5" style="margin-left:10px;padding:25px 0px 0px 0px" onclick="location.href='/Mill/Mill_cycle.aspx?F=ZB-T1HIST&M=7';"> 
                <strong> 
                    <asp:Label ID="Label14" runat="server" Text="循環提運機#7#8"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/Check/Check_data.aspx?F=KY&M=0';"> 
                <strong> 
                    <asp:Label ID="Label3" runat="server" Text="檢測數據"></asp:Label>
                </strong>
            </div>

            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/Check/add_data.aspx?F=KY';">
                <strong> 
                    <asp:Label ID="Label4" runat="server" Text="新增檢測數據"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/PV/PV_H.aspx?F=ZB-T1HIST';"> 
                <strong> 
                    <asp:Label ID="Label9" runat="server" Text="日太陽能-線西"></asp:Label>
                </strong>
            </div>

            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/PV/PV_D.aspx?F=ZB-T1HIST';"> 
                <strong> 
                    <asp:Label ID="Label10" runat="server" Text="月太陽能-線西"></asp:Label>
                </strong>
            </div>
        </div>

        <div class="row justify-content-center text-center" style="margin:10px 0px 20px 0px;height:80px">
            <div class="c col-5" style="margin-right:10px;padding-top:25px" onclick="location.href='/PV/PV_H.aspx?F=ZB-T1HIST&M=2';"> 
                <strong> 
                    <asp:Label ID="Label15" runat="server" Text="日太陽能-伸港"></asp:Label>
                </strong>
            </div>

            <div class="c col-5" style="margin-left:10px;padding-top:25px" onclick="location.href='/PV/PV_D.aspx?F=ZB-T1HIST&M=2';"> 
                <strong> 
                    <asp:Label ID="Label16" runat="server" Text="月太陽能-伸港"></asp:Label>
                </strong>
            </div>
        </div>

    </div>
</asp:Content>
