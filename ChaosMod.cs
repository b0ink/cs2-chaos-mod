using ChaosMod.Effects;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;

namespace ChaosMod;

public partial class ChaosMod : BasePlugin, IPluginConfig<ChaosModConfig>
{
    public override string ModuleName => "Chaos Mod";
    public override string ModuleAuthor => "BOINK";
    public override string ModuleVersion => "0.0.1";


    public ChaosModConfig Config { get; set; } = new();

    public required List<BaseEffect> Effects { get; set; }

    public override void Load(bool hotReload)
    {
        RegisterEffects();
        RegisterEvents();
    }


    public BaseEffect? GetRandomEffect()
    {
        var shuffledEffects = new List<BaseEffect>();
        foreach(var effect in Effects)
        {
            shuffledEffects.Add(effect);
        }

        shuffledEffects.Shuffle();

        BaseEffect? selectedEffect = null;
        foreach(var effect in shuffledEffects)
        {
            if (effect.IsActive) continue;
            if (!effect.CanRunEffect()) continue;

            bool hasConflict = false;
            foreach (var conflict in effect.Conflicts)
            {
                if (conflict.IsActive)
                {
                    hasConflict = true;
                }
            }

            if (hasConflict)
            {
                continue;
            }

            selectedEffect = effect;
            break;
        }
 
        return selectedEffect;
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

    //public void RegisterEffects()
    //{
    //    Init.RegisterEffects(Effects, Conflicts);


    //}
}

