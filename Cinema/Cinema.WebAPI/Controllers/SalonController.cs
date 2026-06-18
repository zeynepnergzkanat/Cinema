using Microsoft.AspNetCore.Authorization;
using Cinema.Application.Contracts;
using Cinema.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace Cinema.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SalonController(ISalonService salonService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await salonService.GetAllAsync();
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateSalonRequest request)
    {
        var result = await salonService.CreateAsync(request);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await salonService.DeleteAsync(id);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok();
    }
}