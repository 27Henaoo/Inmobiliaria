using LibInmobiliaria.Entidades;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Comercial;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Comercial
{
    public class EmpleadosCompradoresNegocio : IEmpleadosCompradoresNegocio
    {

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        public List<EmpleadosCompradores> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se consultan todos los registros de EmpleadosCompradores y se devuelven en forma de lista.
            return this.iConexion.EmpleadosCompradores!.ToList();
        }

        // Método para guardar EmpleadosCompradores nuevos.
        public EmpleadosCompradores Guardar(EmpleadosCompradores entidad)
        {
            // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se agrega la entidad al conjunto de EmpleadosCompradores.
            this.iConexion.EmpleadosCompradores!.Add(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad guardada.
            return entidad;
        }

        // Método para modificar EmpleadosCompradores existentes.
        public EmpleadosCompradores Modificar(EmpleadosCompradores entidad)
        {
            // Si el Id es 0, no se puede modificar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se obtiene la entrada de Entity Framework para la entidad recibida.
            var entry = this.iConexion.Entry<EmpleadosCompradores>(entidad);

            // Se marca la entidad como modificada.
            entry.State = EntityState.Modified;

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad modificada.
            return entidad;
        }
        // Método para borrar EmpleadosCompradores existentes.
        public EmpleadosCompradores Borrar(EmpleadosCompradores entidad)
        {
            // Si el Id es 0, no se puede borrar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se marca la entidad para eliminarla.
            this.iConexion.EmpleadosCompradores!.Remove(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad borrada.
            return entidad;
        }

    }
}
