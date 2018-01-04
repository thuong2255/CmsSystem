namespace CmsSystem.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private UserSystemDbContext _dbContext;

        public UserSystemDbContext Init()
        {
            return _dbContext ?? (_dbContext = new UserSystemDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
