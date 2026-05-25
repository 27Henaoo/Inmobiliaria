using LibModelos._5._2LoginRegisterEntidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._8.Seguridad;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._8.Seguridad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Seguridad
{
    public class RegisterModel : PageModel
    {
        private IUsuariosNegocio? iUsuariosNegocio;

        [BindProperty] public Usuarios? Usuario { get; set; }

        [BindProperty] public string? ClaveConfirmacion { get; set; }

        public RegisterModel()
        {
            iUsuariosNegocio = new UsuariosNegocio();
        }

        public void OnGet()
        {
            Usuario = new Usuarios();
        }

        public IActionResult OnPostBtRegistrar()
        {
            try
            {
                if (Usuario == null)
                    return Page();

                if (string.IsNullOrWhiteSpace(Usuario.Nombre))
                    throw new Exception("Debe ingresar el nombre.");

                if (string.IsNullOrWhiteSpace(Usuario.Correo))
                    throw new Exception("Debe ingresar el correo.");

                if (string.IsNullOrWhiteSpace(Usuario.ClaveHash))
                    throw new Exception("Debe ingresar la clave.");

                if (Usuario.ClaveHash != ClaveConfirmacion)
                    throw new Exception("Las claves no coinciden.");

                //Rol 3 = Guest
                Usuario.Rol = 3;

                //Se completa porque el modelo Usuarios lo exige.
                Usuario.ClaveSalt = "";

                Usuario = iUsuariosNegocio!.Guardar(Usuario);

                if (Usuario.Id == 0)
                    return Page();

                return RedirectToPage("/Seguridad/Login");
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

        public IActionResult OnPostBtLogin()
        {
            return RedirectToPage("/Seguridad/Login");
        }
    }
}
