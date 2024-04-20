using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace lab05
{
    /// <summary>
    /// Lógica de interacción para Actualizar.xaml
    /// </summary>
    public partial class Actualizar : Window
    {
        public string connectionString = "Data Source=LAB1504-28\\SQLEXPRESS;Initial Catalog=lab5;User Id=reynoso;Password=123";
        public List<Clientes> ListaClientes { get; set; }
        public Actualizar()
        {
            InitializeComponent();
            ListaClientes = new List<Clientes>();
            dataClientes.ItemsSource = ListaClientes;

            dataClientes.SelectionChanged += DataClientes_SelectionChanged;

        }

        private void Button_Actualizar(object sender, RoutedEventArgs e)
        {
            string idCliente = txtIdCliente.Text;
            string nuevoNombreCompañia = txtNuevoNombreCompañia.Text;
            string nuevoNombreContacto = txtNuevoNombreContacto.Text;
            string nuevoCargoContacto = txtNuevoCargoContacto.Text;
            string nuevaDireccion = txtNuevaDireccion.Text;
            string nuevaCiudad = txtNuevaCiudad.Text;
            string nuevoPais = txtNuevoPais.Text;
            string nuevoTelefono = txtNuevoTelefono.Text;

            Clientes clienteActualizado = new Clientes(idCliente, nuevoNombreCompañia, nuevoNombreContacto, nuevoCargoContacto, nuevaDireccion, nuevaCiudad, nuevoPais, nuevoTelefono);
            ListaClientes.Add(clienteActualizado);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("USP_ActualizarClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@IdCliente", idCliente);
                    command.Parameters.AddWithValue("@NuevoNombreCompañia", nuevoNombreCompañia);
                    command.Parameters.AddWithValue("@NuevoNombreContacto", nuevoNombreContacto);
                    command.Parameters.AddWithValue("@NuevoCargoContacto", nuevoCargoContacto);
                    command.Parameters.AddWithValue("@NuevaDireccion", nuevaDireccion);
                    command.Parameters.AddWithValue("@NuevaCiudad", nuevaCiudad);
                    command.Parameters.AddWithValue("@NuevoPais", nuevoPais);
                    command.Parameters.AddWithValue("@NuevoTelefono", nuevoTelefono);

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

        }

        private void Button_Listar(object sender, RoutedEventArgs e)
        {
            List<Clientes> clientes = new List<Clientes>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string idCliente = reader.GetString(reader.GetOrdinal("idCliente"));
                                string nombreCompañia = reader.GetString(reader.GetOrdinal("NombreCompañia"));
                                string nombreContacto = reader.GetString(reader.GetOrdinal("NombreContacto"));
                                string cargoContacto = reader.GetString(reader.GetOrdinal("CargoContacto"));
                                string direccion = reader.GetString(reader.GetOrdinal("Direccion"));
                                string ciudad = reader.GetString(reader.GetOrdinal("Ciudad"));
                                string pais = reader.GetString(reader.GetOrdinal("Pais"));
                                string telefono = reader.GetString(reader.GetOrdinal("Telefono"));

                                clientes.Add(new Clientes(idCliente, nombreCompañia, nombreContacto, cargoContacto, direccion, ciudad, pais, telefono));
                            }
                        }
                    }
                }

                dataClientes.ItemsSource = clientes;
                dataClientes.Visibility = Visibility.Visible;
        }

        private void DataClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataClientes.SelectedItem != null)
            {
                Clientes clienteSeleccionado = (Clientes)dataClientes.SelectedItem;

                txtIdCliente.Text = clienteSeleccionado.idCliente;
                txtNuevoNombreCompañia.Text = clienteSeleccionado.NombreCompañia;
                txtNuevoNombreContacto.Text = clienteSeleccionado.NombreContacto;
                txtNuevoCargoContacto.Text = clienteSeleccionado.CargoContacto;
                txtNuevaDireccion.Text = clienteSeleccionado.Direccion;
                txtNuevaCiudad.Text = clienteSeleccionado.Ciudad;
                txtNuevoPais.Text = clienteSeleccionado.Pais;
                txtNuevoTelefono.Text = clienteSeleccionado.Telefono;
            }
        }

    }
}
