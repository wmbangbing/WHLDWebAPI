using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class Plan
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

        [ForeignKey("Task")]
        [DisplayName("任务编号")]
        public int TId { get; set; }

        [ForeignKey("XBInfo")]
        [DisplayName("小班编号")]
        public int XBId { get; set; }

        public virtual Task Task { get; set; }
        public virtual XBInfo XBInfo { get; set; }

        public virtual ICollection<PlanGhcsRel> PlanGhcsRels { get; set; }


        //public virtual ICollection<Task> Tasks { get; set; }
    }
}