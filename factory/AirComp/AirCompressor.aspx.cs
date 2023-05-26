using System;
using System.Collections.Generic;
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





namespace factory
{
    public partial class AirCompressor_KH : System.Web.UI.Page
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
            string time_s = DateTime.Now.ToString(tb_SDATE.Text + "-01");
            //這個月最後一天
            string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            SQLDB db = new SQLDB();
            string sql = "SELECT DataDate FROM G_Air_Comp WHERE FactoryID like '"  + F +  "'AND MID = '"+ M +"' AND DataDate >= @time_s AND DataDate <= @time_e GROUP BY DataDate";
            //新增parameter
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Add(new SqlParameter("@time_s", time_s));
            p_list.Add(new SqlParameter("@time_e", time_e));
            DataTable dt = db.GetDataTable(sql, p_list, CommandType.Text);
            //判斷是否有資料
            
            if (dt.Rows.Count > 0)
            {
                //比功率
                List<List<string>> par_list = new List<List<string>>();
                sql = "SELECT DataDate,Specific_Power FROM G_Air_Comp where FactoryID like '" + F + "'AND MID = '"+ M +"' AND DataDate >= @time_s AND DataDate <= @time_e";
                p_list.Clear();
                p_list.Add(new SqlParameter("@time_s", time_s));
                p_list.Add(new SqlParameter("@time_e", time_e));
                DataTable dx = db.GetDataTable(sql, p_list, CommandType.Text);
                string d = "";
                for (int j = 0; j < dx.Rows.Count; j++)
                {
                    decimal z = Convert.ToDecimal(dx.Rows[j][1].ToString());
                    if (z > 0)
                    {
                        string datatime = Convert.ToDateTime(dx.Rows[j][0].ToString()).ToString("yyyy-MM-dd");
                        d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                    }
                }
                par_list.Add(new List<string>() { d });
                //平均比功率
                string y = time_s.Substring(0, 4);
                string m = time_s.Substring(5, 2);
                //轉換月 06月---6月
                int mv = Convert.ToInt32(m);
                //取得最後一天
                int x = Convert.ToInt32(time_e.Substring(8, 2));
                sql = "SELECT (SUM(ISNULL(Power_C_01,0)) + SUM(ISNULL(Power_C_02,0)) + SUM(ISNULL(Power_C_03,0)) + SUM(ISNULL(Power_C_04,0)) + SUM(ISNULL(Power_C_05,0))) / SUM(ISNULL(Air_Consumption,0))  AS avgSP," +
                    "FactoryID , DATEPART(YYYY,DataDate) as tYear, DATEPART(MM,DataDate) as tMonth" +
                    " FROM G_Air_Comp where Specific_Power>0" +
                    "GROUP BY FactoryID,MID , DATEPART(YYYY,DataDate), DATEPART(MM,DataDate)" +
                    "HAVING FactoryID = '" + F + "'AND MID = '"+ M +"' and DATEPART(YYYY,DataDate) = @y and DATEPART(MM,DataDate)=@m";
                p_list.Clear();
                p_list.Add(new SqlParameter("@y", y));
                p_list.Add(new SqlParameter("@m", m));

                dx = db.GetDataTable(sql, p_list, CommandType.Text);
                decimal v = 0;
                if (dx.Rows.Count > 0)
                {
                    v = Convert.ToDecimal(dx.Rows[0][0].ToString());
                }
                v = Math.Round(v, 2);
                string dd = "";
                for (int i = 0; i < x; i++)
                {
                    string datatime = Convert.ToDateTime(time_s).AddDays(i).ToString("yyyy-MM-dd");
                    dd += "{x:" + "'" + datatime + "'" + ",y:" + v + "\"},";
                }
                par_list.Add(new List<string>() { dd });
                par_list.Add(new List<string>() { "{ unit:'day'}" });
                par_list.Add(new List<string>() { "{ displayFormats:'MM-DD'}" });
                par_list.Add(new List<string>() { "{ min:'" + time_s + "'}" });
                par_list.Add(new List<string>() { "{ max:'" + time_e + "'}" });
                par_list.Add(new List<string>() { "{ b:'比功率'}" });
                par_list.Add(new List<string>() { "{ avg_b:'平均比功率'}" });
                par_list.Add(new List<string>() { "{ title:'"+ factory + mv + "月比功率'}" });
                    
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

                for (int j = 0; j < 12; j++)
                {
                    d = "";
                    CheckBox ch_p1 = (CheckBox)GV1.HeaderRow.FindControl(ch_name[j]);
                    HyperLink hyper = (HyperLink)GV1.Rows[0].FindControl("hyl_datatime");
                    string date_md = hyper.Text.Replace("/", "-");
                    string date_ymd = Convert.ToDateTime(date_md).ToString("yyyy-MM-dd");
                    if (ch_p1.Checked == true)
                    {
                        for (int i = 0; i < GV1.Rows.Count; i++)
                        {
                            Label lb_p1 = (Label)GV1.Rows[i].FindControl(lb_name[j]);
                            string datatime = Convert.ToDateTime(date_ymd).AddDays(i).ToString("yyyy-MM-dd");
                            d += "{x:" + "'" + datatime + "'" + ",y:" + lb_p1.Text + "\"},";
                        }
                    }
                    par_list.Add(new List<string>() { d });
                    par_list.Add(new List<string>() { "{ name:'" + t[j] + "'}" });
                }
                    
                //轉換為JSON
                System.Web.Script.Serialization.JavaScriptSerializer o = new System.Web.Script.Serialization.JavaScriptSerializer();
                string datas = o.Serialize(par_list);
                   
                //修改格式
                datas = datas.Replace("\\", "");
                datas = datas.Replace("\"", "");
                datas = datas.Replace("u0027", "'");

                //Session["datas"] = datas;
                return datas;
                
            }
            
