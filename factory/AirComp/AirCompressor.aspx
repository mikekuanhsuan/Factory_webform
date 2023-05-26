﻿<%@ Page  Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AirCompressor.aspx.cs" Inherits="factory.AirCompressor_KH" %>
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

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
    <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
        <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
            <strong>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                <asp:HyperLink ID="hyl_factory" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:HyperLink>
                <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">空壓機比功率</asp:Label>
            </strong>
        </div>
        <div class="row">
            <div id="name" class="col-auto">
                <strong>
                    <asp:Label ID="lb_factory" runat="server" CssClass="auto-style1"></asp:Label>
                </strong>
            </div>
            <div class="col-auto">
                <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" OnTextChanged="tb_SDATE_TextChanged" CssClass="mar-r" ></asp:TextBox>
                <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一個月" OnClick="imgb_p_Click" CssClass="mar-r" />
                <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一個月" CssClass="mar-r" OnClick="imgb_n_Click" />
                <asp:ImageButton ID="imgb_excel" runat="server" ImageUrl="~/img/excel.png" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="匯出EXCEL" OnClick="imgb_excel_Click" />
            </div>
            <div class="col-auto" style="align-self:center;">
                <strong>
                    <asp:Label ID="Label2" runat="server" Text="空壓機比功率/月管理" CssClass="auto-style1"></asp:Label>
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
        <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataSourceID="SDS1" DataKeyNames="FactoryID,DataDate" OnDataBound="GV1_DataBound" style="margin-right: 0px">
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
                <asp:BoundField DataField="Specific_Power" HeaderText="比功率" SortExpression="Specific_Power" />
                <asp:BoundField DataField="GetDateTime" HeaderText="抄表時間" SortExpression="GetDateTime" DataFormatString="{0:HH-​mm}" Visible="False" />
                <asp:BoundField DataField="Air_acc_01" HeaderText="累計量1" SortExpression="Air_acc_01" Visible="False" />
                <asp:BoundField DataField="Air_acc_02" HeaderText="累計量2" SortExpression="Air_acc_02" Visible="False" />
                <asp:TemplateField HeaderText="空氣用量&lt;br&gt;M3/Min" SortExpression="Air_Consumption">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("Air_Consumption") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_p" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" /><br>空氣用量M3/Min
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_p" runat="server" Text='<%# Bind("Air_Consumption") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用電量">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("Power_C") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_p0" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />
                        <br>用電量 </br>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_p0" runat="server" Text='<%# Bind("Power_C") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#1用電量" SortExpression="Power_C_01">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Power_C_01") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_p1" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />#1<br />用電量
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_p1" runat="server" Text='<%# Bind("Power_C_01", "{0:F0}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#1&lt;br&gt;KWH(時)" SortExpression="Power_CH_01">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("Power_CH_01") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lb_r6" runat="server" Text="#1&lt;br&gt;KWH(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_kwh1" runat="server" Text='<%# Bind("Power_CH_01", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Power_01" HeaderText="#1_KWH" SortExpression="Power_01" />
                <asp:TemplateField HeaderText="#1運轉(時)" SortExpression="WorkTime_01">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("WorkTime_01") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_w1" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />
                        <asp:Label ID="lb_r1" runat="server" Text="#1&lt;br&gt;運轉(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_w1" runat="server" Text='<%# Bind("WorkTime_01") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#2用電量" SortExpression="Power_C_02">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Power_C_02") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_p2" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />#2<br>用電量
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_p2" runat="server" Text='<%# Bind("Power_C_02", "{0:F0}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#2&lt;br&gt;KWH(時)" SortExpression="Power_CH_02">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("Power_CH_02") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lb_r7" runat="server" Text="#2&lt;br&gt;KWH(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_kwh2" runat="server" Text='<%# Bind("Power_CH_02", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Power_02" HeaderText="#2_KWH" SortExpression="Power_02" />
                <asp:TemplateField HeaderText="#2運轉(時)" SortExpression="WorkTime_02">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("WorkTime_02") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_w2" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />
                        <asp:Label ID="lb_r2" runat="server" Text="#2&lt;br&gt;運轉(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_w2" runat="server" Text='<%# Bind("WorkTime_02") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#3用電量" SortExpression="Power_C_03">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Power_C_03") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_p3" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />#3<br>用電量
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_p3" runat="server" Text='<%# Bind("Power_C_03", "{0:F0}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#3&lt;br&gt;KWH(時)" SortExpression="Power_CH_03">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("Power_CH_03") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lb_r8" runat="server" Text="#3&lt;br&gt;KWH(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_kwh3" runat="server" Text='<%# Bind("Power_CH_03", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Power_03" HeaderText="#3_KWH" SortExpression="Power_03" />
                <asp:TemplateField HeaderText="#3運轉(時)" SortExpression="WorkTime_03">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("WorkTime_03") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_w3" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />
                        <asp:Label ID="lb_r3" runat="server" Text="#3&lt;br&gt;運轉(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_w3" runat="server" Text='<%# Bind("WorkTime_03") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#4用電量" SortExpression="Power_C_04">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Power_C_04") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_p4" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />#4<br>用電量
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_p4" runat="server" Text='<%# Bind("Power_C_04", "{0:F0}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#4&lt;br&gt;KWH(時)" SortExpression="Power_CH_04">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("Power_CH_04") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lb_r9" runat="server" Text="#4&lt;br&gt;KWH(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_kwh4" runat="server" Text='<%# Bind("Power_CH_04", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Power_04" HeaderText="#4_KWH" SortExpression="Power_04" />
                <asp:TemplateField HeaderText="#4運轉(時)" SortExpression="WorkTime_04">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("WorkTime_04") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_w4" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />
                        <asp:Label ID="lb_r4" runat="server" Text="#4&lt;br&gt;運轉(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_w4" runat="server" Text='<%# Bind("WorkTime_04") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#5用電量" SortExpression="Power_C_05">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("Power_C_05") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_p5" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />#5<br>用電量
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_p5" runat="server" Text='<%# Bind("Power_C_05", "{0:F0}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#5&lt;br&gt;KWH(時)" SortExpression="Power_CH_05">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("Power_CH_05") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:Label ID="lb_r5" runat="server" Text="#5&lt;br&gt;KWH(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_kwh5" runat="server" Text='<%# Bind("Power_CH_05", "{0:N2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Power_05" HeaderText="#5_KWH" SortExpression="Power_05" />
                <asp:TemplateField HeaderText="#5運轉(時)" SortExpression="WorkTime_05">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("WorkTime_05") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="ch_w5" runat="server" AutoPostBack="True" OnCheckedChanged="ch_p0_CheckedChanged" />
                        <asp:Label ID="lb_r10" runat="server" Text="#5&lt;br&gt;運轉(時)"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_w5" runat="server" Text='<%# Bind("WorkTime_05") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <strong>
                    <asp:Label ID="Label3" runat="server" CssClass="auto-style1" Text="無資料"></asp:Label>
                </strong>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [G_Air_Comp] WHERE (([FactoryID] = @FactoryID) AND ([MID] = @MID))">
            <SelectParameters>
                <asp:QueryStringParameter Name="FactoryID" QueryStringField="F" Type="String" />
                <asp:QueryStringParameter Name="MID" QueryStringField="M" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
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
        document.getElementById("phone").style.display = "";
        document.getElementById("name").style.display = "none";
        canvas.height = 80;
        d.style.width = "320%";
    }

