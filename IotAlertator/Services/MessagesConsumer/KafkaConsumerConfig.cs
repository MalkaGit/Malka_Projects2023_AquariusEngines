using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services.MessagesConsumer
{
    internal class KafkaConsumerConfig
    {
        public string? BootstrapServers { get; set; }
        public string? TopicName;
    }
}
