using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.Uow;

namespace MongoDb.Console.MongoDB
{
    [DependsOn(
        // typeof(AbpPermissionManagementMongoDbModule),
        // typeof(AbpSettingManagementMongoDbModule),
        // typeof(AbpIdentityMongoDbModule),
        // typeof(AbpIdentityServerMongoDbModule),
        // typeof(AbpBackgroundJobsMongoDbModule),
        // typeof(AbpAuditLoggingMongoDbModule),
        // typeof(AbpTenantManagementMongoDbModule),
        // typeof(AbpFeatureManagementMongoDbModule),
        typeof(AbpMongoDbModule)
    )]
    public class MongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<MongoDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });
        }   
    }
}