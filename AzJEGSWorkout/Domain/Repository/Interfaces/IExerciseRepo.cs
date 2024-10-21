using Domain.Models;

namespace Domain.Repository.Interfaces;

public interface IExerciseRepo
{
    public Task<List<Exercise>> List();
}