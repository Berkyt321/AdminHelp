using Rocket.API;

namespace fr.zilothewolf.adminhelp
{
    public class AdminHelpConfig : IRocketPluginConfiguration
    {
        public bool logEveryCommandInConsole;
        public bool showMessageInConsole;
        public string commandBusyColor;
        public string commandUnBusyColor;
        public string messageColor;
        public string messageSentColor;
        public string messageNoAdminOnlineColor;

        public void LoadDefaults()
        {
            logEveryCommandInConsole = true;
            showMessageInConsole = true;
            commandBusyColor = "red";
            commandUnBusyColor = "green";
            messageColor = "blue";
            messageSentColor = "green";
            messageNoAdminOnlineColor = "red";
        }
    }
}
