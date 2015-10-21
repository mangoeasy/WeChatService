using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeChatService.Library.Models.Interfaces;

namespace WeChatService.Library.Models
{
    public class Article : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ViewCount { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string FromUrl { get; set; }
        public Guid? SiteId { get; set; }
        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }
        public Guid? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual Account Author { get; set; }
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
        [ForeignKey("ArticleTypeId")]
        public virtual ArticleType ArticleType { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
