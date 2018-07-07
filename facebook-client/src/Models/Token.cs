using System;

namespace Social.Facebook.Model
{
    public class ValidateResult
    {
        public Token Data { get; set; }
    }

    public class Token
    {
        public string App_id { get; set; }
        public string Application { get; set; }
        public bool Is_valid { get; set; }
        public string User_id { get; set; }
        public string Expires_at { get; set; }
    }
}