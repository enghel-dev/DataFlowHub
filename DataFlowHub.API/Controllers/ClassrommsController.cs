using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataFlowHub.Application.Services;
using DataFlowHub.Application.DTOs;

namespace DataFlowHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassrommsController : ControllerBase
    {
        private readonly ClassroomServices _classroomServices;

        public ClassrommsController(ClassroomServices classroomServices)
        {
            _classroomServices = classroomServices;
        }

        [HttpGet("List Classroom")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lista = await _classroomServices.GetAll();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        [HttpPost("New Classroom")]
        public async Task<IActionResult> Create(ClassroomDTOs classroomDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "The model is not valid" });
                }
                await _classroomServices.Create(classroomDTOs);
                return StatusCode(201, "Successfully created classroom");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        [HttpPut("Update Classroom")]
        public async Task<IActionResult> Update(int id, [FromBody] ClassroomDTOs classroomDTOs)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { msj = "The model is not valid" });
                }

                if (id != classroomDTOs.Id)
                {
                    return BadRequest(new { msj = "The ID does not match" });
                }

                id = classroomDTOs.Id;
                await _classroomServices.Update(classroomDTOs);
                return Ok("Successfully updated classroom");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error" + ex.Message);
            }
        }

        [HttpDelete ("Delete Classroom")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { msj = "The id must be greater than 0" });
                }

                var classroom = await _classroomServices.GetById(id);
                if (classroom == null)
                {
                    return NotFound(new { msj = "Classroom not found" });
                }

                await _classroomServices.Delete(id);
                return Ok(new { msj = "Classroom deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Get classroom by id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { msj = "The id must be greater than 0" });
                }

                var classroom = await _classroomServices.GetById(id);

                if (classroom == null)
                {
                    return NotFound(new { msj = "Classroom not found" });
                }

                return Ok(classroom);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}
