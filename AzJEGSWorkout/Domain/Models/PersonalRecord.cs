namespace Domain.Models;

public class PersonalRecord
{
    public Guid Id { get; set; }
    public Guid ExerciseSetId { get; set; }
    public string Type { get; set; } = "";// Reps, Vol, Weight
}