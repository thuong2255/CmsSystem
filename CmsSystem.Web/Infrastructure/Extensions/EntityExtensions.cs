using CmsSystem.Model.Models;
using CmsSystem.Web.Models;

namespace CmsSystem.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateAction(this Model.Models.Action action, ActionVm actionVm)
        {
            action.Id = actionVm.Id;
            action.Name = actionVm.Name;
            action.Link = actionVm.Link;
            action.ParentId = actionVm.ParentId;
            action.Description = actionVm.Description;
            action.Display = actionVm.Display;
            action.Position = actionVm.Position;
            action.CreatedBy = actionVm.CreatedBy;
            action.CreatedDate = actionVm.CreatedDate;
            action.Icon = actionVm.Icon;
        }

        public static void UpdateRole(this Role role, RoleVm roleVm)
        {
            role.Id = roleVm.Id;
            role.Name = roleVm.Name;
            role.Description = roleVm.Description;
            role.CreatedDate = roleVm.CreatedDate;
            role.CreatedBy = roleVm.CreatedBy;
        }

        public static void UpdateFunction(this Function function, FunctionVm functionVm)
        {
            function.Id = functionVm.Id;
            function.Code = functionVm.Code;
            function.Description = functionVm.Description;
            function.CreatedBy = functionVm.CreatedBy;
            function.CreatedDate = functionVm.CreatedDate;
        }

        public static void UpdateUser(this User user, UserVm useVm)
        {
            user.Id = useVm.Id;
            user.FullName = useVm.FullName;
            user.UserName = useVm.UserName;
            user.Password = useVm.Password;
            user.Salt = useVm.Salt;
            user.Address = useVm.Address;
            user.Email = useVm.Email;
            user.Mobile = useVm.Mobile;
            user.Description = useVm.Description;
            user.IsAdmin = useVm.IsAdmin;
            user.Status = useVm.Status;
            user.CreatedDate = useVm.CreatedDate;
            user.LastLogin = useVm.LastLogin;
        }
    }
}