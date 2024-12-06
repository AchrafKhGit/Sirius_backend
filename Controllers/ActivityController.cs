using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Models.Activity;
using sirius.Models.Task;
using sirius.Repositories;

namespace sirius.Controllers;

[ApiController]
[Route("[controller]s")]
public class ActivityController : ControllerBase
{
    private readonly IActivityRepository _repository;
    private readonly IMapper _mapper;

    public ActivityController(IActivityRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllActivities()
    {
        var activities = await _repository.GetAllActivities();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivityById(long id)
    {
        var activity = await _repository.GetActivityById(id);
        return Ok(activity);
    }

    [HttpGet("ByName/{name}")]
    public async Task<IActionResult> GetActivityByName(string name)
    {
        var activity = await _repository.GetActivityByName(name);
        return Ok(activity);
    }

    [HttpGet("tasks/{id}")]
    public async Task<IActionResult> GetActivityTasks(long id)
    {
        var tasks = await _repository.GetTasksByActivityId(id);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> AddActivity([FromBody] ActivityCreateDto activityDto)
    {
        try
        {
            var activity = await _repository.CreateActivity(activityDto);
            
            var activityView = _mapper.Map<ActivityViewDto>(activity);
            return CreatedAtAction(nameof(GetActivityById), new { id = activityView.Id }, activityView);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActivity(long id, [FromBody] ActivityUpdateDto activityDto)
    {
        try
        {
            var existingActivity = await _repository.GetActivityById(id);
            if (existingActivity == null) return NotFound();

            var updatedActivity = _mapper.Map(activityDto, existingActivity);
            var result  = await _repository.UpdateActivity(updatedActivity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(long id)
    {
        var result = await _repository.DeleteActivity(id);
        if (!result) return NotFound();
        
        return NoContent();
    }
}