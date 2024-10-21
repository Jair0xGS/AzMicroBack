using Domain.Errors;
using Domain.Models;
using Domain.Repository.Interfaces;
using ErrorOr;

namespace Domain.Repository.Imp.Redis;

public class WorkoutRepo(RedisConnection cnn):IWorkoutRepo
{
    private string key = "workout";
    private string history_key = "workout_history";
    public Task<List<Workout>> ListForUser(Guid userId)
    {
        return cnn.GetAll<Workout>($"{key}:{userId.ToString()}:*");
    }

    public async Task<ErrorOr<Workout>> View(Guid id)
    {
        var reg=  await cnn.GetFirst<Workout>($"{key}:*:{id.ToString()}");
        if (reg is null) return GErrors.Workout.NotFound;
        return reg;
    }

    public async Task<ErrorOr<Created>> Create(Workout reg)
    {
        await cnn.Write($"{key}:{reg.UserId}:{reg.Id}",reg,null);
        return Result.Created;
    }

    public async Task<ErrorOr<Created>> CreateHistory(WorkoutHistory reg)
    {
        await cnn.Write($"{key}:{reg.UserId}:{reg.WorkoutId}:{reg.Id}",reg,null);
        return Result.Created;
    }

    public async Task<ErrorOr<Updated>> Update(Workout reg)
    {
        await cnn.Write($"{key}:{reg.UserId}:{reg.Id}",reg,null);
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        await cnn.RemoveFirst($"{key}:*:{id.ToString()}");
        return Result.Deleted;
    }
}