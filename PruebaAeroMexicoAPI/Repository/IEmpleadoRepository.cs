using PruebaAeroMexicoAPI.Models;

namespace PruebaAeroMexicoAPI.Repository
{
    public interface IEmpleadoRepository
    {
        Task<List<Empleado>> ObtenerTodosAsync();
        Task<Empleado> ObtenerPorNumeroAsync(int numeroEmp);
        Task AgregarAsync(Empleado empleado);
        Task ActualizarAsync(Empleado empleado);
        Task EliminarAsync(int numeroEmp);
    }
}
