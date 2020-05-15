using Newtonsoft.Json;
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
            // TODO: add try catch block.
            var httpClient = new HttpClient();

            key = string.Format("https://api.adviceslip.com/advice/search/{0}", key);
            var response = await httpClient.GetAsync(key);
            var content  = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            var searchObjects = JsonConvert.DeserializeObject<SearchObject>(content);

            return searchObjects.Slips;
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
