using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Contracts;
using Cinema.Application.Dtos;
namespace Cinema.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SeansController(ISeansService seansService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await seansService.GetAllAsync();
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await seansService.GetByIdAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);
        return Ok(result.Data);
    }
    [HttpGet("film/{filmId}")]
    public async Task<IActionResult> GetByFilmId(Guid filmId)
    {
        var result = await seansService.GetByFilmIdAsync(filmId);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateSeansRequest request)
    {
        var result = await seansService.CreateAsync(request);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok(result.Data);
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await seansService.DeleteAsync(id);
        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);
        return Ok();
    }
}