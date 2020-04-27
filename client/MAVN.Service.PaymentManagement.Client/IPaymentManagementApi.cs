using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.PaymentManagement.Client.Models.Responses;
using Refit;

namespace MAVN.Service.PaymentManagement.Client
{
    /// <summary>
    /// PaymentManagement client API interface.
    /// </summary>
    [PublicAPI]
    public interface IPaymentManagementApi
    {
        /// <summary>
        /// Retrieves a list of payment providers requirements.
        /// </summary>
        [Get("/api/payments/requirements")]
        Task<AvailablePaymentProvidersRequirementsResponse> GetAvailablePaymentProvidersRequirementsAsync();
    }
}
