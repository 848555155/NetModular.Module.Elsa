using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;

namespace NetModular.Module.Elsa.Infrastructure.Repositories
{
    public class ElsaDbContext : DbContext
    {
        public ElsaDbContext(IDbContextOptions options) : base(options)
        {
        }
    }
}
