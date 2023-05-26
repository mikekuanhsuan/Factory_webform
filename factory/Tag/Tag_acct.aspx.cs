using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;
using System.Data.SqlClient;


namespace factory
{
    public partial class Tag_acct : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        public SQLDB db = new SQLDB();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ff.title(GV1);
                GV1.DataBind();
            }

        }
        
        /*
        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            GridViewRow row = GV1.SelectedRow;
            DataTable dt = new DataTable();
            if (GV2.Rows.Count == 0)
            {
                dt.Columns.Add("ServerName", typeof(string));
                dt.Columns.Add("TagName", typeof(string));
                dt.Columns.Add("SourceTag", typeof(string));
                dt.Columns.Add("TagDesc", typeof(string));
                //dt.Columns.Add("Rep_Live", typeof(CheckBox));
                //dt.Columns.Add("Rep_Hour", typeof(CheckBox));
                //dt.Columns.Add("Rep_Min", typeof(CheckBox));
            }
            else
            {
                dt = ViewState["GV2"] as DataTable;
            }

            Label lb_TagName = (Label)GV1.Rows[GV1.SelectedIndex].FindControl("lb_TagName");
            string TagName = lb_TagName.Text;
            string SourceTag = row.Cells[3].Text;
            string TagDesc = row.Cells[1].Text;
            if (row.Cells[2].Text == "&nbsp;")
            {
                TagName = " ";
            }

            if (row.Cells[3].Text == "&nbsp;")
            {
                SourceTag = " ";
            }

            if (row.Cells[1].Text == "&nbsp;")
            {
                TagDesc = " ";
            }
            DataRow NewRow = dt.NewRow();
            string ServerName = TagName.Substring(0, 9);
            NewRow["ServerName"] = ServerName;
            NewRow["TagName"] = TagName;
            NewRow["SourceTag"] = SourceTag;
            NewRow["TagDesc"] = TagDesc;

            int x = 0;
            if (GV2.Rows.Count == 0)
            {
                dt.Rows.Add(NewRow);
            }
            else
            {
                for (int i = 0; i < GV2.Rows.Count; i++)
                {
                    if (GV2.Rows[i].Cells[3].Text == SourceTag)
                    {
                        x++;
                    } 
                }
                if (x == 0)
                {
                    dt.Rows.Add(NewRow);
                }
            }
           
            ViewState["GV2"] = dt;
            GV2.DataSource = dt;
            GV2.DataBind();
            btn_confirm.Visible = true;
            tb_group_name.Visible = true;
            
        }
        */
        protected void GV2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;
            for (int i = 0; i < GV2.Columns.Count; i++)
            {
                dc = new DataColumn();
                dc.ColumnName = GV2.Columns[i].HeaderText;
                dt.Columns.Add(dc);
            }
            for (int i = 0; i < GV2.Rows.Count; i++)
            {
                dr = dt.NewRow();
                for (int j = 0; j < GV2.Columns.Count; j++)
                {
                    dr[j] = GV2.Rows[i].Cells[j].Text;
                }
                dt.Rows.Add(dr);
            }
            dt.Rows[GV2.SelectedIndex].Delete();
            ViewState["GV2"] = dt;
            GV2.DataSource = dt;
            GV2.DataBind();
            */
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            /*
            SQLDB db = new SQLDB();
            string sql = "SELECT TOP 1 Tag_Group FROM Tag_Name_Desc ORDER BY Tag_Group DESC";
            DataTable dt =  db.GetDataTable(sql, CommandType.Text);
            string number = "";
            if (dt.Rows.Count > 0)
            {
                number = dt.Rows[0][0].ToString();
            }
            else
            {
                number = "0";
            }
            string G = "G";
            int n = Convert.ToInt32(number.Substring(1, 4));
            n += 1;
            if (n < 10)
            {
                number = "G000" + n.ToString();
            }
            else if (n < 100)
            {
                number = "G00" + n.ToString();
            }
            else if (n < 1000)
            {
                number = "G0" + n.ToString();
            }
            else
            {
                number = "G" + n.ToString();
            }
            if (tb_group_name.Text == "" || tb_group_name.Text == null)
            {
                lb_error.Visible = true;
            }
            else
            {
                //新增群組
                List<SqlParameter> p_list = new List<SqlParameter>();
                sql = "INSERT INTO Tag_group VALUES(@number,@group_name)";
                p_list.Add(new SqlParameter("@group_name", tb_group_name.Text));
                p_list.Add(new SqlParameter("@number", number));
                db.RunCmd(sql, p_list, CommandType.Text);
                //新增資料
                for (int i = 0; i < GV2.Rows.Count; i++)
                {

                    string ServerName = GV2.Rows[i].Cells[1].Text;
                    string TagName = GV2.Rows[i].Cells[2].Text;
                    string SourceTag = GV2.Rows[i].Cells[3].Text;
                    string TagDesc = GV2.Rows[i].Cells[4].Text;

                    //string sql = "SELECT isnull(MAX(tag_order)+1,1) FROM TagList";
                    //DataTable dt = db.GetDataTable(sql,CommandType.Text);
                    sql = "IF NOT EXISTS (SELECT * FROM Tag_Name_Desc WHERE TagName = @T) BEGIN INSERT INTO Tag_Name_Desc(ServerName,TagName,SourceTag,Tag_Desc,Tag_Group) VALUES (@ServerName, @TagName,@SourceTag,@TagDesc,@number) END";
                    p_list.Clear();
                    p_list.Add(new SqlParameter("@T", TagName));
                    p_list.Add(new SqlParameter("@ServerName", ServerName));
                    p_list.Add(new SqlParameter("@TagName", TagName));
                    p_list.Add(new SqlParameter("@SourceTag", SourceTag));
                    p_list.Add(new SqlParameter("@TagDesc", TagDesc));
                    p_list.Add(new SqlParameter("@number", number));
                    db.RunCmd(sql, p_list, CommandType.Text);
                }
                Response.Redirect("Tag_Modify.aspx");
            }
            */
        }

        protected void btn_add_tag_Click(object sender, EventArgs e)
        {
            lb_error_group.Visible = false;
            //判斷有無選擇群組
            if (GV1.SelectedIndex == -1)
            {
                lb_error_tag.Text = "請選擇群組";
                lb_error_tag.Visible = true;
            }
            else
            {
                //取得Tag名稱
                List<SqlParameter> par_list = new List<SqlParameter>();
                lb_error_tag.Visible = false;
                string sql = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value,L.SourceServer FROM Live AS L " +
                    "LEFT JOIN Tag AS T ON L.TagName = T.TagName " +
                    "WHERE SourceTag = @SourceTag AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL ORDER BY L.DateTime DESC";
                par_list.Add(new SqlParameter("@SourceTag", tb_name.Text));
                db.replace(1);
                DataTable dt = db.GetDataTable(sql,par_list,CommandType.Text);
                //判斷此Tag是否存在
                if (dt.Rows.Count > 0)
                {
                    //判斷是否已經存在
                    string key = GV1.DataKeys[GV1.SelectedIndex].Value.ToString();
                    db.replace(2);
                    sql = "SELECT SourceTag FROM Tag_Name_Desc WHERE SourceTag = @SourceTag AND Tag_Group = @Tag_Group";
                    par_list.Clear();
                    par_list.Add(new SqlParameter("@SourceTag", tb_name.Text));
                    par_list.Add(new SqlParameter("@Tag_Group", key));
                    DataTable dc = db.GetDataTable(sql, par_list, CommandType.Text);
                    if (dc.Rows.Count > 0)
                    {
                        lb_error_tag.Text = "Tag已存在!";
                        lb_error_tag.Visible = true;
                    }
                    else
                    {
                        lb_error_tag.Visible = false;
                        sql = "SELECT MAX(Tag_Sort) FROM Tag_Name_Desc WHERE Tag_Group = '" + key + "'";
                        DataTable dx = db.GetDataTable(sql, CommandType.Text);
                        sql = "INSERT INTO Tag_Name_Desc VALUES(@ServerName,@TagName,@SourceTag,@Tag_Desc,@Tag_Group,NULL,@Tag_Sort)";
                        par_list.Clear();
                        par_list.Add(new SqlParameter("@ServerName", dt.Rows[0][5].ToString()));
                        par_list.Add(new SqlParameter("@TagName", dt.Rows[0][1].ToString()));
                        par_list.Add(new SqlParameter("@SourceTag", dt.Rows[0][2].ToString()));
                        par_list.Add(new SqlParameter("@Tag_Desc", dt.Rows[0][0].ToString()));
                        par_list.Add(new SqlParameter("@Tag_Group", key));
                        par_list.Add(new SqlParameter("@Tag_Sort", dx.Rows[0][0].ToString()));
                        db.RunCmd(sql, par_list, CommandType.Text);
                        GV2.DataBind();
                    }
                }
                else
                {
                    lb_error_tag.Text = "Tag名稱有誤!";
                    lb_error_tag.Visible = true;
                }
            }
        }

        protected void GV1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ff.title(GV1);
            GV1.DataBind();
        }

        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            lb_error_group.Visible = false;
            lb_error_tag.Visible = false;
            //被選取到的那列增加背景顏色
            for (int i = 0; i < GV1.Rows.Count; i++)
            {
                if (GV1.SelectedIndex == i)
                {
                    GV1.Rows[i].Attributes.Add("style", "background :#FFFDD7");
                }
                else
                {
                    GV1.Rows[i].Attributes.Clear();
                }
            }
            Session["S"] = GV1.SelectedIndex;
        }

        protected void btn_add_group_Click(object sender, EventArgs e)
        {
            //新增群組
            lb_error_tag.Visible = false;
            if (tb_group.Text == "" || tb_group.Text == null)
            {
                lb_error_group.Visible = true;
            }
            else
            {
                //先取得最大的編號
                SQLDB db = new SQLDB();
                string sql = "SELECT TOP 1 Group_ID FROM Tag_group ORDER BY Group_ID DESC";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                string number = "";
                if (dt.Rows.Count > 0)
                {
                    number = dt.Rows[0][0].ToString();
                }
                else
                {
                    number = "0";
                }
                string G = "G";
                //取得群組編號的數字後+1
                int n = Convert.ToInt32(number.Substring(1, 4));
                n += 1;
                if (n < 10)
                {
                    number = "G000" + n.ToString();
                }
                else if (n < 100)
                {
                    number = "G00" + n.ToString();
                }
                else if (n < 1000)
                {
                    number = "G0" + n.ToString();
                }
                else
                {
                    number = "G" + n.ToString();
                }
                sql = "SELECT TOP 1 Group_Sort FROM Tag_group ORDER BY Group_Sort DESC";
                dt = db.GetDataTable(sql, CommandType.Text);
                int sort = 1;
                if (dt.Rows.Count > 0)
                {
                    sort = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                }

                sql = "INSERT INTO Tag_group VALUES(@number,@name,@sort)";
                List<SqlParameter> par_list = new List<SqlParameter>();
                par_list.Add(new SqlParameter("@number", number));
                par_list.Add(new SqlParameter("@name", tb_group.Text));
                par_list.Add(new SqlParameter("@sort", sort));
                db.RunCmd(sql, par_list, CommandType.Text);
                GV1.DataBind();
                lb_error_group.Visible = false;
            }
        }

        protected void imgb_up_group_Click(object sender, ImageClickEventArgs e)
        {
            int g = Convert.ToInt32(Session["S"].ToString());
            string key = GV1.DataKeys[g].Value.ToString();

            //第一筆資料不能再往上了
            if (g != 0)
            {
                //取得目前資料的排序並且+1
                string sql = "SELECT Group_Sort FROM Tag_group WHERE Group_ID = '" + key + "'";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                int up_sort = Convert.ToInt32(dt.Rows[0][0].ToString());
                int down_sort = Convert.ToInt32(dt.Rows[0][0].ToString());
                up_sort += 1;
                //當前選中的那筆資料往上
                sql = "UPDATE Tag_group SET Group_Sort = " + up_sort + " WHERE Group_ID = '" + key + "'";
                db.RunCmd(sql, CommandType.Text);
                //上面那筆資料往下
                sql = "UPDATE Tag_group SET Group_Sort = " + down_sort + " WHERE Group_ID = '" + GV1.DataKeys[g-1].Value.ToString() + "'";
                db.RunCmd(sql, CommandType.Text);
                GV1.DataBind();

                //往上的那列新增背景
                GV1.Rows[g-1].Attributes.Add("style", "background :#FFFDD7");
                
                Session["S"] = Convert.ToInt32(Session["S"].ToString()) - 1;
                SDS2.SelectCommand = "SELECT * FROM Tag_Name_Desc WHERE Tag_Group = '" + key+"'";
            }
        }


        protected void imgb_down_group_Click(object sender, ImageClickEventArgs e)
        {
            int g = Convert.ToInt32(Session["S"].ToString());
            string key = GV1.DataKeys[g].Value.ToString();

            //最後一筆資料不能往下
            if (g != GV1.Rows.Count - 1)
            {
                //取得目前資料的排序並且-1
                string sql = "SELECT Group_Sort FROM Tag_group WHERE Group_ID = '" + key + "'";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                int up_sort = Convert.ToInt32(dt.Rows[0][0].ToString());
                int down_sort = Convert.ToInt32(dt.Rows[0][0].ToString());
                up_sort -= 1;
                //當前選中的那筆資料往下
                sql = "UPDATE Tag_group SET Group_Sort = " + up_sort + " WHERE Group_ID = '" + key + "'";
                db.RunCmd(sql, CommandType.Text);
                //下面那筆資料往上
                sql = "UPDATE Tag_group SET Group_Sort = " + down_sort + " WHERE Group_ID = '" + GV1.DataKeys[g + 1].Value.ToString() + "'";
                db.RunCmd(sql, CommandType.Text);
                GV1.DataBind();

                //往下的那列新增背景
                GV1.Rows[g + 1].Attributes.Add("style", "background :#FFFDD7");

                Session["S"] = Convert.ToInt32(Session["S"].ToString()) + 1;
                SDS2.SelectCommand = "SELECT * FROM Tag_Name_Desc WHERE Tag_Group = '" + key + "'";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}