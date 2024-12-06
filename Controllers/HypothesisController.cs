using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Entities;
using sirius.Models.Hypothesis;
using sirius.Repositories;

namespace sirius.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HypothesisController : ControllerBase
{
    private readonly IHypothesisRepository _repository;
    private readonly IMapper _mapper;

    public HypothesisController(IHypothesisRepository repository , IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHypothesis()
    {
        var hypotheses = await _repository.GetAllHypothesis();
        var hypoView = _mapper.Map<IEnumerable<HypothesisViewDto>>(hypotheses);

        return Ok(hypoView);
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetHypothesisById(long id)
    {
        var hypothesis = await _repository.GetHypothesisById(id);
        if (hypothesis == null)
            return NotFound();

        return Ok(hypothesis);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHypothesis([FromForm] HypothesisCreateDto createDto)
    {
        try
        {
            var hypothesis = await _repository.CreateHypothesis(createDto);
            if (hypothesis.Category == null)
            {
                return BadRequest("Category is missing in the hypothesis entity.");
            }
            
            var hypothesisView = _mapper.Map<HypothesisViewDto>(hypothesis);
            return CreatedAtAction(nameof(GetHypothesisById), new { id = hypothesisView.Id }, hypothesisView);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHypothesis(long id, [FromForm] HypothesisUpdateDto updateDto)
    {
        try
        {
            var existingHypothesis = await _repository.GetHypothesisById(id);
            if (existingHypothesis == null) return NotFound();

            var updatedHypothesis = _mapper.Map(updateDto, existingHypothesis);
            var result  = await _repository.UpdateHypothesis(updatedHypothesis);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteHypothesis(long id)
    {
        var result = await _repository.DeleteHypothesis(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}