<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="acct_mgt.aspx.cs" Inherits="factory.acct_mgt.acct_mgt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>帳號管理</title>
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
    <style type="text/css">
        .auto-style4 {
            text-align: right;
            font-size: large;
        }
        .auto-style5 {
            display: block;
            width: 100%;
            overflow-x: auto;
            -webkit-overflow-scrolling: touch;
            margin-bottom: 0px;
        }
        .auto-style6 {
            text-align: right;
            height: 26px;
        }
        .auto-style7 {
            height: 26px;
        }
        .auto-style8 {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-auto" style="height:20px">
            <asp:ImageButton ID="imgb_add" runat="server" ImageUrl="~/img/add.png" Width="20px" data-placement="right" data-toggle="tooltip" title="新增帳號" OnClick="imgb_add_Click" />
        </div>
    </div>
    <div class="row"style="overflow-x:auto;">
        <div class="col-auto">
            <asp:GridView ID="GV1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="User_ID" DataSourceID="SDS1" CssClass="table table-sm table-hover table-bordered" PageSize="15" OnPageIndexChanging="GV1_PageIndexChanging" OnSelectedIndexChanged="GV1_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnb_change" runat="server" CausesValidation="False" CommandName="Select" Text="選取"></asp:LinkButton>
                            <asp:ImageButton ID="imgb_uncheck" runat="server" CommandName="Select" ImageUrl="~/img/square.png" Width="20px" Visible="False" />
                            <asp:ImageButton ID="imgb_check" runat="server" CommandName="Select" ImageUrl="~/img/squarecheck.png" Width="20px" Visible="False" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="User_ID" HeaderText="帳號" />
                    <asp:BoundField DataField="User_Pwd" HeaderText="密碼" SortExpression="User_Pwd" />
                    <asp:BoundField DataField="User_Name" HeaderText="姓名" SortExpression="User_Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="FactoryID" HeaderText="工廠編號" SortExpression="FactoryID" />
                    <asp:BoundField DataField="DepartmentID" HeaderText="部門編號" SortExpression="DepartmentID" />
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
            <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [S_User]"></asp:SqlDataSource>
        </div>
        <div class="col-auto">
            <asp:FormView ID="FV1" runat="server" CellPadding="4" DataKeyNames="User_ID" DataSourceID="SDS2" ForeColor="#333333" CssClass="auto-style5" OnItemCommand="FV1_ItemCommand" OnItemDeleted="FV1_ItemDeleted">
                <EditItemTemplate>
                    <asp:ImageButton ID="ImageButton5" runat="server" CommandName="save" ImageUrl="~/img/save.png" Width="20px" data-placement="right" data-toggle="tooltip" title="存檔" />
                    &nbsp;<asp:ImageButton ID="ImageButton6" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/img/error.png" Width="20px" data-placement="right" data-toggle="tooltip" title="取消" />
                    <br />
                    <table class="w-100">
                        <tr>
                            <td class="auto-style6"><strong><span class="auto-style8">帳號:</span> </strong></td>
                            <td class="auto-style7"><strong>&nbsp;</strong><asp:Label ID="lb_acct" runat="server" Text='<%# Bind("User_ID") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>密碼:</strong></td>
                            <td><strong>&nbsp;<asp:TextBox ID="tb_pwd" runat="server" Text='<%# Bind("User_Pwd") %>' />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>姓名:</strong></td>
                            <td><strong>&nbsp;<asp:TextBox ID="tb_name" runat="server" Text='<%# Bind("User_Name") %>' />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>Email:</strong></td>
                            <td><strong>&nbsp;<asp:TextBox ID="tb_Email" runat="server" Text='<%# Bind("Email") %>' TextMode="Email" />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>工廠編號:</strong></td>
                            <td><strong>&nbsp;<asp:DropDownList ID="ddl_f" runat="server" DataSourceID="SDS3" DataTextField="FactoryID" DataValueField="FactoryID" SelectedValue='<%# Bind("FactoryID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SDS3" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory] ORDER BY [aOrder]"></asp:SqlDataSource>
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>部門編號:</strong></td>
                            <td><strong>&nbsp;</strong></strong><strong><asp:TextBox ID="tb_dept" runat="server" Text='<%# Bind("DepartmentID") %>' />
                                </strong></td>
                        </tr>
                    </table>
                    <strong>
                    <asp:Label ID="lb_err" runat="server" CssClass="auto-style4" style="font-size: large; color: #FF0000"></asp:Label>
                    </strong>
                </EditItemTemplate>
                <EditRowStyle BackColor="#E3EAEB" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <InsertItemTemplate>
                    <asp:ImageButton ID="ImageButton9" runat="server" CommandName="add" ImageUrl="~/img/data add.png" Width="20px" />
                    &nbsp;<asp:ImageButton ID="ImageButton8" runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/img/error.png" Width="20px" />
                    <table class="w-100">
                        <tr>
                            <td class="auto-style4"><strong><span class="auto-style8">帳號:</span></strong></td>
                            <td>
                                <strong>&nbsp;<asp:TextBox ID="tb_acct" runat="server" Text='<%# Bind("User_ID") %>' />
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6"><strong><span class="auto-style8">密碼:</span> </strong> </td>
                            <td class="auto-style7">
                                <strong>
                                &nbsp;<asp:TextBox ID="tb_pwd" runat="server" Text='<%# Bind("User_Pwd") %>' />
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>姓名:</strong></td>
                            <td>
                                <strong>&nbsp;<asp:TextBox ID="tb_name" runat="server" Text='<%# Bind("User_Name") %>' />
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>Email:</strong></td>
                            <td><strong>&nbsp;<asp:TextBox ID="tb_Email" runat="server" Text='<%# Bind("Email") %>' TextMode="Email" />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>工廠編號:</strong></td>
                            <td><strong>&nbsp;<asp:DropDownList ID="ddl_f" runat="server" DataSourceID="SDS3" DataTextField="FactoryID" DataValueField="FactoryID" SelectedValue='<%# Bind("FactoryID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SDS3" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory] ORDER BY [aOrder]"></asp:SqlDataSource>
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>部門編號:</strong></td>
                            <td><strong>&nbsp;</strong></strong><strong><asp:TextBox ID="tb_dept" runat="server" Text='<%# Bind("DepartmentID") %>' />
                                </strong></td>
                        </tr>
                    </table>
                    <strong>
                    <asp:Label ID="lb_err" runat="server" CssClass="auto-style4" style="font-size: large; color: #FF0000"></asp:Label>
                    </strong>
                    <br />
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/img/pen.png" Width="20px" data-placement="right" data-toggle="tooltip" title="編輯" />
                    &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/img/delete.png" Width="20px" data-placement="right" data-toggle="tooltip" title="刪除" OnClientClick="return confirm('確定要刪除嗎?')" />
                    <table class="w-100">
                        <tr>
                            <td class="text-right"><strong>帳號: </strong></td>
                            <td><strong>
                                &nbsp;<asp:Label ID="User_IDLabel" runat="server" CssClass="auto-style1" Text='<%# Bind("User_ID") %>' style="font-size: large" />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>密碼:</strong></td>
                            <td><strong>
                                &nbsp;<asp:Label ID="User_PasswordLabel" runat="server" CssClass="auto-style1" Text='<%# Bind("User_Pwd") %>' style="font-size: large" />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>姓名:</strong></td>
                            <td><strong>
                                &nbsp;<asp:Label ID="User_NameLabel" runat="server" CssClass="auto-style1" Text='<%# Bind("User_Name") %>' style="font-size: large" />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>Email:</strong></td>
                            <td>
                                <strong>
                                &nbsp;<asp:Label ID="USer_EmailLabel" runat="server" CssClass="auto-style3" Text='<%# Bind("Email") %>'></asp:Label>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>工廠編號:</strong></td>
                            <td><strong>
                                &nbsp;<asp:Label ID="FactoryIDLabel" runat="server" CssClass="auto-style1" Text='<%# Bind("FactoryID") %>' style="font-size: large" />
                                </strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4"><strong>部門編號:</strong></td>
                            <td><strong>
                                &nbsp;<asp:Label ID="DepartmentIDLabel" runat="server" CssClass="auto-style1" Text='<%# Bind("DepartmentID") %>' style="font-size: large" />
                                </strong><strong>
                                </strong></strong></td>
                        </tr>
                    </table>
                </ItemTemplate>
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
            </asp:FormView>
            <asp:SqlDataSource ID="SDS2" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" DeleteCommand="DELETE FROM [S_User] WHERE [User_ID] = @User_ID" InsertCommand="INSERT INTO [S_User] ([User_ID], [FactoryID], [DepartmentID], [User_Account], [User_Pwd], [User_Name]) VALUES (@User_ID, @FactoryID, @DepartmentID, @User_Account, @User_Pwd, @User_Name)" SelectCommand="SELECT * FROM [S_User] WHERE ([User_ID] = @User_ID)" UpdateCommand="UPDATE [S_User] SET [FactoryID] = @FactoryID, [DepartmentID] = @DepartmentID, [User_Account] = @User_Account, [User_Pwd] = @User_Pwd, [User_Name] = @User_Name WHERE [User_ID] = @User_ID">
                <DeleteParameters>
                    <asp:Parameter Name="User_ID" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="User_ID" Type="String" />
                    <asp:Parameter Name="FactoryID" Type="String" />
                    <asp:Parameter Name="DepartmentID" Type="String" />
                    <asp:Parameter Name="User_Account" Type="String" />
                    <asp:Parameter Name="User_Pwd" Type="String" />
                    <asp:Parameter Name="User_Name" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="GV1" Name="User_ID" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="FactoryID" Type="String" />
                    <asp:Parameter Name="DepartmentID" Type="String" />
                    <asp:Parameter Name="User_Account" Type="String" />
                    <asp:Parameter Name="User_Pwd" Type="String" />
                    <asp:Parameter Name="User_Name" Type="String" />
                    <asp:Parameter Name="User_ID" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>

</asp:Content>
