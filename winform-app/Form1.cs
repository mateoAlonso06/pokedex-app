using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace winform_app
{
    public partial class Form1 : Form
    {
        private List<Pokemon> listaPokemon;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PokemonBussines negocio = new PokemonBussines();
            listaPokemon = negocio.Listar();
            dgvPokemon.DataSource = listaPokemon;
            dgvPokemon.Columns["UrlImagen"].Visible = false;
            cargarImagen(listaPokemon[0].UrlImagen);
        }

        private void dgvPokemon_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemon.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                picPokemon.Load(imagen);
            }
            catch (Exception ex)
            {
                picPokemon.Load("https://editorial.unc.edu.ar/wp-content/uploads/sites/33/2022/09/placeholder.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmPokemon alta = new FrmPokemon();
            alta.ShowDialog();
        }
    }
}
