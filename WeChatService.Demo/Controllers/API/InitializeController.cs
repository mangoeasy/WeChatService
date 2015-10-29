using System.Net;

namespace WeChatService.Demo.Controllers.API
{
    public class InitializeController : BaseApiController
    {
        public object Get()
        {
            //1、初始化WeChatServices项目
            HttpGet("http://wechatservice.mangoeasy.com/api/Initialize/");
            return true;
        }
        private static string HttpGet(string url)
        {
            try
            {
                var myWebClient = new WebClient { Credentials = CredentialCache.DefaultCredentials };
                var pageData = myWebClient.DownloadData(url); //从指定网站下载数据  
                var pageHtml = System.Text.Encoding.Default.GetString(pageData); //如果获取网站页面采用的是GB2312，则使用这句
                return pageHtml;
            }

            catch (WebException webEx)
            {
                return webEx.Message;
            }
        }
    }
}
