using LibInmobiliaria.Entidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class EstadosCivilesModel : PageModel
    {
        [BindProperty] public List<EstadosCiviles>? Lista { get; set; }
        public void OnGet()
        {
            IEstadosCivilesNegocio iEstadosCivilesNegocio = new EstadosCivilesNegocio();
            Lista = iEstadosCivilesNegocio.Consultar();
        }
    }
}
