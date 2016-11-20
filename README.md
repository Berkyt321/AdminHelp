# RocketMod-AdminHelp
https://dev.rocketmod.net/plugins/adminhelp/ ‎
AdminHelp is a plugin that allow players to ask to the Admins something through a command. That is to avoid chat spamming, and to quietly report someone.

&nbsp;
<h1>Commands:</h1>
-"/adminhelp &lt;message&gt;" Send a message only to the Admins and console (depending on the config)
Aliase: adminh
-"/adminbusy" Toggles the display of AdminHelp messages for the player
Aliase: adminb
-"/admincolor &lt;color&gt;/&lt;firstColor secondColor..&gt;/&lt;list&gt;" A little command to see what colors are available for the plugin (You'll soon going to be able to use custom colors)
Aliase: adminh

&nbsp;
<h1>Features:</h1>
-Allow players to send a message only visible to Admins (and console depending on the config)
-Permissions to allow to send/receive the AdminHelp messages
-Messages receivers can now turn it on/off doing /adminbusy
-Custom message display using the translation file and custom colors

&nbsp;
<h1>Config:</h1>
-"logEveryCommandInConsole" if true messages will be sent in console when players uses /adminbusy and a few other actions
-"showMessageInConsole" if true then the /adminh command will display the messages in the console too
-"commandBusyColor" color for the message when someone is now busy (only sent to caller)
-"commandUnBusyColor" color for the message when someone is not busy anymore (only sent to caller)
-"messageColor" color of the /adminhelp message
-"messageSentColor" color of the message sent to the player when his message has been successfuly sent to admin(s)
-"messageNoAdminOnlineColor" color of the message sent to the player when he does /adminhelp but there is no admin(s) online/not busy

&nbsp;
<h1>Permissions:</h1>
-"adminhelp.send" allow someone to use /adminhelp
-"adminhelp.receive" allow someone to receive the /adminhelp messages (admins already does)
-"adminhelp.busy" allow someone to use /adminbusy
-"adminhelp.busy.bypass" allow someone to bypass the busy state
-"adminhelp.color" allow someone to use the /admincolor command

&nbsp;
<h3>Comming features:</h3>
-If there is no admin online at the moment, then the first admin that is going to connect will receive the messages
-Add more colors and custom choice
-Remake the look of the messages
