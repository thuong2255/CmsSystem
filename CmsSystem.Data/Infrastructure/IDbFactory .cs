using System;

namespace CmsSystem.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        UserSystemDbContext Init();
    }
}
