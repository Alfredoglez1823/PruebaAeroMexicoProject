using Newtonsoft.Json;
using PruebaAeroMexicoAPI.Controllers;
using PruebaAeroMexicoAPI.Models;


namespace PruebaAeroMexicoAPI.Repository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly string _jsonFilePath;
        private List<Empleado> _empleados;

        public EmpleadoRepository()
        {
            _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DB", "empleados.json");
            _empleados = LeerDesdeJSONAsync().Result;
        }

        public async Task<List<Empleado>> ObtenerTodosAsync()
        {
            return await Task.FromResult(_empleados);
        }

        public async Task<Empleado> ObtenerPorNumeroAsync(int numeroEmp)
        {
            return await Task.FromResult(_empleados.FirstOrDefault(e => e.numeroEmp == numeroEmp));
        }

        public async Task AgregarAsync(Empleado empleado)
        {
            _empleados.Add(empleado);
            await GuardarEnJSONAsync();
        }

        public async Task ActualizarAsync(Empleado empleadoActualizado)
        {
            var empleado = _empleados.FirstOrDefault(e => e.numeroEmp == empleadoActualizado.numeroEmp);
            if (empleado != null)
            {
                empleado.nombre = empleadoActualizado.nombre ?? empleado.nombre;
                empleado.apellido = empleadoActualizado.apellido ?? empleado.apellido;

                await GuardarEnJSONAsync();
            }
            else
            {
                throw new KeyNotFoundException("Empleado no encontrado");
            }
        }

        public async Task EliminarAsync(int numeroEmp)
        {
            var empleado = _empleados.FirstOrDefault(e => e.numeroEmp == numeroEmp);
            if (empleado != null)
            {
                _empleados.Remove(empleado);
                await GuardarEnJSONAsync();
            }
            else
            {
                throw new KeyNotFoundException("Empleado no encontrado");
            }
        }

        private async Task<List<Empleado>> LeerDesdeJSONAsync()
        {
            if (File.Exists(_jsonFilePath))
            {
                var json = await File.ReadAllTextAsync(_jsonFilePath);
                var wrapper = JsonConvert.DeserializeObject<EmpleadosWrapper>(json);
                return wrapper?.empleados ?? new List<Empleado>();
            }
            return new List<Empleado>();
        }

        private async Task GuardarEnJSONAsync()
        {
            var wrapper = new EmpleadosWrapper { empleados = _empleados };
            var json = JsonConvert.SerializeObject(wrapper, Formatting.Indented);
            await File.WriteAllTextAsync(_jsonFilePath, json);
        }
    }
}
