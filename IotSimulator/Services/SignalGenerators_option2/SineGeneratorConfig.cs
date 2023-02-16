using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators_option2
{
    internal class SineGeneratorConfig : ISignalGeneratorConfig
    {
        public int NormalIntervalInMs { get; set; }
        public int MinAbnormalIntervalInMs { get; set; }
        public int MaxAbnormalIntervalInMs { get; set; }

        public int Frequency { get; set; }
        public int MinAmplitude { get; set; }
        public int MaxAmplitude { get; set; }
    }
}
