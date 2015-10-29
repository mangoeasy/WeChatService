using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeChatService.Library.Models
{
    public class Province
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; } 
    }

    public class City
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual Province Province { get; set; }
    }
}
