
using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Sujetos;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Sujetos

{
    public class PersonasNegocio : IPersonasNegocio
    {
        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        // Método para consultar todos los Personas.
        public List<Personas> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            // Se consultan todos los registros de Personas y se devuelven en forma de lista.
            return this.iConexion.Personas!.Include(x => x._EstadoCivil).Include(x => x._Nacionalidad).Include(x => x.Telefonos).Include(x => x.Direcciones)
                   .Include(x => x.ExpedientesLaborales).ToList();
        }

        // Método para guardar un avión nuevo.
        public Personas Guardar(Personas entidad)
        {
            // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            // Se agrega la entidad al conjunto de Personas.
            this.iConexion.Personas!.Add(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad guardada.
            return entidad;
        }

        // Método para modificar un avión existente.
        public Personas Modificar(Personas entidad)
        {
            // Si el Id es 0, no se puede modificar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            // Se obtiene la entrada de Entity Framework para la entidad recibida.
            var entry = this.iConexion.Entry<Personas>(entidad);

            // Se marca la entidad como modificada.
            entry.State = EntityState.Modified;

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad modificada.
            return entidad;
        }

        // Método para borrar un avión existente.
        public Personas Borrar(Personas entidad)
        {
            // Si el Id es 0, no se puede borrar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            // Se marca la entidad para eliminarla.
            this.iConexion.Personas!.Remove(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad borrada.
            return entidad;
        }
    }
}
