﻿/*
    The MIT License (MIT)

    Copyright (c) 2014 Mehmetali Shaqiri (mehmetalishaqiri@gmail.com)

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE. 
 */


using System;
using System.Security.Claims;

namespace openvpn.api.core.auth
{
    public class ExternalLoginModel
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }
        public string ExternalAccessToken { get; set; }

        public string Picture { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Profile { get; set; }

        public static ExternalLoginModel FromIdentity(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }


            var loginProvider = providerKeyClaim.Issuer;
            var providerKey = providerKeyClaim.Value;
            var email = identity.FindFirst("Email") != null ? identity.FindFirst("Email").Value : String.Empty;
            var token = identity.FindFirst("ExternalAccessToken") != null
                ? identity.FindFirst("ExternalAccessToken").Value
                : String.Empty;

            var name = identity.FindFirst("Name") != null ? identity.FindFirst("Name").Value : String.Empty;

            var picture = identity.FindFirst("Picture") != null ? identity.FindFirst("Picture").Value : String.Empty;

            var profile = identity.FindFirst("Profile") != null ? identity.FindFirst("Profile").Value : String.Empty;

            return new ExternalLoginModel
            {
                LoginProvider = loginProvider,
                ProviderKey = providerKey,
                UserName = email,
                ExternalAccessToken = token,
                Name = name,
                Email = email,
                Picture = picture,
                Profile = profile
            };
        }
    }
}
