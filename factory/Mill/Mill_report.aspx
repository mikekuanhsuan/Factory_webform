<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Mill_report.aspx.cs" Inherits="factory.Mill.Mill_report" %>
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
                    <asp:ImageButton ID="imgb_excel" runat="server" ImageUrl="~/img/excel.png" Width="30px" Style="vertical-align:bottom;border:1px solid rgba(0, 0, 0, 0.125);" data-placement="right" data-toggle="tooltip" title="匯出EXCEL" OnClick="imgb_excel_Click" />
                </div>
                <div class="col-auto">
                    <asp:Button ID="btn_chart" runat="server" Class="btn btn-success" Text="趨勢圖" Style="padding-top:3px;padding-bottom:3px" OnClick="btn_chart_Click" />
                </div>
                <div class="col-auto">
                    <asp:Button ID="btn_add" runat="server" Class="btn btn-primary" Text="新增檢測數據" Style="padding-top:3px;padding-bottom:3px" OnClick="btn_add_Click" />
                </div>
                <div class="col-auto">
                    <strong>
                        <asp:Label ID="lb_error" runat="server" Text="請至少選擇一個或此欄位為空" CssClass="auto-style2" Visible="False"></asp:Label>
                    </strong>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top:10px">
            <div class="col-12"> 
                <ul class="nav nav-tabs">
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Power" runat="server" class="nav-link">電流/功率</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Temp" runat="server" class="nav-link">溫度</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Wind" runat="server" class="nav-link">風壓/轉速</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Fd" runat="server" class="nav-link">秤飼機</asp:HyperLink>
                    </li>
                    <li class="nav-item">
                        <asp:HyperLink ID="hyl_Quality" runat="server" class="nav-link">成品品質</asp:HyperLink>
                    </li>
                </ul>
            </div>
        </div>

        <div id="G1" runat="server" class="xd table-responsive" style="margin-top:5px">
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="my table table-sm table-hover table-bordered text-center" DataKeyNames="FactoryID,Mill_ID,DDate,DataTime" DataSourceID="SDS1" OnRowCreated="GV1_RowCreated" style="margin-top: 0px" OnDataBound="GV1_DataBound">
                <Columns>
                    <asp:BoundField DataField="FactoryID" HeaderText="FactoryID" ReadOnly="True" SortExpression="FactoryID" Visible="False" />
                    <asp:BoundField DataField="Mill_ID" HeaderText="Mill_ID" ReadOnly="True" SortExpression="Mill_ID" Visible="False" />
                    <asp:BoundField DataField="DDate" HeaderText="DDate" ReadOnly="True" SortExpression="DDate" Visible="False" />
                    <asp:BoundField DataField="DataTime" HeaderText="DataTime" ReadOnly="True" SortExpression="DataTime" Visible="False" />
                    <asp:BoundField DataField="DTime" HeaderText="時間" SortExpression="DTime" >
                    <ItemStyle Width="45px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="磨機電流A" SortExpression="Motor_Current_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Motor_Current_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_0" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            磨機電流A
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_0" runat="server" Text='<%# Eval("Motor_Current_A", "{0:N2}") == ""?"0":Eval("Motor_Current_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="磨機電流B" SortExpression="Motor_Current_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Motor_Current_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_1" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            磨機電流B
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_1" runat="server" Text='<%# Eval("Motor_Current_B", "{0:N2}") == ""?"0":Eval("Motor_Current_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="磨機功率A" SortExpression="Motor_PowerKW_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Motor_PowerKW_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_2" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            磨機功率A
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_2" runat="server" Text='<%# Eval("Motor_PowerKW_A", "{0:N2}") == ""?"0":Eval("Motor_PowerKW_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="磨機功率B" SortExpression="Motor_PowerKW_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Motor_PowerKW_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_3" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            磨機功率B
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_3" runat="server" Text='<%# Eval("Motor_PowerKW_B", "{0:N2}") == ""?"0":Eval("Motor_PowerKW_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="循環提運機" SortExpression="Bucket_Elevator_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Bucket_Elevator_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_4" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            循環提運機
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_4" runat="server" Text='<%# Eval("Bucket_Elevator_A", "{0:N2}") == ""?"0":Eval("Bucket_Elevator_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="入庫提運機" SortExpression="Bucket_Elevator_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("Bucket_Elevator_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_5" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            入庫提運機<br />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_5" runat="server" Text='<%# Eval("Bucket_Elevator_B", "{0:N2}") == ""?"0":Eval("Bucket_Elevator_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="O-SEPA選粉機電流" SortExpression="OSEPA_Current">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("OSEPA_Current") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_6" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            O-SEPA選粉機電流
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_6" runat="server" Text='<%# Eval("OSEPA_Current", "{0:N2}") == ""?"0":Eval("OSEPA_Current", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="O-SEPA選粉機轉速" SortExpression="OSEPA_RPM">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("OSEPA_RPM") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_7" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            O-SEPA選粉機轉速
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_7" runat="server" Text='<%# Eval("OSEPA_RPM", "{0:N2}") == ""?"0":Eval("OSEPA_RPM", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="袋式收塵機M1" SortExpression="Bag_Colletcot_M1">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("Bag_Colletcot_M1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_8" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            袋式收塵機M1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_8" runat="server" Text='<%# Eval("Bag_Colletcot_M1", "{0:N2}") == ""?"0":Eval("Bag_Colletcot_M1", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="袋式收塵機M2" SortExpression="Bag_Colletcot_M2">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("Bag_Colletcot_M2") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_9" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            袋式收塵機M2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_9" runat="server" Text='<%# Eval("Bag_Colletcot_M2", "{0:N2}") == ""?"0":Eval("Bag_Colletcot_M2", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="袋式收塵機S" SortExpression="Bag_Colletcot_S">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("Bag_Colletcot_S") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_10" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            袋式收塵機S
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_10" runat="server" Text='<%# Eval("Bag_Colletcot_S", "{0:N2}") == ""?"0":Eval("Bag_Colletcot_S", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="耳軸承#1入口" SortExpression="TE_Mill_Bearing_In_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox12" runat="server" Text='<%#  Bind("TE_Mill_Bearing_In_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_11" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            耳軸承#1入口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_11" runat="server" Text='<%# Eval("TE_Mill_Bearing_In_A", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_In_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="耳軸承#1出口" SortExpression="TE_Mill_Bearing_Out_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("TE_Mill_Bearing_Out_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_12" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            耳軸承#1出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_12" runat="server" Text='<%# Eval("TE_Mill_Bearing_Out_A", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_Out_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="潤滑油#1入口">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("TE_Mill_Bearing_Oil_In_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_13" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            潤滑油#1入口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_13" runat="server" Text='<%# Eval("TE_Mill_Bearing_Oil_In_A", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_Oil_In_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="潤滑油#1出口">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("TE_Mill_Bearing_Oil_Out_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_14" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            潤滑油#1出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_14" runat="server" Text='<%# Eval("TE_Mill_Bearing_Oil_Out_A", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_Oil_Out_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="耳軸承#2入口" SortExpression="TE_Mill_Bearing_In_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("TE_Mill_Bearing_In_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_15" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            耳軸承#2入口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_15" runat="server" Text='<%# Eval("TE_Mill_Bearing_In_B", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_In_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="耳軸承#2出口" SortExpression="TE_Mill_Bearing_Out_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("TE_Mill_Bearing_Out_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_16" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            耳軸承#2出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_16" runat="server" Text='<%# Eval("TE_Mill_Bearing_Out_B", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_Out_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="潤滑油#2入口">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("TE_Mill_Bearing_Oil_In_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_17" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            潤滑油#2入口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_17" runat="server" Text='<%# Eval("TE_Mill_Bearing_Oil_In_B", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_Oil_In_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="潤滑油#2出口">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("TE_Mill_Bearing_Oil_Out_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_18" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            潤滑油#2出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_18" runat="server" Text='<%# Eval("TE_Mill_Bearing_Oil_Out_B", "{0:N2}") == ""?"0":Eval("TE_Mill_Bearing_Oil_Out_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1&lt;br&gt;料溫" SortExpression="TE_Mill_RAW_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("TE_Mill_RAW_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_19" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1<br />料溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_19" runat="server" Text='<%# Eval("TE_Mill_RAW_A", "{0:N2}") == ""?"0":Eval("TE_Mill_RAW_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1&lt;br&gt;氣溫" SortExpression="TE_Mill_Air_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("TE_Mill_Air_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_20" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1 <br />氣溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_20" runat="server" Text='<%# Eval("TE_Mill_Air_A", "{0:N2}") == ""?"0":Eval("TE_Mill_Air_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2&lt;br&gt;料溫" SortExpression="TE_Mill_RAW_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("TE_Mill_RAW_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_21" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2 <br />料溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_21" runat="server" Text='<%# Eval("TE_Mill_RAW_B", "{0:N2}") == ""?"0":Eval("TE_Mill_RAW_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2&lt;br&gt;氣溫" SortExpression="TE_Mill_Air_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("TE_Mill_Air_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_22" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2 <br />氣溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_22" runat="server" Text='<%# Eval("TE_Mill_Air_B", "{0:N2}") == ""?"0":Eval("TE_Mill_Air_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="風析機出口" SortExpression="TE_S_In">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox24" runat="server" Text='<%# Bind("TE_S_In") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_23" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            風析機出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_23" runat="server" Text='<%# Eval("TE_S_In", "{0:N2}") == ""?"0":Eval("TE_S_In", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="成品入庫料溫" SortExpression="TE_Product">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox25" runat="server" Text='<%# Bind("TE_Product") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_24" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            成品入庫料溫
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_24" runat="server" Text='<%# Eval("TE_Product", "{0:N2}") == ""?"0":Eval("TE_Product", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1馬達線圈R" SortExpression="TE_Motor_1_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("TE_Motor_1_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_25" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1馬達線圈R
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_25" runat="server" Text='<%# Eval("TE_Motor_1_A", "{0:N2}") == ""?"0":Eval("TE_Motor_1_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2馬達線圈S" SortExpression="TE_Motor_2_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox27" runat="server" Text='<%# Bind("TE_Motor_2_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_26" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2馬達線圈S
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_26" runat="server" Text='<%# Eval("TE_Motor_2_A", "{0:N2}") == ""?"0":Eval("TE_Motor_2_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#3馬達線圈T" SortExpression="TE_Motor_3_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox28" runat="server" Text='<%# Bind("TE_Motor_3_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_27" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #3馬達線圈T
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_27" runat="server" Text='<%# Eval("TE_Motor_3_A", "{0:N2}") == ""?"0":Eval("TE_Motor_3_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#4負載端軸承" SortExpression="TE_Motor_4_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("TE_Motor_4_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_28" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #4負載端軸承
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_28" runat="server" Text='<%# Eval("TE_Motor_4_A", "{0:N2}") == ""?"0":Eval("TE_Motor_4_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#5無負載端軸承" SortExpression="TE_Motor_5_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("TE_Motor_5_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_29" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #5無負載端軸承
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_29" runat="server" Text='<%# Eval("TE_Motor_5_A", "{0:N2}") == ""?"0":Eval("TE_Motor_5_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#6主減速機供油溫度" SortExpression="TE_Motor_6_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("TE_Motor_6_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_30" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #6主減速機供油溫度
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_30" runat="server" Text='<%# Eval("TE_Motor_6_A", "{0:N2}") == ""?"0":Eval("TE_Motor_6_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1馬達線圈R" SortExpression="TE_Motor_1_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("TE_Motor_1_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_31" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1馬達線圈R
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_31" runat="server" Text='<%# Eval("TE_Motor_1_B", "{0:N2}") == ""?"0":Eval("TE_Motor_1_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2馬達線圈S" SortExpression="TE_Motor_2_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("TE_Motor_2_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_32" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2馬達線圈S
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_32" runat="server" Text='<%# Eval("TE_Motor_2_B", "{0:N2}") == ""?"0":Eval("TE_Motor_2_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#3馬達線圈T" SortExpression="TE_Motor_3_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("TE_Motor_3_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_33" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #3馬達線圈T
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_33" runat="server" Text='<%# Eval("TE_Motor_3_B", "{0:N2}") == ""?"0":Eval("TE_Motor_3_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#4負載端軸承" SortExpression="TE_Motor_4_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox35" runat="server" Text='<%# Bind("TE_Motor_4_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_34" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #4負載端軸承
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_34" runat="server" Text='<%# Eval("TE_Motor_4_B", "{0:N2}") == ""?"0":Eval("TE_Motor_4_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#5無負載端軸承" SortExpression="TE_Motor_5_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox36" runat="server" Text='<%# Bind("TE_Motor_5_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_35" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #5無負端軸承
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_35" runat="server" Text='<%# Eval("TE_Motor_5_B", "{0:N2}") == ""?"0":Eval("TE_Motor_5_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#6主減速機供油溫度" SortExpression="TE_Motor_6_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox37" runat="server" Text='<%# Bind("TE_Motor_6_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_36" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #6主滅速機供油溫度
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_36" runat="server" Text='<%# Eval("TE_Motor_6_B", "{0:N2}") == ""?"0":Eval("TE_Motor_6_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="磨機出口#A" SortExpression="WP_Mill_A">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox38" runat="server" Text='<%# Bind("WP_Mill_A") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_37" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            磨機出口#A
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_37" runat="server" Text='<%# Eval("WP_Mill_A", "{0:N2}") == ""?"0":Eval("WP_Mill_A", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="磨機出口#B" SortExpression="WP_Mill_B">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox39" runat="server" Text='<%# Bind("WP_Mill_B") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_38" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            磨機出口#B
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_38" runat="server" Text='<%# Eval("WP_Mill_B", "{0:N2}") == ""?"0":Eval("WP_Mill_B", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="風析機出口" SortExpression="WP_OSEPA">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox40" runat="server" Text='<%# Bind("WP_OSEPA") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_39" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            風析機出口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_39" runat="server" Text='<%# Eval("WP_OSEPA", "{0:N2}") == ""?"0":Eval("WP_OSEPA", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S系排風機入口" SortExpression="WP_BC_S_IN">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox41" runat="server" Text='<%# Bind("WP_BC_S_IN") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_40" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            S系排風機入口
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_40" runat="server" Text='<%# Eval("WP_BC_S_IN", "{0:N2}") == ""?"0":Eval("WP_BC_S_IN", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S系排風機壓差" SortExpression="WP_BC_S_Diff">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox42" runat="server" Text='<%# Bind("WP_BC_S_Diff") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_41" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            S系排風機壓差
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_41" runat="server" Text='<%# Eval("WP_BC_S_Diff", "{0:N2}") == ""?"0":Eval("WP_BC_S_Diff", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機M系#1" SortExpression="RPM_BC_M1">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox43" runat="server" Text='<%# Bind("RPM_BC_M1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_42" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            收塵排風機M系#1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_42" runat="server" Text='<%# Eval("RPM_BC_M1", "{0:N2}") == ""?"0":Eval("RPM_BC_M1", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機M系#2" SortExpression="RPM_BC_M2">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox44" runat="server" Text='<%# Bind("RPM_BC_M2") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_43" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            收塵排風機M系#2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_43" runat="server" Text='<%# Eval("RPM_BC_M2", "{0:N2}") == ""?"0":Eval("RPM_BC_M2", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機 S" SortExpression="RPM_BC_S">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox45" runat="server" Text='<%# Bind("RPM_BC_S") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_44" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            收塵排風機S
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_44" runat="server" Text='<%# Eval("RPM_BC_S", "{0:N2}") == ""?"0":Eval("RPM_BC_S", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機M系#1" SortExpression="OP_BC_M1">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox46" runat="server" Text='<%# Bind("OP_BC_M1") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_45" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            收塵排風機M系#1
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_45" runat="server" Text='<%# Eval("OP_BC_M1", "{0:N2}") == ""?"0":Eval("OP_BC_M1", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機M系#2" SortExpression="OP_BC_M2">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox47" runat="server" Text='<%# Bind("OP_BC_M2") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_46" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            收塵排風機M系#2
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_46" runat="server" Text='<%# Eval("OP_BC_M2", "{0:N2}") == ""?"0":Eval("OP_BC_M2", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="收塵排風機S" SortExpression="OP_BC_S">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox48" runat="server" Text='<%# Bind("OP_BC_S") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_47" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            收塵排風機S
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_47" runat="server" Text='<%# Eval("OP_BC_S", "{0:N2}") == ""?"0":Eval("OP_BC_S", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1A飼量調節器" SortExpression="W_M1_A_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox60" runat="server" Text='<%# Bind("W_M1_A_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_48" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1A飼量調節器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_48" runat="server" Text='<%# Eval("W_M1_A_P", "{0:N2}") == ""?"0":Eval("W_M1_A_P", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1A積數器" SortExpression="W_M1_A_C">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox59" runat="server" Text='<%# Bind("W_M1_A_C") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_49" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1A積數器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_49" runat="server" Text='<%# Eval("W_M1_A_C", "{0:N2}") == ""?"0":Eval("W_M1_A_C", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1A飼料量" SortExpression="W_M1_A_Q">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox58" runat="server" Text='<%# Bind("W_M1_A_Q") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_50" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1A飼料量
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_50" runat="server" Text='<%# Eval("W_M1_A_Q", "{0:N2}") == ""?"0":Eval("W_M1_A_Q", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1B飼量調節器" SortExpression="W_M1_B_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox57" runat="server" Text='<%# Bind("W_M1_B_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_51" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1B飼量調節器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_51" runat="server" Text='<%# Eval("W_M1_B_P", "{0:N2}") == ""?"0":Eval("W_M1_B_P", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1B積數器" SortExpression="W_M1_B_C">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox56" runat="server" Text='<%# Bind("W_M1_B_C") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_52" runat="server" AutoPostBack="True" OnDataBinding="cb_box_CheckedChanged" />
                            <br />
                            #1B積數器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_52" runat="server" Text='<%# Eval("W_M1_B_C", "{0:N2}") == ""?"0":Eval("W_M1_B_C", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#1B飼料量" SortExpression="W_M1_B_Q">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox55" runat="server" Text='<%# Bind("W_M1_B_Q") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_53" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #1B飼料量
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_53" runat="server" Text='<%# Eval("W_M1_B_Q", "{0:N2}") == ""?"0":Eval("W_M1_B_Q", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2A飼量調節器" SortExpression="W_M2_A_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox54" runat="server" Text='<%# Bind("W_M2_A_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_54" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2A飼量調節器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_54" runat="server" Text='<%# Eval("W_M2_A_P", "{0:N2}") == ""?"0":Eval("W_M2_A_P", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2A積數器" SortExpression="W_M2_A_C">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox53" runat="server" Text='<%# Bind("W_M2_A_C") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_55" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2A積數器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_55" runat="server" Text='<%# Eval("W_M2_A_C", "{0:N2}") == ""?"0":Eval("W_M2_A_C", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2A飼料量" SortExpression="W_M2_A_Q">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox52" runat="server" Text='<%# Bind("W_M2_A_Q") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_56" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2A飼料量
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_56" runat="server" Text='<%# Eval("W_M2_A_Q", "{0:N2}") == ""?"0":Eval("W_M2_A_Q", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2B飼量調節器" SortExpression="W_M2_B_P">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox51" runat="server" Text='<%# Bind("W_M2_B_P") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_57" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2B飼量調節器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_57" runat="server" Text='<%# Eval("W_M2_B_P", "{0:N2}") == ""?"0":Eval("W_M2_B_P", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2B積數器" SortExpression="W_M2_B_C">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox50" runat="server" Text='<%# Bind("W_M2_B_C") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_58" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2B積數器
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_58" runat="server" Text='<%# Eval("W_M2_B_C", "{0:N2}") == ""?"0":Eval("W_M2_B_C", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#2B飼料量" SortExpression="W_M2_B_Q">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox49" runat="server" Text='<%# Bind("W_M2_B_Q") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="cb_59" runat="server" AutoPostBack="True" OnCheckedChanged="cb_box_CheckedChanged" />
                            <br />
                            #2B飼料量
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lb_59" runat="server" Text='<%# Eval("W_M2_B_Q", "{0:N2}") == ""?"0":Eval("W_M2_B_Q", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <strong>
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style2" Text="無資料!"></asp:Label>
                    </strong>
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [G_Milling_Machine]"></asp:SqlDataSource>
        </div>
        <div id="G2" runat="server" class="row">
            <div class="col-xl-6 table-responsive">
                <asp:GridView ID="GV2" runat="server" AutoGenerateColumns="False" DataSourceID="SDS2" CssClass="table table-sm table-hover table-bordered text-center">
                <Columns>
                    <asp:BoundField DataField="DateTime" HeaderText="時間" SortExpression="DateTime" DataFormatString="{0:HH:mm}" />
                    <asp:BoundField DataField="Mill_ID" HeaderText="磨機" SortExpression="Mill_ID" />
                    <asp:BoundField DataField="ProductName" HeaderText="成品" SortExpression="ProductName" />
                    <asp:BoundField DataField="Moisture" HeaderText="水份%" SortExpression="Moisture" />
                    <asp:BoundField DataField="Specific_Surface" HeaderText="比表面積㎡/kg" SortExpression="Specific_Surface" />
                    <asp:BoundField DataField="Residual_On_Sieve" HeaderText="篩餘%" SortExpression="Residual_On_Sieve" />
                </Columns>
                    <EmptyDataTemplate>
                        <strong>
                        <asp:Label ID="Label2" runat="server" CssClass="auto-style2" Text="無資料!"></asp:Label>
                        </strong>
                    </EmptyDataTemplate>
            </asp:GridView>
            <asp:SqlDataSource ID="SDS2" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve FROM A_Product_Quality AS Q LEFT OUTER JOIN A_Product AS P ON Q.ProductID = P.ProductID">
                <SelectParameters>
                    <asp:ControlParameter ControlID="tb_SDATE" Name="newparameter" PropertyName="Text" />
                </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
    </div>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <script>
        var d = document.getElementById('ContentPlaceHolder1_GV1');
        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            document.getElementById("phone").style.display = "";
            document.getElementById("name").style.display = "none";
            //d.style.height = "200%";
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
    </script>

</asp:Content>
