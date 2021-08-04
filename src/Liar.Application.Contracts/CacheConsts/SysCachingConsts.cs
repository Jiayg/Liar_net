using Liar.Domain.Shared.Consts;

namespace Liar.Application.Contracts.CacheConsts
{
    public class SysCachingConsts : CachingConsts
    {
        public const string MenuListCacheKey = "liar:menus:list";

        public const string MenuTreeListCacheKey = "liar:menus:tree";
        public const string MenuRelationCacheKey = "liar:menus:relation";
        public const string MenuCodesCacheKey = "liar:menus:codes";

        public const string DetpListCacheKey = "liar:depts:list";
        public const string DetpTreeListCacheKey = "liar:depts:tree";
        public const string DetpSimpleTreeListCacheKey = "liar:depts:tree:simple";

        public const string RoleListCacheKey = "liar:roles:list";

        public const string UserValidateInfoKeyPrefix = "liar:users:validateinfo";
    }
}
