using Volo.Abp.AspNetCore.Mvc;

namespace Liar.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class LiarController : AbpController
    {
        protected LiarController()
        { 
        }
    }
}