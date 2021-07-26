using Liar.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Liar.Permissions
{
    public class LiarPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(LiarPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(LiarPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<LiarResource>(name);
        }
    }
}
