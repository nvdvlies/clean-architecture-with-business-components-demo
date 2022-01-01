﻿using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Demo.Events
{
    public class Event<T> : Event, INotification where T : IEventData
    {
        public new T Data => (T)base.Data;

        [JsonConstructor]
        public Event(Topics topic, T data, string subject, string dataVersion, string correlationId) : base(topic, data, subject, dataVersion, correlationId)
        {
        }
    }

    public class Event
    {
        [JsonInclude]
        public string Type { get; private set; }
        [JsonInclude]
        public Topics Topic { get; private set; }
        [JsonInclude]
        public IEventData Data { get; private set; }
        [JsonInclude]
        public DateTime CreatedOn { get; private set; }
        [JsonInclude]
        public string Subject { get; private set; }
        [JsonInclude]
        public string DataVersion { get; private set; }
        [JsonInclude]
        public string CorrelationId { get; private set; }

        [JsonConstructor]
        public Event(Topics topic, IEventData data, string subject, string dataVersion, string correlationId)
        {
            Type = GetType().FullName;
            Topic = topic;
            Data = data;
            CreatedOn = DateTime.UtcNow;
            Subject = subject;
            DataVersion = dataVersion;
            CorrelationId = correlationId;
        }
    }
}