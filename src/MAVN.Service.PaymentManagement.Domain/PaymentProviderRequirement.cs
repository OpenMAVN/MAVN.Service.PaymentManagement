namespace MAVN.Service.PaymentManagement.Domain
{
    public class PaymentProviderRequirement
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string JsonKey { get; set; }

        public bool IsOptional { get; set; }

        public bool IsSecret { get; set; }
    }
}
