using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gConnectionSql;
using Microsoft.VisualBasic.ApplicationServices;
using Infragistics.Win.UltraWinGrid;
using static libreria.ModSeguridad;
using static libreria.ModVariables;
using Infragistics.Win.UltraWinGrid;
using System.Windows.Input;

namespace Observado_Procesos
{
    public partial class Form1 : Form
    {
        private DataTable dtListaProcesos = new DataTable();
        public Form1(bool auto)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (auto)
            {
                MessageBox.Show(auto.ToString());
                MessageBox.Show("Automatico");

            }
            else
            {
                MessageBox.Show(auto.ToString());
                MessageBox.Show("manual");
            }


            if (Load_Siac(this, "SIACDB", "\"|Empresa=001|Usuario=Admin|**\""))
            {

            }

           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Obtener el nombre de la máquina
            sgMaquina = Environment.MachineName;

            // Obtener el nombre del ejecutable
            sgEjecutable = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            GenerarTabla();
            CambiarProcesoTabla(0);
            Automatico();
            TableConfig();
        }

        #region'Region Configuraciones iniciales sistema'
        //carga del data table del sistema
        private void GenerarTabla()
        {
            dtListaProcesos.Columns.Add("Proceso");
            dtListaProcesos.Columns.Add("Estado");
            dtListaProcesos.Rows.Add("Procesos 1", "Procesando");
            dtListaProcesos.Rows.Add("Procesos 2", "Procesando");
            dtListaProcesos.Rows.Add("Procesos 3", "Procesando");
            dtListaProcesos.Rows.Add("Procesos 4", "Procesando");
            dtListaProcesos.Rows.Add("Procesos 5", "Procesando");

            ultraGrid1.DataSource = dtListaProcesos;


        }
        //configracion de columnas botones UltraGird
        private void TableConfig()
        {
            // Crear una columna de botones
            UltraGridColumn botonColumna = ultraGrid1.DisplayLayout.Bands[0].Columns.Add("Boton", "Ejecutar");

            // Configurar la columna como una columna de botones
            botonColumna.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            botonColumna.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            // botonColumna.CellButtonAppearance.Image = Properties.Resources.icono_boton; // Puedes cambiar esto según tu icono

            // Manejar el evento de clic en la celda
            this.ultraGrid1.ClickCellButton += UltraGrid_ClickCellButton;

        }
        #endregion

        #region'Eventos'
        //se encargara de realizar las ejecuciones de cada boton de las celdas
        private void UltraGrid_ClickCellButton(object sender, EventArgs e)
        {
            // Obtener la información de la celda activada
            UltraGrid grid = sender as UltraGrid;
            UltraGridCell cell = grid?.ActiveCell;

            // Verificar si la celda es de tipo botón
            if (cell?.Column.Key == "Boton")
            {
                // Realizar acciones cuando se hace clic en el botón
                Console.WriteLine($"Botón presionado en la fila: {cell.Row.Index}");
            }

            // Deseleccionar la celda activa
            cell.Selected = false;
            // Desactivar la celda activa
            grid.PerformAction(UltraGridAction.ExitEditMode);
        }
        #endregion

        #region'Metodos'
        public async Task Automatico()
        {
         
            await ExtraccionFacturasMeypar();
            await pruebas();
           // await GenerarTabla();
        }
        #endregion
        static async Task pruebas()
        {
            SqlConnection oCon;
            SqlDataAdapter oRs;
            SqlCommand ocomand;
            gConnectionSql.gConnection oconexion = new gConnection();


            oCon = null;

            string hola = sgtipo;
            string adios;
            // Suponiendo que oconexion es una instancia de gConnectionSql.gConnection
            if (oconexion.gConexion(sgtipo, sgprovider, sgservidor, sgbaseapp, sguser, sgpw, ref oCon))
            {
                Console.WriteLine("Entre");
                //return; // o Exit Sub en VB.NET
                Console.WriteLine("***************************************************************************************");
            }
            Console.WriteLine("aqui");
            // Continuar con el resto del código si la condición es verdadera
           

        }

