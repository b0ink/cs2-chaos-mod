using CounterStrikeSharp.API.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosMod;

public static class Helpers
{
    public static CCSWeaponBase? GetPlayerActiveWeapon(CCSPlayerController? player)
    {
        if(player == null) return null;
        if(!player.IsValid()) return null;

        var weapon = player.PlayerPawn?.Value?.WeaponServices?.ActiveWeapon?.Value;
        if (weapon == null || !weapon.IsValid)
            return null;

        var wep = new CCSWeaponBase(weapon.Handle);
        if (wep == null || !wep.IsValid)
            return null;

        return wep;
    }
}


public static class Extensions
{
    public static bool IsValid(this CCSPlayerController? player)
    {
        if (player == null) return false;
        if (!player.IsValid) return false;
        if (player.Connected != PlayerConnectedState.PlayerConnected) return false;
        if (player.IsHLTV) return false;
        if (player.PlayerPawn == null) return false;
        if (!player.PlayerPawn.IsValid) return false;

        return true;
    }

    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
