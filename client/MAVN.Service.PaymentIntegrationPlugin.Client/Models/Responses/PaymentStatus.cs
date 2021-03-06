﻿namespace MAVN.Service.PaymentIntegrationPlugin.Client.Models.Responses
{
    /// <summary>
    /// Payment status
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>None</summary>
        None,
        /// <summary>NotFound</summary>
        NotFound,
        /// <summary>Pending</summary>
        Pending,
        /// <summary>Processing</summary>
        Processing,
        /// <summary>Success</summary>
        Success,
        /// <summary>Cancelled</summary>
        Cancelled,
        /// <summary>Rejected</summary>
        Rejected,
        /// <summary>Reserved</summary>
        Reserved,
    }
}
