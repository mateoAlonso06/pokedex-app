using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscosDB
{
    public partial class frmDiscos : Form
    {
        private List<Disco> listaDisco;
        public frmDiscos()
        {
            InitializeComponent();
        }

        private void frmDiscos_Load(object sender, EventArgs e)
        {
            DiscosConexion discosConexion = new DiscosConexion();
            listaDisco = discosConexion.charts();
            dgvDiscos.DataSource = listaDisco;
            dgvDiscos.Columns["UrlImagenTapa"].Visible = false;
            cargarImagen(listaDisco[0].UrlImagenTapa);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                imgDisco.Load(imagen);
            }
            catch (Exception ex)
            {
                imgDisco.Load("https://editorial.unc.edu.ar/wp-content/uploads/sites/33/2022/09/placeholder.png");
            }
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Disco seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagenTapa);
        }
    }
}
