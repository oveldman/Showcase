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

namespace Web.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly ILogger<ResumeController> _logger;
        private readonly IResumeManager _resumeManager;

        public ResumeController(ILogger<ResumeController> logger, IResumeManager resumeManager)
        {
            _logger = logger;
            _resumeManager = resumeManager;
        }

        [HttpGet]
        public async Task<ResumeViewModel> GetInfoAsync()
        {
            ResumeInfo resumeInfo = await _resumeManager.GetInfoAsync();

            return new() {
                ProfileName = resumeInfo.Name
            };
        }
    }
}