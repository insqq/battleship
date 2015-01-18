using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace Web_battleship
{
    public class Users
    {
        public  HttpSessionState sck;
        System.Web.UI.Page lobby_page;

        public List<Ship> shipList;
        public Ship pickedShip;
        public byte[] field1;
        public byte[] field2;
        public bool ready;
        public bool myMove;
        public bool putAllShips;
        public string nick;
        public string userID;
        private int _state;
        

        public int state
        {
            set
            {
                _state = value;
                if (value == 1)
                {
                    Global.tryStartGame();
                }
            }
            get
            {
                return _state;
            }
        }

        public Users(string _nick, string _userID, HttpSessionState _sck1)
        {
            reInit();
            nick = _nick;
            userID = _userID;
            sck = _sck1;
            
        }

        public void reInit()
        {
            field1 = new byte[100];
            field2 = new byte[100];
            putAllShips = false;
            myMove = false;
            ready = false;
            _state = 0;
            for (int i = 0; i < 100; i++)
            {
                field1[i] = 0;
                field2[i] = 0;
            }
            initShips();
        }
        void initShips()
        {
            shipList = new List<Ship>();
            shipList.Add(new Ship(1));
            shipList.Add(new Ship(1));
            shipList.Add(new Ship(1));
            shipList.Add(new Ship(1));
            shipList.Add(new Ship(2));
            shipList.Add(new Ship(2));
            shipList.Add(new Ship(2));
            shipList.Add(new Ship(3));
            shipList.Add(new Ship(3));
            shipList.Add(new Ship(4));
        }
        public String setShip(object sender)      // try set ship
        {
            int X = 0;
            int Y = 0;
            int n = 0;
            int m = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int k = 0; k < 10; k++)
                {
                    if (((Button)sender).ID == "" + i + k)
                    {
                        X = i;
                        Y = k;
                    }
                }
            }
            if (pickedShip.state == 1)
            {
                n = X - 1 + pickedShip.size + 2;
                m = Y - 1 + 3;
            }
            if (pickedShip.state == 2)
            {
                n = X - 1 + 3;
                m = Y - 1 + pickedShip.size + 2;
            }
            for (int i = X - 1; i < n; i++)
            {
                for (int k = Y - 1; k < m; k++)
                {
                    try
                    {
                        if (i < 0 || k < 0) continue;
                        if (i > 9 || k > 9) continue;
                        if (field1[i*10 +k] == 1) return "u cant place ship here1";
                    }
                    catch { }
                }
            }

            if (pickedShip.state == 2)
            {
                for (int i = 11 - pickedShip.size; i < 10; i++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (((Button)sender).ID == ""+k +i) return "u cant place ship here2";
                    }
                }
            }

            if (pickedShip.state == 1)
            {
                for (int i = 11 - pickedShip.size; i < 10; i++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if (((Button)sender).ID == ""+i +k) return "u cant place ship here3";
                    }
                }
            }

            if (pickedShip.state == 1)
            {
                for (int i = X; i < X + pickedShip.size; i++)
                {
                    field1[i*10+ Y] = 1;
                }
            }

            if (pickedShip.state == 2)
            {
                for (int i = Y; i < Y + pickedShip.size; i++)
                {
                    field1[X*10 +i] = 1;
                }
            }

            foreach (Ship item in shipList)
            {
                if (item.picked)
                {
                    item.picked = false;
                    item.placed = true;
                }
            }

            bool flag = true;
            foreach (Ship item in shipList)
            {
                if (!item.placed)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                putAllShips = true;
                ((UpdatePanel)sck["upd_Ready"]).ContentTemplateContainer.Visible = true;
                ((UpdatePanel)sck["upd_Ready"]).Update();
            }

            bool isSameShip = false;
            foreach (Ship item in shipList)
            {
                if (pickedShip.size == item.size && !item.placed)
                {
                    item.picked = true;
                    pickedShip = item;
                    isSameShip = true;
                    //reDrawShips();
                    break;
                }
            }

            if(!isSameShip) pickedShip = null;
            return "placed";
        }
    }
}

