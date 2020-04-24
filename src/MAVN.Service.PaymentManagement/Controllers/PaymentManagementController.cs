using AutoMapper;
using MAVN.Service.PaymentManagement.Client;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.PaymentManagement.Controllers
{
    [Route("api/PaymentManagement")] // TODO fix route
    public class PaymentManagementController : Controller, IPaymentManagementApi
    {
        private readonly IMapper _mapper;

        public PaymentManagementController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
