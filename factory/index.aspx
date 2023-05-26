<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="factory.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-3.5.1.slim.min.js"></script>
    <script src="js/moment.min.js"></script>
    <script src="js/Chart.min.js"></script>
    <script src="js/hammerjs@2.0.8.js"></script>  
    <script src="js/chartjs-plugin-zoom.js"></script>
    <script>
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            location.href = "indexm.aspx";
        }
    </script>
    <title>首頁</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
    <div class="container-fluid">
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <center><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/AirComp/AirCompressor.aspx?F=KY-T1HIST&amp;M=0" Font-Bold="True" ForeColor="Red">觀音廠</asp:HyperLink></center>
                <canvas id="myAreaChart0" width="100%" height="15"></canvas>
            </div>
        </div>
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <center><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="/AirComp/AirCompressor.aspx?F=BL-T1HIST&M=0" class="link-primary" Font-Bold="True" ForeColor="Red">八里廠</asp:HyperLink></center>
                <canvas id="myAreaChart1" width="100%" height="15"></canvas>
            </div>
        </div>
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <center><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="/AirComp/AirCompressor.aspx?F=QX-T1HIST&M=0" Font-Bold="True" ForeColor="Red">全興廠</asp:HyperLink></center>
                <canvas id="myAreaChart2" width="100%" height="15"></canvas>
            </div>
        </div>
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <center><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="/AirComp/AirCompressor.aspx?F=KH-PCC-LH&M=0" Font-Bold="True" ForeColor="Red">高雄廠</asp:HyperLink></center>
                <canvas id="myAreaChart3" width="100%" height="15"></canvas>
            </div>
        </div>
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <center><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="/AirComp/AirCompressor.aspx?F=LD-T1HIST&M=0" Font-Bold="True" ForeColor="Red">龍德廠</asp:HyperLink></center>
                <canvas id="myAreaChart4" width="100%" height="15"></canvas>
            </div>
        </div>
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <center><asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="/AirComp/AirCompressor.aspx?F=LZ-T1HIST&M=0" Font-Bold="True" ForeColor="Red">利澤廠</asp:HyperLink></center>
                <canvas id="myAreaChart5" width="100%" height="15"></canvas>
            </div>
        </div>
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <center><asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="/AirComp/AirCompressor.aspx?F=HL-T1HIST&M=0" Font-Bold="True" ForeColor="Red">花蓮廠</asp:HyperLink></center>
                <canvas id="myAreaChart6" width="100%" height="15"></canvas>
            </div>
        </div>
        
        <div class="row" style="margin:0px;margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <canvas id="myAreaChart7" width="100%" height="15"></canvas>
            </div>
        </div>
    </div>
    
<script>
    //暫時移除彰濱廠 無資料會造成JS損壞無法繼續下去
    

    var x = [test0, test1, test2, test4, test5, test6, test7];
    for (var q = 0; q < 8; q++)
    {
        var name = "myAreaChart" + q;
        var ctx = document.getElementById(name);
        var color = ["blue", "red", "yellow", 'green', 'purple', 'orange', 'black', 'pink',
            'brown', '#4D1F00', '#704214', '#006400', '#ffb6c1', '#c71585', '#f8f8ff', '#1e90ff',
            '#00bfff', '#7fffd4', '#228b22', '#B15BFF', '#F00078', '#D9FFFF', '#46A3FF', '#8C8C00'];
        var myLineChart = new Chart(ctx, {
            type: 'line',
            data: {
                datasets: [
                    {
                        label: x[q][4][0].b,
                        lineTension: 0.3, //曲線幅度
                        backgroundColor: color[0], // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
                        borderColor: color[0], // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
                        data: x[q][0],
                        fill: false, //背景是否要填滿
                        //pointStyle: 'line',
                        pointRadius: 3
                    },
                    {
                        label: x[q][5][0].avg_b,
                        lineTension: 0.3, //曲線幅度
                        backgroundColor: color[1], // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
                        borderColor: color[1], // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
                        data: x[q][1],
                        fill: false, //背景是否要填滿
                        borderWidth: 1
                    },
                ],
            },
            options: {
                elements: {
                    point: {
                        radius: 0
                    }
                },
                responsive: true,
                title: {
                    display: true,
                    text: x[q][6][0].title,
                },
                scales: {
                    xAxes: [{
                        type: 'time',
                        position: 'bottom',
                        time: {
                            unit: 'day',
                            displayFormats: {
                                day: 'MM-DD',
                            },
                            format: "YYYY-MM-DD",
                            min: x[q][2][0].min,
                            max: x[q][3][0].max,
                            
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
    }
</script>
        -->
</asp:Content>
