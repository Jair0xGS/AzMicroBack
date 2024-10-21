namespace Domain.Models;

public class WorkoutHistory
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid WorkoutId { get; set; }
    public long DurationSeconds { get; set; }
    public DateTime CreatedAt { get; set; }
    private List<WorkoutExercise> Exercises { get; set; } = [];
}