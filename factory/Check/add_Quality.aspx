<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="add_Quality.aspx.cs" Inherits="factory.Check.add_Quality" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>新增實驗紀錄</title>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.12.4.js"></script>
    <script src="../js/jquery-ui-1.12.1.js"></script>
    <style>
        td {
            padding-right:10px
        }
        .auto-style1 {
            font-size: large;
        }
        .auto-style2 {
            font-size: x-large;
        }

        .auto-style3 {
            color: #FF0000;
        }

    </style>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px;border:1px solid rgba(0, 0, 0, 0.125);">
                <strong>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">新增實驗數據</asp:Label>
                </strong> 
        </div>
    <div class="col-xxl-7 card" style="padding:0px;margin-top:20px">
        <div style="margin-bottom:10px;padding-left:10px">
            <div class="row">
                <div class="col-6">
                    <strong>
                        <asp:Label ID="Label2" runat="server" Text="實驗檢測紀錄" CssClass="auto-style2"></asp:Label>
                    </strong>
                </div>
                <div class="col-auto" style="margin-top:5px">
                    <asp:Button ID="btn_v" runat="server" Text="顯示實驗記錄" Class="btn btn-success" OnClick="btn_v_Click" />
                </div>
            </div>
        </div>
        <div style="padding-left:10px;padding-bottom:10px">
            廠區:
            <asp:DropDownList ID="DDL_F" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_F_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div style="padding-left:10px;padding-bottom:10px">
            取樣日期:
            <asp:TextBox ID="tb_SDATE" runat="server" placeholder="選擇日期:" TextMode="SingleLine" Width="130px" autocomplete="off"></asp:TextBox>
            時:
            <asp:DropDownList ID="DDL_H" runat="server">
                <asp:ListItem>00</asp:ListItem>
                <asp:ListItem>01</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>04</asp:ListItem>
                <asp:ListItem>05</asp:ListItem>
                <asp:ListItem>06</asp:ListItem>
                <asp:ListItem>07</asp:ListItem>
                <asp:ListItem>08</asp:ListItem>
                <asp:ListItem>09</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
            </asp:DropDownList>
            分:
            <asp:DropDownList ID="DDL_M" runat="server">
                <asp:ListItem>00</asp:ListItem>
                <asp:ListItem>01</asp:ListItem>
                <asp:ListItem>02</asp:ListItem>
                <asp:ListItem>03</asp:ListItem>
                <asp:ListItem>04</asp:ListItem>
                <asp:ListItem>05</asp:ListItem>
                <asp:ListItem>06</asp:ListItem>
                <asp:ListItem>07</asp:ListItem>
                <asp:ListItem>08</asp:ListItem>
                <asp:ListItem>09</asp:ListItem>
                <asp:ListItem>10</asp:ListItem>
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
                <asp:ListItem>13</asp:ListItem>
                <asp:ListItem>14</asp:ListItem>
                <asp:ListItem>15</asp:ListItem>
                <asp:ListItem>16</asp:ListItem>
                <asp:ListItem>17</asp:ListItem>
                <asp:ListItem>18</asp:ListItem>
                <asp:ListItem>19</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>21</asp:ListItem>
                <asp:ListItem>22</asp:ListItem>
                <asp:ListItem>23</asp:ListItem>
                <asp:ListItem>24</asp:ListItem>
                <asp:ListItem>25</asp:ListItem>
                <asp:ListItem>26</asp:ListItem>
                <asp:ListItem>27</asp:ListItem>
                <asp:ListItem>28</asp:ListItem>
                <asp:ListItem>29</asp:ListItem>
                <asp:ListItem>30</asp:ListItem>
                <asp:ListItem>31</asp:ListItem>
                <asp:ListItem>32</asp:ListItem>
                <asp:ListItem>33</asp:ListItem>
                <asp:ListItem>34</asp:ListItem>
                <asp:ListItem>35</asp:ListItem>
                <asp:ListItem>36</asp:ListItem>
                <asp:ListItem>37</asp:ListItem>
                <asp:ListItem>38</asp:ListItem>
                <asp:ListItem>39</asp:ListItem>
                <asp:ListItem>40</asp:ListItem>
                <asp:ListItem>41</asp:ListItem>
                <asp:ListItem>42</asp:ListItem>
                <asp:ListItem>43</asp:ListItem>
                <asp:ListItem>44</asp:ListItem>
                <asp:ListItem>45</asp:ListItem>
                <asp:ListItem>46</asp:ListItem>
                <asp:ListItem>47</asp:ListItem>
                <asp:ListItem>48</asp:ListItem>
                <asp:ListItem>49</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>51</asp:ListItem>
                <asp:ListItem>52</asp:ListItem>
                <asp:ListItem>53</asp:ListItem>
                <asp:ListItem>54</asp:ListItem>
                <asp:ListItem>55</asp:ListItem>
                <asp:ListItem>56</asp:ListItem>
                <asp:ListItem>57</asp:ListItem>
                <asp:ListItem>58</asp:ListItem>
                <asp:ListItem>59</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id="data" runat="server" style="padding-left:10px">
            <strong>
                <asp:RadioButtonList ID="rbl_L" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="Lab_North">北保</asp:ListItem>
                    <asp:ListItem Value="Lab_Central">中保</asp:ListItem>
                    <asp:ListItem Value="Lab_Center">混研中心</asp:ListItem>
            </asp:RadioButtonList>
                <asp:RadioButtonList ID="rbl_M" runat="server" RepeatDirection="Horizontal" CssClass="auto-style1">
                </asp:RadioButtonList>
                <asp:RadioButtonList ID="rbl_P" runat="server" RepeatDirection="Horizontal" CssClass="auto-style1">
                    <asp:ListItem Value="C1" Selected="True">(C1)卜特蘭I型水泥</asp:ListItem>
                    <asp:ListItem Value="C2">(C2)卜特蘭II型水泥</asp:ListItem>
                    <asp:ListItem Value="SK">(SS)爐石粉(100級)</asp:ListItem>
                    <asp:ListItem Value="SS">(SK)爐石粉(120級)</asp:ListItem>
                </asp:RadioButtonList>
            </strong>
        </div>
        <div id="datas" runat="server">
            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right" style="padding-right:10px">
                    <B>水份</B>
                </div>
                <div class="col-sm-auto col-6 text-left" style="padding:0px">
                    <asp:TextBox ID="tb_M" runat="server" class="form-control"  TextMode="Number" step="0.01"></asp:TextBox>
                </div>
                <div class="col-auto text-left">
                    <B>%</B>
                </div>
            </div>

            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right" style="padding-left:0px;padding-right:10px">
                    <B>比表面積</B>
                </div>
                <div class="col-sm-auto col-6 text-left" style="padding:0px">
                    <asp:TextBox ID="tb_S" runat="server" class="form-control"  TextMode="Number" step="0.01"></asp:TextBox>
                </div>
                <div class="col-auto text-left">
                    <B>㎡/kg</B>
                </div>
            </div>

            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right" style="padding-right:10px">
                    <B>篩餘</B>
                </div>
                <div class="col-sm-auto col-6 text-left" style="padding:0px">
                    <asp:TextBox ID="tb_R" runat="server" class="form-control"  TextMode="Number" step="0.01"></asp:TextBox>
                </div>
                <div class="col-auto text-left">
                    <B>%</B>
                </div>
            </div>

            <div class="row align-items-center" style="margin-bottom:10px">
                <div class="col-xl-2 col-sm-2 col-3 text-right"></div>
                <div class="text-left">
                    <strong>
                    <asp:Label ID="lb_error" runat="server" Text="至少填選一個值" CssClass="auto-style3" Visible="False"></asp:Label>
                    </strong>
                    <asp:Button ID="btn_save" runat="server" Class="btn btn-outline-primary" Text="儲存" style="margin-left:143px" OnClick="btn_save_Click"/>
                </div>
            </div>
        </div>
        <div id="tip" runat="server" class="col-xxl-3 col-lg-4 col-sm-5 bg-light">
            <div>
                <asp:Label ID="lb_M1" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <asp:Label ID="lb_M2" runat="server" Text=""></asp:Label>
            </div>

            <div>
                <asp:Label ID="lb_M3" runat="server" Text=""></asp:Label>
            </div>
            <div class="text-right">
                <asp:Label ID="lb_save" runat="server" Text="已經儲存"></asp:Label>
            </div>
        </div>
        </div>
    </div>
    <script>
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            document.getElementById("phone").style.display = "";
        }
    </script>
</asp:Content>
