using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_battleship
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["name"] != "")
            {
                Response.Redirect("~/lobby.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (Users item in Global.Sessions)
            {
                if (item.nick.Equals(TextBox1.Text))
                {
                    Response.Write("nick name allready taken");
                    return;
                }
            }
            foreach (Users item in Global.Sessions)
            {
                if (item.userID == Session.SessionID)
                {
                    item.nick = TextBox1.Text;
                }
            }

            Session["name"] = TextBox1.Text;
            Response.Redirect("~/lobby.aspx");
        }
    }
}