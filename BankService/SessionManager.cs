using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace BankService
{
    public class SessionManager
    {
        public Dictionary<Guid, Tuple<string, DateTime>> ActiveUsers { get; set; }
        private static volatile SessionManager instance;
        private static object syncRoot = new Object();
        private static Timer timer;
        private static int TimeoutMinutes = 10;

        private SessionManager()
        {
            int n = 1;
            ActiveUsers = new Dictionary<Guid, Tuple<string, DateTime>>();
            //timer = new Timer(n * 60000); 
            //timer.Elapsed += timer_Elapsed;
            //timer.Enabled = true;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var item in ActiveUsers)
            {
                if ((DateTime.Now - item.Value.Item2).Minutes > TimeoutMinutes)
                {
                    ActiveUsers.Remove(item.Key);
                }
            }
        }

        public static SessionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SessionManager();
                        }
                    }
                }
                return instance;
            }
        }

        public Guid AddUser(string userName)
        {
            lock (syncRoot)
            {
                if (ActiveUsers.Count(entry => entry.Value.Item1 == userName) == 0)
                {
                    Guid guid = Guid.NewGuid();
                    ActiveUsers.Add(guid, new Tuple<string, DateTime>(userName, DateTime.Now));
                    return guid;
                }
                else
                {
                    ActiveUsers.RefreshUser(userName);
                    return ActiveUsers.Where(entry => entry.Value.Item1 == userName).First().Key;
                }
            }
        }

        public bool ValidateUser(Guid guid)
        {
            bool userIsValid = false;
            lock (syncRoot)
            {
                if (ActiveUsers.ContainsKey(guid))
                {
                    userIsValid = true;
                    ActiveUsers.RefreshUser(guid);
                }
            }
            return userIsValid;
        }

        public string GetUserNameByGuid(Guid guid)
        {
            return ActiveUsers.Where(entry => entry.Key == guid).FirstOrDefault().Value.Item1;
        }
    }
}