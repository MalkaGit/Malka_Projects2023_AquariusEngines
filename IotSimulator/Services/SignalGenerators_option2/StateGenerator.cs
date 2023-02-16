using Confluent.Kafka;
using IotSimulator.Model;
using IotSimulator.Services.MessageProducer;
using IotSimulator.Services.SignalGenerators_option2.CommonHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators_option2
{
    internal class StateGenerator : ISignalGenerator
    {
        private StateGeneratorConfig _config;
        private IProducer _producer;
        private SignalGeneratorEngine _generatorEngine;
        protected Random _random;

        internal StateGenerator(StateGeneratorConfig config, IProducer producer, SignalGeneratorEngine generatorEngine)
        {
            _config = config;
            _producer = producer;
            _generatorEngine = generatorEngine;
            _random = new Random();
        }

        //no more duplication
        public async Task Start(int sensorId)
        {
            await _generatorEngine.Start(sensorId, CalcNormalValue, CalcAbnormalValue);
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

        protected double CalcNormalValue(DateTime dt)
        {
            double result = _random.Next(_config.MinValue, _config.MaxValue - 1) + _random.NextDouble();
            return result;
        }
    }
}
