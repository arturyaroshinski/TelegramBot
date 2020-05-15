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
    public class AdviceCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "/advice";

        // Return random advice as string.
        private async Task<string> GetAdviceAsync()
        {
            // TODO: add try catch block.
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://api.adviceslip.com/advice");
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();

            var slip = JsonConvert.DeserializeObject<SlipRequest>(content);
            return slip.Slip.Advice;
        }

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var advice = await GetAdviceAsync();
            await client.SendTextMessageAsync(chatId, advice);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Name);
    }
}