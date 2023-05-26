using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace factory.lib
{
    public class f_class
    {
        //工廠名稱
        public string factory_name(String F)
        {
            string name = "";
            if (F == "KY-T1HIST")
            {
                name = "觀音廠";
            }
            else if (F == "BL-T1HIST")
            {
                name = "八里廠";
            }
            else if (F == "QX-T1HIST")
            {
                name = "全興廠";
            }
            else if (F == "ZB-T1HIST")
            {
                name = "彰濱廠";
            }
            else if (F == "KH-PCC-LH")
            {
                name = "高雄廠";
            }
            else if (F == "LD-T1HIST")
            {
                name = "龍德廠";
            }
            else if (F == "LZ-T1HIST")
            {
                name = "利澤廠";
            }
            else if (F == "HL-T1HIST")
            {
                name = "花蓮廠";
            }
            else if (F == "XG-T1HIST")
            {
                name = "小港廠";
            }
            else
            {
                name = "";
            }
            return name;
        }
        //工廠名稱
        public string f_name(string F)
        {
            string name = "";
            if (F == "KY")
            {
                name = "觀音廠";
            }
            else if (F == "BL")
            {
                name = "八里廠";
            }
            else if (F == "QX")
            {
                name = "全興廠";
            }
            else if (F == "ZB")
            {
                name = "彰濱廠";
            }
            else if (F == "KH")
            {
                name = "高雄廠";
            }
            else if (F == "LD")
            {
                name = "龍德廠";
            }
            else if (F == "LZ")
            {
                name = "利澤廠";
            }
            else if (F == "HL")
            {
                name = "花蓮廠";
            }
            else if (F == "XG")
            {
                name = "小港廠";
            }
            else
            {
                name = "";
            }
            return name;
        }
        //檢查日期
        public void check_date(string n,TextBox tb)
        {
            //檢查日期是否有誤
            DateTime t;
            bool flag = DateTime.TryParse(n, out t);
            if (flag == false || n.Length != 10)
            {
                string d = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                tb.Text = d;
            }
        }
        public void check_ym(string n, TextBox tb)
        {
            bool ym = Regex.IsMatch(n, "^\\d{4}-((0\\d)|(1[012]))$");
            if (ym == false)
            {
                string time = DateTime.Now.ToString("yyyy-MM");
                tb.Text = time;
            }
        }
        //判斷網址取得機器 適用於磨機日報
        public string get_m(string M, string F)
        {
            //全興廠
            if (F == "QX-T1HIST" && M == "1")
            {
                return "#1";
            }
            else if (F == "QX-T1HIST" && M == "3")
            {
                return "#2#3";
            }
            //龍德廠
            if (F == "LD-T1HIST" && M == "1")
            {
                return "#1";
            }
            else if (F == "LD-T1HIST" && M == "2")
            {
                return "#2";
            }
            //其餘
            if (M == "1")
            {
                return "#1#2";
            }
            else if (M == "3")
            {
                return "#3#4";
            }
            else if (M == "5")
            {
                return "#5#6";
            }
            else
            {
                return "#7#8";
            }
        }
        public string get_mn(string M, string F)
        {
            //全興廠
            if (F == "QX" && M == "1")
            {
                return "#1";
            }
            else if (F == "QX" && M == "3")
            {
                return "#2#3";
            }
            //龍德廠
            if (F == "LD" && M == "1")
            {
                return "#1";
            }
            else if (F == "LD" && M == "2")
            {
                return "#2";
            }
            //其餘
            if (M == "1")
            {
                return "#1#2";
            }
            else if (M == "3")
            {
                return "#3#4";
            }
            else if (M == "5")
            {
                return "#5#6";
            }
            else
            {
                return "#7#8";
            }
        }

        //判斷當前日期 停用/啟動 下一天的功能
        public void Enabled(string n, ImageButton b)
        {
            DateTime start = Convert.ToDateTime(n);
            DateTime end = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            TimeSpan ts = start - end;
            int x = Convert.ToInt32(ts.TotalSeconds);
            x = Convert.ToInt32(ts.Days);
            if (x >= 0)
            {
                b.Enabled = false;
            }
            else
            {
                b.Enabled = true;
            }
        }
        //判斷當前日期 停用/啟動 下一個月的功能
        public void En_imgb_n(string n, ImageButton b)
        {
            DateTime start = Convert.ToDateTime(n);
            DateTime end = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM"));
            TimeSpan ts = start - end;
            int x = Convert.ToInt32(ts.Days);
            if (x >= -1)
            {
                b.Enabled = false;
            }
            else
            {
                b.Enabled = true;
            }

            
        }
        //固定表頭
        public void title(GridView GV)
        {
            if (GV.Rows.Count > 0)
            {
                GV.UseAccessibleHeader = true;
                GV.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}