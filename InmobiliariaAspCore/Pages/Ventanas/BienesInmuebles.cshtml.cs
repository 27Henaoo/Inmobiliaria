using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._5.Financiero;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._6.Patrimonio;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._5.Financiero;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._6.Patrimonio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class BienesInmueblesModel : PageModel
    {
        // Para combos

        private IExpedientesFinancierosNegocio? iExpedientesFinancierosNegocio;
        public List<ExpedientesFinancieros> ExpedientesFinancieros { get; set; } = new List<ExpedientesFinancieros>();

        private IBienesInmueblesNegocio? iBienesInmueblesNegocio;

        [BindProperty] public List<BienesInmuebles>? Lista { get; set; }
        [BindProperty] public BienesInmuebles? BienInmueble { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public BienesInmueblesModel()
        {
            iBienesInmueblesNegocio = new BienesInmueblesNegocio();
            iExpedientesFinancierosNegocio = new ExpedientesFinancierosNegocio();
        }

        private void CargarCombos()
        {
            ExpedientesFinancieros = iExpedientesFinancierosNegocio!.Consultar();
        }

        private void ValidarExpediente()
        {
            if (BienInmueble == null)
                return;

            if (BienInmueble.ExpedienteFinanciero == 0)
                throw new Exception("Debe seleccionar un expediente financiero.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iBienesInmueblesNegocio == null)
                    return;

                Lista = iBienesInmueblesNegocio.Consultar();
                CargarCombos();
                BienInmueble = null;
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

            BienInmueble = new BienesInmuebles()
            {
                FechaAdquisicion = DateTime.Now
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                BienInmueble = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (BienInmueble == null)
                    return;

                ValidarExpediente();

                if (BienInmueble.Id == 0)
                    BienInmueble = iBienesInmueblesNegocio!.Guardar(BienInmueble!);
                else
                    BienInmueble = iBienesInmueblesNegocio!.Modificar(BienInmueble!);

                if (BienInmueble.Id == 0)
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
                if (BienInmueble == null)
                    return;

                BienInmueble = iBienesInmueblesNegocio!.Borrar(BienInmueble!);

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

                BienInmueble = Lista!.FirstOrDefault(x => x.Id == data);

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