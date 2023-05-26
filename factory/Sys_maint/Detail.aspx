<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="factory.Sys_maint.Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>資料表維護</title>
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <asp:Label ID="Label1" runat="server" Text="資料表:"></asp:Label>
                <asp:DropDownList ID="DDL_table" runat="server" AutoPostBack="True">
                    <asp:ListItem>G_Milling_Machine_Mapping</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="Label2" runat="server" Text="廠區:"></asp:Label>
                <asp:DropDownList ID="DDL_factory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_factory_SelectedIndexChanged"></asp:DropDownList>

                <asp:Label ID="Label3" runat="server" Text="機器:"></asp:Label>
                <asp:DropDownList ID="DDL_Mill" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_Mill_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

        <div class="row" style="margin-top:5px">
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
                </ul>
            </div>
        </div>

        <div style="margin-top:10px">
            <asp:DetailsView ID="DV1" runat="server" Height="50px" Width="25%" AutoGenerateRows="False" DataKeyNames="FactoryID,Mill_ID" DataSourceID="SDS1" CssClass="table table-sm table-hover table-bordered text-center" OnDataBound="DV1_DataBound" OnItemCommand="DV1_ItemCommand" OnModeChanged="DV1_ModeChanged">
                <EmptyDataTemplate>
                    <strong>
                    <asp:Label ID="Label5" runat="server" CssClass="auto-style1" Text="無資料!"></asp:Label>
                    </strong>
                </EmptyDataTemplate>
                <Fields>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CommandName="Update" ImageUrl="~/img/save.png" ToolTip="儲存" Width="20px" />
                            &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CommandName="Cancel" ImageUrl="~/img/error.png" ToolTip="取消" Width="20px" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/img/pen.png" ToolTip="編輯" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FactoryID" HeaderText="工廠編號" ReadOnly="True" SortExpression="FactoryID" />
                    <asp:BoundField DataField="Mill_ID" HeaderText="機器編號" ReadOnly="True" SortExpression="Mill_ID" />
                    <asp:BoundField DataField="Motor_Current_A" HeaderText="磨機電流A" SortExpression="Motor_Current_A" />
                    <asp:BoundField DataField="Motor_Current_B" HeaderText="磨機電流B" SortExpression="Motor_Current_B" />
                    <asp:BoundField DataField="Motor_PowerKW_A" HeaderText="磨機功率A" SortExpression="Motor_PowerKW_A" />
                    <asp:BoundField DataField="Motor_PowerKW_B" HeaderText="磨機功率B" SortExpression="Motor_PowerKW_B" />
                    <asp:BoundField DataField="Bucket_Elevator_A" HeaderText="循環提運機" SortExpression="Bucket_Elevator_A" />
                    <asp:BoundField DataField="Bucket_Elevator_B" HeaderText="入庫提運機" SortExpression="Bucket_Elevator_B" />
                    <asp:BoundField DataField="OSEPA_Current" HeaderText="O-SEPA選粉機電流" SortExpression="OSEPA_Current" />
                    <asp:BoundField DataField="OSEPA_RPM" HeaderText="O-SEPA選粉機轉速" SortExpression="OSEPA_RPM" />
                    <asp:BoundField DataField="Bag_Colletcot_M1" HeaderText="袋式收塵機 M1" SortExpression="Bag_Colletcot_M1" />
                    <asp:BoundField DataField="Bag_Colletcot_M2" HeaderText="袋式收塵機 M1" SortExpression="Bag_Colletcot_M2" />
                    <asp:BoundField DataField="Bag_Colletcot_S" HeaderText="袋式收塵機 S" SortExpression="Bag_Colletcot_S" />
                    <asp:BoundField DataField="TE_Mill_Bearing_In_A" HeaderText="耳軸承 #1 入口" SortExpression="TE_Mill_Bearing_In_A" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Out_A" HeaderText="耳軸承 #1 出口" SortExpression="TE_Mill_Bearing_Out_A" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_In_A" HeaderText="潤滑油 #1 入口" SortExpression="TE_Mill_Bearing_Oil_In_A" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_Out_A" HeaderText="潤滑油 #1 出口" SortExpression="TE_Mill_Bearing_Oil_Out_A" />
                    <asp:BoundField DataField="TE_Mill_Bearing_In_B" HeaderText="耳軸承 #2 入口" SortExpression="TE_Mill_Bearing_In_B" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Out_B" HeaderText="耳軸承 #2 出口" SortExpression="TE_Mill_Bearing_Out_B" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_In_B" HeaderText="潤滑油 #2 入口" SortExpression="TE_Mill_Bearing_Oil_In_B" />
                    <asp:BoundField DataField="TE_Mill_Bearing_Oil_Out_B" HeaderText="潤滑油 #2 出口" SortExpression="TE_Mill_Bearing_Oil_Out_B" />
                    <asp:BoundField DataField="TE_Mill_RAW_A" HeaderText="#1 料溫" SortExpression="TE_Mill_RAW_A" />
                    <asp:BoundField DataField="TE_Mill_Air_A" HeaderText="#1 氣溫" SortExpression="TE_Mill_Air_A" />
                    <asp:BoundField DataField="TE_Mill_RAW_B" HeaderText="#2 料溫" SortExpression="TE_Mill_RAW_B" />
                    <asp:BoundField DataField="TE_Mill_Air_B" HeaderText="#2 氣溫" SortExpression="TE_Mill_Air_B" />
                    <asp:BoundField DataField="TE_S_In" HeaderText="風析機出口" SortExpression="TE_S_In" />
                    <asp:BoundField DataField="TE_Product" HeaderText="成品入庫料溫" SortExpression="TE_Product" />
                    <asp:BoundField DataField="TE_Motor_1_A" HeaderText="#1馬達線圈R" SortExpression="TE_Motor_1_A" />
                    <asp:BoundField DataField="TE_Motor_2_A" HeaderText="#2馬達線圈S" SortExpression="TE_Motor_2_A" />
                    <asp:BoundField DataField="TE_Motor_3_A" HeaderText="#3馬達線圈T" SortExpression="TE_Motor_3_A" />
                    <asp:BoundField DataField="TE_Motor_4_A" HeaderText="#4負載端軸承" SortExpression="TE_Motor_4_A" />
                    <asp:BoundField DataField="TE_Motor_5_A" HeaderText="#5無負載端軸承" SortExpression="TE_Motor_5_A" />
                    <asp:BoundField DataField="TE_Motor_6_A" HeaderText="#6主減速機供油溫度" SortExpression="TE_Motor_6_A" />
                    <asp:BoundField DataField="TE_Motor_1_B" HeaderText="#1馬達線圈R" SortExpression="TE_Motor_1_B" />
                    <asp:BoundField DataField="TE_Motor_2_B" HeaderText="#2馬達線圈S" SortExpression="TE_Motor_2_B" />
                    <asp:BoundField DataField="TE_Motor_3_B" HeaderText="#3馬達線圈T" SortExpression="TE_Motor_3_B" />
                    <asp:BoundField DataField="TE_Motor_4_B" HeaderText="#4負載端軸承" SortExpression="TE_Motor_4_B" />
                    <asp:BoundField DataField="TE_Motor_5_B" HeaderText="#5無負載端軸承" SortExpression="TE_Motor_5_B" />
                    <asp:BoundField DataField="TE_Motor_6_B" HeaderText="#6主減速機供油溫度" SortExpression="TE_Motor_6_B" />
                    <asp:BoundField DataField="WP_Mill_A" HeaderText="磨機出口 #A" SortExpression="WP_Mill_A" />
                    <asp:BoundField DataField="WP_Mill_B" HeaderText="磨機出口 #B" SortExpression="WP_Mill_B" />
                    <asp:BoundField DataField="WP_OSEPA" HeaderText="風析機出口" SortExpression="WP_OSEPA" />
                    <asp:BoundField DataField="WP_BC_S_IN" HeaderText="S系排風機 入口" SortExpression="WP_BC_S_IN" />
                    <asp:BoundField DataField="WP_BC_S_Diff" HeaderText="S系排風機 壓差" SortExpression="WP_BC_S_Diff" />
                    <asp:BoundField DataField="RPM_BC_M1" HeaderText="收塵排風機 M系#1" SortExpression="RPM_BC_M1" />
                    <asp:BoundField DataField="RPM_BC_M2" HeaderText="收塵排風機 M系#2" SortExpression="RPM_BC_M2" />
                    <asp:BoundField DataField="RPM_BC_S" HeaderText="收塵排風機 S" SortExpression="RPM_BC_S" />
                    <asp:BoundField DataField="OP_BC_M1" HeaderText="收塵排風機 M系#1" SortExpression="OP_BC_M1" />
                    <asp:BoundField DataField="OP_BC_M2" HeaderText="收塵排風機 M系#2" SortExpression="OP_BC_M2" />
                    <asp:BoundField DataField="OP_BC_S" HeaderText="收塵排風機 S" SortExpression="OP_BC_S" />
                    <asp:BoundField DataField="W_M1_A_P" HeaderText="#1A 飼量調節器" SortExpression="W_M1_A_P" />
                    <asp:BoundField DataField="W_M1_A_C" HeaderText="#1A 積數器" SortExpression="W_M1_A_C" />
                    <asp:BoundField DataField="W_M1_A_Q" HeaderText="#1A 飼料量" SortExpression="W_M1_A_Q" />
                    <asp:BoundField DataField="W_M1_B_P" HeaderText="#1B 飼量調節器" SortExpression="W_M1_B_P" />
                    <asp:BoundField DataField="W_M1_B_C" HeaderText="#1B 積數器" SortExpression="W_M1_B_C" />
                    <asp:BoundField DataField="W_M1_B_Q" HeaderText="#1B 飼料量" SortExpression="W_M1_B_Q" />
                    <asp:BoundField DataField="W_M2_A_P" HeaderText="#2A 飼量調節器" SortExpression="W_M2_A_P" />
                    <asp:BoundField DataField="W_M2_A_C" HeaderText="#2A 積數器" SortExpression="W_M2_A_C" />
                    <asp:BoundField DataField="W_M2_A_Q" HeaderText="#2A 飼料量" SortExpression="W_M2_A_Q" />
                    <asp:BoundField DataField="W_M2_B_P" HeaderText="#2B 飼量調節器" SortExpression="W_M2_B_P" />
                    <asp:BoundField DataField="W_M2_B_C" HeaderText="#2B 積數器" SortExpression="W_M2_B_C" />
                    <asp:BoundField DataField="W_M2_B_Q" HeaderText="#2B 飼料量" SortExpression="W_M2_B_Q" />
                </Fields>
            </asp:DetailsView>
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" DeleteCommand="DELETE FROM [G_Milling_Machine_Mapping] WHERE [FactoryID] = @FactoryID AND [Mill_ID] = @Mill_ID" InsertCommand="INSERT INTO [G_Milling_Machine_Mapping] ([FactoryID], [Mill_ID], [Motor_Current_A], [Motor_Current_B], [Motor_PowerKW_A], [Motor_PowerKW_B], [Bucket_Elevator_A], [Bucket_Elevator_B], [OSEPA_Current], [OSEPA_RPM], [Bag_Colletcot_M1], [Bag_Colletcot_M2], [Bag_Colletcot_S], [TE_Mill_Bearing_In_A], [TE_Mill_Bearing_Out_A], [TE_Mill_Bearing_Oil_In_A], [TE_Mill_Bearing_Oil_Out_A], [TE_Mill_Bearing_In_B], [TE_Mill_Bearing_Out_B], [TE_Mill_Bearing_Oil_In_B], [TE_Mill_Bearing_Oil_Out_B], [TE_Mill_RAW_A], [TE_Mill_Air_A], [TE_Mill_RAW_B], [TE_Mill_Air_B], [TE_S_In], [TE_Product], [TE_Motor_1_A], [TE_Motor_2_A], [TE_Motor_3_A], [TE_Motor_4_A], [TE_Motor_5_A], [TE_Motor_6_A], [TE_Motor_1_B], [TE_Motor_2_B], [TE_Motor_3_B], [TE_Motor_4_B], [TE_Motor_5_B], [TE_Motor_6_B], [WP_Mill_A], [WP_Mill_B], [WP_OSEPA], [WP_BC_S_IN], [RPM_BC_M1], [RPM_BC_M2], [RPM_BC_S], [OP_BC_M1], [OP_BC_M2], [OP_BC_S], [W_M1_A_P], [W_M1_A_C], [W_M1_A_Q], [W_M1_B_P], [W_M1_B_C], [W_M1_B_Q], [W_M2_A_P], [W_M2_A_C], [W_M2_A_Q], [W_M2_B_P], [W_M2_B_C], [W_M2_B_Q]) VALUES (@FactoryID, @Mill_ID, @Motor_Current_A, @Motor_Current_B, @Motor_PowerKW_A, @Motor_PowerKW_B, @Bucket_Elevator_A, @Bucket_Elevator_B, @OSEPA_Current, @OSEPA_RPM, @Bag_Colletcot_M1, @Bag_Colletcot_M2, @Bag_Colletcot_S, @TE_Mill_Bearing_In_A, @TE_Mill_Bearing_Out_A, @TE_Mill_Bearing_Oil_In_A, @TE_Mill_Bearing_Oil_Out_A, @TE_Mill_Bearing_In_B, @TE_Mill_Bearing_Out_B, @TE_Mill_Bearing_Oil_In_B, @TE_Mill_Bearing_Oil_Out_B, @TE_Mill_RAW_A, @TE_Mill_Air_A, @TE_Mill_RAW_B, @TE_Mill_Air_B, @TE_S_In, @TE_Product, @TE_Motor_1_A, @TE_Motor_2_A, @TE_Motor_3_A, @TE_Motor_4_A, @TE_Motor_5_A, @TE_Motor_6_A, @TE_Motor_1_B, @TE_Motor_2_B, @TE_Motor_3_B, @TE_Motor_4_B, @TE_Motor_5_B, @TE_Motor_6_B, @WP_Mill_A, @WP_Mill_B, @WP_OSEPA, @WP_BC_S_IN, @RPM_BC_M1, @RPM_BC_M2, @RPM_BC_S, @OP_BC_M1, @OP_BC_M2, @OP_BC_S, @W_M1_A_P, @W_M1_A_C, @W_M1_A_Q, @W_M1_B_P, @W_M1_B_C, @W_M1_B_Q, @W_M2_A_P, @W_M2_A_C, @W_M2_A_Q, @W_M2_B_P, @W_M2_B_C, @W_M2_B_Q)" SelectCommand="SELECT * FROM [G_Milling_Machine_Mapping]" UpdateCommand="UPDATE [G_Milling_Machine_Mapping] SET [Motor_Current_A] = @Motor_Current_A, [Motor_Current_B] = @Motor_Current_B, [Motor_PowerKW_A] = @Motor_PowerKW_A, [Motor_PowerKW_B] = @Motor_PowerKW_B, [Bucket_Elevator_A] = @Bucket_Elevator_A, [Bucket_Elevator_B] = @Bucket_Elevator_B, [OSEPA_Current] = @OSEPA_Current, [OSEPA_RPM] = @OSEPA_RPM, [Bag_Colletcot_M1] = @Bag_Colletcot_M1, [Bag_Colletcot_M2] = @Bag_Colletcot_M2, [Bag_Colletcot_S] = @Bag_Colletcot_S, [TE_Mill_Bearing_In_A] = @TE_Mill_Bearing_In_A, [TE_Mill_Bearing_Out_A] = @TE_Mill_Bearing_Out_A, [TE_Mill_Bearing_Oil_In_A] = @TE_Mill_Bearing_Oil_In_A, [TE_Mill_Bearing_Oil_Out_A] = @TE_Mill_Bearing_Oil_Out_A, [TE_Mill_Bearing_In_B] = @TE_Mill_Bearing_In_B, [TE_Mill_Bearing_Out_B] = @TE_Mill_Bearing_Out_B, [TE_Mill_Bearing_Oil_In_B] = @TE_Mill_Bearing_Oil_In_B, [TE_Mill_Bearing_Oil_Out_B] = @TE_Mill_Bearing_Oil_Out_B, [TE_Mill_RAW_A] = @TE_Mill_RAW_A, [TE_Mill_Air_A] = @TE_Mill_Air_A, [TE_Mill_RAW_B] = @TE_Mill_RAW_B, [TE_Mill_Air_B] = @TE_Mill_Air_B, [TE_S_In] = @TE_S_In, [TE_Product] = @TE_Product, [TE_Motor_1_A] = @TE_Motor_1_A, [TE_Motor_2_A] = @TE_Motor_2_A, [TE_Motor_3_A] = @TE_Motor_3_A, [TE_Motor_4_A] = @TE_Motor_4_A, [TE_Motor_5_A] = @TE_Motor_5_A, [TE_Motor_6_A] = @TE_Motor_6_A, [TE_Motor_1_B] = @TE_Motor_1_B, [TE_Motor_2_B] = @TE_Motor_2_B, [TE_Motor_3_B] = @TE_Motor_3_B, [TE_Motor_4_B] = @TE_Motor_4_B, [TE_Motor_5_B] = @TE_Motor_5_B, [TE_Motor_6_B] = @TE_Motor_6_B, [WP_Mill_A] = @WP_Mill_A, [WP_Mill_B] = @WP_Mill_B, [WP_OSEPA] = @WP_OSEPA, [WP_BC_S_IN] = @WP_BC_S_IN, [RPM_BC_M1] = @RPM_BC_M1, [RPM_BC_M2] = @RPM_BC_M2, [RPM_BC_S] = @RPM_BC_S, [OP_BC_M1] = @OP_BC_M1, [OP_BC_M2] = @OP_BC_M2, [OP_BC_S] = @OP_BC_S, [W_M1_A_P] = @W_M1_A_P, [W_M1_A_C] = @W_M1_A_C, [W_M1_A_Q] = @W_M1_A_Q, [W_M1_B_P] = @W_M1_B_P, [W_M1_B_C] = @W_M1_B_C, [W_M1_B_Q] = @W_M1_B_Q, [W_M2_A_P] = @W_M2_A_P, [W_M2_A_C] = @W_M2_A_C, [W_M2_A_Q] = @W_M2_A_Q, [W_M2_B_P] = @W_M2_B_P, [W_M2_B_C] = @W_M2_B_C, [W_M2_B_Q] = @W_M2_B_Q WHERE [FactoryID] = @FactoryID AND [Mill_ID] = @Mill_ID">
                <DeleteParameters>
                    <asp:Parameter Name="FactoryID" Type="String" />
                    <asp:Parameter Name="Mill_ID" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="FactoryID" Type="String" />
                    <asp:Parameter Name="Mill_ID" Type="String" />
                    <asp:Parameter Name="Motor_Current_A" Type="String" />
                    <asp:Parameter Name="Motor_Current_B" Type="String" />
                    <asp:Parameter Name="Motor_PowerKW_A" Type="String" />
                    <asp:Parameter Name="Motor_PowerKW_B" Type="String" />
                    <asp:Parameter Name="Bucket_Elevator_A" Type="String" />
                    <asp:Parameter Name="Bucket_Elevator_B" Type="String" />
                    <asp:Parameter Name="OSEPA_Current" Type="String" />
                    <asp:Parameter Name="OSEPA_RPM" Type="String" />
                    <asp:Parameter Name="Bag_Colletcot_M1" Type="String" />
                    <asp:Parameter Name="Bag_Colletcot_M2" Type="String" />
                    <asp:Parameter Name="Bag_Colletcot_S" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_In_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Out_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_In_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_Out_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_In_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Out_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_In_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_Out_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_RAW_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Air_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_RAW_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Air_B" Type="String" />
                    <asp:Parameter Name="TE_S_In" Type="String" />
                    <asp:Parameter Name="TE_Product" Type="String" />
                    <asp:Parameter Name="TE_Motor_1_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_2_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_3_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_4_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_5_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_6_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_1_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_2_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_3_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_4_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_5_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_6_B" Type="String" />
                    <asp:Parameter Name="WP_Mill_A" Type="String" />
                    <asp:Parameter Name="WP_Mill_B" Type="String" />
                    <asp:Parameter Name="WP_OSEPA" Type="String" />
                    <asp:Parameter Name="WP_BC_S_IN" Type="String" />
                    <asp:Parameter Name="RPM_BC_M1" Type="String" />
                    <asp:Parameter Name="RPM_BC_M2" Type="String" />
                    <asp:Parameter Name="RPM_BC_S" Type="String" />
                    <asp:Parameter Name="OP_BC_M1" Type="String" />
                    <asp:Parameter Name="OP_BC_M2" Type="String" />
                    <asp:Parameter Name="OP_BC_S" Type="String" />
                    <asp:Parameter Name="W_M1_A_P" Type="String" />
                    <asp:Parameter Name="W_M1_A_C" Type="String" />
                    <asp:Parameter Name="W_M1_A_Q" Type="String" />
                    <asp:Parameter Name="W_M1_B_P" Type="String" />
                    <asp:Parameter Name="W_M1_B_C" Type="String" />
                    <asp:Parameter Name="W_M1_B_Q" Type="String" />
                    <asp:Parameter Name="W_M2_A_P" Type="String" />
                    <asp:Parameter Name="W_M2_A_C" Type="String" />
                    <asp:Parameter Name="W_M2_A_Q" Type="String" />
                    <asp:Parameter Name="W_M2_B_P" Type="String" />
                    <asp:Parameter Name="W_M2_B_C" Type="String" />
                    <asp:Parameter Name="W_M2_B_Q" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Motor_Current_A" Type="String" />
                    <asp:Parameter Name="Motor_Current_B" Type="String" />
                    <asp:Parameter Name="Motor_PowerKW_A" Type="String" />
                    <asp:Parameter Name="Motor_PowerKW_B" Type="String" />
                    <asp:Parameter Name="Bucket_Elevator_A" Type="String" />
                    <asp:Parameter Name="Bucket_Elevator_B" Type="String" />
                    <asp:Parameter Name="OSEPA_Current" Type="String" />
                    <asp:Parameter Name="OSEPA_RPM" Type="String" />
                    <asp:Parameter Name="Bag_Colletcot_M1" Type="String" />
                    <asp:Parameter Name="Bag_Colletcot_M2" Type="String" />
                    <asp:Parameter Name="Bag_Colletcot_S" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_In_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Out_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_In_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_Out_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_In_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Out_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_In_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Bearing_Oil_Out_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_RAW_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_Air_A" Type="String" />
                    <asp:Parameter Name="TE_Mill_RAW_B" Type="String" />
                    <asp:Parameter Name="TE_Mill_Air_B" Type="String" />
                    <asp:Parameter Name="TE_S_In" Type="String" />
                    <asp:Parameter Name="TE_Product" Type="String" />
                    <asp:Parameter Name="TE_Motor_1_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_2_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_3_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_4_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_5_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_6_A" Type="String" />
                    <asp:Parameter Name="TE_Motor_1_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_2_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_3_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_4_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_5_B" Type="String" />
                    <asp:Parameter Name="TE_Motor_6_B" Type="String" />
                    <asp:Parameter Name="WP_Mill_A" Type="String" />
                    <asp:Parameter Name="WP_Mill_B" Type="String" />
                    <asp:Parameter Name="WP_OSEPA" Type="String" />
                    <asp:Parameter Name="WP_BC_S_IN" Type="String" />
                    <asp:Parameter Name="RPM_BC_M1" Type="String" />
                    <asp:Parameter Name="RPM_BC_M2" Type="String" />
                    <asp:Parameter Name="RPM_BC_S" Type="String" />
                    <asp:Parameter Name="OP_BC_M1" Type="String" />
                    <asp:Parameter Name="OP_BC_M2" Type="String" />
                    <asp:Parameter Name="OP_BC_S" Type="String" />
                    <asp:Parameter Name="W_M1_A_P" Type="String" />
                    <asp:Parameter Name="W_M1_A_C" Type="String" />
                    <asp:Parameter Name="W_M1_A_Q" Type="String" />
                    <asp:Parameter Name="W_M1_B_P" Type="String" />
                    <asp:Parameter Name="W_M1_B_C" Type="String" />
                    <asp:Parameter Name="W_M1_B_Q" Type="String" />
                    <asp:Parameter Name="W_M2_A_P" Type="String" />
                    <asp:Parameter Name="W_M2_A_C" Type="String" />
                    <asp:Parameter Name="W_M2_A_Q" Type="String" />
                    <asp:Parameter Name="W_M2_B_P" Type="String" />
                    <asp:Parameter Name="W_M2_B_C" Type="String" />
                    <asp:Parameter Name="W_M2_B_Q" Type="String" />
                    <asp:Parameter Name="FactoryID" Type="String" />
                    <asp:Parameter Name="Mill_ID" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
        <br/>
    </div>
</asp:Content>
