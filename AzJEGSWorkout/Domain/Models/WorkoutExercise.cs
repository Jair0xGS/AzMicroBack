namespace Domain.Models;

public class WorkoutExercise
{
    public Guid Id { get; set; }
    public Guid ExerciseId { get; set; }
    public string WeightUnit { get; set; } = "";
    private List<ExerciseSet> Sets { get; set; } = [];
}