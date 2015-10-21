using System;

namespace WeChatService.Web.Models
{
    /// <summary>
    /// 价格曲线
    /// </summary>
    public class PriceTrendModel
    {
        public DateTime PriceDate { get; set; }
        public decimal? ProductPrice { get; set; }
        public string SalesArea { get; set; }
    }
}
