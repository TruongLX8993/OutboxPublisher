using System;
using Newtonsoft.Json;

namespace Publisher.Model
{
    public enum OutBoxEventStatus
    {
        Pending,
        Success,
        Failure
    }
    public class OutboxEvent
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string AggregateId { get; set; }
     
        public DateTime? CreatedDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public object Payload { get; set; }
        public OutBoxEventStatus Status { get; set; }
        public RabbitMqInfo RabbitMqInfo { get; set; }
        public string PayloadString => JsonConvert.SerializeObject(Payload);
    }
}