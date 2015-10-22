using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
