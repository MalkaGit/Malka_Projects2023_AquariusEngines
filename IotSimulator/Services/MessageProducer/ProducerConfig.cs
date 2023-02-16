using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.MessageProducer
{
    internal class ProducerConfig
    {
        public string BootstrapServers { get; set; }
        public string TopicName;

    }
}
