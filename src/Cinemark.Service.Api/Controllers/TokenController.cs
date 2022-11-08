﻿using AutoMapper;
using Cinemark.Application.Dto;
using Cinemark.Application.Events.Queries;
using Cinemark.Domain.Models;
using Cinemark.Domain.Models.Commom;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cinemark.Service.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public TokenController(IMapper mapper,
            IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultData<TokenDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResultData<TokenDto>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultData<TokenDto>>> Get([FromQuery] GetTokenDto request)
        {
            var token = await _mediator.Send(new GetTokenByUsuarioQuery
            {
                Usuario = _mapper.Map<Usuario>(request)
            });

            return HttpResult(_mapper.Map<ResultData<TokenDto>>(token));
        }
    }
}
