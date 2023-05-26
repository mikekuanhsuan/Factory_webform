<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="experiment_data.aspx.cs" Inherits="factory.Check.experiment_data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>實驗數據</title>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <script>
            $(function () {
                $("#<%=tb_SDATE.ClientID%>").datepicker({
                closeText: "關閉",
                prevText: "&#x3C;上個月",
                nextText: "下個月&#x3E;",
                currentText: "今天",
                monthNames: ["一月", "二月", "三月", "四月", "五月", "六月",
                    "七月", "八月", "九月", "十月", "十一月", "十二月"],
                monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月",
                    "七月", "八月", "九月", "十月", "十一月", "十二月"],
                dayNames: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
                dayNamesShort: ["週日", "週一", "週二", "週三", "週四", "週五", "週六"],
                dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
                weekHeader: "週",
                dateFormat: "yy-mm-dd",
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: true,
                yearSuffix: "年",
            });
        });
    </script>
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
        #ContentPlaceHolder1_imgb_p:hover,#ContentPlaceHolder1_imgb_n:hover{
            background:gainsboro;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
            <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
                <strong>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                    <asp:HyperLink ID="hyl_factory" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:HyperLink>
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">實驗室/品管檢測</asp:Label>
                </strong> 
            </div>
            <div class="row">
                <div class="col-auto" id="name">
                    <strong>
                        <asp:DropDownList ID="DDL_factory" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="DDL_factory_SelectedIndexChanged"></asp:DropDownList>
                    </strong>
                </div>
                <div class="col-auto">
                    <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" CssClass="mar-r" OnTextChanged="tb_SDATE_TextChanged"  ></asp:TextBox>
                    <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一天" CssClass="mar-r" OnClick="imgb_p_Click"  />
                    <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一天" CssClass="mar-r" OnClick="imgb_n_Click"  />
                </div>
                <div class="col-auto">
                    <asp:Button ID="btn_add" runat="server" Class="btn btn-success" Text="新增實驗數據" Style="padding-top:3px;padding-bottom:3px" OnClick="btn_add_Click" />
                </div>
            </div>
        </div>

        <div runat="server" id="bookmark" class="row" style="margin-top:10px">
            <div class="col-12"> 
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M0" runat="server" class="nav-link">全部</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M1" runat="server" class="nav-link"></asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M2" runat="server" class="nav-link"></asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M3" runat="server" class="nav-link"></asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M4" runat="server" class="nav-link"></asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>
        <div class="row">
            <div class="col-xl-6 table-responsive">
                <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataSourceID="SDS1" AllowSorting="True" OnSorting="GV1_Sorting">
                    <Columns>
                        <asp:BoundField DataField="DateTime" DataFormatString="{0:HH:mm}" HeaderText="時間" SortExpression="DateTime" />
                        <asp:BoundField DataField="Mill_ID" HeaderText="磨機" SortExpression="Mill_ID" />
                        <asp:BoundField DataField="ProductName" HeaderText="成品" SortExpression="ProductName" />
                        <asp:BoundField DataField="Moisture" HeaderText="水份%" SortExpression="Moisture" />
                        <asp:BoundField DataField="Specific_Surface" HeaderText="比表面積㎡/kg" SortExpression="Specific_Surface" DataFormatString="{0:N0}" />
                        <asp:BoundField DataField="Residual_On_Sieve" HeaderText="篩餘%" SortExpression="Residual_On_Sieve" />
                    </Columns>
                    <EmptyDataTemplate>
                        <strong>
                        <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="無資料!" style="font-size: large; color: #FF3300"></asp:Label>
                        </strong>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve, Q.Visible FROM A_Product_Quality2 AS Q LEFT OUTER JOIN A_Product AS P ON Q.ProductID = P.ProductID"></asp:SqlDataSource>
            </div>
        </div>
    </div>
    <script>
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            document.getElementById("phone").style.display = "";
        }
    </script>
</asp:Content>
