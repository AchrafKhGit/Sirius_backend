using sirius.Entities;
using sirius.Models.Activity;

namespace sirius.Repositories;

public interface IActivityRepository
{
    public Task<IEnumerable<Activity>> GetAllActivities();
    public Task<Activity> GetActivityById(long id);
    public Task<Activity> GetActivityByName(string name);
    public Task<List<Entities.Task>> GetTasksByActivityId(long id);
    public Task<Activity> CreateActivity(ActivityCreateDto createDto);
    public Task<Activity> UpdateActivity(Activity activity);
    public Task<bool> DeleteActivity(long id);
}