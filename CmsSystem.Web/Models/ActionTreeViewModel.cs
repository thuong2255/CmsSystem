using System;
using System.Collections.Generic;

namespace CmsSystem.Web.Models
{
    public class ActionTreeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public List<RightAction> Actions { get; set; }
    }
}