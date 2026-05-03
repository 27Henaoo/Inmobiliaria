using LibInmobiliaria.Entidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class EstadosCivilesModel : PageModel
    {
        private IEstadosCivilesNegocio? iEstadosCivilesNegocio;
        [BindProperty] public List<EstadosCiviles>? Lista { get; set; }
        [BindProperty] public EstadosCiviles? EstadoCivil { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public EstadosCivilesModel()
        {
            iEstadosCivilesNegocio = new EstadosCivilesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iEstadosCivilesNegocio == null)
                    return;
                Lista = iEstadosCivilesNegocio.Consultar();
                EstadoCivil = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            EstadoCivil = new EstadosCiviles()
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
                EstadoCivil = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (EstadoCivil == null)
                    return;
                if (EstadoCivil.Id == 0)
                    EstadoCivil = iEstadosCivilesNegocio!.Guardar(EstadoCivil!);
                else
                    EstadoCivil = iEstadosCivilesNegocio!.Modificar(EstadoCivil!);
                if (EstadoCivil.Id == 0)
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
                if (EstadoCivil == null)
                    return;
                EstadoCivil = iEstadosCivilesNegocio!.Borrar(EstadoCivil!);
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
                EstadoCivil = Lista!.FirstOrDefault(x => x.Id == data);
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
