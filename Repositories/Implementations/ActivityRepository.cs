using Microsoft.EntityFrameworkCore;
using sirius.Configurations;
using sirius.Entities;
using Activity = sirius.Entities.Activity;
using sirius.Models.Activity;

namespace sirius.Repositories.Implementations;

public class ActivityRepository : IActivityRepository
{
    private readonly SiriusDbContext _db;

    public ActivityRepository(SiriusDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Activity>> GetAllActivities()
    {
        var activities = await _db.Activities.ToListAsync();
        return activities;
    }

    public async Task<Activity> GetActivityById(long id)
    {
        var activity = await _db.Activities.FirstOrDefaultAsync(x => x.Id == id);
        return activity;
    }

    public async Task<Activity> GetActivityByName(string name)
    {
        var activity = await _db.Activities.FirstOrDefaultAsync(x => x.Name == name);
        return activity;
    }

    public async Task<List<Entities.Task>> GetTasksByActivityId(long id)
    {
        var tasks = await _db.Tasks.Where(x => x.ActivityId == id).ToListAsync();
        return tasks;
    }

    public async Task<Activity> CreateActivity(ActivityCreateDto createDto)
    {
        var activity = new Activity()
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Budget = createDto.Budget,
            Effort = createDto.Effort,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
            LotId = createDto.LotId
        };
        
        await _db.Activities.AddAsync(activity);
        await _db.SaveChangesAsync();
        return activity;
    }

    public async Task<Activity> UpdateActivity(Activity activity)
    {
        var existingActivity = await _db.Activities.FirstOrDefaultAsync(x => x.Id == activity.Id);
        if (existingActivity == null)
            throw new Exception("Activity not found");
        
        _db.Entry(existingActivity).CurrentValues.SetValues(activity);
        await _db.SaveChangesAsync();
        
        return existingActivity;
    }

    public async Task<bool> DeleteActivity(long id)
    {
        var existingActivity = await _db.Activities.FirstOrDefaultAsync(x => x.Id == id);
        if (existingActivity == null) return false;
        
        _db.Activities.Remove(existingActivity);
        await _db.SaveChangesAsync();
        return true;
    }
}