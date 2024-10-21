using System.Text.Json;
using Domain.Models;
using Domain.Repository.Interfaces;

namespace Domain.Repository.Imp.Redis;

public class ExerciseRepo(RedisConnection cnn):IExerciseRepo
{

    private string key = "exercise";
    
    public Task<List<Exercise>> List()
    {
        return cnn.GetAll<Exercise>(key);
    }
}