using System.Threading;
using System.Threading.Tasks;
using Lab_4.Domain.Abstraction;
using Lab_4.Domain.Entity;
using MediatR;

namespace Lab_4.Domain.Handler.Query
{
    public static class SetStudent
    {
        public class Request : StudentEntity, IRequest<Unit>
        {
            public Request(string firstName, string lastName, string studentTicket, long passportNumber,
                string passportSeries, int @class, string hometown) : base(firstName, lastName, studentTicket,
                passportNumber,
                passportSeries, @class, hometown)
            {
            }
        }
        
        public class Handler : IRequestHandler<Request>
        {
            private readonly IRepository<StudentEntity> _repository;

            public Handler(IRepository<StudentEntity> repository)
            {
                _repository = repository;
            }
            
            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
               await _repository.SetAsync(request);
               return Unit.Value;
            }
        }
    }
}