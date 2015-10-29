using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IAccountService : IDisposable
    {
        void Insert(Account account);
        void Update();
        void Delete(Guid id);
        Account GetAccount(Guid id);
        IQueryable<Account> GetAccounts();
    }
}
