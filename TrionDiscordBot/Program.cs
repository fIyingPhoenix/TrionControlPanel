using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using TrionDiscordBot.Commands;
using TrionDiscordBot.Data;

namespace TrionDiscordBot
{

    internal class Program
    {

        static async Task Main()
        {
            var DiscordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = Models.Discord.Token(),
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Config.DiscordClient = new DiscordClient(DiscordConfig);
             
            //EVENT HANDLERS 
            Config.DiscordClient.ComponentInteractionCreated += InteractionEventHandler;
            Config.DiscordClient.ModalSubmitted += ModalEventHandler;
            Config.DiscordClient.VoiceStateUpdated += VoiceChannelHandler;
            Config.DiscordClient.GuildMemberAdded += UserJoinHandler;
            Config.DiscordClient.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { Models.Discord.Prefix() },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };
            Config.Commands = Config.DiscordClient.UseCommandsNext(commandsConfig);

           
            var SlashCommandsConfig = Config.DiscordClient.UseSlashCommands();

            SlashCommandsConfig.RegisterCommands<Account>(1077224912462295160);

            //ERROR EVENT HANDLERS
            Config.Commands.CommandErrored += OnCommandError;

            await Config.DiscordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task OnCommandError(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            // Casting my ErrorEventArgs as a ChecksFailedException
            if (e.Exception is ChecksFailedException castedException)
            {
                string cooldownTimer = string.Empty;

                foreach (var check in castedException.FailedChecks)
                {
                    var cooldown = (CooldownAttribute)check; //The cooldown that has triggered this method
                    TimeSpan timeLeft = cooldown.GetRemainingCooldown(e.Context); //Getting the remaining time on this cooldown
                    cooldownTimer = timeLeft.ToString(@"hh\:mm\:ss");
                }

                var cooldownMessage = new DiscordEmbedBuilder()
                {
                    Title = "Wait for the Cooldown to End",
                    Description = "Remaining Time: " + cooldownTimer,
                    Color = DiscordColor.Red
                };

                await e.Context.Channel.SendMessageAsync(embed: cooldownMessage);
            }
        }

        private static Task UserJoinHandler(DiscordClient sender, GuildMemberAddEventArgs args)
        {
            throw new NotImplementedException();
        }

        private static Task VoiceChannelHandler(DiscordClient sender, VoiceStateUpdateEventArgs args)
        {
            throw new NotImplementedException();
        }

        private static Task ModalEventHandler(DiscordClient sender, ModalSubmitEventArgs args)
        {
            throw new NotImplementedException();
        }

        private static Task InteractionEventHandler(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            throw new NotImplementedException();
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}