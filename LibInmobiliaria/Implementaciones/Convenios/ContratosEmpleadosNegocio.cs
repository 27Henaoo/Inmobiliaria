using LibModelos._5._1ModelosComunes;
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Convenios;
using Microsoft.EntityFrameworkCore;

namespace LibInmobiliaria.Implementaciones.Convenios
{
    public class ContratosEmpleadosNegocio : IContratosEmpleadosNegocio
    {
        private string ObtenerDatosContratosEmpleados(ContratosEmpleados entidad)
        {
            return $"Id: {entidad.Id}, " +
                   $"FechaCierre: {entidad.FechaCierre}, " +
                   $"PrecioAcordado: {entidad.PrecioAcordado}, " +
                   $"VendidaArrendada: {entidad.VendidaArrendada}, " +
                   $"Empleado: {entidad.Empleado}, " +
                   $"Contrato: {entidad.Contrato}, " +
                   $"_Empleado: {entidad._Empleado}, " +
                   $"_Contrato: {entidad._Contrato}, ";
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
                Tabla = "ContratosEmpleados",
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

        public List<ContratosEmpleados> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Se consultan los registros de ContratosEmpleados en forma de lista.
                var lista = this.iConexion.ContratosEmpleados!.Include(x => x._Contrato!).ThenInclude(x => x._Cliente).Include(x => x._Contrato!)
            .ThenInclude(x => x._Propiedad!).ThenInclude(x => x._TipoPropiedad).Include(x => x._Contrato!).ThenInclude(x => x._Comprador)
            .Include(x => x._Contrato!).ThenInclude(x => x._JefeSector!).ThenInclude(x => x._Sector).Include(x => x._Empleado!)
            .ThenInclude(x => x._EstadoCivil).Include(x => x._Empleado!).ThenInclude(x => x._Nacionalidad).Include(x => x._Empleado!)
            .ThenInclude(x => x.Telefonos).Include(x => x._Empleado!).ThenInclude(x => x.Direcciones).Include(x => x._Empleado!)
            .ThenInclude(x => x.ExpedientesLaborales).Include(x => x._Empleado!).ThenInclude(x => x._Sector).Include(x => x._Empleado!)
            .ThenInclude(x => x._JefeSector).Include(x => x._Empleado!).ThenInclude(x => x._TipoContrato).ToList();

                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Se consultaron los registros de ContratosEmpleados",
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
                    descripcion: "Fallo al consultar los registros de ContratosEmpleados",
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

        // Método para guardar ContratosEmpleados nuevos.
        public ContratosEmpleados Guardar(ContratosEmpleados entidad)
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

                // Se agrega la entidad al conjunto de ContratosEmpleados.
                this.iConexion.ContratosEmpleados!.Add(entidad);

                AgregarHistorico(
                    accion: "Guardar",
                    registroId: null,
                    descripcion: "Se guardo un nuevo registro de ContratosEmpleados",
                    cambios: "Se creo un nuevo registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosContratosEmpleados(entidad),
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
                    descripcion: "Fallo al guardar un registro de ContratosEmpleados",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosContratosEmpleados(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        // Método para modificar ContratosEmpleados existentes.
        public ContratosEmpleados Modificar(ContratosEmpleados entidad)
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

                var anterior = this.iConexion.ContratosEmpleados!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de ContratosEmpleados no existe");

                string valorAnterior = ObtenerDatosContratosEmpleados(anterior);
                string valorNuevo = ObtenerDatosContratosEmpleados(entidad);

                // Se obtiene la entrada de Entity Framework para la entidad recibida.
                var entry = this.iConexion.Entry<ContratosEmpleados>(entidad);

                // Se marca la entidad como modificada.
                entry.State = EntityState.Modified;

                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Se modifico un registro de ContratosEmpleados",
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
                    descripcion: "Fallo al modificar un registro de ContratosEmpleados",
                    cambios: "No se pudo modificar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosContratosEmpleados(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
        // Método para borrar ContratosEmpleados existentes.
        public ContratosEmpleados Borrar(ContratosEmpleados entidad)
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

                var anterior = this.iConexion.ContratosEmpleados!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de ContratosEmpleados no existe");

                string valorAnterior = ObtenerDatosContratosEmpleados(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.ContratosEmpleados!.Remove(entidad);

                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Se borro un registro de ContratosEmpleados",
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
                    descripcion: "Fallo al borrar un registro de ContratosEmpleados",
                    cambios: "No se pudo eliminar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosContratosEmpleados(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
    }
}
