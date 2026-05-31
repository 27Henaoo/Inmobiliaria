
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Seguridad;
using LibModelos._5._2LoginRegisterEntidades;
using Microsoft.EntityFrameworkCore;

using LibModelos._5._1ModelosComunes;
namespace LibInmobiliaria.Implementaciones.Seguridad
{
    public class RolesNegocio : IRolesNegocio
    {
        private string ObtenerDatosRoles(Roles entidad)
        {
            return $"Id: {entidad.Id}, " +
                   $"Nombre: {entidad.Nombre}, " +
                   $"Usuarios: {entidad.Usuarios}, ";
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
                Tabla = "Roles",
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


        private IConexion? iConexion;

        public List<Roles> Consultar()
        {
            // Se crea una nueva conexión.
            this.iConexion = new Conexion();

            // Se asigna la cadena de conexión.
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                // Se consultan los registros de Roles en forma de lista.
                var lista = this.iConexion.Roles!.ToList();

                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Se consultaron los registros de Roles",
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
                    descripcion: "Fallo al consultar los registros de Roles",
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

        public Roles Guardar(Roles entidad)
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

                // Se agrega la entidad al conjunto de Roles.
                this.iConexion.Roles!.Add(entidad);

                AgregarHistorico(
                    accion: "Guardar",
                    registroId: null,
                    descripcion: "Se guardo un nuevo registro de Roles",
                    cambios: "Se creo un nuevo registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosRoles(entidad),
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
                    descripcion: "Fallo al guardar un registro de Roles",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosRoles(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        public Roles Modificar(Roles entidad)
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

                var anterior = this.iConexion.Roles!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de Roles no existe");

                string valorAnterior = ObtenerDatosRoles(anterior);
                string valorNuevo = ObtenerDatosRoles(entidad);

                // Se obtiene la entrada de Entity Framework para la entidad recibida.
                var entry = this.iConexion.Entry<Roles>(entidad);

                // Se marca la entidad como modificada.
                entry.State = EntityState.Modified;

                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Se modifico un registro de Roles",
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
                    descripcion: "Fallo al modificar un registro de Roles",
                    cambios: "No se pudo modificar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosRoles(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        public Roles Borrar(Roles entidad)
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

                var anterior = this.iConexion.Roles!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El registro de Roles no existe");

                string valorAnterior = ObtenerDatosRoles(anterior);

                // Se marca la entidad para eliminarla.
                this.iConexion.Roles!.Remove(entidad);

                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Se borro un registro de Roles",
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
                    descripcion: "Fallo al borrar un registro de Roles",
                    cambios: "No se pudo eliminar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosRoles(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }
    }
}
