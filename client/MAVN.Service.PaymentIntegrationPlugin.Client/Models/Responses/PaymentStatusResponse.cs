namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses
{
    /// <summary>
    /// Payment status response model
    /// </summary>
    public class PaymentStatusResponse
    {
        /// <summary>Error code</summary>
        public CheckIntegrationErrorCode ErrorCode { get; set; }

        /// <summary>Payment status</summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>Payment url</summary>
        public PaymentStatus PaymentUrl { get; set; }
    }
}
