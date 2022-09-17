using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_PT_SPLENDOR
{
    public partial class Form1 : Form
    {
        //Variables Globales
        int pagina = 0;
        readonly int numeroRegistros = 7;
        int contador;
        int idCliente;
        public Form1()
        {
            InitializeComponent();
        }
        private void LimpiarCuadrosRexto()
        {
            txtCedula.Clear();
            txtNombre.Clear();
            idCliente = 0;
        }
        private void TotalRegistros()
        {
            LogicaCliente logicaCliente = new LogicaCliente();
            logicaCliente.TOTAL_REGISTROS(ref contador);
            lblTotal.Text = contador.ToString();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarClientes();
            PaginadoRegistros();
            PerzoanlizarDGV();
        }
        private void MostrarClientes()
        {
            DataTable dataTable = new DataTable();
            LogicaCliente logicaCliente = new LogicaCliente();
            logicaCliente.OBTENER_CLIENTES_PAGINADO(ref dataTable, pagina, numeroRegistros);
            dgvCliente.DataSource = dataTable;
            TotalRegistros();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarCliente();
            LimpiarCuadrosRexto();
        }
        private void GuardarCliente()
        {
            ModeloCliente cliente = CapturarDatos();
            LogicaCliente logicaCliente = new LogicaCliente();
            if (logicaCliente.INSERTAR_CLIENTE(cliente))
            {
                MessageBox.Show("Cliente Registrado","Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MostrarClientes();
        }
        private ModeloCliente CapturarDatos(int idCliente = 0)
        {
            ModeloCliente cliente = new ModeloCliente();
            cliente.ID_CLIENTE = idCliente;
            cliente.NOMBRE_CLIENTE = txtNombre.Text.ToUpper();
            cliente.CEDULA_CLIENTE = txtCedula.Text.ToUpper();
            return cliente;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if(pagina > 0)
            {
                return;
            }
            pagina += 1;
            MostrarClientes();
            PaginadoRegistros();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (pagina == 0)
            {
                return;
            }
            pagina -= 1;
            MostrarClientes();
            PaginadoRegistros();
        }
        private void PaginadoRegistros()
        {
            lblPagina.Text = (pagina + 1).ToString();
            lblRegistros.Text = dgvCliente.Rows.Count.ToString();

        }
        private void PerzoanlizarDGV()
        {
            dgvCliente.Columns[2].Visible = false;
            dgvCliente.Columns[3].HeaderText = "Nombre";
            dgvCliente.Columns[4].HeaderText = "Cedula";
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCliente.Columns[0].Index)
            {
                idCliente = int.Parse(dgvCliente.SelectedCells[2].Value.ToString());
                txtNombre.Text = dgvCliente.SelectedCells[3].Value.ToString();
                txtCedula.Text = dgvCliente.SelectedCells[4].Value.ToString();
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
                btnCancelar.Visible = true;
            }
            if (e.ColumnIndex == dgvCliente.Columns[1].Index)
            {
                var respuesta = MessageBox.Show("Esta seguro de eliminar este registro?", "Administracion de Clientes", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    idCliente = int.Parse(dgvCliente.SelectedCells[2].Value.ToString());
                    EliminarCliente(idCliente);
                    MessageBox.Show("Cliente Eliminado Satisfactoriamente", "Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            GuardarCambios();
            LimpiarCuadrosRexto();
        }
        private void GuardarCambios()
        {
            ModeloCliente cliente = CapturarDatos(idCliente);
            LogicaCliente logicaCliente = new LogicaCliente();
            if (logicaCliente.ACTUALIZAR_DATOS(cliente))
            {
                MessageBox.Show("Cliente Actualizado Satisfactoriamente", "Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MostrarClientes();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCuadrosRexto();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            btnCancelar.Visible = false;
        }
        private void EliminarCliente(int idCliente)
        {
            LogicaCliente logicaCliente = new LogicaCliente();
            logicaCliente.ELIMINAR_CLIENTE(idCliente);
        }
    }
}
