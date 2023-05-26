<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Factory_state.aspx.cs" Inherits="factory.Light.Factory_state" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="refresh" content="610">
    <title>
        工廠電腦狀態
    </title>
    <style>
        .b {
            border:1px solid rgba(0, 0, 0, 0.125)
        }
        .b:hover{
            background:gainsboro;
        }
        .auto-style1 {
            font-size: x-large;
        }
    </style>
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid" style="padding-top:5px">
        <div class="row" style="text-align:-webkit-center">
            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>觀音廠(192.168.101.45)</strong>
                </div>
                <div>
                    <asp:Label ID="lb_KY1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="KY_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
                <div>
                    <asp:ImageButton ID="imgb_KY" runat="server" ImageUrl="~/img/link.png" Width="60px" />   
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>
                <div>
                    <asp:Label ID="lb_KY2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_KY" runat="server" ImageUrl="~/img/remove.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>
            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>八里廠(192.168.21.45)</strong>
                </div>

                <div>
                    <asp:Label ID="lb_BL1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="BL_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>

                <div>
                    <asp:ImageButton ID="imgb_BL" runat="server" ImageUrl="~/img/link.png" Width="60px" />
                     <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>
                
                <div>
                    <asp:Label ID="lb_BL2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_BL" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>

            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>全興廠(192.168.41.45)</strong>
                </div>

                <div>
                    <asp:Label ID="lb_QX1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="QX_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>

                <div>
                    <asp:ImageButton ID="imgb_QX" runat="server" ImageUrl="~/img/link.png" Width="60px"/>
                    <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>

                <div>
                    <asp:Label ID="lb_QX2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_QX" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>

            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>彰濱廠(192.168.51.45)</strong>
                </div>
                 <div>
                    <asp:Label ID="lb_ZB1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="ZB_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>

                <div>
                    <asp:ImageButton ID="imgb_ZB" runat="server" ImageUrl="~/img/link.png" Width="60px"/>
                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>

                <div>
                    <asp:Label ID="lb_ZB2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_ZB" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>

            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>高雄廠(192.168.61.45)</strong>
                </div>
                
                <div>
                    <asp:Label ID="lb_KH1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="KH_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>

                <div>
                    <asp:ImageButton ID="imgb_KH" runat="server" ImageUrl="~/img/link.png" Width="60px"/>
                    <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>

                <div>
                    <asp:Label ID="lb_KH2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_KH" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>

            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>龍德廠(192.168.31.45)</strong>
                </div>

                <div>
                    <asp:Label ID="lb_LD1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="LD_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>

                <div>
                    <asp:ImageButton ID="imgb_LD" runat="server" ImageUrl="~/img/link.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>

                <div>
                    <asp:Label ID="lb_LD2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_LD" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>

            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>利澤廠(192.168.91.45)</strong>
                </div>

                <div>
                    <asp:Label ID="lb_LZ1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="LZ_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
                
                <div>
                    <asp:ImageButton ID="imgb_LZ" runat="server" ImageUrl="~/img/link.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>

                <div>
                    <asp:Label ID="lb_LZ2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_LZ" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>

            <div class="col-xl-3 col-md-4 col-6 b">
                <div>
                    <strong>花蓮廠(192.168.71.45)</strong>
                </div>

                <div>
                    <asp:Label ID="lb_HL1" runat="server" Text=""></asp:Label>
                    <asp:Image ID="HL_230" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>

                <div>
                    <asp:ImageButton ID="imgb_HL" runat="server" ImageUrl="~/img/link.png" Width="60px"/>
                    <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/img/arrow.png" Width="60px" />
                    <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/img/server.png" Width="60px" />
                </div>

                <div>
                    <asp:Label ID="lb_HL2" runat="server" Text=""></asp:Label>
                    <asp:Image ID="H_HL" runat="server" ImageUrl="~/img/correct.png" Width="20px" style="margin-bottom:5px" />
                </div>
            </div>
        </div>
        <div style="padding-top:3%">
            <div class="row justify-content-center">
                <div class="col-auto col-sm-10 col-lg-8">
                    <asp:GridView ID="GV1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataKeyNames="DTime,Host1,Host2" DataSourceID="SDS1" OnPageIndexChanging="GV1_PageIndexChanging" OnRowDataBound="GV1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="DTime" HeaderText="時間" ReadOnly="True" SortExpression="DTime" />
                            <asp:BoundField DataField="Host1" HeaderText="Host1" ReadOnly="True" SortExpression="Host1" />
                            <asp:BoundField DataField="Host2" HeaderText="Host2" ReadOnly="True" SortExpression="Host2" />
                            <asp:TemplateField HeaderText="燈號" SortExpression="Light">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("Light") %>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/img/remove.png" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                                    <PagerTemplate>
                <div style="text-align:right">
                            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First" CommandName="Page" Text="<<"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev" CommandName="Page" Text="<"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next" CommandName="Page" Text=">"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last" CommandName="Page" Text=">>"></asp:LinkButton>&nbsp;&nbsp;
                            <asp:TextBox ID="txtNewPageIndex" runat="server" Width="70px"></asp:TextBox>
                            <asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>" ForeColor="Blue"></asp:Label>
                           /<asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>" ForeColor="Blue"></asp:Label>
                            <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1" CommandName="Page" Text="GO"></asp:LinkButton>
                    </div>
            </PagerTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory_State] WHERE ([Light] = @Light) ORDER BY [DTime] DESC">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="False" Name="Light" Type="Boolean" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
