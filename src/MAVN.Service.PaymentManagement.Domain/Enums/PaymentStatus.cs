namespace MAVN.Service.PaymentManagement.Domain.Enums
{
    public enum PaymentStatus
    {
        None,
        NotFound,
        Pending,
        Processing,
        Success,
        Cancelled,
        Rejected,
        Reserved,
    }
}
