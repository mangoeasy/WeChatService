using System;
using System.ComponentModel.DataAnnotations;
using WeChatService.Library.Models.Interfaces;

namespace WeChatService.Library.Models
{
    public class Price : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 价格类型 出厂价 国际价 市场价
        /// </summary>
        public virtual PriceType Type { get; set; }
        /// <summary>
        /// 品规
        /// </summary>
        public virtual ProductStandard ProductStandard { get; set; }
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
        public DateTime PriceDate { get; set; }
        public virtual Site Site { get; set; }
        public string Url { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