        private async Task ExtraccionFacturasMeypar()
        {
            int ierror;
            string smensaje;

            SqlConnection oCon;
            SqlDataAdapter oRs;
            SqlCommand ocomand = null;
            gConnectionSql.gConnection oconexion = new gConnection();
            oCon = null;

            string sgtipoo = sgtipo;
            string sgproviderr = sgprovider;
            string sgservidorr = sgservidor;
            string sgbaseappp = sgbase;
            string sguserr = sguser;
            string sgpww = sgpw;
            string sgUsuarioo = sgUsuario;
            string sgMaquinaa = sgMaquina;

            try
            {
                // Suponiendo que oconexion es una instancia de gConnectionSql.gConnection
                if (oconexion.gConexion(sgtipo, sgprovider, sgservidor, sgbase, sguser, sgpw, ref oCon))
                {
                    // Crear un nuevo comando y asignar la conexión
                    ocomand = new SqlCommand();
                    ocomand.Connection = oCon;

                    // Nombre del stored procedure
                    ocomand.CommandText = "SpMigradorParqueos";
                    ocomand.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros si es necesario
                    // Suponiendo que 'ocomand' es un objeto SqlCommand
                    ocomand.Parameters.AddWithValue("@i_FInicio", DateTime.Now.ToString("yyyyMMdd"));
                    // o puedes asignar directamente una cadena con la fecha formateada si lo prefieres
                    // ocomand.Parameters.AddWithValue("@i_FInicio", "20161222");
                    //ocomand.Parameters.AddWithValue("@i_Intervalo", LwiIntervalo);
                    ocomand.Parameters.AddWithValue("@i_servidor", sgservidor);
                    ocomand.Parameters.AddWithValue("@i_usuario", sgUsuario);
                    ocomand.Parameters.AddWithValue("@i_maquina", sgMaquina);
                    ocomand.Parameters.Add("@o_error", SqlDbType.Int);
                    ocomand.Parameters.Add("@o_mensaje", SqlDbType.VarChar, 8000);
                    ocomand.Parameters["@o_error"].Direction = ParameterDirection.Output;
                    ocomand.Parameters["@o_mensaje"].Direction = ParameterDirection.Output;

                    // Ejecutar el stored procedure
                    ocomand.ExecuteNonQuery();

                    // Suponiendo que 'ocomand' es un objeto SqlCommand
                    ierror = (int)ocomand.Parameters["@o_error"].Value;
                    smensaje = ocomand.Parameters["@o_mensaje"].Value.ToString();

                   
                    if (ierror == 1)
                    {
                        this.UTextEditorResponse.Text += Environment.NewLine + "Error - " + smensaje;

                        UltraGridRow row = ultraGrid1.Rows[0];
                        row.Band.Columns.Exists("Estado");
                        row.Cells["Estado"].Appearance.BackColor = System.Drawing.Color.Red;
                        row.Cells["Estado"].Value = "Error";
                    }
                    else
                    {
                        this.UTextEditorResponse.Text += Environment.NewLine + smensaje;

                        UltraGridRow row = ultraGrid1.Rows[0];
                        row.Band.Columns.Exists("Estado");
                        row.Cells["Estado"].Appearance.BackColor = System.Drawing.Color.Green;
                        row.Cells["Estado"].Value = "OK";

                        CambiarProcesoTabla(1);
                    }
                }
                else
                {
                    this.UTextEditorResponse.Text = "No se puede establecer conexion con la base de datos";
                    UltraGridRow row = ultraGrid1.Rows[0];
                    row.Band.Columns.Exists("Estado");
                    row.Cells["Estado"].Appearance.BackColor = System.Drawing.Color.Red;
                    row.Cells["Estado"].Value = "Error";
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error al ejecutar el stored procedure: {ex.Message}");

                this.UTextEditorResponse.Text = $"Error al ejecutar el stored procedure: {ex.Message}";

                UltraGridRow row = ultraGrid1.Rows[0];
                row.Band.Columns.Exists("Estado");
                row.Cells["Estado"].Appearance.BackColor = System.Drawing.Color.Red;
                row.Cells["Estado"].Value = "Error";

            }
            finally
            {
                ocomand.Dispose();
                if (oCon.State == ConnectionState.Open)
                {
                    oCon.Close();
                }
                oCon.Dispose();
            }
        }

        private void CambiarProcesoTabla(int start)
        {
            int endIndex = ultraGrid1.Rows.Count-1;

            // Iterar a través del rango de índices
            for (int rowIndex = start; rowIndex <= endIndex; rowIndex++)
            {
                // Obtener la fila correspondiente al índice
                UltraGridRow row = ultraGrid1.Rows[rowIndex];

                // Verificar si la columna existe en la fila
                if (row.Band.Columns.Exists("Estado"))
                {
                    // Asignar el nuevo texto a la celda en la columna especificada
                    row.Cells["Estado"].Value = "En cola";
                }
            }
        }
      
    }
}
//string sgtipoo = sgtipo;

//string sgproviderr = sgprovider;
//string sgservidorr = sgservidor;
//string sgbaseappp = sgbaseapp;
//string sguserr = sguser;
//string sgpww = sgpw;