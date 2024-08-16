using PruebaAeroMexicoAPI.Models;

namespace PruebaAeroMexicoAPI.Services
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> ObtenerEmpleadosAsync();
        Task<Empleado> ObtenerEmpleadoPorNumeroAsync(int numeroEmp);
        Task AgregarEmpleadoAsync(Empleado empleado);
        Task ActualizarEmpleadoAsync(int numeroEmp, Empleado empleadoActualizado);
        Task EliminarEmpleadoAsync(int numeroEmp);
    }
}
