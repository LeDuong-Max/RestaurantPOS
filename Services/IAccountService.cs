using BusinessObject;

namespace Services
{
    public interface IAccountService
    {
        Account? GetAccount(string username);
        void UpdateAccount(Account account);
        List<Account>? GetAllAccount();
    }
}
