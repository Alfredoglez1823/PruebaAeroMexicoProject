using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAeroMexicoAPI.Models;
using PruebaAeroMexicoAPI.Services;

namespace PruebaAeroMexicoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get(int? numeroEmp)
        {
            try
            {
                if (numeroEmp.HasValue)
                {
                    var empleado = await _empleadoService.ObtenerEmpleadoPorNumeroAsync(numeroEmp.Value);
                    if (empleado == null)
                    {
                        return NotFound(new { message = "Empleado no encontrado" });
                    }
                    return Ok(empleado);
                }
                else
                {
                    return Ok(await _empleadoService.ObtenerEmpleadosAsync());
                }
            }
            catch (Exception ex)
            {
                // Log the error (el logging sería implementado aquí)
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud", details = ex.Message });
            }
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _empleadoService.AgregarEmpleadoAsync(empleado);
                return CreatedAtAction(nameof(Get), new { numeroEmp = empleado.numeroEmp }, empleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al agregar el empleado", details = ex.Message });
            }
        }

        // PUT
        [HttpPut("{numeroEmp}")]
        public async Task<IActionResult> Put(int numeroEmp, [FromBody] Empleado empleadoActualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _empleadoService.ActualizarEmpleadoAsync(numeroEmp, empleadoActualizado);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Empleado no encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el empleado", details = ex.Message });
            }
        }

        //Delete
        [HttpDelete("{numeroEmp}")]
        public async Task<IActionResult> Delete(int numeroEmp)
        {
            try
            {
                await _empleadoService.EliminarEmpleadoAsync(numeroEmp);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Empleado no encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al eliminar el empleado", details = ex.Message });
            }
        }
    }


    public class EmpleadosWrapper
    {
        public List<Empleado> empleados { get; set; }
    }
}
