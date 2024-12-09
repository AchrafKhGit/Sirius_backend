using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Models.Objectifs;
using sirius.Repositories;

namespace sirius.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class ObjectifsController : ControllerBase
{
    private readonly IObjectifsRepository _repository;
    private readonly IMapper _mapper;

    public ObjectifsController(IObjectifsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllObjectif()
    {
        var objectifs = await _repository.GetAllObjectifs();
        var objectifsView = _mapper.Map<List<ObjectifsViewDto>>(objectifs);
        return Ok(objectifsView);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetObjectifsById(long id)
    {
        var objectifs = await _repository.GetObjectifById(id);
        if (objectifs == null) return NotFound();
        return Ok(objectifs);
    }

    [HttpPost]
    public async Task<IActionResult> CreateObjectif([FromForm] ObjectifsCreateDto objectifsCreateDto)
    {
        try
        {
            var objectif = await _repository.CreateObjectif(objectifsCreateDto);
            
            var objectifView = _mapper.Map<ObjectifsViewDto>(objectif);
            return CreatedAtAction(nameof(GetObjectifsById), new { id = objectifView.Id }, objectifView);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateObjectif(long id, [FromForm] ObjectifsUpdateDto updateDto)
    {
        try
        {
            var existingObjectif = await _repository.GetObjectifById(id);
            if (existingObjectif == null) return NotFound();

            var updatedObjectif = _mapper.Map(updateDto, existingObjectif);
            var result  = await _repository.UpdateObjectif(updatedObjectif);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteObjectif(long id)
    {
        var result = await _repository.DeleteObjectif(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
    
}