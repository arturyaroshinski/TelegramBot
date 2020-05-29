using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Yaroshinski.Core.Constants;
using Yaroshinski.Core.Interfaces;
using Yaroshinski.Core.Services;

namespace Yaroshinski.Core.Commands
{
    /// <inheritdoc cref="ITelegramCommand"/>
    public class AdviceCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = Constant.ADVICE_COMMAND;

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            string advice = await HttpHandler.GetRandomAdviceAsync();
            await client.SendTextMessageAsync(chatId, advice);
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Name);
    }
}