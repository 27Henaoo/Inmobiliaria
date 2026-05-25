using LibModelos._5._2LoginRegisterEntidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._8.Seguridad;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Seguridad
{
    public class LoginModel : PageModel
    {
        private IUsuariosNegocio? iUsuariosNegocio;

        [BindProperty] public Usuarios? Usuario { get; set; }

        public LoginModel()
        {
            iUsuariosNegocio = new UsuariosNegocio();
        }

        public void OnGet()
        {
            Usuario = new Usuarios();
        }

        public IActionResult OnPostBtIngresar()
        {
            try
            {
                if (Usuario == null)
                    return Page();

                //Se completan campos que el modelo exige, aunque para login no se usen
                if (Usuario.Nombre == null)
                    Usuario.Nombre = "";

                if (Usuario.Correo == null)
                    Usuario.Correo = "";

                if (Usuario.ClaveHash == null)
                    Usuario.ClaveHash = "";

                if (Usuario.ClaveSalt == null)
                    Usuario.ClaveSalt = "";

                Usuario = iUsuariosNegocio!.ValidarLogin(Usuario);

                if (Usuario == null || Usuario.Id == 0)
                {
                    ViewData["Mensaje"] = "Correo o clave incorrectos.";
                    return Page();
                }

                HttpContext.Session.SetInt32("UsuarioId", Usuario.Id);
                HttpContext.Session.SetString("UsuarioNombre", Usuario.Nombre);
                HttpContext.Session.SetInt32("UsuarioRol", Usuario.Rol);

                if (Usuario._Rol != null)
                    HttpContext.Session.SetString("UsuarioRolNombre", Usuario._Rol.Nombre);
                else
                    HttpContext.Session.SetString("UsuarioRolNombre", "");

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    ViewData["Mensaje"] = ex.InnerException.Message;
                else
                    ViewData["Mensaje"] = ex.Message;

                return Page();
            }
        }

        public IActionResult OnPostBtRegister()
        {
            return RedirectToPage("/Seguridad/Register");
        }
    }
}
