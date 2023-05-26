<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testgridview.aspx.cs" Inherits="factory.testgridview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.12.4.js"></script>

    <script src="../js/highstock.js"></script>
    <script src="../js/exporting.js"></script>
</head>

<body>
    <form id="form1" runat="server">
    <figure class="highcharts-figure">
        <div id="container0" style="height: 400px;"></div>
    </figure>

    <figure class="highcharts-figure">
        <div id="container1" style="height: 200px;"></div>
    </figure>

    <figure class="highcharts-figure">
        <div id="container2" style="height: 200px;"></div>
    </figure>

    <figure class="highcharts-figure">
        <div id="container3" style="height: 200px;"></div>
    </figure>


    <figure class="highcharts-figure">
        <div id="container4" style="height: 200px;"></div>
    </figure>


    <figure class="highcharts-figure">
        <div id="container5" style="height: 200px;"></div>
    </figure>


    <figure class="highcharts-figure">
        <div id="container6" style="height: 200px;"></div>
    </figure>


    <figure class="highcharts-figure">
        <div id="container7" style="height: 200px;"></div>
    </figure>


    <figure class="highcharts-figure">
        <div id="container8" style="height: 200px;"></div>
    </figure>


    <figure class="highcharts-figure">
        <div id="container9" style="height: 200px;"></div>
    </figure>
    </form>
</body>

<script>
    var line1 = <%=line1()%>;
    console.log(line1);
</script>
<script>
    var line1 = <%=line1()%>;
    var line2 = <%=line2()%>;
    var line3 = <%=line3()%>;
    var line4 = <%=line4()%>;
    var line5 = <%=line5()%>;
    var line6 = <%=line6()%>;
    var y = [line1[2], line2[2], line3[2], line4[2], line5[2], line6[2]];
    console.log(y);


    var chartSummary = [];
    for (var i = 0; i < 6; i++) {
        ['mousemove', 'touchmove', 'touchstart'].forEach(function (eventType) {
            document.getElementById('container'+i).addEventListener(
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
                        format: '{value:%m-%d %H:%M}'
                    },
                    crosshair: true,
                    events: {
                        afterSetExtremes: function (event) {
                            var xMin = event.min;
                            var xMax = event.max;
                            for (var j = 0; j < 6; j++) {
                                if (i != j) {
                                    var ex1 = chartSummary[j].xAxis[0].getExtremes();
                                    if (ex1.min != xMin || ex1.max != xMax) chartSummary[j].xAxis[0].setExtremes(xMin, xMax, true, false);
                                }
                            }
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    crosshair: true
                },
                series: [{
                    name: "",
                    data: y[i],
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
                    events: {
                        afterSetExtremes: function (event) {
                            var xMin = event.min;
                            var xMax = event.max;
                            for (var j = 0; j < 6; j++) {
                                if (i != j) {
                                    var ex1 = chartSummary[j].xAxis[0].getExtremes();
                                    if (ex1.min != xMin || ex1.max != xMax) chartSummary[j].xAxis[0].setExtremes(xMin, xMax, true, false);
                                }
                            }
                        }
                    }
                },
                yAxis: {
                    title: {
                        text: ''
                    },
                    crosshair: true
                },
                series: [{
                    name: "",
                    data: y[i],
                    lineWidth: 0.5,
                }],

            });
        }
    }

</script>

</html>
