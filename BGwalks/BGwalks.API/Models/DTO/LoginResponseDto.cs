namespace BGwalks.API.Models.DTO;

public class LoginResponseDto
{
  public string? JwtToken { get; set; }
  public List<String>? Roles { get; set; } = new();

}