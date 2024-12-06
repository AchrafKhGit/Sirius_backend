using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Models.HypothesisHistory;
using sirius.Repositories;

namespace sirius.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HypothesisHistoryController : ControllerBase
{
    private readonly IHypothesisHistoryRepository _repository;
    private readonly IMapper _mapper;

    public HypothesisHistoryController(IHypothesisHistoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHistories()
    {
        var histories = await _repository.GetAllHypothesisHistories();
        return Ok(_mapper.Map<IEnumerable<HistoryViewDto>>(histories));
    }

    [HttpGet("hypothesis/{hypothesisId}")]
    public async Task<IActionResult> GetHistoryByHypothesisId(int hypothesisId)
    {
        var histories = await _repository.GetHypothesisHistoriesByHypothesisId(hypothesisId);
        return Ok(_mapper.Map<IEnumerable<HistoryViewDto>>(histories));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHistoryById(int id)
    {
        var history = await _repository.GetHypothesisHistoryById(id);
        if (history == null)
            return NotFound();
                
        return Ok(history);
    }
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteHistory(long id)
    {
        var result = await _repository.DeleteHypothesisHistory(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}