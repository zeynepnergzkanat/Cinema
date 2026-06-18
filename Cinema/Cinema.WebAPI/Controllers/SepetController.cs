using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Contracts;
using Cinema.Application.Dtos;

namespace Cinema.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SepetController(ISepetService sepetService) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != userId)
            return Forbid();

        var result = await sepetService.GetByUserIdAsync(userId);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> Ekle(string userId, SepeteEkleRequest request)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != userId)
            return Forbid();

        var result = await sepetService.EkleAsync(userId, request);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok();
    }

    [HttpDelete("temizle/{userId}")]
    public async Task<IActionResult> Temizle(string userId)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != userId)
            return Forbid();

        var result = await sepetService.TemizleAsync(userId);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok();
    }
}