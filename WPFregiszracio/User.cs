using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFregiszracio
{
    internal class User
    {
        public User(string nev, string email, string jelszo)
        {
            Nev = nev;
            Email = email;
            Jelszo = jelszo;
        }

        public string Nev { get; set; }
        public string Email { get; set; }
        public string Jelszo { get; set; }
    }
}
