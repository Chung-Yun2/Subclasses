using System.Linq;
using Exiled.Events.EventArgs.Server;
using Respawning;
using Subclasses.API.Features;

namespace Subclasses.Handlers;

public class ServerHandlers
{
    public void OnRespawningTeam(RespawningTeamEventArgs ev)
    {
        foreach (Subclass subclass in Collections.Subclasses.Where(s => s.Role.TryGetAssignedSpawnableTeam(out var team) && team == ev.NextKnownTeam))
        {
            subclass.Count = 1;
        }
    }
}