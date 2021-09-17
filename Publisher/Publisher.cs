using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Publisher.Model;
using Publisher.RabbitMq;
using RabbitMQClientLib;

namespace Publisher
{
    public class Publisher
    {
        private readonly IDictionary<string, RabbitMQClient> _clients = new Dictionary<string, RabbitMQClient>();
        private readonly IOutBoxEventRepository _eventRepository;

        public Publisher(IOutBoxEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Publish(IEnumerable<OutboxEvent> events)
        {
            foreach (var outboxEvent in events)
            {
                try
                {
                    var client = GetRabbitMq(outboxEvent.RabbitMqInfo);
                    var sender = client.GetSender(outboxEvent.RabbitMqInfo);
                    sender.Send(outboxEvent.Payload.ToString());
                }
                catch (Exception e)
                {
                    _eventRepository.UpdateEventStatus(outboxEvent.Id, OutBoxEventStatus.Failure);
                }

                _eventRepository.UpdateEventStatus(outboxEvent.Id, OutBoxEventStatus.Success);
            }
        }

        private RabbitMQClient GetRabbitMq(RabbitMqInfo info)
        {
            if (_clients.ContainsKey(info.Uri))
            {
                return _clients[info.Uri];
            }

            var client = new RabbitMQClient(info.Uri);
            _clients.Add(info.Uri, client);
            return client;
        }
    }
}