using BusinessObject;
using DataAccessLayer;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public bool CheckUsernameExists(string username)
        {
            return AccountDAO.CheckUsernameExists(username);
        }

        public void CreateAccount(Account newAccount)
        {
            AccountDAO.CreateAccount(newAccount);
        }

        public Account? GetAccount(string username)=>AccountDAO.GetAccount(username);

        public List<Account>? GetAllAccount()=>AccountDAO.GetAllAccount();

        public void UpdateAccount(Account account)
        {
            AccountDAO.UpdateAccount(account);
        }
    }
}
