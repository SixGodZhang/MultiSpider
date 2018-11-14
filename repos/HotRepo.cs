using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    [Table("Hot")]
    public class HotRepo : IRepo
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }
        public string MetricsArea { get; set; }
        public int AnswerCount { get; set; }
        public int Score { get; set; }
        public string Link { get; set; }
    }
}