            else
            {
                return ""; 
            }
            
        }

        

        //計算工作時間
        public void worktime()
        {
            for (int i = 0; i < GV1.Rows.Count; i++)
            {
                if (GV1.Columns[11].Visible == true)
                {
                    Label lb_w1 = (Label)GV1.Rows[i].FindControl("lb_w1");
                    lb_w1.Text = (Math.Round(Convert.ToDouble(lb_w1.Text) / 3600,1)).ToString();
                }
                if (GV1.Columns[15].Visible == true)
                {
                    Label lb_w2 = (Label)GV1.Rows[i].FindControl("lb_w2");
                    lb_w2.Text = (Math.Round(Convert.ToDouble(lb_w2.Text) / 3600,1)).ToString();
                }
                if (GV1.Columns[19].Visible == true)
                {
                    Label lb_w3 = (Label)GV1.Rows[i].FindControl("lb_w3");
                    lb_w3.Text = (Math.Round(Convert.ToDouble(lb_w3.Text) / 3600,1)).ToString();
                }
                if (GV1.Columns[23].Visible == true)
                {
                    Label lb_w4 = (Label)GV1.Rows[i].FindControl("lb_w4");
                    lb_w4.Text = (Math.Round(Convert.ToDouble(lb_w4.Text) / 3600,1)).ToString();
                }
                if (GV1.Columns[27].Visible == true)
                {
                    Label lb_w5 = (Label)GV1.Rows[i].FindControl("lb_w5");
                    lb_w5.Text = (Math.Round(Convert.ToDouble(lb_w5.Text) / 3600,1)).ToString();
                }
            }
        }
        
       

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //傳出日期到下一頁
            if (Session["data"] == null)
            {
                tb_SDATE.Text = Request.Form[tb_SDATE.UniqueID];
                Session["data"] = Request.Form[tb_SDATE.UniqueID];
            }
            else
            {
                tb_SDATE.Text = Request.Form[tb_SDATE.UniqueID];
                Session["d"] = Session["data"];
                Session["data"] = Request.Form[tb_SDATE.UniqueID];
            }
            */
            if (!IsPostBack)
            {
                // 參考這篇http://infofabwhat.blogspot.com/2013/06/c-textbox-readonly-postback.html
                tb_SDATE.Attributes.Add("readonly", "readonly");

                
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                string D = Request.QueryString["D"];
                
                //預設為觀音廠 網址列參數禁止亂打
                string name = ff.factory_name(F);
                
                if (name == "")
                {
                    Response.Redirect("AirCompressor.aspx?F=KY-T1HIST&M=0");
                }
                else
                {
                    if (name == "龍德廠" && (M != "0" && M != "1" && M != "2"))
                    {
                        Response.Redirect("AirCompressor.aspx?F=LD-T1HIST&M=0");
                    }
                    else if (name == "觀音廠" && (M != "0" && M != "1" && M != "2"))
                    {
                        Response.Redirect("AirCompressor.aspx?F=KY-T1HIST&M=0");
                    }
                    else if (name == "全興廠" && (M != "0" && M != "1" && M != "2"))
                    {
                        Response.Redirect("AirCompressor.aspx?F=QX-T1HIST&M=0");
                    }
                    else if (name == "彰濱廠" && (M != "0" && M != "1" && M != "4"))
                    {
                        Response.Redirect("AirCompressorH.aspx?F=" + F + "&M=0");
                    }
                    else if (name != "觀音廠" && name != "龍德廠" && name != "全興廠" && name != "彰濱廠" && M != "0")
                    {
                        Response.Redirect("AirCompressorH.aspx?F=" + F + "&M=0");
                    }
                }
                
                if (M != "0")
                {
                    name += "#" + M;
                }

                //判斷D是否正確並重定向
                
                string now = DateTime.Now.ToString("yyyy/MM");
                if (D == null)
                {
                    Response.Redirect("AirCompressor.aspx?F=" + F + "&M=" + M + "&D=" + now + "");
                }
                else
                {
                    bool ym = Regex.IsMatch(D, "^\\d{4}/((0\\d)|(1[012]))$");
                    if (ym == false)
                    {
                        Response.Redirect("AirCompressor.aspx?F=" + F + "&M=" + M + "&D=" + now + "");
                    }
                }
                
                tb_SDATE.Text = D.Replace("/", "-");
                //取得前一天的時間在轉為當月的第一天
                //string time_s = DateTime.Now.ToString("yyyy-MM-dd");
                //time_s = Convert.ToDateTime(time_s).AddDays(-1).ToString("yyyy-MM-01");
                //tb_SDATE.Text = Convert.ToDateTime(time_s).ToString("yyyy-MM");
                string time_s = tb_SDATE.Text + "-01";
                string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";
                
                lb_factory.Text = name;

                //只有全興廠2資料不一樣
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

                //隱藏不必要的欄位
                SQLDB db = new SQLDB();
                string sql = "SELECT * FROM G_Air_Comp_Mapping WHERE FactoryID = '" + F + "'AND MID = '"+ M +"'";
                DataTable dt = db.GetDataTable(sql,CommandType.Text);
                foreach (DataRow row in dt.Rows)
                {
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
                    GV1.Columns[i+10].Visible = false;
                }
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp WHERE FactoryID ='" + F + "'AND MID = '" + M + "'  AND DataDate >= '" + time_s + "' AND DataDate <= '" + time_e + "'";

                
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
                ff.En_imgb_n(tb_SDATE.Text,imgb_n);
            }
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
            string sql = "SELECT (SUM(ISNULL(Power_C_01,0)) + SUM(ISNULL(Power_C_02,0)) + SUM(ISNULL(Power_C_03,0)) + SUM(ISNULL(Power_C_04,0)) + SUM(ISNULL(Power_C_05,0))) / SUM(ISNULL(Air_Consumption,0))  AS avgSP," +
                    "FactoryID , DATEPART(YYYY,DataDate) as tYear, DATEPART(MM,DataDate) as tMonth" +
                    " FROM G_Air_Comp where Specific_Power>0" +
                    "GROUP BY FactoryID,MID , DATEPART(YYYY,DataDate), DATEPART(MM,DataDate)" +
                    "HAVING FactoryID = '" + F + "'AND MID = '" + M + "' and DATEPART(YYYY,DataDate) = @y and DATEPART(MM,DataDate)=@m";
            string y = tb_SDATE.Text.Substring(0, 4);
            string m = tb_SDATE.Text.Substring(5, 2);
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Add(new SqlParameter("@y", y));
            p_list.Add(new SqlParameter("@m", m));
            SQLDB db = new SQLDB();
            DataTable dx = db.GetDataTable(sql, p_list,CommandType.Text);
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
                            if (s == "" && j!= 1)
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

                string name = "空壓機比功率_" + lb_factory.Text + "_" + tb_SDATE.Text;
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

                            ws.Cells[j + 201, 1].Value = dt.Rows[j][i].ToString();
                            ws.Cells[j + 201, 2].Value = v;
                        }
                        else
                        {
                            ws.Cells[j + 22, i + 1].Value = Convert.ToDouble(dt.Rows[j][i].ToString());
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
                Response.AddHeader("Content-Disposition", "attachment;filename="+name+".xlsx");
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
            ff.check_ym(n,tb_SDATE);
            n = tb_SDATE.Text;
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            Response.Redirect("AirCompressor.aspx?F=" + F + "&M=" + M + "&D=" + n.Replace("-","/") + "");
            /*
            if (F == "QX-T1HIST" && M == "2")
            {
                M = "#2#3#4";
            }
            else
            {
                M = "#" + M;
            }
            string time_s = DateTime.Now.ToString(tb_SDATE.Text + "-01");
            //time_s = Convert.ToDateTime(time_s).AddDays(-1).ToString("yyyy-MM-01");
            string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            //參數化
            if (SDS1.SelectParameters["time_s"] == null)
            {
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
                SDS1.SelectParameters.Add("time_s", time_s);
                SDS1.SelectParameters.Add("time_e", time_e);
            }
            else
            {
                SDS1.SelectParameters["time_s"].DefaultValue = time_s;
                SDS1.SelectParameters["time_e"].DefaultValue = time_e;
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
            }
            GV1.DataBind();
            //計算工作時間和固定表頭
            worktime();
            title();

            En_imgb_p();
            */
        }

        protected void ch_p0_CheckedChanged(object sender, EventArgs e)
        {
            //固定表頭
            ff.title(GV1);
        }

        protected void GV1_DataBound(object sender, EventArgs e)
        {
            if (GV1.Rows.Count > 0)
            {
                string F = Request.QueryString["F"];
                string M = Request.QueryString["M"];
                SQLDB db = new SQLDB();
                //修改資料日期超連結
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    HyperLink hyper = (HyperLink)GV1.Rows[i].FindControl("hyl_datatime");
                    string h = tb_SDATE.Text.Substring(0,5) + hyper.Text.Replace("/","-");
                    string sql = "SELECT DataDate FROM G_Air_Comp_H WHERE FactoryID = '" + F + "'AND MID = '"+ "#" + M + "' AND DataDate = '" + h + "'";
                    DataTable dt = db.GetDataTable(sql,CommandType.Text);
                    if (dt.Rows.Count > 0)
                    {
                        Session["d"] = null;
                        //Response.Write("AirCompressorH.aspx?F=" + F + "&M=" + M  +"&D=" + tb_SDATE.Text + "-" + hyper.Text.Substring(3, 2) + "");
                        hyper.NavigateUrl = "AirCompressorH.aspx?F=" + F + "&M=" + M + "&D=" + tb_SDATE.Text + "-" + hyper.Text.Substring(3, 2) + "";
                    }
                }
            }
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string n = Request.Form[tb_SDATE.UniqueID];
            ff.check_ym(n,tb_SDATE);
            n = tb_SDATE.Text;
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            n = Convert.ToDateTime(n).AddMonths(-1).ToString("yyyy/MM");
            Response.Redirect("AirCompressor.aspx?F=" + F + "&M=" + M + "&D=" + n.Replace("-", "/") + "");

            /*
            string time_s = DateTime.Now.ToString(tb_SDATE.Text + "-01");
            time_s = Convert.ToDateTime(time_s).AddMonths(-1).ToString("yyyy-MM-dd");
            tb_SDATE.Text = time_s.Substring(0, 7);
            string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
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
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
                SDS1.SelectParameters.Add("time_s", time_s);
                SDS1.SelectParameters.Add("time_e", time_e);
            }
            else
            {
                SDS1.SelectParameters["time_s"].DefaultValue = time_s;
                SDS1.SelectParameters["time_e"].DefaultValue = time_e;
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
            }
            GV1.DataBind();
            //計算工作時間和固定表頭
            worktime();
            title();

            En_imgb_p();
            */
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            string n = Request.Form[tb_SDATE.UniqueID];
            ff.check_ym(n,tb_SDATE);
            string F = Request.QueryString["F"];
            string M = Request.QueryString["M"];
            n = Convert.ToDateTime(tb_SDATE.Text).AddMonths(1).ToString("yyyy/MM");
            Response.Redirect("AirCompressor.aspx?F=" + F + "&M=" + M + "&D=" + n.Replace("-", "/") + "");

            /*
            //判斷時間是否大於當前日期
            string time_s = tb_SDATE.Text;
            time_s = Convert.ToDateTime(time_s).AddMonths(1).ToString("yyyy-MM-dd");
            tb_SDATE.Text = time_s.Substring(0, 7);
            string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
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
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
                SDS1.SelectParameters.Add("time_s", time_s);
                SDS1.SelectParameters.Add("time_e", time_e);
            }
            else
            {
                SDS1.SelectParameters["time_s"].DefaultValue = time_s;
                SDS1.SelectParameters["time_e"].DefaultValue = time_e;
                SDS1.SelectCommand = "SELECT * FROM G_Air_Comp WHERE FactoryID = '" + F + "'AND MID = '" + M + "' AND DataDate >= @time_s AND DataDate <= @time_e";
            }
            GV1.DataBind();
            //計算工作時間
            worktime();
            title();

            En_imgb_p();
            */
        }
    }
}