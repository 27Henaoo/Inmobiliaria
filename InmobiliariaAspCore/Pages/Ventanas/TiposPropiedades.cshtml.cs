using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class TiposPropiedadesModel : PageModel
    {
        private ITiposPropiedadesNegocio? iTiposPropiedadesNegocio;
        [BindProperty] public List<TiposPropiedades>? Lista { get; set; }
        [BindProperty] public TiposPropiedades? TipoPropiedad { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TiposPropiedadesModel()
        {
            iTiposPropiedadesNegocio = new TiposPropiedadesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iTiposPropiedadesNegocio == null)
                    return;
                Lista = iTiposPropiedadesNegocio.Consultar();
                TipoPropiedad = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            TipoPropiedad = new TiposPropiedades()
            {
                //Fecha = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                TipoPropiedad = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (TipoPropiedad == null)
                    return;
                if (TipoPropiedad.Id == 0)
                    TipoPropiedad = iTiposPropiedadesNegocio!.Guardar(TipoPropiedad!);
                else
                    TipoPropiedad = iTiposPropiedadesNegocio!.Modificar(TipoPropiedad!);
                if (TipoPropiedad.Id == 0)
                    return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (TipoPropiedad == null)
                    return;
                TipoPropiedad = iTiposPropiedadesNegocio!.Borrar(TipoPropiedad!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                TipoPropiedad = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtCerrar()
        {
            OnPostBtRefrescar();
            Borrando = false;
        }
    }
}
