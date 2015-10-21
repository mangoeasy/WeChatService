using System.Linq;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Models;

namespace WeChatService.Web.Controllers.API
{
    public class ProvinceController : BaseApiController
    {
        private readonly IProvinceService _provinceService;
        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        public object Get()
        {
            Mapper.Reset();
            Mapper.CreateMap<City, CityModel>();
            Mapper.CreateMap<Province, ProvinceModel>()
                .ForMember(n => n.CityModels, opt => opt.MapFrom(src => src.Cities));
            return _provinceService.GetProvinces().ToArray().Select(Mapper.Map<Province, ProvinceModel>);
        }
	}
}