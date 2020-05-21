using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.PaymentManagement.Domain;

namespace MAVN.Service.PaymentManagement.MsSqlRepositories.Entities
{
    [Table("payment_requests")]
    public class PaymentRequestEntity : IPaymentRequest
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        public string PaymentId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ExternalPaymentEntityId { get; set; }
    }
}
