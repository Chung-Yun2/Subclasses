using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using Subclasses.API.Extensions;

namespace Subclasses.API.Features;

public abstract class Subclass
{
    public abstract string Name { get; set; }
    public abstract string CustomInfo { get; set; }
    public abstract string Message { get; set; }
    public abstract string ConsoleMessage { get; set; }
    public abstract RoleTypeId Role { get; set; }
    
    public virtual ushort Count { get; set; } = 1;
    public virtual ushort Chance { get; set; } = 100;
    public virtual ushort MaxHealth { get; set; } = 100;
    public virtual Dictionary<ushort, Dictionary<string, ushort>> Inventory { get; set; } = new();
    public virtual Dictionary<AmmoType, ushort> Ammo { get; set; } = new();
    public virtual SpawnLocationType? SpawnLocation { get; set; } = null;

    private HashSet<Player> _trackedPlayers = new();

    public virtual void Init(Player player)
    {
        _trackedPlayers.Add(player);

        OnAdded(player);
    }

    protected virtual void OnAdded(Player player)
    {
        player.SetMaxHealth(MaxHealth);
        player.ClearInventory();
        player.ClearAmmo();
        player.GiveItems(Inventory);

        if (SpawnLocation is not null)
            player.Teleport(SpawnLocation);

        foreach (var pair in Ammo)
            player.AddAmmo(pair.Key, pair.Value);

        player.ShowHint(Message, 10f);

        var builder = new StringBuilder();
        builder.AppendLine();
        builder.AppendLine();
        builder.AppendLine(ConsoleMessage);

        player.SendConsoleMessage(builder.ToString(), "white");

        player.CustomInfo = CustomInfo;

        SubscribeEvents();
    }

    protected virtual void Remove(Player player)
    {
        player.CustomInfo = string.Empty;
        _trackedPlayers.Remove(player);

        UnsubscribeEvents();
    }

    protected virtual void SubscribeEvents()
    {
        Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
    }

    protected virtual void UnsubscribeEvents()
    {
        Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
    }

    protected virtual void OnChangingRole(ChangingRoleEventArgs ev)
    {
        if (Check(ev.Player))
            Remove(ev.Player);
    }

    protected virtual bool Check(Player player)
    {
        return player != null && _trackedPlayers.Contains(player);
    }

    public static Subclass Get(string name)
    {
        return Collections.Subclasses.FirstOrDefault(s => s.Name == name);
    }

    public static bool TryGet(Player player, out Subclass subclass)
    {
        subclass = Collections.Subclasses.FirstOrDefault(s => s._trackedPlayers.Contains(player));
        return subclass is not null;
    }

    public void Register()
    {
        Collections.Subclasses.Add(this);
    }
}