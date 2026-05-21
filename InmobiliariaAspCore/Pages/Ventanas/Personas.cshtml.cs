using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._2.Sujetos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._2.Sujetos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class PersonasModel : PageModel
    {
        //Para los combos 

        private IEstadosCivilesNegocio? iEstadosCivilesNegocio;
        public List<EstadosCiviles> EstadosCiviles { get; set; } = new List<EstadosCiviles>();

        private INacionalidadesNegocio? iNacionalidadesNegocio;
        public List<Nacionalidades> Nacionalidades { get; set; } = new List<Nacionalidades>();

        private IPersonasNegocio? iPersonasNegocio;
        [BindProperty] public List<Personas>? Lista { get; set; }
        [BindProperty] public Personas? Persona { get; set; }
        [BindProperty] public bool Borrando { get; set; }

        public PersonasModel()
        {
            iPersonasNegocio = new PersonasNegocio();
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
                if (iPersonasNegocio == null)
                    return;
                Lista = iPersonasNegocio.Consultar();
                CargarCombos();
                Persona = null;
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
            Persona = new Personas()
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
                Persona = Lista!.FirstOrDefault(x => x.Id == data);
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
                if (Persona == null)
                    return;
                if (Persona.Id == 0)
                    Persona = iPersonasNegocio!.Guardar(Persona!);
                else
                    Persona = iPersonasNegocio!.Modificar(Persona!);
                if (Persona.Id == 0)
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
                if (Persona == null)
                    return;
                Persona = iPersonasNegocio!.Borrar(Persona!);
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
                Persona = Lista!.FirstOrDefault(x => x.Id == data);
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
