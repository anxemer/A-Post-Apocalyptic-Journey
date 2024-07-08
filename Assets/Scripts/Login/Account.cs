using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Login
{
    [System.Serializable]
    public class Account
    {
        public string UserName { get; set; }
        public int Password { get; set; }
        public int id { get; set; }

        public Account(string username, int password, int id)
        {
            UserName = username;
            Password = password;
            this.id = id;
        }
    }
}
