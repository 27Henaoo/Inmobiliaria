using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._2.Sujetos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class ExpedientesLaboralesModel : PageModel
    {
        //Para combos
        private IPersonasNegocio? iPersonasNegocio;
        public List<Personas> Personas { get; set; } = new List<Personas>();

        private IExpedientesLaboralesNegocio? iExpedientesLaboralesNegocio;

        [BindProperty] public List<ExpedientesLaborales>? Lista { get; set; }
        [BindProperty] public ExpedientesLaborales? ExpedienteLaboral { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ExpedientesLaboralesModel()
        {
            iExpedientesLaboralesNegocio = new ExpedientesLaboralesNegocio();
            iPersonasNegocio = new PersonasNegocio();
        }

        private void CargarCombos()
        {
            Personas = iPersonasNegocio!.Consultar();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iExpedientesLaboralesNegocio == null)
                    return;

                Lista = iExpedientesLaboralesNegocio.Consultar();
                CargarCombos();
                ExpedienteLaboral = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            CargarCombos();

            ExpedienteLaboral = new ExpedientesLaborales()
            {
                FechaIngreso = DateTime.Now,
                FechaEgreso = DateTime.Now
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                ExpedienteLaboral = Lista!.FirstOrDefault(x => x.Id == data);

                Lista = null;
                Borrando = false;
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (ExpedienteLaboral == null)
                    return;

                if (ExpedienteLaboral.Id == 0)
                    ExpedienteLaboral = iExpedientesLaboralesNegocio!.Guardar(ExpedienteLaboral!);
                else
                    ExpedienteLaboral = iExpedientesLaboralesNegocio!.Modificar(ExpedienteLaboral!);

                if (ExpedienteLaboral.Id == 0)
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
                if (ExpedienteLaboral == null)
                    return;

                ExpedienteLaboral = iExpedientesLaboralesNegocio!.Borrar(ExpedienteLaboral!);

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

                ExpedienteLaboral = Lista!.FirstOrDefault(x => x.Id == data);

                Lista = null;
                Borrando = true;
            }
            catch (Exception ex)
            {
                CargarCombos();
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