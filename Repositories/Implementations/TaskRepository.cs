using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Models.Task;
using Task = sirius.Entities.Task;

namespace sirius.Repositories.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly SiriusDbContext _db;

    public TaskRepository(SiriusDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Task>> GetAllTasks()
    {
        var tasks = await _db.Tasks.ToListAsync();
        return tasks;
    }

    public async Task<Task> GetTaskById(long id)
    {
        var task = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        return task;
    }

    public async Task<Task> CreateTask(TaskCreateDto createDto)
    {
        var task = new Task
        {
            Name = createDto.Name,
            Description = createDto.Description,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
            Budget = createDto.Budget,
            Effort = createDto.Effort,
            ActivityId = createDto.ActivityId
        };
        await _db.Tasks.AddAsync(task);
        await _db.SaveChangesAsync();
        return task;

    }

    public async Task<Task> UpdateTask(Task task)
    {
        var existingTask = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == task.Id);
        if (existingTask == null)
            throw new Exception("Task not found");
        
        _db.Entry(existingTask).CurrentValues.SetValues(task);
        await _db.SaveChangesAsync();
        
        return existingTask;
    }

    public async Task<bool> DeleteTask(long id)
    {
        var existingTask = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingTask == null) return false;
        
        _db.Tasks.Remove(existingTask);
        await _db.SaveChangesAsync();
        return true;
    }
}