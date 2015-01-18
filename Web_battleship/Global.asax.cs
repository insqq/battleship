using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;

namespace Web_battleship
{
    public class Global : System.Web.HttpApplication
    {
        private static List<Users> _sessionInfo;
        private static readonly object padlock = new object();

        public static List<Users> Sessions
        {
            get
            {
                lock (padlock)
                {
                    if (_sessionInfo == null)
                    {
                        _sessionInfo = new List<Users>();
                    }
                    return _sessionInfo;
                }
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["name"] = "";
            Sessions.Add(new Users("", Session.SessionID, Session));
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            int index = -1;
            foreach (Users item in Sessions)
            {
                if (item.userID == Session.SessionID)
                {
                    index = Sessions.IndexOf(item);
                    break;
                }
            }
            Sessions.RemoveAt(index);
            Session.Clear();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public static Users getUser(HttpSessionState sender)
        {
            foreach (Users item in Sessions)
            {
                if (item.userID.Equals(sender.SessionID))
                {
                    return item;
                }
            }
            return null;
        }

        public static void tryStartGame()
        {
            string userID1 = "-1";
            string userID2 = "-1";
            Users tmp1 = null;
            Users tmp2 = null;
            try
            {
                for (int i = 0; i < Sessions.Count; i++)
                {
                    if (Sessions[i].state == 1 && userID1 == "-1")
                    {
                        userID1 = Sessions[i].userID;
                    }
                    else if (Sessions[i].state == 1 && Sessions[i].userID != userID1)
                    {
                        userID2 = Sessions[i].userID;
                    }
                }
            }
            catch { }

            if (userID1 != "-1" && userID2 != "-1")
            {
                for (int i = 0; i < Sessions.Count; i++)
                {
                    if (Sessions[i].userID == userID1)
                    {
                        Sessions[i].state = 2;
                        tmp1 = Sessions[i];
                    }
                    if (Sessions[i].userID == userID2)
                    {
                        Sessions[i].state = 2;
                        tmp2 = Sessions[i];
                    }
                }
                new Thread(delegate()
                {
                    new GameProccess(tmp1, tmp2);
                }).Start();
            }
        }
    }
}