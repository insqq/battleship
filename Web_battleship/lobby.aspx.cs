using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Drawing;

namespace Web_battleship
{
    public partial class lobby : System.Web.UI.Page
    {
        Button[,] buttons1;
        Button[,] buttons2;
        
        string status = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Label_name.Text = Session["name"].ToString();
                myGraphics();
                return;
            }
            myGraphics();
            Session["mylbl"] = Label_statusTop;
            Session["upd_Ready"] = UpdatePanel_Ready;
            Session["Panel1"] = UpdatePanel1;
            Session["Panel2"] = UpdatePanel2;
            Session["Panel"] = Panel1;

        }

        protected void Button_logout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/default.aspx");

        }

        protected void Button_findGame_Click(object sender, EventArgs e)
        {
            Label_EnemyNickname.Text = "";
            if (Global.getUser(Session).state == 1)
            {
                ((Button)sender).Text = "Find Game";
                Global.getUser(Session).state = 0;
                Timer2.Enabled = false;
                Response.Redirect(Request.RawUrl);
                return;
            }

            if (Global.getUser(Session).state == 0)
            {
                ((Button)sender).Text = "Cancel Finding";
                Global.getUser(Session).state = 1;
                Timer2.Enabled = true;
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void Button_MakeHit_Click(object sender, EventArgs e)
        {
            int hit = Convert.ToInt32(((Button)sender).ID) - 10100;
            int result = GameProccess.makeHit(Session, hit);

            if (result == 0)
            {
                Panel1.Enabled = false;
            }

            if (result == 5)
            {
                printStatus("!YOU WIN!");
                Global.getUser(Session).reInit();
                Button_findGame.Text = "Find Game";
                Global.getUser(Session).state = 0;
                Button_findGame.Enabled = true;
                return;
            }
        }

        protected void Button_Ready_Click(object sender, EventArgs e)
        {
            UpdatePanel2.ContentTemplateContainer.Visible = true;
            UpdatePanel3.ContentTemplateContainer.Visible = false;
            UpdatePanel_Ready.ContentTemplateContainer.Visible = false;
            UpdatePanel3.Update();
            Global.getUser(Session).ready = true;
            Global.getUser(Session).state = 3;
            Timer_WaitingPlayer.Enabled = true;
            
        }

        private void userPutShip(object sender, EventArgs e)
        {
            status = "wrong action";
            if (Global.getUser(Session).pickedShip != null)
                status = Global.getUser(Session).setShip(sender);

            reDrawShips();
            UpdatePanel3.Update();
            myGraphics();
        }

        protected void Button_pickShipClick(object sender, EventArgs e)
        {
            UpdatePanel3.Update();
            int fromB = 0;
            if (sender == Button_ship_size1) fromB = 1;
            if (sender == Button_ship_size2) fromB = 2;
            if (sender == Button_ship_size3) fromB = 3;
            if (sender == Button_ship_size4) fromB = 4;
            
            foreach (Ship item in Global.getUser(Session).shipList)
            {
                if (item.size == fromB && item.placed == false)
                {
                    foreach (Ship var in Global.getUser(Session).shipList)
                    {
                        var.picked = false;
                    }

                    item.picked = true;
                    Global.getUser(Session).pickedShip = item;
                    reDrawShips();
                    break;
                }
            }


        }

        public void reDrawShips()
        {
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;

            foreach (Ship item in Global.getUser(Session).shipList)
            {
                if (item.size == 1 && item.picked == false && item.placed == false)
                    count1++;
                if (item.size == 2 && item.picked == false && item.placed == false)
                    count2++;
                if (item.size == 3 && item.picked == false && item.placed == false)
                    count3++;
                if (item.size == 4 && item.picked == false && item.placed == false)
                    count4++;
            }
            Button_ship_size1.Text = "x" + count1;
            Button_ship_size2.Text = "x" + count2;
            Button_ship_size3.Text = "x" + count3;
            Button_ship_size4.Text = "x" + count4;

        }

        protected void ImageButton_Wrap_Click(object sender, ImageClickEventArgs e)
        {
            if (Global.getUser(Session).pickedShip != null)
                if (Global.getUser(Session).pickedShip.state == 1) Global.getUser(Session).pickedShip.state = 2;
                else if (Global.getUser(Session).pickedShip != null)
                    if (Global.getUser(Session).pickedShip.state == 2) Global.getUser(Session).pickedShip.state = 1;
        }
        

        protected void Timer1_Tick(object sender, EventArgs e)     //game proccess timer
        {
            string chatmessage = "";
            foreach (string item in GameProccess.chat)
            {
                chatmessage += item;
            }
            TextBox_Chat.Text = chatmessage;

            printStatus("Enemy move!");

            if (GameProccess.getP2(Session) == null)
            {
                
                printStatus("Enemy leave, you win!");
                Global.getUser(Session).reInit();
                Button_findGame.Text = "Find Game";
                Global.getUser(Session).state = 0;
                Button_findGame.Enabled = true;
                Global.getUser(Session).myMove = false;
            }
            if (Global.getUser(Session).state == 4)
            {
                
                printStatus("you lose");
                Button_findGame.Text = "Find Game";
                Global.getUser(Session).reInit();
                Global.getUser(Session).state = 0;
                Button_findGame.Enabled = true;
            }
            if (Global.getUser(Session).myMove)
            {
                printStatus("Your Move!");
                Panel1.Enabled = true;
            }
            UpdatePanel1.Update();
            UpdatePanel2.Update();
            
        }

        void printStatus(string msg)
        {
            Label_statusTop.Text = msg;
            UpdatePanel_status.Update();
        }

        protected void Timer2_Tick(object sender, EventArgs e)   //search game timer
        {
            
            if (Global.getUser(Session).state == 2)
            {
                
                Timer2.Enabled = false;
                PlaceHolder1.Visible = true;
                UpdatePanel3.ContentTemplateContainer.Visible = true;
                Button_findGame.Enabled = false;
                UpdatePanel3.Update();
                Response.Redirect(Request.RawUrl);
            }
        }  

        void rePaint()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    if (Global.getUser(Session).field1[i * 10 + k] == 1)
                    {
                        buttons1[i, k].BackColor = Color.Green;
                    }
                    if (Global.getUser(Session).field1[i * 10 + k] == 2)
                    {
                        buttons1[i, k].Text = "X";
                    }
                    if (Global.getUser(Session).field1[i * 10 + k] == 3)
                    {
                        buttons1[i, k].BackColor = Color.Red;
                    }


                    if (Global.getUser(Session).field2[i * 10 + k] == 2)
                    {
                        buttons2[i, k].Text = "X";

                    }
                    if (Global.getUser(Session).field2[i * 10 + k] == 3)
                    {
                        buttons2[i, k].BackColor = Color.Red;
                    }
                }
            }
            UpdatePanel1.Update();
            UpdatePanel2.Update();
        }
        void myGraphics()
        {
            PlaceHolder1.Controls.Clear();
            PlaceHolder2.Controls.Clear();
            buttons1 = new Button[10, 10];
            buttons2 = new Button[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    buttons1[i, k] = new Button();
                    buttons2[i, k] = new Button();
                    PlaceHolder1.Controls.Add(buttons1[i, k]);
                    PlaceHolder2.Controls.Add(buttons2[i, k]);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    buttons1[i, k].ID = "" + i + k;

                    buttons1[i, k].Attributes.Add("style", "margin-top: 0;");
                    buttons1[i, k].Attributes.Add("style", "margin-bottom: 0;");
                    buttons1[i, k].Height = 20;
                    buttons1[i, k].Width = 20;
                    buttons1[i, k].TabIndex = (short)(3 + i + k);
                    buttons1[i, k].Attributes.Add("onmouseover", "event_onMouseOver(" + "" + i + k + ");");
                    buttons1[i, k].Attributes.Add("onmouseout", "event_onMouseOut(" + "" + i + k + ");");
                    buttons1[i, k].Attributes.Add("onclick", "event_SetShip(" + "" + i + k + ");");
                    buttons1[i, k].Click += new System.EventHandler(this.userPutShip);
                    if (Global.getUser(Session).field1[i * 10 + k] == 1)
                    {
                        buttons1[i, k].BackColor = Color.Green;
                    }
                    else
                    {
                        buttons1[i, k].BackColor = System.Drawing.Color.White;
                    }

                    buttons2[i, k].ID = "" + 101 + i + k;
                    buttons2[i, k].Height = 20;
                    buttons2[i, k].Width = 20;
                    buttons2[i, k].TabIndex = (short)(3 + i + k);
                    buttons2[i, k].BackColor = System.Drawing.Color.White;
                    buttons2[i, k].Click += new System.EventHandler(this.Button_MakeHit_Click);
                }
            }
            rePaint();


            try
            {
                UpdatePanel_EnemyNickname.Visible = true;
                Label_EnemyNickname.Text = "Enemy nickname: " + GameProccess.getP2(Session).nick;
            }
            catch { } 
            if (Global.getUser(Session).state == 1)    //1 - search game
            {
                
                printStatus("Search enemy player");

                Timer2.Enabled = true;
                Button_findGame.Text = "Cancel Finding";
                UpdatePanel5.ContentTemplateContainer.Visible = false;
                PlaceHolder1.Visible = false;
                UpdatePanel2.ContentTemplateContainer.Visible = false;
                UpdatePanel3.ContentTemplateContainer.Visible = false;
                UpdatePanel_Ready.ContentTemplateContainer.Visible = false;
                return;
            }

            if (Global.getUser(Session).state == 0 )   // 0 - i lobby
            {
                
                
                printStatus("Search stopped");
                Timer2.Enabled = false;
                Button_findGame.Text = "Find Game";
                UpdatePanel5.ContentTemplateContainer.Visible = false;
                PlaceHolder1.Visible = false;
                Panel1.Enabled = false;
                UpdatePanel2.ContentTemplateContainer.Visible = false;
                UpdatePanel3.ContentTemplateContainer.Visible = false;
                UpdatePanel_Ready.ContentTemplateContainer.Visible = false;
                return;
            }

            

            if (Global.getUser(Session).state == 2)   // 2 - ready
            {
                
                
                printStatus("place your ships");
                Button_findGame.Text = "Cancel Finding";
                Button_findGame.Enabled = false;
                Panel1.Enabled = false;
                UpdatePanel2.ContentTemplateContainer.Visible = false;
                if (!Global.getUser(Session).putAllShips)
                {
                    UpdatePanel_Ready.ContentTemplateContainer.Visible = false;
                }
                UpdatePanel5.ContentTemplateContainer.Visible = false;
                reDrawShips();
                return;
            }

            if (Global.getUser(Session).state == 3)  // 3 - play game
            {
                if (GameProccess.getP2(Session) == null)
                {
                    printStatus("Enemy leave, you win!");
                    Global.getUser(Session).myMove = false;
                    Global.getUser(Session).state = 5;
                    return;
                }
                Button_findGame.Text = "Cancel Finding";
                Button_findGame.Enabled = false;
                UpdatePanel5.ContentTemplateContainer.Visible = false;
                if (!GameProccess.getP2(Session).ready)   // if waiting player
                {
                    printStatus("wait");
                    Timer_WaitingPlayer.Enabled = true;
                }
                else
                {
                    Timer1.Enabled = true;
                    //Panel1.Enabled = true;
                }
                UpdatePanel3.ContentTemplateContainer.Visible = false;
                UpdatePanel_Ready.ContentTemplateContainer.Visible = false;
                return;
            }
        }
        protected void Timer_WaitingPlayer_Tick(object sender, EventArgs e)
        {
            string chatmessage = "";
            foreach (string item in GameProccess.chat)
            {
                chatmessage += item;
            }
            TextBox_Chat.Text = chatmessage;
            if (GameProccess.getP2(Session) == null)
            {
                Timer_Chat.Enabled = false;
                UpdatePanel_chat.Visible = false;
                printStatus("Enemy leave, you win!");
                Global.getUser(Session).reInit();
                Button_findGame.Text = "Find Game";
                Global.getUser(Session).state = 0;
                Button_findGame.Enabled = true;
                Global.getUser(Session).myMove = false;
                return;
            }
            UpdatePanel5.ContentTemplateContainer.Visible = true;
            
            if (!GameProccess.getP2(Session).ready)
            {
                return;
            }
            Timer1.Enabled = true;
            Timer_WaitingPlayer.Enabled = false;
            UpdatePanel5.ContentTemplateContainer.Visible = false;
            GameProccess.firstHit(Session);
        }

        protected void Button_chatEnter_Click(object sender, EventArgs e)
        {
            string msg = Global.getUser(Session).nick+ ": "+ TextBox_chatinput.Text+ "<br />";
            GameProccess.chat.Add(msg);
            TextBox_chatinput.Text = "";
        }

        protected void Timer_Chat_Tick(object sender, EventArgs e)
        {
            string chatmessage = "";
            foreach (string item in GameProccess.chat)
            {
                chatmessage += item;
            }
            TextBox_Chat.Text = chatmessage;
            
        }
    }
}