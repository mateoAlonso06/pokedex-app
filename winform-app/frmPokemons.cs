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
    public partial class frmPokemons : Form
    {
        private List<Pokemon> listaPokemon;
        public frmPokemons()
        {
            InitializeComponent();
        }

        private void frmPokemons_Load(object sender, EventArgs e)
        {
            cargar();
            comboCampo.Items.Add("Número");
            comboCampo.Items.Add("Nombre");
            comboCampo.Items.Add("Descripcion");
            comboCriterio.Enabled = false;
            comboClave.Enabled = false;
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvPokemons.CurrentRow != null)
            {
                Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.UrlImagen);
            }
        }

        public void cargarImagen(string imagen)
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxPokemon.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog();
            cargar();
        }

        private void cargar()
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.listar();
                dgvPokemons.DataSource = listaPokemon;
                ocultarColumnas();
                cargarImagen(listaPokemon[0].UrlImagen);
                extenderCols(dgvPokemons);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void extenderCols(DataGridView Data)
        {
            foreach (DataGridViewColumn column in dgvPokemons.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
            seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;

            frmAltaPokemon alta = new frmAltaPokemon(seleccionado);
            alta.ShowDialog();
            cargar();
        }

        // eliminación física del pokemon del registro | borra de la base de datos
        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }

        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }

        private void ocultarColumnas()
        {
            dgvPokemons.Columns["UrlImagen"].Visible = false;
            dgvPokemons.Columns["Id"].Visible = false;
        }

        private void eliminar(bool logico = false)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            Pokemon seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Desea eliminar al pokemon del registro?", "Eliminando...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
                    if (logico)
                    {
                        negocio.eliminarLogico(seleccionado.Id);
                        MessageBox.Show("Eliminado (Dado de baja)");
                    }
                    else
                    {
                        negocio.eliminar(seleccionado.Id);
                        MessageBox.Show("Eliminado correctamente");
                    }
                    cargar();
                }
                else
                {
                    MessageBox.Show("La operación se ha cancelado");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void btnFiltroRapido_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                string campo = comboCampo.SelectedItem.ToString();
                string criterio = comboCriterio.SelectedItem.ToString();
                string filtro = comboClave.Text;
                dgvPokemons.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Pokemon> listaFiltrada;
            string filtro = txtFiltro.Text.ToLower();

            if (filtro.Length >= 1)
                // permite buscar dentro de la lista
                listaFiltrada = listaPokemon.FindAll(x => x.Nombre.ToLower().Contains(filtro));

            else
                listaFiltrada = listaPokemon;

            dgvPokemons.DataSource = null;
            dgvPokemons.DataSource = listaFiltrada;
            extenderCols(dgvPokemons);
            ocultarColumnas();
        }

        private void comboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = comboCampo.SelectedItem.ToString();
            comboCriterio.Enabled = true;
            if(opcion == "Número")
            {
                comboCriterio.Items.Clear();
                comboCriterio.Items.Add("Mayor a: ");
                comboCriterio.Items.Add("Menor a: ");
                comboCriterio.Items.Add("Igual a: ");
            }
            else
            {
                comboCriterio.Items.Clear();
                comboCriterio.Items.Add("Empieca con: ");
                comboCriterio.Items.Add("Termina con: ");
                comboCriterio.Items.Add("Contiene: ");
            }
        }

        private void comboCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboClave.Enabled = true;
        }
    }
}
