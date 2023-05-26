<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Power_D.aspx.cs" Inherits="factory.power.Power_D" %>
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
            $("#<%=tb_SDATE.ClientID%>").monthpicker({
                prevText: "&#x3C;上一年",
                nextText: "下一年&#x3E;",
                /*
                修改UI的大小
                .ui-datepicker {
                    font-size:62.5%;
                }
                */
            });
        });
    </script>
    <style type="text/css">
        /*height: calc(100vh - 580px);*/
        
        .GV1 table tbody {
            display:block;
            height: calc(38vh);
            overflow-y: overlay;
        }
        
        .GV1 table thead, tbody tr {
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
                <asp:HyperLink ID="hyl_factory" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:HyperLink>
                <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">用電量</asp:Label>
            </strong> 
        </div>
        <div class="row">
            <div id="name" class="col-auto">
                <strong>
                    <asp:Label ID="lb_factory" runat="server" CssClass="auto-style1"></asp:Label>
                </strong>
            </div>
            <div class="col-auto">
                <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" CssClass="mar-r" OnTextChanged="tb_SDATE_TextChanged" ></asp:TextBox>
                <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一個月" CssClass="mar-r" OnClick="imgb_p_Click" />
                <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一個月" CssClass="mar-r" OnClick="imgb_n_Click" />
                <asp:ImageButton ID="imgb_excel" runat="server" ImageUrl="~/img/excel.png" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="匯出EXCEL" OnClick="imgb_excel_Click" />
            </div>
            <div class="col-auto" style="align-self:center;">
                <strong>
                    <asp:Label ID="Label2" runat="server" Text="用電量/月管理" CssClass="auto-style1"></asp:Label>
                </strong>
            </div>
        </div>
        </div>
        <div class="row" style="margin:10px 0px 20px 0px">
            <div class="col-12" style="border:1px solid rgba(0, 0, 0, 0.125);"> 
                <canvas id="myAreaChart" width="100%" height="22"></canvas>
            </div>
        </div>

        <div class="GV1 table-responsive">
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataKeyNames="FactoryID,DataDate" DataSourceID="SDS1" OnDataBound="GV1_DataBound">
                <Columns>
                    <asp:BoundField DataField="FactoryID" HeaderText="FactoryID" ReadOnly="True" SortExpression="FactoryID" Visible="False" />
                    <asp:TemplateField HeaderText="日期" SortExpression="DataDate">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("DataDate", "{0:MM/dd}") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="hyl_datatime" runat="server" Text='<%# Bind("DataDate", "{0:MM/dd}") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DTime" HeaderText="DTime" SortExpression="DTime" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_Total" HeaderText="Power_KWH_Total" SortExpression="Power_KWH_Total" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_A" HeaderText="Power_KWH_A" SortExpression="Power_KWH_A" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_B" HeaderText="Power_KWH_B" SortExpression="Power_KWH_B" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_C" HeaderText="Power_KWH_C" SortExpression="Power_KWH_C" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_01" HeaderText="Power_KWH_01" SortExpression="Power_KWH_01" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_02" HeaderText="Power_KWH_02" SortExpression="Power_KWH_02" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_03" HeaderText="Power_KWH_03" SortExpression="Power_KWH_03" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_04" HeaderText="Power_KWH_04" SortExpression="Power_KWH_04" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_05" HeaderText="Power_KWH_05" SortExpression="Power_KWH_05" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_06" HeaderText="Power_KWH_06" SortExpression="Power_KWH_06" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_07" HeaderText="Power_KWH_07" SortExpression="Power_KWH_07" Visible="False" />
                    <asp:BoundField DataField="Power_KWH_08" HeaderText="Power_KWH_08" SortExpression="Power_KWH_08" Visible="False" />
                    <asp:BoundField DataField="Power_C_Total" HeaderText="用電量" SortExpression="Power_C_Total" DataFormatString="{0:#,0.####}" NullDisplayText="0" />
                    <asp:BoundField DataField="Power_C_A" HeaderText="Power_C_A" SortExpression="Power_C_A" Visible="False" />
                    <asp:BoundField DataField="Power_C_B" HeaderText="Power_C_B" SortExpression="Power_C_B" Visible="False" />
                    <asp:BoundField DataField="Power_C_C" HeaderText="Power_C_C" SortExpression="Power_C_C" Visible="False" />
                    <asp:TemplateField HeaderText="#1&lt;br&gt;KWH(時)" SortExpression="Power_C_01">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Power_C_01") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p1" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n1" runat="server" Text="#1&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p1" runat="server" Text='<%# Eval("Power_C_01", "{0:#,0.####}")== ""?"0":Eval("Power_C_01", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2&lt;br&gt;KWH(時)" SortExpression="Power_C_02">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Power_C_02") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p2" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n2" runat="server" Text="#2&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p2" runat="server" Text='<%# Eval("Power_C_02", "{0:#,0.####}")== ""?"0":Eval("Power_C_02", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#3&lt;br&gt;KWH(時)" SortExpression="Power_C_03">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Power_C_03") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p3" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n3" runat="server" Text="#3&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p3" runat="server" Text='<%# Eval("Power_C_03", "{0:#,0.####}")== ""?"0":Eval("Power_C_03", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#4&lt;br&gt;KWH(時)" SortExpression="Power_C_04">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Power_C_04") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p4" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n4" runat="server" Text="#4&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p4" runat="server" Text='<%# Eval("Power_C_04", "{0:#,0.####}")== ""?"0":Eval("Power_C_04", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#5&lt;br&gt;KWH(時)" SortExpression="Power_C_05">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Power_C_05") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p5" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n5" runat="server" Text="#5&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p5" runat="server" Text='<%# Eval("Power_C_05", "{0:#,0.####}")== ""?"0":Eval("Power_C_05", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#6&lt;br&gt;KWH(時)" SortExpression="Power_C_06">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Power_C_06") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p6" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n6" runat="server" Text="#6&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p6" runat="server" Text='<%# Eval("Power_C_06", "{0:#,0.####}")== ""?"0":Eval("Power_C_06", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#7&lt;br&gt;KWH(時)" SortExpression="Power_C_07">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Power_C_07") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p7" runat="server" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n7" runat="server" Text="#7&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p7" runat="server" Text='<%# Eval("Power_C_07", "{0:#,0.####}")== ""?"0":Eval("Power_C_07", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#8&lt;br&gt;KWH(時)" SortExpression="Power_C_08">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Power_C_08") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="ch_p8" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><asp:Label ID="lb_n8" runat="server" Text="#8&lt;br&gt;KWH(時)"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_p8" runat="server" Text='<%# Eval("Power_C_08", "{0:#,0.####}")== ""?"0":Eval("Power_C_08", "{0:#,0.####}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <strong>
                    <asp:Label ID="Label3" runat="server" CssClass="auto-style2" Text="無資料"></asp:Label>
                    </strong>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [G_Power_D]"></asp:SqlDataSource>
        </div>
    </div>
    <script>
        //判斷畫面寬度修改趨勢圖大小
        var d = document.getElementById('ContentPlaceHolder1_GV1');
        console.log(d);
        var w = screen.width;
        var canvas = document.getElementsByTagName('canvas')[0];
        if (w < 1000) {
            canvas.height = 47;
        }
        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            //手機導覽列
            document.getElementById("phone").style.display = "";
            document.getElementById("name").style.display = "none";
            canvas.height = 80;
            d.style.width = "320%";
        }
    </script>
    <script>
        var chart = <%=chart()%>;
        console.log(chart);
        var ctx = document.getElementById("myAreaChart");
        var barcolor = ["rgba(255, 206, 86, 0.8)", "rgba(75, 192, 192, 0.8)", "rgba(240, 0, 120, 0.8)", "rgba(70, 163, 255, 0.8)",
            "rgba(238, 232, 170, 0.8)", "rgba(34, 139, 34, 0.8)", "rgba(178, 34, 34, 0.8)", "rgba(199, 21, 133, 0.8)",
            "rgba(255, 182, 193, 0.8)", "rgba(112, 66, 20, 0.8)"];
        var myLineChart = new Chart(ctx, {
            type: 'bar',
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
                title: {
                    display: true,
                    text: chart[2][0].title,
                    fontSize: 16
                },
                scales: {
                    xAxes: [{
                        barPercentage: 0.4,
                        type: 'time',
                        offset: true,
                        position: 'bottom',
                        time: {
                            unit: 'day',
                            displayFormats: {
                                day: 'MM-DD',
                            },
                            format: "YYYY-MM-DD",
                            min: chart[0][0].min,
                            max: chart[1][0].max,
                            //stepSize:1,
                        },
                        gridLines: {
                            display: false,
                        },
                    }],

                    yAxes: [{ //Y軸
                        /*
                        ticks: { //Y軸的刻度
                            min: 0,
                            max: 100,
                            stepSize: 10 //間距
                        },
                        */
                        id: 'A',
                        type: 'linear',
                        position: 'left',
                    },
                    {
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
        myLineChart.options.scales.yAxes[1].display = false;
        //沒有用電量的時候不要第二條Y軸刻度
        if (chart[3].length > 0) {
            const newDataSet = {
                type: 'line',
                yAxisID: 'A',
                label: chart[4][0].power,
                lineTension: 0.3, //曲線幅度
                backgroundColor: 'blue', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
                borderColor: 'blue', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
                data: chart[3],
                fill: false, //背景是否要填滿
                //pointStyle: 'line',
                pointRadius: 3
            }
            myLineChart.data.datasets.push(newDataSet);
            var c = 0; //調整顏色用
            for (var i = 0; i < chart.length - 5; i += 2) {
                const newbar = {
                    type: 'bar',
                    label: chart[6 + i][0].p,
                    yAxisID: 'B',
                    backgroundColor: barcolor[c],
                    borderColor: barcolor[c],
                    data: chart[5 + i],
                }

                myLineChart.data.datasets.push(newbar);
                c++;
                myLineChart.options.scales.yAxes[1].display = true;
            }
        }
        else {
            var c = 0; //調整顏色用
            for (var i = 0; i < chart.length - 5; i += 2) {
                const newbar = {
                    type: 'bar',
                    label: chart[6 + i][0].p,
                    yAxisID: 'A',
                    backgroundColor: barcolor[c],
                    borderColor: barcolor[c],
                    data: chart[5 + i],
                }
                myLineChart.data.datasets.push(newbar);
                c++;
            }
        }
        myLineChart.update();
    </script>
</asp:Content>
