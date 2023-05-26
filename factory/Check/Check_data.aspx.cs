using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using factory.lib;

namespace factory.Check
{
    public partial class Check_data : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        public string chart()
        {
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string D = Request.QueryString["D"];
            string E = Request.QueryString["E"];
            List<List<string>> par_list = new List<List<string>>();
            List<SqlParameter> p_list = new List<SqlParameter>();
            string d = "";
            //取得機器
            string gm = ff.get_m(M, F);
            if (M == "0")
            {
                SQLDB db = new SQLDB();
                string sql = "SELECT DateTime,ISNULL(Moisture,0),ISNULL(Specific_Surface,0),ISNULL(Residual_On_Sieve,0) FROM A_Product_Quality " +
                    "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%' AND Visible IS NOT NULL";
                p_list.Add(new SqlParameter("@F", F));
                p_list.Add(new SqlParameter("@D1", tb_SDATE.Text));
                p_list.Add(new SqlParameter("@D2", tb_EDATE.Text));
                DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            double z = Convert.ToDouble(dt.Rows[j][i + 1].ToString());
                            string datatime = Convert.ToDateTime(dt.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            if (z > 0)
                            {
                                d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                            }
                        }
                        par_list.Add(new List<string>() { d });
                        d = "";
                    }
                    par_list.Add(new List<string>() { "{ min:'" + tb_SDATE.Text + "'}" });
                    par_list.Add(new List<string>() { "{ max:'" + tb_EDATE.Text + "'}" });

                    //轉換為JSON
                    System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string datas = o.Serialize(par_list);

