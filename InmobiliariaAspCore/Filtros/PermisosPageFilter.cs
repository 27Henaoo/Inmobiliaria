using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InmobiliariaAspCore.Filtros
{
    public class PermisosPageFilter : IAsyncPageFilter
    {
        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var rol = context.HttpContext.Session.GetString("UsuarioRolNombre");

            if (string.IsNullOrWhiteSpace(rol))
                rol = "Guest";

            var ruta = context.HttpContext.Request.Path.Value;

            if (!TieneAccesoPagina(ruta, rol))
            {
                context.Result = new RedirectToPageResult("/Index");
                return;
            }

            var handler = ObtenerHandler(context);

            if (!TienePermisoHandler(handler, rol, context.HttpContext.Request.Method))
            {
                context.Result = new RedirectToPageResult("/Index");
                return;
            }

            await next();
        }

        private string ObtenerHandler(PageHandlerExecutingContext context)
        {
            var handler = "";

            if (context.HandlerMethod != null && context.HandlerMethod.MethodInfo != null)
                handler = context.HandlerMethod.MethodInfo.Name;

            var handlerQuery = context.HttpContext.Request.Query["handler"].ToString();

            if (!string.IsNullOrWhiteSpace(handlerQuery))
                handler = handlerQuery;

            handler = handler.Replace("OnPost", "");
            handler = handler.Replace("OnGet", "");
            handler = handler.Replace("Async", "");

            return handler.Trim().ToLower();
        }

        private bool TienePermisoHandler(string handler, string rol, string metodo)
        {
            if (!metodo.Equals("POST", StringComparison.OrdinalIgnoreCase))
                return true;

            if (rol.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                return true;

            var accionesBloqueadasGuest = new List<string>()
            {
                "btnuevo",
                "btguardar",
                "btmodificar",
                "btborrar",
                "btborrarval"
            };

            var accionesBloqueadasEjecutivo = new List<string>()
            {
                "btborrar",
                "btborrarval"
            };

            if (rol.Equals("Ejecutivo", StringComparison.OrdinalIgnoreCase))
            {
                if (accionesBloqueadasEjecutivo.Contains(handler))
                    return false;

                return true;
            }

            if (rol.Equals("Guest", StringComparison.OrdinalIgnoreCase) || rol.Equals("Invitado", StringComparison.OrdinalIgnoreCase))
            {
                if (accionesBloqueadasGuest.Contains(handler))
                    return false;

                return true;
            }

            return false;
        }

        private bool TieneAccesoPagina(string? ruta, string rol)
        {
            if (string.IsNullOrWhiteSpace(ruta))
                return true;

            ruta = ruta.ToLower().TrimEnd('/');

            //Estas paginas siempre quedan permitidas.
            if (ruta == "" ||
                ruta == "/" ||
                ruta == "/index" ||
                ruta == "/privacy" ||
                ruta.StartsWith("/seguridad"))
                return true;

            //Si no es una ventana CRUD, se permite.
            if (!ruta.StartsWith("/ventanas"))
                return true;

            if (rol.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                return true;

            var paginasGuest = new List<string>()
            {
                "/ventanas/propiedades",
                "/ventanas/tipospropiedades",
                "/ventanas/mapapropiedades",
                "/ventanas/explorarpropiedades"
            };

            var paginasEjecutivo = new List<string>()
            {
                "/ventanas/propiedades",
                "/ventanas/mapapropiedades",
                "/ventanas/explorarpropiedades",
                "/ventanas/clientes",
                "/ventanas/compradores",
                "/ventanas/codeudores",
                "/ventanas/contratos",

                "/ventanas/empleadossectores",
                "/ventanas/jefessectores",
                "/ventanas/sectores",
                "/ventanas/expedienteslaborales",

                "/ventanas/empleadoscompradores",
                "/ventanas/codeudorescompradores",
                "/ventanas/contratosempleados",
                "/ventanas/contratoscodeudores"
            };

            if (rol.Equals("Ejecutivo", StringComparison.OrdinalIgnoreCase))
                return paginasEjecutivo.Contains(ruta);

            if (rol.Equals("Guest", StringComparison.OrdinalIgnoreCase) ||rol.Equals("Invitado", StringComparison.OrdinalIgnoreCase))
                return paginasGuest.Contains(ruta);

            return paginasGuest.Contains(ruta);
        }
    }
}