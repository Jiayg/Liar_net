using Volo.Abp.Settings;

namespace Liar.Settings
{
    public class LiarSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(LiarSettings.MySetting1));
        }
    }
}
