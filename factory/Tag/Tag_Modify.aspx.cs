using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace factory
{
    public partial class Tag_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                div_GV2.Visible = false;
                GV2.Visible = false;
            }
        }


        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            others o = new others();
            o.Page(sender, e);
        }

        protected void imgb_add_Click(object sender, ImageClickEventArgs e)
        {

            div_GV2.Visible = true;
            GV2.Visible = true;
            if (GV1.Rows.Count > 0)
            {
                string SourceTag = "";
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    if (i == GV1.Rows.Count - 1)
                    {
                        SourceTag += "'" + GV1.Rows[i].Cells[3].Text + "'";
                    }
                    else
                    {
                        SourceTag += "'" + GV1.Rows[i].Cells[3].Text + "',";
                    }
                }

                SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T ON L.TagName = T.TagName WHERE  L.TagName  LIKE  '%KY-T1HIST%' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND SourceTag NOT IN (" + SourceTag + ") ORDER BY L.DateTime DESC";
                Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T ON L.TagName = T.TagName WHERE  L.TagName  LIKE  '%KY-T1HIST%' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND SourceTag NOT IN (" + SourceTag + ") ORDER BY L.DateTime DESC";
            }
            else
            {
                SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag as T on L.TagName = T.TagName WHERE  L.TagName  LIKE  '%KY-T1HIST%' AND L.TagName NOT LIKE '%$%' ESCAPE '/' ORDER BY L.DateTime DESC";
                Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag as T on L.TagName = T.TagName WHERE  L.TagName  LIKE  '%KY-T1HIST%' AND L.TagName NOT LIKE '%$%' ESCAPE '/' ORDER BY L.DateTime DESC";
            }
            
        }

        protected void GV2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string i = Convert.ToString(Session["page"]);
            SDS4.SelectCommand = i;
            others o = new others();
            o.Page(sender, e);
        }

        protected void ddl_f_SelectedIndexChanged(object sender, EventArgs e)
        {
            string factory = ddl_factory.SelectedValue;
            if (GV1.Rows.Count > 0)
            {
                string SourceTag = "";
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    if (i == GV1.Rows.Count - 1)
                    {
                        SourceTag += "'" + GV1.Rows[i].Cells[3].Text + "'";
                    }
                    else
                    {
                        SourceTag += "'" + GV1.Rows[i].Cells[3].Text + "',";
                    }
                }
                SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T on L.TagName = T.TagName WHERE  L.TagName  LIKE '" + '%' + factory + '%' + "' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL AND SourceTag NOT IN (" + SourceTag + ") ORDER BY L.DateTime DESC";
                Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T on L.TagName = T.TagName WHERE  L.TagName  LIKE '" + '%' + factory + '%' + "' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL AND SourceTag NOT IN (" + SourceTag + ") ORDER BY L.DateTime DESC";
            }
            else
            {
                SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T on L.TagName = T.TagName WHERE  L.TagName  LIKE '" + '%' + factory + '%' + "' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL  ORDER BY L.DateTime DESC";
                Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T on L.TagName = T.TagName WHERE  L.TagName  LIKE '" + '%' + factory + '%' + "' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL ORDER BY L.DateTime DESC";
            }
            GV2.PageIndex = 0;
        }

        protected void ddl_group_DataBound(object sender, EventArgs e)
        {
            SDS2.SelectCommand = "SELECT * FROM Tag_Name_Desc WHERE Tag_Group = '" + ddl_group.SelectedValue + "'";
        }

        protected void ddl_group_TextChanged(object sender, EventArgs e)
        {
            div_GV2.Visible = false;
            GV2.Visible = false;
            
        }

        protected void btn_select_Click(object sender, EventArgs e)
        {
            if (cbl_check.Items[0].Selected)
            {
                if (tb_Desc.Text != "")
                {
                    SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                }
                else if (tb_source.Text != "")
                {
                    SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                }
                else
                {
                    SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.TagName not like '%$%' escape '/' AND L.Value is not null order by L.DateTime desc";
                }
            }
            else
            {
                if (tb_Desc.Text != "")
                {
                    SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "'  AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "'  AND T.Description like '" + '%' + tb_Desc.Text + '%' + "' AND L.Value is not null order by L.DateTime desc";
                }

                else if (tb_source.Text != "")
                {
                    SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null  order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE CHARINDEX('" + tb_source.Text + "',L.SourceTag) > 0 AND L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null  order by L.DateTime desc";
                }
                else
                {
                    SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null order by L.DateTime desc";
                    Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value from Live as L left join Tag as T on L.TagName = T.TagName WHERE  L.TagName like '" + '%' + ddl_factory.SelectedValue + '%' + "' AND L.Value is not null order by L.DateTime desc";
                }
            }
        }

        protected void GV2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //新增資料
            List<SqlParameter> p_list = new List<SqlParameter>();
            SQLDB db = new SQLDB();
            string sql = "INSERT INTO Tag_Name_Desc(ServerName,TagName,SourceTag,Tag_Desc,Tag_Group) VALUES(@ServerName,@TagName,@SourceTag,@Tag_Desc,@Tag_Group)";
            p_list.Add(new SqlParameter("@ServerName", ddl_factory.SelectedValue));
            p_list.Add(new SqlParameter("@TagName", GV2.Rows[GV2.SelectedIndex].Cells[2].Text));
            p_list.Add(new SqlParameter("@SourceTag", GV2.Rows[GV2.SelectedIndex].Cells[3].Text));
            p_list.Add(new SqlParameter("@Tag_Desc", GV2.Rows[GV2.SelectedIndex].Cells[1].Text));
            p_list.Add(new SqlParameter("@Tag_Group", ddl_group.SelectedValue));
            db.RunCmd(sql, p_list, CommandType.Text);
            GV1.DataBind();

            string factory = ddl_factory.SelectedValue;
            string SourceTag = "";
            for (int i = 0; i < GV1.Rows.Count; i++)
            {
                if (i == GV1.Rows.Count - 1)
                {
                    SourceTag += "'" + GV1.Rows[i].Cells[3].Text + "'";
                }
                else
                {
                    SourceTag += "'" + GV1.Rows[i].Cells[3].Text + "',";
                }
            }
            SDS4.SelectCommand = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T on L.TagName = T.TagName WHERE  L.TagName  LIKE '" + '%' + factory + '%' + "' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL AND SourceTag NOT IN (" + SourceTag + ") ORDER BY L.DateTime DESC";
            Session["page"] = "SELECT T.Description,L.TagName,L.SourceTag,L.DateTime,L.Value FROM Live AS L LEFT JOIN Tag AS T on L.TagName = T.TagName WHERE  L.TagName  LIKE '" + '%' + factory + '%' + "' AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL AND SourceTag NOT IN (" + SourceTag + ") ORDER BY L.DateTime DESC";


        }
    }
}