    using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Elsa.Infrastructure.Repositories.PostgreSQL
{
    public class ConnectionDefinitionRepository : SqlServer.ConnectionDefinitionRepository
    {
        public ConnectionDefinitionRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}