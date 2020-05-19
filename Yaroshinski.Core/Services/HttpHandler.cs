using System;
using System.Collections.Generic;
using System.Text;
using Flurl.Http;
using Flurl;
using System.Threading.Tasks;
using Yaroshinski.Core.Models;
using System.Linq;

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
                var response = await "https://api.adviceslip"
                    .AppendPathSegment("advice")
                    .GetAsync()
                    .ReceiveJson<Slip>();

                return response.Advice;
            }
            catch (Exception)
            {
                return "Error: advice not received for some reason.";
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
                var response = await "https://api.adviceslip.com"
                    .AppendPathSegments("advice", "search", key)
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
                return $"No advices with word '{key}' was found";
            }
        }
    }
}
