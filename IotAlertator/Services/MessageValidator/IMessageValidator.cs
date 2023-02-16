using IotAlertator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services.MessageValidator
{
    internal interface IMessageValidator
    {
        bool IsValid(IotMessage iotMessage);
    }
}
