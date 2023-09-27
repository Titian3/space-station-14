using Content.Client.HealthOverlay;
using Robust.Shared.Console;
using Robust.Shared.GameObjects;

namespace Content.Client.Commands
{
    public sealed class ToggleHealthOverlayCommand : IConsoleCommand
    {
        public string Command => "togglehealthoverlay";
        public string Description => "Toggles a health bar above mobs. If given bool as argument will set that state.";
        public string Help => $"Usage: {Command} [bool]";

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            if (args.Length > 1)
            {
                shell.WriteLine(Help);
                return;
            }

            var system = EntitySystem.Get<HealthOverlaySystem>();

            if (args.Length == 0)
            {
                system.Enabled = !system.Enabled;
            }
            else
            {
                if (bool.TryParse(args[0], out var isEnabled))
                {
                    system.Enabled = isEnabled;
                }
                else
                {
                    shell.WriteLine("Invalid value. Expected true or false.");
                    return;
                }
            }
            shell.WriteLine($"Health overlay system {(system.Enabled ? "enabled" : "disabled")}.");
        }
    }
}
