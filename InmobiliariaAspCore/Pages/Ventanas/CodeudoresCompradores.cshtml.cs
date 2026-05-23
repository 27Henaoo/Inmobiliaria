using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class CodeudoresCompradoresModel : PageModel
    {
        //Para combos

        private ICompradoresNegocio? iCompradoresNegocio;
        public List<Compradores> Compradores { get; set; } = new List<Compradores>();

        private ICodeudoresNegocio? iCodeudoresNegocio;
        public List<Codeudores> Codeudores { get; set; } = new List<Codeudores>();

        private ICodeudoresCompradoresNegocio? iCodeudoresCompradoresNegocio;

        [BindProperty] public List<CodeudoresCompradores>? Lista { get; set; }
        [BindProperty] public CodeudoresCompradores? CodeudorComprador { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public CodeudoresCompradoresModel()
        {
            iCodeudoresCompradoresNegocio = new CodeudoresCompradoresNegocio();
            iCompradoresNegocio = new CompradoresNegocio();
            iCodeudoresNegocio = new CodeudoresNegocio();
        }

        private void CargarCombos()
        {
            Compradores = iCompradoresNegocio!.Consultar();
            Codeudores = iCodeudoresNegocio!.Consultar();
        }

        //Validamos relaciones
        private void ValidarRelacion()
        {
            if (CodeudorComprador == null)
                return;

            if (CodeudorComprador.Comprador == 0)
                throw new Exception("Debe seleccionar un comprador.");

            if (CodeudorComprador.Codeudor == 0)
                throw new Exception("Debe seleccionar un codeudor.");

            var relaciones = iCodeudoresCompradoresNegocio!.Consultar();

            var relacionExistente = relaciones.FirstOrDefault(x => x.Comprador == CodeudorComprador.Comprador &&
                x.Codeudor == CodeudorComprador.Codeudor && x.Id != CodeudorComprador.Id
            );

            if (relacionExistente != null)
                throw new Exception("Este comprador ya tiene asociado ese codeudor.");
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iCodeudoresCompradoresNegocio == null)
                    return;

                Lista = iCodeudoresCompradoresNegocio.Consultar();
                CargarCombos();
                CodeudorComprador = null;
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

            CodeudorComprador = new CodeudoresCompradores();

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                CodeudorComprador = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (CodeudorComprador == null)
                    return;

                ValidarRelacion();

                if (CodeudorComprador.Id == 0)
                    CodeudorComprador = iCodeudoresCompradoresNegocio!.Guardar(CodeudorComprador!);
                else
                    CodeudorComprador = iCodeudoresCompradoresNegocio!.Modificar(CodeudorComprador!);

                if (CodeudorComprador.Id == 0)
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
                if (CodeudorComprador == null)
                    return;

                CodeudorComprador = iCodeudoresCompradoresNegocio!.Borrar(CodeudorComprador!);

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

                CodeudorComprador = Lista!.FirstOrDefault(x => x.Id == data);

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