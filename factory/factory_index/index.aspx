<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="factory.factory_index.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <script src="../js/jquery.ui.monthpicker.js"></script>
    <script src="../js/moment.min.js"></script>
    <script src="../js/Chart.min.js"></script>
    <script src="../js/hammerjs@2.0.8.js"></script>
    <script src="../js/chartjs-plugin-zoom.js"></script>
    <script src="../js/chartjs-plugin-datalabels.min.js"></script>
    <style>
        .mar-r {
            margin-right:10px;
        }

        #ContentPlaceHolder1_imgb_p:hover,#ContentPlaceHolder1_imgb_n:hover{
            background:gainsboro;
        }
        .auto-style1 {
            font-size: 20px;
        }
        .auto-style2 {
            font-size: larger;
        }
        </style>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
        <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
            <strong>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style2">首頁 ＞</asp:HyperLink>
                <asp:HyperLink ID="hyl_factory" runat="server" ForeColor="Maroon" CssClass="auto-style2"></asp:HyperLink>
                <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style2">用電量</asp:Label>
            </strong> 
        </div>

            <div class="row">
            <div id="name" class="col-auto">
                <strong>
                    <asp:Label ID="lb_factory" runat="server" CssClass="auto-style2"></asp:Label>
                </strong>
            </div>
            <div class="col-auto">
                <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇年月:" TextMode="SingleLine" Width="130px"  autocomplete="off" AutoPostBack="True" OnTextChanged="tb_SDATE_TextChanged" CssClass="mar-r" ></asp:TextBox>
                <asp:ImageButton ID="imgb_p" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;transform: rotate(-180deg);border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="上一個月" OnClick="imgb_p_Click" CssClass="mar-r" />
                <asp:ImageButton ID="imgb_n" runat="server" ImageUrl="~/img/caret-right-fill.svg" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="下一個月" CssClass="mar-r" OnClick="imgb_n_Click"/>
                <asp:ImageButton ID="imgb_pdf" runat="server" ImageUrl="~/img/pdf.png" Width="30px" Style="vertical-align:bottom" data-placement="right" data-toggle="tooltip" title="匯出PDF" OnClick="imgb_pdf_Click" Visible="False" />
            </div>
        </div>
        </div>

        <div class="row">
            <div class="col-xxl-5 col-lg-7 col-12">
                <div class="card" style="margin-top:20px;background:rgb(204,255,255)">
                    <div class="row">
                        <div class="col-6">
                            <span class="auto-style1" style="padding-left:10px">台電</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-auto w" style="padding-left:60px">
                            <span class="auto-style1" style="padding-left:10px">契約內容</span>
                        </div>
                        <div class="col-sm-3 col-3 q text-right">
                            <strong>
                                <asp:Label ID="lb_contract" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                        <div class="col-auto k">
                            <strong>
                                <asp:Label ID="Label3" runat="server" class="auto-style1" Text="KW"></asp:Label>
                            </strong>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-auto w" style="padding-left:110px">
                            <span class="auto-style1">尖峰</span>
                        </div>
                        <div class="col-sm-3 col-3 q text-right">
                            <strong>
                                <asp:Label ID="lb_rush" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                        <div class="col-sm-2 col-lg-auto col-auto k">
                            <strong>
                                <asp:Label ID="Label7" runat="server" class="auto-style1" Text="KWH"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-auto">
                            <strong>
                                <asp:Label ID="lb_rush_p" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-auto w" style="padding-left:90px">
                            <span class="auto-style1">半尖峰</span>
                        </div>
                        <div class="col-sm-3 col-3 q text-right">
                            <strong>
                                <asp:Label ID="lb_half" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                        <div class="col-sm-2 col-lg-auto col-auto k">
                            <strong>
                                <asp:Label ID="Label8" runat="server" class="auto-style1" Text="KWH"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-auto">
                            <strong>
                                <asp:Label ID="lb_half_p" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-auto w" style="padding-left:50px">
                            <span class="auto-style1">週六半尖峰</span>
                        </div>
                        <div class="col-sm-3 col-3 q text-right">
                            <strong>
                                <asp:Label ID="lb_sd" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                        <div class="col-sm-2 col-lg-auto col-auto k">
                            <strong>
                                <asp:Label ID="Label11" runat="server" class="auto-style1" Text="KWH"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-auto">
                            <strong>
                                <asp:Label ID="lb_sd_p" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-auto w" style="padding-left:110px">
                            <span class="auto-style1">離峰</span>
                        </div>
                        <div class="col-sm-3 col-3 q text-right">
                            <strong>
                                <asp:Label ID="lb_off" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                        <div class="col-sm-2 col-lg-auto col-auto k">
                            <strong>
                                <asp:Label ID="Label13" runat="server" class="auto-style1" Text="KWH"></asp:Label>
                            </strong>
                        </div>
                        <div class="col-auto">
                            <strong>
                                <asp:Label ID="lb_off_p" runat="server" class="auto-style1" Text=""></asp:Label>
                            </strong>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xxl-6  col-lg-5 col-12">
                <div class="card" style="margin-top:20px;background:rgb(204,255,204);height:182px">
                    <div class="auto-style1" style="padding-left:10px">
                        <asp:Label ID="lb_power_name" runat="server" Text="太陽能"></asp:Label>
                    </div>
                        <div class="row auto-style1">
                            <div class="col-sm-4 col-5 text-right">
                                建置
                            </div>
                            <div class="col-3 text-right" style="padding:0px">
                                <strong>
                                    <asp:Label ID="lb_sys" runat="server" Text=""></asp:Label>
                                </strong>
                            </div>
                            <div class="col-auto">
                                <strong>
                                    <asp:Label ID="Label14" runat="server" Text="KW"></asp:Label>
                                </strong>
                            </div>
                        </div>

                        <div class="row auto-style1">
                            <div class="col-sm-4 col-5 text-right">
                                發電量
                            </div>
                            <div class="col-3 text-right" style="padding:0px">
                                <strong>
                                    <asp:Label ID="lb_energy" runat="server"  Text=""></asp:Label>
                                </strong>
                            </div>
                            <div class="col-auto">
                                <strong>
                                    <asp:Label ID="Label16" runat="server"  Text="KWH"></asp:Label>
                                </strong>
                            </div>
                        </div>
                    <div runat="server" id="zb">
                        <div class="auto-style1" style="padding-left:10px">
                            <asp:Label ID="lb_zb_name" runat="server" Text="太陽能-線西"></asp:Label>
                        </div>
                            <div class="row auto-style1">
                                <div class="col-sm-4 col-5 text-right">
                                    建置
                                </div>
                                <div class="col-3" style="padding:0px">
                                    <strong>
                                        <asp:Label ID="lb_sys2" runat="server" Text=""></asp:Label>
                                    </strong>
                                </div>
                                <div class="col-auto">
                                    <strong>
                                        <asp:Label ID="Label17" runat="server" Text="KW"></asp:Label>
                                    </strong>
                                </div>
                            </div>

                            <div class="row auto-style1">
                                <div class="col-sm-4 col-5 text-right">
                                    發電量
                                </div>
                                <div class="col-3" style="padding:0px">
                                    <strong>
                                        <asp:Label ID="lb_energy_2" runat="server"  Text=""></asp:Label>
                                    </strong>
                                </div>
                                <div class="col-auto">
                                    <strong>
                                        <asp:Label ID="Label19" runat="server"  Text="KWH"></asp:Label>
                                    </strong>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="div_power" runat="server" style="margin-top:20px">
            <div class="row">
                <div class="col-xxl-5 col-lg-7 col-12 ">
                    <div id="link_d" runat="server" class="col-12 card" style="background-color:seashell;" data-placement="right" data-toggle="tooltip" title="連結到用電量">
                        <div class="row" style="height:45px;align-content:center">
                            <div class="col-xxl-3 col-sm-4 col-5">
                                <strong>
                                    <asp:Label ID="Label1" runat="server" Text="廠內用電量" CssClass="auto-style1"></asp:Label>
                                </strong>
                            </div>

                            <div class="col-sm-4 col-auto text-right">
                                <strong>
                                    <asp:Label ID="lb_power" runat="server" CssClass="auto-style1" Text=""></asp:Label>
                                </strong>
                            </div>
                            <div class="col-auto">
                                <strong>
                                    <asp:Label ID="Label4" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                </strong>
                            </div>
                        </div>
                    </div>

                    <div id="d" runat="server" style="margin-top:10px;margin-bottom:20px">
                        <div class="row" style="margin:0px"> 
                            <div class="col-12" style="margin-top:10px;padding-bottom:15px;border:1px solid rgba(0, 0, 0, 0.125)">
                                <div class="row">
                                    <div id="Div1" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="Label5" runat="server" Text="空壓機" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_air" runat="server" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label10" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d1" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_n1" runat="server" Text="#1 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p1" runat="server" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label2" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d2" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_n2" runat="server" Text="#2 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p2" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label6" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d3" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_n3" runat="server" Text="#3 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p3" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label9" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d4" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_n4" runat="server" Text="#4 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p4" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label12" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d5" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_5" runat="server" Text="#5 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p5" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label15" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d6" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_6" runat="server" Text="#6 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p6" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label18" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d7" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_7" runat="server" Text="#7 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p7" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label21" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="d8" runat="server" class="col-12" style="margin-top:15px">
                                        <div class="card" style="background-color:cornsilk">
                                            <div class="row" style="height:45px;align-content:center">
                                                <div class="col-xxl-3 col-sm-4 col-5">
                                                    <strong>
                                                        <asp:Label ID="lb_8" runat="server" Text="#8 1050" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <strong>
                                                        <asp:Label ID="lb_p8" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>

                                                <div class="col-3">
                                                    <strong>
                                                        <asp:Label ID="Label24" runat="server" Text="KWH" CssClass="auto-style1"></asp:Label>
                                                    </strong>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xxl-6  col-lg-5 col-12" style ="margin-bottom:10px">
                    <canvas class="card" id="myCanvas" width="100%" height="63"></canvas>
                </div>
            </div>
        </div>
    

