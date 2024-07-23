namespace MagicalProduct.API.Payload.Response;

public class LoginResponse
{
    public string AccessToken { get; set; }
    public string Id { get; set; } = null!;
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public bool? Gender { get; set; }
    public bool? Status { get; set; }
    public int? RoleId { get; set; }
}