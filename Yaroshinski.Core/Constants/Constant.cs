namespace Yaroshinski.Core.Constants
{
    /// <summary>
    /// All constants.
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// Command for random advice.
        /// </summary>
        public const string ADVICE_COMMAND = "/advice";

        /// <summary>
        /// Command for getting advice by key-word.
        /// </summary>
        public const string KEY_COMMAND = "/key";

        /// <summary>
        /// Command for starting communication with bot.
        /// </summary>
        public const string START_COMMAND = "/start";

        /// <summary>
        /// User greetings.
        /// </summary>
        public const string SAY_HI = "Hello @{0}! Type '/advice' for getting random advice.";

        /// <summary>
        /// Error message for receiving data from api.
        /// </summary>
        public const string ERROR_MESSAGE = "Error: advice not received for some reason.";

        /// <summary>
        /// Message about lack of advice with key-word.
        /// </summary>
        public const string NO_ADVICE_BY_KEY_WORD = "No advices with word '{0}' was found.";

        /// <summary>
        /// Domain link for API.
        /// </summary>
        public const string API_DOMAIN = "https://api.adviceslip.com";

        /// <summary>
        /// Part of GET request for random advice.
        /// </summary>
        public const string RANDOM_ADVICE_PATH = "advice";

        /// <summary>
        /// Part of GET request for advice by key-word.
        /// </summary>
        public const string SEARCH_ADVICE_PATH = "search";
    }
}
