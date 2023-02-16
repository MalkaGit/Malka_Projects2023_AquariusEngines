using IotSimulator.Model;
using IotSimulator.Services.MessageProducer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators
{
    internal class SineGenerator : ISignalGenerator
    {
        private  readonly   SineGeneratorConfig _config;
        private readonly    IProducer           _producer;
        protected readonly  Random              _random;


        internal SineGenerator(SineGeneratorConfig config, IProducer producer)
        {
            _config     = config;
            _producer   = producer;
            _random     = new Random();
        }

        public async Task Start(int sensorId)
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
                    value = CalcNormalValue(now);
                else
                    value = CalcAbnormalValue(now);
                
                var msg = new IotMessage(sensorId, now, value, eSignalType.Sine);
                this._producer.ProduceAsync(msg);
            }
        }

        protected  double CalcAbnormalValue(DateTime dt)
        {
            if (_random.Next(2) == 1)
            {
                return _config.MaxAmplitude + _random.Next(1, 100);
            }
            else
            {
                return _config.MinAmplitude - _random.Next(1, 100);
            }
        }

        protected  double CalcNormalValue(DateTime dt)
        {
            return 1;
        }
    }
}
