using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class RolePmsnRel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Role")]
        [DisplayName("角色Id")]
        public int RId { get; set; }

        [ForeignKey("Permission")]
        [DisplayName("权限Id")]
        public int PId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}