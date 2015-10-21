using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeChatService.Library.Models
{
    public class ArticleType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ArticleType Paren { get; set; }
        public virtual ICollection<Article> Articles { get; set; } 
    }
}
