using System;
using WeChatService.Library.Models.Enum;

namespace WeChatService.Web.Models
{
    public class QuotedPriceModels
    {
        public QuotedPriceModel[] Models { get; set; }
        public int CurrentPageIndex { get; set; }
        public int AllPage { get; set; }
        public int PageSize { get; set; }
    }
    /// <summary>
    /// 用户报价
    /// </summary>
    public class QuotedPriceModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 牌号
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public string ProductOfPlace { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDelivery { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 供求
        /// </summary>
        public QuotedType QuotedType { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public Currency Currency { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Contact { get; set; }
        public UserModel UserModel { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
