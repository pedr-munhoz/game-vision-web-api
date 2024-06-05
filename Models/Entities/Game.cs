namespace game_vision_web_api.Models.Entities;

public class Game
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public long TeamId { get; set; }
    public Team Team { get; set; } = null!;
    public List<Play> Plays { get; set; } = null!;
}
