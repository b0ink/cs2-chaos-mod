using System;
using CounterStrikeSharp.API.Core;

namespace ChaosMod;

public partial class ChaosMod
{

	public void RegisterEvents()
	{
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

}

