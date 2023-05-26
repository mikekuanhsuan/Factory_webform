<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tag_report_KY.aspx.cs" Inherits="factory.Tag_report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>磨機報表－觀音廠</title>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
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
                    dateFormat: "yy-mm-dd ",
                    firstDay: 1,
                    isRTL: false,
                    showMonthAfterYear: true,
                    yearSuffix: "年",
                });
            });
        </script>
    <style>
        .table tbody tr td{
            vertical-align: middle;
        }
        .auto-style1 {
            height: 60px;
        }

    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Label ID="Label5" runat="server" Text="廠區:"></asp:Label>
        <asp:DropDownList ID="ddl_fty" runat="server" DataSourceID="sds2" DataTextField="FactoryName" DataValueField="FactoryID"></asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Text="選擇日期:"></asp:Label>
        <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇日期:" TextMode="SingleLine" Width="130px" required autocomplete="off" onkeydown="return false;"></asp:TextBox>
        <asp:Button ID="btn_confirm" runat="server" Text="確定" class="btn btn-primary" Height="32px" OnClick="btn_confirm_Click" />
        <asp:SqlDataSource ID="sds2" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory] ORDER BY [aOrder]"></asp:SqlDataSource>
    </div>
    <div class="table-responsive">
        <table style="width:140%; margin:0" class="table table-sm table-hover table-bordered text-center">
            <tr>
                <td rowspan="6" style="width:28px">時間</td>
                <td colspan="4">1050KW馬達</td>
                <td colspan="6">電流(AMP)</td>
                <td colspan="6">夾鉗式流量計(%)</td>
                <td colspan="9">溫度℃</td>
                <td colspan="10">磨機馬達及軸承溫度℃</td>
                <td colspan="5">風壓(MMWG)</td>
                <td colspan="3">轉速(RPM)</td>
                <td colspan="6">#1秤飼機</td>
                <td rowspan="5" style="width:44px">添<br>加<br>劑<br>飼<br>料<br>量</td>
                <td rowspan="5" style="width:37px">產<br>量</td>
                <td colspan="6">#2秤飼機</td>
                <td rowspan="5" style="width:44px">添<br>加<br>劑<br>飼<br>料<br>量</td>
                <td rowspan="5" style="width:37px">產<br>量</td>
                <td colspan="3">成品品質</td>
            </tr>
            <tr>
                <td colspan="2" rowspan="3">電流</td>
                <td rowspan="3">功率</td>
                <td rowspan="3">功率</td>
                <td rowspan="5" style="width:48px">循<br>環<br>提<br>運<br>機</td>
                <td colspan="2" rowspan="3">O-SEPA</td>
                <td colspan="3" rowspan="3" >收塵排風機</td>
                <td colspan="4">耳軸承</td>
                <td rowspan="5" style="width:37px">主<br>減<br>速<br>機</td>
                <td rowspan="5" style="width:37px">選<br>粉<br>機</td>
                <td colspan="4">耳軸承</td>
                <td colspan="2">磨機</td>
                <td colspan="2">磨機</td>
                <td rowspan="5" style="width:37px">風<br>析<br>機<br>出<br>口</td>
                <td colspan="5">#1Mill</td>
                <td colspan="5">#2Mill</td>
                <td colspan="2" rowspan="4">磨<br>機<br>出<br>口</td>
                <td rowspan="5" style="width:37px">風<br>析<br>機<br>出<br>口</td>
                <td colspan="2" rowspan="2" class="text-center">S系排風機</td>
                <td colspan="3" rowspan="2" class="text-center">收塵排風機</td>
                <td colspan="3">熟料/爐石</td>
                <td colspan="3">石膏</td>
                <td colspan="3">熟料/爐石</td>
                <td colspan="3">石膏</td>
                <td rowspan="4" style="width:37px">水<br>分</td>
                <td rowspan="4" style="width:53px">比<br>表<br>面<br>機</td>
                <td rowspan="4" style="width:37px">篩<br>餘</td>
            </tr>
            <tr>
                <td colspan="2">#1Mill</td>
                <td colspan="2">#2Mill</td>
                <td colspan="2">#1Mill</td>
                <td colspan="2">#2Mill</td>
                <td colspan="2">#1Mill</td>
                <td colspan="2">#2Mill</td>
                <td rowspan="4" style="width:29px">1馬達線圈R</td>
                <td rowspan="4" style="width:29px">2馬達線圈S</td>
                <td rowspan="4" style="width:29px">3馬達線圈T</td>
                <td rowspan="4" style="width:29px">4負載端軸承</td>
                <td rowspan="4" style="width:29px">5無負載端軸承</td>
                <td rowspan="4" style="width:29px">1馬達線圈R</td>
                <td rowspan="4" style="width:29px">2馬達線圈S</td>
                <td rowspan="4" style="width:29px">3馬達線圈T</td>
                <td rowspan="4" style="width:29px">4負載端軸承</td>
                <td rowspan="4" style="width:29px">5無負載端軸承</td>
                <td rowspan="4" style="width:37px">飼<br>量<br>調<br>節<br>器</td>
                <td rowspan="3" style="width:35px">積<br>數<br>器</td>
                <td rowspan="3" style="width:35px">飼<br>料<br>量</td>
                <td rowspan="4" style="width:37px">飼<br>量<br>調<br>節<br>器</td>
                <td rowspan="3" style="width:35px">積<br>數<br>器</td>
                <td rowspan="3" style="width:35px">飼<br>料<br>量</td>
                <td rowspan="4" style="width:37px">飼<br>量<br>調<br>節<br>器</td>
                <td rowspan="3" style="width:37px">積<br>數<br>器</td>
                <td rowspan="3" style="width:37px">飼<br>料<br>量</td>
                <td rowspan="4" style="width:37px">飼<br>量<br>調<br>節<br>器</td>
                <td rowspan="3" style="width:37px">積<br>數<br>器</td>
                <td rowspan="3" style="width:37px">飼<br>料<br>量</td>
            </tr>
            <tr>
                <td rowspan="3" style="width:37px">入口端</td>
                <td rowspan="3" style="width:37px">出口端</td>
                <td rowspan="3" style="width:37px">入口端</td>
                <td rowspan="3" style="width:37px">出口端</td>
                <td rowspan="3" style="width:37px">入口端</td>
                <td rowspan="3" style="width:37px">出口端</td>
                <td rowspan="3" style="width:37px">入口端</td>
                <td rowspan="3" style="width:37px">出口端</td>
                <td rowspan="3" style="width:37px">料溫</td>
                <td rowspan="3" style="width:37px">氣溫</td>
                <td rowspan="3" style="width:37px">料溫</td>
                <td rowspan="3" style="width:37px">氣溫</td>
                <td rowspan="3" class="auto-style1" style="width:42px">入口</td>
                <td rowspan="3" style="width:42px">壓差</td>
                <td rowspan="2">M系</td>
                <td rowspan="2">M系</td>
                <td rowspan="3" class="auto-style2" style="width:47px">S系</td>
            </tr>
            <tr>
                <td>#1</td>
                <td>#2</td>
                <td>#1</td>
                <td>#2</td>
                <td rowspan="2" style="width:37px">A</td>
                <td rowspan="2" style="width:44px">RPM</td>
                <td>M</td>
                <td>M</td>
                <td rowspan="2" class="auto-style2" style="width:37px">S</td>
                
            </tr>
            <tr>
                <td class="auto-style3" style="width:40px">A</td>
                <td class="auto-style3" style="width:40px">A</td>
                <td class="auto-style3" style="width:40px">KW</td>
                <td class="auto-style3" style="width:40px">KW</td>
                <td class="auto-style3" style="width:37px">#1</td>
                <td class="auto-style3" style="width:37px">#2</td>
                <td class="auto-style3" style="width:37px">#1</td>
                <td class="auto-style3" style="width:37px">#2</td>
                <td class="auto-style3" style="width:37px">#1</td>
                <td class="auto-style3" style="width:37px">#2</td>
                <td colspan="2" class="auto-style3">(T/H)</td>
                <td colspan="2" class="auto-style3">(T/H)</td>
                <td class="auto-style3">RPM</td>
                <td class="auto-style3">T/H</td>
                <td colspan="2" class="auto-style3">(T/H)</td>
                <td colspan="2" class="auto-style3">(T/H)</td>
                <td class="auto-style3">RPM</td>
                <td class="auto-style3">T/H</td>
                <td class="auto-style3" style="width:37px">%</td>
                <td class="auto-style3">(<span style="color: rgb(0, 0, 0); font-family: &quot;Open Sans&quot;, Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(247, 247, 247); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial; display: inline !important; float: none;">㎡/kg</span>)</td>
                <td class="auto-style3" style="width:37px">(%)</td>
            </tr>
            <tr>
                <td colspan="2">操作<br>標準</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered" Width="140%" ShowHeader="False">
                <Columns>
                    <asp:BoundField DataField="0" HeaderText="0" >
                    <ItemStyle Width="28.94px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="1" HeaderText="1" >
                    <HeaderStyle Width="30px" />
                    <ItemStyle Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="2" HeaderText="2" >
                    <ItemStyle Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="3" HeaderText="3" >
                    <ItemStyle Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="4" HeaderText="4" >
                    <ItemStyle Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="5" HeaderText="5" >
                    <ItemStyle Width="48px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="6" HeaderText="6" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="7" HeaderText="7" >
                    <ItemStyle Width="44px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="8" HeaderText="8" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="9" HeaderText="9" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="10" HeaderText="10" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="11" HeaderText="11" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="12" HeaderText="12" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="13" HeaderText="13" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="14" HeaderText="14" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="15" HeaderText="15" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="16" HeaderText="16" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="17" HeaderText="17" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="18" HeaderText="18" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="19" HeaderText="19" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="20" HeaderText="20" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="21" HeaderText="21" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="22" HeaderText="22" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="23" HeaderText="23" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="24" HeaderText="24" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="25" HeaderText="25" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="26" HeaderText="26" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="27" HeaderText="27" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="28" HeaderText="28" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="29" HeaderText="29" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="30" HeaderText="30" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="31" HeaderText="31" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="32" HeaderText="32" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="33" HeaderText="33" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="34" HeaderText="34" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="35" HeaderText="35" >
                    <ItemStyle Width="29px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="36" HeaderText="36" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="37" HeaderText="37" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="38" HeaderText="38" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="39" HeaderText="39" >
                    <ItemStyle Width="42px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="40" HeaderText="40" >
                    <ItemStyle Width="42px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="41" HeaderText="41" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="42" HeaderText="42" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="43" HeaderText="43" >
                    <ItemStyle Width="47px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="44" HeaderText="44" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="45" HeaderText="45" >
                    <ItemStyle Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="46" HeaderText="46" >
                    <ItemStyle Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="47" HeaderText="47" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="48" HeaderText="48" >
                    <ItemStyle Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="49" HeaderText="49" >
                    <ItemStyle Width="35px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="50" HeaderText="50" >
                    <ItemStyle Width="44px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="51" HeaderText="51" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="52" HeaderText="52" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="53" HeaderText="53" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="54" HeaderText="54" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="55" HeaderText="55" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="56" HeaderText="56" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="57" HeaderText="57" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="58" HeaderText="58" >
                    <ItemStyle Width="44px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="59" HeaderText="59" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="60" HeaderText="60" >
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="61" HeaderText="61" >
                    <ItemStyle Width="53px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="62" HeaderText="62" >
                    
                    <ItemStyle Width="37px" />
                    </asp:BoundField>
                    
                </Columns>
            </asp:GridView>
</div>
  
    </asp:Content>