                    //修改格式
                    datas = datas.Replace("\\", "");
                    datas = datas.Replace("\"", "");
                    datas = datas.Replace("u0027", "'");
                    return datas;
                }
                else
                {
                    return "";
                }

            }
            else //各別的磨機
            {
                SQLDB db = new SQLDB();
                string sql = "SELECT DateTime, ISNULL(Moisture,0), ISNULL(Specific_Surface,0), ISNULL(Residual_On_Sieve,0) From A_Product_Quality" +
                " WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%' AND Mill_ID = @M AND Visible IS NOT NULL";
                p_list.Add(new SqlParameter("@F", F));
                p_list.Add(new SqlParameter("@D1", tb_SDATE.Text));
                p_list.Add(new SqlParameter("@D2", tb_EDATE.Text));
                p_list.Add(new SqlParameter("@M", gm));

                DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            double z = Convert.ToDouble(dt.Rows[j][i + 1].ToString());
                            string datatime = Convert.ToDateTime(dt.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            if (z > 0)
                            {
                                d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                            }
                        }
                        par_list.Add(new List<string>() { d });
                        d = "";
                    }
                    par_list.Add(new List<string>() { "{ min:'" + tb_SDATE.Text + "'}" });
                    par_list.Add(new List<string>() { "{ max:'" + tb_EDATE.Text + "'}" });

                    //轉換為JSON
                    System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string datas = o.Serialize(par_list);

                    //修改格式
                    datas = datas.Replace("\\", "");
                    datas = datas.Replace("\"", "");
                    datas = datas.Replace("u0027", "'");
                    return datas;

                }
                else
                {
                    return ""; 
                }

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_SDATE.Attributes.Add("readonly", "readonly");
                //F
                string md = DateTime.Now.ToString("MM/dd");
                string emd = DateTime.Now.AddDays(1).ToString("MM/dd");
                string F = Request.QueryString["F"];
                if (F != "KY" && F != "BL" && F != "QX" && F != "ZB" && F != "KH" && F != "LD" && F != "LZ" && F != "HL")
                {
                    Response.Redirect("Check_data.aspx?F=KY&M=0&D=" + md + "&E=" + emd + "");
                }
                string M = Request.QueryString["M"];
                //M
                if (F == "KY" && M != "0" && M != "1" && M != "3" && M != "5")
                {
                    Response.Redirect("Check_data.aspx?F=KY&M=0&D=" + md + "&E=" + emd + "");
                }
                else if ((F == "BL" || F == "QX") && M != "0" && M != "1" && M != "3")
                {
                    Response.Redirect("Check_data.aspx?F=" + F + "&M=0&D=" + md + " & E = " + emd + "");
                }
                else if (F == "LD" && M != "0" && M != "1" && M != "2")
                {
                    Response.Redirect("Check_data.aspx?F=LD&M=0&D=" + md + "");
                }
                else if ((F == "KH" || F == "LZ" || F == "HL") && M != "0" && M != "1")
                {
                    Response.Redirect("Check_data.aspx?F=" + F + "&M=0&D=" + md + "&E=" + emd + "");
                }
                else if (F == "ZB" && M != "0" && M != "1" && M != "3" && M != "5" && M != "7")
                {
                    Response.Redirect("Check_data.aspx?F=" + F + "&M=0&D=" + md + "&E=" + emd + "");
                }
                //D
                string D = Request.QueryString["D"];
                DateTime t;
                bool flag = DateTime.TryParse(D, out t);
                if (flag == false || D.Length != 5)
                {
                    Response.Redirect("Check_data.aspx?F=" + F + "&M="+M+"&D=" + md + "&E=" + emd + "");
                }
                //E
                string E = Request.QueryString["E"];
                bool flagg = DateTime.TryParse(E, out t);
                if (flagg == false || E.Length != 5)
                {
                    Response.Redirect("Check_data.aspx?F=" + F + "&M=" + M + "&D=" + md + "&E=" + emd + "");
                }

                SQLDB db = new SQLDB();
                string sql = "SELECT * FROM Factory ORDER BY aOrder ASC ";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                for (int i = 0; i < dt.Rows.Count-1; i++)
                {
                    DDL_factory.Items.Add(new ListItem(dt.Rows[i][1].ToString()+"廠", dt.Rows[i][0].ToString().Substring(0,2)));
                }
                string time_s = DateTime.Now.ToString("yyyy-" + D.Replace("/", "-") + "");
                string time_e = DateTime.Now.ToString("yyyy-" + E.Replace("/", "-") + "");
                tb_SDATE.Text = time_s;
                tb_EDATE.Text = time_e;

                //頁籤名稱和超連結
                List<SqlParameter> p_list = new List<SqlParameter>();
                p_list.Add(new SqlParameter("@F", F));
                sql = "SELECT Mill_ID FROM G_Milling_Machine_Mapping WHERE FactoryID LIKE @F+'%'";
                dt = db.GetDataTable(sql, p_list, CommandType.Text);
                hyl_M0.NavigateUrl = "Check_data.aspx?F=" + F + "&M=0&D=" + D + "&E=" + E + "";
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count == 4)
                    {
                        string t2 = dt.Rows[1][0].ToString();
                        hyl_M2.Text = t2;
                        hyl_M2.NavigateUrl = "Check_data.aspx?F=" + F + "&M=3&D=" + D + "&E=" + E + "";

                        string t3 = dt.Rows[2][0].ToString();
                        hyl_M3.Text = t3;
                        hyl_M3.NavigateUrl = "Check_data.aspx?F=" + F + "&M=5&D=" + D + "&E=" + E + "";

                        string t4 = dt.Rows[3][0].ToString();
                        hyl_M4.Text = t4;
                        hyl_M4.NavigateUrl = "Check_data.aspx?F=" + F + "&M=7&D=" + D + "&E=" + E + "";
                    }
                    else if (dt.Rows.Count == 3)
                    {
                        string t2 = dt.Rows[1][0].ToString();
                        hyl_M2.Text = t2;
                        hyl_M2.NavigateUrl = "Check_data.aspx?F=" + F + "&M=3&D=" + D + "&E=" + E + "";
                        
                        string t3 = dt.Rows[2][0].ToString();
                        hyl_M3.Text = t3;
                        hyl_M3.NavigateUrl = "Check_data.aspx?F=" + F + "&M=5&D=" + D + "&E=" + E + "";
                    }
                    else if (dt.Rows.Count == 2)
                    {
                        string t2 = dt.Rows[1][0].ToString();
                        hyl_M2.Text = t2;
                        hyl_M2.NavigateUrl = "Check_data.aspx?F=" + F + "&M=3&D=" + D + "&E=" + E + "";
                    }
                    string t1 = dt.Rows[0][0].ToString();
                    hyl_M1.Text = t1;
                    hyl_M1.NavigateUrl = "Check_data.aspx?F=" + F + "&M=1&D=" + D + "&E=" + E + "";
                }
                else
                {
                    bookmark.Visible = false;
                }

                for (int i = 0; i < DDL_factory.Items.Count; i++)
                {
                    if (DDL_factory.Items[i].Value == F)
                    {
                        DDL_factory.Items[i].Selected = true;
                    }
                }

                time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
                string gm = ff.get_m(M, F);
                if (M == "0")
                {
                    SDS1.SelectParameters.Clear();
                    /*
                    SDS1.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve From (Select MAX(Dtime)AS sn FROM A_Product_Quality GROUP BY LEFT(Convert(Varchar,Dtime,120),13),Mill_ID) AS A " +
                        "LEFT JOIN A_Product_Quality as Q on A.sn = Q.Dtime " +
                        "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
                        "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%'";
                    */
                    SDS1.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve , Q.Visible From A_Product_Quality as Q " +
                        "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
                        "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%' ORDER BY DateTime ASC";
                    SDS1.SelectParameters.Add("F", F);
                    SDS1.SelectParameters.Add("D1", tb_SDATE.Text);
                    SDS1.SelectParameters.Add("D2", tb_EDATE.Text);
                }
                else
                {
                    SDS1.SelectParameters.Clear();
                    /*
                    SDS1.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve From (Select MAX(Dtime)AS sn FROM A_Product_Quality GROUP BY LEFT(Convert(Varchar,Dtime,120),13),Mill_ID) AS A " +
                        "LEFT JOIN A_Product_Quality as Q on A.sn = Q.Dtime " +
                        "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
                        "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%' AND Mill_ID = @M";
                    */
                    SDS1.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve , Q.Visible From A_Product_Quality as Q " +
                        "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
                        "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%' AND Mill_ID = @M ORDER BY DateTime ASC";

                    SDS1.SelectParameters.Add("F", F);
                    SDS1.SelectParameters.Add("D1", tb_SDATE.Text);
                    SDS1.SelectParameters.Add("D2", tb_EDATE.Text);
                    SDS1.SelectParameters.Add("M", gm);
                }
                //判斷當前日期 停用/啟動 下一個月的功能
                ff.Enabled(tb_SDATE.Text, imgb_n);
                //手機超連結和名稱
                string name = ff.f_name(F);

                hyl_factory.Text = name + " ＞";
                hyl_factory.NavigateUrl = "../phone/Factory_" + F + ".aspx";
            }
        }
        protected void DDL_factory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n,tb_SDATE);
            ff.check_date(tb_EDATE.Text, tb_EDATE);
            string D = tb_SDATE.Text.Substring(5,5).Replace("-","/");
            string E = tb_EDATE.Text.Substring(5, 5).Replace("-", "/");
            Response.Redirect("Check_data.aspx?F=" + DDL_factory.Text + "&M=0&D=" + D + "&E=" + E + "");
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n,tb_SDATE);
            ff.check_date(tb_EDATE.Text, tb_EDATE);
            string D = tb_SDATE.Text.Substring(5, 5).Replace("-", "/");
            string E = tb_EDATE.Text.Substring(5, 5).Replace("-", "/");
            string M = Request.QueryString["M"];
            Response.Redirect("Check_data.aspx?F=" + DDL_factory.Text + "&M=" + M + "&D=" + D + "&E=" + E + "");
        }
        //結束時間
        protected void tb_EDATE_TextChanged(object sender, EventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n, tb_SDATE);
            ff.check_date(tb_EDATE.Text, tb_EDATE);
            string D = tb_SDATE.Text.Substring(5, 5).Replace("-", "/");
            string E = tb_EDATE.Text.Substring(5, 5).Replace("-", "/");
            string M = Request.QueryString["M"];
            Response.Redirect("Check_data.aspx?F=" + DDL_factory.Text + "&M=" + M + "&D=" + D + "&E=" + E + "");
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n,tb_SDATE);
            ff.check_date(tb_EDATE.Text, tb_EDATE);
            string D = Convert.ToDateTime(tb_SDATE.Text).AddDays(-1).ToString("MM/dd");
            string E = Convert.ToDateTime(tb_EDATE.Text).AddDays(-1).ToString("MM/dd");
            string M = Request.QueryString["M"];
            Response.Redirect("Check_data.aspx?F=" + DDL_factory.Text + "&M=" + M + "&D=" + D + "&E=" + E + "");
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string n = tb_SDATE.Text;
            ff.check_date(n,tb_SDATE);
            ff.check_date(tb_EDATE.Text, tb_EDATE);
            string D = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("MM/dd");
            string E = Convert.ToDateTime(tb_EDATE.Text).AddDays(1).ToString("MM/dd");
            string M = Request.QueryString["M"];
            Response.Redirect("Check_data.aspx?F=" + DDL_factory.Text + "&M=" + M + "&D=" + D + "&E=" + E + "");

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            string F = Request.QueryString["F"];
            Response.Redirect("add_data.aspx?F="+F+"");
        }

        protected void GV1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string M = Request.QueryString["M"];
            string F = Request.QueryString["F"];
            string time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
            string gm = ff.get_m(M, F);
            if (M == "0")
            {
                SDS1.SelectParameters.Clear();
                SDS1.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve , Q.Visible. From A_Product_Quality as Q " +
                    "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
                    "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%'";
                SDS1.SelectParameters.Add("F", F);
                SDS1.SelectParameters.Add("D1", tb_SDATE.Text);
                SDS1.SelectParameters.Add("D2", time_s);
            }
            else
            {
                SDS1.SelectParameters.Clear();
                SDS1.SelectCommand = "SELECT Q.DateTime, Q.Mill_ID, P.ProductName, Q.Moisture, Q.Specific_Surface, Q.Residual_On_Sieve , Q.Visible From A_Product_Quality as Q " +
                    "LEFT JOIN A_Product AS P ON Q.ProductID = P.ProductID " +
                    "WHERE DTime >= @D1 AND DTime < @D2 AND FactoryID LIKE @F+'%' AND Mill_ID = @M";

                SDS1.SelectParameters.Add("F", F);
                SDS1.SelectParameters.Add("D1", tb_SDATE.Text);
                SDS1.SelectParameters.Add("D2", time_s);
                SDS1.SelectParameters.Add("M", gm);
            }
        }

    }
}