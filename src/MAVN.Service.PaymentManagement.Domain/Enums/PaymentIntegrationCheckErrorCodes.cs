namespace MAVN.Service.PaymentManagement.Domain.Enums
{
    public enum PaymentIntegrationCheckErrorCodes
    {
        None,
        Fail,
        PartnerConfigurationNotFound,
        PartnerConfigurationPropertyIsMissing,
        PaymentIntegrationProviderIsMissing,
    }
}
