using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using factory.lib;
using System.Data;
using System.Data.SqlClient;

using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml;


namespace factory.AirComp
{
    public partial class AirCompressorH : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        //新寫法趨勢圖
        
        public string test()
        {
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];

            if (F == "QX-T1HIST" && M == "2")
            {
                M = "#2#3#4";
            }
            else if (F == "ZB-T1HIST" && M == "1")
            {
                M = "#1#2#3";
            }
            else if (F == "ZB-T1HIST" && M == "4")
            {
                M = "#4#5";
            }
            else
            {
                M = "#" + M;
            }


            string factory = ff.factory_name(F);
            factory += "  ";
            string time_s = tb_SDATE.Text + " 09:00";
            string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd 08:00");

            SQLDB db = new SQLDB();
            //新增parameter
            List<SqlParameter> p_list = new List<SqlParameter>();
            //判斷是否有資料

            //給下面CHECKBOX用的日期
            List<string> c_list = new List<string>();
            //比功率
            List<List<string>> par_list = new List<List<string>>();
            string sql = "SELECT DTime,Specific_Power FROM G_Air_Comp_H where FactoryID = '" + F  + "'AND MID = '" + M + "' AND DTime >= @time_s AND DTime <= @time_e";
            p_list.Clear();
            p_list.Add(new SqlParameter("@time_s", time_s));
            p_list.Add(new SqlParameter("@time_e", time_e));
            DataTable dx = db.GetDataTable(sql, p_list, CommandType.Text);
            string d = "";
            string dd = "";
            //平均比功率
            sql = "SELECT (SUM(ISNULL(Power_C_01,0)) + SUM(ISNULL(Power_C_02,0)) + SUM(ISNULL(Power_C_03,0)) + SUM(ISNULL(Power_C_04,0)) +" +
                    "SUM(ISNULL(Power_C_05,0))) / SUM(ISNULL(Air_Consumption,0))AS avgSP,FactoryID " +
                    "FROM G_Air_Comp_H WHERE Specific_Power> 0 AND DTime >= @time_s AND DTime <= @time_e " +
                    "GROUP BY FactoryID,MID HAVING FactoryID = '"+ F +"' AND MID = '" + M +"'";
            p_list.Clear();
            p_list.Add(new SqlParameter("@time_s", time_s));
            p_list.Add(new SqlParameter("@time_e", time_e));
            DataTable dc = db.GetDataTable(sql, p_list, CommandType.Text);
            decimal v;
            if (dc.Rows.Count == 0)
            {
                v = 0;
            }
            else
            {
                v = Convert.ToDecimal(dc.Rows[0][0].ToString());
            }
            v = Math.Round(v, 2);
            for (int j = 0; j < dx.Rows.Count; j++)
            {
                decimal z = Convert.ToDecimal(dx.Rows[j][1].ToString());
                string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd HH:mm");
                if (z > 0)
                {
                    //比功率大於0的時候才加
                    d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                }
                c_list.Add(datatime);
            }
            //平均比功率
            for (int i = 0; i < 24; i++)
            {
                if (GV1.Rows.Count > 0)
                {
                    string datatime = Convert.ToDateTime(dx.Rows[0][0].ToString()).ToString("yyyy-MM-dd 09:00");
                    datatime = Convert.ToDateTime(datatime).AddHours(i).ToString("yyyy-MM-dd HH:mm");
                    dd += "{x:" + "'" + datatime + "'" + ",y:" + v + "\"},";
                }
            }
            par_list.Add(new List<string>() { d });
            par_list.Add(new List<string>() { dd });
            par_list.Add(new List<string>() { "{ unit:'hour'}" });
            par_list.Add(new List<string>() { "{ displayFormats:'HH:mm'}" });
            par_list.Add(new List<string>() { "{ b:'比功率'}" });
            par_list.Add(new List<string>() { "{ avg_b:'平均比功率'}" });
            string tt = tb_SDATE.Text.Substring(5,5).Replace("-","/");
            par_list.Add(new List<string>() { "{ title:'" + factory + tt + "比功率'}" });

