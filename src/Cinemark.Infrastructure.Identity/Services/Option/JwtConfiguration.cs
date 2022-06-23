﻿namespace Cinemark.Infrastructure.Identity.Services.Option
{
    public class JwtConfiguration
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Expires { get; set; }
    }
}