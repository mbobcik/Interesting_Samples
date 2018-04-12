import discord
from discord.ext.commands import Bot
from discord.ext import commands
import asyncio
from pprint import pprint
import time
import json

print("Starting BoboBot...")
ConfigData = json.load(open('botData.json'))
Loop = asyncio.get_event_loop()
DiscordClient = discord.Client()
CommandBot = commands.Bot(command_prefix = "!")

@CommandBot.event
async def on_ready():
    print("BoboBot is online and ready!")
    print("All visible servers: ")
    showServers()

    newGame = discord.Game()
    newGame.name = "with girl"
    newGame.url = "With girl"
    newGame.type = 0

    showVisibleUsers();
    await CommandBot.change_presence(game = newGame, status = "??", afk = 0)

@CommandBot.event
async def on_message(message):
    logMessage(message)

        

    if message.content.lower() == "cookie":
        await CommandBot.send_message(message.channel, ":cookie:")

    if message.content.lower() == "ahoj":
        await CommandBot.send_message(message.channel, "Cau " + message.author.mention)

    if message.content.lower() == "!servers":
        showServers();

    if message.content.lower() == "!users":
        showVisibleUsers();

    if message.content.lower() == "bot stop":
        print("Stopping bot...")
        await CommandBot.logout()
        await CommandBot.close()
        await DiscordClient.logout()
        await DiscordClient.close()
        print("BoboBot is now offline.")

def UserToString(user):
    return user.name + "#" + user.discriminator

def logMessage(message):
    if message.server is None or message.channel is None:
        channel = "Private"
    else:
        channel = message.server.name+"#"+ message.channel.name
    print(message.timestamp.strftime("%Y-%m-%d %H:%M:%S") + " " + UserToString(message.author) +"@"+ channel + " > " + message.content)

def showServers():
    for server in CommandBot.servers:
        print("\t" + server.name + " in "+ server.region.name)
        for channel in server.channels:
            print("\t\t#" + channel.name)
    print()

def showVisibleUsers():
    print("All visible users: (" + str(countUsers()) + ")")
    for server in CommandBot.servers:
        for member in server.members:
            print("\t" + UserToString(member))      
    print()

def countUsers():
    count = 0
    for server in CommandBot.servers:
        for member in server.members:
            count+=1
    return count;

CommandBot.run(ConfigData["secret"])