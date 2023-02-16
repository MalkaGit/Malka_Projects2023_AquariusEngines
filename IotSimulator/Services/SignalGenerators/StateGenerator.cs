using IotSimulator.Model;
using IotSimulator.Services.MessageProducer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators
{
    internal class StateGenerator : ISignalGenerator
    {
        private StateGeneratorConfig _config;
        private IProducer _producer;
        protected Random _random;

        internal StateGenerator(StateGeneratorConfig config, IProducer producer)
        {
            _config = config;
            _producer = producer;
            _random = new Random();
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

                var msg = new IotMessage(sensorId, now, value, eSignalType.State);
                this._producer.ProduceAsync(msg);
            }
        }

        protected double CalcAbnormalValue(DateTime dt)
        {
           if (_random.Next(2) == 1)
           {
                return _config.MaxValue + _random.Next(0, 100);
           }
           else
           {
                return _config.MinValue - _random.Next(0, 100);
            }
        }

        protected  double CalcNormalValue(DateTime dt)
        {
            double result = _random.Next(_config.MinValue, _config.MaxValue-1) + _random.NextDouble();
            return result;
        }
    }
}
