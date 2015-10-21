using System;
using System.ComponentModel.DataAnnotations;

namespace WeChatService.Library.Models
{
    public class PetrochemicalPrice
    {
        [Key]
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
        public virtual Site Site { get; set; }
        public string Url { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
