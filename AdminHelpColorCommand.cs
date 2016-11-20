using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;

namespace fr.zilothewolf.adminhelp
{
    public class AdminHelpColorCommand : IRocketCommand
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
                return new List<string>() { "adminhelp.color" };
            }
        }

        public string Name
        {
            get
            {
                return "admincolor";
            }
        }

        public string Syntax
        {
            get
            {
                return " <color|list>";
            }
        }

        public string Help
        {
            get
            {
                return AdminHelp.Instance.Translate("command_color_help");
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>()
                {
                    "adminc"
                };
            }
        }
        #endregion

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;

            //Check syntax, if wrong, returns a synxtax example
            if (command.Length > 0)
            {

                //Send the list of possible colors
                if (command[0] == "list")
                {
                    UnturnedChat.Say(player, AdminHelp.Instance.Translate("admincolor_list") + " black, blue, cyan, green, gray/grey, magenta, red, white, yellow");
                }

                //Send an exemple message with the chosen color
                else
                {
                    foreach (string str in command)
                    {
                        UnturnedChat.Say(player, AdminHelp.Instance.Translate("admincolor_example") + " " + str, AdminHelp.Instance.setColor(str, "admincolorCommand <- This isn't a big deal"));
                    }

                    UnturnedChat.Say(player, AdminHelp.Instance.Translate("admincolor_message"));
                }
            }
            else
            {
                UnturnedChat.Say(player, AdminHelp.Instance.Translate("command_syntax_message") + " /admincolor" + this.Syntax);
            }
        }
    }
}