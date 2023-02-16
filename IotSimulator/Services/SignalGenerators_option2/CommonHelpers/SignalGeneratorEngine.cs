using Confluent.Kafka;
using IotSimulator.Model;
using IotSimulator.Services.MessageProducer;
using IotSimulator.Services.SignalGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators_option2.CommonHelpers
{
    internal class SignalGeneratorEngine
    {
        protected readonly ISignalGeneratorConfig _config;
        protected readonly IProducer _producer;
        protected readonly Random _random;


        public SignalGeneratorEngine(ISignalGeneratorConfig config, IProducer producer)
        {
            _config = config;
            _producer = producer;
            _random= new Random();
        }

        public async Task Start(int sensorId, Func<DateTime,double> calcNormalValue, Func<DateTime,double> calcAbnormalValue)
        {
            bool createNormalValue;
            double value = 0;
            DateTime now;

            while (true)
            {
                createNormalValue = (_random.Next(2) == 1);

                if (createNormalValue)
                    await Task.Delay(_config.NormalIntervalInMs);
                else
                    await Task.Delay(_config.MinAbnormalIntervalInMs + _random.Next(_config.MaxAbnormalIntervalInMs - _config.MinAbnormalIntervalInMs));
                
                now = DateTime.UtcNow;
                if (createNormalValue)
                    value = calcNormalValue(now);
                else
                    value = calcAbnormalValue(now);

                var msg = new IotMessage(sensorId, now, value, eSignalType.State);
                this._producer.ProduceAsync(msg);
            }
        }
       
    }
}
