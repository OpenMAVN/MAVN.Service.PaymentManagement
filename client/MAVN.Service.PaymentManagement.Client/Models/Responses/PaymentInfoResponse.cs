namespace MAVN.Service.PaymentManagement.Client.Models.Responses
{
    /// <summary>
    /// Response model containing information for payment
    /// </summary>
    public class PaymentInfoResponse
    {
        /// <summary>
        /// Url for the payment request
        /// </summary>
        public string PaymentUrl { get; set; }
    }
}
