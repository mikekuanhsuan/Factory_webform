using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using factory.lib;

using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Xml;

namespace factory.power
{
    public partial class Power_D : System.Web.UI.Page
    {
        public f_class ff = new f_class();
        //圖
        public string chart()
        {
            string F = Request.QueryString["F"];
            string time_s = tb_SDATE.Text + "-01";
            string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            string factory = ff.factory_name(F) + "  ";
            int mv = Convert.ToInt32(tb_SDATE.Text.Substring(5, 2));
            List<List<string>> par_list = new List<List<string>>();

            par_list.Add(new List<string>() { "{ min:'" + time_s + "'}" });
            par_list.Add(new List<string>() { "{ max:'" + time_e + "'}" });
            par_list.Add(new List<string>() { "{ title:'" + factory + mv + "月用電量'}" });
            
            //新增checkbox的名稱和值還有title名稱
            List<string> ch_name = new List<string>();
            List<string> lb_name = new List<string>();
            List<string> t = new List<string>();
            
            for (int i = 0; i < 8; i++)
            {
                if (GV1.Columns[19 + i].Visible == true)
                {
                    string name1 = "ch_p" + (i + 1).ToString();
                    ch_name.Add(name1);
                    string name2 = "lb_p" + (i + 1).ToString();
                    lb_name.Add(name2);
                    t.Add(GV1.Columns[19 + i].HeaderText.Replace("<br>", ""));
                }
            }


            //有資料時才新增，以往是用SQL去撈資料，這邊改用GV來抓資料
            if (GV1.Rows.Count > 0)
            {

                //日期
                List<string> date = new List<string>();
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    HyperLink time = (HyperLink)GV1.Rows[i].FindControl("hyl_datatime");
                    string y = tb_SDATE.Text.Substring(0, 4).ToString();
                    string x = Convert.ToDateTime(time.Text).ToString(y + "-MM-dd");
                    date.Add(x);
                }
                string d = "";
                if (GV1.Columns[15].Visible == true)
                {
                    //用電量
                    for (int i = 0; i < GV1.Rows.Count; i++)
                    {
                        string datatime = date[i];
                        int z = Convert.ToInt32(GV1.Rows[i].Cells[15].Text.Replace(",", ""));
                        d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                    }
                    par_list.Add(new List<string>() { d });
                    par_list.Add(new List<string>() { "{ power:'用電量'}" });
                }
                else
                {
                    par_list.Add(new List<string>() { "" });
                    par_list.Add(new List<string>() { "" });
                }

                //其他
                for (int i = 0; i < t.Count; i++)
                {
                    d = "";
                    CheckBox ch_p1 = (CheckBox)GV1.HeaderRow.FindControl(ch_name[i]);
                    if (ch_p1.Checked == true)
                    {
                        for (int j = 0; j < GV1.Rows.Count; j++)
                        {
                            Label lb_p1 = (Label)GV1.Rows[j].FindControl(lb_name[i]);
                            //資料如果NULL或空給0
                            if (lb_p1.Text == "" || lb_p1.Text == null)
                            {
                                lb_p1.Text = "0";
                            }
                            string datatime = date[j];
                            int z = Convert.ToInt32(lb_p1.Text.Replace(",", ""));
                            d += "{x:" + "'" + datatime + "'" + ",y:" + z + "\"},";
                        }
                        par_list.Add(new List<string>() { d });
                        string n = t[i];
                        par_list.Add(new List<string>() { "{ p:'" + n + "'}" });
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
            else
            {
                return "";
            }
        }
        
        //改變日期
        public void change_data(string time_s)
        {
            string F = Request.QueryString["F"];
            string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            //參數化
            if (SDS1.SelectParameters["time_s"] == null)
            {
                SDS1.SelectCommand = "SELECT * FROM G_Power_D WHERE FactoryID ='" + F + "' AND DataDate >= @time_s AND DataDate <= @time_e";
                SDS1.SelectParameters.Add("time_s", time_s);
                SDS1.SelectParameters.Add("time_e", time_e);
            }
            else
            {
                SDS1.SelectParameters["time_s"].DefaultValue = time_s;
                SDS1.SelectParameters["time_e"].DefaultValue = time_e;
                SDS1.SelectCommand = "SELECT * FROM G_Power_D WHERE FactoryID ='" + F + "' AND DataDate >= @time_s AND DataDate <= @time_e";
            }
            GV1.DataBind();
            ff.title(GV1);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 參考這篇http://infofabwhat.blogspot.com/2013/06/c-textbox-readonly-postback.html
                tb_SDATE.Attributes.Add("readonly", "readonly");
                
                string F = Request.QueryString["F"];
                string D = Request.QueryString["D"];
                string name = ff.factory_name(F);

                //判斷網址列上的F
                if (name == "")
                {
                    Response.Redirect("Power_D.aspx?F=KY-T1HIST");
                }

                //判斷D是否正確並重定向
                string now = DateTime.Now.ToString("yyyy/MM");
                if (D == null)
                {
                    
                    Response.Redirect("Power_D.aspx?F=" + F + "&D=" + now + "");
                }
                else
                {
                    bool ym = Regex.IsMatch(D, "^\\d{4}/((0\\d)|(1[012]))$");
                    if (ym == false)
                    {
                        Response.Redirect("Power_D.aspx?F=" + F + "&D=" + now + "");
                    }
                }

                tb_SDATE.Text = D.Replace("/", "-");
                
                //手機label加超連結
                hyl_factory.Text = name + " ＞";
                string f = F.Substring(0, 2);
                hyl_factory.NavigateUrl = "../phone/Factory_" + f + ".aspx";

                lb_factory.Text = name;
                //取得當月第一天和最後一天
                string time_s = tb_SDATE.Text+"-01";
                string time_e = Convert.ToDateTime(time_s).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

                //隱藏不必要的欄位
                string sql = "SELECT * FROM G_Power_Mapping WHERE FactoryID = '"+ F +"'";
                SQLDB db = new SQLDB();
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][1].ToString() == "")
                    {
                        GV1.Columns[15].Visible = false;
                    }
                    int z = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        if (dt.Rows[0][9 + z].ToString() == "")
                        {
                            GV1.Columns[19 + i].Visible = false;
                        }
                        z += 2;
                    }
                }
                sql = "SELECT * FROM G_Power_D WHERE FactoryID ='" + F + "' AND DataDate >= '" + time_s + "' AND DataDate <= '" + time_e + "'";
                SDS1.SelectCommand = sql;

