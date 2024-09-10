using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace game_vision_web_api.Models.Entities;

public class User : IdentityUser
{
    public long? TeamId { get; set; }
    public Team? Team { get; set; }
}
