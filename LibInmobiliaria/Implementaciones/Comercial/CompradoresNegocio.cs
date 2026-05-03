using LibInmobiliaria.Entidades;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Comercial;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Comercial
{
    public class CompradoresNegocio : ICompradoresNegocio
    {

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        public List<Compradores> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se consultan todos los registros de Compradores y se devuelven en forma de lista.
            return this.iConexion.Compradores!.ToList();
        }

        // Método para guardar Compradores nuevos.
        public Compradores Guardar(Compradores entidad)
        {
            // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se agrega la entidad al conjunto de Compradores.
            this.iConexion.Compradores!.Add(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad guardada.
            return entidad;
        }

        // Método para modificar Compradores existentes.
        public Compradores Modificar(Compradores entidad)
        {
            // Si el Id es 0, no se puede modificar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se obtiene la entrada de Entity Framework para la entidad recibida.
            var entry = this.iConexion.Entry<Compradores>(entidad);

            // Se marca la entidad como modificada.
            entry.State = EntityState.Modified;

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad modificada.
            return entidad;
        }
        // Método para borrar Compradores existentes.
        public Compradores Borrar(Compradores entidad)
        {
            // Si el Id es 0, no se puede borrar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se marca la entidad para eliminarla.
            this.iConexion.Compradores!.Remove(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad borrada.
            return entidad;
        }

    }
}
