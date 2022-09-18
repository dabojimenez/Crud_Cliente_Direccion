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
    public partial class Direcciones_CU : UserControl
    {
        //variables globales
        ModeloCliente cliente;
        int idDirecciones;
        public Direcciones_CU(ModeloCliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente;
        }
        /// <summary>
        /// Se captura los datos que han sido llenados en los cuadros de texto
        /// </summary>
        /// <param name="idDirecciones">Valor por defecto es cero, si en caso no sea obtenido el identificado del registro de Direccion</param>
        /// <returns>Retorna un Objeto de tipo ModeloDirecciones</returns>
        private ModeloDirecciones CapturarDatos(int idDirecciones = 0)
        {
            ModeloDirecciones direcciones = new ModeloDirecciones();
            direcciones.ID_CLIENTE= cliente.ID_CLIENTE;
            direcciones.ID_DIR = idDirecciones;
            direcciones.CALLE_PRINCIPAL_CLI = txtCallePrincipal.Text.ToUpper();
            direcciones.CALLE_SECUNDARIA_CLI = txtCalleSecundaria.Text.ToUpper();
            return direcciones;
        }
        private void Direcciones_CU_Load(object sender, EventArgs e)
        {
            txtNombre.Text = cliente.NOMBRE_CLIENTE;
            txtCedula.Text = cliente.CEDULA_CLIENTE;
            MostrarDirecciones();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCallePrincipal.Text) && !string.IsNullOrEmpty(txtCalleSecundaria.Text))
            {
                GuardarDireecion();
                LimpiarCuadrosRexto();
                return;
            }
            MessageBox.Show("Existen Cuadros de textos Vacios", "Administracion de Direcciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Se realiza la insercion de un registro de tipo ModeloDirecciones
        /// </summary>
        private void GuardarDireecion()
        {
            ModeloDirecciones direcciones = CapturarDatos();
            LogicaDirecciones logicaDirecciones = new LogicaDirecciones();
            if (logicaDirecciones.INSERTAR_DIRECCIONES(direcciones))
            {
                MessageBox.Show("Direcciones Registradas Exitosamente", "Administracion de Direcciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MostrarDirecciones();
        }

        //Metodo que Llenara de datos al DataGridView
        private void MostrarDirecciones()
        {
            DataTable dt = new DataTable();
            LogicaDirecciones logicaDirecciones = new LogicaDirecciones();
            logicaDirecciones.MOSTRAR_DIRECCIONES_CLIENTE(ref dt, cliente.ID_CLIENTE);
            if (dt.Rows.Count > 0)
            {
                dgvDirecciones.DataSource = dt;
                dgvDirecciones.Visible = true;
                PerzoanlizarDGV();
                return;
            }
            dgvDirecciones.Visible = false;
        }
        /// <summary>
        /// Perzonalizacion del DataGridView, de forma que la cabecera de las columnas sean entendibles para el usuario
        /// </summary>
        private void PerzoanlizarDGV()
        {
            dgvDirecciones.Columns[2].Visible = false;
            dgvDirecciones.Columns[3].HeaderText = "Calle Principal";
            dgvDirecciones.Columns[4].HeaderText = "Calle Secundaria";
        }

        private void dgvDirecciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvDirecciones.Columns[0].Index)
            {
                MostrarDatosCuadrosTexto();
                idDirecciones = ObtenerIdDireccion();
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
                btnCancelar.Visible = true;
                return;
            }
            if (e.ColumnIndex == dgvDirecciones.Columns[1].Index)
            {
                var respuesta = MessageBox.Show("Esta seguro de eliminar definitivamente este registro?", "Administracion de Direcciones", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta.Equals(DialogResult.Yes))
                {
                    idDirecciones = ObtenerIdDireccion();
                    ModeloDirecciones direcciones = CapturarDatos(idDirecciones);
                    LogicaDirecciones logicaDirecciones = new LogicaDirecciones();
                    if (logicaDirecciones.ELIMINAR_DIRECCION(direcciones))
                    {
                        MessageBox.Show("Registro Eliminado Satisfactoriamente", "Administracion de Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarDirecciones();
                    }
                }
            }
        }
        private void MostrarDatosCuadrosTexto()
        {
            txtCallePrincipal.Text = dgvDirecciones.SelectedCells[3].Value.ToString();
            txtCalleSecundaria.Text = dgvDirecciones.SelectedCells[4].Value.ToString();
        }
        /// <summary>
        /// Obtenemos el identificador del registro Direccion
        /// </summary>
        /// <returns>Retorna el IdDireccion</returns>
        private int ObtenerIdDireccion()
        {
            return int.Parse(dgvDirecciones.SelectedCells[2].Value.ToString());
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCallePrincipal.Text) && !string.IsNullOrEmpty(txtCalleSecundaria.Text))
            {
                ActualizarRegistroDirecciones();
                return;
            }
            MessageBox.Show("Existen Cuadros de textos Vacios", "Administracion de Direcciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ActualizarRegistroDirecciones()
        {
            ModeloDirecciones direcciones = CapturarDatos(idDirecciones);
            LogicaDirecciones logicaDirecciones = new LogicaDirecciones();
            if (logicaDirecciones.ACTUALIZAR_DIRECCION(direcciones))
            {
                MessageBox.Show("Registro de Direccion Actualizado Exitosamente", "Administracion de Direcciones", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarDirecciones();
            }
            MostrarDirecciones();
        }

        private void LimpiarCuadrosRexto()
        {
            txtCallePrincipal.Clear();
            txtCalleSecundaria.Clear();
            txtCallePrincipal.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCuadrosRexto();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            btnCancelar.Visible = false;
        }
    }
}
