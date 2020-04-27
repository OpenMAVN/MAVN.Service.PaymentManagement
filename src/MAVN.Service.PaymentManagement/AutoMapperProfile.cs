﻿using AutoMapper;
using MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Client.Models.Responses;
using MAVN.Service.PaymentManagement.Domain;

namespace MAVN.Service.PaymentManagement
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PaymentProviderRequirements, PaymentProviderProperties>();
            CreateMap<PaymentIntegrationProperty, PaymentProviderRequirement>();
        }
    }
}
