using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class DepartamentosModel : PageModel
    {

        private IDepartamentosNegocio? iDepartamentosNegocio;
        [BindProperty] public List<Departamentos>? Lista { get; set; }
        [BindProperty] public Departamentos? Departamento { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public DepartamentosModel()
        {
            iDepartamentosNegocio = new DepartamentosNegocio();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iDepartamentosNegocio == null)
                    return;
                Lista = iDepartamentosNegocio.Consultar();
                Departamento = null;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            Departamento = new Departamentos()
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
                Departamento = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Departamento == null)
                    return;
                if (Departamento.Id == 0)
                    Departamento = iDepartamentosNegocio!.Guardar(Departamento!);
                else
                    Departamento = iDepartamentosNegocio!.Modificar(Departamento!);
                if (Departamento.Id == 0)
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
                if (Departamento == null)
                    return;
                Departamento = iDepartamentosNegocio!.Borrar(Departamento!);
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
                Departamento = Lista!.FirstOrDefault(x => x.Id == data);
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
