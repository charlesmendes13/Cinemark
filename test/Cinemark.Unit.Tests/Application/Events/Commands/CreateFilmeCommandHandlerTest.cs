﻿using Cinemark.Application.Events.Commands;
using Cinemark.Domain.Interfaces.EventBus;
using Cinemark.Domain.Interfaces.Repositories;
using Cinemark.Domain.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Cinemark.Unit.Tests.Application.Events.Commands
{
    public class CreateFilmeCommandHandlerTest
    {
        [Fact]
        public async void CreateFilmeCommandHandler()
        {
            var filme = new Filme()
            {
                Id = 1,
                Nome = "E o Vento Levou",
                Categoria = "Drama",
                FaixaEtaria = 12,
                DataLancamento = new DateTime(1971, 10, 3)
            };
            
            var filmeRepositoryMock = new Mock<IFilmeRepository>();
            filmeRepositoryMock.Setup(x => x.InsertAsync(It.IsAny<Filme>()))
                .ReturnsAsync(filme);

            var createFilmeSenderMock = new Mock<ICreateFilmeSender>();
            createFilmeSenderMock.Setup(x => x.SendMessageAsync(It.IsAny<Filme>()))
                .Returns(Task.FromResult(filme));

            var command = new CreateFilmeCommand() { Filme = filme };            
            var handler = new CreateFilmeCommandHandler(filmeRepositoryMock.Object, createFilmeSenderMock.Object);
                       
            var result = await handler.Handle(command, new CancellationToken());

            result.Id.Should().Be(1);
            result.Nome.Should().Be("E o Vento Levou");
            result.Categoria.Should().Be("Drama");
            result.FaixaEtaria.Should().Be(12);
            result.DataLancamento.Should().Be(new DateTime(1971, 10, 3));
        }
    }
}