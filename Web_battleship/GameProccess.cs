using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Web.SessionState;

namespace Web_battleship
{

    public class GameProccess
    {
        static public List<string> chat;
        static Users p1, p2;
        public GameProccess(Users _p1, Users _p2)
        {
            p1 = _p1;
            p2 = _p2;
            chat = new List<string>();

        }

        public void move(Users p)
        {

        }

        UpdatePanel Panel2(Users p)
        {
            return (UpdatePanel)p.sck["Panel2"];
        }
        void upd1()
        {
            ((UpdatePanel)p1.sck["Panel1"]).Update();
            ((UpdatePanel)p1.sck["Panel2"]).Update();
        }
        void upd2()
        {
            ((UpdatePanel)p2.sck["Panel1"]).Update();
            ((UpdatePanel)p2.sck["Panel2"]).Update();
        }

        static public Users getP2(HttpSessionState _sck)
        {
            if(Global.getUser(_sck) == p1)
            {
                return Global.getUser(p2.sck);
            }

            if (Global.getUser(_sck) == p2)
            {
                return Global.getUser(p1.sck);
            }
            return null;
        }
        static public void firstHit(HttpSessionState _sck)
        {
            if (Global.getUser(_sck) == p1)
            {
                Global.getUser(_sck).myMove = true;
                return;
            }

            Global.getUser(_sck).myMove = false;
        }
        static public int makeHit(HttpSessionState _sck, int hit)
        {
            if (getP2(_sck).field1[hit] == 0)
            {
                getP2(_sck).field1[hit] = 2;
                Global.getUser(_sck).field2[hit] = 2;
                getP2(_sck).myMove = true;
                Global.getUser(_sck).myMove = false;
                return 0;
            }
            if (getP2(_sck).field1[hit] == 1)
            {
                getP2(_sck).field1[hit] = 3;
                Global.getUser(_sck).field2[hit] = 3;
                if (checkWin(_sck))
                {
                    getP2(_sck).state = 4;
                    Global.getUser(_sck).state = 5;
                    return 5;
                }
                return 1;
            }

            return 2;
        }

        static bool checkWin(HttpSessionState _sck)
        {
            foreach (byte item in getP2(_sck).field1)
            {
                if (item == 1) return false;
            }
            return true;
        }
    }
}