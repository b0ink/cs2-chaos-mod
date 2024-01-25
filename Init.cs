using System;
using ChaosMod.Effects;
using System.Linq;
using System.Reflection;

namespace ChaosMod;


public partial class ChaosMod
{
    public void RegisterEffects()
    {

        //Effects = new List<BaseEffect>
        //{
        //    new RapidFire(),
        //    new SlowFire(),
        //};

        Effects = new List<BaseEffect>();

        var assembly = Assembly.GetExecutingAssembly();
        var allTypes = assembly.GetTypes();

        var effectTypes = allTypes.Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(BaseEffect)));

        foreach (var effectType in effectTypes)
        {
            var effectInstance = Activator.CreateInstance(effectType) as BaseEffect;
            if (effectInstance != null)
            {
                Effects.Add(effectInstance);
            }
        }

        foreach (var effect in Effects)
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

        foreach (var effect in Effects)
        {
            foreach (var ConflictType in effect.ConflictTypes)
            {
                var effectOfType = Effects.Where(e => e.GetType() == ConflictType).FirstOrDefault();
                if (effectOfType == null) continue;

                effect.Conflicts.Add(effectOfType);
                effectOfType.Conflicts.Add(effect);
            }
        }

    }
}

