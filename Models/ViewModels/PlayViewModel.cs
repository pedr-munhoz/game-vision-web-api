using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_vision_web_api.Models.ViewModels;

public class PlayViewModel
{
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
}