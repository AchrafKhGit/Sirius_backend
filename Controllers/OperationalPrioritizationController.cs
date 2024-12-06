//using AutoMapper;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Entities;
using sirius.Models.OperationalPrioritization;
using sirius.Repositories.Implementations;

namespace sirius.Controllers;

[Route("api/[controller]s")]
[ApiController]

public class OperationalPrioritizationController : ControllerBase
{
    public readonly IMapper _mapper;
    public readonly IOperationalPrioritizationRepository _repository;

    public OperationalPrioritizationController(IMapper mapper,IOperationalPrioritizationRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrioritizationViewDto>>> GetAllOperationalPrioritizations()
    {
        var prioritizations = _mapper.Map<List<PrioritizationViewDto>>(await _repository.GetAllOperationalPrioritizations());
        return Ok(prioritizations);
    }
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<PrioritizationViewDto>> GetOperationalPrioritizationById(long id)
    {
        var prioritization = _mapper.Map<PrioritizationViewDto>(await _repository.FindOperationalPrioritizationById(id));
        return Ok(prioritization);
    }
    
    [HttpPost]
    public async Task<ActionResult<PrioritizationViewDto>> CreateOperationalPrioritization([FromBody] PrioritizationCreateDto prioCreate)
    {
        if (!ModelState.IsValid) return BadRequest();
        var prio = _mapper.Map<OperationalPrioritization>(prioCreate);
        var createdPrio = await _repository.CreateOperationalPrioritization(prio);
        var createdPrioView = _mapper.Map<PrioritizationViewDto>(createdPrio);
        return new CreatedAtActionResult(nameof(GetOperationalPrioritizationById), "OperationalPrioritization", new { id = prio.Id }, createdPrioView);
    }
    
    [HttpPut]
    public async Task<ActionResult<PrioritizationViewDto>> UpdateOperationalPrioritization([FromBody] PrioritizationUpdateDto prioUpdate)
    {
        if (!ModelState.IsValid) return BadRequest();
        var prio = _mapper.Map<OperationalPrioritization>(prioUpdate);
        var updatedPrio = await _repository.UpdateOperationalPrioritization(prio);
        var updatedPrioView = _mapper.Map<PrioritizationViewDto>(updatedPrio);
        return new CreatedAtActionResult(nameof(GetOperationalPrioritizationById), "OperationalPrioritization", new { id = updatedPrioView.Id }, updatedPrioView);
    }
    
    [HttpPut("deactivate/{id}")]
    public async Task<IActionResult> DeactivateOperationalPrioritization(long id)
    {
        var result = await _repository.DeactivateOperationalPrioritization(id);
        if (!result) return NotFound();

        return NoContent(); 
    }

    [HttpPut("activate/{id}")]
    public async Task<IActionResult> ActivateOperationalPrioritization(long id)
    {
        var result = await _repository.ActivateOperationalPrioritization(id);
        if (!result) return NotFound();

        return NoContent(); 
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOperationalPrioritization(long id)
    {
        var result = await _repository.DeleteOperationalPrioritization(id);
        if (!result) return NotFound();

        return NoContent(); 
    }
}

