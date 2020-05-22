using Flurl;
using Flurl.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Yaroshinski.Core.Constants;
using Yaroshinski.Core.Models;

namespace Yaroshinski.Core.Services
{
    /// <summary>
    /// Http request handler.
    /// </summary>
    class HttpHandler
    {
        /// <summary>
        /// Return random advice from API.
        /// </summary>
        /// <returns>Advice as string.</returns>
        public static async Task<string> GetRandomAdviceAsync()
        {
            try
            {
                var response = await Constant.API_DOMAIN
                    .AppendPathSegment(Constant.RANDOM_ADVICE_PATH)
                    .GetAsync()
                    .ReceiveJson<SlipRequest>();

                return response.Slip.Advice;
            }
            catch (Exception)
            {
                return Constant.ERROR_MESSAGE;
            }
        }

        /// <summary>
        /// Return advice by a key-word.
        /// </summary>
        /// <param name="key">Key-word</param>
        /// <returns>Advice as string.</returns>
        public static async Task<string> GetAdviceByKeyWordAsync(string key)
        {
            try
            {
                var response = await Constant.API_DOMAIN
                    .AppendPathSegments(Constant.RANDOM_ADVICE_PATH, Constant.SEARCH_ADVICE_PATH, key)
                    .GetAsync()
                    .ReceiveJson<SearchObject>();

                var rnd = new Random(Guid.NewGuid().ToByteArray().Sum(x => x));

                return response.Slips[rnd.Next(0, response.Slips.Length)].Advice;
            }
            catch (Exception)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    key = " ";
                }
                return string.Format(Constant.NO_ADVICE_BY_KEY_WORD, key);
            }
        }
    }
}
