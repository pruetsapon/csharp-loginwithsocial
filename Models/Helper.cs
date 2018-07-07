using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social.Models.Config;

namespace Social.Models
{
    public class Helper
    {
        private readonly AppConfig _config;
        public Helper(AppConfig config)
        {
             _config = config;
        }

        public Social.Facebook.Client GetFacebookClient()
        {
            return new Social.Facebook.Client(
                this._config.FacebookApi.BaseUrl,
                this._config.FacebookApi.AppId,
                this._config.FacebookApi.SecretKey
            );
        }

        public Social.Google.Client GetGoogleClient()
        {
            return new Social.Google.Client(
                this._config.GoogleApi.BaseUrl
            );
        }
    }
}
