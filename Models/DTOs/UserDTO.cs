namespace game_vision_web_api.Models.DTOs;

public class UserDTO
{
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public TeamDTO? TeamDTO { get; set; }
}
