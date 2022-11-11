using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace Kafka.Producer
{
    public class KafkaProducer<TK, TV> : IKafkaProducer<TK, TV>
    {
        private readonly IProducer<TK, TV> _producer;
        private readonly string _topic;

        public KafkaProducer(IProducer<TK, TV> producer, IOptions<KafkaProducerConfig<TK, TV>> topicOptions)
        {
            _producer = producer;
            _topic = topicOptions.Value.Topic;
        }

        public async Task ProduceAsync(TK key, TV value)
        {
            await _producer.ProduceAsync(_topic, new Message<TK, TV> { Key = key, Value = value });
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}