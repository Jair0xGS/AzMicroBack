using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class UserCreateCommand
{
    [Required(ErrorMessage = "Nombre requerido.")]
    [MinLength(2, ErrorMessage = "Nombre muy corto.")]
    [StringLength(50, ErrorMessage = "El nombre es muy largo.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Apellido requerido.")]
    [MinLength(2, ErrorMessage = "Apellido muy corto.")]
    [StringLength(50, ErrorMessage = "El apellido es muy largo.")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email requerido.")]
    [MinLength(2, ErrorMessage = "Email muy corto.")]
    [EmailAddress(ErrorMessage = "El correo debe ser valido.")]
    public string Email { get; set; } = "";

    [MinLength(5, ErrorMessage = "Password muy corta.")]
    public string Password { get; set; } = "";
};

public class UserUpdateCommand
{
    [Required(ErrorMessage = "Id requerido.")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nombre requerido.")]
    [MinLength(2, ErrorMessage = "Nombre muy corto.")]
    [StringLength(50, ErrorMessage = "El nombre es muy largo.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Apellido requerido.")]
    [MinLength(2, ErrorMessage = "Apellido muy corto.")]
    [StringLength(50, ErrorMessage = "El apellido es muy largo.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email requerido.")]
    [MinLength(2, ErrorMessage = "Email muy corto.")]
    [EmailAddress(ErrorMessage = "El correo debe ser valido.")]
    public string Email { get; set; } = "";
};
