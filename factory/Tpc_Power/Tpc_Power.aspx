<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tpc_Power.aspx.cs" Inherits="factory.Tpc_Power.Tpc_Power" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>台電用電量</title>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>

    <style type="text/css">
        /*height: calc(100vh - 580px);*/
        
        .GV1 table tbody {
            display:block;
            height: calc(76vh);
            overflow-y: overlay;
        }
        
        .GV1 table thead, .GV1 tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }
        
        .GV1 table thead {
            width: calc( 200% – 1em )
        }
        
        .auto-style1 {
            font-size: large;
        }

        .mar-r {
            margin-right:10px;
        }

        #ContentPlaceHolder1_imgb_p:hover,#ContentPlaceHolder1_imgb_n:hover,#ContentPlaceHolder1_imgb_excel:hover{
            background:gainsboro;
        }

        .auto-style2 {
            font-size: large;
            color: #FF3300;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
        <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
            <strong>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">台電電量</asp:Label>
            </strong> 
        </div>
        <div class="row">
            <div id="name" class="col-auto">
                <strong>
                    <asp:DropDownList runat="server" ID="ddl_f" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="ddl_f_SelectedIndexChanged"></asp:DropDownList>
                </strong>
            </div>
            <div class="col-auto">
                <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" CssClass="mar-r" OnTextChanged="tb_SDATE_TextChanged" ></asp:TextBox>
                <asp:DropDownList ID="ddl_t" runat="server"  CssClass="mar-r" Height="30px" Width="130px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddl_t_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一個月" CssClass="mar-r" OnClick="imgb_p_Click"  />
                <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一個月" CssClass="mar-r" OnClick="imgb_n_Click" />
                <asp:ImageButton ID="imgb_excel" runat="server" ImageUrl="~/img/excel.png" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="匯出EXCEL" OnClick="imgb_excel_Click" />
            </div>
        </div>
        </div>

        <div class="row" style="margin-top:10px">
            <div class="col-12"> 
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M0" runat="server" class="nav-link" >每月</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M1" runat="server" class="nav-link" >每日</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_M2" runat="server" class="nav-link" >每15分鐘</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>

        <div class="GV1 col-xxl-8 table-responsive" style="padding:0px">
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataSourceID="SDS1">
                <Columns>
                    <asp:BoundField DataField="FactoryName" HeaderText="廠區" ReadOnly="True" SortExpression="FactoryName" />
                    <asp:BoundField DataField="DTime" HeaderText="時間" SortExpression="DTime" DataFormatString="{0:MM/dd}" />
                    <asp:BoundField DataField="半尖峰時段" HeaderText="半尖峰時段" ReadOnly="True" SortExpression="半尖峰時段" NullDisplayText="0" />
                    <asp:BoundField DataField="尖峰時段" HeaderText="尖峰時段" ReadOnly="True" SortExpression="尖峰時段" NullDisplayText="0" />
                    <asp:BoundField DataField="週六半尖峰時段" HeaderText="週六半尖峰時段" ReadOnly="True" SortExpression="週六半尖峰時段" NullDisplayText="0" />
                    <asp:BoundField DataField="離峰時段" HeaderText="離峰時段" ReadOnly="True" SortExpression="離峰時段" NullDisplayText="0" />
                </Columns>
                <EmptyDataTemplate>
                    <strong>
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="無資料!"></asp:Label>
                    </strong>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT TOP 1 *
FROM (
	SELECT F.FactoryName+'廠' as FactoryName, G.DTime, G.Power_KW, G.Electricity_period
	FROM G_TPC_D AS G
	LEFT JOIN Factory AS F ON G.FactoryID = F.FactoryID
	WHERE DTime &gt;='2021-09-01' AND DTime &lt;= '2021-09-30'
) t 
PIVOT (
	MAX(Power_KW) 
	FOR Electricity_period IN ([半尖峰時段], [尖峰時段], [週六半尖峰時段],[離峰時段])
) p">
            </asp:SqlDataSource>
            </div>
        </div>

        <script>
            //判斷是否為手機
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                //手機導覽列
                document.getElementById("phone").style.display = "";
                //document.getElementById("name").style.display = "none";
                var d = document.getElementById('ContentPlaceHolder1_GV1');
                d.style.width = "120%";
            }
        </script>
        <script>
            function loadJs() {
                var head = $("head").remove("script[role='reload']");
                $("<scri" + "pt>" + "</scr" + "ipt>").attr({ role: 'reload', src: '../js/jquery.ui.monthpicker.js', type: 'text/javascript' }).appendTo(head);
            }
            var getUrlString = location.href; //獲取當前網址
            var url = new URL(getUrlString);  //取得GET
            var T = url.searchParams.get('T');
            if (T == "D") {
                loadJs();
                $(function () {
                    $("#<%=tb_SDATE.ClientID%>").monthpicker({
                    prevText: "&#x3C;上一年",
                    nextText: "下一年&#x3E;",
                });
            });
        } else {
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
            }
            if (T == "Y") {
                var h = document.getElementsByTagName('tbody')[0];
                h.style.height = "427px";
            }
        </script>
</asp:Content>
