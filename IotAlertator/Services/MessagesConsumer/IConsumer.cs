using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.Services.MessagesConsumer
{
    internal interface IConsumer : IDisposable
    {
        void Start();
    }
}
