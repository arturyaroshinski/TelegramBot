using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Yaroshinski.Core.Interfaces;
using Yaroshinski.Core.Services;

namespace Yaroshinski.Core.Commands
{
    /// <inheritdoc cref="ITelegramCommand"/>
    public class KeyCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = "/key";

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            // TODO: рассплитить по пробелам
            var key = message.Text.Replace(" ", "").Substring(4);
            string advice = await HttpHandler.GetAdviceByKeyWordAsync(key);

            await client.SendTextMessageAsync(chatId, advice);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Name);
    }
}