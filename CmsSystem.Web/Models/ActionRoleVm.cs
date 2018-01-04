using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CmsSystem.Model.Models;
using Action = CmsSystem.Model.Models.Action;

namespace CmsSystem.Web.Models
{
    public class ActionRoleVm
    {

        public int RoleId { get; set; }

        public int ActionId { get; set; }


        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual ActionVm Action { get; set; }

        public virtual RoleVm Role { get; set; }
    }
}