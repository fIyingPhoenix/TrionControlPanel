using Framework.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrionDatabase
{
    internal class AccountManager
    {
        public enum AccountOpResult
        {
            Ok,
            NameTooLong,
            PassTooLong,
            EmailTooLong,
            NameAlreadyExist,
            NameNotExist,
            DBInternalError,
            BadLink
        }
        const int MaxAccountLength = 16;
        const int MaxEmailLength = 64;

        public static AccountOpResult CreateAccount(string username, string password, string email = "", uint bnetAccountId = 0, byte bnetIndex = 0)
        {
            if (username.Length > MaxAccountLength)
                return AccountOpResult.NameTooLong;

            if (password.Length > MaxAccountLength)
                return AccountOpResult.PassTooLong;

            if (GetId(username) != 0)
                return AccountOpResult.NameAlreadyExist;

            (byte[] salt, byte[] verifier) = SRP6.MakeAccountRegistrationData<GruntSRP6>(username, password);

            return AccountOpResult.Ok;
        }

        private static int GetId(string username)
        {
            throw new NotImplementedException();
        }
    }
}
