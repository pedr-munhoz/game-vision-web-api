using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;

namespace game_vision_web_api.Services;

public class PlayService
{
    public async Task<List<Play>> Get(long gameId)
    {
        var entities = new List<Play>();

        return entities;
    }

    public async Task<Play> Update(long id, PlayViewModel model)
    {
        var entity = new Play
        {
            PlayNumber = model.PlayNumber,
            Offense = model.Offense,
            Defense = model.Defense,
            Down = model.Down,
            Distance = model.Distance,
            Formation = model.Formation,
            Name = model.Name,
            Yards = model.Yards,
            Result = model.Result,
            FirstDown = model.FirstDown,
            Touchdown = model.Touchdown,
            Notes = model.Notes,
            Game = new Game { Name = "placeholder" },
        };

        return entity;
    }

    public async Task<int> Load(long gameId, string folderId)
    {
        var entities = new List<Play>();

        var count = entities.Count;

        return count;
    }
}