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
    public SecurityController(
        ISecurityService securityService)
    {
        _securityService = securityService;
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
}
