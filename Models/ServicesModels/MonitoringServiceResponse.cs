﻿namespace Interfaces.Services
{
    public class MonitoringServiceResponse
    {
        public string Id { get; set; }
        public string Host { get; set; }
        public bool IsSuccessful { get; set; }
        public long MiliSecondsResponse { get; set; }
        public string Message { get; set; }
    }
}