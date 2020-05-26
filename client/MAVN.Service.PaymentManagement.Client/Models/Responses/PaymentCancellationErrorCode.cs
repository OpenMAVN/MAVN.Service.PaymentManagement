namespace MAVN.Service.PaymentManagement.Client.Models.Responses
{
    /// <summary>
    /// Holds error codes for payment cancellation
    /// </summary>
    public enum PaymentCancellationErrorCode
    {
        /// <summary>
        /// No errors
        /// </summary>
        None,
        /// <summary>
        /// Payment does not exist
        /// </summary>
        PaymentDoesNotExist,
    }
}
