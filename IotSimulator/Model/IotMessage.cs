using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IotSimulator.Model
{
    public class IotMessage
    {
        public IotMessage() {}

        public IotMessage(int SensorId, DateTime Time, double SignalValue, eSignalType SignalType)
        { 
            this.SensorId       = SensorId;
            this.Time           = Time;   
            this.SignalValue    = SignalValue;
            this.SignalType     = SignalType;
        }
        public int          SensorId    { get; set; }
        public DateTime     Time        { get; set; }
        public double       SignalValue { get; set; }
        public eSignalType  SignalType  { get; set; }

    }

    public enum eSignalType
    {
        Sine,
        State
    }
}
