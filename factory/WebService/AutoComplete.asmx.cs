using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using factory.lib;
using System.Data;
using System.Data.SqlClient;

namespace factory
{
    /// <summary>
    ///AutoComplete 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下列一行。
    [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {
        public AutoComplete()
        {

            //如果使用設計的元件，請取消註解下行程式碼 
            //InitializeComponent(); 
        }
        [WebMethod]
        public string[] GetCompletion(string prefixText)
        {

            SQLDB db = new SQLDB();
            db.replace(1);
            List<string> tmp = new List<string>();
            List<SqlParameter> par_list = new List<SqlParameter>();
            string sql = "SELECT L.SourceTag FROM Live AS L WHERE CHARINDEX('"+ prefixText + "',L.SourceTag) > 0 AND L.TagName NOT LIKE '%$%' ESCAPE '/' AND L.Value IS NOT NULL ORDER BY L.DateTime DESC";
            par_list.Add(new SqlParameter("@Text", prefixText));
            DataTable dt = db.GetDataTable(sql, CommandType.Text);

            for (int i = 1; i < dt.Rows.Count; i++)
            {
                tmp.Add(dt.Rows[i][0].ToString());
            }
            return tmp.ToArray();
        }
    }
}
