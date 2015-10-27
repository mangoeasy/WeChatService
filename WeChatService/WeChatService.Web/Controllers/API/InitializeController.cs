using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeChatService.Library.Services;

namespace WeChatService.Web.Controllers.API
{
    public class InitializeController : BaseApiController
    {
        private readonly IProvinceService _provinceService;
        public InitializeController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }
        public object Get()
        {
            //1、初始化本项目
            var p = _provinceService.GetProvinces().FirstOrDefault();
            return true;
        }
    }
}
