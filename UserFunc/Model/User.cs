namespace UserFunc.Model;

public class User{
  public string UserId { get; set; }
  public string RoleId { get; set; }
  public string UserName { get; set; }
  private string Password { get; set; }
  public string Name { get; set; }
  public string LastName { get; set; }
  public string DocumentType { get; set; }
  public string DucumentNumber { get; set; }
  public string Gender { get; set; }
  public string PhoneNumber { get; set; }
  public bool Enabled { get; set; }
  public DateTime? EmailVerificationDate { get; set; }
  public string? ResetToken { get; set; }
  public DateTime? CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}