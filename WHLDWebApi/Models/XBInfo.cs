using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class XBInfo
    {
        [Key]
        [DisplayName("任务Id")]
        public int Id { get; set; }

        [DisplayName("小班面积")]
        public double Area { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }

    }
}