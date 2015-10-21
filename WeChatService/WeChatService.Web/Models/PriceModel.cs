using System;

namespace WeChatService.Web.Models
{
    public class PriceModels
    {
        public PriceModel[] Models { get; set; }
        public int CurrentPageIndex { get; set; }
        public int AllPage { get; set; }
        public int PageSize { get; set; }
    }
    public class PriceModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 价格类型 出厂价 国际价 市场价
        /// </summary>
        public string PriceType { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 格规
        /// </summary>
        public string ProductStandard { get; set; }
        /// <summary>
        /// 格规Id
        /// </summary>
        public Guid ProductStandardId { get; set; }
        public decimal? ProductPrice { get; set; }
        public string Unit { get; set; }
        /// <summary>
        /// 价格条件
        /// </summary>
        public string PriceTerm { get; set; }
        /// <summary>
        /// 销售区域
        /// </summary>
        public string SalesArea { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        public string Factory { get; set; }
        public decimal? YesterdayPrice { get; set; }
        public DateTime PriceDate { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}