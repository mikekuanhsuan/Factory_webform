using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using factory.lib;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace factory
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<object> s = new List<object>();
            s.Add(null);
            object x = s[0];
            if (x == null)
            {
                Response.Write("123");
            }
            Response.Write(x);
        }
    }
}