﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Yaroshinski.Core.Interfaces;

namespace Yaroshinski.Core.Commands
{
    /// <inheritdoc cref="ITelegramCommand"/>
    public class AdviceCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "/advice";

        // Return random advice as string.
        private async Task<string> GetAdvice()
        {
            var httpClient = new HttpClient();

            var respone = await httpClient.GetAsync("https://api.adviceslip.com/advice");
            var content = await respone.Content.ReadAsStringAsync();
            content = content.Substring(8, content.Length - 9);

            // TODO: slip
            var slip = JsonConvert.DeserializeObject<Slip>(content);
            return slip.Advice;
        }

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var advice = await GetAdvice();
            await client.SendTextMessageAsync(chatId, advice);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Name);
    }
}