namespace Domain.Models;

public class ExerciseSet
{
    public Guid Id { get; set; }
    public int SetNumber { get; set; }
    public float Weight { get; set; }
    public int Reps  { get; set; }
}