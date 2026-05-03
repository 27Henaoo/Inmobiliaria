using LibInmobiliaria.Entidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class TiposContratosModel : PageModel
    {
        private ITiposContratosNegocio? iTiposContratosNegocio;
        [BindProperty] public List<TiposContratos>? Lista { get; set; }
        [BindProperty] public TiposContratos? TipoContrato { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TiposContratosModel()
        {
            iTiposContratosNegocio = new TiposContratosNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iTiposContratosNegocio == null)
                    return;
                Lista = iTiposContratosNegocio.Consultar();
                TipoContrato = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            TipoContrato = new TiposContratos()
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
                TipoContrato = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (TipoContrato == null)
                    return;
                if (TipoContrato.Id == 0)
                    TipoContrato = iTiposContratosNegocio!.Guardar(TipoContrato!);
                else
                    TipoContrato = iTiposContratosNegocio!.Modificar(TipoContrato!);
                if (TipoContrato.Id == 0)
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
                if (TipoContrato == null)
                    return;
                TipoContrato = iTiposContratosNegocio!.Borrar(TipoContrato!);
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
                TipoContrato = Lista!.FirstOrDefault(x => x.Id == data);
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
