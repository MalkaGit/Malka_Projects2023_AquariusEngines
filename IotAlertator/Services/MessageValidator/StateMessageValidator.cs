using IotAlertator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services.MessageValidator
{
    internal class StateMessageValidator : IMessageValidator
    {
        private readonly StateMessageValidatorConfig _config;

        public StateMessageValidator(StateMessageValidatorConfig config)
        {
            _config = config;   
        }

        public bool IsValid(IotMessage iotMessage)
        {
           if (iotMessage.SignalValue < _config.MinValue)  return false;
            if (iotMessage.SignalValue > _config.MaxValue) return false;
            return true;
        }
    }
}
