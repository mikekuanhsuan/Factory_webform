using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;

namespace factory.Mill
{
    public partial class cycle_Mill : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        protected void Page_Load(object sender, EventArgs e)
        {
            tb_SDATE.Attributes.Add("readonly", "readonly");
            tb_EDATE.Attributes.Add("readonly", "readonly");
            string F = Request.QueryString["F"];
            string name = ff.factory_name(F);
            if (name == "")
            {
                Response.Redirect("/Mill/cycle_Mill.aspx?F=KY-T1HIST");
            }
            //開始和結束時間
            string time_s = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string time_e = DateTime.Now.ToString("yyyy-MM-dd");
            tb_SDATE.Text = time_s;
            tb_EDATE.Text = time_e;
            //dropdownlist新增
            if (F == "KY-T1HIST")
            {
                ddl_group.Items.Add(new ListItem("#1#2", "r1r2"));
                ddl_group.Items.Add(new ListItem("#3#4", "r3r4"));
                ddl_group.Items.Add(new ListItem("#5#6", "r5r6"));
            }
            else if (F == "BL-T1HIST")
            {
                ddl_group.Items.Add(new ListItem("#1#2", "r1r2"));
                ddl_group.Items.Add(new ListItem("#3#4", "r3r4"));
            }
            else if (F == "QX-T1HIST")
            {
                ddl_group.Items.Add(new ListItem("#1", "r1"));
                ddl_group.Items.Add(new ListItem("#2#3", "r2r3"));
            }
            else if (F == "ZB-T1HIST")
            {
                ddl_group.Items.Add(new ListItem("#1#2", "r1"));
                ddl_group.Items.Add(new ListItem("#3#4", "r3r4"));
                ddl_group.Items.Add(new ListItem("#5#6", "r5r6"));
                ddl_group.Items.Add(new ListItem("#7#8", "r7r8"));
            }
            else if (F == "KH-T1HIST")
            {
                ddl_group.Items.Add(new ListItem("#1#2", "r1r2"));
            }
            else if (F == "LD-T1HIST")
            {
                ddl_group.Items.Add(new ListItem("#1", "r1"));
                ddl_group.Items.Add(new ListItem("#2", "r2"));
            }
            else if (F == "LZ-T1HIST")
            {
                ddl_group.Items.Add(new ListItem("#1#2", "r1r2"));
            }
            else
            {
                ddl_group.Items.Add(new ListItem("#1#2", "r1r2"));
            }

        }
    }
}