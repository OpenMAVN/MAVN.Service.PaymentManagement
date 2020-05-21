using System.ComponentModel.DataAnnotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Requests
{
    /// <summary>
    /// Request model to get payment info
    /// </summary>
    public class GetPaymentInfoRequest
    {
        /// <summary>
        /// External payment id of the request
        /// </summary>
        [Required]
        public string ExternalPaymentEntityId { get; set; }
    }
}
