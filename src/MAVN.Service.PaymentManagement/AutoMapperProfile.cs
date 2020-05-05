using AutoMapper;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Client.Models.Requests;
using MAVN.Service.PaymentManagement.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Domain;
using MAVN.Service.PaymentManagement.MsSqlRepositories.Entities;

namespace MAVN.Service.PaymentManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaymentProviderRequirements, PaymentProviderProperties>(MemberList.Source)
                .ForMember(c => c.Properties, c => c.MapFrom(s => s.Requirements));
            CreateMap<PaymentProviderRequirement, PaymentProviderProperty>(MemberList.Source);
            CreateMap<PaymentIntegrationProperty, PaymentProviderRequirement>(MemberList.Source);

            CreateMap<PaymentProviderSupportedCurrencies, PaymentIntegrationSupportedCurrencies>(MemberList.Source);

            CreateMap<PaymentGenerationRequest, GeneratePaymentData>(MemberList.Source);
            CreateMap<PaymentGenerationResult, PaymentGenerationResponse>(MemberList.Source);

            CreateMap<PaymentValidationRequest, PaymentValidationData>(MemberList.Source);

            CreateMap<IPaymentRequest, PaymentRequestEntity>(MemberList.Source);
        }
    }
}
