﻿using Cinemark.Domain.Interfaces.EventBus;
using Microsoft.Extensions.Hosting;

namespace Cinemark.Infrastructure.Services.Consumer
{
    public class FilmeCreateConsumer : BackgroundService
    {
        private readonly IFilmeCreateEventBus _filmeCreateEventBus;

        public FilmeCreateConsumer(IFilmeCreateEventBus filmeCreateEventBus)
        {
            _filmeCreateEventBus = filmeCreateEventBus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _filmeCreateEventBus.HandleMessageAsync();
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
