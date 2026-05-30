using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Comercial;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Comercial
{
    public class EmpleadosCompradoresNegocio : IEmpleadosCompradoresNegocio
    {
        private string ObtenerDatosEmpleadosCompradores(EmpleadosCompradores entidad)
        {
            return $"Id: {entidad.Id}, " +
                   $"FechaAsesoramiento: {entidad.FechaAsesoramiento}, " +
                   $"Comprador: {entidad.Comprador}, " +
                   $"Empleado: {entidad.Empleado}, " +
                   $"_Comprador: {entidad._Comprador}, " +
                   $"_Empleado: {entidad._Empleado}, ";
        }


        private void AgregarHistorico(
            string accion,
            int? registroId,
            string descripcion,
            string? cambios,
            string? valorAnterior,
            string? valorNuevo,
            bool exitoso,
            string? error)
        {
            this.iConexion!.Historicos!.Add(new Historicos()
            {
                Usuario = "ADMIN",
                Tabla = "EmpleadosCompradores",
                Accion = accion,
                RegistroId = registroId,
                Descripcion = descripcion,
                Cambios = cambios,
                ValorAnterior = valorAnterior,
                ValorNuevo = valorNuevo,
                Origen = "Inmobiliaria.Api",
                Exitoso = exitoso,
                Error = error,
                Fecha = DateTime.Now
            });
        }



        // Variable privada para manejar la conexión a la base de datos.
        private IConexion? iConexion;

        public List<EmpleadosCompradores> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Se consultan los registros de EmpleadosCompradores en forma de lista.
                var lista = this.iConexion.EmpleadosCompradores!.Include(x => x._Comprador!).ThenInclude(x => x._EstadoCivil).Include(x => x._Comprador!)
                                 .ThenInclude(x => x._Nacionalidad).Include(x => x._Comprador!).ThenInclude(x => x.Telefonos).Include(x => x._Comprador!)
                                 .ThenInclude(x => x.Direcciones).Include(x => x._Comprador!).ThenInclude(x => x.ExpedientesLaborales).Include(x => x._Empleado!)
                                 .ThenInclude(x => x._EstadoCivil).Include(x => x._Empleado!).ThenInclude(x => x._Nacionalidad).Include(x => x._Empleado!)
                                 .ThenInclude(x => x.Telefonos).Include(x => x._Empleado!).ThenInclude(x => x.Direcciones).Include(x => x._Empleado!)
                                 .ThenInclude(x => x.ExpedientesLaborales).Include(x => x._Empleado!).ThenInclude(x => x._Sector).Include(x => x._Empleado!)
                                 .ThenInclude(x => x._JefeSector).Include(x => x._Empleado!).ThenInclude(x => x._TipoContrato).ToList();

                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Se consultaron los registros de EmpleadosCompradores",
                    cambios: "No se modificaron datos",
                    valorAnterior: null,
                    valorNuevo: $"Cantidad de registros consultados: {lista.Count}",
                    exitoso: true,
                    error: "N/A"
                );

                this.iConexion.SaveChanges();
                return lista;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Fallo al consultar los registros de EmpleadosCompradores",
                    cambios: "No se pudo consultar la lista",
                    valorAnterior: "Error al Consultar",
                    valorNuevo: "Error al Consultar",
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para guardar EmpleadosCompradores nuevos.
        public EmpleadosCompradores Guardar(EmpleadosCompradores entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Si el Id es distinto de 0, significa que la entidad supuestamente ya fue guardada.
                if (entidad.Id != 0)
                    throw new Exception("Ya se guardo");

                // Se agrega la entidad al conjunto de EmpleadosCompradores.
                this.iConexion.EmpleadosCompradores!.Add(entidad);

                AgregarHistorico(
                    accion: "Guardar",
                    registroId: null,
                    descripcion: "Se guardo un nuevo registro de EmpleadosCompradores",
                    cambios: "Se creo un nuevo registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosCompradores(entidad),
                    exitoso: true,
                    error: "N/A"
                );

                // Se guardan los cambios en la base de datos.
                this.iConexion.SaveChanges();

                // Se devuelve la entidad guardada.
                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Guardar",
                    registroId: entidad.Id,
                    descripcion: "Fallo al guardar un registro de EmpleadosCompradores",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosCompradores(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para modificar EmpleadosCompradores existentes.
        public EmpleadosCompradores Modificar(EmpleadosCompradores entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Si el Id es 0, no se puede modificar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede modificar");

                var anterior = this.iConexion.EmpleadosCompradores!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de EmpleadosCompradores no existe");

                string valorAnterior = ObtenerDatosEmpleadosCompradores(anterior);
                string valorNuevo = ObtenerDatosEmpleadosCompradores(entidad);

                // Se obtiene la entrada de Entity Framework para la entidad recibida.
                var entry = this.iConexion.Entry<EmpleadosCompradores>(entidad);

                // Se marca la entidad como modificada.
                entry.State = EntityState.Modified;

                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Se modifico un registro de EmpleadosCompradores",
                    cambios: "Se cambio la informacion del registro",
                    valorAnterior: valorAnterior,
                    valorNuevo: valorNuevo,
                    exitoso: true,
                    error: "N/A"
                );

                // Se guardan los cambios en la base de datos.
                this.iConexion.SaveChanges();

                // Se devuelve la entidad modificada.
                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Fallo al modificar un registro de EmpleadosCompradores",
                    cambios: "No se pudo modificar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosCompradores(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
        // Método para borrar EmpleadosCompradores existentes.
        public EmpleadosCompradores Borrar(EmpleadosCompradores entidad)
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Si el Id es 0, no se puede borrar porque no existe en base de datos.
                if (entidad.Id == 0)
                    throw new Exception("No se puede borrar");

                var anterior = this.iConexion.EmpleadosCompradores!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de EmpleadosCompradores no existe");

                string valorAnterior = ObtenerDatosEmpleadosCompradores(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.EmpleadosCompradores!.Remove(entidad);

                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Se borro un registro de EmpleadosCompradores",
                    cambios: "Se elimino el registro",
                    valorAnterior: valorAnterior,
                    valorNuevo: null,
                    exitoso: true,
                    error: "N/A"
                );

                // Se guardan los cambios en la base de datos.
                this.iConexion.SaveChanges();

                // Se devuelve la entidad borrada.
                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Fallo al borrar un registro de EmpleadosCompradores",
                    cambios: "No se pudo eliminar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosCompradores(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

    }
}
