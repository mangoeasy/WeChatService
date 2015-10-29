using System;

namespace WeChatService.Library.Models.Interfaces
{
    public interface IDtStamped
    {
        DateTime? UpdateTime { get; set; }
        DateTime CreatedTime { get; set; }
        bool IsDeleted { get; set; }
    } 
}
