using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace listadoTareas
{
    public partial class FormAgTareas : Form
    {
        FormInicio formInicio = new FormInicio();
        public FormAgTareas()
        {
            InitializeComponent();
            
        }

        private void buttonVolver_Click(object sender, EventArgs e)
        {

            this.Hide();
            formInicio.Show();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            //Datos con = new Datos();

            TareaModel model = new TareaModel()
            {
                Asignatura = textBoxAsignatura.Text,
                Mandato = textBoxMandato.Text,
                FechaAsignacion = Convert.ToDateTime(dateTimePickerInicio.Text),
                FechaEntrega = Convert.ToDateTime(dateTimePickerEntrega.Text),
                Descripcion = textBoxDescripcion.Text
            };

            Datos.CrearRegistro(model);

            MessageBox.Show("Registro agregado correctamente", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FormInicio lista = new FormInicio();
            this.Hide();
            lista.Show();
        }

        private void FormAgTareas_Load(object sender, EventArgs e)
        {

        }

        private void textBoxAsignatura_TextChanged(object sender, EventArgs e)
        {

        }
        private void buttonCerrar_Click(object sender, EventArgs e) => Application.Exit();

        private void buttonCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}