using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;

namespace Liar
{
    [DependsOn(
        typeof(LiarDomainSharedModule), 
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule), 
        typeof(AbpObjectExtendingModule)
    )]
    public class LiarApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            LiarDtoExtensions.Configure();
        }
    }
}