                if (GV1.Rows.Count > 0)
                {
                    GV1.DataBind();
                    //某些欄位需要顯示哪些機器名稱
                    List<string> ll = new List<string>();
                    for (int i = 1; i < 9; i++)
                    {
                        string n = "lb_n" + i.ToString();
                        ll.Add(n);
                    }
                    for (int j = 0; j < 8; j++)
                    {
                        Label txt = (Label)GV1.HeaderRow.FindControl(ll[j]);
                        txt.ToolTip = dt.Rows[0][9 + j*2].ToString();
                    }
                    //固定表頭
                    ff.title(GV1);
                }
                ff.En_imgb_n(tb_SDATE.Text,imgb_n);
            }
        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            //檢查日期格式
            string n = tb_SDATE.Text;
            ff.check_ym(n,tb_SDATE);

            //用重新導向的方式
            string F = Request.QueryString["F"];
            Response.Redirect("Power_D.aspx?F="+F+"&D="+n.Replace("-","/")+"");
            /*
            //更改資料
            string time_s = tb_SDATE.Text + "-01";
            change_data(time_s);
            En_imgb_p();
            */
        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            //檢查日期格式
            string n = tb_SDATE.Text;
            ff.check_ym(n,tb_SDATE);


            //更改資料
            string time_s = Convert.ToDateTime(tb_SDATE.Text).AddMonths(-1).ToString("yyyy-MM");
            //用重新導向的方式
            string F = Request.QueryString["F"];
            Response.Redirect("Power_D.aspx?F=" + F + "&D=" + time_s.Replace("-", "/") + "");
            /*
            tb_SDATE.Text = time_s;
            time_s += "-01";
            change_data(time_s);
            En_imgb_p();
            */
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {
            //檢查日期格式
            string n = tb_SDATE.Text;
            ff.check_ym(n, tb_SDATE);


            
            //判斷時間是否大於當前日期
            string time_s = tb_SDATE.Text;
            time_s = Convert.ToDateTime(time_s).AddMonths(1).ToString("yyyy-MM");
            //用重新導向的方式
            string F = Request.QueryString["F"];
            Response.Redirect("Power_D.aspx?F=" + F + "&D=" + time_s.Replace("-", "/") + "");
            /*
            tb_SDATE.Text = time_s;
            time_s += "-01";
            //更改資料
            change_data(time_s);
            En_imgb_p();
            */
        }

        protected void imgb_excel_Click(object sender, ImageClickEventArgs e)
        {
            //有資料的時候才執行
            if (GV1.Rows.Count > 0)
            {
                //新增checkbox的名稱和值還有title名稱
                List<string> ch_name = new List<string>();
                List<string> lb_name = new List<string>();
                List<string> t = new List<string>();

                for (int i = 0; i < 8; i++)
                {
                    if (GV1.Columns[19 + i].Visible == true)
                    {
                        string name1 = "ch_p" + (i + 1).ToString();
                        ch_name.Add(name1);
                        string name2 = "lb_p" + (i + 1).ToString();
                        lb_name.Add(name2);
                        t.Add(GV1.Columns[19 + i].HeaderText.Replace("<br>", ""));
                    }
                }

                //將gridview轉換成datatable
                //欄
                DataTable dt = new DataTable();
                DataColumn dc;
                DataRow dr;
                int c = 0;

                for (int i = 0; i < GV1.Columns.Count; i++)
                {
                    if (GV1.Columns[i].Visible == true)
                    {
                        string n = GV1.Columns[i].HeaderText;
                        if (n == null)
                        {
                            n = t[c];
                            c += 1;
                        }
                        n = n.Replace("<br>", "");
                        dc = new DataColumn();
                        dc.ColumnName = n;
                        dt.Columns.Add(dc);
                    }
                }
                c = 0;
                string s;
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    int x = 0;
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
                            if (s == null || s == "")
                            {

                                Label lb = (Label)GV1.Rows[i].FindControl(lb_name[c]);
                                s = lb.Text;
                                c += 1;
                            }
                            dr[x] = s;
                            x+=1;
                        }
                    }
                    c = 0;
                    dt.Rows.Add(dr);
                }
                string name = "用電量_" + lb_factory.Text + "_" + tb_SDATE.Text;
                
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add(name);

                //欄位名稱
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[21, i + 1].Value = dt.Columns[i].ToString();
                    ws.Cells[21, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[21, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                //自動欄寬
                //ws.Cells[ws.Dimension.Address].AutoFitColumns();
                string y = tb_SDATE.Text.Substring(0, 4);

                //資料
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            ws.Cells[j + 22, i + 1].Value = y + "/" + dt.Rows[j][i].ToString();
                            ws.Cells[j + 22, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            ws.Cells[j + 200, i + 1].Value = dt.Rows[j][i].ToString();
                        }
                        else
                        {
                            string w = dt.Rows[j][i].ToString();
                            if (w == "" || w == null || w == "&nbsp;")
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

                serie = line.Series.Add(ws.Cells[data], ws.Cells["A200:A300"]);
                serie.HeaderAddress = ws.Cells[21, 2];//设置每条线的名称
                
                //bar
                var bar = chart.PlotArea.ChartTypes.Add(eChartType.ColumnClustered);
                //新增位置
                List<string> p = new List<string>();
                p.Add("C22:C" + ct.ToString());
                p.Add("D22:D" + ct.ToString());
                p.Add("E22:E" + ct.ToString());
                p.Add("F22:F" + ct.ToString());
                p.Add("G22:G" + ct.ToString());
                p.Add("H22:H" + ct.ToString());
                p.Add("I22:I" + ct.ToString());
                p.Add("J22:J" + ct.ToString());

                ExcelChartSerie serie0 = null;
                for (int i = 0; i < ch_name.Count; i++)
                {
                    CheckBox ch_p1 = (CheckBox)GV1.HeaderRow.FindControl(ch_name[i]);
                    if (ch_p1.Checked == true)
                    {
                        serie0 = bar.Series.Add(ws.Cells[p[i]], ws.Cells["A200:A300"]);
                        serie0.Header = t[i];
                        //第二條Y軸線
                        bar.UseSecondaryAxis = true;
                    }
                }


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

        protected void ch_p0_CheckedChanged(object sender, EventArgs e)
        {
            ff.title(GV1);
        }

        protected void GV1_DataBound(object sender, EventArgs e)
        {
            if (GV1.Rows.Count > 0)
            {
                string F = Request.QueryString["F"];
                SQLDB db = new SQLDB();
                //修改資料日期超連結
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    HyperLink hyper = (HyperLink)GV1.Rows[i].FindControl("hyl_datatime");
                    Session["d"] = null;
                    //Response.Write("Power_H.aspx?F=" + F + "&D=" + tb_SDATE.Text + "-" + hyper.Text.Substring(3, 2) + "");
                    hyper.NavigateUrl = "Power_H.aspx?F=" + F + "&D=" + tb_SDATE.Text + "-" + hyper.Text.Substring(3,2) + "";
                }
            }
        }
    }
}