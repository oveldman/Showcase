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
using Microsoft.Net.Http.Headers;

namespace Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly UserManager<ShowCaseUser> _userManager;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationManager authenicationManager, UserManager<ShowCaseUser> userManager)
        {
            _logger = logger;
            _authenticationManager = authenicationManager;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<BearerViewModel> LoginAsync(LoginViewModel loginRequest) 
        {
            return await _authenticationManager.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<GeneralResponse> ChangePasswordAsync(ChangePasswordViewModel changePasswordRequest) 
        {
            if (!changePasswordRequest.NewPassword?.Equals(changePasswordRequest.ConfirmPassword) ?? true) {
                return new GeneralResponse {
                    ErrorMessages = new() { "The new password and the confirm needs to be equal. " }
                };
            }

            var email = _authenticationManager.GetUsername(Request.Headers[HeaderNames.Authorization]);
            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.ChangePasswordAsync(user, changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);

            return new GeneralResponse {
                Succes = result.Succeeded,
                ErrorMessages = result.Errors.Select(e => e.Description).ToList()
            };
        }

        /*
        [HttpPost]
        [AllowAnonymous]
        [Route("FirstTime")]
        public async Task<GeneralResponse> FirstTime(ChangePasswordViewModel changePasswordRequest) 
        {
            var user = await _userManager.FindByNameAsync("test@test.nl");

            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, changePasswordRequest.NewPassword);

            return new GeneralResponse() {
                Succes = true,
                ErrorMessages = result.Errors.Select( e => e.Description ).ToList()
            };
        }
        */
    }
}