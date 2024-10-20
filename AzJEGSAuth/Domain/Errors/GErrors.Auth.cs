using ErrorOr;

namespace Domain.Errors;

public partial class GErrors
{
    public static class Auth
    {
      public static Error InvalidToken => Error.Validation(
         "Auth.InvalidToken",
         description: "Token invalido."
      );
      public static Error CouldNotDecodeClaims => Error.Validation(
         "Auth.CouldNotDecodeClaims ",
         description: "Token decodificado incorrectamente."
      );
    }
}