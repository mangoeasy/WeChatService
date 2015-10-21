using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface ICompanyService : IDisposable
    {
        void Insert(Company company);
        void Update();
        Company GetCompany(Guid id);
        IQueryable<Company> GetCompanies();
    }
}
