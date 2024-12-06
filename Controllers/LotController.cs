using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Models.Activity;
using sirius.Models.Lot;
using sirius.Repositories;
using Siruis_backend.Repositories.Implementations;

namespace sirius.Controllers;

[ApiController]
[Route("[controller]s")]
public class LotController : ControllerBase
{
     private readonly ILotRepository _repository;
    private readonly IMapper _mapper;

    public LotController(ILotRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLots()
    {
        var lots = await _repository.GetAllLots();
        return Ok(lots);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLotById(long id)
    {
        var lot = await _repository.GetLotById(id);
        return Ok(lot);
    }

    [HttpGet("ByName/{name}")]
    public async Task<IActionResult> GetLotByName(string name)
    {
        var lot = await _repository.GetLotByName(name);
        return Ok(lot);
    }

    [HttpGet("activities/{id}")]
    public async Task<IActionResult> GetLotActivities(long id)
    {
        var activities = await _repository.GetActivitiesByLotId(id);
        return Ok(activities);
    }

    [HttpPost]
    public async Task<IActionResult> AddLot([FromBody] LotCreateDto lotDto)
    {
        try
        {
            var lot = await _repository.CreateLot(lotDto);
            
            var lotView = _mapper.Map<LotViewDto>(lot);
            return CreatedAtAction(nameof(GetLotById), new { id = lotView.Id }, lotView);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLot(long id, [FromBody] LotUpdateDto lotDto)
    {
        try
        {
            var existingLot = await _repository.GetLotById(id);
            if (existingLot == null) return NotFound();

            var updatedLot = _mapper.Map(lotDto, existingLot);
            var result  = await _repository.UpdateLot(updatedLot);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLot(long id)
    {
        var result = await _repository.DeleteLot(id);
        if (!result) return NotFound();
        
        return NoContent();
    }
}