using sirius.Models.Task;

namespace sirius.Repositories;

public interface ITaskRepository
{
    public Task<IEnumerable<Entities.Task>> GetAllTasks();
    public Task<Entities.Task> GetTaskById(long id);
    public Task<Entities.Task> CreateTask(TaskCreateDto task);
    public Task<Entities.Task> UpdateTask(Entities.Task task);
    public Task<bool> DeleteTask(long id);
}