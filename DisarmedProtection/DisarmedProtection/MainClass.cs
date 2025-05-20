using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InventorySystem.Disarming;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features;
using Log = LabApi.Features.Console.Logger;
using LabApi.Loader.Features.Plugins;
using MapGeneration;
using Utils.NonAllocLINQ;
using PlayerStatsSystem;
using PlayerRoles.PlayableScps.Scp3114;
using PlayerRoles;

namespace DisarmedProtection
{
    public class MainClass : Plugin<Config>
    {
        public override void Enable()
        {
            PlayerEvents.Cuffing += this.OnPlayerCuffing;
            PlayerEvents.Hurting += this.OnPlayerHurting;
            PlayerEvents.Uncuffing += this.OnPlayerUncuffing;
        }

        public override void Disable()
        {
            PlayerEvents.Cuffing -= this.OnPlayerCuffing;
            PlayerEvents.Hurting -= this.OnPlayerHurting;
            PlayerEvents.Uncuffing -= this.OnPlayerUncuffing;
        }

        public void OnPlayerCuffing(PlayerCuffingEventArgs ev)
        {
            if (Config.DisarmLimit > 0 && DisarmedPlayers.Entries.Count(p => p.Disarmer == ev.Player.NetworkId) >= Config.DisarmLimit)
            {
                ev.Player.SendHint(Config.DisarmLimitMessage, 5);
                Log.Debug($"Player {ev.Player.Nickname} has reached the limit disarmed people.", Config.Debug);
                ev.IsAllowed = false;
            }
        }

        public void OnPlayerHurting(PlayerHurtingEventArgs ev)
        {
            RoleTypeId role = ev.Player.RoleBase is Scp3114Role skeleton && Config.DisguiseProtection ? skeleton.CurIdentity.StolenRole : ev.Player.Role;
            if (ev.DamageHandler is not Scp018DamageHandler
            && ev.Player.IsDisarmed && ev.Attacker.IsHuman
            && Config.ProtectedRoles.TryGetValue(role, out var settings)
            && settings.TryGetValue(ev.Attacker.Team, out List<FacilityZone> zones)
            && zones.Contains(ev.Player.Zone)
            && (Config.ProtectionDistance <= 0 || (ev.Player.Position - ev.Player.DisarmedBy.Position).sqrMagnitude <= Config.ProtectionDistance))
            {
                Log.Debug($"Player {ev.Player.Nickname} ({ev.Player.Role}) is protected against damage from player {ev.Attacker.Nickname} ({ev.Player.Team}) in {ev.Player.Room.Zone} zone.", Config.Debug);
                ev.IsAllowed = false;
            }
        }

        public void OnPlayerUncuffing(PlayerUncuffingEventArgs ev)
        {
            if (ev.Target.DisarmedBy == null && Config.RaDisarmedRelease || ev.Target.DisarmedBy != null && (ev.Target.DisarmedBy == ev.Player || Config.AnyoneRelease))
            {
                return;
            }
            ev.Player.SendHint(Config.ReleaseFailMessage, 5);
            Log.Debug($"Player {ev.Player.Nickname} can't release any player disarmed via RA console.", Config.Debug);
            ev.IsAllowed = false;
        }

        public override string Name { get; } = "DisarmedProtection";
        public override string Description { get; } = null;
        public override string Author { get; } = "Phineapple18";
        public override Version Version { get; } = new(1, 0, 2);
        public override Version RequiredApiVersion { get; } = new(LabApiProperties.CompiledVersion);
    }
}
