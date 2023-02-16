using IotAlertator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotAlertator.ModelMappers
{
    /// <summary>
    /// mapper using extention methods
    /// </summary>
    internal static class IotMessageMapper
    {
        internal static IotAlert? ToIotAlert(this IotMessage iotMessage)
        {
            return iotMessage == null ?
                null :
                new IotAlert(iotMessage.SensorId, iotMessage.SignalTime, iotMessage.SignalValue, iotMessage.SignalType);
        }
    }
}
