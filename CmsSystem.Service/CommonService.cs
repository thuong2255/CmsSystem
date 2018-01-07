using CmsSystem.Common.Models;
using System.Collections.Generic;

namespace CmsSystem.Service
{
    public interface ICommonService
    {
        IEnumerable<StatusModel<bool>> GetStatusUser();
    }

    public class CommonService : ICommonService
    {
        public IEnumerable<StatusModel<bool>> GetStatusUser()
        {
            var listStatus = new List<StatusModel<bool>>
            {
                new StatusModel<bool> {Name = "Hoạt động", Value = true },
                new StatusModel<bool> {Name = "Khóa", Value = false }
            };
            return listStatus;
        }
    }
}