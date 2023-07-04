using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace listadoTareas
{
    public partial class FormInicio : Form
    {
        List<TareaModel> lista = new List<TareaModel>();
        public FormInicio()
        {
            InitializeComponent();
            checkBoxColorP.CheckedChanged += checkBoxColorP_CheckedChanged;
            ObtenerLista();
        }

        private void ObtenerLista()
        {
            lista = Datos.GetList();
            
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Asociar la lista de usuarios al DataGridView
            dataGridView1.DataSource = lista;
        }

        private void FormInicio_Load(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            FormAgTareas agregar = new FormAgTareas();
            this.Hide();
            agregar.Show();
        }

        private void checkBoxColorP_CheckedChanged(object sender, EventArgs e)
        {
            FormAgTareas formAgTareas = new FormAgTareas();
            if (checkBoxColorP.Checked)
            {
                // Cambiar a color oscuro
                this.BackColor = Color.Black;
                label1.ForeColor = Color.White;
                buttonAgregar.BackColor = Color.Black;
                buttonAgregar.ForeColor = Color.White;
                button1.BackColor = Color.Black;
                button1.ForeColor = Color.White;
                button2.BackColor = Color.Black;
                button2.ForeColor = Color.White;
                dataGridView1.BackgroundColor = Color.Black;
                dataGridView1.ForeColor = SystemColors.ControlText;
            }
            else
            {
                // Cambiar a color claro
                this.BackColor = SystemColors.Control;
                label1.ForeColor = SystemColors.ControlText;
                buttonAgregar.BackColor = SystemColors.Control;
                buttonAgregar.ForeColor = SystemColors.ControlText;
                button1.BackColor = SystemColors.Control;
                button1.ForeColor = SystemColors.ControlText;
                button2.BackColor = SystemColors.Control;
                button2.ForeColor = SystemColors.ControlText;
                dataGridView1.BackgroundColor = SystemColors.Control;
                dataGridView1.ForeColor = SystemColors.ControlText;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int filaSeleccionada = dataGridView1.SelectedCells[0].RowIndex;
            TareaModel elementoSeleccionado = lista[filaSeleccionada];

            Datos.Eliminar(elementoSeleccionado.Id);
            MessageBox.Show("Registro eliminado correctamente", "Listado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObtenerLista();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int filaSeleccionada = dataGridView1.SelectedCells[0].RowIndex;
            TareaModel elementoSeleccionado = lista[filaSeleccionada];

            Datos.ActivarInactivar(elementoSeleccionado.Id, elementoSeleccionado.Activo);
            MessageBox.Show("Registro Activado/Inactivado correctamente", "Listado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ObtenerLista();
        }
    }
}