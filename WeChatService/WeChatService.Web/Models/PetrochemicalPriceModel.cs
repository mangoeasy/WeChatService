using System;

namespace WeChatService.Web.Models
{
    public class PetrochemicalPriceModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 报价
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 销售区域
        /// </summary>
        public string SalesArea { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        public string Factory { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 牌号
        /// </summary>
        public string ProductStandard { get; set; }
        public DateTime CreatedTime { get; set; }
      
    }
}