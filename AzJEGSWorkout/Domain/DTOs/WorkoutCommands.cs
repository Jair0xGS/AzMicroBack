using Domain.Models;

namespace Domain.DTOs;

public class WorkoutCreateCommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = "";
    private List<WorkoutExercise> Exercises { get; set; } = [];
}

public class WorkoutCreateHistoryCommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = "";
    private List<WorkoutExercise> Exercises { get; set; } = [];
}


public class WorkoutUpdateCommand
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = "";
    private List<WorkoutExercise> Exercises { get; set; } = [];
}
