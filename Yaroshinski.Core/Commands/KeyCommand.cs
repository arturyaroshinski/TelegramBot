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
    public class KeyCommand : ITelegramCommand
    {
        /// <inheritdoc/>
        public string Name { get; } = Constant.KEY_COMMAND;

        /// <inheritdoc/>
        public async Task Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var key = GetKeyWord(message.Text);

            string advice = await HttpHandler.GetAdviceByKeyWordAsync(key);

            await client.SendTextMessageAsync(chatId, advice);
        }

        private string GetKeyWord(string text)
        {
            var words = text.Split(" ");
            string key = string.Empty;

            foreach (var word in words)
            {
                if (words.Length == 2 && word != Constant.KEY_COMMAND)
                {
                    key += word;
                }
            }

            return key;
        }

        /// <inheritdoc/>
        public bool Contains(Message message) => message.Type != MessageType.Text ? false : message.Text.Contains(Name);
    }
}