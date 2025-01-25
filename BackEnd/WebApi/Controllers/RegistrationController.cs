using System.Security.Claims;
using BackEnd.AppService.Domain.Registration;
using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.AppService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService _registrationService;
    public RegistrationController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost("employee")]
    public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeRequest request)
    {
        try
        {
            request.ManagerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _registrationService.CreateAsync(request);
            return Created();
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

    [HttpPut("employee")]
    public async Task<IActionResult> AlterEmployee([FromBody] UpdateEmployeeRequest request)
    {
        try
        {
            request.ManagerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _registrationService.UpdateAsync(request);

            return Created()

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
