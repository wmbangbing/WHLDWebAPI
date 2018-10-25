using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class PlanGhcsRel
    {
        [Key]
        [DisplayName("Id")]
        public int Id { get; set; }

        [ForeignKey("Plan")]
        [DisplayName("计划Id")]
        public int PId { get; set; }

        [ForeignKey("Ghcs")]
        [DisplayName("管护措施Id")]
        public int GId { get; set; }

        public virtual Plan Plan { get; set; }
        public virtual Ghcs Ghcs { get; set; }
    }
}