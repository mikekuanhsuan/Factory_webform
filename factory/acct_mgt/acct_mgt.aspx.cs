using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using factory.lib;
using System.Data.SqlClient;

namespace factory.acct_mgt
{
    public partial class acct_mgt : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            others o = new others();
            o.Page(sender, e);
        }

        protected void GV1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FV1.ChangeMode(FormViewMode.ReadOnly);
            //按下選取後選後的按鈕會隱藏 其他兩個會顯示
            for (int i = 0; i < GV1.Rows.Count; i++)
            {
                LinkButton lb = (LinkButton)GV1.Rows[i].FindControl("lnb_change");
                lb.Visible = false;
                ImageButton ib_c = (ImageButton)GV1.Rows[i].FindControl("imgb_uncheck");
                ImageButton ib_s = (ImageButton)GV1.Rows[i].FindControl("imgb_check");
                if (GV1.SelectedIndex == i)
                {
                    ib_s.Visible = true;
                    ib_c.Visible = false;
                }
                else
                {
                    ib_s.Visible = false;
                    ib_c.Visible = true;
                }
            }
        }

        protected void FV1_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            SQLDB db = new SQLDB();
            TextBox tb_acct = (TextBox)FV1.FindControl("tb_acct");
            TextBox tb_pwd = (TextBox)FV1.FindControl("tb_pwd");
            TextBox tb_name = (TextBox)FV1.FindControl("tb_name");
            TextBox tb_email = (TextBox)FV1.FindControl("tb_email");
            DropDownList ddl_f = (DropDownList)FV1.FindControl("ddl_f");
            TextBox tb_dept = (TextBox)FV1.FindControl("tb_dept");
            Label lb_err = (Label)FV1.FindControl("lb_err");
            List<SqlParameter> p_list = new List<SqlParameter>();
            others o = new others();
            Label lb_acct = (Label)FV1.FindControl("lb_acct");
            //FV修改資料
            if (e.CommandName == "save")
            {
                //判斷密碼是否有改 如果沒有密碼不能變動
                string sql = "SELECT * FROM S_User WHERE User_ID = @tb_acct";
                p_list.Clear();
                p_list.Add(new SqlParameter("@tb_acct", lb_acct.Text));
                DataTable dt = db.GetDataTable(sql, p_list ,CommandType.Text);
                string pwd = dt.Rows[0][1].ToString();
                string en = o.encryption(tb_pwd.Text);
                if (pwd == tb_pwd.Text)
                {
                    en = pwd;
                }

                p_list.Clear();
                p_list.Add(new SqlParameter("@tb_acct", lb_acct.Text));
                p_list.Add(new SqlParameter("@ddl_f", ddl_f.SelectedValue));
                p_list.Add(new SqlParameter("@tb_dept", tb_dept.Text));
                p_list.Add(new SqlParameter("@en", en));
                p_list.Add(new SqlParameter("@tb_name", tb_name.Text));
                p_list.Add(new SqlParameter("@tb_email", tb_email.Text));
                sql = "UPDATE S_User SET FactoryID = @ddl_f, DepartmentID  = @tb_dept," +
                "User_Pwd = @en, User_Name = @tb_name, Email = @tb_email WHERE User_ID = @tb_acct";
                db.RunCmd(sql, p_list, CommandType.Text);
                FV1.ChangeMode(FormViewMode.ReadOnly);
                FV1.DataBind();
                GV1.DataBind();

            }
            if (e.CommandName == "add")
            {
                string sql = "SELECT * FROM S_User WHERE User_ID = @tb_acct";
                p_list.Add(new SqlParameter("@tb_acct", tb_acct.Text));
                DataTable dt = db.GetDataTable(sql, p_list , CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    lb_err.Text = "重複帳號";
                }
                else
                {
                    if (tb_acct.Text == "" || tb_pwd.Text == "" || tb_name.Text == "")
                    {
                        lb_err.Text = "資料未填齊";
                    }
                    else
                    {
                        string en = o.encryption(tb_pwd.Text);
                        
                        sql = "INSERT INTO S_User VALUES(@tb_acct, @tb_pwd, @tb_name, @tb_email, @ddl_f, @tb_dept)";
                        p_list.Clear();
                        p_list.Add(new SqlParameter("@tb_acct", tb_acct.Text));
                        p_list.Add(new SqlParameter("@tb_pwd", en));
                        p_list.Add(new SqlParameter("@tb_name", tb_name.Text));
                        p_list.Add(new SqlParameter("@tb_email", tb_email.Text));
                        p_list.Add(new SqlParameter("@ddl_f", ddl_f.SelectedValue));
                        p_list.Add(new SqlParameter("@tb_dept", tb_dept.Text));
                        db.RunCmd(sql, p_list ,CommandType.Text);
                        //FV1.ChangeMode(FormViewMode.ReadOnly);
                        //FV1.DataBind();
                        //手動清空欄位
                        tb_acct.Text = "";
                        tb_pwd.Text = "";
                        tb_name.Text = "";
                        tb_email.Text = "";
                        tb_dept.Text = "";
                        lb_err.Text = "新增成功!";
                        GV1.DataBind();
                    }
                }
            }
        }

        protected void imgb_add_Click(object sender, ImageClickEventArgs e)
        {
            FV1.ChangeMode(FormViewMode.Insert);
        }

        protected void FV1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            GV1.DataBind();
        }
    }
}