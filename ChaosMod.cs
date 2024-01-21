using ChaosMod.Effects;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;

namespace ChaosMod;


public class ChaosMod : BasePlugin, IPluginConfig<ChaosModConfig>
{
    public override string ModuleName => "Chaos Mod";
    public override string ModuleAuthor => "BOINK";
    public override string ModuleVersion => "0.0.1";


    public ChaosModConfig Config { get; set; } = new();

    public required List<BaseEffect> Effects { get; set; }
    public override void Load(bool hotReload)
    {
        RegisterEffects();

        RegisterEventHandler<EventRoundStart>((@event, @info) =>
        {
            ClearEffects();
            return HookResult.Continue;
        });

        RegisterEventHandler<EventRoundEnd>((@event, @info) =>
        {
            ClearEffects();
            return HookResult.Continue;
        });

        RegisterEventHandler<EventRoundFreezeEnd>((@event, @info) =>
        {
            var effect = GetRandomEffect(Effects);
            if (effect == null)
                return HookResult.Continue;

            effect.PreStart();
            return HookResult.Continue;
        });

    }

    static BaseEffect GetRandomEffect(List<BaseEffect> list)
    {
        Random random = new Random();
        int randomIndex = random.Next(0, list.Count);
        return list[randomIndex];
    }

    public override void Unload(bool hotReload)
    {

    }

    public void OnConfigParsed(ChaosModConfig config)
    {
        this.Config = config;
    }

    public void ClearEffects()
    {
        foreach(var effect in Effects)
        {
            effect.PreStop();
        }
    }

    public void RegisterEffects()
    {
        Effects = new List<BaseEffect>
        {
            new RapidFire{ },
            new SlowFire{ },
        };

        foreach (var effect in Effects)
        {
            effect.Plugin = this;
            effect.Init();
            if(effect.Duration == 0)
            {
                effect.Duration = 15;
            }
        }
    }
}

