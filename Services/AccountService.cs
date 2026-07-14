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

        public void CreateAccount(Account newAccount)
        {
            newAccount.Status = 1;
            bool isExist = iaccountRepository.CheckUsernameExists(newAccount.Username);

            if (isExist == true)
            {
                throw new Exception("Username này đã tồn tại! Vui lòng chọn tên khác.");
            }
            iaccountRepository.CreateAccount(newAccount);
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
