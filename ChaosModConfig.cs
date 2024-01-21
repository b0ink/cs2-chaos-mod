using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace ChaosMod;

public class ChaosModConfig : BasePluginConfig
{
    [JsonPropertyName("ConfigName")] 
    public string ConfigName { get; set; } = "value";

    [JsonPropertyName("ConfigNameSecond")]
    public string ConfigNameSecond { get; set; } = "value";
}