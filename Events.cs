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
            var effect = GetRandomEffect();
            if (effect == null)
                return HookResult.Continue;

            effect.PreStart();


            AddTimer(5.0f, () =>
            {
                var effect = GetRandomEffect();
                if (effect == null)
                {
                    Console.WriteLine("no valid effect");
                    return;

                }

                effect.PreStart();
            });
            return HookResult.Continue;
        });
    }

}

