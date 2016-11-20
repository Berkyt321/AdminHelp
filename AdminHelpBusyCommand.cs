using Rocket.API;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace fr.zilothewolf.adminhelp
{
    public class AdminHelpBusyCommand : IRocketCommand
    {
        #region Declarations
        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "adminhelp.busy" };
            }
        }

        public string Name
        {
            get
            {
                return "adminbusy";
            }
        }

        public string Syntax
        {
            get
            {
                return "";
            }
        }

        public string Help
        {
            get
            {
                return AdminHelp.Instance.Translate("command_busy_help");
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>()
                {
                    "adminb"
                };
            }
        }
        #endregion

        //Simple toggle command, using a method in the AdminHelp class
        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            AdminHelp.Instance.changeBusyState(player, player.CSteamID);
        }
    }
}