using System.Collections.Generic;
using Yaroshinski.Core.Commands;
using Yaroshinski.Core.Interfaces;

namespace Yaroshinski.Core.Services
{
    /// <inheritdoc cref="ICommandService"/>
    public class CommandService : ICommandService
    {
        private readonly IEnumerable<ITelegramCommand> _commands;

        /// <summary>
        /// Base constructor.
        /// </summary>
        public CommandService()
        {
            _commands = new List<ITelegramCommand>
            {
                // TODO: add new commands.

                new StartCommand(),
                new AdviceCommand(),
            };
        }

        /// <inheritdoc/>
        public IEnumerable<ITelegramCommand> Get() => _commands;
    }
}
