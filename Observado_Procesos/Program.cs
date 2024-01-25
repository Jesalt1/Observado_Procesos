using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Observado_Procesos
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            bool modoAutomatico = ObtenerModoAutomatico(args);

            if (modoAutomatico)
            {
                // La aplicación se está ejecutando en modo automático, realiza las tareas automáticas
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(modoAutomatico));
            }
            else
            {
                // La aplicación se está ejecutando de forma manual, abre el formulario principal
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(modoAutomatico));
            }
        }

        static bool ObtenerModoAutomatico(string[] args)
        {
            // Si se proporciona un argumento booleano (/modoAutomatico:true), lo usamos
            if (args.Length > 0 && args[0].Equals("/Automatico:true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // En caso contrario, asumimos el modo manual
            return false;
        }

        static void RealizarTareasAutomaticas()
        {
            // Implementa aquí la lógica para realizar las tareas automáticas
            MessageBox.Show("Automatico");
        }
    }


}
