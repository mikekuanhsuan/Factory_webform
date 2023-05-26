<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tag_trend_h.aspx.cs" Inherits="factory.Tag_trend_h" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <title>趨勢圖－小時</title>
    <script src="../js/jquery-1.12.4.js"></script>
      <script src="../js/moment.min.js"></script>
      <script src="../js/Chart.min.js"></script>
      <script src="../js/hammerjs@2.0.8.js"></script>  
      <script src="../js/chartjs-plugin-zoom.js"></script>
      <style type="text/css">
          .auto-style2 {
              font-size: x-large;
              color: #FF0000;
          }
      </style>
    <link href="../css/jquery-ui.css" rel="stylesheet"/>
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <script src="../js/jquery.ui.monthpicker.js"></script>	
    <script>
            $(function () {
                $("#<%=tb_SDATE.ClientID%>").monthpicker({
                    prevText: "&#x3C;上一年",
                    nextText: "下一年&#x3E;",
                });
            });

            $(function () {
                $("#<%=tb_EDATE.ClientID%>").monthpicker({
                    prevText: "&#x3C;上一年",
                    nextText: "下一年&#x3E;",
                });
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <asp:Label ID="Label2" runat="server" Text="廠區:"></asp:Label>
    <asp:DropDownList ID="ddl_fty" runat="server" DataSourceID="sds1" DataTextField="FactoryName" DataValueField="FactoryID"></asp:DropDownList>
    <asp:SqlDataSource ID="sds1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory] ORDER BY [aOrder]"></asp:SqlDataSource>
    <asp:Label ID="Label1" runat="server" Text="開始時間:"></asp:Label>
    <asp:TextBox ID="tb_SDATE" runat="server"  TextMode="SingleLine" placeholder="選擇年月" Width="130px" required autocomplete="off" onkeydown="return false;"></asp:TextBox>
    <asp:Label ID="Label3" runat="server" Text="結束時間:"></asp:Label>
    <asp:TextBox ID="tb_EDATE" runat="server"  TextMode="SingleLine" placeholder="選擇年月" Width="130px" required autocomplete="off" onkeydown="return false;"></asp:TextBox>
    <asp:Button ID="btn_confrim" runat="server" Text="確定" class="btn btn-primary" OnClick="btn_confrim_Click" Height="30px" Style="padding-top:3px" />
</div>    
<div>
    <strong>
        <asp:Label ID="lb_err" runat="server" Text="無資料" CssClass="auto-style2" Visible="False"></asp:Label>
    </strong>
    <div class="card-body"><canvas id="myAreaChart" width="100%" height="40"></canvas></div>
</div>

<script>
    var ctx = document.getElementById("myAreaChart");
    var color = ["red", "blue", "yellow", 'green', 'purple', 'orange', 'black', 'pink',
        'brown', '#4D1F00', '#704214', '#006400', '#ffb6c1', '#c71585', '#f8f8ff', '#1e90ff',
        '#00bfff', '#7fffd4', '#228b22', '#B15BFF', '#F00078', '#D9FFFF', '#46A3FF', '#8C8C00'];
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            datasets: [
                /*
                {
                    label: "1",
                    lineTension: 0.3, //曲線幅度
                    backgroundColor: "red", // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
                    borderColor: "red", // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
                    data: [10, 30, 39, 20, 25, 34, 1],
                    fill: false, //背景是否要填滿   
                },
                {
                    label: '溫度B',
                    fill: false,
                    backgroundColor: "blue",
                    borderColor: "blue",
                    data: [18, 33, 22, 19, 11, 39, 30],
                },
                */
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
                text: '每小時趨勢圖'
            },
            scales: {
                xAxes: [{
                    type: 'time',
                    position: 'bottom',
                    time: {
                        unit: <%Response.Write(Session["unit"]);%>,
                            displayFormats: {
                                <%Response.Write(Session["displayFormats"]);%>
                            },
                            format: "YYYY-MM-DD HH:mm",
                            min: '<%Response.Write(Session["min"]);%>',
                            max: '<%Response.Write(Session["max"]);%>',
                            stepSize:<%Response.Write(Session["stepSize"]);%>
                        },
                        gridLines: {
                            display: false,
                        },

                    }],
                    /*
                    yAxes: [{ //Y軸
                        ticks: { //Y軸的刻度
                            min: 0,
                            max: 100,
                            stepSize: 10 //間距
                        },
                    }],
                    */
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
                            enabled: true,
                            mode: 'xy',
                        },
                        zoom: { //縮放功能
                            enabled: true,
                            mode: 'xy',
                        }
                    }
                }
            }
    });
    var count_data = <% Response.Write(Session["count_data"]);%>;
    var TagName = <% Response.Write(Session["TagName"]);%>;
    var datas = <% Response.Write(Session["datas"]);%>;
    

    for (i = 0; i < count_data; i++) {
        const newDataSet = {
            label: TagName[i],
            lineTension: 0.3, //曲線幅度
            backgroundColor: color[i], // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
            borderColor: color[i], // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
            data: datas[i],
            fill: false, //背景是否要填滿
            pointStyle: 'line',

        }
        myLineChart.data.datasets.push(newDataSet);
    }
    myLineChart.update();


</script>
</asp:Content>
