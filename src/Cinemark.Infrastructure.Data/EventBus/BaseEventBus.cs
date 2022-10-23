﻿using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class BaseEventBus<T> : IBaseEventBus<T> where T : class
    {
        private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfiguration;
        private readonly string _queueName;

        private readonly IConnection _connection;
        private readonly IModel _model;

        public BaseEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration,
            string queueName)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _queueName = queueName;

            #region ConnectionFactory

            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfiguration.Value.HostName,
                UserName = _rabbitMqConfiguration.Value.UserName,
                Password = _rabbitMqConfiguration.Value.Password
            };

            factory.DispatchConsumersAsync = true;

            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();

            #endregion

            #region Queue

            _model.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            #endregion
        }

        public virtual async Task PublisherAsync(T entity)
        {
            try
            {
                var json = JsonConvert.SerializeObject(entity);
                var body = Encoding.UTF8.GetBytes(json);

                _model.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);

                await Task.Yield();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<T?> SubscriberAsync()
        {
            var entity = default(T?);

            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    entity = JsonConvert.DeserializeObject<T>(message);

                    _model.BasicAck(ea.DeliveryTag, false);

                    await Task.Yield();
                }
                catch (Exception)
                {
                    throw;
                }
            };

            _model.BasicConsume(_queueName, false, consumer);

            return await Task.FromResult(entity);
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}
