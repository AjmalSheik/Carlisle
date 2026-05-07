using DLM.Application.Common.Response;
using DLM.Application.Features.Doctors.Commands;
using DLM.Application.Features.Doctors.DTOs;
using DLM.Application.Features.Doctors.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DLM.API.Controllers.Doctors
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor(CreateDoctorCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(
    ApiResponse<DoctorDTO>.SuccessResult(
        result
       
    )
);
        }


        [HttpPost("GetDoctors")]
        public async Task<IActionResult> GetDoctors(GetDoctorsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(
    ApiResponse<List<DoctorSPDto>>.SuccessResult(
        result

    )
);
        }

        [HttpPost("GetDoctorById")]
        public async Task<IActionResult> GetDoctorById(GetDoctorByIdQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(
    ApiResponse<DoctorDTO>.SuccessResult(
        result

    )
);

        }

        [HttpPost("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor(DeleteDoctorCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(
    ApiResponse<bool>.SuccessResult(
        result

    )
);
        }

        [HttpPost("UpdateDoctor")]
        public async Task<IActionResult> UpdateDoctor(UpdateDoctorCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(
    ApiResponse<DoctorDTO>.SuccessResult(
        result

    )
);
        }

    }
}
