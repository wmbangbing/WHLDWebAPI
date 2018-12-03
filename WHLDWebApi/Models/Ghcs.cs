using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class Ghcs
    {
        [Key]
        [DisplayName("管护Id")]
        public int GId { get; set; }

        [DisplayName("管护措施")]
        public string Measure { get; set; }

        [DisplayName("说明")]
        public string Desc { get; set; }

        [JsonIgnore]
        public virtual ICollection<PlanGhcsRel> PlanGhcsRels { get; set; }

    }
}