using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegistroConDetalle.UI.Consultas;
using RegistroConDetalle.UI.Resgistros;

namespace RegistroConDetalle
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rPersonas registro = new rPersonas();
            registro.MdiParent = this;
            registro.Show();
        }

        private void PersonastoolStripButton_Click(object sender, EventArgs e)
        {
            rPersonas registro = new rPersonas();
            registro.MdiParent = this;
            registro.Show();
        }

        private void GrupotoolStripButton_Click(object sender, EventArgs e)
        {
            rGrupos registro = new rGrupos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rGrupos registro = new rGrupos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void personasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cPersonas consulta = new cPersonas();
            consulta.MdiParent = this;
            consulta.Show();
        }

        private void gruposToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cGrupos consulta = new cGrupos();
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}
