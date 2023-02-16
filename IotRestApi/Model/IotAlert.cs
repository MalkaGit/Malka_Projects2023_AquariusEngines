namespace IotRestApi.Model
{
    public class IotAlert
    {
        public IotAlert() { }

        public IotAlert(int SensorId, DateTime SignalTime, double SignalValue, eSignalType SignalType)
        {
            this.SensorId = SensorId;
            this.SignalTime = SignalTime;
            this.SignalValue = SignalValue;
            this.SignalType = SignalType;
        }

        public long AlertId { get; set; }
        public int SensorId { get; set; }
        public DateTime SignalTime { get; set; }
        public double SignalValue { get; set; }
        public eSignalType SignalType { get; set; }

    }
}
