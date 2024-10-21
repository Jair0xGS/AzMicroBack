using ErrorOr;

namespace Domain.Errors;

public partial class GErrors
{
    public static class Workout
    {
      public static Error NotFound => Error.NotFound(
         "Workout.NotFound",
         description: "Ejercicio no encontrado."
      );
    }
}