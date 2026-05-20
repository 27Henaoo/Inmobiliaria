using LibModelos._5._1ModelosComunes;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._1.Catalogos;
using LibPresentacion._1._1.ImplementacionesPresentacion._1._1._3.RRHH;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._1.Catalogos;
using LibPresentacion._1._2.InterfacesPresentacion._1._2._3.RRHH;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InmobiliariaAspCore.Pages.Ventanas
{
    public class SectoresModel : PageModel
    {
        private ICiudadesNegocio? iCiudadesNegocio;
        public List<Ciudades> Ciudades { get; set; } = new List<Ciudades>();

        private IJefesSectoresNegocio? iJefesSectoresNegocio;
        public List<JefesSectores> JefesSectores { get; set; } = new List<JefesSectores>();

        private ISectoresNegocio? iSectoresNegocio;

        [BindProperty] public List<Sectores>? Lista { get; set; }
        [BindProperty] public Sectores? Sector { get; set; }
        [BindProperty] public bool Borrando { get; set; }
        [BindProperty] public int JefeSectorSeleccionado { get; set; }

        public SectoresModel()
        {
            iSectoresNegocio = new SectoresNegocio();
            iCiudadesNegocio = new CiudadesNegocio();
            iJefesSectoresNegocio = new JefesSectoresNegocio();
        }

        // Carga los combos de la pantalla
        private void CargarCombos()
        {
            Ciudades = iCiudadesNegocio!.Consultar();
            JefesSectores = iJefesSectoresNegocio!.Consultar();
        }

        // Asigna el jefe seleccionado al sector guardado
        private void AsignarJefeSector()
        {
            //Si no seleccionaron jefe, no hace nada
            if (JefeSectorSeleccionado == 0)
                return;

            //Si no hay sector, no hace nada
            if (Sector == null)
                return;

            //Consultamos todos los jefes
            var jefes = iJefesSectoresNegocio!.Consultar();

            //Buscamos si este sector ya tiene jefe
            var jefeActual = jefes.FirstOrDefault(x => x.Sector == Sector.Id);

            //Si ya tiene un jefe diferente, no dejamos asignar otro
            if (jefeActual != null && jefeActual.Id != JefeSectorSeleccionado)
                throw new Exception("Este sector ya tiene un jefe asignado.");

            //Buscamos el jefe que seleccionamos en el combo
            var jefeSeleccionado = jefes.FirstOrDefault(x => x.Id == JefeSectorSeleccionado);

            //Si no existe, mostramos error
            if (jefeSeleccionado == null)
                throw new Exception("El jefe seleccionado no existe.");

            jefeSeleccionado.Sector = Sector.Id;
            iJefesSectoresNegocio!.Modificar(jefeSeleccionado);
        }

        public void OnGet()
        {
            OnPostBtRefrescar();
        }

        public void OnPostBtRefrescar()
        {
            try
            {
                if (iSectoresNegocio == null)
                    return;

                Lista = iSectoresNegocio.Consultar();
                CargarCombos();
                Sector = null;
                Borrando = false;

                //Limpiamos jefe seleccionado
                JefeSectorSeleccionado = 0;
            }
            catch (Exception ex)
            {
                ViewData["Mensaje"] = ex.Message;
            }
        }

        public void OnPostBtNuevo()
        {
            CargarCombos();
            Sector = new Sectores()
            {
                FechaCreacion = DateTime.Now,
            };

            //Como es nuevo, todavia no tiene jefe seleccionado
            JefeSectorSeleccionado = 0;
            Borrando = false;
        }

        public void OnPostBtModificar(int data)
        {
            try
            {
                OnPostBtRefrescar();
                Sector = Lista!.FirstOrDefault(x => x.Id == data);
                Lista = null;
                CargarCombos();

                //Si el sector ya tiene jefe, lo dejamos seleccionado
                if (Sector != null && Sector._JefeSector != null)
                    JefeSectorSeleccionado = Sector._JefeSector.Id;
                else
                    JefeSectorSeleccionado = 0;
                Borrando = false;
            }
            catch (Exception ex)
            {
                CargarCombos();
                ViewData["Mensaje"] = ex.Message;
            }
        }

        // Botón Guardar
        public void OnPostBtGuardar()
        {
            try
            {
                if (Sector == null)
                    return;
                if (Sector.Id == 0)
                    Sector = iSectoresNegocio!.Guardar(Sector!);
                else
                    Sector = iSectoresNegocio!.Modificar(Sector!);
                if (Sector.Id == 0)
                    return;

                //Debemos guardar el sectr asignando el jefe
                AsignarJefeSector();

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
                if (Sector == null)
                    return;

                Sector = iSectoresNegocio!.Borrar(Sector!);

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
                Sector = Lista!.FirstOrDefault(x => x.Id == data);
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