using PruebaAeroMexicoAPI.Models;
using PruebaAeroMexicoAPI.Repository;

namespace PruebaAeroMexicoAPI.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public async Task<List<Empleado>> ObtenerEmpleadosAsync()
        {
            return await _empleadoRepository.ObtenerTodosAsync();
        }

        public async Task<Empleado> ObtenerEmpleadoPorNumeroAsync(int numeroEmp)
        {
            return await _empleadoRepository.ObtenerPorNumeroAsync(numeroEmp);
        }

        public async Task AgregarEmpleadoAsync(Empleado empleado)
        {
            await _empleadoRepository.AgregarAsync(empleado);
        }

        public async Task ActualizarEmpleadoAsync(int numeroEmp, Empleado empleadoActualizado)
        {
            empleadoActualizado.numeroEmp = numeroEmp;
            await _empleadoRepository.ActualizarAsync(empleadoActualizado);
        }

        //HolaContratame:D
        public async Task EliminarEmpleadoAsync(int numeroEmp)
        {
            await _empleadoRepository.EliminarAsync(numeroEmp);
        }
    }
}
