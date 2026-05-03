using LibInmobiliaria.Entidades;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Financiero;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Financiero
{
    public class ActivosFinancierosNegocio : IActivosFinancierosNegocio
    {

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        public List<ActivosFinancieros> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se consultan todos los registros de ActivosFinancieros y se devuelven en forma de lista.
            return this.iConexion.ActivosFinancieros!.ToList();
        }

        // Método para guardar ActivosFinancieros nuevos.
        public ActivosFinancieros Guardar(ActivosFinancieros entidad)
        {
            // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se agrega la entidad al conjunto de ActivosFinancieros.
            this.iConexion.ActivosFinancieros!.Add(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad guardada.
            return entidad;
        }

        // Método para modificar ActivosFinancieros existentes.
        public ActivosFinancieros Modificar(ActivosFinancieros entidad)
        {
            // Si el Id es 0, no se puede modificar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se obtiene la entrada de Entity Framework para la entidad recibida.
            var entry = this.iConexion.Entry<ActivosFinancieros>(entidad);

            // Se marca la entidad como modificada.
            entry.State = EntityState.Modified;

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad modificada.
            return entidad;
        }
        // Método para borrar ActivosFinancieros existentes.
        public ActivosFinancieros Borrar(ActivosFinancieros entidad)
        {
            // Si el Id es 0, no se puede borrar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se marca la entidad para eliminarla.
            this.iConexion.ActivosFinancieros!.Remove(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad borrada.
            return entidad;
        }
    }
}
