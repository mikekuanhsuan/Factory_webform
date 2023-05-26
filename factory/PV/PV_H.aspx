<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PV_H.aspx.cs" Inherits="factory.PV.PV_H" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    </script>
    <style type="text/css">
        .GV1 table tbody {
            display:block;
            height: calc(38vh);
            overflow-y: overlay;
        }
        .GV1 table thead,.GV1 tbody tr {
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
            color: #FF0000;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
        <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
            <strong>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                <asp:HyperLink ID="hyl_factory" runat="server"  ForeColor="Maroon" CssClass="auto-style1"></asp:HyperLink>
                <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">太陽能</asp:Label>
            </strong> 
        </div>
        <div class="row">
            <div class="col-auto" id="name">
                <strong>
                    <asp:Label ID="lb_factory" runat="server" CssClass="auto-style1"></asp:Label>
                </strong>
            </div>
            <div class="col-auto">
                <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" CssClass="mar-r" OnTextChanged="tb_SDATE_TextChanged" ></asp:TextBox>
                <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一天" CssClass="mar-r" OnClick="imgb_p_Click" />
                <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一天" CssClass="mar-r" OnClick="imgb_n_Click" />
                <asp:ImageButton ID="imgb_excel" runat="server" ImageUrl="~/img/excel.png" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="匯出EXCEL" OnClick="imgb_excel_Click" />
            </div>
            <div class="col-auto" style="align-self:center;">
                <strong>
                    <asp:Label ID="Label2" runat="server" Text="太陽能/日管理" CssClass="auto-style1"></asp:Label>
                </strong>
            </div>
            <div class="col" style="align-self:center;">
                <div class="text-right">
                    <strong>
                        <asp:HyperLink ID="hyl_mgt" runat="server" CssClass="auto-style1">太陽能/月管理</asp:HyperLink>
                    </strong>
                </div>
            </div>
        </div>
        </div>
        <div class="row justify-content-center" style="margin:10px 0px 20px 0px">
            <div class="col-12 col-xl-7 card"> 
                <canvas id="myAreaChart" width="100%" height="40"></canvas>
            </div>
        </div>
        <div class="row justify-content-center" style="margin:0px">
            <div class="col-12 col-xl-7" style="padding:0px">
                <div class="GV1 table-responsive">
                    <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataSourceID="SDS1">
                        <Columns>
                            <asp:BoundField DataField="DTime" DataFormatString="{0:MM/dd HH:mm}" HeaderText="時間" SortExpression="DTime" />
                            <asp:BoundField DataField="Power_Generation" HeaderText="發電量(度)" SortExpression="Power_Generation" />
                            <asp:BoundField DataField="kwh_avg" HeaderText="每kW系統日均發電度數" SortExpression="kwh_avg" />
                        </Columns>
                        <EmptyDataTemplate>
                            <strong>
                            <asp:Label ID="Label3" runat="server" CssClass="auto-style2" Text="無資料!"></asp:Label>
                            </strong>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT [DTime], [Power_Generation], [kwh_avg] FROM [G_SE_H]"></asp:SqlDataSource>
                </div>
            </div>
        </div>

    <script>
        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            //手機導覽列
            document.getElementById("phone").style.display = "";
            document.getElementById("name").style.display = "none";
            var canvas = document.getElementsByTagName('canvas')[0];
            canvas.height = 80;
        }
    </script>
    </div>
    <script>
        var chart = <%=chart()%>;
        console.log(chart);
        var maxv = chart[7][0].max;
        maxv = parseInt(maxv, 10);
        var stepSizev = chart[8][0].stepSize;
        stepSizev = parseInt(stepSizev, 10);

        var ctx = document.getElementById("myAreaChart");
        var myLineChart = new Chart(ctx, {
            type: 'bar',
            data: {
                datasets: [
                    //資料一律由外部新增的方式
                ],
            },
            options: {
                "hover": {
                    "animationDuration": 0
                },
                "animation": {
                    "duration": 1,
                    "onComplete": function () {
                        if (!/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
                            var chartInstance = this.chart,
                                ctx = chartInstance.ctx;

                            ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontSize, Chart.defaults.global.defaultFontStyle, Chart.defaults.global.defaultFontFamily);
                            ctx.textAlign = 'center';
                            ctx.textBaseline = 'bottom';

                            this.data.datasets.forEach(function (dataset, i) {
                                var meta = chartInstance.controller.getDatasetMeta(i);
                                if (i == 0) {
                                    meta.data.forEach(function (bar, index) {
                                        ctx.fillStyle = "black";
                                        if (bar._model.y < 275) {
                                            var data = dataset.data[index];
                                            ctx.fillText(data.y, bar._model.x, bar._model.y - 5);
                                        } else if (bar._model.y < 300) {
                                            var data = dataset.data[index];
                                            ctx.fillText(data.y, bar._model.x, bar._model.y - 15);
                                        } else {
                                            var data = dataset.data[index];
                                            ctx.fillText(data.y, bar._model.x, bar._model.y - 20);
                                        }

                                    });
                                } else {
                                    ctx.fillStyle = "red";
                                    meta.data.forEach(function (bar, index) {
                                        var data = dataset.data[index];
                                        ctx.fillText(data.y, bar._model.x, bar._model.y - 5);
                                    });
                                }

                            });
                        }
                    }
                },

                elements: {
                    point: {
                        radius: 0
                    }
                },
                responsive: true,
                title: {
                    display: true,
                    text: chart[2][0].title,
                    fontSize: 16
                },
                scales: {
                    xAxes: [{
                        barPercentage: 1,
                        type: 'time',
                        offset: true,
                        position: 'bottom',
                        time: {
                            unit: 'hour',
                            displayFormats: {
                                hour: 'HH:mm',
                            },
                            format: "YYYY-MM-DD HH:mm",
                            min: chart[0][0].min,
                            max: chart[1][0].max,
                            //stepSize:1,
                        },
                        gridLines: {
                            display: false,
                        },
                    }],

                    yAxes: [{ //Y軸
                        ticks: { //Y軸的刻度
                            min: 0,
                            max: maxv,
                            stepSize: stepSizev //間距
                        },
                        
                        id: 'A',
                        type: 'linear',
                        position: 'left',
                    },
                    {
                        
                        ticks: { //Y軸的刻度
                            min: 0,
                            max: 3,
                        },
                        
                        id: 'B',
                        type: 'linear',
                        position: 'right',
                        gridLines: {
                            display: false,
                        },
                        /*
                        ticks: {
                            display: false
                        }
                        */
                    }
                    ],
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
                    enabled: false,
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
        const newDataSet = {
            type: 'bar',
            yAxisID: 'A',
            label: chart[4][0].power,
            backgroundColor: '#82aa31', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
            borderColor: '#82aa31', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
            data: chart[3],
            order: 2
        }
        myLineChart.data.datasets.push(newDataSet);

        const DataSet = {
            type: 'line',
            yAxisID: 'B',
            lineTension: 0.3, //曲線幅度
            label: chart[6][0].power_avg,
            backgroundColor: '#fce247', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
            borderColor: '#fce247', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
            data: chart[5],
            fill: false, //背景是否要填滿
            pointRadius: 3,
            order: 1
        }
        myLineChart.data.datasets.push(DataSet);
        myLineChart.update();
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            myLineChart.options.tooltips.enabled = true;
        }
    </script>
</asp:Content>
