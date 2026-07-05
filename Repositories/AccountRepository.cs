using BusinessObject;
using DataAccessLayer;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account? GetAccount(string username)=>AccountDAO.GetAccount(username);

        public List<Account>? GetAllAccount()=>AccountDAO.GetAllAccount();

        public void UpdateAccount(Account account)
        {
            AccountDAO.UpdateAccount(account);
        }
    }
}
