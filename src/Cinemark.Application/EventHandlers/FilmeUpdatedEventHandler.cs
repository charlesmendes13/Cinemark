﻿using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using Cinemark.Domain.Contracts.Events;
using MediatR;

namespace Cinemark.Application.EventHandlers
{
    public class FilmeUpdatedEventHandler : 
        INotificationHandler<FilmeUpdatedEvent>
    {
        private readonly IFilmeEventBus _eventBus;

        public FilmeUpdatedEventHandler(IFilmeEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(FilmeUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var queueName = typeof(Filme).Name + FilmeQueue.Updated.Name;

            await _eventBus.PublisherAsync(queueName, notification.Filme);
        }
    }
}
