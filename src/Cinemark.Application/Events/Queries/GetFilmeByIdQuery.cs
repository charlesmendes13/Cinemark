using Cinemark.Domain.Models;
using MediatR;

namespace Cinemark.Application.Events.Queries
{
    public class GetFilmeByIdQuery : IRequest<Filme>
    {
        public int Id { get; set; }
    }
}
