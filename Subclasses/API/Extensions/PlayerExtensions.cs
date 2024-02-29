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

    public static void GiveItems(this Player player, Dictionary<ushort, Dictionary<string, ushort>> inventory)
    {
        foreach (KeyValuePair<ushort, Dictionary<string, ushort>> items in inventory)
        {
            KeyValuePair<string, ushort> item = items.Value.FirstOrDefault(item => UnityEngine.Random.Range(0, 101) <= item.Value);
            
            if (!CustomItem.TryGive(player, item.Key))
                player.AddItem((ItemType)Enum.Parse(typeof(ItemType), item.Key));

            break;
        }
    }
}