namespace Publisher
{
    public class RabbitMqInfo
    {
        public string Uri { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public int QueueNumber { get; set; }
    }
}