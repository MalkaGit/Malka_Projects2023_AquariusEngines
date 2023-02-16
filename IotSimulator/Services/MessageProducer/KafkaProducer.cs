using IotSimulator.Model;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IotSimulator.Services.MessageProducer
{
    internal class KafkaProducer : IProducer
    {
        private readonly ProducerConfig     _config;
        private IProducer<string, string>?  _producer;

        internal KafkaProducer(ProducerConfig config)
        {
            _config = config;
            _producer = null;
        }

        public bool Initialize()
        {
            //TODO: initalized as critical section
            try
            {
                var config = new Confluent.Kafka.ProducerConfig
                {
                    BootstrapServers =  _config.BootstrapServers,
                    ClientId =          Dns.GetHostName(),
                };
                _producer = new ProducerBuilder<string, string>(config).Build();
                return true;
            }
            catch (Exception ex)
            {
                //TODO: log error
                return false;
            }
        }

        public void ProduceAsync(IotMessage iotMessage)
        {
            if (_producer == null)
                throw new Exception("ProduceAsync called but producer is not initalized");

            try
            {
               
                string topicName    = _config.TopicName;
                string jsonMessage  = JsonSerializer.Serialize(iotMessage);

                _producer.Produce(
                    topicName,
                    new Message<string, string> { Key = iotMessage.SensorId.ToString(), Value = jsonMessage },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            //TODO: log error
                            //Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                    });

                _producer.Flush(TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {
                //TODO: log error
            }
        }

        public void Dispose()
        {
            //https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
            if (_producer != null)
                _producer.Dispose();
        }
    }
}
