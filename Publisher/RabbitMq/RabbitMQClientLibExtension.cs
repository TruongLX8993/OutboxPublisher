using RabbitMQClientLib;

namespace Publisher.RabbitMq
{
    public static class RabbitMQClientLibExtension
    {
        public static RabbitMqSimpleSender GetSender(this RabbitMQClient rabbitMqClient, RabbitMqInfo rabbitMqInfo)
        {
            if (!string.IsNullOrEmpty(rabbitMqInfo.ExchangeName))
            {
                rabbitMqClient.DeclareExchangeAndQueue(rabbitMqInfo.ExchangeName,
                    rabbitMqInfo.QueueName,
                    rabbitMqInfo.QueueNumber);
            }
            else
            {
                rabbitMqClient.DeclareQueue(rabbitMqInfo.QueueName);
            }

            return rabbitMqClient.CreateSender(rabbitMqInfo.ExchangeName ?? "", rabbitMqInfo.QueueName);
        }
    }
}