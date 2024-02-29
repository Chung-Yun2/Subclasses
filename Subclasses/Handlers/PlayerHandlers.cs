using System.Linq;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using Subclasses.API.Features;
using UnityEngine;

namespace Subclasses.Handlers
{
    public class PlayerHandlers
    {
        public void OnSpawned(SpawnedEventArgs ev)
        {
            if (ev.Reason is not SpawnReason.Respawn and not SpawnReason.LateJoin and not SpawnReason.RoundStart and not SpawnReason.Revived)
                return;

            foreach (var subclass in Collections.Subclasses.Where(s => s.Role == ev.Player.Role && s.Count > 0))
            {
                if (Random.Range(0, 101) >= subclass.Chance) 
                    continue;
                
                subclass.Count--;
                subclass.Init(ev.Player);
            }
        }
    }
}