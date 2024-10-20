using ErrorOr;

namespace Domain.Errors;

public partial class GErrors
{
   public static class User
   {
      public static Error UserNotFound => Error.NotFound(
         "User.NotFound",
         description: "Usuario no encontrado."
      );
      
      public static Error InvalidCredentials => Error.Validation(
         "User.InvalidCredentials",
         description: "Credenciales Incorrectas."
      );
      
      public static Error EmailExists => Error.Validation(
         "User.EmailExists",
         description: "Correo ya existe."
      );
   }
}