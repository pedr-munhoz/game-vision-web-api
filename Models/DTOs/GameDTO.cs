namespace game_vision_web_api.Models.DTOs;

public class GameDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public long TeamId { get; set; }
    public List<PlayDTO> Plays { get; set; } = null!;
}
