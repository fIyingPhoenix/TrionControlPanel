using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TrionDatabase;
using TrionLibrary.Setting;
using TrionLibrary.Models;
using TrionDiscordBot.Data;

namespace TrionDiscordBot.Commands
{
    public class Account : ApplicationCommandModule
    {
        private static System.Timers.Timer _checkUsernamesTimer;
        public Account()
        {
            // Initialize the timer to trigger every 5 minutes (300,000 milliseconds)
            _checkUsernamesTimer = new (300000);
            _checkUsernamesTimer.Elapsed += async (sender, e) => await CheckUsernamesAutomatically();
            _checkUsernamesTimer.AutoReset = true; // Repeat every 5 minutes
            _checkUsernamesTimer.Enabled = true;   // Start the timer
        }

        private async Task CheckUsernamesAutomatically()
        {
            throw new NotImplementedException();
        }

        private static List<string> predefinedUsernames = new List<string>();

       [SlashCommand("create","Create a account!")]
        public async Task CreateAccount (InteractionContext ctx,
            [Option("Username","Your Username")]string username,
            [Option("Password", "Your Password")] string password,
            [Option("Email", "Your Email")] string email)
        {
            try
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
               new DiscordInteractionResponseBuilder()
               .WithContent(AccountCreate.CreateAuth(username, password, email, "acore_auth" , Enums.Cores.AzerothCore).ToString()).AsEphemeral(true));


            }
            catch (Exception ex) 
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                .WithContent("Error, Faild to create account! " + ex.Message).AsEphemeral(true));
                //var EmbededMessage = new DiscordEmbedBuilder()
                //{
                //    Title = ex.Message ,
                //    Color = DiscordColor.Red
                //};
                //await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                //.AddEmbed(EmbededMessage)
                //.AsEphemeral(true));// Make the embed ephemeral as well
            }
        }

    }
}
