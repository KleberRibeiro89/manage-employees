using BackEnd.AppService.Domain;
using BackEnd.AppService.Domain.Validator;
using BackEnd.AppService.Models.Requests;
using BackEnd.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    private readonly ISecurityService _securityService;
    private readonly AppDbContext _appDbContex;
    public SecurityController(
        ISecurityService securityService, 
        AppDbContext appDbContex)
    {
        _securityService = securityService;
        _appDbContex = appDbContex;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody]SignInRequest request)
    {
        var validate = new SingInRequestValidator(_appDbContex).Validate(request);
        if (!validate.IsValid)
        {
            return BadRequest(validate.Errors);
        }

        var token = await _securityService.SignInAsync(request);

        return Ok(new { Token = token });
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var validate = new ForgotPasswordValidator(_appDbContex).Validate(request); 
        if (!validate.IsValid)
        {
            return BadRequest(validate.Errors);
        }

        await _securityService.ForgotPasswordAsync(request);
        return Accepted();
    }

    [HttpPut("remember-password")]
    public async Task<IActionResult> CreateNewPasswordAsync([FromBody] CreateNewPasswordRequest request)
    {
        var validate = new CreateNewPasswordValidator(_appDbContex).Validate(request);
        if (!validate.IsValid)
        {
            return BadRequest(validate.Errors);
        }
        await _securityService.CreateNewPasswordRequestAsync(request);
        return Accepted();
    }
}
