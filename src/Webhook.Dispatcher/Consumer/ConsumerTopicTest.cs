using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Webhook.Dispatcher.Consumer
{
    public class ConsumerTopicTest
    {
        private readonly ILogger<ConsumerTopicTest> _logger;

        public ConsumerTopicTest(ILogger<ConsumerTopicTest> logger)
        {
            _logger = logger;
        }
        public async Task Consume(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "group_id",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };

            var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe("teste");

            while (true)
            {
                var cr = consumer.Consume(cancellationToken);
                if(cr.IsPartitionEOF)
                {
                    break;
                }
                _logger.LogInformation(
                    $@"Mensagem lida: {cr.Message.Value}
                    Partition: {cr.Partition}
                    Topic Partition: {cr.TopicPartition}
                    Offset: {cr.Offset}
                    TopicPartitionOffset: {cr.TopicPartitionOffset}
                    ");
            }
            _logger.LogInformation("Mensagem processadas com sucesso");
        }
    }
}