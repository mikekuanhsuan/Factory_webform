using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace factory
{
    public partial class test : System.Web.UI.MasterPage
    {
        public string name()
        {
            string n = Session["Factory_Name"].ToString();
            if (n == "" || n == null)
            {
                return "";
            }
            else
            {
                return Session["Factory_Name"].ToString() + "廠";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //這個要刪除暫時先用這樣
            if (Session["Factory_Name"] == null)
            {
                Session["User_ID"] = "toyz123";
                Session["Factory_Name"] = "觀音";
                Session["User_Name"] = "玩具";
            }
            */
            //沒登入就跳出去
            
            if (Session["User_ID"] == null)
            {
                Response.Redirect("/login.aspx");
            }
            
            //顯示各廠選單
            string F = Request.QueryString["F"];
            //string ID = Session["FactoryID"].ToString();
            /*
            if (ID == "KY-T1HIST")
            {
                factory_KY.Visible = true;
            }
            else if (ID == "BL-T1HIST")
            {
                factory_BL.Visible = true;
            }
            else if (ID == "QX-T1HIST")
            {
                factory_QX.Visible = true;
            }
            else if (ID == "ZB-T1HIST")
            {
                factory_ZB.Visible = true;
            }
            else if (ID == "KH-PCC-LH")
            {
                factory_KH.Visible = true;
            }
            else if (ID == "LD-T1HIST")
            {
                factory_LD.Visible = true;
            }
            else if (ID == "LZ-T1HIST")
            {
                factory_LZ.Visible = true;
            }
            else if (ID == "HL-T1HIST")
            {
                factory_HL.Visible = true;
            }
            else if (ID == "XF-T1HIST")
            {
                factory_XG.Visible = true;
            }
            else
            {
                factory_KY.Visible = true;
                factory_BL.Visible = true;
                factory_QX.Visible = true;
                factory_ZB.Visible = true;
                factory_KH.Visible = true;
                factory_LD.Visible = true;
                factory_LZ.Visible = true;
                factory_HL.Visible = true;
                factory_XG.Visible = true;
            }
            */
            //這段要拿掉
            factory_KY.Visible = true;
            factory_BL.Visible = true;
            factory_QX.Visible = true;
            factory_ZB.Visible = true;
            factory_KH.Visible = true;
            factory_LD.Visible = true;
            factory_LZ.Visible = true;
            factory_HL.Visible = true;
            factory_XG.Visible = true;

            //顯示選單裡面的內容
            if (F == "KY-T1HIST")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_KY_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_KY_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Mill_1 = (HyperLink)FindControl("factory_KY_Mill_1");
                hyl_Mill_1.Visible = true;
                HyperLink hyl_Mill_3 = (HyperLink)FindControl("factory_KY_Mill_3");
                hyl_Mill_3.Visible = true;
                HyperLink hyl_Mill_5 = (HyperLink)FindControl("factory_KY_Mill_5");
                hyl_Mill_5.Visible = true;
                HyperLink hyl_PV = (HyperLink)FindControl("factory_KY_PV");
                hyl_PV.Visible = true;
            }
            else if (F == "BL-T1HIST")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_BL_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_BL_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Mill_1 = (HyperLink)FindControl("factory_BL_Mill_1");
                hyl_Mill_1.Visible = true;
                HyperLink hyl_Mill_3 = (HyperLink)FindControl("factory_BL_Mill_3");
                hyl_Mill_3.Visible = true;
            }
            else if (F == "QX-T1HIST")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_QX_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_QX_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Air1 = (HyperLink)FindControl("factory_QX_Air1");
                hyl_Air1.Visible = true;
                HyperLink hyl_Air2 = (HyperLink)FindControl("factory_QX_Air2");
                hyl_Air2.Visible = true;
                HyperLink hyl_Mill_1 = (HyperLink)FindControl("factory_QX_Mill_1");
                hyl_Mill_1.Visible = true;
                HyperLink hyl_Mill_3 = (HyperLink)FindControl("factory_QX_Mill_3");
                hyl_Mill_3.Visible = true;
            }
            else if (F == "ZB-T1HIST")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_ZB_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_ZB_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Mill_1 = (HyperLink)FindControl("factory_ZB_Mill_1");
                hyl_Mill_1.Visible = true;
                HyperLink hyl_Mill_3 = (HyperLink)FindControl("factory_ZB_Mill_3");
                hyl_Mill_3.Visible = true;
                HyperLink hyl_Mill_5 = (HyperLink)FindControl("factory_ZB_Mill_5");
                hyl_Mill_5.Visible = true;
                HyperLink hyl_Mill_7 = (HyperLink)FindControl("factory_ZB_Mill_7");
                hyl_Mill_7.Visible = true;
                HyperLink hyl_PV_1 = (HyperLink)FindControl("factory_ZB_PV_1");
                hyl_PV_1.Visible = true;
                HyperLink hyl_PV_2 = (HyperLink)FindControl("factory_ZB_PV_2");
                hyl_PV_2.Visible = true;
            }
            else if (F == "KH-PCC-LH")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_KH_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_KH_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Mill = (HyperLink)FindControl("factory_KH_Mill");
                hyl_Mill.Visible = true;
            }
            else if (F == "LD-T1HIST")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_LD_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_LD_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Air1 = (HyperLink)FindControl("factory_LD_Air1");
                hyl_Air1.Visible = true;
                HyperLink hyl_Air2 = (HyperLink)FindControl("factory_LD_Air2");
                hyl_Air2.Visible = true;
                HyperLink hyl_Mill_1 = (HyperLink)FindControl("factory_LD_Mill_1");
                hyl_Mill_1.Visible = true;
                HyperLink hyl_Mill_2 = (HyperLink)FindControl("factory_LD_Mill_2");
                hyl_Mill_2.Visible = true;
            }
            else if (F == "LZ-T1HIST")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_LZ_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_LZ_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Mill = (HyperLink)FindControl("factory_LZ_Mill");
                hyl_Mill.Visible = true;
                HyperLink hyl_PV = (HyperLink)FindControl("factory_LZ_PV");
                hyl_PV.Visible = true;
            }
            else if (F == "HL-T1HIST")
            {
                HyperLink hyl_Power = (HyperLink)FindControl("factory_HL_Power");
                hyl_Power.Visible = true;
                HyperLink hyl_Air = (HyperLink)FindControl("factory_HL_Air");
                hyl_Air.Visible = true;
                HyperLink hyl_Mill = (HyperLink)FindControl("factory_HL_Mill");
                hyl_Mill.Visible = true;
            }
            else if (F == "XG-T1HIST")
            {
                /*
                HyperLink hyl_PV = (HyperLink)FindControl("factory_XG_PV");
                hyl_PV.Visible = true;
                */
            }
            /*
            //只能看自己的廠 如果修改網址列參數則跳回首頁
            if (F != null)
            {
                if (F.Substring(0,2) != ID.Substring(0,2) && ID != "root")
                {
                    Response.Redirect("/index.aspx");
                }
            }
            */
        }
    }
}