<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Tag_acct.aspx.cs" Inherits="factory.Tag_acct" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Tag選取</title>
    <script src="../js/jquery-3.5.1.slim.min.js"></script>
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
        .auto-style2 {
            font-size: large;
            color: #FF0000;
        }
        .GV1 table tbody {
            display:block;
            height: calc(68vh);
            overflow-y: overlay;
        }
        
        .GV1 table thead, .GV1 tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }
        
        .GV1 table thead {
            width: calc( 200% – 1em )
        }
        #ContentPlaceHolder1_imgb_up_group:hover,#ContentPlaceHolder1_imgb_down_group:hover{
            background:gainsboro;
        }
        .auto-style3 {
            position: relative;
            width: auto;
            flex: 0 0 auto;
            max-width: 100%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div style="border:1px solid rgba(0, 0, 0, 0.125);background:aliceblue;">
            <div id="phone" style="display: none;background:antiquewhite;height:35px;padding-top:4px">
                <strong>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="../indexm.aspx" ForeColor="Maroon" CssClass="auto-style1">首頁 ＞</asp:HyperLink>
                    <asp:Label ID="lb_name" runat="server" ForeColor="Maroon" CssClass="auto-style1">Tag選取</asp:Label>
                </strong> 
            </div>
            <div class="row">
                <div class="auto-style3">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="tb_name" runat="server" placeholder="請輸入Tag名稱"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                  TargetControlID="tb_name" 
                                  ServiceMethod="GetCompletion"
                                  ServicePath="../WebService/AutoComplete.asmx"
                                  MinimumPrefixLength="4"
                                  CompletionSetCount="30"
                                  EnableCaching="True">
                    </asp:AutoCompleteExtender> 
                    <asp:Button ID="btn_add_tag" runat="server" Class="btn btn-primary" Text="加入" Style="padding-top:3px;padding-bottom:3px" OnClick="btn_add_tag_Click" />
                    <strong>
                        <asp:Label ID="lb_error_tag" runat="server" CssClass="auto-style2" Visible="False"></asp:Label>
                    </strong>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="GV1 col-xxl-4 col-xl-5 col-lg-6 col-md-7 col-sm-8 table-responsive">
                <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataSourceID="SDS1" DataKeyNames="Group_ID" OnRowCommand="GV1_RowCommand" OnSelectedIndexChanged="GV1_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <EditItemTemplate>
                                <asp:ImageButton ID="ImageButton5" runat="server" CommandName="Update" ImageUrl="~/img/save.png" ToolTip="儲存" Width="20px" />
                                &nbsp;<asp:ImageButton ID="ImageButton6" runat="server" CommandName="Cancel" ImageUrl="~/img/error.png" ToolTip="取消" Width="20px" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                &nbsp;<asp:ImageButton ID="imgb_edit" runat="server" CommandName="Edit" ImageUrl="~/img/pen.png" Width="20px" ToolTip="編輯" />
                                &nbsp;<asp:ImageButton ID="imgb_delete" runat="server" ImageUrl="~/img/delete.png" Width="20px" OnClientClick="return confirm('確定要刪除嗎?')" CommandName="Delete" ToolTip="刪除"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="群組名稱" SortExpression="Group_DESC">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Group_DESC") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" Text='<%# Bind("Group_DESC") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SDS1" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" SelectCommand="SELECT [Group_DESC], [Group_ID] FROM [Tag_group] ORDER BY [Group_Sort] DESC" DeleteCommand="DELETE FROM [Tag_group] WHERE [Group_ID] = @Group_ID" InsertCommand="INSERT INTO [Tag_group] ([Group_DESC], [Group_ID]) VALUES (@Group_DESC, @Group_ID)" UpdateCommand="UPDATE [Tag_group] SET [Group_DESC] = @Group_DESC WHERE [Group_ID] = @Group_ID">
                    <DeleteParameters>
                        <asp:Parameter Name="Group_ID" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Group_DESC" Type="String" />
                        <asp:Parameter Name="Group_ID" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Group_DESC" Type="String" />
                        <asp:Parameter Name="Group_ID" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                    <div class="row">
                        <div class="col-auto" id="g1">
                            <asp:TextBox ID="tb_group" runat="server" placeholder="請輸入群組名稱"></asp:TextBox>
                            <asp:Button ID="btn_add_group" runat="server" Class="btn btn-success" Text="新增群組" Style="padding-top:3px;padding-bottom:3px;margin-bottom:5px" OnClick="btn_add_group_Click" />
                        </div>
                        <div class="col-auto">
                            <asp:ImageButton ID="imgb_up_group" runat="server" ImageUrl="~/img/up-arrow.png" Width="30px" Style="border:1px solid rgba(0, 0, 0, 0.125);" ToolTip="群組往上" OnClick="imgb_up_group_Click" />
            &nbsp;          <asp:ImageButton ID="imgb_down_group" runat="server" ImageUrl="~/img/down-arrow.png" Width="30px" Style="border:1px solid rgba(0, 0, 0, 0.125);" ToolTip="群組往下" OnClick="imgb_down_group_Click"/>
                        </div>
                    </div>
            </div>

            <div class="GV2 col-auto">
                <asp:GridView ID="GV2" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-hover table-bordered text-center" DataKeyNames="ServerName,TagName,SourceTag,Tag_Group" DataSourceID="SDS2">
                    <Columns>
                        <asp:BoundField DataField="ServerName" HeaderText="ServerName" ReadOnly="True" SortExpression="ServerName" Visible="False" />
                        <asp:BoundField DataField="TagName" HeaderText="TagName" ReadOnly="True" SortExpression="TagName" Visible="False" />
                        <asp:TemplateField HeaderText="Tag名稱" SortExpression="SourceTag">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SourceTag") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Select" Text='<%# Bind("SourceTag") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tag_Desc" HeaderText="Tag_Desc" SortExpression="Tag_Desc" Visible="False" />
                        <asp:BoundField DataField="Tag_Group" HeaderText="Tag_Group" ReadOnly="True" SortExpression="Tag_Group" Visible="False" />
                        <asp:BoundField DataField="Tag_MID" HeaderText="Tag_MID" SortExpression="Tag_MID" Visible="False" />
                        <asp:BoundField DataField="Tag_Sort" HeaderText="Tag_Sort" SortExpression="Tag_Sort" Visible="False" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton7" runat="server" CommandName="Delete" ImageUrl="~/img/delete.png" OnClientClick="return confirm('確定要刪除嗎?')" Width="20px" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SDS2" runat="server" ConnectionString="<%$ ConnectionStrings:ZDBConnStr %>" DeleteCommand="DELETE FROM [Tag_Name_Desc] WHERE [ServerName] = @ServerName AND [TagName] = @TagName AND [SourceTag] = @SourceTag AND [Tag_Group] = @Tag_Group" InsertCommand="INSERT INTO [Tag_Name_Desc] ([ServerName], [TagName], [SourceTag], [Tag_Desc], [Tag_Group], [Tag_MID], [Tag_Sort]) VALUES (@ServerName, @TagName, @SourceTag, @Tag_Desc, @Tag_Group, @Tag_MID, @Tag_Sort)" SelectCommand="SELECT ServerName, TagName, SourceTag, Tag_Desc, Tag_Group, Tag_MID, Tag_Sort FROM Tag_Name_Desc WHERE (Tag_Group = @Tag_Group) ORDER BY Tag_Sort DESC" UpdateCommand="UPDATE [Tag_Name_Desc] SET [Tag_Desc] = @Tag_Desc, [Tag_MID] = @Tag_MID, [Tag_Sort] = @Tag_Sort WHERE [ServerName] = @ServerName AND [TagName] = @TagName AND [SourceTag] = @SourceTag AND [Tag_Group] = @Tag_Group">
                    <DeleteParameters>
                        <asp:Parameter Name="ServerName" Type="String" />
                        <asp:Parameter Name="TagName" Type="String" />
                        <asp:Parameter Name="SourceTag" Type="String" />
                        <asp:Parameter Name="Tag_Group" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ServerName" Type="String" />
                        <asp:Parameter Name="TagName" Type="String" />
                        <asp:Parameter Name="SourceTag" Type="String" />
                        <asp:Parameter Name="Tag_Desc" Type="String" />
                        <asp:Parameter Name="Tag_Group" Type="String" />
                        <asp:Parameter Name="Tag_MID" Type="String" />
                        <asp:Parameter Name="Tag_Sort" Type="Int32" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GV1" Name="Tag_Group" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Tag_Desc" Type="String" />
                        <asp:Parameter Name="Tag_MID" Type="String" />
                        <asp:Parameter Name="Tag_Sort" Type="Int32" />
                        <asp:Parameter Name="ServerName" Type="String" />
                        <asp:Parameter Name="TagName" Type="String" />
                        <asp:Parameter Name="SourceTag" Type="String" />
                        <asp:Parameter Name="Tag_Group" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </div>
        </div>
        <div>
            <strong>
                <asp:Label ID="lb_error_group" runat="server" Text="群組名稱禁止為空!" CssClass="auto-style2" Visible="False"></asp:Label>
            </strong>
        </div>
    </div>
    <script>

        //判斷是否為手機
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
            //手機導覽列
            document.getElementById("phone").style.display = "";

            document.getElementById("ContentPlaceHolder1_tb_group").style.width = "162px";
            document.getElementById("g1").style.paddingRight = "0px";
        }
    
    </script>
</asp:Content>
