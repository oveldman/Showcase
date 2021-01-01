using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Web.Managers.Interfaces;
using Web.Models.Authentication;

namespace Web.Managers
{
    public class ShowCaseAuthenticationManager : IAuthenticationManager
    {
        private readonly string _issuer;
        private readonly SymmetricSecurityKey _securityKey;

        public ShowCaseAuthenticationManager(string issuer, string key) {
            _issuer = issuer;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));  
        }

        public BearerViewModel Authenticate(string username, string password)
        {
            var bearerModel = new BearerViewModel {
                Succes = false
            };

            // TODO: Use the signin manager of Microsoft Owin
            if (username == "test" && password == "abc@123")  
            {  
                var signinCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);  

                DateTime validUntil = DateTime.Now.AddMinutes(30);
  
                var tokenOptions = new JwtSecurityToken(  
                    issuer: _issuer,  
                    audience: _issuer,  
                    claims: new List<Claim>(),  
                    expires: validUntil,  
                    signingCredentials: signinCredentials  
                );  
  
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);  

                bearerModel.Succes = true;
                bearerModel.AccessToken = tokenString;
                bearerModel.Type = "Bearer";
                bearerModel.Username = username;
                bearerModel.Issued = DateTime.Now.ToString();
                bearerModel.Expires = validUntil.ToString();

                // Ticks  divide by 10000000 makes time in seconds. 
                bearerModel.ExpiresIn = ((validUntil.Ticks - DateTime.Now.Ticks) / 10000000).ToString();
            }
            else {
                bearerModel.ErrorMessage = "Username and/or password isn't correct.";
            }  

            return bearerModel;
        }
    }
}
