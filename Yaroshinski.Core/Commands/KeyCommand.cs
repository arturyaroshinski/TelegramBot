using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Yaroshinski.Core.Interfaces;
using Yaroshinski.Core.Models;

namespace Yaroshinski.Core.Commands
{
    /// <inheritdoc cref="ITelegramCommand"/>
    public class KeyCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "/key";

        // Return array of Spil objects
        private async Task<Slip[]> GetSlipsAsync(string key)
        {
            var httpClient = new HttpClient();

            try
            {
                var keyRequest = string.Format("https://api.adviceslip.com/advice/search/{0}", key);
                var response = await httpClient.GetAsync(keyRequest);
                var content = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();

                var searchObjects = JsonConvert.DeserializeObject<SearchObject>(content);

                if (searchObjects.Slips == null)
                {
                    throw new HttpRequestException();
                }

                return searchObjects.Slips;
            }
            catch (Exception)
            {
                return new Slip[]
                {
                    new Slip(){ Advice = $"No advices with word '{key}' was found"}
                };
            }
        }

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var key = message.Text.Trim().Substring(5);

            Slip[] slips = await GetSlipsAsync(key);
            foreach (var slip in slips)
            {
                await client.SendTextMessageAsync(chatId, slip.Advice);
            }
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Name);
    }
}