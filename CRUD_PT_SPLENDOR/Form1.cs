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
        /// <summary>
        /// Metodo que otorga a la variable global contador, la cantidad de registros existentes
        /// </summary>
        private void TotalRegistros()
        {
            LogicaCliente logicaCliente = new LogicaCliente();
            logicaCliente.TOTAL_REGISTROS(ref contador);
            lblTotal.Text = contador.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarClientes();
            PaginadoRegistros();
            PerzoanlizarDGV();
        }
        /// <summary>
        /// Se llena el DataGridView con registros de tipo ModeloCliente
        /// </summary>
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
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtCedula.Text))
            {
                GuardarCliente();
                LimpiarCuadrosRexto();
                return;
            }
            MessageBox.Show("Existen Cuadros de textos Vacios", "Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void GuardarCliente()
        {
            ModeloCliente cliente = CapturarDatos();
            LogicaCliente logicaCliente = new LogicaCliente();
            if (logicaCliente.INSERTAR_CLIENTE(cliente))
            {
                MessageBox.Show("Cliente Registrado Exitosamente","Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dgvCliente.Columns[3].Visible = false;
            //dgvCliente.Columns[4].DataPropertyName = "Nombre";
            dgvCliente.Columns[4].HeaderText = "Nombre";
            dgvCliente.Columns[5].HeaderText = "Cedula";
        }

        private void MostrarDatosCuadrosTexto()
        {
            txtNombre.Text = dgvCliente.SelectedCells[4].Value.ToString();
            txtCedula.Text = dgvCliente.SelectedCells[5].Value.ToString();
        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCliente.Columns[0].Index)
            {
                idCliente = ObtenerIdCliente();
                MostrarDatosCuadrosTexto();
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
                btnCancelar.Visible = true;
                return;
            }
            if (e.ColumnIndex == dgvCliente.Columns[1].Index)
            {
                var respuesta = MessageBox.Show("Esta seguro de eliminar este registro?", "Administracion de Clientes", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    idCliente = ObtenerIdCliente();
                    EliminarCliente(idCliente);
                    MessageBox.Show("Cliente Eliminado Satisfactoriamente", "Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                MostrarClientes();
                return;
            }
            if (e.ColumnIndex == dgvCliente.Columns[2].Index)
            {
                txtCedula.ReadOnly = true;
                txtNombre.ReadOnly = true;
                pnlDirecciones.Controls.Clear();
                btnCancelar.Visible = true;
                idCliente = ObtenerIdCliente();
                MostrarDatosCuadrosTexto();
                ModeloCliente cliente = CapturarDatos(idCliente);
                Direcciones_CU direcciones = new Direcciones_CU(cliente);
                direcciones.Dock = DockStyle.Fill;
                pnlDirecciones.Controls.Add(direcciones);
                return;
            }
        }
        /// <summary>
        /// Se obtiene el identificador del registro, dentro del DataGridView
        /// </summary>
        /// <returns>Retorna el IdCliente</returns>
        private int ObtenerIdCliente()
        {
            return int.Parse(dgvCliente.SelectedCells[3].Value.ToString());
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtCedula.Text))
            {
                GuardarCambios();
                LimpiarCuadrosRexto();
                return;
            }
            MessageBox.Show("Existen Cuadros de textos Vacios", "Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtCedula.ReadOnly = false;
            txtNombre.ReadOnly = false;
            pnlDirecciones.Controls.Clear();
            pnlDirecciones.Controls.Add(lblDirecciones);
        }
        private void EliminarCliente(int idCliente)
        {
            LogicaCliente logicaCliente = new LogicaCliente();
            logicaCliente.ELIMINAR_CLIENTE(idCliente);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            BuscarClienteXNombreCedula(txtCedula.Text);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            BuscarClienteXNombreCedula(txtNombre.Text);
        }
        /// <summary>
        /// Metodo que realiza una consulta a BBDD, por medio de una palabra de busqueda
        /// </summary>
        /// <param name="buscar">Parametro para realziar la busqueda</param>
        private void BuscarClienteXNombreCedula(string buscar)
        {
            DataTable dataTable = new DataTable();
            LogicaCliente logicaCliente = new LogicaCliente();
            logicaCliente.BUSCAR_CLIENTE(ref dataTable, buscar);
            dgvCliente.DataSource = dataTable;
        }
    }
}
