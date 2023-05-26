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

namespace factory.Tpc_Power
{
    public partial class Tpc_Power : System.Web.UI.Page
    {
        public f_class ff = new f_class();

        public void check_y(string D)
        {
            bool ym = Regex.IsMatch(D, "^\\d{4}");
            if (ym == false)
            {
                string time = DateTime.Now.ToString("yyyy");
                ddl_t.Text = time;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string T = Request.QueryString["T"];
                string F = Request.QueryString["F"];
                string D = Request.QueryString["D"];
                tb_SDATE.Attributes.Add("readonly", "readonly");

                SQLDB db = new SQLDB();
                string sql = "SELECT * FROM Factory ORDER BY aOrder ASC ";
                DataTable dt = db.GetDataTable(sql, CommandType.Text);
                for (int i = 0; i < dt.Rows.Count-1; i++)
                {
                    ddl_f.Items.Add(new ListItem(dt.Rows[i][1].ToString() + "廠", dt.Rows[i][0].ToString().Substring(0, 2)));
                    if (dt.Rows[i][0].ToString().Substring(0, 2) == F)
                    {
                        ddl_f.Items[i].Selected = true;
                    }
                }



                if (F != "KY" && F!="BL" && F != "QX" && F != "ZB" && F != "KH" && F != "LD" && F != "LZ" && F != "HL" && F != "XG")
                {
                    Response.Redirect("Tpc_Power.aspx?F=KY&T=D");
                }

                if (T != "Y" && T != "D" && T != "M")
                {
                    Response.Redirect("Tpc_Power.aspx?F="+F+"&T=D");
                }

                if (T == "D")
                {
                    string time_s = DateTime.Now.ToString("yyyy/MM");
                    if (D == "" || D == null)
                    {
                        Response.Redirect("Tpc_Power.aspx?F="+F+"&T="+T+"&D="+time_s+"");
                    }
                    ff.check_ym(D, tb_SDATE);
                    D = D.Replace("/", "-");
                    tb_SDATE.Text = D;
                    ff.En_imgb_n(D, imgb_n);
                    D += "-01";
                    string time_e = Convert.ToDateTime(D).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
                    SDS1.SelectCommand = "SELECT * FROM (SELECT F.FactoryName+'廠' as FactoryName, G.DTime, G.Power_KW, G.Electricity_period " +
                        "FROM G_TPC_D AS G LEFT JOIN Factory AS F ON G.FactoryID = F.FactoryID WHERE G.FactoryID LIKE @F+'%' AND DTime >=@time_s AND DTime <= @time_e ) t " +
                        "PIVOT ( MAX(Power_KW) FOR Electricity_period IN ([半尖峰時段], [尖峰時段], [週六半尖峰時段],[離峰時段])) p";
                    SDS1.SelectParameters.Add("F", ddl_f.SelectedValue);
                    SDS1.SelectParameters.Add("time_s", D);
                    SDS1.SelectParameters.Add("time_e", time_e);
                    GV1.DataBind();
                    ff.title(GV1);

                }
                else if (T == "M")
                {
                    string time_s = DateTime.Now.ToString("yyyy-MM-dd");
                    if (D == "" || D == null)
                    {
                        Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
                    }
                    DateTime t;
                    bool flag = DateTime.TryParse(D, out t);
                    if (flag == false || D.Length != 10)
                    {
                        Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
                    }
                    
                    time_s = D;
                    tb_SDATE.Text = time_s;
                    ff.Enabled(time_s, imgb_n);
                    string time_e = Convert.ToDateTime(time_s).AddDays(+1).ToString("yyyy-MM-dd");
                    ((BoundField)GV1.Columns[1]).DataFormatString = "{0:MM/dd HH:mm}";
                    SDS1.SelectCommand = "SELECT * FROM (SELECT F.FactoryName+'廠' as FactoryName, G.DTime, G.Power_KW, G.Electricity_period " +
                    "FROM G_TPC AS G LEFT JOIN Factory AS F ON G.FactoryID = F.FactoryID WHERE G.FactoryID LIKE @F+'%' AND DTime >=@time_s AND DTime < @time_e ) t " +
                    "PIVOT ( MAX(Power_KW) FOR Electricity_period IN ([半尖峰時段], [尖峰時段], [週六半尖峰時段],[離峰時段])) p";
                    SDS1.SelectParameters.Add("F", ddl_f.SelectedValue);
                    SDS1.SelectParameters.Add("time_s", time_s);
                    SDS1.SelectParameters.Add("time_e", time_e);
                    GV1.DataBind();
                    ff.title(GV1);
                    
                }
                else
                {

                    tb_SDATE.Visible = false;
                    int y = 2021;
                    string year = DateTime.Now.ToString("yyyy");
                    if (D == "" || D == null)
                    {
                        Response.Redirect("Tpc_Power.aspx?F="+F+"&T="+T+"&D="+year+"");
                    }
                    check_y(D);

                    //下拉選單
                    for (int i = 0; i < 10; i++)
                    {
                        string v = (i + y).ToString();
                        ddl_t.Items.Add(new ListItem(v, v));
                        if (v == D)
                        {
                            ddl_t.Items[i].Selected = true;
                        }
                    }
                    ddl_t.Visible = true;


                    string time_s = D + "-01-01";
                    string time_e = Convert.ToDateTime(time_s).AddYears(1).AddDays(-1).ToString("yyyy-MM-dd");
                    ((BoundField)GV1.Columns[1]).DataFormatString = "{0:yyyy/MM}";
                    SDS1.SelectCommand = "SELECT * FROM (SELECT F.FactoryName+'廠' as FactoryName, G.DTime, G.Power_KW, G.Electricity_period " +
                    "FROM G_TPC_M AS G LEFT JOIN Factory AS F ON G.FactoryID = F.FactoryID WHERE G.FactoryID LIKE @F+'%' AND DTime >=@time_s AND DTime < @time_e ) t " +
                    "PIVOT ( MAX(Power_KW) FOR Electricity_period IN ([半尖峰時段], [尖峰時段], [週六半尖峰時段],[離峰時段])) p";
                    SDS1.SelectParameters.Add("F", ddl_f.SelectedValue);
                    SDS1.SelectParameters.Add("time_s", time_s);
                    SDS1.SelectParameters.Add("time_e", time_e);
                    GV1.DataBind();
                    ff.title(GV1);
                    imgb_n.Visible = false;
                    imgb_p.Visible = false;
                }
                //頁籤
                hyl_M0.NavigateUrl = "Tpc_Power.aspx?F="+F+"&T=Y";
                hyl_M1.NavigateUrl = "Tpc_Power.aspx?F="+F+"&T=D";
                hyl_M2.NavigateUrl = "Tpc_Power.aspx?F="+F+"&T=M";
            }

        }

