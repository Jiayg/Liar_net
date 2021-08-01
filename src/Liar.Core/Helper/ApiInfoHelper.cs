using Liar.Core.Consts;
using Liar.Core.Models;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Liar.Core.Helper
{
    public static class ApiInfoHelper
    {
        public static readonly List<SwaggerApiInfo> ApiInfos = new List<SwaggerApiInfo>()
        {
            new SwaggerApiInfo
            {
                UrlPrefix = LiarApiVersionConsts.v1,
                Name = "Liar Api v1",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "Liar Api v1",
                    Description = "Liar Api Document!"
                }
            },
             new SwaggerApiInfo
            {
                UrlPrefix = LiarApiVersionConsts.v2,
                Name = "Liar Api v2",
                OpenApiInfo = new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "Liar Api v2",
                    Description = "Liar Api Document!"
                }
            }
        };
    }
}
