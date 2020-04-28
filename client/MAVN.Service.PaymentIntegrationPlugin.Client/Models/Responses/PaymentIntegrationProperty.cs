using JetBrains.Annotations;

namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses
{
    /// <summary>
    /// Payment integration property
    /// </summary>
    [PublicAPI]
    public class PaymentIntegrationProperty
    {
        /// <summary>Property  name</summary>
        public string Name { get; set; }

        /// <summary>Property description</summary>
        public string Description { get; set; }

        /// <summary>Json property name for this field</summary>
        public string JsonKey { get; set; }

        /// <summary>IsOptional property flag</summary>
        public bool IsOptional { get; set; }

        /// <summary>IsSecret property flag</summary>
        public bool IsSecret { get; set; }
    }
}
