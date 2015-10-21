using System;
using System.ComponentModel.DataAnnotations;

namespace WeChatService.Library.Models
{
    public class QQInfo
    {
        [Key]
        public Guid Id { get; set; }
        public string Userid { get; set; }
        public string City { get; set; }
        public string Figureurl { get; set; }
        public string Figureurl1 { get; set; }
        public string Figureurl2 { get; set; }
        public string FigureurlQQ1 { get; set; }
        public string FigureurlQQ2 { get; set; }
        public string Gender { get; set; }
        public string IsLost { get; set; }
        public string IsYellowVip { get; set; }
        public string IsYellowYearVip { get; set; }
        public string Level { get; set; }
        public string Msg { get; set; }
        public string Nickname { get; set; }
        public string Province { get; set; }
        public string Ret { get; set; }
        public string Vip { get; set; }
        public string Year { get; set; }
        public string YellowVipLevel { get; set; }
        public string AutomaticPassword { get; set; }
    }
}
