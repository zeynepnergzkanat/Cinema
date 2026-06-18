using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Contracts;
using Cinema.Application.Dtos;

namespace Cinema.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BiletController(IBiletService biletService) : ControllerBase
{

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != userId)
            return Forbid();

        var result = await biletService.GetByUserIdAsync(userId);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpPost("satinal/{userId}")]
    public async Task<IActionResult> SatinAl(string userId, OdemeRequest request)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != userId)
            return Forbid();

        var result = await biletService.SatinAlAsync(userId, request);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok();
    }

    [HttpGet("seans/{seansId}")]
    public async Task<IActionResult> GetBySeansId(Guid seansId)
    {
        var result = await biletService.GetBySeansIdAsync(seansId);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }

}