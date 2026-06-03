using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.RRHH;
using Microsoft.EntityFrameworkCore;


namespace LibInmobiliaria.Implementaciones.RRHH
{
    public class EmpleadosSectoresNegocio : IEmpleadosSectoresNegocio
    {
        private string ObtenerDatosEmpleadosSectores(EmpleadosSectores entidad)
        {
            return $"Id: {entidad.Id}, " +
                   $"Cedula: {entidad.Cedula}, " +
                   $"Nombre: {entidad.Nombre}, " +
                   $"Apellido: {entidad.Apellido}, " +
                   $"Genero: {entidad.Genero}, " +
                   $"Correo: {entidad.Correo}, " +
                   $"FechaNacimiento: {entidad.FechaNacimiento}, " +
                   $"FechaRegistro: {entidad.FechaRegistro}, " +
                   $"EstadoCivil: {entidad.EstadoCivil}, " +
                   $"Nacionalidad: {entidad.Nacionalidad}, " +
                   $"_EstadoCivil: {entidad._EstadoCivil}, " +
                   $"_Nacionalidad: {entidad._Nacionalidad}, " +
                   $"Telefonos: {entidad.Telefonos}, " +
                   $"Direcciones: {entidad.Direcciones}, " +
                   $"ExpedientesLaborales: {entidad.ExpedientesLaborales}, " +
                   $"Sueldo: {entidad.Sueldo}, " +
                   $"Estado: {entidad.Estado}, " +
                   $"Jornada: {entidad.Jornada}, " +
                   $"JefeSector: {entidad.JefeSector}, " +
                   $"TipoContrato: {entidad.TipoContrato}, " +
                   $"Sector: {entidad.Sector}, " +
                   $"_Sector: {entidad._Sector}, " +
                   $"_TipoContrato: {entidad._TipoContrato}, " +
                   $"_JefeSector: {entidad._JefeSector}, " +
                   $"EmpleadosCompradores: {entidad.EmpleadosCompradores}, " +
                   $"ContratosEmpleados: {entidad.ContratosEmpleados}, ";
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
                Tabla = "EmpleadosSectores",
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

        // Método para consultar todos los EmpleadosSectores.
        public List<EmpleadosSectores> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Se consultan los registros de EmpleadosSectores en forma de lista.
                var lista = this.iConexion.EmpleadosSectores!.Include(x => x._EstadoCivil).Include(x => x._Nacionalidad).Include(x => x.Telefonos).Include(x => x.Direcciones)
                       .Include(x => x.ExpedientesLaborales).Include(x => x._Sector).Include(x => x._TipoContrato).Include(x => x._JefeSector).Include(x => x.EmpleadosCompradores!)
                       .ThenInclude(x => x._Comprador).Include(x => x.ContratosEmpleados!).ThenInclude(x => x._Contrato).ToList();

                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Se consultaron los registros de EmpleadosSectores",
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
                    descripcion: "Fallo al consultar los registros de EmpleadosSectores",
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

        // Método para guardar un avión nuevo.
        public EmpleadosSectores Guardar(EmpleadosSectores entidad)
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

                // Se agrega la entidad al conjunto de EmpleadosSectores.
                this.iConexion.EmpleadosSectores!.Add(entidad);

                AgregarHistorico(
                    accion: "Guardar",
                    registroId: null,
                    descripcion: "Se guardo un nuevo registro de EmpleadosSectores",
                    cambios: "Se creo un nuevo registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosSectores(entidad),
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
                    descripcion: "Fallo al guardar un registro de EmpleadosSectores",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosSectores(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para modificar un avión existente.
        public EmpleadosSectores Modificar(EmpleadosSectores entidad)
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

                var anterior = this.iConexion.EmpleadosSectores!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de EmpleadosSectores no existe");

                string valorAnterior = ObtenerDatosEmpleadosSectores(anterior);
                string valorNuevo = ObtenerDatosEmpleadosSectores(entidad);

                // Se obtiene la entrada de Entity Framework para la entidad recibida.
                var entry = this.iConexion.Entry<EmpleadosSectores>(entidad);

                // Se marca la entidad como modificada.
                entry.State = EntityState.Modified;

                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Se modifico un registro de EmpleadosSectores",
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
                    descripcion: "Fallo al modificar un registro de EmpleadosSectores",
                    cambios: "No se pudo modificar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosSectores(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para borrar un avión existente.
        public EmpleadosSectores Borrar(EmpleadosSectores entidad)
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

                var anterior = this.iConexion.EmpleadosSectores!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de EmpleadosSectores no existe");

                string valorAnterior = ObtenerDatosEmpleadosSectores(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.EmpleadosSectores!.Remove(entidad);

                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Se borro un registro de EmpleadosSectores",
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
                    descripcion: "Fallo al borrar un registro de EmpleadosSectores",
                    cambios: "No se pudo eliminar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosEmpleadosSectores(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
    }
}
