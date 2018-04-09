using System;
using System.Windows.Input;
using ProtoBuf;

namespace Nop.EventNotify
{
    [ProtoContract]
    public class Message
    {
        public Message()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message(object body)
        {
            this.Body = body;
            BodyType = Body.GetType().AssemblyQualifiedName;
            SerializeType = SerializeTypeEnum.Json;
            CorrelationId = Guid.NewGuid().ToString("N");
            MessageId = Guid.NewGuid().ToString("N"); ;
            ObjectId = string.Empty;
            MessageType = MessageTypeEnum.Event;
            CreatedDate = DateTime.Now;
        }
        [ProtoMember(1)]
        public string MessageId { get; set; }
        /// <inheritdoc />
        /// <summary>
        /// Gets the body.
        /// </summary>
        public object Body { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the delay for sending, enqueing or processing the body.
        /// </summary>
        [ProtoMember(2)]
        public TimeSpan Delay { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the time to live for the message in the queue.
        /// </summary>
        [ProtoMember(3)]
        public TimeSpan TimeToLive { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the correlation id.
        /// </summary>
        [ProtoMember(4)]
        public string CorrelationId { get; set; }
        [ProtoMember(5)]
        public SerializeTypeEnum SerializeType { get; set; }
        [ProtoMember(6)]
        public int Version { get; set; }

        //public Type BodyType { get; private set; }
        [ProtoMember(7)]
        public string BodyType { get; private set; }

        [ProtoMember(8)]
        public string BodyDataJsonSerialize
        {
            get
            {
                if (SerializeType == SerializeTypeEnum.Json)
                {
                    return Serialize.JsonSerializeObject(Body);
                }
                return null;
            }
            set => Body = DeserializeBodyData(value);
        }
        [ProtoMember(9)]
        public byte[] BodyDataProtobufSerialize
        {
            get
            {
                if (SerializeType == SerializeTypeEnum.Protobuf)
                {
                    return Serialize.ProtoBufSerialize(Body);
                }
                return null;
            }
            set => Body = DeserializeBodyData(value);
        }

        [ProtoMember(10)]
        public MessageTypeEnum MessageType { get; set; }
        [ProtoMember(11)]
        public DateTime CreatedDate { get; set; }
        [ProtoMember(12)]
        public DateTime? ProcessDate { get; set; }
        [ProtoMember(13)]
        public string ResultKey { get; set; }
        [ProtoMember(14)]
        public string ResultQueueName { get; set; }
        [ProtoMember(15)]
        public string ResultBrokerName { get; set; }
        [ProtoMember(16)]
        public string ObjectId { get; set; }
        [ProtoMember(17)]
        public bool IsSendResult { get; set; }

        private object DeserializeBodyData(object value)
        {
            // Assembly assembly = Assembly.Load(BodyAssembly);
            Type type = Type.GetType(BodyType);
            switch (SerializeType)
            {
                case SerializeTypeEnum.Json:
                    {
                        return Serialize.JsonDeserializeObject((string)value, type);
                    }
                case SerializeTypeEnum.Protobuf:
                    {
                        return Serialize.ProtoBufDeserialize((byte[])value, type);
                    }

            }
            return null;
        }

        public static Message CreateMessage(dynamic obj)
        {
            Message message = new Message()
            {
                MessageId = obj.MessageId,
                Delay = TimeSpan.FromMilliseconds(obj.Delay),
                TimeToLive = TimeSpan.FromMilliseconds(obj.TimeToLive),
                CorrelationId = obj.CorrelationId,
                SerializeType = (SerializeTypeEnum)obj.SerializeType,
                Version = obj.Version,
                BodyType = obj.BodyType,
                MessageType = (MessageTypeEnum)obj.MessageType,
                CreatedDate = obj.CreatedDate,
                ProcessDate = obj.ProcessDate,
            };

            return message;
        }

        public byte[] ToMessage()
        {
            return Serialize.ProtoBufSerialize(this);
        }

        public Message Clone()
        {
            return new Message()
            {
                ResultBrokerName = this.ResultBrokerName,
                ResultQueueName = this.ResultQueueName,
                ResultKey = this.ResultKey,
                Body = this.Body,
                BodyType = this.BodyType,
                CorrelationId = this.CorrelationId,
                CreatedDate = this.CreatedDate,
                Delay = this.Delay,
                MessageId = this.MessageId,
                MessageType = this.MessageType,
                ProcessDate = this.ProcessDate,
                SerializeType = this.SerializeType,
                TimeToLive = this.TimeToLive,
                Version = this.Version,
                ObjectId = this.ObjectId
            };
        }

        public static Message CreateMessageFromQueue(byte[] bytes)
        {
            return Serialize.ProtoBufDeserialize<Message>(bytes);
        }

    }
}