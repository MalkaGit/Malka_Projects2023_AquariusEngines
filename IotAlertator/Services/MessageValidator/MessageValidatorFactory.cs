using IotAlertator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services.MessageValidator
{
    internal class MessageValidatorsFactory
    {
        Dictionary<eSignalType, IMessageValidator> _validatorsMap;


        internal MessageValidatorsFactory(SineMessageValidatorConfig sineConfig, StateMessageValidatorConfig stateConfig)
        {
            _validatorsMap = new Dictionary<eSignalType, IMessageValidator>();
            _validatorsMap[eSignalType.State]   = new StateMessageValidator(stateConfig);
            _validatorsMap[eSignalType.Sine]    = new SineMessageValidator(sineConfig);
        }

        public Dictionary<eSignalType, IMessageValidator> GetValidatorsMap()
        {
            return _validatorsMap;
        }
    }
}
