namespace game_vision_web_api.Services;

public class GoogleDriveService
{
    public async Task<(List<string> result, string? error)> ListFileIds(string folderId)
    {
        var mockResult = new List<string>
            {
                "1kC7S_HlPGr94CpH1LHyzaxviFk4hO8zF",
                "1ZVavEoVP1YMburYaQyW3rNVE3VAPjyEm",
                "1EP000A2_O5-3NtGGym-3_Tugl0jqE8l8",
                "1xaJ581-JYfOIxrnyBEf_r5oRcW2FT7L7",
                "1heMoM2A4K6iVOSFFmhtRwXb_gTJqcQiE",
                "1-8aZtHGcgNeAOJX4P51ITnTncprJPK7K",
                "12Td2kNupA82rs9zCPVuqCi39PI4yAYlP",
                "1kC7S_HlPGr94CpH1LHyzaxviFk4hO8zF",
            };

        return (result: mockResult, error: null);
    }
}