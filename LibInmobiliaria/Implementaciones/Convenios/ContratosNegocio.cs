using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Convenios;
using Microsoft.EntityFrameworkCore;


namespace LibInmobiliaria.Implementaciones.Convenios {
    public class ContratosNegocio : IContratosNegocio
    {

        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        public List<Contratos> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se consultan todos los registros de Contratos y se devuelven en forma de lista.
            return this.iConexion.Contratos!.Include(x => x._Cliente).Include(x => x._Propiedad!).ThenInclude(x => x._TipoPropiedad).Include(x => x._Comprador)
                                            .Include(x => x._JefeSector!).ThenInclude(x => x._Sector).Include(x => x.ContratosEmpleados!).ThenInclude(x => x._Empleado)
                                            .Include(x => x.ContratosCodeudores!).ThenInclude(x => x._Codeudor).ToList();

        }

        // Método para guardar Contratos nuevos.
        public Contratos Guardar(Contratos entidad)
        {
            // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se agrega la entidad al conjunto de Contratos.
            this.iConexion.Contratos!.Add(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad guardada.
            return entidad;
        }

        // Método para modificar Contratos existentes.
        public Contratos Modificar(Contratos entidad)
        {
            // Si el Id es 0, no se puede modificar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se obtiene la entrada de Entity Framework para la entidad recibida.
            var entry = this.iConexion.Entry<Contratos>(entidad);

            // Se marca la entidad como modificada.
            entry.State = EntityState.Modified;

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad modificada.
            return entidad;
        }
        // Método para borrar Contratos existentes.
        public Contratos Borrar(Contratos entidad)
        {
            // Si el Id es 0, no se puede borrar porque no existe en base de datos.
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            // Se marca la entidad para eliminarla.
            this.iConexion.Contratos!.Remove(entidad);

            // Se guardan los cambios en la base de datos.
            this.iConexion.SaveChanges();

            // Se devuelve la entidad borrada.
            return entidad;
        }
    }
}

