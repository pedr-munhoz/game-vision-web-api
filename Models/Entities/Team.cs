namespace game_vision_web_api.Models.Entities;

public class Team
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Prefix { get; set; } = null!;
    public List<Game> Games { get; set; } = null!;
}