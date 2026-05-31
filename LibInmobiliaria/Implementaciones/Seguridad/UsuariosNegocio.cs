
using LibInmobiliaria.Interfaces;
using LibInmobiliaria.Interfaces.Seguridad;
using LibModelos._5._2LoginRegisterEntidades;
using LibModelos._5._1ModelosComunes;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibInmobiliaria.Implementaciones.Seguridad
{
    public class UsuariosNegocio : IUsuariosNegocio
    {
        private string ObtenerDatosUsuarios(Usuarios entidad)
        {
            return $"Id: {entidad.Id}, " +
                   $"Nombre: {entidad.Nombre}, " +
                   $"Correo: {entidad.Correo}, " +
                   $"ClaveHash: {"Oculto"}, " +
                   $"ClaveSalt: {"Oculto"}, " +
                   $"Rol: {entidad.Rol}, " +
                   $"_Rol: {entidad._Rol}, ";
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
                Tabla = "Usuarios",
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

        public List<Usuarios> Consultar()
        {
            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                //Se consultan todos los usuarios con su rol, no se hce desde Roles porque puede haber filtracion de datos
                var lista = this.iConexion.Usuarios!.Include(x => x._Rol).ToList();

                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Se consultaron los registros de Usuarios",
                    cambios: "No se modificaron datos",
                    valorAnterior: null,
                    valorNuevo: $"Cantidad de registros consultados: {lista.Count}",
                    exitoso: true,
                    error: "N/A"
                );

                this.iConexion.SaveChanges();

                //Se limpian los datos sensibles antes de devolverlos.
                foreach (var usuario in lista)
                {
                    usuario.ClaveHash = "";
                    usuario.ClaveSalt = "";
                }

                return lista;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Consultar",
                    registroId: null,
                    descripcion: "Fallo al consultar los registros de Usuarios",
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

        public Usuarios Guardar(Usuarios entidad)
        {
            if (entidad.Id != 0)
                return CrearUsuarioVacio();

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                return CrearUsuarioVacio();

            if (string.IsNullOrWhiteSpace(entidad.Correo))
                return CrearUsuarioVacio();

            if (string.IsNullOrWhiteSpace(entidad.ClaveHash))
                return CrearUsuarioVacio();

            if (entidad.Rol == 0)
                return CrearUsuarioVacio();

            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("string_conexion");

            try
            {
                var correo = entidad.Correo.Trim().ToLower();

                var usuarioExistente = this.iConexion.Usuarios!
                    .FirstOrDefault(x => x.Correo.ToLower() == correo);

                if (usuarioExistente != null)
                {
                    AgregarHistorico(
                        accion: "Guardar",
                        registroId: entidad.Id,
                        descripcion: "Fallo al guardar un registro de Usuarios",
                        cambios: "No se pudo crear el registro",
                        valorAnterior: null,
                        valorNuevo: ObtenerDatosUsuarios(entidad),
                        exitoso: false,
                        error: "El correo ya se encuentra registrado"
                    );

                    this.iConexion.SaveChanges();
                    return CrearUsuarioVacio();
                }

                var claveOriginal = entidad.ClaveHash;

                entidad.Correo = correo;
                entidad.ClaveSalt = CrearSalt();
                entidad.ClaveHash = CrearHash(claveOriginal, entidad.ClaveSalt);

                this.iConexion.Usuarios!.Add(entidad);

                AgregarHistorico(
                    accion: "Guardar",
                    registroId: null,
                    descripcion: "Se guardo un nuevo registro de Usuarios",
                    cambios: "Se creo un nuevo registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosUsuarios(entidad),
                    exitoso: true,
                    error: "N/A"
                );

                this.iConexion.SaveChanges();

                entidad.ClaveHash = "";
                entidad.ClaveSalt = "";

                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Guardar",
                    registroId: entidad.Id,
                    descripcion: "Fallo al guardar un registro de Usuarios",
                    cambios: "No se pudo crear el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosUsuarios(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        public Usuarios Modificar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede modificar");

            if (string.IsNullOrWhiteSpace(entidad.Nombre))
                throw new Exception("Debe ingresar el nombre.");

            if (string.IsNullOrWhiteSpace(entidad.Correo))
                throw new Exception("Debe ingresar el correo.");

            if (entidad.Rol == 0)
                throw new Exception("Debe seleccionar un rol.");

            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                var usuarioActual = this.iConexion.Usuarios!.FirstOrDefault(x => x.Id == entidad.Id);

                if (usuarioActual == null)
                    throw new Exception("El usuario no existe.");

                string valorAnterior = ObtenerDatosUsuarios(usuarioActual);

                entidad.Correo = entidad.Correo.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(entidad.ClaveHash))
                {
                    entidad.ClaveHash = usuarioActual.ClaveHash;
                    entidad.ClaveSalt = usuarioActual.ClaveSalt;
                }
                else
                {
                    var claveOriginal = entidad.ClaveHash;

                    entidad.ClaveSalt = CrearSalt();
                    entidad.ClaveHash = CrearHash(claveOriginal, entidad.ClaveSalt);
                }

                string valorNuevo = ObtenerDatosUsuarios(entidad);

                var entry = this.iConexion.Entry<Usuarios>(entidad);

                entry.State = EntityState.Modified;

                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Se modifico un registro de Usuarios",
                    cambios: "Se cambio la informacion del registro",
                    valorAnterior: valorAnterior,
                    valorNuevo: valorNuevo,
                    exitoso: true,
                    error: "N/A"
                );

                this.iConexion.SaveChanges();

                entidad.ClaveHash = "";
                entidad.ClaveSalt = "";

                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Modificar",
                    registroId: entidad.Id,
                    descripcion: "Fallo al modificar un registro de Usuarios",
                    cambios: "No se pudo modificar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosUsuarios(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        public Usuarios Borrar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se puede borrar");

            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            try
            {
                var anterior = this.iConexion.Usuarios!.FirstOrDefault(x => x.Id == entidad.Id);

                if (anterior == null)
                    throw new Exception("El usuario no existe.");

                string valorAnterior = ObtenerDatosUsuarios(anterior);

                this.iConexion.Usuarios!.Remove(entidad);

                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Se borro un registro de Usuarios",
                    cambios: "Se elimino el registro",
                    valorAnterior: valorAnterior,
                    valorNuevo: null,
                    exitoso: true,
                    error: "N/A"
                );

                this.iConexion.SaveChanges();

                return entidad;
            }
            catch (Exception ex)
            {
                AgregarHistorico(
                    accion: "Borrar",
                    registroId: entidad.Id,
                    descripcion: "Fallo al borrar un registro de Usuarios",
                    cambios: "No se pudo eliminar el registro",
                    valorAnterior: null,
                    valorNuevo: ObtenerDatosUsuarios(entidad),
                    exitoso: false,
                    error: ex.Message
                );

                this.iConexion.SaveChanges();
                throw;
            }
        }

        public Usuarios? BuscarPorCorreo(string correo)
        {
            this.iConexion = new Conexion();

            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            correo = correo.Trim().ToLower();

            var usuario = this.iConexion.Usuarios!.Include(x => x._Rol).FirstOrDefault(x => x.Correo.ToLower() == correo);

            if (usuario != null && usuario._Rol != null)
                usuario._Rol.Usuarios = null;

            return usuario;
        }

        public Usuarios? ValidarLogin(string correo, string clave)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return CrearUsuarioVacio();

            if (string.IsNullOrWhiteSpace(clave))
                return CrearUsuarioVacio();

            var usuario = BuscarPorCorreo(correo);

            if (usuario == null)
                return CrearUsuarioVacio();

            var claveCorrecta = ValidarClave(
                clave,
                usuario.ClaveSalt,
                usuario.ClaveHash
            );

            if (!claveCorrecta)
                return CrearUsuarioVacio();

            usuario.ClaveHash = "";
            usuario.ClaveSalt = "";

            if (usuario._Rol != null)
                usuario._Rol.Usuarios = null;

            return usuario;
        }

        private string CrearSalt()
        {
            var bytes = RandomNumberGenerator.GetBytes(32);

            return Convert.ToBase64String(bytes);
        }

        private Usuarios CrearUsuarioVacio()
        {
            return new Usuarios()
            {
                Id = 0,
                Nombre = "",
                Correo = "",
                ClaveHash = "",
                ClaveSalt = "",
                Rol = 0
            };
        }

        private string CrearHash(string clave, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var claveBytes = Encoding.UTF8.GetBytes(clave);

            var datos = new byte[saltBytes.Length + claveBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, datos, 0, saltBytes.Length);
            Buffer.BlockCopy(claveBytes, 0, datos, saltBytes.Length, claveBytes.Length);

            using var sha256 = SHA256.Create();

            var hashBytes = sha256.ComputeHash(datos);

            return Convert.ToBase64String(hashBytes);
        }

        private bool ValidarClave(string clave, string salt, string hashGuardado)
        {
            var hashIngresado = CrearHash(clave, salt);

            return hashIngresado == hashGuardado;
        }
    }
}
