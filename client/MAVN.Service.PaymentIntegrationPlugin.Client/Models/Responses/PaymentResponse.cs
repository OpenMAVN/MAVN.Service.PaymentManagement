using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses
{
    /// <summary>
    /// Payment response model
    /// </summary>
    [PublicAPI]
    public class PaymentResponse
    {
        /// <summary>Error code</summary>
        public CheckIntegrationErrorCode ErrorCode { get; set; }

        /// <summary>Payment page url</summary>
        public string PaymentPageUrl { get; set; }

        /// <summary>Payment id</summary>
        public string PaymentId { get; set; }
    }
}
