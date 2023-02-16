using IotSimulator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.MessageProducer
{
    internal interface IProducer : IDisposable
    {
        bool Initialize();
        void ProduceAsync(IotMessage iotMessage);
    }
}
