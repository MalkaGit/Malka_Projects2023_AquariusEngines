using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators
{
    internal interface ISignalGenerator
    {
       Task Start(int sensorId);
    }
}
