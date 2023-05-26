<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Check_data.aspx.cs" Inherits="factory.Check.Check_data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>檢測數據</title>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <script src="../js/jquery.ui.monthpicker.js"></script>
    <script src="../js/moment.min.js"></script>
    <script src="../js/Chart.min.js"></script>
    <script src="../js/hammerjs@2.0.8.js"></script>  
    <script src="../js/chartjs-plugin-zoom.js"></script>
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

            $(function () {
                $("#<%=tb_EDATE.ClientID%>").datepicker({
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
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">中控室檢測</asp:Label>
                </strong> 
            </div>
            <div class="row">
                <div class="col-auto" id="name">
                    <strong>
                        <asp:DropDownList ID="DDL_factory" runat="server" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="DDL_factory_SelectedIndexChanged"></asp:DropDownList>
                    </strong>
                </div>
                <div class="col-auto">
                    開始:
                    <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" CssClass="mar-r" OnTextChanged="tb_SDATE_TextChanged" ></asp:TextBox>
                    結束:
                    <asp:TextBox ID="tb_EDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" CssClass="mar-r" OnTextChanged="tb_EDATE_TextChanged"></asp:TextBox>
                    <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一天" CssClass="mar-r" OnClick="imgb_p_Click" />
                    <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一天" CssClass="mar-r" OnClick="imgb_n_Click" />
                </div>
                <div class="col-auto">
                    <asp:Button ID="btn_add" runat="server" Class="btn btn-primary" Text="新增檢測數據" Style="padding-top:3px;padding-bottom:3px" OnClick="btn_add_Click" />
                </div>
            </div>
        </div>

       <div class="row" style="margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <canvas id="myAreaChart" width="100%" height="22"></canvas>
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
                        <asp:BoundField DataField="Specific_Surface" HeaderText="比表面積㎡/kg" SortExpression="Specific_Surface" />
                        <asp:BoundField DataField="Residual_On_Sieve" HeaderText="篩餘%" SortExpression="Residual_On_Sieve" />
                        <asp:BoundField DataField="Visible" HeaderText="最新一筆" SortExpression="Visible" />
                    </Columns>
                    <EmptyDataTemplate>
                        <strong>
                        <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="無資料!" style="font-size: large; color: #FF3300"></asp:Label>
                        </strong>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve, Q.Visible FROM A_Product_Quality AS Q LEFT OUTER JOIN A_Product AS P ON Q.ProductID = P.ProductID"></asp:SqlDataSource>
            </div>
        </div>
    </div>
    <script>
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            document.getElementById("phone").style.display = "";
        }
    </script>
    <script>
        var chart = <%=chart()%>;
        console.log(chart);
        var ctx = document.getElementById("myAreaChart");
        var myLineChart = new Chart(ctx, {
            type: 'line',
            data: {
                datasets: [
                    //資料一律由外部新增的方式
                ],
            },
            options: {
                elements: {
                    point: {
                        radius: 0
                    }
                },
                responsive: true,
                scales: {
                    xAxes: [{
                        barPercentage: 0.4,
                        type: 'time',
                        time: {
                            unit: 'hour',
                            displayFormats: {
                                hour: 'MM-DD HH:mm'
                            },
                            format: "YYYY-MM-DD HH:mm",
                            min: chart[3][0].min,
                            max: chart[4][0].max,
                            //stepSize:1,
                        },
                        gridLines: {
                            display: false,
                        },
                    }],
                },
                legend: { //顯示各標題並且可隱藏
                    display: true,
                    position: 'bottom',
                    labels: {
                        boxWidth: 20,
                        boxHeight: 80,
                        fontStyle: 'bold',
                    }
                },
                tooltips: { //滑鼠移到點上顯示資訊
                    enabled: true,
                    intersect: false,
                    mode: 'index',
                },
                /*
                hover: {
                    mode: 'x',
                    intersect: false
                },
                */
                plugins: {
                    zoom: {
                        pan: { //平移功能
                            enabled: false,
                            mode: 'xy',
                        },
                        zoom: { //縮放功能
                            enabled: false,
                            mode: 'xy',
                        }
                    }
                }
            }
        });
        if (chart[0].length > 0)
        {
            const newDataSet = {
                type: 'line',
                label: '水份%',
                lineTension: 0.3, //曲線幅度
                backgroundColor: 'blue', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
                borderColor: 'blue', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
                data: chart[0],
                fill: false, //背景是否要填滿
                //pointStyle: 'line',
                pointRadius: 3
            }
            myLineChart.data.datasets.push(newDataSet);
        }
        const newDataSet1 = {
            type: 'line',
            label: '比表面積㎡/kg',
            lineTension: 0.3, //曲線幅度
            backgroundColor: 'red', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
            borderColor: 'red', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
            data: chart[1],
            fill: false, //背景是否要填滿
            //pointStyle: 'line',
            pointRadius: 3
        }
        myLineChart.data.datasets.push(newDataSet1);
        const newDataSet2 = {
            type: 'line',
            label: '篩餘%',
            lineTension: 0.3, //曲線幅度
            backgroundColor: 'green', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
            borderColor: 'green', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
            data: chart[2],
            fill: false, //背景是否要填滿
            //pointStyle: 'line',
            pointRadius: 3
        }
        myLineChart.data.datasets.push(newDataSet2);
        myLineChart.update();
    </script>
</asp:Content>
