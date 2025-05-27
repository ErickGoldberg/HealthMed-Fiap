using HealthMed.Application.DTOs;
using HealthMed.Application.InputModels;
using HealthMed.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientsController(IPatientService patientService) : ControllerBase
    {
        /// <summary>
        /// Lista os médicos disponíveis com filtros opcionais.
        /// </summary>
        /// <param name="specialty">Especialidade médica (opcional).</param>
        /// <returns>Status 200 OK.</returns>
        [HttpGet("doctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces(typeof(List<DoctorDto>))]
        public async Task<IActionResult> GetDoctors([FromQuery] string? specialty = null)
        {
            var result = await patientService.GetAvailableDoctorsAsync(specialty);
            return Ok(result.Data);
        }

        /// <summary>
        /// Realiza o agendamento de uma nova consulta.
        /// </summary>
        /// <param name="input">Dados da consulta.</param>
        /// <returns>Status 201 Created, ou 400 Bad Request.</returns>
        [HttpPost("appointments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BookAppointment(BookAppointmentInputModel input)
        {
            var result = await patientService.BookAppointmentAsync(input);
            return result.IsSuccess ? Created() : BadRequest(result.Message);
        }

        /// <summary>
        /// Cancela uma consulta agendada, mediante justificativa.
        /// </summary>
        /// <param name="appointmentId">ID da consulta.</param>
        /// <param name="input">Justificativa do cancelamento.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpPut("appointments/{appointmentId:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelAppointment(Guid appointmentId, [FromBody] CancelAppointmentInputModel input)
        {
            var result = await patientService.CancelAppointmentAsync(appointmentId, input.Reason);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        /// <summary>
        /// Cadastra um novo paciente.
        /// </summary>
        /// <param name="input">Dados do paciente.</param>
        /// <returns>Status 201 Created, ou 400 Bad Request.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(CreateOrEditPatientInputModel input)
        {
            var result = await patientService.CreatePatientAsync(input);
            return result.IsSuccess ? Created() : BadRequest(result.Message);
        }

        /// <summary>
        /// Atualiza os dados de um paciente existente.
        /// </summary>
        /// <param name="input">Dados atualizados do paciente.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(CreateOrEditPatientInputModel input)
        {
            var result = await patientService.UpdatePatientAsync(input);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        /// <summary>
        /// Remove um paciente pelo ID.
        /// </summary>
        /// <param name="id">ID do paciente.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await patientService.DeletePatientAsync(id);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }
    }
}