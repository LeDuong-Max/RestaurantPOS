using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static Account? GetAccount(string username)
        {
            using var db = new RestaurantPosContext();
            return db.Accounts
             .Include(a => a.RoleNavigation)
             .FirstOrDefault(a => a.Username == username);
        }
        public static void UpdateAccount(Account account)
        {
            using var db = new RestaurantPosContext();
            db.Accounts.Update(account);
            db.SaveChanges();
        }
        public static List<Account>? GetAllAccount()
        {
            using var db = new RestaurantPosContext();
            return db.Accounts
                     .Include(a => a.RoleNavigation)
                     .ToList();
        }
        public static void CreateAccount(Account newAccount)
        {
            using var db = new RestaurantPosContext();
            db.Accounts.Add(newAccount);
            db.SaveChanges();
        }
        public static bool CheckUsernameExists(string username)
        {
            using var db = new RestaurantPosContext();
            return db.Accounts.Any(a => a.Username.ToLower() == username.ToLower());
        }
    }
}
