using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Patrimonio;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Patrimonio
{
    public class BienesInmueblesNegocio : IBienesInmueblesNegocio
    {

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        public List<BienesInmuebles> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se consultan todos los registros de BienesInmuebles y se devuelven en forma de lista.
            return this.iConexion.BienesInmuebles!.ToList();
        }

        // Método para guardar BienesInmuebles nuevos.
        public BienesInmuebles Guardar(BienesInmuebles entidad)
        {
            // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se agrega la entidad al conjunto de BienesInmuebles.
            this.iConexion.BienesInmuebles!.Add(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad guardada.
            return entidad;
        }

        // Método para modificar BienesInmuebles existentes.
        public BienesInmuebles Modificar(BienesInmuebles entidad)
        {
            // Si el Id es 0, no se puede modificar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se obtiene la entrada de Entity Framework para la entidad recibida.
            var entry = this.iConexion.Entry<BienesInmuebles>(entidad);

            // Se marca la entidad como modificada.
            entry.State = EntityState.Modified;

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad modificada.
            return entidad;
        }
        // Método para borrar BienesInmuebles existentes.
        public BienesInmuebles Borrar(BienesInmuebles entidad)
        {
            // Si el Id es 0, no se puede borrar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se marca la entidad para eliminarla.
            this.iConexion.BienesInmuebles!.Remove(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad borrada.
            return entidad;
        }

    }
}
