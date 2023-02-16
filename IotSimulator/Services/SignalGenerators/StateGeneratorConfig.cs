using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators
{
    internal class StateGeneratorConfig : ISignalGeneratorConfig
    {
        public int NormalIntervalInMs { get; set; }
        public int MinAbnormalIntervalInMs { get; set; }
        public int MaxAbnormalIntervalInMs { get; set; }


        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}

