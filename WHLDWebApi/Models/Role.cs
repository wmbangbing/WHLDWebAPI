using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class Role
    {
        [Key]
        [DisplayName("角色Id")]
        public int Id { get; set; }

        public string Name { get; set; }

        //public virtual ICollection<Plan> Plans { get; set; }
        public virtual ICollection<UserRoleRel> UserRoleRels { get; set; }
        public virtual ICollection<RolePmsnRel> RolePmsnRels { get; set; }

    }
}