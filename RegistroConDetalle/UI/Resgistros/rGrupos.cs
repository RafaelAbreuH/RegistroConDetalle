using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegistroConDetalle.DAL;
using RegistroConDetalle.Entidades;

namespace RegistroConDetalle.UI.Resgistros
{
    public partial class rGrupos : Form
    {
        public rGrupos()
        {
            InitializeComponent();
            LlenarComboBox();
        }
        private void LlenarCampos(Grupos grupo)
        {
            IdnumericUpDown.Value = grupo.GrupoId;
            fechaDateTimePicker.Value = grupo.Fecha;
            NombretextBox.Text = grupo.Descripcion;

            //Cargar el detalle al Grid
            detalleDataGridView.DataSource = grupo.Detalle;

            //Ocultar columnas
            detalleDataGridView.Columns["Id"].Visible = false;
            detalleDataGridView.Columns["PersonaId"].Visible = false;
        }

        private void LlenarComboBox()
        {
            Repositorio<Personas> repositorio = new Repositorio<Personas>(new Contexto());
            PersonacomboBox.DataSource = repositorio.GetList(c => true);
            PersonacomboBox.ValueMember = "PersonaId";
            PersonacomboBox.DisplayMember = "Nombres";
        }

        // ToINT -------------------
        private int ToInt(object valor)
        {
            int retorno = 0;

            int.TryParse(valor.ToString(), out retorno);

            return retorno;
        }
        // -------------------

        private Grupos LlenaClase()
        {
            Grupos grupo = new Grupos();

            grupo.GrupoId = Convert.ToInt32(IdnumericUpDown.Value);
            grupo.Fecha = fechaDateTimePicker.Value;

            //Agregar cada linea del Grid al detalle
            foreach (DataGridViewRow item in detalleDataGridView.Rows)
            {
                grupo.AgregarDetalle(
                    ToInt(item.Cells["Id"].Value),
                    ToInt(item.Cells["GrupoId"].Value),
                    ToInt(item.Cells["PersonaId"].Value),
                    item.Cells["Cargo"].ToString()
                  );
            }
            return grupo;
        }

        private bool Errores()
        {
            bool Errores = false;

            if (detalleDataGridView.RowCount == 0)
            {
                MyerrorProvider.SetError(detalleDataGridView,
                    "Es obligatorio seleccionar las personas");
                Errores = true;
            }

            return Errores;
        }


        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IdnumericUpDown.Value);
            Grupos grupos = RegistroConDetalle.BLL.GruposBLL.Buscar(id);

            if (grupos != null)
            {
                LlenarCampos(grupos);
            }
            else
                MessageBox.Show("No se encontro!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            IdnumericUpDown.Value = 0;
            NombretextBox.Clear();
            fechaDateTimePicker.Value = DateTime.Now;
            CargotextBox.Clear();

            detalleDataGridView.DataSource = null;
            MyerrorProvider.Clear();
        }


        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            List<GruposDetalle> detalle = new List<GruposDetalle>();

            if (detalleDataGridView.DataSource != null)
            {
                detalle = (List<GruposDetalle>)detalleDataGridView.DataSource;
            }

            //Agregar un nuevo detalle con los datos introducidos.
            detalle.Add(
                new GruposDetalle(
                    id: 0,
                    grupoId: (int)IdnumericUpDown.Value,
                    personaId: (int)PersonacomboBox.SelectedValue,
                    cargo: (string)CargotextBox.Text
                ));

            //Cargar el detalle al Grid
            detalleDataGridView.DataSource = null;
            detalleDataGridView.DataSource = detalle;

        }

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (detalleDataGridView.Rows.Count > 0
                 && detalleDataGridView.CurrentRow != null)
            {
                //convertir el grid en la lista
                List<GruposDetalle> detalle
                    = (List<GruposDetalle>)detalleDataGridView.DataSource;

                //remover la fila
                detalle.RemoveAt(detalleDataGridView.CurrentRow.Index);

                // Cargar el detalle al Grid
                detalleDataGridView.DataSource = null;
                detalleDataGridView.DataSource = detalle;
            }
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Grupos grupo;
            bool Paso = false;

            if (Errores())
            {
                MessageBox.Show("Favor revisar todos los campos", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            grupo = LlenaClase();

            //Determinar si es Guardar o Modificar
            if (IdnumericUpDown.Value == 0)
                Paso = RegistroConDetalle.BLL.GruposBLL.Guardar(grupo);
            else
                //todo: validar que exista.
                Paso = RegistroConDetalle.BLL.GruposBLL.Modificar(grupo);

            //Informar el resultado
            if (Paso)
            {
                NuevoButton.PerformClick();
                MessageBox.Show("Guardado!!", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo guardar!!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IdnumericUpDown.Value);

            //todo: validar que exista
            if (RegistroConDetalle.BLL.GruposBLL.Eliminar(id))
                MessageBox.Show("Eliminado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
