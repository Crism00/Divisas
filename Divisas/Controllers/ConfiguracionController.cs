using Divisas.Database;
using Divisas.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Divisas.Controllers
{
    public class ConfiguracionController
    {   
        public Config GetConfiguracion()
        {
            using (var dbContext = new DivisasDbContext())
            {
                var configuracion = dbContext.Configuraciones.Find(1);
                if (configuracion != null)
                {
                    Console.WriteLine("Configuración encontrada");
                    Console.WriteLine(configuracion.NombreNegocio);
                    Console.WriteLine(configuracion.Direccion);
                    Console.WriteLine(configuracion.Ciudad);
                    Console.WriteLine(configuracion.Estado);
                    return configuracion;
                }
                else
                {
                    Console.WriteLine("No se encontró la configuración");
                    return null;
                }
            }
        }

        public void SaveConfiguracion(Config configuracion)
        {
            try
            {
                using (var dbContext = new DivisasDbContext())
                {
                    var configuracionExistente = dbContext.Configuraciones.Find(1);
                    if (configuracionExistente != null)
                    {
                        // Actualizar la configuración existente
                        configuracionExistente.NombreNegocio = configuracion.NombreNegocio;
                        configuracionExistente.Direccion = configuracion.Direccion;
                        configuracionExistente.Ciudad = configuracion.Ciudad;
                        configuracionExistente.Estado = configuracion.Estado;
                        configuracionExistente.Logotipo = configuracion.Logotipo;

                        dbContext.Configuraciones.Update(configuracionExistente);
                        Console.WriteLine("Configuración actualizada");
                    }
                    else
                    {
                        // Crear una nueva configuración
                        dbContext.Configuraciones.Add(configuracion);
                        Console.WriteLine("Nueva configuración guardada");
                    }

                    dbContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error al guardar la configuración: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
            }
        }
    }
}