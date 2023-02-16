using IotAlertator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services.MessageValidator
{
    internal class SineMessageValidator : IMessageValidator
    {
        private readonly SineMessageValidatorConfig _config;

        public SineMessageValidator(SineMessageValidatorConfig config)
        {
            _config = config;
        }

        public bool IsValid(IotMessage iotMessage)
        {
            //TODO: implement
            return true;
        }
    }
}
