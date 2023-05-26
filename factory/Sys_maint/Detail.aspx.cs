using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using factory.lib;

namespace factory.Sys_maint
{
    public partial class Detail : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        public void Select_Command()
        {
            SDS1.SelectParameters.Clear();
            SDS1.SelectParameters.Add("F", DDL_factory.Text);
            SDS1.SelectParameters.Add("M", DDL_Mill.Text);
            SDS1.SelectCommand = "SELECT * FROM G_Milling_Machine_Mapping WHERE FactoryID = @F AND Mill_ID = @M";
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string F = Request.QueryString["F"];
                string name = ff.f_name(F);
                //判斷網址列F
                if (name == "")
                {
                    Response.Redirect("Detail.aspx?F=KY&M=1&B=Power");
                }
                name = name.Substring(0, 2);
                string M = Request.QueryString["M"];
                string B = Request.QueryString["B"];
                //M
                if (name == "觀音" && M != "1" && M != "3" && M != "5")
                {
                    Response.Redirect("Detail.aspx?F=" + F + "&M=1&B=Power");
                }
                else if ((name == "八里" || name == "全興") && M != "1" && M != "3")
                {
                    Response.Redirect("Detail.aspx?F=" + F + "&M=1&B=Power");
                }
                else if (name == "龍德" && M != "1" && M != "2")
                {
                    Response.Redirect("Detail.aspx?F=" + F + "&M=1&B=Power");
                }
                else if (name == "彰濱" && M != "1" && M != "3" && M != "5" && M != "7")
                {
                    Response.Redirect("Detail.aspx?F=" + F + "&M=1&B=Power");
                }
                else if (name != "觀音" && name != "八里" && name != "全興" && name != "龍德" && name != "彰濱" && M != "1")
                {
                    Response.Redirect("Detail.aspx?F=" + F + "&M=1&B=Power");
                }
                //B
                if (B != "Power" && B != "Temp" && B != "Wind" && B != "Fd" && B != "Quality")
                {
                    Response.Redirect("Detail.aspx?F=" + F + "&M=" + M + "&B=Power");
                }

                SQLDB db = new SQLDB();
                string sql = "SELECT * FROM Factory ORDER BY aOrder ASC ";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DDL_factory.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
                    if (DDL_factory.Items[i].Text == name)
                    {
                        DDL_factory.Items[i].Selected = true;
                    }
                }


                sql = "SELECT Mill_ID FROM G_Milling_Machine_Mapping WHERE FactoryID ='" + DDL_factory.Text + "'";
                db = new SQLDB();
                dt = db.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DDL_Mill.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
                        if (DDL_Mill.Items[i].Text.Replace("#", "").Substring(0, 1) == M)
                        {
                            DDL_Mill.Items[i].Selected = true;
                        }
                    }
                }
                if (F == "QX" && M == "3")
                {
                    DDL_Mill.Items[1].Selected = true;
                }

                string m = ff.get_mn(M,F);

                SDS1.SelectCommand = "SELECT * FROM G_Milling_Machine_Mapping WHERE FactoryID LIKE @F+'%'  AND Mill_ID = @M";
                SDS1.SelectParameters.Add("F", F);
                SDS1.SelectParameters.Add("M", m);

                hyl_Power.NavigateUrl = "Detail.aspx?F=" + F + "&M=" + M + "&B=Power";
                hyl_Temp.NavigateUrl = "Detail.aspx?F=" + F + "&M=" + M + "&B=Temp";
                hyl_Wind.NavigateUrl = "Detail.aspx?F=" + F + "&M=" + M + "&B=Wind";
                hyl_Fd.NavigateUrl = "Detail.aspx?F=" + F + "&M=" + M + "&B=Fd";

            }
        }

        protected void DV1_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            
            if (e.CommandName == "Edit")
            {
                Select_Command();
            }

        }
        protected void DV1_ModeChanged(object sender, EventArgs e)
        {
            Select_Command();
        }


        protected void DV1_DataBound(object sender, EventArgs e)
        {
            string B = Request.QueryString["B"];
            if (DV1.Rows.Count > 1)
            {
                if (B == "Power")
                {
                    for (int i = 0; i < 49; i++)
                    {
                        DV1.Rows[i + 14].Visible = false;
                    }
                }
                else if (B == "Temp")//25
                {
                    
                    for (int i = 0; i < 11; i++)
                    {
                        DV1.Rows[i + 3].Visible = false;
                    }

                    for (int i = 0; i < 23; i++)
                    {
                        DV1.Rows[i + 40].Visible = false;
                    }
                }
                else if (B == "Wind")//11
                {
                    for (int i = 0; i < 37; i++)
                    {
                        DV1.Rows[i + 3].Visible = false;
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        DV1.Rows[i + 51].Visible = false;
                    }
                }
                else if (B == "Fd")
                {
                    for (int i = 0; i < 48; i++)
                    {
                        DV1.Rows[i + 3].Visible = false;
                    }
                }
            }
            
        }


        protected void DDL_factory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_Mill.Items.Clear();
            string sql = "SELECT Mill_ID FROM G_Milling_Machine_Mapping WHERE FactoryID ='"+DDL_factory.Text+"'";
            SQLDB db = new SQLDB();
            DataTable dt = db.GetDataTable(sql, CommandType.Text);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DDL_Mill.Items.Add(new ListItem(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString()));
                }
            }
            string F = DDL_factory.Text.Substring(0, 2);
            string M = DDL_Mill.Text.Replace("#", "").Substring(0, 1);
            string B = Request.QueryString["B"];
            Response.Redirect("Detail.aspx?F=" + F + "&M=" + M + "&B="+B+"");
            
            
        }

        protected void DDL_Mill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string F = DDL_factory.Text.Substring(0, 2);
            string M = DDL_Mill.Text.Replace("#", "").Substring(0, 1);
            string B = Request.QueryString["B"];
            if (F == "QX" && M == "2")
            {
                Response.Redirect("Detail.aspx?F=" + F + "&M=3&B=" + B + "");
            }
            else
            {
                Response.Redirect("Detail.aspx?F=" + F + "&M=" + M + "&B=" + B + "");
            }

        }

    }
}