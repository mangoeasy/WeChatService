using System;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Help;
using WeChatService.Web.Infrastructure;
using WeChatService.Web.Infrastructure.Filters;
using WeChatService.Web.Models;
using WeChatService.Library.Models;

namespace WeChatService.Web.Controllers.API
{
    public class UserRegisterController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly ICompanyService _companyService;
        public UserRegisterController(IAccountService accountService, ICompanyService companyService)
        {
            _accountService = accountService;
            _companyService = companyService;
        }
        public object Post(RegisterModel model)
        {
            if (_accountService.GetAccounts().Any(n => n.Phone == model.Phone || (string.IsNullOrEmpty(model.Email) == false && n.Email == model.Email)))
            {
                return Failed("用户重复");
            }

            var id = Guid.NewGuid();
            var str = string.Format("userId={0}&name={1}&portraitUri={2}", id.ToString().Replace("-", string.Empty), model.Name, string.Empty);
            var rongCloudUerModel = RongCloudHelp.HttpPost(str);
            var account = new Account
            {
                Id = id,
                Email = model.Email,
                Phone = model.Phone,
                RongCloudUserToken = rongCloudUerModel != null ? rongCloudUerModel.token : null,
                CreatedTime = DateTime.Now,
                Name = model.Name,
                Password = model.Password,
                Token = Guid.NewGuid(),
                Valid = true
            };
            try
            {
                if (account.RongCloudUserToken == null)
                {
                    return Failed("注册融云用户失败");
                }
                _accountService.Insert(account);
            }
            catch (Exception ex)
            {
                _accountService.Delete(account.Id);
                return Failed(ex.Message);
            }

