using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrionLibrary.Models
{
    public class Account
    {
        public class Auth
        {
            public string Username;
            public byte[] Salt;
            public byte[] Verifier;
            public string Email;
            public int RegMail;
        }
    }
}
