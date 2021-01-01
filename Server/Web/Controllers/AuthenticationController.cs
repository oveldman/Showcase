using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Manager;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Models.Authentication;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public BearerViewModel Login(LoginViewModel loginRequest) 
        {
            var bearerModel = new BearerViewModel {
                Succes = false
            };

            // TODO: Use the signin manager of Microsoft Owin
            if (loginRequest.Username == "test" && loginRequest.Password == "abc@123")  
            {  
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));  
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);  

                DateTime validUntil = DateTime.Now.AddMinutes(30);
  
                var tokenOptions = new JwtSecurityToken(  
                    issuer: "https://localhost:50001",  
                    audience: "https://localhost:50001",  
                    claims: new List<Claim>(),  
                    expires: validUntil,  
                    signingCredentials: signinCredentials  
                );  
  
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);  

                bearerModel.Succes = true;
                bearerModel.AccessToken = tokenString;
                bearerModel.Type = "Bearer";
                bearerModel.Username = loginRequest.Username;
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