            //新增checkbox的名稱和值還有title名稱
            List<string> ch_name = new List<string>();
            List<string> lb_name = new List<string>();
            List<string> t = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    string name1 = "ch_p";
                    ch_name.Add(name1);
                    string name2 = "lb_p";
                    lb_name.Add(name2);
                    name1 = "ch_p" + i.ToString();
                    ch_name.Add(name1);
                    name2 = "lb_p" + i.ToString();
                    lb_name.Add(name2);
                }
                else
                {
                    string name1 = "ch_p" + i.ToString();
                    ch_name.Add(name1);
                    string name2 = "lb_p" + i.ToString();
                    lb_name.Add(name2);
                    name1 = "ch_w" + i.ToString();
                    ch_name.Add(name1);
                    name2 = "lb_w" + i.ToString();
                    lb_name.Add(name2);
                }
            }
            //新增TITLE名稱
            t.Add(GV1.Columns[6].HeaderText.Replace("<br>", ""));
            t.Add(GV1.Columns[7].HeaderText.Replace("<br>", ""));
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 17; j += 4)
                {
                    t.Add(GV1.Columns[8 + j].HeaderText.Replace("<br>", ""));
                    t.Add(GV1.Columns[11 + j].HeaderText.Replace("<br>", ""));
                }
            }
            if (GV1.Rows.Count > 0)
            {
                for (int j = 0; j < 12; j++)
                {
                    d = "";
                    CheckBox ch_p1 = (CheckBox)GV1.HeaderRow.FindControl(ch_name[j]);
                    if (ch_p1.Checked == true)
                    {
                        for (int i = 0; i < GV1.Rows.Count; i++)
                        {
                            Label lb_p1 = (Label)GV1.Rows[i].FindControl(lb_name[j]);
                            if (lb_p1.Text == "" || lb_p1.Text == null)
                            {
                                lb_p1.Text = "0";
                            }
                            string datatime = c_list[i].ToString();
                            d += "{x:" + "'" + datatime + "'" + ",y:" + lb_p1.Text + "\"},";

                        }
                    }
                    par_list.Add(new List<string>() { d });
                    par_list.Add(new List<string>() { "{ name:'" + t[j] + "'}" });
                }
            }
   
            //轉換為JSON
            System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
            string datas = o.Serialize(par_list);
            //修改格式
            datas = datas.Replace("\\", "");
            datas = datas.Replace("\"", "");
            datas = datas.Replace("u0027", "'");
            return datas;

        }
        
        //計算工作時間
        public void worktime()
        {
            for (int i = 0; i < GV1.Rows.Count; i++)
            {
                if (GV1.Columns[11].Visible == true)
                {
                    Label lb_w1 = (Label)GV1.Rows[i].FindControl("lb_w1");
                    lb_w1.Text = (Convert.ToInt32(lb_w1.Text) / 60).ToString();
                }
                if (GV1.Columns[15].Visible == true)
                {
                    Label lb_w2 = (Label)GV1.Rows[i].FindControl("lb_w2");
                    lb_w2.Text = (Convert.ToInt32(lb_w2.Text) / 60).ToString();
                }
                if (GV1.Columns[19].Visible == true)
                {
                    Label lb_w3 = (Label)GV1.Rows[i].FindControl("lb_w3");
                    lb_w3.Text = (Convert.ToInt32(lb_w3.Text) / 60).ToString();
                }
                if (GV1.Columns[23].Visible == true)
                {
                    Label lb_w4 = (Label)GV1.Rows[i].FindControl("lb_w4");
                    lb_w4.Text = (Convert.ToInt32(lb_w4.Text) / 60).ToString();
                }
                if (GV1.Columns[27].Visible == true)
                {
                    Label lb_w5 = (Label)GV1.Rows[i].FindControl("lb_w5");
                    lb_w5.Text = (Convert.ToInt32(lb_w5.Text) / 60).ToString();
                }
            }
        }




        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                // 參考這篇http://infofabwhat.blogspot.com/2013/06/c-textbox-readonly-postback.html
                tb_SDATE.Attributes.Add("readonly", "readonly");

                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                //取得傳過來的資料日期
                string D = Request.QueryString["D"];
                
                //判斷時間是否為9點前還是9點後
                DateTime start = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                DateTime end = Convert.ToDateTime("09:00");
                TimeSpan ts = start - end;
                int x = Convert.ToInt32(ts.TotalSeconds);
                string d;
                if (x > 0)
                {
                    d = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    d = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                }

                //預設為觀音廠 網址列參數禁止亂打
                string name = ff.factory_name(F);
                if (name == "")
                {
                    Response.Redirect("AirCompressorH.aspx?F=KY-T1HIST&M=0&D=" + d + "");
                }
                else
                {
                    if (name == "龍德廠" && (M != "0" && M != "1" && M != "2"))
                    {
                        Response.Redirect("AirCompressorH.aspx?F=LD-T1HIST&M=0&D=" + d + "");
                    }
                    else if (name == "全興廠" && (M != "0" && M != "1" && M != "2"))
                    {
                        Response.Redirect("AirCompressorH.aspx?F=QX-T1HIST&M=0&D=" + d + "");
                    }
                    else if (name == "觀音廠" && (M != "0" && M != "1" && M != "2"))
                    {
                        Response.Redirect("AirCompressorH.aspx?F=KY-T1HIST&M=0&D=" + d + "");
                    }
                    else if (name == "彰濱廠" && (M != "0" && M != "1" && M != "4"))
                    {
                        Response.Redirect("AirCompressorH.aspx?F=" + F + "&M=1&D=" + d + "");
                    }
                    else if (name != "觀音廠" && name != "龍德廠" && name != "全興廠" && name != "彰濱廠" && M != "0")
                    {
                        Response.Redirect("AirCompressorH.aspx?F=" + F + "&M=0&D=" + d + "");
                    }
                }
                //判斷資料日期
                if (D == "")
                {
                    Response.Redirect("AirCompressorH.aspx?F=" + F + "&M=" + M + "&D=" + d + "");
                }
                else
                {
                    DateTime t;
                    bool flag = DateTime.TryParse(D, out t);
                    if (flag == false || D.Length !=10)
                    {
                        Response.Redirect("AirCompressorH.aspx?F=" + F + "&M=" + M + "&D=" + d + "");
                    }
                }
                //給日期
                //string dd = D.Replace("/", "-");
                string time_s = D;
                tb_SDATE.Text = time_s;

                //超連結
                string hyl = tb_SDATE.Text.Substring(0, 7).Replace("-", "/");
                hyl_mgt.NavigateUrl = "/AirComp/AirCompressor.aspx?F=" + F + "&M=" + M + "&D=" + hyl + "";


                if (M != "0")
                {
                    name += "#" + M;
                }
                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";

                lb_factory.Text = name;

                string m;
                if (F == "QX-T1HIST" && M == "2")
                {
                    m = "#2#3#4";
                }
                else if (F == "ZB-T1HIST" && M == "1")
                {
                    m = "#1#2#3";
                }
                else if (F == "ZB-T1HIST" && M == "4")
                {
                    m = "#4#5";
                }
                else
                {
                    m = "#" + M;
                }

                
                if (Session["d"] != null)
                {
                    d = Session["d"].ToString();
                    Session["d"] = null;
                    Response.Redirect("AirCompressorH.aspx?F="+F+"&M="+M+"&D="+d+"");
                    //tb_SDATE.Text = Session["d"].ToString();
                    //time_s = tb_SDATE.Text;
                }

                time_s += " 09:00";
                string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd 08:00");
                //隱藏不必要的欄位
                SQLDB db = new SQLDB();
                string sql = "SELECT * FROM G_Air_Comp_Mapping WHERE FactoryID = '" + F + "'AND MID = '" + m + "'";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                foreach (DataRow row in dt.Rows)
                {
                    /*
                    if (row[2].ToString() == "")
                    {
                        GV1.Columns[5].Visible = false;
                    }
                    if (row[3].ToString() == "")
                    {
                        GV1.Columns[6].Visible = false;

                    }
                    */
                    if (row[4].ToString() == "")
                    {
                        GV1.Columns[8].Visible = false;
                        GV1.Columns[9].Visible = false;
                        GV1.Columns[11].Visible = false;
                    }
                    if (row[5].ToString() == "")
                    {
                        
                        GV1.Columns[12].Visible = false;
                        GV1.Columns[13].Visible = false;
                        GV1.Columns[15].Visible = false;
                        
                    }
                    if (row[6].ToString() == "")
                    {
                        
                        GV1.Columns[16].Visible = false;
                        GV1.Columns[17].Visible = false;
                        GV1.Columns[19].Visible = false;
                        
                    }
                    if (row[7].ToString() == "")
                    {
                        GV1.Columns[20].Visible = false;
                        GV1.Columns[21].Visible = false;
                        GV1.Columns[23].Visible = false;    
                    }
                    if (row[8].ToString() == "")
                    {
                        
                        GV1.Columns[24].Visible = false;
                        GV1.Columns[25].Visible = false;
                        GV1.Columns[27].Visible = false;
                        
                    }
                }
                
                for (int i = 0; i < 17; i += 4)
                {
                    GV1.Columns[i + 10].Visible = false;
                }

                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp_H WHERE FactoryID ='" + F + "'AND MID = '" + m + "'  AND DTime >= '" + time_s + "' AND DTime <= '"+time_e+"'";

                if (GV1.Rows.Count > 0)
                {
                    GV1.DataBind();
                    //計算工作時間
                    worktime();
                    
                    //某些欄位需要顯示哪些機器名稱
                    List<string> ll = new List<string>();
                    for (int i = 1; i < 11; i++)
                    {
                        string n = "lb_r" + i.ToString();
                        ll.Add(n);
                    }

                    for (int j = 0; j < 10; j++)
                    {
                        Label txt = (Label)GV1.HeaderRow.FindControl(ll[j]);
                        txt.ToolTip = dt.Rows[0][4 + j].ToString();
                    }
                    //固定表頭
                    ff.title(GV1);
                }
                //判斷當前日期 停用/啟動 下一個月的功能
                start = Convert.ToDateTime(tb_SDATE.Text);
                end = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                ts = start - end;
                x = Convert.ToInt32(ts.Days);
                if (x >= 0)
                {
                    imgb_n.Enabled = false;
                }
                else
                {
                    imgb_n.Enabled = true;
                }

                
            }

            //傳值給下一個網頁
            Session["d"] = tb_SDATE.Text;
        }


        protected void imgb_excel_Click(object sender, ImageClickEventArgs e)
        {
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            if (F == "QX-T1HIST" && M == "2")
            {
                M = "#2#3#4";
            }
            else
            {
                M = "#" + M;
            }
            //平均比功率
            string sql = "SELECT (SUM(ISNULL(Power_C_01,0)) + SUM(ISNULL(Power_C_02,0)) + SUM(ISNULL(Power_C_03,0)) + SUM(ISNULL(Power_C_04,0)) +" +
                "SUM(ISNULL(Power_C_05,0))) / SUM(ISNULL(Air_Consumption,0))AS avgSP,FactoryID " +
                "FROM G_Air_Comp_H WHERE Specific_Power> 0 AND DTime >= @time_s AND DTime <= @time_e " +
                "GROUP BY FactoryID,MID HAVING FactoryID = '"+F+"' AND MID = '"+M+"'";
            string time_s = tb_SDATE.Text + " 09:00";
            string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd 08:00");
            string y = tb_SDATE.Text.Substring(0, 4);
            string m = tb_SDATE.Text.Substring(5, 2);
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Add(new SqlParameter("@time_s", time_s));
            p_list.Add(new SqlParameter("@time_e", time_e));
            SQLDB db = new SQLDB();
            DataTable dx = db.GetDataTable(sql, p_list, CommandType.Text);
            decimal v = 0;
            if (dx.Rows.Count > 0)
            {
                v = Convert.ToDecimal(dx.Rows[0][0].ToString());
                v = Math.Round(v, 2);
            }
            // gv label名稱和gv表頭名稱
            List<string> lb_name = new List<string>();//下面轉EXCEL檔案用
            List<string> ch_name = new List<string>();
            List<string> t = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    string name1 = "ch_p";
                    ch_name.Add(name1);
                    string name2 = "lb_p";
                    lb_name.Add(name2);
                    name1 = "ch_p" + i.ToString();
                    ch_name.Add(name1);
                    name2 = "lb_p" + i.ToString();
                    lb_name.Add(name2);
                }
                else
                {
                    string name1 = "ch_p" + i.ToString();
                    ch_name.Add(name1);
                    string name2 = "lb_p" + i.ToString();
                    lb_name.Add(name2);
                    name1 = "ch_w" + i.ToString();
                    ch_name.Add(name1);
                    name2 = "lb_w" + i.ToString();
                    lb_name.Add(name2);
                }
            }
            //這個是用來新增DataTable的資料
            List<string> lb_name2 = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    string name2 = "lb_p";
                    lb_name2.Add(name2);
                    name2 = "lb_p" + i.ToString();
                    lb_name2.Add(name2);
                }
                else
                {
                    string name2 = "lb_p" + i.ToString();
                    lb_name2.Add(name2);
                    name2 = "lb_kwh" + i.ToString();
                    lb_name2.Add(name2);
                    name2 = "lb_w" + i.ToString();
                    lb_name2.Add(name2);
                }
            }

            t.Add(GV1.Columns[6].HeaderText.Replace("<br>", ""));
            t.Add(GV1.Columns[7].HeaderText.Replace("<br>", ""));
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 17; j += 4)
                {
                    t.Add(GV1.Columns[8 + j].HeaderText.Replace("<br>", ""));
                    t.Add(GV1.Columns[9 + j].HeaderText.Replace("<br>", ""));
                    t.Add(GV1.Columns[11 + j].HeaderText.Replace("<br>", ""));
                }
            }

            //有資料的時候才匯出
            if (GV1.Rows.Count > 0)
            {
                //將gridview轉換成datatable
                //欄
                DataTable dt = new DataTable();
                DataColumn dc;
                DataRow dr;
                int cc = 0;

                for (int i = 0; i < GV1.Columns.Count; i++)
                {
                    if (GV1.Columns[i].Visible == true)
                    {
                        string n = GV1.Columns[i].HeaderText;
                        if (n == null)
                        {
                            n = t[cc];
                            cc += 1;
                        }
                        n = n.Replace("<br>", "");
                        dc = new DataColumn();
                        dc.ColumnName = n;
                        dt.Columns.Add(dc);
                    }
                }
                string s;
                cc = 0;
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    int x = 0;
                    dr = dt.NewRow();
                    for (int j = 0; j < GV1.Columns.Count; j++)
                    {
                        if (GV1.Columns[j].Visible == true)
                        {
                            s = GV1.Rows[i].Cells[j].Text;
                            if (j == 1)
                            {
                                HyperLink hyper = (HyperLink)GV1.Rows[i].FindControl("hyl_datatime");
                                s = hyper.Text;
                            }
                            if (s == "" && j != 1)
                            {
                                Label lb = (Label)GV1.Rows[i].FindControl(lb_name2[cc]);
                                s = lb.Text;
                                cc += 1;
                            }
                            dr[x] = s;
                            x += 1;
                        }
                    }
                    cc = 0;
                    dt.Rows.Add(dr);
                }

                string name = "空壓機比功率_" + lb_factory.Text +"_"+ tb_SDATE.Text;
                
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add(name);
                int c = dt.Columns.Count;
                //欄位名稱
                for (int i = 0; i < c; i++)
                {
                    ws.Cells[21, i + 1].Value = dt.Columns[i].ToString();
                    ws.Cells[21, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[21, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                //自動欄寬
                //ws.Cells[ws.Dimension.Address].AutoFitColumns();
                ws.Cells[200, 2].Value = "平均比功率";

                //資料
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < c; i++)
                    {
                        ws.Cells[j + 22, i + 1].Value = dt.Rows[j][i].ToString();

                        if (i == 0)
                        {
                            ws.Cells[j + 22, i + 1].Value = y + "/" + dt.Rows[j][i].ToString();
                            ws.Cells[j + 22, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            ws.Cells[j + 201, 1].Value = dt.Rows[j][i].ToString().Substring(6,5);
                            ws.Cells[j + 201, 2].Value = v;
                        }
                        else
                        {
                            string w = dt.Rows[j][i].ToString();
                            if (w == "" || w == null || w== "&nbsp;")
                            {
                                w = "0";
                            }
                            ws.Cells[j + 22, i + 1].Value = Convert.ToDouble(w);
                        }
                    }
                }

                //chart
                ExcelChartSerie serie = null;
                ExcelChart chart = ws.Drawings.AddChart("chart", eChartType.LineMarkers);

                chart.SetPosition(0, 0);
                chart.Legend.Add();
                chart.Legend.Position = eLegendPosition.Bottom;
                chart.Title.Text = name;
                chart.ShowHiddenData = true;
                chart.SetSize(1300, 400);//设置图表大小

                //line
                var line = chart.PlotArea.ChartTypes.Add(eChartType.LineMarkers);
                int ct = 21 + GV1.Rows.Count;
                string data = "B22:B" + ct.ToString();

                serie = line.Series.Add(ws.Cells[data], ws.Cells["A201:A300"]);
                serie.HeaderAddress = ws.Cells[21, 2];//设置每条线的名称


                //bar
                var bar = chart.PlotArea.ChartTypes.Add(eChartType.ColumnClustered);
                //新增位置
                List<string> p = new List<string>();
                p.Add("C22:C" + ct.ToString());
                p.Add("D22:D" + ct.ToString());
                p.Add("E22:E" + ct.ToString());
                p.Add("G22:G" + ct.ToString());
                p.Add("H22:H" + ct.ToString());
                p.Add("J22:J" + ct.ToString());
                p.Add("K22:K" + ct.ToString());
                p.Add("M22:M" + ct.ToString());
                p.Add("N22:N" + ct.ToString());
                p.Add("P22:P" + ct.ToString());
                p.Add("Q22:Q" + ct.ToString());
                p.Add("S22:S" + ct.ToString());

                ExcelChartSerie serie0 = null;
                for (int i = 0; i < 11; i++)
                {
                    CheckBox ch_p1 = (CheckBox)GV1.HeaderRow.FindControl(ch_name[i]);
                    if (ch_p1.Checked == true)
                    {
                        serie0 = bar.Series.Add(ws.Cells[p[i]], ws.Cells["A201:A300"]);
                        serie0.Header = t[i];
                        //第二條Y軸線
                        bar.UseSecondaryAxis = true;
                    }
                }



                //日期和平均比功率 在200層
                ct = 200 + GV1.Rows.Count;
                data = "B201:B" + ct.ToString();
                serie = line.Series.Add(ws.Cells[data], ws.Cells["A201:A300"]);
                serie.HeaderAddress = ws.Cells[200, 2];//设置每条线的名称 


                //== 輸出Excel 2007檔案
                MemoryStream MS = new MemoryStream(); //需要System.IO命名空間
                excel.SaveAs(MS);
                Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".xlsx");
                //增加HTTP表頭讓EDGE可以用
                Response.AppendHeader("X-UA-Compatible", "IE=edge,chrome=1");
                Response.BinaryWrite(MS.ToArray());
                MS.Close();
                MS.Dispose();
                Response.Flush(); //== 不寫這兩段程式，輸出EXCEL檔並開啟後，會出現檔案內容混損，需要修復的字母
                Response.End();
            }
            
        }



        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            
            string n = Request.Form[tb_SDATE.UniqueID];
            ff.check_date(n,tb_SDATE);

            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string s = Convert.ToDateTime(tb_SDATE.Text).ToString("yyyy-MM-dd");
            Session["d"] = Request.Form[tb_SDATE.UniqueID];
            Response.Redirect("AirCompressorH.aspx?F="+F+"&M="+M+"&D="+s+"");
            /*
            if (F == "QX-T1HIST" && M == "2")
            {
                M = "#2#3#4";
            }
            else
            {
                M = "#" + M;
            }
            string time_s = tb_SDATE.Text + " 09:00";
            string time_e = Convert.ToDateTime(time_s).AddDays(1).ToString("yyyy-MM-dd 08:00");
            //參數化
            if (SDS1.SelectParameters["time_s"] == null)
            {
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp_H WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DTime >= @time_s AND DTime <= @time_e";
                SDS1.SelectParameters.Add("time_s", time_s);
                SDS1.SelectParameters.Add("time_e", time_e);
            }
            else
            {
                SDS1.SelectParameters["time_s"].DefaultValue = time_s;
                SDS1.SelectParameters["time_e"].DefaultValue = time_e;
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp_H WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DTime >= @time_s AND DTime <= @time_e";
            }
            GV1.DataBind();
            //計算工作時間
            worktime();
            //固定表頭
            title();
            */
        }

        protected void ch_p0_CheckedChanged(object sender, EventArgs e)
        {
            //固定表頭
            ff.title(GV1);
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            string time_s = tb_SDATE.Text;
            ff.check_date(time_s,tb_SDATE);
            
            time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(-1).ToString("yyyy-MM-dd");
            Session["d"] = time_s;
            string s = Convert.ToDateTime(time_s).ToString("yyyy-MM-dd");
            Response.Redirect("AirCompressorH.aspx?F="+F+"&M="+M+"&D="+ s + "");
            
            /*
            tb_SDATE.Text = time_s;
            time_s += " 09:00";
            string time_e = Convert.ToDateTime(time_s).ToString("yyyy-MM-dd 08:00");
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            if (F == "QX-T1HIST" && M == "2")
            {
                M = "#2#3#4";
            }
            else
            {
                M = "#" + M;
            }
            
            //參數化
            if (SDS1.SelectParameters["time_s"] == null)
            {
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp_H WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
                SDS1.SelectParameters.Add("time_s", time_s);
                SDS1.SelectParameters.Add("time_e", time_e);
            }
            else
            {
                SDS1.SelectParameters["time_s"].DefaultValue = time_s;
                SDS1.SelectParameters["time_e"].DefaultValue = time_e;
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp_H WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
            }
            
            GV1.DataBind();
            //計算工作時間和固定表頭
            worktime();
            title();
            */
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            
            string time_s = tb_SDATE.Text;
            ff.check_date(time_s,tb_SDATE);

            time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
            //判斷時間是否大於當前日期
            DateTime start = Convert.ToDateTime(time_s);
            DateTime end = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            TimeSpan ts = start - end;
            int x = Convert.ToInt32(ts.Days);
            if (x >= 0)
            {
                time_s = DateTime.Now.ToString("yyyy-MM-dd");
            }
            
            Session["d"] = time_s;
            string s = Convert.ToDateTime(time_s).ToString("yyyy-MM-dd");
            Response.Redirect("AirCompressorH.aspx?F=" + F + "&M=" + M + "&D=" + s + "");
            

            /*
            string time_s = DateTime.Now.ToString(Request.Form[tb_SDATE.UniqueID]);
            DateTime t;
            bool flag = DateTime.TryParse(time_s, out t);
            if (flag == false || time_s.Length != 10)
            {
                time_s = DateTime.Now.ToString("yyyy-MM-dd");
            }
            time_s = Convert.ToDateTime(time_s).AddDays(+1).ToString("yyyy-MM-dd");
            tb_SDATE.Text = time_s;
            time_s += " 09:00";
            string time_e = Convert.ToDateTime(time_s).AddDays(+1).ToString("yyyy-MM-dd 08:00");
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            if (F == "QX-T1HIST" && M == "2")
            {
                M = "#2#3#4";
            }
            else
            {
                M = "#" + M;
            }

            //參數化
            if (SDS1.SelectParameters["time_s"] == null)
            {
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp_H WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
                SDS1.SelectParameters.Add("time_s", time_s);
                SDS1.SelectParameters.Add("time_e", time_e);
            }
            else
            {
                SDS1.SelectParameters["time_s"].DefaultValue = time_s;
                SDS1.SelectParameters["time_e"].DefaultValue = time_e;
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp_H WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
            }

            GV1.DataBind();
            //計算工作時間和固定表頭
            worktime();
            title();
            */
        }

    }
}