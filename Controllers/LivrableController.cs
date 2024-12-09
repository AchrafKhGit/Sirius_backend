using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Models.Livrable;
using sirius.Repositories;

namespace sirius.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class LivrableController : ControllerBase
{
    private readonly ILivrableRepository _repository;
    private readonly IMapper _mapper;

    public LivrableController(ILivrableRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLivrables()
    {
        var livrables = await _repository.GetAllLivrables();
        var livrablesView = _mapper.Map<List<LivrableViewDto>>(livrables);
        return Ok(livrablesView);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLivrableById(long id)
    {
        var livrable = await _repository.GetLivrableById(id);
        if (livrable == null) return NotFound();
        return Ok(livrable);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLivrable([FromForm] LivrableCreateDto createDto)
    {
        try
        {
            var livrable = await _repository.CreateLivrable(createDto);
            
            var livrableView = _mapper.Map<LivrableViewDto>(livrable);
            return CreatedAtAction(nameof(GetLivrableById), new { id = livrableView.Id }, livrableView);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLivrable(long id, [FromBody] LivrableUpdateDto updateDto)
    {
        try
        {
            var existingLivrable = await _repository.GetLivrableById(id);
            if (existingLivrable == null) return NotFound();

            var updatedLivrable = _mapper.Map(updateDto, existingLivrable);
            var result  = await _repository.UpdateLivrable(updatedLivrable);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLivrable(long id)
    {
        var result = await _repository.DeleteLivrable(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
    
}