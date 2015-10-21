using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeChatService.Library.Models.Interfaces;

namespace WeChatService.Library.Models
{
    public class Site : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<PetrochemicalPrice> PetrochemicalPrices { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
