using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab_4.Domain.Abstraction;
using Lab_4.Domain.Entity;
using MediatR;

namespace Lab_4.Domain.Handler.Command
{
    public static class GetStudents
    {
        public class GetStudentRequest : IRequest<GetStudentResponse>
        {
            public string Id { get; }

            public GetStudentRequest(string id)
            {
                Id = id;
            }
        }

        public class GetStudentResponse
        {
            public StudentEntity Entity { get; }

            public GetStudentResponse(StudentEntity entity)
            {
                Entity = entity;
            }
        }
        
        public class GetStudentsRequest : IRequest<GetStudentsResponse> { }

        public class GetStudentsResponse
        {
            public IEnumerable<StudentEntity> Entities { get; }

            public GetStudentsResponse(IEnumerable<StudentEntity> entities)
            {
                Entities = entities;
            }
        }

        public class GetStudentHandler : IRequestHandler<GetStudentRequest, GetStudentResponse>
        {
            private readonly IRepository<StudentEntity> _repository;

            public GetStudentHandler(IRepository<StudentEntity> repository)
            {
                _repository = repository;
            }

            public async Task<GetStudentResponse> Handle(GetStudentRequest request, CancellationToken cancellationToken)
            {
                var entity = await _repository.GetAsync(request.Id);
                return new GetStudentResponse(entity);
            }
        }
        
        public class GetStudentsHandler : IRequestHandler<GetStudentsRequest, GetStudentsResponse>
        {
            private readonly IRepository<StudentEntity> _repository;

            public GetStudentsHandler(IRepository<StudentEntity> repository)
            {
                _repository = repository;
            }

            public async Task<GetStudentsResponse> Handle(GetStudentsRequest request,
                CancellationToken cancellationToken)
            {
                var entities = await _repository.GetAllAsync();
                return new GetStudentsResponse(entities);
            }
        }
    }
}