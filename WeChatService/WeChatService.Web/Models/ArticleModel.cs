using System;

namespace WeChatService.Web.Models
{
    public class ArticleModels
    {
        public ArticleModel[] Models { get; set; }
        public int CurrentPageIndex { get; set; }
        public int AllPage { get; set; }
        public int PageSize { get; set; }
    }
    public class ArticleModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string FromUrl { get; set; }
        public string Site { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int ViewCount { get; set; }
        public bool IsPublish { get; set; }
        /// <summary>
        /// 是否推荐
        /// </summary>
        public bool IsRecommend { get; set; }
        /// <summary>
        /// 是否推送
        /// </summary>
        public bool IsPush { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumbnail { get; set; }
        public DateTime? PublishDate { get; set; }
        /// <summary>
        /// 采集日期
        /// </summary>
        public DateTime? CollectDate { get; set; }
        public Guid ArticleTypeId { get; set; }
        public string ArticleTypeName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}
