using System;
using Exiled.API.Features;
using Subclasses.Handlers;

namespace Subclasses
{
    public class Subclasses : Plugin<Configs.Config>
    {
        private PlayerHandlers _playerHandlers = new();

        private ServerHandlers _serverHandlers = new();
        
        public override string Author { get; } = "chungyun";

        public override string Name { get; } = "Subclasses"; 

        public override string Prefix { get; } = "Subclasses";

        public override Version Version => new Version(1, 0, 0);
        
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Spawned += _playerHandlers.OnSpawned;
            Exiled.Events.Handlers.Server.RespawningTeam += _serverHandlers.OnRespawningTeam;

            Config.LoadConfigs();

            foreach (var subclass in Config.Subclasses) 
                subclass.Register();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Spawned -= _playerHandlers.OnSpawned;
            Exiled.Events.Handlers.Server.RespawningTeam -= _serverHandlers.OnRespawningTeam;

            base.OnDisabled();
        }
    }
}