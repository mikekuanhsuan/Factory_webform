<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cycle_Mill.aspx.cs" Inherits="factory.Mill.cycle_Mill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <script src="../js/jquery.ui.monthpicker.js"></script>
    <script src="../js/moment.min.js"></script>
    <script src="../js/highstock.js"></script>
    <script src="../js/exporting.js"></script>
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

    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
    </style>

    <script type="text/javascript">
        // 隱藏讀取遮罩
        function HideProgressBar() {
            var progress = $('#divProgress');
            var maskFrame = $("#divMaskFrame");
            progress.hide();
            maskFrame.hide();
        }
        // 顯示讀取畫面
        function displayProgress() {
            var w = $(document).width();
            var h = $(window).height();
            var progress = $('#divProgress');
            progress.css({ "z-index": 999999, "top": (h / 2) - (progress.height() / 2), "left": (w / 2) - (progress.width() / 2) });
            progress.show();
        }
        // 顯示遮罩畫面
        function displayMaskFrame() {
            var w = $(window).width();
            var h = $(document).height();
            var maskFrame = $("#divMaskFrame");
            maskFrame.css({ "z-index": 999998, "opacity": 0.7, "width": w, "height": h });
            maskFrame.show();
        }

        function ShowProgressBar() {
            //displayProgress();
            //displayMaskFrame();

            var mill = document.getElementById('ContentPlaceHolder1_ddl_group').options[document.getElementById('ContentPlaceHolder1_ddl_group').selectedIndex].value;
            var f = new URL(location.href).searchParams.get('F');
            var s = document.getElementById('ContentPlaceHolder1_tb_SDATE').value;
            var e = document.getElementById('ContentPlaceHolder1_tb_EDATE').value;
            var url = "get_cycle_datas.aspx/GetData?Mill='" + mill + "'&F='" + f + "'&s='" + s + "'&e='" + e + "'";

            $.ajax({
                type: "GET",
                url: url,
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //結束讀取畫面
                    HideProgressBar();

                    var datas = JSON.parse(data.d);
                    console.log(datas);
                    
                    if (datas != 1) {
                        //刪除節點
                        var content = document.getElementById("r0");
                        var nodelist = content.childNodes;
                        for (var i = nodelist.length - 1; i >= 0; i--) {
                            var x = content.removeChild(nodelist[i]);
                            if (x.nodeType == 1) {
                                x = null;
                            }
                        }

                        //新增節點
                        var el = document.getElementById('r0');
                        var em = '';
                        for (var k = 0; k < datas.datas.length - 1; k++) {
                            if (k == 0) {
                                em += '<div class="col-xxl-2 col-auto" style="color:red;align-self:center;text-align:center">' + datas.datas[0][0][k] + '</div> <div class="col-xxl-10"><figure class="card" highcharts-figure=""><div id="container' + k + '" style="height:250px"></div></figure></div>';
                            } else {
                                em += '<div class="col-xxl-2 col-auto" style="color:red;align-self:center;text-align:center">' + datas.datas[0][0][k] + '</div> <div class="col-xxl-10"><figure class="card" highcharts-figure=""><div id="container' + k + '" style="height:150px"></div></figure></div>';
                            }
                        }
                        el.innerHTML = em;
                        //繪圖
                        var chartSummary = [];

                        for (var i = 0; i < datas.datas.length - 1; i++) {
                            ['mousemove', 'touchmove', 'touchstart'].forEach(function (eventType) {
                                document.getElementById('container' + i).addEventListener(
                                    eventType,
                                    function (e) {
                                        var chart,
                                            point,
                                            i,
                                            event;
                                        for (i = 0; i < Highcharts.charts.length; i = i + 1) {
                                            chart = Highcharts.charts[i];
                                            // Find coordinates within the chart

                                            event = chart.pointer.normalize(e);
                                            // Get the hovered point
                                            point = chart.series[0].searchPoint(event, true);
                                            if (point) {
                                                point.highlight(e);
                                            }
                                        }
                                    }
                                );
                            });

                            Highcharts.Pointer.prototype.reset = function () {
                                return undefined;
                            };


                            Highcharts.Point.prototype.highlight = function (event) {
                                event = this.series.chart.pointer.normalize(event);
                                this.onMouseOver(); // Show the hover marker
                                this.series.chart.tooltip.refresh(this); // Show the tooltip
                                this.series.chart.xAxis[0].drawCrosshair(event, this); // Show the crosshair
                            };

                            function syncExtremes(e) {
                                var thisChart = this.chart;
                                if (e.trigger !== 'syncExtremes') { // Prevent feedback loop
                                    Highcharts.each(Highcharts.charts, function (chart) {
                                        if (chart !== thisChart) {
                                            if (chart.xAxis[0].setExtremes) { // It is null while updating
                                                chart.xAxis[0].setExtremes(
                                                    e.min,
                                                    e.max,
                                                    undefined,
                                                    false,
                                                    { trigger: 'syncExtremes' }
                                                );
                                            }
                                        }
                                    });
                                }
                            };

                            if (i == 0) {
                                chartSummary[i] = new Highcharts.stockChart('container' + i, {
                                    rangeSelector: {
                                        enabled: false
                                    },

                                    title: {
                                        text: ''
                                    },
                                    subtitle: {
                                        //text: 'Using the Boost module'
                                    },
                                    tooltip: {
                                        formatter: function () {
                                            return '<b>' + '</b><br/>' +
                                                Highcharts.dateFormat('%Y-%m-%d %H:%M:%S',
                                                    new Date(this.x)) + ' ： ' + this.y;
                                        },

                                        //xDateFormat: '%Y-%m-%d %H:%M:%S',
                                        shared: true
                                    },
                                    legend: {
                                        enabled: false
                                    },
                                    xAxis: {
                                        type: 'datetime',
                                        labels: {
                                            format: '{value:%m-%d %H:%M}'
                                        },
                                        crosshair: true,
                                        events: {
                                            afterSetExtremes: function (event) {
                                                var xMin = event.min;
                                                var xMax = event.max;
                                                for (var j = 1; j < datas.datas.length - 1; j++) {
                                                    var ex1 = chartSummary[j].xAxis[0].getExtremes();
                                                    if (ex1.min != xMin || ex1.max != xMax) chartSummary[j].xAxis[0].setExtremes(xMin, xMax, true, false);
                                                }
                                            }
                                        },
                                    },
                                    yAxis: {
                                        title: {
                                            text: ''
                                        },
                                        crosshair: true,
                                        opposite: false
                                    },
                                    series: [{
                                        name: "",
                                        data: datas.datas[i + 1][0],
                                        lineWidth: 0.5,
                                    }],

                                });
                            }
                            else {
                                chartSummary[i] = new Highcharts.chart('container' + i, {

                                    /*
                                       chart: {
                                           type: 'line',
                                           events: {
                                               load: function () {
                                                   var series = this.series;
                                                   for (let i = 0; i < series.length; i++) {
                                                       let newData = []
                                                       for (let j = 0; j < series[i].data.length; j++) {
                                                           newData.push({ x: new Date(series[i].data[j].name).getTime() + 28800000, y: series[i].data[j].y });
                                                       }
                           
                                                       this.series[i].update({
                                                           data: newData
                                                       }, false);
                                                   }
                                                   this.redraw();
                                               }
                                           },
                                       },
                                       */
                                    plotOptions: {
                                        series: {
                                            marker: {
                                                enabled: true,
                                                symbol: 'circle',
                                                radius: 0
                                            }
                                        }
                                    },
                                    rangeSelector: {
                                        enabled: false
                                    },

                                    title: {
                                        text: ''
                                    },
                                    subtitle: {
                                        //text: 'Using the Boost module'
                                    },
                                    tooltip: {
                                        formatter: function () {
                                            return '<b>' + '</b><br/>' +
                                                Highcharts.dateFormat('%Y-%m-%d %H:%M:%S',
                                                    new Date(this.x)) + ' ： ' + this.y;
                                        },

                                        /*
                                        formatter: function () {
                                            var content = '<span style="font-size: 10px;">' + this.x + '</span><b_r/>';
                                            for (var i = 0; i < this.points.length; i++) {
                                                content += '<span style="color: ' + this.points[i].series.color + '">' + this.points[i].series.name + '</span>: ' + this.points[i].y + '<b_r/>';
                                            };
                                            var date = new Date();
                                            content += '<span>当前时间: ' + date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDay() + ' ' + (date.getMinutes() + 1) + ':' + (date.getSeconds() + 1) + ':' + (date.getHours() + 1) + '</span>'
                                            return content;
                                        },
                                        */
                                        //xDateFormat: '%Y-%m-%d %H:%M:%S',
                                        shared: true
                                    },
                                    legend: {
                                        enabled: false
                                    },
                                    xAxis: {
                                        type: 'datetime',
                                        labels: {
                                            enabled: false,
                                            format: '{value:%m-%d %H:%M}'
                                        },
                                        crosshair: true,
                                    },
                                    yAxis: {
                                        title: {
                                            text: ''
                                        },
                                        crosshair: true
                                    },
                                    series: [{
                                        name: "",
                                        data: datas.datas[i + 1][0],
                                        lineWidth: 0.5,
                                    }],
                                });
                            }
                        }
                    }
                    
                }
            });
            
        }

        $(document).ready(function () {
            document.getElementById('ContentPlaceHolder1_btn_select').click();

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divProgress" style="text-align:center; display: none; position: fixed; top: 50%;  left: 50%;" >
        <asp:Image ID="imgLoading" runat="server" ImageUrl="../img/loader.gif" />
        <br />
        <font color="#1B3563" size="2px">資料處理中</font>
    </div>
    <div id="divMaskFrame" style="background-color: #F2F4F7; display: none; left: 0px;
        position: absolute; top: 0px;">
    </div>

    <div class ="container-fluid">
        <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
            <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
                <strong>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">趨勢圖-分鐘</asp:Label>
                </strong> 
            </div>

            <div class="row">
               <div class="col-auto">
                    <span class="auto-style1">磨機:</span>
                    <asp:DropDownList ID="ddl_group" runat="server"></asp:DropDownList>
                    <span class="auto-style1">開始:</span>
                    <asp:TextBox ID="tb_SDATE" runat="server" placeholder="起始日期:" TextMode="SingleLine" Width="125px"  autocomplete="off" CssClass="mar-r" ></asp:TextBox>
                    <span class="auto-style1">結束:</span>
                    <asp:TextBox ID="tb_EDATE" runat="server" placeholder="結束日期:" TextMode="SingleLine" Width="125px"  autocomplete="off" CssClass="mar-r"></asp:TextBox>
                </div>
                <div class="col-auto">
                    <asp:Button ID="btn_select" runat="server" Class="btn btn-primary" Text="查詢" Style="padding-top:3px;padding-bottom:3px" OnClientClick="ShowProgressBar(); return false;" />
                </div>
            </div>
        </div>

        <div class="row" id="r0">
           
        </div>

    </div>
    <script>
        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            //手機導覽列
            document.getElementById("phone").style.display = "";
        }
    </script>
</asp:Content>
