using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.PaymentManagement.Client;
using MAVN.Service.PaymentManagement.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.PaymentManagement.Controllers
{
    [Route("api/payments")]
    public class PaymentManagementController : Controller, IPaymentManagementApi
    {
        private readonly IPaymentProvidersService _paymentProvidersService;
        private readonly IMapper _mapper;

        public PaymentManagementController(
            IPaymentProvidersService paymentProvidersService,
            IMapper mapper)
        {
            _paymentProvidersService = paymentProvidersService;
            _mapper = mapper;
        }

        public async Task<AvailablePaymentProvidersRequirementsResponse> GetAvailablePaymentProvidersRequirementsAsync()
        {
            var requirements = await _paymentProvidersService.GetPaymentProvidersRequirementsAsync();

            return new AvailablePaymentProvidersRequirementsResponse
            {
                ProvidersRequirements = _mapper.Map<List<PaymentProviderProperties>>(requirements),
            };
        }
    }
}
