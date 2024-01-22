using System;
using ChaosMod.Effects;
using System.Linq;

namespace ChaosMod;


public partial class ChaosMod
{
    public void RegisterEffects()
    {

        Effects = new List<BaseEffect>
        {
            new RapidFire(),
            new SlowFire(),
        };


        foreach(var effect in Effects)
        {
            effect.Conflicts = new List<BaseEffect>();
            effect.ConflictTypes = new List<Type>();
            effect.Plugin = this;
            effect.Init();

            if (effect.Duration == 0)
            {
                effect.Duration = 15;
            }
        }

        //RapidFire rapidFireInstance = Effects.OfType<RapidFire>().FirstOrDefault();

        foreach (var effect in Effects)
        {
            foreach (var ConflictType in effect.ConflictTypes)
            {
                var type = ConflictType.GetType();
                var effectOfType = Effects.Where(e => e.GetType() == type).FirstOrDefault();
                if (effectOfType == null) continue;
                effect.Conflicts.Add(effectOfType);
                effectOfType.Conflicts.Add(effect);
            }
        }




    }


}

