namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses
{
    /// <summary>
    /// Check integration error code enum
    /// </summary>
    public enum CheckIntegrationErrorCode
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
