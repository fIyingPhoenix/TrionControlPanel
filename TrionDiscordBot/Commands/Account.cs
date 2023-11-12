using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using TrionDiscordBot.Data;
using TrionDiscordBot.Encrypting;
using DSharpPlus.SlashCommands;
using System.Formats.Asn1;
using MySql.Data.MySqlClient;
using System.Diagnostics.CodeAnalysis;
using DSharpPlus;
using DSharpPlus.Entities;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrionDiscordBot.Commands
{
    public class Account : ApplicationCommandModule
    {
        Database database = new();
        List<Models.Database.AccountID> accountID;
        uint id = 0;
        [SlashCommand("create","Create a account!")]
        public async Task CreateAccount (InteractionContext ctx,
            [Option("Username","Your Username")]string username,
            [Option("Password", "Your Username")] string password,
            [Option("Email", "Your Username")] string email)
        {
            try
            {
                string PasswordHash = Password.CalculatePassHashBnet(email.ToUpper(), password.ToUpper());

                await BentAccountCreate(email, PasswordHash);

                await GameAccountCreate(username, PasswordHash, email, id);
            }
            catch (Exception ex) 
            {
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                    new DiscordInteractionResponseBuilder().WithContent("Error, Faild to create account!"));
                var EmbededMessage = new DiscordEmbedBuilder()
                {
                    Title = ex.Message ,
                    Color = DiscordColor.Red
                };
                await ctx.Channel.SendMessageAsync(embed: EmbededMessage);
            }
        }     
        private async Task BentAccountCreate(string email, string passwordHash)
        {
            string sql = "INSERT INTO battlenet_accounts (`email`,`sha_pass_hash`) VALUES (@Email, @PasswordHash)";
            await Database.SaveData(sql, new { Email = email, PasswordHash = passwordHash });

            await GetBNetAccountID(email);
        }

        private async Task GameAccountCreate(string username, string passwordHash, string email, uint bnetAccountID)
        {
            string sql = "INSERT INTO account (`username`, `reg_mail`, `email`, `battlenet_account`) VALUES(@Username, @Reg_mail, @Email, @Battlenet_account)";
            await Database.SaveData(sql, new {Username = username, Reg_mail = email, Email = email, Battlenet_account = bnetAccountID });
        }
        private async Task GetBNetAccountID(string email)
        {
            string sql = "SELECT id FROM battlenet_accounts WHERE `email` = @Email";
            accountID = await database.LoadData<Models.Database.AccountID, dynamic>(sql, new {Email = email});
            foreach (var _id in accountID) { id = _id.ID; }
        }

    }
}
