using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using factory.lib;
using System.Diagnostics;

namespace factory.Light
{
    public partial class Factory_state : System.Web.UI.Page
    {
        //判斷是否有連線
        public void network(string ip, Label lb1, Label lb2, Image img1, Image img2)
        {
            SQLDB db = new SQLDB();
            //230~8台主機
            string sql = "SELECT TOP 1 * FROM Factory_State WHERE HOST1 = '192.168.10.230' AND HOST2 = '"+ ip +"' ORDER BY DTime DESC";
            DataTable dt = db.GetDataTable(sql, CommandType.Text);
            //8台主機~230
            sql = "SELECT TOP 1 * FROM Factory_State WHERE HOST1 = '" + ip +"' ORDER BY DTime DESC";
            DataTable dx = db.GetDataTable(sql, CommandType.Text);

            if (dt.Rows[0][3].ToString() == "True")
            {
                //計算時間要是資料庫時間大於10分鐘就是斷線
                DateTime start1 = Convert.ToDateTime(dt.Rows[0][0].ToString());
                DateTime end1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                TimeSpan ts1 = end1 - start1;
                int x = Convert.ToInt32(ts1.TotalSeconds);
                if (x > 630)
                {
                    img2.ImageUrl = "../img/remove.png";
                }
                else
                {
                    img2.ImageUrl = "../img/correct.png";
                }

            }

            if (dx.Rows[0][3].ToString() == "True")
            {
                //計算時間要是資料庫時間大於10分鐘就是斷線
                DateTime start1 = Convert.ToDateTime(dx.Rows[0][0].ToString());
                DateTime end1 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                TimeSpan ts1 = end1 - start1;
                int x = Convert.ToInt32(ts1.TotalSeconds);
                if (x > 630)
                {
                    img1.ImageUrl = "../img/remove.png";
                }
                else
                {
                    img1.ImageUrl = "../img/correct.png";
                }

            }
            string s = Convert.ToDateTime(dt.Rows[0][0].ToString()).ToString("MM-dd HH:mm:ss");
            lb1.Text =  s;
            s = Convert.ToDateTime(dx.Rows[0][0].ToString()).ToString("MM-dd HH:mm:ss");
            lb2.Text =  s;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                List<string> ip = new List<string>();
                ip.Add("192.168.101.45");
                ip.Add("192.168.21.45");
                ip.Add("192.168.41.45");
                ip.Add("192.168.51.45");
                ip.Add("192.168.31.45");
                ip.Add("192.168.61.45");
                ip.Add("192.168.91.45");
                ip.Add("192.168.71.45");
                
                List<Image> img1 = new List<Image>();
                img1.Add(KY_230);
                img1.Add(BL_230);
                img1.Add(QX_230);
                img1.Add(ZB_230);
                img1.Add(KH_230);
                img1.Add(LD_230);
                img1.Add(LZ_230);
                img1.Add(HL_230);

                List<Image> img2 = new List<Image>();
                img2.Add(H_KY);
                img2.Add(H_BL);
                img2.Add(H_QX);
                img2.Add(H_ZB);
                img2.Add(H_KH);
                img2.Add(H_LD);
                img2.Add(H_LZ);
                img2.Add(H_HL);

                List<Label> lb1 = new List<Label>();
                lb1.Add(lb_KY1);
                lb1.Add(lb_BL1);
                lb1.Add(lb_QX1);
                lb1.Add(lb_ZB1);
                lb1.Add(lb_LD1);
                lb1.Add(lb_KH1);
                lb1.Add(lb_LZ1);
                lb1.Add(lb_HL1);
                List<Label> lb2 = new List<Label>();
                lb2.Add(lb_KY2);
                lb2.Add(lb_BL2);
                lb2.Add(lb_QX2);
                lb2.Add(lb_ZB2);
                lb2.Add(lb_LD2);
                lb2.Add(lb_KH2);
                lb2.Add(lb_LZ2);
                lb2.Add(lb_HL2);

                for (int i = 0; i < 8; i++)
                {
                    network(ip[i], lb1[i], lb2[i],img1[i],img2[i]);
                }

            }
            
        }

        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //分頁
            //SDS1.SelectCommand = Session["l"].ToString();
            others o = new others();
            o.Page(sender, e);

        }

        protected void imgb_Click(object sender, ImageClickEventArgs e)
        {
            /*
            ImageButton clickedButton = (ImageButton)sender;
            string f = clickedButton.ID.ToString();
            string sql = "";
            if (f == "imgb_KY")
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.101.45' ORDER BY DTime DESC";
            }
            else if (f == "imgb_BL")
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.21.45' ORDER BY DTime DESC";
            }
            else if (f == "imgb_QX")
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.41.45' ORDER BY DTime DESC";
            }
            else if (f == "imgb_ZB")
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.51.45' ORDER BY DTime DESC";
            }
            else if (f == "imgb_KH")
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.61.45' ORDER BY DTime DESC";
            }
            else if (f == "imgb_LD")
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.31.45' ORDER BY DTime DESC";
            }
            else if (f == "imgb_LZ")
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.91.45' ORDER BY DTime DESC";
            }
            else
            {
                sql = "SELECT * FROM Factory_State WHERE Host1 = '192.168.71.45' ORDER BY DTime DESC";
            }
            GV1.PageIndex = 0;
            SDS1.SelectCommand = sql;
            Session["l"] = sql;
            */
        }

        public void fname(string host,int i,int n)
        {
            if (host == "192.168.71.45")
            {
                GV1.Rows[i].Cells[n].Text = "花蓮廠(192.168.71.45)";
            }
            else if (host == "192.168.21.45")
            {
                GV1.Rows[i].Cells[n].Text = "八里廠(192.168.21.45)";
            }
            else if (host == "192.168.31.45")
            {
                GV1.Rows[i].Cells[n].Text = "龍德廠(192.168.31.45)";
            }
            else if (host == "192.168.41.45")
            {
                GV1.Rows[i].Cells[n].Text = "全興廠(192.168.41.45)";
            }
            else if (host == "192.168.51.45")
            {
                GV1.Rows[i].Cells[n].Text = "彰濱廠(192.168.51.45)";
            }
            else if (host == "192.168.61.45")
            {
                GV1.Rows[i].Cells[n].Text = "高雄廠(192.168.61.45)";
            }
            else if (host == "192.168.91.45")
            {
                GV1.Rows[i].Cells[n].Text = "利澤廠(192.168.91.45)";
            }
            else if(host == "192.168.1011.45")
            {
                GV1.Rows[i].Cells[n].Text = "觀音廠(192.168.101.45)";
            }
        }

        protected void GV1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (GV1.Rows.Count > 0)
            {
                for (int i = 0; i < GV1.Rows.Count; i++) 
                {
                    string host1 = GV1.Rows[i].Cells[1].Text;
                    string host2 = GV1.Rows[i].Cells[2].Text;
                    fname(host1, i ,1);
                    fname(host2, i ,2);

                }
            }

        }
    }
}