using IotAlertator.Repository;
using IotAlertator.Services.MessageValidator;
using IotAlertator.ModelMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IotAlertator.Model;

namespace IotAlertator.Services
{
    /// <summary>
    /// no need to change the class when new type of signal is added.
    //</summary>
    internal class MessageHandler : IMessageHandler
    {
        private readonly Dictionary<eSignalType, IMessageValidator> _validatorsMap;
        private readonly IRepository _repository;

        public MessageHandler(Dictionary<eSignalType,IMessageValidator> validatorsMap, IRepository repository)
        {
            _validatorsMap = validatorsMap;
            _repository = repository;
        }

        public bool HandleMessage(string jsonMesssage)
        {
            try
            {
                IotMessage? message = JsonSerializer.Deserialize<IotMessage>(jsonMesssage);
                if (message == null)
                {
                    //TODO: Log
                    return false;
                }


                var validator = _validatorsMap [message.SignalType];
                if (validator != null)
                {
                    //TODO: Log
                    return false;
                }

                if(validator.IsValid(message))
                        return true;
                
                
                var iotAlert = message.ToIotAlert();
                int affectedRows = _repository.CreateIotAlert(iotAlert);
                if (affectedRows != 1)
                {
                    //TODO: write to DB 
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                //TODO: Log
                return false;
            }
        }
    }
}
