using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repos
{
    [Table("Hot")]
    public class HotRepo : IRepo
    {
        [Key]
        public int ID { get; set; }
        [Column("标题")]
        public string Title { get; set; }
        [Column("关注度")]
        public string MetricsArea { get; set; }
        [Column("回答数目")]
        public int AnswerCount { get; set; }
        [Column("Score")]
        public int Score { get; set; }
        [Column("链接")]
        public string Link { get; set; }
        [Column("数据生成时间")]
        public DateTime Date { get; set; }
    }
}
