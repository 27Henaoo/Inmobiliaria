using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._7.Convenios;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._7.Convenios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class ContratosCodeudoresModel : PageModel
    {
        //para combos

        private IContratosNegocio? iContratosNegocio;
        public List<Contratos> Contratos { get; set; } = new List<Contratos>();

        private ICodeudoresNegocio? iCodeudoresNegocio;
        public List<Codeudores> Codeudores { get; set; } = new List<Codeudores>();

        private IContratosCodeudoresNegocio? iContratosCodeudoresNegocio;

        [BindProperty] public List<ContratosCodeudores>? Lista { get; set; }
        [BindProperty] public ContratosCodeudores? ContratoCodeudor { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public ContratosCodeudoresModel()
        {
            iContratosCodeudoresNegocio = new ContratosCodeudoresNegocio();
            iContratosNegocio = new ContratosNegocio();
            iCodeudoresNegocio = new CodeudoresNegocio();
        }

        private void CargarCombos()
        {
            Contratos = iContratosNegocio!.Consultar();
            Codeudores = iCodeudoresNegocio!.Consultar();
        }

        private void ValidarRelacion()
        {
            if (ContratoCodeudor == null)
                return;

            if (ContratoCodeudor.Contrato == 0)
                throw new Exception("Debe seleccionar un contrato.");

            if (ContratoCodeudor.Codeudor == 0)
                throw new Exception("Debe seleccionar un codeudor.");

            var relaciones = iContratosCodeudoresNegocio!.Consultar();

            var relacionExistente = relaciones.FirstOrDefault(x => x.Contrato == ContratoCodeudor.Contrato &&
                x.Codeudor == ContratoCodeudor.Codeudor && x.Id != ContratoCodeudor.Id
            );

            if (relacionExistente != null)
                throw new Exception("Este contrato ya tiene asociado ese codeudor.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iContratosCodeudoresNegocio == null)
                    return;

                Lista = iContratosCodeudoresNegocio.Consultar();
                CargarCombos();
                ContratoCodeudor = null;
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

            ContratoCodeudor = new ContratosCodeudores()
            {
                FechaCierre = DateTime.Now,
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                ContratoCodeudor = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (ContratoCodeudor == null)
                    return;

                ValidarRelacion();

                if (ContratoCodeudor.Id == 0)
                    ContratoCodeudor = iContratosCodeudoresNegocio!.Guardar(ContratoCodeudor!);
                else
                    ContratoCodeudor = iContratosCodeudoresNegocio!.Modificar(ContratoCodeudor!);

                if (ContratoCodeudor.Id == 0)
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
                if (ContratoCodeudor == null)
                    return;

                ContratoCodeudor = iContratosCodeudoresNegocio!.Borrar(ContratoCodeudor!);

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

                ContratoCodeudor = Lista!.FirstOrDefault(x => x.Id == data);

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