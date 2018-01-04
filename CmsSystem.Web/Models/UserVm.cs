using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CmsSystem.Model.Models;

namespace CmsSystem.Web.Models
{
    public class UserVm
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Description { get; set; }

        public bool IsAdmin { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastLogin { get; set; }

        public virtual IEnumerable<RoleUserVm> RoleUsers { get; set; }
    }
}