using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class CiudadesModel : PageModel
    {
        //Paquete para combos 

        private IDepartamentosNegocio? iDepartamentosNegocio;
        public List<Departamentos> Departamentos { get; set; } = new List<Departamentos>();

        private ICiudadesNegocio? iCiudadesNegocio;
        [BindProperty] public List<Ciudades>? Lista { get; set; }
        [BindProperty] public Ciudades? Ciudad { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public CiudadesModel()
        {
            iCiudadesNegocio = new CiudadesNegocio();
            iDepartamentosNegocio = new DepartamentosNegocio();
        }

        private void CargarCombos()
        {
            Departamentos = iDepartamentosNegocio!.Consultar();

        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iCiudadesNegocio == null)
                    return;
                Lista = iCiudadesNegocio.Consultar();
                CargarCombos();
                Ciudad = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            CargarCombos();
            Ciudad = new Ciudades()
            {
                FechaCreacion = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Ciudad = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Ciudad == null)
                    return;
                if (Ciudad.Id == 0)
                    Ciudad = iCiudadesNegocio!.Guardar(Ciudad!);
                else
                    Ciudad = iCiudadesNegocio!.Modificar(Ciudad!);
                if (Ciudad.Id == 0)
                    return;
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (Ciudad == null)
                    return;
                Ciudad = iCiudadesNegocio!.Borrar(Ciudad!);
                OnPostBtRefrescar();
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtBorrarVal(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Ciudad = Lista!.FirstOrDefault(x => x.Id == data);
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

