using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._4.Comercial;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._4.Comercial;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class CompradoresModel : PageModel
    {
        //para combos

        private IEstadosCivilesNegocio? iEstadosCivilesNegocio;
        public List<EstadosCiviles> EstadosCiviles { get; set; } = new List<EstadosCiviles>();

        private INacionalidadesNegocio? iNacionalidadesNegocio;
        public List<Nacionalidades> Nacionalidades { get; set; } = new List<Nacionalidades>();

        private ICompradoresNegocio? iCompradoresNegocio;

        [BindProperty] public List<Compradores>? Lista { get; set; }
        [BindProperty] public Compradores? Comprador { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public CompradoresModel()
        {
            iCompradoresNegocio = new CompradoresNegocio();
            iEstadosCivilesNegocio = new EstadosCivilesNegocio();
            iNacionalidadesNegocio = new NacionalidadesNegocio();
        }

        private void CargarCombos()
        {
            EstadosCiviles = iEstadosCivilesNegocio!.Consultar();
            Nacionalidades = iNacionalidadesNegocio!.Consultar();
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iCompradoresNegocio == null)
                    return;

                Lista = iCompradoresNegocio.Consultar();
                CargarCombos();
                Comprador = null;
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

            Comprador = new Compradores()
            {
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now,
            };

            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();

                Comprador = Lista!.FirstOrDefault(x => x.Id == data);

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
                if (Comprador == null)
                    return;

                if (Comprador.Id == 0)
                    Comprador = iCompradoresNegocio!.Guardar(Comprador!);
                else
                    Comprador = iCompradoresNegocio!.Modificar(Comprador!);

                if (Comprador.Id == 0)
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
                if (Comprador == null)
                    return;

                Comprador = iCompradoresNegocio!.Borrar(Comprador!);

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

                Comprador = Lista!.FirstOrDefault(x => x.Id == data);

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