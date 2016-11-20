using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace fr.zilothewolf.adminhelp
{
    public class AdminHelpMainCommand : IRocketCommand
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
                return new List<string>() { "adminhelp.send" };
            }
        }

        public string Name
        {
            get
            {
                return "adminhelp";
            }
        }

        public string Syntax
        {
            get
            {
                return " <message>";
            }
        }

        public string Help
        {
            get
            {
                return AdminHelp.Instance.Translate("adminhelp_help");
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>()
                {
                    "adminh"
                };
            }
        }
        #endregion

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            //Check the syntax, if wrong, it returns the syntax
            if (command.Length > 0)
            {

                string message = string.Join(" ", command);
                int adminFound = 0;

                //Send the message to each player in the online players list which either has the permission or is admin, and isn't in the busyAdmin list
                foreach (UnturnedPlayer admin in AdminHelp.Instance.Players())
                {
                    if ((admin.IsAdmin || admin.HasPermission("adminhelp.receive")) && (!AdminHelp.Instance.isAdminBusy(admin.CSteamID) || player.HasPermission("adminhelp.busy.bypass")))
                    {
                        if (player.HasPermission("adminhelp.busy.bypass"))
                        {
                            UnturnedChat.Say(admin, AdminHelp.Instance.Translate("adminhelp_prefix") + "{Bypass} " + player.CharacterName + AdminHelp.Instance.Translate("adminhelp_sufix") + message, AdminHelp.Instance.setColor(AdminHelp.configMessageColor, "adminhMessage"));
                            adminFound++;
                        }
                        else {
                            UnturnedChat.Say(admin, AdminHelp.Instance.Translate("adminhelp_prefix") + player.CharacterName + AdminHelp.Instance.Translate("adminhelp_sufix") + message, AdminHelp.Instance.setColor(AdminHelp.configMessageColor, "adminhMessage"));
                            adminFound++;
                        }
                    }
                }

                //Check config, if yes, send a copy of the message into the console
                if (AdminHelp.configDisplayMessageInConsole)
                {
                    AdminHelp.sendToConsole(AdminHelp.Instance.Translate("adminhelp_prefix") + player.CharacterName + AdminHelp.Instance.Translate("adminhelp_sufix") + message);
                }

                //Tells the player if the message has been sent or not
                if (adminFound > 0)
                {
                    UnturnedChat.Say(player, AdminHelp.Instance.Translate("adminhelp_sent"), AdminHelp.Instance.setColor(AdminHelp.configMessageSentColor, "messageSent"));
                    if (AdminHelp.configlogEveryCommandInConsole)
                    {
                        AdminHelp.sendToConsole(AdminHelp.Instance.Translate("adminhelp_sent"));
                    }
                }
                else
                {
                    UnturnedChat.Say(player, AdminHelp.Instance.Translate("no_admin_online"), AdminHelp.Instance.setColor(AdminHelp.configMessageNoAdminOnlineColor, "messageNoAdminOnline"));
                    if (AdminHelp.configlogEveryCommandInConsole)
                    {
                        AdminHelp.sendToConsole(AdminHelp.Instance.Translate("no_admin_online"));
                    }
                }
            }
            else
            {
                UnturnedChat.Say(player, AdminHelp.Instance.Translate("command_syntax_message") + " /adminhelp" + Syntax);
            }
        }
    }
}