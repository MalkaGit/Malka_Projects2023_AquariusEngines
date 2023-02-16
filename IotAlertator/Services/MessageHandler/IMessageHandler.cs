using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services
{
    internal interface IMessageHandler
    {
        bool HandleMessage(string jsonMesssage);
    }
}
