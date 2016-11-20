using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using UnityEngine;

namespace fr.zilothewolf.adminhelp
{
    public class AdminHelp : RocketPlugin<AdminHelpConfig>
    {
        public string pluginVersion = "Release 1.0.0";

        public static AdminHelp Instance;
        public static bool configlogEveryCommandInConsole;
        public static bool configDisplayMessageInConsole;
        public static string configCommandBusyColor;
        public static string configCommandUnBusyColor;
        public static string configMessageColor;
        public static string configMessageSentColor;
        public static string configMessageNoAdminOnlineColor;

        public static List<CSteamID> busyAdmins = new List<CSteamID>();

        ////////////////////////
        //   Start and stop   //
        //     Set events     //
        ////////////////////////

        protected override void Load()
        {
            sendToConsole("Loading plugin..");

            Instance = this;
            configlogEveryCommandInConsole = Configuration.Instance.logEveryCommandInConsole;
            configDisplayMessageInConsole = Configuration.Instance.showMessageInConsole;
            configCommandBusyColor = Configuration.Instance.commandBusyColor;
            configCommandUnBusyColor = Configuration.Instance.commandUnBusyColor;
            configMessageColor = Configuration.Instance.messageColor;
            configMessageSentColor = Configuration.Instance.messageSentColor;
            configMessageNoAdminOnlineColor = Configuration.Instance.messageNoAdminOnlineColor;

            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;

            sendToConsole("Plugin by: Zilothewolf");
            sendToConsole("Version: " + pluginVersion);
            sendToConsole("Plugin enabled!");
        }

        protected override void Unload()
        {
            sendToConsole("Unloading plugin..");

            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;

            sendToConsole("Plugin disabled!");
        }


        ///////////////////////////////
        //   Sets all the messages   //
        ///////////////////////////////

        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList()
                {
                    {"command_syntax_message", "Please do"},
                    {"no_admin_online", "There is no Admins online for now, or they are busy, sorry!"},
                    {"adminhelp_help", "Send a message to Admins only"},
                    {"adminhelp_sent", "Message has been sent!"},
                    {"adminhelp_prefix", "[AdminH] "},
                    {"adminhelp_sufix", ": "},
                    {"admincolor_help", "Display the color with an example message"},
                    {"admincolor_example", "This is an example message displaying in:"},
                    {"admincolor_message", "If some colors are white, it means they doesn't exist"},
                    {"admincolor_list", "Here are the colors:"},
                    {"admincolor_console_error", "Wrong color, set to default value WHITE, error at:"},
                    {"adminbusy_console_busy", "is now busy"},
                    {"adminbusy_console_unbusy", "is not busy anymore"},
                    {"adminbusy_help", "Disable receiving the AdminHelp messages"},
                    {"adminbusy_on", "You will no longer receive AdminHelp messages"},
                    {"adminbusy_off", "You will now receive AdminHelp messages"},
                };
            }
        }


        //////////////////
        //    Others    //
        //////////////////

        //When called, return the online players list
        public List<UnturnedPlayer> Players()
        {
            List<UnturnedPlayer> list = new List<UnturnedPlayer>();

            foreach (SteamPlayer sp in Provider.Players)
            {
                UnturnedPlayer p = UnturnedPlayer.FromSteamPlayer(sp);
                list.Add(p);
            }

            return list;
        }

        //Busy admin list
        public bool isAdminBusy(CSteamID playerID)
        {
            bool toReturn = false;

            if (busyAdmins.Contains(playerID))
            {
                toReturn = true;
            }

            return toReturn;
        }
        public void changeBusyState(UnturnedPlayer player, CSteamID playerID)
        {
            if (busyAdmins.Contains(playerID))
            {
                busyAdmins.Remove(playerID);
                UnturnedChat.Say(player, AdminHelp.Instance.Translate("command_busy_off"), this.setColor(configCommandUnBusyColor, "busy_off"));
                if (configlogEveryCommandInConsole)
                {
                    sendToConsole(player.CharacterName + " " + AdminHelp.Instance.Translate("console_unbusy"));
                }
            }
            else
            {
                busyAdmins.Add(playerID);
                UnturnedChat.Say(player, AdminHelp.Instance.Translate("command_busy_on"), this.setColor(configCommandBusyColor, "busy_on"));
                if (configlogEveryCommandInConsole)
                {
                    sendToConsole(player.CharacterName + " " + AdminHelp.Instance.Translate("console_busy"));
                }
            }
        }

        //Return a color from a string
        public Color setColor(string color, string whereIsCalled)
        {
            Color toReturn;

            if (color.ToLower() == "black")
            {
                toReturn = new Color(0, 0, 0, 1);
            }
            else if (color.ToLower() == "blue")
            {
                toReturn = new Color(0, 0, 1, 1);
            }
            else if (color.ToLower() == "cyan")
            {
                toReturn = new Color(0, 1, 1, 1);
            }
            else if (color.ToLower() == "green")
            {
                toReturn = new Color(0, 1, 0, 1);
            }
            else if (color.ToLower() == "gray" || color == "grey")
            {
                toReturn = new Color((float)0.5, (float)0.5, (float)0.5, 1);
            }
            else if (color.ToLower() == "magenta")
            {
                toReturn = new Color(1, 0, 1, 1);
            }
            else if (color.ToLower() == "red")
            {
                toReturn = new Color(1, 0, 0, 1);
            }
            else if (color.ToLower() == "white")
            {
                toReturn = new Color(1, 1, 1, 1);
            }
            else if (color.ToLower() == "yellow")
            {
                toReturn = new Color(1, (float)0.92, (float)0.016, 1);
            }
            else
            {
                if (configlogEveryCommandInConsole)
                {
                    sendToConsole(Translate("admincolor_console_error") + " " + whereIsCalled);
                }
                toReturn = new Color(1, 1, 1, 1);
            }

            return toReturn;
        }


        //////////////////
        //    Events    //
        //////////////////

        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            CSteamID playerID = player.CSteamID;

            if (busyAdmins.Contains(playerID))
            {
                busyAdmins.Remove(playerID);
                if (configlogEveryCommandInConsole)
                {
                    sendToConsole(Translate("console_unbusy") + "" + playerID.ToString());
                }
            }
        }


        ////////////////////
        //     Console    //
        ////////////////////

        static public void sendToConsole(string message)
        {
            Rocket.Core.Logging.Logger.Log(message);
        }
    }
}