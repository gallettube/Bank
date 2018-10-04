using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankService
{
    public static class Extensions
    {
        // updates the user's time to live
        public static void RefreshUser(this Dictionary<Guid, Tuple<string, DateTime>> dict, string userName)
        {
            var guid = dict.Where(e => e.Value.Item1 == userName).FirstOrDefault().Key;
            dict.RefreshUser(guid);
        }

        public static void RefreshUser(this Dictionary<Guid, Tuple<string, DateTime>> dict, Guid guid)
        {
            string userName = dict[guid].Item1;
            dict[guid] = new Tuple<string, DateTime>(userName, DateTime.Now);
        }
    }
}