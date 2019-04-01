using System;
using System.Collections.Generic;

namespace MyShop.Core.Domain.Authentication
{
    public class JsonWebToken : BaseEntity
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public long Expires { get; private set; }
        
        // TODO: Add validation!
        public JsonWebToken(Guid id, string accessToken, string refreshToken,
            long expires) : base(id)
        {
            SetAccessToken(accessToken);
            SetRefreshToken(refreshToken);
            SetExpires(expires);
        }

        private void SetAccessToken(string accessToken)
        {
            AccessToken = accessToken;
            SetUpdatedDate();
        }

        public void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
            SetUpdatedDate();
        }

        private void SetExpires(long expires)
        {
            Expires = expires;
            SetUpdatedDate();
        }
    }
}