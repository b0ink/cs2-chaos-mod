using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Timers;
using CounterStrikeSharp.API.Modules.Utils;
using Timer = CounterStrikeSharp.API.Modules.Timers.Timer;

namespace ChaosMod;

public enum EffectDurationType
{
    Short,
    Normal,
    Long
};


public abstract class BaseEffect
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract EffectDurationType DurationType { get; }

    public bool IsActive { get; set; }
    public int Duration { get; set; }

    public Timer? Timer { get; set; }

    public BasePlugin Plugin { get; set; }

    public List<Type> ConflictTypes = new();
    public List<BaseEffect> Conflicts = new();

    public virtual void Init()
    {
        // Register Events/Listeners here
    }


    public void RegisterConflict<T>()
    {
        
        ConflictTypes.Add(typeof(T));
    }

    public void PreStart()
    {
        Server.PrintToChatAll($" [{ChatColors.Green}CHAOS{ChatColors.Default}] {this.Name}");
        foreach(var player in Utilities.GetPlayers())
        {
            if (player.IsValid())
            {
                player.ExecuteClientCommand("play sounds/buttons/bell1.wav");
            }
        }
        this.IsActive = true;
        this.Timer = Plugin.AddTimer((float)this.Duration, () =>
        {
            this.PreStop();
        });
    }

    public void PreStop()
    {
        if (this.IsActive)
        {
            this.Stop();
        }

        this.IsActive = false;
        if(this.Timer != null)
        {
            this.Timer.Kill();
        }
        this.Timer = null;
    }

    public abstract void Start();
    public abstract void Stop();

    // Listeners can be registered per-effect

    //public virtual void OnMapStart()
    //{

    //}

    //public virtual void OnTick()
    //{

    //}

    public virtual bool CanRunEffect()
    {
        return true;
    }

}
