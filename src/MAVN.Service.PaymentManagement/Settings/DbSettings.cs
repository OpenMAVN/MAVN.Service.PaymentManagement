﻿using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.PaymentManagement.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        public string SqlDbConnString { get; set; }
    }
}
