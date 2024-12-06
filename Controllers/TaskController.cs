using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sirius.Models.Task;
using sirius.Repositories;

namespace sirius.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _repository;
    private readonly IMapper _mapper;

    public TaskController(ITaskRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _repository.GetAllTasks();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(long id)
    {
        var task = await _repository.GetTaskById(id);
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TaskCreateDto taskDto)
    {
        try
        {
            var task = await _repository.CreateTask(taskDto);
            
            var taskView = _mapper.Map<TaskViewDto>(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = taskView.Id }, taskView);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(long id, [FromBody] TaskUpdateDto taskDto)
    {
        try
        {
            var existingTask = await _repository.GetTaskById(id);
            if (existingTask == null) return NotFound();

            var updatedTask = _mapper.Map(taskDto, existingTask);
            var result  = await _repository.UpdateTask(updatedTask);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(long id)
    {
        var result = await _repository.DeleteTask(id);
        if (!result) return NotFound();
        
        return NoContent();
    }
}