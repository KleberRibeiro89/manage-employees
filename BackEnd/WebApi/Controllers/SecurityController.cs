using System.Security.Claims;
using BackEnd.AppService.Domain.Registration;
using BackEnd.AppService.Domain.Security;
using BackEnd.AppService.Extensions;
using BackEnd.AppService.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SecurityController : ControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly IRegistrationService _registrationService;
    public SecurityController(
        ISecurityService securityService, IRegistrationService registrationService)
    {
        _securityService = securityService;
        _registrationService = registrationService;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] SignInRequest request)
    {
        try
        {
            var token = await _securityService.SignInAsync(request);

            return Ok(new { Token = token });
        }
        catch (CustomValidatorException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        try
        {
            await _securityService.ForgotPasswordAsync(request);
            return Accepted();

        }
        catch (CustomValidatorException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("create-new-password")]
    public async Task<IActionResult> CreateNewPasswordAsync([FromBody] CreateNewPasswordRequest request)
    {
        try
        {
            await _securityService.CreateNewPasswordRequestAsync(request);
            return Accepted();

        }
        catch (CustomValidatorException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("new-password")]
    public async Task<bool> NewPassword()
    {
        var managerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var result = await _registrationService.NewPasswordAsync(managerId);
        return result;
    }
}
