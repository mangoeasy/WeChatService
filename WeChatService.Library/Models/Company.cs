using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeChatService.Library.Models.Interfaces;

namespace WeChatService.Library.Models
{
    public class Company : IDtStamped
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