<script>
    var canvas = document.getElementsByTagName('canvas')[0];
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        canvas.height = 85;
        document.getElementById("phone").style.display = "";
        document.getElementById("name").style.display = "none";

        //契約內容
        document.getElementsByClassName('w')[0].style.paddingLeft = "35px"
        //尖峰
        document.getElementsByClassName('w')[1].style.paddingLeft = "85px"
        //半尖峰
        document.getElementsByClassName('w')[2].style.paddingLeft = "65px"
        //周六半尖峰
        document.getElementsByClassName('w')[3].style.paddingLeft = "25px"
        //離峰
        document.getElementsByClassName('w')[4].style.paddingLeft = "85px"
        
        for (var i = 0; i < 5; i++){
            //資料
            document.getElementsByClassName('q')[i].style.padding = "0px"
            //KWH
            document.getElementsByClassName('k')[i].style.paddingRight = "0px"
        }

       
    }
    var doughnut = <%=doughnut()%>;
    if (doughnut != "1") {
        console.log(doughnut);
        //var x = [15, 15, 15, 15, 15, 15, 10, 10, 10];
        var ctx = document.getElementById('myCanvas').getContext('2d');
        var myData = {
            type: 'doughnut',
            data: {
                labels:
                    doughnut[1],
                datasets: [
                    {
                        data: doughnut[0],
                        fill: false,
                        backgroundColor: ["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgba(34, 139, 34, 0.8)", "rgba(255, 182, 193, 0.8)", "rgba(178, 34, 34, 0.8)", "rgba(70, 163, 255, 0.8)", "#00E3E3", "#8A2BE2"],
                        borderColor: ["rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgba(34, 139, 34, 0.8)", "rgba(255, 182, 193, 0.8)", "rgba(178, 34, 34, 0.8)", "rgba(70, 163, 255, 0.8)", "#00E3E3", "#8A2BE2"],
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: '用電量',
                    fontSize: 16,
                },
                tooltips: { //滑鼠移到點上顯示資訊
                    enabled: false,
                    intersect: false,
                    mode: 'index',
                },
                legend: {
                    display: true,
                    position: "left",
                    align: "start",
                    labels: {
                        boxWidth: 20,
                        boxHeight: 80,
                        fontStyle: 'bold',
                        fontSize: 18,
                    },

                },
                plugins: {
                    datalabels: {
                        color: '#ffffff',
                        formatter: function (value) {
                            return Math.round(value) + '%';
                        },
                        font: {
                            weight: 'bold',
                            size: 16,
                        }
                    }
                }
            }
        };
        var myChart = new Chart(ctx, myData);
    }
</script>

</asp:Content>
