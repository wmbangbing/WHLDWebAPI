using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class UserRoleRel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        [DisplayName("用户Id")]
        public int UId { get; set; }

        [ForeignKey("Role")]
        [DisplayName("角色Id")]
        public int RId { get; set; }


        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

    }
}