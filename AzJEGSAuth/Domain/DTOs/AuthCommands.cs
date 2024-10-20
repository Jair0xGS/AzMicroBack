using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class LoginCommand
{
    [Required(ErrorMessage = "Tipo autenticacion requerido.")]
    public string Type { get; set; }
    [Required(ErrorMessage = "Access requerido.")]
    public string Access { get; set; }
    [Required(ErrorMessage = "Secret requerido.")]
    public string Secret { get; set; }
};

public class LogoutCommand
{
    [Required(ErrorMessage = "Access requerido.")]
    public string Access { get; set; }
}

public class RefreshCommand
{
    [Required(ErrorMessage = "Refresh requerido.")]
    public string Refresh { get; set; }
}