        protected void tb_SDATE_TextChanged(object sender, EventArgs e)
        {
            string F = Request.QueryString["F"];
            string T = Request.QueryString["T"];
            if (T == "D")
            {
                ff.check_ym(tb_SDATE.Text, tb_SDATE);
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + tb_SDATE.Text.Replace("-", "/") + "");
            }
            else if (T == "M")
            {
                ff.check_date(tb_SDATE.Text, tb_SDATE);
                string time_s = tb_SDATE.Text;
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
            }
            else
            {
                check_y(ddl_t.SelectedValue);
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + ddl_t.SelectedValue + "");
            }

        }

        protected void imgb_p_Click(object sender, ImageClickEventArgs e)
        {
            string F = Request.QueryString["F"];
            string T = Request.QueryString["T"];
            if (T == "D")
            {
                ff.check_ym(tb_SDATE.Text, tb_SDATE);
                string time_s = Convert.ToDateTime(tb_SDATE.Text).AddMonths(-1).ToString("yyyy/MM");
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
            }
            else if (T == "M")
            {
                ff.check_date(tb_SDATE.Text, tb_SDATE);
                string time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(-1).ToString("yyyy-MM-dd");
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
            }
            else
            {
                check_y(ddl_t.SelectedValue);
                string time_s = (Convert.ToInt32(ddl_t.SelectedValue) - 1).ToString();
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
            }
        }

        protected void imgb_n_Click(object sender, ImageClickEventArgs e)
        {

            string F = Request.QueryString["F"];
            string T = Request.QueryString["T"];
            if (T == "D")
            {
                ff.check_ym(tb_SDATE.Text, tb_SDATE);
                string time_s = Convert.ToDateTime(tb_SDATE.Text).AddMonths(1).ToString("yyyy/MM");
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
            }
            else if (T == "M")
            {
                ff.check_date(tb_SDATE.Text, tb_SDATE);
                string time_s = Convert.ToDateTime(tb_SDATE.Text).AddDays(1).ToString("yyyy-MM-dd");
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
            }
            else
            {
                check_y(ddl_t.SelectedValue);
                string time_s = (Convert.ToInt32(ddl_t.SelectedValue) + 1).ToString();
                Response.Redirect("Tpc_Power.aspx?F=" + F + "&T=" + T + "&D=" + time_s + "");
            }
        }

        protected void imgb_excel_Click(object sender, ImageClickEventArgs e)
        {
            if (GV1.Rows.Count > 0)
            {
                string T = Request.QueryString["T"];
                if (T == "D")
                {
                    T = "每日";
                }
                else if (T == "M")
                {
                    T = "每15分鐘";
                }
                else
                {
                    T = "每月";
                }
                string name = "台電電量" + "_" + T + "_" + tb_SDATE.Text + ddl_t.Text;
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excel = new ExcelPackage();
                var ws = excel.Workbook.Worksheets.Add(name);
                var workbook = excel.Workbook;
                for (int i = 0; i < GV1.Columns.Count; i++)
                {
                    ws.Cells[1, i + 1].Value = GV1.Columns[i].HeaderText;
                    ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }
                for (int i = 0; i < GV1.Rows.Count; i++)
                {
                    for (int j = 0; j < GV1.Columns.Count; j++)
                    {
                        if (j == 1)
                        {
                            if (T == "每月")
                            {
                                ws.Cells[i + 2, j + 1].Value = GV1.Rows[i].Cells[j].Text;
                            }
                            else
                            {
                                ws.Cells[i + 2, j + 1].Value = tb_SDATE.Text.Substring(0, 4) + "/" + GV1.Rows[i].Cells[j].Text;
                            }
                            
                        }
                        else if (j >= 2)
                        {
                            ws.Cells[i + 2, j + 1].Value = Convert.ToInt32(GV1.Rows[i].Cells[j].Text);
                        }
                        else
                        {
                            ws.Cells[i + 2, j + 1].Value = GV1.Rows[i].Cells[j].Text;
                        }
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

        protected void ddl_t_SelectedIndexChanged(object sender, EventArgs e)
        {
            string T = Request.QueryString["T"];
            check_y(ddl_t.SelectedValue);
            Response.Redirect("Tpc_Power.aspx?F=" + ddl_f.SelectedValue + "&T=" + T + "&D=" + ddl_t.SelectedValue + "");

        }

        protected void ddl_f_SelectedIndexChanged(object sender, EventArgs e)
        {
            string T = Request.QueryString["T"];
            string D = Request.QueryString["D"];
            Response.Redirect("Tpc_Power.aspx?F=" + ddl_f.SelectedValue + "&T="+T+"&D="+D+"");
        }

    }
}