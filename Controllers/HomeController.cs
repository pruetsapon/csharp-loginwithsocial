using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Social.Models.Config;
using Social.ViewModels;
using Social.Models;
using Microsoft.Extensions.Options;

namespace Social.Controllers
{
    public class HomeController : Controller
    {
        private Helper _helper;
        private readonly AppConfig _config;
        public HomeController(
            IOptions<AppConfig> config)
        {
            _config = config.Value;
            _helper = new Helper(_config);
        }

        public IActionResult Index()
        {
            ViewData["FacebookAppId"] = _config.FacebookApi.AppId;
            ViewData["GoogleAppId"] = _config.GoogleApi.AppId;
            return View();
        }

        [HttpPost("api/facebook/login")]
        public IActionResult FaceBookLogin([FromBody] FBLoginViewModel loginData)
        {
            var fbClient = _helper.GetFacebookClient();
            var result = fbClient.ValidateAccessToken(loginData.AccessToken).Result;
            if(result != null)
            {
                var token = result.Data;
                if(token.Is_valid && token.Application == _config.FacebookApi.AppName)
                {
                    return Ok(loginData);
                }
            }
            return Unauthorized();
        }

        [HttpPost("api/google/login")]
        public IActionResult GoogleLogin([FromBody] GGLoginViewModel loginData)
        {
            var ggClient = _helper.GetGoogleClient();
            var result = ggClient.ValidateAccessToken(loginData.AccessToken).Result;
            if(result != null)
            {
                if(result.Azp == _config.GoogleApi.AppId)
                {
                    return Ok(loginData);
                }
            }
            return Unauthorized();
        }

    }
}
