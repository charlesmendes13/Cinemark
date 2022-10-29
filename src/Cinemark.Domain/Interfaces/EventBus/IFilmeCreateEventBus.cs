﻿using Cinemark.Domain.Models;

namespace Cinemark.Domain.Interfaces.EventBus
{
    public interface IFilmeCreateEventBus : IBaseEventBus<Filme>
    {
        Task<bool> HandlerMessageAsync(Filme filme);
    }
}
