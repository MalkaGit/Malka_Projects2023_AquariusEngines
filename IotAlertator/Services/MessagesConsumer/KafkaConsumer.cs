using Confluent.Kafka;
using IotAlertator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace IotAlertator.Services.MessagesConsumer
{
    /// <summary>
    /// no need to change the class when new type of signal is added.
    /// Notes:
    ///     - offsets are automatically committed.
    ///     - no extra thread is created for the Poll (Consume) loop.
    /// </summary>
    internal class KafkaConsumer : IConsumer
    {
        private readonly CancellationToken           _cancellationToken;
        private readonly KafkaConsumerConfig         _config;
        private readonly IMessageHandler             _messageHandler;
        private IConsumer<Ignore, string>?          _consumer;

        internal KafkaConsumer(CancellationToken cancellationToken, Services.MessagesConsumer.KafkaConsumerConfig config, IMessageHandler messageHandler)
        {
            _cancellationToken  = cancellationToken;
            _config             = config;
            _messageHandler     = messageHandler;
            _consumer           = null;
        }

        private bool Initialize()
        {
            //TODO: initalized as critical section

            try
            {
                _consumer = null;

                List<string> topicNames = new List<string>() { _config.TopicName };
                
                var config = new Confluent.Kafka.ConsumerConfig
                {
                    BootstrapServers            = _config.BootstrapServers,
                    GroupId                     = "csharp-consumer",
                    
                    EnableAutoOffsetStore       = false,
                    EnableAutoCommit            = true,
                    StatisticsIntervalMs        = 5000,
                    SessionTimeoutMs            = 6000,
                    AutoOffsetReset             = AutoOffsetReset.Earliest,
                    EnablePartitionEof          = true,
                    PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
                };

                _consumer = new ConsumerBuilder<Ignore, string>(config)
                    .SetErrorHandler((_, e) => { Console.WriteLine($"Error: {e.Reason}"); /*TODO: */})
                    .Build();

                _consumer.Subscribe(topicNames);

                return true;
            }
            catch (Exception ex)
            {
                //TODO: log
                return false;
            }
        }

        public void Start()
        {
            //TODO: handle call to start serveral times 

            if (this.Initialize() == false || _consumer == null)
                throw new InvalidOperationException("failed to initialize consumer");

            try
            {
                while (true)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(_cancellationToken);

                        if (consumeResult.IsPartitionEOF) {
                            //TODO: Log
                            Console.WriteLine($"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");
                            continue;
                        }

                        _messageHandler.HandleMessage(consumeResult.Message.Value);
                        
                        try
                        {
                            // Store the offset associated with consumeResult to a local cache. Stored offsets are committed to Kafka by a background thread every AutoCommitIntervalMs. 
                            // The offset stored is actually the offset of the consumeResult + 1 since by convention, committed offsets specify the next message to consume. 
                            // If EnableAutoOffsetStore had been set to the default value true, the .NET client would automatically store offsets immediately prior to delivering messages to the application. 
                            // Explicitly storing offsets after processing gives at-least once semantics, the default behavior does not.
                            _consumer.StoreOffset(consumeResult);
                        }
                        catch (KafkaException e)
                        {
                            //TODO: Log
                            Console.WriteLine($"Store Offset error: {e.Error.Reason}");
                        }
                    }
                    catch (ConsumeException e)
                    {
                        //TODO: log
                        Console.WriteLine($"Consume error: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                //TODO: log
                Console.WriteLine("Closing consumer.");
                _consumer.Close();
            }
        }

        public void Dispose()
        {
            //https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
            if (_consumer != null) 
                _consumer.Dispose();
        }

    }
}














//Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
