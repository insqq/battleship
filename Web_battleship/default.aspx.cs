﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web_battleship
{
    public partial class Start_Page : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button_Enter_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/main_Page.aspx");
        }
    }
}