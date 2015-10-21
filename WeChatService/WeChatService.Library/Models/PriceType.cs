using System;
using System.ComponentModel.DataAnnotations;

namespace WeChatService.Library.Models
{
    public class PriceType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
