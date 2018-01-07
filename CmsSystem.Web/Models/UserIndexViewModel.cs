using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CmsSystem.Web.Models
{
    public class UserIndexViewModel
    {
        public IPagedList<UserVm> UsersVm { get; set; }

        public string Search { get; set; }
    }
}