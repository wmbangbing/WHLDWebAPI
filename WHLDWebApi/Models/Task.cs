using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class Task
    {
        [Key]
        [DisplayName("任务Id")]
        public int Id { get; set; }

        [DisplayName("任务名称")]
        public string Title { get; set; }

        [DisplayName("任务时间")]
        public string DateTime { get; set; }

        [DisplayName("任务负责人")]
        public string Person { get; set; }

        //public virtual Plan Plan { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }


    }
}