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
using Web.Managers.Interfaces;

namespace Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationManager authenicationManager)
        {
            _logger = logger;
            _authenticationManager = authenicationManager;
        }

        [HttpPost]
        public async Task<BearerViewModel> LoginAsync(LoginViewModel loginRequest) 
        {
            return await _authenticationManager.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
        }
    }
}