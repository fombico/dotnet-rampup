using System;
using Steeltoe.Management.Endpoint.Health;

namespace NotesApp.Actuators
{
    public class CustomHealthContributor : IHealthContributor
    {
        public Health Health()
        {
            Health health = new Health();

            health.Details.Add("time", DateTime.Now);
            health.Status = HealthStatus.UP;

            return health;
        }

        public string Id { get; } = "serverDetails";
    }
}