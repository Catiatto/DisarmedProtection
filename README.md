# DisarmedProtection (1.0.2)
Plugin for the "SCP: Secret Laboratory" game, that allows disarmed players to be protected from certain teams in certain zones.

## Features
- Disarmed players won't take damage (except SCP-018) from certain teams (excepts SCPS), if they are in certain zone(s).
- Disarmed SCP-3114 can be protected, if the protection applies to stolen role.
- The protection can be further reduced by the distance between disarmed and disarmer.
- Players can have a limit on the number of people they can disarm.
- Disarmed players can be configurably released by anyone or only their disarmer.
- Players disarmed via RA console can be configurably be prevented from being released by players.

## Installation
Place *DisarmedProtection* dll in "...\AppData\Roaming\SCP Secret Laboratory\LabAPI-Beta\plugins".

## Config
|Name|Description|Type|Default value|
|---|---|---|---|
|debug|Should debug be enabled?|bool|false|
|protected_roles|Roles that, if disarmed, won't take damage from certain teams in certain zones.|Dictionary<RoleTypeId, Dictionary<Team, List\<FacilityZone>>>|ClassD:<br/>&nbsp;&nbsp;Scientists:<br/>&nbsp;&nbsp;&nbsp;- LightContainment<br/>&nbsp;&nbsp;&nbsp;- HeavyContainment<br/>&nbsp;&nbsp;&nbsp;- Entrance<br/>&nbsp;&nbsp;&nbsp;- Surface<br/>&nbsp;&nbsp;FoundationForces:<br/>&nbsp;&nbsp;&nbsp;- Entrance<br/>Scientists:<br/>&nbsp;&nbsp;ChaosInsurgency:<br/>&nbsp;&nbsp;&nbsp;- Entrance<br/>&nbsp;&nbsp;&nbsp;- Surface|
|protection_distance|Maximum distance between Disarmer and Disarmed where the protection works. Set to 0 or below to disable.|float|5f|
|disarm_limit|How many people can 1 person disarm? Set to 0 or below to disable.|int|2|
|disarm_limit_message|Hint a player will receive, if they try to disarm a player over an allowed limit.|string|You have reached the maximum limit of players you can disarm.|
|ra_disarmed_release|Can players disarmed via RA console be released by other players?|bool|true|
|anyone_release|Can anyone release the Disarmed (true) or only the Disarmer (false)?|bool|false|
|release_fail_message|Hint a player will receive, if they try to release Disarmed while not being a Disarmer and above being false.|string|You can't release any player you haven't disarmed.|
|disguise_protection|Should SCP-3114 be protected, if protection applies to disguised role?|bool|true|
