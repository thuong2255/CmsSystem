using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CmsSystem.Model.Models;

namespace CmsSystem.Web.Models
{
    public class FunctionRoleVm
    {

        public int RoleId { get; set; }

        public int FunctionId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public virtual RoleVm Role { get; set; }

        public virtual FunctionVm Function { get; set; }
    }
}