</script>

<script>
    var test = <%=test()%>;
    console.log(test);
    var ctx = document.getElementById("myAreaChart");
    /*
    var color = ["blue", "red", "yellow", 'green', 'purple', 'orange', 'black', 'pink',
        'brown', '#4D1F00', '#704214', '#006400', '#ffb6c1', '#c71585', '#f8f8ff', '#1e90ff',
        '#00bfff', '#7fffd4', '#228b22', '#B15BFF', '#F00078', '#D9FFFF', '#46A3FF', '#8C8C00'];
    */
    var barcolor = ["rgba(255, 206, 86, 0.8)", "rgba(75, 192, 192, 0.8)", "rgba(240, 0, 120, 0.8)", "rgba(70, 163, 255, 0.8)",
        "rgba(238, 232, 170, 0.8)", "rgba(34, 139, 34, 0.8)", "rgba(178, 34, 34, 0.8)", "rgba(199, 21, 133, 0.8)",
        "rgba(255, 182, 193, 0.8)", "rgba(112, 66, 20, 0.8)"];


    var myLineChart = new Chart(ctx, {
        type: 'bar',
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
                text: test[8][0].title,
                fontSize:16
            },
            scales: {
                xAxes: [{
                    barPercentage: 0.4,
                    type: 'time',
                    offset: true,
                    position: 'bottom',
                    time: {
                        unit: test[2][0].unit,
                        displayFormats: {
                            day: test[3][0].displayFormats,
                        },
                        format: "YYYY-MM-DD",
                        min: test[4][0].min,
                        max: test[5][0].max,
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
    //var count_data = <% Response.Write(Session["count_data"]);%>;
    //var TagName = <% Response.Write(Session["TagName"]);%>;
    //var datas = <% Response.Write(Session["datas"]);%>;
    //console.log(datas);
    const newDataSet = {
        type: 'line',
        yAxisID: 'A',
        label: test[6][0].b,
        lineTension: 0.3, //曲線幅度
        backgroundColor: 'blue', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
        borderColor: 'blue', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
        data: test[0],
        fill: false, //背景是否要填滿
        //pointStyle: 'line',
        pointRadius:3
    }
    myLineChart.data.datasets.push(newDataSet);
    const newData = {
        type: 'line',
        yAxisID: 'A',
        label: test[7][0].avg_b,
        lineTension: 0.3, //曲線幅度
        backgroundColor: 'red', // 折線點的顏色 可改成用"rgba(2,117,216,0.2)",
        borderColor: 'red', // 折線線的顏色 可改成用"rgba(2,117,216,0.2)",
        data: test[1],
        fill: false, //背景是否要填滿
        borderWidth:1
        //pointStyle: 'line',
    }
    myLineChart.options.scales.yAxes[1].display = false;
    myLineChart.data.datasets.push(newData);

    var c = 0; //調整顏色用
    for (var i = 0; i < 21; i+=2)
    {
        if (test[9+i][0] != null)
        {
            const newbar = {
                label: test[10 + i][0].name,
                yAxisID: 'B',
                backgroundColor: barcolor[c],
                borderColor: barcolor[c],
                data: test[9 + i],
                ticks: {
                    display: true
                }
            }
            myLineChart.data.datasets.push(newbar);
            c++;
            myLineChart.options.scales.yAxes[1].display = true;
        }
    }
    myLineChart.update();
</script>
</asp:Content>
