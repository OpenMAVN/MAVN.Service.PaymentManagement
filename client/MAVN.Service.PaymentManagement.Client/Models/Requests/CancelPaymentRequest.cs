using System;
using System.ComponentModel.DataAnnotations;

namespace MAVN.Service.PaymentManagement.Client.Models.Requests
{
    /// <summary>
    /// Request model to cancel a payment request
    /// </summary>
    public class CancelPaymentRequest
    {
        /// <summary>
        /// Id of the payment request
        /// </summary>
        [Required]
        public Guid PaymentRequestId { get; set; }
    }
}
