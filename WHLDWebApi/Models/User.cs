using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WHLDWebApi.Models
{
    public class User
    {
        [Key]
        [DisplayName("用户Id")]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string Remark { get; set; }

        public virtual ICollection<UserRoleRel> UserRoleRels { get; set; }
    }
}