using Elsa.Models;
using NetModular.Lib.Data.Abstractions.SqlQueryable;
using NetModular.Module.Elsa.Domain.WorkflowDefinitionVersion;

namespace NetModular.Module.Elsa.Infrastructure.Extensions
{
    public static class WorkflowDefinitionVersionExtension
    {
        public static INetSqlQueryable<WorkflowDefinitionVersionEntity> WithVersion(
            this INetSqlQueryable<WorkflowDefinitionVersionEntity> query,
            VersionOptions version)
        {
            return query.WhereIf(version.IsDraft, a => a.IsPublished == false)
                .WhereIf(version.IsLatest, a => a.IsLatest == true)
                .WhereIf(version.IsPublished, a => a.IsPublished == true)
                .WhereIf(version.IsLatestOrPublished, a => a.IsPublished == true || a.IsLatest == true)
                .WhereIf(version.Version > 0, a => a.Version == version.Version)
                .OrderByDescending(a => a.Version);
        }
    }
}
