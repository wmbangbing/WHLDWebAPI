using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class Permission
    {
        [Key]
        [DisplayName("权限Id")]
        public int Id { get; set; }

        public virtual ICollection<RolePmsnRel> RolePmsnRels { get; set; }

    }
}