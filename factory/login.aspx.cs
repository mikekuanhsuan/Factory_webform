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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["act"] != null)
                {
                    cb_act.Checked = true;
                    tb_act.Text = Request.Cookies["act"].Value;
                }
            }
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {

            string sql = "SELECT * FROM S_User WHERE User_ID = @User_ID";
            SQLDB db = new SQLDB();
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Add(new SqlParameter("@User_ID", tb_act.Text));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                p_list.Clear();
                sql = "SELECT S.*,F.FactoryName FROM S_User as S left join Factory as F on S.FactoryID = F.FactoryID WHERE User_ID = @User_ID AND User_Pwd = @User_Pwd";
                others o = new others();
                string en = o.encryption(tb_pwd.Text);
                p_list.Add(new SqlParameter("@User_ID", tb_act.Text));
                p_list.Add(new SqlParameter("@User_Pwd", en));
                dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    if (cb_act.Checked)
                    {
                        Response.Cookies["act"].Expires = DateTime.Now.AddDays(7);
                    }
                    Response.Cookies["act"].Value = tb_act.Text.Trim();

                    Session["User_ID"] = dt.Rows[0][0].ToString();
                    Session["User_Pwd"] = dt.Rows[0][1].ToString();
                    Session["User_Name"] = dt.Rows[0][2].ToString();
                    Session["FactoryID"] = dt.Rows[0][4].ToString();
                    Session["DepartmentID"] = dt.Rows[0][5].ToString();
                    Session["Factory_Name"] = dt.Rows[0][6].ToString();
                    Response.Redirect("index.aspx");
                }
                else
                {
                    lb_error.Text = "密碼錯誤!";
                }
            }
            else
            {
                lb_error.Text = "無此帳號!";
            }

        }
    }
}