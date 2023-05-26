using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace factory.lib
{
    public class others
    {
        //分頁
        public void Page(object sender, GridViewPageEventArgs e)
        {
            //http://shaurong.blogspot.com/2019/10/aspnet-gridview-x-x.html
            GridView gvw = (GridView)sender;
            if (e.NewPageIndex < 0)
            {
                TextBox pageNum = (TextBox)gvw.BottomPagerRow.FindControl("txtNewPageIndex");
                int Pa = 0;
                if (pageNum.Text != "")
                {
                    Pa = Pa = int.Parse(pageNum.Text);
                }

                if (Pa <= 0)
                {
                    gvw.PageIndex = 0;
                }
                else
                {
                    gvw.PageIndex = Pa - 1;
                }
            }
            else
            {
                gvw.PageIndex = e.NewPageIndex;
            }
        }
        //加密
        public string encryption(string n)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(n);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        //解密
        public string decrypt(string n)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(n);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
}