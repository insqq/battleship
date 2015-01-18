using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Web_battleship
{
    public partial class admin_page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label lbl = new Label();
            lbl.Text = "" + Global.Sessions.Count;
            this.Controls.Add(lbl);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(string)));

            for (int i = 0; i < Global.Sessions.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = Global.Sessions[i].userID;
                dt.Rows.Add(dr);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}