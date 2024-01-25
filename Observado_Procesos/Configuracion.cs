using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Observado_Procesos
{


    public static class Configuracion
    {
        public static bool ModoAutomatico
        {
            get
            {
                // Puedes cambiar el nombre del valor según tus preferencias
                string valor = ConfigurationManager.AppSettings["modoAutomatico"];
                return !string.IsNullOrEmpty(valor) && valor.Equals("true", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}

