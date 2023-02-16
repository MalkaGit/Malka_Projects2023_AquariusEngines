using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators
{
    internal interface ISignalGeneratorConfig
    {
        int NormalIntervalInMs { get; set; }
        int MinAbnormalIntervalInMs { get; set; }
        int MaxAbnormalIntervalInMs { get; set; }

    }
}
