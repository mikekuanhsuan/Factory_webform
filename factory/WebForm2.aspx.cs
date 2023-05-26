using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Data;
using OfficeOpenXml;
using System.IO;
using System.Text.RegularExpressions;
using factory.lib;
using System.Data.SqlClient;

namespace factory
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            Capture("D:/ScreenShot.jpg");//path to Save Captured files  
        }
        public void Capture(string CapturedFilePath)
        {
            Bitmap bitmap = new Bitmap
            (Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap as System.Drawing.Image);
            graphics.CopyFromScreen(200, 100, 200, 200, bitmap.Size);
            bitmap.Save(CapturedFilePath, ImageFormat.Bmp);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        public SQLDB db = new SQLDB();

        public void call_sql(string f, string m, string h, string d, string v1, string v2, string v3)
        {
            string sql = "DELETE FROM test123 WHERE FactoryID = @FactoryID AND MID = @MID AND DTime = @DTime";
            List<SqlParameter> p_list = new List<SqlParameter>();
            p_list.Add(new SqlParameter("@FactoryID", f));
            p_list.Add(new SqlParameter("@MID", m));
            p_list.Add(new SqlParameter("@DTime", d));
            db.RunCmd(sql, p_list, CommandType.Text);

            sql = "INSERT INTO test123 VALUES(@FactoryID,@MID,@DataDate,@DHour,@DTime,@v1,@v2,@v3)";
            p_list.Clear();
            p_list.Add(new SqlParameter("@FactoryID", f));
            p_list.Add(new SqlParameter("@MID", m));
            p_list.Add(new SqlParameter("@DataDate", d));
            p_list.Add(new SqlParameter("@DHour", h));
            p_list.Add(new SqlParameter("@DTime", d));
            v1 = v1.Length == 0 ? "0" : v1;
            v2 = v2.Length == 0 ? "0" : v2;
            v3 = v3.Length == 0 ? "0" : v3;
            p_list.Add(new SqlParameter("@v1", v1));
            p_list.Add(new SqlParameter("@v2", v2));
            p_list.Add(new SqlParameter("@v3", v3));
            db.RunCmd(sql, p_list, CommandType.Text);
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            
            if (FileUpload1.HasFile && Path.GetExtension(FileUpload1.FileName) == ".xlsx")
            {
                using (var excel = new ExcelPackage(FileUpload1.PostedFile.InputStream))
                {
                    //取得所有表
                    string f = "";
                    for (int i = 0; i < excel.Workbook.Worksheets.Count; i++)
                    {
                        var sheet = excel.Workbook.Worksheets[i];
                        string name = sheet.Name;
                        string n = "比表篩餘水分";
                        //取得是哪個工廠
                        if (name.IndexOf("觀音廠") > -1)
                        {
                            f = "KY-T1HIST";
                        }
                        else if (name.IndexOf("八里廠") > -1)
                        {
                            f = "BL-T1HIST";
                        }
                        else if (name.IndexOf("全興廠") > -1)
                        {
                            f = "QX-T1HIST";
                        }
                        else if (name.IndexOf("彰濱廠") > -1)
                        {
                            f = "ZB-T1HIST";
                        }
                        else if (name.IndexOf("高雄廠") > -1)
                        {
                            f = "KH-PCC-LH";
                        }
                        else if (name.IndexOf("龍德廠") > -1)
                        {
                            f = "LD-T1HIST";
                        }
                        else if (name.IndexOf("利澤廠") > -1)
                        {
                            f = "LZ-T1HIST";
                        }
                        else if (name.IndexOf("花蓮廠") > -1)
                        {
                            f = "HL-T1HIST";
                        }

                        //判斷是否為比表篩餘水分的表
                        if (name.IndexOf(n) > -1)
                        {
                            //過濾掉名稱並且在每一個數字前加上#
                            Regex regex = new Regex(@"\D");
                            string m = regex.Replace(name, "");
                            int c = m.Length + 1;
                            for (int j = 0; j < c; j += 2)
                            {
                                m = m.Insert(j, "#");
                            }

                            //取得資料
                            for (int j = 0; j < 31; j++)
                            {
                                //10~16
                                for (int k = 0; k < 22; k+=3)
                                {
                                    //日期轉換格式
                                    string time = sheet.Cells[4 + j, 1].Value.ToString();
                                    double TimeNow = Convert.ToDouble(time);
                                    DateTime dt = new DateTime(1899, 12, 30);
                                    dt = dt.AddDays(TimeNow);
                                    //小時
                                    string h = sheet.Cells[1, 8 + k].Text;

                                    string v1 = sheet.Cells[4 + j, 8  + k].Text;
                                    string v2 = sheet.Cells[4 + j, 9  + k].Text;
                                    string v3 = sheet.Cells[4 + j, 10 + k].Text;
                                    if (v1.Length + v2.Length + v3.Length > 1)
                                    {
                                        string d = dt.ToString("yyyy-MM-dd ") + h + ":00:00";
                                        call_sql(f, m, h, d, v1, v2, v3);

                                        /*
                                        Response.Write("面積:" + v1 +" 塞於:" + v2 + " 水分:" + v3);
                                        Response.Write("我是時間:" + dt.ToString("yyyy-MM-dd") + " 小時:" + h);
                                        Response.Write("工廠:" + f);
                                        Response.Write("<br>");
                                        */
                                    }
                                }
                                
                                //17~1
                                for (int k = 0; k < 28; k += 3)
                                {
                                    //日期轉換格式
                                    string time = sheet.Cells[4 + j, 1].Value.ToString();
                                    double TimeNow = Convert.ToDouble(time);
                                    DateTime dt = new DateTime(1899, 12, 30);
                                    dt = dt.AddDays(TimeNow);
                                    //小時
                                    string h = sheet.Cells[36, 2 + k].Text;
                                    string v1 = sheet.Cells[39 + j, 2 + k].Text;
                                    string v2 = sheet.Cells[39 + j, 3 + k].Text;
                                    string v3 = sheet.Cells[39 + j, 4 + k].Text;
                                    if (v1.Length + v2.Length + v3.Length > 1)
                                    {
                                        if (h == "24")
                                        {
                                            h = "0";
                                            string d = (dt.AddDays(1).ToString("yyyy-MM-dd ")) + h + ":00:00";
                                            call_sql(f, m, h, d, v1, v2, v3);
                                            /*
                                            h = "0";
                                            Response.Write("面積:" + v1 + " 塞於:" + v2 + " 水分:" + v3);
                                            Response.Write("我是時間:" + dt.AddDays(1).ToString("yyyy-MM-dd") + " 小時:" + h);
                                            Response.Write("工廠:" + f);
                                            Response.Write("<br>");
                                            */
                                        }
                                        else
                                        {
                                            string d = dt.ToString("yyyy-MM-dd ") + h + ":00:00";
                                            call_sql(f, m, h, d, v1, v2, v3);
                                            /*
                                            Response.Write("面積:" + v1 + " 塞於:" + v2 + " 水分:" + v3);
                                            Response.Write("我是時間:" + dt.ToString("yyyy-MM-dd") + " 小時:" + h);
                                            Response.Write("工廠:" + f);
                                            Response.Write("<br>");
                                            */
                                        }
                                    }
                                }
                                
                                //2~8
                                for (int k = 0; k < 22; k += 3)
                                {
                                    //日期轉換格式
                                    string time = sheet.Cells[4 + j, 1].Value.ToString();
                                    double TimeNow = Convert.ToDouble(time);
                                    DateTime dt = new DateTime(1899, 12, 30);
                                    dt = dt.AddDays(TimeNow);
                                    //小時
                                    string h = sheet.Cells[71, 2 + k].Text;
                                    string v1 = sheet.Cells[74 + j, 2 + k].Text;
                                    string v2 = sheet.Cells[74 + j, 3 + k].Text;
                                    string v3 = sheet.Cells[74 + j, 4 + k].Text;
                                    if (v1.Length + v2.Length + v3.Length > 1)
                                    {
                                        string d = dt.AddDays(1).ToString("yyyy-MM-dd ") + h + ":00:00";
                                        call_sql(f, m, h, d, v1, v2, v3);
                                    }
                                }
                                
                            }
                        }
                    }
                    /*
                    var sheet1 = excel.Workbook.Worksheets[2];
                    var sheet2 = excel.Workbook.Worksheets[3];
                    var sheet3 = excel.Workbook.Worksheets[4];
                    string msg1 = sheet1.Cells[5, 1].Value.ToString();
                    string msg2 = sheet1.Cells[1, 1].Text;
                    double TimeNow = Convert.ToDouble(msg1);
                    DateTime dt = new DateTime(1899, 12, 30);
                    dt = dt.AddDays(TimeNow);
                    Label1.Text = dt.ToString("yyyy-MM-dd");
                    */
                }
            }
            else
            {
                UploadStatusLabel.Text = "You did not specify a file to upload.";
            }
            

        }
    }
}