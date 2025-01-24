using BackEnd.AppService.Domain;
using BackEnd.AppService.Domain.Validator;
using BackEnd.AppService.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly ISecurityService _securityService;
    public SecurityController(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody]SignInRequest request)
    {
        var validate = new SingInRequestValidator().Validate(request);
        if (!validate.IsValid)
        {
            return BadRequest(validate.Errors);
        }

        var token = await _securityService.SignInAsync(request);

        return Ok(token);
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var validate = new ForgotPasswordValidator().Validate(request); 
        if (!validate.IsValid)
        {
            return BadRequest(validate.Errors);
        }

        await _securityService.ForgotPasswordAsync(request);
        return Ok(new { });
    }
}
