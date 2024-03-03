using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.CustomItems.API.Features;

namespace Subclasses.API.Extensions;

public static class PlayerExtensions
{
    public static void SetMaxHealth(this Player player, int maxHealth)
    {
        player.MaxHealth = maxHealth;
        player.Health = maxHealth;
    }

    public static void GiveItems(this Player player, Dictionary<ushort, Dictionary<string, ushort>> items)
    {
        foreach (var item in items)
        {
            if (UnityEngine.Random.Range(0, 101) <= item.Value.Values.First())
                if (!CustomItem.TryGive(player, item.Value.Keys.First(), false))
                {
                    if (Enum.TryParse(item.Value.Keys.First(), out ItemType itemType))
                        player.AddItem(itemType);
                    else
                        Log.Error($"{item.Value.Keys.First()} is not item");
                }
        }
    }
}