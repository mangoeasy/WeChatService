using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class CompanyService : BaseService, ICompanyService
    {
        public CompanyService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }

        public void Insert(Company company)
        {
            DbContext.Companies.Add(company);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public Company GetCompany(Guid id)
        {
            return DbContext.Companies.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Company> GetCompanies()
        {
            return DbContext.Companies;
        }
    }
}
