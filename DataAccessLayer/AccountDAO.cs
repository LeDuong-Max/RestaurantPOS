using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static Account? GetAccount(string username)
        {
            using var db = new RestaurantPosContext();
            return db.Accounts.FirstOrDefault(c => c.Username == username);
        }
    }
}
