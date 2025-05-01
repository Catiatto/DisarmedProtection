using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

using MapGeneration;
using PlayerRoles;

namespace DisarmedProtection
{
    public class Config
    {
        [Description("Should debug be enabled?")]
        public bool Debug { get; set; } = false;

        [Description("Roles that, if disarmed, won't take damage from certain teams in certain zones.")]
        public Dictionary<RoleTypeId, Dictionary<Team, List<FacilityZone>>> ProtectedRoles { get; set; } = new()
        {
            {
                RoleTypeId.ClassD,
                new()
                {
                    {
                        Team.Scientists,
                        new()
                        {
                            FacilityZone.LightContainment,
                            FacilityZone.HeavyContainment,
                            FacilityZone.Entrance,
                            FacilityZone.Surface
                        }
                    },
                    {
                        Team.FoundationForces,
                        new()
                        {
                            FacilityZone.Entrance
                        }
                    }
                }
            },
            {
                RoleTypeId.Scientist,
                new()
                {
                    {
                        Team.ChaosInsurgency,
                        new()
                        {
                            FacilityZone.Entrance,
                            FacilityZone.Surface
                        }
                    }
                }
            }
        };

        [Description("Maximum distance between Disarmer and Disarmed where the protection works. Set to 0 or below to disable.")]
        public float ProtectionDistance { get; set; } = 5f;

        [Description("How many people can 1 person disarm? Set to 0 or below to disable.")]
        public int DisarmLimit { get; set; } = 2;
        
        [Description("Hint a player will receive, if they try to disarm a player over an allowed limit.")]
        public string DisarmLimitMessage { get; set; } = "You have reached the maximum limit of players you can disarm.";

        [Description("Can players disarmed via RA console be released by other players?")]
        public bool RaDisarmedRelease { get; set; } = true;

        [Description("Can disarmed player be released by anyone (true) or only their Disarmer (false)?")]
        public bool AnyoneRelease { get; set; } = false;

        [Description("Hint a player will receive, if releasing fails.")]
        public string ReleaseFailMessage { get; set; } = "You can't release any player you haven't disarmed.";

        [Description("Should SCP-3114 be protected, if protection applies to disguised role?")]
        public bool DisguiseProtection { get; set; } = true;
    }
}
