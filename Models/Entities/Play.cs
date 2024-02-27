namespace game_vision_web_api.Models.Entities;

public class Play
{
    public long Id { get; set; }
    public int? PlayNumber { get; set; }
    public string? Offense { get; set; }
    public string? Defense { get; set; }
    public int? Down { get; set; }
    public int? Distance { get; set; }
    public string? Formation { get; set; }
    public string? Name { get; set; }
    public int? Yards { get; set; }
    public string? Result { get; set; }
    public bool? FirstDown { get; set; }
    public bool? Touchdown { get; set; }
    public string? Notes { get; set; }
    public Game Game { get; set; } = null!;
}
