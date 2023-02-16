using IotSimulator.Services.MessageProducer;
using IotSimulator.Services.SignalGenerators_option2.CommonHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Services.SignalGenerators_option2
{
    //avoid code duplocation without using abstract class 
    internal class SineGenerator : ISignalGenerator
    {
        private readonly SineGeneratorConfig _config;
        private readonly IProducer _producer;
        private SignalGeneratorEngine _generatorEngine;

        protected readonly Random _random;


        internal SineGenerator(SineGeneratorConfig config, IProducer producer, SignalGeneratorEngine generatorEngine)
        {
            _config = config;
            _producer = producer;
            _generatorEngine = generatorEngine;
            _random = new Random();
        }

        public async Task Start(int sensorId)
        {
            await _generatorEngine.Start(sensorId, CalcNormalValue, CalcAbnormalValue);
        }


        protected double CalcAbnormalValue(DateTime dt)
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

        protected double CalcNormalValue(DateTime dt)
        {
            return 1;
        }
    }
}
