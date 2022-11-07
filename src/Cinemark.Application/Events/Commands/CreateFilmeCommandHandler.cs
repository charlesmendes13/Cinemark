﻿using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;

namespace Cinemark.Application.Events.Commands
{
    public class CreateFilmeCommandHandler : IRequestHandler<CreateFilmeCommand, ResultData<Filme>>
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IFilmeEventBus _filmeEventBus;

        public CreateFilmeCommandHandler(IFilmeRepository filmeRepository,
            IFilmeEventBus filmeEventBus)
        {
            _filmeRepository = filmeRepository;
            _filmeEventBus = filmeEventBus;
        }

        public async Task<ResultData<Filme>> Handle(CreateFilmeCommand request, CancellationToken cancellationToken)
        {
            var filme = await _filmeRepository.InsertAsync(request.Filme);

            if (!filme.Success)
                return new ErrorData<Filme>("O Filme já foi Cadastrado");
                
            await _filmeEventBus.PublisherAsync(typeof(Filme).Name + "_Insert", filme.Data);

            return new SuccessData<Filme>(filme.Data);
        }
    }
}
