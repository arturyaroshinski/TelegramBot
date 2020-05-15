using System.Collections.Generic;

namespace Yaroshinski.Core.Models
{
    /// <summary>
    /// A search object contains the results of a slip search query.
    /// </summary>
    public class SearchObject
    {
        /// <summary>
        /// User query.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Collection of Slip objects.
        /// </summary>
        public Slip[] Slips { get; set; }
    }
}
