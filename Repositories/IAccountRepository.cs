using BusinessObject;

namespace Repositories
{
    public interface IAccountRepository
    {
        public Account? GetAccount(string username);
        public void UpdateAccount(Account account);
        public List<Account>? GetAllAccount();
    }
}
