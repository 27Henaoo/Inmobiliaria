using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class TrabajadoresModel : PageModel
    {
        //Para combos
        private IEstadosCivilesNegocio? iEstadosCivilesNegocio;
        public List<EstadosCiviles> EstadosCiviles { get; set; } = new List<EstadosCiviles>();

        private INacionalidadesNegocio? iNacionalidadesNegocio;
        public List<Nacionalidades> Nacionalidades { get; set; } = new List<Nacionalidades>();

        
        private ITrabajadoresNegocio? iTrabajadoresNegocio;

        [BindProperty] public List<Trabajadores>? Lista { get; set; }
        [BindProperty] public Trabajadores? Trabajador { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public TrabajadoresModel()
        {
            iTrabajadoresNegocio = new TrabajadoresNegocio();
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
                if (iTrabajadoresNegocio == null)
                    return;
                Lista = iTrabajadoresNegocio.Consultar();
                CargarCombos();
                Trabajador = null;
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
            Trabajador = new Trabajadores()
            {
                FechaNacimiento = DateTime.Now,
                FechaRegistro = DateTime.Now
            };
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Trabajador = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Trabajador == null)
                    return;
                if (Trabajador.Id == 0)
                    Trabajador = iTrabajadoresNegocio!.Guardar(Trabajador!);
                else
                    Trabajador = iTrabajadoresNegocio!.Modificar(Trabajador!);
                if (Trabajador.Id == 0)
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
                if (Trabajador == null)
                    return;
                Trabajador = iTrabajadoresNegocio!.Borrar(Trabajador!);
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
                Trabajador = Lista!.FirstOrDefault(x => x.Id == data);
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
