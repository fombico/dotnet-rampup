using System;
using System.Collections.Generic;
using Steeltoe.Management.Endpoint.Info;

namespace NotesApp.Actuators
{
    public class CustomInfoContributor : IInfoContributor
    {
        public void Contribute(IInfoBuilder builder)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("time", DateTime.Now);
            builder.WithInfo(result);
        }
    }
}