namespace game_vision_web_api.Models.Entities;

public class Play
{
    public long Id { get; set; }
    public string? FileId { get; set; }
    public int? PlayNumber { get; set; }
    public string? Offense { get; set; }
    public string? Defense { get; set; }
    public int? Down { get; set; }
    public int? Distance { get; set; }
    public string? Goal { get; set; }
    public string? Situation { get; set; }
    public int? Yards { get; set; }
    public string? OffensiveFormation { get; set; }
    public string? OffensivePlay { get; set; }
    public string? DefensiveFormation { get; set; }
    public string? DefensivePlay { get; set; }
    public string? Result { get; set; }
    public string? Penalty { get; set; }
    public bool? FirstDown { get; set; }
    public bool? Touchdown { get; set; }
    public bool? Safety { get; set; }
    public string? Runner { get; set; }
    public string? Passer { get; set; }
    public string? Target { get; set; }
    public string? TargetPosition { get; set; }
    public string? DefensiveTarget { get; set; }
    public string? Tackler { get; set; }
    public string? Interceptor { get; set; }
    public string? OfensiveNotes { get; set; }
    public string? DefensiveNotes { get; set; }
    public long GameId { get; set; }
    public Game Game { get; set; } = null!;
}
