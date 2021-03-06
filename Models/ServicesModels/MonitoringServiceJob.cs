﻿namespace Models.ServicesModels
{
    public struct MonitoringServiceJob
    {
        public string Id { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Cron { get; set; }
        public bool ExecuteJob { get; set; }
        public bool MonitoringServiceId { get; set; }
        public bool NotifyOnFailure { get; set; }
        public string NotificationEmail { get; set; }
    }
}
