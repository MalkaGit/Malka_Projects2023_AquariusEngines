// See https://aka.ms/new-console-template for more information

using IotSimulator.Services;
using IotSimulator.Services.MessageProducer;
using IotSimulator.Services.SignalGenerators;

ProducerConfig producerConfig = new ProducerConfig();
producerConfig.BootstrapServers = "127.0.0.1:9092";
producerConfig.TopicName = "ae_messages";

IProducer producer = new KafkaProducer(producerConfig);
bool success = producer.Initialize();
if (!success)
{
    //TODO: console
    return;
}

StateGeneratorConfig stateGeneratorConfig = new StateGeneratorConfig()
{   NormalIntervalInMs = 20000, MinAbnormalIntervalInMs = 20000, MaxAbnormalIntervalInMs = 50000, MinValue = 256, MaxValue = 4095 
};

SineGeneratorConfig sineGeneratorConfig = new SineGeneratorConfig()
{ NormalIntervalInMs = 20000, MinAbnormalIntervalInMs = 20000, MaxAbnormalIntervalInMs = 50000, Frequency = 100, MinAmplitude = 0, MaxAmplitude = 32 };


StateGenerator stateGenerator = new StateGenerator(stateGeneratorConfig, producer);
SineGenerator sineGenerator    = new SineGenerator(sineGeneratorConfig, producer);

var signalsGenerator   = new SignalsGenerator(new ISignalGenerator[] { stateGenerator, sineGenerator });
signalsGenerator.Start();
Console.ReadLine();
