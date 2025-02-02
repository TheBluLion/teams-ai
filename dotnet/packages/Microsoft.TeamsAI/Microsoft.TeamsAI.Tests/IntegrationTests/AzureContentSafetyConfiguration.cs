﻿namespace Microsoft.TeamsAI.Tests.Integration
{
    internal class AzureContentSafetyConfiguration
    {
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }

        public AzureContentSafetyConfiguration(string apiKey, string endpoint)
        {
            ApiKey = apiKey;
            Endpoint = endpoint;
        }

        public AzureContentSafetyConfiguration() { }
    }
}
