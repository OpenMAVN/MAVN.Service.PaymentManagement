namespace MAVN.Service.PaymentManagement.Client.Models.Responses
{
    /// <summary>
    /// Check payment integration error code enum
    /// </summary>
    public enum CheckPaymentIntegrationErrorCode
    {
        /// <summary>None</summary>
        None,
        /// <summary>Success</summary>
        Success,
        /// <summary>Fail</summary>
        Fail,
        /// <summary>PartnerConfigurationNotFound</summary>
        PartnerConfigurationNotFound,
        /// <summary>PartnerConfigurationPropertyIsMissing</summary>
        PartnerConfigurationPropertyIsMissing,
    }
}
