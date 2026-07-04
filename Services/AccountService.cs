using BusinessObject;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void UpdateAccount(Account account)
        {
            iaccountRepository.UpdateAccount(account);
        }
    }
}
