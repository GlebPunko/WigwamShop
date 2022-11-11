using Confluent.Kafka;

namespace Kafka.Consumer
{
    public class KafkaConsumerConfig<TKey, TValue> : ConsumerConfig
    {
        public string Topic { get; set; } = null!;
        public string Broker { get; } = "broker:29092";

        public KafkaConsumerConfig()
        {
            AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
            EnableAutoOffsetStore = false;
            AllowAutoCreateTopics = true;
        }
    }
}
