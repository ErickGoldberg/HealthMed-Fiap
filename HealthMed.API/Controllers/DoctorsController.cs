using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DoctorsController(IDoctorService doctorService) : ControllerBase
    {
        private readonly IDoctorService _doctorService = doctorService;

        /// <summary>
        /// Retorna os dados de um médico pelo ID.
        /// </summary>
        /// <param name="id">ID do médico.</param>
        /// <returns>Status 200 OK se encontrado, ou 404 Not Found.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces(typeof(DoctorDto))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            return result.Data != null ? Ok(result.Data) : NotFound();
        }

        /// <summary>
        /// Cadastra um novo médico.
        /// </summary>
        /// <param name="input">Dados do médico.</param>
        /// <returns>Status 201 Created, ou 400 Bad Request.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(CreateOrEditDoctorInputModel input)
        {
            var result = await _doctorService.CreateDoctorAsync(input);
            return result.IsSuccess ? Created() : BadRequest(result.Message);
        }

        /// <summary>
        /// Atualiza os dados de um médico existente.
        /// </summary>
        /// <param name="input">Dados atualizados do médico.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(CreateOrEditDoctorInputModel input)
        {
            var result = await _doctorService.UpdateDoctorAsync(input);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        /// <summary>
        /// Remove um médico pelo ID.
        /// </summary>
        /// <param name="id">ID do médico.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _doctorService.DeleteDoctorAsync(id);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        /// <summary>
        /// Cadastra um novo horário de disponibilidade para o médico.
        /// </summary>
        /// <param name="input">Dados do horário.</param>
        /// <returns>Status 201 Created, ou 400 Bad Request.</returns>
        [HttpPost("availability")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAvailability(AddAvailabilityInputModel input)
        {
            var result = await _doctorService.AddAvailabilityAsync(input);
            return result.IsSuccess ? Created() : BadRequest(result.Message);
        }

        /// <summary>
        /// Remove um horário de disponibilidade do médico.
        /// </summary>
        /// <param name="availabilityId">ID do horário de disponibilidade.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpDelete("availability/{availabilityId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveAvailability(Guid availabilityId)
        {
            var result = await _doctorService.RemoveAvailabilityAsync(availabilityId);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        /// <summary>
        /// Aceita uma consulta médica agendada.
        /// </summary>
        /// <param name="appointmentId">ID da consulta.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpPut("appointments/{appointmentId:guid}/accept")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AcceptAppointment(Guid appointmentId)
        {
            var result = await _doctorService.AcceptAppointmentAsync(appointmentId);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }

        /// <summary>
        /// Recusa uma consulta médica agendada.
        /// </summary>
        /// <param name="appointmentId">ID da consulta.</param>
        /// <returns>Status 204 No Content, ou 400 Bad Request.</returns>
        [HttpPut("appointments/{appointmentId:guid}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectAppointment(Guid appointmentId)
        {
            var result = await _doctorService.RejectAppointmentAsync(appointmentId);
            return result.IsSuccess ? NoContent() : BadRequest(result.Message);
        }
    }
}
