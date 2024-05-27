using System.Linq;
using Content.Server.Administration;
using Content.Server.Administration.Commands;
using Content.Server.Administration.Managers;
using Content.Server.Database;
using Content.Server.EUI;
using Content.Shared.Administration.BanList;
using Content.Shared.Eui;
using Content.Shared.Ghost.Roles;

namespace Content.Server.Ghost.Roles.UI
{
    public sealed class GhostRolesEui : BaseEui
    {
        private readonly BanManager _banManager;
        private readonly GhostRoleSystem _ghostRoleSystem;


        public GhostRolesEui()
        {
            IoCManager.InjectDependencies(this);
            _banManager = IoCManager.Resolve<BanManager>();
            _ghostRoleSystem = IoCManager.Resolve<IEntitySystemManager>().GetEntitySystem<GhostRoleSystem>();
        }

        public override GhostRolesEuiState GetNewState()
        {
            return new(_ghostRoleSystem.GetGhostRolesInfo(Player));
        }

        public override void HandleMessage(EuiMessageBase msg)
        {
            base.HandleMessage(msg);

            switch (msg)
            {
                case RequestGhostRoleMessage req:

                    var roleBans = _banManager.GetRoleBans(Player.UserId);

                    // Set a breakpoint here to inspect the roleBans variable
                    var hasBan = false;
                    if (roleBans != null && roleBans.Any())
                    {
                        hasBan = true;
                    }

                    if (!hasBan)
                    {
                        _ghostRoleSystem.Request(Player, req.Identifier);
                    }
                    break;
                case FollowGhostRoleMessage req:
                    _ghostRoleSystem.Follow(Player, req.Identifier);
                    break;
                case LeaveGhostRoleRaffleMessage req:
                    _ghostRoleSystem.LeaveRaffle(Player, req.Identifier);
                    break;
            }
        }

        public override void Closed()
        {
            base.Closed();

            _ghostRoleSystem.CloseEui(Player);
        }
    }
}
