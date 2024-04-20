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
    /// Lógica de interacción para Borrar.xaml
    /// </summary>
    public partial class Borrar : Window
    {
        public string connectionString = "Data Source=LAB1504-28\\SQLEXPRESS;Initial Catalog=lab5;User Id=reynoso;Password=123";
        public List<ClientesBorrados> ListaClientes { get; set; }
        public Borrar()
        {
            InitializeComponent();
            ListaClientes = new List<ClientesBorrados>();
            dataClientes.ItemsSource = ListaClientes;
        }

        private void Button_Borrar(object sender, RoutedEventArgs e)
        {
            string idCliente = txtIdCliente.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("USP_EliminarCliente", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@idCliente", idCliente);

                    int rowsAffected = command.ExecuteNonQuery();
                }
            }

        }

        private void Button_Listar(object sender, RoutedEventArgs e)
        {
            List<ClientesBorrados> clientes = new List<ClientesBorrados>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("USP_ListarClientesBorrados", connection))
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
                                bool estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? false : reader.GetBoolean(reader.GetOrdinal("Estado"));

                                clientes.Add(new ClientesBorrados(idCliente, nombreCompañia, nombreContacto, cargoContacto, direccion, ciudad, pais, telefono, estado));
                            }
                        }
                    }
                }

                dataClientes.ItemsSource = clientes;
                dataClientes.Visibility = Visibility.Visible;
        }   
    }
}
