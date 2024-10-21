using Domain.DTOs;
using Domain.Models;
using ErrorOr;

namespace Domain.Repository.Interfaces;

public interface IWorkoutRepo
{
    public Task<List<Workout>> ListForUser(Guid userId);
    public Task<ErrorOr<Workout>> View(Guid id);
    public Task<ErrorOr<Created>> Create(Workout reg);
    public Task<ErrorOr<Created>> CreateHistory(WorkoutHistory reg);
    public Task<ErrorOr<Updated>> Update(Workout reg);
    public Task<ErrorOr<Deleted>> Delete(Guid id);
}