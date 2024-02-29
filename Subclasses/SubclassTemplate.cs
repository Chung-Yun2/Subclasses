using System.Collections.Generic;
using Exiled.API.Enums;
using PlayerRoles;
using Subclasses.API.Features;

namespace Subclasses;

public class SubclassTemplate : Subclass
{
    public override string Name { get; set; } = "Janitor";

    public override string CustomInfo { get; set; } = "Janitor";

    public override string Message { get; set; } = "You have been spawned as Janitor!";

    public override string ConsoleMessage { get; set; } = "You have been spawned as Janitor!";

    public override RoleTypeId Role { get; set; } = RoleTypeId.ClassD;

    public override ushort Count { get; set; } = 4;

    public override ushort Chance { get; set; } = 20;

    public override SpawnLocationType? SpawnLocation { get; set; } = SpawnLocationType.InsideLczWc;

    public override Dictionary<ushort, Dictionary<string, ushort>> Inventory { get; set; } = new()
    {
        {
            0, new()
            {
                {
                    ItemType.KeycardJanitor.ToString(), 100
                }
            }
        },
        {
            1, new()
            {
                {
                    ItemType.Medkit.ToString(), 30
                },
                {
                    ItemType.Adrenaline.ToString(), 40
                }
            }
        }
    };
}