            Mapper.Reset();
            Mapper.CreateMap<QQInfo, QQInfoModel>();
            Mapper.CreateMap<Company, CompanyModel>()
                .ForMember(n => n.CityId, opt => opt.MapFrom(src => src.City.Id));
            Mapper.CreateMap<Account, UserModel>()
                .ForMember(n => n.CompanyModel, opt => opt.MapFrom(src => src.Company))
                .ForMember(n => n.QQInfoModel, opt => opt.MapFrom(src => src.QQInfo));
            return Mapper.Map<Account, UserModel>(account);
        }
        [CallApiAuthority]
        public object Put(UserModel model)
        {
            var currentUser = HttpContext.Current.User.Identity.GetUser();
            var user = _accountService.GetAccount(currentUser.Id);
            if (user == null) return Failed("can't find user");
            user.Name = model.Name;
            if (!string.IsNullOrEmpty(model.Phone))
            {
                user.Phone = model.Phone;
            }
            if (string.IsNullOrEmpty(user.Email) == false &&
                _accountService.GetAccounts().Count(n => n.Email == model.Email) < 2)
            {
                user.Email = model.Email;
                user.UpdateTime = DateTime.Now;
            }
            if (model.CompanyModel != null)
            {
                var company = _companyService.GetCompany(model.CompanyModel.Id);
                if (company != null)
                {
                    company.Name = model.CompanyModel.Name;
                    company.CityId = model.CompanyModel.CityId;
                }
                else
                {
                    user.Company = new Company
                    {
                        Id = Guid.NewGuid(),
                        Name = model.CompanyModel.Name,
                        CityId = model.CompanyModel.CityId
                    };
                }
                _companyService.Update();
            }
            _accountService.Update();
            return Success();

        }
    }

    public class UserRegisterForQQController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public UserRegisterForQQController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public object Post(QQInfoModel model)
        {
            var id = Guid.NewGuid();
            if (_accountService.GetAccounts().Any(n => n.QQInfo.Userid == model.Userid))
            {
                return Failed("用户重复");
            }
            var str = string.Format("userId={0}&name={1}&portraitUri={2}", id.ToString().Replace("-", string.Empty), model.Nickname, string.Empty);
            var rongCloudUerModel = RongCloudHelp.HttpPost(str);
            var account = new Account
            {
                Id = id,
                RongCloudUserToken = rongCloudUerModel != null ? rongCloudUerModel.token : null,
                CreatedTime = DateTime.Now,
                Name = model.Nickname,
                Token = Guid.NewGuid(),
                Valid = true
            };
            try
            {
                if (account.RongCloudUserToken == null)
                {
                    return Failed("注册融云用户失败");
                }
                _accountService.Insert(account);
            }
            catch (Exception ex)
            {
                _accountService.Delete(account.Id);
                return Failed(ex.Message);
            }

            Mapper.Reset();
            Mapper.CreateMap<QQInfo, QQInfoModel>();
            Mapper.CreateMap<Company, CompanyModel>()
                .ForMember(n => n.CityId, opt => opt.MapFrom(src => src.City.Id));
            Mapper.CreateMap<Account, UserModel>()
                .ForMember(n => n.CompanyModel, opt => opt.MapFrom(src => src.Company))
                .ForMember(n => n.QQInfoModel, opt => opt.MapFrom(src => src.QQInfo));
            return Mapper.Map<Account, UserModel>(account);

        }
    }
    public class UseChangePasswordController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public UseChangePasswordController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [CallApiAuthority]
        public object Put(ChangePasswordViewModel model)
        {
            var currentUser = HttpContext.Current.User.Identity.GetUser();
            var user = _accountService.GetAccounts()
                    .FirstOrDefault(n => n.Id == currentUser.Id);
            if (model!=null && user != null && (string.IsNullOrEmpty(model.NewPassword) == false))
            {
                user.Password = model.NewPassword;
                _accountService.Update();
                return Success();
            }
            return Failed(string.Empty);
        }
    }
    public class UseSignController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public UseSignController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public object Post(SignModel model)
        {
            var user = _accountService.GetAccounts().FirstOrDefault(n => n.Phone == model.Phone || (string.IsNullOrEmpty(model.Email) && n.Email == model.Email));
            if (user != null)
            {
                Mapper.Reset();
                Mapper.CreateMap<QQInfo, QQInfoModel>();
                Mapper.CreateMap<Company, CompanyModel>()
                    .ForMember(n => n.CityId, opt => opt.MapFrom(src => src.City.Id));
                Mapper.CreateMap<Account, UserModel>()
                    .ForMember(n => n.CompanyModel, opt => opt.MapFrom(src => src.Company))
                    .ForMember(n => n.QQInfoModel, opt => opt.MapFrom(src => src.QQInfo));
                return Mapper.Map<Account, UserModel>(user);
            }
            return Failed("Failed");
        }
    }
    public class UseSignForQQController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public UseSignForQQController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public object Post()
        {
            var userid = HttpContext.Current.Request["userid"];
            var account = _accountService.GetAccounts().FirstOrDefault(n => n.QQInfo.Userid == userid);
            if (account != null)
            {
                Mapper.Reset();
                Mapper.CreateMap<QQInfo, QQInfoModel>();
                Mapper.CreateMap<Company, CompanyModel>()
                    .ForMember(n => n.CityId, opt => opt.MapFrom(src => src.City.Id));
                Mapper.CreateMap<Account, UserModel>()
                    .ForMember(n => n.CompanyModel, opt => opt.MapFrom(src => src.Company))
                    .ForMember(n => n.QQInfoModel, opt => opt.MapFrom(src => src.QQInfo));
                return Mapper.Map<Account, UserModel>(account);
            }
            return Failed("Failed");
        }
    }
    public class UserController : BaseApiController
    {
        private readonly IAccountService _accountService;
        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [CallApiAuthority]
        public object Get()
        {
            var phone = HttpContext.Current.Request["phone"];
            var userId = HttpContext.Current.Request["userid"] == null
                ? Guid.Empty
                : new Guid(HttpContext.Current.Request["userid"]);
            var email = HttpContext.Current.Request["email"];
            var name = HttpContext.Current.Request["name"];

            Mapper.Reset();
            Mapper.CreateMap<QQInfo, QQInfoModel>();
            Mapper.CreateMap<Company, CompanyModel>()
                .ForMember(n => n.CityId, opt => opt.MapFrom(src => src.City.Id));
            Mapper.CreateMap<Account, UserModel>()
                .ForMember(n => n.CompanyModel, opt => opt.MapFrom(src => src.Company))
                .ForMember(n => n.QQInfoModel, opt => opt.MapFrom(src => src.QQInfo))
                .ForMember(n => n.Token, opt => opt.Ignore())
                .ForMember(n => n.RongCloudUserToken, opt => opt.Ignore());
            ;
            return _accountService.GetAccounts()
                    .Where(n => n.IsDeleted == false)
                    .Where(n => (string.IsNullOrEmpty(phone) == false && n.Phone == phone) || (string.IsNullOrEmpty(email) == false && n.Email == email) || (string.IsNullOrEmpty(name) == false && n.Name == name) || n.Id == userId).ToArray().Select(Mapper.Map<Account, UserModel>);



        }
    }

}