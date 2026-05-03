using LibInmobiliaria.Entidades;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class NacionalidadesModel : PageModel
    {
        private INacionalidadesNegocio? iNacionalidadesNegocio;
        [BindProperty] public List<Nacionalidades>? Lista { get; set; }
        [BindProperty] public Nacionalidades? Nacionalidad { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public NacionalidadesModel()
        {
            iNacionalidadesNegocio = new NacionalidadesNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iNacionalidadesNegocio == null)
                    return;
                Lista = iNacionalidadesNegocio.Consultar();
                Nacionalidad = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Nacionalidad = new Nacionalidades()
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
                Nacionalidad = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Nacionalidad == null)
                    return;
                if (Nacionalidad.Id == 0)
                    Nacionalidad = iNacionalidadesNegocio!.Guardar(Nacionalidad!);
                else
                    Nacionalidad = iNacionalidadesNegocio!.Modificar(Nacionalidad!);
                if (Nacionalidad.Id == 0)
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
                if (Nacionalidad == null)
                    return;
                Nacionalidad = iNacionalidadesNegocio!.Borrar(Nacionalidad!);
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
                Nacionalidad = Lista!.FirstOrDefault(x => x.Id == data);
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
