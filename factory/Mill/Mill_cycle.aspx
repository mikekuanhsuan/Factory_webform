<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Mill_cycle.aspx.cs" Inherits="factory.Mill.Mill_cycle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <script src="../js/jquery.ui.monthpicker.js"></script>
    <script src="../js/moment.min.js"></script>
    <script src="../js/Chart.min.js"></script>
    <script src="../js/hammerjs@2.0.8.js"></script>  
    <script src="../js/chartjs-plugin-zoom.js"></script>
    <script src="../js/freeze-table.js"></script>

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
            color: #FF3300;
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
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1"></asp:Label>
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
                    <asp:ImageButton ID="imgb_excel" runat="server" ImageUrl="~/img/excel.png" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="匯出EXCEL" OnClick="imgb_excel_Click"  />
                </div>
                <div class="col-auto">
                    <asp:Button ID="btn_chart" runat="server" Class="btn btn-success" Text="趨勢圖" Style="padding-top:3px;padding-bottom:3px" OnClick="btn_chart1_onClick" />
                    <asp:CheckBox ID="cb_Tagname" runat="server" AutoPostBack="True"/> Tagname
                </div>

                <div class="col-auto">
                    <strong>
                        <asp:Label ID="lb_error" runat="server" Text="請至少選擇一個或此欄位為空" CssClass="auto-style2" Visible="False"></asp:Label>
                    </strong>
                </div>
            </div>
        </div>

        <div class="xd table-responsive" style="margin-top:20px">
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="my table table-sm table-hover table-bordered text-center" DataKeyNames="FactoryID,Mill_ID,DDate,DataTime" DataSourceID="SDS1" Width="110%" OnDataBound="GV1_DataBound" style="margin-top: 0px">
                <Columns>
                    <asp:BoundField DataField="DTime" HeaderText="時間" SortExpression="DTime" />
                    <asp:TemplateField HeaderText="功率#1" SortExpression="Motor_PowerKW_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Motor_PowerKW_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_0" runat="server" />
                            <br />
                            功率<br />#1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_0" runat="server" Text='<%# Eval("Motor_PowerKW_A", "{0:N1}") == ""?"0":Eval("Motor_PowerKW_A", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="功率#2" SortExpression="Motor_PowerKW_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Motor_PowerKW_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_1" runat="server" />
                            <br />
                            功率<br />#2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_1" runat="server" Text='<%# Eval("Motor_PowerKW_B", "{0:N1}") == ""?"0":Eval("Motor_PowerKW_B", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="循環提運機" SortExpression="Bucket_Elevator_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Bucket_Elevator_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_2" runat="server" />
                            <br />
                            循環<br />提運機
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_2" runat="server" Text='<%# Eval("Bucket_Elevator_A", "{0:N1}") == ""?"0":Eval("Bucket_Elevator_A", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="O-SEPA選粉機電流" SortExpression="OSEPA_Current">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("OSEPA_Current") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_3" runat="server" />
                            <br />
                            O-SEPA<br />選粉機電流
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_3" runat="server" Text='<%# Eval("OSEPA_Current", "{0:N1}") == ""?"0":Eval("OSEPA_Current", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="O-SEPA選粉機轉速" SortExpression="OSEPA_RPM">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("OSEPA_RPM") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_4" runat="server" />
                            <br />
                            O-SEPA<br />選粉機轉速
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_4" runat="server" Text='<%# Eval("OSEPA_RPM", "{0:N1}") == ""?"0":Eval("OSEPA_RPM", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="袋式收塵機M1" SortExpression="Bag_Colletcot_M1">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Bag_Colletcot_M1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_5" runat="server" />
                            <br />
                            袋式<br />收塵機M1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_5" runat="server" Text='<%# Eval("Bag_Colletcot_M1", "{0:N1}") == ""?"0":Eval("Bag_Colletcot_M1", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="袋式收塵機M2" SortExpression="Bag_Colletcot_M2">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Bag_Colletcot_M2") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_6" runat="server" />
                            <br />
                            袋式<br />收塵機M2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_6" runat="server" Text='<%# Eval("Bag_Colletcot_M2", "{0:N1}") == ""?"0":Eval("Bag_Colletcot_M2", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="袋式收塵機S" SortExpression="Bag_Colletcot_S">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Bag_Colletcot_S") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_7" runat="server" />
                            <br />
                            袋式<br />收塵機S
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_7" runat="server" Text='<%# Eval("Bag_Colletcot_S", "{0:N1}") == ""?"0":Eval("Bag_Colletcot_S", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1料溫" SortExpression="TE_Mill_Air_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("TE_Mill_Air_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_8" runat="server" />
                            <br />
                            #1<br />料溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_8" runat="server" Text='<%# Eval("TE_Mill_Air_A", "{0:N1}") == ""?"0":Eval("TE_Mill_Air_A", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1氣溫" SortExpression="TE_Mill_RAW_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("TE_Mill_RAW_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_9" runat="server" />
                            <br />
                            #1<br />氣溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_9" runat="server" Text='<%# Eval("TE_Mill_RAW_A", "{0:N1}") == ""?"0":Eval("TE_Mill_RAW_A", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2料溫" SortExpression="TE_Mill_Air_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("TE_Mill_Air_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_10" runat="server" />
                            <br />
                            #2<br />料溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_10" runat="server" Text='<%# Eval("TE_Mill_Air_B", "{0:N1}") == ""?"0":Eval("TE_Mill_Air_B", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2氣溫" SortExpression="TE_Mill_RAW_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("TE_Mill_RAW_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_11" runat="server" />
                            <br />
                            #2<br />氣溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_11" runat="server" Text='<%# Eval("TE_Mill_RAW_B", "{0:N1}") == ""?"0":Eval("TE_Mill_RAW_B", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(溫度)風析機出口" SortExpression="TE_S_In">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("TE_S_In") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_12" runat="server" />
                            <br />
                            風析機<br />出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_12" runat="server" Text='<%# Eval("TE_S_In", "{0:N1}") == ""?"0":Eval("TE_S_In", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1磨出口" SortExpression="WP_Mill_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("WP_Mill_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_13" runat="server" />
                            <br />
                            #1<br />磨出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_13" runat="server" Text='<%# Eval("WP_Mill_A", "{0:N1}") == ""?"0":Eval("WP_Mill_A", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2磨出口" SortExpression="WP_Mill_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("WP_Mill_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_14" runat="server" />
                            <br />
                            #2<br />磨出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_14" runat="server" Text='<%# Eval("WP_Mill_B", "{0:N1}") == ""?"0":Eval("WP_Mill_B", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(風壓)風析機出口" SortExpression="WP_OSEPA">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("WP_OSEPA") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_15" runat="server" />
                            <br />
                            風析機<br />出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_15" runat="server" Text='<%# Eval("WP_OSEPA", "{0:N1}") == ""?"0":Eval("WP_OSEPA", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S系排風機入口" SortExpression="WP_BC_S_IN">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("WP_BC_S_IN") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_16" runat="server" />
                            <br />
                            S系排風機<br />入口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_16" runat="server" Text='<%# Eval("WP_BC_S_IN", "{0:N1}") == ""?"0":Eval("WP_BC_S_IN", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S系排風機壓差" SortExpression="WP_BC_S_DIFF">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("WP_BC_S_DIFF") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_17" runat="server" />
                            <br />
                            S系排風機<br />壓差
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_17" runat="server" Text='<%# Eval("WP_BC_S_DIFF", "{0:N1}") == ""?"0":Eval("WP_BC_S_DIFF", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機M系#1" SortExpression="OP_BC_M1">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("OP_BC_M1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_18" runat="server" />
                            <br />
                            收塵排風機<br />M系#1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_18" runat="server" Text='<%# Eval("OP_BC_M1", "{0:N1}") == ""?"0":Eval("OP_BC_M1", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機M系#2" SortExpression="OP_BC_M2">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("OP_BC_M2") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_19" runat="server" />
                            <br />
                            收塵排風機<br />M系#2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_19" runat="server" Text='<%# Eval("OP_BC_M2", "{0:N1}") == ""?"0":Eval("OP_BC_M2", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機S" SortExpression="OP_BC_S">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("OP_BC_S") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_20" runat="server" />
                            <br />
                            收塵排風機<br />S
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_20" runat="server" Text='<%# Eval("OP_BC_S", "{0:N1}") == ""?"0":Eval("OP_BC_S", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="秤#_1" SortExpression="W_M1_A_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("W_M1_A_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_21" runat="server" />
                            <br />
                            秤<br /> #_1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_21" runat="server" Text='<%# Eval("W_M1_A_P", "{0:N1}") == ""?"0":Eval("W_M1_A_P", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="秤#_2" SortExpression="W_M1_B_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("W_M1_B_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_22" runat="server" />
                            <br />
                            秤<br /> #_2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_22" runat="server" Text='<%# Eval("W_M1_B_P", "{0:N1}") == ""?"0":Eval("W_M1_B_P", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1產量">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <br />
                            #1<br />產量
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_Yield1" runat="server" Text='<%# 0 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="秤#2_1" SortExpression="W_M2_A_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox24" runat="server" Text='<%# Bind("W_M2_A_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_23" runat="server" />
                            <br />
                            秤<br /> #2_1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_23" runat="server" Text='<%# Eval("W_M2_A_P", "{0:N1}") == ""?"0":Eval("W_M2_A_P", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="秤#2_2" SortExpression="W_M2_B_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox25" runat="server" Text='<%# Bind("W_M2_B_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_24" runat="server" />
                            <br />
                            秤<br /> #2_2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_24" runat="server" Text='<%# Eval("W_M2_B_P", "{0:N1}") == ""?"0":Eval("W_M2_B_P", "{0:N1}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2產量">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <br />
                            #2<br />產量
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_Yield2" runat="server" Text="<%# 0 %>"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [G_Milling_P_Machine]"></asp:SqlDataSource>
        </div>
    </div>
    <script>
        var d = document.getElementById('ContentPlaceHolder1_GV1');
        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            document.getElementById("phone").style.display = "";
            document.getElementById("name").style.display = "none";
            $(document).ready(function () {
                //抓取GV下的DIV賦予CLASS名稱
                var x = document.querySelector('div.xd div');
                if (x != null) {
                    x.className = "basic";
                    $(".basic").freezeTable();
                    $(".basic").freezeTable({
                        'scrollable': true,
                    });
                }
            });
        }
        
        var b = document.getElementById("ContentPlaceHolder1_cb_Tagname");
        if (b.checked == true) {
            d.style.width = "150%";
            var name = <%=cb_name()%>;
            var v = name.split(',');
            var table = document.getElementById("ContentPlaceHolder1_GV1");
            var row = table.insertRow(1);
            var cell = row.insertCell(0);
            cell.innerHTML = "";
            for (var i = 0; i < v.length-2; i++) {
                cell = row.insertCell(1 + i);
                cell.innerHTML = v[i];
            };
            cell = row.insertCell(24);
            cell.innerHTML = "";
            cell = row.insertCell(25);
            cell.innerHTML = v[23];
            cell = row.insertCell(26);
            cell.innerHTML = v[24];
        }
    </script>
</asp:Content>
