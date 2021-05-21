using System;
using System.Threading.Tasks;
using Lab_4.Domain.Entity;
using Lab_4.Domain.Handler.Command;
using Lab_4.Domain.Handler.Query;
using Lab_4.Domain.Service;
using Lab_4.Host.Cfg;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_4.Host.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentsController : ControllerBase
    {
        private readonly IHostelService _hostelService;
        private readonly IResidentService _residentService;
        private readonly IMediator _mediator;
        private readonly AppConfig _cfg;

        public StudentsController(IMediator mediator, IHostelService hostelService, IOptions<AppConfig> cfg,
            IResidentService residentService)
        {
            _mediator = mediator;
            _hostelService = hostelService;
            _residentService = residentService;
            _cfg = cfg.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var domainRequest = new GetStudents.GetStudentsRequest();
            var domainResponse = await _mediator.Send(domainRequest);
            
            return new ObjectResult(domainResponse.Entities);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var domainRequest = new GetStudents.GetStudentRequest(id);
            var domainResponse = await _mediator.Send(domainRequest);
            
            return new ObjectResult(domainResponse.Entity);
        }
        
        [HttpGet("forHostel")]
        public async Task<IActionResult> GetStudentsForHostel()
        {
            var domainRequest = new GetStudents.GetStudentsRequest();
            var domainResponse = await _mediator.Send(domainRequest);
            
            return new ObjectResult(_hostelService.GetStudentsForHostel(domainResponse.Entities, _cfg.UniversityTown));
        }

        [HttpGet("nonResidentsPercentage")]
        public async Task<IActionResult> GetPercentageOfNonResidentStudents()
        {
            var domainRequest = new GetStudents.GetStudentsRequest();
            var domainResponse = await _mediator.Send(domainRequest);

            return new ObjectResult(
                _residentService.GetNonResidentStudentsPercentage(domainResponse.Entities, _cfg.UniversityTown));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentEntity request)
        {
            var passportSeries = Guid.NewGuid().ToString();
            var domainRequest = new SetStudent.Request(request.FirstName, request.LastName, request.StudentTicket,
                request.PassportNumber, passportSeries, request.Class, request.HomeTown);
            
            await _mediator.Send(domainRequest);
            return new ObjectResult(passportSeries);
        }
    }
}