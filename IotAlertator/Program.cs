// See https://aka.ms/new-console-template for more information

using IotAlertator.Model;
using IotAlertator.Repository;
using IotAlertator.Services;
using IotAlertator.Services.MessagesConsumer;
using IotAlertator.Services.MessageValidator;

CancellationTokenSource cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => {//Ctrl-C event
    e.Cancel = true;                 // prevent the process from terminating.
    cts.Cancel();
};

SineMessageValidatorConfig      sineConfig          = new SineMessageValidatorConfig()  { Frequency = 100, MinAmplitude = 0, MaxAmplitude = 32 };
StateMessageValidatorConfig     stateConfig         = new StateMessageValidatorConfig() { MinValue = 256, MaxValue = 4095 };
MessageValidatorsFactory        validatorFactory    = new MessageValidatorsFactory(sineConfig, stateConfig);



WebClientRepositoryConfig webClientRepositoryConfig = new WebClientRepositoryConfig() { BaseAddress = "http://localhost:5028" };
IRepository repository = new WebClientRepository(webClientRepositoryConfig);

//SqlRepositoryConfig repositryConfig = new SqlRepositoryConfig() { ConnectionString = "Server=DESKTOP-LU6BPF1\\SQLEXPRESS;Database=Research;Trusted_Connection=True;MultipleActiveResultSets=true" };
//IRepository repository = new SQLRepository(repositryConfig);


IMessageHandler messageHandler       = new MessageHandler(validatorFactory.GetValidatorsMap(),repository);


KafkaConsumerConfig consumerConfig  = new KafkaConsumerConfig() { BootstrapServers = "127.0.0.1:9092", TopicName = "ae_messages" };
IConsumer consumer                  = new KafkaConsumer(cts.Token,consumerConfig, messageHandler);



consumer.Start();
Console.WriteLine($"Started consumer, Ctrl-C to stop consuming");