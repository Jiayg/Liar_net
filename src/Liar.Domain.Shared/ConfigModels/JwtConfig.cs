using System;
using System.Collections.Generic;
using System.Text;

namespace Liar.Domain.Shared
{
    public class JwtConfig
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public string TokenTime { get; set; }


        /// <summary>
        /// AccessToken过期时间，单位分钟
        /// </summary>
        public int Expire { get; set; }

        /// <summary>
        /// RefreshToken受众
        /// </summary>
        public string RefreshTokenAudience { get; set; }

        /// <summary>
        /// RefreshToken过期时间，单位分钟
        /// </summary>
        public int RefreshTokenExpire { get; set; }
    }
}
