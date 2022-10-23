﻿using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Models;
using Cinemark.Infrastructure.Data.Context;
using Cinemark.Infrastructure.Data.EventBus.Option;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cinemark.Infrastructure.Data.EventBus
{
    public class FilmeDeleteEventBus : BaseEventBus<Filme>, IFilmeDeleteEventBus
    {
        private const string queueName = "Filme_Delete";

        private readonly MongoContext _mongoContext;
        private IMongoCollection<Filme> _mongoCollection;

        public FilmeDeleteEventBus(IOptions<RabbitMqConfiguration> rabbitMqConfiguration,
            MongoContext mongoContext)
            : base(rabbitMqConfiguration, queueName)
        {
            _mongoContext = mongoContext;
            _mongoCollection = _mongoContext.GetCollection<Filme>(typeof(Filme).Name);
        }

        public async Task HandleMessageAsync()
        {
            var filme = await base.SubscriberAsync();

            if (filme != null)
                await _mongoCollection.DeleteOneAsync(Builders<Filme>.Filter.Eq("_id", filme.Id));
        }
    }
}
