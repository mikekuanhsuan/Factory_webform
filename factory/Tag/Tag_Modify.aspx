<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tag_Modify.aspx.cs" Inherits="factory.Tag_update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
    <title>Tag報表</title>
    <style type="text/css">
        .auto-style1 {
            font-size: large;
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
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">Tag設定</asp:Label>
                </strong> 
            </div>

            <div class="row">
                <div class="col-auto" style="padding-right:0px;padding-top:3px">
                    <asp:ImageButton ID="imgb_add" runat="server" ImageUrl="~/img/add.png" ToolTip="新增資料" Width="20px" OnClick="imgb_add_Click" />
                </div>
                <div class="col-auto">
                    <asp:Label ID="Label2" runat="server" Text="群組:" CssClass="auto-style1" style="font-size: large"></asp:Label>
                    <asp:DropDownList ID="ddl_group" runat="server" DataSourceID="SDS1" DataTextField="Group_DESC" DataValueField="Group_ID" AutoPostBack="True" OnDataBound="ddl_group_DataBound" OnTextChanged="ddl_group_TextChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Tag_group] ORDER BY [Group_ID] DESC"></asp:SqlDataSource>
                </div>
            </div>
        </div>
        
            <div class="table-responsive col-xl-8 col-12" style="padding:0px">
                <asp:GridView ID="GV1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered" DataKeyNames="ServerName,TagName,SourceTag" DataSourceID="SDS2" OnPageIndexChanging="GV1_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" CommandName="Update" ImageUrl="~/img/save.png" Width="20px" ToolTip="儲存" />
                                &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" CommandName="Cancel" ImageUrl="~/img/error.png" ToolTip="取消" Width="20px" />
                            </EditItemTemplate>

                            <ItemTemplate>
                                &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" CommandName="Edit" ImageUrl="~/img/edit.png" Width="20px" ToolTip="編輯" />
                                &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ImageUrl="~/img/delete.png"  Width="20px" OnClientClick="return confirm('您確定要刪除？');" ToolTip="刪除" />
                            </ItemTemplate>
                            <HeaderStyle Width="70px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="ServerName" HeaderText="ServerName" ReadOnly="True" SortExpression="ServerName" />
                        <asp:BoundField DataField="TagName" HeaderText="TagName" ReadOnly="True" SortExpression="TagName" />
                        <asp:BoundField DataField="SourceTag" HeaderText="SourceTag" ReadOnly="True" SortExpression="SourceTag" />
                        <asp:TemplateField HeaderText="Tag_Desc" SortExpression="Tag_Desc">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Tag_Desc") %>' TextMode="MultiLine" Width="100%"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Tag_Desc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                <PagerTemplate>
                <table>
                    <tr>
                    <!--    <td style="text-align: right">  --> 
                                    <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First" CommandName="Page" Text="<<"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev" CommandName="Page" Text="<"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next" CommandName="Page" Text=">"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last" CommandName="Page" Text=">>"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtNewPageIndex" runat="server" Width="70px"></asp:TextBox>&nbsp;&nbsp;
                                     <asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;
                                    / <asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1" CommandName="Page" Text="GO"></asp:LinkButton>
                      <!--  </td>--> 
                    </tr>
                </table>
                </PagerTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SDS2" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Tag_Name_Desc] WHERE ([Tag_Group] = @Tag_Group)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddl_group" Name="Tag_Group" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        

        <div runat="server" id="div_GV2" style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
            <div class="row">
                <div class="col-auto">
                    <asp:SqlDataSource ID="SDS3" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT * FROM [Factory] ORDER BY [aOrder]"></asp:SqlDataSource>
                        <strong>
                            <asp:Label ID="Label1" runat="server" Text="廠區:" CssClass="auto-style1"></asp:Label>
                        <asp:DropDownList ID="ddl_factory" runat="server" DataSourceID="SDS3" DataTextField="FactoryName" DataValueField="FactoryID" AutoPostBack="True" OnTextChanged="ddl_f_SelectedIndexChanged">
                        </asp:DropDownList>
                        </strong>
                </div>

                <div class="col-auto">
                    <asp:Label ID="Label3" runat="server" Text="Description:" CssClass="auto-style1"></asp:Label>
                    <asp:TextBox ID="tb_Desc" runat="server" Width="147px"></asp:TextBox>
                </div>

                <div class="col-auto">
                    <asp:Label ID="lb_SourceTag" runat="server" Text="SourceTag:" CssClass="auto-style1"></asp:Label>
                    <asp:TextBox ID="tb_source" runat="server" Width="147px"></asp:TextBox>
                </div>

                <div class="col-auto">
                    <asp:Label ID="Label5" runat="server" Text="不顯示:" CssClass="auto-style1"></asp:Label>

                    <asp:CheckBoxList ID="cbl_check" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" TextAlign="Left">
                        <asp:ListItem Value="1" Selected="True">$</asp:ListItem>
                    </asp:CheckBoxList>

                    <asp:Button ID="btn_select" runat="server" Text="查詢" class="btn btn-primary" Style="padding-top:3px" Height="30px" OnClick="btn_select_Click"/> 
                </div>
            </div>
        </div>
        <div class="row">
            <div class="table-responsive col-xl-8 col-12">
                <asp:GridView ID="GV2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered" DataSourceID="SDS4" OnPageIndexChanging="GV2_PageIndexChanging" OnSelectedIndexChanged="GV2_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton5" runat="server" CommandName="Select" ImageUrl="~/img/data add.png" ToolTip="新增資料" Width="20px" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                        <asp:BoundField DataField="TagName" HeaderText="TagName" SortExpression="TagName" />
                        <asp:BoundField DataField="SourceTag" HeaderText="SourceTag" SortExpression="SourceTag" />
                        <asp:BoundField DataField="DateTime" HeaderText="DateTime" SortExpression="DateTime" Visible="False" />
                        <asp:BoundField DataField="Value" HeaderText="Value" SortExpression="Value" Visible="False" />
                    </Columns>
                            <PagerTemplate>
                <table>
                    <tr>
                    <!--    <td style="text-align: right">  --> 
                                    <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First" CommandName="Page" Text="<<"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev" CommandName="Page" Text="<"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next" CommandName="Page" Text=">"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last" CommandName="Page" Text=">>"></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:TextBox ID="txtNewPageIndex" runat="server" Width="70px"></asp:TextBox>&nbsp;&nbsp;
                                     <asp:Label ID="lblPageIndex" runat="server" Text="<%#((GridView)Container.Parent.Parent).PageIndex + 1 %>" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;
                                    / <asp:Label ID="lblPageCount" runat="server" Text="<%# ((GridView)Container.Parent.Parent).PageCount %>" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1" CommandName="Page" Text="GO"></asp:LinkButton>
                      <!--  </td>--> 
                    </tr>
                </table>
                </PagerTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SDS4" runat="server" ConnectionString="<%$ ConnectionStrings:RuntimeConnStr %>" SelectCommand="select T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName order by L.DateTime desc"></asp:SqlDataSource>
            </div>
        </div>
    </div>


    <script>
        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            //手機導覽列
            document.getElementById("phone").style.display = "";
        }
    </script>
</asp:Content>
