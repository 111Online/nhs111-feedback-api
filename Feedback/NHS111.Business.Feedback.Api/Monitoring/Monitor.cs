﻿using System.Reflection;
using System.Threading.Tasks;
using NHS111.Utils.Monitoring;

namespace NHS111.Business.Feedback.Api.Monitoring
{
    public class Monitor : BaseMonitor
    {

        public override string Metrics()
        {
            return "Metrics";
        }

        public override async Task<bool> Health()
        {
            return false;
        }

        public override string Version()
        {
            return Assembly.GetCallingAssembly().GetName().Version.ToString();
        }
    }
}