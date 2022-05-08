using AutoMapper;
using CRMContracts;
using CRMContracts.Email;
using CRMEntities.Models;
using CRMServices.DataTransferObjects;
using CRMWebHost.ActionFilters;
using CRMWebHost.Extensions;
using EmailService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CRMWebHost.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        //private readonly ApplicationSignInManager _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public AuthenticationController(ILoggerManager logger, IMapper mapper, IEmailService emailService, IConfiguration configuration, 
                                       UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
            _emailService = emailService;
            _configuration = configuration;
            //_signInManager = signInManager;
        }
        [HttpPost ("signup")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            user.CreatedOn = DateTime.UtcNow;
            user.IsActive = true;
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return StatusCode(201);
        }
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong username or password.");
                return Unauthorized();
            }
            //var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, true, false);

            //if (!signInResult.Succeeded)
            //{
            //    _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong username or password.");
            //    return Unauthorized();
            //}
            return Ok(new {isSuccess = true,message ="Login successful.", Token = await _authManager.CreateToken() });
        }
        [HttpPost("forgetPassword")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
            if (user == null)
            {
                _logger.LogInfo($"User with Email: {forgotPasswordModel.Email} doesn't exist in the database.");
                return NotFound();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var appDomain = _configuration.GetSection("ApplicationDomain").Value;
            var resetPasswordUrl = $"{appDomain}/auth/reset-password?email={user.Email}&token={WebUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user))}";

            IRecipient recipient = new Recipient(user.UserName, user.Email);
            IEmailModel emailModel = new ResetPasswordEmailModel(resetPasswordUrl, recipient);
            emailModel.TemplateName = "ResetPassword";
            await _emailService.SendEmailAsync(emailModel);
            return Ok(new { isSuccess = true, message = "Password reset Email sent.", resetToken = token });
        }

        [HttpPost("resetPassword")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            var purpose = UserManager<User>.ResetPasswordTokenPurpose;
            if (user == null)
            {
                _logger.LogInfo($"User with Emaail: {resetPasswordModel.Email} doesn't exist in the database.");
                return NotFound();
            }
            if (!await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, purpose, resetPasswordModel.Token))
            {
                return this.BadRequest(new { isSuccess = "false", message = "Link expired" });
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                return BadRequest(new { isSuccess = "false", message = string.Join(Environment.NewLine, resetPassResult.Errors) });
            }
            return Ok(new { isSuccess = true, message = "Password updated." });
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var applicationUser = await _userManager.GetUserAsync(User);
            await _userManager.UpdateSecurityStampAsync(applicationUser);
            return Ok(new { isSuccess = true, message = "Logged out." });
        }
    }
}
