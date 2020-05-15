namespace Yaroshinski.Core.Models
{
    /// <summary>
    /// Slip request object.
    /// </summary>
    public class SlipRequest
    {
        public Slip Slip { get; set; }
    }

    /// <summary>
    /// Slip object.
    /// </summary>
    public class Slip
    {
        /// <summary>
        /// The unique ID of this advice slip.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The advice being given.
        /// </summary>
        public string Advice { get; set; }
    }
}
