using System;
using System.Collections.Generic;

namespace MyShop.Core.Domain.Authentication
{
    public class JsonWebToken : BaseEntity, IIdentifiable
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public long Expires { get; private set; }
        public IDictionary<string, string> Claims { get; private set; }
        
        // TODO: Add validation!
        public JsonWebToken(Guid id, string accessToken, string refreshToken,
            long expires, IDictionary<string, string> claims) : base(id)
        {
            SetAccessToken(accessToken);
            SetRefreshToken(refreshToken);
            SetExpires(expires);
            SetClaims(claims);
        }

        private void SetAccessToken(string accessToken)
        {
            AccessToken = accessToken;
            SetUpdatedDate();
        }

        private void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
            SetUpdatedDate();
        }

        private void SetExpires(long expires)
        {
            Expires = expires;
            SetUpdatedDate();
        }

        private void SetClaims(IDictionary<string, string> claims)
        {
            Claims = claims;
            SetUpdatedDate();
        }
    }
}