using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Seguridad
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            //Se limpia toda la sesion del usuario
            HttpContext.Session.Clear();

            //Se envia nuevamente al login
            return RedirectToPage("/Seguridad/Login");
        }
    }
}
