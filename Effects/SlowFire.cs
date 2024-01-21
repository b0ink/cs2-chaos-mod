using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace ChaosMod.Effects;

public class SlowFire : BaseEffect
{
    public override string Name => "Slow Fire";
    public override string Description => "Reduces weapon fire rate";
    public override EffectDurationType DurationType => EffectDurationType.Normal;

    public override void Init()
    {
        Plugin.RegisterEventHandler<EventWeaponFire>(this.OnWeaponFire);
    }

    public HookResult OnWeaponFire(EventWeaponFire @event, GameEventInfo info)
    {
        if (!this.IsActive)
            return HookResult.Continue;

        var player = @event.Userid;
        if (player == null || !player.IsValid())
            return HookResult.Continue;

        var weapon = Helpers.GetPlayerActiveWeapon(player);
        if (weapon == null)
            return HookResult.Continue;


        Server.NextFrame(() =>
        {
            int delay = weapon.NextPrimaryAttackTick - Server.TickCount;
            delay = delay * 2;
            weapon.NextPrimaryAttackTick = Server.TickCount + delay;
        });

        return HookResult.Continue;
    }
    public override void Start()
    {
        //Server.PrintToChatAll("start slow fire");
    }

    public override void Stop()
    {
        //Server.PrintToChatAll("stop slow fire");

    }
}
