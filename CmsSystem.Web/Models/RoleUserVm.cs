using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CmsSystem.Model.Models;

namespace CmsSystem.Web.Models
{
    public class RoleUserVm
    {

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual UserVm User { get; set; }

        public virtual RoleVm Role { get; set; }
    }
}