﻿using Cinemark.Domain.AggregatesModels.FilmeAggregate;
using MediatR;

namespace Cinemark.Application.Queries
{
    public class GetFilmeByIdQuery : IRequest<Filme>
    {
        public Guid Id { get; private set; }

        public GetFilmeByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
