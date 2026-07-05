using BusinessObject;
using Repositories;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iaccountRepository;
        public AccountService()
        {
            iaccountRepository = new AccountRepository();
        }
        public Account? GetAccount(string username)
        {
            return iaccountRepository.GetAccount(username);
        }

        public List<Account>? GetAllAccount()=>iaccountRepository.GetAllAccount();

        public void UpdateAccount(Account account)
        {
            iaccountRepository.UpdateAccount(account);
        }
    }
}
