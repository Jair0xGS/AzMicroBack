namespace Domain.Models;

public class Exercise
{
    public Guid Id { get; set; } 
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> Category { get; set; } = [];
    public List<string> BodyPart { get; set; } = [];
}