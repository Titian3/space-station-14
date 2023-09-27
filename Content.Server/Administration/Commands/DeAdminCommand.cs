using System.Threading.Tasks;
using Content.Server.Administration.Managers;
using Content.Server.Database;
using Content.Server.Players;
using Content.Shared.Administration;
using JetBrains.Annotations;
using Robust.Server.Player;
using Robust.Shared.Console;
using Robust.Shared.Network.Messages;


namespace Content.Server.Administration.Commands
{
    [UsedImplicitly]
    [AdminCommand(AdminFlags.None)]
    public sealed class DeAdminCommand : IConsoleCommand
    {
        public string Command => "deadmin";
        public string Description => "Temporarily de-admins you so you can experience the round as a normal player.";
        public string Help => "Usage: deadmin\nUse readmin to re-admin after using this.";

        public async void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            var player = shell.Player as IPlayerSession;
            if (player == null)
            {
                shell.WriteLine("You cannot use this command from the server console.");
                return;
            }

            await ExecuteClientSideCommand(shell); // Await the command to complete

            var mgr = IoCManager.Resolve<IAdminManager>();
            mgr.DeAdmin(player);
        }

        public async Task<bool> ExecuteClientSideCommand(IConsoleShell shell)
        {
            var testcommand = new MsgConCmd();
            testcommand.Text = "togglehealthoverlay false";
            if (shell.Player != null)
                shell.Player.ConnectedClient.SendMessage(testcommand);
            shell.Player.ConnectedClient.
            return true;
        }
    }
}
